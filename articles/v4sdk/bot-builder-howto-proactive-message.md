---
title: Send proactive notifications to users - Bot Service
description: Learn how bots send notification messages. See how to retrieve conversation references and test proactive messages. View code samples and design considerations.
keywords: proactive message, notification message, bot notification, 
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 11/23/2020
monikerRange: 'azure-bot-service-4.0'
---

# Send proactive notifications to users

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Typically, a bot sends a message to a user directly in response to receiving a message from the user.
Occasionally, a bot might need to send a _proactive message_, a message in response to stimulus not originating from the user.

Proactive messages can be useful in a variety of scenarios. For example, if the user has previously asked the bot to monitor the price of a product, the bot can alert the user if the price of the product has dropped by 20%. Or, if a bot requires some time to compile a response to the user's question, it may inform the user of the delay and allow the conversation to continue in the meantime. When the bot finishes compiling the response to the question, it will share that information with the user.

> [!Note]
> This article covers information about proactive messages for bots in general. For information about proactive messages in Microsoft Teams, see:
> - The **Teams conversation bot** sample in [**C#**](https://aka.ms/cs-teams-conversations-sample), [**JavaScript**](https://aka.ms/js-teams-conversations-sample), or [**Python**](https://aka.ms/py-teams-conversations-sample).
> - Microsoft Teams documentation on how to [send proactive messages](/microsoftteams/platform/bots/how-to/conversations/send-proactive-messages).

## Requirements

Before you can send a proactive message, your bot needs a _conversation reference_. Your bot can retrieve the conversation reference from any activity it has received from the user, but this typically requires the user to interact with the bot at least once before the bot can send a proactive message.

Many channels prohibit a bot from messaging a user unless the user has messaged the bot at least once. Some channels allow exceptions. For instance, the Teams channel allows your bot to send a proactive (or 1-on-1) message to individuals in an already established group conversation that includes the bot.

## Prerequisites

- Understand [bot basics](bot-builder-basics.md).
- A copy of the **proactive messages** sample in [**C#**](https://aka.ms/proactive-sample-cs), [**JavaScript**](https://aka.ms/proactive-sample-js), or [**Python**](https://aka.ms/bot-proactive-python-sample-code). The sample is used to explain proactive messaging in this article.

## About the proactive sample

In general, a bot as an application has a few layers:

- The web application that can accept HTTP requests and specifically supports a messaging endpoint.
- An adapter that handles connectivity with the channels.
- A handler for the turn, typically encapsulated in a _bot_ class that handles the conversational reasoning for the bot app.

In response to an incoming message from the user, the app calls the adapter's _process activity_ method, which creates a turn and turn context, calls its middleware pipeline, and then calls the bot's turn handler.

To initiate a proactive message, the bot application needs to be able to receive additional input.
The application logic for initiating a proactive message is outside the scope of the SDK.
For this sample, a _notify_ endpoint, in addition to a standard _messages_ endpoint, is used to trigger the proactive turn.

In response to a GET request on this notify endpoint, the app calls the adapter's _continue conversation_ method, which behaves similarly to the the _process activity_ method. The _continue conversation_ method:

- Takes an appropriate conversation reference for the user and the callback method to use for the proactive turn.
- Creates an event activity and turn context for the proactive turn.
- Calls the adapter's middleware pipeline.
- Calls the provided callback method.
- The turn context uses the conversation reference to send any messages to the user.

The sample has a bot, a messages endpoint, and an additional notify endpoint that is used to send proactive messages to the user, as shown in the following illustration.

![proactive bot](media/proactive-sample-bot.png)

## Retrieve and store conversation reference

When the Emulator connects to the bot, the bot receives two conversation update activities. In the bot's conversation update activity handler, the conversation reference is retrieved and stored in a dictionary as shown below.

# [C#](#tab/csharp)

**Bots\ProactiveBot.cs**

[!code-csharp[OnConversationUpdateActivityAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/16.proactive-messages/Bots/ProactiveBot.cs?range=26-37&highlight=3-4,9)]

# [JavaScript](#tab/javascript)

**bots/proactiveBot.js**

[!code-javascript[onConversationUpdateActivity](~/../botbuilder-samples/samples/javascript_nodejs/16.proactive-messages/bots/proactiveBot.js?range=13-17&highlight=2)]

[!code-javascript[onConversationUpdateActivity](~/../botbuilder-samples/samples/javascript_nodejs/16.proactive-messages/bots/proactiveBot.js?range=41-44&highlight=2-3)]

# [Python](#tab/python)

**bots/proactive_bot.py**
[!code-python[on_conversation_update_activity](~/../botbuilder-samples/samples/python/16.proactive-messages/bots/proactive_bot.py?range=14-16&highlight=2)]

[!code-python[on_conversation_update_activity](~/../botbuilder-samples/samples/python/16.proactive-messages/bots/proactive_bot.py?range=35-45)]

---

The conversation reference includes a _conversation_ property that describes the conversation in which the activity exists. The conversation includes a _user_ property that lists the users participating in the conversation, and a _service URL_ property that indicates where replies to the current activity may be sent. A valid conversation reference is needed to send proactive messages to users. (For the Teams channel, the service URL maps to a regionalized server.)

> [!NOTE]
> In a real-world scenario you would persist conversation references in a database instead of using an object in memory.

## Send proactive message

The second controller, the _notify_ controller, is responsible for sending the proactive message to the user. It uses the following steps to generate a proactive message.

1. Retrieves the reference for the conversation to which to send the proactive message.
1. Calls the adapter's _continue conversation_ method, providing the conversation reference and the turn handler delegate to use. (The continue conversation method generates a turn context for the referenced conversation and then calls the specified turn handler delegate.)
1. In the delegate, uses the turn context to send the proactive message. Here, the delegate is defined on the notify controller, and it sends the proactive message to the user.

> [!NOTE]
> While each channel should use a stable service URL, the URL can change over time. For more information about the service URL, see the [Basic activity structure](https://github.com/microsoft/botframework-sdk/blob/master/specs/botframework-activity/botframework-activity.md#basic-activity-structure) and [Service URL](https://github.com/microsoft/botframework-sdk/blob/master/specs/botframework-activity/botframework-activity.md#service-url) sections of the Bot Framework Activity Schema.
>
> If the service URL changes, previous conversation references will no longer be valid and calls to _continue conversation_ will generate an error or exception. In this case, your bot will need to acquire a new conversation reference for the user before it can send proactive messages again.

# [C#](#tab/csharp)

**Controllers\NotifyController .cs**

Each time the bot's notify page is requested, the notify controller retrieves the conversation references from the dictionary.
The controller then uses the `ContinueConversationAsync` and `BotCallback` methods to send the proactive message.

[!code-csharp[Notify logic](~/../botbuilder-samples/samples/csharp_dotnetcore/16.proactive-messages/Controllers/NotifyController.cs?range=17-54&highlight=20,32-37)]

# [JavaScript](#tab/javascript)

**index.js**

Each time the server's `/api/notify` page is requested, the server retrieves the conversation references from the dictionary.
The server then uses the `continueConversation` method to send the proactive message.
The parameter to `continueConversation` is a function that serves as the bot's turn handler for this turn.

[!code-javascript[Notify logic](~/../botbuilder-samples/samples/javascript_nodejs/16.proactive-messages/index.js?range=70-84&highlight=4-8)]

# [Python](#tab/python)

Each time the bot's notify page is requested, the server retrieves the conversation references from the dictionary.
The server then uses the `_send_proactive_message` to send the proactive message.

[!code-python[Notify logic](~/../botbuilder-samples/samples/python/16.proactive-messages/app.py?range=98-106&highlight=5-9)]

---

To send a proactive message, the adapter requires an app ID for the bot. In a production environment, you can use the bot's app ID. To test the bot locally with the Emulator, you can use the empty string ("").

## Test your bot

1. If you have not done so already, install the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme).
1. Run the sample locally on your machine.
1. Start the Emulator and connect to your bot.
1. Load to your bot's api/notify page. This will generate a proactive message in the Emulator.

## Additional information

Besides the sample used in this article, additional samples are available on [GitHub](https://github.com/Microsoft/BotBuilder-Samples/).

### Design considerations

When implementing proactive messages in your bot, don't send several proactive messages within a short amount of time. Some channels enforce restrictions on how frequently a bot can send messages to the user, and will disable the bot if it violates those restrictions.

An ad hoc proactive message is the simplest type of proactive message. The bot simply interjects the message into the conversation whenever it is triggered, without any regard for whether the user is currently engaged in a separate topic of conversation with the bot and will not attempt to change the conversation in any way.

To handle notifications more smoothly, consider other ways to integrate the notification into the conversation flow, such as setting a flag in the conversation state or adding the notification to a queue.

### About the proactive turn

The _continue conversation_ method uses the conversation reference and a turn callback handler to:

1. Create a turn in which the bot application can send the proactive message. The adapter creates an `event` activity for this turn, with its name set to "ContinueConversation".
1. Send the turn through the adapter's middleware pipeline.
1. Call the turn callback handler to perform custom logic.

In the **proactive messages** sample, the turn callback handler is defined in the notify controller and sends the message directly to the conversation, without sending the proactive activity through the bot's normal turn handler.
The sample code also does not access or update the bot's state on the proactive turn.

Many bots are stateful and use state to manage a conversation over multiple turns.
When the continue conversation method creates a turn context, the turn will have the correct user and conversation state associated with it, and you can integrate proactive turns into your bot's logic.
If you need the bot logic to be aware of the proactive message, you have a few options for doing so. You can:

- Provide the bot's turn handler as the turn callback handler. The bot will then receive the "ContinueConversation" event activity.
- Use the turn callback handler to add information to the turn context first, and then call the bot's turn handler.

In both of these cases, you will need to design your bot logic to handle the proactive event.

## Next steps

> [!div class="nextstepaction"]
> [Implement sequential conversation flow](bot-builder-dialog-manage-conversation-flow.md)

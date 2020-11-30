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

Typically, each message that a bot sends to the user directly relates to the user's prior input.
In some cases, a bot may need to send the user a message that is not directly related to the current topic of conversation or to the last message the user sent. These types of messages are called _proactive messages_.

Proactive messages can be useful in a variety of scenarios. For example, if the user has previously asked the bot to monitor the price of a product, the bot can alert the user if the price of the product has dropped by 20%. Or, if a bot requires some time to compile a response to the user's question, it may inform the user of the delay and allow the conversation to continue in the meantime. When the bot finishes compiling the response to the question, it will share that information with the user.

## Requirements

Before you can send a proactive message, your bot needs a _conversation reference_. Your bot can retrieve the conversation reference from any activity it has received from the user, but this typically requires the user to interact with the bot at least once before the bot can send a proactive message.

Many channels prohibit a bot from messaging a user unless the user has messaged the bot at least once. Some channels allow exceptions. For instance, the Teams channel allows your bot to send a proactive (or 1-on-1) message to individuals in an already established group conversation that includes the bot.

More information about proactive messages in Teams can be found in these resources:

- The **Teams conversation bot** sample in [**C#**](https://aka.ms/cs-teams-conversations-sample), [**JavaScript**](https://aka.ms/js-teams-conversations-sample), or [**Python**](https://aka.ms/py-teams-conversations-sample).
- Microsoft Teams documentation on how to [send proactive messages](/microsoftteams/platform/bots/how-to/conversations/send-proactive-messages)

## Prerequisites

- Understand [bot basics](bot-builder-basics.md).
- A copy of the **proactive messages** sample in [**C#**](https://aka.ms/proactive-sample-cs), [**JavaScript**](https://aka.ms/proactive-sample-js), or [**Python**](https://aka.ms/bot-proactive-python-sample-code). The sample is used to explain proactive messaging in this article.

## About the proactive sample

The sample has a bot and an additional controller that is used to send proactive messages to the user, as shown in the following illustration.

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
1. In the delegate, uses the turn context to send the proactive message.

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

In the **proactive messages** sample, the turn callback handler sends the message directly to the conversation, without sending the proactive activity through the bot's turn handler.

If you need the bot logic to be aware of the proactive message, you have a few options for doing so. You can:

- Provide the bot's turn handler as the turn callback handler. The bot will then receive the "ContinueConversation" event activity.
- Use the turn callback handler to add information to the turn context, and then call the bot's turn handler.

In both of these cases, you will need to design your bot logic to handle the proactive event.

### Avoiding 401 "Unauthorized" Errors

By default, the Bot Builder SDK adds a `serviceUrl` to the list of trusted host names if the incoming request is authenticated by BotAuthentication. They are maintained in an in-memory cache. If your bot is restarted, a user awaiting a proactive message cannot receive it unless they have messaged the bot again after it restarted.

To avoid this, you must manually add the `serviceUrl` to the list of trusted host names.

# [C#](#tab/csharp)

```csharp
MicrosoftAppCredentials.TrustServiceUrl(serviceUrl);
```

For proactive messaging, `serviceUrl` is the URL of the channel that the recipient of the proactive message is using and can be found in `Activity.ServiceUrl`.

You'll want to add the above code just prior to the the code that sends the proactive message. In the [Proactive Messages Sample](https://aka.ms/proactive-sample-cs), you would put it in `NotifyController.cs` just before `await turnContext.SendActivityAsync("proactive hello");`.

# [JavaScript](#tab/javascript)

```js
MicrosoftAppCredentials.trustServiceUrl(serviceUrl);
```

For proactive messaging, `serviceUrl` is the URL of the channel that the recipient of the proactive message is using and can be found in `activity.serviceUrl`.

You'll want to add the above code just prior to the the code that sends the proactive message. In the [Proactive Messages Sample](https://aka.ms/proactive-sample-js), you would put it in `index.js` just before `await turnContext.sendActivity('proactive hello');`.

# [Python](#tab/python)

```python
MicrosoftAppCredentials.trustServiceUrl(serviceUrl)
```

For proactive messaging, `serviceUrl` is the URL of the channel that the recipient of the proactive message is using and can be found in `activity.serviceUrl`.

You'll want to add the above code just prior to the the code that sends the proactive message. In the [Proactive Messages Sample](https://aka.ms/bot-proactive-python-sample-code), you add it in `app.py` prior sending the *proactive hello* message.

---

## Next steps

> [!div class="nextstepaction"]
> [Implement sequential conversation flow](bot-builder-dialog-manage-conversation-flow.md)

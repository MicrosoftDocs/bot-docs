---
title: Send and receive text message in Bot Framework SDK
description: Learn how to make bots send and receive text messages.
keywords: sending message, message activities, simple text message, message, text message, receive message
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: how-to
ms.date: 12/14/2021
monikerRange: 'azure-bot-service-4.0'
---

# Send and receive text message

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

The primary way your bot will communicate with users, and likewise receive communication, is through **message** activities. Some messages may simply consist of plain text, while others may contain richer content such as cards or attachments. Your bot's turn handler receives messages from the user, and you can send responses to the user from there. The turn context object provides methods for sending messages back to the user. This article describes how to send simple text messages.

Markdown is supported for most text fields, but support may vary by channel.

For a running bot sending and receiving messages, follow the quickstarts at the top of the table of contents or check out the [article on how bots work](bot-builder-basics.md#bot-application-structure), which also links to simple samples available for you to run yourself.

## Send a text message

To send a simple text message, specify the string you want to send as the activity:

# [C#](#tab/csharp)

In the bot's activity handlers, use the turn context object's `SendActivityAsync` method to send a single message response. You can also use the object's `SendActivitiesAsync` method to send multiple responses at once.

```cs
await turnContext.SendActivityAsync($"Welcome!");
```

# [JavaScript](#tab/javascript)

In the bot's activity handlers, use the turn context object's `sendActivity` method to send a single message response. You can also use the object's `sendActivities` method to send multiple responses at once.

```javascript
await context.sendActivity("Welcome!");
```

# [Java](#tab/java)

In the bot's activity handlers, use the turn context object's `sendActivity` method to send a single message response. You can also use the object's `sendActivities` method to send multiple responses at once.

```java
turnContext.sendActivity("Welcome!");
```

# [Python](#tab/python)

In the bot's activity handlers, use the turn context object's `send_activity` method to send a single message response.

```python
await turn_context.send_activity("Welcome!")
```

# [LG](#tab/lg)

**Language Generation** (LG) provides templates that include one or more variations of text that are used for composition and expansion.
One of the variations provided will be picked at random by the LG system.

The following example shows a simple template that includes two variations.

```lg
# GreetingPrefix
- Hi
- Hello
```

---

## Receive a text message

To receive a simple text message, use the *text* property of the *activity* object.

# [C#](#tab/csharp)

In the bot's activity handlers, use the following code to receive a message.

```cs
var responseMessage = turnContext.Activity.Text;
```

# [JavaScript](#tab/javascript)

In the bot's activity handlers, use the following code to receive a message.

```javascript
let text = turnContext.activity.text;
```

# [Java](#tab/java)

In the bot's activity handlers, use the following code to receive a message.

```java
String responseMessage = turnContext.getActivity().getText();
```

# [Python](#tab/python)

In the bot's activity handlers, use the following code to receive a message.

```python
response = context.activity.text
```

# [LG](#tab/lg)

Add the following LG template to your .lg file to receive a message.

```lg
# EchoMessage
You said '${turn.activity.text}'
```

---

## Send a typing indicator

Users expect a timely response to their messages. If your bot performs some long-running task like calling a server or executing a query without giving the user some indication that the bot heard them, the user could get impatient and send additional messages or just assume the bot is broken.

Web Chat and Direct Line channel bots can support the sending of a typing indication to show the user that the message was received and is being processed. Be aware that your bot needs to let the turn end within 15 seconds or the Connector service will timeout. For longer processes read more about sending [proactive messages](bot-builder-howto-proactive-message.md).

The following example demonstrates how to send a typing indication.

# [C#](#tab/csharp)

```csharp
protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
{
    if (string.Equals(turnContext.Activity.Text, "wait", System.StringComparison.InvariantCultureIgnoreCase))
    {
        await turnContext.SendActivitiesAsync(
            new Activity[] {
                new Activity { Type = ActivityTypes.Typing },
                new Activity { Type = "delay", Value= 3000 },
                MessageFactory.Text("Finished typing", "Finished typing"),
            },
            cancellationToken);
    }
    else
    {
        var replyText = $"Echo: {turnContext.Activity.Text}. Say 'wait' to watch me type.";
        await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
    }
}
```

# [JavaScript](#tab/javascript)

```javascript
this.onMessage(async (context, next) => {
    if (context.activity.text === 'wait') {
        await context.sendActivities([
            { type: ActivityTypes.Typing },
            { type: 'delay', value: 3000 },
            { type: ActivityTypes.Message, text: 'Finished typing' }
        ]);
    } else {
        await context.sendActivity(`You said '${ context.activity.text }'. Say "wait" to watch me type.`);
    }
    await next();
});
```

# [Java](#tab/java)

```java
@Override
protected CompletableFuture<Void> onMessageActivity(TurnContext turnContext) {
    if (turnContext.getActivity().getText().toLowerCase().equals("wait")) {
        List<Activity> activities = new ArrayList<Activity>();
        activities.add(new Activity(ActivityTypes.TYPING));
        activities.add(new Activity(ActivityTypes.DELAY) {{setValue(3000);}});
        activities.add(MessageFactory.text("Finished typing", "Finished typing", null));
        return turnContext.sendActivities(activities).thenApply(result -> null);
    } else {

        return turnContext.sendActivity(MessageFactory.text("Echo: " + turnContext.getActivity().getText()))
            .thenApply(sendResult -> null);
    }
}
```

# [Python](#tab/python)

```python
async def on_message_activity(self, turn_context: TurnContext):
    if turn_context.activity.text == "wait":
        return await turn_context.send_activities([
            Activity(
                type=ActivityTypes.typing
            ),
            Activity(
                type="delay",
                value=3000
            ),
            Activity(
                type=ActivityTypes.message,
                text="Finished Typing"
            )
        ])
    else:
        return await turn_context.send_activity(
            f"You said {turn_context.activity.text}.  Say 'wait' to watch me type."
        )
```

# [LG](#tab/lg)

```lg
# TypingIndicator
[Activity
    Type = typing
]
```

---

## Additional resources

- [Activity processing](~/v4sdk/bot-builder-basics.md#the-activity-processing-stack)
- [Message activity section](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md#message-activity)
- [Language Generation](bot-builder-concept-language-generation.md)

## Next steps

> [!div class="nextstepaction"]
> [Add media to messages](./bot-builder-howto-add-media-attachments.md)

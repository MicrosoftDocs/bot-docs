---
title: Send and receive text message - Bot Service
description: Learn about how to send and receive text messages within the Bot Framework SDK.
keywords: sending message, message activities, simple text message, message, text message, receive message  
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/23/2019
monikerRange: 'azure-bot-service-4.0'
---

# Send and receive text message

[!INCLUDE[applies-to](../includes/applies-to.md)]

The primary way your bot will communicate with users, and likewise receive communication, is through **message** activities. Some messages may simply consist of plain text, while others may contain richer content such as cards or attachments. Your bot's turn handler receives messages from the user, and you can send responses to the user from there. The turn context object provides methods for sending messages back to the user. This article describes how to send simple text messages.

Markdown is supported for most text fields, but support may vary by channel.

For a running bot sending and receiving messages, follow the quickstarts at the top of the table of contents or check out the [article on how bots work](bot-builder-basics.md#bot-structure), which also links to simple samples available for you to run yourself.

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

# [Python](#tab/python)

In the bot's activity handlers, use the turn context object's `send_activity` method to send a single message response.

```python
await turn_context.send_activity("Welcome!")
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

# [Python](#tab/python)

In the bot's activity handlers, use the following code to receive a message.

```python
response = context.activity.text
```

---

## Send a typing indicator
Users expect a timely response to their messages. If your bot performs some long-running task like calling a server or executing a query without giving the user some indication that the bot heard them, the user could get impatient and send additional messages or just assume the bot is broken. Web Chat and Direct Line channel bots can support the sending of a typing indication to show the user that the message was received and is being processed.

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

---

## Additional resources

- For more information about activity processing in general, see [activity processing](~/v4sdk/bot-builder-basics.md#the-activity-processing-stack).
- For more information on formatting, see the [message activity section](https://aka.ms/botSpecs-activitySchema#message-activity) of the Bot Framework Activity schema.

## Next steps

> [!div class="nextstepaction"]
> [Add media to messages](./bot-builder-howto-add-media-attachments.md)

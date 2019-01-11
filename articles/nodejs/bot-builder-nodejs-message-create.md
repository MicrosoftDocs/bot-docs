---
title: Create messages | Microsoft Docs
description: Learn how create messages with the Bot Framework SDK for Node.js.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 09/7/2017
monikerRange: 'azure-bot-service-3.0'
---
# Create messages

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

Communication between the bot and the user is through messages. Your bot will send message activities to communicate information to users, and likewise, will also receive message activities from users. Some messages may simply consist of plain text, while others may contain richer content such as text to be spoken, suggested actions, media attachments, rich cards, and channel-specific data.

This article describes some of the commonly-used message methods you can use to enhance your user experience.

## Default message handler

The Bot Framework SDK for Node.js comes with a default message handler built into the [`session`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html) object. This message handler allows you to send and receive text messages between the bot and the user.

### Send a text message

Sending a text message using the default message handler is simple, just call `session.send` and pass in a **string**.

This sample shows how you can send a text message to greet the user.
```javascript
session.send("Good morning.");
```

This sample shows how you can send a text message using JavaScript string template.
```javascript
var msg = `You ordered: ${order.Description} for a total of $${order.Price}.`;
session.send(msg); //msg: "You ordered: Potato Salad for a total of $5.99."
```

### Receive a text message

When a user sends the bot a message, the bot receives the message through the `session.message` property.

This sample shows how to access the user's message.
```javascript
var userMessage = session.message.text;
```

## Customizing a message

To have more control over the text formatting of your messages, you can create a custom [`message`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html) object and set the properties necessary before sending it to the user.

This sample shows how to create a custom `message` object and set the `text`, `textFormat`, and `textLocale` properties.

```javascript
var customMessage = new builder.Message(session)
    .text("Hello!")
    .textFormat("plain")
    .textLocale("en-us");
session.send(customMessage);
```

In cases where you do not have the `session` object in scope, you can use `bot.send` method to send a formatted message to the user.

The `textFormat` property of a message can be used to specify the format of the text. The `textFormat` property can be set to **plain**, **markdown**, or **xml**. The default value for `textFormat` is **markdown**. 

## Message property

The [`Message`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html) object has an internal **data** property that it uses to manage the message being sent. Other properties you set are through the different methods this object expose to you. 

## Message methods

Message properties are set and retrieved through the object's methods. The table below provide a list of the methods you can call to set/get the different **Message** properties.

| Method | Description |
| ---- | ---- | 
| [`addAttachment(attachment:AttachmentType)`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#addattachment) | Adds an attachment to a message|
| [`addEntity(obj:Object)`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#addentity) | Adds an entity to the message. |
| [`address(adr:IAddress)`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#address) | Address routing information for the message. To send user a proactive message, save the message's address in the userData bag. |
| [`attachmentLayout(style:string)`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#attachmentlayout) | Hint for how clients should layout multiple attachments. The default value is 'list'. |
| [`attachments(list:AttachmentType)`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#attachments) | A list of cards or images to send to the user. |
| [`compose(prompts:string[], ...args:any[])`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#compose) | Composes a complex and randomized reply to the user. |
| [`entities(list:Object[])`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#entities) | Structured objects passed to the bot or user. |
| [`inputHint(hint:string)`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#inputhint) | Hint sent to user letting them know if the bot is expecting further input or not. The built-in prompts will automatically populate this value for outgoing messages. |
| [`localTimeStamp((optional)time:string)`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#localtimestamp) | Local time when message was sent (set by client or bot, Ex: 2016-09-23T13:07:49.4714686-07:00.) |
| [`originalEvent(event:any)`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#originalevent) | Message in original/native format of the channel for incoming messages. |
| [`sourceEvent(map:ISourceEventMap)`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#sourceevent) | For outgoing messages can be used to pass source specific event data like custom attachments. |
| [`speak(ssml:TextType, ...args:any[])`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#speak) | Sets the speak field of the message as *Speech Synthesis Markup Language (SSML)*. This will be spoken to the user on supported devices. |
| [`suggestedActions(suggestions:ISuggestedActions `&#124;` IIsSuggestedActions)`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#suggestedactions) | Optional suggested actions to send to the user. Suggested actions will be displayed only on the channels that support suggested actions. |
| [`summary(text:TextType, ...argus:any[])`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#summary) | Text to be displayed as fall-back and as short description of the message content in (e.g.: List of recent conversations.) |
| [`text(text:TextType, ...args:any[])`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#text) | Sets the message text. |
| [`textFormat(style:string)`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#textformat) | Set the text format. Default format is **markdown**. |
| [`textLocale(locale:string)`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#textlocale) | Set the target language of the message. |
| [`toMessage()`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#tomessage) | Gets the JSON for the message. |
| [`composePrompt(session:Session, prompts:string[], args?:any[])`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#composeprompt-1) | Combines an array of prompts into a single localized prompt and then optionally fills the prompts template slots with the passed in arguments. |
| [`randomPrompt(prompts:TextType)`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message.html#randomprompt) | Gets a random prompt from the array of **prompts* that is passed in. |

## Next step

> [!div class="nextstepaction"]
> [Send and receive attachments](bot-builder-nodejs-send-receive-attachments.md)


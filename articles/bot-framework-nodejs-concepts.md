---
title: Key concepts in the Bot Builder SDK for Node.js | Microsoft Docs
description: Learn about key concepts in the Bot Builder SDK for Node.js.
keywords: Bot Framework, Node.js, Bot Builder, SDK, key concepts, core concepts
author: DeniseMak
manager: rstand
ms.topic: develop-nodejs-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 03/08/2017
ms.reviewer:
#ROBOTS: Index
---

# Key concepts in the Bot Builder SDK for Node.js

This article introduces key concepts in the Bot Builder SDK for Node.js.

## Connector

> [!NOTE]
> This content will be updated.

The Bot Framework Connector is a service that connects your bot to multiple channels such as Skype, Facebook
, Slack, and more. 
It facilitates communication between bot and user, by relaying messages from bot to channel and from channel to bot. 

The Bot Builder SDK for Node.js provides the **UniversalBot** and **ChatConnector** classes for configuring the bot to send and receive messages through the Bot Framework Connector.
For an example that demonstrates using these classes, see [Create a bot with the Bot Builder SDK for Node.js](bot-framework-nodejs-getstarted.md).

## Saving State

A key to good bot design is to track the context of a conversation, so that your bot remembers things like the last question the user asked. 
Bots built using Bot Builder SDK are designed to be be stateless so that they can easily be scaled to run across multiple compute nodes. The Bot Framework provides a storage system that stores bot data, so that the bot web service can be scaled. Because of that you should generally avoid the temptation to save state using a global variable or function closure. Doing so will create issues when you want to scale out your bot. Instead, use the following properties of your bot's [session][Session] object to save data relative to a user or conversation:

* **userData** stores information globally for the user across all conversations.
* **conversationData** stores information globally for a single conversation. This data is visible to everyone within the conversation so care should be used to what’s stored there. It’s disabled by default and needs to be enabled using the bots [persistConversationData][PersistConversationData] setting.
* **privateConversationData** stores information globally for a single conversation but it is private data specific to the current user. This data spans all dialogs so it’s useful for storing temporary state that you want cleaned up when the conversation ends.
* **dialogData** persists information for a single dialog instance. This is essential for storing temporary information in between the steps of a [waterfall](bot-framework-nodejs-howto-manage-conversation-flow.md#ask-questions) in a dialog.

See [Saving user data](bot-framework-nodejs-howto-save-user-data.md) for an example that demonstrates how to save user data.

## Messages

Messages can consist of strings, attachments, and rich cards. You use the [send][SessionSend] method to send messages in response to a message from the user. Your bot may call **send()** as many times as it likes in response to a message from the user. <!--TODO: What does "as many times mean"? --> For a guide to how to use message handlers to manage conversation flow, see [Manage conversation flow](bot-framework-nodejs-howto-manage-conversation-flow.md).

For an example that demonstrates how to send a rich graphical card containing interactive buttons that the user clicks to initiate an action, see [Send a rich card](bot-framework-nodejs-howto-send-card-buttons.md). For an example that demonstrates how to send and receive attachments, see [Send attachments](bot-framework-nodejs-howto-send-receive-attachments.md).



### Message ordering

<!-- TODO -- Is there any reason to call it auto-batching? -->
When sending multiple replies, the individual replies will be automatically grouped into a batch and delivered to the user as a set in an effort to preserve the original order of the messages. This automatic batching waits a default of 250ms after every call to **send()** before initiating the next call to **send()**.

<!-- TODO: Find out if we ever really need to do the following:
To avoid a 250ms pause after the last call to send() you can manually trigger delivery of the batch by calling session.sendBatch(). In practice it’s rare that you actually need to call sendBatch() as the built-in prompts and session.endConversation() automatically call sendBatch() for you.
--> 

The goal of batching is to try and avoid multiple messages from the bot being displayed out of order. <!-- Unfortunately, not all channels can guarantee this. --> In particular, some channels tend to download images before displaying a message to the user so that if you send a message containing an image followed immediately by a message without images you’ll sometimes see the messages flipped in the user's feed. To minimize the chance of this you can try to insure that your images are coming from content deliver networks (CDNs) and avoid the use of overly large images. In extreme cases you may even need to insert a 1-2 second delay between the message with the image and the one that follows it. You can make this delay feel a bit more natural to the user by calling **session.sendTyping()** before starting your delay. To learn more about sending a typing indicator, see [How to send a typing indicator](bot-framework-nodejs-howto-send-typing-indicator.md).


The message batching delay is configurable. To disable the SDK’s auto-batching logic, set the default delay to a large number and then manually call **sendBatch()** with a callback to invoke after the batch is delivered.

> [!NOTE]
> Additional content coming soon...



## Next steps

Build your first bot by following the steps at [Get started](bot-framework-nodejs-getstarted.md).


## Additional Resources

* [Manage conversation flow](bot-framework-nodejs-howto-manage-conversation-flow.md)
* [Send a rich card](bot-framework-nodejs-howto-send-card-buttons.md)
* [Send attachments](bot-framework-nodejs-howto-send-receive-attachments.md)
* [Saving user data](bot-framework-nodejs-howto-save-user-data.md)
* [How to send a typing indicator](bot-framework-nodejs-howto-send-typing-indicator.md)
* [session object][Session]
* [session.send][SessionSend]
* [session.sendTyping][SessionSendTyping]


<!-- TODO: Update links to point to new docs -->

[PersistConversationData]:(https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iuniversalbotsettings.html#persistconversationdata)

[Session]: (https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html
[SessionSend]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#send
[SessionSendTyping]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#sendtyping

[waterfall]:(../articles/bot-framework-nodejs-howto-manage-conversation-flow.md#ask-questions)
[SaveUserData]:(../articles/bot-framework-nodejs-howto-save-user-data.md)
[GetStarted]:(../articles/bot-framework-nodejs-getstarted.md)




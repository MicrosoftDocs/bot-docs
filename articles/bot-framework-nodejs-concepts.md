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

The Bot Framework Connector is a service that connects your bot to multiple channels such as Skype, Email, Slack, and more. 
It facilitates communication between bot and user, by relaying messages from bot to channel and from channel to bot. 

The Bot Builder SDK for Node.js provides the **UniversalBot** and **ChatConnector** classes for configuring the bot to send and receive messages through the Bot Framework Connector.
For an example that demonstrates using these classes, see [Create a bot with the Bot Builder SDK for Node.js](bot-framework-nodejs-getstarted.md).

## Saving State

A key to good bot design is to track the context of a conversation, so that your bot remembers things like the last question the user asked. 
Bots built using Bot Builder SDK are designed to be be stateless so that they can easily be scaled to run across multiple compute nodes. The Bot Framework provides a storage system that stores bot data, so that the bot web service can be scaled. Because of that you should generally avoid the temptation to save state using a global variable or function closure. Doing so will create issues when you want to scale out your bot. Instead, use the following properties of your bot's session object to save data relative to a user or conversation:

* **userData** stores information globally for the user across all conversations.
* **conversationData** stores information globally for a single conversation. This data is visible to everyone within the conversation so care should be used to what’s stored there. It’s disabled by default and needs to be enabled using the bots [persistConversationData][PersistConversationData] setting.
* **privateConversationData** stores information globally for a single conversation but it is private data specific to the current user. This data spans all dialogs so it’s useful for storing temporary state that you want cleaned up when the conversation ends.
* **dialogData** persists information for a single dialog instance. This is essential for storing temporary information in between the steps of a [waterfall](bot-framework-nodejs-howto-manage-conversation-flow.md#ask-questions) in a dialog.

See [Saving user data](bot-framework-nodejs-howto-save-user-data.md) for an example that demonstrates how to save user data.

## Messages

Messages can consist of strings, attachments, and rich cards. You use the **session.send()** method to send messages in response to a message from the user. Your bot may call **send()** as many times as it likes in response to a message from the user. <!--TODO: What does "as many times mean"? --> For a guide to how to use message handlers to manage conversation flow, see [Manage conversation flow](bot-framework-nodejs-howto-manage-conversation-flow.md).

For an example that demonstrates how to send a rich graphical card containing interactive buttons that the user clicks to initiate an action, see [Send a rich card](bot-framework-nodejs-howto-send-card-buttons.md). For an example that demonstrates how to send and receive attachments, see [Send attachments](bot-framework-nodejs-howto-send-receive-attachments.md).

When sending multiple replies, the individual replies will be automatically grouped into a batch and delivered to the user as a set in an effort to preserve the original order of the messages.


> [!NOTE]
> Additional content coming soon...



## Next steps

Build your first bot by following the steps at [Get started](bot-framework-nodejs-getstarted.md).




<!-- TODO: Update links to point to new docs -->

[PersistConversationData]:(https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iuniversalbotsettings.html#persistconversationdata)
[waterfall]:(../articles/bot-framework-nodejs-howto-manage-conversation-flow.md#ask-questions)
[SaveUserData]:(../articles/bot-framework-nodejs-howto-save-user-data.md)
[GetStarted]:(../articles/bot-framework-nodejs-getstarted.md)




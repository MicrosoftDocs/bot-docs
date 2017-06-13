---
title: Key concepts in the Bot Builder SDK for Node.js | Microsoft Docs
description: Understand the key concepts and tools for building and deploying conversational bots available in the Bot Builder SDK for Node.js.
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---

# Key concepts in the Bot Builder SDK for Node.js
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-concepts.md)
> - [Node.js](../nodejs/bot-builder-nodejs-concepts.md)

This article introduces key concepts in the Bot Builder SDK for Node.js. For an introduction to Bot Framework, see [Bot Framework overview](../overview-introduction-bot-framework.md).

## Connector

The Bot Framework Connector is a service that connects your bot to multiple *channels*, which are clients like Skype, Facebook, Slack, and SMS. 
The Connector facilitates communication between bot and user by relaying messages from bot to channel and from channel to bot. 
Your bot's logic is hosted as a web service that receives messages from users through the Connector service, and your bot's replies are sent to the Connector using HTTPS POST. 

The Bot Builder SDK for Node.js provides the [UniversalBot][UniversalBot] and [ChatConnector][ChatConnector] classes for configuring the bot to send and receive messages through the Bot Framework Connector. The `UniversalBot` class forms the brains of your bot. It's responsible for managing all the conversations your bot has with a user. The `ChatConnector` class connects your bot to the Bot Framework Connector Service.
For an example that demonstrates using these classes, see [Create a bot with the Bot Builder SDK for Node.js](bot-builder-nodejs-quickstart.md).

The Connector also normalizes the messages that the bot sends to channels so that you can develop your bot in a platform-agnostic way. Normalizing a message involves converting it from the Bot Framework’s schema into the channel’s schema. In cases where the channel does not support all aspects of the framework’s schema, the Connector will try to convert the message to a format that the channel supports. For example, if the bot sends a message that contains a card with action buttons to the SMS channel, the Connector may render the card as an image and include the actions as links in the message’s text. The [Channel Inspector][ChannelInspector] is a web tool that shows you how the Connector renders messages on various channels.

## Messages

Messages can consist of text to be displayed, text to be spoken, attachments, rich cards, and suggested actions. You use the [session.send][SessionSend] method to send messages in response to a message from the user. Your bot may call `send` as many times as it likes in response to a message from the user. For an example that demonstrates this, see [Respond to user messages][RespondMessages].

For an example that demonstrates how to send a rich graphical card containing interactive buttons that the user clicks to initiate an action, see [Add rich cards to messages](bot-builder-nodejs-send-rich-cards.md). For an example that demonstrates how to send and receive attachments, see [Send attachments](bot-builder-nodejs-send-receive-attachments.md). For an example that demonstrates how to send a message that specifies text to be spoken by your bot on a speech-enabled channel, see [Add speech to messages](bot-builder-nodejs-text-to-speech.md). For an example that demonstrates how to send suggested actions, see [Send suggested actions](bot-builder-nodejs-send-suggested-actions.md).

## Dialogs
Dialogs help you organize the conversational logic in your bot and are fundamental to [designing conversation flow](../bot-design-conversation-flow.md). For an introduction to dialogs, see [Manage a conversation with dialogs](bot-builder-nodejs-dialog-manage-conversation.md).

## Actions
You'll want to design your bot to be able to handle interruptions like requests for cancellation or help at any time during the conversation flow. The Bot Builder SDK for Node.js provides global message handlers that trigger actions like cancellation or invoking other dialogs. 
 See <!--[Handling cancel](bot-builder-nodejs-manage-conversation-flow.md#handling-cancel), [Confirming interruptions](bot-builder-nodejs-manage-conversation-flow.md#confirming-interruptions) and-->[Listen for messages using actions](bot-builder-nodejs-global-handlers.md) for examples of how to use [triggerAction][triggerAction] handlers.


## Recognizers
When users ask your bot for something, like "help" or "find news", your bot needs to understand what the user is asking for and then take the appropriate action. You can design your bot to recognize intents based on the user’s input and associate that intent with actions. 

You can use the built-in regular expression recognizer that the Bot Builder SDK provides, call an external service such as the LUIS API, or implement a custom recognizer to determine the user's intent. 
See [Recognize user intent](bot-builder-nodejs-recognize-intent.md) for examples that demonstrate how to add recognizers to your bot and use them to trigger actions.


## Saving State

A key to good bot design is to track the context of a conversation, so that your bot remembers things like the last question the user asked. 
Bots built using Bot Builder SDK are designed to be be stateless so that they can easily be scaled to run across multiple compute nodes. The Bot Framework provides a storage system that stores bot data, so that the bot web service can be scaled. Because of that you should generally avoid saving state using a global variable or function closure. Doing so will create issues when you want to scale out your bot. Instead, use the following properties of your bot's [session][Session] object to save data relative to a user or conversation:

* **userData** stores information globally for the user across all conversations.
* **conversationData** stores information globally for a single conversation. This data is visible to everyone within the conversation so exercise with care when storing data to this property. It’s disabled by default and needs to be enabled using the bots [persistConversationData][PersistConversationData] setting.
* **privateConversationData** stores information globally for a single conversation but it is private data specific to the current user. This data spans all dialogs so it’s useful for storing temporary state that you want cleaned up when the conversation ends.
* **dialogData** persists information for a single dialog instance. This is essential for storing temporary information in between the steps of a [waterfall](bot-builder-nodejs-dialog-waterfall.md) in a dialog.

For an example that demonstrates how user data are saved, see [Saving user data](bot-builder-nodejs-save-user-data.md).

## Natural language understanding

Bot Builder lets you use LUIS to add natural language understanding to your bot using the [LuisRecognizer][LuisRecognizer] class. You can add an instance of a **LuisRecognizer** that references your published language model and then add handlers to take actions in response to the user's utterances. To see LUIS in action watch the following 10 minute tutorial:

* [Microsoft LUIS Tutorial][LUISVideo] (video)

## Additional resources

The following articles walk you through building your first bot and adding basic conversational abilities to it.

* [Create a bot with the Bot Builder SDK for Node.js](bot-builder-nodejs-quickstart.md)
* [Send and receive messages](bot-builder-nodejs-use-default-message-handler.md)
* [Prompt users for input](bot-builder-nodejs-prompts.md)
* [Listen for messages by using actions](bot-builder-nodejs-global-handlers.md)

The following task-focused articles demonstrate features of the Bot Builder SDK for Node.js.

* [Recognize user intent](bot-builder-nodejs-recognize-intent.md)
* [Add rich cards to messages](bot-builder-nodejs-send-rich-cards.md)
* [Send and receive attachments](bot-builder-nodejs-send-receive-attachments.md)
* [Add speech to messages](bot-builder-nodejs-text-to-speech.md)
* [Add suggested actions to messages](bot-builder-nodejs-send-suggested-actions.md)
* [Saving user data](bot-builder-nodejs-save-user-data.md)
* [Send a typing indicator](bot-builder-nodejs-send-typing-indicator.md)


[PersistConversationData]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iuniversalbotsettings.html#persistconversationdata
[UniversalBot]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot.html
[ChatConnector]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.chatconnector.html

[ChannelInspector]: https://docs.botframework.com/en-us/channel-inspector/channels/Skype

[Session]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html
[SessionSend]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#send
[SessionSendTyping]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#sendtyping
[triggerAction]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialog.html#triggeraction
[waterfall]: bot-builder-nodejs-prompts.md
[SaveUserData]: bot-builder-nodejs-save-user-data.md
[GetStarted]: bot-builder-nodejs-quickstart.md
[RespondMessages]:bot-builder-nodejs-use-default-message-handler.md

[LUISRecognizer]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.luisrecognizer
[LUISVideo]: https://vimeo.com/145499419

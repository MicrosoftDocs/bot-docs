---
layout: page
title: Building your Bot Using the Bot Connector REST API
permalink: /en-us/connector/overview/
weight: 3000
parent1: Building your Bot Using the Bot Connector REST API
---


Microsoft Bot Framework is a comprehensive offering that you use to build and deploy high quality bots for your users to enjoy wherever they are talking. Bot Connector is a component of the framework, and it's the piece that connects your bot to users on channels that you configured your bot to work on. The connector service sits between your bot and the channels, and passes the messages between them. The connector also normalizes the messages that the bot sends the channel, if necessary. Normalizing a message involves converting it from the framework's schema into the channel's schema. In cases where the channel does not support all aspects of the framework's schema, the connector will try to convert the message to a format that the channel supports. For example, if the bot sends a message that contains a card with action buttons to the SMS channel, the connector may render the card as an image and include the actions in the message's text (for example, Reply with 'Confirm'). 

To send messages to and receive messages from the connector, you use the [Bot Connector REST API](../reference). The API is a collection of endpoints and JSON objects that you use to start conversations, send messages, add attachments, and get members of the conversation among other actions. 

The following sections provide details about building your bot by using the REST API.

|[Authentication](../authentication)|To use the REST API, each call must include an Authorization header and access token. This section describes how to get an access token, and how to verify that the messages you receive are coming from the bot connector service.
|[Starting a conversation](../conversation)|The user starts most conversations but sometimes you may want to start the conversation. For example, if you know the user is interested in a topic and you want to alert them to a related event or news article. This section describes how to start a conversation.
|[Sending and receiving messages](../messages)|A conversation is a series of messages between your bot and the user. This section describes a message and how to send a reply.
|[Adding attachments to a message](../attachments)|A message can be a simple text string or something more complex such as a Hero card that contains text, images, and action buttons. This section describes how to add attachments such as images, links, and rich cards.
|[Adding channel-specific attachments](../channeldata)|Some channels provide features that require additional data that can't be described using the attachment schema. This section describes how to add channel-specific attachments to a message.
|[Saving user data](,,/userdata)|A bot may save data about the user, a conversation, or a single user within a conversation. This section describes how to save user data.

For a quick start guide that covers these topics, see [Quick Start](/en-us/connector/getstarted/).

The framework also provides .NET and Node developers Bot Builder SDKs that they can use to build their bots. The SDKs are provided as libraries and as open source on GitHub (see [Bot Framework Downloads](/en-us/downloads/)). In addition to modeling the connector service, the SDKs also provide dialogs and form flows that you can use for guided conversations such as ordering a sandwhich. For details about using the SDKs, see [Bot Builder for .NET](../../csharp/builder/overview) and [Bot Builder for Node.js](../../csharp/builder/overview).

One of the keys to building a great bot is effectively determining the user's intent when they ask your bot to do something. Determining intent can be handled by using regular expressions or by using a natural language service such as Microsoft's Language Understanding Intelligent Service [LUIS]( https://www.luis.ai/). [Read more](/en-us/natural-language/)

For other services that let you add intelligence to your bot, such the Vision, Speech, Language, and Search APIs, see [Bot Intelligence](../../bot-intelligence/getting-started) and [Cognitive Services](https://www.microsoft.com/cognitive-services/en-us/apis).


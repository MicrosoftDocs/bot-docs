---
title: Bot Builder SDK concepts | Microsoft Docs
description: Bot Builder SDK key concepts.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/14/2018
monikerRange: 'azure-bot-service-4.0'
---
<!--
    Organization: An attempted narrative flow, skirting [most] language-specific implementation details.
    Try to keep each section to one to three paragraphs of "reasonable" length.
-->

# Key concepts in the Bot Builder SDK
This article introduces key concepts in the Bot Builder SDK, a component of the [Azure Bot Service](bot-service-overview.md).

<!--For a description of the basic architecture of a bot, see [Bot architecture](bot-architecture.md). -->

## The Bot Connector Service and channels

The Bot Connector Service allows communication between your bot application and channels such as Skype, Email, Slack, and others. The connector service is also a component of the Azure Bot Service.

The connector service normalizes messages that the bot sends to a channel, allowing you to develop your bot in a channel-agnostic way. Normalizing a message involves converting it from the bot builder schema into the channel’s schema. In cases where the channel does not support all aspects of the bot builder schema, the service will try to convert the message to a format that the channel supports. For example, if the bot sends a message that contains a card with action buttons to the SMS channel, the connector may send the card as an image and include the actions as links in the message’s text.

The [Channel Inspector](https://docs.botframework.com/en-us/channel-inspector/channels/Skype/) is a web tool that shows you how the connector renders messages on various channels.

<!--Q:Do we need to discuss Direct Line at all?-->

### Activities and conversations
The Bot Connector Service uses JSON to exchange information between the bot and the user. The Bot Builder SDK wraps this information in a language-specific _activity_ object. The most common type of activity is a message, but there are other activity types that indicate when a participant has been added or removed from a conversation, when the bot has been added or removed from a user's contact list, and so on.

When your bot application receives an activity, your application converts the activity to a language-appropriate object. Depending on the elements of the SDK and the language you are using, this conversion may be automated for you.
<!--Link to Activities overview.-->

Every activity belongs to a logical _conversation_. A conversation is specific to a channel and has an ID that is unique to that channel. A conversation represents an interaction between one or more bots and a specific user or group of users.
<!--Link to Conversations overview.-->

## The Bot Builder SDK
The SDK provides a variety of ways to build a bot application. There will be some variation in how your application code is built into your bot, based on the language-specific library you use and the particular elements of the SDK you use.

The following elements of the SDK are important to understand, whether they are called implicity or explicity by your code.

### Your bot application
Aside from the components used to communicate with the Bot Connector Service, your bot application contatins your application-specific logic and a collection of more application-independent middleware components. Incoming and outgoing activities are managed in the SDK by a bot adapter.

<!--
### Connectors
Do we need to discuss connectors for the connector-only version of a bot?
-->

### The bot adapter
The _bot adapter_ encapsulates authentication processes and sends activites to the Bot Connector Service and receives activities from it. When your bot receives an activity, the adapter creates a [context object](#context), passes it to your bot's middleware and application logic, and sends responses back to the user's channel.

The context object includes the activity in the context's request property. To provide application-specific logic to process incoming activities, you implement a callback method.
<!--Link to adapter topic if we have it -->

### Authentication
<!--[In the non-connector-only case,-->The adapter authenticates each incoming activity the application receives, using information from the activity and the `Authentication` header from the REST request.

The adapter uses a _connector_ object and your application's credentials to authenticate the outbound activities to the user.
<!--
The SDK provides connectors for known channels. What about Direct Line clients? Is this all automated for us by the Bot Connector Service?
-->

Bot Connector Service authentication uses JWT (JSON Web Token) `Bearer` tokens and the **MicrosoftAppID** and **MicrosoftAppPassword** that Azure creates for you when you create a bot service or register your bot. Your application will need these credantials at initialization time, so that the adapter can authenticate traffic.

> ![NOTE] If you are running or testing your bot locally, you can do so without configuring the adapter to authenticate traffic to and from your bot.

### Context
A _context object_ captures information about the incoming activity, any outgoing activities, the sender and receiver, the channel, the conversation, and other data needed to process the activity. When an adapter receives an activity, it generates a context object.  

The adapter passes the context object to its middleware and to the application's receive callback method. Any middleware you use or design will implement a method that takes a context object. Similarly, any receive callback method you define for your application will also take a context object. 
<!-- Add specific method name that middleware must implement + Context Object Topic link -->

You use the context object to retrieve information about the activity, the user, the conversation, and so on. Each received activity is processed independently of other incoming activities. Use _state middleware_ to implement state persistence between activities.

To start or resume a conversation, use the adapter's _create conversation_ or _continue conversation_ methods, respectively. To create a response activity, use the context's _reply_ method.

### Middleware
Middleware are reusable components that you can add to the adapter at initialization. Middleware allows you to do additional processing on activities before or after your _recieve callback_ method. The middleware can participate in the pipeline by implementing one or more of the _context created_, _receive activity_, and _send activity_ methods. The adapter adds to the context object's responses property any outgoing activities from the bot, and the adapter sends these activities back to the user. 

<!-- TODO: Link to Middleware topic -->

## State
State represents information that your bot saves in order to respond appropriately to incoming messages. You can use state to store information about the progress of a multi-step interaction, or a user's preferences, status updates, and so on.

Bot adapters are designed to be stateless so that they can easily be scaled to run across multiple compute nodes.
The SDK provides state middleware to persist data, and includes Azure Table Storage, file storage, and memory storage that you can use for data storage. You can also create your own storage components for your bot.

<!-- TODO: Add link to state content + PR 193, if accepted will impact content -->

> ![NOTE] File and memory storage don't scale across nodes, and you should generally avoid saving state using a global variable or function closures. Doing so will create issues when you want to scale out your bot. Instead, add stage manager middleware to your adapter, and use the state property of the context object to save data relative to a user or conversation.

## Next steps
<!-- TODO: Add next steps -->
If you haven't already, follow a quickstart and use the emulator to get a basic bot running in a local environment.
---
title: Basics of the Microsoft Bot Framework
description: Become familiar with the Microsoft Bot Framework. Understand how bots communicate with users, and learn about activities, channels, HTTP POST requests, and more.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: conceptual
ms.date: 10/26/2022
monikerRange: 'azure-bot-service-4.0'
ms.custom: abs-meta-21q1
---

# Basics of the Microsoft Bot Framework

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

A bot is an app that users interact with in a conversational way, using text, graphics (such as cards or images), or speech. Azure AI Bot Service is a cloud platform. It hosts bots and makes them available to _channels_, such as Microsoft Teams, Facebook, or Slack.

The Bot Framework Service, which is a component of the Azure AI Bot Service, sends information between the user's bot-connected app and the bot. Each channel can include additional information in the activities they send. Before creating bots, it's important to understand how a bot uses activity objects to communicate with its users.

This diagram illustrates two activity types, _conversation update_ and _message_, that might be exchanged when a user communicates with an echo bot.

:::image type="content" source="media/bot-builder-activity.png" alt-text="activity diagram":::

The Bot Framework Service sends a _conversation update_ when a party joins the conversation. For example, on starting a conversation with the Bot Framework Emulator, you might see two conversation update activities (one for the user joining the conversation and one for the bot joining). To distinguish these conversation update activities, check who is included in the _members added_ property of the activity.

The _message_ activity carries conversation information between the parties. In an echo bot example, the message activities are carrying simple text and the channel will render this text. Alternatively, the message activity might carry text to be spoken, suggested actions or cards to be displayed.

> [!TIP]
> It's up to each channel to implement the Bot Framework protocol, and how each channel does so might be a little different. For example, some channels send conversation update activities first, and some send conversation update activities after they send the first message activity. A channel might include both the bot and user in one conversation update activity, while another might send two conversation update activities.

In this example, the bot created and sent a message activity in response to the inbound message activity it had received. However, a bot can respond in other ways to a received message activity, and it's common for a bot to respond to a conversation update activity by sending a message activity with a welcome message. For more information, see how to [welcome a user](bot-builder-welcome-user.md).

## The Bot Framework SDK

The Bot Framework SDK allows you to build bots that can be hosted on the Azure AI Bot Service. The service defines a REST API and an activity protocol for how your bot and channels or users can interact. The SDK builds upon this REST API and provides an abstraction of the service so that you can focus on the conversational logic. While you don't need to understand the REST service to use the SDK, understanding some of its features can be helpful.

Bots are apps that have a conversational interface. They can be used to shift simple, repetitive tasks, such as taking a dinner reservation or gathering profile information, on to automated systems that may no longer require direct human intervention. Users converse with a bot using text, interactive cards, and speech. A bot interaction can be a quick question and answer, or it can be a sophisticated conversation that intelligently provides access to services.

> [!NOTE]
> Support for features provided by the SDK and REST API varies by channel.
> You can test your bot using the Bot Framework Emulator, but you should also test all features of your bot on each channel in which you intend to make your bot available.

Interactions involve the exchange of _activities_, which are handled in _turns_.

### Activities

Every interaction between the user (or a channel) and the bot is represented as an _activity_.
The Bot Framework [Activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md) defines the activities that can be exchanged between a user or channel and a bot. Activities can represent human text or speech, app-to-app notifications, reactions to other messages, and so on.

<a id="defining-a-turn"></a>

### Turns

In a conversation, people often speak one-at-a-time, taking turns speaking. With a bot, it generally reacts to user input. Within the Bot Framework SDK, a _turn_ consists of the user's incoming activity to the bot and any activity the bot sends back to the user as an immediate response. You can think of a turn as the processing associated with the bot receiving a given activity.

For example, a user might ask a bot to perform a certain task. The bot might respond with a question to get more information about the task, at which point this turn ends. On the next turn, the bot receives a new message from the user that might contain the answer to the bot's question, or it might represent a change of subject or a request to ignore the initial request to perform the task.

## Bot application structure

The SDK defines a _bot_ class that handles the conversational reasoning for the bot app. The bot class:

- Recognizes and interprets the user's input.
- Reasons about the input and performs relevant tasks.
- Generates responses about what the bot is doing or has done.

The SDK also defines an _adapter_ class that handles connectivity with the channels. The adapter:

- Provides a method for handling requests from and methods for generating requests to the user's channel.
- Includes a middleware pipeline, which includes turn processing outside of your bot's turn handler.
- Calls the bot's turn handler and catches errors not otherwise handled in the turn handler.

In addition, bots often need to retrieve and store state each turn.
State is handled through _storage_, _bot state_, and _property accessor_ classes.
The SDK doesn't provide built-in storage, but does provide abstractions for storage and a few implementations of a storage layer.
The [managing state](bot-builder-concept-state.md) topic describes these state and storage features.

:::image type="content" source="../media/bot-builder-basics/how-bots-work.png" alt-text="A bot has connectivity and reasoning elements, and an abstraction for state":::

The SDK doesn't require you use a specific application layer to send and receive web requests.
The Bot Framework has templates and samples for ASP.NET (C#), restify (JavaScript), and aiohttp (Python).
However, you can choose to use a different application layer for your app.

When you create a bot using the SDK, you provide the code to receive the HTTP traffic and forward it to the adapter. The Bot Framework provides a few templates and samples that you can use to develop your own bots.

[!INCLUDE [java-python-sunset-alert](../includes/java-python-sunset-alert.md)]

### Bot logic

The bot object contains the conversational reasoning or logic for a turn and exposes a _turn handler_, which is the method that can accept incoming activities from the bot adapter.

The SDK provides a couple different paradigms for managing your bot logic.

- _Activity handlers_ provide an event-driven model in which the incoming activity types and subtypes are the events. Consider an activity handler for bots that have limited, short interactions with the user.
  - Use an [activity handler](bot-activity-handler-concept.md) and implement handlers for each activity type or subtype your bot will recognize and react to.
  - Use a [Teams activity handler](bot-builder-basics-teams.md) to create bots that can connect to the Teams channel. (The Teams channel requires the bot to handle some channel-specific behavior.)
- The [dialogs library](bot-builder-concept-dialog.md) provides a state-based model to manage a long-running conversation with the user.
  - Use an activity handler and a _component dialog_ for largely sequential conversations.
    For more information, see [about component and waterfall dialogs](bot-builder-concept-waterfall-dialogs.md).
- Implement your own bot class and provide your own logic for handling each turn. For an example, see how to [create your own prompts to gather user input](bot-builder-primitive-prompts.md).

### The bot adapter

The adapter has a _process activity_ method for starting a turn.

- It takes the request body (the request payload, translated to an activity) and the request header as arguments.
- It checks whether the authentication header is valid.
- It creates a _context_ object for the turn. The context object includes information about the activity.
- It sends the context object through its _middleware_ pipeline.
- It then sends the context object to the bot object's turn handler.

The adapter also:

- Formats and sends response activities. These responses are typically messages for the user, but can also include information to be consumed by the user's channel directly.
- Surfaces other methods provided by the Bot Connector REST API, such as _update message_ and _delete message_.
- Catches errors or exceptions not otherwise caught for the turn.

#### The turn context

The _turn context_ object provides information about the activity such as the sender and receiver, the channel, and other data needed to process the activity. It also allows for the addition of information during the turn across various layers of the bot.

The turn context is one of the most important abstractions in the SDK. Not only does it carry the inbound activity to all the middleware components and the application logic but it also provides the mechanism whereby the middleware components and the bot logic can send outbound activities.

#### Middleware

Middleware is much like any other messaging middleware, comprising a linear set of components that are each executed in order, giving each a chance to operate on the activity. The final stage of the middleware pipeline is a callback to the turn handler on the bot class the application has registered with the adapter's _process activity_ method. Middleware implements an _on turn_ method which the adapter calls.

The turn handler takes a turn context as its argument, typically the application logic running inside the turn handler function will process the inbound activity's content and generate one or more activities in response, sending these outbound activities using the _send activity_ function on the turn context. Calling _send activity_ on the turn context will cause the middleware components to be invoked on the outbound activities. Middleware components execute before and after the bot's turn handler function. The execution is inherently nested and, as such, sometimes referred to being like an onion.

The [middleware](bot-builder-concept-middleware.md) topic describes middleware in greater depth.

### Bot state and storage

As with other web apps, a bot is inherently stateless.
State within a bot follows the same paradigms as modern web applications, and the Bot Framework SDK provides storage layer and state management abstractions to make state management easier.

The [managing state](bot-builder-concept-state.md) topic describes these state and storage features.

### Messaging endpoint and provisioning

Typically, your application will need a REST endpoint at which to receive messages. It will also need to provision resources for your bot in accordance with the platform you decide to use.

Follow the [Create a bot](../bot-service-quickstart-create-bot.md) quickstart to create and test a simple echo bot.

## HTTP Details

Activities arrive at the bot from the Bot Framework Service via an HTTP POST request. The bot responds to the inbound POST request with a 200 HTTP status code. Activities sent from the bot to the channel are sent on a separate HTTP POST to the Bot Framework Service. This, in turn, is acknowledged with a 200 HTTP status code.

The protocol doesn't specify the order in which these POST requests and their acknowledgments are made. However, to fit with common HTTP service frameworks, typically these requests are nested, meaning that the outbound HTTP request is made from the bot within the scope of the inbound HTTP request. This pattern is illustrated in the earlier diagram. Since there are two distinct HTTP connections back to back, the security model must provide for both.

> [!NOTE]
> The bot has 15 seconds to acknowledge the call with a status 200 on most channels. If the bot doesn't respond within 15 seconds, an HTTP GatewayTimeout error (504) occurs.

## The activity processing stack

Let's drill into the previous sequence diagram with a focus on the arrival of a message activity.

:::image type="complex" source="media/bot-builder-activity-processing-stack.png" alt-text="Sequence diagram illustrating how an activity is processed by a bot.":::
    The channel sends the user's message to the Azure AI Bot Service, and the service forwards the message to the bot's messaging endpoint. The bot's response is sent to the user within the scope of the turn.
:::image-end:::

In the example above, the bot replied to the message activity with another message activity containing the same text message. Processing starts with the HTTP POST request, with the activity information carried as a JSON payload, arriving at the web server. Often, ASP.NET projects are used for C# bots, and a popular framework such as Express or restify is used for JavaScript Node.js bots.

The _adapter_, an integrated component of the SDK, is the core of the SDK runtime. The activity is carried as JSON in the HTTP POST body. This JSON is deserialized to create the _activity_ object that is then handed to the adapter through its _process activity_ method. On receiving the activity, the adapter creates a _turn context_ and calls the middleware.

As mentioned above, the turn context provides the mechanism for the bot to send outbound activities, most often in response to an inbound activity. The turn context provides _send_, _update_, and _delete activity_ response methods. Each response method runs in an asynchronous process.

> [!IMPORTANT]
> The thread handling the primary bot turn deals with disposing of the context object when it's done. **Be sure to `await` any activity calls** so the primary thread will wait on the generated activity before finishing its processing and disposing of the turn context. Otherwise, if a response (including its handlers) takes any significant amount of time and tries to act on the context object, it may get a _context was disposed_ error.

## Bot templates

You need to choose the application layer use for your app; however, the Bot Framework has templates and samples for ASP.NET (C#), restify (JavaScript), and aiohttp (Python). The documentation is written assuming you use one of these platforms, but the SDK doesn't require it of you. See the [Create a bot](../bot-service-quickstart-create-bot.md) quickstart for instructions on how to access and install the templates.

A bot is a web application, and templates are provided for each language version of the SDK.
All templates provide a default endpoint implementation and adapter.
Each template includes:

- Resource provisioning
- A language-specific HTTP endpoint implementation that routes incoming activities to an adapter.
- An adapter object
- A bot object

The main difference between the different template types is in the bot object.
The templates are:

- **Empty bot**
  - Includes an activity handler that welcomes a user to the conversation by sending a "hello world" message on the first turn of the conversation.
- **Echo bot**
  - Uses an activity handler to welcome users and echo back user input.
- **Core bot**
  - Brings together many features of the SDK and demonstrates best practices for a bot.
  - Uses an activity handler to welcome users.
  - Uses a component dialog and child dialogs to manage the conversation.
  - The dialogs use Language Understanding (LUIS) and QnA Maker features.

[!INCLUDE [qnamaker-sunset-alert](../includes/qnamaker-sunset-alert.md)]

[!INCLUDE [luis-sunset-alert](../includes/luis-sunset-alert.md)]

## Additional information

### Managing bot resources

You'll need to manage the resources for your bot, such as its app ID and password, and also information for any connected services. When you deploy your bot, it will need secure access to this information. To avoid complexity, most of the Bot Framework SDK articles don't describe how to manage this information.

- For general security information, see [Bot Framework security guidelines](bot-builder-security-guidelines.md).
- To manage keys and secrets in Azure, see [About Azure Key Vault](/azure/key-vault/general/overview).

### Channel adapters

The SDK also lets you use channel adapters, in which the adapter itself additionally performs the tasks that the Bot Connector Service would normal do for a channel.

The SDK provides a few channel adapters in some languages.
More channel adapters are available through the Botkit and Community repositories.
For more information, see the Bot Framework SDK repository's table of [channels and adapters](https://github.com/microsoft/botframework-sdk#channels-and-adapters).

### The Bot Connector REST API

The Bot Framework SDK wraps and builds upon the Bot Connector REST API. If you want to understand the underlying HTTP requests that support the SDK, see the Connector [authentication](../rest-api/bot-framework-rest-connector-authentication.md) and associated articles.
The activities a bot sends and receives conform to the [Bot Framework Activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md).

## Next steps

- To understand the role of state in bots, see [managing state](bot-builder-concept-state.md).
- To understand key concepts of developing bots for Microsoft Teams, see [How Microsoft Teams bots work](bot-builder-basics-teams.md)

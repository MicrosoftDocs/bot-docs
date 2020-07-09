---
title: Bot Framework Frequently Asked Questions General - Bot Service
description: Frequently Asked Questions about Bot Framework general.
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 06/09/2020
---

# Bot Framework general

## Why doesn't the Typing activity do anything?

Some channels do not support transient typing updates in their client.

## What is the difference between the Connector library and Builder library in the SDK?

The Connector library is the exposition of the REST API. The Builder library adds the conversational dialog programming model and other features such as prompts, waterfalls, chains, and guided form completion. The Builder library also provides access to cognitive services such as LUIS.

## How do user messages relate to HTTPS method calls?

When the user sends a message over a channel, the Bot Framework web service will issue an HTTPS POST to the bot's web service endpoint. The bot may send zero, one, or many messages back to the user on that channel, by issuing a separate HTTPS POST to the Bot Framework for each message that it sends.


## How can I intercept all messages between the user and my bot?

Using the Bot Framework SDK for .NET, you can provide implementations of the `IPostToBot` and `IBotToUser` interfaces to the `Autofac` dependency injection container. Using the Bot Framework SDK for any language, you can use middleware for much the same purpose. The [BotBuilder-Azure](https://github.com/Microsoft/BotBuilder-Azure) repository contains C# and Node.js libraries that will log this data to an Azure table.

## What is the IDialogStack.Forward method in the Bot Framework SDK for .NET?

The primary purpose of `IDialogStack.Forward` is to reuse an existing child dialog that is often "reactive", where the child dialog (in `IDialog.StartAsync`) waits for an object `T` with some `ResumeAfter` handler. In particular, if you have a child dialog that waits for an `IMessageActivity` `T`, you can forward the incoming `IMessageActivity` (already received by some parent dialog) by using the `IDialogStack.Forward` method. For example, to forward an incoming `IMessageActivity` to a `LuisDialog`, call `IDialogStack.Forward` to push the `LuisDialog` onto the dialog stack, run the code in `LuisDialog.StartAsync` until it schedules a wait for the next message, and then immediately satisfy that wait with the forwarded `IMessageActivity`.

`T` is usually an `IMessageActivity`, since `IDialog.StartAsync` is typically constructed to wait for that type of activity. You might use `IDialogStack.Forward` to a `LuisDialog` as a mechanism to intercept messages from the user for
some processing before forwarding the message to an existing `LuisDialog`. Alternatively, you can also use `DispatchDialog` with `ContinueToNextGroup` for that purpose.

You would expect to find the forwarded item in the first `ResumeAfter` handler (e.g. `LuisDialog.MessageReceived`) that is scheduled by `StartAsync`.


## What is the difference between "proactive" and "reactive"?

From the perspective of your bot, "reactive" means that the user initiates the conversation by sending a message to the bot, and the bot reacts by responding to that message. In contrast, "proactive" means that the bot initiates the conversation by sending the first message to the user. For example, a bot may send a proactive message to notify a user when a timer expires or an event occurs.

## How can I send proactive messages to the user?

For examples that show how to send proactive messages, see the [C# V4 samples](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/16.proactive-messages) and [Node.js V4 samples](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/16.proactive-messages) within the BotBuilder-Samples repository on GitHub.

## How can I reference non-serializable services from my C# dialogs in SDK v3?

There are multiple options:

* Resolve the dependency through `Autofac` and `FiberModule.Key_DoNotSerialize`. This is the cleanest solution.
* Use [NonSerialized](https://msdn.microsoft.com/library/system.nonserializedattribute(v=vs.110).aspx) and [OnDeserialized](https://msdn.microsoft.com/library/system.runtime.serialization.ondeserializedattribute(v=vs.110).aspx) attributes to restore the dependency on deserialization. This is the simplest solution.
* Do not store that dependency, so that it won't be serialized. This solution, while technically feasible, is not recommended.
* Use the reflection serialization surrogate. This solution may not be feasible in some cases and risks serializing too much.

## What is an ETag?  How does it relate to bot data bag storage?

An [ETag](https://en.wikipedia.org/wiki/HTTP_ETag) is a mechanism for [optimistic concurrency control](https://en.wikipedia.org/wiki/Optimistic_concurrency_control). The bot data bag storage uses ETags to prevent conflicting updates to the data. An ETag error with HTTP status code 412 "Precondition Failed" indicates that there were multiple messages received in parallel before the bot could finish its first operation.

The dialog stack and state are stored in bot data bags. For example, you might see the "Precondition Failed" ETag error if your bot is still processing a previous message when it receives a new message for that conversation.

## What are some community-authored dialogs?

* [BotAuth](https://www.nuget.org/packages/BotAuth) - Azure Active Directory authentication
* [BestMatchDialog](http://www.garypretty.co.uk/2016/08/01/bestmatchdialog-for-microsoft-bot-framework-now-available-via-nuget/) - Regular expression-based dispatch of user text to dialog methods

## What are some community-authored templates?

* [ES6 BotBuilder](https://github.com/brene/botbuilder-es6-template) - ES6 Bot Builder template

## What is rate limiting?

The Bot Framework service must protect itself and its customers against abusive call patterns (e.g., denial of service attack), so that no single bot can adversely affect the performance of other bots. To achieve this kind of protection, we've added rate limits (also known as throttling) to our endpoints. By enforcing a rate limit, we can restrict the frequency with which a client or bot can make a specific call. For example: with rate limiting enabled, if a bot wanted to post a large number of activities, it would have to space them out over a time period. Please note that the purpose of rate-limiting is not to cap the total volume for a bot. It is designed to prevent abuse of the conversational infrastructure that does not follow human conversation patterns. For example, flooding two conversations with more content than two human could ever consume.

## How will I know if I'm impacted?

It is unlikely you'll experience rate limiting, even at high volume. Most rate limiting would only occur due to bulk sending of activities (from a bot or from a client), extreme load testing, or a bug. When a request is throttled, an HTTP 429 (Too Many Requests) response is returned along with a Retry-After header indicating the amount of time (in seconds) to wait before retrying the request would succeed. You can collect this information by enabling analytics for your bot via Azure Application Insights. Or, you can add code in your bot to log messages.

## How does rate limiting occur?

It can happen if:

* A bot sends messages too frequently
* A client of a bot sends messages too frequently
* Direct Line clients request a new Web Socket too frequently

### What are the rate limits?

We're continuously tuning the rate limits to make them as lenient as possible while at the same time protecting our service and our users. Because thresholds will occasionally change, we aren't publishing the numbers at this time. If you are impacted by rate limiting, feel free to reach out to us at [bf-reports@microsoft.com](mailto://bf-reports@microsoft.com).

## How to implement human hand off?

At times it is necessary to transfer (hand off) a conversation from a bot to a human being. This happens for example if the bot does not understand the user, or if the request cannot be automated. In these cases, the bot provides a smooth transition to the human intervention.

It is important to keep in mind that hand off are [event activities](https://github.com/Microsoft/botframework-sdk/blob/master/specs/botframework-activity/botframework-activity.md#event-activity) which communicate programmatic information from a client or channel to a bot. These events are used to handle the hand off between a bot and the agent hub and are also known as **handoff events**.

When a bot detects the need to hand the conversation off to an agent, it signals its intent by sending a **handoff initiation event**. See this [handoff protocol](~/bot-service-design-pattern-handoff-human.md#handoff-protocol) example.

The Bot Framework SDK supports handoff to an agent, known as escalation. There a few **event types** for signaling handoff operations. The events are exchanged between a **bot** and an **agent hub**, also called engagement hub. This hub is defined as an application or a system that allows agents, typically humans, to receive and handle requests from users, as well as escalation requests from bots. See [Handoff Library](https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/handoff-library#handoff-library).

You can select one of the following models for integration with the agent hubs:

- [Bot as an agent](~/bot-service-design-pattern-handoff-human.md#bot-as-an-agent)
- [Bot as a proxy](~/bot-service-design-pattern-handoff-human.md#bot-as-a-proxy)

The handoff protocol is identical for both models, however the onboarding details differ between the models and the agent engagement hubs. The protocol is centered around events for initiation (sent by the bot to the channel) and status update (sent by the channel to the bot). For more information, see the [protocol](https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/handoff-library#protocol) of the **hand off library**.

> [!NOTE]
> DirectLine channel supports handoff in the [bot as an agent](~/bot-service-design-pattern-handoff-human.md#bot-as-an-agent) scenario.  This is because there is an application that handles the handoff event.

In the case of channels that do not handle the handoff, the middleware is used to transform the handoff event into API calls specific to the agent hub.

### ??Questions??

1. The [Transition conversations from bot to human](~/bot-service-design-pattern-handoff-human.md) article refers to the following:

    - [Integration with Microsoft Dynamics Omnichannel for Customer Service](https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/handoff-library/csharp_dotnetcore/samples)
    - [Integration with LiverPerson LiveEngage platform](https://developers.liveperson.com/third-party-bots-microsoft-bot-framework.html)

    **Q1: Do we need to show how to use them perhaps in a specific how to article?**

1. The [PR 1786](https://github.com/MicrosoftDocs/bot-docs/issues/1786) says *We and customers have implemented connectors (middleware + adapter) for LivePerson proxy, ServiceNow, RingCentral and others.*

    **Q2: Do we need to refer to this?**: [HandoffMiddleware.cs](https://github.com/microsoft/BotBuilder-Samples/blob/master/experimental/handoff-library/csharp_dotnetcore/samples/LivePersonAgentBot/Middleware/HandoffMiddleware.cs).

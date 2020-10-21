---
title: Transition conversations from bot to human - Bot Service
description: Learn how to design for situations where a user starts a conversation with a bot and then must be handed off to a human.
author: arturl
ms.author: arturl
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 10/21/2020
monikerRange: 'azure-bot-service-4.0'
---

# Transition conversations from bot to human

Regardless of how much artificial intelligence a bot possesses, there may still be times when it needs to hand off the conversation to a human being. This can be necessary either because the bot does not understand the user (because of an AI limitation), or if the request cannot be automated and requires a human action. In such cases the bot should recognize when it needs to hand off and provide the user with a smooth transition.

Microsoft Bot Framework is an open platform that allows developers to integrate with a variety of agent engagement platforms.


<!-- We don't own this aka link, and for v4, I think there is an updated pattern.
You can read more about the Bot Framework handoff protocol <a href="https://aka.ms/bfhandoffprotocol" target="blank">here</a>.
-->

## Handoff integration models

Microsoft Bot Framework supports two models for integration with agent engagement platforms. The handoff protocol is identical for both models, however the onboarding details differ between the models and the agent engagement platforms.

The goal is not to offer a universal solution for integration with any customer's system, but rather to provide a **common language** and **best practices** for bot developers and system integrators building conversational AI systems with human in the loop.

### Bot as an agent

In the first model, known as "Bot as an agent", the bot joins the ranks of the live agents connected to the agent hub and responds to user requests as if the requests came from any other Bot Framework channel. The conversation between the user and the bot can be escalated to a human agent, at which point the bot disengages from the active conversation.

The main advantage of this mode is in its simplicity – an existing bot can be onboarded to the agent hub with minimal effort, with all of the complexity of message routing taken care of by the agent hub.

![Bot as an agent scenario](~/media/designing-bots/patterns/bot-as-agent-2.PNG)

### Bot as a proxy

The second model is known as "Bot as a proxy". The user talks directly to the bot, until the bot decides that it needs help from a human agent. The message router component in the bot redirects the conversation to the agent hub, which dispatches it to the appropriate agent. The bot stays in the loop and can collect the transcript of the conversation, filter messages, or provide additional content to both the agent and the user.

Flexibility and control are the main advantages of this model. The bot can support a variety of channels and have control over how the conversations are escalated and routed between the user, the bot, and the agent hub.

![Bot as a proxy scenario](~/media/designing-bots/patterns/bot-as-proxy-2.PNG)

## Handoff protocol

The protocol is centered around events for initiation, sent by the bot to the channel, and status update, sent by the channel to the bot.


### Handoff initiation

The *Handoff Initiation* event is created by the bot to initiate handoff.

The event contains two components:

- The **context of the handoff request** that is necessary to route the conversation to the right agent.
- The **transcript of the conversation**. The agent can read the conversation that took place between the customer and the bot before the handoff was initiated.

The following are the handoff initiation event fields:

- **Name** - The `name` is a **required** field that is set to `"handoff.initiate"`.
- **Value** - The `value` field is an object containing agent hub-specific JSON content, such as required agent skill and so on.  This field is **optional**.

    ```json
    { "Skill" : "credit cards" }
    ```

- **Attachments** - The `attachments` is an **optional** field containing the list of `Attachment` objects. Bot Framework defines the "Transcript" attachment type that is used to send conversation transcript to the agent hub if required. Attachments can be sent either inline (subject to a size limit) or offline by providing `ContentUrl`.

    ```C#
    handoffEvent.Attachments = new List<Attachment> {
        new Attachment {
            Content = transcript,
            ContentType = "application/json",
            Name = "Trasnscript",
        }};
    ```

    > [!NOTE]
    > Agent hubs **must ignore** attachment types they don't understand.

- **Conversation** - The `conversation` is a **required** field of type `ConversationAccount` describing the conversation being handed over. Critically, it MUST include the conversation `Id` that can be used for correlation with the other events.

When a bot detects the need to hand the conversation off to an agent, it signals its intent by sending a handoff initiation event.
In C# an higher level API `CreateHandoffInitiation` method can be used as demonstrated in the code snippet below.

```C#
var activities = GetRecentActivities();
var handoffContext = new { Skill = "credit cards" };
var handoffEvent =
    EventFactory.CreateHandoffInitiation(
        turnContext, handoffContext, new Transcript(activities));
await turnContext.SendActivityAsync(handoffEvent);
```

### Handoff status

The *Handoff Status* event is sent to the bot by the agent hub. The event informs the bot about the status of the initiated handoff operation.

> [!NOTE]
> Bots are **not required** to handle the event, however they **must not** reject it.

The following are the handoff status event fields:

- **Name** - The `name` is a **required** field that is set to `"handoff.status"`.

- **Value** - The `value` is a **required** field describing the current status of the handoff operation. It is a JSON object containing the **required** field `state` and an optional field `message`, as defined below.

The `state` has one of the following values:

- `accepted`- An agent has accepted the request and taken control of the conversation.
- `failed`- Handoff request has failed. The `message` might contain additional information relevant to the failure.
- `completed` - Handoff request has completed.

The format and possible value of the `message` field are unspecified.

- Successful handoff completion:

    ```json
    { "state" : "completed" }
    ```

- Handoff operation failed due to a timeout:

    ```json
    { "state" : "failed", "message" : "Cannot find agent with requested skill" }
    ```

- **Conversation** -`Conversation`is a **required** field of type `ConversationAccount` describing the conversation that has been accepted or rejected. The `Id` of the conversation MUST be the same as in the HandoffInitiation that initiated the handoff.

## Handoff library

The [Handoff Library](https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/handoff-library) has been created to complement the Bot Framework v4 SDK in supporting handoff; specifically:

- Implements the additions to the Bot Framework SDK to support handoff to an agent (also known as *escalation*.
- Contains definitions of three event types for signaling handoff operations.

> [!NOTE]
> Integrations with specific agent hubs are not part of the library.

## Additional resources

- [Integration with Microsoft Dynamics Omnichannel for Customer Service](https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/handoff-library/csharp_dotnetcore/samples)
- [Integration with LivePerson LiveEngage platform](https://developers.liveperson.com/third-party-bots-microsoft-bot-framework.html)
- [Dialogs](v4sdk/bot-builder-dialog-manage-conversation-flow.md)
- [Cognitive services](https://www.microsoft.com/cognitive-services/text-analytics-api)


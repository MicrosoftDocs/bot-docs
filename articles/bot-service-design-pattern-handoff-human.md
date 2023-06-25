---
title: Transition conversations from bot to human
description: Learn how to design for situations where a user starts a conversation with a bot and then must be handed off to a human.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: bot-service
ms.date: 07/27/2022
monikerRange: 'azure-bot-service-4.0'
---

# Transition conversations from bot to human

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

Regardless of how much artificial intelligence a bot possesses, it might still need to hand off the conversation to a human being. Such a hand off might be necessary if the bot doesn't understand the user (because of an AI limitation), or if the request can't be automated and requires a human action. In such cases, the bot should recognize when it needs to hand off the conversation and provide the user with a smooth transition.

Microsoft Bot Framework is an open platform that allows developers to integrate with various agent engagement platforms.

## Handoff integration models

Microsoft Bot Framework supports two models for integration with agent engagement platforms. The handoff protocol is identical for both models, however the onboarding details differ between the models and the agent engagement platforms.

The goal isn't to offer a universal solution for integration with any customer's system, but rather to provide a _common language_ and _best practices_ for bot developers and system integrators with which to build conversational AI systems with a human in the loop.

### Bot as an agent

In the first model, known as _bot as an agent_, the bot joins the ranks of the live agents connected to the agent hub and responds to user requests as if the requests came from any other Bot Framework channel. The conversation between the user and the bot can be escalated to a human agent, at which point the bot disengages from the active conversation.

The main advantage of this model is its simplicity&mdash;you can add an existing bot to the agent hub with minimal effort, and the agent hub will handle the complexity of message routing.

:::image type="content" source="media/designing-bots/patterns/bot-as-agent-2.PNG" alt-text="Diagram of an agent hub that can direct messages to a bot or human agents.":::

### Bot as a proxy

The second model is known as _bot as a proxy_. The user talks directly to the bot, until the bot decides that it needs help from a human agent. The message router component in the bot redirects the conversation to the agent hub, which dispatches it to the appropriate agent. The bot stays in the loop and can collect the transcript of the conversation, filter messages, or provide additional content to both the agent and the user.

Flexibility and control are the main advantages of this model. The bot can support multiple channels and have control over how the conversations are escalated and routed between the user, the bot, and the agent hub.

:::image type="content" source="media/designing-bots/patterns/bot-as-proxy-2.PNG" alt-text="Diagram of a bot that can route messages to an agent hub.":::

## Handoff protocol

The protocol is centered around events for initiation, sent by the bot to the channel, and status update, sent by the channel to the bot.

### Handoff initiation

A _handoff initiation_ event is created by the bot to initiate handoff.

The event can include:

- The context of the handoff request, to route the conversation to an appropriate agent.
- A transcript of the conversation, so an agent can read the conversation that took place between the customer and the bot before the handoff was initiated.

The following are common handoff initiation event properties:

- Name: Required, the _name_ property must be set to "handoff.initiate".
- Conversation: Required, the _conversation_ property describes the conversation in which the activity exists. Conversation _must_ include the conversation `Id`.
- Value: Optional, the _value_ property can contain agent hub-specific JSON content that the hub can use to route the conversation to a relevant agent.
- Attachments: Optional, the _attachments_ property can include a transcript as an attachment. The Bot Framework defines a _transcript_ attachment type. An attachment can be sent either inline (subject to a size limit) or offline by providing `ContentUrl`.

    ```csharp
    handoffEvent.Attachments = new List<Attachment> {
        new Attachment {
            Content = transcript,
            ContentType = "application/json",
            Name = "Transcript",
        }
    };
    ```

    > [!NOTE]
    > Agent hubs **must ignore** attachment types they don't understand.

When a bot detects the need to hand the conversation off to an agent, it signals its intent by sending a handoff initiation event.
The SDK for C# includes a `CreateHandoffInitiation` method to create a valid handoff initiation event.

```csharp
var activities = GetRecentActivities();
var handoffContext = new { Skill = "credit cards" };
var handoffEvent =
    EventFactory.CreateHandoffInitiation(
        turnContext, handoffContext, new Transcript(activities));
await turnContext.SendActivityAsync(handoffEvent);
```

### Handoff status

A _handoff status_ event is sent to the bot by the agent hub. The event informs the bot about the status of the initiated handoff operation.

> [!NOTE]
> Bots are _not required_ to handle a handoff status event; however, they _must not_ reject it.

The following are common handoff status event fields:

- Name: Required, the _name_ property must be set to "handoff.status".
- Conversation: Required, the _conversation_ property describes the conversation in which the activity exists. Conversation _must_ include the conversation `Id`.
- Value: Required, the _value_ property that describes the current status of the handoff operation. The value has the following properties.
  - State: Required, the _state_ property can have one of these values:

    | Value       | Meaning                                                                                                          |
    |:------------|:-----------------------------------------------------------------------------------------------------------------|
    | "accepted"  | An agent accepted the request and taken control of the conversation.                                             |
    | "failed"    | The handoff request failed. The _message_ property might contain additional information relevant to the failure. |
    | "completed" | The handoff request completed.                                                                                   |

  - Message: Optional, the _message_ property is an object defined by the agent hub.

  Here are some example value objects:

    ```json
    { "state" : "completed" }
    ```

    ```json
    { "state" : "failed", "message" : "Can't find agent with requested skill" }
    ```

## Handoff library

The [Handoff Library](https://github.com/microsoft/BotBuilder-Samples/tree/main/experimental/handoff-library) has been created to complement the Bot Framework v4 SDK in supporting handoff; specifically:

- Implements the additions to the Bot Framework SDK to support handoff to an agent (also known as _escalation_).
- Contains definitions of three event types for signaling handoff operations.

> [!NOTE]
> Integrations with specific agent hubs are not part of the library.

## Additional resources

- [Integration with Microsoft Dynamics Omnichannel for Customer Service](https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/handoff-library/csharp_dotnetcore/samples)
- [Integration with LivePerson LiveEngage platform](https://developers.liveperson.com/third-party-bots-microsoft-bot-framework.html)
- [Dialogs](v4sdk/bot-builder-dialog-manage-conversation-flow.md)
- [Azure AI services](https://www.microsoft.com/cognitive-services/text-analytics-api)

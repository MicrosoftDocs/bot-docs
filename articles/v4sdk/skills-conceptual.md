---
title: Skills overview | Microsoft Docs
description: Describes how conversational logic in one bot can be used by another bot using the Bot Framework SDK.
keywords: bot skill, host bot, skill bot, skill consumer.
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 04/28/2020
monikerRange: 'azure-bot-service-4.0'
---

# Skills overview

[!INCLUDE [applies-to-v4](../includes/applies-to.md)]

<!-- Value prop: why skills -->
Starting with version 4.7 of the Bot Framework SDK, you can extend a bot using another bot (a skill).
A skill can be consumed by various other bots, facilitating reuse, and in this way, you can create a user-facing bot and extend it by consuming your own or 3rd party skills.

<!-- Terminology -->
- A _skill_ is a bot that can perform a set of tasks for another bot.
  Depending on design, a skill can also function as a typical, user-facing bot.
- A _skill consumer_ is a bot that can invoke one or more skills. With respect to skills, a _root bot_ is a user-facing bot that is also a skill consumer.
- A _skill manifest_ is a JSON file that describes the actions the skill can perform, its input and output parameters, and the skill's endpoints.
  - Developers who don't have access to the skill's source code can use the information in the manifest to design their skill consumer.
  - The _skill manifest schema_ is a JSON file that describes the schema of the skill manifest. The current version is [skill-manifest-2.0.0.json](https://github.com/microsoft/botframework-sdk/blob/master/schemas/skills/skill-manifest-2.0.0.json).
  - See how to [implement a skill](./skill-implement-skill.md), [write a v2.1 skill manifest](skills-write-manifest-2-1.md) (preview), and [write a v2.0 skill manifest](skills-write-manifest-2-0.md) for sample skill manifests.

In other words, the user interacts directly with the root bot, and the root bot delegates some of its conversational logic to a skill.

<!-- Requirements/contract -->
The skills feature is designed so that:

- Skills and consumers communicate over HTTP using the bot framework protocol.
- A skill consumer can consume multiple skills.
- A skill consumer can consume a skill regardless of the language used to implement the skill. For example, a C# bot can consume a skill implemented using Python.
- A skill can also be a skill consumer and call other skills.
- Skills support user authentication; however, user authentication is local to the skill and cannot be transferred to another bot.
- Skills can work with both the Bot Framework adapter and custom adapters.

![Block diagram](./media/skills-block-diagram.png)

<!--TBD: - Skills support proactive messaging. -->

## Conceptual architecture

A skill and skill consumer are separate bots, and you publish them independently. A skill needs to include additional logic to send an `endOfConversation` activity when it completes, so that the skill consumer knows when to stop forwarding activities to the skill.

A root bot implements at least two HTTP endpoints, one for receiving activities from the user and one for receiving activities from skills. The skill consumer needs to pair code that receives the HTTP method request from the skill with a skill handler.
It requires added logic for managing a skill, such as when to invoke or cancel the skill, and so on. In addition to the usual bot and adapter objects, the consumer includes a few skill-related objects, used to exchange activities with the skill.

This diagram outlines the flow of activities from the user to the root bot to a skill and back again.

![Architecture diagram](./media/skills-conceptual-architecture.png)

1. The root bot's adapter receives activities from the user and forwards them to the root bot's activity handler.
1. The root bot uses a skill HTTP client to send an activity to the skill. It gets the consumer-skill conversation information from a skill definition and a skill conversation ID factory. This includes the service URL that the skill will use to reply to the activity.
1. The skill's adapter receives activities from the skill consumer and forwards them to the skill's activity handler.
1. When the skill responds, the root bot's skill handler receives the activity. It gets the root-user conversation information from the skill conversation ID factory. It then forwards the activity to the root bot's adapter.
1. The root bot's adapter internally generates a proactive message to resume the conversation with the user.
1. The root bot's adapter sends any messages from the skill to the user.

These objects help manage skills and route skill traffic:

- A _Bot Framework skill_ describes routing information for a skill and can be read from the skill consumer's configuration file.
- A _skill HTTP client_ sends activities to a skill.
- A _skill handler_ receives activities from a skill.
- The _skill conversation ID factory_ translates between the user-root conversation reference and the root-skill conversation reference.
- The Bot Connector service provides both channel and bot-to-bot authentication. Using an _authentication configuration_ object, you can add claims validation to a bot (skill or skill consumer) to limit which applications or users have access.

The skill client and skill handler objects both use the _conversation ID factory_ to translate between the conversation the root bot uses to interact with the user and the conversation the root bot uses to interact with the skill.

## Bot-to-bot communication

It's important to understand certain aspects of this design, independent of which bot you're designing.

<!--
- infrastructure concerns:
  - stateless, cross-server application (memory management and middleware).
  - authentication, in both directions, plus claims validation.
- implementation concerns:
  - classes, objects, and logic you need to add to your host (and skill).
  - when to start and stop a skill.
  - managing multiple skills.
-->

### Conversation references

The user-root conversation is different than the root-skill conversation.

The _conversation ID factory_ helps to manage traffic between a skill consumer and a skill. The factory translates between the ID of the conversation the root has with the user and the one it has with the skill.
In other words, it generates a conversation ID for use between the root and the skill, and recovers the original user-root conversation ID from the root-skill conversation ID.

<!-- Hopefully, this just gets folded into the SDK and does not need to get described in detail.
- The host needs to save or encode original conversation ID and service URL and create a conversation ID for use between it and the skill.
  - Generated conversation IDs must be usable as a URL path parameter.
  - A modified activity gets the new conversation ID and service URL (of the host, as the host provides a channel interface to the skill).
- Upon receiving an activity from the skill, host needs to decode or recover the original conversation ID and service URL so that the activity can get forwarded back to the user in the original conversation.
-->

### Cross-server coordination
<!-- or, Statelessness in the host -->

The root and skill bots communicate over HTTP.
So, the instance of the root bot that receives an activity from a skill may not be the same instance that sent the initiating activity; in other words, different servers may handler these two requests.

- Always save state in the skill consumer before forwarding an activity to a skill.
  This ensures that the instance that receives traffic from a skill can pick up where the previous instance left off before it invoked the skill.
- When the skill handler receives an activity from a skill, it translates it into a form appropriate for the skill consumer, and forwards it to the consumer's adapter.

### Skill consumer and skill state

The skill consumer and skill manage their own state separately. However, the consumer creates the conversation ID that it uses to communicate with the skill. This can have an effect on conversation state in the skill.

> [!IMPORTANT]
> As noted previously, when the skill consumer delegates a user activity to a skill, a different instance of the consumer may receive the skill response. The consumer should always save conversation state immediately before forwarding an activity to a skill.

### Bot-to-bot authentication

<!-- TODO Add appropriate info about this new(?) feature to the bot basics article. -->

Service-level authentication is managed by the Bot Connector service. The framework uses bearer tokens and bot application IDs to verify the identity of each bot.

The Bot Framework uses an _authentication configuration_ object to validate the authentication header on incoming requests.

#### Claims validation

You can add a _claims validator_ to the authentication configuration. The claims are evaluated after the authentication header. Throw an error or exception in your validation code to reject the request.

There are various reasons you might reject an otherwise authenticated request:

- When the skill consumer should accept traffic only from skills that it may have initiated a conversation with.
- When a skill is part of a paid-for service, and users not in the database should not have access.
- When you want to restrict access to the skill to specific skill consumers.

<!--TODO Need a link for more information about claims and claims-based validation.-->

## Additional information

From the user's perspective, the root bot is the bot they are interacting with.
From the skill's perspective, the skill consumer is the channel over which it communicates with the user.

- For more information about skill bots and skill manifests, see [about skill bots](skills-about-skill-bots.md).
- For more information about skill consumers, see [about skill consumers](skills-about-skill-consumers.md).

---
title: Update a skill to support both single-tenant and multitenant agents
description: Learn how to multitenant skill to a single-tenant skill for Copilot Studio agents.
keywords: skills
author: JonathanFingold
ms.author: kunsinghms
manager: shellyha
ms.reviewer: micchow
ms.topic: reference
ms.service: azure-ai-bot-service
monikerRange: 'azure-bot-service-4.0'
ms.custom:
  - evergreen
ms.update-cycle: 1095-days
---

# Update a skill to support both single-tenant and multitenant agents

Copilot Studio users can [implement skills in Copilot Studio](/microsoft-copilot-studio/configuration-add-skills).

> [!NOTE]
> For additional details, see [Same-tenant restriction](skill-pva.md#same-tenant-restriction).

These skills have the following artifacts which refer to multitenant and single-tenant implementations:

- an Entra ID app registration
- a deployment (also referred to as a descriptor)
- the source code

The Copilot Studio agent calls the skill based on the Bot Framework SDK, as shown in the following illustration:

:::image type="content" source="./media/skill-pva/agent-to-skill-calls.png" alt-text="Graphic illustrating the call flow between a Copilot Studio agent and a Bot Framework skill.":::

In this scenario, the following actions occur:

- Copilot Studio creates the token as per the app registration setting. For newly created Copilot Studio agents, this is single-tenant, which means that the token audience is set to the same tenant ID as the agent.
- Copilot Studio only accepts tokens for the tenant ID the agent is in, or for the Bot Framework tenant.
- Entra ID only issues tokens for the Bot Framework tenant if the skill is multitenant.

For skills already deployed to the same tenant as the Copilot Studio agent, but the skill is used by an existing multitenant agent, you must update the multitenant skill to also accept a single-tenant skill token.

1. Update the skill's validation configuration to include the tenant ID of the agent. For example start by adding [this code](https://github.com/microsoft/BotBuilder-Samples/blob/6952b9e548038d58e3c8cd607acaa72dcf7648a0/samples/csharp_dotnetcore/80.skills-simple-bot-to-bot/EchoSkillBot/Startup.cs#L29-L55) to the Startup.cs of your skill. Update line 38 of the code snippet to specify the tenant ID string inline.

1. In the configuration of your skill, do **not** set `MicrosoftAppTenantId`.

1. Keep the value of `MicrosoftAppType` in the configuration set to `MultiTenant`.

1. Build the skill and deploy it again.

1. The skill's application registration must be in the same tenant as your agent for the skill to work with a single-tenant agent.

You can now add the updated skill into a single-tenant or multitenant agent. For each Copilot Studio agent, the skill must be deployed with its application registration in the tenant where the agent using the skill was created.

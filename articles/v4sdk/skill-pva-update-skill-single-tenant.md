---
title: Update a multitenant skill to a single-tenant skill
description: Learn how to multitenant skill to a single-tenant skill for Copilot Studio agents.
keywords: skills
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: reference
ms.service: azure-ai-bot-service
monikerRange: 'azure-bot-service-4.0'
ms.custom:
  - evergreen
---

# Update a multitenant skill to a single-tenant skill

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
- Copilot Studio only accepts tokens for the tenant ID the agent is in, or for the Bot Framework skill.
- The Entra ID only issues tokens for the Bot Framework skill if the skill is multitenant.
 
## Convert a skill to single-tenant

For skills already deployed into the same tenant as the Copilot Studio agent, and only used by the new agent, [convert the skill to a single-tenant skill](skill-pva-convert-skill-single-tenant.md).

## Update a multitenant skill to a single-tenant skill

For skills already deployed into the same tenant as the Copilot Studio agent, but the skill is used by an existing multitenant agent, you need to update the multitenant skill to accept a single-tenant skill token.

1. Update the skills validation configuration to all the tenant ID of the agent. For more information, see [Multitenant to single-tenant code update](skill-pva-convert-skill-single-tenant.md#multitenant-to-single-tenant-code-update).
1. Create the token for the agent's tenant ID (deploy the app regristration to the correct tenant).

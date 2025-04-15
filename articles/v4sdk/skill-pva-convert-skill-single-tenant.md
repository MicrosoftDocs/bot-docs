---
title: Convert an existing skill from multitenant to single-tenant
description: Learn how to convert an existing skill from multitenant to single-tenant for use in Copilot Studio agents.
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

# Convert an existing skill from multitenant to single-tenant

You can convert existing skills from multitenant support to single-tenant support. To convert a multitenant skill to a single-tenant skill, you need to perform the following changes:

- Create a new single-tenant Entra ID app registration
- Update the skill configuration to use single-tenant
- (Optional) update the source code
- Deploy the skill

The following values are required for single-tenant skills:

| Property               | Value                   |
| ---------------------- | ----------------------- |
| `MicrosoftAppType`     | `SingleTenant`          |
| `MicrosoftAppId`       | The bot's app ID        |
| `MicrosoftAppPassword` | The bot's app password  |
| `MicrosoftAppTenantId` | The bot's app tenant ID |

For reference, the following values were used for multitenant skills:

| Property               | Value                                           |
| ---------------------- | ----------------------------------------------- |
| `MicrosoftAppType`     | `MultiTenant`                                   |
| `MicrosoftAppId`       | The bot's app ID                                |
| `MicrosoftAppPassword` | The bot's app password                          |
| `MicrosoftAppTenantId` | Not applicable; left blank for multitenant bots |

After converting the values, import the skill into an instance of your agent created as a single-tenant instance. You can also view the Entra ID app registration, to see how it was created. Go to **Manage** > **Authentication** > **Supported account types**.

:::image type="content" source="./media/skill-pva/authentication-supported-account-types.png" alt-text="Screenshot highlighting the Supported account type options.":::

## Multitenant to single-tenant code update

After converting the values, you might need to also update the code to allow connection of the specified tenant to the skill. For more information, see [BotBuilder-Samples](https://github.com/microsoft/BotBuilder-Samples/blob/6952b9e548038d58e3c8cd607acaa72dcf7648a0/samples/csharp_dotnetcore/80.skills-simple-bot-to-bot/EchoSkillBot/Startup.cs#L29-L55).

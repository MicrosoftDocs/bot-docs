---
title: Implement a skill for Power Virtual Agents | Microsoft Docs
description: Learn how to implement a skill that can be used in Power Virtual Agents, using the Bot Framework SDK.
keywords: skills
author: clearab
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 04/15/2020
monikerRange: 'azure-bot-service-4.0'
---

# Implement a skill for use in Power Virtual Agents

[!INCLUDE[applies-to](../includes/applies-to.md)]

A skill is a bot that can be used by another bot. In this way you can create a single user-facing bot and extend it with one or more skills. You can learn more about skills in general in [Skills Overview](skills-conceptual.md), and how to build them in [Implement a skill](skill-implement-skill.md). Alternatively, the Virtual Assistant templates contain a set of [pre-built skills](bot-builder-skills-overview.md) you can customize and deploy instead of building one from scratch.

If you expect that your skill will be consumed from a [Power Virtual Agents](https://powerva.microsoft.com/#/) bot there are some additional restrictions placed on your Skill you'll need to account for.

## Manifest restrictions

Power Virtual Agents places restrictions on what you may declare in your manifest.

- You may declare only 25 or fewer actions.
- Each action is limited to 25 or fewer inputs or outputs.
- You cannot use the array type for inputs or outputs.
- Only the English language ('en' locale) is currently supported

## Same-tenant restriction

In order to ensure compliance and adequate governance of custom skills being registered for use within Power Virtual Agents, it is required to register Skill as an application in Azure Active Directory. Upon adding a skill, we validate if the skill's application ID is the in the tenant of the signed in user and the skills endpoint matches the registered application's `Home Page URL`.

Please add in the Manifest URL to the [Home Page in the Azure Portal](/azure/active-directory/manage-apps/application-proxy-configure-custom-home-page#change-the-home-page-in-the-azure-portal) prior to registering a skill.

## Validation performed during registering a Skill

When an end user attempts to connect to your skill from their Power Virtual Agents bot, they'll first need to [import the skill to Power Virtual Agents](/power-virtual-agents/advanced-use-skills). Your skill will go through a series of validation checks. A failure of one of these checks may result in an error message as described in this table.

Validation step|Error message|Description or mitigation
|---|---|---
|Validate Skill manifest URL|The link isn't valid; The link must begin with https:// | Re-enter the link as a secure URL. |
|Validate if Skill manifest can be retrieved|We ran into problems getting the Skill manifest.| Verify your manifest URL is a link to your manifest, and can be downloaded as a .json file.|
|Validate if Skill manifest can be read|The manifest is too large; The manifest is incompatible.| Fix syntactical errors in the manifest. See [Manifest restrictions](#manifest-restrictions) |
|Validate if Skill is previously registered|This Skill has already been added to your bot.|The end user has already added your skill to PVA. |
|Validate Skill manifest endpoint origin|There's a mismatch in your Skill endpoints.|You AAD app's homepage URL domain and manifest URL domain must match. See [Same-tenant restriction](#same-tenant-restriction)|
|Validate Skill is hosted in signed in user's tenant|To add a Skill, it must first be registered.| A global administrator must register the Skill into the signed in user's organization. |
|Validate Skill actions|The Skill is limited to 25 actions.|There are too many Skill actions defined in Skill manifest. Remove actions and try again. |
|Validate Skill action input parameters|Actions are limited to 25 inputs.|There are too many Skill action input parameters. Remove parameters and try again. |
|Validate Skill action output parameters|Actions are limited to 25 outputs.|There are too many Skill action output parameters. Remove parameter and try again. |
|Validate Skill count|Your bot can have a maximum of 25 Skills.| There are too many Skills added into a bot. Remove an existing Skill and try again. |
|Validate Skill action language|Currently, Skills are only supported in English.| Skill has actions with unsupported locales. We only support Skills with Actions in English ('en') locale. |
|Validate AAD app setting |The Skill must be registered multi-tenant.| Verify that your AAD app is marked as multi-tenant. See [Convert app to be multi-tenant](/azure/active-directory/develop/howto-convert-app-to-be-multi-tenant#update-registration-to-be-multi-tenant) |
|Validate security token |It looks like something went wrong.|There may be a transient error to acquire a security token to trigger Skill. Retry.|
|Validate Skill health|Something went wrong while checking your Skill.|PVA received an unknown response when sending an `EndOfConversation` activity to your Skill. Make sure your Skill is running and responding correctly.|

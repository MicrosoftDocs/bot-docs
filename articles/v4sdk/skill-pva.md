---
title: Implement a skill for Power Virtual Agents | Microsoft Docs
description: Learn how to implement a skill that can be used in Power Virtual Agents, using the Bot Framework SDK.
keywords: skills
author: clearab
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 03/30/2020
monikerRange: 'azure-bot-service-4.0'
---

# Implement a skill for use in Power Virtual Agents

[!INCLUDE[applies-to](../includes/applies-to.md)]

If you expect that your Skill will be consumed from a [Power Virtual Agents](https://powerva.microsoft.com/#/) bot there are some additional restrictions placed on your Skill you'll need to account for.

## Manifest restrictions

Power Virtual Agents places restrictions on what you may declare in your manifest.

- You may declare only 25 or fewer actions.
- Each action is limited to 25 or fewer inputs or outputs.
- You cannot use the array type for inputs or outputs.
- Only the English language ('en' locale) is currently supported

## Same-tenant restriction

(Need Pawan's assistance with writing this section.)

Please add in the Manifest URL to the [Home Page in the Azure Portal](/azure/active-directory/manage-apps/application-proxy-configure-custom-home-page#change-the-home-page-in-the-azure-portal) and re-validate the Skill.

## Validation performed during registering a Skill

When an end user attempts to connect to your Skill from their Power Virtual Agents bot, they'll first need to [import the Skill to Power Virtual Agents](/power-virtual-agents/advanced-use-skills). Your Skill will go through a series of validation checks. A failure of one of these checks may result in an error message as described in this table.

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

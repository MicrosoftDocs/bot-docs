---
title: Implement a skill for Power Virtual Agents
description: Learn how to implement a skill that can be used in Power Virtual Agents, using the Bot Framework SDK.
keywords: skills
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: reference
ms.service: bot-service
ms.date: 11/02/2021
monikerRange: 'azure-bot-service-4.0'
---

# Implement a skill for use in Power Virtual Agents

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

A skill is a bot that can be used by another bot. In this way you can create a single user-facing bot and extend it with one or more skills. You can learn more about skills in general in [Skills Overview](skills-conceptual.md), and how to build them in [Implement a skill](skill-implement-skill.md). Alternatively, the Virtual Assistant templates contain a set of [pre-built skills](bot-builder-skills-overview.md) you can customize and deploy instead of building one from scratch.

If you expect that your skill will be consumed from a [Power Virtual Agents](https://powerva.microsoft.com/#/) bot, there are some additional restrictions placed on your skill you'll need to account for.

## Manifest restrictions

Power Virtual Agents places restrictions on what you may declare in your [skill manifest](./skills-write-manifest.md).

- You may declare only 100 or fewer actions.
- Each action is limited to 25 or fewer inputs or outputs.
- You can't use the array type for inputs or outputs.

## Same-tenant restriction

In order to ensure compliance and adequate governance of custom skills being registered for use within Power Virtual Agents, your skill bot must be a registered application in Azure Active Directory. Upon adding a skill, we validate if the skill's application ID is the in the tenant of the signed in user and the skills endpoint matches the registered application's `Home Page URL`.

Before you can register your bot as a skill within Power Virtual Agents, you must ensure that for the bot, the [home page in the Azure Portal](/azure/active-directory/app-proxy/application-proxy-configure-custom-home-page#change-the-home-page-in-the-azure-portal) is set to the bot's skill manifest URL.

## Validation performed during registering a Skill

When an end user attempts to connect to your skill from their Power Virtual Agents bot, they'll first need to [import the skill to Power Virtual Agents](/power-virtual-agents/advanced-use-skills). Your skill will go through a series of validation checks. A failure of one of these checks may result in an error message as described in this table.

| Validation step | Error code | Error message | Description or mitigation
| :-- | :-- | :-- | :--
| Manifest URL is valid | `URL_MALFORMED`, `URL_NOT_HTTPS` | The link isn't valid; The link must begin with https:// | Re-enter the link as a secure URL.
| Manifest is retrievable | `MANIFEST_FETCH_FAILED` | We ran into problems getting the skill manifest. | Verify your manifest URL is a link to your manifest; try opening your manifest URL in a web browser. If the URL renders the page within 10 seconds, re-register your skill.
| Manifest is readable | `MANIFEST_TOO_LARGE` | The manifest is too large. | Your manifest must be 500 KB or less.
| Manifest is readable | `MANIFEST_MALFORMED` | The manifest is incompatible. | Check if the manifest is a valid JSON file. Check if the manifest contains required properties, such as `name`, `msaAppId`, and so on. See [Manifest restrictions](#manifest-restrictions) for more information.
| Skill is not yet registered | `MANIFEST_ALREADY_IMPORTED` | This skill has already been added to your bot. | Delete the skill and register it again.
| Manifest endpoint and homepage domains match | `MANIFEST_ENDPOINT_ORIGIN_MISMATCH` | There's a mismatch in your skill endpoints. | You Azure AD app's homepage URL domain and manifest URL domain must match. See [Same-tenant restriction](#same-tenant-restriction)
| Skill is hosted in signed in user's tenant | `APPID_NOT_IN_TENANT` | To add a skill, it must first be registered.| A global administrator must register the skill into the signed in user's organization.
| Actions are limited | `LIMITS_TOO_MANY_ACTIONS` | The skill is limited to 100 actions.|There are too many skill actions defined in skill manifest. Remove actions and try again.
| Action input parameters are limited | `LIMITS_TOO_MANY_INPUTS` | Actions are limited to 25 inputs.|There are too many skill action input parameters. Remove parameters and try again.
| Action output parameters are limited | `LIMITS_TOO_MANY_OUTPUTS` | Actions are limited to 25 outputs.|There are too many skill action output parameters. Remove parameter and try again.
| Skill count is limited | `LIMITS_TOO_MANY_SKILLS` | Your bot can have a maximum of 25 skills.| There are too many skills added into a bot. Remove an existing skill and try again.
| Security token is valid | `AADERROR_OTHER` | It looks like something went wrong.|There may be a transient error to acquire a security token to trigger the skill. Retry importing the skill.
| Skill is healthy | `ENDPOINT_HEALTHCHECK_FAILED`, `HEALTH_PING_FAILED` | Something went wrong while checking your skill. | Power Virtual Agents received an unknown response when sending an `EndOfConversation` activity to your skill. Make sure your skill is running and responding correctly.
| Skill is authorized | `ENDPOINT_HEALTHCHECK_UNAUTHORIZED` | This skill has not allow-listed your bot. | Check if your bot has been added to the skill's allow list. For more information, see the Power Virtual Agents how to [Configure a Skill](/power-virtual-agents/configuration-add-skills#configure-a-skill-for-use-in-power-virtual-agents).

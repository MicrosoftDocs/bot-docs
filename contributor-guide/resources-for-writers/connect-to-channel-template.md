---
#Required; ≤ ~60 characters; include product name (Bot Framework).
#    Can exceed 60 characters; although it will be truncated in search, The keywords are still valuable.
title: Connect a Bot Framework bot to {channel-name}
#Required; ~100 to ~160 characters; include scenario and benefit.
# For the last part of the description, mention whether this applies to the Azure Bot Service, a custom channel adapter, or both.
description: Learn how to configure bots to use {channel-name} to communicate with people. See how to connect bots to {channel-name} through {the-type-of-connection}.
author: #Required; a current writer's GitHub alias, with correct capitalization.
manager: shellyha #Required; current manager's Github alias.
ms.author: #Required; Microsoft alias of author; can use the alias of an MS FTE on the team (yours or iawilt).
ms.reviewer: micchow
ms.service: bot-service
ms.topic: how-to
ms.date: #Required; mm/dd/yyyy format.
ms.custom: template-how-to
monikerRange: 'azure-bot-service-4.0'
---

<!-- This template provides the basic structure of a how-to connect-to-a-particular-channel article.
    1. Make sure all placeholder text is replaced with appropriate values.
    1. Remove all the comments in this template before you sign-off or merge to the main branch.
-->

# How to connect a bot to {channel-name}

<!-- Introductory paragraphs:

This article describes how to connect a bot to {channel-name}.
The {channel-name} channel lets your bot communicate with users through {summary-of-service-or-device}.

# If there's an official custom channel adapter, mention it with a link to its docs (in V5, the SDK proper will no longer contain concrete custom channel adapters).

-->

<!-- Add explanation of the most important moving pieces of the channel's docs, with links, such as:
- the root of their dev docs
- if different, the start of the bot-specific topic set
- their schema docs
# our activity and card schema docs:
- For more information about the Bot Framework protocols, see:
  [Bot Framework activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md) and
  [Bot Framework card schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-cards.md).
-->

## Prerequisites

<!-- Remove this section if prerequisites are not needed

# If the how-to requires Azure.
- An Azure subscription. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A bot published to Azure that you want to connect to {channel-name}.

# If the how-to requires a channel-specific developer account, prefer their phrasing for the link for creating the account.
- If you don't have a {channel-name} developer account, [{sign-up-for-one}]({their-link}).

# Add additional prereqs as necessary.

-->

<!--
If the user need to go back and forth between the Azure portal and the channel's developer site.
Make explicit which site the subprocess is for, both in the heading and as the first step of the procedure.
-->
## {do-subprocess} in {site}

<!-- steps, with optional preamble-->

## Test the connection

<!-- Procedures
# This should be pretty light, as the sample bot should be ready'ish.
-->

## Additional information

<!-- Does the channel support a testing/staging environment for bot clients?
# If so, point to their docs for managing that.
-->

### Bot support in the {channel-name}

<!-- Overview of supported aspects of the channel...if we can get this info from the devs.
# What gets mapped to what, in both directions?
   - what Bot Framework features are supported or not? How are they surfaced in the channel?
   - what channel features are supported or not? How are they surfaced as an activity?
# Are there novel timing restrictions?
# Are there novel authentication requirements?
-->

## Next steps

<!-- These links assume the article is in the root of the `articles` folder. -->

- For information about channel support in the Bot Connector Service, see [Connect a bot to channels](bot-service-manage-channels.md).
- For information about building bots, see [How bots work](v4sdk/bot-builder-basics.md) and the [Create a bot with the Bot Framework SDK](bot-service-quickstart-create-bot.md) quickstart.
- For information about deploying bots, see [Tutorial: Provision a bot in Azure](tutorial-provision-a-bot.md) and [Tutorial: Publish a basic bot](tutorial-publish-a-bot.md).

<!--
Remove all the comments in this template before you sign-off or merge to the 
main branch.
-->

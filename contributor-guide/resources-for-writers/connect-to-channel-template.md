---
#Required; ≤ ~60 characters; include product name (Bot Framework).
#    Can exceed 60 characters; although it will be truncated in search, The keywords are still valuable.
title: Connect a Bot Framework bot to {channel-name}
#Required; ~100 to ~160 characters; include scenario and benefit.
# For the last part of the description, mention whether this applies to the Azure Bot Service, a custom channel adapter, or both.
description: Learn how to configure bots to use {channel-name} to communicate with people. See how to connect bots to {channel-name} through {the-type-of-connection}.
author: #Required; a current writer's GitHub alias, with correct capitalization.
manager: #Required; current manager's Github alias.
ms.author: #Required; Microsoft alias of author; can use the alias of an MS FTE on the team.
ms.service: bot-service
ms.topic: how-to
ms.date: #Required; mm/dd/yyyy format.
ms.custom: template-how-to
---

<!-- This template provides the basic structure of a how-to connect-to-a-particular-channel article.
    1. Make sure all placeholder text is replaced with appropriate values.
    1. Remove all the comments in this template before you sign-off or merge to the main branch.
-->

# How to connect a bot to {channel-name}

<!-- Introductory paragraphs:

This article describes how to connect {an echoBot|the xxx sample bot} to {channel-name}.
If you'd like to learn more about the features supported when connecting your bot to {channel X}, see [{channel concept}]({link}).

-->

<!-- Add explanation of the most important moving pieces of the channel's docs, with links, such as:
- the root of their dev docs
- if different, the start of the bot-specific topic set
- their schema docs
# our activity and card schema docs:
- For more information about the Bot Framework protocols, see the Bot Framework
  [activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md) and
  [card schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-cards.md).
-->

## Feature support

<!-- Overview of supported aspects of the channel...if we can get this info from the devs.
# What gets mapped to what, in both directions?
   - what Bot Framework features are supported or not?
   - what channel features are supported or not?
# Are there novel timing restrictions?
# Are there novel authentication requirements?
# If there are multiple ways to connect to the channel (Azure vs channel adapter), how does their support/feature set differ, if at all?
-->

## Prerequisites

<!-- Remove this section if prerequisites are not needed

# If the how-to requires Azure.
- If you don't have an Azure account, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.

# If the how-to requires a channel-specific developer account, prefer their phrasing for the link for creating the account.
- If you don't have a {channel-name} developer account, [{sign-up-for-one}]({their-link}).

# Add additional prereqs as necessary.

# If the reference bot is available in only one language:
- The bot to connect to the channel&mdash;this article uses the [{echo-bot|adapter-sample-bot}](project-folder-for-the-sample-in-the-samples-repo) as an example.

# Otherwise, only mention and link to the relevant language-specific samples.
# Use the same set of language tabs in later sections as necessary.

### [C#](#tab/csharp)

- The bot to connect to the channel&mdash;this article uses the [{echo-bot|adapter-sample-bot}](C#-project-folder-for-the-sample-in-the-samples-repo) as an example.
- [.NET Core SDK](https://dotnet.microsoft.com/download) version 3.1 or later.

### [JavaScript](#tab/javascript)

- The bot to connect to the channel&mdash;this article uses the [{echo-bot|adapter-sample-bot}](JavaScript-project-folder-for-the-sample-in-the-samples-repo) as an example.
- [Node.js](https://nodejs.org/) version 10.14 or later.

### [Java](#tab/java)

- The bot to connect to the channel&mdash;this article uses the [{echo-bot|adapter-sample-bot}](Java-project-folder-for-the-sample-in-the-samples-repo) as an example.
- [Maven](https://maven.apache.org/).
- JDK 1.8 or later.

### [Python](#tab/python)

- The bot to connect to the channel&mdash;this article uses the [{echo-bot|adapter-sample-bot}](Python-project-folder-for-the-sample-in-the-samples-repo) as an example.
- Python [3.6][https://www.python.org/downloads/release/python-369/], [3.7][https://www.python.org/downloads/release/python-375/], or [3.8][https://www.python.org/downloads/release/python-383/]

---

-->

## Connection options

<!--

The following instructions show you how to connect a bot to {channel-name}.

# If there are two or more options, call them out in a list, with links to the H3s; otherwise, remove this bit, and just keep the one H3 that makes sense.

Select one of the following ways to connect your bot:

- [{Connect your bot in the Azure portal}](#connect-your-bot-in-the-azure-portal)
- [{Connect your bot using the channel adapter}](#connect-your-bot-using-the-channel-adapter)

-->

### Connect your bot in the Azure portal

### Connect your bot using the channel adapter

<!-- Procedures:
# For the 4 channel adapters that have samples, follow the lead in the sample.
# Each distinct way to connect your bot should probably include a mention to deploy your bot as part of the process (with link to deploy article) and any other steps to make it fully user ready.
-->

## Test the connection

<!-- Procedures
# This should be pretty light, as the sample bot should be ready'ish.
-->

## ???

<!-- Does the channel support a testing/staging environment for bot clients?
# If so, point to their docs for managing that.
-->

## Next steps

- For information about channel support in the Bot Connector Service, see [Connect a bot to channels](../../articles/bot-service-manage-channels.md).
- For information about building bots, see [How bots work](../../articles/v4sdk/bot-builder-basics.md) and the [Create a bot with the Bot Framework SDK](../../articles/bot-service-quickstart-create-bot.md) quickstart.
- For information about deploying bots, see [Deploy your bot](../../articles/bot-builder-deploy-az-cli.md) and [Set up continuous deployment](../../articles/bot-service-build-continuous-deployment.md).

<!--
Remove all the comments in this template before you sign-off or merge to the 
main branch.
-->

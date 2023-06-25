---
title: Regionalization support
description: Learn about regionalization in Azure AI Bot Service and how to meet your data compliance requirements.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: conceptual
ms.date: 01/11/2023
monikerRange: 'azure-bot-service-4.0'
---

# Regionalization in Azure AI Bot Service

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Azure AI Bot Service is a global Azure service that allows bot developers in all regions to register their bot and connect it to different channels. This also lets developers meet compliance obligations, especially following the [Schrems II decision](https://blogs.microsoft.com/eupolicy/2021/05/06/eu-data-boundary/).

Use an Azure Bot resource to register a bot with regional Azure AI Bot Services. Adding regional settings to a bot ensures user personal data is preserved, stored, and processed within certain geographic boundaries (like EU boundaries). This article explains the areas of bot development impacted by regionalization and where to update settings to maintain compliance.

## Deploy locally developed regional Azure bots

Your bot can be hosted anywhere, even if you have a regional Azure Bot resource. To maintain complete end-to-end data residency, however, you should host your bot code in the same locality as your Azure Bot resource. For example, developers hosting bots in the European Union will want to ensure their bots are deployed in a region within EU geographical boundaries.

For more information about deploying regionalized bots, see [Provision and publish a bot](../provision-and-publish-a-bot.md).

## Register regional Azure bots

When you create a bot in Azure, you can set its region to maintain data compliance. When you create a bot, make sure to create your resource in a geographically compliant region. For more information, see [Create an Azure Bot resource](abs-quickstart.md#create-the-resource).

>[!NOTE]
> Bot data may go beyond geographical boundaries as bot end-to-end scenarios may depend on many services.
> The regional Azure AI Bot Service only supports data in Azure AI Bot Services. Other Azure services&mdash;such as Azure AI services&mdash;and third-party channels may not align with compliance obligation and run the risk of data leaving the geographical region.  

For guidance about reliability support in Azure AI Bot Service, see [What is reliability in Azure AI Bot Service](/azure/reliability/reliability-bot).

## Add authentication to a regional Azure bot

Sometimes a bot must access secured online resources on behalf of the user. [OAuth](bot-builder-concept-authentication.md) is used to authenticate the user and authorize the bot.

- For information about which regions and clouds are supported, see [supported OAuth URLs](../ref-oauth-redirect-urls.md).
- For information on how to add user authentication, see [Add authentication to a bot](bot-builder-authentication.md).

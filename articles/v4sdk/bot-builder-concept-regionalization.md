---
title: Regionalization in Azure Bot Services
description: Learn about regionalization in Azure Bot Service and how to meet your data compliance requirements.
keywords: azure bot services, regionalization, schrems ii, region
author: emgrol
ms.author: v-eolshefski
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: conceptual
ms.date: 04/05/2022
monikerRange: 'azure-bot-service-4.0'
---

# Regionalization in Azure Bot Service

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Azure Bot Service is a global Azure service that allows bot developers in all regions to register their bot and connect it to different channels. This also lets developers meet compliance obligations, especially following the [Schrems II decision](https://blogs.microsoft.com/eupolicy/2021/05/06/eu-data-boundary/).

Use an Azure Bot resource to register a bot with regional Azure Bot services. Adding regional settings to a bot ensures user personal data is preserved, stored, and processed within certain geographic boundaries (like EU boundaries). This article explains the areas of bot development impacted by regionalization and where to update settings to maintain compliance.

## Deploy locally developed regional Azure bots

Your bot can be hosted anywhere, even if you have a regional Azure Bot resource. To maintain complete end-to-end data residency, however, you should host your bot code in the same locality as your Azure Bot resource. For example, developers hosting bots in the European Union will want to ensure their bots are deployed in a region within EU geographical boundaries.

For more information about deploying local bots, see [Deploy your bot in Azure](../bot-builder-deploy-az-cli.md).

## Register regional Azure bots

When you create a bot in Azure, you can set its region to maintain data compliance. When you create a bot, make sure to create your resource in a geographically compliant region. For more information, see [Create an Azure Bot resource](abs-quickstart.md#create-the-resource).

>[!NOTE]
> Bot data may go beyond geographical boundaries as bot end-to-end scenarios may depend on many services.
> The regional Azure Bot service only supports data in Azure Bot services. Other Azure services&mdash;like LUIS and QnA Maker&mdash;and third-party channels may not align with compliance obligation and run the risk of data leaving the geographical region.  

## Add authentication to a regional Azure bot

Sometimes a bot must access secured online resources on behalf of the user. [OAuth](bot-builder-concept-authentication.md) is used to authenticate the user and authorize the bot.

The following regions are supported for OAuth and regional Azure Bot services:

[!INCLUDE [regionalization](../includes/azure-bot-regionalization.md)]

For more information, see [Add authentication to a bot](bot-builder-authentication.md).

## Regional Azure bot channels

The regional Azure Bot Service supports the following channels:

- [WebChat](../bot-service-channel-connect-webchat.md)
- [DirectLine](../bot-service-channel-directline.md)
- [DirectLine Speech](../bot-service-channel-connect-directlinespeech.md)
- [Facebook](../bot-service-channel-connect-facebook.md)

Further channel support may be added in the future. For more information, see the article about [managing channels](../bot-service-manage-channels.md).

## Additional information

For information about adding regionalization to Bot Framework Composer bots, see the relevant articles about adding regional settings, including:

- [Create your first bot with Azure](/composer/quickstart-create-bot-with-azure.md) in Composer
- [Provision Azure resources](/composer/how-to-provision-azure-resources.md) in Composer
- [Publish your bot to Azure](/composer/how-to-publish-bot.md) from Composer

---
title: Use direct line app service extension within a VNET
titleSuffix: Bot Service
description: Learn how to use a direct line app service extension with an Azure Virtual Network. See how to create an environment and configure an outbound connection.
services: bot-service
manager: kamrani
ms.service: bot-service
ms.topic: conceptual
ms.author: kamrani 
ms.date: 07/25/2019
---

# Use direct line app service extension within a VNET

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to use the Direct Line App Service Extension with an Azure Virtual Network (VNET).

## Create an App Service Environment and other Azure resources

1. Direct line app service extension is available on all **Azure App Services**, including those hosted within an **Azure App Service Environment**. An Azure App Service Environment provides isolation and is ideal for working within a VNET.
    - Instructions for creating an external App Service Environment can be found in [Create an External App Service environment](https://docs.microsoft.com/azure/app-service/environment/create-external-ase) article.
    - Instructions for creating an internal App Service Environment can be found in [Create and use an Internal Load Balancer App Service Environment](https://docs.microsoft.com/azure/app-service/environment/create-ilb-ase) article.
1. Once you have created your App Service Environment, you need to add an App Service Plan inside of it where you can deploy your bots (and thus run Direct Line App Service Extension). To do this:
    - Go to https://portal.azure.com/
    - Create a new “App Service Plan” resource.
    - Under Region, select your App Service Environment
    - Finish creating your App Service Plan

## Configure the VNET Network Security Groups (NSG)

1. Direct Line App Service Extension requires an outbound connection so that it can issue HTTP requests. This can be configured as an outbound rule in your VNET NSG that is associated with the App Service Environment's subnet. The rule that required is as follows:

|Field|Value|
|---|---|
|Source|Any|
|Source Port|*|
|Destination|Service Tag|
|Destination Service Tag|AzureBotService|
|Destination port ranges|443|
|Protocol|Any|
|Action|Allow|

---
title: Use Direct Line App Service extension within a VNET in Bot Framework SDK
description: Use a Direct Line App Service extension with an Azure Virtual Network. Create an environment and configure an outbound connection.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: Daniel.Evans
ms.service: bot-service
ms.topic: how-to
ms.date: 09/01/2019
ms-custom: abs-meta-21q1 
---

# Use Direct Line App Service extension within a VNET

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to use the Direct Line App Service extension with an Azure Virtual Network (VNET).

## Create an App Service Environment and other Azure resources

1. The Direct Line App Service extension is available on all **Azure App Services**, including those hosted within an **Azure App Service Environment**. An Azure App Service Environment provides isolation and is ideal for working within a VNET.
    - Instructions for creating an external App Service Environment can be found in [Create an External App Service environment](/azure/app-service/environment/create-external-ase) article.
    - Instructions for creating an internal App Service Environment can be found in [Create and use an Internal Load Balancer App Service Environment](/azure/app-service/environment/create-ilb-ase) article.
1. Once you have created your App Service Environment, you need to add an App Service Plan inside of it where you can deploy your bots (and thus run Direct Line App Service extension). To do this:
    - Go to the [Azure portal](https://portal.azure.com/).
    - Create a new "App Service Plan" resource.
    - Under Region, select your App Service Environment
    - Finish creating your App Service Plan

## Configure the VNET Network Security Groups (NSG)

1. Direct Line App Service extension requires an outbound connection so that it can issue HTTP requests. This can be configured as an outbound rule in your VNET NSG that is associated with the App Service Environment's subnet. The rule that required is as follows:

|Field|Value|
|---|---|
|Source|Any|
|Source Port|*|
|Destination|Service Tag|
|Destination Service Tag|AzureBotService|
|Destination port ranges|443|
|Protocol|Any|
|Action|Allow|

---
title: Direct Line app service extension with Private Links
titleSuffix: Bot Service
description: Direct Line app service extension with Private Links
services: bot-service
manager: kamrani
ms.service: bot-service
ms.topic: conceptual
ms.author: dev
ms.date: 08/27/2020
---

# The Direct Line App Service Extension and Private Links

> [!NOTE]
> The usage of Private Links as described in this documentation is current offered in a **preview** capacity.

[!INCLUDE[applies-to-v4](includes/applies-to.md)]

This article describes how to combine the Direct Line app service extension and [Azure Private Links](https://azure.microsoft.com/en-us/services/private-link/) to restrict traffic from your bot and the **Azure Bot Service** to within the Azure backbone.


## Prerequisites

To perform the steps described next, you need to have the **Bot Channel Registration** resource and the related **Bot App Service** (your bot) in Azure, with the **Direct Line app service extension** enabled.

## Limitations

1. Private Links as used here are only able compatible with the Direct Line app service extension channel. All other channels require incoming traffic to the bot.
1. Private Links require a VNET be present in the resource group the Private Link is being added to.
1. During the preview period the Private Link must be in the same Azure region as the App Gateway it is targeting.
1. The below regions are supported during the preview period:

    |Region|Gateway Resource ID|Sub-Resource Name|
    |---|---|---|
    |WestUS|/subscriptions/ac2864c6-a747-49d0-a51c-35aff431c78f/resourceGroups/Intercom-directline-westus2/providers/Microsoft.Network/applicationGateways/bc-l7st-westus2|bc-l7st-westus2|
    |NorthEurope2|/subscriptions/ac2864c6-a747-49d0-a51c-35aff431c78f/resourceGroups/Intercom-directline-northeurope2/providers/Microsoft.Network/applicationGateways/bc-l7st-northeurope2|bc-l7st-northeurope2|
    |USGov Virginia|placeholder|placeholder|

## Create a Private Link via the Azure Portal

1. From the **Azure** portal click ***Create a Resource*** and begin the process of creating a **Private Link**.
1. When presented with the option to ***Create a Private Endpoint*** or ***Create a Private Link Service***, select **Create a Private Endpoint**.

    ![Create a Private Link](./media/private-links/private-endpoint-create.png)
1. Supply details of your **Subscription** and **Resource Group** then supply a name for your **Private Link** and be sure to select a **region** from the list in the **Limitations** section above.

    ![Configure Basic Private Link](./media/private-links/private-link-basic.PNG)
1. For **Connection Method** select to **Connect to an Azure resource by resource ID or alias**.
1. Provide the **Resource ID** and **Sub-Resource** from the table provided in the **Limitations** section above.

    ![Configure Rousource for Private Link](./media/private-links/private-link-resource.PNG)
1. Finish configuration and deployment of your **Private Link** as normal.
1. If after deployment your Private Link reports a status of "***approval pending***" it will require a manual sign off from the Azure Bot Service team. This is normally completed within 1 work day.

## Configure DNS
1. Once your **Private Endpoint** has been approved you will need to configure your **VNET** internal DNS to resolve the appropriate domain names to the IP of your **Private Endpoint**.
1. From the **Azure** portal click **Create a Resource** and begin the process of creating a [Private DNS Zone](https://docs.microsoft.com/en-us/azure/dns/private-dns-privatednszone).
1. The name of your **Private DNS Zone** must match the URL the **DL-ASE** uses to contact the service, in commercial Azure this value is **botframework.com**, in **USGov** clouds **botframework.us**.


## Next steps

> [!div class="nextstepaction"]
> [Continue developing with the Direct Line Extension](./bot-service-channel-directline-extension.md)

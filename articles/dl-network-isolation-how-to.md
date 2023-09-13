---
title: Configure network isolation
description: Learn how to configure your bot in a virtual network to restrict user access to your bot.
displayName: private network, isolated network
author: JonathanFingold
ms.author: v-bondkendal
manager: iawilt
ms.reviewer: yiba
ms.service: bot-service
ms.topic: how-to
ms.date: 07/01/2022
---

# Configure network isolation

**Commencing September 1, 2023, it is strongly advised to employ [Azure Service Tag](https://learn.microsoft.com/en-us/azure/virtual-network/service-tags-overview#available-service-tags) method for network isolation. The utilization of DL-ASE should be limited to highly specific scenarios. Prior to implementing this solution in a production environment, we kindly recommend consulting your support team for guidance.**

You can add network isolation to an existing Direct Line App Service extension bot.
A private endpoint lets your network isolated bot communicate with required Bot Framework services so that the bot can run correctly while being limited to the virtual network.

To add network isolation to your bot:

1. Use a virtual network and configure the network to prevent outbound traffic. At this point, your bot will lose the ability to communicate with other Bot Framework services.
1. Configure private endpoints to restore connectivity.
1. Restart you app service and test your bot within your isolated network.
1. Disable public network access to your bot.

## Prerequisites

- An Azure account. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
  - A subscription with permission to create Azure Virtual Network and network security group resources.
- A working Direct Line App Service extension bot.
  - Your bot uses the Bot Framework SDK for C# or JavaScript, version 4.16 or later.
  - Your bot has named pipes enabled.
  - Your bot's app service has the Direct Line App Service extension enabled.
- A Web Chat control connected to your bot's Direct Line client.

To confirm that your existing bot is configured correctly:

1. In a browser, open the Direct Line client endpoint for your bot. For example, `https://<your-app_service>.azurewebsites.net/.bot`.
1. Verify the page displays the following:

    ```json
    {"v":"123","k":true,"ib":true,"ob":true,"initialized":true}
    ```

    - **v** shows the build version of the Direct Line App Service extension.
    - **k** indicates whether the extension was able to read an extension key from its configuration.
    - **initialized** indicates whether the extension was able to download bot metadata from Azure AI Bot Service.
    - **ib** indicates whether the extension was able to establish an inbound connection to the bot.
    - **ob** indicates whether the extension was able to establish an outbound connection from the bot.

## Create a virtual network

1. Go to [Azure portal](https://portal.azure.com).
1. Create an Azure Virtual Network resource in the same region as your bot.
    - This creates both a virtual network and a subnet.
    - Don't create any virtual machines.
    - For general instructions, see [Create a virtual network using the Azure portal](/azure/virtual-network/quick-create-portal).
1. Open the app service resource for your bot and enable virtual network integration.
    - Use the virtual network and subnet from the previous step.
    - For general instructions, see [Enable virtual network integration in Azure App Service](/azure/app-service/configure-vnet-integration-enable).
1. Create a second subnet. You'll use the second subnet later to add your private endpoint.

### Deny outbound traffic from your network

1. Open the network security group associated with your first subnet.
    - If no security group is configured, create one. For more information, see [Network security groups](/azure/virtual-network/network-security-groups-overview).
1. Under **Settings**, select **Outbound security rules**.
    1. In the outbound security rules list, enable **DenyAllInternetOutbound**.
1. Go to the app service resource for your bot.
1. Restart your app service.

### Verify that connectivity is broken

1. In a separate browser tab, open the Direct Line client endpoint for your bot. For example, `https://<your-app_service>.azurewebsites.net/.bot`.
1. Verify the page displays the following:

    ```json
    {"v":"123","k":true,"ib":true,"ob":true,"initialized":false}
    ```

    The value of `initialized` should be `false`, because your app service and app service extension are unable to connect to other Bot Framework services to initialize itself. Your bot is now isolated in a virtual network for outbound connections.

## Create your private endpoint

1. Go to [Azure portal](https://portal.azure.com).
1. Open the Azure Bot resource for your bot.
1. Under **Settings**, select **Networking**.
    1. On the **Private access** tab and select **Create a private endpoint**.
        1. On the **Resource** tab, for **Target sub-resource**, select **Bot** from the list.
        1. On the **Virtual Network** tab, select your virtual network and the second subnet you created.
        1. Save your private endpoint.

### Add your private endpoint to your bot's app service

1. Open the Azure App Service resource for your bot.
1. Under **Settings**, select **Configuration**.
    1. On the **Application settings** tab, select **New application setting**.
        1. Set **Name** to `DirectLineExtensionABSEndpoint`.
        1. Set **Value** to the private endpoint URL, for example, `https://<your_azure_bot>.privatelink.directline.botframework.com/v3/extension`.
        1. Save the new setting.

## Restart your app service and verify that connectivity is restored

1. Restart the app service for your bot.
1. In a separate browser tab, open the Direct Line client endpoint for your bot. For example, `https://<your-app_service>.azurewebsites.net/.bot`.
1. Verify the page displays the following:

    ```json
    {"v":"123","k":true,"ib":true,"ob":true,"initialized":true}
    ```

    The value of `initialized` should be `true`.

1. Use the Web Chat control connected to your bot's Direct Line client to interact with your bot inside the private network.

If your private endpoint doesn't work correctly, you can add a rule to allow outbound traffic specifically to Azure AI Bot Service.

> [!NOTE]
> This will make you virtual network a little less isolated.

1. Open the network security group associated with your first subnet.
1. Under **Settings**, select **Outbound security rules**.
    1. In the outbound security rules list, enable **AllowAzureBotService**.
1. Go to the app service resource for your bot.
1. Restart your app service.

## Disable public network access to your bot

You can block public access to your Azure AI Bot Service and only allow access through Private Endpoint. You can disable network access of Azure AI Bot Service in Azure portal.

> [!TIP]
> This will unconfigure the Teams channels. No other channels (except Direct Line) can be configurated or updated in Azure portal.

1. Go to [Azure portal](https://portal.azure.com).
1. Open the app service for your bot.
1. Disable public network access.

## Additional information

### Virtual network configuration

You have a couple options to configure your bot for a virtual network.

- Create a virtual network and then enable Azure App Service within the network. This is the option used in this article.
- Create an App Service environment and then add an App Service Plan within the environment.

### [Virtual network](#tab/network)

1. Create a virtual network.
1. Enable Azure App Service integration within the virtual network.

These are the steps used in this article, as described in the [Create a virtual network](#create-a-virtual-network) section.

For more information, see [Create a virtual network using the Azure portal](/azure/virtual-network/quick-create-portal) and [Enable virtual network integration in Azure App Service](/azure/app-service/configure-vnet-integration-enable).

### [App Service Environment](#tab/environment)

The Direct Line App Service extension is available on all Azure App services, including those hosted within an Azure App Service Environment. An Azure App Service Environment provides isolation and is a good way to work within a virtual network.

1. Create an internal or external App Service Environment. For more information, see [Create an External App Service Environment](/azure/app-service/environment/create-external-ase) and [Create and use an Internal Load Balancer App Service Environment](/azure/app-service/environment/create-ilb-ase).
1. Add an App Service Plan inside your environment. You can deploy your bots&mdash;such as a Direct Line App Service extension bot&mdash;within your plan.
    1. In the [Azure portal](https://portal.azure.com), create a new App Service Plan resource.
    1. Under **Region**, select your App Service Environment.
    1. Finish creating your App Service Plan.

---

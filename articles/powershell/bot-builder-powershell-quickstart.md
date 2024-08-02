---
title: Publish a bot with Azure PowerShell - Azure AI Bot Service
description: Learn how to publish a bot with Azure PowerShell.
author: iaanw
ms.author: iawilt
manager: leeclontz
ms.topic: how-to
ms.service: azure-ai-bot-service
ms.date: 02/23/2021
ms.custom:
  - devx-track-azurepowershell
  - modeapi
  - evergreen
---

# Create and publish a bot with Azure PowerShell

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article shows you how to use Azure PowerShell to create a bot and register it with Azure using an existing Microsoft Entra ID application registration.

Use an **Azure Bot** resource to host your bot.
You'll create and develop your bot locally and host it on Azure or a different platform. Follow the steps described in how-to [Register a bot with Azure](../bot-service-quickstart-registration.md). When you register your bot, you provide the web address where your bot is hosted. You can still host it in Azure.
    <!--?Should the lead in be "...and host it on any platform?-->

You can run these commands locally, using Azure PowerShell, or remotely through the Azure portal, using Azure CloudShell. For more information about Azure CloudShell, see the [Overview of Azure Cloud Shell](/azure/cloud-shell/overview).

> [!IMPORTANT]
> While the **Az.BotService** PowerShell module is in preview, you must install it separately using the `Install-Module` cmdlet.

Creating a bot with Azure AI Bot Service and creating a bot locally are independent, parallel ways to create a bot.

## Prerequisites

- If you don't have an Azure subscription, create a [free](https://azure.microsoft.com/free/)
  account before you begin.

- An existing Microsoft Entra ID application registration that can be used from any Microsoft Entra ID tenant.
  - To complete this quickstart, you'll need the app ID and secret for the application registration.

- [Install the Az PowerShell module](/powershell/azure/install-az-ps). This is required because the Az.BotService module is in preview.

  ```azurepowershell-interactive
  Install-Module -Name Az.BotService -AllowClobber
  ```

- If you choose to use Azure PowerShell locally:
  - Connect to your Azure account using the
    [Connect-AzAccount](/powershell/module/az.accounts/connect-azaccount) cmdlet.

## Choose your subscription

If you have multiple Azure subscriptions, choose the appropriate subscription in which the resources should be billed.

1. To list the subscriptions you can access, use the [Get-AzSubscription](/powershell/module/az.accounts/get-azsubscription) cmdlet.

    ```azurepowershell-interactive
    Get-AzSubscription
    ```

1. Set the specific subscription using the [Set-AzContext](/powershell/module/az.accounts/set-azcontext) cmdlet.

    You should use the same subscription for your bot as for the application registration.

    ```azurepowershell-interactive
    Set-AzContext -SubscriptionId "<your-subscription-name-or-id>"
    ```

## Create a resource group

If you don't already have an [Azure resource group](/azure/azure-resource-manager/management/overview) you want to use for your bot, create a new one using the [New-AzResourceGroup](/powershell/module/az.resources/new-azresourcegroup) cmdlet.

- A resource group is a logical container in which Azure resources are deployed and managed as a group.

The following example creates a resource group with the specified name and in the specified location.

```azurepowershell-interactive
New-AzResourceGroup -Name <your-resource-group-name> -Location <your-resource-group-location>
```

## Create a new bot service

To create a new bot service for your bot, you use the [New-AzBotService](/powershell/module/az.botservice/new-azbotservice)
cmdlet. The following example creates a new bot service with the specified values.

```azurepowershell-interactive
New-AzBotService -ResourceGroupName <your-resource-group-name> -Name <your-bot-handle> -ApplicationId <your-app-registration-id> -Location <your-bot-service-location> -Sku S1 -Description "<your-bot-description>" -Webapp
```

<!-- Will need to provide the secret when prompted for it. -->
<!--Unable to complete this step.-->

To retrieve the status of a bot service, you use the
[Get-AzBotService](/powershell/module/az.botservice/get-azbotservice) cmdlet. The following example
gets a list of all the resources in the specified resource group.
<!--?How do you get the status of each service/resource?-->

```azurepowershell-interactive
Get-AzBotService -ResourceGroupName <your-resource-group-name>
```

## Initialize project folder

To initialize the project file folder, you use the
[Initialize-AzBotServicePrepareDeploy](/powershell/module/az.botservice/initialize-azbotservicepreparedeploy)
cmdlet. The following example initializes the specified file in the specified folder.
<!--?What about the languages other than C#?-->

```azurepowershell-interactive
Initialize-AzBotServicePrepareDeploy -CodeDir C:\tmp\MyEchoBot -ProjFileName MyEchoBot.csproj
```

## Publish bot service to Azure

To publish your bot service to Azure, you use the
[Publish-AzBotServiceApp](/powershell/module/az.botservice/publish-azbotserviceapp) cmdlet. The
following example publishes the specified bot service to Azure.

```azurepowershell-interactive
Publish-AzBotServiceApp -ResourceGroupName myResourceGroup -CodeDir D:\tmp\MyEchoBot -Name MyEchoBot
```

## Download code

To download the code to work on it locally, you use the
[Export-AzBotServiceApp](/powershell/module/az.botservice/export-azbotserviceapp) cmdlet. The
following example downloads the code for the specified bot service app in the specified resource
group.

```azurepowershell-interactive
Export-AzBotServiceApp -ResourceGroupName myResourceGroup -Name MyEchoBot
```

## Clean up resources

If the resources created in this article aren't needed, you can delete them by running the following
examples.

### Delete the Bot Service

To delete the Bot Service from the resource group, you use the
[Remove-AzBotService](/powershell/module/az.botservice/remove-azbotservice)
cmdlet. The following example deletes the bot service from the specified resource group.

```azurepowershell-interactive
Remove-AzBotService -Name MyEchoBot -ResourceGroupName myResourceGroup
```

### Delete the resource group

> [!CAUTION]
> The following example deletes the specified resource group and all resources contained within it.
> If resources outside the scope of this article exist in the specified resource group, they'll
> also be deleted.

```azurepowershell-interactive
Remove-AzResourceGroup -Name myResourceGroup
```

## Next steps

After you download the code, you can continue to develop the bot locally on your machine. Once you
test your bot and are ready to upload the bot code to the Azure portal, follow the instructions
listed under [set up continuous deployment](../bot-service-build-continuous-deployment.md) topic to
automatically update code after you make changes.

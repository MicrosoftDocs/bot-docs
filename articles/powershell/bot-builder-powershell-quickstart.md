---
title: Create a bot with Azure PowerShell - Bot Service
description: Learn how to create a bot with Azure PowerShell.
keywords: Quickstart, create bot, bot service, web app bot
author: mmiele
ms.author: v-mimiel
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 02/08/2021
ms.custom: devx-track-azurepowershell
---

# Create a bot with Azure PowerShell

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article shows you how to build a bot by using Azure PowerShell.

You have two approaches to create a bot with Azure PowerShell:

1. **Web App**. Create a bot and register it with Azure using a Web application as shown in this article. You use this approach if you develop and host a bot in Azure.
1. **Bot channels registration**. Create and develop your bot locally and host it on a platform different from Azure. When you register your bot, you provide the web address where your bot is hosted. You can still host it in Azure. Follow the steps described in the [Bot channels registration](~/bot-service-quickstart-registration.md) article.

[!INCLUDE [Azure vs local development](~/includes/snippet-quickstart-paths.md)]

## Prerequisites

- If you don't have an Azure subscription, create a [free](https://azure.microsoft.com/free/)
  account before you begin.

- If you choose to use Azure PowerShell locally:
  - [Install the Az PowerShell module](/powershell/azure/install-az-ps).
  - Connect to your Azure account using the
    [Connect-AzAccount](/powershell/module/az.accounts/connect-azaccount) cmdlet.
- If you choose to use Azure Cloud Shell:
  - See [Overview of Azure Cloud Shell](/azure/cloud-shell/overview) for
    more information.

> [!IMPORTANT]
> While the **Az.BotService** PowerShell module is in preview, you must install it separately using
> the `Install-Module` cmdlet.

```azurepowershell-interactive
Install-Module -Name Az.BotService -AllowClobber
```

- An Azure application registration.

- If you have multiple Azure subscriptions, choose the appropriate subscription in which the
  resources should be billed. Select a specific subscription using the
  [Set-AzContext](/powershell/module/az.accounts/set-azcontext) cmdlet.

  ```azurepowershell-interactive
  Set-AzContext -SubscriptionId 00000000-0000-0000-0000-000000000000
  ```

## Create a resource group

Create an [Azure resource group](/azure/azure-resource-manager/management/overview)
using the [New-AzResourceGroup](/powershell/module/az.resources/new-azresourcegroup)
cmdlet. A resource group is a logical container in which Azure resources are deployed and managed as
a group.

The following example creates a resource group with the specified name and in the specified location.

```azurepowershell-interactive
New-AzResourceGroup -Name myResourceGroup -Location westus2
```

## Create a new bot service

To create a new bot service, you use the [New-AzBotService](/powershell/module/az.botservice/new-azbotservice)
cmdlet. The following example creates a new bot service with the specified values.

```azurepowershell-interactive
New-AzBotService -ResourceGroupName myResourceGroup -Name MyEchoBot -ApplicationId 00000000-0000-0000-0000-000000000000 -Location westus -Sku S1 -Description 'My Echo Bot' -Webapp
```

To retrieve the status of a bot service, you use the
[Get-AzBotService](/powershell/module/az.botservice/get-azbotservice) cmdlet. The following example
gets a list of all the bot services in the specified resource group and their statuses.

```azurepowershell-interactive
Get-AzBotService -ResourceGroupName myResourceGroup
```

## Initialize project folder

To initialize the project file folder, you use the
[Initialize-AzBotServicePrepareDeploy](/powershell/module/az.botservice/initialize-azbotservicepreparedeploy)
cmdlet. The following example initializes the specified file in the specified folder.

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
> If resources outside the scope of this article exist in the specified resource group, they will
> also be deleted.

```azurepowershell-interactive
Remove-AzResourceGroup -Name myResourceGroup
```

## Next steps

After you download the code, you can continue to develop the bot locally on your machine. Once you
test your bot and are ready to upload the bot code to the Azure portal, follow the instructions
listed under [set up continuous deployment](../bot-service-build-continuous-deployment.md) topic to
automatically update code after you make changes.

---
title: Use Azure CLI to create an Azure Bot resource
description: Learn how to create an Azure Bot resource with the Azure CLI and an ARM template.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: how-to
ms.date: 07/22/2022
ms.custom: template-how-to
---

# Use Azure CLI to create or update an Azure Bot resource

[!INCLUDE [applies-to-v4](./includes/applies-to-v4-current.md)]

This article describes how to create or update an Azure Bot resource with the Azure CLI and an Azure Resource Manager template (ARM template).

This is part of the larger process to provision and publish a bot.

- For information on the complete process, see [Provision and publish a bot](provision-and-publish-a-bot.md).
- For information on how to create an App Service resource, see [Use Azure CLI to create an App Service resource](provision-app-service.md).
- For instructions for how to use the Azure Portal, see the [Create an Azure Bot resource](v4sdk/abs-quickstart.md) quickstart.

## Prerequisites

[!INCLUDE [Azure CLI prerequisites](./includes/az-cli/prereqs.md)]

- This process uses an Azure Resource Manager template (ARM template) to create an Azure Bot resource for your bot.

  If you don't have the current templates, create a copy in your bot project of the **deploymentTemplates** folder: [C#](https://github.com/microsoft/botbuilder-dotnet/tree/main/generators/dotnet-templates/Microsoft.BotFramework.CSharp.EchoBot/content), [JavaScript](https://github.com/microsoft/botbuilder-js/tree/main/generators/generator-botbuilder/generators/app/templates/echo), [Python](https://github.com/microsoft/botbuilder-python/tree/main/generators/app/templates/echo/%7B%7Bcookiecutter.bot_name%7D%7D), or [Java](https://github.com/microsoft/botbuilder-java/tree/main/generators/generators/app/templates/echo/project).

> [!TIP]
> This is part of the larger process to provision and publish a bot.
> See how to [Provision and publish a bot](provision-and-publish-a-bot.md) for a complete list of prerequisites.

## Edit parameters file

Edit the parameters file for the ARM template to contain the values you want to use.

> [!IMPORTANT]
> You must use the same `appType` and `appId` values when you create your App Service and Azure Bot resources.

If your project doesn't yet contain the most recent ARM template and parameters files, you can copy them from the Bot Framework SDK repo for your language: [C#](https://github.com/microsoft/botbuilder-dotnet/tree/main/generators/dotnet-templates/Microsoft.BotFramework.CSharp.EchoBot/content), [JavaScript](https://github.com/microsoft/botbuilder-js/tree/main/generators/generator-botbuilder/generators/app/templates/echo), [Python](https://github.com/microsoft/botbuilder-python/tree/main/generators/app/templates/echo/%7B%7Bcookiecutter.bot_name%7D%7D), or [Java](https://github.com/microsoft/botbuilder-java/tree/main/generators/generators/app/templates/echo/project).

This table describes the _deployment parameters_ in the parameters file, for use with the `parameters` command option.
By default, the name of the parameters file is **parameters-for-template-AzureBot-with-rg.json**.

| Parameter | Type | Description |
|:-|:-|:-|
| `azureBotId` |String| Required. The globally unique and immutable bot ID. |
| `azureBotSku` | String | Optional. The SKU of the Azure Bot resource. Allowed values: "F0" (free) and "S1" (standard). Default is "S1". |
| `azureBotRegion` | String | Optional. The location of the Azure Bot. Allowed values: "global" and "westeurope". Default is "global". |
| `botEndpoint` |String| Optional. The messaging endpoint for your bot, such as `https://<appServiceName>.azurewebsites.net/api/messages`. |
| `appType` | String | Required. How the identities of your bot resources are managed. Allowed values are: "MultiTenant", "SingleTenant", and "UserAssignedMSI". Default is "MultiTenant". |
| `appId` |String| Required. The client ID or app ID from the identity resource you created earlier. This is the Microsoft app ID of the app service. |
| `UMSIName` | String | Optional. For user-assigned managed identity app types, the name of the identity resource.|
| `UMSIResourceGroupName` | String | Optional. For user-assigned managed identity app types, the resource group for the identity resource. |
| `tenantId` | String | Optional. For user-assigned managed identity and single-tenant app types, The Azure AD tenant ID for the identity resource. |

> [!TIP]
> The bot's messaging endpoint must be set before a published bot can receive messages.

Not all parameters apply to all app types.

### [User-assigned managed identity](#tab/userassigned)

Provide values for `UMSIName`, `UMSIResourceGroupName`, and `tenantId`.

### [Single-tenant](#tab/singletenant)

- Provide a value for `tenantId`.
- Leave `UMSIName` and `UMSIResourceGroupName` blank.

### [Multi-tenant](#tab/multitenant)

Leave `UMSIName`, `UMSIResourceGroupName`, and `tenantId` blank.

---

## Create the Azure Bot resource

To create the Azure Bot resource for your bot, use the following command.

```azurecli
az deployment group create –-resource-group <resource-group> --template-file <template-file-path> --parameters @<parameters-file-path>
```

| Option         | Description                                                                                         |
|:---------------|:----------------------------------------------------------------------------------------------------|
| resource-group | Name of the Azure resource group in which to create the App Service.                                |
| template-file  | The path to the ARM template for the App Service. The path can be relative or absolute.             |
| parameters     | The path to the parameters file to use with the ARM template. The path can be relative or absolute. |

For projects created with the latest generators, the ARM template and parameter files are located in the _DeploymentTemplates\DeployUseExistResourceGroup_ folder within the project.
The default file names are **template-AzureBot-with-rg.json** and **parameters-for-template-AzureBot-with-rg.json**.

## To update your Azure Bot resource

To add or update the messaging endpoint for your Azure Bot, use the following command.

```azurecli
az bot update --resource-group <resource group> --name <azureBotId> --endpoint <messaging-endpoint>
```

| Option         | Description                                                                                             |
|:---------------|:--------------------------------------------------------------------------------------------------------|
| resource-group | Name of the Azure resource group in which to create the App Service.                                    |
| name           | The globally unique and immutable bot ID.                                                               |
| endpoint       | The messaging endpoint for your bot, such as `https://<appServiceName>.azurewebsites.net/api/messages`. |

## Additional information

For more information about ARM templates, see [What are ARM templates?](/azure/azure-resource-manager/templates/overview) and [How to use Azure Resource Manager (ARM) deployment templates with Azure CLI](/azure/azure-resource-manager/templates/deploy-cli).

## Next steps

If you created the App Service as part of a bot deployment, see [Create resources with ARM templates](provision-and-publish-a-bot.md#create-resources-with-arm-templates) to continue the process.

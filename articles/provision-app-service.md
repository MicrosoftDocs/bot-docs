---
title: Use Azure CLI to create an App Service resource
description: Learn how to create an App Service resource with the Azure CLI and an ARM template.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: how-to
ms.date: 01/18/2023
ms.custom: template-how-to, devx-track-arm-template, devx-track-azurecli
---

# Use Azure CLI to create an App Service resource

[!INCLUDE [applies-to-v4](./includes/applies-to-v4-current.md)]

This article describes how to create an App Service resource with the Azure CLI and an Azure Resource Manager template (ARM template) as part of the process to provision and publish a bot. The app service is sometimes referred to as a _web app_.

> [!IMPORTANT]
> Python bots can't be deployed to a resource group that contains Windows services or bots.
> Multiple Python bots can be deployed to the same resource group; however, you need to create other services (such as Azure AI services) in another resource group.

- For information on the complete process, see how to [Provision and publish a bot](provision-and-publish-a-bot.md).
- For information on how to create an Azure Bot resource, see [Use Azure CLI to create an Azure Bot resource](provision-azure-bot.md).

[!INCLUDE [java-python-sunset-alert](includes/java-python-sunset-alert.md)]

## Prerequisites

[!INCLUDE [Azure CLI prerequisites](./includes/az-cli/prereqs.md)]

- This process uses an Azure Resource Manager template (ARM template) to create an App Service resource for your bot.

  If you don't have the current templates, create a copy in your bot project of the **deploymentTemplates** folder: [C#](https://github.com/microsoft/botbuilder-dotnet/tree/main/generators/dotnet-templates/Microsoft.BotFramework.CSharp.EchoBot/content), [JavaScript](https://github.com/microsoft/botbuilder-js/tree/main/generators/generator-botbuilder/generators/app/templates/echo), [Python](https://github.com/microsoft/botbuilder-python/tree/main/generators/app/templates/echo/%7B%7Bcookiecutter.bot_name%7D%7D), or [Java](https://github.com/microsoft/botbuilder-java/tree/main/generators/generators/app/templates/echo/project).

> [!TIP]
> This is part of the larger process to provision and publish a bot.
> See how to [Provision and publish a bot](provision-and-publish-a-bot.md) for a complete list of prerequisites.

## Edit parameters file

Edit the parameters file for the ARM template to contain the values you want to use.

> [!IMPORTANT]
> You must use the same `appType` value for your App Service and Azure Bot resources.

If your project doesn't yet contain the most recent ARM template and parameters files, you can copy them from the Bot Framework SDK repo for your language: [C#](https://github.com/microsoft/botbuilder-dotnet/tree/main/generators/dotnet-templates/Microsoft.BotFramework.CSharp.EchoBot/content), [JavaScript](https://github.com/microsoft/botbuilder-js/tree/main/generators/generator-botbuilder/generators/app/templates/echo), [Python](https://github.com/microsoft/botbuilder-python/tree/main/generators/app/templates/echo/%7B%7Bcookiecutter.bot_name%7D%7D), or [Java](https://github.com/microsoft/botbuilder-java/tree/main/generators/generators/app/templates/echo/project).

This table describes the _deployment parameters_ in the parameters file, for use with the `parameters` command option.
By default, the name of the parameters file is **parameters-for-template-BotApp-with-rg.json**.

| Parameter | Type | Description |
|:-|:-|:-|
| `appServiceName` | String | Required. The globally unique name of the app service. |
| `existingAppServicePlanName` | String | Optional. The name of an _existing_ app service plan with which to create the app service for the bot. |
| `existingAppServicePlanLocation` | String | Optional. The location of the _existing_ app service plan. |
| `newAppServicePlanName` | String | Optional. The name of the _new_ app service plan. |
| `newAppServicePlanLocation` | String | Optional. The location of the _new_ app service plan. |
| `newAppServicePlanSku` | Object | Optional. The SKU for the _new_ app service plan. Default is the S1 (Standard) service plan. |
| `appType` | String | Required. How the identities of your bot resources are managed. Allowed values: "MultiTenant", "SingleTenant", and "UserAssignedMSI". Default is "MultiTenant". |
| `appId` |String| Required. The client ID or app ID from the identity resource you created earlier. This is used as the Microsoft app ID of the app service. |
| `appSecret` |String| Optional. For single-tenant and multi-tenant app types, the password for the identity resource. |
| `UMSIName` | String | Optional. For user-assigned managed identity app types, the name of the identity resource.|
| `UMSIResourceGroupName` | String | Optional. For user-assigned managed identity app types, the resource group for the identity resource. |
| `tenantId` | String | Optional. For user-assigned managed identity and single-tenant app types, The Azure AD tenant ID for the identity resource. |

Not all parameters apply to all app types.

### [User-assigned managed identity](#tab/userassigned)

- Provide values for `UMSIName`, `UMSIResourceGroupName`, and `tenantId`.
- Leave `appSecret` blank.

### [Single-tenant](#tab/singletenant)

- Provide values for `appSecret` and `tenantId`.
- Leave `UMSIName` and `UMSIResourceGroupName` blank.

### [Multi-tenant](#tab/multitenant)

- Provide a value for `appSecret`.
- Leave `UMSIName`, `UMSIResourceGroupName`, and `tenantId` blank.

---

Some parameters are specific to using an existing or new app service plan.

### [Existing plan](#tab/existingplan)

- Provide values for `existingAppServicePlanName` and `existingAppServicePlanLocation`.
- Leave `newAppServicePlanName`, `newAppServicePlanLocation`, and `newAppServicePlanSku` blank.

### [New plan](#tab/newplan)

- Provide values for `newAppServicePlanName`, `newAppServicePlanLocation`, and `newAppServicePlanSku`.
- Leave `existingAppServicePlanName` and `existingAppServicePlanLocation` blank.

---

## Create the app service

Create the app service for your bot.

```azurecli
az deployment group create --resource-group <resource-group> --template-file <template-file-path> --parameters "@<parameters-file-path>"
```

| Option         | Description                                                                                         |
|:---------------|:----------------------------------------------------------------------------------------------------|
| resource-group | Name of the Azure resource group in which to create the app service.                                |
| template-file  | The path to the ARM template for the app service. The path can be relative or absolute.             |
| parameters     | The path to the parameters file to use with the ARM template. The path can be relative or absolute. |

For projects created with the latest generators, the ARM template and parameter files are located in the _DeploymentTemplates\DeployUseExistResourceGroup_ folder within the project.
The default file names are **template-BotApp-with-rg.json** and **parameters-for-template-BotApp-with-rg.json**.

> [!TIP]
>
> - The base URL for your app service is based on the app service name: `https:<app-service-name>.azurewebsites.net`.
> - The messaging endpoint for your bot will be the base URL plus `/api/messages`, such as `https:<app-service-name>.azurewebsites.net/api/messages`.

## Additional information

For more information about ARM templates, see [What are ARM templates?](/azure/azure-resource-manager/templates/overview) and [How to use Azure Resource Manager (ARM) deployment templates with Azure CLI](/azure/azure-resource-manager/templates/deploy-cli).

## Next steps

If you created the App Service as part of a bot deployment, see [Create resources with ARM templates](provision-and-publish-a-bot.md#create-resources-with-arm-templates) to continue the process.

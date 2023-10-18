---
title: Provision and publish a bot in Azure
description: Learn how to create Azure resources and publish your bot to Azure.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: how-to
ms.date: 10/26/2022
ms.custom: template-how-to, devx-track-azurecli
---

# Provision and publish a bot

[!INCLUDE [applies-to-v4](./includes/applies-to-v4-current.md)]

This article describes how to use the Azure CLI to create resources for your bot, prepare your bot for deployment, and deploy your bot to Azure.

This article assumes that you have a bot ready to be deployed. For information on how to create a simple echo bot, see [Create a bot with the Bot Framework SDK](bot-service-quickstart-create-bot.md). You can also use one of the samples provided in the [Bot Framework Samples repository](https://github.com/microsoft/BotBuilder-Samples#readme).

> [!TIP]
> This article creates an Azure Bot resource for your bot.
> Existing bots that use a Web App Bot resource or a Bot Channels Registration resource will continue to work, but you can't create new bots that use these resource types.

[!INCLUDE [java-python-sunset-alert](includes/java-python-sunset-alert.md)]

## Prerequisites

- For Java bots, install [Maven](https://maven.apache.org/).
- This process uses two Azure Resource Manager templates (ARM templates) to create resources for your bot.

  If you don't have the current templates, create a copy in your bot project of the **deploymentTemplates** folder: [C#](https://github.com/microsoft/botbuilder-dotnet/tree/main/generators/dotnet-templates/Microsoft.BotFramework.CSharp.EchoBot/content), [JavaScript](https://github.com/microsoft/botbuilder-js/tree/main/generators/generator-botbuilder/generators/app/templates/echo), [Python](https://github.com/microsoft/botbuilder-python/tree/main/generators/app/templates/echo/%7B%7Bcookiecutter.bot_name%7D%7D), or [Java](https://github.com/microsoft/botbuilder-java/tree/main/generators/generators/app/templates/echo/project).

[!INCLUDE [Azure CLI prerequisites](./includes/az-cli/prereqs.md)]

> [!NOTE]
> If your bot uses additional resources, such as a storage service or language services, these need to be deployed separately.

## Plan your deployment

Before you begin, make these decisions.

| Decision | Notes |
|:-|:-|
| How you'll manage the identities of your bot resources in Azure | You can use a user-assigned managed identity, a single-tenant app registration, or a mutli-tenant app registration. For more information, see [Create an identity resource](#create-an-identity-resource). |
| In which resource group or resource groups you'll create your bot resources | Until you're familiar with this process, we recommend using one resource group. For more information, see [Manage Azure resources](/azure/azure-resource-manager/management/). |
| Whether your bot will be _regional_ or _global_ | For information about regional bots, see [Regionalization in Azure AI Bot Service](v4sdk/bot-builder-concept-regionalization.md). |

[!INCLUDE [Note about support for each identity app type](includes/azure-bot-resource/identity-app-type-support.md)]

> [!IMPORTANT]
> Python bots can't be deployed to a resource group that contains Windows services or bots.
> However, multiple Python bots can be deployed to the same resource group.
> Create other services, such as Azure AI services, in a different resource group.

### Azure resources

Before you can deploy your bot, you create (or _provision_) the Azure resources it will need.
For some of the steps, you can use an existing resource or create a new one.

You may find it helpful to decide ahead of time on the names of the new resources you'll create and the names of the existing resources you'll use.
Your bot will use these types of resources.

- The Azure subscription that you'll use to provision, publish, and manage the bot
- One or more resource groups
- A user-assigned managed identity _or_ an Entra ID app registration
- An App Service Plan resource
- An App Service resource
- An Azure Bot resource

### Information used across resources

As you create resources in Azure, Azure will generate or request IDs, passwords, and other information that you'll need in later steps.
The following table lists the information beyond resource names you'll need to record, in which step it's generated, and in which steps it's used.

> [!CAUTION]
> Many of these IDs and passwords are sensitive information. For information about common security guidelines, see [Bot Framework security guidelines](v4sdk/bot-builder-security-guidelines.md).

### [User-assigned managed identity](#tab/userassigned)

| Information | Where generated or found | Where used |
|:-|:-|:-|
| Tenant ID | [Sign in and select subscription](#sign-in-and-select-subscription) | [Use Azure CLI to create an App Service resource](provision-app-service.md), [Use Azure CLI to create or update an Azure Bot resource](provision-azure-bot.md), [Update project configuration settings](#update-project-configuration-settings) |
| App type | [Create an identity resource](#create-an-identity-resource) | [Use Azure CLI to create an App Service resource](provision-app-service.md), [Use Azure CLI to create or update an Azure Bot resource](provision-azure-bot.md), [Update project configuration settings](#update-project-configuration-settings) |
| Client ID | [Create an identity resource](#create-an-identity-resource) | [Use Azure CLI to create an App Service resource](provision-app-service.md), [Use Azure CLI to create or update an Azure Bot resource](provision-azure-bot.md), [Update project configuration settings](#update-project-configuration-settings) |
| Base app service URL | [Use Azure CLI to create an App Service resource](provision-app-service.md) | [Use Azure CLI to create or update an Azure Bot resource](provision-azure-bot.md) |
| App Service name | [Use Azure CLI to create an App Service resource](provision-app-service.md) | [Publish your bot to Azure](#publish-your-bot-to-azure) |

### [Single-tenant](#tab/singletenant)

| Information | Where generated or found | Where used |
|:-|:-|:-|
| Tenant ID | [Sign in and select subscription](#sign-in-and-select-subscription) | [Use Azure CLI to create an App Service resource](provision-app-service.md), [Use Azure CLI to create or update an Azure Bot resource](provision-azure-bot.md), [Update project configuration settings](#update-project-configuration-settings) |
| App type | [Create an identity resource](#create-an-identity-resource) | [Use Azure CLI to create an App Service resource](provision-app-service.md), [Use Azure CLI to create or update an Azure Bot resource](provision-azure-bot.md), [Update project configuration settings](#update-project-configuration-settings) |
| App ID | [Create an identity resource](#create-an-identity-resource) | [Use Azure CLI to create an App Service resource](provision-app-service.md), [Use Azure CLI to create or update an Azure Bot resource](provision-azure-bot.md) |
| App password | [Create an identity resource](#create-an-identity-resource) | [Use Azure CLI to create an App Service resource](provision-app-service.md), [Update project configuration settings](#update-project-configuration-settings) |
| Base app service URL | [Use Azure CLI to create an App Service resource](provision-app-service.md) | [Use Azure CLI to create or update an Azure Bot resource](provision-azure-bot.md) |
| App Service name | [Use Azure CLI to create an App Service resource](provision-app-service.md) | [Publish your bot to Azure](#publish-your-bot-to-azure) |

### [Multi-tenant](#tab/multitenant)

| Information | Where generated or found | Where used |
|:-|:-|:-|
| App type | [Create an identity resource](#create-an-identity-resource) | [Use Azure CLI to create an App Service resource](provision-app-service.md), [Use Azure CLI to create or update an Azure Bot resource](provision-azure-bot.md), [Update project configuration settings](#update-project-configuration-settings) |
| App ID | [Create an identity resource](#create-an-identity-resource) | [Use Azure CLI to create an App Service resource](provision-app-service.md), [Use Azure CLI to create or update an Azure Bot resource](provision-azure-bot.md) |
| App password | [Create an identity resource](#create-an-identity-resource) | [Use Azure CLI to create an App Service resource](provision-app-service.md), [Update project configuration settings](#update-project-configuration-settings) |
| Base app service URL | [Use Azure CLI to create an App Service resource](provision-app-service.md) | [Use Azure CLI to create or update an Azure Bot resource](provision-azure-bot.md) |
| App Service name | [Use Azure CLI to create an App Service resource](provision-app-service.md) | [Publish your bot to Azure](#publish-your-bot-to-azure) |

---

## Sign in and select subscription

[!INCLUDE [Sign into Azure and select a subscription](./includes/az-cli/sign-in-select-subscription.md)]

## Create resource groups

If you don't already have an appropriate resource group, use the `az group create` command to create the new resource groups you need.

```azurecli
az group create --name "<group>" --location "<region>"
```

| Option   | Description                                       |
|:---------|:--------------------------------------------------|
| name     | The name of the resource group to create.         |
| location | The region in which to create the resource group. |

> [!TIP]
> Currently, regionalized bots are only available for Europe.
> For a regionalized bot, use "westeurope" for the location.

For more information, see [How to manage Azure resource groups with the Azure CLI](/cli/azure/manage-azure-groups-azure-cli).

## Create an identity resource

### [User-assigned managed identity](#tab/userassigned)

1. To create a user-assigned managed identity, use the `az identity create` command.
    On success, the command generates JSON output.

    ```azurecli
    az identity create --resource-group "<group>" --name "<identity>"
    ```

    | Option         | Description                                                     |
    |:---------------|:----------------------------------------------------------------|
    | resource-group | The name of the resource group in which to create the identity. |
    | name           | The name of the identity resource to create.                    |

    For more information, see the [az identity](/cli/azure/identity) reference.

1. Record values you'll need in later steps.
   1. The resource group name for the identity resource
   1. The name of the identity resource
   1. The `clientId` from the command output

### [Single-tenant](#tab/singletenant)

Use the following commands to create your app registration and set its password.
On success, these commands generate JSON output.

1. Use the `az ad app create` command to create an Entra ID app registration.
    This command generates an app ID that you'll use in the next step.

    ```azurecli
    az ad app create --display-name "<app-registration-display-name>" --sign-in-audience "AzureADMyOrg"
    ```
  
    | Option           | Description                                                                               |
    |:-----------------|:------------------------------------------------------------------------------------------|
    | display-name     | The display name for your app registration.                                               |
    | sign-in-audience | The supported Microsoft accounts for the app. Use `AzureADMyOrg` for a single tenant app. |

1. Use the `az ad app credential reset` command to generate a new password for your app registration.

   ```azurecli
   az ad app credential reset --id "<appId>"
   ```

1. Record values you'll need in later steps: the _app ID_ and _password_ from the command output.

For more information about `az ad app`, see the [command reference](/cli/azure/ad/app). For more information about the `sign-in-audience` parameter, see [sigInAudience values](/graph/api/resources/application#signinaudience-values).

### [Multi-tenant](#tab/multitenant)

[!INCLUDE [create-identity-multi-tenant](includes/az-cli/create-identity-multi-tenant.md)]

---

## Create resources with ARM templates

Create the App Service and the Azure Bot resources for your bot.
Both steps use an ARM template and the `az deployment group create` Azure CLI command to create the resource or resources.

1. Create an App Service resource for your bot. The App service can be within a new or existing App Service Plan.

    For detailed steps, see [Use Azure CLI to create an App Service](./provision-app-service.md).

1. Create an Azure Bot resource for your bot.

   For detailed steps, see [Use Azure CLI to create or update an Azure Bot](./provision-azure-bot.md).

> [!IMPORTANT]
> You can do these steps in either order.
> However, if you create your Azure Bot first, you'll need to update its messaging endpoint after you create your App Service resource.

## Update project configuration settings

[!INCLUDE [app ID and password](includes/authentication/azure-bot-appid-password.md)]

## Prepare your project files

[!INCLUDE [Create Kudu files, compress files](includes/az-cli/prepare-for-deployment.md)]

## Publish your bot to Azure

[!INCLUDE [deploy code to Azure](includes/az-cli/deploy-to-azure.md)]

## Test in Web Chat

[!INCLUDE [test in web chat](includes/deploy/snippet-test-in-web-chat.md)]

## Clean up resources

If you're not going to publish this application, delete the associated resources with the following steps:

1. In the Azure portal, open the resource group for your bot.
    1. Select **Delete resource group** to delete the group and all the resources it contains.
    1. Enter the _resource group name_ in the confirmation pane, then select **Delete**.
1. If you created a single-tenant or multi-tenant app:
    1. Go to the Entra ID blade.
    1. Locate the app registration you used for your bot, and delete it.

## Additional resources

[!INCLUDE [Azure documentation for bot hosting](includes/azure-docs/apps-and-resources.md)]

### Kudu files

The web app deployment command uses Kudu to deploy C#, JavaScript, and Python bots.
When using the non-configured [zip deploy API](https://github.com/projectkudu/kudu/wiki/Deploying-from-a-zip-file-or-url) to deploy your bot's code, the behavior is as follows:

_Kudu assumes by default that deployments from .zip files are ready to run and don't require extra build steps during deployment, such as npm install or dotnet restore/dotnet publish._

It's important to include your built code with all necessary dependencies in the zip file being deployed; otherwise, your bot won't work as intended. For more information, see the Azure documentation on how to [Deploy files to App Service](/azure/app-service/deploy-zip).

## Next steps

> [!div class="nextstepaction"]
> [Set up continuous deployment](bot-service-build-continuous-deployment.md)

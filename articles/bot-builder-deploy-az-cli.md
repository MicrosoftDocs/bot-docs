---
title: Deploy your bot using Azure CLI | Microsoft Docs
description: Deploy your bot to the Azure cloud.
keywords: deploy bot, azure deploy, publish bot, az deploy bot, visual studio deploy bot, msbot publish, msbot clone
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: get-started-article
ms.service: bot-service
ms.subservice: abs
ms.date: 12/14/2018
---

# Deploy your bot using Azure CLI

[!INCLUDE [pre-release-label](./includes/pre-release-label.md)]

After you have created your bot and tested it locally, you can deploy it to Azure to make it accessible from anywhere. Deploying your bot to Azure will involve paying for the services you use. The [billing and cost management](https://docs.microsoft.com/en-us/azure/billing/) article helps you understand Azure billing, monitor usage and costs, and manage your account and subscriptions.

In this article, we'll show you how to deploy C# and JavaScript bots to Azure using `az` and `msbot` cli. It would be useful to read this article before following the steps, so that you fully understand what is involved in deploying a bot.

## Prerequisites

- If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/) before you begin.
- Install the latest version of the [Azure cli tool](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest).
- Install the latest `botservice` extension for the `az` tool.
  - First, remove the old version using `az extension remove -n botservice` command. Next, use the `az extension add -n botservice` command to install the latest version.
- Install latest version of the [MSBot](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/MSBot) tool.
- Install latest released version of [Bot Framework Emulator](https://aka.ms/Emulator-wiki-getting-started).
- Install and configure [ngrok](https://github.com/Microsoft/BotFramework-Emulator/wiki/Tunneling-%28ngrok%29).
- Knowledge of [.bot](v4sdk/bot-file-basics.md) file.

## Deploy JavaScript and C# bots using az cli

You've already created and tested a bot locally, and now you want to deploy it to Azure. These steps assume that you have created the required Azure resources.

Open a command prompt to log in to the Azure portal.

```cmd
az login
```

A browser window will open, allowing you to sign in.

### Set the subscription

Set the default subscription to use.

```cmd
az account set --subscription "<azure-subscription>"
```

If you are not sure which subscription to use for deploying the bot, you can view the list of `subscriptions` for your account by using `az account list` command.

Navigate to the bot folder.

```cmd
cd <local-bot-folder>
```

### Create and download a Web App Bot

Create the bot resource into which you will publish your bot.

```cmd
az bot create --name <bot-resource-name> --kind webapp --location <location> --version v4 --lang <language> --verbose --resource-group <resource-group-name>
```

| Option | Description |
|:---|:---|
| --name | A unique name that is used to deploy the bot in Azure. It could be the same name as your local bot. DO NOT include spaces in the name. |
| --location | Geographic location used to create the bot service resources. For example, `eastus`, `westus`, `westus2`, and so on. |
| --lang | The language to use to create the bot: `Csharp`, or `Node`; default is `Csharp`. |
| --resource-group | Name of resource group in which to create the bot. You can configure the default group using `az configure --defaults group=<name>`. |

Next, download the bot you just created. This command will create a subdirectory under the save-path; however, the specified path must already exist.

```cmd
az bot download --name <bot-resource-name> --resource-group <resource-group-name> --save-path "<path>"
```

| Option | Description |
|:---|:---|
| --name | The name of the bot in Azure. |
| --resource-group | Name of resource group the bot is in. |
| --save-path | An existing directory to download bot code to. |

### Decrypt the downloaded .bot file

The sensitive information in the .bot file is encrypted.

Get the encryption key.

1. Log into the [Azure portal](http://portal.azure.com/).
1. Open the Web App Bot resource for your bot.
1. Open the bot's **Application Settings**.
1. In the **Application Settings** window, scroll down to the **Application settings**.
1. Locate the **botFileSecret** and copy its value.

Decrypt the .bot file.

```cmd
msbot secret --bot <name-of-bot-file> --secret "<bot-file-secret>" --clear
```

| Option | Description |
|:---|:---|
| --bot | The relative path to the downloaded .bot file. |
| --secret | The encryption key. |

### Use the downloaded .bot file in your project

Copy the decrypted .bot file to the directory that contains your local bot project.

Update your bot to use this new .bot file.

# [C#](#tab/csharp)

In **appsettings.json**, update the **botFilePath** property to point to the new .bot file.

# [JavaScript](#tab/javascript)

In **.env**, update the **botFilePath** property to point to the new .bot file.

---

### Update the .bot file

If your bot uses LUIS, QnA Maker, or Dispatch services, you will need to add references to them to your .bot file. Otherwise, you can skip this step.

1. Open your bot in the BotFramework Emulator, using the new .bot file. The bot does not need to be running locally.
1. In the **BOT EXPLORER** panel, expand the **SERVICES** section.
1. To add references to LUIS apps, click the plus-sign (+) to the right of **SERVICES**.
   1. Select **Add Language Understanding (LUIS)**.
   1. If it prompts you to log into your Azure account, do so.
   1. It presents a list of LUIS applications you have access to. Select the ones for your bot.
1. To add references to a QnA Maker knowledge base, click the plus-sign (+) to the right of **SERVICES**.
   1. Select **Add QnA Maker**.
   1. If it prompts you to log into your Azure account, do so.
   1. It presents a list of knowledge bases you have access to. Select the ones for your bot.
1. To add references to Dispatch models, click the plus-sign (+) to the right of **SERVICES**.
   1. Select **Add Dispatch**.
   1. If it prompts you to log into your Azure account, do so.
   1. It presents a list of Dispatch models you have access to. Select the ones for your bot.

### Test your bot locally

At this point, your bot should work the same way it did with the old .bot file. Make sure that it works as expected with the new .bot file.

### Publish your bot to Azure

<!-- TODO: re-encrypt your .bot file? -->

Publish your local bot to Azure. This step might take a while.

```cmd
az bot publish --name <bot-resource-name> --proj-file "<project-file-name>" --resource-group <resource-group-name> --code-dir <directory-path> --verbose --version v4
```

<!-- Question: What should --proj-file be for a Node project? -->

| Option | Description |
|:---|:---|
| --name | The resource name of the bot in Azure. |
| --proj-file | The startup project file name (without the .csproj) that needs to be published. For example: EnterpriseBot. |
| --resource-group | Name of resource group. |
| --code-dir | The directory to upload bot code from. |

Once this completes with a "Deployment successful!" message, your bot is deployed in Azure.

<!-- TODO: If we tell them to re-encrypt, this step is not necessary. -->

Clear the encryption key setting.

1. Log into the [Azure portal](http://portal.azure.com/).
1. Open the Web App Bot resource for your bot.
1. Open the bot's **Application Settings**.
1. In the **Application Settings** window, scroll down to the **Application settings**.
1. Locate the **botFileSecret** and delete it.

## Additional resources

When you deploy a bot, typically these resources are created in the Azure portal:

| Resources      | Description |
|----------------|-------------|
| Web App Bot | An Azure Bot Service bot that is deployed to an Azure App Service.|
| [App Service](https://docs.microsoft.com/en-us/azure/app-service/)| Enables you to build and host web applications.|
| [App Service plan](https://docs.microsoft.com/en-us/azure/app-service/azure-web-sites-web-hosting-plans-in-depth-overview)| Defines a set of compute resources for a web app to run.|
| [Application Insights](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-overview)| Provides tools for collecting and analyzing telemetry.|
| [Storage account](https://docs.microsoft.com/en-us/azure/storage/common/storage-introduction)| Provides cloud storage that is highly available, secure, durable, scalable, and redundant.|

To see documentation on `az bot` commands, see the [reference](https://docs.microsoft.com/en-us/cli/azure/bot?view=azure-cli-latest) topic.

If you are unfamiliar with Azure resource group, see this [terminology](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-overview#terminology) topic.

## Next steps
> [!div class="nextstepaction"]
> [Set up continous deployment](bot-service-build-continuous-deployment.md)

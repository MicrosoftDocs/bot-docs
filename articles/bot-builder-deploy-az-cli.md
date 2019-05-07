---
title: Deploy your bot | Microsoft Docs
description: Deploy your bot to the Azure cloud.
keywords: deploy bot, azure deploy bot, publish bot
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: get-started-article
ms.service: bot-service
ms.subservice: abs
ms.date: 04/12/2019
---

# Deploy your bot

[!INCLUDE [pre-release-label](./includes/pre-release-label.md)]

After you have created your bot and tested it locally, you can deploy it to Azure to make it accessible from anywhere. Deploying your bot to Azure will involve paying for the services you use. The [billing and cost management](https://docs.microsoft.com/azure/billing/) article helps you understand Azure billing, monitor usage and costs, and manage your account and subscriptions.

In this article, we'll show you how to deploy C# and JavaScript bots to Azure. It would be useful to read this article before following the steps, so that you fully understand what is involved in deploying a bot.

## Prerequisites
- If you don't have an [Azure subscription](http://portal.azure.com), create a free account before you begin.
- A [**CSharp**](./dotnet/bot-builder-dotnet-sdk-quickstart.md) or [**JavaScript**](./javascript/bot-builder-javascript-quickstart.md) bot that you have developed on your local machine.

## 1. Prepare for deployment
The deployment process requires a target Web App Bot in Azure so that your local bot can be deployed into it. The target Web App Bot and the resources that are provisioned with it in Azure are used by your local bot for deployment. This is necessary because your local bot does not have all the required Azure resources provisioned. When you create a target Web App bot, the following resources are provisioned for you:
-	Web App Bot – you will use this bot to deploy your local bot into.
-	App Service Plan - provides the resources that an App Service app needs to run.
-	App Service - service for hosting web applications
-	Storage account - contains all your Azure Storage data objects: blobs, files, queues, tables, and disks.

During the creation of the target Web App Bot, an app ID and password are also generated for your bot. In Azure, the app ID and password support [service authentication and authorization](https://docs.microsoft.com/azure/app-service/overview-authentication-authorization). You will retrieve some of this information for use in your local bot code.

> [!IMPORTANT]
> The programming language for the bot template used in the Azure portal must match the programming language your bot is written in.

Creating a new Web App Bot is optional if you have already created a bot in Azure that you'd like to use.

1. Log in to the [Azure portal](https://portal.azure.com).
1. Click **Create new resource** link found on the upper left-hand corner of the Azure portal, then select **AI + Machine Learning > Web App bot**.
1. A new blade will open with information about the Web App Bot.
1. In the **Bot Service** blade, provide the requested information about your bot.
1. Click **Create** to create the service and deploy the bot to the cloud. This process may take several minutes.

### Download the source code
After creating the target Web App Bot, you need to download the bot code from the Azure portal to your local machine. The reason for downloading code is to get the service references (e.g. MicrosoftAppID, MicrosoftAppPassword, LUIS, or QnA) that are in the appsettings.json or .env file.

1. In the **Bot Management** section, click **Build**.
1. Click on **Download Bot source code** link in the right-pane.
1. Follow the prompts to download the code, and then unzip the folder.
	1. [!INCLUDE [download keys snippet](~/includes/snippet-abs-key-download.md)]

### Update your local appsettings.json or .env file

Open the appsettings.json or .env file you downloaded. Copy **all** entries listed in it and add them to your _local_ appsettings.json or .env file. Resolve any duplicate service entries or duplicate service IDs. Keep any additional service references your bot depends on.

Save the file.

### Update local bot code
Update the local Startup.cs or index.js file to use appsettings.json or .env file instead using the .bot file. The .bot file has been deprecated and we are working on updating VSIX templates, Yeoman generators, samples, and remaining docs to all use appsettings.json or .env file instead of the .bot file. In the meantime, you'll need to make changes to the bot code.

Update the code to read settings from appsettings.json or .env file.

# [C#](#tab/csharp)
In the `ConfigureServices` method, use the configuration object that ASP.NET Core provides, for example:

**Startup.cs**
```csharp
var appId = Configuration.GetSection("MicrosoftAppId").Value;
var appPassword = Configuration.GetSection("MicrosoftAppPassword").Value;
options.CredentialProvider = new SimpleCredentialProvider(appId, appPassword);
```

# [JS](#tab/js)

In JavaScript, reference .env variables off of the `process.env` object, for example:

**index.js**

```js
const adapter = new BotFrameworkAdapter({
    appId: process.env.MicrosoftAppId,
    appPassword: process.env.MicrosoftAppPassword
});
```
---

- Save the file and test your bot.

### Setup a repository

To support continuous deployment, create a git repository using your favorite git source control provider. Commit your code into the repository.

Make sure that your repository root has the correct files, as described under [prepare your repository](https://docs.microsoft.com/azure/app-service/deploy-continuous-deployment#prepare-your-repository).

### Update App Settings in Azure
The local bot does not use an encrypted .bot file, but _if_ the Azure portal is configured to use an encrypted .bot file. You can resolve this by removing the **botFileSecret** stored in the Azure bot settings.
1. In the Azure portal, open the **Web App Bot** resource for your bot.
1. Open the bot's **Application Settings**.
1. In the **Application Settings** window, scroll down to **Application settings**.
1. Check to see if your bot has **botFileSecret** and **botFilePath** entries. If you do, delete it.
1. Save the changes.

## 2. Deploy using Azure Deployment Center

Now, you need to upload your bot code to Azure. Follow instructions in the [Continuous deployment to Azure App Service](https://docs.microsoft.com/azure/app-service/deploy-continuous-deployment) topic.

Note that it is recommended to build using `App Service Kudu build server`.

Once you've configured continuous deployment, changes you commit to your repo are published. However, if you add services to your bot, you will need to add entries for these to your .bot file.

## 3. Test your deployment

Wait for a few seconds after a successful deployment and optionally restart your Web App to clear any cache. Go back to your Web App Bot blade and test using the Web Chat provided in the Azure portal.

## Additional resources
- [How to investigate common issues with continuous deployment](https://github.com/projectkudu/kudu/wiki/Investigating-continuous-deployment)


---
title: Deploy bots from botbuilder-samples repo | Microsoft Docs
description: Deploy your bot to the Azure cloud.
keywords: deploy bot, azure deploy, publish bot, az deploy bot, visual studio deploy bot, msbot publish, msbot clone
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: get-started-article
ms.service: bot-service
ms.subservice: abs
ms.date: 01/15/2019
monikerRange: 'azure-bot-service-4.0'
---

# Deploy bots from botbuilder-samples repo

[!INCLUDE [pre-release-label](./includes/pre-release-label.md)]

In this article, we'll show you how to deploy C# and JavaScript samples that are in the [botbuilder-samples](https://github.com/Microsoft/BotBuilder-Samples)
repo to Azure.

Instructions to deploy sample bots is _different_ from the instructions to [deploy a bot that you might create with all the resources already provisioned in Azure](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-deploy-az-cli?view=azure-bot-service-4.0&tabs=csharp).

> [!IMPORTANT]
> Deploying a bot from the [botbuilder-samples](https://github.com/Microsoft/BotBuilder-Samples) repo to Azure will provision Azure services and will involve paying for the services you use.
> The [billing and cost management](https://docs.microsoft.com/en-us/azure/billing/) article helps you understand Azure billing, monitor usage and costs, and manage your account and subscriptions.

It would be useful to read this article once through before following the steps, so that you fully understand what is involved in deploying a bot.

## Prerequisites

- If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/) before you begin.
- Install [.NET Core SDK](https://dotnet.microsoft.com/download) >= v2.2. Use `dotnet --version` to see what version you have.
- Install the latest version of the [Azure cli tool](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest). Use `az --version` to see what version you have.
- Install latest version of the [MSBot](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/MSBot) tool.
  - You'll need [LUIS CLI](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/LUIS#installation) if the the clone operation includes LUIS or Dispatch resources.
  - You'll need [QnA Maker CLI](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/QnAMaker#as-a-cli) if the clone operation includes QnA Maker resources.
- Install [Bot Framework Emulator](https://aka.ms/Emulator-wiki-getting-started).
- Install and configure [ngrok](https://github.com/Microsoft/BotFramework-Emulator/wiki/Tunneling-%28ngrok%29).
- Knowledge of [.bot](v4sdk/bot-file-basics.md) files.

With msbot 4.3.2 and later, you need Azure CLI version 2.0.54 or later. If you installed the botservice extension, remove it with this command.

```cmd
az extension remove --name botservice
```

### C\#

 `msbot clone services` does not upload your code files to Azure, just the DLL and a few other files. You need to compile the sample before running this command.

### Service names

In addition to deploying your bot, the `msbot clone services` command provisions all associated services to your chosen subscription.

If any those name-service combinations already exist in the subscription, the command will generate an error, and you will need to delete your partial deployment before starting again. This includes LUIS applications, QnA Maker knowledge bases, and Dispatch models.

## Deploy JavaScript and C# bots using az cli

Open a command prompt to log in to the Azure portal.

```cmd
az login
```

A browser window will open, allowing you to sign in.

### Set the subscription

Set the subscription by using the following command:

```cmd
az account set --subscription "<azure-subscription>"
```

If you are not sure which subscription to use for deploying the bot, you can view the list of `subscriptions` for your account by using `az account list` command.

Navigate to the bot folder.

```cmd
cd <local-bot-folder>
```

### Clone the sample

**Azure subscription account**: Before proceeding, read the instructions that apply to you based on the type of email account you use to log in to Azure.

**Creating services**: The `msbot clone services` command will create Azure services for the bot.

1. It lists the services it will create and prompts you to confirm the operation before continuing. If you decline, the command exits without creating any of the services.
1. It has you authenticate with Azure before proceeding.

**LUIS services**: If your bot uses LUIS or Dispatch, you will need to include your LUIS authoring key in the `msbot clone services` command.

#### MSA email account

If you are using a [MSA](https://en.wikipedia.org/wiki/Microsoft_account) email account, you will need to create the appId and appSecret to use with `msbot clone services` command.

- Go to the [Application Registration Portal](https://apps.dev.microsoft.com/). Click on **Add an app** to register your application, create **Application Id**, and **Generate New Password**.
- Save both application id and the new password you just generated, so you that can use them with `msbot clone services` command.
- To deploy, use the command that applies to your bot.

# [C#](#tab/csharp)

`msbot clone services --folder deploymentScripts/msbotClone --location "<geographic-location>" --proj-file "<your.csproj>" --name "<bot-name>" --appId "xxxxxxxx" --appSecret "xxxxxxx" --verbose --luisAuthoringKey <luis-authoring-key>`

[!INCLUDE [deployment note](./includes/deployment-note-cli.md)]

# [JavaScript](#tab/js)

`msbot clone services --folder deploymentScripts/msbotClone --location "<geographic-location>"   --code-dir . --name "<bot-name>" --appId "xxxxxxxx" --appSecret "xxxxxxx" --verbose --luisAuthoringKey <luis-authoring-key>`

[!INCLUDE [deployment note](./includes/deployment-note-cli.md)]

---

#### Business or school account

If your are using an email account provided to you by your business or school to log in to Azure, you don't need to create the application id and password. To deploy, use the command that applies to your bot.

# [C#](#tab/csharp)

`msbot clone services --folder deploymentScripts/msbotClone --location "<geographic-location>" --verbose --proj-file "<your-project-file>" --name "<bot-name>" --luisAuthoringKey <luis-authoring-key>`

[!INCLUDE [deployment note](./includes/deployment-note-cli.md)]

# [JavaScript](#tab/js)

`msbot clone services --folder deploymentScripts/msbotClone --location "<geographic-location>" --verbose --code-dir . --name "<bot-name>" --luisAuthoringKey <luis-authoring-key>`

[!INCLUDE [deployment note](./includes/deployment-note-cli.md)]

---

#### Save the secret used to decrypt .bot file

It is important to note that the deployment process creates a _new .bot file and encrypts it using a secret_. While the bot is being deployed, you'll see the following message in the command-line asking you to save the .bot file secret.

`The secret used to decrypt myAzBot.bot is:`
`hT6U1SIPQeXlebNgmhHYxcdseXWBZlmIc815PpK0WWA=`

`NOTE: This secret is not recoverable and you should save it in a safe place according to best security practices.
Copy this secret and use it to open the <file.bot> the first time.`

Save the .bot file secret for later use. The new encrypted .bot file is used in the Azure portal with the botFileSecret. If you need to change the bot file name or secret later on, go to **App Service Settings -> Application Settings** section in the portal. Note that in the appsettings.json or .env file, the bot file name is updated with the latest bot file that was created.  

### Test your bot

In the emulator, use production endpoint to test your app. If you want to test it locally, make sure your bot is running on your local machine.

### To update your bot code in Azure

DO NOT use `msbot clone services` command to update the bot code in Azure. You must use the `az bot publish` command as shown below:

```cmd
az bot publish --name "<your-azure-bot-name>" --proj-name "<your-proj-name>" --resource-group "<azure-resource-group>" --code-dir "<folder>" --verbose --version v4
```

| Arguments        | Description |
|----------------  |-------------|
| `name`      | The name you used when the bot was first deployed to Azure.|
| `proj-name` | For C#, use the startup project file name (without the .csproj) that needs to be published. For example: `EnterpriseBot`. For Node.js, use the main entry point for the bot. For example, `index.js`. |
| `resource-group` | The Azure resource group that `msbot clone services` command used.|
| `code-dir`  | Points to the local bot folder.|

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

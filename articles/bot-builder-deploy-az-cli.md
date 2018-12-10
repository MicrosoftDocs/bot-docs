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
ms.date: 12/08/2018
---

# Deploy your bot using Azure CLI

[!INCLUDE [pre-release-label](./includes/pre-release-label.md)]

After you have created your bot and tested it locally, you can deploy it to Azure to make it accessible from anywhere. Deploying your bot to Azure will involve paying for the services you use. The [billing and cost management](https://docs.microsoft.com/en-us/azure/billing/) article helps you understand Azure billing, monitor usage and costs, and manage your account and subscriptions.

In this article, we'll show you how to deploy C# and JavaScript bots to Azure using `msbot` tool. It would be useful to read this article before following the steps, so that you fully understand what is involved in deploying a bot.


## Prerequisites
- If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/) before you begin.
- Install [.NET Core SDK](https://dotnet.microsoft.com/download) >=v2.2. 
- Install the latest version of the [Azure cli tool](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest).
- Install the latest `botservice` extension for the `az` tool. 
  - First, remove the old version using `az extension remove -n botservice` command. Next, use the `az extension add -n botservice` command to install the latest version.
- Install latest version of the [MSBot](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/MSBot) tool.
- Install [Bot Framework Emulator](https://aka.ms/Emulator-wiki-getting-started).
- Install and configure [ngrok](https://github.com/Microsoft/BotFramework-Emulator/wiki/Tunneling-%28ngrok%29).
- Knowledge of [.bot](v4sdk/bot-file-basics.md) file.

## Deploy JavaScript and C# bots using az cli
You've already created a bot, and now you want to deploy it to Azure. These steps assume that you have created the required Azure resources, and updated the service references in the .bot file by either using the `msbot connect` command or the Bot Framework Emulator. If the .bot file is not up to date, the deployment process might complete without errors or warnings, but the deployed bot will not work.

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

### Azure subscription account
Before proceeding, read the instructions that apply to you based on the type of email account you use to log in to Azure.

**MSA email account**

If you are using a [MSA](https://en.wikipedia.org/wiki/Microsoft_account) email account, you will need to create the appId and appSecret to use with `msbot clone services` command. 

- Go to the [Application Registration Portal](https://apps.dev.microsoft.com/). Click on **Add an app** to register your application, create **Application Id**, and **Generate New Password**. 
- Save both application id and the new password you just generated, so you that can use them with `msbot clone services` command. 
- To deploy, use the command that applies to your bot.

# [C#](#tab/csharp)


`msbot clone services --folder deploymentScripts/msbotClone --location "<geographic-location>" --proj-file "<your.csproj>" --name "<bot-name>" --appid "xxxxxxxx" --password "xxxxxxx" --verbose`

[!INCLUDE [deployment note](./includes/deployment-note-cli.md)]

# [JavaScript](#tab/js)

`msbot clone services --folder deploymentScripts/msbotClone --location "<geographic-location>"   --code-dir . --name "<bot-name>" --appid "xxxxxxxx" --password "xxxxxxx" --verbose`


[!INCLUDE [deployment note](./includes/deployment-note-cli.md)]

---

**Business or school account**

If your are using an email account provided to you by your business or school to log in to Azure, you don't need to create the application id and password. To deploy, use the command that applies to your bot.

# [C#](#tab/csharp)

`msbot clone services --folder deploymentScripts/msbotClone --location "<geographic-location>" --verbose --proj-file "<your-project-file>" --name "<bot-name>"`

[!INCLUDE [deployment note](./includes/deployment-note-cli.md)]

# [JavaScript](#tab/js)

`msbot clone services --folder deploymentScripts/msbotClone --location "<geographic-location>" --verbose --code-dir . --name "<bot-name>"`


[!INCLUDE [deployment note](./includes/deployment-note-cli.md)]

---

#### Save the secret used to decrypt .bot file
It is important to note that the deployment process creates a _new .bot file and encrypts it using a secret_. While the bot is being deployed, you'll see the following message in the command-line asking you to save the .bot file secret. 

`The secret used to decrypt myAzBot.bot is:`
`hT6U1SIPQeXlebNgmhHYxcdseXWBZlmIc815PpK0WWA=`

`NOTE: This secret is not recoverable and you should save it in a safe place according to best security practices.
      Copy this secret and use it to open the <file.bot> the first time.`
      
Save the .bot file secret for later use.

### Test your bot
In the emulator, use production endpoint to test your app. If you want to test it locally, make sure your bot is running on your local machine. 

### To update your bot code in Azure
DO NOT use `msbot clone services` command to update your bot code in Azure. You must use the `az bot publish` command as shown below:

```cmd
az bot publish --name "<your-azure-bot-name>" --proj-file "<your-proj-file>" --resource-group "<azure-resource-group>" --code-dir "<folder>" --verbose --version v4
```

| Arguments        | Description |
|----------------  |-------------|
| `name`      | The name you used when the bot was first deployed to Azure.|
| `proj-file` | For C# bot, it is the .csproj file. For JS/TS bot, it is the startup project file name (e.g. index.js or index.ts) of your local bot.|
| `resource-group` | The Azure resource group that `msbot clone services` command used.|
| `code-dir`  | Points to the local bot folder.|



## Additional resources

When you deploy a bot, typically these reources are created in the Azure portal:

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

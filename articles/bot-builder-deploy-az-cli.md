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
ms.date: 05/23/2019
monikerRange: 'azure-bot-service-4.0'
---

# Deploy your bot

[!INCLUDE [applies-to](./includes/applies-to.md)]

In this article, we'll show you how to deploy your bot to Azure. It would be useful to read this article before following the steps, so that you fully understand what is involved in deploying a bot.

## Prerequisites
- If you don't have an Azure subscription, create an [account](https://azure.microsoft.com/free/) before you begin.
- A CSharp, JavaScript, or TypeScript bot that you have developed on your local machine.
- Latest version of the [Azure cli](https://docs.microsoft.com/en-us/cli/azure/?view=azure-cli-latest).
- Familiarity with [Azure cli and ARM templates](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-overview).

## 1. Prepare for deployment
When you create a bot using Visual Studio or Yeoman templates, the source code generated contains a `deploymentTemplates` folder with ARM templates. The deployment process documented here uses the ARM template to provision required resources for the bot in Azure by using the Azure CLI. 

> [!IMPORTANT]
> With the release of Bot Framework SDK 4.3, we have _deprecated_ the use of .bot file in favor of appsettings.json or .env file for managing resources. For information on migrating settings from the .bot file to appsettings.json or .env file, see [managing bot resources](v4sdk/bot-file-basics.md).

### Login to Azure

You've already created and tested a bot locally, and now you want to deploy it to Azure. Open a command prompt to log in to the Azure portal.

```cmd
az login
```
A browser window will open, allowing you to sign in.

### Set the subscription
Set the default subscription to use.

```cmd
az account set --subscription "<azure-subscription>"
```

If you are not sure which subscription to use for deploying the bot, you can view the list of subscriptions for your account by using `az account list` command. Navigate to the bot folder.

### Create an App registration
Registering the application means that you can use Azure AD to authenticate users and request access to user resources. Your bot requires a Registered app in Azure that provides the bot access to the Bot Framework Service for sending and receiving authenticated messages. To create register an app via the Azure CLI, perform the following command:

```cmd
az ad app create --display-name "displayName" --password "AtLeastSixteenCharacters_0" --available-to-other-tenants
```

| Option   | Description |
|:---------|:------------|
| display-name | The display name of the application. |
| password | App password, aka 'client secret'. The password must be at least 16 characters long, contain at least 1 upper or lower case alphabetical character, and contain at least 1 special character|
| available-to-other-tenants| The application can be used from any Azure AD tenants. This must be `true` to enable your bot to work with the Azure Bot Service channels.|

The above command outputs JSON with the key `appId`, save the value of this key for the ARM deployment, where it will be used for the `appId` parameter. The password provided will be used for the `appSecret` parameter.

You can deploy your bot in a new resource group or an exising resource group. Choose the option that works best for you.

# [Deploy via ARM template (with **new** Resource Group)](#tab/newrg)

### Create Azure resources

You'll create a new resource group in Azure and then use the ARM template to create the resources specified in it. In this case, we are provding App Service Plan, Web App, and Bot Channels Registration.

```cmd
az deployment create --name "<name-of-deployment>" --template-file "template-with-new-rg.json" --location "location-name" --parameters appId="<msa-app-guid>" appSecret="<msa-app-password>" botId="<id-or-name-of-bot>" botSku=F0 newAppServicePlanName="<name-of-app-service-plan>" newWebAppName="<name-of-web-app>" groupName="<new-group-name>" groupLocation="<location>" newAppServicePlanLocation="<location>"
```

| Option   | Description |
|:---------|:------------|
| name | Friendly name for the deployment. |
| template-file | The path to the ARM template. You can use `template-with-new-rg.json` file provided in the `deploymentTemplates` folder of the project. |
| location |Location. Values from: `az account list-locations`. You can configure the default location using `az configure --defaults location=<location>`. |
| parameters | Provide deployment parameter values. `appId` value you got from running the `az ad app create` command. `appSecret` is the password you provided in the previous step. The `botId` parameter should be globally unique and is used as the immutable bot ID. It is also used to configure the display name of the bot, which is mutable. `botSku` is the pricing tier and can be F0 (Free) or S1 (Standard). `newAppServicePlanName` is the name of App Service Plan. `newWebAppName` is the name of the Web App you are creating. `groupName` is the name of the Azure resource group you are creating. `groupLocation` is the location of the Azure resource group. `newAppServicePlanLocation` is the location of the App Service Plan. |

# [Deploy via ARM template (with **existing**  Resource Group)](#tab/erg)

### Create Azure resources

When using an existing resource group, you can either use an existing App Service Plan or create a new one. Steps for both options are listed below. 

**Option 1: Existing App Service Plan** 

In this case, we are using existing App Service Plan, but creating new a Web App and Bot Channels Registration. 

_Note: The botId parameter should be globally unique and is used as the immutable bot ID. Also used to configure the displayName of the bot, which is mutable._

```cmd
az group deployment create --name "<name-of-deployment>" --resource-group "<name-of-resource-group>" --template-file "template-with-preexisting-rg.json" --parameters appId="<msa-app-guid>" appSecret="<msa-app-password>" botId="<id-or-name-of-bot>" newWebAppName="<name-of-web-app>" existingAppServicePlan="<name-of-app-service-plan>" appServicePlanLocation="<location>"
```

**Option 2: New App Service Plan** 

In this case, we are creating App Service Plan, Web App, and Bot Channels Registration. 

```cmd
az group deployment create --name "<name-of-deployment>" --resource-group "<name-of-resource-group>" --template-file "template-with-preexisting-rg.json" --parameters appId="<msa-app-guid>" appSecret="<msa-app-password>" botId="<id-or-name-of-bot>" newWebAppName="<name-of-web-app>" newAppServicePlanName="<name-of-app-service-plan>" appServicePlanLocation="<location>"
```

| Option   | Description |
|:---------|:------------|
| name | Friendly name for the deployment. |
| resource-group | Name of the azure resource group |
| template-file | The path to the ARM template. You can use `template-with-preexisting-rg.json` file provided in the `deploymentTemplates` folder of the project. |
| location |Location. Values from: `az account list-locations`. You can configure the default location using `az configure --defaults location=<location>`. |
| parameters | Provide deployment parameter values. `appId` value you got from running the `az ad app create` command. `appSecret` is the password you provided in the previous step. The `botId` parameter should be globally unique and is used as the immutable bot ID. It is also used to configure the display name of the bot, which is mutable. `newWebAppName` is the name of the Web App you are creating. `newAppServicePlanName` is the name of App Service Plan. `newAppServicePlanLocation` is the location of the App Service Plan. |

---

### Retrieve or create necessary IIS/Kudu files

**For C# bots**

```cmd
az bot prepare-deploy --lang Csharp --code-dir "." --proj-file-path "MyBot.csproj"
```

You must provide the path to the .csproj file relative to --code-dir. This can be performed via the --proj-file-path argument. The command would resolve --code-dir and --proj-file-path to "./MyBot.csproj"

**For JavaScript bots**

```cmd
az bot prepare-deploy --code-dir "." --lang Javascript
```

This command will fetch a web.config which is needed for Node.js apps to work with IIS on Azure App Services. Make sure web.config is saved to the root of your bot.

**For TypeScript bots**

```cmd
az bot prepare-deploy --code-dir "." --lang Typescript
```

This command works similarly to JavaScript above, but for a Typescript bot.

### Zip up the code directory manually

When using the non-configured [zip deploy API](https://github.com/projectkudu/kudu/wiki/Deploying-from-a-zip-file-or-url) to deploy your bot's code, Web App/Kudu's behavior is as follows:

_Kudu assumes by default that deployments from zip files are ready to run and do not require additional build steps during deployment, such as npm install or dotnet restore/dotnet publish._

As such, it is important to include your built code and with all necessary dependencies in the zip file being deployed to the Web App, otherwise your bot will not work as intended.

> [!IMPORTANT]
> Before zipping your project files, make sure that you are _in_ the correct folder. 
> - For C# bots, it is the folder that has the .csproj file. 
> - For JS bots, it is the folder that has the app.js or index.js file. 
>
> Select all the files and zip them up **while in that folder**, then run the command while still in that folder.
>
> If your root folder location is incorrect, the **bot will fail to run in the Azure portal**.

## 2. Deploy code to Azure
At this point we are ready to deploy the code to the Azure Web App. Run the following command from the command line to perform deployment using the kudu zip push deployment for a web app.

```cmd
az webapp deployment source config-zip --resource-group "<new-group-name>" --name "<name-of-web-app>" --src "code.zip" 
```

| Option   | Description |
|:---------|:------------|
| resource-group | Resource group name in Azure that you created earlier. |
| name | Name of the Web App you used earlier. |
| src  | The path to the zipped file you created. |

## 3. Test in Web Chat
- In the Azure portal, go to your Web App bot blade.
- In the **Bot Management** section, click **Test in Web Chat**. Azure Bot Service will load the Web Chat control and connect to your bot.
- Wait for a few seconds after a successful deployment and optionally restart your Web App to clear any cache. Go back to your Web App Bot blade and test using the Web Chat provided in the Azure portal.

## Additional information
Deploying your bot to Azure will involve paying for the services you use. The [billing and cost management](https://docs.microsoft.com/en-us/azure/billing/) article helps you understand Azure billing, monitor usage and costs, and manage your account and subscriptions.

## Next steps
> [!div class="nextstepaction"]
> [Set up continous deployment](bot-service-build-continuous-deployment.md)

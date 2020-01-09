---
title: Deploy your bot - Bot Service
description: Deploy your bot to the Azure cloud
keywords: deploy bot, azure deploy bot, publish bot
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 08/06/2019
monikerRange: 'azure-bot-service-4.0'
---

# Deploy your bot

[!INCLUDE [applies-to](./includes/applies-to.md)]

In this article we will show you how to deploy a basic bot to Azure. We will explain how to prepare your bot for deployment, deploy your bot to Azure, and test your bot in Web Chat. It would be useful to read this article before following the steps, so that you fully understand what is involved in deploying a bot.

## Prerequisites
[!INCLUDE [deploy prerequisite](~/includes/deploy/snippet-prerequisite.md)]
<!-- - A subscription to [Microsoft Azure](https://azure.microsoft.com/free/)
- A C#, JavaScript, or TypeScript bot that you have developed on your local machine
- Latest version of the [Azure CLI](https://docs.microsoft.com/cli/azure/?view=azure-cli-latest)
- Familiarity with [Azure CLI and ARM templates](https://docs.microsoft.com/azure/azure-resource-manager/resource-group-overview) -->

## Prepare for deployment
[!INCLUDE [deploy prepare intro](~/includes/deploy/snippet-prepare-deploy-intro.md)]
<!-- When you create a bot using [Visual Studio template](https://docs.microsoft.com/azure/bot-service/dotnet/bot-builder-dotnet-sdk-quickstart?view=azure-bot-service-4.0) or [Yeoman template](https://docs.microsoft.com/azure/bot-service/javascript/bot-builder-javascript-quickstart?view=azure-bot-service-4.0), the source code generated contains a `deploymentTemplates` folder with ARM templates. The deployment process documented here uses the ARM template to provision required resources for the bot in Azure by using the Azure CLI. 

> [!NOTE]
> With the release of Bot Framework SDK 4.3, we have _deprecated_ the use of .bot file in favor of appsettings.json or .env file for managing resources. For information on migrating settings from the .bot file to appsettings.json or .env file, see [managing bot resources](v4sdk/bot-file-basics.md). -->

### 1. Login to Azure
[!INCLUDE [deploy az login](~/includes/deploy/snippet-az-login.md)]
<!-- You've already created and tested a bot locally, and now you want to deploy it to Azure. Open a command prompt to log in to the Azure portal.

```cmd
az login
```
A browser window will open, allowing you to sign in.

> [!NOTE]
> If you deploy your bot to a non-Azure cloud such as US Gov, you need to run `az cloud set --name <name-of-cloud>` before `az login`, where &lt;name-of-cloud> is the name of a registered cloud, such as `AzureUSGovernment`. If you want to go back to public cloud, you can run `az cloud set --name AzureCloud`.  -->


### 2. Set the subscription
[!INCLUDE [deploy az subscription](~/includes/deploy/snippet-az-set-subscription.md)]
<!-- Set the default subscription to use.

```cmd
az account set --subscription "<azure-subscription>"
```

If you are not sure which subscription to use for deploying the bot, you can view the list of subscriptions for your account by using `az account list` command. Navigate to the bot folder. -->

### 3. Create an App registration
[!INCLUDE [deploy create app registration](~/includes/deploy/snippet-create-app-registration.md)]
<!-- Registering the application means that you can use Azure AD to authenticate users and request access to user resources. Your bot requires a Registered app in Azure that provides the bot access to the Bot Framework Service for sending and receiving authenticated messages. To create register an app via the Azure CLI, perform the following command:

```cmd
az ad app create --display-name "displayName" --password "AtLeastSixteenCharacters_0" --available-to-other-tenants
```

| Option   | Description |
|:---------|:------------|
| display-name | The display name of the application. |
| password | App password, aka 'client secret'. The password must be at least 16 characters long, contain at least 1 upper or lower case alphabetical character, and contain at least 1 special character.|
| available-to-other-tenants| The application can be used from any Azure AD tenants. This must be `true` to enable your bot to work with the Azure Bot Service channels.|

The above command outputs JSON with the key `appId`, save the value of this key for the ARM deployment, where it will be used for the `appId` parameter. The password provided will be used for the `appSecret` parameter.

> [!NOTE] 
> If you would like to use an existing App registration you can use the command:
> ``` cmd
> az bot create --kind webapp --resource-group "<name-of-resource-group>" --name "<name-of-web-app>" --appid "<existing-app-id>" --password "<existing-app-password>" --lang <Javascript|Csharp>
> ``` -->

### 4. Deploy via ARM template
You can deploy your bot in a new resource group or an existing resource group. Choose the option that works best for you.

> [!NOTE]
> Python bots cannot be deployed to a resource group that contains Windows services/bots.  Multiple Python bots can be deployed to the same resource group, but create other services (LUIS, QnA, etc.) in another resource group.
>

#### **Deploy via ARM template (with **new** Resource Group)**
<!-- ##### Create Azure resources -->
[!INCLUDE [ARM with new resourece group](~/includes/deploy/snippet-ARM-new-resource-group.md)]
<!-- You'll create a new resource group in Azure and then use the ARM template to create the resources specified in it. In this case, we are providing App Service Plan, Web App, and Bot Channels Registration.

```cmd
az deployment create --name "<name-of-deployment>" --template-file "template-with-new-rg.json" --location "location-name" --parameters appId="<msa-app-guid>" appSecret="<msa-app-password>" botId="<id-or-name-of-bot>" botSku=F0 newAppServicePlanName="<name-of-app-service-plan>" newWebAppName="<name-of-web-app>" groupName="<new-group-name>" groupLocation="<location>" newAppServicePlanLocation="<location>"
```

| Option   | Description |
|:---------|:------------|
| name | Friendly name for the deployment. |
| template-file | The path to the ARM template. You can use `template-with-new-rg.json` file provided in the `deploymentTemplates` folder of the project. |
| location |Location. Values from: `az account list-locations`. You can configure the default location using `az configure --defaults location=<location>`. |
| parameters | Provide deployment parameter values. `appId` value you got from running the `az ad app create` command. `appSecret` is the password you provided in the previous step. The `botId` parameter should be globally unique and is used as the immutable bot ID. It is also used to configure the display name of the bot, which is mutable. `botSku` is the pricing tier and can be F0 (Free) or S1 (Standard). `newAppServicePlanName` is the name of App Service Plan. `newWebAppName` is the name of the Web App you are creating. `groupName` is the name of the Azure resource group you are creating. `groupLocation` is the location of the Azure resource group. `newAppServicePlanLocation` is the location of the App Service Plan. | -->

#### **Deploy via ARM template (with **existing**  Resource Group)**

[!INCLUDE [ARM with existing resourece group](~/includes/deploy/snippet-ARM-existing-resource-group.md)]
<!-- When using an existing resource group, you can either use an existing App Service Plan or create a new one. Steps for both options are listed below. 

**Option 1: Existing App Service Plan** 

In this case, we are using existing App Service Plan, but creating new a Web App and Bot Channels Registration. 

> [!NOTE]
> The botId parameter should be globally unique and is used as the immutable bot ID. Also used to configure the displayName of the bot, which is mutable.

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
| parameters | Provide deployment parameter values. `appId` value you got from running the `az ad app create` command. `appSecret` is the password you provided in the previous step. The `botId` parameter should be globally unique and is used as the immutable bot ID. It is also used to configure the display name of the bot, which is mutable. `newWebAppName` is the name of the Web App you are creating. `newAppServicePlanName` is the name of App Service Plan. `newAppServicePlanLocation` is the location of the App Service Plan. | -->

---

### 5. Prepare your code for deployment
#### 5.1 Retrieve or create necessary IIS/Kudu files
[!INCLUDE [retrieve or create IIS/Kudu files](~/includes/deploy/snippet-IIS-Kudu-files.md)]

<!-- ##### [C#](#tab/csharp) -->

<!-- ```cmd
az bot prepare-deploy --lang Csharp --code-dir "." --proj-file-path "MyBot.csproj"
```

You must provide the path to the .csproj file relative to --code-dir. This can be performed via the --proj-file-path argument. The command would resolve --code-dir and --proj-file-path to "./MyBot.csproj"

##### [JavaScript](#tab/javascript)

```cmd
az bot prepare-deploy --code-dir "." --lang Javascript
```

This command will fetch a web.config which is needed for Node.js apps to work with IIS on Azure App Services. Make sure web.config is saved to the root of your bot.

##### [TypeScript](#tab/typescript)

```cmd
az bot prepare-deploy --code-dir "." --lang Typescript
```

This command works similarly to JavaScript above, but for a Typescript bot.

---

> [!NOTE]
> After executing the above-mentioned command, you should be able to see a `.deployment` file in your bot project folder. -->

#### 5.2 Zip up the code directory manually
[!INCLUDE [zip up code](~/includes/deploy/snippet-zip-code.md)]
<!-- When using the non-configured [zip deploy API](https://github.com/projectkudu/kudu/wiki/Deploying-from-a-zip-file-or-url) to deploy your bot's code, Web App/Kudu's behavior is as follows:

_Kudu assumes by default that deployments from zip files are ready to run and do not require additional build steps during deployment, such as npm install or dotnet restore/dotnet publish._

As such, it is important to include your built code and with all necessary dependencies in the zip file being deployed to the Web App, otherwise your bot will not work as intended.

> [!IMPORTANT]
> Before zipping your project files, make sure that you are _in_ the correct folder. 
> - For C# bots, it is the folder that has the .csproj file. 
> - For JS bots, it is the folder that has the app.js or index.js file. 
>
>**Within** the project folder, select all the files and zip them up, then run the command while still in the folder. 
>
> If your root folder location is incorrect, the **bot will fail to run in the Azure portal**. -->

## Deploy code to Azure
[!INCLUDE [deploy code to Azure](~/includes/deploy/snippet-deploy-code-to-az.md)]
<!-- At this point we are ready to deploy the code to the Azure Web App. Run the following command from the command line to perform deployment using the kudu zip push deployment for a web app. -->
<!-- 
```cmd
az webapp deployment source config-zip --resource-group "<new-group-name>" --name "<name-of-web-app>" --src "code.zip" 
```

| Option   | Description |
|:---------|:------------|
| resource-group | Resource group name in Azure that you created earlier. |
| name | Name of the Web App you used earlier. |
| src  | The path to the zipped file you created. | -->

## Test in Web Chat
[!INCLUDE [test in web chat](~/includes/deploy/snippet-test-in-web-chat.md)]
<!-- 1. In your browser, navigate to the [Azure portal](https://ms.portal.azure.com).
2. In the left panel, click **Resource groups**.
3. In the right panel, search for your group.
4. Click on your group name.
5. Click the link of your Bot Channel Registration.
6. In the *Bot Channel Registration blade*, click **Test in Web Chat**.
Alternatively, in the right panel, click the Test box.

For more information about channel registration, see [Register a bot with Bot Service](https://docs.microsoft.com/azure/bot-service/bot-service-quickstart-registration?view=azure-bot-service-3.0).

> [!NOTE]
> A blade is the surface on which service functions or navigation elements appear when selected. -->

## Additional information
Deploying your bot to Azure will involve paying for the services you use. The [billing and cost management](https://docs.microsoft.com/azure/billing/) article helps you understand Azure billing, monitor usage and costs, and manage your account and subscriptions.

## Next steps
> [!div class="nextstepaction"]
> [Set up continuous deployment](bot-service-build-continuous-deployment.md)

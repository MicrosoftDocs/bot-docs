---
title: Deploy your bot to Azure | Microsoft Docs
description: Deploy your bot to the Azure cloud.
keywords: deploy bot, azure deploy, publish bot, az deploy bot, visual studio deploy bot, msbot publish, msbot clone
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: get-started-article
ms.service: bot-service
ms.subservice: abs
ms.date: 12/07/2018
---

# Deploy your bot to Azure

[!INCLUDE [pre-release-label](./includes/pre-release-label.md)]

After you have created your bot and tested it locally, you can deploy it to Azure to make it accessible from anywhere. Deploying your bot to Azure will involve paying for the services you use. The [billing and cost management](https://docs.microsoft.com/en-us/azure/billing/) article helps you understand Azure billing, monitor usage and costs, and manage your account and subscriptions.

In this article, we'll show you how to deploy a C#, JavaScript, and TypeScript bot to Azure using `msbot` and `az` cli tools. We'll also provide steps to deploy C# bots using Visual Studio and the Azure portal. It would be useful to read this article before following the steps, so that you fully understand what is involved in deploying a bot.


# [AZ CLI](#tab/csbotazcli)

## Prerequisites
- If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/) before you begin.
- Install the latest version of the [Azure cli tool](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest).
- Install the latest `botservice` extension for the `az` tool. 
  - First, remove the old version using `az extension remove -n botservice` command. Next, use the `az extension add -n botservice` command to install the latest version.
- Install latest version of the [MSBot](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/MSBot) tool.
- Install [Bot Framework Emulator](https://aka.ms/Emulator-wiki-getting-started).
- Install and configure [ngrok](https://github.com/Microsoft/BotFramework-Emulator/wiki/Tunneling-%28ngrok%29).
- Knowledge of [.bot](v4sdk/bot-file-basics.md) file.

Instructions in the following sections apply to these scenarios:
- You've already created a bot, and now you want to deploy it to Azure. This assumes that you created the required Azure resources, and updated the service references in the .bot file by using msbot connect command.
- You want to deploy a sample to Azure from the [botbuilder-samples](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples) repo. In this case, you do not need to create Azure resources or update the .bot file. The deployment process will use the bot.receipt file to determine which resources are needed and provision it. 

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

### Deploy your bot to Azure
You'll use the ` msbot clone` command to deploy your bot to Azure.

Navigate to the bot folder. 
```cmd 
cd <local bot folder>
```

#### Azure subscription account
Before proceeding, read the instructions that apply to you based on the type of email account you use to log in to Azure.

**MSA email account**

If you are using a [MSA](https://en.wikipedia.org/wiki/Microsoft_account) email account, you will need to create the appId and appSecret to use with `msbot clone services` command. 

- Go to the [Application Registration Portal](https://apps.dev.microsoft.com/). Click on **Add an app** to register your application, create **Application Id**, and **Generate New Password**. 
- Save both application id and the new password you just generated, so you that can use them with `msbot clone services` command. 

| Bot      | Command |
|----------|---------|
| CSharp       | `msbot clone services --folder deploymentScripts\msbotClone --location westus2 --proj-file "<your.csproj>" --name "<bot-name>" --appid "xxxxxxxx" --password "xxxxxxx" --verbose`|
| JavaScript/TypeScript       | `msbot clone services --folder deploymentScripts\msbotClone --location westus2   --code-dir . --name "<bot-name>" --appid "xxxxxxxx" --password "xxxxxxx" --verbose`|


**Business or school account**

If your are using an email account provided to you by your business or school to log in to Azure, you don't need to create the application id and password. You can simply use the following command:

| Bot      | Command |
|----------|---------|
| CSharp      | `msbot clone services --folder deploymentScripts\msbotClone --location westus2 --verbose --proj-file "<your-project-file>" --name "<bot-name>"`|
| JavaScript/TypeScript       | `msbot clone services --folder deploymentScripts\msbotClone --location westus2 --verbose --code-dir . --name "<bot-name>"`

It is highly recommended that you use the `--verbose` option to help troubleshoot problems that might occur during the deployment of the bot. Additional options used with the `msbot clone services` command are described below:

| Arguments    | Description |
|--------------|-------------|
| `folder`     | Location of the `bot.receipe`  file. By default the receipe file is created in the `DeploymentsScript\MSBotClone`. DO NOT MODIFY this file.|
| `location`   | Geographic location used to create the bot service resources. In our example, we are using `westus2`.|
| `proj-file`  | For C# bot it is the .csproj file. For JS bot it is the startup project file name (e.g. index.js) of your local bot.|
| `name`       | A unique name that is used to deploy the bot in Azure. It could be the same name as your local bot. DO NOT include spaces in the name.|

Before Azure resources can be created, you'll be prompted to complete authentication. Follow the instructions that appear on the screen to complete this step.

Note that the above step takes _few seconds to minutes_ to complete, and the resource that are created in Azure have their names mangled. To learn more about name mangling, see [issue# 796](https://github.com/Microsoft/botbuilder-tools/issues/796) in the GitHub repo.

#### Save the secret used to decrypt .bot file
While the bot is being deployed, you'll see a message in the command-line asking you to save the .bot file secret. 

`The secret used to decrypt myAzBot02.bot is:`
`hT6U1SIPQeXlebNgmhHYxcdseXWBZlmIc815PpK0WWA=`

`NOTE: This secret is not recoverable and you should save it in a safe place according to best security practices.
      Copy this secret and use it to open the <file.bot> the first time.`
      
Save the secret for later use.

### Test your bot
After the bot is deployed, the emulator is launched for you automatically. You can test the bot in using production endpoint. If you want to test it locally, make sure your bot is running on your local machine. 

### To update your bot code in Azure
DO NOT use `msbot clone services` command to update your bot code in Azure. You must use the `az bot publish` command as shown below:

```cmd
az bot publish --name "<your-azure-bot-name>" --proj-file "<your-proj-file>" --resource-group "<azure-resource-group>" --code-dir "<folder>" --verbose --version v4
```

| Arguments        | Description |
|----------------  |-------------|
| `name`      | The name you used when you initially deployed your bot to Azure.|
| `proj-file` | For C# bot, it is the .csproj file. For JS bot, it is the startup project file name (e.g. index.js or index.ts) of your local bot.|
| `code-dir`  | Points to the local bot folder.|

# [Visual Studio](#tab/csbotvs)

## Prerequisites
- Knowledge of [.bot](v4sdk/bot-file-basics.md) file.

## Deploy your C# bot from Visual Studio
You will first deploy the bot to Azure from Visual Studio in an App Service. Then you’ll configure your bot with the Azure Bot Service using Bot Channels Registration.

**Note: If your Visual Studio project name has spaces, the deployment steps outlined below will not work.**

In the Solution Explorer window, right click on your project’s node and select Publish.

![publish setting](media/azure-bot-quickstarts/getting-started-publish-setting.png)

2. In the Pick a publish target dialog, ensure **App Service** is selected on the left and **Create New** is selected on the right.

3. Click the Publish button.

4. In upper right of the dialog, ensure the dialog is showing the correct user ID for your Azure subscription.

![publish main](media/azure-bot-quickstarts/getting-started-publish-main.png)

5. Enter App Name, Subscription, Resource Group, and Hosting Plan information.

6. When ready, click Create. It can take a few minutes to complete the process.

7. Once complete, a web browser will open showing your bot’s public URL.

8. Make a copy of this URL (it will be something like https://<yourbotname>.azurewebsites.net/).

> [!NOTE] 
> You’ll need to use the HTTPS version of the URL when registering your bot. Azure provides SSL support with Azure App Service.

## Create your bot channels registration
With your bot deployed in Azure you need to register it with the Azure Bot Service.

1. Access the Azure Portal at https://portal.azure.com.

2. Log in using the same identity you used earlier from Visual Studio to publish your bot.

3. Click Create a resource.

4. In the Search the Marketplace field type Bot Channels Registration and press Enter.

5. In the returned list, choose Bot Channels Registration:

![publish](media/azure-bot-quickstarts/getting-started-bot-registration.png)

6. Click create in the blade that opens.

7. Provide a Name for your bot.

8. Choose the same Subscription where you deployed your bot’s code.

9. Pick your existing Resource group which will set the location.

10. You can choose the F0 Pricing tier for development and testing.

11. Enter your bot’s URL. Make sure you start with HTTPS and that you add the /api/messages For example
https://yourbotname.azurewebsites.net/api/messages

12. Turn off Application Insights for now.

13. Click the Microsoft App ID and password

14. In the new blade click Create New.

15. In the new blade that opens to the right, click the "Create App ID in the App Registration Portal" which will open in a new browser tab.

![bot msa](media/azure-bot-quickstarts/getting-started-msa.png)

16. In the new tab, make a copy of the App ID and save it somewhere. 

17. Click the Generate an app password to continue button.

18. A browser dialog opens and provides you with your app’s password, which will be the only time you get it. Copy and save this password somewhere you can get to later.

19. Click OK once you’ve got the password saved.

20. Just close the browser tab and return to the Azure Portal tab.

21. Paste in your App ID and Password in the correct fields and click OK.

22. Now click Create to set up your channel registration. This can take a few seconds to a few minutes.

## Update your bot’s Application Settings
In order for your bot to authenticate with the Azure Bot Service, you need to add two settings to your Bot’s Application Settings in Azure App Service. 

1. Click App Services. Type your bot’s name in the Subscriptions text box. Then click on your bot's name in the list.

![app service](media/azure-bot-quickstarts/getting-started-app-service.png)

2. In the list of options on the left within your bot's options, locate Application Settings in the Settings section and click it.

![bot id](media/azure-bot-quickstarts/getting-started-app-settings-1.png)

3. Scroll until you find the Application settings section.

![bot msa](media/azure-bot-quickstarts/getting-started-app-settings-2.png)

4. Click Add new setting.

5. Type **MicrosoftAppId** for the name and your App ID for the value.

6. Click Add new setting

7. Type **MicrosoftAppPassword** for the name and your password for the value.

8. Click the Save button up top.

## Test Your Bot in Production
At this point, you can test your bot from Azure using the built-in Web Chat client.

1. Go back to your Resource group in the Azure portal

2. Open your bot.

3. Under **Bot management**, select **Test in Web Chat**.

![test in webchat](media/azure-bot-quickstarts/getting-started-test-webchat.png)

4. Type a message like `Hi` and press Enter. The bot will echo back `Turn 1: You sent Hi`.

---

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

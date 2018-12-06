---
title: Deploy your bot to Azure | Microsoft Docs
description: Deploy your bot to the Azure cloud.
keywords: deploy bot, azure deploy, publish bot, az deploy bot, visual studio deploy bot
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: get-started-article
ms.service: bot-service
ms.subservice: abs
ms.date: 12/06/2018
---

# Deploy your bot to Azure

[!INCLUDE [pre-release-label](./includes/pre-release-label.md)]

After you have created your bot and tested it locally, you can deploy it to Azure to make it accessible from anywhere. Deploying your bot to Azure will involve paying for the services you use. The [billing and cost management](https://docs.microsoft.com/en-us/azure/billing/) article helps you understand Azure billing, monitor usage and costs, and manage your account and subscriptions.

In this article, we'll show you how to deploy a C# and JS bot to Azure using the `az` cli. We'll also provide steps to deploy C# bots using Visual Studio and the Azure portal. It would be useful to read this article before following the steps, so that you fully understand what is involved in deploying a bot.

## Prerequisites
- If you don't have an Azure subscription, create a [free account](https://azure.microsoft.com/free/) before you begin.
- Install the latest version of the [Azure CLI tool](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest).
- Install the latest version of the `botservice` extension. To install the latest version, first remove the old version using `az extension remove -n botservice` command. Then, use the `az extension add -n botservice` command to install the latest version.
- Knowledge of [.bot](v4sdk/bot-file-basics.md) file.

# [AZ CLI](#tab/csbotazcli)
## Deploy JavaScript and C# bots using az cli
The `az` cli works on existing azure resource. Therefore, you'll need to create a new Echo bot and associated resources in Azure before you can deploy your local bot.  

Open a command prompt to log in to the Azure portal as shown below.

```azurecli
az login
```
A browser window will open, allowing you to sign in. 

### Set the subscription 
 Set the subscription by using the following command:

```azurecli
az account set --subscription "<azure-subscription>"
``` 

If you are not sure which subscription to use for deploying the bot, you can view the list of `subscriptions` for your account by using `az account list` command.

### Set the resource group
You also need to specify a resource group. To use an existing resource group, use the following command:

```azurecli
az configure --defaults group="<azure-resource-group>"
```
Use the same resource group that your local bot uses for services like Application Insights, Storage, QnA etc.  

### Create a bot
Use the `create` command to first create a bot in Azure. By creating this bot, you get all the required Azure resources needed for deploying you local bot. 

```azurecli
az bot create --resource-group "<azure-resource-group>" --name "<your-bot-name>" --kind webapp --version v4 --description "<bot-description>" --lang "<language name>"
```

#### Important note

It is highly recommended that you use the `--verbose` option with `az` commands to help troubleshoot problems that might occur during the creation and publishing of the bot. Additional options used with the `create` command are described below:

| Arguments        | Description |
|----------------  |-------------|
| `kind` | Specifies the application type, which in this case is `webapp`.|
| `version`  | The version of the Bot Builder SDK to use to create the bot. We are creating a `v4` bot.|
| `lang` |  `CSharp` or `Node`. If you do not provide a value for this argument, a `CSharp` bot is created.|

Before Azure resources can be created, you'll be prompted to complete authentication. Follow the instructions that appear on the screen to complete this step.

Note that the above step takes _few seconds to minutes_ to complete, and the resource that are created in Azure have their names mangled. To learn more about name mangling, see [issue# 796](https://github.com/Microsoft/botbuilder-tools/issues/796) in the GitHub repo.

Typically, when you create a bot using `az` cli, the following Azure reources are created:

| Resources      | Description |
|----------------|-------------|
| Web App Bot | An Azure Bot Service bot that is deployed to an Azure App Service.|
| [App Service](https://docs.microsoft.com/en-us/azure/app-service/)| Enables you to build and host web applications.|
| [App Service plan](https://docs.microsoft.com/en-us/azure/app-service/azure-web-sites-web-hosting-plans-in-depth-overview)| Defines a set of compute resources for a web app to run.|
| [Application Insights](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-overview)| Provides tools for collecting and analyzing telemetry.|
| [Storage account](https://docs.microsoft.com/en-us/azure/storage/common/storage-introduction)| Provides cloud storage that is highly available, secure, durable, scalable, and redundant.|

## Download the source code
Create a directory where you'd like to download the bot code from the Azure portal. The download `command` takes the name of the bot you created in the Azure portal as an argument. 

```azcli
mkdir "<directory-name>"
cd "<directory-name>"
az bot download --name "<your-bot-name>"
```

## Decrypt the bot file
After you download the code, you need to decrypt the .bot file. 
1. For C# bot, open `appsettings.json` file and copy the _bot file secret_. For JS, it will be in the `.env` file. 
1. Decrypt the .bot file you downloaded using the following command:
```azcli
msbot secret -b "<filename.bot>" --secret "<bot-file-secret>" --clear
```

## Add service references in the .bot file
We need to create a single .bot file that will have all the service references your local bot will need when deployed to Azure. To do that, here are the three files that we'll work with: 

| File           | Description |
|----------------|-------------|
| Local bot file | Contains references to services (e.g. LUIS or QnA) your local bot uses.|
| Bot file downloaded from the Azure portal | This is the .bot file you _decrypted_ in the above section. It contains references to additional services that your local bot needs.|
| A blank file | Used to temporarily store references to all the services from the above bot files.|

1. Create a new text file. We'll use this file to combine the relevant sections of the two bot files. 
1. Open the _decrypted bot file_. Copy the entire file and paste it in the new file you just created. 
1. Switch to the folder that has the local bot file. Open the _local bot file_, copy the references to services such as, LUIS, QnA, Application Insights etc. into the _new bot file_ you just created. Note that the bot file you downloaded from Azure will also have some of these services listed in it. If you want to use resources that you have in your local bot file, then remove the duplicate ones. Any services that was created by the `az bot create` command that you do not need should also be removed from the Azure portal.  

In our example, the following `services` section in the _local bot file_ has an entry for a LUIS service. We only need to copy this part into the `services` section of the _new bot file_.

```json
...
"services": [
     {
        "type": "luis",
        "name": "LuisBot",
        "appId": "xxxxxxxxxxxx",
        "version": "v0.1",
        "authoringKey": "xxxxxxxxxxxxxx",
        "region": "westus",
        "id": "178"
    }
],
...
```

**Important:** If your local bot does not use any additional service references like Application Insights, Storage, LUIS or QnA, then you don't need to copy anything from your _local bot file_ into the _new bot file_. 

4. Delete the existing content of the _local bot file_, and then copy the contents from the _new bot file_ into it. Save the bot file.

## Deploy your bot code

As you deploy your bot, make sure you use the name of the bot that was created in the Azure portal using the `create` command ealier. 

```azurecli
az bot publish --name "<your-bot-name>" --proj-file "<your-proj-file>" --resource-group "<azure-resource-group>" --code-dir "<folder>" --verbose --version v4
```

- In the command above:
  - `proj-file` for:  
     - C# is the .csproj file. 
     - JS is the startup project file name (e.g. index.js) of your local bot.
  - `code-dir` points to the local bot folder.

After the above command completes, you'll see a message with details about the deployment. At this point your bot code is deployed to the Azure portal. 

## Remove bot file secret
1. Open your **Web App Bot** resource
1. In the Azure portal, select **Application Settings** for your bot.
1. Scroll down to the **App Setting Name** section.
1. Update the **botFilePath** with the name of the .bot file your local bot uses.
1. Delete the **botFileSecret** entry. 
1. Save changes.

### Test in WebChat
You can test the bot using the "Test in Webchat" option in the Azure portal.

![Azure Echo bot](media/bot-builder-tools/az-echo-bot.png) 

If you modify your local bot code, use the `az bot publish` command to upload it again to the Azure portal.

# [Visual Studio](#tab/csbotvs)

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
- To see documentation on `az bot` commands, see the [reference](https://docs.microsoft.com/en-us/cli/azure/bot?view=azure-cli-latest) topic.
- If you are unfamiliar with Azure resource group, see this [terminology](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-overview#terminology) topic.

## Next steps
> [!div class="nextstepaction"]
> [Set up continous deployment](bot-service-build-continuous-deployment.md)

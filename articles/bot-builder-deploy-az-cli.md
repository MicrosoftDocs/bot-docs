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
ms.date: 02/07/2019
---

# Deploy your bot 

[!INCLUDE [pre-release-label](./includes/pre-release-label.md)]

After you have created your bot and tested it locally, you can deploy it to Azure to make it accessible from anywhere. Deploying your bot to Azure will involve paying for the services you use. The [billing and cost management](https://docs.microsoft.com/en-us/azure/billing/) article helps you understand Azure billing, monitor usage and costs, and manage your account and subscriptions.

In this article, we'll show you how to deploy C# and JavaScript bots to Azure. It would be useful to read this article before following the steps, so that you fully understand what is involved in deploying a bot.

## Prerequisites
- Install latest version of the [MSBot](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/MSBot) tool.
- A [CSharp](./dotnet/bot-builder-dotnet-sdk-quickstart.md) or [JavaScript](./javascript/bot-builder-javascript-quickstart.md) bot that you have developed on your local machine. 

## Create a Web App Bot in Azure
This section is optional if you have already created a bot in Azure that you'd like to use.

1. Log in to the [Azure portal](https://portal.azure.com).
1. Click **Create new resource** link found on the upper left-hand corner of the Azure portal, then select **AI + Machine Learning > Web App bot**.
1. A new blade will open with information about the Web App Bot. 
1. In the **Bot Service** blade, provide the requested information about your bot.
1. Click **Create** to create the service and deploy the bot to the cloud. This process may take several minutes.

## Download the source code
1. In the **Bot Management** section, click **Build**.
1. Click on **Download Bot source code** link in the right-pane.
1. Follow the prompts to download the code, and then unzip the folder.

## Decrypt the .bot file
The source code you downloaded from the Azure portal includes an encrypted .bot file. You'll need to decrypt it to copy values into your local .bot file.  

1. In the Azure portal, open the Web App Bot resource for your bot.
1. Open the bot's **Application Settings**.
1. In the **Application Settings** window, scroll down to **Application settings**.
1. Locate the **botFileSecret** and copy its value.

Use `msbot cli` to decrypt the file.
```
msbot secret --bot <name-of-bot-file> --secret "<bot-file-secret>" --clear
```

## Update the .bot file
Open the .bot file you decrypted. Copy entries listed under the `services` section, and add them to your local .bot file. For example:

```
{
   "type": "endpoint",
   "name": "production",
   "endpoint": "https://<something>.azurewebsites.net/api/messages",
   "appId": "<App Id>",
   "appPassword": "<App Password>",
   "id": "2
}
```

Save the file.
 
## Setup a repository
Create a git repository using your favorite git source control provider. Commit your code into the repository.
 
## Update App Settings in Azure
1. In the Azure portal, open the **Web App Bot** resource for your bot.
1. Open the bot's **Application Settings**.
1. In the **Application Settings** window, scroll down to **Application settings**.
1. Locate the **botFileSecret** and delete it.
1. Update the name of the bot file to match the file you checked into the repo.
1. Save changes.


## Deploy using Azure Deployment Center
Now, you need to connect your git repository with Azure App Services. Follow instructions in the [setup continuous deployment](https://docs.microsoft.com/en-us/azure/app-service/deploy-continuous-deployment) topic. Note that it is recommended to build using `App Service Kudu build server`.

## Test your deployment
Wait for a few seconds after a succesful deployment and optionally restart your Web App to clear any cache. Go back to your Web App Bot blade and test using the Web Chat provided in the Azure portal.

## Additional resources
- [How to investigate common issues with continuous deployment](https://github.com/projectkudu/kudu/wiki/Investigating-continuous-deployment)

<!--

## Prerequisites

[!INCLUDE [prerequisite snippet](~/includes/deploy/snippet-prerequisite.md)]


## Deploy JavaScript and C# bots using az cli

You've already created and tested a bot locally, and now you want to deploy it to Azure. These steps assume that you have created the required Azure resources.

[!INCLUDE [az login snippet](~/includes/deploy/snippet-az-login.md)]

### Create a Web App Bot

If you don't already have a resource group to which to publish your bot, create one:

[!INCLUDE [az create group snippet](~/includes/deploy/snippet-az-create-group.md)]

[!INCLUDE [az create web app snippet](~/includes/deploy/snippet-create-web-app.md)]

Before proceeding, read the instructions that apply to you based on the type of email account you use to log in to Azure.

#### MSA email account

If you are using an [MSA](https://en.wikipedia.org/wiki/Microsoft_account) email account, you will need to create the app ID and app password on the Application Registration Portal to use with `az bot create` command.

[!INCLUDE [create bot msa snippet](~/includes/deploy/snippet-create-bot-msa.md)]

#### Business or school account

[!INCLUDE [create bot snippet](~/includes/deploy/snippet-create-bot.md)]

### Download the bot from Azure

Next, download the bot you just created. 
[!INCLUDE [download bot snippet](~/includes/deploy/snippet-download-bot.md)]

### Decrypt the downloaded .bot file and use in your project

The sensitive information in the .bot file is encrypted.

[!INCLUDE [decrypt bot snippet](~/includes/deploy/snippet-decrypt-bot.md)]

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

[!INCLUDE [publish snippet](~/includes/deploy/snippet-publish.md)]


[!INCLUDE [clear encryption snippet](~/includes/deploy/snippet-clear-encryption.md)]

## Additional resources

[!INCLUDE [additional resources snippet](~/includes/deploy/snippet-additional-resources.md)]

## Next steps
> [!div class="nextstepaction"]
> [Set up continous deployment](bot-service-build-continuous-deployment.md)

-->

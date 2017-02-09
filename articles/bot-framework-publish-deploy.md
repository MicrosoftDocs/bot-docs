---
title: Bot Framework Deploy Bot to Azure | Microsoft Docs
description: Detailed walkthrough of how to deploy a bot to Microsoft Azure.
services: Bot Framework
documentationcenter: BotFramework-Docs
author: kbrandl
manager: rstand

ms.service: Bot Framework
ms.topic: article
ms.workload: Cognitive Services
ms.date: 02/06/2017
ms.author: v-kibran@microsoft.com

---
# Deploy a bot to Microsoft Azure

> [!NOTE]
> This article is not applicable for bots that are created by using the Azure Bot Service (since bot deployment is included within the functionality that the Azure Bot Service provides). 

There are several valid ways to deploy a bot to Azure. 
This article presents a detailed walk through of the following three methods: 

- [Deploy from Visual Studio ](#vs) 
- [Deploy via continuous integration from local git](#git)
- [Deploy via continuous integration from GitHub](#github)

> [!IMPORTANT]
> You must have a Microsoft Azure subscription before you can deploy your bot to Microsoft Azure. 
If you do not already have a subscription, you can register for a [free trial](https://azure.microsoft.com/en-us/free/).

## <a id="vs"></a>Deploy from Visual Studio

The process of deploying a bot via Visual Studio consists of the following steps:

- [Step 1: Launch the Microsoft Azure Publishing Wizard in Visual Studio](#vs1)
- [Step 2: Use the Azure Publishing Wizard to publish your bot to the cloud](#vs2)
- [Step 3: Test the connection to your bot](#vs3)

### <a id="vs1"></a>Step 1: Launch the Microsoft Azure Publishing Wizard in Visual Studio

- Open your project in Visual Studio.
- In Solution Explorer, right-click on the project and select **Publish**. This starts the Microsoft Azure publishing wizard.

![Right-click on the project and choose Publish to start the Microsoft Azure publishing wizard](media/connector-getstarted-publish-dialog.png)

### <a id="vs2"></a>Step 2: Use the Azure Publishing Wizard to publish your bot to the cloud 

- Select **Microsoft Azure App Service** as the project type and click **Next**.<br/><br/>
![Select Microsoft Azure App Service and click Next](media/connector-getstarted-publish.png)

- Create your App Service by clicking **New…** on the right side of the dialog. <br/><br/>
![Click New to create a new Azure App Service](media/connector-getstarted-publish-app-service.png)

- Click **Change Type** and change the service's type to Web App. 
Then, name your web app and fill out the rest of the information as appropriate for your implementation. <br/><br/>
![Name your App Service, then click New App Service Plan to define one](media/connector-getstarted-publish-app-service-create.png)

- Click **Create** to create your app service. <br/><br/>
![Create the App Service Plan by clicking Create](media/connector-getstarted-publish-app-service-create-spinner.png)

- Copy the **Destination URL** value to the clipboard 
(you'll need this value later to test the connection to the bot and to [register](bot-framework-publish-register.md) it with the Bot Framework Developer Portal), 
and then click **Validate Connection** to verify that the settings have been configured correctly. 
If validation is successful, click **Next**. <br/><br/>
![Validate Connection and click Next to proceed to the final step.](media/connector-getstarted-publish-destination.png)

- By default, your bot will be published in a Release configuration. 
(If you want to debug your bot, change **Configuration** to Debug.) 
Click **Publish** to publish your bot to Microsoft Azure. <br/><br/>
![Select Release Configuration and click Publish](media/connector-getstarted-publish-configuration.png)

> [!NOTE]
> During the publishing process, you will see a number of messages displayed in the Visual Studio 2015 Output window. 
> When publishing completes, your bot's HTML page will be displayed in your default browser. 

### <a id="vs3"></a>Step 3: Test the connection to your bot

[!include[Verify connection to your bot](../includes/snippet-verify-deployment-using-emulator.md)]

### Next steps

[!include[Next steps](../includes/snippet-next-steps-after-deployment.md)]

## <a id="git"></a>Deploy via continuous integration from local git

The process of deploying a bot via continuous integration from local git consists of the following steps:

- [Step 1: Install the Azure CLI](#git1)
- [Step 2: Create and configure an Azure site](#git2) 
- [Step 3: Commit changes to git and push to the Azure site](#git3)
- [Step 4: Test the connection to your bot](#git4)

### <a id="git1"></a>Step 1: Install the Azure CLI

Install the Azure CLI by following the instructions <a href="https://docs.microsoft.com/en-us/azure/xplat-cli-install" target="_blank">here</a>.

### <a id="git2"></a>Step 2: Create and configure an Azure site 

Login to your Azure account by running this command at the command prompt and following the instructions:

```
azure login
```

Next, run the following command to create a new Azure site and configure it for git (where *\<appname\>* is the name of the site that you want to create):

```
azure site create --git <appname>
```

> [!TIP]
> The URL of the site that is created will be in the following format: *https://appname.azurewebsites.net*.

### <a id="git3"></a>Step 3: Commit changes to git and push to the Azure site

To update the site with your latest changes, run the following commands to commit the changes and push those changes to the Azure site:

```
git add .
git commit -m "<your commit message>"
git push azure master
```

You will be prompted to enter your deployment credentials. 
If you don't yet have them, you can configure them on the Azure Portal by following these steps:

- Login to the <a href="http://portal.azure.com" target="_blank">Azure Portal</a>
- Click the site that you've just created, and open the **Settings** blade
- In the **Publishing** section, click **Deployment credentials**, specify a username and password, and save  
![Deployment credentials](media/publishing-your-bot-deployment-credentials.png)
- Return to the command prompt and provide the deployment credentials as requested

Once you've entered your deployment credentials at the command prompt, your bot will be deployed to the Azure site.

### <a id="git4"></a>Step 4: Test the connection to your bot

[!include[Verify connection to your bot](../includes/snippet-verify-deployment-using-emulator.md)]

### Next steps

[!include[Next steps](../includes/snippet-next-steps-after-deployment.md)]

## <a id="github"></a>Deploy via continuous integration from GitHub

The process of deploying a bot via continuous integration from GitHub consists of the following steps:

- [Step 1: Get a GitHub repository](#github1)
- [Step 2: Create an Azure web app](#github2)
- [Step 3: Set up continous deployment from your GitHub repository to Azure](#github3)
- [Step 4: Update Application settings with bot credentials](#github4)
- [Step 5: Test the connection to your bot](#github5)

> [!NOTE]
> To implement this process, you must have a <a href="http://github.com/" target="_blank">GitHub</a> account. 

> [!TIP]
> Be sure to replace "*echobotsample*" with your bot ID in all settings and URLs that are shown in the examples below. 

### <a id="github1"></a>Step 1: Get a GitHub repository

In this walk through, we'll use the <a href="https://github.com/fuselabs/echobot" target="_blank">echobot</a> GitHub repository, which contains the Node.js code for creating a simple bot. 
Start by <a href="https://help.github.com/articles/fork-a-repo/" target="_blank">forking</a> the <a href="https://github.com/fuselabs/echobot" target="_blank">echobot</a> repository. 

### <a id="github2"></a>Step 2: Create an Azure web app

Next, login to the <a href="http://portal.azure.com/" target="_blank">Azure Portal</a> and create an Azure web app.

![Create an Azure web app](media/azure-create-webapp.png)

### <a id="github3"></a>Step 3: Set up continous deployment from your GitHub repository to Azure

Specify GitHub as the **Deployment Source** for your web app. 
When you are asked to authorize Azure access to your GitHub repo, choose the branch from which to deploy. 

![Set up continuous deployment to Azure from your Github repo](media/azure-deployment.png)

The deployment process may take a minute or two to complete. 
You can verify that the deployment has completed by visiting the web app in a browser. 

> [!NOTE]
> The URL of the web app will be *https://appname.azurewebsites.net*, where *appname* is the value that you specified when creating the app. 
> In this example, the URL is <a href="http://echobotsample.azurewebsites.net" target="_blank">https://echobotsample.azurewebsites.net</a>. 

![Hello world web app in browser](media/azure-browse.png)

### <a id="github4"></a>Step 4: Update Application settings with bot credentials

Update **Application settings** to specify values for **MICROSOFT_APP_ID** and **MICROSOFT_APP_PASSWORD** using the 
values that you acquired when you [registered](bot-framework-publish-register.md) the bot in the Bot Framework Developer Portal. 

> [!NOTE]
> If you have not yet registered the bot in the Bot Framework Developer Portal, you can populate **MICROSOFT_APP_ID** and **MICROSOFT_APP_PASSWORD**  
> with temporary (placeholder) values for now. 
> Once you register your bot, return to the Azure Portal and update these values with the **App ID** and **App Password** values that you acquire during the registration process.

![Enter your Bot Framework App ID and App Secret into Azure settings](media/azure-secrets.png)

### <a id="github5"></a>Step 5: Test the connection to your bot

[!include[Verify connection to your bot](../includes/snippet-verify-deployment-using-emulator.md)]

##<a id="next"></a> Next steps
After you have deployed your bot to the cloud and verified that the deployment was successful by testing the bot using the Bot Framework Emulator, 
the next step in the bot publication process will depend upon whether or not you've already registered your bot with the Bot Framework.

### If you have already registered your bot with the Bot Framework:
Return to the <a href="https://dev.botframework.com" target="_blank">Bot Framework Developer Portal</a> to specify the HTTPS endpoint for your bot, now that it's been deployed. 
For details about this process, see [Maintain bot registration data](bot-framework-publish-register.md#maintain). 

Next, you can [configure the bot to run on one or more channels](bot-framework-publish-configure.md).

### If you have not yet registered your bot with the Bot Framework:
Register your bot by following the instructions found in [Register a bot with the Bot Framework](bot-framework-publish-register.md). 

After you've registered your bot, update the Microsoft App Id and Microsoft App Password values in your application's configuration settings to specify the **app ID** and **password** values that were generated for your bot during the registration process, and then re-deploy the bot. 
Verify the new deployment by testing the bot using the Bot Framework Emulator (supplying App ID and Password that were generated during the registration process). 

Next, you can [configure the bot to run on one or more channels](bot-framework-publish-configure.md).

> [!NOTE]
> Although the examples in this article use Microsoft Azure, you can deploy your bot to any cloud service. 
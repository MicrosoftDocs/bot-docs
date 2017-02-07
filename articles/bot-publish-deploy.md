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
> This tutorial is not applicable for bots that are created by using the Azure Bot Service. 
> For details about deployment in that scenario, see [Create a bot with the Azure Bot Service](bot-framework-azure-getstarted.md).

There are several valid ways to deploy a bot to Azure. 
In this tutorial, we'll present a detailed walk through of the following three deployment methods: 

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
(you'll need this value later to test the connection to the bot and to [register](bot-publish-register.md) it with the Bot Framework Developer Portal), 
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
> The URL of the site that is created will be in the following format: *appname.azurewebsites.net*.

### <a id="git3"></a>Step 3: Commit changes to git and push to the Azure site

To update the site with your latest changes, run the following commands to commit the changes and push those changes to the Azure site:

```
git add .
git commit -m "<your commit message>"
git push azure master
```

You will be prompted to enter your deployment credentials. 
If you don't yet have them, you can configure them on the Azure Portal by following these simple steps:

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
- [Step 4: Update Application settings with temporary App ID and App Secret](#github4)
- [Step 5: Test the connection to your bot](#github5)

### <a id="github1"></a>Step 1: Get a GitHub repository

... 

### <a id="github2"></a>Step 2: Create an Azure web app

... 

### <a id="github3"></a>Step 3: Set up continous deployment from your GitHub repository to Azure

... 

### <a id="github4"></a>Step 4: Update Application settings with temporary App ID and App Secret

... 

### <a id="github5"></a>Step 5: Test the connection to your bot

[!include[Verify connection to your bot](../includes/snippet-verify-deployment-using-emulator.md)]

##<a id="next"></a> Next steps
In this tutorial, you deployed a bot to Microsoft Azure (using any one of the three methods described) and then verified that the deployment was successful by testing the bot using the Bot Framework Emulator. 
As the next step in the publication process, you can now [configure the bot](bot-publish-configure.md) to run on one or more channels by using the Bot Framework Developer Portal.

> [!NOTE]
> Although this tutorial uses Microsoft Azure, you can deploy your bot to any cloud service. 
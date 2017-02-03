---
layout: page
title: Deploying your Bot to the Cloud
permalink: /en-us/deployment/
weight: 4025
parent1: Deploying your Bot to the Cloud
---

You can deploy your bot to your favorite cloud service but we encourage you to use Microsoft Azure.

<div class="docs-text-note"><strong>IMPORTANT</strong>: You must have an Azure subscription before you can deploy your bot to Azure. For information about getting a free Azure subscription, see <a href="https://azure.microsoft.com/en-us/free/" target="_blank">Azure Subscription</a>.</div>

The steps shown here assume that you have a complete bot that's ready to publish.

In Visual Studio, right click on the project in Solution Explorer and select **Publish**. (Alternatively, click the **Build** menu and select **Publish**.) This starts the Azure publishing wizard.

![Right click on the project and choose __Publish__ to start the Azure publish wizard](/en-us/images/connector/connector-getstarted-publish-dialog.png)

Next, select **Microsoft Azure App Service** as the project type.

![Select %Microsoft Azure App Service and click Next](/en-us/images/connector/connector-getstarted-publish.png)

Now, create your App Service by clicking **New…** on the right side of the dialog.

![Click new to create a _New..._ Azure App Service](/en-us/images/connector/connector-getstarted-publish-app-service.png)

Click **Change Type** and change the service's type to Web App. Then, name your web app and fill out the rest of the information as appropriate for your implementation. You can set **App Service Plan** to any name. The plan lets you give a name to a combination of location and system size so you can reuse it for future deployments. 

![Give your App Service a name, then click New App Service Plan to define one](/en-us/images/connector/connector-getstarted-publish-app-service-create.png)

Click **Create** to create your app service.

![Create your definition for an App Service Plan ](/en-us/images/connector/connector-getstarted-publish-app-service-create-spinner.png)

![Complete the Create App Service wizard by clicking Create](/en-us/images/connector/connector-getstarted-publish-destination.png)

After you're returned to the Publish Web wizard, copy the destination URL to the clipboard because you'll need it when you register your bot. Next, click **Validate Connection** to ensure that the configuration is good. If all goes well, click **Next**.

By default your bot will be published in a Release configuration. If you want to debug your bot, change **Configuration** to Debug. Next click **Publish** to finish publishing your bot to Azure.

![Validate and click next to move on to the last step.](/en-us/images/connector/connector-getstarted-publish-configuration.png)

During the publishing process, you will see a number of messages displayed in the Visual Studio 2015 Output window. After publishing completes, your bot's HTML page is displayed in your default browser. 


### Test the connection to your bot

Test your deployment by using [Bot Framework Emulator](/en-us/emulator/). In the emulator, use the URL of the newly deployed bot. 

## Next steps

* Register your bot with Bot Framework Developer Portal (see [Registering a Bot](/en-us/registration)). 

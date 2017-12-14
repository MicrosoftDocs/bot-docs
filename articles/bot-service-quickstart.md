---
title: Create a bot with Bot Service | Microsoft Docs
description: Learn how to create a bot with Bot Service, an integrated, dedicated bot development environment.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 12/13/2017
---
# Create a bot with Bot Service
Bot Service provides the core components for creating bots, including the Bot Builder SDK for developing bots and the Bot Framework for connecting bots to channels. Bot Service provides five templates you can choose from when creating your bots with support for .NET and Node.js. In this topic, learn how to use Bot Service to create a new bot that uses the Bot Builder SDK.

## Log in to Azure
Log in to the [Azure portal](http://portal.azure.com).

> [!TIP]
> If you do not already have a subscription, you can register for a <a href="https://azure.microsoft.com/en-us/free/" target="_blank">free account</a>.

## Create a new bot service

1. Click the **New** button found on the upper left-hand corner of the Azure portal, then select **AI + Cognitive Services > Web App bot**. 

2. A new blade will open with information about the **Web App Bot**. Click the **Create** button to start the bot creation process. 

3. In the **Bot Service** blade, provide the requested information about your bot as specified in the table below the image.  <br/>
 ![Create Web App Bot blade](~/media/azure-bot-quickstarts/sdk-create-bot-service-blade.png)

 | Setting | Suggested value | Description |
 | ---- | ---- | ---- |
 | **Bot name** | Your bot's display name | The display name for the bot that appears in channels and directories. This name can be changed at anytime. |
 | **Subscription** | Your subscription | Select the Azure subscription you want to use. |
 | **Resource Group** | myResourceGroup | You can create a new [resource group](/azure/azure-resource-manager/resource-group-overview#resource-groups) or choose from an existing one. |
 | **Location** | The default location | Select the geographic location for your resource group. Your location choice can be any location listed, though it's often best to choose a location closest to your customer. The location cannot be changed once the bot is created. |
 | **Pricing tier** | F0 | Select a pricing tier. You may update the pricing tier at any time. For more information, see [Bot Service pricing](https://azure.microsoft.com/en-us/pricing/details/bot-service/). |
 | **App name** | A unique name | The unique URL name of the bot. For example, if you name your bot *myawesomebot*, then your bot's URL will be `http://myawesomebot.azurewebsites.net`. The name must use alphanumeric and underscore characters only. There is a 35 character limit to this field. The App name cannot be changed once the bot is created. |
 | **Bot template** | Basic | Choose either **C#** or **Node.js** and select the **Basic** template for this quickstart, then click **Select**. The Basic template creates an echo bot. [Learn more](bot-service-concept-templates.md) about the templates. |
 | **App service plan/Location** | Your app service plan  | Select an [app service plan](https://azure.microsoft.com/en-us/pricing/details/app-service/plans/) location. Your location choice can be any location listed, though it's often best to choose a location closest to your customer. (Not available for Functions Bot.) |
 | **Azure Storage** | Your Azure storage account | You can create a new data storage account or use an existing one. By default, the bot will use [Table Storage](/azure/storage/common/storage-introduction#table-storage). |
 | **Application Insights** | On | Decide if you want to turn [Application Insights](/bot-framework/bot-service-manage-analytics) **On** or **Off**. If you select **On**, you must also specify a regional location. Your location choice can be any location listed, though it's often best to choose a location closest to your customer. |

 > [!NOTE]
 > 
 > If you are creating a **Functions Bot**, you will not see an **App service plan/Location** field. Instead, you will see a *Hosting plan* field. In that case, choose a [Hosting plan](bot-service-overview-readme.md#hosting-plans).

4. Click **Create** to create the service and deploy the bot to the cloud. This process may take several minutes.

Confirm that the bot has been deployed by checking the **Notifications**. The notifications will change from **Deployment in progress...** to **Deployment succeeded**. Click **Go to resource** button to open the bot's resources blade.

 > [!TIP] 
 > For performance reasons, **Functions Bot** running Node.js templates have already been packaged using the *Azure Functions Pack* tool. The *Azure Functions Pack* tool takes all the Node.js modules and combined them into one *.js file.
 > For more information, see [Azure Functions Pack](https://github.com/Azure/azure-functions-pack).
 
## Test the bot
Now that your bot is created, [test it in Web Chat](bot-service-manage-test-webchat.md). Enter a message and your bot should respond.

## Next steps

In this topic, you learned how to create a **Basic** Web App Bot/Functions Bot by using Bot Service and verified the bot's functionality by using the built-in Web Chat control within Azure. Now, learn how to manage your bot and start working with its source code.

> [!div class="nextstepaction"]
> [Manage a bot](bot-service-manage-overview.md)


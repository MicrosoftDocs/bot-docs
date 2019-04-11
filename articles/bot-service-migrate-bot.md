---
title: Migrate your bot to Azure | Microsoft Docs
description: Learn how to migrate your bot from the legacy Bot Framework Portal to a bot service in the Azure portal.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: abs
ms.date: 3/22/2019
monikerRange: 'azure-bot-service-3.0'
---

# Migrate your bot to Azure

All **Azure Bot Service (Preview)** bots created in the [Bot Framework Portal](http://dev.botframework.com) must migrate to the new Bot Service in Azure. The service was made generally available (GA) in December 2017. 

Note that, registration bots connected only to the following channels are *not* required to migrate: **Teams**, **Skype**, or **Cortana**. For example, a registration bot connected to **Facebook** and **Skype** *is required* to migrate but a registration bot connected to **Skype** and **Cortana** *is not required* to migrate.

> [!IMPORTANT]
> Before migrating a Functions bot created with Node.js, it is required that you use **Azure Functions Pack** to package the **node_modules** modules together. Doing so will improve performance during migration and execution of the Functions bot after it is migrated. 
> To package your modules, see [Package a Functions bot with Funcpack](#package-a-functions-bot-with-funcpack).

To migrate your bot, do the following:

1. Sign into the [Bot Framework Portal](http://dev.botframework.com) and click **My bots**.
2. Click **Migrate** button for the bot you want to migrate.
3. Accept the **Terms** and click **Migrate** to start the migration process or click **Cancel** to cancel this action.

> [!IMPORTANT]
> While the migration task is in progress, do not navigate away from the page or refresh the page. Doing so will cause the migration task to stop pre-maturely and you will have to perform the action again. 
> To ensure the migration completes successfully, wait for the confirmation message.

After the migration process completes successfully, the **Migration status** will indicate that the bot is migrated and a **Roll back migration** button will be available for a week after the migration date in case of issues.

Clicking the name of a migrated bot will open the bot in the [Azure portal](http://portal.azure.com).

## Package a Functions bot with Funcpack

Functions bots created with Node.js must be packed using [Funcpack](https://github.com/Azure/azure-functions-pack) before migrating. To Funcpack your project, follow these steps:

1.	[Download](bot-service-build-download-source-code.md) your code locally if you haven’t already.
2.	Update all the npm packages in **packages.json** to the latest versions and then run `npm install`.
3.	Open **messages/index.js** and change `module.exports = { default: connector.listen() }`
to `module.exports = connector.listen();`
4.	Install Funcpack via npm: `npm install -g azure-functions-pack`
5.	To package the **node_modules** directory, run the following command: `funcpack pack ./`
6.	Test your bot locally by running the Functions bot using the Bot Framework Emulator. More info on how to run the *funcpack* bot [here](https://github.com/Azure/azure-functions-pack#how-to-run). 
7.	Upload your code back to Azure. Make sure the `.funcpack` directory is uploaded. You do not need to upload the **node_modules** directory.
8. Test your remote bot to make sure it responds as expected.
9. Migrate your bot using the steps above.

## Migration under the hood

Depending on the type of bot you are migrating, the list below can help you better understand what is happening under the hood.

* **Web App Bot** or **Functions Bot**: For these types of bots, the source code project is copied from the old bot to the new bot. Other resources such as your bot's storage, Application Insights, LUIS, etc. are left as is. In those cases, the new bot contains a copy of the IDs/keys/passwords to those existing resources. 
* **Bot Channels Registration**: For this type of bots, the migration process simply create a new **Bot Channels Registration** and copy the endpoint from the old bot over. 
* Regardless of the types of bots you are migrating, the migration process does not modify the existing bot's state in anyway. This allows you to safely roll back if you need to do so.
* If you have [continuous deployment](bot-service-build-continuous-deployment.md) set up, you will need to set it up again so that your source control connects to the new bot instead.

## Understanding Azure Resources after migration
Once migration is done, your **Resource Group** will contain a number of Azure resources that are needed for your bot to work. The type and number of resources depend on the type of bot have you migrated. Refer to following sections to lean more.

### Registration Bot

This is the simplest type. The Resource Group in Azure will only contain one resource of type “Bot Channel Registration”. This is the equivalent of the previous bot record in the Bot Framework Developer Portal.

![Bot Channel Registration bot listings in Azure](~/media/bot-service-migrate-bot/channel-registration-bot.png)

### Web App Bot
The Web App bot migration will provision a Bot Service resource of type “Web App Bot” and a new App Service Web App (in green in the screenshot below). The previous Azure Bot Service (Preview) bot will still be there and can be deleted (in red in the screenshot below).

![Web App bot listings in Azure](~/media/bot-service-migrate-bot/web-app-bot.png)

### Functions Bot
The Functions bot migration will provision a Bot Service resource of type “Functions Bot” and a new App Service Functions App (in green in the screenshot below). The previous Azure Bot Service (Preview) bot will still be there and can be deleted (in red in the screenshot below).

![Functions bot listings in Azure](~/media/bot-service-migrate-bot/functions-bot.png)


## Roll back migration

In the event something went wrong with the bot during migration or after it is migrated, you can **Roll back migration**. To roll back a migration, do the following:

1. Sign into the [Bot Framework Portal](http://dev.botframework.com) and click **My bots**.
2. Click **Roll back migration** button for the bot you want to roll back. A prompt will appear.
3. Click **Yes, roll back** to proceed or **Cancel** to cancel the roll back action.

> [!NOTE]
> Roll back functionality will be available for a week after migration, and should be used only if you run into issues in the migrated bot.

## Migration troubleshooting/Known issues
My node.js/functions bot migrated successfully, but it fails to respond:

* **Check endpoint**
  * Go to the **Settings** blade in your bot resource and verify that the bot endpoint has a “code” querystring parameter with a value in it. If not, you need to add it.
* **Check secrets folder in kudu for backups**
  * In some rare cases there might be a few backup secret files that are creating a conflict. Go to the **home\data\Functions\secrets** folder in Kudu and delete any **host.snapshot** (or **host.backup**) file you find in there. There should be only one **host.json** and one **messages.json**. Finally restart the App Service and retry chatting with your bot.

For any other issue please submit a CRI to Azure Support or file an issue in [GitHub](https://github.com/MicrosoftDocs/bot-framework-docs/issues).


## Next steps

Now that your bot is migrated, learn how to manage your bot from the Azure portal.

> [!div class="nextstepaction"]
> [Manage a bot](bot-service-manage-overview.md)

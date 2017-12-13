---
title: Migrate your bot to Azure | Microsoft Docs
description: Learn how to migrate your bot from the legacy Bot Framework Portal to a bot service in the Azure portal.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 12/13/2017
---

# Migrate your bot to Azure

All bots created in the [Bot Framework Portal](http://dev.botframework.com) must migrate to the new Bot Service in Azure by 3/31/2018.

To migrate your bot, do the following:

1. Sign into your [Bot Framework Portal](http://dev.botframework.com) and click **My bots**.
2. Click **Migrate** button for the bot you want to migrate.
   > [!NOTE]
   > Before migrating a Functions bot using Node.js, it is recommended that you use the *Azure Functions Pack* to package the Node.js modules together. Doing so will improve performance during migration and execution of the Functions bot after it is migrated. 
   > For more information about, see [Azure Functions Pack](https://github.com/Azure/azure-functions-pack).
3. Accept the **Terms** and click **Migrate** to start the migration process or click **Cancel** to cancel this action.

> [!IMPORTANT]
> While the migration task is in progress, do not navigate away from the page or refresh the page. Doing so will cause the migration task to stop pre-maturely and you will have to perform the action again. 
> To ensure the migration completes successfully, wait for the confirmation message.

After the migration process complete successfully, the **Migration status** will indicate that the bot is migrated and a **Roll back migration** button is available if you need to undo the migration.

Clicking the name of a migrated bot will open the bot in the [Azure portal](http://portal.azure.com).

## Migration under the hood

Depending on the type of bot you are migrating, the list below can help you better understand what is happening under the hood.

* **Web App Bot** or **Functions Bot**: For these types of bots, the source code project is copied from the old bot to the new bot. Other resources such as your bot's storage, Application Insights, LUIS, etc. are left as is. In those cases, the new bot contains a copy of the IDs/keys/passwords to those existing resources. 
* **Bot Channels Registration**: For this type of bots, the migration process simply create a new **Bot Channels Registration** and copy the endpoint from the old bot over. 
* Regardless of the types of bot you are migrating, the migration process does not modify the existing bot's state in anyway. This allows you to safely roll back if you need to do so.
* If you have [continuous deployment](bot-service-build-continuous-deployment.md) set up, you will need to set it up again so that your source control connects to the new bot instead.

## Roll back migration

In the event something went wrong with the bot during migration or after it is migrated, you can **Roll back migration**. To roll back a migration, do the following:

1. Sign into your [Bot Framework Portal](http://dev.botframework.com) and click **My bots**.
2. Click **Roll back migration** button for the bot you want to roll back. A prompt will appear.
3. Click **Yes, roll back** to proceed or **Cancel** to cancel the roll back action.

> [!NOTE]
> Roll back functionality is coming soon for registration bots.

## Next steps

Now that your bot is migrated, learn how to manage your bot from the Azure portal.

> [!div class="nextstepaction"]
> [Manage a bot](bot-service-manage-overview.md)

---
title: Manage custom state data with Azure Cosmos DB | Microsoft Docs
description: Learn how to save and retrieve state data using Azure Cosmos DB with the Bot Framework SDK for Node.js.
author: DucVo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Manage custom state data with Azure Cosmos DB for Node.js

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

In this article, you’ll implement Cosmos DB storage to store and manage your bot’s state data. The default Connector State Service used by bots is not intended for the production environment. You should either use [Azure Extensions](https://www.npmjs.com/package/botbuilder-azure) available on GitHub or implement a custom state client using data storage platform of your choice. Here are some of the reasons to use custom state storage:

- higher state API throughput (more control over performance)
- lower-latency for geo-distribution
- control over where the data is stored (e.g.: West US vs East US)
- access to the actual state data
- state data db not shared with other bots
- store more than 32kb

## Prerequisites

- [Node.js](https://nodejs.org/en/).
- [Bot Framework Emulator](~/bot-service-debug-emulator.md)
- Must have a Node.js bot. If you do not have one, go [create a bot](bot-builder-nodejs-quickstart.md). 

## Create Azure account
If you don't have an Azure account, click [here](https://azure.microsoft.com/en-us/free/) to sign up for a free account.

## Set up the Azure Cosmos DB database
1. After you’ve logged into the Azure portal, create a new *Azure Cosmos DB* database by clicking **New**. 
2. Click **Databases**. 
3. Find **Azure Cosmos DB** and click **Create**.
4. Fill in the fields. For the **API** field, select **SQL (DocumentDB)**. When done filling in all the fields, click the **Create** button at the bottom of the screen to deploy the new database. 
5. After the new database is deployed, navigate to your new database. Click **Access keys** to find keys and connection strings. Your bot will use this information to call the storage service to save state data.

## Install botbuilder-azure module

To install the `botbuilder-azure` module from a command prompt, navigate to the bot's directory and run the following npm command:

```nodejs
npm install --save botbuilder-azure
```

## Modify your bot code

To use your **Azure Cosmos DB** database, add the following lines of code to your bot's **app.js** file.

1. Require the newly installed module.

   ```javascript
   var azure = require('botbuilder-azure'); 
   ```

2. Configure the connection settings to connect to Azure.
   ```javascript
   var documentDbOptions = {
       host: 'Your-Azure-DocumentDB-URI', 
       masterKey: 'Your-Azure-DocumentDB-Key', 
       database: 'botdocs',   
       collection: 'botdata'
   };
   ```
   The `host` and `masterKey` values can be found in the **Keys** menu of your database. If the `database` and `collection` entries do not exist in the Azure database, they will be created for you.

3. Using the `botbuilder-azure` module, create two new objects to connect to the Azure database. First, create an instance of `DocumentDBClient` passing in the connection configuration settings (defined as `documentDbOptions` from above). Next, create an instance of `AzureBotStorage` passing in the `DocumentDBClient` object. For example:
   ```javascript
   var docDbClient = new azure.DocumentDbClient(documentDbOptions);

   var cosmosStorage = new azure.AzureBotStorage({ gzipData: false }, docDbClient);
   ```

4. Specify that you want to use your custom database instead of the in-memory storage. For example:

   ```javascript
   var bot = new builder.UniversalBot(connector, function (session) {
        // ... Bot code ...
   })
   .set('storage', cosmosStorage);
   ```

Now you are ready to test the bot with the emulator.

## Run your bot app

From a command prompt, navigate to your bot's directory and run your bot with the following command:

```nodejs
node app.js
```

## Connect your bot to the emulator

At this point, your bot is running locally. Start the emulator and then connect to your bot from the emulator:

1. Type <strong>http://localhost:port-number/api/messages</strong> into the emulator's address bar, where port-number matches the port number shown in the browser where your application is running. You can leave <strong>Microsoft App ID</strong> and <strong>Microsoft App Password</strong> fields blank for now. You'll get this information later when you [register your bot](~/bot-service-quickstart-registration.md).
2. Click **Connect**.
3. Test your bot by sending your bot a message. Interact with your bot as you normally would. When you are done, go to **Storage Explorer** and view your saved state data.

## View state data on Azure Portal

To view the state data, sign into your Azure portal and navigate to your database. Click  **Data Explorer (preview)** to verify that the state information from your bot is being saved.

## Next step

Now that you have full control over your bot's state data, let's take a look at how you can use it to better manage conversation flow.

> [!div class="nextstepaction"]
> [Manage conversation flow](bot-builder-nodejs-dialog-manage-conversation-flow.md)

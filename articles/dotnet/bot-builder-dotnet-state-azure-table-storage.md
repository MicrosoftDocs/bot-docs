---
title:  Manage custom state data with Azure Table storage | Microsoft Docs
description: Learn how to save and retrieve state data using Azure Table Storage with the Bot Framework SDK for .NET
author: kaiqb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Manage custom state data with Azure Table Storage for .NET

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

In this article, you’ll implement Azure Table storage to store and manage your bot’s state data. The default Connector State Service used by bots is not intended for the production environment. You should either use [Azure Extensions](https://github.com/Microsoft/BotBuilder-Azure) available on GitHub or implement a custom state client using data storage platform of your choice. Here are some of the reasons to use custom state storage:
 - Higher state API throughput (more control over performance)
 - Lower-latency for geo-distribution
 - Control over where the data is stored
 - Access to the actual state data
 - Store more than 32kb of data

## Prerequisites
You'll need:
 - [Microsoft Azure Account](https://azure.microsoft.com/en-us/free/)
 - [Visual Studio 2015 or later](https://www.visualstudio.com/)
 - [Bot Builder Azure NuGet Package](https://www.nuget.org/packages/Microsoft.Bot.Builder.Azure/)
 - [Autofac Web Api2 NuGet Package](https://www.nuget.org/packages/Autofac.WebApi2/)
 - [Bot Framework Emulator](https://emulator.botframework.com/)
 - [Azure Storage Explorer](http://storageexplorer.com/)
 
## Create Azure account
If you don't have an Azure account, click [here](https://azure.microsoft.com/en-us/free/) to sign up for a free account.

## Set up the Azure Table storage service
1. After you’ve logged into the Azure portal, create a new Azure Table storage service by clicking on **New**. 
2. Search for **Storage account** that implements the Azure Table. 
3. Fill in the fields, click the **Create** button at the bottom of the screen to deploy the new storage service. After the new storage service is deployed, it will display features and options available to you.
4. Select the **Access keys** tab on the left, and copy the connection string for later use. Your bot will use this connection string to call the storage service to save state data.

## Install NuGet packages
1. Open an existing C# bot project, or create a new one using the C# Bot template in Visual Studio. 
2. Install the following NuGet packages:
   - Microsoft.Bot.Builder.Azure
   - Autofac.WebApi2

## Add connection string 
Add the following entry in your Web.config file: 
```XML
  <connectionStrings>
    <add name="StorageConnectionString"
    connectionString="YourConnectionString"/>
  </connectionStrings>
```
Replace "YourConnectionString" with the connection string to the Azure Table storage you saved earlier. Save the Web.config file.

## Modify your bot code
In the Global.asax.cs file, add the following `using` statements:
```cs
using Autofac;
using System.Configuration;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
```
In the `Application_Start()` method, create an instance of the `TableBotDataStore` class. The `TableBotDataStore` class implements the `IBotDataStore<BotData>` interface. The `IBotDataStore` interface allows you to override the default Connector State Service connection.
 ```cs
 var store = new TableBotDataStore(ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString);
 ```
Register the service as shown below:
 ```cs
 Conversation.UpdateContainer(
            builder =>
            {
                builder.Register(c => store)
                          .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
                          .AsSelf()
                          .SingleInstance();

                builder.Register(c => new CachingBotDataStore(store,
                           CachingBotDataStoreConsistencyPolicy
                           .ETagBasedConsistency))
                           .As<IBotDataStore<BotData>>()
                           .AsSelf()
                           .InstancePerLifetimeScope();

                
            });
 ```
Save the Global.asax.cs file.

## Run your bot app
Run your bot in Visual Studio, the code you added will create the custom **botdata** table in Azure.

## Connect your bot to the emulator
At this point, your bot is running locally. Next, start the emulator and then connect to your bot in the emulator:
1. Type http://localhost:port-number/api/messages into the address bar, where port-number matches the port number shown in the browser where your application is running. You can leave <strong>Microsoft App ID</strong> and <strong>Microsoft App Password</strong> fields blank for now. You'll get this information later when you [register your bot](~/bot-service-quickstart-registration.md).
2. Click **Connect**. 
3. Test your bot by typing a few messages in the emulator. 

## View data in Azure Table storage
To view the state data, open **Storage Explorer** and connect to Azure using your Azure Portal credential or connect directly to the table using the storage name and storage key then navigate to your table name.  

## Next steps
In this article, you implemented Azure Table storage for saving and managing your bot's data. Next, learn how to model conversation flow by using dialogs.

> [!div class="nextstepaction"]
> [Manage conversation flow](bot-builder-dotnet-manage-conversation-flow.md)


## Additional resources

If you are unfamiliar with Inversion of Control containers and Dependency Injection pattern used in the code above, go to [Autofac](http://autofac.readthedocs.io/en/latest/) site for information. 

You can also download a complete [Azure Table storage](https://github.com/Microsoft/BotBuilder-Azure/tree/master/CSharp/Samples/AzureTable) sample from GitHub.

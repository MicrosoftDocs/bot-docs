---
title: Manage custom state data with Azure Cosmos DB | Microsoft Docs
description: Learn how to save and retrieve state data using Azure Cosmos DB with the Bot Framework SDK for .NET
author: kaiqb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Manage custom state data with Azure Cosmos DB for .NET

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

In this article, you’ll implement Azure Cosmos DB to store and manage your bot’s state data. The default Connector State Service used by bots is not intended for the production environment. You should either use [Azure Extensions](https://github.com/Microsoft/BotBuilder-Azure) available on GitHub or implement a custom state client using data storage platform of your choice. Here are some of the reasons to use custom state storage:
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
 - [Bot Framework Emulator](~/bot-service-debug-emulator.md)
 
## Create Azure account
If you don't have an Azure account, click [here](https://azure.microsoft.com/en-us/free/) to sign up for a free account.

## Set up the Azure Cosmos DB database
1. After you’ve logged into the Azure portal, create a new *Azure Cosmos DB* database by clicking **New**. 
2. Click **Databases**. 
3. Find **Azure Cosmos DB** and click **Create**.
4. Fill in the fields. For the **API** field, select **SQL (DocumentDB)**. When done filling in all the fields, click the **Create** button at the bottom of the screen to deploy the new database. 
5. After the new database is deployed, navigate to your new database. Click **Access keys** to find keys and connection strings. Your bot will use this information to call the storage service to save state data.

## Install NuGet packages
1. Open an existing C# bot project, or create a new one using the Bot template in Visual Studio. 
2. Install the following NuGet packages:
   - Microsoft.Bot.Builder.Azure
   - Autofac.WebApi2

## Add connection string 
Add the following entries into the Web.config file:
```XML
<add key="DocumentDbUrl" value="Your DocumentDB URI"/>
<add key="DocumentDbKey" value="Your DocumentDB Key"/>
```
You'll replace the value with your URI and Primary Key found in your Azure Cosmos DB. Save the Web.config file.

## Modify your bot code
To use **Azure Cosmos DB** storage, add the following lines of code to your bot's **Global.asax.cs** file inside the **Application_Start()** method.

```cs
using System;
using Autofac;
using System.Web.Http;
using System.Configuration;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;

namespace SampleApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var uri = new Uri(ConfigurationManager.AppSettings["DocumentDbUrl"]);
            var key = ConfigurationManager.AppSettings["DocumentDbKey"];
            var store = new DocumentDbBotDataStore(uri, key);

            Conversation.UpdateContainer(
                        builder =>
                        {
                            builder.Register(c => store)
                                .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
                                .AsSelf()
                                .SingleInstance();

                            builder.Register(c => new CachingBotDataStore(store, CachingBotDataStoreConsistencyPolicy.ETagBasedConsistency))
                                .As<IBotDataStore<BotData>>()
                                .AsSelf()
                                .InstancePerLifetimeScope();

                        });

        }
    }
}
```

Save the global.asax.cs file. Now you are ready to test the bot with the emulator.

## Run your bot app
Run your bot in Visual Studio, the code you added will create the custom **botdata** table in Azure.

## Connect your bot to the emulator
At this point, your bot is running locally. Next, start the emulator and then connect to your bot in the emulator:
1. Type http://localhost:port-number/api/messages into the address bar, where port-number matches the port number shown in the browser where your application is running. You can leave <strong>Microsoft App ID</strong> and <strong>Microsoft App Password</strong> fields blank for now. You'll get this information later when you [register your bot](~/bot-service-quickstart-registration.md).
2. Click **Connect**. 
3. Test your bot by typing a few messages in the emulator. 

## View state data on Azure Portal
To view the state data, sign into your Azure portal and navigate to your database. Click **Data Explorer (preview)** to verify that the state information from your bot is being saved. 

## Next steps
In this article, you used Cosmos DB for saving and managing your bot's data. Next, learn how to model conversation flow by using dialogs.

> [!div class="nextstepaction"]
> [Manage conversation flow](bot-builder-dotnet-manage-conversation-flow.md)

## Additional resources
If you are unfamiliar with Inversion of Control containers and Dependency Injection pattern used in the code above, visit [Autofac](http://autofac.readthedocs.io/en/latest/) site for information. 

You can also download a [sample](https://github.com/Microsoft/BotBuilder-Azure/tree/master/CSharp/Samples/DocumentDb) from GitHub to learn more about using Cosmos DB for managing state. 

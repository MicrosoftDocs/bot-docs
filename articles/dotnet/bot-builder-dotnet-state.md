---
title: Manage state data | Microsoft Docs
description: Learn how to save and retrieve state data with the Bot Framework SDK for .NET.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/17
monikerRange: 'azure-bot-service-3.0'
---

# Manage state data

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-state.md)
> - [Node.js](../nodejs/bot-builder-nodejs-state.md)

[!INCLUDE [State concept overview](../includes/snippet-dotnet-concept-state.md)]

## In-memory data storage

In-memory data storage is intended for testing only. This storage is volatile and temporary. The data is cleared each time the bot is restarted. To use the in-memory storage for testing purposes, you will need to: 

Install the following NuGet packages: 
- Microsoft.Bot.Builder.Azure
- Autofac.WebApi2

In the **Application_Start** method, create a new instance of the in-memory storage, and register the new data store:

```cs
// Global.asax file

var store = new InMemoryDataStore();

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
GlobalConfiguration.Configure(WebApiConfig.Register);

```

You can use this method to set your own custom data storage or use any of the *Azure Extensions*.

## Manage custom data storage

For performance and security reasons in the production environment, you may implement your own data storage or consider implementing one of the following data storage options:

1. [Manage state data with Cosmos DB](bot-builder-dotnet-state-azure-cosmosdb.md)

2. [Manage state data with Table storage](bot-builder-dotnet-state-azure-table-storage.md)

With either of these [Azure Extensions](https://www.nuget.org/packages/Microsoft.Bot.Builder.Azure/) options, the mechanism for setting and persisting data via the Bot Framework SDK for .NET remains the same as the in-memory data storage.

## Bot state methods

This table lists the methods that you can use to manage state data.

| Method | Scoped to | Objective |                                                
|----|----|----|
| `GetUserData` | User | Get state data that has previously been saved for the user on the specified channel |
| `GetConversationData` | Conversation | Get state data that has previously been saved for the conversation on the specified channel |
| `GetPrivateConversationData` | User and Conversation | Get state data that has previously been saved for the user within the conversation on the specified channel |
| `SetUserData` | User | Save state data for the user on the specified channel |
| `SetConversationData` | Conversation | Save state data for the conversation on the specified channel. <br/><br/>**Note**: Because the `DeleteStateForUser` method does not delete data that has been stored using the `SetConversationData` method, you must NOT use this method to store a user's personally identifiable information (PII). |
| `SetPrivateConversationData` | User and Conversation | Save state data for the user within the conversation on the specified channel |
| `DeleteStateForUser` | User | Delete state data for the user that has previously been stored by using either the `SetUserData` method or the `SetPrivateConversationData` method. <br/><br/>**Note**: Your bot should call this method when it receives an activity of type [deleteUserData](bot-builder-dotnet-activities.md#deleteuserdata) or an activity of type [contactRelationUpdate](bot-builder-dotnet-activities.md#contactrelationupdate) that indicates the bot has been removed from the user's contact list. |

If your bot saves state data by using one of the "**Set...Data**" methods, future messages that your bot receives in the same context will contain that data, which your bot can access by using the corresponding "**Get...Data**" method.

## Useful properties for managing state data

Each [Activity][Activity] object contains properties that you will use to manage state data.

| Property | Description | Use case |
|----|----|----|
| `From` | Uniquely identifies a user on a channel | Storing and retrieving state data that is associated with a user |
| `Conversation` | Uniquely identifies a conversation | Storing and retrieving state data that is associated with a conversation |
| `From` and `Conversation` | Uniquely identifies a user and conversation | Storing and retrieving state data that is associated with a specific user within the context of a specific conversation |

> [!NOTE]
> You may use these property values as keys even if you opt to store state data in your own database, rather than using the Bot Framework state data store.

## Handle concurrency issues

Your bot may receive an error response with HTTP status code **412 Precondition Failed** 
when it attempts to save state data, if another instance of the bot has changed the data. 
You can design your bot to account for this scenario, as shown in the following code example.

[!code-csharp[Handle exception saving state](../includes/code/dotnet-state.cs#handleException)]

## Additional resources

- [Bot Framework troubleshooting guide](../bot-service-troubleshoot-general-problems.md)
- <a href="/dotnet/api/?view=botbuilder-3.11.0" target="_blank">Bot Framework SDK for .NET Reference</a>

[Activity]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/dc/d2f/class_microsoft_1_1_bot_1_1_connector_1_1_activity.html

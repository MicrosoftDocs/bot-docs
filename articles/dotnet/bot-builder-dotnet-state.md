---
title: Manage state data | Microsoft Docs
description: Learn how to save and retrieve state data with the Bot Builder SDK for .NET.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 12/13/17
---

# Manage state data
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-state.md)
> - [Node.js](../nodejs/bot-builder-nodejs-state.md)

[!include[State concept overview](../includes/snippet-dotnet-concept-state.md)]  

## In-memory data storage

In-memory data storage is intended for testing only. This storage is volatile and temporary. The data is cleared each time the bot is restarted. To use the in-memory storage for testing purposes, you will need to do two things. First, within the **Conversation.UpdateContainer** method, create a new instance of the in-memory storage:

```cs
Conversation.UpdateContainer(
            builder =>
            {
                var store = new InMemoryDataStore();
                ...
            });
```

Then, register the new data store:

```cs
Conversation.UpdateContainer(
            builder =>
            {
                var store = new InMemoryDataStore();
                builder.Register(c => store)
                          .Keyed<IBotDataStore<BotData>>(AzureModule.Key_DataStore)
                          .AsSelf()
                          .SingleInstance();
            });
```

You can use this method to set your own custom data storage or use any of the *Azure Extensions*.

## Manage custom data storage

For performance and security reasons in the production environment, you may implement your own data storage or consider implementing one of the following data storage options:

1. [Manage state data with Cosmos DB](bot-builder-dotnet-state-azure-cosmosdb.md)

2. [Manage state data with Table storage](bot-builder-dotnet-state-azure-table-storage.md)

With either of these [Azure Extensions](https://www.npmjs.com/package/botbuilder-azure) options, the mechanism for setting and persisting data via the Bot Framework SDK for .NET remains the same as the in-memory data storage.

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

##<a id="state-client"></a> Create a state client

The `StateClient` object enables you to manage state data using the Bot Builder SDK for .NET. 
If you have access to a message that belongs to the same context in which you want to manage state data, you can create a state client by calling the `GetStateClient` method on the `Activity` object.

[!code-csharp[Get State client](../includes/code/dotnet-state.cs#getStateClient1)]

If you do not have access to a message that belongs to the same context in which you want to manage state data, you can create a state client by simply creating a new instance of the `StateClient` class. In this example, `microsoftAppId` and `microsoftAppPassword` are the Bot Framework authentication credentials that you acquire for your bot during the [bot creation](../bot-service-quickstart.md) process.

> [!NOTE]
> To find your bot's **AppID** and **AppPassword**, see [MicrosoftAppID and MicrosoftAppPassword](~/bot-service-manage-overview.md#microsoftappid-and-microsoftapppassword).

[!code-csharp[Get State client](../includes/code/dotnet-state.cs#getStateClient2)]

> [!NOTE]
> The default state client is stored in a central service. For some channels, you may want to use a state API that is hosted within the channel itself, so that state data can be stored in a compliant store that the channel supplies.

## Get state data

Each of the "**Get...Data**" methods returns a `BotData` object that contains the state data for the specified user and/or conversation. To get a specific property value from a `BotData` object, call the `GetProperty` method. 

The following code example shows how to get a typed property from user data. 

[!code-csharp[Get state property](../includes/code/dotnet-state.cs#getProperty1)]

The following code example shows how to get a property from a complex type within user data.

[!code-csharp[Get state property](../includes/code/dotnet-state.cs#getProperty2)]

If no state data exists for the user and/or conversation that is specified for a "**Get...Data**" method call, 
the `BotData` object that is returned will contain these property values: 
- `BotData.Data` = null
- `BotData.ETag` = "*"

## Save state data

To save state data, first get the `BotData` object by calling the appropriate "**Get...Data**" method, 
then update it by calling the `SetProperty` method for each property you want to update, 
and save it by calling the appropriate "**Set...Data**" method. 

> [!NOTE]
> You may store up to 32 kilobytes of data for each user on a channel, each conversation on a channel, 
> and each user within the context of a conversation on a channel. 

The following code example shows how to save a typed property in user data.

[!code-csharp[Set state property](../includes/code/dotnet-state.cs#setProperty1)]

The following code example shows how to save a property in a complex type within user data. 

[!code-csharp[Set state property](../includes/code/dotnet-state.cs#setProperty2)]

## Handle concurrency issues

Your bot may receive an error response with HTTP status code **412 Precondition Failed** 
when it attempts to save state data, if another instance of the bot has changed the data. 
You can design your bot to account for this scenario, as shown in the following code example.

[!code-csharp[Handle exception saving state](../includes/code/dotnet-state.cs#handleException)]

## Additional resources

- [Bot Framework troubleshooting guide](../bot-service-troubleshoot-general-problems.md)
- <a href="/dotnet/api/?view=botbuilder-3.11.0" target="_blank">Bot Builder SDK for .NET Reference</a>

[Activity]: /dotnet/api/microsoft.bot.connector.activity

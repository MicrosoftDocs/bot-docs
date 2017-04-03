---
title: Manage state using the Bot Builder SDK for .NET | Microsoft Docs
description: Learn how to manage state using the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, state, user state, bot state, conversation state
author: kbrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/16/2017
ms.reviewer:
#ROBOTS: Index
---

# Manage state

[!include[State concept overview](~/includes/snippet-dotnet-concept-state.md)]  
This article describes how to manage state data using the State service via the Bot Builder SDK for .NET.

> [!IMPORTANT]
> If your bot uses [dialogs](~/dotnet/dialogs.md), 
> conversation state (the dialog stack and the state of each dialog in the stack) is automatically stored 
> using the Bot Framework State service. 

## Bot state methods

This table lists the methods within the `BotState` object of a [state client](#state-client) 
that you can use to manage state data.

| Method | Scoped to | Objective |                                                
|----|----|----|
| `GetUserData` | User | Get state data that has previously been saved for the user on the specified channel |
| `GetConversationData` | Conversation | Get state data that has previously been saved for the conversation on the specified channel |
| `GetPrivateConversationData` | User and Conversation | Get state data that has previously been saved for the user within the conversation on the specified channel |
| `SetUserData` | User | Save state data for the user on the specified channel |
| `SetConversationData` | Conversation | Save state data for the conversation on the specified channel |
| `SetPrivateConversationData` | User and Conversation | Save state data for the user within the conversation on the specified channel |
| `DeleteStateForUser` | User | Delete state data for the user on the specified channel. Your bot should call this method when it receives an activity of type [deleteUserData](~/dotnet/activities.md#deleteuserdata) or an activity of type [contactRelationUpdate](~/dotnet/activities.md#contactrelationupdate) that indicates the bot has been removed from the user's contact list. |

If your bot saves state data by using one of the "**Set...**" methods, 
future messages that your bot receives in the same context will contain that data, 
which your bot can access by using one of the "**Get...**" methods.

> [!NOTE]
> You may store up to 32 kilobytes of data 
> for each user on a channel, each conversation on a channel, and each user within the context of a conversation on a channel. 

##<a id="state-client"></a> Create a state client

The `StateClient` object enables you to manage state data using the Bot Builder SDK for .NET. 
To create a state client using an `Activity` object, call the `GetStateClient` method.

[!code-csharp[Get State client](~/includes/code/dotnet-state.cs#getStateClient1)]

If you do not have access to an `Activity` object, you can create a state client by simply 
creating a new instance of the `StateClient` class. In this example, `microsoftAppId` and `microsoftAppPassword` are the Bot Framework authentication 
credentials that you acquire for your bot during the [bot registration](~/portal-register-bot.md) 
process.

[!code-csharp[Get State client](~/includes/code/dotnet-state.cs#getStateClient2)]

## Get state data

Each of the "**Get...**" methods returns a `BotData` object that contains the state data for the specified user and/or conversation. 
To get a specific property value from a `BotData` object, call the `GetProperty` method. 

This code sample shows how to get a typed property from user data. 

[!code-csharp[Get state property](~/includes/code/dotnet-state.cs#getProperty1)]

This code sample shows how to get a property from a complex type within user data.

[!code-csharp[Get state property](~/includes/code/dotnet-state.cs#getProperty2)]

If no state data exists for the user and/or conversation that is specified in 
a "**Get...**" method call, 
the `BotData` object that is returned will contain these property values: 
- `BotData.Data` = null
- `BotData.ETag` = "*"

## Set state data

To update state data, first get the `BotData` object by calling the appropriate "**Get...**" method, 
then update it by calling the `SetProperty` method for each property you want to update, 
and save it by calling the appropriate "**Set...**" method. 

This code sample shows how to update a typed property in user data.

[!code-csharp[Set state property](~/includes/code/dotnet-state.cs#setProperty1)]

This code sample shows how to update a property in a complex type within user data. 

[!code-csharp[Set state property](~/includes/code/dotnet-state.cs#setProperty2)]

## Handle concurrency issues

Your bot may receive an error response with HTTP status code **412 Precondition Failed** 
when it attempts to save state data, if another instance of the bot has changed the data. 
You can design your bot to account for this scenario, as shown in this code sample.

[!code-csharp[Handle exception saving state](~/includes/code/dotnet-state.cs#handleException)]

## Additional resources

- [Bot Framework troubleshooting guide](~/bot-framework-troubleshooting-guide.md#state)
- <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/db/dbb/namespace_microsoft_1_1_bot_1_1_connector.html" target="_blank">Connector library</a>
---
title: Key concepts in the Bot Builder SDK for .NET | Microsoft Docs
description: Learn about key concepts in the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, key concepts, core concepts
author: kbrandl
manager: rstand
ms.topic: develop-dotnet-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 03/08/2017
ms.reviewer: rstand
#ROBOTS: Index
---

# Key concepts in the Bot Builder SDK for .NET

This article introduces key concepts in the Bot Builder SDK for .NET.

##<a id="connector"></a> Connector service

The Bot Framework Connector service provides a single API that enables a bot to 
communicate across multiple [channels](#channel) such as Skype, Email, Slack, and more. 
It facilitates communication between bot and user, 
by relaying messages from bot to channel and from channel to bot. 
For details about using the Connector service via the Bot Builder SDK for .NET, see [Connector service](bot-framework-dotnet-connector.md).

## State service

> [!NOTE]
> Content coming soon...

## Dialogs

> [!NOTE]
> Content coming soon...

For details about using dialogs within the Bot Builder SDK for .NET, see [Dialogs](bot-framework-dotnet-dialogs.md).

## FormFlow

> [!NOTE]
> Content coming soon...

For details about using FormFlow within the BotBuilder SDK for .NET, see [FormFlow](bot-framework-dotnet-formflow.md).

##<a id="activity"></a> Activity

The [Connector service](#connector) uses an **Activity** object to pass information back and forth between bot and channel (user). 
The most common type of activity is **message**, but there are other activity types that can be used to communicate 
various types of information to a bot or channel. 
The following table lists and describes the various activity types.

| Activity.Type | Interface | Description |
|------|------|------|
| message | IMessageActivity | Represents a communication between bot and user. |
| conversationUpdate | IConversationUpdateActivity | Indicates that the bot was added to a conversation or other conversation metadata has changed. |
| contactRelationUpdate | IContactRelationUpdateActivity | Indicates that the bot was added or removed from a user's contact list. |
| typing | IContactRelationUpdateActivity | Indicates that the user or bot on the other end of the conversation is compiling a response. | 
| ping | n/a | Represents an attempt to determine whether the bot is reachable. | 
| deleteUserData | n/a | Indicates to a bot that a user has requested that the bot delete any user data it may have stored. |

For details about sending and receiving activities by using the Connector service via Bot Builder SDK for .NET, 
see [Send and receive activities](bot-framework-dotnet-send-and-receive.md).

##<a id="channel"></a> Channel

> [!NOTE]
> Content coming soon...

## Naming conventions

The Bot Builder SDK for .NET library uses strongly-typed, pascal-cased naming conventions. 
However, the JSON messages that are transported back and forth over the wire use camel-case naming conventions. 
For example:

| C# property | JSON property |
|-----|-----|
| ReplyToId | replyToId | 

The following snippet shows an example of an Activity object serialized for transport over the wire.

```JSON
{
    "type": "message",
    "conversation": {
        "id": "GZxAXM39a6jdG0n2HQF5TEYL1vGgTG853w2259xn5VhGfs"
    },
    "timestamp": "2016-03-22T04:19:11.2100568Z",
    "channelid": "skype",
    "text": "You said:test",
    "attachments": [],
    "from": {
        "name": "Test Bot",
        "id": "MyTestBot",
    },
    "recipient": {
        "name": "tom",
        "id": "1hi3dbQ94Kddb",
    },
    "locale": "en-Us",
    "replyToId": "7TvTPn87HlZ",
    "entities": []
}
```

## Next steps

> [!NOTE]
> Content coming soon...





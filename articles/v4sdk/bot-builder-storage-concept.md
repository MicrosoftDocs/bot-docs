---
title: Save state and access data | Microsoft Docs
description: Describes what the state manager, conversation state and user state is within the Bot Builder SDK.
author: DeniseMak
ms.author: v-demak
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/15/2018
monikerRange: 'azure-bot-service-4.0'

---
# Save state and access data

A key to good bot design is to track the context of a conversation, so that your bot remembers things like the last question the user asked. A bot's *state* is information it remembers in order to respond appropriately to incoming messages. The Bot Builder SDK provides classes for storing and retrieving state data as a set of properties associated with a user or a conversation. These properties are saved as key-value pairs. 

* Conversation properties help your bot keep track of the current conversation the bot is having with the user. If your bot completes a sequence of steps or switches between conversation topics, you can use conversation properties to manage steps in a sequence or track the current topic. Since conversation properties reflect the state of the current conversation, you typically clear them at the end of a session, when the bot receives an `endConversation` activity.
* User properties can be used for many purposes, such as determining where the user's prior conversation left off or simply greeting a returning user by name. If you store a user's preferences, you can use that information to customize the conversation the next time you chat. For example, you might alert the user to a news article about a topic that interests her, or alert a user when an appointment becomes available.  

<!-- 
*Conversation state* pertains to the current conversation that the user is having with your bot. When the conversation ends, your bot deletes this data.

You can also store *user state* that persists after a conversation ends. For example, if you store a user's preferences, you can use that information to customize the conversation the next time you chat. For example, you might alert the user to a news article about a topic that interests her, or alert a user when an appointment becomes available. 
-->

<!-- You should generally avoid saving state using a global variable or function closures.
Doing so will create issues when you want to scale out your bot. Instead, --> 
## Conversation and user properties

The state property of your bot's context object provides a simple key-value store for accessing conversation and user state. This implementation provides an abstraction that lets you manage state information as key-value pairs, independent of where the data is actually stored. 

<!-- todo: replace with language-agnostic image or tabs -->
```
   context.State.ConversationProperties["welcomePromptCompleted"] = true;
   context.State.UserProperties["Name"] = "Patti Owens";
```
In order for the user and conversation properties to persist, your bot must set up state manager middleware. This middleware connects the key-value store for the properties to the storage medium, which could be a database, file, or memory.

<!-- todo: replace with language-agnostic image -->


# [C#](#tab/csharpmemorymiddleware)
```csharp
// initialize a bot adapter to save conversation state to memory storage
var botadapter = new Bot.Builder.Adapters.BotFrameworkAdapter("", "") // blank AppID and password
      .Use(new Bot.Builder.Middleware.ConversationStateManagerMiddleware(new MemoryStorage()));
```
# [JavaScript](#tab/jsmemorymiddleware)
```javascript
const bot = new Bot(adapter)
    .use(new MemoryStorage())
    .use(new ConversationStateManager())
```    
---

## Types of underlying storage

The SDK provides bot state manager middleware to persist conversation and user state, that can be accessed using the bot's context. This state manager can use Azure Table Storage, file storage, or memory storage as the underlying data storage.
You can also create your own storage components for your bot.

Bots built using Azure table storage can be designed to be stateless and scalable across multiple compute nodes.

> [!NOTE] 
> File and memory storage won't scale across nodes.

## Writing directly to storage

You can also use the BotBuilder SDK to write directly to storage, without using middleware. This enables you to read and write data outside the middleware pipeline or without using the bot context. This can be appropriate to data that your bot uses, that comes from a source outside your bot's conversation flow.

For example, let's say your bot allows the user to ask for the weather report, and your bot retrieves the weather report for a specified date, by reading it from an external database. The content of the weather database isn't dependent on user information or the conversation context, so just read it directly from storage instead of using the state manager.  See [How to write directly to storage](bot-builder-how-to-v4-storage.md) for an example.

## Creating your own custom storage middleware

You can create your own middleware to persist a set of properties to storage, similar to `ConversationProperties` and `UserProperties`. For example, a bot for a company might have `context.State.CompanyProperties` for access to company-wide information.


## Additional resources

- [How to save state (.NET)](bot-builder-how-to-v4-state.md)
- How to save state (Node.js)
- [How to write directly to storage (.NET)](bot-builder-how-to-v4-storage.md)
- How to write directly to storage (Node.js)

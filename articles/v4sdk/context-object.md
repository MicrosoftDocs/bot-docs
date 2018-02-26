---
title: context-object | Microsoft Docs
description: Structure and function of the context object
author: v-stbee
ms.author: v-stbee
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/23/2018
monikerRange: 'azure-bot-service-4.0'
---

# The context object
The bot adapter creates a **BotContext** object when it receives an activity. It attaches the activity to the context object and sends it through the pipelines. The context object contains information about the conversation, channels, user, request, responses, state, and intents needed to process the activity.

## Properties

### Adapter
*Adapter* refers to the singleton **BotFrameworkAdapter** object. The adapter controls the processing of each incoming activity and mediates communication between the user and the Bot middleware. The context object supplies information for communication with the adapter.

### ConversationReference
*ConversationReference* supplies information about the conversation with the user and lets you control aspects of that conversation. It allows you to get or set values for the activity ID, the adapter, the channel ID, the conversation reference, the service endpoint for operations on the conversation, and the participating user.

### Request
*Request* supplies information about the user's interaction with the bot. It supplies channel information, including channel ID, sender and recipient addresses, and channel-specific payload and metadata. It also supplies information about the activity, including the activity ID, activity type, the timestamp, the conversation address, activity metadata, the service URL for activity responses, and the original ID that the current activity is replying to.

### Responses
This is a list of responses in the conversation.

### State
*State* gives you a convenient mechanism for storing state data, either for the duration of a conversation or in a more persistent manner.

The context object itself is stateless, and exists only during the pipeline processing of the specific activity for which it was created. Conversation and user state can be persisted in the context object's State property. Each context object created in a series of activities can track state between the activities by accessing this *State* property.

The State property saves dynamically typed information to the location specified in the adapter singleton's **.Use()** directives in `Startup.cs`. The **UserStateManagerMiddleware** and **ConversationStateManagerMiddleware** objects specify the save location.

*State* includes two subfields: *ConversationProperties* and *UserProperties*. Each of these can be treated as an indexed container, where the index is a string constituting the name of a name/value pair:

```cs
contextObject.State.ConversationProperties["desiredLocation"] = "Hawaii";
contextObject.State.ConversationProperties["desiredYear"] = 2019;
contextObject.State.ConversationProperties["payInAdvance"] = true;

contextObject.State.UserProperties["name"] = "Bethany";
contextObject.State.UserProperties["age"] = 23;
contextObject.State.UserProperties["GPA"] = 3.76;
```

Use *ConversationProperties* for conversation-related information that you want to disappear at the end of the conversation, and use *UserProperties* for information that you want to persist past the current conversation. When the bot receives an *endOfConversation* activity, the conversation ends, and you should clear all data in *ConversationProperties*. Similarly, when the bot receives a *deleteUserData* activity, you should clear all user data in *UserProperties*.

### TopIntent
This gives you the highest-scoring intent, as determined by the recognizers your bot is using. It includes the intent's name and score, as well as a list of intent entities.

## Methods

### **Delay**
Introduces a processing delay (specified in milliseconds).

### **IfIntent**
Checks whether the *TopIntent* property matches the string you pass into it.

### **Reply**
Queues a new message response.

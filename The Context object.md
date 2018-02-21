---
title: The Context object | Microsoft Docs
description: Description of the structure and function of the Context object
author: v-stbee
ms.author: v-stbee
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/15/2018
monikerRange: 'azure-bot-service-4.0'
---

# The context object
When your **Bot** object receives an activity, it creates a **BotContext** object. The Bot then sends the activity along with this context object through the pipelines. The context object provides all the information about the conversation, its state, channels, user, request, response, and intents needed to process the activity.

The context object contains various members that provide information about the incoming activity, the user, the channel, the conversation, state information, and other data needed to process the activity.

## Properties

### Bot
This is a reference to the **Bot** object created in your **MessagesController** file. The context object supplies information for communication with this **Bot** object.

### ConversationReference
*ConversationReference* supplies information about the conversation with the user and lets you control aspects of that conversation. It allows you to get or set values for the activity ID, the participating Bot, the channel ID, the conversation reference, the service endpoint for operations on the conversation, and the participating user.

<!-- | ConversationReference property | Description |
| ------------- | ------------- |
| ActivityId | Sets or gets the ID of the specific activity object. |
| Bot | Sets or gets the **Bot** object participating in the conversation. |
| ChannelId | Sets or gets the channel ID. |
| Conversation | Sets or gets the conversation reference. |
| ServiceUrl | Sets or gets the service endpoint where operations concerning the referenced conversation can be performed. |
| User | Sets or gets the user participating in the conversation. | -->

### Request
*Request* supplies information about the user's interaction with the Bot. It supplies information about the channel, including:
- channel ID
- sender and recipient addresses
- channel metadata

*Request* also supplies information about the activity, including:
- activity type
- activity ID
- timestamp
- conversation address
- the original ID for which the current activity is a reply
- the service URL for activity responses
- activity metadata

### Responses
This is a list of responses in the conversation.

### State
The context object itself is stateless. It exists only during the pipeline processing of the specific activity for which it was created. Conversation and user state can be stored in the Context object's State property (a **BotState** object). The State property saves to the location specified in the **.Use(BotStateManager)** directive in the MessagesController.cs file. Each Context object created in a series of activities can track state by accessing the State property.

Quantities saved in state are dynamically typed.

The State property contains two subfields: Conversation (a **ConversationState** object) and User (a **UserState** object). Each of these, as well as the State property itself, can be treated as an indexed container, where the index is a string constituting the name of a name/value pair:

```cs
contextObject.State.Conversation["desiredLocation"] = "Hawaii";
contextObject.State.Conversation["desiredYear"] = 2019;
contextObject.State.Conversation["payInAdvance"] = true;

contextObject.State.User["name"] = "Bethany";
contextObject.State.User["age"] = 23;
contextObject.State.User["GPA"] = 3.76;
```
#### Context.State.Conversation vs. Context.State.User
TBD

### TemplateManager
This allows you to manage your own display templates.

### TopIntent
This gives you the highest-scoring intent, as determined by the **Recognizer**s your Bot is using.

## Methods

### **IfIntent**
**IfIntent** checks whether the *TopIntent* property matches the string you pass into it.

### **Reply** and **ReplyWith**
These methods queue a new message response. **ReplyWith** lets you bind your response data to a template ID.

### **ToBotContext**
TBD
---
title: Manage state data | Microsoft Docs
description: Learn how to save and retrieve state data with the Bot Framework SDK for Node.js.
author: DucVo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Manage state data

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-state.md)
> - [Node.js](../nodejs/bot-builder-nodejs-state.md)

[!INCLUDE [State concept overview](../includes/snippet-dotnet-concept-state.md)]

## In-memory data storage

In-memory data storage is intended for testing only. This storage is volatile and temporary. The data is cleared each time the bot is restarted. To use the in-memory storage for testing purposes, you will need to do two things. First create a new instance of the in-memory storage:

```javascript
var inMemoryStorage = new builder.MemoryBotStorage();
```

Then, set it to the bot when you create the **UniversalBot**:

```javascript
var inMemoryStorage = new builder.MemoryBotStorage();
var bot = new builder.UniversalBot(connector, [..waterfall steps..])
                    .set('storage', inMemoryStorage); // Register in-memory storage 
```

You can use this method to set your own custom data storage or use any of the *Azure Extensions*.

## Manage custom data storage

For performance and security reasons in the production environment, you may implement your own data storage or consider implementing one of the following data storage options:

1. [Manage state data with Cosmos DB](bot-builder-nodejs-state-azure-cosmosdb.md)

2. [Manage state data with Table storage](bot-builder-nodejs-state-azure-table-storage.md)

With either of these [Azure Extensions](https://www.npmjs.com/package/botbuilder-azure) options, the mechanism for setting and persisting data via the Bot Framework SDK for Node.js remains the same as the in-memory data storage.

## Storage containers

In the Bot Framework SDK for Node.js, the `session` object exposes the following properties for storing state data.

| Property | Scoped to | Description |
| ---- | ---- | ---- |
| [`userData`][userDataURL] | User | Contains data that is saved for the user on the specified channel. This data will persist across multiple conversations. |
| [`privateConversationData`][privateConversationDataURL] | Conversation | Contains data that is saved for the user within the context of a particular conversation on the specified channel. This data is private to the current user and will persist for the current conversation only. The property is cleared when the conversation ends or when `endConversation` is called explicitly. |
| [`conversationData`][conversationDataURL] | Conversation | Contains data that is saved in the context of a particular conversation on the specified channel. This data is shared with all users participating in the conversation and will persist for the current conversation only. The property is cleared when the conversation ends or when `endConversation` is called explicitly. |
| [`dialogData`][dialogDataURL] | Dialog | Contains data that is saved for the current dialog only. Each dialog maintains its own copy of this property. The property is cleared when the dialog is removed from the dialog stack. |

These four properties correspond to the four data storage containers that can be used to store data. Which properties you use to store data will depend upon the appropriate scope for the data you are storing, the nature of the data that you are storing, and how long you want the data to persist. For example, if you need to store user data that will be available across multiple conversations, consider using the `userData` property. If you need to temporarily store local variable values within the scope of a dialog, consider using the `dialogData` property. If you need to temporarily store data that must be accessible across multiple dialogs, consider using the `conversationData` property.

## Data Persistence

By default, data that is stored using the `userData`, `privateConversationData`, and `conversationData` properties is set to persist after the conversation ends. If you do not want the data to persist in the `userData` container, set the `persistUserData` flag to **false**. If you do not want the data to persist in the `conversationData` container, set the `persistConversationData` flag to **false**. 

```javascript
// Do not persist userData
bot.set(`persistUserData`, false);

// Do not persist conversationData
bot.set(`persistConversationData`, false);
```

> [!NOTE]
> You cannot disable data persistence for the `privateConversationData` container; it is always persisted.

## Set data

You can store simple JavaScript objects by saving them directly to a storage container. For a complex object like `Date`, consider converting it to `string`. This is because state data is serialized and stored as JSON. The following code samples show how to store primitive data, an array, an object map, and a complex `Date` object. 

**Store primitive data**

```javascript
session.userData.userName = "Kumar Sarma";
session.userData.userAge = 37;
session.userData.hasChildren = true;
```

**Store an array**

```javascript
session.userData.profile = ["Kumar Sarma", "37", "true"];
```

**Store an object map**

```javascript
session.userData.about = {
    "Profile": {
        "Name": "Kumar Sarma",
        "Age": 37,
        "hasChildren": true
    },
    "Job": {
        "Company": "Contoso",
        "StartDate": "June 8th, 2010",
        "Title": "Developer"
    }
}
```
**Store Date and Time** 

For a complex JavaScript object, convert it to a string before saving to storage container. 

```javascript 
var startDate = builder.EntityRecognizer.resolveTime([results.response]); 

// Date as string: "2017-08-23T05:00:00.000Z" 
session.userdata.start = startDate.toISOString(); 
``` 

### Saving data

Data that is created in each storage container will remain in memory until the container is saved. The Bot Framework SDK for Node.js sends data to the `ChatConnector` service in batches to be saved when there are messages to be sent. To save the data that exists in the storage containers without sending any messages, you can manually call the [`save`](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#save) method. If you do not call the `save` method, the data that exists in the storage containers will be persisted as part of the batch processing.

```javascript
session.userData.favoriteColor = "Red";
session.userData.about.job.Title = "Senior Developer"; 
session.save();
```

## Get data

To access the data that is saved in a particular storage container, simply reference the corresponding property. The following code samples show how to access data that was previously stored as primitive data, an array, an object map, and a complex Date object.

**Access primitive data**

```javascript
var userName = session.userData.userName;
var userAge = session.userData.userAge;
var hasChildren = session.userData.hasChildren;
```

**Access an array**

```javascript
var userProfile = session.userData.userProfile;

session.send("User Profile:");
for(int i = 0; i < userProfile.length, i++){
    session.send(userProfile[i]);
}
```

**Access an object map**

```javascript
var about = session.userData.about;

session.send("User %s works at %s.", about.Profile.Name, about.Job.Company);
```

**Access a Date object** 

Retrieve date data as string then convert it into a JavaScript's Date object. 

```javascript 
// startDate as a JavaScript Date object. 
var startDate = new Date(session.userdata.start); 
``` 

## Delete data

By default, data that is stored in the `dialogData` container is cleared when a dialog is removed from the dialog stack. Likewise, data that is stored in the `conversationData` container and `privateConversationData` container is cleared when the `endConversation` method is called. However, to delete data stored in the `userData` container, you have to explicitly clear it.

To explicitly clear the data that is stored in any of the storage containers, simply reset the container as shown in the following code sample. 

```javascript
// Clears data stored in container.
session.userData = {}; 
session.privateConversationData = {};
session.conversationData = {};
session.dialogData = {};
```

Never set a data container `null` or remove it from the `session` object, as doing so will cause errors the next time you try to access the container. Also, you may want to manually call `session.save();` after you manually clear a container in memory, to clear any corresponding data that has previously been persisted.

## Next steps

Now that you understand how to manage user state data, let's take a look at how you can use it to better manage conversation flow.

> [!div class="nextstepaction"]
> [Manage conversation flow](bot-builder-nodejs-dialog-manage-conversation-flow.md)

## Additional resources
- [Prompt user for input](bot-builder-nodejs-dialog-prompt.md)

[userDataURL]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#userdata
[conversationDataURL]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#conversationdata
[privateConversationDataURL]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#privateconversationdata
[dialogDataURL]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#dialogdata

[ChatConnector]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.chatconnector.html

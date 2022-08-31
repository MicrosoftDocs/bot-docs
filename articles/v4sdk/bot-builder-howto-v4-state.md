---
title: Save user and conversation data
description: Learn how the Bot Framework SDK manages user and conversation data (state). See how to set up storage for this data, read it, and write it. 
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 08/30/2022
monikerRange: 'azure-bot-service-4.0'
---

# Save user and conversation data

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

A bot is inherently stateless. Once your bot is deployed, it may not run in the same process or on the same machine from one turn to the next. However, your bot may need to track the context of a conversation so that it can manage its behavior and remember answers to previous questions. The state and storage features of the Bot Framework SDK allow you to add state to your bot. Bots use state management and storage objects to manage and persist state. The state manager provides an abstraction layer that lets you access state properties using property accessors, independent of the type of underlying storage.

## Prerequisites

- Knowledge of [bot basics](bot-builder-basics.md) and how bots [manage state](bot-builder-concept-state.md) is required.
- The code in this article is based on the **State Management Bot sample**. You'll need a copy of the sample in either [C#][cs-sample], [JavaScript][js-sample], [Java][java-sample], or [Python][py-sample].

[cs-sample]: https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/45.state-management
[js-sample]: https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/45.state-management
[java-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/45.state-management
[py-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/45.state-management

## About this sample

Upon receiving user input, this sample checks the stored conversation state to see if this user has previously been prompted to provide their name. If not, the user's name is requested and that input is stored within user state. If so, the name stored within user state is used to converse with the user and their input data, along with the time received and input channel ID, is returned back to the user. The time and channel ID values are retrieved from the user conversation data and then saved to conversation state. The following diagram shows the relationship between the bot, user profile, and conversation data classes.

## [C#](#tab/csharp)

:::image type="content" source="media/StateBotSample-Overview.png" alt-text="Class diagram outlining the structure of the C# sample.":::

## [JavaScript](#tab/javascript)

:::image type="content" source="media/StateBotSample-JS-Overview.png" alt-text="Class diagram outlining the structure of the JavaScript sample.":::

## [Java](#tab/java)

:::image type="content" source="media/StateBotSample-Overview.png" alt-text="Class diagram outlining the structure of the Java sample.":::

## [Python](#tab/python)

:::image type="content" source="media/StateBotSample-Python-Overview.png" alt-text="Class diagram outlining the structure of the Python sample.":::

---

## Define classes

## [C#](#tab/csharp)

The first step in setting up state management is to define the classes containing the information to manage in the user and conversation state. The example used in this article, defines the following classes:

- In **UserProfile.cs**, you define a `UserProfile` class for the user information that the bot will collect.
- In **ConversationData.cs**, you define a `ConversationData` class to control our conversation state while gathering user information.

The following code examples show the definitions for the `UserProfile` and `ConversationData` classes.

**UserProfile.cs**

[!code-csharp[UserProfile class](~/../BotBuilder-Samples/samples/csharp_dotnetcore/45.state-management/UserProfile.cs?range=7-10)]

**ConversationData.cs**

[!code-csharp[ConversationData class](~/../BotBuilder-Samples/samples/csharp_dotnetcore/45.state-management/ConversationData.cs?range=7-17)]

## [JavaScript](#tab/javascript)

This step isn't necessary in JavaScript.

## [Java](#tab/java)

The first step in setting up state management is to define the classes containing the information to manage in the user and conversation state. The example used in this article defines the following classes:

- In **UserProfile.java**, you define a `UserProfile` class for the user information that the bot will collect.
- In **ConversationData.java**, you define a `ConversationData` class to control our conversation state while gathering user information.

The following code examples show the definitions for the `UserProfile` and `ConversationData` classes.

**UserProfile.java**

[!code-java[UserProfile class](~/../BotBuilder-Samples/samples/java_springboot/45.state-management/src/main/java/com/microsoft/bot/sample/statemanagement/UserProfile.java?range=18-28)]

**ConversationData.java**

[!code-java[ConversationData class](~/../BotBuilder-Samples/samples/java_springboot/45.state-management/src/main/java/com/microsoft/bot/sample/statemanagement/ConversationData.java?range=18-46)]

## [Python](#tab/python)

The first step in setting up state management is to define the classes containing the information to manage in the user and conversation state. The example used in this article, defines the following classes:

- The **user_profile.py** contains the `UserProfile` class that stores the user information collected by the bot.
- The **conversation_data.py** contains the `ConversationData` class that controls the conversation state while gathering user information.

The following code examples show the definitions for the `UserProfile` and `ConversationData` classes.

**user_profile.py**

[!code-python[user_profile](~/../botbuilder-samples/samples/python/45.state-management/data_models/user_profile.py?range=5-7)]

**conversation_data.py**

[!code-python[conversation_data](~/../botbuilder-samples/samples/python/45.state-management/data_models/conversation_data.py?range=5-14)]

---

## Create conversation and user state objects

## [C#](#tab/csharp)

Next, you register `MemoryStorage` that is used to create `UserState` and `ConversationState` objects. The user and conversation state objects are created at `Startup` and dependency injected into the bot constructor. Other services for a bot that are registered are: a credential provider, an adapter, and the bot implementation.

**Startup.cs**

[!code-csharp[MemoryStorage, UserState, and ConversationState definitions](~/../BotBuilder-Samples/samples/csharp_dotnetcore/45.state-management/Startup.cs?range=34-35,55-61)]

**Bots/StateManagementBot.cs**

[!code-csharp[Bot constructor](~/../BotBuilder-Samples/samples/csharp_dotnetcore/45.state-management/Bots/StateManagementBot.cs?range=15-22)]

## [JavaScript](#tab/javascript)

Next, you register `MemoryStorage` that is then used to create `UserState` and `ConversationState` objects. These are created in **index.js** and consumed when the bot is created.

**index.js**

[!code-javascript[MemoryStorage, UserState, ConversationState, and bot objects](~/../BotBuilder-Samples/samples/javascript_nodejs/45.state-management/index.js?range=43-50)]

**bots/stateManagementBot.js**

[!code-javascript[state properties and constructor](~/../BotBuilder-Samples/samples/javascript_nodejs/45.state-management/bots/stateManagementBot.js?range=7-19)]

## [Java](#tab/java)

Next, you register the `StateManagementBot` in Application.java. Both ConversationState and UserState are provided by default from the BotDependencyConfiguration class, and Spring will inject them into the getBot method.

**Application.java**

[!code-java[getBot method](~/../BotBuilder-Samples/samples/java_springboot/45.state-management/src/main/java/com/microsoft/bot/sample/statemanagement/Application.java?range=54-60)]

## [Python](#tab/python)

Next, you register `MemoryStorage` that is used to create `UserState` and `ConversationState` objects. These are created in **app.py** and consumed when the bot is created.

**app.py**

[!code-python[MemoryStorage, UserState, ConversationState, and bot objects](~/../botbuilder-samples/samples/python/45.state-management/app.py?range=68-74)]

**bots/state_management_bot.py**

[!code-python[bot constructor](~/../botbuilder-samples/samples/python/45.state-management/bots/state_management_bot.py?range=14-30)]

---

## Add state property accessors

## [C#](#tab/csharp)

Now you create property accessors using the `CreateProperty` method that provides a handle to the `BotState` object. Each state property accessor allows you to get or set the value of the associated state property. Before you use the state properties, use each accessor to load the property from storage and get it from the state cache. To get the properly scoped key associated with the state property, you call the `GetAsync` method.

**Bots/StateManagementBot.cs**

[!code-csharp[Create accessors](~/../BotBuilder-Samples/samples/csharp_dotnetcore/45.state-management/Bots/StateManagementBot.cs?range=42,45)]

## [JavaScript](#tab/javascript)

Now you create property accessors for `UserState` and `ConversationState`. Each state property accessor allows you to get or set the value of the associated state property. You use each accessor to load the associated property from storage and retrieve its current state from cache.

**bots/stateManagementBot.js**

[!code-javascript[Create accessors](~/../BotBuilder-Samples/samples/javascript_nodejs/45.state-management/bots/stateManagementBot.js?range=13-15)]

## [Java](#tab/java)

Now you create property accessors using the `createProperty` method. Each state property accessor allows you to get or set the value of the associated state property. Before you use the state properties, use each accessor to load the property from storage and get it from the state cache. To get the properly scoped key associated with the state property, you call the `get` method.

**StateManagementBot.java**

[!code-java[Create property accessors](~/../BotBuilder-Samples/samples/java_springboot/45.state-management/src/main/java/com/microsoft/bot/sample/statemanagement/StateManagementBot.java?range=105-108)]

## [Python](#tab/python)

Now you create property accessors for `UserProfile` and `ConversationData`. Each state property accessor allows you to get or set the value of the associated state property. You use each accessor to load the associated property from storage and retrieve its current state from cache.

**bots/state_management_bot.py**

[!code-python[Create accessors](~/../botbuilder-samples/samples/python/45.state-management/bots/state_management_bot.py?range=27-30)]

---

## Access state from your bot

The preceding section covers the initialization-time steps to add state property accessors to our bot. Now, you can use those accessors at run-time to read and write state information. The sample code below uses the following logic flow:

## [C#](#tab/csharp)

- If `userProfile.Name` is empty and `conversationData.PromptedUserForName` is _true_, you retrieve the user name provided and store this within user state.
- If `userProfile.Name` is empty and `conversationData.PromptedUserForName` is _false_, you ask for the user's name.
- If `userProfile.Name` was previously stored, you retrieve message time and channel ID from the user input, echo all data back to the user, and store the retrieved data within conversation state.

**Bots/StateManagementBot.cs**

[!code-csharp[OnMessageActivityAsync](~/../BotBuilder-Samples/samples/csharp_dotnetcore/45.state-management/bots/StateManagementBot.cs?range=38-85)]

Before you exit the turn handler, you use the state management objects' _SaveChangesAsync()_ method to write all state changes back to storage.

**Bots/StateManagementBot.cs**

[!code-csharp[OnTurnAsync](~/../BotBuilder-Samples/samples/csharp_dotnetcore/45.state-management/bots/StateManagementBot.cs?range=24-31)]

## [JavaScript](#tab/javascript)

- If `userProfile.Name` is empty and `conversationData.PromptedUserForName` is _true_, you retrieve the user name provided and store this within user state.
- If `userProfile.Name` is empty and `conversationData.PromptedUserForName` is _false_, you ask for the user's name.
- If `userProfile.Name` was previously stored, you retrieve message time and channel ID from the user input, echo all data back to the user, and store the retrieved data within conversation state.

**bots/stateManagementBot.js**

[!code-javascript[OnMessage](~/../BotBuilder-Samples/samples/javascript_nodejs/45.state-management/bots/stateManagementBot.js?range=21-58)]

Before you exit each dialog turn, you use the state management objects' _saveChanges()_ method to persist all changes by writing state back out to storage.

**bots/stateManagementBot.js**

[!code-javascript[OnDialog](~/../BotBuilder-Samples/samples/javascript_nodejs/45.state-management/bots/stateManagementBot.js?range=72-81)]

## [Java](#tab/java)

- If `userProfile.getName()` is empty and `conversationData.getPromptedUserForName()` is _true_, you retrieve the user name provided and store this within user state.
- If `userProfile.getName()` is empty and `conversationData.getPromptedUserForName()` is _false_, you ask for the user's name.
- If `userProfile.getName()` was previously stored, you retrieve message time and channel ID from the user input, echo all data back to the user, and store the retrieved data within conversation state.

**StateManagementBot.java**

[!code-java[onMessageActivity method](~/../BotBuilder-Samples/samples/java_springboot/45.state-management/src/main/java/com/microsoft/bot/sample/statemanagement/StateManagementBot.java?range=102-168)]

Before you exit the turn handler, you use the state management objects' _saveChanges()_ method to write all state changes back to storage.

**StateManagementBot.java**

[!code-java[onTurn method](~/../BotBuilder-Samples/samples/java_springboot/45.state-management/src/main/java/com/microsoft/bot/sample/statemanagement/StateManagementBot.java?range=58-64)]

## [Python](#tab/python)

- If `user_profile.name` is empty and `conversation_data.prompted_for_user_name` is _true_, the bot retrieves the name provided by the user and stores it in the user's state.
- If `user_profile.name` is empty and `conversation_data.prompted_for_user_name` is _false_, the bot asks for the user's name.
- If `user_profile.name` was previously stored, the bot retrieves message time and channel ID from the user input, echoes the data back to the user, and stores the retrieved data in the conversation state.

**bots/state_management_bot.py**

[!code-python[on_message_activity](~/../botbuilder-samples/samples/python/45.state-management/bots/state_management_bot.py?range=47-89)]

Before each dialog turn ends, the bot uses the state management objects' `save_changes` method to persist all changes by writing state information in the storage.

**bots/state_management_bot.py**

[!code-python[on_turn](~/../botbuilder-samples/samples/python/45.state-management/bots/state_management_bot.py?range=32-36)]

---

## Test your bot

1. Download and install the latest [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md)
1. Run the sample locally on your machine.
   If you need instructions, refer to the README for [C#][cs-sample], [JavaScript][js-sample], [Java][java-sample], or [Python][py-sample].
1. Use the Emulator to test your sample bot.

## Additional information

This article described how you can add state to your bot. See the following table for more information about related topics.

| Topic | Notes |
|:-|:-|
| Privacy | If you intend to store user's personal data, you should ensure compliance with [General Data Protection Regulation](https://blog.botframework.com/2018/04/23/general-data-protection-regulation-gdpr). |
| State management | All of the state management calls are asynchronous, and last-writer-wins by default. In practice, you should get, set, and save state as close together in your bot as possible. For a discussion of how to implement optimistic locking, see [Implement custom storage for your bot](bot-builder-custom-storage.md). |
| Critical business data | Use bot state to store preferences, user name, or the last thing they ordered, but don't use it to store critical business data. For critical data, [create your own storage components](bot-builder-custom-storage.md) or write directly to [storage](bot-builder-howto-v4-storage.md). |
| Recognizer-Text | The sample uses the Microsoft/Recognizers-Text libraries to parse and validate user input. For more information, see the [overview](https://github.com/Microsoft/Recognizers-Text#microsoft-recognizers-text-overview) page. |

## Next steps

Learn how to ask the user a series of questions, validate their answers, and save their input.

> [!div class="nextstepaction"]
> [Create your own prompts to gather user input](bot-builder-primitive-prompts.md).

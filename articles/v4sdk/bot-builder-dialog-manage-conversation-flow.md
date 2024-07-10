---
title: Implement sequential conversation flow
description: Learn how to manage linear conversation flow with dialogs in the Bot Framework SDK.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 10/26/2022
monikerRange: 'azure-bot-service-4.0'
ms.custom:
  - evergreen
---

# Implement sequential conversation flow

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Gathering information by posing questions is one of the main ways a bot interacts with users. The dialogs library provides useful built-in features such as _prompt_ classes that make it easy to ask questions and validate the response to make sure it matches a specific data type or meets custom validation rules.

You can manage linear and more complex conversation flows using the dialogs library. In a linear interaction, the bot runs through a fixed sequence of steps, and the conversation finishes. A dialog is useful when the bot needs to gather information from the user.

This article shows how to implement linear conversation flow by creating prompts and calling them from a waterfall dialog.
For examples of how to write your own prompts without using the dialogs library, see the [Create your own prompts to gather user input](bot-builder-primitive-prompts.md) article.

[!INCLUDE [java-python-sunset-alert](../includes/java-python-sunset-alert.md)]

## Prerequisites

- Knowledge of [bot basics][concept-basics], [managing state][concept-state], and the [dialogs library][concept-dialogs].
- A copy of the **Multi turn prompts** sample in [C#][cs-sample], [JavaScript][js-sample], [Java][java-sample], or [Python][python-sample].

## About this sample

The multi-turn prompts sample uses a waterfall dialog, a few prompts, and a component dialog to create a linear interaction that asks the user a series of questions. The code uses a dialog to cycle through these steps:

| Steps        | Prompt type  |
|:-------------|:-------------|
| Ask the user for their mode of transportation | Choice prompt |
| Ask the user for their name | Text prompt |
| Ask the user if they want to provide their age | Confirm prompt |
| If they answered yes, ask for their age | Number prompt, with validation to only accept ages greater than 0 and less than 150 |
| If they're not using Microsoft Teams, ask them for a profile picture | Attachment prompt, with validation to allow a missing attachment |
| Ask if the collected information is "ok" | Reuse Confirm prompt |

Finally, if they answered yes, display the collected information; otherwise, tell the user that their information won't be kept.

## Create the main dialog

# [C#](#tab/csharp)

To use dialogs, install the **Microsoft.Bot.Builder.Dialogs** NuGet package.

The bot interacts with the user via `UserProfileDialog`. When creating the bot's `DialogBot` class, the `UserProfileDialog` is set as its main dialog. The bot then uses a `Run` helper method to access the dialog.

:::image type="content" source="media/user-profile-dialog.png" alt-text="Class diagram for the C# sample.":::

**Dialogs\UserProfileDialog.cs**

Begin by creating the `UserProfileDialog` that derives from the `ComponentDialog` class, and has seven steps.

In the `UserProfileDialog` constructor, create the waterfall steps, prompts and the waterfall dialog, and add them to the dialog set. The prompts need to be in the same dialog set in which they're used.

[!code-csharp[Constructor snippet](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=20-47)]

Next, add the steps that the dialog uses to prompt for input. To use a prompt, call it from a step in your dialog and retrieve the prompt result in the following step using `stepContext.Result`. Behind the scenes, prompts are a two-step dialog. First, the prompt asks for input. Then it returns the valid value, or starts over from the beginning with a reprompt until it receives a valid input.

You should always return a non-null `DialogTurnResult` from a waterfall step. If you don't, your dialog may not work as designed. Shown below is the implementation for `NameStepAsync` in the waterfall dialog.

[!code-csharp[Name step](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=61-66)]

In `AgeStepAsync`, specify a retry prompt for when the user's input fails to validate, either because it's in a format that the prompt can't parse, or the input fails a validation criteria. In this case, if no retry prompt was provided, the prompt will use the initial prompt text to reprompt the user for input.

[!code-csharp[Age step](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=79-98&highlight=10)]

**UserProfile.cs**

The user's mode of transportation, name, and age are saved in an instance of the `UserProfile` class.

[!code-csharp[UserProfile class](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/UserProfile.cs?range=11-20)]

**Dialogs\UserProfileDialog.cs**

In the last step, check the `stepContext.Result` returned by the dialog called in the previous waterfall step. If the return value is true, the user profile accessor gets and updates the user profile. To get the user profile, call `GetAsync` and then set the values of the `userProfile.Transport`, `userProfile.Name`, `userProfile.Age` and `userProfile.Picture` properties. Finally, summarize the information for the user before calling `EndDialogAsync`, which ends the dialog. Ending the dialog pops it off the dialog stack and returns an optional result to the dialog's parent. The parent is the dialog or method that started the dialog that just ended.

[!code-csharp[SummaryStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=136-178&highlight=5-11,41-42)]

# [JavaScript](#tab/javascript)

To use dialogs, your project needs to install the **botbuilder-dialogs** npm package.

The bot interacts with the user via a `UserProfileDialog`. When creating the bot's `DialogBot`, the `UserProfileDialog` is set as its main dialog. The bot then uses a `run` helper method to access the dialog.

:::image type="content" source="media/user-profile-dialog-js.png" alt-text="Class diagram for the JavaScript sample.":::

**dialogs/userProfileDialog.js**

Begin by creating the `UserProfileDialog` that derives from the `ComponentDialog` class, and has seven steps.

In the `UserProfileDialog` constructor, create the waterfall steps, prompts and the waterfall dialog, and add them to the dialog set. The prompts need to be in the same dialog set in which they're used.

[!code-javascript[Constructor snippet](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/dialogs/userProfileDialog.js?range=29-51)]

Next, add the steps that the dialog uses to prompt for input. To use a prompt, call it from a step in your dialog and retrieve the prompt result in the following step from the step context, in this case by using `step.result`. Behind the scenes, prompts are a two-step dialog. First, the prompt asks for input. Then it returns the valid value, or starts over from the beginning with a reprompt until it receives a valid input.

You should always return a non-null `DialogTurnResult` from a waterfall step. If you don't, your dialog may not work as designed. Shown below is the implementation for the `nameStep` in the waterfall dialog.

[!code-javascript[name step](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/dialogs/userProfileDialog.js?range=79-82)]

In `ageStep`, specify a retry prompt for when the user's input fails to validate, either because it's in a format that the prompt can't parse, or the input fails a validation criteria, specified in the constructor above. In this case, if no retry prompt was provided, the prompt will use the initial prompt text to reprompt the user for input.

[!code-javascript[age step](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/dialogs/userProfileDialog.js?range=94-105&highlight=5)]

**userProfile.js**

The user's mode of transportation, name, and age are saved in an instance of the `UserProfile` class.

[!code-javascript[user profile](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/userProfile.js?range=4-11)]

**dialogs/userProfileDialog.js**

In the last step, check the `step.result` returned by the dialog called in the previous waterfall step. If the return value is true, the user profile accessor gets and updates the user profile. To get the user profile, call `get`, and then set the values of the `userProfile.transport`, `userProfile.name`, `userProfile.age` and `userProfile.picture` properties. Finally, summarize the information for the user before calling `endDialog`, which ends the dialog. Ending the dialog pops it off the dialog stack and returns an optional result to the dialog's parent. The parent is the dialog or method that started the dialog that just ended.

[!code-javascript[summary step](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/dialogs/userProfileDialog.js?range=137-167&highlight=3-9,29-30)]

**Create the extension method to run the waterfall dialog**

A `run` helper method, defined inside `userProfileDialog`, is used to create and access the dialog context. Here, `accessor` is the state property accessor for the dialog state property, and `this` is the user profile component dialog. Since component dialogs define an inner dialog set, an outer dialog set must be created that's visible to the message handler code and used to create a dialog context.

The dialog context is created by calling the `createContext` method, and is used to interact with the dialog set from within the bot's turn handler. The dialog context includes the current turn context, the parent dialog, and the dialog state, which provides a method for preserving information within the dialog.

The dialog context allows you to start a dialog with the string ID, or continue the current dialog (like a waterfall dialog that has multiple steps). The dialog context is passed through to all the bot's dialogs and waterfall steps.

[!code-javascript[run method](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/dialogs/userProfileDialog.js?range=59-68)]

# [Java](#tab/java)

The bot interacts with the user via `UserProfileDialog`. When creating the bot's `DialogBot` class, the `UserProfileDialog` is set as its main dialog. The bot then uses a `Run` helper method to access the dialog.

:::image type="content" source="media/user-profile-dialog-java.png" alt-text="Class diagram for the Java sample.":::

**UserProfileDialog.java**

Begin by creating the `UserProfileDialog` that derives from the `ComponentDialog` class, and has seven steps.

In the `UserProfileDialog` constructor, create the waterfall steps, prompts and the waterfall dialog, and add them to the dialog set. The prompts need to be in the same dialog set in which they're used.

[!code-java[Constructor snippet](~/../botbuilder-samples/samples/java_springboot/05.multi-turn-prompt/src/main/java/com/microsoft/bot/sample/multiturnprompt/UserProfileDialog.java?range=34-59)]

Next, add the steps that the dialog uses to prompt for input. To use a prompt, call it from a step in your dialog and retrieve the prompt result in the following step using `stepContext.getResult()`. Behind the scenes, prompts are a two-step dialog. First, the prompt asks for input. Then it returns the valid value, or starts over from the beginning with a reprompt until it receives a valid input.

You should always return a non-null `DialogTurnResult` from a waterfall step. If you don't, your dialog may not work as designed. Shown below is the implementation for `nameStep` in the waterfall dialog.

[!code-java[Name step](~/../botbuilder-samples/samples/java_springboot/05.multi-turn-prompt/src/main/java/com/microsoft/bot/sample/multiturnprompt/UserProfileDialog.java?range=71-77)]

In `ageStep`, specify a retry prompt for when the user's input fails to validate, either because it's in a format that the prompt can't parse, or the input fails a validation criteria. In this case, if no retry prompt was provided, the prompt will use the initial prompt text to reprompt the user for input.

[!code-java[Age step](~/../botbuilder-samples/samples/java_springboot/05.multi-turn-prompt/src/main/java/com/microsoft/bot/sample/multiturnprompt/UserProfileDialog.java?range=92-105&highlight=7)]

**UserProfile.java**

The user's mode of transportation, name, and age are saved in an instance of the `UserProfile` class.

[!code-java[UserProfile class](~/../botbuilder-samples/samples/java_springboot/05.multi-turn-prompt/src/main/java/com/microsoft/bot/sample/multiturnprompt/UserProfile.java?range=8=16)]

**UserProfileDialog.java**

In the last step, check the `stepContext.Result` returned by the dialog called in the previous waterfall step. If the return value is true, the user profile accessor gets and updates the user profile. To get the user profile, call `get` and then set the values of the `userProfile.Transport`, `userProfile.Name`, `userProfile.Age` and `userProfile.Picture` properties. Finally, summarize the information for the user before calling `endDialog`, which ends the dialog. Ending the dialog pops it off the dialog stack and returns an optional result to the dialog's parent. The parent is the dialog or method that started the dialog that just ended.

[!code-java[SummaryStep](~/../botbuilder-samples/samples/java_springboot/05.multi-turn-prompt/src/main/java/com/microsoft/bot/sample/multiturnprompt/UserProfileDialog.java??range=142-191&highlight=3-8,43)]

# [Python](#tab/python)

To use dialogs, install the **botbuilder-dialogs** and **botbuilder-ai** PyPI packages by running `pip install botbuilder-dialogs` and `pip install botbuilder-ai` from a terminal.

The bot interacts with the user via `UserProfileDialog`. When the bot's `DialogBot` class is created, the `UserProfileDialog` is set as its main dialog. The bot then uses a `run_dialog` helper method to access the dialog.

:::image type="content" source="media/user-profile-dialog-python.png" alt-text="Class diagram for the Python sample.":::

**dialogs\user_profile_dialog.py**

Begin by creating the `UserProfileDialog` that derives from the `ComponentDialog` class, and has seven steps.

In the `UserProfileDialog` constructor, create the waterfall steps, prompts and the waterfall dialog, and add them to the dialog set. The prompts need to be in the same dialog set in which they're used.

[!code-python[Constructor snippet](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/dialogs/user_profile_dialog.py?range=26-57)]

Next, add the steps that the dialog uses to prompt for input. To use a prompt, call it from a step in your dialog and retrieve the prompt result in the following step using `step_context.result`. Behind the scenes, prompts are a two-step dialog. First, the prompt asks for input. Then it returns the valid value, or starts over from the beginning with a reprompt until it receives a valid input.

You should always return a non-null `DialogTurnResult` from a waterfall step. If you don't, your dialog may not work as designed. Here you can see the implementation for the `name_step` in the waterfall dialog.

[!code-python[name step](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/dialogs/user_profile_dialog.py?range=73-79)]

In `age_step`, specify a retry prompt for when the user's input fails to validate, either because it's in a format that the prompt can't parse, or the input fails a validation criteria, specified in the constructor above. In this case, if no retry prompt was provided, the prompt will use the initial prompt text to reprompt the user for input

[!code-python[age step](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/dialogs/user_profile_dialog.py?range=100-116)]

**data_models\user_profile.py**

The user's mode of transportation, name, and age are saved in an instance of the `UserProfile` class.

[!code-python[user profile](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/data_models/user_profile.py?range=7-16)]

**dialogs\user_profile_dialog.py**

In the last step, check the `step_context.result` returned by the dialog called in the previous waterfall step. If the return value is true, the user profile accessor gets and updates the user profile. To get the user profile, call `get`, and then set the values of the `user_profile.transport`, `user_profile.name`, and `user_profile.age` properties. Finally, summarize the information for the user before calling `end_dialog`, which ends the dialog. Ending the dialog pops it off the dialog stack and returns an optional result to the dialog's parent. The parent is the dialog or method that started the dialog that just ended.

[!code-python[summary step](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/dialogs/user_profile_dialog.py?range=166-204)]

**Create the extension method to run the waterfall dialog**

A `run_dialog()` helper method is defined in **helpers\dialog_helper.py** that's used to create and access the dialog context. Here, `accessor` is the state property accessor for the dialog state property, and `dialog` is the user profile component dialog. Since component dialogs define an inner dialog set, an outer dialog set must be created that's visible to the message handler code and use that to create a dialog context.

Create the dialog context by calling the `create_context`, which is used to interact with the dialog set from within the bot's turn handler. The dialog context includes the current turn context, the parent dialog, and the dialog state, which provides a method for preserving information within the dialog.

The dialog context allows you to start a dialog with the string ID, or continue the current dialog (such as a waterfall dialog that has multiple steps). The dialog context is passed through to all the bot's dialogs and waterfall steps.

[!code-python[run method](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/helpers/dialog_helper.py?range=8-19)]

---

## Run the dialog

# [C#](#tab/csharp)

**Bots\DialogBot.cs**

The `OnMessageActivityAsync` handler uses the `RunAsync` method to start or continue the dialog. `OnTurnAsync` uses the bot's state management objects to persist any state changes to storage. The `ActivityHandler.OnTurnAsync` method calls the various activity handler methods, such as `OnMessageActivityAsync`. In this way, the state is saved after the message handler completes but before the turn itself completes.

[!code-csharp[overrides](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Bots/DialogBot.cs?range=33-48&highlight=5-7)]

# [JavaScript](#tab/javascript)

The `onMessage` method registers a listener that calls the dialog's `run` method to start or continue the dialog.

Separately, the bot overrides the `ActivityHandler.run` method to save conversation and user state to storage. In this way, the state is saved after the message handler completes but before the turn itself completes.

**bots/dialogBot.js**

[!code-javascript[message listener](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/bots/dialogBot.js?range=24-31&highlight=5)]

[!code-javascript[override](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/bots/dialogBot.js?range=34-43&highlight=7-9)]

# [Java](#tab/java)

**DialogBot.java**

The `onMessageActivity` handler uses the `run` method to start or continue the dialog. `onTurn` uses the bot's state management objects to persist any state changes to storage. The `ActivityHandler.onTurn` method calls the various activity handler methods, such as `onMessageActivity`. In this way, the state is saved after the message handler completes but before the turn itself completes.

[!code-java[overrides](~/../botbuilder-samples/samples/java_springboot/05.multi-turn-prompt/src/main/java/com/microsoft/bot/sample/multiturnprompt/DialogBot.java?range=40-58&highlight=6-8)]

# [Python](#tab/python)

The `on_message_activity` handler uses the helper method to start or continue the dialog. The `on_turn` method uses the bot's state management objects to persist any state changes to storage. The `on_message_activity` method gets called last after other defined handlers are run, such as `on_turn`. In this way, the state is saved after the message handler completes but before the turn itself completes.

**bots\dialog_bot.py**

[!code-python[overrides](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/bots/dialog_bot.py?range=39-51&highlight=4-6)]

---

## Register services for the bot

This bot uses the following services:

- Basic services for a bot: a credential provider, an adapter, and the bot implementation.
- Services for managing state: storage, user state, and conversation state.
- The dialog the bot will use.

# [C#](#tab/csharp)

**Startup.cs**

Register services for the bot in `Startup`. These services are available to other parts of the code through dependency injection.

[!code-csharp[ConfigureServices](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Startup.cs?range=15-37)]

# [JavaScript](#tab/javascript)

**index.js**

Register services for the bot in `index.js`.

[!code-javascript[Create adapter, memory, state, dialog, and bot](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/index.js?range=35-73)]

# [Java](#tab/java)

**Application.java**

Spring will provide the ConversationState, UserState, and Dialog via dependency injection. Override the getBot and return an instance of the DialogBot.

[!code-java[ConfigureServices](~/../botbuilder-samples/samples/java_springboot/05.multi-turn-prompt/src/main/java/com/microsoft/bot/sample/multiturnprompt/Application.java?range=52-59)]

# [Python](#tab/python)

Register services for the bot in `app.py`.

[!code-python[configure services](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/app.py?range=28-77)]

---

> [!NOTE]
> Memory storage is used for testing purposes only and isn't intended for production use.
> Be sure to use a persistent type of storage for a production bot.

## Test your bot

1. If you haven't done so already, install the [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md).
1. Run the sample locally on your machine.
1. Start the Emulator, connect to your bot, and send messages as shown below.

:::image type="content" source="../media/emulator-v4/multi-turn-prompt.png" alt-text="An example transcript of a conversation with the multi-turn prompt bot.":::

## Additional information

### About dialog and bot state

In this bot, two state property accessors are defined:

- One created within conversation state for the dialog state property. The dialog state tracks where the user is within the dialogs of a dialog set, and it's updated by the dialog context, such as when the _begin dialog_ or _continue dialog_ methods are called.
- One created within user state for the user profile property. The bot uses this to track information it has about the user, and you must explicitly manage this state in the dialog code.

The _get_ and _set_ methods of a state property accessor get and set the value of the property in the state management object's cache. The cache is populated the first time the value of a state property is requested in a turn, but it must be persisted explicitly. In order to persist changes to both of these state properties, a call to the _save changes_ method, of the corresponding state management object, is performed.

This sample updates the user profile state from within the dialog. This practice can work for some bots, but it won't work if you want to reuse a dialog across bots.

There are various options for keeping dialog steps and bot state separate. For example, once your dialog gathers complete information, you can:

- Use the _end dialog_ method to provide the collected data as return value back to the parent context. This can be the bot's turn handler or an earlier active dialog on the dialog stack and it's how the prompt classes are designed.
- Generate a request to an appropriate service. This might work well if your bot acts as a front end to a larger service.

### Definition of a prompt validator method

# [C#](#tab/csharp)

**UserProfileDialog.cs**

Below is a validator code example for the `AgePromptValidatorAsync` method definition. `promptContext.Recognized.Value` contains the parsed value, which is an integer here for the number prompt. `promptContext.Recognized.Succeeded` indicates whether the prompt was able to parse the user's input or not. The validator should return false to indicate that the value wasn't accepted and the prompt dialog should reprompt the user; otherwise, return true to accept the input and return from the prompt dialog. You can change the value in the validator per your scenario.

[!code-csharp[prompt validator method](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=180-184)]

# [JavaScript](#tab/javascript)

**dialogs\userProfileDialog.js**

Below is a validator code example for the `agePromptValidator` method definition. `promptContext.recognized.value` contains the parsed value, which is an integer here for the number prompt. `promptContext.recognized.succeeded` indicates whether the prompt was able to parse the user's input or not. The validator should return false to indicate that the value wasn't accepted and the prompt dialog should reprompt the user; otherwise, return true to accept the input and return from the prompt dialog. You can change the value in the validator per your scenario.

[!code-javascript[age prompt validator](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/dialogs/userProfileDialog.js?range=169-172)]

# [Java](#tab/java)

**UserProfileDialog.java**

Below is a validator code example for the `agePromptValidator` method definition. `promptContext.getRecognized().getValue()` contains the parsed value, which is an integer here for the number prompt. `promptContext.getRecognized().getSucceeded()` indicates whether the prompt was able to parse the user's input or not. The validator should return false to indicate that it didn't accept the value. The prompt dialog should reprompt the user; otherwise, return true to accept the input and return from the prompt dialog. You can change the value in the validator per your scenario.

[!code-csharp[prompt validator method](~/../botbuilder-samples/samples/java_springboot/05.multi-turn-prompt/src/main/java/com/microsoft/bot/sample/multiturnprompt/UserProfileDialog.java?range=193-201)]

# [Python](#tab/python)

**dialogs/user_profile_dialog.py**

Below is a validator code example for the `age_prompt_validator` method definition. `prompt_context.recognized.value` contains the parsed value, which is an integer here for the number prompt. `prompt_context.recognized.succeeded` indicates whether the prompt was able to parse the user's input or not. The validator should return false to indicate that the value wasn't accepted and the prompt dialog should reprompt the user; otherwise, return true to accept the input and return from the prompt dialog. You can change the value in the validator per your scenario.

[!code-python[prompt validator method](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/dialogs/user_profile_dialog.py?range=207-212)]

---

## Next steps

> [!div class="nextstepaction"]
> [Add natural language understanding to your bot](bot-builder-howto-v4-luis.md)

[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md

[cs-sample]: https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/05.multi-turn-prompt
[js-sample]: https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/05.multi-turn-prompt
[java-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/05.multi-turn-prompt
[python-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/05.multi-turn-prompt

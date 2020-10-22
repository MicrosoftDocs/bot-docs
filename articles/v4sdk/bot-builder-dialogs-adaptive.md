---
title: Create a bot using adaptive dialogs   - Bot Service
description: Learn how to manage a conversation flow with adaptive in the Bot Framework SDK.
keywords: conversation flow, repeat, loop, menu, dialogs, prompts, adaptive, language generation
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/08/2020
monikerRange: 'azure-bot-service-4.0'
---

# Create a bot using adaptive dialogs  

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article shows how to use **Adaptive dialog** and **Language Generation** features to achieve the same functionality obtained with the waterfall model.

## Prerequisites

- Knowledge of [bot basics][concept-basics], [managing state][concept-state], and the [dialogs library][concept-dialogs].
- A copy of the **multi turn prompt** sample in either [**C#**][cs-sample] or [**JavaScript** preview][js-sample].

### Preliminary steps to add an adaptive dialog to a bot

You must follow the steps described below to add an adaptive dialog to a bot.

1. Update all Bot Builder NuGet packages to version 4.9.x.
1. Add the `Microsoft.Bot.Builder.Dialogs.Adaptive` package to your bot project.
1. Update the the bot adapter to add storage and the user and conversation state objects to every turn context.
1. Use a dialog manager in the bot code to start or continue the root dialog each turn.

## About the sample

This sample uses an adaptive dialog, a few prompts, and a component dialog to create a simple interaction that asks the user a series of questions. The questions are created using LG templates:

- For C#, defined in [RootDialog.lg](https://github.com/microsoft/BotBuilder-Samples/blob/master/samples/csharp_dotnetcore/adaptive-dialog/01.multi-turn-prompt/Dialogs/RootDialog.lg)
- For JavaScript, defined in [userProfileDialog.lg](https://github.com/microsoft/BotBuilder-Samples/blob/master/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/dialogs/userProfileDialog.lg) (JavaScript).

The code uses a dialog to cycle through these steps:

> [!div class="mx-tdCol2BreakAll"]
> | Steps        | LG template  |
> |:-------------|:-------------|
> | Ask the user for their mode of transportation | `ModeOfTransportPrompt` |
> | Ask the user for their name | `AskForName` |
> | Ask the user if they want to provide their age | `AgeConfirmPrompt` |
> | If they answered yes, asks for their age | `AskForAge` prompt with validation to only accept ages greater than 0 and less than 150 |
> | Asks if the collected information is "ok" | `ConfirmPrompt` prompt |

Finally, if they answered yes, display the collected information; otherwise, tell the user that their information will not be kept.

## Create the main dialog

# [C#](#tab/csharp)

To use dialogs, install the **Microsoft.Bot.Builder.Dialogs.Adaptive** NuGet package.

The bot interacts with the user via a root adaptive dialog. The bot then uses the `DialogManager.OnTurnAsync` to run the dialog.

![Root dialog](media/bot-builder-root-dialog-adaptive.png)

**Dialogs\RootDialog.cs**

The code begins by instantiating the root adaptive dialog. At this time, the following `WelcomeUserSteps` and `OnBeginDialogSteps` are added to the dialog. Notice the `paths` definition referencing the `Dialogs\RootDialog.lg` file that contains the LG templates used in the creation of the adaptive dialog.

[!code-csharp[define dialog snippet](~/../botbuilder-samples/samples/csharp_dotnetcore/adaptive-dialog/01.multi-turn-prompt/Dialogs/RootDialog.cs?range=17-38&highlight=3-4,21)]

Notice also:

- The LG template generator is added to the adaptive dialog, to enable the use of the **LG templates**.
- The two triggers are added, with their actions being provided by the two helper methods.

The `WelcomeUserSteps` method provides the actions to perform when the trigger fires. The `Foreach` actions iterates through the `membersAdded` list to greets the user added to the conversation.

> [!NOTE]
> Within the context of adaptive dialogs and triggers, all dialogs are valid actions, and the action types (`Foreach`, `IfCondition`, `SendActivity`) are all dialogs.\
> Some channels send two conversation update events: one for the bot added to the conversation and another for the user.
> The code filters cases where the bot itself is the recipient of the message. For more information, see [Categorized activities by channel](https://docs.microsoft.com/azure/bot-service/bot-service-channels-reference?view=azure-bot-service-4.0#welcome).

[!code-csharp[Welcome user](~/../botbuilder-samples/samples/csharp_dotnetcore/adaptive-dialog/01.multi-turn-prompt/Dialogs/RootDialog.cs?range=40-64)]

The `GatherUserInformation` implements the **steps** that the dialog uses. It defines the prompts using the LG templates from the `RootDialog.lg` file. The code below shows how the `Name` prompt is created.

[!code-csharp[gather user information](~/../botbuilder-samples/samples/csharp_dotnetcore/adaptive-dialog/01.multi-turn-prompt/Dialogs/RootDialog.cs?range=66-133)]

The `IfCondition` action uses an adaptive expression to either ask the user for their age or send an acknowledgement message, depending on their response to the previous question. Again it uses LG templates to format the prompts and messages.

[!code-csharp[if condition](~/../botbuilder-samples/samples/csharp_dotnetcore/adaptive-dialog/01.multi-turn-prompt/Dialogs/RootDialog.cs?range=91-119)]

# [JavaScript](#tab/javascript)

To use dialogs, your project needs to install the **botbuilder-dialogs-adaptive** npm package.
The sample demonstrates how to use **Adaptive dialog** and **Language Generation** features to achieve the same waterfall model functionality.

At start up, the main dialog `UserProfileDialog` is initialized. The bot uses it to interact with the user.

**dialogs/userProfileDialog.js**

The code begins by instantiating the `UserProfileDialog ` class which in turns
creates an instance of the `AdaptiveDialog` root dialog. At this time, the dialog steps are created using the **Language Generation** templates.

The `OnBeginDialog` implements the **steps** that the dialog uses. It defines the prompts using the LG templates from the `userProfileDialog.lg` file.

[!code-javascript[userProfileDialog constructor](~/../botbuilder-samples/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/dialogs/userProfileDialog.js?range=30-31)]

[!code-javascript[userProfileDialog constructor](~/../botbuilder-samples/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/dialogs/userProfileDialog.js?range=89-90)]

---

## Register the adaptive dialog

To allow the use of the adaptive dialog, the start up code must register the dialog as shown in the lines highlighted below, along with the other services.

# [C#](#tab/csharp)

The root adaptive dialog is created when the bot is created.

# [JavaScript](#tab/javascript)

**index.js**

The code creates the component dialog and services in `index.js`. In particular:

- The root component dialog.
- Basic services for a bot: a credential provider, an adapter, and the bot implementation.
- Services for managing state: storage, user state, and conversation state.

Import the required bot services and the component dialog class `userProfileDialog`.

[!code-javascript[index-import](~/../botbuilder-samples/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/index.js?range=7-13)]

Create conversation state with in-memory storage provider.

[!code-javascript[index-storage](~/../botbuilder-samples/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/index.js?range=49-54)]

Create the main dialog and the bot.

[!code-javascript[index-main-dialog](~/../botbuilder-samples/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/index.js?range=56-58)]

Listen for incoming requests and route the message to the bot's main handler.

[!code-javascript[index-run](~/../botbuilder-samples/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/index.js?range=68-74)]

---

## Run the dialog

# [C#](#tab/csharp)

**Bots/Dialogs.cs**

The `DialogManager.OnTurnAsync` runs the adaptive dialog with activities.
The implementation shown can run any type of `Dialog`. The `ConversationState` is used by the Dialog system. The `UserState` isn't, however, it might have been used in a dialog implementation. The `DialogManager.OnTurnAsync` method takes care of saving the state.

[!code-csharp[Dialogs](~/../botbuilder-samples/samples/csharp_dotnetcore/adaptive-dialog/01.multi-turn-prompt/Bots/DialogBot.cs?range=30-34&highlight=4)]

# [JavaScript](#tab/javascript)

**bots/dialogBot.js**

The `DialogBot` extends the `ActivityHandler` and runs the adaptive dialog with activities.
The state information contained by the`conversationState` and the `userState` are stored for the `dialogManager` to use.

[!code-javascript[index-run](~/../botbuilder-samples/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/bots/dialogBot.js?range=7-30&highlight=13-14,19-2)]

---

> [!NOTE]
> Memory storage is used for testing purposes only and is not intended for production use.
> Be sure to use a persistent type of storage for a production bot.

## To test the bot

1. If you have not done so already, install the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme).
1. Run the sample locally on your machine.
1. Start the Emulator, connect to your bot, and send messages as shown below.

![Sample run of the multi-turn prompt dialog](../media/emulator-v4/multi-turn-prompt-adaptive-sample.png)

## Next steps

> [!div class="nextstepaction"]
> [Create a bot using combined dialog types](bot-builder-mixed-dialogs.md)

<!-- Footnote-style links -->

[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md

[prompting]: bot-builder-prompts.md
[component-dialogs]: bot-builder-compositcontrol.md

[cs-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/adaptive-dialog/01.multi-turn-prompt
[js-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt

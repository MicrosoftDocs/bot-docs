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

[!INCLUDE[applies-to](../includes/applies-to.md)]

This article shows how to use **Adaptive dialog** and **Language Generation** features to achieve the same functionality obtained with the waterfall model.

## Prerequisites

- Knowledge of [bot basics][concept-basics], [managing state][concept-state], and the [dialogs library][concept-dialogs].
- A copy of the **multi turn prompt** sample in either [**C#**][cs-sample] or [**JavaScript** preview][js-sample].

### Preliminary steps to add an adaptive dialog to a bot

You must follow the steps described below to add an adaptive dialog to a bot.

1. Update all packages to version 4.9.x from the [Nuget](https://www.nuget.org/) site.
1. Add the `Microsoft.Bot.Builder.Dialogs.Adaptive` package.
1. Add and configure `DialogManager` in `DialogBot.cs`. This internally takes care of saving state on each turn.
1. Update the `adapter` to use `storage`, `conversation state` and `user state`.

## About the sample

This sample uses an adaptive dialog, a few prompts, and a component dialog to create a simple interaction that asks the user a series of questions. The questions are created using LG templates:

- For C#, defined in [RootDialog.lg](https://github.com/microsoft/BotBuilder-Samples/blob/vishwac/r9/js/experimental/adaptive-dialog/csharp_dotnetcore/01.multi-turn-prompt/Dialogs/RootDialog.lg)
- For JavaScript, defined in [userProfileDialog.lg](https://github.com/microsoft/BotBuilder-Samples/blob/vishwac/r9/js/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/dialogs/userProfileDialog.lg) (JavaScript).

The code uses a dialog to cycle through these steps:

> [!div class="mx-tdCol2BreakAll"]
> | Steps        | LG template  |
> |:-------------|:-------------|
> | Ask the users for their mode of transportation | `ModeOfTransportPrompt` |
> | Ask the users for their name | `AskForName` |
> | Ask the users if they want to provide their age | `AgeConfirmPrompt` |
> | If they answered yes, asks for their age | `AskForAge` prompt with validation to only accept ages greater than 0 and less than 150 |
> | Asks if the collected information is "ok" | `ConfirmPrompt` prompt |

Finally, if they answered yes, display the collected information; otherwise, tell the user that their information will not be kept.

## Create the main dialog

# [C#](#tab/csharp)

To use dialogs, install the **Microsoft.Bot.Builder.Dialogs** and **Microsoft.Bot.Builder.Dialogs.Adaptive** NuGet packages.

The bot interacts with the user via the `RootDialog`. When the bot's `RootDialog` is created, the `AdaptiveDialog` is set as the main dialog. The bot then uses the `DialogManager.OnTurnAync` to run the dialog.

![Root dialog](media/bot-builder-root-dialog-adaptive.png)

**Dialogs/RootDialog.cs**

The code begins by instantiating the `RootDialog` class which in turns
creates an instance of the `AdaptiveDialog`. At this time, the following `WelcomeUserSteps` and `OnBeginDialogSteps` are added to the dialog.
The created dialog is then added to the `DialogSet` and the name is saved in the dialog state. Finally, the name of the initial dialog to run is assigned to `InitialDialogId`. Notice the `paths` definition referencing the `RootDialog.lg` file that contains the LG templates used in the creation of the adaptive dialog.

[!code-csharp[RootDialog snippet](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/01.multi-turn-prompt/Dialogs/RootDialog.cs?range=18-49&highlight=6-25)]

In `WelcomeUserSteps`, the code iterates through the `membersAdded` list to greets the user added to the conversation.

> [!NOTE]
> Some channels send two conversation update events: one for the bot added to the conversation and another for the user.
> The code filters cases where the bot itself is the recipient of the message.

[!code-csharp[RootDialog snippet](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/01.multi-turn-prompt/Dialogs/RootDialog.cs?range=51-75&highlight=13-20)]

The `OnBeginDialogSteps` implements the **steps** that the dialog uses. It defines the prompts using the LG templates from the `RootDialog.lg` file. The following code shows how the `Name` prompt is created:

[!code-csharp[RootDialog snippet](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/01.multi-turn-prompt/Dialogs/RootDialog.cs?range=88-92)]

The following code shows how a prompt is built conditionally:

[!code-csharp[RootDialog snippet](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/01.multi-turn-prompt/Dialogs/RootDialog.cs?range=102-130)]


# [JavaScript](#tab/javascript)

To use dialogs, your project needs to install the **botbuilder-dialogs**, and the **botbuilder-dialogs-adaptive** npm packages.
The sample demonstrates how to use **Adaptive dialog** and **Language Generation** features to achieve the same waterfall model functionality.

At start up, the main dialog `UserProfileDialog` is initialized. The bot uses it to interact with the user.

**dialogs/userProfileDialog.js**

The code begins by instantiating the `UserProfileDialog ` class which in turns
creates an instance of the `AdaptiveDialog` root dialog. At this time, the dialog steps are created using the **Language Generation** templates.

The `OnBeginDialog` implements the **steps** that the dialog uses. It defines the prompts using the LG templates from the `userProfileDialog.lg` file. The following code shows how the `Name` prompt is created:

[!code-javascript[userProfileDialog snippet](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/dialogs/userProfileDialog.js?range=20-24)]

The following code shows how a prompt is built conditionally:

[!code-javascript[userProfileDialog snippet](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/dialogs/userProfileDialog.js?range=39-64)]


---

## Register the adaptive dialog

To allow the use of the adaptive dialog, the start up code must register the dialog as shown in the lines highlighted below, along with the other services.

# [C#](#tab/csharp)

**Startup.cs**

You register the adaptive dialogs in the `Startup` class, along with the other services.

[!code-csharp[ConfigureServices](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/01.multi-turn-prompt/Startup.cs?range=21-54&highlight=5-15)]

# [JavaScript](#tab/javascript)


**index.js**

The code registers the adaptive dialog and services in `index.js`. In particular, it registers:

- Adaptive dialog.
- Basic services for a bot: a credential provider, an adapter, and the bot implementation.
- Services for managing state: storage, user state, and conversation state.

Import required bot services and the adaptive dialog class `userProfileDialog`.

[!code-javascript[index-import](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/index.js?range=7-13)]

Create conversation state with in-memory storage provider.

[!code-javascript[index-storage](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/index.js?range=49-54)]

Create the main dialog.

[!code-javascript[index-storage](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/index.js?range=57-58)]

Listen for incoming requests and route the message to the bot's main handler.

[!code-javascript[index-run](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/index.js?range=68-73)]

---

## Run the dialog

# [C#](#tab/csharp)

**Bots/Dialogs.cs**

The `DialogManager.OnTurnAsync` runs the adaptive dialog with activities.
The implementation shown can run any type of `Dialog`. The `ConversationState` is used by the Dialog system. The `UserState` isn't, however, it might have been used in a dialog implementation, and the requirement is that all `BotState` objects are saved at the end of a turn.

[!code-csharp[ConfigureServices](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/01.multi-turn-prompt/Bots/DialogBot.cs?range=18-41&highlight=21)]

# [JavaScript](#tab/javascript)

**bots/dialogBot.js**

The `DialogBot` extends the `ActivityHandler` and runs the adaptive dialog with activities.

[!code-javascript[DialogBot](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt/bots/dialogBot.js?range=7-30&highlight=19-21)]

---

> [!NOTE]
> Memory storage is used for testing purposes only and is not intended for production use.
> Be sure to use a persistent type of storage for a production bot.

## To test the bot

1. If you have not done so already, install the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme).
1. Run the sample locally on your machine.
1. Start the emulator, connect to your bot, and send messages as shown below.

![Sample run of the multi-turn prompt dialog](../media/emulator-v4/multi-turn-prompt-adaptive-sample.png)

## Next steps

> [!div class="nextstepaction"]
> [Create a bot using adaptive, component, waterfall, and custom dialogs](bot-builder-mixed-dialogs.md)

<!-- Footnote-style links -->

[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md

[prompting]: bot-builder-prompts.md
[component-dialogs]: bot-builder-compositcontrol.md

[cs-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/vishwac/r9/js/experimental/adaptive-dialog/csharp_dotnetcore/01.multi-turn-prompt
[js-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/vishwac/r9/js/experimental/adaptive-dialog/javascript_nodejs/01.multi-turn-prompt

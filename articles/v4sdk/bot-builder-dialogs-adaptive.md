---
title: Create a bot using adaptive dialogs   - Bot Service
description: Learn how to manage a conversation flow with adaptive in the Bot Framework SDK.
keywords: conversation flow, repeat, loop, menu, dialogs, prompts, adaptive, language generation
author: JonathanFingold
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
- A copy of the **waterfall custom dialog with adaptive** sample in either [**C#**][cs-sample], [**JavaScript** preview][js-sample]

### Preliminary steps to add an adaptive dialog to a bot

You must follow the steps described below to add an adaptive dialog to a bot.

1. Update all packages to version 4.9.x from the [Nuget](https://www.nuget.org/) site.
1. Add the `Microsoft.Bot.Builder.Dialogs.Adaptive` package.
1. Add and configure `DialogManager` in `DialogBot.cs`. This internally takes care of saving state on each turn.
1. Update the `adapter` to use `storage`, `conversation state` and `user state`.


## About the sample

This sample uses an adaptive dialog, a few prompts, and a component dialog to create a simple interaction that asks the user a series of questions. The code uses a dialog to cycle through these steps:

| Steps        | Prompt type  |
|:-------------|:-------------|
| Ask the user for their mode of transportation | Choice prompt |
| Ask the user for their name | Text prompt |
| Ask the user if they want to provide their age | Confirm prompt |
| If they answered yes, asks for their age | Number prompt with validation to only accept ages greater than 0 and less than 150 |
| If they're not using Microsoft Teams, ask them for a profile picture | Attachment prompt with validation to allow a missing attachment |
| Asks if the collected information is "ok" | Reuse Confirm prompt |

Finally, if they answered yes, display the collected information; otherwise, tell the user that their information will not be kept.

## Create the main dialog

# [C#](#tab/csharp)

To use dialogs, install the **Microsoft.Bot.Builder.Dialogs** and **Microsoft.Bot.Builder.Dialogs.Adaptive** NuGet packages.

The bot interacts with the user via `UserProfileDialog`. At the time the `DialogBot` class is instantiated, the `UserProfileDialog` is sets as the main dialog. The bot then uses a `Run` helper method to access the dialog.

<!-- Add diagram -->

**Dialogs\RootDialog.cs**

The user interacts with

The bot interacts with the user via the `RootDialog`. When the `RootDialog` class is instantiated, an instance of the `AdaptiveDialog` is created.

![Root dialog](media/bot-builder-root-dialog-adaptive.png)


Triggers are also added to the `AdaptiveDialog` instance. In particular, how to welcome the user and how to respond to the user's messages. The created dialog is then added to the `DialogSet` and name is saved in the dialog state. Finally, the name of the initial dialog to run is assigned to `InitialDialogId`. Notice the `paths` definition referencing the `RootDialog.lg` file that contains the LG templates used in the creation of the adaptive dialog.

[!code-csharp[Constructor snippet](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/01.multi-turn-prompt/Dialogs/RootDialog.cs?range=18-49&highlight=6-25)]

In `WelcomeUserSteps`, the code iterates through the `membersAdded` list to greets the user added to the conversation.

> [!NOTE]
> Some channels send two conversation update events: one for the bot added to the conversation and another for the user.
> The code filters cases where the bot itself is the recipient of the message.

[!code-csharp[Constructor snippet](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/01.multi-turn-prompt/Dialogs/RootDialog.cs?range=51-75&highlight=13-20)]

The `OnBeginDialogSteps` implements the steps that the dialog uses. It defines the prompts using the LG templates from the `RootDialog.lg` file. The following code shows `Name` prompt is created:

```csharp
{
    Prompt = new ActivityTemplate("${AskForName()}"),
    Property = "user.userProfile.Name"
}

```

The following code shows how a prompt is built conditionally:

[!code-csharp[Constructor snippet](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/01.multi-turn-prompt/Dialogs/RootDialog.cs?range=102-130)]


# [JavaScript](#tab/javascript)

To use dialogs, your project needs to install the **botbuilder-dialogs** npm package. Q? What about the adaptive dialogs?


---

## Register services and adaptive dialogs

To allow the use of the adaptive dialogs, the start up code must contain the lines highlighted below along with the other services.

# [C#](#tab/csharp)

**Startup.cs**

You register the adaptive dialogs in the `Startup` class, along with the other services.

[!code-csharp[ConfigureServices](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/01.multi-turn-prompt/Startup.cs?range=21-54&highlight=5-15)]


# [JavaScript](#tab/javascript)

**index.js**

You register services for the bot in `index.js`.

---

> [!NOTE]
> Memory storage is used for testing purposes only and is not intended for production use.
> Be sure to use a persistent type of storage for a production bot.


## To test the bot

1. If you have not done so already, install the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme).
1. Run the sample locally on your machine.
1. Start the emulator, connect to your bot, and send messages as shown below.

<!-- ![Sample run of the multi-turn prompt dialog](../media/emulator-v4/mixed-dialogs.png) -->


## Next steps

> [!div class="nextstepaction"]
> [TBD](bot-builder-howto-v4-luis.md)

<!-- Footnote-style links -->

[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md

[prompting]: bot-builder-prompts.md
[component-dialogs]: bot-builder-compositcontrol.md

[cs-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/vishwac/r9/js/experimental/adaptive-dialog/csharp_dotnetcore/01.multi-turn-prompt
[js-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/vishwac/r9/js/experimental/adaptive-dialog/javascript_nodejs/05.multi-turn-prompt
[python-sample]:

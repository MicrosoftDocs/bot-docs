---
title: Create a bot using both traditional and adaptive dialogs   - Bot Service
description: Learn how to manage a conversation flow with conventional and adaptive in the Bot Framework SDK.
keywords: conversation flow, repeat, loop, menu, dialogs, prompts, waterfalls, dialog set
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/07/2020
monikerRange: 'azure-bot-service-4.0'
---

# Create a bot using adaptive, component, and waterfall dialogs  

[!INCLUDE[applies-to](../includes/applies-to.md)]

This article shows how to create a bot using adaptive along with traditional dialogs.

BotFramework provides a built-in base class called `Dialog`. By sub-classing this class, you can create new ways to define and control dialog flows used by the bot. By adhering to the features of this class, you can create custom dialogs that can be used side-by-side with other dialog types, as well as built-in or custom prompts.


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

The sample in this article demonstrates a custom `Dialog` class called `SlotFillingDialog`, which takes a series of "slots" which define a value the bot needs to collect from the user, as well as the prompt it should use. The bot will iterate through all of the slots until they are all full, at which point the dialog completes.


## Create the main dialog

# [C#](#tab/csharp)

To use dialogs, install the **Microsoft.Bot.Builder.Dialogs** and **Microsoft.Bot.Builder.Dialogs.Adaptive** NuGet packages.

The bot interacts with the user via `UserProfileDialog`. When we create the bot's `DialogBot` class, we will set the `UserProfileDialog` as its main dialog. The bot then uses a `Run` helper method to access the dialog.


**Dialogs\UserProfileDialog.cs**

We begin by creating the `UserProfileDialog` that derives from the `ComponentDialog` class, and has 7 steps.

**UserProfile.cs**

The user's name, and age are saved in an instance of the `UserProfile` class.

**Dialogs\UserProfileDialog.cs**

In the last step, we check the `stepContext.Result` returned by the dialog

# [JavaScript](#tab/javascript)

To use dialogs, your project needs to install the **botbuilder-dialogs** npm package. Q? What about the adaptive dialogs?


**dialogs/userProfileDialog.js**

We begin by creating the `UserProfileDialog` that derives from the `ComponentDialog` class.


**userProfile.js**

The user's name, and age are saved in an instance of the `UserProfile` class.

**dialogs/userProfileDialog.js**

In the last step, we check the `step.result` returned by the dialog called in the previous waterfall step

---

## Register services and adaptive dialogs

To allow the use of the adaptive dialogs, the start up code must contain the lines highlighted below along with the other services.

# [C#](#tab/csharp)

**Startup.cs**

You register services for the bot in `Startup` along with the adaptive dialogs.

[!code-csharp[ConfigureServices](~/../botbuilder-samples-adaptive/experimental/adaptive-dialog/csharp_dotnetcore/04.waterfall-or-custom-dialog-with-adaptive/Startup.cs?range=15-48&highlight=8-15)]


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

![Sample run of the multi-turn prompt dialog](../media/emulator-v4/mixed-dialogs.png)


## Next steps

> [!div class="nextstepaction"]
> [TBD](bot-builder-howto-v4-luis.md)

<!-- Footnote-style links -->

[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md

[prompting]: bot-builder-prompts.md
[component-dialogs]: bot-builder-compositcontrol.md

[cs-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/vishwac/r9/js/experimental/adaptive-dialog/csharp_dotnetcore/04.waterfall-or-custom-dialog-with-adaptive
[js-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/vishwac/r9/js/experimental/adaptive-dialog/javascript_nodejs/19.custom-dialogs
[python-sample]:

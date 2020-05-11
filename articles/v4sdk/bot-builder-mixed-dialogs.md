---
title: Create a bot using adaptive, component, and waterfall dialogs - Bot Service
description: Learn how to manage a conversation flow with conventional and adaptive in the Bot Framework SDK.
keywords: conversation flow, dialogs, component dialogs, custom dialogs, waterfall dialogs, adaptive dialogs
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/07/2020
monikerRange: 'azure-bot-service-4.0'
---

# Create a bot using adaptive, component, waterfall, and custom dialogs  

[!INCLUDE[applies-to](../includes/applies-to.md)]

All dialogs derive from a base _dialog_ class.
If you use the _dialog manager_ to run your root dialog, all of the dialog classes can work together.
This article shows how to use component, waterfall, custom, and adaptive dialogs together in one bot.

This article focuses on the code that allows these dialogs to work together. See the [additional information](#additional-information) for articles that cover each type of dialog in more detail.

## Prerequisites

- Knowledge of [bot basics][bot-basics], [managing state][concept-state], the [dialogs library][about-dialogs], and [adaptive dialogs][about-adaptive-dialogs].
- A copy of the **waterfall or custom dialog with adaptive** sample in either [**C#**][cs-sample], [**JavaScript** (preview)][js-sample]

### Preliminary steps to add an adaptive dialog to a bot

You must follow the steps described below to add an adaptive dialog to a bot.
These steps are covered in more detail in how to [create a bot using adaptive dialogs][basic-adaptive-how-to].

#### [C#](#tab/cs)

1. Update all Bot Builder NuGet packages to version 4.9.x.
1. Add the `Microsoft.Bot.Builder.Dialogs.Adaptive` package to your bot project.
1. Update the the bot adapter to add storage and the user and conversation state objects to every turn context.
1. Use a dialog manager in the bot code to start or continue the root dialog each turn.

#### [JavaScript](#tab/js)

1. Update all Bot Builder npm packages to version 4.9.x.
1. Add the `botbuilder-dialogs-adaptive` package to your bot project.
1. In the bot's on-turn handler:
   1. Create a dialog manager.
   1. Set the dialog manager's storage and user and conversation state properties.
   1. Use the dialog manager to start or continue the root dialog.

---

## About the sample

By way of illustration, this sample combines various dialog types together in one bot.
It does not demonstrate best practices for designing conversation flow.
The sample:

- Defines a custom _slot filling_ dialog.
- Creates a root component dialog:
  - A waterfall dialog manages the top-level conversation flow.
  - Together, an adaptive dialog, 2 custom slot filling dialogs, and a few prompts manage the rest of the conversation flow.

The custom dialog accepts a list of properties (slots to fill). If any of the values for these properties are missing, it will prompt for them until all of the slots are filled.
The samples _binds_ a property to the adaptive dialog to allow the adaptive dialog to also fill slots.

This article focuses on how the various dialog types work together.
For information about configuring your bot to use adaptive dialogs, see how to [create a bot using adaptive dialogs][basic-adaptive-how-to].
For more on using adaptive dialogs to gather user input, see [about inputs in adaptive dialogs][about-input-dialogs].

## Define the custom dialog

### [C#](#tab/csharp)

### [JavaScript](#tab/javascript)

---

## Create the main dialog

### [C#](#tab/csharp)

### [JavaScript](#tab/javascript)

---

## To test the bot

1. If you have not done so already, install the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme).
1. Run the sample locally on your machine.
1. Start the emulator, connect to your bot, and send messages as shown below.

![Sample run of the multi-turn prompt dialog](../media/emulator-v4/mixed-dialogs.png)

## Additional information

For more information on how to use each dialog type, see these articles:

- For waterfall and prompt dialogs, see [implement sequential conversation flow][basic-dialog-how-to].
- For component dialogs, see [manage dialog complexity][component-how-to].
- For adaptive and input dialogs, see [create a bot using adaptive dialogs][basic-adaptive-how-to].

## Next steps

> [!div class="nextstepaction"]
> [TBD](tbd.md)

<!-- Footnote-style links -->

[bot-basics]: bot-builder-basics.md
[about-dialogs]: bot-builder-concept-dialog.md
[about-adaptive-dialogs]: tbd.md
[about-input-dialogs]: tbd.md

[basic-adaptive-how-to]: bot-builder-dialogs-adaptive.md
[basic-dialog-how-to]: bot-builder-dialog-manage-conversation-flow.md
[component-how-to]: bot-builder-compositcontrol.md

[cs-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/vishwac/r9/js/experimental/adaptive-dialog/csharp_dotnetcore/04.waterfall-or-custom-dialog-with-adaptive
[js-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/vishwac/r9/js/experimental/adaptive-dialog/javascript_nodejs/19.custom-dialogs

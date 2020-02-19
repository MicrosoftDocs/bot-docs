---
title: Create advanced conversation flow using branches and loops - Bot Service
description: Learn how to manage a complex conversation flow with dialogs in the Bot Framework SDK.
keywords: complex conversation flow, repeat, loop, menu, dialogs, prompts, waterfalls, dialog set
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 01/30/2020
monikerRange: 'azure-bot-service-4.0'
---

# Create advanced conversation flow using branches and loops

[!INCLUDE[applies-to](../includes/applies-to.md)]

You can create complex conversation flows using the dialogs library.
This article covers how to manage complex conversations that branch and loop and how to pass arguments between different parts of the dialog.

## Prerequisites

- Knowledge of [bot basics][concept-basics], [managing state][concept-state], the [dialogs library][concept-dialogs], and how to [implement sequential conversation flow][simple-dialog].
- A copy of the complex dialog sample in [**C#**][cs-sample], [**JavaScript**][js-sample], or [**Python**][python-sample].

## About this sample

This sample represents a bot that can sign users up to review up to two companies from a list.
The bot uses 3 component dialogs to manage the conversation flow.
Each component dialog includes a waterfall dialog and any prompts needed to gather user input.
These dialogs are described in more detail in the following sections.
It uses conversation state to manage its dialogs and uses user state to save information about the user and which companies they want to review.

The bot derives from the activity handler. Like many of the sample bots, it welcomes the user, uses dialogs to handle messages from the user, and saves user and conversation state before the turn ends.

### [C#](#tab/csharp)

To use dialogs, install the **Microsoft.Bot.Builder.Dialogs** NuGet package.

![Complex bot flow](./media/complex-conversation-flow.png)

### [JavaScript](#tab/javascript)

To use dialogs, your project needs to install the **botbuilder-dialogs** npm package.

![Complex bot flow](./media/complex-conversation-flow-js.png)

### [Python](#tab/python)

To use dialogs, your project needs to install the **botbuilder-dialogs** PyPI package by running `pip install botbuilder-dialogs`.

![Complex bot flow](./media/complex-conversation-flow-python.png)

---

## Define the user profile

The user profile will contain information gathered by the dialogs, the user's name, age, and companies selected to review.

### [C#](#tab/csharp)

**UserProfile.cs**

[!code-csharp[UserProfile class](~/../botbuilder-samples/samples/csharp_dotnetcore/43.complex-dialog/UserProfile.cs?range=8-16)]

### [JavaScript](#tab/javascript)

**userProfile.js**

[!code-javascript[UserProfile class](~/../botbuilder-samples/samples/javascript_nodejs/43.complex-dialog/userProfile.js?range=4-12)]

### [Python](#tab/python)

**data_models/user_profile.py**

[!code-python[UserProfile class](~/../botbuilder-samples/samples/python/43.complex-dialog/data_models/user_profile.py?range=7-13)]

---

## Create the dialogs

This bot contains 3 dialogs:

- The main dialog starts the overall process and then summarizes the collected information.
- The top-level dialog collects the user information and includes branching logic, based on the user's age.
- The review-selection dialog allows the user to iteratively select companies to review. It uses looping logic to do so.

### The main dialog

The main dialog has 2 steps:

1. Start the top-level dialog.
1. Retrieve and summarize the user profile that the top-level dialog collected, save that information to user state, and then signal the end of the main dialog.

#### [C#](#tab/csharp)

**Dialogs\MainDialog.cs**

[!code-csharp[step implementations](~/../botbuilder-samples/samples/csharp_dotnetcore/43.complex-dialog/Dialogs/MainDialog.cs?range=31-50)]

#### [JavaScript](#tab/javascript)

**dialogs/mainDialog.js**

[!code-javascript[step implementations](~/../botbuilder-samples/samples/javascript_nodejs/43.complex-dialog/dialogs/mainDialog.js?range=43-55)]

#### [Python](#tab/python)

**dialogs\main_dialog.py**

[!code-python[step implementations](~/../botbuilder-samples/samples/python/43.complex-dialog/dialogs/main_dialog.py?range=29-50)]

---

### The top-level dialog

The top-level dialog has 4 steps:

1. Ask for the user's name.
1. Ask for the user's age.
1. Either start the review-selection dialog or progress to the next step, based on the user's age.
1. Finally, thank the user for participating and return the collected information.

The first step creates an empty user profile as part of the dialog state. The dialog starts with an empty profile and adds information to the profile as it progresses. When it ends, the last step returns the collected information.

In the third (start selection) step, the conversation flow branches, based on the user's age.

#### [C#](#tab/csharp)

**Dialogs\TopLevelDialog.cs**

[!code-csharp[step implementations](~/../botbuilder-samples/samples/csharp_dotnetcore/43.complex-dialog/Dialogs/TopLevelDialog.cs?range=39-96&highlight=30-42)]

#### [JavaScript](#tab/javascript)

**dialogs/topLevelDialog.js**

[!code-javascript[step implementations](~/../botbuilder-samples/samples/javascript_nodejs/43.complex-dialog/dialogs/topLevelDialog.js?range=32-76&highlight=25-33)]

#### [Python](#tab/python)

**dialogs\top_level_dialog.py**

[!code-python[step implementations](~/../botbuilder-samples/samples/python/43.complex-dialog/dialogs/top_level_dialog.py?range=43-95&highlight=29-38)]

---

### The review-selection dialog

The review-selection dialog has 2 steps:

1. Ask the user to choose a company to review or `done` to finish.
   - If the dialog was started with any initial information, the information is available through the _options_ property of the waterfall step context. The review-selection dialog can restart itself, and it uses this to allow the user to choose more than one company to review.
   - If the user has already selected a company to review, that company is removed from the available choices.
   - A `done` choice is added to allow the user to exit the loop early.
1. Repeat this dialog or exit, as appropriate.
   - If the user chose a company to review, add it to their list.
   - If the user has chosen 2 companies or they chose to exit, end the dialog and return the collected list.
   - Otherwise, restart the dialog, initializing it with the contents of their list.

#### [C#](#tab/csharp)

**Dialogs\ReviewSelectionDialog.cs**

[!code-csharp[step implementations](~/../botbuilder-samples/samples/csharp_dotnetcore/43.complex-dialog/Dialogs/ReviewSelectionDialog.cs?range=42-106&highlight=55-64)]

#### [JavaScript](#tab/javascript)

**dialogs/reviewSelectionDialog.js**

[!code-javascript[step implementations](~/../botbuilder-samples/samples/javascript_nodejs/43.complex-dialog/dialogs/reviewSelectionDialog.js?range=33-78&highlight=39-45)]

#### [Python](#tab/python)

**dialogs/review_selection_dialog.py**

[!code-python[step implementations](~/../botbuilder-samples/samples/python/43.complex-dialog/dialogs/review_selection_dialog.py?range=42-99&highlight=51-58)]

---

## Run the dialogs

The _dialog bot_ class extends the activity handler, and it contains the logic for running the dialogs.
The _dialog and welcome bot_ class extends the dialog bot to also welcome a user when they join the conversation.

The bot's turn handler repeats the conversation flow defined by the 3 dialogs.
When it receives a message from the user:

1. It runs the main dialog.
   - If the dialog stack is empty, this will start the main dialog.
   - Otherwise, the dialogs are still in mid-process, and this will continue the active dialog.
1. It saves state, so that any updates to the user, conversation, and dialog state are persisted.

### [C#](#tab/csharp)

**Bots\DialogBot.cs**

[!code-csharp[Overrides](~/../botbuilder-samples/samples/csharp_dotnetcore/43.complex-dialog/Bots/DialogBot.cs?range=33-48&highlight=5-7,14-15)]

### [JavaScript](#tab/javascript)

**bots/dialogBot.js**

[!code-javascript[onMessage](~/../botbuilder-samples/samples/javascript_nodejs/43.complex-dialog/bots/dialogBot.js?range=24-32&highlight=4-5)]
[!code-javascript[run](~/../botbuilder-samples/samples/javascript_nodejs/43.complex-dialog/bots/dialogBot.js?range=35-44&highlight=7-9)]

### [Python](#tab/python)

**bots/dialog_bot.py**

[!code-python[Overrides](~/../botbuilder-samples/samples/python/43.complex-dialog/bots/dialog_bot.py?range=29-41&highlight=4-6,9-13)]

---

## Register services for the bot

Create and register services as needed:

- Basic services for the bot: an adapter and the bot implementation.
- Services for managing state: storage, user state, and conversation state.
- The root dialog the bot will use.

### [C#](#tab/csharp)

**Startup.cs**

[!code-csharp[ConfigureServices](~/../botbuilder-samples/samples/csharp_dotnetcore/43.complex-dialog/Startup.cs?range=18-37)]

### [JavaScript](#tab/javascript)

**index.js**

[!code-javascript[ConfigureServices](~/../botbuilder-samples/samples/javascript_nodejs/43.complex-dialog/index.js?range=26-43)]

### [Python](#tab/python)

**app.py**

[!code-python[ConfigureServices](~/../botbuilder-samples/samples/python/43.complex-dialog/app.py?range=29-32)]
[!code-python[ConfigureServices](~/../botbuilder-samples/samples/python/43.complex-dialog/app.py?range=70-77)]

---

> [!NOTE]
> Memory storage is used for testing purposes only and is not intended for production use.
> Be sure to use a persistent type of storage for a production bot.

## To test the bot

1. If you have not done so already, install the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme).
1. Run the sample locally on your machine.
1. Start the emulator, connect to your bot, and send messages as shown below.

![test complex dialog sample](~/media/emulator-v4/test-complex-dialog.png)

## Additional resources

For an introduction on how to implement a dialog, see [implement sequential conversation flow][simple-dialog], which uses a single waterfall dialog and a few prompts to create a simple interaction that asks the user a series of questions.

The Dialogs library includes basic validation for prompts. You can also add custom validation. For more information, see [gather user input using a dialog prompt][dialog-prompts].

To simplify your dialog code and reuse it multiple bots, you can define portions of a dialog set as a separate class.
For more information, see [reuse dialogs][component-dialogs].

## Next steps

> [!div class="nextstepaction"]
> [Reuse dialogs](bot-builder-compositcontrol.md)

<!-- Footnote-style links -->

[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md

[simple-dialog]: bot-builder-dialog-manage-conversation-flow.md
[dialog-prompts]: bot-builder-prompts.md
[component-dialogs]: bot-builder-compositcontrol.md

[cs-sample]: https://aka.ms/cs-complex-dialog-sample
[js-sample]: https://aka.ms/js-complex-dialog-sample
[python-sample]: https://aka.ms/python-complex-dialog-sample

---
title: Manage dialog complexity | Microsoft Docs
description: Learn how to modularize your dialog complexity using component dialogs in the Bot Framework SDK.
keywords: composite control, modular bot logic
author: v-ducvo
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/14/2020
monikerRange: 'azure-bot-service-4.0'
---

# Manage dialog complexity

[!INCLUDE[applies-to](../includes/applies-to.md)]

<!--TODO This sample doesn't really need a separate LG tab. We need to decide whether we'll use the LG updated sample or the old sample.-->

With component dialogs, you can create independent dialogs to handle specific scenarios, breaking a large dialog set into more manageable pieces. Each of these pieces has its own dialog set, and avoids any name collisions with the dialog sets outside of it. Component dialogs are reusable in that they can be:

- Added to another `ComponentDialog` or `DialogSet` in your bot.
- Exported as a part of a package.
- Used within other bots.

## Prerequisites

- Knowledge of [bot basics][concept-basics], the [dialogs library][concept-dialogs], and how to [manage conversations][simple-flow].
- A copy of the multi-turn prompt sample in [**C#**][cs-sample], [**JavaScript**][js-sample], or [**Python**][python-sample]<!--, or [**Language Generation**][lg-sample]-->.

## About the sample

In the multi-turn prompt sample, we use a waterfall dialog, a few prompts, and a component dialog to create a simple interaction that asks the user a series of questions. The code uses a dialog to cycle through these steps:

| Steps        | Prompt type  |
|:-------------|:-------------|
| Ask the user for their mode of transportation | Choice prompt |
| Ask the user for their name | Text prompt |
| Ask the user if they want to provide their age | Confirm prompt |
| If they answered yes, asks for their age  | Number prompt with validation to only accept ages greater than 0 and less than 150. |
| Asks if the collected information is "ok" | Reuse Confirm prompt |

Finally, if they answered yes, display the collected information; otherwise, tell the user that their information will not be kept.

## Implement the component dialog

In the multi-turn prompt sample, we use a _waterfall dialog_, a few _prompts_, and a _component dialog_ to create a simple interaction that asks the user a series of questions.

A component dialog encapsulates one or more dialogs. The component dialog has an inner dialog set, and the dialogs and prompts that you add to this inner dialog set have their own IDs, visible only from within the component dialog.

# [C#](#tab/csharp)

To use dialogs, install the **Microsoft.Bot.Builder.Dialogs** NuGet package.

**Dialogs\UserProfileDialog.cs**

Here the `UserProfileDialog` class derives from the `ComponentDialog` class.

[!code-csharp[Class](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=16)]

Within the constructor, the `AddDialog` method adds dialogs and prompts to the component dialog. The first item you add with this method is set as the initial dialog, but you can change this by explicitly setting the `InitialDialogId` property. When you start a component dialog, it will start its _initial dialog_.

[!code-csharp[Constructor](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=20-47)]

This is the implementation of the first step of the waterfall dialog.

[!code-csharp[First step](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=61-66)]

For more information on implementing waterfall dialogs, see how to [implement sequential conversation flow](bot-builder-dialog-manage-complex-conversation-flow.md).

# [JavaScript](#tab/javascript)

To use dialogs, your project needs to install the **botbuilder-dialogs** npm package.

**dialogs/userProfileDialog.js**

Here the `UserProfileDialog` class extends `ComponentDialog`.

[!code-javascript[Class](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/dialogs/userProfileDialog.js?range=28)]

Within the constructor, the `AddDialog` method adds dialogs and prompts to the component dialog. The first item you add with this method is set as the initial dialog, but you can change this by explicitly setting the `InitialDialogId` property. When you start a component dialog, it will start its _initial dialog_.

[!code-javascript[Constructor](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/dialogs/userProfileDialog.js?range=29-51)]

This is the implementation of the first step of the waterfall dialog.

[!code-javascript[First step](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/dialogs/userProfileDialog.js?range=70-77)]

For more information on implementing waterfall dialogs, see how to [implement sequential conversation flow](bot-builder-dialog-manage-complex-conversation-flow.md).

# [Python](#tab/python)

To use dialogs, install the **botbuilder-dialogs** and **botbuilder-ai** PyPI packages by running `pip install botbuilder-dialogs` and `pip install botbuilder-ai` from a terminal.

**dialogs/user_profile_dialog.py**

Here the `UserProfileDialog` class extends `ComponentDialog`.

[!code-python[Class](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/dialogs/user_profile_dialog.py?range=25)]

Within the constructor, the `add_dialog` method adds dialogs and prompts to the component dialog. The first item you add with this method is set as the initial dialog, but you can change this by explicitly setting the `initial_dialog_id` property. When you start a component dialog, it will start its _initial dialog_.

[!code-python[Constructor](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/dialogs/user_profile_dialog.py?range=25-57)]

This is the implementation of the first step of the waterfall dialog.

[!code-python[First step](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/dialogs/user_profile_dialog.py?range=59-71)]

For more information on implementing waterfall dialogs, see how to [implement sequential conversation flow](bot-builder-dialog-manage-complex-conversation-flow.md).

<!--
# [LG](#tab/lg)

To use dialogs, install the **Microsoft.Bot.Builder.Dialogs** NuGet package.

**Dialogs\UserProfileDialog.cs**

Here the `UserProfileDialog` class derives from the `ComponentDialog` class.

[!code-csharp[Class](~/../botbuilder-samples/experimental/language-generation/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=15)]

Within the constructor, the `AddDialog` method adds dialogs and prompts to the component dialog. The first item you add with this method is set as the initial dialog, but you can change this by explicitly setting the `InitialDialogId` property. When you start a component dialog, it will start its *initial dialog*.
Notice the `paths` definition referencing the `UserProfileDialog.lg` containing the LG template items.

[!code-csharp[Constructor](~/../botbuilder-samples/experimental/language-generation/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=15-47&highlight=10)]

This is the implementation of the first step of the waterfall dialog,
Notice the use of the template item `AskForName`.

[!code-csharp[First step](~/../botbuilder-samples/experimental/language-generation/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=62-67&highlight=5)]

The prompt dialog is generated using the `GenerateActivity` function and passing to it the LG `AskForName` parameter defined in the template file [UserProfileDialog.LG](https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/language-generation/csharp_dotnetcore/05.multi-turn-prompt/Resources/UserProfileDialog.LG). Similar prompt generation is applicable to all the items in the template.

For more information on implementing waterfall dialogs, see how to [implement sequential conversation flow](bot-builder-dialog-manage-complex-conversation-flow.md).
-->

---

At run-time, the component dialog maintains its own dialog stack. When the component dialog is started:

- An instance is created and added to the outer dialog stack
- It creates an inner dialog stack that it adds to its state
- It starts its initial dialog and adds that to the inner dialog stack.

From the parent context, it will look like the component is the active dialog. From inside the component, it will look like the initial dialog is the active dialog.

### Implement the rest of the dialog and add it to the bot

In the outer dialog set, the one to which you add the component dialog, the component dialog has the ID that you created it with. In the outer set, the component looks like a single dialog, much like prompts do.

To use a component dialog, add an instance of it to the bot's dialog set - this is the outer dialog set.

# [C#](#tab/csharp)

**Bots\DialogBot.cs**

In the sample, this is done using the `RunAsync` method that is called from the bot's `OnMessageActivityAsync` method.

[!code-csharp[OnMessageActivityAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Bots/DialogBot.cs?range=42-48&highlight=6)]

# [JavaScript](#tab/javascript)

**dialogs/userProfileDialog.js**

In the sample, we've added a `run` method to the user profile dialog.

[!code-javascript[run method](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/dialogs/userProfileDialog.js?range=59-68)]

**bots/dialogBot.js**

The `run` method is called from the bot's `onMessage` method.

[!code-javascript[onMessage](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/bots/dialogBot.js?range=24-31&highlight=5)]

# [Python](#tab/python)

**helpers/dialog_helper.py**

In the sample, we've added a `run_dialog` method to the user profile dialog.

[!code-python[DialogHelper.run_dialog](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/helpers/dialog_helper.py?range=8-19)]

The `run_dialog` method that is called from the bot's `on_message_activity` method.

**bots/dialog_bot.py**
[!code-python[om_message_activity](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/bots/dialog_bot.py?range=46-51&highlight=2-6)]

<!--
# [LG](#tab/lg)

**Bots\DialogBot.cs**

In the sample, this is done using the `Run` method that is called from the bot's `OnMessageActivityAsync` method.

[!code-csharp[OnMessageActivityAsync](~/../botbuilder-samples/experimental/language-generation/csharp_dotnetcore/05.multi-turn-prompt/Bots/DialogBot.cs?range=42-48&highlight=6)]
-->

---

## To test the bot

1. If you have not done so already, install the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme).
1. Run the sample locally on your machine.
1. Start the emulator, connect to your bot, and send messages as shown below.

![Sample run of the multi-turn prompt dialog](../media/emulator-v4/multi-turn-prompt.png)

## Additional information

### How cancellation works for component dialogs

If you call _cancel all dialogs_ from the component dialog's context, the component dialog will cancel all of the dialogs on its inner stack and then end, returning control to the next dialog on the outer stack.

If you call _cancel all dialogs_ from the outer context, the component is cancelled, along with the rest of the dialogs on the outer context.

Keep this in mind when managing nested component dialogs in your bot.

## Next steps

Learn how to create complex conversations that branch and loop.

> [!div class="nextstepaction"]
> [Handle user interruptions](bot-builder-dialog-manage-complex-conversation-flow.md)

<!-- Footnote-style links -->

<!--concepts-->
[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md
<!--how to-->
[simple-flow]: bot-builder-dialog-manage-conversation-flow.md
[prompting]: bot-builder-prompts.md
[component-dialogs]: bot-builder-compositcontrol.md
<!--samples-->
[cs-sample]: https://aka.ms/cs-multi-prompts-sample
[js-sample]: https://aka.ms/js-multi-prompts-sample
[python-sample]: https://aka.ms/python-multi-prompts-sample

<!--[lg-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/language-generation/csharp_dotnetcore/05.multi-turn-prompt-->

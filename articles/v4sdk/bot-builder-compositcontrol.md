---
title: Manage dialog complexity
description: Learn how to modularize your dialog complexity using component dialogs in the Bot Framework SDK.
keywords: composite control, modular bot logic
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

# Manage dialog complexity

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

With component dialogs, you can create independent dialogs to handle specific scenarios, breaking a large dialog set into more manageable pieces. Each of these pieces has its own dialog set, and avoids any name collisions with the dialog sets outside of it. Component dialogs are reusable in that they can be:

- Added to another `ComponentDialog` or `DialogSet` in your bot.
- Exported as a part of a package.
- Used within other bots.

[!INCLUDE [java-python-sunset-alert](../includes/java-python-sunset-alert.md)]

## Prerequisites

- Knowledge of [bot basics][concept-basics], the [dialogs library][concept-dialogs], and how to [manage conversations][simple-flow].
- A copy of the multi-turn prompt sample in [**C#**][cs-sample], [**JavaScript**][js-sample], [**Java**][java-sample], or [**Python**][python-sample].

## About the sample

In the multi-turn prompt sample, we use a waterfall dialog, a few prompts, and a component dialog to create an interaction that asks the user a series of questions. The code uses a dialog to cycle through these steps:

| Steps                                          | Prompt type                                                                         |
|:-----------------------------------------------|:------------------------------------------------------------------------------------|
| Ask the user for their mode of transportation  | Choice prompt                                                                       |
| Ask the user for their name                    | Text prompt                                                                         |
| Ask the user if they want to provide their age | Confirm prompt                                                                      |
| If they answered yes, ask for their age        | Number prompt with validation to only accept ages greater than 0 and less than 150. |
| Ask if the collected information is "ok"       | Reuse Confirm prompt                                                                |

Finally, if they answered yes, display the collected information; otherwise, tell the user that their information won't be kept.

## Implement your component dialog

In the multi-turn prompt sample, we use a _waterfall dialog_, a few _prompts_, and a _component dialog_ to create an interaction that asks the user a series of questions.

A component dialog encapsulates one or more dialogs. The component dialog has an inner dialog set, and the dialogs and prompts that you add to this inner dialog set have their own IDs, visible only from within the component dialog.

### [C#](#tab/csharp)

To use dialogs, install the **Microsoft.Bot.Builder.Dialogs** NuGet package.

**Dialogs\UserProfileDialog.cs**

Here the `UserProfileDialog` class derives from the `ComponentDialog` class.

[!code-csharp[Class](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=16)]

Within the constructor, the `AddDialog` method adds dialogs and prompts to the component dialog. The first item you add with this method is set as the initial dialog. You can change the initial dialog by explicitly setting the `InitialDialogId` property. When you start a component dialog, it will start its _initial dialog_.

[!code-csharp[Constructor](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=20-47)]

The following code represents the first step of the waterfall dialog.

[!code-csharp[First step](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Dialogs/UserProfileDialog.cs?range=61-66)]

For more information on implementing waterfall dialogs, see how to [implement sequential conversation flow](bot-builder-dialog-manage-complex-conversation-flow.md).

### [JavaScript](#tab/javascript)

To use dialogs, your project needs to install the **botbuilder-dialogs** npm package.

**dialogs/userProfileDialog.js**

Here the `UserProfileDialog` class extends `ComponentDialog`.

[!code-javascript[Class](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/dialogs/userProfileDialog.js?range=28)]

Within the constructor, the `AddDialog` method adds dialogs and prompts to the component dialog. The first item you add with this method is set as the initial dialog. You can change the initial dialog by explicitly setting the `InitialDialogId` property. When you start a component dialog, it will start its _initial dialog_.

[!code-javascript[Constructor](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/dialogs/userProfileDialog.js?range=29-51)]

The following code represents the first step of the waterfall dialog.

[!code-javascript[First step](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/dialogs/userProfileDialog.js?range=70-77)]

For more information on implementing waterfall dialogs, see how to [implement sequential conversation flow](bot-builder-dialog-manage-complex-conversation-flow.md).

### [Java](#tab/java)

**UserProfileDialog.java**

Here the `UserProfileDialog` class derives from the `ComponentDialog` class.

[!code-java[Class](~/../botbuilder-samples/samples/java_springboot/05.multi-turn-prompt/src/main/java/com/microsoft/bot/sample/multiturnprompt/UserProfileDialog.java?range=31)]

Within the constructor, the `addDialog` method adds dialogs and prompts to the component dialog. The first item you add with this method is set as the initial dialog. You can change the initial dialog by calling the `setInitialDialogId` method and provide the name of the initial dialog. When you start a component dialog, it will start its _initial dialog_.

[!code-java[Constructor](~/../botbuilder-samples/samples/java_springboot/05.multi-turn-prompt/src/main/java/com/microsoft/bot/sample/multiturnprompt/UserProfileDialog.java?range=34-59)]

The following code represents the first step of the waterfall dialog.

[!code-java[First step](~/../botbuilder-samples/samples/java_springboot/05.multi-turn-prompt/src/main/java/com/microsoft/bot/sample/multiturnprompt/UserProfileDialog.java?range=71-77)]

For more information on implementing waterfall dialogs, see how to [implement sequential conversation flow](bot-builder-dialog-manage-complex-conversation-flow.md).

### [Python](#tab/python)

To use dialogs, install the **botbuilder-dialogs** and **botbuilder-ai** PyPI packages by running `pip install botbuilder-dialogs` and `pip install botbuilder-ai` from a terminal.

**dialogs/user_profile_dialog.py**

Here the `UserProfileDialog` class extends `ComponentDialog`.

[!code-python[Class](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/dialogs/user_profile_dialog.py?range=25)]

Within the constructor, the `add_dialog` method adds dialogs and prompts to the component dialog. The first item you add with this method is set as the initial dialog. You can change the initial dialog by explicitly setting the `initial_dialog_id` property. When you start a component dialog, it will start its _initial dialog_.

[!code-python[Constructor](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/dialogs/user_profile_dialog.py?range=25-57)]

The following code represents the first step of the waterfall dialog.

[!code-python[First step](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/dialogs/user_profile_dialog.py?range=59-71)]

For more information on implementing waterfall dialogs, see how to [implement sequential conversation flow](bot-builder-dialog-manage-complex-conversation-flow.md).

---

At run-time, the component dialog maintains its own dialog stack. When the component dialog is started:

- An instance is created and added to the outer dialog stack
- It creates an inner dialog stack that it adds to its state
- It starts its initial dialog and adds that to the inner dialog stack.

The parent context sees the component as the active dialog. However, to the context inside the component, it looks like the initial dialog is the active dialog.

## Call the dialog from your bot

In the outer dialog set, the one to which you added the component dialog, the component dialog has the ID that you created it with. In the outer set, the component looks like a single dialog, much like prompts do.

To use a component dialog, add an instance of it to the bot's dialog set.

### [C#](#tab/csharp)

**Bots\DialogBot.cs**

In the sample, this is done using the `RunAsync` method that is called from the bot's `OnMessageActivityAsync` method.

[!code-csharp[OnMessageActivityAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/05.multi-turn-prompt/Bots/DialogBot.cs?range=42-48&highlight=6)]

### [JavaScript](#tab/javascript)

**dialogs/userProfileDialog.js**

In the sample, we've added a `run` method to the user profile dialog.

[!code-javascript[run method](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/dialogs/userProfileDialog.js?range=59-68)]

**bots/dialogBot.js**

The `run` method is called from the bot's `onMessage` method.

[!code-javascript[onMessage](~/../botbuilder-samples/samples/javascript_nodejs/05.multi-turn-prompt/bots/dialogBot.js?range=24-31&highlight=5)]

### [Java](#tab/java)

**DialogBot.java**

In the sample, this is done using the `run` method that is called from the bot's `onMessageActivity` method.

[!code-java[OnMessageActivity](~/../botbuilder-samples/samples/java_springboot/05.multi-turn-prompt/src/main/java/com/microsoft/bot/sample/multiturnprompt/DialogBot.java?range=50-58&highlight=8)]

### [Python](#tab/python)

**helpers/dialog_helper.py**

In the sample, we've added a `run_dialog` method to the user profile dialog.

[!code-python[DialogHelper.run_dialog](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/helpers/dialog_helper.py?range=8-19)]

The `run_dialog` method that is called from the bot's `on_message_activity` method.

**bots/dialog_bot.py**
[!code-python[om_message_activity](~/../botbuilder-samples/samples/python/05.multi-turn-prompt/bots/dialog_bot.py?range=46-51&highlight=2-6)]

---

## Test your bot

1. If you haven't done so already, install the [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md).
1. Run the sample locally on your machine.
1. Start the Emulator, connect to your bot, and send messages as shown below.

:::image type="content" source="../media/emulator-v4/multi-turn-prompt.png" alt-text="Sample transcript from the multi-turn prompt dialog.":::

## Additional information

### How cancellation works for component dialogs

If you call _cancel all dialogs_ from the component dialog's context, the component dialog will cancel all of the dialogs on its inner stack and then end, returning control to the next dialog on the outer stack.

If you call _cancel all dialogs_ from the outer context, the component is canceled, along with the rest of the dialogs on the outer context.

## Next steps

Learn how to create complex conversations that branch and loop.

> [!div class="nextstepaction"]
> [Handle user interruptions](bot-builder-dialog-manage-complex-conversation-flow.md)

[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md

[simple-flow]: bot-builder-dialog-manage-conversation-flow.md
[prompting]: bot-builder-prompts.md
[component-dialogs]: bot-builder-compositcontrol.md

[cs-sample]: https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/05.multi-turn-prompt
[js-sample]: https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/05.multi-turn-prompt
[java-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/05.multi-turn-prompt
[python-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/05.multi-turn-prompt
[lg-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/language-generation/05.multi-turn-prompt

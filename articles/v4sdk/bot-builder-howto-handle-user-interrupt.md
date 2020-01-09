---
title: Handle user interruptions - Bot Service
description: Learn how to handle user interrupt and direct conversation flow.
keywords: interrupt, interruptions, switching topic, break
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 11/05/2019
monikerRange: 'azure-bot-service-4.0'
---

# Handle user interruptions

[!INCLUDE[applies-to](../includes/applies-to.md)]

Handling interruptions is an important aspect of a robust bot. Users will not always follow your defined conversation flow, step by step. They may try to ask a question in the middle of the process, or simply want to cancel it instead of completing it. In this topic, we will explore some common ways to handle user interruptions in your bot.

## Prerequisites

- Knowledge of [bot basics][concept-basics], [managing state][concept-state], the [dialogs library][concept-dialogs], and how to [reuse dialogs][component-dialogs].
- A copy of the core bot sample in either [**CSharp**][cs-sample], [**JavaScript**][js-sample] or [**Python**][python-sample].

## About this sample

The sample used in this article models a flight booking bot that uses dialogs to get flight information from the user. At any time during the conversation with the bot, the user can issue _help_ or _cancel_ commands to cause an interruption. There are two types of interruptions we handle here:

- **Turn level**: Bypass processing at the turn level but leave the dialog on the stack with the information that was provided. In the next turn, continue from where we left off. 
- **Dialog level**: Cancel the processing completely, so the bot can start all over again.

## Define and implement the interruption logic

First, we define and implement the _help_ and _cancel_ interruptions.

# [C#](#tab/csharp)

To use dialogs, install the **Microsoft.Bot.Builder.Dialogs** NuGet package.

**Dialogs\CancelAndHelpDialog.cs**

We begin by implementing the `CancelAndHelpDialog` class to handle user interruptions.

[!code-csharp[Class signature](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Dialogs/CancelAndHelpDialog.cs?range=12)]

In the `CancelAndHelpDialog` class the `OnContinueDialogAsync` method calls the `InerruptAsync` method to check if the user has interrupted the normal flow. If the flow is interrupted, base class methods are called; otherwise, the return value from the `InterruptAsync` is returned.

[!code-csharp[Overrides](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Dialogs/CancelAndHelpDialog.cs?range=22-31)]

If the user types "help", the `InterrupAsync` method sends a message and then calls `DialogTurnResult (DialogTurnStatus.Waiting)` to indicate that the dialog on top is waiting for a response from the user. In this way, the conversation flow is interrupted for a turn only, and in the next turn we continue from where we left off.

If the user types "cancel", it calls `CancelAllDialogsAsync` on its inner dialog context, which clears its dialog stack and causes it to exit with a cancelled status and no result value. To the `MainDialog` (shown later on), it will appear that the booking dialog ended and returned null, similar to when the user chooses not to confirm their booking.

[!code-csharp[Interrupt](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Dialogs/CancelAndHelpDialog.cs?range=33-56)]

# [JavaScript](#tab/javascript)

To use dialogs, install the **botbuilder-dialogs** npm package.

**dialogs/cancelAndHelpDialog.js**

We begin by implementing the `CancelAndHelpDialog` class to handle user interruptions.

[!code-javascript[Class signature](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/dialogs/cancelAndHelpDialog.js?range=11)]

In the `CancelAndHelpDialog` class the `onContinueDialog` method calls the `interrupt` method to check if the user has interrupted the normal flow. If the flow is interrupted, base class methods are called; otherwise, the return value from the `interrupt` is returned.

[!code-javascript[Overrides](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/dialogs/cancelAndHelpDialog.js?range=12-18)]

If the user types "help", the `interrupt` method sends a message and then returns a `{ status: DialogTurnStatus.waiting }` object to indicate that the dialog on top is waiting for a response from the user. In this way, the conversation flow is interrupted for a turn only, and in the next turn we continue from where we left off.

If the user types "cancel", it calls `cancelAllDialogs` on its inner dialog context, which clears its dialog stack and causes it to exit with a cancelled status and no result value. To the `MainDialog` (shown later on), it will appear that the booking dialog ended and returned null, similar to when the user chooses not to confirm their booking.

[!code-javascript[Interrupt](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/dialogs/cancelAndHelpDialog.js??range=20-39)]


## [Python](#tab/python)

To use dialogs, install the `botbuilder-dialogs` package and make sure that the sample `requirements.txt` file contains the proper reference such as `botbuilder-dialogs>=4.5.0`. 
For more information, about installing the packages, see the samples repository [README](https://github.com/microsoft/botbuilder-python) file.
> [!NOTE]
> Doing `pip install botbuilder-dialogs` will also install `botbuilder-core`, `botbulder-connector`, and `botbuilder-schema`.

**dialogs/cancel-and-help-dialog.py**

We begin by implementing the `CancelAndHelpDialog` class to handle user interruptions.

[!code-python[class signature](~/../botbuilder-python/samples/python/13.core-bot/dialogs/cancel_and_help_dialog.py?range=14)]

In the `CancelAndHelpDialog` class the `on_continue_dialog` method calls the `interrupt` method to check if the user has interrupted the normal flow. If the flow is interrupted, base class methods are called; otherwise, the return value from the `InterruptAsync` is returned.

[!code-python[dialog](~/../botbuilder-python/samples/python/13.core-bot/dialogs/cancel_and_help_dialog.py?range=18-23)]

If the user types *help* or *?*, the `interrupt` method sends a message and then calls 
`DialogTurnResult(DialogTurnStatus.Waiting)` to indicate that the dialog on top of the stack is waiting for a response from the user. In this way, the conversation flow is interrupted for a turn only, and in the next turn we continue from where we left off.

If the user types *cancel* or *quit*, it calls `cancel_all_dialogs()` on its inner dialog context, which clears its dialog stack and causes it to exit with a cancelled status and no result value. To the `MainDialog`, shown later, it will appear that the booking dialog ended and returned null, similar to when the user chooses not to confirm their booking.

[!code-python[interrupt](~/../botbuilder-python/samples/python/13.core-bot/dialogs/cancel_and_help_dialog.py?range=25-47)]

---

## Check for interruptions each turn

Now that we've covered how the interrupt handling class works, let's step back and look at what happens when the bot receives a new message from the user.

# [C#](#tab/csharp)

**Dialogs\MainDialog.cs**

As the new message activity arrives, the bot runs the `MainDialog`. The `MainDialog` prompts the user for what it can help with. And then it starts the `BookingDialog` in the `MainDialog.ActStepAsync` method, with a call to `BeginDialogAsync` as shown below.

[!code-csharp[ActStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Dialogs/MainDialog.cs?range=58-101&highlight=6,26)]

Next, in the `FinalStepAsync` method of the `MainDialog` class, the booking dialog ended and the booking is considered to be complete or cancelled.

[!code-csharp[FinalStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Dialogs/MainDialog.cs?range=130-150)]

The code in `BookingDialog` is not shown here as it is not directly related to interruption handling. It is used to prompt users for booking details. You can find that code in **Dialogs\BookingDialogs.cs**.

# [JavaScript](#tab/javascript)

**dialogs/mainDialog.js**

As the new message activity arrives, the bot runs the `MainDialog`. The `MainDialog` prompts the user for what it can help with. And then it starts the `bookingDialog` in the `MainDialog.actStep` method, with a call to `beginDialog` as shown below.

[!code-javascript[Act step](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/dialogs/mainDialog.js?range=71-115&highlight=6,27)]

Next, in the `finalStep` method of the `MainDialog` class, the booking dialog ended and the booking is considered to be complete or cancelled.

[!code-javascript[Final step](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/dialogs/mainDialog.js?range=142-159)]

The code in `BookingDialog` is not shown here as it is not directly related to interruption handling. It is used to prompt users for booking details. You can find that code in **dialogs/bookingDialogs.js**.

## [Python](#tab/python)

**dialogs/main_dialog.py**

As the new message activity arrives, the bot runs the `MainDialog`. The `MainDialog` prompts the user for what it can help with. And then it starts the `bookingDialog` in the `MainDialog.act_step` method, with a call to `begin_dialog` as shown below.

[!code-python[act step](~/../botbuilder-python/samples/python/13.core-bot/dialogs/main_dialog.py?range=63-100&highlight=4-5,20)]

Next, in the `final_step` method of the `MainDialog` class, the booking dialog ended and the booking is considered to be complete or cancelled.

[!code-python[final step](~/../botbuilder-python/samples/python/13.core-bot/dialogs/main_dialog.py?range=102-118)]

---

## Handle unexpected errors

Next, we deal with any unhandled exceptions that might occur.

# [C#](#tab/csharp)

**AdapterWithErrorHandler.cs**

In our sample, the adapter's `OnTurnError` handler receives any exceptions thrown by your bot's turn logic. If there is an exception thrown, the handler deletes the conversation state for the current conversation to prevent the bot from getting stuck in a error-loop caused by being in a bad state.

[!code-csharp[AdapterWithErrorHandler](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/AdapterWithErrorHandler.cs?range=19-50)]

# [JavaScript](#tab/javascript)

**index.js**

In our sample, the adapter's `onTurnError` handler receives any exceptions thrown by your bot's turn logic. If there is an exception thrown, the handler deletes the conversation state for the current conversation to prevent the bot from getting stuck in a error-loop caused by being in a bad state.

[!code-javascript[AdapterWithErrorHandler](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/index.js?range=35-57)]

## [Python](#tab/python)

**adapter_with_error_handler.py**

In our sample, the adapter's `on_error` handler receives any exceptions thrown by your bot's turn logic. If there is an exception thrown, the handler deletes the conversation state for the current conversation to prevent the bot from getting stuck in a error-loop caused by being in a bad state.

[!code-python[adapter_with_error_handler](~/../botbuilder-python/samples/python/13.core-bot/adapter_with_error_handler.py?range=15-54)]

---

## Register services

# [C#](#tab/csharp)

**Startup.cs**

Finally, in `Startup.cs`, the bot is created as a transient, and on every turn, a new instance of the bot is created.

[!code-csharp[Add transient bot](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Startup.cs?range=43-44)]

For reference, here are the class definitions that are used in the call to create the bot above.

[!code-csharp[MainDialog signature](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Dialogs/MainDialog.cs?range=17)]
[!code-csharp[DialogAndWelcomeBot signature](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Bots/DialogAndWelcomeBot.cs?range=16)]
[!code-csharp[DialogBot signature](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Bots/DialogBot.cs?range=18)]

# [JavaScript](#tab/javascript)

**index.js**

Finally, in `index.js`, the bot is created.

[!code-javascript[Create bot](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/index.js?range=75-78)]

For reference, here are the class definitions that are used in the call to create the bot above.

[!code-javascript[MainDialog signature](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/dialogs/mainDialog.js?range=11)]
[!code-javascript[DialogAndWelcomeBot signature](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/bots/dialogAndWelcomeBot.js?range=8)]
[!code-javascript[DialogBot signature](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/bots/dialogBot.js?range=6)]

## [Python](#tab/python)

**app.py**
Finally, in `app.py`, the bot is created.

[!code-python[create bot](~/../botbuilder-python/samples/python/13.core-bot/app.py?range=44-48)]

For reference, here are the class definitions that are used in the call to create the bot.

[!code-python[main dialog](~/../botbuilder-python/samples/python/13.core-bot/dialogs/main_dialog.py?range=20)]

[!code-python[dialog and welcome](~/../botbuilder-python/samples/python/13.core-bot/bots/dialog_and_welcome_bot.py?range=21)]

[!code-python[dialog](~/../botbuilder-python/samples/python/13.core-bot/bots/dialog_bot.py?range=9)]

---

## To test the bot

1. If you have not done so already, install the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme).
1. Run the sample locally on your machine.
1. Start the emulator, connect to your bot, and send messages as shown below.

<!--![test dialog prompt sample](~/media/emulator-v4/test-dialog-prompt.png)-->

## Additional information

- The [authentication sample](https://aka.ms/logout) shows how to handle logout which uses similar pattern shown here for handling interruptions.

- You should send a default response instead of doing nothing and leaving the user wondering what is going on. The default response should tell the user what commands the bot understands so the user can get back on track.

- At any point in the turn, the turn context's _responded_ property indicates whether the bot has sent a message to the user this turn. Before the turn ends, your bot should send some message to the user, even if it is a simple acknowledgement of their input.

<!-- Footnote-style links -->

[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md

[using-luis]: bot-builder-howto-v4-luis.md
[using-qna]: bot-builder-howto-qna.md

[simple-flow]: bot-builder-dialog-manage-conversation-flow.md
[prompting]: bot-builder-prompts.md
[component-dialogs]: bot-builder-compositcontrol.md

[cs-sample]: https://aka.ms/cs-core-sample
[js-sample]: https://aka.ms/js-core-sample
[python-sample]: https://aka.ms/bot-core-python-sample-code

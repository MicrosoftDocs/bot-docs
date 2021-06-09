---
title: Handle user interruptions - Bot Service
description: Learn how bots handle user interruptions. See how to implement help and cancel interruptions, how to create and test bots, and how to handle unexpected errors.
keywords: interrupt, interruptions, switching topic, break
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 04/02/2021
monikerRange: 'azure-bot-service-4.0'
---

# Handle user interruptions

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Handling interruptions is an important aspect of a robust bot. Users will not always follow your defined conversation flow, step by step. They may try to ask a question in the middle of the process, or simply want to cancel it instead of completing it. In this topic, this topic describes some common ways to handle user interruptions in your bot.

## Prerequisites

- Knowledge of [bot basics][concept-basics], [managing state][concept-state], the [dialogs library][concept-dialogs], and how to [reuse dialogs][component-dialogs].
- A copy of the core bot sample in either [**C#**][cs-sample], [**JavaScript**][js-sample], [**Java**][java-sample] or [**Python**][python-sample].

## About this sample

The sample used in this article models a flight booking bot that uses dialogs to get flight information from the user. At any time during the conversation with the bot, the user can issue _help_ or _cancel_ commands to cause an interruption. There are two types of interruptions handled:

- **Turn level**: Bypass processing at the turn level but leave the dialog on the stack with the information that was provided. In the next turn, continue from where the conversation left off.
- **Dialog level**: Cancel the processing completely, so the bot can start all over again.

## Define and implement the interruption logic

First, define and implement the _help_ and _cancel_ interruptions.

# [C#](#tab/csharp)

To use dialogs, install the **Microsoft.Bot.Builder.Dialogs** NuGet package.

**Dialogs\CancelAndHelpDialog.cs**

Implement the `CancelAndHelpDialog` class to handle user interruptions. The cancellable dialogs, `BookingDialog` and `DateResolverDialog` derive from this class.

[!code-csharp[Class signature](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Dialogs/CancelAndHelpDialog.cs?range=12)]

In the `CancelAndHelpDialog` class the `OnContinueDialogAsync` method calls the `InterruptAsync` method to check if the user has interrupted the normal flow. If the flow is interrupted, base class methods are called; otherwise, the return value from the `InterruptAsync` is returned.

[!code-csharp[Overrides](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Dialogs/CancelAndHelpDialog.cs?range=22-31)]

If the user types "help", the `InterruptAsync` method sends a message and then calls `DialogTurnResult (DialogTurnStatus.Waiting)` to indicate that the dialog on top is waiting for a response from the user. In this way, the conversation flow is interrupted for a turn only, and the next turn continues from where the conversation left off.

If the user types "cancel", it calls `CancelAllDialogsAsync` on its inner dialog context, which clears its dialog stack and causes it to exit with a cancelled status and no result value. To the `MainDialog` (shown later on), it will appear that the booking dialog ended and returned null, similar to when the user chooses not to confirm their booking.

[!code-csharp[Interrupt](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Dialogs/CancelAndHelpDialog.cs?range=33-56)]

# [JavaScript](#tab/javascript)

To use dialogs, install the **botbuilder-dialogs** npm package.

**dialogs/cancelAndHelpDialog.js**

Implement the `CancelAndHelpDialog` class to handle user interruptions. The cancellable dialogs, `BookingDialog` and `DateResolverDialog` extend this class.

[!code-javascript[Class signature](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/dialogs/cancelAndHelpDialog.js?range=11)]

In the `CancelAndHelpDialog` class the `onContinueDialog` method calls the `interrupt` method to check if the user has interrupted the normal flow. If the flow is interrupted, base class methods are called; otherwise, the return value from the `interrupt` is returned.

[!code-javascript[Overrides](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/dialogs/cancelAndHelpDialog.js?range=12-18)]

If the user types "help", the `interrupt` method sends a message and then returns a `{ status: DialogTurnStatus.waiting }` object to indicate that the dialog on top is waiting for a response from the user. In this way, the conversation flow is interrupted for a turn only, and the next turn continues from where the conversation left off.

If the user types "cancel", it calls `cancelAllDialogs` on its inner dialog context, which clears its dialog stack and causes it to exit with a cancelled status and no result value. To the `MainDialog` (shown later on), it will appear that the booking dialog ended and returned null, similar to when the user chooses not to confirm their booking.

[!code-javascript[Interrupt](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/dialogs/cancelAndHelpDialog.js?range=20-39)]

# [Java](#tab/java)

**CancelAndHelpDialog.java**

Implement the `CancelAndHelpDialog` class to handle user interruptions. The cancellable dialogs, `BookingDialog` and `DateResolverDialog` derive from this class.

[!code-java[Class signature](~/../botbuilder-samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/CancelAndHelpDialog.java?range=20)]

In the `CancelAndHelpDialog` class the `onContinueDialog` method calls the `interrupt` method to check if the user has interrupted the normal flow. If the flow is interrupted, base class methods are called; otherwise, the return value from the `interrupt` is returned.

[!code-java[Overrides](~/../botbuilder-samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/CancelAndHelpDialog.java?range=43-51)]

If the user types "help", the `interrupt` method sends a message and then calls `DialogTurnResult(DialogTurnStatus.WAITING)` to indicate that the dialog on top is waiting for a response from the user. In this way, the conversation flow is interrupted for a turn only, and the next turn continues from where the conversation left off.

If the user types "cancel", it calls `cancelAllDialogs` on its inner dialog context, which clears its dialog stack and causes it to exit with a cancelled status and no result value. To the `MainDialog` (shown later on), it will appear that the booking dialog ended and returned null, similar to when the user chooses not to confirm their booking.

[!code-java[Interrupt](~/../botbuilder-samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/CancelAndHelpDialog.java?range=53-79)]


## [Python](#tab/python)

To use dialogs, install the `botbuilder-dialogs` package and make sure that the sample `requirements.txt` file contains the proper reference such as `botbuilder-dialogs>=4.5.0`.
For more information, about installing the packages, see the samples repository [README](https://github.com/microsoft/botbuilder-python) file.
> [!NOTE]
> Running `pip install botbuilder-dialogs` will also install `botbuilder-core`, `botbuilder-connector`, and `botbuilder-schema`.

**dialogs/cancel-and-help-dialog.py**

Implement the `CancelAndHelpDialog` class to handle user interruptions. The cancellable dialogs, `BookingDialog` and `DateResolverDialog` derive from this class.

[!code-python[class signature](~/../botbuilder-samples/samples/python/13.core-bot/dialogs/cancel_and_help_dialog.py?range=14)]

In the `CancelAndHelpDialog` class the `on_continue_dialog` method calls the `interrupt` method to check if the user has interrupted the normal flow. If the flow is interrupted, base class methods are called; otherwise, the return value from the `interrupt` is returned.

[!code-python[dialog](~/../botbuilder-samples/samples/python/13.core-bot/dialogs/cancel_and_help_dialog.py?range=18-23)]

If the user types *help* or *?*, the `interrupt` method sends a message and then calls `DialogTurnResult(DialogTurnStatus.Waiting)` to indicate that the dialog on top of the stack is waiting for a response from the user. In this way, the conversation flow is interrupted for a turn only, and the next turn continues from where the conversation left off.

If the user types *cancel* or *quit*, it calls `cancel_all_dialogs()` on its inner dialog context, which clears its dialog stack and causes it to exit with a cancelled status and no result value. To the `MainDialog`, shown later, it will appear that the booking dialog ended and returned null, similar to when the user chooses not to confirm their booking.

[!code-python[interrupt](~/../botbuilder-samples/samples/python/13.core-bot/dialogs/cancel_and_help_dialog.py?range=25-47)]

---

## Check for interruptions each turn

Once the interrupt handling class is implemented, review what happens when this bot receives a new message from the user.

# [C#](#tab/csharp)

**Dialogs\MainDialog.cs**

As the new message activity arrives, the bot runs the `MainDialog`. The `MainDialog` prompts the user for what it can help with. And then it starts the `BookingDialog` in the `MainDialog.ActStepAsync` method, with a call to `BeginDialogAsync` as shown below.

[!code-csharp[ActStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Dialogs/MainDialog.cs?range=59-102&highlight=6,26)]

Next, in the `FinalStepAsync` method of the `MainDialog` class, the booking dialog ended and the booking is considered to be complete or cancelled.

[!code-csharp[FinalStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Dialogs/MainDialog.cs?range=131-151)]

The code in `BookingDialog` is not shown here as it is not directly related to interruption handling. It is used to prompt users for booking details. You can find that code in **Dialogs\BookingDialogs.cs**.

# [JavaScript](#tab/javascript)

**dialogs/mainDialog.js**

As the new message activity arrives, the bot runs the `MainDialog`. The `MainDialog` prompts the user for what it can help with. And then it starts the `bookingDialog` in the `MainDialog.actStep` method, with a call to `beginDialog` as shown below.

[!code-javascript[Act step](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/dialogs/mainDialog.js?range=73-117&highlight=6,27)]

Next, in the `finalStep` method of the `MainDialog` class, the booking dialog ended and the booking is considered to be complete or cancelled.

[!code-javascript[Final step](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/dialogs/mainDialog.js?range=144-161)]

The code in `BookingDialog` is not shown here as it is not directly related to interruption handling. It is used to prompt users for booking details. You can find that code in **dialogs/bookingDialogs.js**.

# [Java](#tab/java)

**MainDialog.java**

As the new message activity arrives, the bot runs the `MainDialog`. The `MainDialog` prompts the user for what it can help with. And then, it starts the `BookingDialog` in the `MainDialog.actStep` method, with a call to `beginDialog` as shown below.

[!code-java[ActStep](~/../botbuilder-samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/MainDialog.java?range=100-156&highlight=4,27)]

Next, in the `finalStep` method of the `MainDialog` class, the booking dialog ended and the booking is considered to be complete or cancelled.

[!code-java[FinalStep](~/../botbuilder-samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/MainDialog.java?range=207-231)]

The code in `BookingDialog` is not shown here as it is not directly related to interruption handling. It is used to prompt users for booking details. You can find that code in **BookingDialogs.java**.


## [Python](#tab/python)

**dialogs/main_dialog.py**

As the new message activity arrives, the bot runs the `MainDialog`. The `MainDialog` prompts the user for what it can help with. And then it starts the `bookingDialog` in the `act_step` method, with a call to `begin_dialog` as shown below.

[!code-python[act step](~/../botbuilder-samples/samples/python/13.core-bot/dialogs/main_dialog.py?range=63-100&highlight=4-6,20)]

Next, in the `final_step` method of the `MainDialog` class, the booking dialog ended and the booking is considered to be complete or cancelled.

[!code-python[final step](~/../botbuilder-samples/samples/python/13.core-bot/dialogs/main_dialog.py?range=102-118)]

---

## Handle unexpected errors

The adapter's error handler handles any exceptions that were not caught in the bot.

# [C#](#tab/csharp)

**AdapterWithErrorHandler.cs**

In the sample, the adapter's `OnTurnError` handler receives any exceptions thrown by your bot's turn logic. If there is an exception thrown, the handler deletes the conversation state for the current conversation to prevent the bot from getting stuck in an error-loop caused by being in a bad state.

[!code-csharp[AdapterWithErrorHandler](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/AdapterWithErrorHandler.cs?range=20-54)]

# [JavaScript](#tab/javascript)

**index.js**

In the sample, the adapter's `onTurnError` handler receives any exceptions thrown by your bot's turn logic. If there is an exception thrown, the handler deletes the conversation state for the current conversation to prevent the bot from getting stuck in a error-loop caused by being in a bad state.

[!code-javascript[AdapterWithErrorHandler](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/index.js?range=31-35,37-62)]

# [Java](#tab/java)

By registering an AdapterWithErrorHandler with the Spring framework in Application.java for the BotFrameworkHttpAdapter in this sample, the adapter's `onTurnError` handler receives any exceptions thrown by your bot's turn logic. If there is an exception thrown, the handler deletes the conversation state for the current conversation to prevent the bot from getting stuck in an error loop caused by being in a bad state. In the Java SDK the AdapterWithErrorHandler.java is implemented as part of the SDK and is included in com.microsoft.bot.integration package. See the Java SDK source code for details on the implementation of this adapter.


## [Python](#tab/python)

**adapter_with_error_handler.py**

In the sample, the adapter's `on_error` handler receives any exceptions thrown by your bot's turn logic. If there is an exception thrown, the handler deletes the conversation state for the current conversation to prevent the bot from getting stuck in a error-loop caused by being in a bad state.

[!code-python[adapter_with_error_handler](~/../botbuilder-samples/samples/python/13.core-bot/adapter_with_error_handler.py?range=16-56)]

---

## Register services

# [C#](#tab/csharp)

**Startup.cs**

Finally, in `Startup.cs`, the bot is created as a transient, and on every turn, a new instance of the bot is created.

[!code-csharp[Add transient bot](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Startup.cs?range=43-44)]

For reference, here are the class definitions that are used in the call to create the bot above.

[!code-csharp[DialogAndWelcomeBot signature](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Bots/DialogAndWelcomeBot.cs?range=16)]
[!code-csharp[DialogBot signature](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Bots/DialogBot.cs?range=18-19)]
[!code-csharp[MainDialog signature](~/../botbuilder-samples/samples/csharp_dotnetcore/13.core-bot/Dialogs/MainDialog.cs?range=17)]

# [JavaScript](#tab/javascript)

**index.js**

Finally, in `index.js`, the bot is created.

[!code-javascript[Create bot](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/index.js?range=69-82)]

For reference, here are the class definitions that are used in the call to create the bot above.

[!code-javascript[MainDialog signature](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/dialogs/mainDialog.js?range=12)]

[!code-javascript[DialogAndWelcomeBot signature](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/bots/dialogAndWelcomeBot.js?range=8)]

[!code-javascript[DialogBot signature](~/../botbuilder-samples/samples/javascript_nodejs/13.core-bot/bots/dialogBot.js?range=6)]

# [Java](#tab/java)

**Application.java**

Finally, in `Application.java`, the bot is created .

[!code-java[Add transient bot](~/../botbuilder-samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/Application.java?range=57-66)]

For reference, here are the class definitions that are used in the call to create the bot above.

[!code-java[DialogAndWelcomeBot signature](~/../botbuilder-samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/DialogAndWelcomeBot.java?range=30)]
[!code-java[DialogBot signature](~/../botbuilder-samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/DialogBot.java?range=26)]
[!code-java[MainDialog signature](~/../botbuilder-samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/MainDialog.java?range=32)]


## [Python](#tab/python)

**app.py**
Finally, in `app.py`, the bot is created.

[!code-python[create bot](~/../botbuilder-samples/samples/python/13.core-bot/app.py?range=46-50)]

For reference, here are the class definitions that are used in the call to create the bot.

[!code-python[main dialog](~/../botbuilder-samples/samples/python/13.core-bot/dialogs/main_dialog.py?range=20)]

[!code-python[dialog and welcome](~/../botbuilder-samples/samples/python/13.core-bot/bots/dialog_and_welcome_bot.py?range=21)]

[!code-python[dialog](~/../botbuilder-samples/samples/python/13.core-bot/bots/dialog_bot.py?range=9)]

---

## To test the bot

1. If you have not done so already, install the [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md).
1. Run the sample locally on your machine.
1. Start the Emulator, connect to your bot, and send messages as shown below.

<!--![test dialog prompt sample](~/media/emulator-v4/test-dialog-prompt.png)-->

## Additional information

- The 24.bot-authentication-msgraph sample in [**C#**](https://aka.ms/auth-sample-cs), [**JavaScript**](https://aka.ms/auth-sample-js), or [**Python**](https://aka.ms/auth-sample-py) shows how to handle a logout request. It uses a pattern similar to the one shown here for handling interruptions.

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

[cs-sample]: https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/13.core-bot
[js-sample]: https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/13.core-bot
[java-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/13.core-bot
[python-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/13.core-bot

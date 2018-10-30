---
title: Dialogs within the Bot Builder SDK | Microsoft Docs
description: Describes what a dialog is and how it work within the Bot Builder SDK.
keywords: conversation flow, recognize intent, single turn, multiple turn, bot conversation, dialogs, prompts, waterfalls, dialog set
author: johnataylor
ms.author: johtaylo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 9/22/2018
monikerRange: 'azure-bot-service-4.0'
---

# Dialogs library

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

The central concept in the SDK to manage conversations is the idea of a Dialog. Dialog objects process inbound Activities and generate outbound responses. The business logic of the bot runs either directly or indirectly within Dialog classes.

At runtime, Dialog instances are arranged in a stack. The Dialog on the top of the stack is referred to as the ActiveDialog. The current active Dialog processes the inbound Activity. Between each turn of the conversation (which is not time-bound and may span over several days), the stack is persisted. 

## Dialog Lifecycle

A Dialog implements three main functions:
- BeginDialog
- ContinueDialog
- ResumeDialog

At runtime, Dialogs and DialogContext classes work together to choose the appropriate Dialog to handle the activity. The DialogContext class ties the persisted Dialog stack, the inbound Activity, and the DialogSet class. A DialogSet contains dialogs that the bot can call.

The DialogContext's interface reflects the underlying notion of Dialog’s begin and continue. The general pattern for the application is always to call ContinueDialog first. If there is no stack and therefore no ActiveDialog, the application should begin the Dialog it chooses by calling BeginDialog on the DialogContext. This will cause the corresponding Dialog entry from the DialogSet to be pushed onto the stack (technically it’s the Dialog’s id that is added to the stack) and it will then delegate a call to BeginDialog on the specific Dialog object. If there had been an ActiveDialog, it would have simply delegated the call to that Dialog’s ContinueDialog, in the processing giving that Dialog any associated persisted properties.

Notice that a **Dialog’s BeginDialog** is initialization code and takes initialization properties (called “options” in the code) and a **Dialog’s ContinueDialog** is the code that is run to continue execution on the arrival of an Activity following persistence. For example, imagine a Dialog that asks the user a question, the question would be asked in the BeginDialog and the answer expected in the ContinueDialog.

To support nesting of Dialogs (where a dialog has child dialog) there is an additional type of continuation; this is called resumption. The DialogContext will call the ResumeDialog method on a parent Dialog when a child Dialog completes.

Prompts and Waterfalls are both concrete examples of Dialogs provided by the SDK. Many scenarios are built by composing these abstractions, but under the covers, the logic executed is always the same begin, i.e., the continue and resume pattern described here. Implementing a Dialog class from scratch is a relatively advanced topic, but an example is included in the [samples](https://github.com/Microsoft/BotBuilder-samples).

The **Dialog** library in the Bot Builder SDK includes built-in features such as _prompts_, _waterfall dialogs_, and _component dialogs_ to help you manage your bot's conversation. You can use prompts to ask users for different types of information, a waterfall to combine multiple steps in a sequence, and component dialogs to package your dialog logic into separate classes that can then be integrated into other bots.
## Waterfall Dialogs and Prompts

The **Dialog** library comes with a set of prompt types you can use to collect various types of user input. For example, to ask a user for text input, you can use the **TextPrompt**; to ask a user for a number, you can use the **NumberPrompt**; to ask for a date and time, you can use the **DateTimePrompt**. Prompts are a particular type of dialog. To use a prompt from a waterfall dialog, add both the waterfall and the prompt to the same dialog set. 

Because of the nature of the prompt-response interaction, implementing a prompt requires at least two steps in a waterfall dialog - one to send the prompt, and a second to capture and process the response.  If you have an additional prompt, you can sometimes combine these by using a single function to first process the user's response and then start the next prompt.

A `WaterfallDialog` is a specific implementation of a dialog that is used to collect information from the user or guide the user through a series of tasks. The tasks are implemented as an array of functions where the result of the first function is passed as an argument into the next function, and so on. Each function typically represents one step in the overall process. At each step, the bot prompts the user for input, waits for a response, and then passes the results to the next step. 

Prompts and Waterfall are both Dialogs, as shown in the following class hierarchy. 

![dialog classes](media/bot-builder-dialog-classes.png)

A waterfall dialog is composed of a sequence of waterfall steps. Each step is an asynchronous delegate that takes a _waterfall step context_ (`step`) parameter. The pattern is that the last thing done in a waterfall step is to either begin a child dialog (typically a prompt) or end the waterfall itself. The following diagram shows a sequence of waterfall steps and the stack operations that take place.

![Dialog concept](media/bot-builder-dialog-concept.png)

You can handle a return value from a dialog either within a waterfall step in a dialog or from your bot's on turn handler.
Within a waterfall step, the dialog provides the return value in the waterfall step context's _result_ property.
You generally only need to check the status of the dialog turn result from your bot's turn logic.

## Repeating a dialog

To repeat a dialog, use the *replace dialog* method. The dialog context's *replace dialog* method will pop the current dialog off the stack and push the replacing dialog onto the top of the stack and begin that dialog. You can use this method to create a loop by replacing a dialog with itself. Note that if you need to persist the internal state for the current dialog, you will need to pass information to the new instance of the dialog in the call to the _replace dialog_ method, and then initialize the dialog appropriately. The options passed into the new dialog can be accessed via the step context's _options_ property in any step of the dialog. This is a great way to handle a complex conversation flow or to manage menus.

## Branch a conversation

The dialog context maintains a _dialog stack_ and for each dialog on the stack, tracks which step is next. Its _begin dialog_ method pushes a dialog onto the top of the stack, and its _end dialog_ method pops the top dialog off the stack.

A dialog can start a new dialog within the same dialog set by calling the dialog context's _begin dialog_ method and providing the ID of the new dialog, which then makes the new dialog the currently active dialog. The original dialog is still on the stack, but calls to the dialog context's _continue dialog_ method are only sent to the dialog that is on top of the stack, the _active dialog_. When a dialog is popped off the stack, the dialog context will resume with the next step of the waterfall on the stack where it left off of the original dialog.

Therefore, you can create a branch within your conversation flow by including a step in one dialog that can conditionally choose a dialog to start out of a set of available dialogs.

## Component dialog
Sometimes you want to write a reusable Dialog that you want to use in different scenarios. An example might be an address Dialog that asks the user to provide values for street, city and zip code. 

The ComponentDialog provides a level of isolation because it has a separate DialogSet . By having a separate DialogSet, it avoids name collisions with the parent containing Dialog, and it creates its own independent internal Dialog runtime (by creating its own DialogContext) and dispatches the Activity to that. This secondary dispatch means that it has had an opportunity to intercept the Activity. This can be very useful if you want to implement features such as “help” and “cancel.”  See Enterprise Bot Template sample. 

## Next steps

> [!div class="nextstepaction"]
> [Use dialog library to gather user input](bot-builder-prompts.md)

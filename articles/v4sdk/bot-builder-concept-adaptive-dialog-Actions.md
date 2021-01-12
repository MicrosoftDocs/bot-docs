---
title: Actions - Bot Framework SDK - adaptive dialogs
description: Collecting user input using adaptive dialogs
keywords: bot, user, Events, triggers, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 04/27/2020
monikerRange: 'azure-bot-service-4.0'
---
<!--P2: Once the samples are done, link to them in each section on the individual actions to point to them as examples of how they are used-->
# Actions in adaptive dialogs

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Actions help to create and maintain the bots conversation flow once an event is captured by a [trigger][triggers]. In a similar way that adaptive dialogs contain a list of triggers, triggers contain a list of actions that once the trigger fires, will execute to accomplish any set of actions needed, such as satisfying a user's request. In addition to creating and maintaining the bot's conversational flow, you can use actions to send messages, respond to user questions using a [knowledge base][www.qnamaker.ai], make calculations, and perform any number of computational tasks for the user. With adaptive dialogs, the path the bot flows through in a dialog can branch and loop. The bot can ask and answer questions, validate the users input, manipulate and store values in [memory][memory-states], and make decisions based on user input.

> [!IMPORTANT]
> Actions are dialogs and any dialog can be used as an action, so actions have all of the power and flexibility you need to create a fully functional and robust bot. While the actions included in the Bot Framework SDK are extensive, you can also create your own custom actions to perform virtually any specialized task or process you need.

## Prerequisites

* [Introduction to adaptive dialogs][introduction]
* [Events and triggers in adaptive dialogs][triggers]

## Actions

Actions that are included with the Bot Framework SDK provide the ability to perform conditional coding such as:

* Branching and looping
* Dialog management tasks such as starting a new dialog or cancelling, ending or repeating a dialog.
* Memory management tasks such as creating, deleting or editing a property saved in [memory][memory-states].
* Accessing external resources such as sending an [HTTP Request](../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#httprequest).
* Preforming an [OAuth login request][oauthinput-inputs] and many others.

>[!TIP]
> Unlike a waterfall dialog where each step is a function, each action in an adaptive dialog is a fully functional dialog with all of the power and flexibility that entails. This enables adaptive dialogs by design to:
>
> * Provide an easier way to handle [interruptions][interruptions]. 
> * Branch conditionally based on context or state.

Adaptive dialogs support the following actions:

### Activities

Used to send any activity such as responding to a user.

* **Send activity**. Send any activity such as responding to a user.
* **Update an activity**. This enables you to update an activity that was sent.
* **DeleteActivity**. Enables you to delete an activity that was sent.
* **Get activity members**. Enables you to get a list of activity members and save it to a property in [memory][memory-states].

For detailed information and examples, see the [Activities](../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#activities) section in the __Actions in adaptive dialogs - reference guide__.

### Requesting user input

The Bot Framework SDK defines a variety of actions used for collecting and validating user input. These important and specialized actions are covered in the [Asking for user input using adaptive dialogs](bot-builder-concept-adaptive-dialog-inputs.md) article.

### Create a condition

Conditional statements such as if statements and for loops enable your bot make decisions and control the conversation flow. These conditions are specified by a set of conditional statements having boolean expressions which are evaluated to a boolean value of true or false.

Conditional actions provided by the Bot Framework SDK include:

* **If/else**. Used to create If and If-Else statements which are used to execute code only if the specified condition is true.
* **Switch**. Used to build a multiple-choice menu.
* **For each loop**. Loop through a set of values stored in an array.
* **For each page loop**. Loop through a large set of values stored in an array one page at a time.
  * **Exit a loop**. Use to break out of a loop.
  * **Continue a loop**.Used to continue the loop.
* **Goto**. Immediately goes to the specified action and continues execution. Determined by actionId.

For detailed information and examples, see the [Conditional statements](../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#conditional-statements) section in the __Actions in adaptive dialogs - reference guide__.

### Dialog management

The dialog management actions are designed to give you control of any dialog related action. dialog management actions provided by the Bot Framework SDK include:

* **Begin a new dialog**. This starts the execution of another dialog. When that dialog finishes, the execution of the current trigger will resume.
* **Cancel a dialog**. Cancels the active dialog. Use when you want the dialog to close immediately, even if that means stopping mid-process.
* **Cancel all dialogs**. Cancels all active dialogs including any active parent dialogs. Use this if you want to pop all dialogs off the stack, you can clear the dialog stack by calling the dialog context's cancel all dialogs method. Emits the `CancelAllDialogs` event.
* **End this dialog**. Ends the active dialog.  Use when you want the dialog to complete and return results before ending. Emits the `EndDialog` event.
* **End dialog turn**. Ends the current turn of conversation without ending the dialog.
* **Repeat this dialog**. Used to restart the parent dialog.
* **Replace this dialog**. Replaces the current dialog with a new dialog.
* **GetConversationMembers**. Enables you to get a list of the conversation members and save it to a property in [memory][memory-states].
* **EditActions**. Enables you to edit the current action sequence on the fly based on user input. Especially useful when handling [interruptions][interruptions].

For detailed information and examples, see the [Dialog management](../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#dialog-management) section in the __Actions in adaptive dialogs - reference guide__.

### Manage properties

The manage properties actions able you to create, update and delete a property.  For more information on properties, see the Bot Framework SDK [Managing state][Managing-state] and the [Managing state in adaptive dialogs][memory-states] articles.

* **Edit an array**. This enables you to perform edit operations on an array.
* **Delete a property**. This enables you to remove a property from [memory][memory-states].
* **Delete properties**. This enables you to delete more than one property in a single action.
* **Create or update a property**. This enables you to set a property's value in [memory][memory-states].
* **Create or update properties**. This enables you to initialize one or more properties in a single action.

For detailed information and examples, see the [Manage properties](../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#manage-properties) section in the __Actions in adaptive dialogs - reference guide__.

### Access external resources

These action enable you to access external resources, such as skills, sending an HTTP request, emitting a custom event or call custom code etc.

* **Begin a skill dialog**. Use the adaptive skill dialog to run a skill.
* **Send an HTTP request**. Enables you to make HTTP requests to any endpoint.
* **Emit a custom event**. Enables you to raise a custom event that your bot can respond to using a [custom trigger][custom-event-trigger].
* **Sign out a user**. Enables you to sign out the currently signed in user.
* **Call custom code**. Enables you to call your own custom code.

For detailed information and examples, see the [Access external resources](../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#access-external-resources) section in the __Actions in adaptive dialogs - reference guide__.

### Debugging options

* **Log to console**. Writes to the console and optionally sends the message as a trace activity.
* **Emit a trace event**. Used to sends a trace activity with te specified payload.

For detailed information and examples, see the [Debugging options](../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#debugging-options) section in the __Actions in adaptive dialogs - reference guide__.

## Additional Information

* To learn about actions specific to gathering user input, see the [asking for user input using adaptive dialogs][inputs] article.
* To learn more about adaptive expressions see the [adaptive expressions][adaptive-expressions] article.
* For detailed information and examples on all actions covered in this article, see the [Actions in adaptive dialogs - reference guide](../adaptive-dialog/adaptive-dialog-prebuilt-actions.md) reference article.

[introduction]:bot-builder-adaptive-dialog-introduction.md
[triggers]:bot-builder-concept-adaptive-dialog-triggers.md
[www.qnamaker.ai]:https://www.qnamaker.ai/
[oauthinput-inputs]:../adaptive-dialog/adaptive-dialog-prebuilt-inputs.md#oauthinput
[concept-dialog]:bot-builder-concept-dialog.md
[interruptions-inputs]:../adaptive-dialog/adaptive-dialog-prebuilt-inputs.md#interruptions
[inputs]:bot-builder-concept-adaptive-dialog-inputs.md
[custom-event-trigger]:bot-builder-concept-adaptive-dialog-triggers.md#custom-event-trigger
[generators]:bot-builder-concept-adaptive-dialog-generators.md
[adaptive-expressions]:bot-builder-concept-adaptive-expressions.md
[memory-states]:bot-builder-concept-adaptive-dialog-memory-states.md
[interruptions]: bot-builder-concept-adaptive-dialog-interruptions.md
[Managing-state]: bot-builder-concept-state.md

---
title: Actions in adaptive dialogs - reference guide
description: Describing the adaptive dialog prebuilt actions
keywords: bot, actions, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 04/30/2021
monikerRange: 'azure-bot-service-4.0'
---

# Actions in adaptive dialogs - reference guide

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article lists the actions defined in the Bot Framework SDK, grouped by their general purpose.

- For an introduction to this topic, see the [Actions][1] topic in the Composer documentation.

## Activities

| Activity to accomplish                         | Action Name           | What this action does                                                |
| ---------------------------------------------- | --------------------- | -------------------------------------------------------------------- |
| Send any activity such as responding to a user.| `SendActivity`        | Sends an activity, such as a response to a user.                     |
| Update an activity                             | `UpdateActivity`      | Updates an activity that was sent.                                   |
| DeleteActivity                                 | `DeleteActivity`      | Deletes an activity that was sent.                                   |
| Get activity members                           | `GetActivityMembers`  | Gets a list of activity members and saves it to a property in memory.|

## Conditional statements

The first two actions are conditional statements designed to help your bot make decisions based on any pre-defined condition that you have created. These conditions are specified by a set of conditional statements having boolean expressions which are evaluated to a boolean value of true or false.

The remaining actions relate to looping statements which enable you to repeat the execution of a block of code for every element in a collection.

| Activity to accomplish             | Action Name       | What this action does                                                       |
| ---------------------------------- | ----------------- | --------------------------------------------------------------------------- |
| Branch: if/else                    | `IfCondition`     | Runs a set of actions based on a Boolean expression.                        |
| Branch: Switch (Multiple options)  | `SwitchCondition` | Runs a set of actions based on a pattern match.                             |
| Loop: for each item                | `ForEach`         | Loops through a set of values stored in an array.                           |
| Loop: for each page (multiple items) | `ForEachPage`   | Loops through a large set of values stored in an array, one page at a time. |
| Exit a loop                        | `BreakLoop`       | Exits the enclosing loop.                                                   |
| Continue a loop                    | `ContinueLoop`    | Starts the next iteration of the enclosing loop.                            |
| Goto a different Action            | `GotoAction`      | Transfers control to the specified action, identified by the action's ID.   |

## Dialog management

| Activity to accomplish | Action Name                      | What this action does                                                     |
| ---------------------- | -------------------------------- | ------------------------------------------------------------------------- |
| Begin a new dialog     | `BeginDialog`     | Begins executing another dialog. When that dialog finishes, the execution of the current trigger will resume.    |
| Cancel a dialog        | `CancelDialog`    | Cancels the active dialog. Use when you want the dialog to close immediately, even if that means stopping mid-process.|
| Cancel all dialogs     | `CancelAllDialogs`| Cancels all active dialogs including any active parent dialogs. Use this if you want to pop all dialogs off the stack, you can clear the dialog stack by calling the dialog context's cancel all dialogs method. Emits the `CancelAllDialogs` event.                   |
| End this dialog        | `EndDialog`       | Ends the active dialog.  Use when you want the dialog to complete and return results before ending. Emits the `EndDialog` event.|
| End dialog turn        | `EndTurn`         | Ends the current turn of conversation without ending the dialog. |
| Repeat this dialog     | `RepeatDialog`    | Used to restart the parent dialog.  |
| Replace this dialog    | `ReplaceDialog`   | Replaces the current dialog with a new dialog. |
| GetConversationMembers | `GetConversationMembers` | Enables you to get a list of the conversation members and save it to a property in memory.|
| EditActions            | `EditActions` | Enables you to edit the current action sequence on the fly based on user input. Especially useful when handling interruptions.  |

## Manage properties

| Activity to accomplish | Action Name           | What this action does                          |
| ---------------------- | --------------------- | ---------------------------------------------- |
| Edit an array          | `EditArray`           | Performs an operation on an array.             |
| Delete a property      | `DeleteProperty`      | Removes a property from memory.                |
| Delete properties      | `DeleteProperties`    | Removes multiple properties at once.           |
| Create or update a property | `SetProperty`    | Sets a property's value in memory.             |
| Create or update properties | `SetProperties`  | Sets the value of multiple properties at once. |

## Access external resources

| Activity to accomplish | Action Name    | What this action does                                                                          |
| ---------------------- | -------------- | ---------------------------------------------------------------------------------------------- |
| Begin a skill dialog   | `BeginSkill`   | Begins a skill and forwards activities to the skill until the skill ends.                      |
| Send an HTTP request   | `HttpRequest`  | Makes an HTTP request to an endpoint.                                                          |
| Emit a custom event    | `EmitEvent`    | Raises a custom event. Add a custom trigger to the adaptive dialog to react to the event.      |
| Sign out a user        | `SignOutUser`  | Signs out the currently signed in user.                                                        |
| Call custom code       | `CodeAction`   | Calls custom code. The custom code must be asynchronous, take a dialog context and an object as parameters, and return a dialog turn result. |

## Debugging options

| Activity to accomplish | Action Name     | What this action does                                                       |
| ---------------------- | --------------- | --------------------------------------------------------------------------- |
| Log to console         | `LogAction`     | Writes to the console and optionally sends the message as a trace activity. |
| Emit a trace event     | `TraceActivity` | Sends a trace activity with whatever payload you specify.                   |

## Additional Information

- To learn about actions specific to gathering user input, see the [asking for user input using adaptive dialogs][2] article.
- To learn more about adaptive expressions see the [adaptive expressions][3] article.

[1]:/composer/concept-dialog#action
[2]:../adaptive-dialog/adaptive-dialog-prebuilt-inputs.md
[3]:../v4sdk/bot-builder-concept-adaptive-expressions.md

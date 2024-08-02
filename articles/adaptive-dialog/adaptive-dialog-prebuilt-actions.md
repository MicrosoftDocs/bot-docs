---
title: Prebuilt actions for adaptive dialogs
description: Learn about the adaptive dialog prebuilt actions, grouped by their general purpose.
keywords: bot, actions, adaptive dialogs
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: azure-ai-bot-service
ms.topic: reference
ms.date: 09/01/2022
monikerRange: 'azure-bot-service-4.0'
ms.custom:
  - evergreen
---

# Actions in adaptive dialogs - reference guide

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article lists the actions defined in the Bot Framework SDK, grouped by their general purpose.

- For an introduction to this topic, see the [Actions](/composer/concept-dialog#action) topic in the Composer documentation.

## Responses and questions

| Action Name       | Action title                       | Description                                               |
|:------------------|:-----------------------------------|:----------------------------------------------------------|
| `Ask`             | Send a response to ask a question  | Uses an activity as a way to prompt the user.             |
| `AttachmentInput` | Prompt for a file or an attachment | Asks the user for a file or image.                        |
| `ChoiceInput`     | Prompt with multi-choice           | Asks the user to pick from a list of choices.             |
| `ConfirmInput`    | Prompt for confirmation            | Asks the user for confirmation (a yes-no question).       |
| `DateTimeInput`   | Prompt for a date or a time        | Asks the user for a date or time value.                   |
| `NumberInput`     | Prompt for a number                | Asks the user for a numeric value.                        |
| `OAuthInput`      | OAuth login                        | Asks the user to sign in with an OAuth identity provider. |
| `SendActivity`    | Send a response                    | Sends an activity, such as a response to a user.          |
| `TextInput`       | Prompt for text                    | Asks the user to type a response.                         |

## Conditions and looping

The conditional actions are designed to help your bot make decisions based on any pre-defined condition that you've created. These actions are specified by a set of conditional statements that have Boolean expressions, which are evaluated to a Boolean value of true or false.

The remaining actions relate to looping statements which enable you to repeat the execution of a block of code for every element in a collection.

| Action Name       | Action title                         | Description                                                           |
|:------------------|:-------------------------------------|:----------------------------------------------------------------------|
| `BreakLoop`       | Break out of loop                    | Exits the enclosing loop.                                             |
| `ContinueLoop`    | Continue loop                        | Starts the next iteration of the enclosing loop.                      |
| `ForEach`         | Loop: For each item                  | Runs a set of actions on each item in a collection.                   |
| `ForEachPage`     | Loop: For each page (multiple items) | Runs a set of actions on each page (subset of items) in a collection. |
| `IfCondition`     | Branch: If/else                      | Runs a set of actions based on a Boolean expression.                  |
| `SwitchCondition` | Branch: Switch (multiple options)    | Runs a set of actions based on the value of a property.               |

## Dialog management

| Action Name | Action title | Description |
|:-|:-|:-|
| `BeginDialog` | Begin a new dialog | Begins a new dialog and adds it to the stack. You can provide input parameters for the new dialog. When the new dialog ends, control returns to the next step in this trigger. |
| `CancelAllDialogs` | Cancel all active dialogs | Cancels all active dialogs. Optionally sends a custom event that can be caught to prevent cancellation from propagating. |
| `CancelDialog` | Cancel dialog | Cancels the active dialog. Optionally sends a custom event that can be caught to prevent cancellation. |
| `ContinueConversation` | Continue conversation | Sends a proactive message. Requires a bot with a configured storage queue. |
| `ContinueConversationLater` | Continue conversation later | Queues a proactive message to be sent after a delay. Requires the bot to have a storage queue configured. |
| `EndDialog` | End this dialog | Ends the current dialog and returns an optional result. |
| `EndTurn` | End turn | Ends the current turn without explicitly ending the dialog. |
| `GetConversationReference` | Get conversation reference | Saves the current conversation reference to memory. For use with the continue conversation actions. |
| `GotoAction` | Go to action | Jump to another action in the current trigger. |
| `RepeatDialog` | Repeat this dialog | Restarts the current dialog. You can provide input parameters for the dialog. |
| `ReplaceDialog` | Replace this dialog | Replaces the current dialog with a new dialog. You can provide input parameters for the new dialog. |

## Manage properties

| Action Name              | Action title             | Description                                                                                            |
|:-------------------------|:-------------------------|:-------------------------------------------------------------------------------------------------------|
| `DeleteActivity`         | Delete Activity          | Deletes an activity that was previously sent to a user.                                                |
| `DeleteProperties`       | Delete properties        | Removes multiple properties at once.                                                                   |
| `DeleteProperty`         | Delete a property        | Removes a property from memory.                                                                        |
| `EditArray`              | Edit an array property   | Performs an operation on an array.                                                                     |
| `GetActivityMembers`     | Get activity members     | Gets the members participating in an activity. Only supported by the BotFrameworkAdapter connector.    |
| `GetConversationMembers` | Get conversation members | Gets the members participating in a conversation. Only supported by the BotFrameworkAdapter connector. |
| `SetProperties`          | Set properties           | Sets the value of multiple properties at once.                                                         |
| `SetProperty`            | Set a property           | Sets a property's value in memory.                                                                     |
| `UpdateActivity`         | Update an activity       | Updates an activity that was previously sent to a user.                                                |

## Access external resources

[!INCLUDE [qnamaker-sunset-alert](../includes/qnamaker-sunset-alert.md)]

| Action Name           | Action title           | Description                                                                                                |
|:----------------------|:-----------------------|:-----------------------------------------------------------------------------------------------------------|
| `BeginSkill`          | Connect to a skill     | Begins a skill and forwards activities to the skill until the skill ends.                                  |
| `EmitEvent`           | Emit a custom event    | Raises a custom event. To allow a dialog to react to the event, add a custom events trigger to the dialog. |
| `HttpRequest`         | Send an HTTP request   | Makes an HTTP request to an endpoint.                                                                      |
| `OAuthInput`          | OAuth login            | Asks the user to sign in with an OAuth identity provider.                                                  |
| `QnAMakerDialog`      | QnAMaker dialog        | Uses a QnA Maker knowledge base to answer user questions.                                                  |
| `SendHandoffActivity` | Send a handoff request | Deprecated. Don't use this action.                                                                         |
| `SignOutUser`         | Sign out user          | Signs out the user from an OAuth identity provider.                                                        |

## Debugging options

| Action Name                 | Action title            | Description                                                                                                 |
|:----------------------------|:------------------------|:------------------------------------------------------------------------------------------------------------|
| `LogAction`                 | Log to console          | Writes to the console and optionally sends the message as a trace activity.                                 |
| `TelemetryTrackEventAction` | Telemetry - track event | Uses the registered telemetry client, to track a custom event.                                              |
| `ThrowException`            | Throw an exception      | Throws an exception. To allow a dialog to catch the exception, add an error occurred trigger to the dialog. |
| `TraceActivity`             | Emit a trace event      | Sends a trace activity.                                                                                     |

## Additional Information

- To learn about actions specific to gathering user input, see the [asking for user input using adaptive dialogs](../adaptive-dialog/adaptive-dialog-prebuilt-inputs.md) article.
- To learn more about adaptive expressions see the [adaptive expressions](../v4sdk/bot-builder-concept-adaptive-expressions.md) article.

---
title: Using declarative assets in adaptive dialogs
description: Using declarative assets in adaptive dialogs
keywords: declarative, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 05/31/2020
---

# Using declarative assets in adaptive dialogs

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article explains the concepts behind declarative assets and bots that incorporate adaptive dialogs using the declarative approach.  Declarative adaptive dialogs consist of JSON-based files that describe all of the attributes of adaptive dialogs, including their [triggers][triggers] and [actions][actions]. These declarative files are loaded at run time using the resource manager to create the adaptive dialogs.

## Prerequisites

- Knowledge of [bot basics][concept-basics], the [dialogs library][concept-dialogs] and [adaptive dialogs][concept-adaptive].

## Declarative files

Declarative files currently consist of _.dialog_ files that describe all of the attributes of an adaptive dialog and _.lg_ files that consist of LG templates that define the [language generation (LG)](bot-builder-concept-adaptive-dialog-generators.md) aspects of your bot.

### .dialog files

Adaptive dialog declarative files that have the .dialog extension contain the following elements:

- The `$schema` value contains a URI pointing to the Schema that describes the format of this declarative file. That schema is a Bot Framework component schema, which adheres to [draft 7](http://json-schema.org/specification-links.html#draft-7) of the JSON schema vocabulary. This schema file enables [IntelliSense][intellisense] to work for your declarative elements. For information on how to create this file, see the section on [The merge command](#the-merge-command) below. The name of the schema file can be any valid filename, but is typically named **app.schema**.
- The `$kind` field identifies the type of component described in this file. For an adaptive dialog, `$kind` must be `Microsoft.AdaptiveDialog`. In sub-objects, `$kind` identifies a trigger or action that is part of the dialog. This field correlates with the `[JsonProperty("$kind")]` class attribute that is associated with every class in the Bot Framework SDK that is designed to work using the declarative approach.
- The `recognizer` value contains a [recognizer type][recognizer-types] and an array of one or more [intents][intents] and optionally an array of one or more [entities][entity].
- The `generator` value contains a link to the .lg file associated with the adaptive dialog that this .dialog file defines.
- The `triggers` value contains an array of one or more [triggers](bot-builder-concept-adaptive-dialog-triggers.md). The type of trigger is declared using the `$kind` keyword. Each trigger contains an array of one or more actions.
- The `actions` value contains an array of one or more [actions](bot-builder-concept-adaptive-dialog-actions.md), each action can have properties associated with it.

An example of a simple .dialog file:

```json
{
  "$schema": "../app.schema",
  "$kind": "Microsoft.AdaptiveDialog",
  "generator": "multiTurnPrompt.lg",
  "recognizer": {
      "$kind": "Microsoft.RegexRecognizer",
      "intents": [
          {
              "intent": "CancelIntent",
              "pattern": "(?i)cancel"
          }
      ]
  },
  "triggers": [
    {
      "$kind": "Microsoft.OnUnknownIntent",
      "actions": [
        {
          "$kind": "Microsoft.SendActivity",
          "activity":  "You said '${turn.activity.text}'"
        }
      ]
    }
  ]
}
```

> [!NOTE]
> The `$schema` file is what enables IntelliSense to work. No warning or error will occur if `$schema` keyword is missing or the `$schema` value references a schema file that cannot be found and everything except IntelliSense will still work as expected.

The elements of the .dialog file defined:

> [!div class="mx-imgBorder"]
> ![Create Get Weather Dialog](./media/adaptive-dialogs/dotdialogfile.png)

For information on how `.dialog` files can be generated automatically using the CLI `luis:build` command when creating a LUIS application see [The dialog file][the-dialog-file-luis] section of the **Deploy LUIS resources using the Bot Framework LUIS CLI commands** article.

For information on how `.dialog` files can be generated automatically using the CLI `qnamaker:build` command when creating a QnA Maker knowledge base see [The dialog file][the-dialog-file-qnamaker] section of the **Deploy QnA Maker knowledge base using the Bot Framework qnamaker CLI commands** article.

### .lg files

Adaptive dialog declarative files that have the .lg extension are described in detail in the [.lg file format][lg-file-format] article.

## The resource explorer

The resource explorer provides the tools you need to import declarative adaptive dialog files into your bot and use them as if they were adaptive dialogs defined directly in your bot source code.

With the resource explorer, you can create resource objects that contain all of the relevant information about the declarative files required to create adaptive dialogs at run time, which is done using the resource explorer's type loader that imports files with the .dialog and .lg extensions.

### The resource object

The resource explorer's _get resource_ method reads the declarative file into a resource object.  The resource object contains the information about the declarative file and can be used by any process that needs to reference it, such as the type loader.

```csharp
var resource = this.resourceExplorer.GetResource("main.dialog");
```

### The type loader

Once the resource explorer's `_get resource_` method reads the declarative file into a resource object, the `_load type_method` casts the resource to an `AdaptiveDialog` object. The `AdaptiveDialog` object can be used the same as any other non-declarative adaptive dialog is used when creating a dialog manager.

```csharp
dialogManager = new DialogManager(resourceExplorer.LoadType<AdaptiveDialog>(resource));
```

### Auto reload dialogs when file changes

Any time a declarative file changes when your bot is running, a _changed_ event fires. You can capture that event and reload your declarative files, that way when any adaptive dialog needs updated you do not need to update your code and recompile your source code or restart your bot. This can be especially useful in a production environment.

<!--This example could be improved-->
```csharp
// auto reload the root dialog when it changes
this.resourceExplorer.Changed += (e, resources) =>
{
    if (resources.Any(resource => resource.Id == "main.dialog"))
    {
        Task.Run(() => this.LoadRootDialogAsync());
    }
};

private void LoadRootDialogAsync()
{
    var resource = this.resourceExplorer.GetResource("main.dialog");
    dialogManager = new DialogManager(resourceExplorer.LoadType<AdaptiveDialog>(resource));
}
```

### The `VersionChanged` event

When your declarative assets have been reloaded to accommodate the changes made to a `.dialog` file, you may need to reevaluate the state of any current conversations to account of any changes to the logic of your dialog. The changes to your logic may be as simple as to clear your conversation stack and restart. More sophisticated logic would enable you to do things like restart a dialog keeping the data you have, allowing you to pick up new properties and or paths that didn't exist before.

The `DialogEvents.VersionChanged` event is captured using the `OnDialogEvent` trigger.

<!--This example could be improved-->
```csharp
Triggers = new List<OnCondition>()
{
    new OnDialogEvent(DialogEvents.VersionChanged)
    {
        Actions = new List<Dialog>()
        {
            new SendActivity("The VersionChanged event fired.")
        }
    }
}
```

## Declarative assets

The Bot Framework SDK has various declarative assets available, each will be listed below. These assets can be used in your .dialog files as the `$kind` value.

### Triggers

This section contains all [triggers](bot-builder-concept-adaptive-dialog-triggers.md), grouped by type:

#### Base trigger

| `$kind` value          | Trigger Name | What this trigger does                                              |
| ---------------------- | ------------ | ------------------------------------------------------------------- |
|`Microsoft.OnCondition` | `OnCondition`| The `OnCondition` trigger is the base trigger that all triggers derive from. When defining triggers in an adaptive dialog they are defined as a list of `OnCondition` triggers. |

#### Recognizer event triggers

| `$kind` value            | Trigger name     | Description                                                                                                                                                   |
| ------------------------ | ---------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------- |
|`Microsoft.OnChooseIntent`| `OnChooseIntent` | This trigger is run when ambiguity has been detected between intents from multiple recognizers in a CrossTrainedRecognizerSet.                                |
|`Microsoft.OnIntent`      | `OnIntent`       | Actions to perform when specified intent is recognized.                                                                                                       |
|`Microsoft.OnQnAMatch`    | `OnQnAMatch`     | This trigger is run when the [QnAMakerRecognizer][qna-maker-recognizer] has returned a `QnAMatch` intent. The entity @answer will have the `QnAMaker` answer. |
|`Microsoft.OnUnknownIntent`|`OnUnknownIntent`| Actions to perform when user input is unrecognized or no match is found in any of the `OnIntent` triggers.                                                    |

#### Dialog event triggers

| `$kind` value            | Trigger name     | Description                                                                                                                           |
| ------------------------ | ---------------- | ------------------------------------------------------------------------------------------------------------------------------------  |
|`Microsoft.OnBeginDialog`  | `OnBeginDialog`  | Actions to perform when this dialog begins. For use with child dialogs only.                                                         |
|`Microsoft.OnCancelDialog` | `OnCancelDialog` | This event allows you to prevent the current dialog from being cancelled due to a child dialog executing a CancelAllDialogs action.  |
|`Microsoft.OnEndOfActions` | `OnEndOfActions` | This event occurs once all actions and ambiguity events have been processed.                                                         |
|`Microsoft.OnError`        | `OnError`        | Action to perform when an `Error` dialog event occurs. This event is similar to `OnCancelDialog` in that you're preventing the current dialog from ending, in this case due to an error in a child dialog.|
|`Microsoft.OnRepromptDialog` |`OnRepromptDialog`| Actions to perform when `RepromptDialog` event occurs.                                                                             |

> [!IMPORTANT]
> Do not use the `OnBeginDialog` trigger in your root dialog as it can potentially cause problems. You can instead use the `OnUnknownIntent` trigger which will fire when your root dialog runs.

> [!TIP]
> Most child dialogs include an `OnBeginDialog` trigger that responds to the `BeginDialog` event. This trigger automatically fires when the dialog begins, which can allow the bot to respond immediately with a welcome message or a [prompt for user input](bot-builder-concept-adaptive-dialog-inputs.md).

##### Dialog event trigger example using declarative

```json
{
    "$schema": "../app.schema",
    "$kind": "Microsoft.AdaptiveDialog",
    "triggers": [
        {
            "$kind": "Microsoft.OnBeginDialog",
            "actions": [
                {
                    "$kind": "Microsoft.SendActivity",
                    "activity": "Hello world!"
                }
            ]
        }
    ]
}
```

#### Activity event triggers

Activity triggers enable you to associate actions to any incoming activity from the client such as when a new user joins and the bot begins a new conversation. Additional information on activities can be found in [Bot Framework Activity schema][botframework-activity].

| `$kind` value                          | Trigger name                  | Description                                                                 |
| -------------------------------------- | ----------------------------- | --------------------------------------------------------------------------- |
|`Microsoft.OnConversationUpdateActivity`| `OnConversationUpdateActivity`| Handle the events fired when a user begins a new conversation with the bot. |
|`Microsoft.OnEndOfConversationActivity` | `OnEndOfConversationActivity` | Actions to perform on receipt of an activity with type `EndOfConversation`. |
|`Microsoft.OnEventActivity`             | `OnEventActivity`             | Actions to perform on receipt of an activity with type `Event`.             |
|`Microsoft.OnHandoffActivity`           | `OnHandoffActivity`           | Actions to perform on receipt of an activity with type `HandOff`.           |
|`Microsoft.OnInvokeActivity`            | `OnInvokeActivity`            | Actions to perform on receipt of an activity with type `Invoke`.            |
|`Microsoft.OnTypingActivity`            | `OnTypingActivity`            | Actions to perform on receipt of an activity with type `Typing`.            |

#### Message event triggers

**Message event** triggers allow you to react to any message event such as when a message is updated (`MessageUpdate`) or deleted (`MessageDeletion`) or when someone reacts (`MessageReaction`) to a message (for example, some of the common message reactions include a Like, Heart, Laugh, Surprised, Sad and Angry reactions).

Message events are a type of activity event and as such, all message events have a base event of `ActivityReceived` and are further refined by `ActivityType`. The Base class that all message triggers derive from is `OnActivity`.

| `$kind` value                        |  Trigger name               | Description                                                               |
| ------------------------------------ |  -------------------------- | ------------------------------------------------------------------------- |
|`Microsoft.OnMessageActivity`         | `OnMessageActivity`         | Actions to perform on receipt of an activity with type `MessageReceived`. |
|`Microsoft.OnMessageDeleteActivity`   | `OnMessageDeleteActivity`   | Actions to perform on receipt of an activity with type `MessageDelete`.   |
|`Microsoft.OnMessageReactionActivity` | `OnMessageReactionActivity` | Actions to perform on receipt of an activity with type `MessageReaction`. |
|`Microsoft.OnMessageUpdateActivity`   | `OnMessageUpdateActivity`   | Actions to perform on receipt of an activity with type `MessageUpdate`.   |

#### Custom event triggers

You can emit your own events by adding the [EmitEvent][emitevent] action to any trigger, then you can handle that custom event in any trigger in any dialog in your bot by defining a _custom event_ trigger. A custom event trigger is a type of `OnDialogEvent` trigger, where its _event_ property to the same value as the emit event's _event name_ property.

> [!TIP]
> You can allow other dialogs in your bot to handle your custom event by setting the emit event's _bubble event_ property to true.

| `$kind` value | Trigger name  | Description                                                                                                                               |
| ----------------------- | ------------- | ----------------------------------------------------------------------------------------------------------------------------------------- |
| Microsoft.OnDialogEvent | OnDialogEvent | Actions to perform when a custom event is detected. Use [Emit a custom event][emitevent]' action to raise a custom event. |

### Actions

This section contains all [actions](bot-builder-concept-adaptive-dialog-actions.md), grouped by type:

#### Send a response

| `$kind` value            | Action Name                   | What this action does                                             |
| ------------------------ | ----------------------------- | ----------------------------------------------------------------- |
| `Microsoft.SendActivity` | [SendActivity][send-activity] | Enables you send any type of activity such as responding to users.|

#### Requesting user input

| `$kind` value              | Input class                      | Description                                          | Returns                                      |
| -------------------------- | -------------------------------- | ---------------------------------------------------- | -------------------------------------------- |
|`Microsoft.AttachmentInput` |[AttachmentInput][attachmentinput]| Used to request/enable a user to **upload a file**.  | A collection of attachment objects.          |
|`Microsoft.ChoiceInput`     | [ChoiceInput][choiceinput]       | Used to asks for a choice from a **set of options**. | The value or index of the selection.         |
|`Microsoft.ConfirmInput`    | [ConfirmInput][confirminput]     | Used to request a **confirmation** from the user.    | A Boolean value.                             |
|`Microsoft.DateTimeInput`   | [DateTimeInput][datetimeinput]   | Used to ask your users for a **date and or time**.   | A collection of date-time objects.           |
|`Microsoft.InputDialog`     | InputDialog                      | This is the base class that all of the input classes derive from. It defines all shared properties. |
|`Microsoft.NumberInput`     | [NumberInput][numberinput]       | Used to ask your users for a **number**.             | A numeric value.                             |
|`Microsoft.OAuthInput`      | [OAuthInput][oauthinput]         | Used to enable your users to **sign into a secure site**.| A token response.                        |
|`Microsoft.TextInput`       | [TextInput][textinput]           | Used to ask your users for a **word or sentence**.   | A string.                                    |

#### Create a condition

| Activity to accomplish | `$kind` value             | Action Name                | What this action does                                                                               |
| ---------------------- | ------------------------- | -------------------------- | --------------------------------------------------------------------------------------------------- |
| Branch: if/else        |`Microsoft.IfCondition`    |[IfCondition][ifcondition]| Used to create If and If-Else statements which are used to execute code only if a condition is true.  |
| Branch: Switch (Multiple options) |`Microsoft.SwitchCondition` | [SwitchCondition][switchcondition] | Used to build a multiple-choice menu.                                           |
| Loop: for each item    |`Microsoft.Foreach`        | [ForEach][foreach]         | Loop through a set of values stored in an array.                                                    |
| Loop: for each page (multiple items) |`Microsoft.ForeachPage` | [ForEachPage][foreachpage] | Loop through a large set of values stored in an array one page at a time.                |
| Exit a loop            |`Microsoft.BreakLoop`      | [BreakLoop][break-loop]    | Break out of a loop.                                                                                |
| Continue a loop        |`Microsoft.ContinueLoop`   | [ContinueLoop][continue-loop] | Continue the loop.                                                                               |
| Goto a different Action|`Microsoft.GotoAction`     | [GotoAction][goto-action]   | Immediately goes to the specified action and continues execution. Determined by `actionId`.        |

#### Dialog management

| `$kind` value           | Action Name                        | What this action does                                                                                                   |
| ----------------------- | ---------------------------------- | ----------------------------------------------------------------------------------------------------------------------- |
|`Microsoft.BeginDialog`  | [BeginDialog][begindialog]         | Begins executing another dialog. When that dialog finishes, the execution of the current trigger will resume.           |
|`Microsoft.CancelDialog` | `CancelDialog`                     | Cancels the active dialog. Use when you want the dialog to close immediately, even if that means stopping mid-process.  |
|`Microsoft.CancelAllDialogs`| [CancelAllDialogs][cancelalldialogs]| Cancels all active dialogs including any active parent dialogs. Use this if you want to pop all dialogs off the stack, you can clear the dialog stack by calling the dialog context's cancel all dialogs method. Emits the `CancelAllDialogs` event.|
|`Microsoft.EndDialog`    | [EndDialog][enddialog]            | Ends the active dialog.  Use when you want the dialog to complete and return results before ending. Emits the `EndDialog` event.|
|`Microsoft.EndTurn`      | [EndTurn][endturn]                | Ends the current turn of conversation without ending the dialog.                                                         |
|`Microsoft.RepeatDialog` | [RepeatDialog][repeatdialog]      | Used to restart the parent dialog.                                                                                       |
|`Microsoft.ReplaceDialog`| [ReplaceDialog][replacedialog]    | Replaces the current dialog with a new dialog                                                                            |
|`Microsoft.UpdateActivity`| [UpdateActivity][update-activity]| This enables you to update an activity that was sent.                                                                    |
|`Microsoft.DeleteActivity` | [DeleteActivity][delete-activity]| Enables you to delete an activity that was sent.                                                                        |
|`Microsoft.GetActivityMembers` | [GetActivityMembers][get-activity-members]| Enables you to get a list of activity members and save it to a property in [memory][memory-states].        |
|`Microsoft.GetConversationMembers`| [GetConversationMembers][get-conversation-members] | Enables you to get a list of the conversation members and save it to a property in [memory][memory-states].|
|`Microsoft.EditActions`  | [EditActions][editactions] | Enables you to edit the current action sequence on the fly based on user input. Especially useful when handling [interruptions][interruptions]. |

#### Manage properties

|  `$kind` value              | Action Name                          | What this action does                                                     |
| --------------------------- | ------------------------------------ | ------------------------------------------------------------------------- |
|`Microsoft.EditArray`        | [EditArray][editarray]               | This enables you to perform edit operations on an array.                  |
|`Microsoft.DeleteProperty`   | [DeleteProperty][deleteproperty]     | This enables you to remove a property from [memory][memory-states].                  |
|`Microsoft.DeleteProperties` | [DeleteProperties][deleteproperties] | This enables you to delete more than one property in a single action.     |
|`Microsoft.SetProperty`      | [SetProperty][setproperty]           | This enables you to set a property's value in [memory][memory-states].               |
|`Microsoft.SetProperties`    | [SetProperties][setproperties]       | This enables you to initialize one or more properties in a single action. |

#### Access external resources

| `$kind` value          | Action Name                 | What this action does                                                                                      |
| ---------------------- | --------------------------- | ---------------------------------------------------------------------------------------------------------- |
|`Microsoft.BeginSkill`  | [BeginSkill][beginskill]    | Use the adaptive skill dialog to run a skill.                                                              |
|`Microsoft.HttpRequest` | [HttpRequest][httprequest]  | Enables you to make HTTP requests to any endpoint.                                                         |
|`Microsoft.EmitEvent`   | [EmitEvent][emitevent]      | Enables you to raise a custom event that your bot can respond to using a [custom trigger][custom-trigger]. |
|`Microsoft.SignOutUser` | [SignOutUser][sign-out-user]| Enables you to sign out the currently signed in user.                                                      |
<!-- NOTE: codeaction is not available via declarative. -->

#### Debugging options

| `$kind` value           | Action Name                     | What this action does                                                      |
| ----------------------- | ------------------------------- | -------------------------------------------------------------------------- |
|`Microsoft.LogAction`    | [LogAction][log-action]        | Writes to the console and optionally sends the message as a trace activity. |
|`Microsoft.TraceActivity`| [TraceActivity][traceactivity] | Sends a trace activity with whatever payload you specify.                   |

[log-action]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#log-action
[traceactivity]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#traceactivity
[emitevent]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#emitevent
[sign-out-user]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#sign-out-user
[editactions]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#editactions

### Recognizers

The Bot Framework SDK provides over a half dozen different recognizers, you specify which recognizer to use in the .dialog file, for example:

```json
{
    "$schema": "../app.schema",
    "$kind": "Microsoft.AdaptiveDialog",
    "recognizer": {
        "$kind": "Microsoft.RegexRecognizer",
        "intents": [
            {
                "intent": "JokeIntent",
                "pattern": "(?i)joke"
            },
            {
                "intent": "FortuneTellerIntent",
                "pattern": "(?i)fortune|future"
            },
            {
                "intent": "CancelIntent",
                "pattern": "(?i)cancel"
            }
        ]
    },
    ...
}
```

Adaptive dialogs support the following recognizers:

- [RegexRecognizer](bot-builder-concept-adaptive-dialog-recognizers.md#regexrecognizer)
- [LUIS recognizer](bot-builder-concept-adaptive-dialog-recognizers.md#luis-recognizer)
- [QnA Maker recognizer](bot-builder-concept-adaptive-dialog-recognizers.md#qna-maker-recognizer)
- [Multi-language recognizer](bot-builder-concept-adaptive-dialog-recognizers.md#multi-language-recognizer)
- [CrossTrained recognizer set](bot-builder-concept-adaptive-dialog-recognizers.md#cross-trained-recognizer-set)
- [RecognizerSet](bot-builder-concept-adaptive-dialog-recognizers.md#recognizer-set)

### Generators

The [generator][generator] value contains a link to the .lg file associated with the adaptive dialog that this .dialog file defines. For example:

```json
{
    "$schema": "../app.schema",
    "$kind": "Microsoft.AdaptiveDialog",
    "generator": "MyBotGeneratorFile.lg",
    ...
}
```

## The Bot Framework Command-Line Interface

Several new [Bot Framework Command-Line Interface (BF CLI)][bf-cli] commands were added with the release of adaptive dialogs in the Bot Framework SDK. This includes two dialog related commands for working with `.dialog` and `.schema` files that are very useful when using the declarative approach to adaptive dialog development.

The new CLI `dialog` group has the following two commands: `dialog:merge` and `dialog:verify`.

### The merge command

The root schema file contains the schemas of all the components that are consumed by your bot. Every consumer of declarative files, including [Composer][composer], needs a schema file.

The `dialog:merge` command is used to generate your project's schema file. You will need to run this command anytime you add a new package or create or modify your own components.

This creates a file named **app.schema** in the current directory, unless specified otherwise using the `-o` option. This file is referenced by the `"$schema` keyword in each of the `.dialog` files in your project.

> [!NOTE]
>
> A valid app.schema file is required for _Intelligent code completion_ tools such as [IntelliSense][intelliSense] to work with any of the declarative assets.

To use the merge command, enter the following at the command prompt, while in the root directory of your project:

```cli
bf dialog:merge <filename.csproj>
```

For additional information on using this command, see [bf dialog:merge][bf-dialogmerge-patterns] in the BF CLI LUIS `README`.

For an example see [Creating the schema file][creating-the-schema-file] in the _Create a bot using declarative adaptive dialogs_ article.

<!--
> [!TIP]
>
> For users of C#: NuGet does not deal well with content files, so all declarative `.dialog`, .lu, .lg, and .qna files will be copied into `generated/<package>` so you can easily include them in your project output.
-->

### The verify command

The `dialog:verify` command will check `.dialog` files to verify that they are compatible with the schema.

To use the `verify` command, enter the following at the command prompt, while in the root directory of your project:

```cli
bf dialog:verify <filename.csproj>
```

For additional information on using this command, see [bf dialog:verify][bf-dialogverify-patterns] in the BF CLI LUIS `README`.

> [!NOTE]
>
> [Composer][composer] creates a merged `.schema` file and valid `.dialog` files; however,
> the verify command can be very useful if you're creating these files by hand.

### Install the Bot Framework CLI

[!INCLUDE [Install the Bot Framework CLI](../includes/install-bf-cli.md)]

### Relevant information

- [Dialog Commands][dialog-commands]

## Additional information

- How to [Create a bot using declarative adaptive dialogs](bot-builder-dialogs-declarative.md)

<!-- Footnote-style links -->
[declarative-ref]: ../adaptive-dialog/adaptive-dialog-prebuilt-declarative.md
[triggers]: /composer/concept-events-and-triggers
[actions]: /composer/concept-dialog#dialog-actions
[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md
[concept-adaptive]: /composer/introduction#adaptive-dialogs
[lg-file-format]: ../file-format/bot-builder-lg-file-format.md
[creating-the-schema-file]: bot-builder-dialogs-declarative.md#creating-the-schema-file

<!-- Declarative files section  -->
[bf-cli]: /azure/bot-service/bf-cli-overview
[bf-cli-install]: /azure/bot-service/bf-cli-overview#installation
[intellisense]: /visualstudio/ide/using-intellisense
[recognizer-types]: ../adaptive-dialog/adaptive-dialog-prebuilt-recognizers.md
[intents]: ../file-format/bot-builder-lu-file-format.md#intent
[entity]: ../file-format/bot-builder-lu-file-format.md#entity
[generator]: /composer/concept-language-generation

<!-- (Triggers) Recognizer event triggers
[cross-trained-recognizer-set]:bot-builder-concept-adaptive-dialog-recognizers.md#cross-trained-recognizer-set
[qna-maker-recognizer]:bot-builder-concept-adaptive-dialog-recognizers.md#qna-maker-recognizer
[botframework-activity]: https://github.com/microsoft/botframework-sdk/blob/master/specs/botframework-activity/botframework-activity.md
  -->
<!-- (Actions) Input  -->
[attachmentinput]: /composer/how-to-ask-for-user-input#file-or-attachment-input
[choiceinput]: /composer/how-to-ask-for-user-input#multiple-choice-input
[confirminput]: /composer/how-to-ask-for-user-input#confirmation-input
[datetimeinput]: /composer/how-to-ask-for-user-input#date-or-time-input
[numberinput]: /composer/how-to-ask-for-user-input#number-input
[oauthinput]: /composer/how-to-use-oauth
[textinput]: /composer/how-to-ask-for-user-input#text-input
[send-activity]: /composer/how-to-define-triggers#activities

<!--  (Actions) Create a condition -->
[ifcondition]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#ifcondition
[switchcondition]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#switchcondition
[foreach]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#foreach
[foreachpage]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#foreachpage
[break-loop]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#break-loop
[continue-loop]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#continue-loop
[goto-action]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#goto-action

<!--  (Actions) Dialog management -->
[begindialog]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#begindialog
[cancelalldialogs]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#cancelalldialogs
[enddialog]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#enddialog
[endturn]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#endturn
[repeatdialog]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#repeatdialog
[replacedialog]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#replacedialog
[update-activity]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#update-activity
[delete-activity]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#delete-activity
[get-activity-members]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#get-activity-members
[get-conversation-members]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#get-conversation-members
[editactions]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#editactions
[memory-states]:bot-builder-concept-adaptive-dialog-memory-states.md
[interruptions]: bot-builder-concept-adaptive-dialog-interruptions.md

<!--  (Actions) Manage properties -->
[editarray]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#editarray
[deleteproperty]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#deleteproperty
[deleteproperties]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#deleteproperties
[setproperty]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#setproperty
[setproperties]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#setproperties

<!--  (Actions) Access external resources -->
[beginskill]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#beginskill
[httprequest]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#httprequest
[emitevent]: /composer/concept-events-and-triggers#custom-events
[sign-out-user]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#sign-out-user
[custom-trigger]: ../adaptive-dialog/adaptive-dialog-prebuilt-triggers.md#custom-event-trigger
<!--[codeaction]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#codeaction-->

<!--  (Actions) Debugging options -->
[log-action]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#log-action
[traceactivity]: ../adaptive-dialog/adaptive-dialog-prebuilt-actions.md#traceactivity

[intelliSense]: /visualstudio/ide/using-intellisense
[composer]: https://learn.microsoft.com/composer

[dialog-commands]: https://github.com/microsoft/botframework-cli/tree/main/packages/cli#bf-dialog
[bf-dialogverify-patterns]: https://github.com/microsoft/botframework-cli/tree/main/packages/cli#bf-dialogverify-patterns
[bf-dialogmerge-patterns]: https://github.com/microsoft/botframework-cli/tree/main/packages/cli#bf-dialogmerge-patterns

[the-dialog-file-luis]: bot-builder-howto-bf-cli-deploy-luis.md#the-dialog-file
[the-dialog-file-qnamaker]: bot-builder-howto-bf-cli-deploy-qna.md#the-dialog-file

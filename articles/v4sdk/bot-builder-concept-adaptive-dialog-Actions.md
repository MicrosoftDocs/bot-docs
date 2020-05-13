---
title: Actions - Bot framework SDK - adaptive dialogs
description: Collecting user input using adaptive dialogs
keywords: bot, user, Events, triggers, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 04/27/2020
---
<!--P2: Once the samples are done, link to them in each section on the individual actions to point to them as examples of how they are used-->
# Actions in adaptive dialogs

Actions help to create and maintain the bots conversation flow once an event is captured by a [trigger][2]. In a similar way that adaptive dialogs contain a list of triggers, triggers contain a list of actions that once the trigger fires, will execute to accomplish any set of actions needed, such as satisfying a user's request. In addition to creating and maintaining the bot's conversational flow, you can use actions to send messages, respond to user questions using a [knowledge base][3], make calculations, and perform any number of computational tasks for the user. With adaptive dialogs, the path the bot flows through in a dialog can branch and loop. The bot can ask and answer questions, validate the users input, manipulate and store values in memory, and make decisions based on user input.

> [!IMPORTANT]
> Actions are dialogs and any dialog can be used as an action, so actions have all of the power and flexibility you need to create a fully functional and robust bot. While the actions included in the bot framework SDK are extensive, you can also create your own custom actions to perform virtually any specialized task or process you need.

## Prerequisites

* [Introduction to adaptive dialogs][1]
* [Events and triggers in adaptive dialogs][2].

## Actions

Actions that are included with the bot framework SDK provide the ability to perform conditional coding such as:

* Branching and looping
* Dialog management tasks such as starting a new dialog or cancelling, ending or repeating a dialog.
* Memory management tasks such as creating, deleting or editing a property saved in memory.
* Accessing external resources such as sending an [HTTP Request](#httprequest).
* Preforming an [OAuth login request][4] and many others.

>[!TIP]
> Unlike a waterfall dialog where each step is a function, each action in an adaptive dialog is a fully functional dialog with all of the power and flexibility that entails. This enables adaptive dialogs by design to:
>
> * Provide an easier way to handle [interruptions][6].
> * Branch conditionally based on context or state.

Adaptive dialogs support the following actions:

### Sending a response

| Activity to accomplish                         | Action Name                   | What this action does                  |
| ---------------------------------------------- | ----------------------------- | -------------------------------------- |
| Send any activity such as responding to a user.| [SendActivity](#sendactivity) | Enables you send any type of activity such as responding to users or in the Teams channel, you could send an invoke activity to get information about a user or thread.  |

### Requesting user input

See [Asking for user input using adaptive dialogs][7].

>[!TIP]
> Inputs are an important and very useful type of action that is covered in the [Inputs in adaptive dialogs][5] article.

### Create a condition

The first two actions are conditional statements designed to help your bot make decisions based on any pre-defined condition that you have created. These conditions are specified by a set of conditional statements having boolean expressions which are evaluated to a boolean value of true or false.

The remaining actions relate to looping statements which enable you to repeat the execution of a block of code for every element in a collection.

| Activity to accomplish | Action Name                | What this action does                                                                                |
| ---------------------- | -------------------------- | ---------------------------------------------------------------------------------------------------- |
| Branch: if/else        | [IfCondition](#ifcondition)| Used to create If and If-Else statements which are used to execute code only if a condition is true. |
| Branch: Switch (Multiple options) | [SwitchCondition](#switchcondition) | Used to build a multiple-choice menu.                                            |
| Loop: for each item    | [ForEach](#foreach)        | Loop through a set of values stored in an array.                                                     |
| Loop: for each page (multiple items) | [ForEachPage](#foreachpage) | Loop through a large set of values stored in an array one page at a time.             |
| Exit a loop            | [BreakLoop](#break-loop)   | Break out of a loop.                                                                                 |
| Continue a loop        | [ContinueLoop](#continue-loop) | Continue the loop.                                                                               |

### Dialog management

| Activity to accomplish | Action Name                      | What this action does                                                     |
| ---------------------- | -------------------------------- | ------------------------------------------------------------------------- |
| Begin a new dialog     | [BeginDialog](#begindialog)      | Begins executing another dialog which when, will return to the caller.    |
| Cancel dialog          | [CancelDialog](#cancelalldialog)| Cancels the active dialog. Use when you want the dialog to close immediately, even if that means stopping mid-process. Emits the `CancelDialog` event.|
| Cancel all dialogs     | [CancelAllDialogs](#cancelalldialogs)| Cancels all active dialogs including any active parent dialogs. Use this if you want to pop all dialogs off the stack, you can clear the dialog stack by calling the dialog context's cancel all dialogs method. Emits the `CancelAllDialogs` event.|
| End this dialog        | [EndDialog](#enddialog)          | Ends the active dialog.  Use when you want the dialog to complete and return results before ending. Emits the `EndDialog` event.|
| End dialog turn        | [EndTurn](#endturn)              | Ends the current turn of conversation without ending the dialog.          |
| Repeat this dialog     | [RepeatDialog](#repeatdialog)    | Used to restart the parent dialog.                                        |
| Replace this dialog    | [ReplaceDialog](#replacedialog)  | Replaces the current dialog with a new dialog                             |
| Update an activity     | [UpdateActivity](#updateactivity)| This enables you to update an activity that was sent.                     |
| DeleteActivity        | [DeleteActivity](#deleteactivity) | Enables you to delete an activity that was sent.                          |
| Get activity members | [GetActivityMembers](#getactivitymembers)| Enables you to get a list of activity members and save it to a property in memory.|
| GetConversationMembers| [GetConversationMembers](#getconversationmembers) | Enables you to get a list of the conversation members and save it to a property in memory.|
| EditActions    | [EditActions](#editactions) | Enables you to edit the current action on the fly based on user input. Especially useful when handling [interruptions][6]. |

### Manage properties

| Activity to accomplish | Action Name                           | What this action does                                                     |
| ---------------------- | ------------------------------------- | ------------------------------------------------------------------------- |
| Edit an array          | [EditArray](#editarray)               | This enables you to perform edit operations on an array.                  |
| Delete a property      | [DeleteProperty](#deleteproperty)     | This enables you to remove a property from memory.                        |
| Delete properties      | [DeleteProperties](#deleteproperties) | This enables you to delete more than one property in a single action.     |
| Set a property         | [SetProperty](#setproperty)           | This enables you to set a property's value in memory.                     |
| Set properties         | [SetProperties](#setproperties)       | This enables you to initialize one or more properties in a single action. |

### Access external resources

| Activity to accomplish | Action Name                | What this action does                                                                                       |
| ---------------------- | -------------------------- | ----------------------------------------------------------------------------------------------------------- |
| Begin a skill dialog   | [AdaptiveSkillDialog](#adaptiveskilldialog) | TBD:TODO                                                                                   |
| Send an HTTP request   | [HttpRequest](#httprequest)| Enables you to make HTTP requests to any endpoint.                                                          |
| Emit a custom event    | [EmitEvent](#emitevent)    | Enables you to raise a custom event that your bot can respond to using a [custom trigger][8].               |
| Sign out a user        | [SignOutUser](#signoutuser)| Enables you to sign out the currently signed in user.                                                       |
| Call custom code       | [CodeAction](#codestep)    | Enables you to call your own custom code.                                                                   |

<!--| Connect to a QnA KB    | [OnQnMakerDialog](#qnamakerdialog)| Why use this since Once QnA Maker is set as a recognizer on a dialog, you can use the OnQnAMatch trigger to render the answer.|-->

> [!IMPORTANT]
> **TODO**: Need information and/or sample code for:
>
> 1. **"Begin a skill dialog"**
> 2. **"Connect to a QnA Knowledgebase" NOTE:** ***YOU DON'T NEED OR WANT TO CONNECT USING THE OnQnMakerDialog - CONNECT TO THE RECOGNIZER THEN USE THE OnQnAMatch TRIGGER***

### Debugging options


| Activity to accomplish | Action Name                     | What this action does                                                       |
| ---------------------- | ------------------------------- | --------------------------------------------------------------------------- |
| Log to console         | [LogAction](#logstep)           | Writes to the console and optionally sends the message as a trace activity. |
| Emit a trace event     | [TraceActivity](#traceactivity) | Enables you to send trace activities with what ever payload you specify.    |

<!-------------------------------------------------------------------------------------------------------------------------------------->
<!-------------------------------------------------------------------------------------------------------------------------------------->

## Source code examples

### Sending a response example

#### Send activity

Sends an activity.

``` C#
// Example of a simple SendActivity step
var greetUserDialog = new AdaptiveDialog("greetUserDialog");
greetUserDialog.Triggers.Add(new OnIntent()
{
    Intent = "greetUser", 
    Actions = new List<Dialog>() {
        new SendActivity("Hello")
    }
});

// Example that includes reference to property on bot state.
var greetUserDialog = new AdaptiveDialog("greetUserDialog");
greetUserDialog.Triggers.Add(new OnIntent()
{
    Intent = "greetUser",
    Actions = new List<Dialog>()
    {
        new TextInput()
        {
            Property = "user.name",
            Prompt = new ActivityTemplate("What is your name?")
        },
        new SendActivity("Hello, ${user.name}")
    }
});
```

See [language generation in adaptive dialogs][9] to learn more about using language generation instead of hard coding actual response text in the _send activity_ action.

### Create a condition examples

#### IfCondition

Branches the conversational flow based on a specific condition. Conditions are expressed using [the common expression language][10]. 

``` C#
var addToDoDialog = new AdaptiveDialog("addToDoDialog");
addToDoDialog.Triggers.Add(new OnIntent()
{
    Intent = "addToDo",
    Actions = new List<Dialog>()
    {
        // Save the userName entity from a recognizer.
        new SaveEntity("dialog.addTodo.title", "@todoTitle"),
        new TextInput()
        {
            Prompt = new ActivityTemplate("What is the title of your todo?"),
            Property = "dialog.addTodo.title"
        },
        // Add the current todo to the todo's list for this user.
        new EditArray()
        {
            ItemsProperty = "user.todos",
            Value = "=dialog.addTodo.title"
            ChangeType = EditArray.ArrayChangeType.Push
        },
        new SendActivity("Ok, I have added ${dialog.addTodo.title} to your todos."),
        new IfCondition()
        {
            Condition = "toLower(dialog.addTodo.title) == 'call santa'",
            Actions = new List<Dialog>()
            {
                new SendActivity("Yes master. On it right now \\[You have unlocked an easter egg] :)")
            }
        },
        new SendActivity("You now have ${count(user.todos)} items in your todo.")
    }
});
```

#### SwitchCondition

Branches a conversational flow based on the outcome of an expression evaluation. See [the common expression language][10] for more information.

``` C#
// Create an adaptive dialog.
var cardDialog = new AdaptiveDialog("cardDialog");
cardDialog.Triggers.Add(new OnIntent()
{
    Intent = "ShowCards",
    Actions = new List<Dialog>()
    {
        // Add choice input.
        new ChoiceInput()
        {
            // Output from the user is automatically set to this property
            Property = "turn.cardDialog.cardChoice",

            // List of possible styles supported by choice prompt.
            Style = ListStyle.Auto,
            Prompt = new ActivityTemplate("What card would you like to see?"),
            Choices = new ChoiceSet(new List<Choice>() {
                new Choice("Adaptive card"),
                new Choice("Hero card"),
                new Choice("Video card")
            })
        },
        // Use SwitchCondition step to dispatch to right dialog based on choice input.
        new SwitchCondition()
        {
            Condition = "turn.cardDialog.cardChoice",
            Cases = new List<Case>()
            {
                new Case("Adaptive card",  new List<Dialog>() { new SendActivity("${AdaptiveCardRef()}") } ),
                new Case("Hero card", new List<Dialog>() { new SendActivity("${HeroCard()}") } ),
                new Case("Video card",     new List<Dialog>() { new SendActivity("${VideoCard()}") } ),
            },
            Default = new List<Dialog>()
            {
                new SendActivity("[AllCards]")
            }
        }
}));
```

#### ForEach

The Foreach loop is a convenient way to retrieve elements of an array or a collection. It is often used to perform action on each item in a collection.

```C#
var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
{
    Generator = new TemplateEngineLanguageGenerator(),
    Triggers = new List<OnCondition>()
    {
        new OnUnknownIntent()
        {
            Actions = new List<Dialog>()
            {
                new SetProperty()
                {
                    Property = "turn.colors",
                    Value = "=createArray('red', 'blue', 'green', 'yellow', 'orange', 'indigo')"
                },
                new Foreach()
                {
                    ItemsProperty = "turn.colors",
                    Actions = new List<Dialog>()
                    {
                        // By default, dialog.foreach.value holds the value of the item
                        // dialog.foreach.index holds the index of the item.
                        // You can use short hands to refer to these via
                        //      $foreach.value
                        //      $foreach.index
                        new SendActivity("${$foreach.index}: Found '${$foreach.value}' in the collection!")
                    }
                }
            }
        }
    }
};
```

#### ForEachPage

Retrieves consecutive pages of results from a collection. Page size denotes how many items from the collection are selected at a time.

```C#
var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
{
    Generator = new TemplateEngineLanguageGenerator(),
    Triggers = new List<OnCondition>()
    {
        new OnUnknownIntent()
        {
            Actions = new List<Dialog>()
            {
                new SetProperty()
                {
                    Property = "turn.colors",
                    Value = "=createArray('red', 'blue', 'green', 'yellow', 'orange', 'indigo')"
                },
                new ForeachPage()
                {
                    ItemsProperty = "turn.colors",
                    PageSize = 2,
                    Actions = new List<Dialog>()
                    {
                        // By default, dialog.foreach.page holds the value of the page
                        //      $foreach.page
                        new SendActivity("Page content: ${join($foreach.page, ', ')}")
                    }
                }
            }
        }
    }
};
```

#### Break Loop

Break out of a loop

```C#
var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
{
    Generator = new TemplateEngineLanguageGenerator(),
    Triggers = new List<OnCondition>()
    {
        new OnUnknownIntent()
        {
            Actions = new List<Dialog>()
            {
                new SetProperty()
                {
                    Property = "turn.colors",
                    Value = "=createArray('red', 'blue', 'green', 'yellow', 'orange', 'indigo')"
                },
                new Foreach()
                {
                    ItemsProperty = "turn.colors",
                    Actions = new List<Dialog>()
                    {
                        new IfCondition()
                        {
                            Condition = "$foreach.value == 'green'",
                            Actions = new List<Dialog>()
                            {
                                new BreakLoop()
                            },
                            ElseActions = new List<Dialog>()
                            {
                                // By default, dialog.foreach.value holds the value of the item
                                // dialog.foreach.index holds the index of the item.
                                // You can use short hands to refer to these via 
                                //      $foreach.value
                                //      $foreach.index
                                new SendActivity("${$foreach.index}: Found '${$foreach.value}' in the collection!")
                            }
                        },
                    }
                }
            }
        }
    }
};
```

#### Continue Loop

Continue current loop without processing rest of the statements within the loop.

```C#
var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
{
    Generator = new TemplateEngineLanguageGenerator(),
    Triggers = new List<OnCondition>()
    {
        new OnUnknownIntent()
        {
            Actions = new List<Dialog>()
            {
                new SetProperty()
                {
                    Property = "turn.colors",
                    Value = "=createArray('red', 'blue', 'green', 'yellow', 'orange', 'indigo')"
                },
                new Foreach()
                {
                    ItemsProperty = "turn.colors",
                    Actions = new List<Dialog>()
                    {
                        new IfCondition()
                        {
                            // Skip items at even position in the collection.
                            Condition = "$foreach.index % 2 == 0",
                            Actions = new List<Dialog>()
                            {
                                new ContinueLoop()
                            },
                            ElseActions = new List<Dialog>()
                            {
                                // By default, dialog.foreach.value holds the value of the item
                                // dialog.foreach.index holds the index of the item.
                                // You can use short hands to refer to these via 
                                //      $foreach.value
                                //      $foreach.index
                                new SendActivity("${$foreach.index}: Found '${$foreach.value}' in the collection!")
                            }
                        },
                    }
                }
            }
        }
    }
};
```

#### Goto action

Jump to a labelled action within the current action scope.

```C#
var adaptiveDialog = new AdaptiveDialog()
{
    Triggers = new List<OnCondition>()
    {
        new OnBeginDialog()
        {
            Actions = new List<Dialog>()
            {
                new GotoAction()
                {
                    ActionId = "end"
                },
                new SendActivity("this will be skipped."),
                new SendActivity()
                {
                    Id = "end",
                    Activity = new ActivityTemplate("The End.")
                }
            }
        }
    }
};
```

### Dialog management examples

#### BeginDialog

Invoke and begin a new dialog. `BeginDialog` requires the name of the target dialog, which can be any type of dialog, it is not limited to Adaptive dialogs.

The `BeginDialog` action defines a property named `ResultProperty` that allows you to specify where to save the results when the dialog ends. By default, `resultProperty` is set to `dialog.results` so anything that is set in that [memory scope][11] will automatically be returned to the caller when the adaptive dialog ends.

``` C#
new BeginDialog("BookFlightDialog")
{
    // Any value returned by BookFlightDialog will be captured in the property specified here.
    ResultProperty = "$bookFlightResult"
}
```

> [!TIP]
> Just like when invoking any dialog in the bot framework SDK, when calling `BeginDialog` to invoke an adaptive dialog, you can use the `options` parameter to pass input information for the dialog.

#### EndDialog

Ends the active dialog. By default, adaptive dialogs have a `resultProperty` set to `dialog.results`' so anything that is set in that memory scope will automatically be returned to the caller when the dialog ends. In addition the `EndDialog` action has the `value` property which contains a value that is passed back to the caller of the dialog.

``` C#
new EndDialog()
{
    // Value property indicates value to return to the caller.
    Value = "=$userName"
}
```

> [!TIP]
> Adaptive dialogs will end automatically by default if the dialog has completed running all of its actions. To override this behavior, set the `AutoEndDialog` property on Adaptive Dialog to false.

#### CancelAllDialog

Cancels all active dialogs including any active parent dialogs.

``` C#
new CancelAllDialog()
```

#### EndTurn

Ends the current turn of conversation without ending the dialog.

``` C#
new EndTurn()
```

#### RepeatDialog

Repeat dialog will restart the parent dialog. This is particularly useful if you are trying to have a conversation where the bot is paging results to the user that they can navigate through.

> [!IMPORTANT]
> Make sure to use `EndTurn()` or one of the Inputs to collect information from the user so you do not accidentally end up implementing an infinite loop.

``` C#
var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
{
    Triggers = new List<OnCondition>()
    {
        new OnUnknownIntent()
        {
            Actions = new List<Dialog>()
            {
                new TextInput()
                {
                    Prompt = new ActivityTemplate("Give me your favorite color. You can always say cancel to stop this."),
                    Property = "turn.favColor",
                },
                new EditArray()
                {
                    ArrayProperty = "user.favColors",
                    Value = "=turn.favColor",
                    ChangeType = EditArray.ArrayChangeType.Push
                },
                // This is required because TextInput will skip prompt if the property exists - which it will from the previous turn.
                // Alternately you can also set `AlwaysPrompt = true` on the TextInput.
                new DeleteProperty() {
                    Property = "turn.favColor"
                },
                // Repeat dialog step will restart this dialog.
                new RepeatDialog()
            }
        },
        new OnIntent("CancelIntent")
        {
            Actions = new List<Dialog>()
            {
                new SendActivity("You have ${count(user.favColors)} favorite colors - ${join(user.favColors, ',', 'and')}"),
                new EndDialog()
            }
        }
    },
    Recognizer = new RegexRecognizer()
    {
        Intents = new List<IntentPattern>()
        {
            new IntentPattern()
            {
                Intent = "HelpIntent",
                Pattern = "(?i)help"
            },
            new IntentPattern()
            {
                Intent = "CancelIntent",
                Pattern = "(?i)cancel|never mind"
            }
        }
    }
};
```

#### ReplaceDialog

Replace current dialog with a new dialog by name.

``` C#
// This sample illustrates the use of ReplaceDialog tied to explicit user confirmation
// to switch to a different dialog.

// Create an adaptive dialog.
var getUserName = new AdaptiveDialog("getUserName");
getUserName.Triggers.Add(new OnIntent()
{
    Intent = "getUserName",
    Actions = new List<Dialog>()
    {
        new TextInput()
        {
            Property = "user.name",
            Prompt = new ActivityTemplate("What is your name?")
        },
        new SendActivity("Hello ${user.name}, nice to meet you!")
    }
});

getUserName.Triggers.Add(new OnIntent()
{
    Intent = "GetWeather",
    Actions = new List<Dialog>()
    {
        // confirm with user that they do want to switch to another dialog
        new ChoiceInput()
        {
            Prompt = new ActivityTemplate("Are you sure you want to switch to talk about the weather?"),
            Property = "turn.contoso.getWeather.confirmChoice",
            Choices = new ChoiceSet(new List<Choice>()
            {
                new Choice("Yes"),
                new Choice("No")
            })
        },
        new SwitchCondition()
        {
            Condition = "turn.contoso.getWeather.confirmChoice",
            Cases = new List<Case>()
            {
                // Call ReplaceDialog to switch to a different dialog.
                // BeginDialog will keep current dialog in the stack to be resumed after child dialog ends.
                // ReplaceDialog will remove current dialog from the stack and add the new dialog.
                {
                    Value = "Yes",
                    Actions = new List<Dialog>()
                    {
                        new ReplaceDialog("getWeatherDialog")
                    }
                },
                {
                    Value = "No",
                    Actions = new List<Dialog>()
                    {
                        new EndDialog()
                    }
                }
            }
        }
    }
});
```

#### Update activity

Update an activity that was sent.

```C#
new UpdateActivity ()
{
    ActivityId = "id",
    Activity = new ActivityTemplate("updated value")
}
```

#### Delete activity

Deletes an activity that was sent. You must provide the activities `activityId` to specify the activity to delete.

```C#
new DeleteActivity ()
{
    ActivityId = "id"
}
```

#### Get activity members

Gets the members of the activity associated with this turn and saves the list to a property.

```C#
new GetActivityMembers()
{
    Property = "turn.activityMembers"
}
```

#### Get conversation members

Gets the members of the current conversation and saves the list to a property.

```C#
new GetConversationMembers()
{
    Property = "turn.convMembers"
}
```

#### EditActions

Modifies the current sequence of actions. Specifically helpful when handling an interruption. You can use EditActions to insert or remove actions anywhere in the sequence, including adding actions to the beginning or end of the sequence.

``` C#
var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
{
    Generator = new TemplateEngineLanguageGenerator(),
    Recognizer = new RegexRecognizer()
    {
        Intents = new List<IntentPattern>()
        {
            new IntentPattern()
            {
                Intent = "appendSteps",
                Pattern = "(?i)append"
            },
            new IntentPattern()
            {
                Intent = "insertSteps",
                Pattern = "(?i)insert"
            },
            new IntentPattern()
            {
                Intent = "endSteps",
                Pattern = "(?i)end"
            }
        }
    },
    Triggers = new List<OnCondition>()
    {
        new OnUnknownIntent()
        {
            Actions = new List<Dialog>()
            {
                new ChoiceInput() 
                {
                    Prompt = new ActivityTemplate("What type of EditAction would you like to see?"),
                    Property = "$userChoice",
                    AlwaysPrompt = true,
                    Choices = new ChoiceSet(new List<Choice>()
                    {
                        new Choice("Append actions"),
                        new Choice("Insert actions"),
                        new Choice("End actions"),
                    })
                },
                new SendActivity("This message is after your EditActions choice..")
            }
        },
        new OnIntent()
        {
            Intent = "appendSteps",
            Actions = new List<Dialog>() {
                new SendActivity("In append steps .. Steps specified via EditSteps will be added to the current plan."),
                new EditActions()
                {
                    Actions = new List<Dialog>() {
                        // These steps will be appended to the current set of steps being executed.
                        new SendActivity("I was appended!")
                    },
                    ChangeType = ActionChangeType.AppendActions
                }
            }
        },
        new OnIntent() {
            Intent = "insertSteps",
            Actions = new List<Dialog>() {
                new SendActivity("In insert steps .. "),
                new EditActions()
                {
                    Actions = new List<Dialog>() {
                        // These steps will be inserted before the current steps being executed.
                        new SendActivity("I was inserted")
                    },
                    ChangeType = ActionChangeType.InsertActions
                }
            }
        },
        new OnIntent()
        {
            Intent = "endSteps",
            Actions = new List<Dialog>()
            {
                new SendActivity("In end steps .. "),
                new EditActions()
                {
                    // The current sequence will be ended. This is especially useful if you are looking to end an active interruption.
                    ChangeType = ActionChangeType.EndSequence
                }
            }
        }
    }
};
```

### Manage properties examples

#### SetProperty

Used to set a property's value in memory. The value can either be an explicit string or an expression. See [adaptive expressions][10] to learn more about expressions.

``` C#
new SetProperty()
{
    Property = "user.firstName",
    // If the value of user.name is 'Mahatma Gandhi', this sets first name to 'Mahatma'
    Value = "=split(user.name, ' ')[0]"
},
```

#### SetProperties

Initialize one or more properties in a single action.

```C#
new SetProperties()
{
    Assignments = new List<PropertyAssignment>()
    {
        new PropertyAssignment()
        {
            Property = "user.name",
            Value = "Vishwac"
        },
        new PropertyAssignment()
        {
            Property = "user.age",
            Value = "=coalesce($age, 30)"
        }
    }
}
```

#### DeleteProperty

Removes a property from memory.

``` C#
new DeleteProperty
{
    Property = "user.firstName"
}
```

#### DeleteProperties

Delete more than one property in a single action.

```C#
new DeleteProperties()
{
    Properties = new List<StringExpression>()
    {
        new StringExpression("user.name"),
        new StringExpression("user.age")
    }
}
```

#### EditArray

Used to perform edit operations on an array property.

``` C#
var addToDoDialog = new AdaptiveDialog("addToDoDialog");
addToDoDialog.Triggers.Add(new OnIntent()
{
    Intent = "addToDo",
    Actions = new List<Dialog>() {
        // Save the userName entity from a recognizer.
        new SaveEntity("dialog.addTodo.title", "@todoTitle"),
        new TextInput()
        {
            Prompt = new ActivityTemplate("What is the title of your todo?"),
            Property = "dialog.addTodo.title"
        },
        // Add the current todo to the todo's list for this user.
        new EditArray()
        {
            ItemsProperty = "user.todos",
            Value = "=dialog.addTodo.title"
            ChangeType = EditArray.ArrayChangeType.Push
        },
        new SendActivity("Ok, I have added ${dialog.addTodo.title} to your todos."),
        new SendActivity("You now have ${count(user.todos)} items in your todo.")
}));
```

### Access external resource examples

#### AdaptiveSkillDialog

<!-- TODO--->

#### HttpRequest

Use this to make HTTP requests to any endpoint.

``` C#
new HttpRequest()
{
    // Set response from the http request to turn.httpResponse property in memory.
    ResultProperty = "turn.httpResponse",
    Method = HttpRequest.HttpMethod.POST,
    Header = new Dictionary<string,string> (), /* request header */
    Body = { }                                 /* request body */
};

```

<!--TODO P1: Would be good to call out that the properties support data binding. So you can have reference to memory in URI, body etc.-->

<!--
#### OnQnAMakerDialog

Use this to to a [QnA Maker][12] knowledge base. To see sample code demonstrating how to implement QnA Maker in adaptive dialogs, clone the GitHub repository [BotBuilder-Samples][16]. <!-Need details like sample app's name etc--->
-->

#### EmitEvent

Used to raise a custom event that your bot can respond to. You can control bubbling behavior on the event raised so it can be contained just to your own dialog or bubbled up the parent chain.

```C#
var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
    {
        Generator = new TemplateEngineLanguageGenerator(),
        Triggers = new List<OnCondition>()
        {
            new OnUnknownIntent()
            {
                Actions = new List<Dialog>()
                {
                    new TextInput()
                    {
                        Prompt = new ActivityTemplate("What's your name?"),
                        Property = "user.name",
                        AlwaysPrompt = true,
                        OutputFormat = "toLower(this.value)"
                    },
                    new EmitEvent()
                    {
                        EventName = "contoso.custom",
                        EventValue = "=user.name",
                        BubbleEvent = true,
                    },
                    new SendActivity("Your name is ${user.name}"),
                    new SendActivity("And you are ${$userType}")
                }
            },
            new OnDialogEvent()
            {
                Event = "contoso.custom",

                // You can use conditions (expression) to examine value of the event as part of the trigger selection process.
                Condition = "turn.dialogEvent.value && (substring(turn.dialogEvent.value, 0, 1) == 'v')",
                Actions = new List<Dialog>()
                {
                    new SendActivity("In custom event: '${turn.dialogEvent.name}' with the following value '${turn.dialogEvent.value}'"),
                    new SetProperty()
                    {
                        Property = "$userType",
                        Value = "VIP"
                    }
                }
            },
            new OnDialogEvent()
            {
                Event = "contoso.custom",

                // You can use conditions (expression) to examine value of the event as part of the trigger selection process.
                Condition = "turn.dialogEvent.value && (substring(turn.dialogEvent.value, 0, 1) == 's')",
                Actions = new List<Dialog>()
                {
                    new SendActivity("In custom event: '${turn.dialogEvent.name}' with the following value '${turn.dialogEvent.value}'"),
                    new SetProperty()
                    {
                        Property = "$userType",
                        Value = "Special"
                    }
                }
            },
            new OnDialogEvent()
            {
                Event = "contoso.custom",
                Actions = new List<Dialog>()
                {
                    new SendActivity("In custom event: '${turn.dialogEvent.name}' with the following value '${turn.dialogEvent.value}'"),
                    new SetProperty()
                    {
                        Property = "$userType",
                        Value = "regular customer"
                    }
                }
            }
        }
    };
```

#### Sign out user

Sign out current signed in user.

```C#
new SignOutUser()
{
    UserId = "userid",
    ConnectionName = "connection-name"
}
```

#### CodeAction

As the name implies, this action enables you to call a custom piece of code.

``` C#
// Example customCodeStep method
private async Task<DialogTurnResult> CodeActionSampleFn(DialogContext dc, System.Object options)
{
    await dc.Context.SendActivityAsync(MessageFactory.Text("In custom code step"));
    // This code step decided to just return the input payload as the result.
    return dc.EndDialogAsync(options)
}

// Adaptive dialog that calls a code step.
var rootDialog = new AdaptiveDialog(rootDialogName) {
    Triggers = new List<OnCondition>()
    {
        new OnUnknownIntent()
        {
            Actions = new List<Dialog>()
            {
                new CodeAction(CodeActionSampleFn),
                new SendActivity("After code step")
            }
        }
    }
};
```

### Debugging option examples

#### TraceActivity

Sends a trace activity with a payload you specify.

> [!NOTE]
> Trace activities can be captured as transcripts and are only sent to Emulator to help with debugging.

``` C#
new TraceActivity()
{
    // Name of the trace event.
    Name = "contoso.TraceActivity",
    ValueType = "Object",
    // Property from memory to include in the trace
    ValueProperty = "user"
}
```

#### LogStep

Writes to the console and optionally sends the message as a trace activity.

``` C#
new LogStep()
{
    Text = new TextTemplate("Hello"),
    // Automatically sends the provided text as a trace activity
    TraceActivity = true
}
```

## Additional Information

* To learn about actions specific to gathering user input, see the [asking for user input using adaptive dialogs][7] article.
* To learn more about adaptive expressions see the [adaptive expressions][10] article.

[1]:bot-builder-adaptive-dialog-introduction.md
[2]:bot-builder-adaptive-dialog-triggers.md
[3]:https://www.qnamaker.ai/
[4]:bot-builder-adaptive-dialog-inputs.md#oauth
[5]:bot-builder-concept-dialog.md
[6]:NeedLinkToInterruptionsInfo/WhereIsIt/IDK/bot-builder-adaptive-dialog-input.md#interruptions
[7]:bot-builder-adaptive-dialog-input.md
[8]:bot-builder-adaptive-dialog-triggers.md#custom-events
[9]:bot-builder-adaptive-dialog-generation.md
[10]:PlaceholderFor-adaptiveExpressions
[11]:bot-builder-adaptive-dialog-memoryscopes.md
[12]:https://www.qnamaker.ai/
[13]:https://docs.microsoft.com/azure/bot-service/bot-builder-tutorial-add-qna?view=azure-bot-service-4.0&tabs=csharp
[14]:https://docs.microsoft.com/azure/bot-service/bot-builder-howto-qna?view=azure-bot-service-4.0&tabs=cs
[15]:https://docs.microsoft.com/dotnet/api/microsoft.bot.builder.ai.qna.dialogs.qnamakerdialog?view=botbuilder-dotnet-stable
[16]:https://github.com/microsoft/BotBuilder-Samples

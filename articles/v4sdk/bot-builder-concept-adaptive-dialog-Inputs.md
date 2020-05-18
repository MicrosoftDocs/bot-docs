---
title: Asking for user input in adaptive dialogs
description: Collecting input from users using adaptive dialogs
keywords: bot, user, actions, triggers, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 04/27/2020
---
<!--TODO P2: For each of the inputs in this article, link to relevant samples in BotBuilder-Samples as examples of how that input is used-->
# Asking for user input in adaptive dialogs

The Bot Framework SDK defines a variety of input dialogs for collecting and validating user input.

> [!IMPORTANT]
> Actions (_trigger actions_) are dialogs, and as such have all of the power and flexibility you need to create a fully functional and robust conversation flow. While the actions included in the bot framework SDK are extensive, you can also create your own custom actions to perform virtually any specialized task or process you need.

## Prerequisites

* A general understanding of adaptive dialogs in the Bot Framework V4 SDK is helpful. For more information, see an [Introduction to adaptive dialogs][1].
* A general understanding of [Events and triggers in adaptive dialogs][2].
* A general understanding of [Actions in adaptive dialogs][3].
* A general understanding of [Memory scopes and managing state in adaptive dialogs][11].
* A familiarity with [Language Generation templates][14].
* A familiarity with [Adaptive expressions][10].

> [!TIP]
> This syntax defined in the [Language Generation templates][14], which includes [Adaptive expressions][10], is used in the `ActivityTemplate` object that is required for several parameters that are used in most of the input actions provided in the bot framework SDK.

## Inputs

Similar to [prompts][4], you can use _inputs_ in adaptive dialogs to ask for and collect input from a user, validate it, and accept it into memory. An input:

* Binds the prompt result to a property in a [state management][6] scope.
* Prompts the user only if the result property doesn't already have a value.
* Saves the input to the specified property if the input from user matches the type of entity expected.
* Accepts validation constraints such as min, max, and so on.
* Can use as input locally relevant intents within a dialog as well as use interruption as a technique to bubble up user response to an appropriate parent dialog that can handle it.

<!-- TODO P0.5: For more information, see [about interruptions in adaptive dialogs](./ all-about-interruptions.md). -->

The adaptive dialogs library defines the following input types:

| Input type       | Input class                       | Description                                          | Returns                                      |
| ---------------- | --------------------------------- | ---------------------------------------------------- | -------------------------------------------- |
| Base class       | [InputDialog](#inputdialog)       | This is the base class that all of the input classes derive from. It defines all shared properties. |
| Text             | [TextInput](#textinput)           | Used to ask your users for a **word or sentence**.   | A string.                                    |
| Number           | [NumberInput](#numberinput)       | Used to ask your users for a **number**.             | A numeric value.                             |
| Confirmation     | [ConfirmInput](#confirminput)     | Used to request a **confirmation** from the user.    | A Boolean value.                             |
| Multiple choice  | [ChoiceInput](#choiceinput)       | Used to asks for a choice from a **set of options**. | The value or index of the selection.         |
| File or attachment |[AttachmentInput](#attachmentinput)| Used to request/enable a user to **upload a file**.| A collection of attachment objects.          |
| Date or time     | [DateTimeInput](#datetimeinput)   | Used to ask your users for a **date and or time**.   | A collection of date-time objects.           |
| Oauth login      | [OAuthInput](#oauthinput)              | Used to enable your users to **sign into a secure site**.| A token response.                        |

<!--TODO P1: Add a general section on locales to show all Supported locales that can be used in the  `DefaultLocale` property.
`DefaultLocale`: Sets the default locale for input processing that will be used unless one is passed by the caller. Supported locales are Spanish, Dutch, English, French, German, Japanese, Portuguese, Chinese.
--->

### InputDialog

The input classes provided by the bot framework SDK all derive from the base _input dialog_, which derives from the _dialog_ class. All input dialogs have these common properties:

#### AllowInterruptions

A Boolean expression. `true` to let the parent dialog interrupt the input dialog; otherwise, `false`.

<!--TODO P0.5: Link to information on interruptions-->

> [!NOTE]
> The inputs parent dialog can also interrupt. This means that when `AllowInterruptions` is `true`, the recognizer in the inputs parent adaptive dialog will run and its triggers are evaluated.

#### AlwaysPrompt

A Boolean expression. If `true`, always prompt for input; if `false`, only prompt when the bound [property](#property) is null or empty.

<!--TODO P1:
This is true enough. I lost some sanity following the code down a rabbit hole. The full story is that the input dialog will run the property value (or the evaluation of the value value if _property is null) through all its recognition and validation logic. Thus, if it is null or empty or isn't recognized or doesn't validate, then the input dialog will prompt the user.
This clarification is probably a low priority, but should happen at some point. Since this logic involves about 4 properties and is a little complex, it would be worth describing outside the list of properties.
--->

#### DefaultValue

An adaptive expression representing the default result for the input dialog. If the user input fails for [max turn count](#maxturncount) turns, the input dialog ends and sets the default value to this property.



```csharp
DefaultValue = "9"
```

#### DefaultValueResponse

The response to send when the users input fails its [Validations](#validations) for [MaxTurnCount](#maxturncount) turns and a [DefaultValue](#defaultvalue) is specified.

```csharp
DefaultValueResponse = new ActivityTemplate("Sorry, we have reach the maximum number of attempts of '${%MaxTurnCount}' to get your input, so for now, we will go with a default value of: '${%DefaultValue}'")
```

#### InvalidPrompt

The activity template with which to reprompt for input if the user input is recognized but fails validation. (If the input fails for [max turn count](#maxturncount) turns, then the [default value](#defaultvalue) is used and the [default value response](#defaultvalueresponse) is sent.)

>[!NOTE]
> The `InvalidPrompt` property works only in conjunction with the [Validations](#validations) property.

```csharp
InvalidPrompt = new ActivityTemplate("Sorry, {this.value} does not work. Please enter a number between one and ten (1-10).")
```

#### MaxTurnCount

An integer expression. The maximum number of times to ask for input. If this limit is exceeded, the [default value](#defaultvalue) is used and the [default value response](#defaultvalueresponse) is sent.

```csharp
MaxTurnCount = 2
```

#### Prompt

The activity template with which to initially prompt for user input.

```csharp
Prompt = new ActivityTemplate("Hi, What is your name?")
```

#### Property

The memory path, or an expression that evaluates to the memory path, of the property to bind the input dialog to. The memory path will be used to get the initial value for the input dialog. It will also be used to store the result of this dialog. Both the `Prompt` and the `Value` property go through recognition and validation steps, so an invalid initial value will result in a prompt.

Use this to define what property the input dialog is bound to. For example:

```csharp
Property = "user.name"
```

#### UnrecognizedPrompt

The activity template with which to reprompt for input if the user input is not recognized. (If the input fails for [max turn count](#maxturncount) turns, then the [default value](#defaultvalue) is used and the [default value response](#defaultvalueresponse) is sent.)

```csharp
UnrecognizedPrompt = new ActivityTemplate("Sorry, '{turn.activity.text}' did not include a valid number")
```

#### Validations

A list of Boolean expressions. Recognized input is invalid if any of these expressions evaluate to `false`. You can use `this.value` to examine the user input in the validation expressions. Validations are expressed using [adaptive expressions][10]

#### Value

A string expression. The memory path of the property to get input from each turn. This property will be used as the initial value for the input dialog if the dialog's [property](#property) evaluates to null or empty. If both the dialog's _property_ and _value_ properties evaluate to null or empty, then the dialog will prompt for input.

Things to keep in mind regarding the `Value` property:

* The `Value` property is an [adaptive expression][10].
* If the expression returns null, the input dialog may attempt to pull data from the input directly.
* If the expression is a value then it will be used as the input.
* The `Value` property allows you to define a how data such as [Recognizer][13] results are bound to the input dialog.

 Examples:

* To bind the input to any age entity recognized in the input: "=@age"
* To use @age or @number as the input: "=coalesce(@age, @number)"

> [!TIP]
> You can see an example that uses these `InputDialog` properties in the code sample in the [NumberInput](#numberinput) section below.

### TextInput

Use _text input_ when you want to verbatim accept user input as a value for a specific piece of information your bot is trying to collect. Examples include _user's name_ and the _subject of an email_.

The `TextInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines one additional property:

* `OutputFormat`: Using [adaptive expressions][10] you can modify the string, for example, in the code example below the `OutputFormat` expression will capitalize the first letter of each word of the users name.

#### TextInput example

``` C#
// Create root dialog as an adaptive dialog.
var getUserNameDialog = new AdaptiveDialog("GetUserNameDialog");

// Add an intent trigger.
getUserNameDialog.Triggers.Add(new OnIntent()
{
    Intent = "GetName",
    Actions = new List<Dialog>()
    {
        // Add TextInput step. This step will capture user's input and use it to populate the 'user.name' property.
        new TextInput()
        {
            Property = "user.fullName",
            Prompt = new ActivityTemplate("Please enter your full name.")
            OutputFormat = "join(foreach(split(this.value, ' '), item, concat(toUpper(substring(item, 0, 1)), substring(item, 1))), ' ')"
        }
    }
});
```
<!--TODO P2: List Samples that use this action, for example: CoreBot->RootDialog.cs--->

### NumberInput

Asks the user for a number.

The `NumberInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these two additional properties:

<!--https://blog.botframework.com/2018/02/01/contributing-luis-microsoft-recognizers-text-part-2/-->

1. `DefaultLocale`: Sets the default locale for input processing that will be used unless one is passed by the caller. Supported locales are Spanish, Dutch, English, French, German, Japanese, Portuguese, Chinese.
2. `OutputFormat`: Using [adaptive expressions][10] you can take actions to manipulate the number in some way. For example, you could write an expression to convert a number entered as a temperature given in Fahrenheit to its equivalent Celsius value, perform a mathematical calculation such as adding tax and shipping costs to the value entered, or simply perform a type conversion to specify that the value is either a float or integer as demonstrated in the sample code below.

#### NumberInput example

``` C#
var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
{
    Generator = new TemplateEngineLanguageGenerator(),
    Triggers = new List<OnCondition> ()
    {
        new OnUnknownIntent()
        {
            Actions = new List<Dialog>()
            {
                new NumberInput() {
                    Property = "user.favoriteNumber",
                    Prompt = new ActivityTemplate("Give me your favorite number (1-10)"),
                    // You can refer to incoming user message via turn.activity.text
                    UnrecognizedPrompt = new ActivityTemplate("Sorry, '{turn.activity.text}' did not include a valid number"),
                    // You can provide a list of validation expressions. Use turn.value to refer to any value extracted by the recognizer.
                    Validations = new List<String> () {
                        "int(this.value) >= 1",
                        "int(this.value) <= 10"
                    },
                    InvalidPrompt = new ActivityTemplate("Sorry, {this.value} does not work. Can you give me a different number that is between 1-10?"),
                    MaxTurnCount = 2,
                    DefaultValue = "9",
                    DefaultValueResponse = new ActivityTemplate("Sorry, we have tried for '${%MaxTurnCount}' number of times and I'm still not getting it. For now, I'm setting '${%property}' to '${%DefaultValue}'"),
                    AllowInterruptions = "false",
                    AlwaysPrompt = true,
                    OutputFormat = "float(this.value)"
                },
                new SendActivity("Your favorite number is {user.favoriteNumber}")
            }
        }
    }
};
```

### ConfirmInput

**Confirmation inputs** are useful to use after you have already asked the user a question and want to confirm their answer. Unlike the **Multiple choice** action that enables your bot to present the user with a list to choose from, confirmation prompts ask the user to make a binary (yes/no) decision.

The `ConfirmInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these additional properties:

1. `ChoiceOptions`: Used to format the presentation of the confirmation choices that are presented to the user, this is an [adaptive expression][10] that evaluates to a `ChoiceSet` object. This `ChoiceSet` object will only be used as a back up if the initial attempt at recognition of the `ConfirmInput` fails. When the `ConfirmInput` action executes, it first tries to evaluate the input as a Boolean value. If that fails, it makes a second attempt, this time using a choice recognizer evaluating against the ChoiceSet.
2. `ConfirmChoices`: The choices or an [adaptive expression][10] that evaluates to the choices that will be presented to the user.
3. `DefaultLocale`: Sets the default locale for input processing that will be used unless one is passed by the caller. Supported locales are Spanish, Dutch, English, French, German, Japanese, Portuguese, Chinese
4. `OutputFormat`: The default output format for the `ConfirmInput` action is a boolean. You can override that using the `OutputFormat` property, an [adaptive expressions][10] which you can use to modify the return results if needed. For example you can use this to cause the  `ConfirmInput` action to return a number: `OutputFormat = "if(this.value == true, 1, 0)"`.
If this property is set then the output of the expression is the value returned by the dialog. <!-- huh? Using [adaptive expressions][10] you can evaluate the yes/no response for what purpose?-->
5. `Style`: This defines the type of list to present to the user when confirming their input. This uses the `ListStyle` enum which consists of:
    1. `None`: Don't include any choices for prompt.
    2. `Auto`: Automatically select the appropriate style for the current channel.
    3. `Inline`: Add choices to prompt as an inline list.
    4. `List`: Add choices to prompt as a numbered list.
    5. `SuggestedAction`: Add choices to prompt as suggested actions.
    6. `HeroCard`: Add choices to prompt as a HeroCard with buttons.

#### ConfirmInput example

``` C#
// Create adaptive dialog.
var ConfirmationDialog = new AdaptiveDialog("ConfirmationDialog") {
    Triggers = new List<OnCondition>()
    {
        new OnUnknownIntent()
        {
            Actions = new List<Dialog>()
            {
                // Add confirmation input.
                new ConfirmInput()
                {
                    Property = "turn.contoso.travelBot.confirmOutcome",
                    // Since this prompt is built as a generic confirmation wrapper, the actual prompt
                    // text is read from a specific memory location. The caller of this dialog needs to
                    // set the prompt string to that location before calling the "ConfirmationDialog".
                    // All prompts support rich language generation based resolution for output generation.
                    // See https://aka.ms/language-generation-file-format to learn more about the LG
                    // template format used in the ActivityTemplate object.
                    Prompt = new ActivityTemplate("${turn.contoso.travelBot.confirmPromptMessage}")
                }
            }
        }
    }
};
```

### ChoiceInput

**Choice inputs** are a set of options presented to the user as a **Multiple choice** selection that enables you to present your users with a list of options to choose from.

The `ChoiceInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these additional properties:

1. `ChoiceOptions`: This property is used to format the presentation of the confirmation choices that are presented to the user.
2. `Choices`: An adaptive expression that evaluates to a ChoiceSet that contains the [ordered] list of choices for the user to choose from.
3. `DefaultLocale`: Sets the default locale for input processing that will be used unless one is passed by the caller. Supported locales are Spanish, Dutch, English, French, German, Japanese, Portuguese, Chinese
4. `OutputFormat`: an adaptive expression that evaluates to one of the `ChoiceOutputFormat` enumeration values:

    ~~~csharp
    switch (this.OutputFormat.GetValue(dc.State))
    {
        case ChoiceOutputFormat.Value:
        default:
            dc.State.SetValue(VALUE_PROPERTY, foundChoice.Value);
            break;
        case ChoiceOutputFormat.Index:
            dc.State.SetValue(VALUE_PROPERTY, foundChoice.Index);
            break;
    }
    ~~~

5. `Style`: This defines the type of list to present to the user when confirming their input. This uses the `ListStyle` enum which consists of:
    1. `None`: Don't include any choices for prompt.
    2. `Auto`: Automatically select the appropriate style for the current channel.
    3. `Inline`: Add choices to prompt as an inline list.
    4. `List`: Add choices to prompt as a numbered list.
    5. `SuggestedAction`: Add choices to prompt as suggested actions.
    6. `HeroCard`: Add choices to prompt as a HeroCard with buttons.
6. `RecognizerOptions`: FindChoicesOptions or expression which evaluates to FindChoicesOptions. The FindChoicesOptions has these properties:
    1. `NoValue`: A Boolean value. `true` to search over each choice's _value_ property; otherwise, `false`. The default is `false`.
    2. `NoAction`: A Boolean value. `true` to search over the title of each choice's _action_ property; otherwise, `false`. The default is `false`.
    3. `RecognizeNumbers`: A Boolean value. `true` to allow the input fall-back on using a number recognizer to match against the input choices; otherwise, `false`. The default is `true`.
    4. `RecognizeOrdinals`: A Boolean value. `true` to allow the input to fall-back on using an ordinal number recognizer to match against the input choices; otherwise, `false`. The default is `true`.

<!--TODO Nice to have: Need more complete description of RecognizerOptions --->

#### ChoiceInput example

``` C#
// Create an adaptive dialog.
var getUserFavoriteColor = new AdaptiveDialog("GetUserColorDialog");
getUserFavoriteColor.Triggers.Add(new OnIntent()
{
    Intent = "GetColor",
    Actions = new List<Dialog>()
    {
        // Add choice input.
        new ChoiceInput()
        {
            // Output from the user is automatically set to this property
            Property = "user.favColor",

            // List of possible styles supported by choice prompt.
            Style = Bot.Builder.Dialogs.Choices.ListStyle.Auto,
            Prompt = new ActivityTemplate("What is your favorite color?"),
            Choices = new ChoiceSet(new List<Choice>()
            {
                new Choice("Red"),
                new Choice("Blue"),
                new Choice("Green")
            })
        }
    }
});
```

### DateTimeInput

<!-- TODO Post R9, maybe mention that this often generates multiple results, depending on how ambiguous the input was. -->

Asks for a date/time.

The `DateTimeInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these additional properties:

1. `DefaultLocale`: Sets the default locale for input processing that will be used unless one is passed by the caller. Supported locales are Spanish, Dutch, English, French, German, Japanese, Portuguese, Chinese.
2. `OutputFormat`: The default output for `DateTimeInput` is an array of `DateTimeResolutions`, this property allows you to define an adaptive expression. Whatever value it returns become the final value for the dialog's `property` property, whether or not it evaluates to a date-time or not.

#### DateTimeInput example

``` C#
var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
{
    Generator = new TemplateEngineLanguageGenerator(_templateEngine),
    Triggers = new List<OnCondition>()
    {
        new OnUnknownIntent()
        {
            Actions = new List<Dialog>()
            {
                new DateTimeInput()
                {
                    Property = "$userDate",
                    Prompt = new ActivityTemplate("Give me a date"),
                },
                new SendActivity("You gave me ${$userDate}")
            }
        }
    }
};
```

### AttachmentInput

Use to request an attachment from user as input.

The `AttachmentInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines this additional property:

- `OutputFormat`: The AttachmentOutputFormat or an expression which evaluates to an AttachmentOutputFormat. Valid `AttachmentOutputFormat` values are:
    1. `All`: return all attachments as a List.
    2. `First`: return only the first attachment.

#### AttachmentInput example

``` C#
var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
{
    Generator = new TemplateEngineLanguageGenerator(_templateEngine),
    Triggers = new List<OnCondition>()
    {
        new OnUnknownIntent()
        {
            Actions = new List<Dialog>()
            {
                new AttachmentInput()
                {
                    Property = "$userAttachmentCarImage",
                    Prompt = new ActivityTemplate("Please give me an image of your car. Drag drop the image to the chat canvas."),
                    OutputFormat = AttachmentOutputFormat.All
                },
                new SendActivity("You gave me ${$userAttachmentCarImage}")
            }
        }
    }
};
```

### OAuthInput

Use to ask user to sign in.

The `OAuthInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these additional properties:

1. `ConnectionName`: Name of the OAuth connection configured in Azure Bot Service settings page for the bot.
2. `Text`: Additional text to display in the sign in card.
3. `Title`: Title text to display in the sign in card.
4. `Timeout`: This is the number of milliseconds `OAuthInput` waits for the user authentication to complete.  The default is 900,000 milliseconds, which is 15 minutes.

The `OAuthInput` action also defines two new methods:

1. `GetUserTokenAsync`: This method attempts to retrieve the user's token.
2. `SignOutUserAsync`: This method signs out the user.


#### OAuth example

```C#
var rootDialog = new AdaptiveDialog(nameof(AdaptiveDialog))
{
    Generator = new TemplateEngineLanguageGenerator(_templateEngine),
    Triggers = new List<OnCondition>()
    {
        new OnUnknownIntent()
        {
            Actions = new List<Dialog>()
            {
                new OAuthInput()
                {
                    // Name of the connection configured on Azure Bot Service for the OAuth connection.
                    ConnectionName = "GitHub",

                    // Title of the sign in card.
                    Title = "Sign in",

                    // Text displayed in sign in card.
                    Text = "Please sign in to your GitHub account.",

                    // Property path to store the authorization token.
                    TokenProperty = "user.authToken"
                },
                new SendActivity("You are signed in with token = ${user.authToken}")
            }
        }
    }
};
```

#### Additional information related to OAuth

The following links provide generalized information on the topic of authentication in the Microsoft bot framework. This information is not tailored or specific to adaptive dialogs.

* [Bot authentication][5]
* [Add authentication to a bot][6]

## Additional information

* To learn more about expressions see the article [Adaptive expressions][10].

[1]:bot-builder-adaptive-dialog-introduction.md
[2]:bot-builder-concept-adaptive-dialog-triggers.md
[3]:bot-builder-concept-adaptive-dialog-actions.md
[4]:https://aka.ms/bot-builder-concept-dialog#prompts
[5]:https://aka.ms/azure-bot-authentication
[6]:https://aka.ms/azure-bot-add-authentication

[8]:bot-builder-concept-adaptive-dialog-triggers.md#custom-events
[9]:bot-builder-concept-adaptive-dialog-generators.md
[10]:bot-builder-concept-adaptive-expressions.md
[11]:bot-builder-concept-adaptive-dialog-memory-states.md
[13]:bot-builder-concept-adaptive-dialog-recognizers.md
[14]:bot-builder-concept-language-generation.md

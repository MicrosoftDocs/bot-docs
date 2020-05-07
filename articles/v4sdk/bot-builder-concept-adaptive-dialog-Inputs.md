---
title: Asking for user input in adaptive dialogs
description: Collecting user input using adaptive dialogs
keywords: bot, user, Events, triggers, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 04/27/2020
---
<!--P2: Once the samples are done, link to them in each section on the individual input actions to point to them as examples of how they are used-->
# Asking for user input in adaptive dialogs

The Bot Framework SDK provides API that makes it easier to collect and validate a variety of data types, and handle instances when users input invalid or unrecognized data.

> [!IMPORTANT]
> Actions are adaptive dialogs, and as such have all of the power and flexibility you need to create a fully functional and robust bot. While the actions included in the bot framework SDK are extensive, you can also create your own custom actions to perform virtually any specialized task or process you need.

## Prerequisites

* A general understanding of adaptive dialogs in the Bot Framework V4 SDK is helpful. For more information, see an [introduction to adaptive dialogs][1].
* A general understanding of [events and triggers in adaptive dialogs][2].
* A general understanding of [actions in adaptive dialogs][3].
* A general understanding of [memory scopes and managing state in adaptive dialogs][11].

## Inputs

Similar to [prompts][4], you can use _inputs_ in adaptive dialogs to ask for and collect a piece of input from a user, validate it, and accept it into memory. An input:

* Binds the prompt result to a property in a [state management][6] scope.
* Prompts the user only if the result property doesn't already have a value.
* Grounds input to the specified property if the input from user matches the type of entity expected.
* Accepts validation constraints such as min, max, and so on.
* Can use as input locally relevant intents within a dialog as well as use interruption as a technique to bubble up user response to an appropriate parent dialog that can handle it. 

For more information, see [about interruptions in adaptive dialogs](./all-about-interruptions.md).

The adaptive dialogs library defines the following input types:

| Input type       | Input class                       | Description                                          | Returns                                      |
| ---------------- | --------------------------------- | ---------------------------------------------------- | -------------------------------------------- |
| Base class       | [InputDialog](#inputdialog)       | This is the base class that all of the input classes derive from. It defines all shared properties. |
| Text | [TextInput](#textinput)           | Used to ask your users for a **word or sentence**.   | A string.                                    |
| Number | [NumberInput](#numberinput)       | Used to ask your users for a **number**.             | A numeric value.                             |
| Confirmation     | [ConfirmInput](#confirminput)     | Used to request a **confirmation** from the user.    | A Boolean value.                             |
| Multiple choice  | [ChoiceInput](#choiceinput)       | Used to asks for a choice from a **set of options**. | The value or index of the selection.         |
| File or attachment |[AttachmentInput](#attachmentinput)| Used to request/enable a user to **upload a file**.  | A collection of attachment objects.        |
| Date or time     | [DateTimeInput](#datetimeinput)   | Used to ask your users for a **date and or time**.   | A collection of date-time objects.           |
| Oauth login      | [OAuthInput](#oauth)              | Used to enable your users to **sign into a secure site**.| A Boolean value.                         |

### InputDialog

The seven input classes provided by the bot framework SDK all derive from `InputDialog`, which derives from `Dialog`. The `InputDialog` class defines all of the properties that are common across all input dialogs:

#### AllowInterruptions

**Allow interruptions** is a boolean value that when _true_ tells the bot that it is ok for the parent dialog to be able to interrupt the child dialog.

#### AlwaysPrompt

**Always prompt** is a boolean value that when _true_ tells the bot to prompt for user input even if the specified [property](#property) already has a value. If _false_ the user will only be prompted for input if the property is empty.

#### DefaultValue

**Default value** is the value returned when no value is supplied by the user.

```csharp
DefaultValue = "9"
```

#### DefaultValueResponse

The **Default Value Response** is the value that is returned after the [max turn count](#maxturncount) has been hit.

```csharp
DefaultValueResponse = new ActivityTemplate("Sorry, we have reach the maximum number of attempts of '${%MaxTurnCount}' to get your input, so for now, we will go with a default value of: '${%DefaultValue}'")
```

#### InvalidPrompt

**Invalid Prompt** is the message that is sent to the user if the response they entered is invalid. An example would be entering text when a number is required.

```csharp
InvalidPrompt = new ActivityTemplate("Sorry, {this.value} does not work. Please enter a number between one and ten (1-10).")
```

#### MaxTurnCount

**Max turn count** denotes the maximum number of re-prompt attempts this specific input will execute before the default value is selected.

```csharp
MaxTurnCount = 2
```

#### Prompt

The **Prompt** property contains the initial message sent to the user to request input.

```csharp
Prompt = new ActivityTemplate("Hi, What is your name?")
```

#### Property

Use this to define what property the input dialog is bound to. For example:

```csharp
Property = "user.name"
```

#### UnrecognizedPrompt

The activity template with which to reprompt for input if the user input is not recognized. (If the input fails for [max turn count](#maxturncount) turns, then the [default value](#defaultvalue) is used and the [default value response](#defaultvalueresponse) is sent.)```

```csharp
UnrecognizedPrompt = new ActivityTemplate("Sorry, '{turn.activity.text}' did not include a valid number")
```

#### Validations

A list of Boolean expressions. Recognized input is invalid if any of these expressions evaluate to `false`. You can use `turn.value` to examine the user input in the validation expressions

#### Value

The `Value` property enables you to get or set an expression that will be evaluated on every turn to define the mapping of the user input to the dialog.

Things to keep in mind regarding the `Value` property:

* If the expression returns null, the input dialog may attempt to pull data from the input directly.
* If the expression is a value then it will be used as the input.
* The `Value` property allows you to define a how data such as Recognizer results are bound to the input dialog.

 Examples:

* To bind the input to any age entity recognized in the input: "=@age"
* To use @age or @number as the input: "=coalesce(@age, @number)"

> [!TIP]
> You can see an example that uses these `InputDialog` properties in the code sample in the [NumberInput](#numberinput) section below.

### TextInput

Use _text input_ when you want to verbatim accept user input as a value for a specific piece of information your bot is trying to collect. Examples include _user's name_ and the _subject of an email_.

The `TextInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines one additional property:

* `OutputFormat`: Using [adaptive expressions][12] you can modify the string, for example, in the code example below the `OutputFormat` expression will capitalize the first letter of each word of the users name.

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

### NumberInput

Asks the user for a number.

The `NumberInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these two additional properties:

<!--https://blog.botframework.com/2018/02/01/contributing-luis-microsoft-recognizers-text-part-2/-->

1. `DefaultLocale`: the default locale (or an expression for the default locale) for input processing. Supported locales are Spanish, Dutch, English, French, German, Japanese, Portuguese, Chinese.
2. `OutputFormat`: Using [adaptive expressions][12] you can take actions to manipulate the number in some way. For example, you could write an expression to convert a number entered as a temperature given in fahrenheit to its equivalent celsius value, perform a mathematical calculation such as adding tax and shipping costs to the value entered, or simply perform a type conversion to specify that the value is either a float or integer as demonstrated in the sample code below.

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

The `ConfirmInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these five additional properties:

1. `ChoiceOptions`: This property is used to format the presentation of the confirmation choices that are presented to the user.
2. `ConfirmChoices`: This is an expression that you use to evaluate the choices entered by the user.
3. `DefaultLocale`: Sets the default locale for input processing that will be used unless one is passed by the caller. Supported locales are Spanish, Dutch, English, French, German, Japanese, Portuguese, Chinese
4. `OutputFormat`: The default output format for `ConfirmInput` is a boolean. If this property is set then the output of the expression is the value returned by the dialog. <!-- huh? -->
5. `Style`: This defines the type of list to present to the user when confirming their input. This uses the `ListStyle` enum which consists of:
    1. `None`: Don't include any choices for prompt.
    2. `Auto`: Automatically select the appropriate style for the current channel.
    3. `Inline`: Add choices to prompt as an inline list.
    4. `List`: Add choices to prompt as a numbered list.
    5. `SuggestedAction`: Add choices to prompt as suggested actions.
    6. `HeroCard`: Add choices to prompt as a HeroCard with buttons.

> [!IMPORTANT]
>
> * Please verify the descriptions in the list of properties above.
> * Need to update the source code comment "// See ../../language-generation/README.md to learn more." 

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
                    // Since this prompt is built as a generic confirmation wrapper, the actual prompt text is
                    // read from a specific memory location. The caller of this dialog needs to set the prompt
                    // string to that location before calling the "ConfirmationDialog".
                    // All prompts support rich language generation based resolution for output generation.
                    // See ../../language-generation/README.md to learn more.
                    Prompt = new ActivityTemplate("${turn.contoso.travelBot.confirmPromptMessage}")
                }
            }
        }
    }
};
```

### ChoiceInput

**Choice inputs** are a set of options presented to the user as a **Multiple choice** selection that enables you to present your users with a list of options to choose from.

The `ChoiceInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these six additional properties:

1. `ChoiceOptions`: This property is used to format the presentation of the confirmation choices that are presented to the user.
2. `Choices`: An adaptive expression that evaluates to a ChoiceSet that contains the [ordered] list of choices for the user to choose from.
3. `DefaultLocale`: Sets the default locale for input processing that will be used unless one is passed by the caller. Supported locales are Spanish, Dutch, English, French, German, Japanese, Portuguese, Chinese
4. `OutputFormat`: an adaptive expression that evaluates to one of the `ChoiceOutputFormat` enumeration values:
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

> [!IMPORTANT]
>
> * Please verify the descriptions in the list of properties above.
> * Need a link to additional information on the `RecognizerOptions`.

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

Asks for a date/time.

The `DateTimeInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these three additional properties:

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

1. `OutputFormat`: `OutputFormat = AttachmentOutputFormat.All` The AttachmentOutputFormat or an expression which evaluates to an AttachmentOutputFormat. Valid `AttachmentOutputFormat` values are:
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

The `OAuthInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these four additional properties:

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

* To learn about action specific to gathering user input see the article [Asking for user input using adaptive dialogs][7].
* To learn more about expressions see the article [the common expressions language][10].

[1]:bot-builder-adaptive-dialog-introduction.md
[2]:bot-builder-adaptive-dialog-triggers.md
[3]:bot-builder-adaptive-dialog-actions.md
[4]:https://docs.microsoft.com/azure/bot-service/bot-builder-concept-dialog?view=azure-bot-service-4.0#prompts
[5]:https://docs.microsoft.com/azure/bot-service/bot-builder-concept-authentication?view=azure-bot-service-4.0
[6]:https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-authentication?view=azure-bot-service-4.0&tabs=aadv1%2Ccsharp
[7]:bot-builder-adaptive-dialog-input.md
[8]:bot-builder-adaptive-dialog-triggers.md#custom-events
[9]:bot-builder-adaptive-dialog-generation.md
[10]:PlaceholderFor-common-expressions-language
[11]:bot-builder-adaptive-dialog-scopes.md
[12]:NEED-LINK-FOR-ADAPTIVE-EXPRESSIONS.MD

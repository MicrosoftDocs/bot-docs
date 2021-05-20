---
title: Inputs in adaptive dialogs - reference guide
description: Describing the adaptive dialogs prebuilt inputs
keywords: bot, inputs, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 04/30/2021
monikerRange: 'azure-bot-service-4.0'
---

# Inputs in adaptive dialogs - reference guide

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

The Bot Framework SDK defines a variety of input dialogs for collecting and validating user input.

| Input type       | Input class                       | Description                                              | Returns                                  |
| ---------------- | --------------------------------- | -------------------------------------------------------- | ---------------------------------------- |
| Base class       | [InputDialog](#inputdialog)       | This is the base class that all of the input classes derive from. It defines all shared properties. | An object. |
| Text             | [TextInput](#textinput)           | Used to ask your users for a **word** or **sentence**.   | A string.                                |
| Number           | [NumberInput](#numberinput)       | Used to ask your users for a **number**.                 | A numeric value.                         |
| Confirmation     | [ConfirmInput](#confirminput)     | Used to request a **confirmation** from the user.        | A Boolean value.                         |
| Multiple choice  | [ChoiceInput](#choiceinput)       | Used to ask for a choice from a **set of options**.      | The value or index of the selection.     |
|File or attachment|[AttachmentInput](#attachmentinput)| Used to request/enable a user to **upload a file**.    | A collection of attachment objects.      |
| Date or time     | [DateTimeInput](#datetimeinput)   | Used to ask your users for a **date and or time**.       | A collection of date-time objects.       |
| Oauth login      | [OAuthInput](#oauthinput)         | Used to enable your users to **sign into a secure site**.| A token response.                        |

## InputDialog

The input classes provided by the Bot Framework SDK all derive from the base _input dialog_, which derives from the _dialog_ class. All input dialogs have these common properties:

### AllowInterruptions

A Boolean expression. `true` to let the parent dialog interrupt the input dialog; otherwise, `false`.

> [!NOTE]
> The inputs parent dialog can also interrupt. This means that when `AllowInterruptions` is `true`, the recognizer in the inputs parent adaptive dialog will run and its triggers are evaluated.

### AlwaysPrompt

A Boolean expression. If `true`, always prompt for input; if `false`, only prompt when the bound [property](#property) is null or empty.

### DefaultValue

An adaptive expression representing the default result for the input dialog. If the user input fails for [max turn count](#maxturncount) turns, the input dialog ends and sets the default value to this property.

### DefaultValueResponse

The response to send when the users input fails its [Validations](#validations) for [MaxTurnCount](#maxturncount) turns and a [DefaultValue](#defaultvalue) is specified.

### InvalidPrompt

The activity template with which to reprompt for input if the user input is recognized but fails validation. (If the input fails for [max turn count](#maxturncount) turns, then the [default value](#defaultvalue) is used and the [default value response](#defaultvalueresponse) is sent.)

>[!NOTE]
> The `InvalidPrompt` property works only in conjunction with the [Validations](#validations) property.

### MaxTurnCount

An integer expression. The maximum number of times to ask for input. If this limit is exceeded, the [default value](#defaultvalue) is used and the [default value response](#defaultvalueresponse) is sent.

### Prompt

The activity template with which to initially prompt for user input.

### Property

The memory path, or an expression that evaluates to the memory path, of the property to bind the input dialog to. The memory path will be used to get the initial value for the input dialog. It will also be used to store the result of this dialog. Both the `Prompt` and the `Value` property go through recognition and validation steps, so an invalid initial value will result in a prompt.

Use this to define what property the input dialog is bound to. For example:

### UnrecognizedPrompt

The activity template with which to reprompt for input if the user input is not recognized. (If the input fails for [max turn count](#maxturncount) turns, then the [default value](#defaultvalue) is used and the [default value response](#defaultvalueresponse) is sent.)

### Validations

A list of Boolean expressions. Recognized input is invalid if any of these expressions evaluate to `false`. You can use `this.value` to examine the user input in the validation expressions. Validations are expressed using [adaptive expressions][adaptive-expressions]

### Value

A string expression. The memory path of the property to get input from each turn. This property will be used as the initial value for the input dialog if the dialog's [property](#property) evaluates to null or empty. If both the dialog's _property_ and _value_ properties evaluate to null or empty, then the dialog will prompt for input.

Things to keep in mind regarding the `Value` property:

* The `Value` property is an [adaptive expression][adaptive-expressions].
* If the expression returns null, the input dialog may attempt to pull data from the input directly.
* If the expression is a value then it will be used as the input.
* The `Value` property allows you to define a how data such as [Recognizer][recognizers] results are bound to the input dialog.

 Examples:

* To bind the input to any age entity recognized in the input: "=@age"
* To use @age or @number as the input: "=coalesce(@age, @number)"

> [!TIP]
> You can see an example that uses these `InputDialog` properties in the code sample in the [NumberInput](#numberinput) section below.

## TextInput

Use _text input_ when you want to verbatim accept user input as a value for a specific piece of information your bot is trying to collect. Examples include _user's name_ and the _subject of an email_.

The `TextInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines one additional property:

* `OutputFormat`: Using [adaptive expressions][adaptive-expressions] you can modify the string, for example, in the code example below the `OutputFormat` expression will capitalize the first letter of each word of the users name.

## NumberInput

Asks the user for a number.

The `NumberInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these two additional properties:

<!--https://blog.botframework.com/2018/02/01/contributing-luis-microsoft-recognizers-text-part-2/-->

1. `DefaultLocale`: Sets the default locale for input processing that will be used unless one is passed by the caller. Supported locales are Spanish, Dutch, English, French, German, Japanese, Portuguese, Chinese.
2. `OutputFormat`: Using [adaptive expressions][adaptive-expressions] you can take actions to manipulate the number in some way. For example, you could write an expression to convert a number entered as a temperature given in Fahrenheit to its equivalent Celsius value, perform a mathematical calculation such as adding tax and shipping costs to the value entered, or simply perform a type conversion to specify that the value is either a float or integer as demonstrated in the sample code below.

## ConfirmInput

**Confirmation inputs** are useful to use after you have already asked the user a question and want to confirm their answer. Unlike the **Multiple choice** action that enables your bot to present the user with a list to choose from, confirmation prompts ask the user to make a binary (yes/no) decision.

The `ConfirmInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these additional properties:

1. `ChoiceOptions`: Used to format the presentation of the confirmation choices that are presented to the user, this is an [adaptive expression][adaptive-expressions] that evaluates to a `ChoiceSet` object. This `ChoiceSet` object will only be used as a back up if the initial attempt at recognition of the `ConfirmInput` fails. When the `ConfirmInput` action executes, it first tries to evaluate the input as a Boolean value. If that fails, it makes a second attempt, this time using a choice recognizer evaluating against the ChoiceSet.
2. `ConfirmChoices`: The choices or an [adaptive expression][adaptive-expressions] that evaluates to the choices that will be presented to the user.
3. `DefaultLocale`: Sets the default locale for input processing that will be used unless one is passed by the caller. Supported locales are Spanish, Dutch, English, French, German, Japanese, Portuguese, Chinese
4. `OutputFormat`: The default output format for the `ConfirmInput` action is a boolean. You can override that using the `OutputFormat` property, an [adaptive expressions][adaptive-expressions] which you can use to modify the return results if needed. For example you can use this to cause the  `ConfirmInput` action to return a number: `OutputFormat = "if(this.value == true, 1, 0)"`.
If this property is set then the output of the expression is the value returned by the dialog.
5. `Style`: This defines the type of list to present to the user when confirming their input. This uses the `ListStyle` enum which consists of:
    1. `None`: Don't include any choices for prompt.
    2. `Auto`: Automatically select the appropriate style for the current channel.
    3. `Inline`: Add choices to prompt as an inline list.
    4. `List`: Add choices to prompt as a numbered list.
    5. `SuggestedAction`: Add choices to prompt as suggested actions.
    6. `HeroCard`: Add choices to prompt as a HeroCard with buttons.

## ChoiceInput

**Choice inputs** are a set of options presented to the user as a **Multiple choice** selection that enables you to present your users with a list of options to choose from.

The `ChoiceInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these additional properties:

1. `ChoiceOptions`: This property is used to format the presentation of the confirmation choices that are presented to the user.
2. `Choices`: An adaptive expression that evaluates to a ChoiceSet that contains the [ordered] list of choices for the user to choose from.
3. `DefaultLocale`: Sets the default locale for input processing that will be used unless one is passed by the caller. Supported locales are Spanish, Dutch, English, French, German, Japanese, Portuguese, Chinese
4. `OutputFormat`: an adaptive expression that evaluates to one of the `ChoiceOutputFormat` enumeration values.
5. `Style`: This defines the type of list to present to the user when confirming their input. This uses the `ListStyle` enum which consists of:
    1. `None`: Don't include any choices for prompt.
    2. `Auto`: Automatically select the appropriate style for the current channel.
    3. `Inline`: Add choices to prompt as an inline list.
    4. `List`: Add choices to prompt as a numbered list.
    5. `SuggestedAction`: Add choices to prompt as suggested actions.
    6. `HeroCard`: Add choices to prompt as a HeroCard with buttons.
6. `RecognizerOptions`: `FindChoicesOptions` or expression which evaluates to `FindChoicesOptions`. The `FindChoicesOptions` has these properties:
    1. `NoValue`: A Boolean value. `true` to search over each choice's _value_ property; otherwise, `false`. The default is `false`.
    2. `NoAction`: A Boolean value. `true` to search over the title of each choice's _action_ property; otherwise, `false`. The default is `false`.
    3. `RecognizeNumbers`: A Boolean value. `true` to allow the input fall-back on using a number recognizer to match against the input choices; otherwise, `false`. The default is `true`.
    4. `RecognizeOrdinals`: A Boolean value. `true` to allow the input to fall-back on using an ordinal number recognizer to match against the input choices; otherwise, `false`. The default is `true`.

## DateTimeInput

Asks for a date/time.

The `DateTimeInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these additional properties:

1. `DefaultLocale`: Sets the default locale for input processing that will be used unless one is passed by the caller. Supported locales are Spanish, Dutch, English, French, German, Japanese, Portuguese, Chinese.
2. `OutputFormat`: The default output for `DateTimeInput` is an array of `DateTimeResolutions`, this property allows you to define an adaptive expression. Whatever value it returns become the final value for the dialog's `property` property, whether or not it evaluates to a date-time or not.

## AttachmentInput

Use to request an attachment from user as input.

The `AttachmentInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines this additional property:

* `OutputFormat`: The `AttachmentOutputFormat` or an expression which evaluates to an `AttachmentOutputFormat`. Valid `AttachmentOutputFormat` values are:
    1. `All`: return all attachments as a List.
    2. `First`: return only the first attachment.

## OAuthInput

Use to ask user to sign in.

The `OAuthInput` action inherits all of the properties defined in [InputDialog](#inputdialog) and defines these additional properties:

1. `ConnectionName`: Name of the OAuth connection configured in Azure Bot Service settings page for the bot.
2. `Text`: Additional text to display in the sign in card.
3. `Title`: Title text to display in the sign in card.
4. `Timeout`: This is the number of milliseconds `OAuthInput` waits for the user authentication to complete.  The default is 900,000 milliseconds, which is 15 minutes.

The `OAuthInput` action also defines two new methods:

1. `GetUserTokenAsync`: This method attempts to retrieve the user's token.
2. `SignOutUserAsync`: This method signs out the user.

The `OAuthInput` action returns a `TokenResponse` object which contains values for `ChannelId`, `ConnectionName`, `Token`, `Expiration`. In the example below, the return value is placed into the `turn` memory scope: `turn.oauth`. You can access values from this as demonstrated in the `LoginSteps()` method: `new SendActivity("Here is your token '${turn.oauth.token}'.")`.

### Additional information related to OAuth

The following links provide generalized information on the topic of authentication in the Microsoft Bot Framework SDK. This information is not tailored or specific to adaptive dialogs.

* [Bot authentication][authentication]
* [Add authentication to a bot][add-authentication]

[authentication]:../v4sdk/bot-builder-concept-authentication.md
[add-authentication]:../v4sdk/bot-builder-authentication.md
[recognizers]:/composer/concept-language-understanding
[adaptive-expressions]:../v4sdk/bot-builder-concept-adaptive-expressions.md

---
title: Use bot response templates in your bot - Azure Bot Service
description: Reusable bot response templates make it easy for bot developers to send various messages and media to users. This article demonstrates how to use bot response templates to send messages and cards.
keywords: language generation, .lg templates, bot response, bot
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: how-to
ms.service: bot-service
ms.date: 09/02/2021
ms.custom: abs-meta-21q1
monikerRange: 'azure-bot-service-4.0'
---

# Use bot response templates in your bot

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Reusable bot response templates make it easy for bot developers to send various messages and media to users. This article demonstrates how to use bot response templates to send messages and cards and how to evaluate text input from users.

## Prerequisites

- A [LUIS](https://luis.ai) account.
- A copy of the language generation version of the core bot sample in [C#](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/language-generation/13.core-bot) or [JavaScript](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/language-generation/13.core-bot).
- Knowledge of [bot basics](../v4sdk/bot-builder-basics.md) and [language generation](../v4sdk/bot-builder-concept-language-generation.md).

## About the sample

This version of the core bot sample shows an example of an airport flight booking application. It uses a Language Understanding (LUIS) service to recognize the user input and return the top recognized LUIS intent.

This article uses a bottom-up approach to using bot response templates in your bots. You'll learn how to:

- Create a `Templates` object and load templates from a templates file.
- Create and use simple response templates.
- Create and use conditional response templates.
- Create and use rich card templates.
- Add LUIS to your bot.
- Test your bot locally with the Emulator.

## Load templates from .lg files

To use the templates, located in .lg files, you need to reference them in your bot logic. The instructions below show you how the bot response templates are referenced in your main bot logic by loading them into a _templates_ object. The example uses the templates from the `welcomeCard.lg` file.

### [C#](#tab/cs)

Make sure you have the **Microsoft.Bot.Builder.LanguageGeneration** package. Add the following snippet to load the package:

**Bots\DialogAndWelcomeBot.cs**

[!code-csharp[add-package](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/13.core-bot/Bots/DialogAndWelcomeBot.cs?range=12)]

After loading the package, create a private `Templates` object called `_templates`:

[!code-csharp[create-Templates](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/13.core-bot/Bots/DialogAndWelcomeBot.cs?range=19)]

The `_templates` object is used to reference templates in your .lg files.

Combine the path for cross-platform support and parse the path that contains `welcomeCard.lg` by adding the following to your code:

[!code-csharp[combine-path](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/13.core-bot/Bots/DialogAndWelcomeBot.cs?range=25-27)]

Now you can reference templates from `welcomeCard.lg` by name:

[!code-csharp[reference-template](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/13.core-bot/Bots/DialogAndWelcomeBot.cs?range=49-57&highlight=7)]

Notice how the `WelcomeCard` template is referenced in the call to the `SendActivityAsync` method.

### [JavaScript](#tab/js)

Make sure you have the **botbuilder-lg** package installed. Add the following snippet to load the package:

**bots/dialogAndWelcomeBot.js**

[!code-javascript[add-package](~/../BotBuilder-Samples/samples/javascript_nodejs/language-generation/13.core-bot/bots/dialogAndWelcomeBot.js?range=6)]

To use templates, parse the `welcomeCard.lg` file and save the templates to `lgTemplates`:

[!code-javascript[create-Templates](~/../BotBuilder-Samples/samples/javascript_nodejs/language-generation/13.core-bot/bots/dialogAndWelcomeBot.js?range=11)]

The `lgtemplates` object is used to reference templates in your .lg files.

Now you can reference templates from `welcomeCard.lg` by name:

[!code-javascript[reference-welcome-card-template](~/../BotBuilder-Samples/samples/javascript_nodejs/language-generation/13.core-bot/bots/dialogAndWelcomeBot.js?range=31-43&highlight=5)]

Notice how the `WelcomeCard` template is referenced in the creation of the `welcomeCard` constant.

---

Now that your bot can reference templates, it's time to starting creating templates in .lg files. These templates let you easily add conversational variety.

## Create simple response templates

A _simple response template_ includes one or more variations of text that are used for composition and expansion. One of the variations provided will be selected at random each time the template is rendered.

The simple response templates in `BookingDialog.lg`, like `PromptForDestinationCity`, `PromptForDepartureCity`, and `ConfirmPrefix`, add variety to flight booking prompts.

**Resources\BookingDialog.lg**

[!code-lg[confirm-message](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/13.core-bot/Resources/BookingDialog.LG?range=4-6)]

A call to this `PromptForDepartureCity` template will produce one of the two possible text prompts:

- _Where would you like to travel to?_
- _What is your destination city?_

### Reference memory from a template

Like more complex templates, simple response templates can reference properties in memory. In `BookingDialog.lg`, the `ConfirmMessage` simple response template references the `Destination`, `Origin`, and `TravelDate` properties:

**Resources\BookingDialog.lg**

[!code-lg[confirm-message](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/13.core-bot/Resources/BookingDialog.LG?range=17-19)]

If the user enters _Seattle_ for the `Origin`, _Paris_ for the `Destination`, and _05/24/2020_ for the `TravelDate`, your bot will produce one of the following results:

- *I have you traveling to: Paris from: Seattle on: 05/24/2020*
- *on 05/24/2020, travelling from Seattle to Paris*

## Create conditional response templates

A _conditional response template_ lets you author content that's selected based on a condition. All conditions are expressed using [adaptive expressions](../v4sdk/bot-builder-concept-adaptive-expressions.md).

The `PromptForMissingInformation` template in `BookingDialog.lg` is an example of an _if-else template_. The if-else template lets you build a template that picks a collection based on a cascading order of conditions. In the template, the user is prompted for pieces of information if their properties are set to `null`:

**Resources\BookingDialog.lg**

[!code-lg[conditional](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/13.core-bot/Resources/BookingDialog.LG?range=31-39)]

If a property is null, then the bot will call the template associated with that property. If all properties are non-null values, then the `ConfirmBooking` template is called.

### Use nested templates

Variations in templates can reference other templates. In the example above, if a property is `null` then the template calls the relevant template to prompt for the missing information.

For example, if `Destination` equals `null`, then the `PromptforDestinationCity` template would be called via `${PromptForDestinationCity()}` to obtain the missing flight destination information. If none of the properties are null, then the template calls the `ConfirmBooking` prompt.

## Create rich card templates

Bot response templates can use cards and media to create a richer conversational experience. In `welcomeCard.lg`, four templates are used to create the [Adaptive Card](https://adaptivecards.io/) that displays when you first start the bot.

The `AdaptiveCard` template defines an Adaptive Card JSON object:

**Resources\WelcomeCard.lg**

[!code-lg[adaptive-card](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/13.core-bot/Resources/welcomeCard.LG?range=24-58)]

This card displays an image, and uses templates for the card header and a set of suggested actions.

### [C#](#tab/cs)

 The `actions` are filled in by calling `cardActionTemplate(title, url, type)` and obtaining the`title`, `url`, and `type` from the `OnMembersAddedAsync()` method in `DialogAndWelcomeBot.cs`:

**Bots\DialogAndWelcomeBot.cs**

[!code-csharp[fill-card](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/13.core-bot/Bots/DialogAndWelcomeBot.cs?range=32-47)]

The `title` is the text in the suggested action button, and the `url` is the url opened when the button is clicked.

### [JavaScript](#tab/js)

 The `actions` are filled in by calling `cardActionTemplate(title, url, type)` and obtaining the`title`, `url`, and `type` from the `OnMembersAddedAsync()` method in `dialogAndWelcomeBot.js`:

**bots/dialogAndWelcomeBot.js**

[!code-javascript[fill-card](~/../BotBuilder-Samples/samples/javascript_nodejs/language-generation/13.core-bot/bots/dialogAndWelcomeBot.js?range=13-29)]

The `title` is the text in the suggested action button, and the `url` is the url opened when the button is clicked.

---

Finally the `WelcomeCard` calls the `AdaptiveCard` template to return the Adaptive Card JSON object.

**Resources\WelcomeCard.lg**

[!code-lg[fill-card](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/13.core-bot/Resources/welcomeCard.LG?range=11-14)]

For more information about the `ActivityAttachment()` function, see [inject functions from the LG library](functions-injected-from-language-generation.md).

## Add LUIS to your bot

After updating your bot logic and bot response templates, you're ready to add LUIS to your bot. Follow the steps in the sections below to add LUIS to your bot. The language model is contained in the `CognitiveModels\FlightBooking.json` file.

- [Create a LUIS app in the LUIS portal](../v4sdk/bot-builder-howto-v4-luis.md#create-a-luis-app-in-the-luis-portal).
- [Retrieve application information in the LUIS portal](../v4sdk/bot-builder-howto-v4-luis.md#retrieve-application-information-from-the-luisai-portal)
- [Update your bot's settings file](../v4sdk/bot-builder-howto-v4-luis.md#update-the-settings-file)

## Test the bot

Download and install the latest [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md).

1. Run the sample locally on your machine. If you need instructions, refer to the `README` file for the original **Core bot** [C#](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/13.core-bot#readme) or [JavaScript](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/13.core-bot#readme) sample.

1. In the Emulator, type a message such as "travel to Paris" or "going from Paris to Berlin". Use any utterance from the `FlightBooking.json`file that was used for training the "BookFlight" intent.

![LUIS booking input](../v4sdk/media/how-to-luis/luis-user-travel-input.png)

If the top intent returned from LUIS resolves to "BookFlight", your bot will continue to ask questions until it has enough information stored to create a travel booking. At that point, it will return this booking information back to you.

![LUIS booking result](../v4sdk/media/how-to-luis/luis-travel-result.png)

At this point, the code bot logic will reset and you can continue to create more bookings.

## Additional Information

- For information about bot response templates, see [Structured response templates](language-generation-structured-response-template.md) and the [.lg file format](../file-format/bot-builder-lg-file-format.md) reference.
- For information about adaptive expressions, see [Adaptive expressions](../v4sdk/bot-builder-concept-adaptive-expressions.md) and [Adaptive expressions prebuilt functions](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md).

<!--
## Next steps

> [!div class="nextstepaction"]->

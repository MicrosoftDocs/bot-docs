---
title: Use language generation templates in your bot - Bot Service
description: Learn how to use language generation templates in your bots.
keywords: language generation, lg templates, C#, JS, bot
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 5/16/2020
monikerRange: 'azure-bot-service-4.0'
---

# Use language generation templates in your bot

Language generation (LG) templates make it easy for bot developers to send a variety of messages and media to users. This article shows you how to leverage LG templates to send simple text messages and cards, and how to evaluate text input from users.

## Prerequisites

- [LUIS](https://luis.ai) account
- A copy of the language generation core bot sample in either [C#](https://aka.ms/lg-csharp-13-core-bot-sample) or [Javascript](https://aka.ms/lg-javascript-13-core-bot-sample)
- Knowledge of [bot basics](../v4sdk/bot-builder-basics.md) and [language generation](../v4sdk/bot-builder-concept-language-generation.md)

## About the sample

This LG core bot sample shows an example of an airport flight booking application. It uses a LUIS service to recognize the user input and return the top recognized LUIS intent.

This article uses a bottom up approach to using LG templates in your bots. You will learn how to:

- create a `Templates` object and [reference templates in your bot logic](#call-templates-in-files)
- create a [simple response template](#create-a-simple-response-template)
- create a [conditional response template](#create-a-conditional-response-template)
- create a [cards template](#create-a-cards-template)
- add [LUIS to your bot](#add-luis-to-your-bot)
- [test your bot](#test-the-bot)

## Call templates in files

To use templates in .lg files you need to reference them in your bot logic. The instructions below show you how the LG templates you will create are referenced in your main bot logic by loading them into a `Templates` object. The example uses the templates from **welcomeCard.lg**.

### [C#](#tab/cs)

Make sure you have the **Microsoft.Bot.Builder.LanguageGeneration** package. Add the following snippet to load the package:

**Bots/DialogAndWelcomeBot.cs**

[!code-csharp[add-package](~/../BotBuilder-Samples/experimental/language-generation/csharp_dotnetcore/13.core-bot/Bots/DialogAndWelcomeBot.cs?range=12)]

After loading the package create a private `Templates` object called **_templates**:

[!code-csharp[create-Templates](~/../BotBuilder-Samples/experimental/language-generation/csharp_dotnetcore/13.core-bot/Bots/DialogAndWelcomeBot.cs?range=19)]

The **_templates** object is used to reference templates in your .lg files.

Combine the path for cross-platform support and parse the path that contains **welcomeCard.lg** by adding the following to your code:

[!code-csharp[add-package](~/../BotBuilder-Samples/experimental/language-generation/csharp_dotnetcore/13.core-bot/Bots/DialogAndWelcomeBot.cs?range=25-27)]

Now you can can reference templates from the **welcomeCard.lg** by name, seen below:

[!code-csharp[add-package](~/../BotBuilder-Samples/experimental/language-generation/csharp_dotnetcore/13.core-bot/Bots/DialogAndWelcomeBot.cs?range=49-57&highlight=12,27,55)]

Notice how the `WelcomeCard` template is referenced in the call `SendActivityAsync()`.

### [JavaScript](#tab/js)

Make sure you have the **botbuilder-lg** packaged installed. Add the following snippet to load the package:

**bots/dialogAndWelcomeBot.js**

[!code-javascript[add-package](~/../BotBuilder-Samples/experimental/language-generation/javascript_nodejs/13.core-bot/bots/dialogAndWelcomeBot.js?range=6)]

To use templates parse **welcomeCard.lg** and save the lg templates to **lgTemplates**:

[!code-javascript[create-Templates](~/../BotBuilder-Samples/experimental/language-generation/javascript_nodejs/13.core-bot/bots/dialogAndWelcomeBot.js?range=11)]

The **lgtemplates** object is used to reference templates in your .lg files.

Now you can can reference templates from the **welcomeCard.lg** by name, seen below:

[!code-javascript[reference-welcome-card-template](~/../BotBuilder-Samples/experimental/language-generation/javascript_nodejs/13.core-bot/bots/dialogAndWelcomeBot.js?range=31-43?highlight=3)]

Notice how the `WelcomeCard` template is referenced in the creation of the **welcomeCard** constant.

---

Now that your bot can reference templates, it's time to starting creating templates in LG files. By using LG you easily add conversational variety.

## Create a simple response template

A [simple response template](../file-format/bot-builder-lg-file-format.md#simple-response-template) includes one or more variations of text that are used for composition and expansion. One of the variations provided will be selected at random by the LG library.

The simple response templates in **BookingDialog.lg**, like `# PromptForDestinationCity`, `# PromptForDepartureCity`, and `# ConfirmPrefix`, add variety to flight booking prompts.

**Resources/BookingDialog.lg**

[!code-lg[confirm-message](~/../BotBuilder-Samples/experimental/language-generation/csharp_dotnetcore/13.core-bot/Resources/BookingDialog.LG?range=4-6)]

For example, a call to `# PromptForDepartureCity`, seen above, will produce one of the two possible text prompts:

- _Where would you like to travel to?_
- _What is your destination city?_

### Reference memory

Like more complex templates, simple response templates can reference memory. In **BookingDialog.LG** the `# ConfirmMessage` simple response template references the `Destination`, `Origin`, and `TravelDate` properties:

**Resources/BookingDialog.lg**

[!code-lg[confirm-message](~/../BotBuilder-Samples/experimental/language-generation/csharp_dotnetcore/13.core-bot/Resources/BookingDialog.LG?range=17-19)]

If the user enters _Seattle_ for the `Origin`, _Paris_ for the `Destination`, and _05/24/2020_ for the `TravelDate`, your bot will produce one of the following results:

- *I have you traveling to: Paris from: Seattle on: 05/24/2020*
- *on 05/24/2020, travelling from Seattle to Paris*

## Create a conditional response template

A [conditional response template](../file-format/bot-builder-lg-file-format.md#conditional-response-template) lets you author content that's selected based on a condition. All conditions are expressed using [adaptive expressions](../v4sdk/bot-builder-concept-adaptive-expressions.md).

The `# PromptForMissingInformation` template in **BookingDialog.lg** is an example of an [if-else template](../file-format/bot-builder-lg-file-format.md#conditional-response-template). The if-else template lets you build a template that picks a collection based on a cascading order of conditions. In the template, the user is prompted for pieces of information if their properties are set to `null`:

**Resources/BookingDialog.lg**

[!code-lg[conditional](~/../BotBuilder-Samples/experimental/language-generation/csharp_dotnetcore/13.core-bot/Resources/BookingDialog.LG?range=31-39)]

If a property is null then the bot will call the template associated with that property. If all properties are non-null values then the `# ConfirmBooking` template is called. 

### Reference other templates

Variations in templates can reference other templates. In the example above, if a property is `null` then the template calls the relevant template to prompt for the missing information.

For example, if `Destination` equals `null`, then the `# PromptforDestinationCity` template would be called via `${PromptForDestinationCity()}` to obtain the missing flight destination information. If none of the properties are null then the template calls the `# ConfirmBooking` prompt.

## Create a cards template

Language generation templates can use cards and media to create a richer conversational experience. In **welcomeCard.lg**, four templates are used to create the [Adaptive Card](https://aka.ms/msbot-adaptivecards) that displays when you first start the bot.

`# Adaptive Card` defines an Adaptive card JSON object:

**Resources/welcomeCard.lg**

[!code-lg[adaptive-card](~/../BotBuilder-Samples/experimental/language-generation/csharp_dotnetcore/13.core-bot/Resources/welcomeCard.LG?range=24-57)]

This card displays an image, and uses LG templates for the card header a set of suggested actions.

### [C#](#tab/cs)

 The `actions` are filled in by calling `# cardActionTemplate(title, url, type)` and obtaining the`title`, `url`, and `type` from the `OnMembersAddedAsync()` method in **DialogAndWelcomeBot.cs**:

**Bots/DialogAndWelcomeBot.cs**

[!code-csharp[fill-card](~/../BotBuilder-Samples/experimental/language-generation/csharp_dotnetcore/13.core-bot/Bots/DialogAndWelcomeBot.cs?range=30-48)]

The `title` is the text in the suggested action button, and the `url` is the url opened when the button is clicked.

### [JavaScript](#tab/js)

 The `actions` are filled in by calling `# cardActionTemplate(title, url, type)` and obtaining the`title`, `url`, and `type` from the `OnMembersAddedAsync()` method in **dialogAndWelcomeBot.js**:

**Bots/DialogAndWelcomeBot.js**

[!code-javascript[fill-card](~/../BotBuilder-Samples/experimental/language-generation/javascript_nodejs/13.core-bot/bots/dialogAndWelcomeBot.js?range=30-48)]

The `title` is the text in the suggested action button, and the `url` is the url opened when the button is clicked.

---

Finally the `# WelcomeCard` calls the `# AdaptiveCard` template to return the Adaptive card JSON object.

**Resources/welcomeCard.lg**

[!code-lg[fill-card](~/../BotBuilder-Samples/experimental/language-generation/csharp_dotnetcore/13.core-bot/Resources/welcomeCard.LG?range=11-14)]

For more information about the `ActivityAttachment()` function, read [inject functions from the LG library](functions-injected-from-language-generation.md)

---

## Add LUIS to your bot

After updating your bot logic and LG templates you are ready to add LUIS to your bot. Follow the steps in the sections below to add LUIS to your bot:

- [Create a LUIS app in the LUIS portal](https://aka.ms/bot-service-add-luis-to-bot#create-a-luis-app-in-the-luis-portal).
- [Retrieve application information in the LUIS portal](https://aka.ms/bot-service-add-luis-to-bot##retrieve-application-information-from-the-luisai-portal)
- [Update your bot's settings file](https://aka.ms/bot-service-add-luis-to-bot#update-the-settings-file)

## Test the bot

Download and install the latest [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme)

1. Run the sample locally on your machine. If you need instructions, refer to the readme file for the [C# Sample](https://aka.ms/cs-core-sample), [JS Sample](https://aka.ms/js-core-sample) or [Python Sample](https://aka.ms/python-core-sample).

1. In the emulator, type a message such as "travel to Paris" or "going from Paris to Berlin". Use any utterance found in the file FlightBooking.json for training the intent "Book flight".

![LUIS booking input](../v4sdk/media/how-to-luis/luis-user-travel-input.png)

If the top intent returned from LUIS resolves to "Book flight" your bot will ask additional questions until it has enough information stored to create a travel booking. At that point it will return this booking information back to your user.

![LUIS booking result](../v4sdk/media/how-to-luis/luis-travel-result.png)

At this point the code bot logic will reset and you can continue to create additional bookings.

## Additional Information

- [Structured response templates](language-generation-structured-response-template.md)
- [.lg file format](../file-format/bot-builder-lg-file-format.md)
- [Adaptive expressions prebuilt functions reference](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md)

<!--
## Next steps

> [!div class="nextstepaction"]->

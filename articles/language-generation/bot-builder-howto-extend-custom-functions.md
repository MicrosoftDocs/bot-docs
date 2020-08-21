---
title: Use custom functions in language generation - Bot Service
description: Learn how to add custom functions to adaptive expressions and use them in language generation.
keywords: language generation, lg templates, custom functions, adaptive expressions, C#, JS, bot
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 8/13/2020
monikerRange: 'azure-bot-service-4.0'
---

# Use custom functions in language generation

[!INCLUDE[applies-to](../includes/applies-to.md)]

Developers can use both [prebuilt functions](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md) supported by adaptive expressions and custom functions in [language generation (LG) templates](../file-format/bot-builder-lg-file-format.md). This article shows you how to define a custom function in your bot to adaptive expressions and use the function in an LG template.

## Prerequisites

- Knowledge of [bot basics](../v4sdk/bot-builder-basics.md), [adaptive expressions](../v4sdk/bot-builder-concept-adaptive-expressions.md), and [language generation](../v4sdk/bot-builder-concept-language-generation.md). Familiarity with [prebuilt functions](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md) is also helpful.
- A copy of the 20.custom-functions sample in [C#](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions) or[JavaScript](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/language-generation/20.custom-functions)

## About the sample

This LG custom functions sample is an example of how to add a simple custom function to adaptive expressions and then use that expression in an LG template definition. The bot asks the user for a number, and if the input is valid returns the square root. The function that computes the square root, `contoso.sqrt`, is defined in the bot logic, and is used in the LG template that generates bot responses.

This article uses a bottom up approach to adding and using custom functions in LG templates. You will learn how to:

- load packages and call LG templates
- define a custom function and add it to adaptive expressions
- use your custom function in an LG template to generate your bot's response

## Load packages and the LG template

### [C#](#tab/cs)

Start by making sure you have the **Microsoft.Bot.Builder.LanguageGeneration** and **AdaptiveExpressions** packages installed. Add the following snippet to load the packages:

**Bots/CustomFunctionBot.cs**

[!code-csharp[load-packages](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Bots/CustomFunctionBot.cs?range=9-10)]

After loading the packages create a private `Templates` object called `_templates`:

[!code-csharp[load-packages](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Bots/CustomFunctionBot.cs?range=19)]

The `_templates` object is used to reference templates in your .lg files.

Combine the path for cross-platform support and parse the path that contains `main.lg` by adding the following to your code:

[!code-csharp[load-packages](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Bots/CustomFunctionBot.cs?range=19)]

See <section describing lg> define your custom function in `main.lg`.

### [JavaScript](#tab/javascript)

Start by making sure you have the **botbuilder-lg** and **adaptive-expressions** packages installed. Add the following snippet to load the packages:

**bot.js**

[!code-javascript[function name](~/../BotBuilder-Samples/samples/javascript_nodejs/language-generation/20.custom-functions/bot.js?range=6-7)]

---

## Define you custom function

### [C#](#tab/cs)

Add a string constant that contains name of your function, `contoso.sqrt`.

[!code-csharp[function-name](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Bots/CustomFunctionBot.cs?range=26)]

> [! IMPORTANT]
> Prefix your functions to avoid namespace collisions.

Now you can define your bot logic for your function and add it to adaptive expressions using the `Expression.Functions.Add()` function. This function adds your custom defined function to adaptive expressions so that it can be called and used.

### [JavaScript](#tab/javascript)

Start by making sure you have the **botbuilder-lg** and **adaptive-expressions** packages installed. Add the following snippet to load the packages:

**bot.js**

[!code-javascript[function name](~/../BotBuilder-Samples/samples/javascript_nodejs/language-generation/20.custom-functions/bot.js?range=6-7)]

Then add a string constant that contains name of your function, `contoso.sqrt`.

[!code-javascript[function name](~/../BotBuilder-Samples/samples/javascript_nodejs/language-generation/20.custom-functions/bot.js?range=10)]

> [! IMPORTANT]
> Prefix your functions to avoid namespace collisions.

---
### [C#](#tab/cs)

Then add the logic for your custom function. The custom function `contoso.sqrt` in this sample returns the square root of the user input if it is valid. If it is not then `null` is returned.

[!code-csharp[function name](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Bots/CustomFunctionBot.cs?range=6-7)]

[!code-csharp[function name](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Bots/CustomFunctionBot.cs?range=26)]

### [JavaScript](#tab/javascript)


## Call custom function in code logic

- add lines that show where called (on-message-async)

## Additional Information

- [Structured response templates](language-generation-structured-response-template.md)
- [.lg file format](../file-format/bot-builder-lg-file-format.md)
- [Adaptive expressions prebuilt functions reference](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md)

<!--
## Next steps

> [!div class="nextstepaction"]->
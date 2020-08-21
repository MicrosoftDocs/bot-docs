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

- Knowledge of [bot basics](../v4sdk/bot-builder-basics.md), [adaptive expressions](../v4sdk/bot-builder-concept-adaptive-expressions.md), [language generation](../v4sdk/bot-builder-concept-language-generation.md), and the [.lg file format](../file-format/bot-builder-lg-file-format.md). Familiarity with [prebuilt functions](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md) is also helpful.
- A copy of the 20.custom-functions sample in [C#](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions) or[JavaScript](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/language-generation/20.custom-functions)

## About the sample

This LG custom functions sample is an example of how to add a simple custom function to adaptive expressions and then use that expression in an LG template definition. The bot asks the user for a number, and if the input is valid returns the square root. The function that computes the square root, `contoso.sqrt`, is defined in the bot logic, and is used in the LG template that generates bot responses.

This article uses a bottom up approach to adding and using custom functions in LG templates. You will learn about:

- the packages you need to use adaptive expressions and LG in your bot
- how to add a custom function to adaptive expressions in your bot's logic
- how to use your custom function in an LG template
- how to load and call your template in your bot logic
- test your bot

## Packages

### [C#](#tab/cs)

To use adaptive expressions and LG, install the **Microsoft.Bot.Builder.LanguageGeneration** and **AdaptiveExpressions** packages. Add the following snippet your main bot file:

**Bots/CustomFunctionBot.cs**

[!code-csharp[packages](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Bots/CustomFunctionBot.cs?range=9-10)]

### [JavaScript](#tab/javascript)

To use adaptive expressions and LG, install the **botbuilder-lg** and **adaptive-expressions** packages installed. Add the following snippet to your main bot file:

**bot.js**

[!code-javascript[packages](~/../BotBuilder-Samples/samples/javascript_nodejs/language-generation/20.custom-functions/bot.js?range=6-7)]

---

## Add a custom function to adaptive expressions

To use custom function in your bot you need to add them to adaptive expressions. This section shows how to add a custom function name `contoso.sqrt`, which returns the square root, to adaptive expressions.

### [C#](#tab/cs)

Start by adding a string constant with the name of your custom function. The name of your function should be short but recognizable. In this sample, the custom function is named `contoso.sqrt`:

**Bots/CustomFunctionBot.js**

[!code-csharp[function-name](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Bots/CustomFunctionBot.cs?range=26)]

> [! IMPORTANT]
> Prefix your functions to avoid namespace collisions.

In the function name `contoso` is the prefix and `sqrt` is short hand for square root, which the function returns.

Now you can define the logic for your function in your bot constructor and add it to adaptive expressions using the `Expression.Functions.Add()` function. Adding your custom function to adaptive expression makes it possible to use your function across LG templates just like you would be able to with any of the prebuilt functions.

The snippet below shows how to add a function, defined as `mySqrtFnName`, to adaptive expressions. This function returns the square root of a single argument, `args`, if valid, and `null` if not not:

[!code-csharp[add-to-adaptive-expressions](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Bots/CustomFunctionBot.cs?range=28-41)]

### [JavaScript](#tab/javascript)

Start by adding a string constant with the name of your custom function. The name of your function should be short but recognizable. In this sample, the custom function is named `contoso.sqrt`:

**bot.js**

[!code-javascript[function-name](~/../BotBuilder-Samples/samples/javascript_nodejs/language-generation/20.custom-functions/bot.js?range=6-7)]

> [! IMPORTANT]
> Prefix your functions to avoid namespace collisions.

In the function name `contoso` is the prefix and `sqrt` is short hand for square root, which the function returns.

Now you can define the logic for your function in your bot constructor and add it to adaptive expressions using the `Expression.functions.add()` function. Adding your custom function to adaptive expression makes it possible to use your function across LG templates just like you would be able to with any of the prebuilt functions.

The snippet below shows how to add a function, defined as `mySqrtFnName`, to adaptive expressions. This function returns the square root of a single argument, `args`, if valid, and `null` if not not:

[!code-javascript[function-name](~/../BotBuilder-Samples/samples/javascript_nodejs/language-generation/20.custom-functions/bot.js?range=19-30)]

---

## Use your custom function in an LG template

After adding your custom function to adaptive expressions you can use it in LG templates. This section describes how to set up the bot's response depending on whether the user input was valid.

### [C#](#tab/cs)

There are two template definitions in the LG. The first is the [simple response template](../file-format/bot-builder-lg-file-format.md#simple-response-template) `sqrtReadBack`:

**Resources/main.lg**

[!code-csharp[add-to-adaptive-expressions](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Resources/main.lg?range=4-5)]

This template generates a response that contains the user input, `${text}` and the result of the second conditional [if-else template](../file-format/bot-builder-lg-file-format.md#if-else-template) `sqrtTemplate`:

[!code-csharp[add-to-adaptive-expressions](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Resources/main.lg?range=9-13)]

In this template, the result of `contoso.sqrt(text)` is used to determine the response:

- If the result is not **null**, the line under the `IF` block is used in `sqrtReadBack`. Note that this line uses an inline expression, `${coalesce(contoso.sqrt(text), 'NaN)}`. The prebuilt function [coalesce](../adaptive-expressions/adaptive-expressions-prebuilt-function.md#coalesce) returns the result of `contoso.sqrt(text)` if it is not null and `Nan` if it is null.
- If the result is **null**, the line under the `ELSE` block is used in `sqrtReadBack`.

### [JavaScript](#tab/javascript)

There are two template definitions in the LG. The first is the [simple response template](../file-format/bot-builder-lg-file-format.md#simple-response-template) `sqrtReadBack`:

**resources/main.lg**

[!code-csharp[add-to-adaptive-expressions](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Resources/main.lg?range=4-5)]

This template generates a response that contains the user input, `${text}` and the result of the second conditional [if-else template](../file-format/bot-builder-lg-file-format.md#if-else-template) `sqrtTemplate`:

[!code-csharp[add-to-adaptive-expressions](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Resources/main.lg?range=9-13)]

In this template, the result of `contoso.sqrt(text)` is used to determine the response:

- If the result is not **null**, the line under the `IF` block is used in `sqrtReadBack`. Note that this line uses an inline expression, `${coalesce(contoso.sqrt(text), 'NaN)}`. The prebuilt function [coalesce](../adaptive-expressions/adaptive-expressions-prebuilt-function.md#coalesce) returns the result of `contoso.sqrt(text)` if it is not null and `Nan` if it is null.
- If the result is **null**, the line under the `ELSE` block is used in `sqrtReadBack`.

---

## Load packages and the LG template


After loading the packages create a private `Templates` object called `_templates`:

[!code-csharp[load-packages](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Bots/CustomFunctionBot.cs?range=19)]

The `_templates` object is used to reference templates in your .lg files.

Combine the path for cross-platform support and parse the path that contains `main.lg` by adding the following to your code:

[!code-csharp[load-packages](~/../BotBuilder-Samples/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions/Bots/CustomFunctionBot.cs?range=19)]


## Additional Information

- [Structured response templates](language-generation-structured-response-template.md)
- [.lg file format](../file-format/bot-builder-lg-file-format.md)
- [Adaptive expressions prebuilt functions reference](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md)

<!--
## Next steps

> [!div class="nextstepaction"]->
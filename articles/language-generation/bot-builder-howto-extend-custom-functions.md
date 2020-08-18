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

Developers can use both [prebuilt functions](../articles/adaptive-expressions/adaptive-expressions-prebuilt-functions.md) supported by adaptive expressions and custom functions in language generation (LG). This article will show you how to define a custom function in your bot and use that function in an LG template.

## Prerequisites

- Knowledge of [bot basics](../v4sdk/bot-builder-basics.md) and [language generation](../v4sdk/bot-builder-concept-language-generation.md).
- A copy of the 20.custom-functions sample in [C#](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/language-generation/20.extending-with-custom-functions) or[JavaScript](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/language-generation/20.custom-functions)

## About the sample

This LG custom functions sample bot returns the square root of the user's input. The function that computes the square root, `contoso.sqrt`, is defined in the bot logic and added to adaptive expressions. The expression is used in the LG templates the way any other adaptive expression, prebuilt or otherwise, would be. 

## Define and custom function to adaptive expressions

- add string constant w/ function name
- add expression
- regular LG calls - templates

## Call custom function in code logic

- add lines that show where called (on-message-async)


## Additional Information

- [Structured response templates](language-generation-structured-response-template.md)
- [.lg file format](../file-format/bot-builder-lg-file-format.md)
- [Adaptive expressions prebuilt functions reference](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md)

<!--
## Next steps

> [!div class="nextstepaction"]->
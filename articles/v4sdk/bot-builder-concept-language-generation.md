---
title: Language Generation
description: Describes how Language Generation works within the Bot Framework SDK.
keywords: language generation
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: bot-service
ms.date: 03/18/2022
monikerRange: 'azure-bot-service-4.0'
---

# Language Generation

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Language Generation (LG) allows developers to extract embedded strings from their code and resource files and manage them through a LG runtime and file format. With LG, developers can create a more natural conversation experience by defining multiple variations on a phrase, executing simple expressions based on context, and referring to conversational memory.

> ![NOTE]
> LG is supported in [Bot Framework Composer](/composer/introduction) and isn't intended for use in SDK-first bots.

LG can be used by developers to:

- achieve a coherent personality, tone of voice for their bot
- separate business logic from presentation
- include variations and sophisticated composition based resolution for any of their bot's replies
- add speech and display adaptations
- construct cards, suggested actions and attachments

At the core of LG lies template expansion and entity substitution. You can provide one-of variation for expansion as well as conditionally expand a template. The output from LG can be a simple text string, multi-line response, or a complex object payload that a layer above LG will use to construct an [activity][1].

The following is a simple greeting LG template. Notice that all of the greetings reference the user's name in memory with the variable `${user.name}`.

```lg
# greetingTemplate
- Hello ${user.name}, how are you?
- Good morning ${user.name}.It's nice to see you again.
- Good day ${user.name}. What can I do for you today?
```

## LG in action

You can use LG in various ways when developing bots. To start, create one or more [.lg file(s)][lg-file-format] to cover all possible scenarios where you would use the language generation sub-system with your bot's replies to a user.

## Multilingual generation and language fallback policy

Your bot might target more than one spoken or display languages. You can manage separate instances of the *TemplateEngine*, one per target language.

## Additional resources

- See [.lg file format][lg-file-format] for more information about .lg files.
- Read [structured response templates](../language-generation/language-generation-structured-response-template.md) to learn more about complex templates.
- [C# API Reference](/dotnet/api/microsoft.bot.builder.languagegeneration)
- [JavaScript API reference](/javascript/api/botbuilder-lg)

[lg-file-format]:../file-format/bot-builder-lg-file-format.md

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
ms.date: 11/01/2021
monikerRange: 'azure-bot-service-4.0'
---

# Language Generation

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Language Generation (LG) allows developers to extract embedded strings from their code and resource files and manage them through a LG runtime and file format. With LG, developers can create a more natural conversation experience by defining multiple variations on a phrase, executing simple expressions based on context, and referring to conversational memory.

LG can be used by developers to:

- achieve a coherent personality, tone of voice for their bot
- separate business logic from presentation
- include variations and sophisticated composition based resolution for any of their bot's replies
- construct speak .vs. display adaptations
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

You can use LG in a variety of ways when developing bots. To start, create one or more [.lg file(s)][3] to cover all possible scenarios where you would use the language generation sub-system with your bot's replies to a user.

# [C#](#tab/csharp)

Make sure you include the language Generation library [`Microsoft.Bot.Builder.LanguageGeneration`](https://www.nuget.org/packages/Microsoft.Bot.Builder.LanguageGeneration/). Then parse and load templates in your .lg file by adding the following:

```csharp
    _templates = Templates.ParseFile(fullPath);
```

# [JavaScript](#tab/javascript)

Make sure you include the language Generation library [`botbuilder-lg`][15]. Then parse and load templates in your .lg file by adding the following:

```javascript
     let lgTemplates = Templates.parseFile(fullPath);
```

---

When you need template expansion, use `Evaluate` and pass in the relevant template name.

# [C#](#tab/csharp)

```csharp
    var lgOutput = _templates.Evaluate(<TemplateName>);
```

# [JavaScript](#tab/javascript)

```javascript
    let lgOutput = lgTemplates.evaluate(<TemplateName>);
```

---

If your template needs specific properties to be passed for resolution/expansion, you can pass them when calling  `Evaluate`.

# [C#](#tab/csharp)

```csharp
    var lgOutput = _templates.Evaluate("WordGameReply", new { GameName = "MarcoPolo" } );
```

# [JavaScript](#tab/javascript)

```javascript
    let lgOutput = lgTemplates.evaluate("WordGameReply", { GameName = "MarcoPolo" } );
```

---

## Multilingual generation and language fallback policy

Your bot might target more than one spoken or display language. You can manage separate instances of the *TemplateEngine*, one per target language. For an example of how to add multiple languages, also known as language fallback, to your bot, see the multi-turn prompt with language fallback sample in [C#](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/language-generation/05.a.multi-turn-prompt-with-language-fallback) or [JavaScript](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/language-generation/05.a.multi-turn-prompt-with-language-fallback).

<!--
## Grammar check and correction
The current library does not include any capabilities for grammar check or correction-->

## Expand API

To get all possible expansions of a template, you can use `ExpandTemplate`.

# [C#](#tab/csharp)

```csharp
    var results = lgTemplates.ExpandTemplate("WordGameReply", { GameName = "MarcoPolo" } )
```

# [JavaScript](#tab/javascript)

```javascript
    const results = lgTemplates.expandTemplate("WordGameReply", { GameName = "MarcoPolo" } )
```

---

For example, given this LG content:

```lg
# Greeting
- Hi
- Hello

#TimeOfDay
- Morning
- Evening

# FinalGreeting
- ${Greeting()} ${TimeOfDay()}

# TimeOfDayWithCondition
- IF: ${time == 'morning'}
    - ${Greeting()} Morning
- ELSEIF: ${time == 'evening'}
    - ${Greeting()} Evening
- ELSE:
    - ${Greeting()} Afternoon
```

The call `ExpandTemplate("FinalGreeting")` results in four evaluations:

- **Hi Morning**
- **Hi Evening**
- **Hello Morning**
- **Hello Evening**

The call `ExpandTemplate("TimeOfDayWithCondition", new { time = "evening" })` with scope, results in two expansions:

- **Hi Evening**
- **Hello Evening**

## Additional resources

- See [.lg file format][3] for more information about .lg files.
- Read [structured response templates](../language-generation/language-generation-structured-response-template.md) to learn more about complex templates.
- [C# API Reference](/dotnet/api/microsoft.bot.builder.languagegeneration)
- [JavaScript API reference](/javascript/api/botbuilder-lg)

[1]:https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md
[3]:../file-format/bot-builder-lg-file-format.md
[6]:https://github.com/microsoft/botframework-cli/tree/master/packages/chatdown
[7]:https://github.com/microsoft/botframework-cli/blob/master/packages/chatdown/docs/chatdown-format.md
[8]:https://github.com/microsoft/botframework-cli/blob/master/packages/chatdown/docs/examples/CardExamples.chat
[9]:https://github.com/microsoft/botframework-cli/blob/master/packages/chatdown/docs/chatdown-format.md#message-commands
[10]:https://github.com/microsoft/botframework-cli/blob/master/packages/chatdown/docs/chatdown-format.md#message-cards
[11]:https://github.com/microsoft/botframework-cli/blob/master/packages/chatdown/docs/chatdown-format.md#message-attachments
[12]:https://botbuilder.myget.org/F/botbuilder-v4-dotnet-daily/api/v3/index.json
[13]:https://botbuilder.myget.org/gallery/botbuilder-v4-js-daily
[14]:https://www.nuget.org/packages/Microsoft.Bot.Builder.LanguageGeneration/4.7.0-preview
[15]:https://www.npmjs.com/package/botbuilder-lg
[20]:../file-format/bot-builder-lg-file-format.md#switch-case
[21]:../file-format/bot-builder-lg-file-format.md#importing-external-references
[22]:https://github.com/microsoft/botbuilder-tools/tree/lg-vscode-extension/packages/LGvscodeExt
[23]:https://github.com/microsoft/botbuilder-tools/tree/V.Future/packages/MSLG
[26]:https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/language-generation/javascript_nodejs
[50]:../file-format/bot-builder-lg-file-format.md#importing-external-references
[100]:https://github.com/microsoft/BotBuilder-Samples/blob/master/experimental/language-generation/csharp_dotnetcore/20.extending-with-custom-functions/README.md
[101]:https://github.com/microsoft/BotBuilder-Samples/blob/master/experimental/language-generation/javascript_nodejs/20.custom-functions/README.md

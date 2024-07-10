---
title: Structured response template in Bot Framework SDK
description: Describe the structure response templates available with language generation.
keywords: structure response template, reference, language generation, lg
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: reference
ms.date: 09/21/2021
monikerRange: 'azure-bot-service-4.0'
ms.custom:
  - evergreen
---

# Structured response template

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Structured response templates let developers define a complex structure that supports the extensive functionality of [Language generation (LG)](../v4sdk/bot-builder-concept-language-generation.md), like templating, composition, while leaving the interpretation of the structured response up to the caller of the LG library.

For bot applications, the following support is provided:

- [activity](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md) definition
- [card](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-cards.md) definition

The [Bot Framework activity](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md) template includes several customizable fields. The following properties are the most commonly used and are configurable via an activity template definition:

| Property          | Use case                                                                                                                          |
|-------------------|-----------------------------------------------------------------------------------------------------------------------------------|
| Text              | Display text used by the channel to render visually                                                                               |
| Speak             | Spoken text used by the channel to render audibly                                                                                 |
| Attachments       | List of attachments with their type. Used by channels to render as UI cards or other generic file attachment types.                |
| SuggestedActions  | List of actions rendered as suggestions to user.                                                                                  |
| InputHint         | Controls audio capture stream state on devices that support spoken input. Possible values include `accepting`, `expecting`, or `ignoring`.   |

There is no default fallback behavior implemented by the template resolver. If a property isn't specified, then it remains unspecified. For example, the `Speak` property isn't automatically assigned to be the `Text` property if only the `Text` property is specified.

## Definition

Here's the definition of a structured template:

```lg
# TemplateName
> this is a comment
[Structure-name
    Property1 = <plain text> .or. <plain text with template reference> .or. <expression>
    Property2 = list of values are denoted via '|'. e.g. a | b
> this is a comment about this specific property
    Property3 = Nested structures are achieved through composition
]
```

Here's an example of a basic text template:

```lg
# AskForAge.prompt
[Activity
    Text = ${GetAge()}
    Speak = ${GetAge()}
]

# GetAge
- how old are you?
- what is your age?
```

Here's an example of text with a suggested action. Use **|** to denote a list.

```lg
> With '|' you are making attachments a list.
# AskForAge.prompt
[Activity
    Text = ${GetAge()}
    SuggestedActions = 10 | 20 | 30
]
```

Here's an example of a [Hero card](/microsoftteams/platform/task-modules-and-cards/cards/cards-reference#hero-card) definition:

```lg
# HeroCard
[Herocard
    title = Hero Card Example
    subtitle = Microsoft Bot Framework
    text = Build and connect intelligent bots to interact with your users naturally wherever they are, from text/sms to Skype, Slack, Office 365 mail and other popular services.
    images = https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg
    buttons = Option 1| Option 2| Option 3
]
```

> [!NOTE]
>
> LG provides some variability in card definition, which is converted to align with the [SDK card definition](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-cards.md). For example, both `image` and `images` fields are supported in all the card definitions in LG even though only `images` are supported in the SDK card definition.
>
>The values defined in all of the `image` and `images` fields in a HeroCard or thumbnail card are combined and converted to an images list in the generated card. For the other types of cards, the last defined value in the template will be assigned to the `image` field. The values you assign to the `image/images` field can be a string, [adaptive expression](/azure/bot-service/bot-builder-concept-adaptive-expressions), or array in the format using **|**.

Below is the combination of the previous templates:

```lg
# AskForAge.prompt
[Activity
    Text = ${GetAge()}
    Speak = ${GetAge()}
    Attachments = ${HeroCard()}
    SuggestedActions = 10 | 20 | 30
    InputHint = expecting
]

# GetAge
- how old are you?
- what is your age?

# HeroCard
[Herocard
    title = Hero Card Example
    subtitle = Microsoft Bot Framework
    text = Build and connect intelligent bots to interact with your users naturally wherever they are, from text/sms to Skype, Slack, Office 365 mail and other popular services.
    images = https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg
    buttons = Option 1| Option 2| Option 3
]
```

By default any template reference is evaluated once during evaluation of a structured template.

For example, `# AskForAge.prompt` returns the same resolution text for both the `Speak` and `Text` properties.

```lg
# AskForAge.prompt
[Activity
    Text = ${GetAge()}
    Speak = ${GetAge()}
]

# GetAge
- how old are you?
- what is your age?
```

You can use `<TemplateName>!()` to request a new evaluation on each reference within a structured template.

In the example below, `Speak` and `Text` may have different resolution text because `GetAge` is re-evaluated on each instance.

```lg
[Activity
    Text = ${GetAge()}
    Speak = ${GetAge!()}
]

# GetAge
- how old are you?
- what is your age?
```

Here's how to display a carousel of cards:

```lg
# AskForAge.prompt
[Activity
> Defaults to carousel layout in case of list of cards
    Attachments = ${foreach($cardValues, item, HeroCard(item)}
]

# AskForAge.prompt_2
[Activity
> Explicitly specify an attachment layout
    Attachments = ${foreach($cardValues, item, HeroCard(item)}
    AttachmentLayout = list
]

# HeroCard (title, subtitle, text)
[Herocard
    title = ${title}
    subtitle = ${subtitle}
    text = ${text}
    images = https://sec.ch9.ms/ch9/7ff5/e07cfef0-aa3b-40bb-9baa-7c9ef8ff7ff5/buildreactionbotframework_960.jpg
    buttons = Option 1| Option 2| Option 3
]
```

Use **\\**  as an escape character.

```lg
> You can use '\' as an escape character
> \${GetAge()} would not be evaluated as expression, would be parsed as '${getAge()}' string
# AskForAge.prompt
[Activity
        Text = \${GetAge()}
        SuggestedActions = 10 \| cards | 20 \| cards
]
```

## Structured template composition

The following composition behavior is supported with structured templates:

- Composition is structure context-aware. If the target template being referred is also a structured template, then the structure type must match. For example, an ActivityTemplate can be referred to in another ActivityTemplate.
- References to simple or conditional response template can exist anywhere inside a structured template.

Suppose you have the following template:

```lg
# T1
[Activity
    Text = ${T2()}
    Speak = foo bar ${T3().speak}
]

# T2
- This is awesome

# T3
[Activity
    Speak = I can also speak!
]
```

A call to `evaluateTemplate('T1')` would result in the following internal structure:

```lg
[Activity
    Text = This is awesome
    Speak = I can also speak!
]
```

## Full reference to another structured template

You can include a reference to another structured template as a property in another structured template, or as  a reference in another simple or conditional response template

Here's an example of full reference to another structured template:

```lg
# ST1
[MyStruct
    Text = foo
    ${ST2()}
]
# ST2
[MyStruct
    Speak = bar
]
```

With this content, a call to `evaluateTemplate('ST1')` will result in the following internal structure:

```lg
[MyStruct
    Text = foo
    Speak = bar
]
```

When the same property exists in both the calling template as well as the called template, the content in the caller will overwrite any content in the called template.

Here's an example:

```lg
# ST1
[MyStruct
    Text = foo
    ${ST2()}
]
# ST2
[MyStruct
    Speak = bar
    Text = zoo
]
```

With this context, a call to `evaluateTemplate('ST1')` will result in the following internal structure:

```lg
[MyStruct
    Text = foo
    Speak = bar
]
```

Note that this style of composition can only exist at the root level. If there is a reference to another structured template within a property, then the resolution is contextual to that property.

## External file reference in attachment structured

There are two prebuilt functions used to externally reference files

1. `fromFile(fileAbsoluteOrRelativePath)` loads a specified file. Content returned by this function will support evaluation of content. Template references, properties, and expressions are evaluated.
1. `ActivityAttachment(content, contentType)` sets the `contentType` if it's not already specified in the content.

With these two prebuilt functions, you can pull in any externally defined content, including all card types. Use the following structured LG to compose an activity:

```lg
# AdaptiveCard
[Activity
                Attachments = ${ActivityAttachment(json(fromFile('../../card.json')), 'adaptiveCard')}
]

# HeroCard
[Activity
                Attachments = ${ActivityAttachment(json(fromFile('../../card.json')), 'heroCard')}
]
```

You can also use attachments, seen below:

```lg
# AdaptiveCard
[Attachment
    contenttype = adaptivecard
    content = ${json(fromFile('../../card.json'))}
]

# HeroCard
[Attachment
    contenttype = herocard
    content = ${json(fromFile('../../card.json'))}
]
```

## Additional Information

- [C# API Reference](/dotnet/api/microsoft.bot.builder.languagegeneration)
- [JavaScript API reference](/javascript/api/botbuilder-lg)
- Read [Debug with Adaptive Tools](../bot-service-debug-adaptive-tools.md) to learn how to analyze and debug templates.

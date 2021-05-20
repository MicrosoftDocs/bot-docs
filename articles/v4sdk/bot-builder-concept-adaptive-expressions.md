---
title: Adaptive expressions
description: Describes how adaptive expressions work within the Bot Framework SDK.
keywords: adaptive expressions
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 04/02/2021
monikerRange: 'azure-bot-service-4.0'
---

# Adaptive expressions

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Bots use adaptive expressions to evaluate the outcome of a condition based on runtime information available in memory to the dialog or the [Language Generation](bot-builder-concept-language-generation.md) system. These evaluations determine how your bot reacts to user input and other factors that impact bot functionality.

Adaptive expressions address this core need by providing an adaptive expression language that can be used with the Bot Framework SDK and other conversational AI components, like [Bot Framework Composer](https://github.com/microsoft/BotFramework-Composer), [Language Generation](bot-builder-concept-language-generation.md), [Adaptive dialogs](bot-builder-adaptive-dialog-Introduction.md), and [Adaptive Cards templating](/adaptive-cards/templating).

An adaptive expression can contain one or more explicit values, [prebuilt functions](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md), or custom functions. Consumers of adaptive expressions also have the capability to inject additional supported functions. For example, all language generation templates are available as functions as well as additional functions that are only available within that component's use of adaptive expressions.

## Operators

Adaptive expressions support the following operator types and expression syntax:

- arithmetic
- comparison
- logical
- other operators and expressions syntax

### [Arithmetic](#tab/arithmetic)

| Operator    |                                  Functionality                                            |   Prebuilt function equivalent    |
|-----------|-------------------------------------------------------------------------------------------|-----------------------------------|
|+          | Addition. Example: A + B                                                    |[add][1]                           |
|-            | Subtraction. Example: A - B                                                |[sub][2]                           |
|unary +    | Positive value. Example: +1, +A                                                    |N/A                                |
|unary -    | Negative value. Example: -2, -B                                            |N/A                                |
|\*            | Multiplication. Example: A \* B                                            |[mul][3]                           |
|/            | Division. Example: A / B                                                    |[div][4]                           |
|^            | Exponentiation. Example: A ^ B                                            |[exp][5]                           |
|%            | Modulus. Example: A % B                                                    |[mod][6]                           |

### [Comparison](#tab/comparison)

| Operator    |                                  Functionality                                            |   Prebuilt function equivalent    |
|-----------|-------------------------------------------------------------------------------------------|-----------------------------------|
|==            | Equals. Example: A == B                                                    |[equals][7]                        |
|\!=            | Not equals. Example: A != B                                                |[not][8]([equals][7]())            |
|\>            | Greater than. Example: A > B                                                   |[greater][9]                       |
|\<            | Less than. Example: A < B                                                        |[less][10]                         |
|\>=         | Greater than or equal. Example: A >= B                                        |[greaterOrEquals][11]              |
|\<=            | Less than or equal. Example: A <= B                                            |[lessOrEquals][12]                 |

### [Logical](#tab/logical)

| Operator    |                                  Functionality                                            |   Prebuilt function equivalent    |
|-----------|-------------------------------------------------------------------------------------------|-----------------------------------|
|&&            |And. Example: exp1 && exp2                                                    |[and][13]                          |
|\|\|        |Or. Example: exp1 \|\| exp2                                                    |[or][14]                           |
|\!            |Not. Example: !exp1                                                            |[not][8]                           |

### [Other](#tab/other)

| Operator    |                                  Functionality                                            |   Prebuilt function equivalent    |
|-----------|-------------------------------------------------------------------------------------------|-----------------------------------|
|&, +            |Concatenation operators. Operands will always be cast to string. Examples: A & B, 'foo' + ' bar' => 'foo bar', 'foo' + 3 => 'foo3', 'foo' + (3 + 3) => 'foo6'                |N/A                                |
|'            |Used to wrap a string literal. Example: 'myValue'                                                |N/A                                |
|"            |Used to wrap a string literal. Example: "myValue"                                                |N/A                                |
|\[\]            |Used to refer to an item in a list by its index. Example: A[0]                                    |N/A                                |
|${}        |Used to denote an expression. Example: ${A == B}.                                              |N/A                                |
|${}        |Used to denote a variable in template expansion. Example: ${myVariable}                        |N/A                                |
|\(\)            |Enforces precedence order and groups sub expressions into larger expressions. Example: (A+B)\*C    |N/A                                |
|\.            |Property selector. Example: myObject.Property1                                                    |N/A                                |
|\\            |Escape character for templates, expressions.                                               |N/A                                |

---

## Variables

Variables are always referenced by their name in the format `${myVariable}`.  They can be referenced either by the property selector operator in the form of `myParent.myVariable`, using the item index selection operator like in `myParent.myList[0]`, or by the [getProperty()](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#getProperty) function.

There are two special variables. **[]** represents an empty list, and **{}** represents a empty object.

## Explicit values

Explicit values can be enclosed in either single quotes 'myExplicitValue' or double quotes "myExplicitValue".

## Functions

An adaptive expression has one or more functions. For more information about functions supported by adaptive expressions, see the [prebuilt functions](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md) reference article.

## Bot Framework Composer

Bot Framework Composer is an open-source visual authoring canvas for developers and multidisciplinary teams to build bots. Composer uses adaptive expressions to create, calculate, and modify values. Adaptive expressions can be used in language generation template definitions and as properties in the authoring canvas. As seen in the example below, properties in memory can also be used in an adaptive expression. 

The expression `(dialog.orderTotal + dialog.orderTax) > 50` adds the values of the properties `dialog.orderTotal` and `dialog.orderTax`, and evaluates to `True` if the sum is greater than 50 or `False` if the sum is 50 or less.

Read [Conversation flow and memory](/composer/concept-memory) for more information about how expressions are used in memory.

## Language generation

Adaptive expressions are used by language generation (LG) systems to evaluate conditions described in LG templates. In the example below, the [join](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#join) prebuilt function is used to list all values in the `recentTasks` collection.

```lg
# RecentTasks
- IF: ${count(recentTasks) == 1}
    - Your most recent task is ${recentTasks[0]}. You can let me know if you want to add or complete a task.
- ELSEIF: ${count(recentTasks) == 2}
    - Your most recent tasks are ${join(recentTasks, ', ', ' and ')}. You can let me know if you want to add or complete a task.
- ELSEIF: ${count(recentTasks) > 2}
    - Your most recent ${count(recentTasks)} tasks are ${join(recentTasks, ', ', ' and ')}. You can let me know if you want to add or complete a task.
- ELSE:
    - You don't have any tasks.
```

Read the [using prebuilt function in variations](../file-format/bot-builder-lg-file-format.md#using-prebuilt-functions-in-variations) section of the [.lg file format](../file-format/bot-builder-lg-file-format.md) article for more information.

## Adaptive Cards templating

[Adaptive Cards templating](/adaptive-cards/templating/) can be used by developers of bots and other technologies to separate data from the layout in an Adaptive Card. Developers can provide [data inline](/adaptive-cards/templating/language#option-a-inline-data) with the `AdaptiveCard` payload, or the more common approach of [separating the data from the template](/adaptive-cards/templating/language#option-b-separating-the-template-from-the-data).

For example, say you have the following data:

```json
{
    "id": "1291525457129548",
    "status": 4,
    "author": "Matt Hidinger",
    "message": "{\"type\":\"Deployment\",\"buildId\":\"9542982\",\"releaseId\":\"129\",\"buildNumber\":\"20180504.3\",\"releaseName\":\"Release-104\",\"repoProvider\":\"GitHub\"}",
    "start_time": "2018-05-04T18:05:33.3087147Z",
    "end_time": "2018-05-04T18:05:33.3087147Z"
}
```
The `message` property is a JSON-serialized string. To access the values within the string, the [json](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#json) prebuilt function can be called:

```json
{
    "type": "TextBlock",
    "text": "${json(message).releaseName}"
}
```

And will result to the following object:

```json
{
    "type": "TextBlock",
    "text": "Release-104"
}
```

For more information and examples see the [adaptive cards templating documentation](/adaptive-cards/templating/).

## Additional resources

- [NuGet AdaptiveExpressions](https://www.nuget.org/packages/AdaptiveExpressions) package for C#
- [npm adaptive-expressions](https://www.npmjs.com/package/adaptive-expressions) package for JavaScript
- [Prebuilt functions](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md) supported by the adaptive expressions library
- [C# API reference](/dotnet/api/adaptiveexpressions)
- [JavaScript API reference](/javascript/api/adaptive-expressions)

[1]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#add
[2]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#sub
[3]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#mul
[4]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#div
[5]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#exp
[6]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#mod
[7]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#equals
[8]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#not
[9]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#greater
[10]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#less
[11]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#greaterOrEquals
[12]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#lessOrEquals
[13]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#and
[14]:../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#or
[15]:https://botbuilder.myget.org/feed/botbuilder-declarative/package/nuget/Microsoft.Bot.Builder.Expressions
[20]:https://github.com/microsoft/BotBuilder-Samples/blob/master/experimental/language-generation/README.md

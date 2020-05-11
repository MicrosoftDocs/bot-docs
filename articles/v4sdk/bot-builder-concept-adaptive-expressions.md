---
title: Adaptive expressions
description: Describes how adaptive expressions work within the Bot Framework SDK.
keywords: adaptive expressions
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/16/2020
monikerRange: 'azure-bot-service-4.0'
---

# Adaptive expressions

Bots use expressions to evaluate the outcome of a condition based on runtime information available in memory to the dialog or the [Language Generation](bot-builder-concept-language-generation.md) system. These evaluations determine how your bot reacts to user input and other factors that impact bot functionality.

Adaptive expressions address this core need by providing an adaptive expression language that can be used with the Bot Framework SDK and other conversational AI components, like [Bot Framework Composer](https://github.com/microsoft/BotFramework-Composer#microsoft-bot-framework-composer-preview), [Language Generation](bot-builder-concept-language-generation.md), [Adaptive dialogs](https://aka.ms/bot-builder-adaptive-dialogs-concept), and [Adaptive Cards](https://docs.microsoft.com/adaptive-cards/).

An adaptive expression can contain one or more explicit values, [prebuilt functions](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md), or custom functions. Consumers of adaptive expressions also have the capability to inject additional supported functions. For example, all Language Generation templates are available as functions as well as additional functions that are only available within that component's use of adaptive expressions.

## Operators

Adaptive expressions support the following operator types and expression syntax:

- [arithmetic](#arithmetic-operators)
- [comparison](#comparison-operators)
- [logical](#logical-operators)
- [arithmetic](#other-operators-and-expression-syntax)

### Arithmetic operators

| Operator    |                                  Functionality                                            |   Prebuilt function equivalent    |
|-----------|-------------------------------------------------------------------------------------------|-----------------------------------|
|+          | Addition. Example: A + B                                                    |[add][1]                           |
|-            | Subtraction. Example: A – B                                                |[sub][2]                           |
|unary +    | Positive value. Example: +1, +A                                                    |N/A                                |
|unary -    | Negative value. Example: –2, -B                                            |N/A                                |
|*            | Multiplication. Example: A * B                                            |[mul][3]                           |
|/            | Division. Example: A / B                                                    |[div][4]                           |
|^            | Exponentiation. Example: A ^ B                                            |[exp][5]                           |
|%            | Modulus. Example: A % B                                                    |[mod][6]                           |

### Comparison operators

| Operator    |                                  Functionality                                            |   Prebuilt function equivalent    |
|-----------|-------------------------------------------------------------------------------------------|-----------------------------------|
|==            | Equals. Example: A == B                                                    |[equals][7]                        |
|!=            | Not equals. Example: A != B                                                |[not][8]([equals][7]())            |
|>            | Greater than. Example: A > B                                                   |[greater][9]                       |
|<            | Less than. Example: A < B                                                        |[less][10]                         |
|>=         | Greater than or equal. Example: A >= B                                        |[greaterOrEquals][11]              |
|<=            | Less than or equal. Example: A <= B                                            |[lessOrEquals][12]                 |

### Logical operators

| Operator    |                                  Functionality                                            |   Prebuilt function equivalent    |
|-----------|-------------------------------------------------------------------------------------------|-----------------------------------|
|&&            |And. Example: exp1 && exp2                                                    |[and][13]                          |
|\|\|        |Or. Example: exp1 \|\| exp2                                                    |[or][14]                           |
|!            |Not. Example: !exp1                                                            |[not][8]                           |


### Other operators and expression syntax

| Operator    |                                  Functionality                                            |   Prebuilt function equivalent    |
|-----------|-------------------------------------------------------------------------------------------|-----------------------------------|
|&, +            |Concatenation operators. Operands will always be cast to string. Examples: A & B, 'foo' + ' bar' => 'foo bar', 'foo' + 3 => 'foo3', 'foo' + (3 + 3) => 'foo6'                |N/A                                |
|'            |Used to wrap a string literal. Example: 'myValue'                                                |N/A                                |
|"            |Used to wrap a string literal. Example: "myValue"                                                |N/A                                |
|[]            |Used to refer to an item in a list by its index. Example: A[0]                                    |N/A                                |
|${}        |Used to denote an expression. Example: ${A == B}.                                              |N/A                                |
|${}        |Used to denote a variable in template expansion. Example: ${myVariable}                        |N/A                                |
|()            |Enforces precedence order and groups sub expressions into larger expressions. Example: (A+B)*C    |N/A                                |
|.            |Property selector. Example: myObject.Property1                                                    |N/A                                |
|\            |Escape character for templates, expressions.                                               |N/A                                |

## Variables

Variables are always referenced by their name in the format `${myVariable}`.  They can be referenced either by the property selector operator in the form of `myParent.myVariable`, using the item index selection operator like in `myParent.myList[0]`, or by the [getProperty](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md#getProperty) function.

There are two special variables. `[]` represents an empty list, and `{}` represents a empty object.

## Explicit values

Explicit values can be enclosed in either single quotes 'myExplicitValue' or double quotes "myExplicitValue".

## Additional resources

- [NuGet AdaptiveExpressions](https://www.nuget.org/packages/AdaptiveExpressions) package for C#
- [npm adaptive-expressions](https://www.npmjs.com/package/adaptive-expressions) package for Javascript
- [Prebuilt functions](../adaptive-expressions/adaptive-expressions-prebuilt-functions.md) supported by the Adaptive Expressions library
<!--
- [API reference](../adaptive-expressions/adaptive-expressions-api-reference.md) for Adaptive Expressions
- [Extend functions](./extend-functions.md)-->

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

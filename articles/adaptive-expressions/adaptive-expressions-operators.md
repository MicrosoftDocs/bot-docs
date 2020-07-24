---
title: Adaptive expressions operatiors
description: Describes operator types supported by adaptive expressions.
keywords: adaptive expressions, operators, reference
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 07/24/2020
monikerRange: 'azure-bot-service-4.0'
---


# Adaptive expressions operators

[!INCLUDE[applies-to](../includes/applies-to.md)]

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

### Logical operators](#tab/logical)

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

---
title: Customize a form using FormBuilder | Microsoft Docs
description: Learn how to dynamically change and customize the conversation flow and contents using FormBuilder for the Bot Framework SDK for .NET.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Customize a form using FormBuilder

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

[Basic features of FormFlow](bot-builder-dotnet-formflow.md) describes a basic FormFlow implementation that 
delivers a fairly generic user experience, and [Advanced features of FormFlow](bot-builder-dotnet-formflow-advanced.md) describes how you can 
customize user experience by using business logic and attributes. 
This article describes how you can use 
[FormBuilder][formBuilder] to customize user experience even further, by 
specifying the sequence in which the form executes steps 
and dynamically defining field values, confirmations, and messages. 

## Dynamically define field values, confirmations, and messages

Using FormBuilder, you can dynamically define field values, confirmations, and messages.

### Dynamically define field values 

A sandwich bot that is designed to add a free drink or cookie to any order that specifies a foot-long sandwich 
uses the `Sandwich.Specials` field to store data about free items. 
In this case, the value of the `Sandwich.Specials` field must be dynamically set 
for each order according to whether or not the order contains a foot-long sandwich. 

The `Specials` field is specified as optional and "None" is designated as text for the choice that indicates no preference.

[!code-csharp[Field definition](../includes/code/dotnet-formflow-formbuilder.cs#fieldDefinition)]

This code example shows how to dynamically set the value of the `Specials` field. 

[!code-csharp[Define value](../includes/code/dotnet-formflow-formbuilder.cs#defineValue)]

In this example, the [Advanced.Field.SetType][setType] method specifies 
the field type (`null` represents an enumeration field). 
The [Advanced.Field.SetActive][setActive] method specifies that the field 
should only be enabled if the length of the sandwich is `Length.FootLong`. 
Finally, the [Advanced.Field.SetDefine][setDefine] method specifies an async 
delegate that defines the field. 
The delegate is passed the current state object and the [Advanced.Field][field] that is being dynamically defined. 
The delegate uses the field's fluent methods to dynamically define values. 
In this example, the values are strings and the `AddDescription` and `AddTerms` methods specify the descriptions and terms for each value.

> [!NOTE]
> To dynamically define a field value, you can implement 
> [Advanced.IField][iField] yourself, 
> or streamline the process by using the [Advanced.FieldReflector][FieldReflector] class as shown in the example above. 

### Dynamically define messages and confirmations

Using FormBuilder, you can also dynamically define messages and confirmations. 
Each message and confirmation runs only when prior steps in the form are inactive or completed. 

This code example shows a dynamically generated confirmation that computes the cost of the sandwich. 

[!code-csharp[Define confirmation](../includes/code/dotnet-formflow-formbuilder.cs#defineConfirmation)]

## Customize a form using FormBuilder

This code example uses FormBuilder to define the steps of the form, 
[validate selections](bot-builder-dotnet-formflow-advanced.md#add-business-logic), 
and [dynamically define a field value and confirmation](#dynamically-define-field-values-confirmations-and-messages). 
By default, steps in the form will be executed in the sequence in which they are listed. 
However, steps might be skipped for fields that already contain values or if explicit navigation is specified. 

[!code-csharp[FormBuilder form](../includes/code/dotnet-formflow-formbuilder.cs#formBuilderForm)]

In this example, the form executes these steps:

- Shows a welcome message. 
- Fills in `SandwichOrder.Sandwich`. 
- Fills in `SandwichOrder.Length`. 
- Fills in `SandwichOrder.Bread`. 
- Fills in `SandwichOrder.Cheese`. 
- Fills in `SandwichOrder.Toppings` and adds missing values if the user selected `ToppingOptions.Everything`. 
-. Shows a message that confirms the selected toppings. 
- Fills in `SandwichOrder.Sauces`. 
- [Dynamically defines](#dynamically-define-field-values) the field value for `SandwichOrder.Specials`. 
- [Dynamically defines](#dynamically-define-messages-and-confirmations) the confirmation for cost of the sandwich. 
- Fills in `SandwichOrder.DeliveryAddress` and [verifies](bot-builder-dotnet-formflow-advanced.md#add-business-logic) the resulting string. If the address does not start with a number, the form returns a message. 
- Fills in `SandwichOrder.DeliveryTime` with a custom prompt. 
- Confirms the order. 
- Adds any remaining fields that were defined in the class but not explicitly referenced by `Field`. (If the example did not call the `AddRemainingFields` method, the form would not include any fields that were not explicity referenced.) 
- Shows a thank you message. 
- Defines an `OnCompletionAsync` handler to process the order. 

## Sample code

[!INCLUDE [Sample code](../includes/snippet-dotnet-formflow-samples.md)]

## Additional resources

- [Basic features of FormFlow](bot-builder-dotnet-formflow.md)
- [Advanced features of FormFlow](bot-builder-dotnet-formflow-advanced.md)
- [Localize form content](bot-builder-dotnet-formflow-localize.md)
- [Define a form using JSON schema](bot-builder-dotnet-formflow-json-schema.md)
- [Customize user experience with pattern language](bot-builder-dotnet-formflow-pattern-language.md)
- <a href="/dotnet/api/?view=botbuilder-3.11.0" target="_blank">Bot Framework SDK for .NET Reference</a>

[formBuilder]: /dotnet/api/microsoft.bot.builder.formflow.formbuilder-1

[setType]: /dotnet/api/microsoft.bot.builder.formflow.advanced.field-1.settype

[setActive]: /dotnet/api/microsoft.bot.builder.formflow.advanced.field-1.setactive

[setDefine]: /dotnet/api/microsoft.bot.builder.formflow.advanced.field-1.setdefine

[field]: /dotnet/api/microsoft.bot.builder.formflow.advanced.field-1

[iField]: /dotnet/api/microsoft.bot.builder.formflow.advanced.ifield-1

[FieldReflector]: /dotnet/api/microsoft.bot.builder.formflow.advanced.fieldreflector-1

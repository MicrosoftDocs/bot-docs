---
title: Define a form using JSON schema and FormFlow in the Bot Builder SDK for .NET | Microsoft Docs
description: Learn how to define a form using JSON schema and FormFlow with the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, FormFlow, JSON, form, define form, JSON schema
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/29/2017
ms.reviewer:
ROBOTS: Index, Follow
---

# Define a form using JSON schema

If you use a [C# class](~/dotnet/formflow.md#create-class) to define the form 
when you create a bot with FormFlow, 
the form derives from the static definition of your type in C#. 
As an alternative, you may instead define the form by using 
<a href="http://json-schema.org/documentation.html" target="_blank">JSON schema</a>. 
A form that is defined using JSON schema is purely data-driven; 
you can change the form (and therefore, the behavior of the bot) simply by updating the schema. 

The JSON schema describes the fields within your 
<a href="http://www.newtonsoft.com/json/help/html/T_Newtonsoft_Json_Linq_JObject.htm" target="_blank">JObject</a> 
and 
includes annotations that control prompts, templates, and terms. 
To use JSON schema with FormFlow, you must add the `Microsoft.Bot.Builder.FormFlow.Json` NuGet package to your 
project and import the `Microsoft.Bot.Builder.FormFlow.Json` namespace.

## Standard keywords 

FormFlow supports these standard <a href="http://json-schema.org/documentation.html" target="_blank">JSON Schema</a> 
keywords:

| Keyword | Description | 
|----|----|
| type | Defines the type of data that the field contains. |
| enum | Defines the valid values for the field. |
| minimum | Defines the minimum numeric value allowed for the field (as described in [NumericAttribute][numericAttribute]). |
| maximum | Defines the maximum numeric value allowed for the field (as described in [NumericAttribute][numericAttribute]). |
| required | Defines which fields are required. |
| pattern | Validates string values (as described in [PatternAttribute][patternAttribute]). |

## Extensions to JSON Schema

FormFlow extends the standard <a href="http://json-schema.org/documentation.html" target="_blank">JSON Schema</a> 
to support several additional properties.

### Additional properties at the root of the schema

| Property | Value |
|----|----|
| OnCompletion | C# script with arguments `(IDialogContext context, JObject state)` for completing the form. |
| References | References to include in scripts. For example, `[assemblyReference, ...]`. Paths should be absolute or relative to the current directory. By default, the script includes `Microsoft.Bot.Builder.dll`. |
| Imports | Imports to include in scripts. For example, `[import, ...]`. By default, the script includes the `Microsoft.Bot.Builder`, `Microsoft.Bot.Builder.Dialogs`, `Microsoft.Bot.Builder.FormFlow`, `Microsoft.Bot.Builder.FormFlow.Advanced`, `System.Collections.Generic`, and `System.Linq` namespaces. |

### Additional properties at the root of the schema or as peers of the type property

| Property | Value |
|----|----|
| Templates | `{ TemplateUsage: { Patterns: [string, ...], <args> }, ...}` |
| Prompt | `{ Patterns:[string, ...] <args>}` |

To specify templates and prompts in JSON schema, use the same vocabulary as defined by 
[TemplateAttribute][templateAttribute] and [PromptAttribute][promptAttribute]. 
Property names and values in the schema should match the property names and values in the underlying C# enumeration. 
For example, this schema snippet defines a template that overrides the `TemplateUsage.NotUnderstood` template and specifies a `TemplateBaseAttribute.ChoiceStyle`: 

```json
"Templates":{ "NotUnderstood": { "Patterns": ["I don't get it"], "ChoiceStyle":"Auto"}}
```

### Additional properties as peers of the type property

| Property | Contents | Description |
|----|----|----|
| DateTime | bool | Indicates whether field is a `DateTime` field. |
| Describe | string or object | Description of a field as described in [DescribeAttribute][describeAttribute]. |
| Terms | `[string,...]` | Regular expressions for matching a field value as described in TermsAttribute. |
| MaxPhrase | int | Runs your terms through `Language.GenerateTerms(string, int)` to expand them. |
| Values | `{ string: {Describe:string|object, Terms:[string, ...], MaxPhrase}, ...}` | The string must be found in the type's enumeration. This allows you to override the automatically generated descriptions and terms. If `MaxPhrase` is specified, the terms are passed through `Language.GenerateTerms(string, int)`. |
| Active | script | C# script with arguments `(JObject state)->bool` to test whether the field, message, or confirmation is active. |
| Validate | script | C# script with arguments `(JObject state, object value)->ValidateResult` for validating a field value. |
| Define | script | C# script with arguments `(JObject state, Field<JObject> field)` for dynamically defining a field. |
| Next | script | C# script with arguments `(object value, JObject state)` for determining the next step after filling in a field. |
| Before | `[confirm|message, ...]` | Messages or confirmations before the containing field. (See below for details.) |
| After| `[confirm|message, ...]` | Messages or confirmations after the containing field. (See below for details.) |
| Dependencies | [string, ...] | Fields that this field, message, or confirmation depends on. |

Use `{Confirm:script|[string, ...], ...templateArgs}` within the value of the **Before** property or 
the **After** property to define a confirmation by using either a C# script with argument `(JObject state)` 
or a set of patterns that will be randomly selected with optional template arguments.

Use `{Message:script|[string, ...] ...templateArgs}` within the value of the **Before** property or 
the **After** property to define a message by using either a C# script with argument `(JObject state)` 
or a set of patterns that will be randomly selected with optional template arguments.

## Scripts

Several of the properties that are described above contain a script as the property value. 
A script can be any snippet of C# code that you might normally find in the body of a method. 
You can add references by using the **References** property and/or the **Imports** property. 
Special global variables include:

| Variable | Description |
|----|----|
| choice | Internal dispatch for the script to execute. |
| state | `JObject` form state bound for all scripts. |
| ifield | `IField<JObject>` to allow reasoning over the current field for all scripts except Message/Confirm prompt builders. |
| value | Object value to be validated for **Validate**. |
| field | `Field<JObject>` to allow dynamically updating a field in **Define**. |
| context | `IDialogContext` context to allow posting results in **OnCompletion**. |

Fields that are defined via JSON schema have the same ability to extend or override the definitions programatically as any other field. They can also be localized in the same way.

## Example JSON schema

The simplest way to define a form is to define everything, including any C# code, directly in the 
JSON schema definition. 
This example shows the JSON schema for the annotated sandwich bot that is described in 
[Customize a form using FormBuilder](~/dotnet/formflow-formbuilder.md).

```json
{
  "References": [ "Microsoft.Bot.Sample.AnnotatedSandwichBot.dll" ],
  "Imports": [ "Microsoft.Bot.Sample.AnnotatedSandwichBot.Resource" ],
  "type": "object",
  "required": [
    "Sandwich",
    "Length",
    "Ingredients",
    "DeliveryAddress"
  ],
  "Templates": {
    "NotUnderstood": {
      "Patterns": [ "I do not understand \"{0}\".", "Try again, I don't get \"{0}\"." ]
    },
    "EnumSelectOne": {
      "Patterns": [ "What kind of {&} would you like on your sandwich? {||}" ],
      "ChoiceStyle": "Auto"
    }
  },
  "properties": {
    "Sandwich": {
      "Prompt": { "Patterns": [ "What kind of {&} would you like? {||}" ] },
      "Before": [ { "Message": [ "Welcome to the sandwich order bot!" ] } ],
      "Describe": { "Image": "https://placeholdit.imgix.net/~text?txtsize=16&txt=Sandwich&w=125&h=40&txttrack=0&txtclr=000&txtfont=bold" },
      "type": [
        "string",
        "null"
      ],
      "enum": [
        "BLT",
        "BlackForestHam",
        "BuffaloChicken",
        "ChickenAndBaconRanchMelt",
        "ColdCutCombo",
        "MeatballMarinara",
        "OvenRoastedChicken",
        "RoastBeef",
        "RotisserieStyleChicken",
        "SpicyItalian",
        "SteakAndCheese",
        "SweetOnionTeriyaki",
        "Tuna",
        "TurkeyBreast",
        "Veggie"
      ],
      "Values": {
        "RotisserieStyleChicken": {
          "Terms": [ "rotis\\w* style chicken" ],
          "MaxPhrase": 3
        }
      }
    },
    "Length": {
      "Prompt": {
        "Patterns": [ "What size of sandwich do you want? {||}" ]
      },
      "type": [
        "string",
        "null"
      ],
      "enum": [
        "SixInch",
        "FootLong"
      ]
    },
    "Ingredients": {
      "type": "object",
      "required": [ "Bread" ],
      "properties": {
        "Bread": {
          "type": [
            "string",
            "null"
          ],
          "Describe": {
            "Title": "Sandwich Bot",
            "SubTitle": "Bread Picker"
          },
          "enum": [
            "NineGrainWheat",
            "NineGrainHoneyOat",
            "Italian",
            "ItalianHerbsAndCheese",
            "Flatbread"
          ]
        },
        "Cheese": {
          "type": [
            "string",
            "null"
          ],
          "enum": [
            "American",
            "MontereyCheddar",
            "Pepperjack"
          ]
        },
        "Toppings": {
          "type": "array",
          "items": {
            "type": "integer",
            "enum": [
              "Everything",
              "Avocado",
              "BananaPeppers",
              "Cucumbers",
              "GreenBellPeppers",
              "Jalapenos",
              "Lettuce",
              "Olives",
              "Pickles",
              "RedOnion",
              "Spinach",
              "Tomatoes"
            ],
            "Values": {
              "Everything": { "Terms": [ "except", "but", "not", "no", "all", "everything" ] }
            }
          },
          "Validate": "var values = ((List<object>) value).OfType<string>(); var result = new ValidateResult {IsValid = true, Value = values} ; if (values != null && values.Contains(\"Everything\")) { result.Value = (from topping in new string[] {  \"Avocado\", \"BananaPeppers\", \"Cucumbers\", \"GreenBellPeppers\", \"Jalapenos\", \"Lettuce\", \"Olives\", \"Pickles\", \"RedOnion\", \"Spinach\", \"Tomatoes\"} where !values.Contains(topping) select topping).ToList();} return result;",
          "After": [ { "Message": [ "For sandwich toppings you have selected {Ingredients.Toppings}." ] } ]
        },
        "Sauces": {
          "type": [
            "array",
            "null"
          ],
          "items": {
            "type": "string",
            "enum": [
              "ChipotleSouthwest",
              "HoneyMustard",
              "LightMayonnaise",
              "RegularMayonnaise",
              "Mustard",
              "Oil",
              "Pepper",
              "Ranch",
              "SweetOnion",
              "Vinegar"
            ]
          }
        }
      }
    },
    "Specials": {
      "Templates": {
        "NoPreference": { "Patterns": [ "None" ] }
      },
      "type": [
        "string",
        "null"
      ],
      "Active": "return (string) state[\"Length\"] == \"FootLong\";",
      "Define": "field.SetType(null).AddDescription(\"cookie\", DynamicSandwich.FreeCookie).AddTerms(\"cookie\", Language.GenerateTerms(DynamicSandwich.FreeCookie, 2)).AddDescription(\"drink\", DynamicSandwich.FreeDrink).AddTerms(\"drink\", Language.GenerateTerms(DynamicSandwich.FreeDrink, 2)); return true;",
      "After": [ { "Confirm": "var cost = 0.0; switch ((string) state[\"Length\"]) { case \"SixInch\": cost = 5.0; break; case \"FootLong\": cost=6.50; break;} return new PromptAttribute($\"Total for your sandwich is {cost:C2} is that ok?\");" } ]
    },
    "DeliveryAddress": {
      "type": [
        "string",
        "null"
      ],
      "Validate": "var result = new ValidateResult{ IsValid = true, Value = value}; var address = (value as string).Trim(); if (address.Length > 0 && (address[0] < '0' || address[0] > '9')) {result.Feedback = DynamicSandwich.BadAddress; result.IsValid = false; } return result;"
    },
    "PhoneNumber": {
      "type": [ "string", "null" ],
      "pattern": "(\\(\\d{3}\\))?\\s*\\d{3}(-|\\s*)\\d{4}"
    },
    "DeliveryTime": {
      "Templates": {
        "StatusFormat": {
          "Patterns": [ "{&}: {:t}" ],
          "FieldCase": "None"
        }
      },
      "DateTime": true,
      "type": [
        "string",
        "null"
      ],
      "After": [ { "Confirm": [ "Do you want to order your {Length} {Sandwich} on {Ingredients.Bread} {&Ingredients.Bread} with {[{Ingredients.Cheese} {Ingredients.Toppings} {Ingredients.Sauces} to be sent to {DeliveryAddress} {?at {DeliveryTime}}?" ] } ]
    },
    "Rating": {
      "Describe": "your experience today",
      "type": [
        "number",
        "null"
      ],
      "minimum": 1,
      "maximum": 5,
      "After": [ { "Message": [ "Thanks for ordering your sandwich!" ] } ]
    }
  },
  "OnCompletion": "await context.PostAsync(\"We are currently processing your sandwich. We will message you the status.\");"
}
```

## Implement FormFlow with JSON schema

To implement FormFlow with a JSON schema, use `FormBuilderJson`, which supports the same fluent interface as `FormBuilder`. This code example shows how to implement the JSON schema for the annotated sandwich bot 
that is described in [Customize a form using FormBuilder](~/dotnet/formflow-formbuilder.md).

[!code-csharp[Use JSON schema](~/includes/code/dotnet-formflow-json-schema.cs#useSchema)]

## Additional resources

- [FormFlow](~/dotnet/formflow.md)
- [Advanced features of FormFlow](~/dotnet/formflow-advanced.md)
- [Customize a form using FormBuilder](~/dotnet/formflow-formbuilder.md)
- [Localize form content](~/dotnet/formflow-localize.md)
- [Customize user experience with pattern language](~/dotnet/formflow-pattern-language.md)


[numericAttribute]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/df/d31/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_numeric_attribute.html

[patternAttribute]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/da/d2b/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_pattern_attribute.html

[templateAttribute]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d7/d0a/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_template_attribute.html

[promptAttribute]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d0/d34/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_prompt_attribute.html

[describeAttribute]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/dd/d38/class_microsoft_1_1_bot_1_1_builder_1_1_form_flow_1_1_describe_attribute.html
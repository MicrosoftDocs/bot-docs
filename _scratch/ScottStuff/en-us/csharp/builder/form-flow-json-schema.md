layout: page
title: Defining your FormFlow using JSON Schema
permalink: /en-us/csharp/builder/formflow-jsonschema/
weight: 2430
parent1: Building your Bot Using Bot Builder for .NET
parent2: Chat Bots
---

<span style="color:red">This needs to be under FormFlow</span>

One drawback to using a C# class to define your form is that it's static. An alternative is to use JObject for your state and define the form using JSON Schema. The advantage of using JObject for your state is that the form definition is entirely driven by data rather than the static definition of C#. The schema provides a way to describe the fields that make up your JObject and also supports annotations similar to C# attributes for controlling prompts, templates and terms.

In order to utilize this feature, add the Microsoft.Bot.Builder.FormFlow.Json NuGet package to your project. This defines the Microsoft.Bot.Builder.FormFlow.Json namespace that contains the code that lets you use JSON Schema with FormFlow.

FormFlow makes use of a number of standard JSON Schema keywords including:

- type&mdash;Defines the fields type
- enum&mdash;Defines the possible field values
- minimum&mdash;Defines the minimum allowed value as described in NumericAttribute
- maximum&mdash;Defines the maximum allowed value as described in NumericAttribute
- required&mdash;Defines what field is required
- pattern&mdash;Validates string values as described in PatternAttribute


Templates and prompts use the same vocabulary as TemplateAttribute and PromptAttribute. The property names are the same and the values are the same as those in the underlying C# enumeration. For example to define a template to override the TemplateUsage.NotUnderstood template and specify a TemplateBaseAttribute.ChoiceStyle you would put this in your schema: 

{% highlight json %}
"Templates":{ "NotUnderstood": { Patterns: ["I don't get it"], "ChoiceStyle":"Auto"}}
{% endhighlight %}


Extensions defined at the root of the schema:

- OnCompletion&mdash;C# script with arguments (IDialogContext context, JObject state) for completing form.
- References&mdash;Define references to include in scripts. For example, [assemblyReference, ...]. Paths should be absolute or relative to the current directory. By default, the script includes Microsoft.Bot.Builder.dll.
- Imports&mdash;Define imports to include in scripts. For example, [import, ...]. By default, the script includes the Microsoft.Bot.Builder, Microsoft.Bot.Builder.Dialogs, Microsoft.Bot.Builder.FormFlow, Microsoft.Bot.Builder.FormFlow.Advanced, System.Collections.Generic, and System.Linq namespaces.

Extensions defined at the root of a schema or as a peer of the "type" property:

- Templates: {TemplateUsage: { Patterns:[string, ...], <args> }, ...}
- Prompt: { Patterns:[string, ...] <args>}

Extensions that are found in a property description as a peer to the "type" property of a JSON Schema:

- DateTime:bool&mdash;Marks a field as being a DateTime field.
- Describe:string|object&mdash;Description of a field as described in DescribeAttribute.
- Terms:[string,...]&mdash;Regular expressions for matching a field value as described in TermsAttribute.
- MaxPhrase:int&mdash;This will run your terms through Language.GenerateTerms(string, int) to expand them.
- Values:{ string: {Describe:string|object, Terms:[string, ...], MaxPhrase}, ...}&mdash;The string must be found in the type's enumeration. This allows you to override the automatically generated descriptions and terms. If MaxPhrase is specified, the terms are passed through Language.GenerateTerms(string, int).
- Active:script&mdash;C# script with arguments (JObject state)->bool to test whether the field, message, or confirmation is active.
- Validate:script&mdash;C# script with arguments (JObject state, object value)->ValidateResult for validating a field value.
- Define:script&mdash;C# script with arguments (JObject state, Field<JObject> field) for dynamically defining a field.
- Next:script&mdash;C# script with arguments (object value, JObject state) for determining the next step after filling in a field.
- Before:[confirm|message, ...]&mdash;Messages or confirmations before the containing field.
- After:[confirm|message, ...]&mdash;Messages or confirmations after the containing field.
- {Confirm:script|[string, ...], ...templateArgs}&mdash;Use with Before or After to define a confirmation using either C# script with argument (JObject state) or using a set of patterns that will be randomly selected with optional template arguments.
- {Message:script|[string, ...] ...templateArgs}&mdash;Use with Before or After to define a message using either C# script with argument (JObject state) or using a set of patterns that will be randomly selected with optional template arguments.
- Dependencies:[string, ...]&mdash;Fields that this field, message or confirmation depends on.

<span style="color:red">For DateTime:bool, what does bool do?</span>

Scripts can be any C# code you would find in a method body. You can add references using "References" and "Imports". Special global variables include:

- choice&mdash;Internal dispatch for script to execute
- state&mdash;JObject form state bound for all scripts
- ifield&mdash;IField<JObject> to allow reasoning over the current field for all scripts except Message/Confirm prompt builders
- value&mdash;Object value to be validated for Validate
- field&mdash;Field<JObject> to allow dynamically updating a field in Define
- context&mdash;IDialogContext context to allow posting results in OnCompletion

<span style="color:red">for ifield, what does "reasoning" mean?</span>


Fields defined through this class have the same ability to extend or override the definitions programatically as any other field. They can also be localized in the same way.

<span style="color:red">through what class?</span>

The simplest way to define your form is to define everything including any C# code directly in your schema definition. The following example shows the JSON Schema that corresponds to the annotated sandwich bot defined in [Using FormBuilder to Customize a Form](/en-us/csharp/builder/formflow-formbuilder/).

{% highlight json %}
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
{% endhighlight %}


To use your JSON Schema, you use FormBuilderJson which supports the same fluent interface as FormBuilder. The following shows how to use the above JSON Schema.

{% highlight csharp %}
        public static IForm<JObject> BuildJsonForm()
        {
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Microsoft.Bot.Sample.AnnotatedSandwichBot.AnnotatedSandwich.json"))
            {
                var schema = JObject.Parse(new StreamReader(stream).ReadToEnd());
                return new FormBuilderJson(schema)
                    .AddRemainingFields()
                    .Build();
            }

            . . .
        }
{% endhighlight %}

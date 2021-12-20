---
title: .lu file format - Bot Service
description: .lu file format reference
keywords: lu file format, reference, language understanding
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: reference
ms.date: 12/2/2021
monikerRange: 'azure-bot-service-4.0'
---

# .lu file format

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

An .lu file describes a language understanding model.
An .lu file contains Markdown-like, simple text-based definitions for language understanding concepts.
You can use one or more .lu files to train a language model for the natural language understanding (NLU) service or engine that your bot uses, such as [Language Understanding (LUIS)][LUIS] or [Orchestrator][].
The NLU engine you choose may only be able to interpret subset of the elements that an .lu file can describe.

An NLU engine relies on a language model to understand what a user says. The engine creates a language model from sets of training examples, just like any machine learning algorithm. Once trained, the engine uses the model to predict the intent of an _utterance_, generally in the form of one or more _intents_ that represent a task or action the user wants to perform and zero or more _entities_ that represent elements relevant to the intent.

You can use LUIS or Orchestrator with any bot developed using the Bot Framework SDK or [Composer][].

This article is a reference for how to represent language model elements in the .lu file format. For information about how language understanding is used in bots, see [Language Understanding](../v4sdk/bot-builder-concept-luis.md) or [Natural language processing in Composer](/composer/concept-natural-language-processing).

## Defining intents using sample utterances

An intent represents a task or action the user wants to perform, as expressed in a user's utterance. You add intents to your bot to enable it to identify groups of questions or commands that represent the same user intention.

Some examples of intents you might define for a travel bot, with the example utterances that they are defined from:

| Intent       | Example utterances                                                                                                           |
|--------------|------------------------------------------------------------------------------------------------------------------------------|
| BookFlight   | "Book me a flight to Maui next week" <br /> "Fly me to Maui on the 17th"  <br /> "I need a plane ticket next Friday to Maui" |
| Greeting     | "Hi" <br /> "Hello" <br /> "Good afternoon"                                                                                  |
| CheckWeather | "What's the weather like in Maui next week?"                                                                                 |
| None         | "I like cookies"<br /> "Bullfrogs have been recorded jumping over 7 feet"                                                    |

In addition to the intents that you define, **None** is a fallback intent that causes the `unknownIntent` event to fire when no intents can be determined from the users utterance. When using LUIS, the **None** intent is a required intent that you need to create with utterances that are outside of your domain. The utterances associated with your **None** intent should comprise roughly 10% of the total utterances in your .lu file.

Intents with their sample utterances are declared in the following way:

```lu
# <intent-name>
    - <utterance1>
    - <utterance2>
```

`# <intent-name>` describes a new intent definition section. Each line after the intent definition are example utterances that describe that intent using the `- <utterance>` format.

Here is an example .lu file demonstrating these intents and example utterances that capture ways users can express the intent:

```lu
> Use ">" to create a comment in your .lu files.
> Use multiple comment ">" characters to define outlining
> sections in the file to help you organize the content.

>> Primary intents
# BookFlight
- Book me a flight to Maui next week
- Fly me to Maui on the 17th
- I need a plane ticket next Friday to Maui

# Greeting
- Hi
- Hello
- Good afternoon

>> Secondary intents
# CheckWeather
- What's the weather like in Maui next week?
```

> [!NOTE]
>Use the **-**, **+**, or **\*** character to denote lists. Numbered lists are not supported.
>
> Use **>** to create a comment.
>
> Multiple comment ("**>**") characters can also be used to define outlining sections in the .lu file to help you organize the content.  Both [Composer][] and [Bot Framework Adaptive Tools][adaptive-tools] allow you to take advantage of outlining when editing LU files.

For more information about intents and utterances, see [Intents in your LUIS app](/azure/cognitive-services/luis/luis-concept-intent) and [Understand what good utterances are for your LUIS app](/azure/cognitive-services/luis/luis-concept-utterance) in the LUIS documentation.

## Entities

An entity is part of an utterance that can be thought of as a parameter that can be used in the interpretation of an intent. For example, in the utterance _Book a ticket to Maui_, _Maui_ is a FlightDestination
entity.

|Sample user utterance|Intent predicted|Entities extracted|Explanation|
|---------------------|----------------|------------------|-----------|
|Hello, how are you?|Greeting| - |No entities to extract.|
|"Book a flight to Maui"|BookFlight|"Maui"|"FlightDestination" entity is extracted as "Maui".|
|"What's the weather like in Maui next week?"|CheckWeather|"Maui", "next week"|"WeatherLocation" entity is extracted as "Maui" and "DateRange" entity is extracted as "next week".|
|"I want to order a small pizza"|orderPizza|"small"|"Size" entity is extracted as "small".|
|"Schedule a meeting at 1pm with Bob in Distribution"|ScheduleMeeting|"1pm", "Bob"|"MeetingTime" entity is extracted as "1pm" and "Attendees" entity is extracted as "Bob".|

> [!TIP]
> For more information specific to using entities in LUIS, see [Entities in LUIS][entity] in the LUIS documentation.

### Entity definitions

An entity definition defines how to recognize a span in an utterance as an entity that you can then use in your bot. There are many different types of entities including: machine-learned, prebuilt, lists, regular expressions, and patterns.

Entity definitions in .lu files start the entry with the at sign (`@`) followed by the type of entity and entity name:

 ```lu
@ <entity-type> <entity-name>
```

Optionally, each entity can also have [roles](#roles) that identify different uses of the same entity. You can also add [features](#adding-features-to-intents-and-entities) to help do a better job of recognizing entities. The general syntax looks like this:

 ```lu
@ <entity-type> <entity-name> [[hasRole[s]] <comma-separated-list-of-roles>] [hasFeature[s] <comma-separated-list-of-features>]
```

Entities that require a definition, like [list](#list-entity) and [regular expression](#regular-expression-entity) entities, are represented using the following notation:

```lu
@ <entity-type> <entity1-name> = <definition>
```

Additional examples of entity declarations will be demonstrated in the following sections along with the entity types they apply to.

With the exception of [prebuilt entities](#prebuilt-entities), entity names can contain multiple words with spaces. All entity names with spaces must be wrapped in quotes:

 ```lu
@ ml "this is a simple entity" role1, role2 = definition
@ ml 'another simple entity' hasRole role1 hasFeatures feature1, feature2
```

### Entity types

There are several types of entities in LUIS. In the following sections you will learn about these entity types along with related concepts such as [roles](#roles) and [features](#adding-features-to-intents-and-entities) with examples of how to create LU templates that use them.

#### Machine-learned entity

[Machine-learned entities][ml-entity] are entities that enable you to provide examples where you label them in the example utterances. This gives them the context needed to learn from. The machine-learned entity is ideal when identifying data that is not always well formatted but has the same meaning.

The following example demonstrates a machine-learned entity named city (`@ ml city`) and a `bookFlight` intent with sample utterances with your entities labeled:

```lu
> Define the city machine-learned entity
@ ml city

> Define the bookFlight intent with sample utterances that contain the machine-learned entities
# bookFlight
- Book a flight from {@city = Cairo} to {@city = Seattle}
- Get me 2 tickets for a flight to {@city = Bangalore}
- Purchase ticket from {@city = Washington} to {@city = Tampa Bay}
```

When a user says something similar like “_I need a flight booked from London to madrid_”, LUIS will detect the 'bookFlight` intent and extract both London and Madrid as city entities.

[Roles](#roles) are essentially an additional layer of contextual information you can add to your machine-learned entities, that also learn from context. The following example utterance shows the departure and destination roles associated with the city entity:

```lu
- Book a flight from {@city:departure = Cairo} to {@city:destination = Seattle}
```

Machine-learned entities can also be complex where they have a hierarchy of entities related to each other. For example, you can have something like a `pizzaOrder` entity that has the following children entities: quantity, size, crust, toppings, and so on.

You define a child entity by prepending a dash (-) to the at sign (@) and indenting, as the following example demonstrates:

```lu
@ prebuilt number
@ list sizeList
@ list crustList
@ list toppingList

@ ml pizzaOrder
    - @ number Quantity
    - @ sizeList Size
    - @ crustList Crust
    - @ toppingList Topping
```

In the above example, the number entity is a [prebuilt entity](#prebuilt-entities). The remaining entities are all [list entities](#list-entity).

The next example shows a definition of an `address` machine-learned entity, with `fromAddress` and `toAddress` as two roles, as well as children.

```lu
@ list cityList
@ prebuilt number
@ prebuilt geographyV2
@ regex regexZipcode = /[0-9]{5}/
@ ml address hasRoles fromAddress, toAddress
@ address =
    - @ number 'door number'
    - @ ml streetName
    - @ ml location usesFeature geographyV2
        - @ cityList city
        - @ regexZipcode zipcode
```

#### Prebuilt entities

Prebuilt LUIS entities are defined by the system.  This saves you work since they are of high quality and provide normalized values that are easier to use in programs. For example the phrase "one thousand and two" would become the number 1002.  The following LUIS [prebuilt entity][prebuilt-entity] types are supported:

- age
- datetimeV2
- dimension
- email
- geographyV2
- keyPhrase
- money
- number
- ordinal
- ordinalV2
- percentage
- personName
- phonenumber
- temperature
- url
- datetime

Here are examples of how to define prebuilt entities:

```lu
@ prebuilt number 
@ prebuilt datetimeV2
@ prebuilt age
```

#### List entity

[List entities][list-entity] represent a fixed, closed set of related words along with their synonyms. The normalized value is returned when any of the corresponding synonyms are recognized.  They are case-sensitive and extracted based on an exact text match.

The following example shows the syntax for defining a list entity:

```lu
@ list <entityName>  =
    - <normalized-value> :
        - <synonym1>
        - <synonym2>
        - ...
    - <normalized-value> :
        - <synonym1>, <synonym2>, ...
```

Extending the `pizzaOrder` example from the machine-learned entity section, here is an example of lists for the size and crust child entities:

```lu
@ list sizeList = 
    - Extra Large :
        - extra large
        - XL
        - xl
        - huge
        - massive
    - Large:
        - large
        - big
    - Medium :
        - medium
        - regular
    - Small :
        - small
        - smallest
        - individual

@ list crustList = 
    - Stuffed Crust :
        - stuffed crust
        - stufffed crust
    - Thin :
        - thin
        - thin crust
    - Thick :
        - thick
        - thick crust
        - Deep Dish
        - deep dish
```

> [!TIP]
> Since a list entity requires an exact match to be extracted, your results may improve by adding common misspellings. One common causes of misspellings is a result of typing errors such as double letters tripled as in "stufffed crust" in the above example.

When using list entities you should include a value from the list directly in the utterance, you do not need to label list entities although you can still use them as place holders in a [pattern](#patterns). The following example shows an utterance with values from the list:

```lu
- I'd like to order a large pepperoni stuffed crust pizza.
```

#### Regular expression entity

A [regular expression entity][regular-expression-entity] extracts an entity based on a regular expression character pattern you provide. Regular expressions are best for structured text or a predefined sequence of alphanumeric values that are expected in a certain format. For example:

| Entity            | Regular expression       | Example          |
|-------------------|--------------------------|------------------|
|Flight Number      | flight [A-Z]{2} [0-9]{4} | flight AS 1234   |
|Credit Card Number | [0-9]{16}                | 5478789865437632 |

Here's an example of the regular expression entity definitions:

```lu
> Flight Number regular expression entity definition
@ regex flightNumber = /flight [A-Z]{2} [0-9]{4}/

> Credit Card Number regular expression entity definition
@ regex creditCardNumber = /[0-9]{16}/
```

## Roles

A role is a named alias for an entity based on context within an utterance. A role can be used with any prebuilt or custom entity type and are used in both example utterances and patterns.

In the example below the **Location** entity has two roles, `origin` and `destination`:

|Entity   |Role        |Purpose                     |
|---------|------------|----------------------------|
|Location |origin      |Where the plane departs from|
|Location |destination |Where the plane lands       |

Roles in .lu file format can be explicitly or implicitly defined. Explicit role definition follows the notation:

```lu
@ <entityType> <entityName> [hasRole[s]] role1, role2, ...
```

Shown below are the variety of ways you can explicitly define entities and their roles:

```lu
> # ml entity definition with roles
> the following are 4 different approaches to define roles:

@ ml name role1, role2

@ ml name hasRoles role1, role2

@ ml name
@ name hasRoles role1, role2

@ ml name
@ name hasRole role1
@ name hasRole role2
```

You can also implicitly define roles directly in patterns and labeled utterances using the following format:

```lu
{@<entityName>:<roleName>}
```

You can see in the example below how the roles `userName:firstName` and `userName:lastName` are implicitly defined:

```lu
# getUserName
- My first name is {@userName:firstName=vishwac}
- My full name is {@userName:firstName=vishwac} {@userName:lastName=kannan}
- Hello, I'm {@userName:firstName=vishwac}
- {@userName=vishwac} is my name

@ ml userName
```

In [patterns](#patterns), you can use roles using the `{<entityName>:<roleName>}` notation. Here's an example:

```lu
# getUserName
- call me {name:userName}
- I'm {name:userName}
- my name is {name:userName}
```

You can also define multiple roles for an entity in patterns, seen below:

```lu
> Roles can be specified for list entity types as well - in this case fromCity and toCity are added as roles to the 'city' list entity defined further below

# BookFlight
- book flight from {city:fromCity} to {city:toCity}
- [can you] get me a flight from {city:fromCity} to {city:toCity}
- get me a flight to {city:toCity}
- I need to fly from {city:fromCity}

$city:Seattle=
- Seattle
- Tacoma
- SeaTac
- SEA

$city:Portland=
- Portland
- PDX
```

## Patterns

[Patterns][] allow you to cover a large number of examples that should be matched by creating an utterance with place holders for where entities should be found. The patterns are a token level regular expression with place holders for entities. If an utterance has any entity place holders or pattern syntax then it is interpreted as a pattern. Otherwise, it is interpreted as an utterance for training machine learning.

The entity place holders can correspond to entities of any type or they can be defined by the pattern itself, such as when a section in the pattern is an entity that is identified by looking at the surrounding words.

### Pattern syntax

The .lu file format supports the LUIS [Pattern syntax][]. Pattern syntax is a template embedded in an utterance. The template should contain both words and entities you want to match, as well as words and punctuation you want to ignore. The template is not a regular expression.

Entities in patterns are surrounded by braces, {}. Patterns can include entities, and entities with roles. [Pattern.any][pattern-any] is an entity only used in patterns.

| Function | Syntax | Nesting level | Example |
|:-|:-|:-|:-|
| entity | {} - braces | 2 | `Where is form {entity-name}?` |
| optional | [] - square brackets</br>There is a limit of 3 on nesting levels of any combination of optional and grouping | 2 | `The question mark is optional [?]` |
| grouping | () - parentheses | 2 | `is (a \| b)` |
| or | \| - vertical bar (pipe)</br>There is a limit of 2 on the vertical bars (Or) in one group | - | `Where is form ({form-name-short} \| {form-name-long} \| {form-number})` |
| beginning and/or end of utterance | ^ - caret | - | `^begin the utterance`</br>`the utterance is done^`</br>`^strict literal match of entire utterance with {number} entity^` |

See the [Pattern syntax][] article in the LUIS documentation for more information.

The following example shows a definition that would be treated as a pattern with an `alarmTime` entity defined by the pattern:

```lu
# DeleteAlarm
- delete the {alarmTime} alarm
```

The utterance "delete the 7am alarm" would match the pattern and would recognize an `alarmTime` entity of "7am".

By contrast, the following example is a _labeled_ utterance where `alarmTime` is a machine-learned entity since it has a labeled value _7AM_:

```lu
# DeleteAlarm
- delete the {alarmTime=7AM} alarm
```

You cannot mix entity labels and entity place holders in the same utterance, but you can use place holders that correspond to machine-learned entities.

> [!TIP]
> You should understand how your bot responds to user input before adding patterns, because patterns are weighted more heavily than example utterances and will skew confidence. There is no harm adding them in the beginning of your model design, but it's easier to see how each pattern changes the model after the model is tested with utterances.

<!--
## Machine-learning features

A [machine-learning feature][] provides additional context to find a concept contained in your sample utterances. Features will ultimately improve your bots ability to determine the user's intent as well as extract the elements (entities) from the user's utterance that are relevant to that intent. Features are hints, not hard rules and these hints are used in conjunction with the labels to find the relevant data.

The smallest unit that can be a feature is a _token_. A token is an alpha-numeric (A-Z, 0-9) string, it cannot contain any spaces or punctuation. A feature is most often a word or phrase such as people's names a location such as a city or other distinguishing traits such as dates and times. They must be an exact match, so use variations of words, such as:

- plural forms
- verb tenses
- abbreviations
- spellings and misspellings

There are two types of features supported in an .lu template:

1. [Phrase list](#phrase-list)
1. [Model (intent or entity) as a feature](#model-as-a-feature)
-->

## Phrase list

A [phrase list][phrase-list] is a list of words or phrases that help find the concept you're trying to identify. The list is not case-sensitive. Phrase lists have two different purposes:

- **Extend the dictionary**: This is the default when you define a phrase list   and is known as _non-interchangeable_. Multi-word phrases become a feature to the machine learning which requires fewer examples to learn. In this usage, there is no relationship between the members of the phase list.
- **Define synonyms**: Interchangeable phrase lists are used to define synonyms that mean _the same thing_.  This usage helps generalize with fewer examples.  Any phrase in the list results in the same feature to the machine learning. To use this requires specifying `interchangeable` in your phrase list definition (`@ phraselist <Name>(interchangeable)`)

>[!NOTE]
> a _feature_ can be a phrase list or entity that you associate with an intent or entity to emphasize the importance of that feature in accurately detecting user intent. See [Add a phrase list as a feature](#add-a-phrase-list-as-a-feature) for more information.

For additional information about when and how to use phrase lists including typical scenarios they are used for, see [Create a phrase list for a concept][phrase-list].

You define phrase lists using the following notation:

```lu
@ phraselist <Name>
    - <phrase1>
    - <phrase2>
```

Here's an example of a phrase list used to extend the dictionary:

```lu
@ phraseList newTerms=
- surf the sky
- jump on the beam
- blue sky pajamas
```

Phrase lists can also be used to define synonyms by marking them as interchangeable.

```lu
@ phraseList Want(interchangeable) =
    - require, need, desire, know

> You can also break up the phrase list values into a bulleted list
@ phraseList Want(interchangeable) =
    - require
    - need
    - desire
    - know
```

By default, phrase lists are available to all learned intents and entities. There are three availability states:

| Availability State   | Description                                                                                     |
| -------------------- | ----------------------------------------------------------------------------------------------- |
| enabledForAllModels  | (default) When a phrase list is marked as `enabledForAllModels`, it is available to all models whether or not you specifically list it as a feature.         |
| disabledForAllModels | When a phrase list is marked as `disabledForAllModels`, it is only used in a model if it is specifically listed as a feature.                |
| disabled             | When a phrase list is marked as `disabled`, it isn't used anywhere, including any models where it is specifically listed as a feature. This provides an easy means to turn off a phrase list to see how well things work without it. |

Phrase lists are globally available by default, and can also be specifically set using the `enabledForAllModels` keyword:

```lu
@ phraselist abc enabledForAllModels
```

Two examples of setting a phrase list to `disabledForAllModels`:

```lu
@ phraselist abc disabledForAllModels

> You can also use this approach
@ phraselist question(interchangeable) =
    - are you
    - you are

@ question disabledForAllModels
```

When setting a phrase list to `disabled`, it won't be used, even when specifically listed as a feature:

```lu
> phrase list definition, temporarily set to disabled to measure its impact

@ phraselist yourPhraseList disabled

> phrase list as feature to intent, will not be used

@ intent yourIntent usesFeature yourPhraseList
```

Phrase lists can be used as features for specific intents and entities as described in the next section.

## Adding features to intents and entities

Machine learning works by taking features and learning how they relate to the desired intent or entity from example utterances.  By default, features are simply the words that make up utterances. Phrase lists provide a means to group together multiple words into a new feature; this makes the machine learning generalize better from fewer examples. By default, phrase lists are global and apply to all machine-learned models, but you can also tie them to specific intents or entities.  You can also use intents or entities as features to detect other intents as entities.  This provides modularity so that you can build up more complex concepts from simpler building blocks.

>[!NOTE]
> In machine learning, a feature is text that describes a distinguishing trait or attribute of data that your system observes and learns from. Phrase lists, intents, and entities can be used as features as explained in this and the following sections.

Features can be added to any learned intent or entity using the `usesFeature` keyword.

### Add a phrase list as a feature

Phrase lists can be added as a feature to intents or entities.  This helps those specific intents or entities without affecting other intents and entities. Here's an example of how to define a phrase list as a feature to another model:

```lu
> phrase list definition

@ phraseList PLCity(interchangeable) =
    - seattle
    - space needle
    - SEATAC
    - SEA

> phrase list as feature to intent 

@ intent getUserProfileIntent usesFeature PLCity

> phrase list as a feature to an ml entity

@ ml myCity usesFeature PLCity

@ regex regexZipcode = /[0-9]{5}/

> a phrase list is used as a feature in a hierarchal entity

@ ml address fromAddress, toAddress
@ address =
    - @ number 'door number'
    - @ ml streetName
    - @ ml location
        - @ ml city usesFeature PLCity
        - @ regexZipcode zipcode
```

### Add an entity or intent as a feature

Below are examples of how to add intents and entities as a feature with `usesFeature`:

```lu
> entity definition - @ <entityType> <entityName> [<roles>]

@ prebuilt personName
@ prebuilt age

> entity definition with roles

@ ml userName hasRoles fistName, lastName

> add an entity as a feature to another entity

@ userName usesFeature personName

> add an entity as feature to an intent

@ intent getUserNameIntent usesFeature personName

> Intent definition

# getUserNameIntent
- utterances

> multiple entities as a feature to a model

@ intent getUserNameIntent usesFeature age, personName

> intent as a feature to another intent

@ intent getUserProfileIntent usesFeature getUserNameIntent

# getUserProfileIntent
- utterances
```

## Metadata

<a id="model-description"></a>

You can include metadata related to your LUIS application or QnA Maker knowledge base in the .lu file. This will help direct the parser to handle the LU content correctly. Metadata is typically added to the beginning of the .lu file.

Here's how to define configuration information using **> !#**:

```lu
> !# @<property> = <value>
> !# @<scope>.<property> = <value>
> !# @<scope>.<property> = <semicolon-delimited-key-value-pairs>
```

Note that any information explicitly passed in via CLI arguments will override information in the .lu file.

```lu
> LUIS application information
> !# @app.name = my luis application
> !# @app.desc = description of my luis application
> !# @app.versionId = 1.0
> !# @app.culture = en-us
> !# @app.luis_schema_version = 7.0.0
> !# @app.settings.NormalizePunctuation = true
> !# @app.settings.NormalizeWordForm = true
> !# @app.settings.UseAllTrainingData = true
> !# @app.tokenizerVersion = 1.0.0
```

See the table below for a description of the application metadata values used in the above example. For information on app.settings in LUIS, see [App and version settings][luis-metadata] in the LUIS documentation.

| Metadata       | Description                           |
| -------------- | ------------------------------------- |
| Name           | Your application name                 |
| VersionId      | The name of that specific version     |
| Culture        | The language used by your application |
| Schema Version | The LUIS schema is updated anytime a new feature or setting is added in LUIS. Use the schema version number that you used when creating or updating your LUIS model. |

## External references

The sections below detail how to make [local file](#local-file-references) and [URI](#uri-references) references.

### Local file references

References the .lu file. Follow Markdown link syntax. Supported references include:

- Reference to another .lu file via `[link name](<.lu file name>)`. Reference can be an absolute path or a relative path from the containing .lu file.
- Reference to a folder with other .lu files is supported through:
  - `[link name](<.lu file path>*)`: looks for .lu files under the specified absolute or relative path
  - `[link name](<.lu file path>**)`: recursively looks for .lu files under the specified absolute or relative path, including subfolders.
- You can also add references to utterances defined in a specific file under an intent section or as QnA pairs.
  - `[link name](<.lu file path>#<INTENT-NAME>)`: finds all utterances under \<INTENT-NAME> in the .lu file and adds them to the list of utterances where the reference is specified.
  - `[link name](<.lu file path>#<INTENT-NAME>*utterances*)`: finds all utterances (not patterns) under \<INTENT-NAME> in the .lu file and adds them to the list of utterances where the reference is specified.
  - `[link name](<.lu file path>#<INTENT-NAME>*patterns*)`: finds all patterns (not utterances) under \<INTENT-NAME> in the .lu file and adds them to the list of patterns where the reference is specified.
  - `[link name](<.lu file path>#*utterances*)`: finds all utterances in the .lu file and adds them to the list of utterances where the reference is specified.
  - `[link name](<.lu file path>#*patterns*)`: finds all patterns in the .lu file and adds them to the list of utterances where the reference is specified.
  - `[link name](<.lu file path>#*utterancesAndPatterns*)`: finds all utterances and patterns in the .lu file and adds them to the list of utterances where the reference is specified.
  - `[link name](<.qna file path>#$name?)`: finds all alterations from the specific alteration definition in the .qna content and adds them to the list of utterances where the reference is specified.
  - `[link name](<.qna file path>#*alterations*?)`: finds all alterations from the .qna content and adds them to the list of utterances where the reference is specified.
  - `[link name](<.qna file path>#?question-to-find?)`: finds all variation questions from the specific question and adds them to the list of utterances where the reference is specified. Note that any spaces in your question will need to be replaced with the **-** character.
  - `[link name](<.qna file path>#*answers*?)`: finds all answers and adds them to the list of utterances where the reference is specified.

Here's an example of the aforementioned references:

```lu
> You can include references to other .lu files

[All LU files](./all.lu)

> References to other files can have wildcards in them

[en-us](./en-us/*)

> References to other lu files can include subfolders as well.
> /** indicates to the parser to recursively look for .lu files in all subfolders as well.

[all LU files](../**)

> You can include deep references to intents defined in a .lu file in utterances

# None
- [None uttearnces](./all.lu#Help)

> With the above statement, the parser will parse all.lu and extract out all utterances associated with the 'Help' intent and add them under 'None' intent as defined in this file.

> NOTE: This **only** works for utterances as entities that are referenced by the uttearnces in the 'Help' intent will not be brought forward to this .lu file.

# All utterances
> you can use the *utterances* wild card to include all utterances from a lu file. This includes utterances across all intents defined in that .lu file.
- [all.lu](./all.lu#*utterances*)
> you can use the *patterns* wild card to include all patterns from a lu file.
> - [all.lu](./all.lu#*patterns*)
> you can use the *utterancesAndPatterns* wild card to include all utterances and patterns from a lu file.
> - [all.lu](./all.lu#*utterancesAndPatterns*)

> You can include wild cards with deep references to QnA maker questions defined in a .qna file in utterances

# None
- [QnA questions](./*#?)

> With the above statement, the parser will parse **all** .lu files under ./, extract out all questions from QnA pairs in those files and add them under 'None' intent as defined in this file.

> You can include deep references to QnA maker questions defined in a .qna file in utterances

# None
- [QnA questions](./qna1.qna#?)

> With the above statement, the parser will parse qna1.lu and extract out all questions from QnA pairs in that file and add them under 'None' intent as defined in this file.
```

### URI references

Below are examples of how to make URI references:

```lu
> URI to LU resource
[import](http://.../foo.lu)

# intent1
> Ability to pull in specific utterances from an intent
- [import](http://.../foo.lu#None)

# intent2
> Ability to pull in utterances or patterns or both from a specific intent 'None'
- [import](http://..../foo.lu#None*utterances*)
- [import](http://..../bar.lu#None*patterns*)
- [import](http://..../taz.lu#None*utterancesandpatterns*)

# intent3
> Ability to pull in all utterances or patterns or both across all intents
- [import](http://..../foo.lu#*utterances*)
- [import](http://..../bar.lu#*patterns*)
- [import](http://..../taz.lu#*utterancesandpatterns*)
```

## Additional Information

- Read [.qna file format](bot-builder-qna-file-format.md) for more information about .qna files.
- Read [Debug with Adaptive Tools](../bot-service-debug-adaptive-tools.md) to learn how to analyze .lu files.

[luis]: https://luis.ai
[intent]: /azure/cognitive-services/luis/luis-concept-intent
[entity]: /azure/cognitive-services/luis/luis-concept-entity-types
[ml-entity]: /azure/cognitive-services/luis/luis-concept-entity-types#machine-learned-ml-entity
[prebuilt-entity]: /azure/cognitive-services/luis/luis-concept-entity-types#prebuilt-entity
[list-entity]: /azure/cognitive-services/luis/luis-concept-entity-types#list-entity
[entity-composite]: /azure/cognitive-services/luis/reference-entity-composite
[regular-expression-entity]: /azure/cognitive-services/luis/luis-concept-entity-types#regular-expression-entity
[roles]: /azure/cognitive-services/luis/luis-concept-roles
[patterns]: /azure/cognitive-services/luis/luis-concept-patterns
[pattern syntax]: /azure/cognitive-services/luis/reference-pattern-syntax
[pattern-any]: /azure/cognitive-services/luis/luis-concept-entity-types#patternany-entity
[machine-learning-feature]: /azure/cognitive-services/luis/luis-concept-feature
[phrase-list]: /azure/cognitive-services/luis/luis-concept-feature#create-a-phrase-list-for-a-concept
[utterances]: /azure/cognitive-services/luis/luis-concept-utterance
[orchestrator]: /composer/concept-orchestrator
[Composer]: /composer/
[adaptive-tools]: /bot-builder-howto-adaptive-tools.md
[luis-metadata]: /azure/cognitive-services/luis/luis-reference-application-settings

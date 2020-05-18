---
title: Recognizers in adaptive dialogs
description: Providing Language Understanding (LU) to your adaptive dialogs using Recognizers
keywords: bot, recognizers, adaptive dialogs, Language Understanding, LU
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 05/06/2020
---

# Recognizers in adaptive dialogs

_Recognizers_ provide language understanding (LU) to your adaptive dialogs.

Recognizers provide the ability to understand user input. Your adaptive dialog can have one or more recognizers configured to transform user input into constructs your bot can process. For example [language understanding](#luis-recognizer) and [question and answering](#qna-maker-recognizer).

## Prerequisites

* A general understanding of [how bots work][1] in the Bot Framework V4 SDK is helpful.
* A general understanding of adaptive dialogs in the Bot Framework V4 SDK is helpful. For more information, see the [introduction to adaptive dialogs][2] and the [dialogs library][3].

## Introduction to Natural Language Processing in adaptive dialogs

Recognizers, along with [generators][4] constitute the natural language processing (NLP) capabilities in adaptive dialogs. NLP is a technological process that enables computer applications, such as bots, to derive meaning from a users input. To do this it attempts to identify valuable information contained in conversations by interpreting the users needs (intents) and extract valuable information (entities) from a sentence, and respond back in a language the user will understand.

A bot's capabilities would be very minimal without NLP; it is what enables your bot to understand the messages your users send and respond appropriately. When a user sends a message with “Hello”, it is the bot's natural language processing capabilities that enables it to know that the user posted a standard greeting, which in turn allows your bot to leverage its AI capabilities to come up with a proper response. In this case, your bot can respond with a greeting.

Without NLP, your bot can’t meaningfully differentiate between when a user enters “Hello” or “Goodbye” or anything else. To a bot without NLP, “Hello” and “Goodbye” will be no different than any other string of characters grouped together in random order. NLP helps provide context and meaning to text or voice based user inputs so that your bot can come up with the best response.

One of the most significant challenges when it comes to NLP in your bot is the fact that users have a blank slate regarding what they can say to your bot. While you can try to predict what users will and will not say, there are bound to be conversations that you did not anticipate, fortunately the Bot Framework SDK provide the tools you need to continually refine your bots NLP capabilities.

The two primary components of NLP in adaptive dialogs are **recognizers** (language understanding) that processes and interprets _user input_ which is the focus of this article, and [**generators**][4] (language generation) that produces _bot responses_, it is the process of producing meaningful phrases and sentences in the form of natural language. Simply put, it is when your bot responds to a user with human readable language.

> [!TIP]
> While it is common for users to communicate with your bot by typing or speaking a message, the recognizer is the sub-system that you can use to process any form of user input whether it is spoken, typed, clicked (like when responding to [adaptive cards][21]), even other modalities such as a geolocation recognizer or a gaze recognizer can be used. The recognizer layer abstracts away the complexities of processing user input from triggers and actions. That way your triggers and actions do not need to interpret the various types of user inputs but instead let the recognizers do it.

## Language Understanding

**Language understanding** (LU) is the subset of NLP that deals with how the bot handles user inputs and converts them into something that it can understand and respond to intelligently. This process is achieved through setting up recognizers and providing training data to the dialog so that the **intents** and **entities** contained in the users message can be captured. When the recognizer identifies an intent, it emits an `IntentRecognized` event containing that intent and the OnIntent trigger that you defined for that intent will execute and preform the actions contained in that trigger.

## Core LU concepts in adaptive dialogs

### Intents

Intents are how you categorize expected user intentions as expressed in their messages to your bot. You can think of an intent as a representation of the action the user wants to accomplish, the purpose or goal expressed in their input. Such things as booking a flight, paying a bill, or finding a news article. You define and name intents that correspond to these actions. For example any bot might define an intent named _Greeting_, a travel app might create an intent named _BookFlight_. Intents are defined in a language understanding template file (.lu), these files are text files with a .lu extension and generally reside in the same directory, and have the same name as your dialog.  For example your root dialog would contain a language understanding template file named **RootDialog.lu**

Here is an example of a simple .lu file that captures a simple **Greeting** intent with a list of example utterances that capture different ways a user might express this intent. You can use a `-`, `+`, or `*` character to denote lists. Numbered lists are not supported.  

```dos
# Greeting
- Hi
- Hello
- How are you?
```

`#<intent-name>` describes a new intent definition section in your lu template file. Each line after the intent definition are example utterances that describe that intent. You can create multiple intent definitions in your .lu file. Each section is identified by `#<intent-name>` notation. Blank lines are skipped when parsing the file.  

### Utterances

Utterances (_trigger phrases_) are inputs from users and as such may contain a nearly infinite number of potential variations. Since utterances are not always well-formed, you will need to provide several example utterances for specific intents that in effect train bots to recognize intents from different utterances. By doing so, your bots will have some "intelligence" to understand human languages.

> [!TIP]
> Utterances are also known as _trigger phrases_ because they are _phrases_ that are _uttered_ by a user that can cause an `OnIntent` _trigger_ to fire.

### Entities

Entities are a collection of objects, each consisting of data extracted from an utterance that add additional, clarifying information describing the intent such as places, times, and people. Entities and intents are both important pieces of data that are extracted from an utterance. Utterances will general include an intent and may include zero or more entities that provide important details related to the intent.

Entities in the [.lu file format][12] are defined in this format: `{<entityName>=<labelled value>}`, such as `{toCity=seattle}` (EntityName is _toCity_ and labelled value is _seattle_).  For example:

```dos
# BookFlight
- book a flight to {toCity=seattle}
- book a flight from {fromCity=new york} to {toCity=seattle}
```

The example above shows the definition of a `BookFlight` intent with two example utterances and two entity definitions: `toCity` and `fromCity`. When triggered, if your chosen recognizer is able to identify a destination city, the city name will be made available as `@toCity` within the triggered actions or a departure city with `@fromCity` as available entity values. The entity values can be used directly in expressions and LG templates, or stored into a property in [memory][14] for later use.

<!--TODO P1:  Need to discuss recognizers in the context of recognition results. There is intent recognizer, entity recognizer, there can be other types of recognizers as well but a recognizer gets 3 property bags to fill in - intents[], entities[], properties[]. 
https://github.com/MicrosoftDocs/bot-docs-pr/pull/2123#discussion_r423237812
-->

<!--TODO P2: Need to have a more in depth discussion of intents & entities. -->

<!--TODO P2: Need a link to the samples, and in each section specify which sample demonstrates that recognizer. -->

<!--TODO P1: Need to document the EntityRecognizers:
    AgeEntityRecognizer.cs
    ConfirmationEntityRecognizer.cs
    CurrencyEntityRecognizer.cs
    DateTimeEntityRecognizer.cs
    DimensionEntityRecognizer.cs
    EmailEntityRecognizer.cs
    EntityRecognizer.cs
    EntityRecognizerSet.cs
    GuidEntityRecognizer.cs
    HashtagEntityRecognizer.cs
    IpEntityRecognizer.cs
    MentionEntityRecognizer.cs
    NumberEntityRecognizer.cs
    NumberRangeEntityRecognizer.cs
    OrdinalEntityRecognizer.cs
    PercentageEntityRecognizer.cs
    PhoneNumberEntityRecognizer.cs
    RegExEntityRecognizer.cs
    TemperatureEntityRecognizer.cs
    TextEntity.cs
    TextEntityRecognizer.cs
    UrlEntityRecognizer.cs
-->

## Recognizer types

The bot framework SDK provides over a half dozen different recognizers, and the ability to create your own.

Adaptive dialogs support the following recognizers:

* [RegexRecognizer](#regexrecognizer)
* [LUIS recognizer](#luis-recognizer)
* [QnA Maker recognizer](#qna-maker-recognizer)
* [Multi-language recognizer](#multi-language-recognizer)
* [CrossTrained recognizer set](#cross-trained-recognizer-set)
* [RecognizerSet](#recognizer-set)
<!--* [Value recognizer](#value-recognizer)-->

<!--TODO P2: Post R9, refactor the linked content out of this article and into one or more reference-like articles.-->

### RegexRecognizer

The _RegEx recognizer_ gives you the ability to extract intent and entity data from an utterance based on regular expression patterns. 

`RegexRecognizer` consist primarily of:

* `Intents`. The `Intents` object contains a list of `IntentPattern` objects and these `IntentPattern` objects consist of an `Intent` property which is the name of the intent, and a `Pattern` property that contains a regular expression used to parse the utterance to determine intent.
* `Entities`. The `Entities` object contains a list of `EntityRecognizer` objects.  The bot framework SDK defines several `EntityRecognizer` classes to help you determine the entities contained in a users utterance:
  * AgeEntityRecognizer
  * ConfirmationEntityRecognizer
  * CurrencyEntityRecognizer
  * DateTimeEntityRecognizer
  * DimensionEntityRecognizer
  * EmailEntityRecognizer
  * EntityRecognizer
  * EntityRecognizerSet
  * GuidEntityRecognizer
  * HashtagEntityRecognizer
  * IpEntityRecognizer
  * MentionEntityRecognizer
  * NumberEntityRecognizer
  * NumberRangeEntityRecognizer
  * OrdinalEntityRecognizer
  * PercentageEntityRecognizer
  * PhoneNumberEntityRecognizer
  * RegExEntityRecognizer
  * TemperatureEntityRecognizer
  * TextEntity
  * TextEntityRecognizer
  * UrlEntityRecognizer

#### RegexRecognizer Code sample

``` C#
var rootDialog = new AdaptiveDialog("rootDialog")
{
    Recognizer = new RegexRecognizer()
    {
        Intents = new List<IntentPattern>()
        {
            new IntentPattern()
            {
                Intent = "AddIntent",
                Pattern = "(?i)(?:add|create) .*(?:to-do|todo|task)(?: )?(?:named (?<title>.*))?"
            },
            new IntentPattern()
            {
                Intent = "HelpIntent",
                Pattern = "(?i)help"
            },
            new IntentPattern()
            {
                Intent = "CancelIntent",
                Pattern = "(?i)cancel|never mind"
            }
        },
        Entities = new List<EntityRecognizer>()
        {
            new ConfirmationEntityRecognizer(),
            new DateTimeEntityRecognizer(),
            new NumberEntityRecognizer()
        }
    }
}
```

> [!TIP]
>
> * RegexRecognizer will emit a 'None' intent when the input utterance does not match any defined intent. You can create an `OnIntent` trigger with `Intent = "None"` to handle this scenario.
> * RegexRecognizer is useful for testing and quick prototyping. For more sophisticated bots we recommend using the LUIS recognizer.
> * You might find the regular expression language (RegEx) [Quick Reference][7] helpful.

### LUIS recognizer

Language Understanding Intelligent Service (LUIS) is a cloud-based API service that applies custom machine-learning intelligence to a user's conversational, natural language text to predict overall meaning, and pull out relevant, detailed information. The LUIS recognizer enables you to extract intents and entities from a users utterance based on the defined LUIS application, which you train in advance.

To create a LUIS recognizer:

``` C#
var rootDialog = new AdaptiveDialog("rootDialog")
{
    Recognizer = new LuisAdaptiveRecognizer()
    {
        ApplicationId = "<LUIS-APP-ID>",
        EndpointKey = "<ENDPOINT-KEY>",
        Endpoint = "<ENDPOINT-URI>"
    }
}
```

> [!TIP]
> The following information will help you learn more about how to incorporate language understanding (LU) into your bot using LUIS:
>
> * [LUIS.ai][9] is a machine learning-based service that enables you to build natural language capabilities into your bot.
> * [What is LUIS][10]
> * [Create a new LUIS app in the LUIS portal][17]
> * [Language Understanding][11]
> * [.lu file format][12]
> * [Adaptive expressions][13]

### QnA Maker Recognizer

[QnAMaker.ai][18] is one of the [Microsoft Cognitive Services][19] that enables you to create rich question-answer pairs from existing content - documents, URLs, PDFs, and so on. You can use the QnA Maker recognizer to integrate with the service.

> [!NOTE]
> QnA Maker Recognizer will emit a `QnAMatch`event which you can handle with an `OnQnAMatch` trigger.
> The entire QnA Maker response will be available in the `answer` property.

```C#
var adaptiveDialog = new AdaptiveDialog()
{
    var recognizer = new QnAMakerRecognizer()
    {
        HostName = configuration["qna:hostname"],
        EndpointKey = configuration["qna:endpointKey"],
        KnowledgeBaseId = configuration["qna:KnowledgeBaseId"],
    }

    Triggers = new List<OnCondition>()
    {
        new OnConversationUpdateActivity()
        {
            Actions = WelcomeUserAction()
        },

        // With QnA Maker set as a recognizer on a dialog, you can use the OnQnAMatch trigger to render the answer.
        new OnQnAMatch()
        {
            Actions = new List<Dialog>()
            {
                new SendActivity()
                {
                    Activity = new ActivityTemplate("Here's what I have from QnA Maker - ${@answer}"),
                }
            }
        }
    }

    // Add adaptiveDialog to the DialogSet.
    AddDialog(adaptiveDialog);
};
```

### Multi-language recognizer

When building a sophisticated multi-lingual bot, you will typically have one recognizer tied to a specific language and locale. The Multi-language recognizer enables you to easily specify the recognizer to use based on the [locale][8] property on the incoming activity from a user.

``` C#
var rootDialog = new AdaptiveDialog("rootDialog")
{
    Recognizer = new MultiLanguageRecognizer()
    {
        Recognizers = new Dictionary<string, Recognizer>()
        {
            {
                "en",
                new RegexRecognizer()
                {
                    Intents = new List<IntentPattern>()
                    {
                        new IntentPattern()
                        {
                            Intent = "AddIntent",
                            Pattern = "(?i)(?:add|create) .*(?:to-do|todo|task)(?: )?(?:named (?<title>.*))?"
                        },
                        new IntentPattern()
                        {
                            Intent = "HelpIntent",
                            Pattern = "(?i)help"
                        },
                        new IntentPattern()
                        {
                            Intent = "CancelIntent",
                            Pattern = "(?i)cancel|never mind"
                        }
                    },
                    Entities = new List<EntityRecognizer>()
                    {
                        new ConfirmationEntityRecognizer(),
                        new DateTimeEntityRecognizer(),
                        new NumberEntityRecognizer()
                    }
                }
            },
            {
                "fr",
                new LuisAdaptiveRecognizer()
                {
                    ApplicationId = "<LUIS-APP-ID>",
                    EndpointKey = "<ENDPOINT-KEY>",
                    Endpoint = "<ENDPOINT-URI>"
                }
            }
        }
    }
};
```

### Recognizer set

Sometimes you might need to run more than one recognizer on every turn of the conversation. The recognizer set does exactly that. All recognizers are run on each turn of the conversation and the result is a union of all recognition results.

```C#
var adaptiveDialog = new AdaptiveDialog()
{
    Recognizer = new RecognizerSet()
    {
        Recognizers = new List<Recognizer>()
        {
            new ValueRecognizer(),
            new QnAMakerRecognizer()
            {
                KnowledgeBaseId = "<KBID>",
                HostName = "<HostName>",
                EndpointKey = "<Key>"
            }
        }
    }
};
```

### Cross-trained recognizer set

The cross-trained recognizer set compares recognition results from more than one recognizer to decide a winner. Given a collection of recognizers, the cross-trained recognizer will:

* Promote the recognition result of one of the recognizer if all other recognizers defer recognition to a single recognizer. To defer recognition, a recognizer can return the `None` intent or an explicit `DeferToRecognizer_recognizerId` as intent.
* Raises an `OnChooseIntent` event to allow your code to choose which recognition result to use. Each recognizer's results are returned via the `turn.recognized.candidates` property. This enables you to choose the most appropriate result.

```C#
var adaptiveDialog = new AdaptiveDialog()
{
    Recognizer = new CrossTrainedRecognizerSet()
    {
        Recognizers = new List<Recognizer>()
        {
            new LuisAdaptiveRecognizer()
            {
                Id = "Luis-main-dialog",
                ApplicationId = "<LUIS-APP-ID>",
                EndpointKey = "<ENDPOINT-KEY>",
                Endpoint = "<ENDPOINT-URI>"
            },
            new QnAMakerRecognizer()
            {
                Id = "qna-main-dialog",
                KnowledgeBaseId = "<KBID>",
                HostName = "<HostName>",
                EndpointKey = "<Key>"
            }
        }
    }
};
```

## Additional Information

* [What is LUIS][10]
* [Language Understanding][11]
* [.lu file format][12]
* [Adaptive expressions][13]
* [Extract data from utterance text with intents and entities][15]
* [Add natural language understanding (LU) to your bot][16]
* [Add natural language generation (LG) to your bot][4]

[1]:bot-builder-basics.md
[2]:bot-builder-adaptive-dialog-introduction.md
[3]:bot-builder-concept-dialog.md
[4]:bot-builder-concept-adaptive-dialog-generators.md
[6]:https://aka.ms/bot-builder-language-generation-concept
[7]:https://docs.microsoft.com/dotnet/standard/base-types/regular-expression-language-quick-reference
[8]:https://github.com/microsoft/botbuilder/blob/master/specs/botframework-activity/botframework-activity.md#locale
[9]:https://luis.ai
[10]:https://aka.ms/luis-what-is-luis
[11]:https://aka.ms/botbuilder-luis-concept?view=azure-bot-service-4.0
[12]:https://github.com/microsoft/botframework-cli/blob/master/packages/luis/docs/lu-file-format.md
[13]:bot-builder-concept-adaptive-expressions.md
[14]:bot-builder-adaptive-dialog-memory-states.md
[15]:https://aka.ms/luis-concept-data-extraction?tabs=v2
[16]:https://aka.ms/bot-service-add-luis-to-bot
[17]:https://aka.ms/luis-create-new-app-in-luis-portal
[18]:https://qnamaker.ai
[19]:https://azure.microsoft.com/services/cognitive-services/
[20]:https://adaptivecards.io
[21]:https://aka.ms/adaptive-cards-overview

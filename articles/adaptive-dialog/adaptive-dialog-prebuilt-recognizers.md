---
title: Recognizers in adaptive dialogs - reference guide
description: Describing the adaptive dialog prebuilt recognizers
keywords: bot, recognizers, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 06/12/2020
monikerRange: 'azure-bot-service-4.0'
---

# Recognizers in adaptive dialogs - reference guide

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Recognizers enable your bot to understand user input and an adaptive dialog can have one or more recognizers configured. For additional information on recognizers see the [Recognizers in adaptive dialogs](../v4sdk/bot-builder-concept-adaptive-dialog-recognizers.md) article.

<!--
Adaptive dialogs provide support for the following recognizers:

* [RegexRecognizer](#regexrecognizer)
* [LUIS recognizer](#luis-recognizer)
* [QnA Maker recognizer](#qna-maker-recognizer)
* [Multi-language recognizer](#multi-language-recognizer)
* [RecognizerSet](#recognizer-set)
* [Cross-Trained recognizer set](#cross-trained-recognizer-set)
-->

## RegexRecognizer

The _RegEx recognizer_ gives you the ability to extract intent and entity data from an utterance based on regular expression patterns.

`RegexRecognizer` consists primarily of:

* `Intents`. The `Intents` object contains a list of `IntentPattern` objects and these `IntentPattern` objects consist of an `Intent` property which is the name of the intent, and a `Pattern` property that contains a regular expression used to parse the utterance to determine intent.
* `Entities`. The `Entities` object contains a list of `EntityRecognizer` objects.  The Bot Framework SDK defines several `EntityRecognizer` classes to help you determine the entities contained in a users utterance:
  * `AgeEntityRecognizer`
  * `ConfirmationEntityRecognizer`
  * `CurrencyEntityRecognizer`
  * `DateTimeEntityRecognizer`
  * `DimensionEntityRecognizer`
  * `EmailEntityRecognizer`
  * `EntityRecognizer`
  * `EntityRecognizerSet`
  * `GuidEntityRecognizer`
  * `HashtagEntityRecognizer`
  * `IpEntityRecognizer`
  * `MentionEntityRecognizer`
  * `NumberEntityRecognizer`
  * `NumberRangeEntityRecognizer`
  * `OrdinalEntityRecognizer`
  * `PercentageEntityRecognizer`
  * `PhoneNumberEntityRecognizer`
  * `RegExEntityRecognizer`
  * `TemperatureEntityRecognizer`
  * `TextEntity`
  * `TextEntityRecognizer`
  * `UrlEntityRecognizer`

### RegexRecognizer Code sample

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
> * `RegexRecognizer` will emit a 'None' intent when the input utterance does not match any defined intent. You can create an `OnIntent` trigger with `Intent = "None"` to handle this scenario.
> * `RegexRecognizer` is useful for testing and quick prototyping. For more sophisticated bots we recommend using the LUIS recognizer.
> * You might find the regular expression language (RegEx) [Quick Reference][2] helpful.

## LUIS recognizer

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
> * [LUIS.ai][4] is a machine learning-based service that enables you to build natural language capabilities into your bot.
> * [What is LUIS][5]
> * [Create a new LUIS app in the LUIS portal][11]
> * [Language Understanding][6]
> * [.lu file format][7]
> * [Adaptive expressions][8]

## QnA Maker Recognizer

[QnAMaker.ai][12] is one of the [Microsoft Cognitive Services][13] that enables you to create rich question-answer pairs from existing content - documents, URLs, PDFs, and so on. You can use the QnA Maker recognizer to integrate with the service.

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

## Multi-language recognizer

When building a sophisticated multi-lingual bot, you will typically have one recognizer tied to a specific language and locale. The Multi-language recognizer enables you to easily specify the recognizer to use based on the [locale][3] property on the incoming activity from a user.

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

## Recognizer set

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

## Cross-trained recognizer set

The cross-trained recognizer set compares recognition results from more than one recognizer to decide a winner. Given a collection of recognizers, the cross-trained recognizer will:

* Promote the recognition result of one of the recognizer if all other recognizers defer recognition to a single recognizer. To defer recognition, a recognizer can return the `None` intent or an explicit `DeferToRecognizer_recognizerId` as intent.
* Raise an `OnChooseIntent` event to allow your code to choose which recognition result to use. Each recognizer's results are returned via the `turn.recognized.candidates` property. This enables you to choose the most appropriate result.

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

### Cross training your LUIS and QnA models

To gain the full benefits of the cross-trained recognizer set you will need to [cross train][cross-train-concepts] your `.lu` and `.qna` files. The Bot Framework Command Line Interface (BF CLI) tool provides a command to automate this process, the [luis:cross-train][bf-luiscross-train] and [qnamaker:cross-train][qnamaker-cross-train] commands. Running the cross-train command will update all `.lu` and `.qna` files in the directory and sub-directories specified.

> [!TIP]
>
> To create the cross-trained files, both `.lu`  and `.qna`, you can use _either_ the BF CLI `luis:cross-train` or `qnamaker:cross-train` command. You do not need to run both commands since they both do the same thing. The following demonstrates using the `luis:cross-train` command:

``` cli
bf luis:cross-train -i <input-folder-name> -o <output-file-name> --config <cross-train-configuration-file>
```

For an end to end example of cross training your bot, see the how to article [Create a bot cross trained to use both LUIS and QnA Maker recognizers][howto-cross-train].

### luis:cross-train required parameters

- `--in`: The directory, including sub-directories, that will be searched for both `.lu` and `.qna` files.
- `--out`: The directory to save the new cross-trained `.lu` and `.qna` output files. This is the directory to which you will point the `luis:build` command's `--in` option.
- `--config`: This points to the cross-train configuration file, a JSON file that is necessary for the command to work. 

#### The cross-train configuration file

The cross-train configuration file

The following is the cross-train configuration file for the [todo bot with LUIS and QnA Maker][cs-sample-todo-bot] sample:

```json
{
    // list each .lu file including variations per lang x locale.
    // Lang x locale is denoted using 4 letter code. e.g. it-it, fr-fr
    // Paths can either be absolute (full) paths or paths relative to this config file.
    "./RootDialog/RootDialog.lu": {
        // indicate if this is an .lu file for a root dialog.
        "rootDialog": true,
        // list of triggers within that dialog
        "triggers": {
            // Key is name of intent within the .lu file (in this case RootDialog.lu)
            // Value is the path to the child dialog's .lu file.
            "AddItem": "./AddToDoDialog/AddToDoDialog.lu",
            "DeleteItem": "./DeleteToDoDialog/DeleteToDoDialog.lu",
            "ViewItem": "./ViewToDoDialog/ViewToDoDialog.lu",
            "GetUserProfile": "./GetUserProfileDialog/GetUserProfileDialog.lu"
        }
    }
}
```

In the triggers section of the cross-train configuration file, list out each intent in the root dialog along with the `.lu` file it points to. You only need to list the `.lu` files and it will discover the `.qna` files as long as they are in the same directory and have the same filename, except with the qna file extension, for example _AddToDoDialog.qna_.

> [!TIP]
>
> If your bot only contains LUIS models, and no QnA Maker models, you can cross train just your LUIS models. For more information on cross training your LUIS models see [LUIS to LUIS Cross training][luis-to-luis-cross-training]

## Additional Information

* [What is LUIS][5]
* [Language Understanding][6]
* [.lu file format][7]
* [Adaptive expressions][8]
* [Extract data from utterance text with intents and entities][9]
* [Add natural language understanding (LU) to your bot][10]
* [Add natural language generation (LG) to your bot][1]

<!-- Footnote-style links -->
[1]:../v4sdk/bot-builder-concept-adaptive-dialog-generators.md
[2]:https://aka.ms/regular-expression-language-reference
[3]:https://github.com/microsoft/botbuilder/blob/master/specs/botframework-activity/botframework-activity.md#locale
[4]:https://luis.ai
[5]:https://aka.ms/luis-what-is-luis
[6]:https://aka.ms/botbuilder-luis-concept?view=azure-bot-service-4.0
[7]:../file-format/bot-builder-lu-file-format.md
[8]:../v4sdk/bot-builder-concept-adaptive-expressions.md
[9]:https://aka.ms/luis-concept-data-extraction?tabs=v2
[10]:https://aka.ms/bot-service-add-luis-to-bot
[11]:https://aka.ms/luis-create-new-app-in-luis-portal
[12]:https://qnamaker.ai
[13]:https://azure.microsoft.com/services/cognitive-services/
[cross-train-concepts]: ../v4sdk/bot-builder-concept-cross-train.md
[luis-to-luis-cross-training]: ../v4sdk/bot-builder-concept-cross-train.md#luis-to-luis-cross-training
[qnamaker-cross-train]: https://aka.ms/botframework-cli#bf-qnamakercross-train
[bf-luiscross-train]: https://aka.ms/botframework-cli#bf-luiscross-train
[cs-sample-todo-bot]: https://aka.ms/csharp-adaptive-dialog-08-todo-bot-luis-qnamaker-sample
[howto-cross-train]: ../v4sdk/bot-builder-howto-cross-train.md

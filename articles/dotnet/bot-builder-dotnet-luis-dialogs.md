---
title: Recognize intents and entities with LUIS  | Microsoft Docs
description: Learn how to enable your bot to understand natural language by using LUIS dialogs in the Bot Builder SDK for .NET.
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 09/27/2017
---

# Recognize intents and entities with LUIS using a prebuilt domain 
This article uses the example of a bot for taking notes, to demonstrate how Language Understanding Intelligent Service ([LUIS][LUIS]) helps your bot respond appropriately to natural language input. A bot detects what a user wants to do by identifying their **intent**. This intent is determined from spoken or textual input, or **utterances**. The intent maps utterances to actions that the bot takes. For example, a note-taking bot recognizes a `Notes.Create` intent to invoke the functionality for creating a note. A bot may also need to extract **entities**, which are important words in utterances. In the example of a note-taking bot, the `Notes.Title` entity identifies the title of each note.

## Create your LUIS app
The LUIS app, which is the web service you configure at [www.luis.ai][LUIS] to provide the intents and entities to the bot. In this example, the LUIS app makes use of the **Notes** <a href="https://docs.microsoft.com/en-us/azure/cognitive-services/LUIS/luis-how-to-use-prebuilt-domains" target="_blank">prebuilt domain</a>, which is a collection of ready-to-use intents and entities. To create the LUIS app, follow these steps:

1.	Log in to [www.luis.ai][LUIS] using your Cognitive Services API account. If you don't have an account, you can create a free account in the [Azure portal](https://ms.portal.azure.com). 
2.	In the **My Apps** page, click **New App**, enter a name like Notes in the **Name** field, and choose **Bootstrap Key** in the **Key to use** field. 
3.	In the **Intents** page, click **Add prebuilt domain intents** and select **Notes.Create**, **Notes.Delete** and **Notes.ReadAloud**.
4.	In the **Intents** page, click on the **None** intent. This intent is meant for utterances that don’t correspond to any other intents. Enter an example of an utterance unrelated to notes, like “Turn off the lights.”
5.	In the **Entities** page, click **Add prebuilt domain entities** and select **Notes.Title**.
6.	In the **Train & Test** page, train your app.
7.	In the **Publish** page, click **Publish**. After successful publish, copy the **Endpoint URL** from the **Publish App** page, to use later in your bot’s code. The URL has a format similar to this example: `https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/3889f7d0-9501-45c8-be5f-8635975eea8b?subscription-key=67073e45132a459db515ca04cea325d3&timezoneOffset=0&verbose=true&q=`

> [!TIP] 
> You can also create the LUIS app by importing the [Notes sample JSON][NotesSampleJSON]. In [www.luis.ai][LUIS], click **Import App** in the **My Apps** page and select the JSON file to import.

## Create a note-taking bot integrated with the LUIS app
To create a bot that uses the LUIS app, you can first start with the sample bot that you create according to the steps in [Create a bot with the Bot Builder SDK for .NET](https://docs.microsoft.com/en-us/bot-framework/dotnet/bot-builder-dotnet-quickstart), and edit the code to correspond to the examples in this article.

> [!TIP] 
> You can also find the sample code described in this article at [Notes bot sample][NotesSample].


## How LUIS passes intents and entities to your bot
The following diagram shows the sequence of events that happen after the bot receives an utterance from the user. First, the bot passes the utterance to the LUIS app and gets a JSON result from LUIS that contains intents and entities. Next, your bot automatically invokes any matching handler in a [LuisDialog](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.dialogs.luisdialog-1?view=botbuilder-3.11.0). The intent handler is associated with the high-scoring intent in the LUIS result by using the [LuisIntent attribute](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.dialogs.luisintentattribute?view=botbuilder-3.11.0). The full details of the match, including the list of intents and entities that LUIS detected, are passed as a [LuisResult](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.luis.models.luisresult?view=botbuilder-3.11.0) to the `result` parameter of the matching handler.

<p align=center>
<img alt="How LUIS passes intents and entities to your bot" src="~/media/bot-builder-dotnet-use-luis/bot-builder-dotnet-luis-message-flow-bot-code-notes.png">
</p>

## Create a class that derives from LuisDialog

To create a [dialog](bot-builder-dotnet-dialogs.md) that uses LUIS, first create a class that derives from [LuisDialog](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.dialogs.luisdialog-1?view=botbuilder-3.11.0) and 
specify the [LuisModel attribute](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.luis.luismodelattribute?view=botbuilder-3.11.0). 
To populate the `modelID` and `subscriptionKey` parameters for the `LuisModel` attribute, use 
the `id` and `subscription-key` attribute values from your LUIS app's endpoint URL. 

The `domain` parameter is determined by the Azure region to which your LUIS app is published. 
If not supplied, it defaults to `westus.api.cognitive.microsoft.com`.
See [Regions and keys](#regions-and-keys) for more information.

[!code-csharp[Class definition](../includes/code/dotnet-luis-dialogs.cs#classDefinitionNotes)]

## Create methods to handle intents

Within the class, create the methods that execute when your LUIS model matches a user's utterance to intent. 
To designate the method that runs when a specific intent is matched, specify the `LuisIntent` attribute. 

This code example defines the method that executes when the `Note.Delete` intent is matched.

[!code-csharp[delete note handler](../includes/code/dotnet-luis-dialogs.cs#deleteNoteHandler)]

In this example, if the LUIS app detects the title of the note the user wants to delete, and finds it in the list of notes, 
it removes it from the list of notes. Otherwise it prompts the user for the name of the note to delete.

> [!NOTE]
> The LUIS app you created is meant to be a starting point for training, and might not be able to detect the title of notes at first. A LUIS app learns from example, so teach it to better recognize the title entity by giving it more example utterances to learn from. You can retrain your LUIS app without any modification to your bot's code. See [Add example utterances](https://docs.microsoft.com/en-us/azure/cognitive-services/LUIS/add-example-utterances) and [train and test your LUIS app](https://docs.microsoft.com/en-us/azure/cognitive-services/LUIS/train-test).

The `TryFindNote` method that `DeleteNote` calls inspects the `result` parameter from LUIS to see if the LUIS app detected a title entity. It then searches for a note with that title. 

[!code-csharp[delete note handler](../includes/code/dotnet-luis-dialogs.cs#tryFindNote)]


## Notes dialog implementation

This code example shows the full dialog implementation for the Notes bot, which also includes methods for handling the Note.Create and Note.ReadAloud intents, as well as a default intent handler which can handle the None intent, or any other intents the LUIS app may detect. 

[!code-csharp[Full LUIS dialog example](../includes/code/dotnet-luis-dialogs.cs#fullNotesExample)]

### Update the Post method

To use the SimpleNoteDialog that you implemented, update the `Post` method in your `MessageController` class to reference it.

[!code-csharp[Full LUIS dialog example](../includes/code/dotnet-luis-dialogs.cs#MessagesControllerPost)]

## Try the bot

You can run the bot using the Bot Framework Emulator and tell it to create a note.
<p align=center>
<img alt="Conversation for creating a note" src="~/media/bot-builder-dotnet-use-luis/dotnet-notes-sample-emulator.png">
</p>

> [!TIP]
> If you find that your bot doesn't always recognize the correct intent or entities, improve your LUIS app's performance by giving it more example utterances to train it. You can retrain your LUIS app without any modification to your bot's code. See [Add example utterances](https://docs.microsoft.com/en-us/azure/cognitive-services/LUIS/add-example-utterances) and [train and test your LUIS app](https://docs.microsoft.com/en-us/azure/cognitive-services/LUIS/train-test).


## Regions and keys
The region to which you publish your LUIS app must correspond to the region or location you specify in the Azure portal when you create a key. To publish a LUIS app to more than one region, you need at least one key per region. LUIS apps created at <a href="https://www.luis.ai" target="_blank">https://www.luis.ai</a> can be published to endpoints in the following regions:

 Azure region   |   Endpoint URL format   |   
------|------|
West US     |   `https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/YOUR-APP-ID?subscription-key=YOUR-SUBSCRIPTION-KEY`  |
East US 2    |   `https://eastus2.api.cognitive.microsoft.com/luis/v2.0/apps/YOUR-APP-ID?subscription-key=YOUR-SUBSCRIPTION-KEY`   |
West Central US     |   `https://westcentralus.api.cognitive.microsoft.com/luis/v2.0/apps/YOUR-APP-ID?subscription-key=YOUR-SUBSCRIPTION-KEY`   |
Southeast Asia     |   `https://southeastasia.api.cognitive.microsoft.com/luis/v2.0/apps/YOUR-APP-ID?subscription-key=YOUR-SUBSCRIPTION-KEY`   |

To publish to the European regions, you can create LUIS apps at <a href="https://eu.luis.ai" target="_blank">https://eu.luis.ai</a>.  

 Azure region   |   Endpoint URL format   |   
------|------|
West Europe     | `https://westeurope.api.cognitive.microsoft.com/luis/v2.0/apps/YOUR-APP-ID?subscription-key=YOUR-SUBSCRIPTION-KEY` |

## Data privacy
To keep LUIS from saving the user's utterances, set the `log` parameter to false in the [LuisModel attribute](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.luis.luismodelattribute?view=botbuilder-3.11.0).

[!code-csharp[Class definition](../includes/code/dotnet-luis-dialogs.cs#classDefinitionLogFalse)]

## Next steps

From trying the bot, you can see how tasks are invoked by a LUIS intent. However, this simple example doesn't allow for interruption of the currently active dialog. Allowing and handling interruptions like "help" or "cancel" is a flexible design that accounts for what users really do. Learn more about using scorable dialogs so that your dialogs can handle interruptions.

> [!div class="nextstepaction"]
> [Global message handlers using scorables](bot-builder-dotnet-scorable-dialogs.md)

## Additional resources

- [Dialogs](bot-builder-dotnet-dialogs.md)
- [Manage conversation flow with dialogs](bot-builder-dotnet-manage-conversation-flow.md)
- [Language understanding](../cognitive-services-bot-intelligence-overview.md#language-understanding)
- <a href="https://www.luis.ai" target="_blank">LUIS</a>
- <a href="https://docs.microsoft.com/en-us/dotnet/api/?view=botbuilder-3.11.0" target="_blank">Bot Builder SDK for .NET Reference</a>

[LUIS]: https://www.luis.ai/
[NotesSample]: https://github.com/Microsoft/BotFramework-Samples/tree/master/docs-samples/CSharp/Simple-LUIS-Notes-Sample
[NotesSampleJSON]: https://github.com/Microsoft/BotFramework-Samples/blob/master/docs-samples/CSharp/Simple-LUIS-Notes-Sample/Notes.json
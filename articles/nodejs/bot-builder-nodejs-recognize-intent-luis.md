---
title: Recognize intents and entities with LUIS  | Microsoft Docs
description: Integrate a bot with LUIS to detect the user's intent and respond appropriately by triggering dialogs using the Bot Builder SDK for Node.js. 
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 08/04/2017
---

# Recognize intents and entities with LUIS 

This article uses the example of a bot for taking notes, to demonstrate how Language Understanding Intelligent Service ([LUIS][LUIS]) helps your bot respond appropriately to natural language input. A bot detects what a user wants to do by identifying their **intent**. This intent is determined from spoken or textual input, or **utterances**. The intent maps utterances to actions that the bot takes, such as invoking a dialog. A bot may also need to extract **entities**, which are important words in utterances. Sometimes entities are required to fulfill an intent. In the example of a note-taking bot, the `Notes.Title` entity identifies the title of each note.

## Create your LUIS app
To create the LUIS app, which is the web service you configure at [www.luis.ai][LUIS] to provide the intents and entities to the bot, do the following steps:

1.	Log in to [www.luis.ai][LUIS] using your Cognitive Services API account. If you don't have an account, you can create a free account in the [Azure portal](https://ms.portal.azure.com). 
2.	In the **My Apps** page, click **New App**, enter a name like Notes in the **Name** field, and choose **Bootstrap Key** in the **Key to use** field. 
3.	In the **Intents** page, click **Add prebuilt domain intents** and select **Notes.Create**, **Notes.Delete** and **Notes.ReadAloud**.
4.	In the **Intents** page, click on the **None** intent. This intent is meant for utterances that don’t correspond to any other intents. Enter an example of an utterance unrelated to weather, like “Turn off the lights”
5.	In the **Entities** page, click **Add prebuilt domain entities** and select **Notes.Title**.
6.	In the **Train & Test** page, train your app.
7.	In the **Publish** page, click **Publish**. After successful publish, copy the **Endpoint URL** from the **Publish App** page, to use later in your bot’s code. The URL has a format similar to this example: `https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/3889f7d0-9501-45c8-be5f-8635975eea8b?subscription-key=67073e45132a459db515ca04cea325d3&timezoneOffset=0&verbose=true&q=`

## Create a note-taking bot integrated with the LUIS app
To create a bot that uses the LUIS app, you can first start with the sample code in [Create a bot with Node.js](https://docs.microsoft.com/en-us/bot-framework/nodejs/bot-builder-nodejs-quickstart#create-your-bot), and add code according to the instructions in the following sections.

> [!TIP] 
> You can also find the sample code described in this article at [Notes bot sample][NotesSample].

## How LUIS passes intents and entities to your bot
The following diagram shows the sequence of events that happen after the bot receives an utterance from the user. First, the bot passes the utterance to the LUIS app and gets a JSON result from LUIS that contains intents and entities. Next, your bot automatically invokes any matching [Dialog][Dialog] that your bot associates with the high-scoring intent in the LUIS result. The full details of the match, including the list of intents and entities that LUIS detected, are passed to the `args` parameter of the matching dialog.

<p align=center>
<img alt="How LUIS passes intents and entities to your bot" src="~/media/bot-builder-nodejs-use-luis/bot-builder-nodejs-luis-message-flow-bot-code-notes.png">
</p>

### Create the bot 
Create the bot that communicates with the Bot Framework Connector service by instantiating a [UniversalBot][UniversalBot] object. The constructor takes a second parameter for a default message handler. This message handler sends a generic help message about the functionality that the note-taking bot provides, and initializes the `session.userData.notes` object for storing notes. You use `session.userData` so the notes are persisted for the user. Edit the code that creates the bot, so that the constructor looks like the following code:

[!code-js[Define a default message handler in the constructor to your bot (JavaScript)](../includes/code/node-basicNote.js#Constructor)]

### Add a LuisRecognizer
The [LuisRecognizer][LuisRecognizer] class calls the LUIS app. You initialize a **LuisRecognizer** using the **Endpoint URL** of the LUIS app that you copied from the **Publish App** page. 

After you create the `UniversalBot`, add code to create the `LuisRecognizer` and add it to the bot: 

[!code-js[Add the recognizer to the bot using the LUIS app URL (JavaScript)](../includes/code/node-basicNote.js#Recognizer)]

### Add dialogs
Now that the notes recognizer is set up to point to the LUIS app, you can add code for the dialogs.  The [matches][matches] option on the [triggerAction][triggerAction] attached to the dialog specifies the name of the intent. The recognizer runs each time the bot receives an utterance from the user. If the highest scoring intent that it detects matches a `triggerAction` bound to a dialog, the bot invokes that dialog.

#### Add the CreateNote dialog
Any entities in the utterance are passed to the dialog using the `args` parameter. The first step of the [waterfall][waterfall] calls [EntityRecognizer.findEntity][EntityRecognizer_findEntity] to get the title of the note from any `Note.Title` entities in the LUIS response. If the LUIS app didn't detect a `Note.Title` entity, the bot prompts the user for the name of the note. The second step of the waterfall prompts for the text to include in the note. Once the bot has the text of the note, the third step uses [session.userData][session_userData] to save the note in a `notes` object, using the title as the key. For more information on `session.UserData` see [Manage state data](./bot-builder-nodejs-state.md). 


The following code for a CreateNote dialog handles the `Note.Create` intent.

[!code-js[Add the CreateNote dialog, which is bound to the Note.Create intent (JavaScript)](../includes/code/node-basicNote.js#CreateNoteDialog)]

If the LUIS app detects an intent that interrupts the `CreateNote` dialog, the [confirmPrompt][confirmPrompt] option on the dialog's `triggerAction` provides a prompt to confirm the interruption. For example, if the bot says "What would you like to call your note?", and the user replies "Actually, I want to delete a note instead", the bot prompts the user using the `confirmPrompt` message.

#### Add the DeleteNote dialog
In the `DeleteNote` dialog, the `triggerAction` matches the `Note.Delete` intent. As in the `CreateNote` dialog, the bot examines the `args` parameter for a title. If no title is detected, the bot prompts the user. The title is used to look up the note to delete from `session.userData.notes`. 

[!code-js[Add the DeleteNote dialog, which is bound to the Note.Delete intent (JavaScript)](../includes/code/node-basicNote.js#DeleteNoteDialog)]

The `DeleteNote` dialog uses the `noteCount` function to determine whether the `notes` object contains notes.

[!code-js[Add a helper function that returns the number of notes (JavaScript)](../includes/code/node-basicNote.js#CountNotesHelper)]

#### Add the ReadNote dialog

For reading a note, the `triggerAction` matches the `Note.ReadAloud` intent. The `session.userData.notes` object is passed as the third argument to `builder.Prompts.choice`, so that the prompt displays a list of notes to the user.

[!code-js[Add the ReadNote dialog, which is bound to the Note.ReadAloud intent (JavaScript)](../includes/code/node-basicNote.js#ReadNoteDialog)]

## Try the bot

You can run the bot using the Bot Framework Emulator and tell it to create a note.
<p align=center>
<img alt="Conversation for creating a note" src="~/media/bot-builder-nodejs-use-luis/bot-builder-nodejs-use-luis-create-note-output.png">
</p>


The use of `triggerAction` to match intents means that the bot can detect and react to intents for every utterance, even utterances that occur in the middle of the steps of a dialog. If the user is in the `CreateNote` dialog, but asks to create a different note before the dialog's conversation flow is complete, the bot detects the second `Note.Create` intent, and prompts the user to verify the interruption. See [Managing conversation flow](bot-builder-nodejs-manage-conversation-flow.md) for more information on how `triggerAction` works with dialogs.


<p align=center>
<img alt="Conversation for creating a note, interrupted by another note request" src="~/media/bot-builder-nodejs-use-luis/bot-builder-nodejs-use-luis-create-note-interruption.png">
</p>

> [!TIP]
> A LUIS app learns from example, so you can improve its performance by giving it more example utterances to train it. You can retrain your LUIS app without any modification to your bot's code. See [Add example utterances](https://docs.microsoft.com/en-us/azure/cognitive-services/LUIS/add-example-utterances) and [train and test your LUIS app](https://docs.microsoft.com/en-us/azure/cognitive-services/LUIS/train-test).

## Next steps

From trying the bot, you can see that the recognizer can trigger interruption of the currently active dialog. Allowing and handling interruptions is a flexible design that accounts for what users really do. Learn more about the various actions you can associate with a recognized intent.

> [!div class="nextstepaction"]
> [Handle user actions](bot-builder-nodejs-dialog-actions.md)


[LUIS]: https://www.luis.ai/

[NotesSample]: https://aka.ms/notes-bot-sample

[triggerAction]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialog.html#triggeraction

[confirmPrompt]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.itriggeractionoptions.html#confirmprompt

[waterfall]: bot-builder-nodejs-dialog-manage-conversation-flow.md#manage-conversation-flow-with-a-waterfall

[session_userData]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session.html#userdata

[EntityRecognizer_findEntity]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.entityrecognizer.html#findentity

[matches]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.itriggeractionoptions.html#matches

[LUISAzureDocs]: https://docs.microsoft.com/en-us/azure/cognitive-services/LUIS/Home

[Dialog]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialog.html

[IntentRecognizerSetOptions]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iintentrecognizersetoptions.html

[LuisRecognizer]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.luisrecognizer

[LUISConcepts]: https://docs.botframework.com/en-us/node/builder/guides/understanding-natural-language/

[DisambiguationSample]: https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/feature-onDisambiguateRoute

[IDisambiguateRouteHandler]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.idisambiguateroutehandler.html

[RegExpRecognizer]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.regexprecognizer.html

[AlarmBot]: https://github.com/Microsoft/BotBuilder/blob/master/Node/examples/basics-naturalLanguage/app.js

[LUISBotSample]: https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/intelligence-LUIS

[UniversalBot]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot.html

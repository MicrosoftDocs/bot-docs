---
title: Enterprise Bot Customization | Microsoft Docs
description: Learn how to customise the Bot created by the Enterprise Template
author: darrenj
ms.author: darrenj
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 09/18/2018
monikerRange: 'azure-bot-service-4.0'
---
# Enterprise Bot Template - Customize your Bot

> [!NOTE]
> This topics applies to v4 version of the SDK. 

## .NET
After you have deployed the Advanced Bot Template following the instruction [here](bot-builder-enterprise-template-deployment.md) and have tested to verify that it works end-to-end,
you can then easily customize the bot based on your scenario and needs. The goal of the template is to provide a solid foundation upon which to build your conversational experience.

## Project Structure

The Folder structure of your Bot follows this structure as shown below and represents our recommended best practice for structuring your Bot project and processing incoming messages.

    | - IPABot.bot          // The .bot file containing all of your Bot configuration including dependencies
    | - README.md           // README file containing links to documentation
    | - Program.cs          // Default Program.cs file
    | - Startup.cs          // Core Bot Initialisation including Bot Configuration LUIS, Dispatcher, etc. 
    | - <BOTNAME>State.cs   // The Root State class for your Bot
    | - appsettings.json    // References above .bot file for Configuration information. App Insights key
    | - CognitiveModels     
        | - LUIS            // .LU file containing base conversational intents (Greeting, Help, Cancel)
        | - QnA             // .LU file containing example QnA items
    | - DeploymentScripts   // msbot clone recipe for depoyment
    | - Dialogs             // All Bot dialogs sit under this folder
        | - Main            // Root Dialog for all messages
            | - MainDialog.cs       // Dialog Logic
            | - MainResponses.cs    // Dialog responses
            | - Resources           // Adaptive Card JSON, Resource File
        | - Onboarding
            | - OnboardingDialog.cs       // Onboarding dialog Logic
            | - OnboardingResponses.cs    // Onboarding dialog responses
            | - OnboardingState.cs        // Localised dialog state
            | - Resources                 Resource File
        | - Cancel
        | - Escalate
        | - Signin
    | - Middleware          // Telemetry, Content Moderator, Regex Middlwware
    | - ServiceClients      // SDK libraries, example GraphClient provided for Auth example
   
## Update Introduction Message

The Introduction message makes use of an [Adaptive Card](https://www.adaptivecards.io). To change this you can find the JSON file within the Dialogs/Main/Resources folder called ```Intro.json```. An [Adaptive Card visualizer](http://adaptivecards.io/visualizer) is available on the Adaptive Cards site, you can use this to open the above JSON file and explore changes to suit your Bot's requirements.

## Update Bot Responses

Each Dialog within the project has a set of responses that are stored within supporting resource files. These can be found within the Resources folder of each Dialog.

Changing responses as shown in the Visual Studio resource editor following a compile will adjust how the Bot responds.

![Customizing Bot Responses](media/enterprise-template/EnterpriseBot-CustomisingResponses.png)

This approach supports multi-lingual responses using the standard resource file localisation approach. Further information can be found [here.](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-2.1)### Update Knowledge Base 

## Update Language Understanding (LUIS) Models

If you make changes to an existing LUIS model within the template it's important to ensure the Dispatcher model is updated with the change and then the LuisGen generated files.

- First, perform the updates to the LUIS models through LuDown/LUIS CLI tools or through the LUIS portal
- Refresh the dispatch model to reflect the LUIS model changes through the following command
```shell
    dispatch refresh -bot "YOURBOT.bot" -secret YOURSECRET
```
- Once complete ensure you run luisgen against the LUIS models to update the `Dispatcher.cs` and `General.cs` folders within your Dialogs\Shared\Resources folder.

## Adding a new LUIS model

In scenarios where you wish to add a new LUIS model to your project you need to update the Bot configuration and Dispatcher to ensure it is aware of the additional model. 

- Create your LUIS model through LuDown/LUIS CLI tools or through the LUIS portal
- Ensure this model is added to your .bot file through the `msbot connect luis` command
- Add this new LUIS model to your Dispatcher through the following command
`
```shell
    dispatch add  -t luis -id YOUR_LUIS_APPID -bot "YOURBOT.bot" -secret YOURSECRET
```
- Refresh the dispatch model to reflect the LUIS model changes through the following command
```shell
    dispatch refresh -bot "YOURBOT.bot" -secret YOURSECRET
```

## Update Knowledge Base 

To update the QnaMaker knowledgebase you can leverage the [QnAMaker CLI](https://github.com/Microsoft/botbuilder-tools/blob/master/packages/QnAMaker) or browse to the QnAMaker [website](https://www.qnamaker.ai) and click on My Knowledge Bases to find your knowledgebase.

Once updated you need to refresh the dispatch model to reflect the QnAMaker changes through the following command
```shell
    dispatch refresh -bot "YOURBOT.bot" -secret YOURSECRET
```

## Adding a new dialog 

To add a new Dialog to your Bot you need to first create a new Folder under Dialogs and ensure this class derives from `EnterpriseDialog`. You then need to wire up the Dialog infrastructure. The OnBoarding dialog shows a simple example which you can refer to and an excerpt is shown below alongside an overview of the steps.

- Add a waterfall dialog to your constructor
- Define the steps for your waterfall
- Create your waterfall step handlers
- Call AddDialog passing your waterfall
- Call AddDialog passing any prompts you use in your waterfall
- Set your InitialDialogId to the first dialog you want the component to run

```
InitialDialogId = nameof(OnboardingDialog);

var onboarding = new WaterfallStep[]
{
    AskForName,
    AskForEmail,
    AskForLocation,
    FinishOnboardingDialog,
};

AddDialog(new WaterfallDialog(InitialDialogId, onboarding));
AddDialog(new TextPrompt(NamePrompt));
AddDialog(new TextPrompt(EmailPrompt));
AddDialog(new TextPrompt(LocationPrompt));
```

Then you need to create the View Part of your dialog to handle responses. Create a new View class and derive from TemplateManager, an example is provided in the OnboardingResponses.cs file and an excerpt is shown below.

```
public const string _namePrompt = "namePrompt";
public const string _haveName = "haveName";
public const string _emailPrompt = "emailPrompt";
      
private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
{
    ["default"] = new TemplateIdMap
    {
        {
            _namePrompt,
            (context, data) => OnboardingStrings.NAME_PROMPT
        },
        {
            _haveName,
            (context, data) => string.Format(OnboardingStrings.HAVE_NAME, data.name)
        },
        {
            _emailPrompt,
            (context, data) => OnboardingStrings.EMAIL_PROMPT
        },
```

Then to render responses you can use a View class instance to access these responses through `ReplyWith` or `RenderTemplate` for Prompts. Examples are shown below.

```
Prompt = await _responder.RenderTemplate(sc.Context, "en", OnboardingResponses._namePrompt),
await _responder.ReplyWith(sc.Context, OnboardingResponses._haveName, new { name });
```

The final piece of Dialog infrastruture is the creation of a State class scoped to your Dialog only. Create a new class and ensure it derives from `DialogState`

Back within your `MainDialog` class, add an additional call to `AddDialog` in the constructor and ensure you add the appropriate dialog triggering code to the LUIS intent evaluation switch statement.

## Conversational Insights using PowerBI Dashboard and Application Insights
- To get started with getting Conversational insights, continue with  [Configure Conversational Analytics with PowerBI Dashbpard](bot-builder-enterprise-template-powerbi.md).


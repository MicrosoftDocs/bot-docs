---
title: Enterprise Bot Customization | Microsoft Docs
description: Learn how to customise the Bot created by the Enterprise Template
author: darrenj
ms.author: darrenj
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 09/18/2018
monikerRange: 'azure-bot-service-4.0'
---
# Enterprise Bot Template - Customize your Bot

> [!NOTE]
> This topic applies to v4 version of the SDK. 

## .NET
After you have deployed and tested that the Enterprise Bot Template works end-to-end as listed in the instructions [here](bot-builder-enterprise-template-deployment.md),
you can easily customize your bot based on your scenario and needs. The goal of the template is to provide a solid foundation upon which to build your conversational experience.

## Project Structure

The folder structure of your Bot is shown below and represents our recommended best practice for structuring your Bot project and processing incoming messages.

    | - YourBot.bot         // The .bot file containing all of your Bot configuration including dependencies
    | - README.md           // README file containing links to documentation
    | - Program.cs          // Default Program.cs file
    | - Startup.cs          // Core Bot Initialisation including Bot Configuration LUIS, Dispatcher, etc. 
    | - <BOTNAME>State.cs   // The Root State class for your Bot
    | - appsettings.json    // References above .bot file for Configuration information. App Insights key
    | - CognitiveModels     
        | - LUIS            // .LU file containing base conversational intents (Greeting, Help, Cancel)
        | - QnA             // .LU file containing example QnA items
    | - DeploymentScripts   // msbot clone recipes for deployment
        | - de              // Deployment files for German
        | - en              // Deployment files for English        
        | - es              // Deployment files for Spanish
        | - fr              // Deployment files for French
        | - it              // Deployment files for Italian
        | - zh              // Deployment files for Chinese
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
    | - Middleware          // Telemetry, Content Moderator
    | - ServiceClients      // SDK libraries, example GraphClient provided for Auth example
   
## Update Introduction Message

The Introduction message makes use of an [Adaptive Card](https://www.adaptivecards.io). To customize this introduction for your bot you can find the JSON file within the Dialogs/Main/Resources folder called ```Intro.json```. Use the [Adaptive Card visualizer](http://adaptivecards.io/visualizer) to modify the Adaptive Card to suit your Bot's requirements.

## Update Bot Responses

Each Dialog within the project has a set of responses that are stored within supporting resource (.resx) files. These can be found within the Resources folder under each Dialog.

You can change the responses in the Visual Studio resource editor as shown below to adjust how your bot responds.

![Customizing Bot Responses](media/enterprise-template/EnterpriseBot-CustomisingResponses.png)

This approach supports multi-lingual responses using the standard resource file localization approach. Further information can be found [here.](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-2.1)

## Updating your Cognitive Models

There are two Cognitive Models included with the Enterprise Template by default, a sample FAQ QnA Maker knowledge base and a LUIS model for General intents (Greeting, Help, Cancel, etc.). These models can be customized to suit your needs. You can also add new LUIS models and QnA Maker knowledge bases to expand your bot's capabilities.

### Updating an existing LUIS Model
To update an existing LUIS model for the Enterprise Template, perform these steps:
1. Make your changes to the LUIS Model in the [LUIS Portal](http://luis.ai) or using the [LuDown](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/Ludown) and [Luis](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/LUIS) CLI tools. 
2. Run the following command to update your Dispatch model to reflect your changes (ensures proper message routing):
```shell
    dispatch refresh --bot "YOUR_BOT.bot" --secret YOUR_SECRET
```
3. Run the following command from your project root for each updated model to update their associated LuisGen classes: 
```shell
    luis export version --appId [LUIS_APP_ID] --versionId [LUIS_APP_VERSION] --authoringKey [YOUR_LUIS_AUTHORING_KEY] | luisgen --cs [CS_FILE_NAME] -o "\Dialogs\Shared\Resources"
```

### Updating an existing QnA Maker knowledge base
To update an existing QnA Maker knowledge base, perform the following steps:
1. Make changes to your QnA Maker knowledge base via the [LuDown](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/Ludown) and [QnA Maker](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/QnAMaker) CLI tools or the [QnA Maker Portal](https://qnamaker.ai).
2. Run the following command to update your Dispatch model to reflect your changes (ensures proper message routing):
```shell
    dispatch refresh --bot "YOUR_BOT.bot" --secret YOUR_SECRET
```

### Adding a new LUIS model

In scenarios where you wish to add a new LUIS model to your project you need to update the Bot configuration and Dispatcher to ensure it is aware of the additional model. 
1. Create your LUIS model through LuDown/LUIS CLI tools or through the LUIS portal
2. Run the following command to connect your new LUIS app to your .bot file:
```shell
    msbot connect luis --appId [LUIS_APP_ID] --authoringKey [LUIS_AUTHORING_KEY] --subscriptionKey [LUIS_SUBSCRIPTION_KEY] 
```
3. Add this new LUIS model to your Dispatcher through the following command
```shell
    dispatch add -t luis -id LUIS_APP_ID -bot "YOUR_BOT.bot" -secret YOURSECRET
```
4. Refresh the dispatch model to reflect the LUIS model changes through the following command
```shell
    dispatch refresh -bot "YOUR_BOT.bot" -secret YOUR_SECRET
```

### Adding an additional QnA Maker knowledge base

In some scenarios you may wish to add an addditional QnA Maker knowledge base to your Bot, this can be performed through the following steps.

1. Create a new QnA Maker knowledge base from a JSON file using the following command executed in your assistant directory
```shell
qnamaker create kb --in <KB.json> --msbot | msbot connect qna --stdin --bot "YOUR_BOT.bot" --secret YOURSECRET
```
2. Run the following command to update your Dispatch model to reflect your changes
```shell
dispatch refresh --bot "YOUR_BOT.bot" --secret YOUR_SECRET
```
3. Update the strongly typed Dispatch class to reflect the new QnA source
```shell
msbot get dispatch --bot "YOUR_BOT.bot" | luis export version --stdin > dispatch.json
luisgen dispatch.json -cs Dispatch -o Dialogs\Shared
```
4.  Update the `Dialogs\Main\MainDialog.cs` file to include the corresponding Dispatch intent for your new QnA source following the example provided.

You should now be able to leverage multiple QnA sources as part of your Bot.

## Adding a new dialog

To add a new Dialog to your Bot you need to first create a new Folder under Dialogs and ensure this class derives from `EnterpriseDialog`. You then need to wire up the Dialog infrastructure. The Onboarding dialog shows a simple example which you can refer to and an excerpt is shown below alongside an overview of the steps.

- Add a waterfall dialog to your constructor
- Define the steps for your waterfall
- Create your waterfall steps
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
    AddDialog(new TextPrompt(DialogIds.NamePrompt));
    AddDialog(new TextPrompt(DialogIds.EmailPrompt));
    AddDialog(new TextPrompt(DialogIds.LocationPrompt));
```

Then you need to create the Template Manager to handle responses. Create a new class and derive from TemplateManager, an example is provided in the OnboardingResponses.cs file and an excerpt is shown below.

```    
 ["default"] = new TemplateIdMap
            {
                { ResponseIds.EmailPrompt,
                    (context, data) =>
                    MessageFactory.Text(
                        text: OnboardingStrings.EMAIL_PROMPT,
                        ssml: OnboardingStrings.EMAIL_PROMPT,
                        inputHint: InputHints.ExpectingInput)
                },
                { ResponseIds.HaveEmailMessage,
                    (context, data) =>
                    MessageFactory.Text(
                        text: string.Format(OnboardingStrings.HAVE_EMAIL, data.email),
                        ssml: string.Format(OnboardingStrings.HAVE_EMAIL, data.email),
                        inputHint: InputHints.IgnoringInput)
                },
                { ResponseIds.HaveLocationMessage,
                    (context, data) =>
                    MessageFactory.Text(
                        text: string.Format(OnboardingStrings.HAVE_LOCATION, data.Name, data.Location),
                        ssml: string.Format(OnboardingStrings.HAVE_LOCATION, data.Name, data.Location),
                        inputHint: InputHints.IgnoringInput)
                },
                
                ...
```

To render responses you can use a Template Manager instance to access these responses through `ReplyWith` or `RenderTemplate` for Prompts. Examples are shown below.

```
Prompt = await _responder.RenderTemplate(sc.Context, sc.Context.Activity.Locale, OnboardingResponses.ResponseIds.NamePrompt)
await _responder.ReplyWith(sc.Context, OnboardingResponses.ResponseIds.HaveNameMessage, new { name });
```

The final piece of Dialog infrastruture is the creation of a State class scoped to your Dialog only. Create a new class and ensure it derives from `DialogState`

Once your dialog is complete, your need to add the dialog to your `MainDialog` component using `AddDialog`. To use your new Dialog, call `dc.BeginDialogAsync()` from within your `RouteAsync` method, triggering with the appropriate LUIS intent if desired.

## Conversational insights using PowerBI dashboard and Application Insights
- To get started with getting Conversational insights, continue with  [Configure conversational analytics with PowerBI dashboard](bot-builder-enterprise-template-powerbi.md).


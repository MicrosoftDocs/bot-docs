---
title: Enable language understanding with LUIS | Microsoft Docs
description: Learn how to enable your bot to understand natural language by using LUIS dialogs in the Bot Builder SDK for .NET.
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 06/23/2017
ms.reviewer:
---

# Enable language understanding with LUIS

This article describes how to use <a href="https://www.luis.ai" target="_blank">LUIS</a> with dialogs to 
create an Alarm bot that a user can interact with using [natural language](../cognitive-services-bot-intelligence-overview.md#language-understanding). 

## Create the class

To create a dialog that uses LUIS, first create a class that derives from `LuisDialog` and 
specify the [LuisModel attribute](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.luis.luismodelattribute?view=botbuilder-3.8). 
To populate the `modelID` and `subscriptionKey` parameters for the `LuisModel` attribute, use 
the `id` and `subscription-key` attribute values from your LUIS application's endpoint. 

The `domain` parameter is determined by the Azure region to which your LUIS app is published. 
If not supplied, it defaults to `westus.api.cognitive.microsoft.com`.
See [Regions and keys](#regions-and-keys) for more information.

[!code-csharp[Class definition](../includes/code/dotnet-luis-dialogs.cs#classDefinition)]

## Create method(s) to handle intent

Within the class, create the methods that will execute when your LUIS model matches a user's utterance to intent. 
To designate the method that will execute when a specific intent is matched, specify the `LuisIntent` attribute. 
This code example defines the method that will execute when the `builtin.intent.alarm.turn_off_alarm` intent is matched.

[!code-csharp[Turn-off-alarm handler](../includes/code/dotnet-luis-dialogs.cs#turnOffAlarmHandler)]

In this example, if the bot finds the alarm that the user wants to turn off, 
it confirms the user's request by using the built-in `Prompt.Confirm` dialog. 
The confirm dialog will spawn a sub-dialog that asks the user to verify the request to turn off the alarm. 
If the user verifies the request, the dialog calls the `AfterConfirming_TurnOffAlarm` method to turn off the alarm. 

## Alarm bot implementation

This code example shows the full dialog implementation for the Alarm bot. 

[!code-csharp[Full LUIS dialog example](../includes/code/dotnet-luis-dialogs.cs#fullExample)]

## Regions and keys
The region to which you publish your LUIS app must correspond to the region or location you specify in the Azure portal when you create a key. To publish a LUIS app to more than one region, you need at least one key per region. LUIS apps created at <a href="https://www.luis.ai" target="_blank">https://www.luis.ai</a> can be published to endpoints in the following regions:

 Azure region   |   Endpoint URL format   |   
------|------|
West US     |   `https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/YOUR-APP-ID?subscription-key=YOUR-SUBSCRIPTION-KEY`  |
East US     |   `https://eastus.api.cognitive.microsoft.com/luis/v2.0/apps/YOUR-APP-ID?subscription-key=YOUR-SUBSCRIPTION-KEY`   |
West Central US     |   `https://westcentralus.api.cognitive.microsoft.com/luis/v2.0/apps/YOUR-APP-ID?subscription-key=YOUR-SUBSCRIPTION-KEY`   |
Southeast Asia     |   `https://southeastasia.api.cognitive.microsoft.com/luis/v2.0/apps/YOUR-APP-ID?subscription-key=YOUR-SUBSCRIPTION-KEY`   |

To publish to the European regions, you can create LUIS apps at <a href="https://eu.luis.ai" target="_blank">https://eu.luis.ai</a>.  

 Azure region   |   Endpoint URL format   |   
------|------|
West Europe     | `https://westeurope.api.cognitive.microsoft.com/luis/v2.0/apps/YOUR-APP-ID?subscription-key=YOUR-SUBSCRIPTION-KEY` |

## Data privacy
To keep LUIS from saving the user's utterances, set the `log` parameter to false in the [LuisModel attribute](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.luis.luismodelattribute?view=botbuilder-3.8).

[!code-csharp[Class definition](../includes/code/dotnet-luis-dialogs.cs#classDefinitionLogFalse)]

## Additional resources

- [Dialogs](bot-builder-dotnet-dialogs.md)
- [Manage conversation flow with dialogs](bot-builder-dotnet-manage-conversation-flow.md)
- [Language understanding](../cognitive-services-bot-intelligence-overview.md#language-understanding)
- <a href="https://www.luis.ai" target="_blank">LUIS</a>
- <a href="https://docs.microsoft.com/en-us/dotnet/api/?view=botbuilder-3.8" target="_blank">Bot Builder SDK for .NET Reference</a>
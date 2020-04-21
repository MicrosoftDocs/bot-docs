---
title: Use a dialog to consume a skill | Microsoft Docs
description: Learn how to consume a skill using dialogs, using the Bot Framework SDK.
keywords: skills
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 04/02/2020
monikerRange: 'azure-bot-service-4.0'
---

# Use a dialog to consume a skill

[!INCLUDE[applies-to](../includes/applies-to.md)]

This article demonstrates how to use a _skill dialog_ within a root bot use a skill bot.
The skill bot can handle both message and event activities.
It demonstrates how to post activities from the parent bot to the skill bot and return the skill responses to the user.
For a sample skill manifest and information about implementing the skill, see how to [use dialogs within a skill]().

For information about using a skill bot outside of dialogs, see how to [implement a skill consumer](skill-implement-consumer.md).

## Prerequisites

- Knowledge of [bot basics](bot-builder-basics.md), [how skills bots work](skills-conceptual.md), and how to [implement a skill](skill-implement-skill.md).
- An Azure subscription. If you don't have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A copy of the **skills skillDialog** sample in [**C#**](https://aka.ms/skills-using-dialogs-cs), [**JavaScript**](https://aka.ms/skills-using-dialogs-js) or [**Python**](https://aka.ms/skills-using-dialogs-py).

## About this sample

The **skills skillDialog** sample includes projects for two bots:

- The _dialog root bot_, which uses a _skill dialog_ class to consume a skill.
- The _dialog skill bot_, which uses a dialog to handle activities coming from skill consumers.

This article focuses on how to use a _skill dialog_ class in a root bot to manage the skill, to send message and event activities and to cancel the skill.

If you use middleware in your skill consumer, see how to [use middleware in a skill consumer]() to avoid some common problems.
For information about other aspects of creating a skill consumer, see how to [implement a skill consumer](skill-implement-consumer.md).

### [C#](#tab/cs)

![Skill consumer class diagram](./media/skill-dialog/dialog-root-bot-cs.png)

### [JavaScript](#tab/js)

![Skill consumer class diagram](./media/skill-dialog/dialog-root-bot-js.png)

### [Python](#tab/python)

![Skill consumer class diagram](./media/skill-dialog/dialog-root-bot-py.png)

---

For information about the dialog skill bot, see how to [use dialogs within a skill]().

## Resources

Bot-to-bot authentication requires that each participating bot has a valid appID and password.

Register both the skill and the skill consumer with Azure. You can use a Bot Channels Registration. For more information, see how to [register a bot with Azure Bot Service](../bot-service-quickstart-registration.md).

## Application configuration

1. Add the root bot's app ID and password.
1. Add the endpoint URL to which the skills should reply to the skill consumer.
1. Add an entry for each skill the skill consumer will use. Each entry includes:
   - An ID the skill consumer will use to identify each skill.
   - The skill's app ID.
   - The skill's messaging endpoint.

### [C#](#tab/cs)

**DialogRootBot\appsettings.json**

Add the root bot's app ID and password to the appsettings.json file. Also, add the app ID for the echo skill bot to the `BotFrameworkSkills` array.

[!code-csharp[configuration file](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/appsettings.json)]

### [JavaScript](#tab/js)

**dialogRootBot/.env**

Add the root bot's app ID and password to the .env file. Also, add the app ID for the echo skill bot.

[!code-javascript[configuration file](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/.env)]

### [Python](#tab/python)

**dialog-root-bot/config.py**

Add the root bot's app ID and password to the .env file. Also, add the app ID for the echo skill bot.

[!code-python[configuration file](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/config.py?range=14-25)]

---

## Dialog logic

The bot's main dialog includes a _skill dialog_ for each skill this bot consumes. The skill dialog manages the skill through the various skill-related objects for you, such as the _skill client_ and the _skill conversation ID factory_ objects.
The main dialog also demonstrates how to cancel the skill (through the skill dialog) based on user input.

The skill this bot uses supports a couple different features. It can book a flight or get the weather for a city. In addition, if it receives a message outside either of these contexts, it sends and echo message.
The skill manifest ([**C#**](https://aka.ms/skilldialog-manifest-cs), [**JavaScript**](https://aka.ms/skilldialog-manifest-js), [**Python**](https://aka.ms/skilldialog-manifest-py)) describes the actions the skill can perform, its input and output parameters, and the skill's endpoints.
Of note, the skill can handle a "BookFlight" or "GetWeather" event. It can also handle messages.

The main dialog includes code to:

- [Initialize the main dialog](#initialize-the-main-dialog)
- [Select a skill](#select-a-skill)
- [Select a skill action](#select-a-skill-action)
- [Start a skill](#start-a-skill)
- [Summarize the skill result](#summarize-the-skill-result)
- [Allow the user to cancel the skill](#allow-the-user-to-cancel-the-skill)

The main dialog inherits from the _component dialog_ class. For more about component dialogs, see how to [manage dialog complexity](bot-builder-compositcontrol.md)

### Initialize the main dialog

The main dialog includes dialogs (for managing conversation flow outside the skill) and a skill dialogs (for managing the skills).
The waterfall includes the following steps, described in more detail in the next few sections.

1. Prompt the user to select the skill to use. (The root bot consumes 1 skill.)
1. Prompt the user to select the action to use for that skill. (The skill bot defines 3 actions.)
1. Start the chosen skill with an initial activity based on the chosen action.
1. Once the skill completes, display the results, if any. Then, restart the waterfall.

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

The `MainDialog` class derives from `ComponentDialog`.
In addition to conversation state, the dialog needs the root bot's app ID and references to the skill conversation ID factory, the skill HTTP client, and the skills configuration objects.

The dialog constructor checks its input parameters, adds skills dialogs, adds prompt and a waterfall dialogs for managing conversation flow outside the skill, and creates a property accessor for tracking the active skill, if any.

The constructor calls `AddSkillDialogs`, a helper method, to create a `SkillDialog` for each skill that is included in the configuration file, as read from the configuration file into a `SkillsConfiguration` object.

<!--
[!code-csharp[fields](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=33-36)]

[!code-csharp[constructor](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=38-84)]
-->

[!code-csharp[AddSkillDialogs](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=204-223)]

#### [JavaScript](#tab/js)

**dialogRootBot/dialogs/mainDialog.js**

The `MainDialog` class derives from `ComponentDialog`.
In addition to conversation state, the dialog needs the root bot's app ID and references to the skill conversation ID factory, the skill HTTP client, and the skills configuration objects. The code retrieves the bot's app ID from the user environment.

The dialog constructor checks its input parameters, adds skills dialogs, adds prompt and a waterfall dialogs for managing conversation flow outside the skill, and creates a property accessor for tracking the active skill, if any.

The constructor calls `AddSkillDialogs`, a helper method, to create a `SkillDialog` for each skill that is included in the configuration file, as read from the configuration file into a `SkillsConfiguration` object.

<!--
[!code-javascript[constructor](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=21-54)]
-->

[!code-javascript[addSkillDialogs](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=178-194)]

### [Python](#tab/python)

**dialog-root-bot/dialogs/main_dialog.py**

The `MainDialog` class derives from `ComponentDialog`.
In addition to conversation state, the dialog needs the root bot's app ID and references to the skill conversation ID factory, the skill HTTP client, and the skills configuration objects.

The dialog constructor checks its input parameters, adds skills dialogs, adds prompt and a waterfall dialogs for managing conversation flow outside the skill, and creates a property accessor for tracking the active skill, if any.

The constructor calls `AddSkillDialogs`, a helper method, to create a `SkillDialog` for each skill that is included in the configuration file, as read from the configuration file into a `SkillConfiguration` object.

<!--
[!code-python[constructor](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=36-106)]
-->

[!code-python[_add_skill_dialogs](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=215-235)]

---

### Select a skill

In its first step, the main dialog prompts the user for which skill they'd like to call, and uses the "SkllPrompt" choice prompt to get the answer. (This bot defines only one skill.)

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

[!code-csharp[SelectSkillStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=103-118)]

#### [JavaScript](#tab/js)

**dialogRootBot/dialogs/mainDialog.js**

[!code-javascript[selectSkillStep](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=87-102)]

#### [Python](#tab/python)

**dialog-root-bot/dialogs/main_dialog.py**

[!code-python[_select_skill_step](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=124-144)]

---

### Select a skill action

In the next step, the main dialog:

1. Saves information about the skill the user selected.
1. Prompts the user for which skill action they'd like to use, and uses the "SkillActionPrompt" choice prompt to get the answer.
   - It uses a helper method to get a list of actions to choose from.
   - The prompt validator associated with this prompt will default to sending the skill a message if the user's input doesn't match one of the choices.

The choices included in this bot help test the actions defined for this skill. More typically, you would read the options from the skill's manifest, and present options to the user based on that list.

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

[!code-csharp[SelectSkillActionStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=120-140)]

[!code-csharp[GetSkillActions](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=225-242)]

[!code-csharp[SkillActionPromptValidator](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=142-152)]

#### [JavaScript](#tab/js)

**dialogRootBot/dialogs/mainDialog.js**

[!code-javascript[selectSkillActionStep](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=104-120)]

[!code-javascript[getSkillActions](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=196-112)]

[!code-javascript[skillActionPromptValidator](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=270-279)]

#### [Python](#tab/python)

**dialog-root-bot/dialogs/main_dialog.py**

[!code-python[_select_skill_action_step](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=146-166)]

[!code-python[_get_skill_actions](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=249-260)]

[!code-python[_skill_action_prompt_validator](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=237-247)]

---

### Start a skill

In the next step, the main dialog:

1. Retrieves information about the skill and skill activity the user selected.
1. Uses a helper method to create the activity to initially send to the skill.
1. Creates the dialog options with which to start the skill dialog. This includes the initial activity to send.
1. Saves state before calling the skill. (This is necessary, as the skill response might come to a different instance of the skill consumer.)
1. Begins the skill dialog, passing in the skill ID to call and the options with which to call it.

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

[!code-csharp[CallSkillActionStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=154-179&highlight=178)]

#### [JavaScript](#tab/js)

**dialogRootBot/dialogs/mainDialog.js**

[!code-javascript[callSkillActionStep](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=126-150&highlight=149)]

#### [Python](#tab/python)

**dialog-root-bot/dialogs/main_dialog.py**

[!code-python[_call_skill_action_step](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=168-190&highlight=190)]

---

### Summarize the skill result

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

[!code-csharp[FinalStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=181-202)]

#### [JavaScript](#tab/js)

**dialogRootBot/dialogs/mainDialog.js**

[!code-javascript[selectSkillStep](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=87-102)]

#### [Python](#tab/python)

**dialog-root-bot/dialogs/main_dialog.py**

[!code-python[_select_skill_step](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=124-144)]

---

### Allow the user to cancel the skill

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

[!code-csharp[OnContinueDialogAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=86-101)]

#### [JavaScript](#tab/js)

**dialogRootBot/dialogs/mainDialog.js**

[!code-javascript[selectSkillStep](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=87-102)]

#### [Python](#tab/python)

**dialog-root-bot/dialogs/main_dialog.py**

[!code-python[_select_skill_step](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=124-144)]

---

## Activity handler logic

Since skill logic for each turn is handled by a main dialog, the activity handler looks much like it would for other dialog samples.

### [C#](#tab/cs)

**DialogRootBot\Bots\RootBot.cs**

[!code-csharp[class definition](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Bots/RootBot.cs?range=15-16)]

[!code-csharp[constructor](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Bots/RootBot.cs?range=18-25)]

[!code-csharp[OnTurnAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Bots/RootBot.cs?range=27-42)]

### [JavaScript](#tab/js)

### [Python](#tab/python)

---

## Service registration

### [C#](#tab/cs)

**DialogRootBot\Startup.cs**

[!code-csharp[ConfigureServices](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Startup.cs?range=30-67)]

### [JavaScript](#tab/js)

### [Python](#tab/python)

---

## Test the root bot

## Additional information

<!--
TODO Write:
- use dialogs within a skill
- manage logging in a skill consumer
-->

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

In addition to conversation state, the dialog needs the root bot's app ID and references to the skill conversation ID factory, the skill HTTP client, and the skills configuration objects.

The `MainDialog` class derives from `ComponentDialog`.

**DialogRootBot\Dialogs\MainDialog.cs**

The dialog constructor checks its input parameters, adds skills dialogs, adds prompt and a waterfall dialogs for managing conversation flow outside the skill, and creates a property accessor for tracking the active skill, if any.

It calls `AddSkillDialogs`, a helper method, to create a `SkillDialog` for each skill that is included in the configuration file, as read from the configuration file into a `SkillsConfiguration` object.

<!--
[!code-csharp[fields](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=33-36)]

[!code-csharp[constructor](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=38-84)]
-->

[!code-csharp[AddSkillDialogs](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=204-223)]

#### [JavaScript](#tab/js)

In addition to conversation state, the dialog needs the root bot's app ID and references to the skill conversation ID factory, the skill HTTP client, and the skills configuration objects. The code retrieves the bot's app ID from the user environment.

The `MainDialog` class derives from `ComponentDialog`.

**dialogRootBot/dialogs/mainDialog.js**

The dialog constructor checks its input parameters, adds skills dialogs, adds prompt and a waterfall dialogs for managing conversation flow outside the skill, and creates a property accessor for tracking the active skill, if any.

It calls `AddSkillDialogs`, a helper method, to create a `SkillDialog` for each skill that is included in the configuration file, as read from the configuration file into a `SkillsConfiguration` object.

<!--
[!code-javascript[constructor](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=21-54)]
-->

[!code-javascript[addSkillDialogs](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=178-194)]

### [Python](#tab/python)

In addition to conversation state, the dialog needs the root bot's app ID and references to the skill conversation ID factory, the skill HTTP client, and the skills configuration objects.

The `MainDialog` class derives from `ComponentDialog`.

**dialog-root-bot/dialogs/main_dialog.py**

The dialog constructor checks its input parameters, adds skills dialogs, adds prompt and a waterfall dialogs for managing conversation flow outside the skill, and creates a property accessor for tracking the active skill, if any.

It calls `AddSkillDialogs`, a helper method, to create a `SkillDialog` for each skill that is included in the configuration file, as read from the configuration file into a `SkillConfiguration` object.

<!--
[!code-python[constructor](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=36-106)]
-->

[!code-python[_add_skill_dialogs](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=215-235)]

---

### Select a skill

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

[!code-csharp[SelectSkillStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=103-118)]

#### [JavaScript](#tab/js)

#### [Python](#tab/python)

---

### Select a skill action

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

[!code-csharp[SelectSkillActionStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=120-140)]

[!code-csharp[GetSkillActions](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=225-242)]

[!code-csharp[SkillActionPromptValidator](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=142-152)]

#### [JavaScript](#tab/js)

#### [Python](#tab/python)

---

### Start a skill

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

[!code-csharp[CallSkillActionStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=154-179)]

[!code-csharp[CreateDialogSkillBotActivity](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=244-295)]

#### [JavaScript](#tab/js)

#### [Python](#tab/python)

---

### Summarize the skill result

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

[!code-csharp[FinalStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=181-202)]

#### [JavaScript](#tab/js)

#### [Python](#tab/python)

---

### Allow the user to cancel the skill

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

[!code-csharp[OnContinueDialogAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=86-101)]

#### [JavaScript](#tab/js)

#### [Python](#tab/python)

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

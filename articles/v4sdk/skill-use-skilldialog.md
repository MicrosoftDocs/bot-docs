---
title: Use a dialog to consume a skill | Microsoft Docs
description: Learn how to consume a skill using dialogs, using the Bot Framework SDK.
keywords: skills
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 11/08/2021
monikerRange: 'azure-bot-service-4.0'
---

# Use a dialog to consume a skill

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article demonstrates how to use a _skill dialog_ within a skill consumer.
The skill dialog posts activities from the parent bot to the skill bot and returns the skill responses to the user.
The skill bot accessed by this consumer can handle both message and event activities.
For a sample skill manifest and information about implementing the skill, see how to [use dialogs within a skill](skill-actions-in-dialogs.md).

For information about using a skill bot outside of dialogs, see how to [implement a skill consumer](skill-implement-consumer.md).

## Prerequisites

- Knowledge of [bot basics](bot-builder-basics.md), [how skills bots work](skills-conceptual.md), and how to [implement a skill consumer](skill-implement-consumer.md).
- Optionally, an Azure subscription. If you don't have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A copy of the **skills skillDialog** sample in [**C#**](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/81.skills-skilldialog#readme), [**JavaScript**](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/javascript_nodejs/81.skills-skilldialog#readme), [**Java**](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/81.skills-skilldialog#readme) or [**Python**](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/python/81.skills-skilldialog#readme).

> [!NOTE]
> Starting with version 4.11, you do not need an app ID and password to test a skill consumer locally in the Emulator. An Azure subscription is still required to deploy your consumer to Azure or to consume a deployed skill.

## About this sample

The **skills skillDialog** sample includes projects for two bots:

- The _dialog root bot_, which uses a _skill dialog_ class to consume a skill.
- The _dialog skill bot_, which uses a dialog to handle activities coming from skill consumers.

This article focuses on how to use a _skill dialog_ class in a root bot to manage the skill, to send message and event activities and to cancel the skill.

<!--
If you use middleware in your skill consumer, see how to [use middleware in a skill consumer](skill-middleware-in-consumer.md) to avoid some common problems.
-->
For information about other aspects of creating a skill consumer, see how to [implement a skill consumer](skill-implement-consumer.md).

### [C#](#tab/cs)

![C# skill consumer class diagram](./media/skill-dialog/dialog-root-bot-cs.png)

### [JavaScript](#tab/js)

![JavaScript skill consumer class diagram](./media/skill-dialog/dialog-root-bot-js.png)

### [Java](#tab/java)

![Java skill consumer class diagram](./media/skill-dialog/dialog-root-bot-java.png)

### [Python](#tab/python)

![Python skill consumer class diagram](./media/skill-dialog/dialog-root-bot-py.png)

---

For information about the dialog skill bot, see how to [use dialogs within a skill](skill-actions-in-dialogs.md).

## Resources

For deployed bots, bot-to-bot authentication requires that each participating bot has a valid app ID and password.
However, you can test skills and skill consumers locally with the Emulator without an app ID and password.

## Application configuration

1. Optionally, add the root bot's app ID and password to the config file.
1. Add the skill host endpoint (the service or callback URL) to which the skills should reply to the skill consumer.
1. Add an entry for each skill the skill consumer will use. Each entry includes:
   - An ID the skill consumer will use to identify each skill.
   - Optionally, the skill's app ID.
   - The skill's messaging endpoint.

> [!NOTE]
> If either the skill or skill consumer uses an app ID and password, both must.

### [C#](#tab/cs)

**DialogRootBot\appsettings.json**

Optionally, add the root bot's app ID and password and add the app ID for the echo skill bot to the `BotFrameworkSkills` array.

[!code-json[configuration file](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/appsettings.json?highlight=2-3,9)]

### [JavaScript](#tab/js)

**dialogRootBot/.env**

Optionally, add the root bot's app ID and password and add the app ID for the echo skill bot.

[!code-javascript[configuration file](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/.env?highlight=2-3,8)]

### [Java](#tab/java)

**DialogRootBot\application.properties**

Optionally, add the root bot's app ID and password and add the app ID for the echo skill bot to the `BotFrameworkSkills` array.

[!code-json[configuration file](~/../botbuilder-samples/samples/java_springboot/81.skills-skilldialog/dialog-root-bot/src/main/resources/application.properties?highlight=1-2,7)]

### [Python](#tab/python)

**dialog-root-bot/config.py**

Optionally, add the root bot's app ID and password and add the app ID for the echo skill bot.

[!code-python[configuration file](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/config.py?range=14-25&highlight=1,3,9)]

---

## Dialog logic

The bot's main dialog includes a _skill dialog_ for each skill this bot consumes. The skill dialog manages the skill through the various skill-related objects for you, such as the _skill client_ and the _skill conversation ID factory_ objects.
The main dialog also demonstrates how to cancel the skill (through the skill dialog) based on user input.

The skill this bot uses supports a couple different features. It can book a flight or get the weather for a city. In addition, if it receives a message outside either of these contexts and a LUIS recognizer is configured, it attempts to interpret the user's intent.

The skill manifest ([**C#**](https://github.com/microsoft/BotBuilder-Samples/blob/main/samples/csharp_dotnetcore/81.skills-skilldialog/DialogSkillBot/wwwroot/manifest/dialogchildbot-manifest-1.0.json), [**JavaScript**](https://github.com/microsoft/BotBuilder-Samples/blob/main/samples/javascript_nodejs/81.skills-skilldialog/dialogSkillBot/manifest/dialogchildbot-manifest-1.0.json), [**Java**](https://github.com/microsoft/BotBuilder-Samples/blob/main/samples/java_springboot/81.skills-skilldialog/dialog-skill-bot/src/main/webapp/manifest/echoskillbot-manifest-1.0.json), [**Python**](https://github.com/microsoft/BotBuilder-Samples/blob/main/samples/python/81.skills-skilldialog/dialog-skill-bot/wwwroot/manifest/dialogchildbot-manifest-1.0.json)) describes the actions the skill can perform, its input and output parameters, and the skill's endpoints.
Of note, the skill can handle a "BookFlight" or "GetWeather" event. It can also handle messages.

The main dialog includes code to:

- [Initialize the main dialog](#initialize-the-main-dialog)
- [Select a skill](#select-a-skill)
- [Select a skill action](#select-a-skill-action)
- [Start a skill](#start-a-skill)
- [Summarize the skill result](#summarize-the-skill-result)
- [Allow the user to cancel the skill](#allow-the-user-to-cancel-the-skill)

The main dialog inherits from the _component dialog_ class. For more about component dialogs, see how to [manage dialog complexity](bot-builder-compositcontrol.md).

### Initialize the main dialog

The main dialog includes dialogs (for managing conversation flow outside the skill) and a skill dialog (for managing the skills).
The waterfall includes the following steps, described in more detail in the next few sections.

1. Prompt the user to select the skill to use. (The root bot consumes one skill.)
1. Prompt the user to select the action to use for that skill. (The skill bot defines three actions.)
1. Start the chosen skill with an initial activity based on the chosen action.
1. Once the skill completes, display the results, if any. Then, restart the waterfall.

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

The `MainDialog` class derives from `ComponentDialog`.
In addition to conversation state, the dialog needs the root bot's app ID and references to the skill conversation ID factory, the skill HTTP client, and the skills configuration objects.

The dialog constructor checks its input parameters, adds skill dialogs, adds prompt and waterfall dialogs for managing conversation flow outside the skill, and creates a property accessor for tracking the active skill, if any.

The constructor calls `AddSkillDialogs`, a helper method, to create a `SkillDialog` for each skill that is included in the configuration file, as read from the configuration file into a `SkillsConfiguration` object.

[!code-csharp[AddSkillDialogs](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=200-219&highlight=18)]

#### [JavaScript](#tab/js)

**dialogRootBot/dialogs/mainDialog.js**

The `MainDialog` class derives from `ComponentDialog`.
In addition to conversation state, the dialog needs the root bot's app ID and references to the skill conversation ID factory, the skill HTTP client, and the skills configuration objects. The code retrieves the bot's app ID from the user environment.

The dialog constructor checks its input parameters, adds skills dialogs, adds prompt and waterfall dialogs for managing conversation flow outside the skill, and creates a property accessor for tracking the active skill, if any.

The constructor calls `addSkillDialogs`, a helper method, to create a `SkillDialog` for each skill that is included in the configuration file, as read from the configuration file into a `SkillsConfiguration` object.

[!code-javascript[addSkillDialogs](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=175-191&highlight=15)]

#### [Java](#tab/java)

**DialogRootBot\dialogs\MainDialog.java**

The `MainDialog` class derives from `ComponentDialog`.
In addition to conversation state, the dialog needs the root bot's app ID and references to the skill conversation ID factory, the skill HTTP client, and the skills configuration objects.

The dialog constructor checks its input parameters, adds skills dialogs, adds prompt and a waterfall dialogs for managing conversation flow outside the skill, and creates a property accessor for tracking the active skill, if any.

The constructor calls `addSkillDialogs`, a helper method, to create a `SkillDialog` for each skill that is included in the configuration file, as read from the configuration file into a `SkillsConfiguration` object.

[!code-java[addSkillDialogs](~/../botbuilder-samples/samples/java_springboot/81.skills-skilldialog/dialog-root-bot/src/main/java/com/microsoft/bot/sample/dialogrootbot/dialogs/MainDialog.java?range=260-279&highlight=18)]

### [Python](#tab/python)

**dialog-root-bot/dialogs/main_dialog.py**

The `MainDialog` class derives from `ComponentDialog`.
In addition to conversation state, the dialog needs the root bot's app ID and references to the skill conversation ID factory, the skill HTTP client, and the skills configuration objects.

The dialog constructor checks its input parameters, adds skills dialogs, adds prompt and waterfall dialogs for managing conversation flow outside the skill, and creates a property accessor for tracking the active skill, if any.

The constructor calls `AddSkillDialogs`, a helper method, to create a `SkillDialog` for each skill that is included in the configuration file, as read from the configuration file into a `SkillConfiguration` object.

[!code-python[_add_skill_dialogs](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=237-261&highlight=25)]

---

### Select a skill

In its first step, the main dialog prompts the user for which skill they'd like to call, and uses the "SkllPrompt" choice prompt to get the answer. (This bot defines only one skill.)

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

[!code-csharp[SelectSkillStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=99-114&highlight=15)]

#### [JavaScript](#tab/js)

**dialogRootBot/dialogs/mainDialog.js**

[!code-javascript[selectSkillStep](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=84-99&highlight=15)]

#### [Java](#tab/java)

**DialogRootBot\Dialogs\MainDialog.java**

[!code-java[selectSkillStepAsync](~/../botbuilder-samples/samples/java_springboot/81.skills-skilldialog/dialog-root-bot/src/main/java/com/microsoft/bot/sample/dialogrootbot/dialogs/MainDialog.java?range=147-168&highlight=21)]

#### [Python](#tab/python)

**dialog-root-bot/dialogs/main_dialog.py**

[!code-python[_select_skill_step](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=155-182&highlight=28)]

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

[!code-csharp[SelectSkillActionStepAsync, GetSkillActions, SkillActionPromptValidator](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=116-136,221-238,137-148)]

#### [JavaScript](#tab/js)

**dialogRootBot/dialogs/mainDialog.js**

[!code-javascript[selectSkillActionStep, getSkillActions, skillActionPromptValidator](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=101-121,192-209,266-276)]

#### [Java](#tab/java)

**DialogRootBot\Dialogs\MainDialog.java**

[!code-java[selectSkillActionStep](~/../botbuilder-samples/samples/java_springboot/81.skills-skilldialog/dialog-root-bot/src/main/java/com/microsoft/bot/sample/dialogrootbot/dialogs/MainDialog.java?range=171-195)]

[!code-java[getSkillActions](~/../botbuilder-samples/samples/java_springboot/81.skills-skilldialog/dialog-root-bot/src/main/java/com/microsoft/bot/sample/dialogrootbot/dialogs/MainDialog.java?range=283-297)]

[!code-java[skillActionPromptValidator](~/../botbuilder-samples/samples/java_springboot/81.skills-skilldialog/dialog-root-bot/src/main/java/com/microsoft/bot/sample/dialogrootbot/dialogs/MainDialog.java?range=97-106)]

#### [Python](#tab/python)

**dialog-root-bot/dialogs/main_dialog.py**

[!code-python[_select_skill_action_step, _get_skill_actions, and _skill_action_prompt_validator](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=155-182,277-292,262-276)]

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

[!code-csharp[CallSkillActionStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=150-175&highlight=10,19,25)]

#### [JavaScript](#tab/js)

**dialogRootBot/dialogs/mainDialog.js**

[!code-javascript[callSkillActionStep](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=123-147&highlight=10,18,24)]

#### [Java](#tab/java)

**DialogRootBot\Dialogs\MainDialog.java**

[!code-java[callSkillActionStep](~/../botbuilder-samples/samples/java_springboot/81.skills-skilldialog/dialog-root-bot/src/main/java/com/microsoft/bot/sample/dialogrootbot/dialogs/MainDialog.java?range=198-224)]

#### [Python](#tab/python)

**dialog-root-bot/dialogs/main_dialog.py**

[!code-python[_call_skill_action_step](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=184-209&highlight=13-15,20,26)]

---

### Summarize the skill result

In the last step, the main dialog:

1. If the skill returned a value, display the result to the user.
1. Clears the active skill from dialog state.
1. Removes the active skill property from conversation state.
1. Restarts itself (the main dialog).

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

[!code-csharp[FinalStepAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=177-198&highlight=9-11)]

#### [JavaScript](#tab/js)

**dialogRootBot/dialogs/mainDialog.js**

[!code-javascript[finalStep](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=149-170&highlight=9-11)]

#### [Java](#tab/java)

**DialogRootBot\Dialogs\MainDialog.java**

[!code-java[FinalStepAsync](~/../botbuilder-samples/samples/java_springboot/81.skills-skilldialog/dialog-root-bot/src/main/java/com/microsoft/bot/sample/dialogrootbot/dialogs/MainDialog.java?range=228-256)]

#### [Python](#tab/python)

**dialog-root-bot/dialogs/main_dialog.py**

[!code-python[_final_step](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=211-235&highlight=9-13)]

---

### Allow the user to cancel the skill

The main dialog overrides the default behavior of the _on continue dialog_ method to allow the user to cancel the current skill, if any. The method:

- If there is an active skill and the user sends an "abort" message, cancel all dialogs and queue the main dialog to restart from the beginning.
- Then, call the base implementation of the _on continue dialog_ method to continue processing the current turn.

#### [C#](#tab/cs)

**DialogRootBot\Dialogs\MainDialog.cs**

[!code-csharp[OnContinueDialogAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Dialogs/MainDialog.cs?range=82-97)]

#### [JavaScript](#tab/js)

**dialogRootBot/dialogs/mainDialog.js**

[!code-javascript[onContinueDialog](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/dialogs/mainDialog.js?range=70-82)]

#### [Java](#tab/java)

**DialogRootBot\Dialogs\MainDialog.java**

[!code-java[onContinueDialog](~/../botbuilder-samples/samples/java_springboot/81.skills-skilldialog/dialog-root-bot/src/main/java/com/microsoft/bot/sample/dialogrootbot/dialogs/MainDialog.java?range=131-139)]

#### [Python](#tab/python)

**dialog-root-bot/dialogs/main_dialog.py**

[!code-python[on_continue_dialog](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/dialogs/main_dialog.py?range=111-127)]

---

## Activity handler logic

Since skill logic for each turn is handled by a main dialog, the activity handler looks much like it would for other dialog samples.

### [C#](#tab/cs)

**DialogRootBot\Bots\RootBot.cs**

[!code-csharp[class definition](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Bots/RootBot.cs?range=15-16)]

[!code-csharp[constructor](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Bots/RootBot.cs?range=18-25)]

[!code-csharp[OnTurnAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogRootBot/Bots/RootBot.cs?range=27-42)]

### [JavaScript](#tab/js)

**dialogRootBot/bots/rootBot.js**

[!code-javascript[class definition](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/bots/rootBot.js?range=8)]

[!code-javascript[partial constructor plus onTurn](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/bots/rootBot.js?range=14-29)]

[!code-javascript[run](~/../botbuilder-samples/samples/javascript_nodejs/81.skills-skilldialog/dialogRootBot/bots/rootBot.js?range=47-55)]

### [Java](#tab/java)

**DialogRootBot\Bots\RootBot.java**

[!code-java[class definition](~/../botbuilder-samples/samples/java_springboot/81.skills-skilldialog/dialog-root-bot/src/main/java/com/microsoft/bot/sample/dialogrootbot/Bots/RootBot.java?range=31)]

[!code-java[constructor](~/../botbuilder-samples/samples/java_springboot/81.skills-skilldialog/dialog-root-bot/src/main/java/com/microsoft/bot/sample/dialogrootbot/Bots/RootBot.java?range=35-38)]

[!code-java[onTurn](~/../botbuilder-samples/samples/java_springboot/81.skills-skilldialog/dialog-root-bot/src/main/java/com/microsoft/bot/sample/dialogrootbot/Bots/RootBot.java?range=40-53)]

### [Python](#tab/python)

**dialog-root-bot/bots/root_bot.py**

[!code-python[class definition, constructor, and on_turn](~/../botbuilder-samples/samples/python/81.skills-skilldialog/dialog-root-bot/bots/root_bot.py?range=16-36)]

---

## Service registration

The services needed to use a skill dialog are the same as those needed for a skill consumer in general.
See how to [implement a skill consumer](skill-implement-consumer.md) for a discussion of the required services.

## Test the root bot

You can test the skill consumer in the Emulator as if it were a normal bot; however, you need to run both the skill and skill consumer bots at the same time. See how to [use dialogs within a skill](skill-actions-in-dialogs.md) for information on how to configure the skill.

Download and install the latest [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md).

1. Run the dialog skill bot and dialog root bot locally on your machine. If you need instructions, refer to the sample's `README` for [**C#**](https://github.com/microsoft/BotBuilder-Samples/blob/main/samples/csharp_dotnetcore/81.skills-skilldialog/DialogSkillBot/wwwroot/manifest/dialogchildbot-manifest-1.0.json), [**JavaScript**](https://github.com/microsoft/BotBuilder-Samples/blob/main/samples/javascript_nodejs/81.skills-skilldialog/dialogSkillBot/manifest/dialogchildbot-manifest-1.0.json), [**Java**](https://github.com/microsoft/BotBuilder-Samples/blob/main/samples/java_springboot/81.skills-skilldialog/dialog-skill-bot/src/main/webapp/manifest/echoskillbot-manifest-1.0.json), [**Python**](https://github.com/microsoft/BotBuilder-Samples/blob/main/samples/python/81.skills-skilldialog/dialog-skill-bot/wwwroot/manifest/dialogchildbot-manifest-1.0.json).
1. Use the Emulator to test the bot.
   - When you first join the conversation, the bot displays a welcome message and asks you what skill you would like to call. The skill bot for this sample has just one skill.
   - Select **DialogSkillBot**.
1. Then, the bot asks you to choose an action for the skill. Choose "BookFlight".
   1. Answer the prompts.
   1. The skill completes, and the root bot displays the booking details before prompting again for the skill you'd like to call.
1. Select **DialogSkillBot** again and "BookFlight".
   1. Answer the first prompt, then enter "abort" to interrupt the skill.
   1. The root bot cancels the skill and prompts for the skill you'd like to call.

### More about debugging

[!INCLUDE [skills-about-debugging](../includes/skills-about-debugging.md)]

## Additional information

See how to [implement a skill consumer](skill-implement-consumer.md) for how to implement a skill consumer in general.

<!-- [Use middleware in a skill consumer](skill-middleware-in-consumer.md) describes how to avoid some common problems. -->

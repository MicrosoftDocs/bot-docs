---
title: Connect a bot to Cortana | Microsoft Docs
description: Learn how to configure a bot for access through the Cortana interface.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 12/13/2017

---
# Connect a bot to Cortana

Cortana is a speech-enabled channel that can send and receive voice messages in addition to textual conversation. A bot intended to connect to Cortana should be designed for speech as well as text. A Cortana *skill* is a bot that can be invoked using a Cortana client. Publishing a bot adds the bot to the list of available skills. 

To add the Cortana channel, open the bot in the [Azure Portal](https://portal.azure.com/), click the **Channels** blade, and then click **Cortana**.

![Add Cortana channel](~/media/channels/cortana-addchannel.png)

## General bot information
All fields marked with an asterisk (*) are required. Bots must be published on the Bot Framework before they can be connected to Cortana.

> [!NOTE]
> Currently, general bot information is only available as **READONLY** on the Azure portal. 
> These fields can now be managed from the **Store Workspace** - see [documentation](https://help.knowledge.store/ux_overview/workspace/index.html) for details

![Provide general information](~/media/channels/cortana_generalInfo-readonly.png)

### Skill Icon 
Select a custom icon to represent this bot.
This icon is displayed in the Cortana canvas when this skill is invoked and anywhere skills are discoverable, such as the Microsoft store. The image must be a PNG, 60 x 60 pixels, and no larger than 30kb.

### Display Name
The name of this skill as displayed to the user at the top of Cortana's visual UI.

### Invocation Name 
The name that Cortana will recognize and use to invoke this skill when spoken aloud by the user.
See the [Invocation Name Guidelines][invocation] for more information on how to choose this phrase.

### Skill description 
Description of the skill. This is used where skills are discoverable, like the Microsoft [Store](https://help.knowledge.store/ux_overview/store/index.html?highlight=store).

### Short description 
This summary will be used to describe the skill in Cortana’s Notebook.


## Register a bot to the Knowledge Store

Scroll down to **Discovery and Management** 

![Register the Bot](~/media/channels/discovery_and_management_register_1.png)

> [!NOTE]
> After you click **Register**, you will be redirected to the Store portal. 

Click **Yes** to allow the Store to access your account information

![App Access Info](~/media/channels/app_access_info.png)

Check the box to agree to the Store’s Terms of Use, and then click **Accept**.

![Terms of Use Accept](~/media/channels/terms_of_use_accept.png)

Upon acceptance, you'll have the option of going through the **First Time User Experience** tutorial. At this point, your bot will be connected to the cortana channel. 

![First Time User Tutorial](~/media/channels/cortana_skills-slide-01.png)

The Store workspace allows you to create and publish new skills for Cortana, explore existing skills created by other developers, access free samples and tutorials to build botlets, skills and more. See the [Store Overview](https://help.knowledge.store/ux_overview/store/index.html) for more details. 

## Enable speech recognition priming
If your bot uses a Language Understanding (LUIS) app, register the LUIS application ID. 

Click the **Settings** tab and then under **Configuration**, enter the LUIS application ID in the **Speech recognition priming with LUIS** text box. This helps your bot recognize spoken utterances that are defined in your LUIS model.
![Enable speech recognition priming](~/media/channels/cortana-speech-luis-priming.png)

For more information on how to configure speech priming, [click here](https://docs.microsoft.com/en-us/bot-framework/bot-service-manage-speech-priming).

## Next steps
* [Getting Started with Skills](https://help.knowledge.store/getting_started/index.html)
* [Cortana Skills Kit](https://aka.ms/CortanaSkillsDocs)
* [Enable debugging](https://aka.ms/cortana-enable-debug)
* [Publish a Cortana skill][publish]
* [Making BotFramework Bots Chainable and Semantic](https://help.knowledge.store/tutorials_code_samples/bf_chainable_skills/index.html)
* [Speech Support in Bot Framework](https://blog.botframework.com/2017/06/26/speech-to-text/)


[invocation]: https://aka.ms/cortana-invocation-guidelines
[publish]: https://aka.ms/cortana-publish
[connected]: https://aka.ms/CortanaSkillsBotConnectedAccount
[CortanaEntity]: https://aka.ms/lgvcto

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

Open the bot on the [Bot Framework Portal](https://dev.botframework.com/), click the **Channels** tab, and then click **Cortana**.

![Add Cortana channel](~/media/channels/cortana-addchannel.png)

## General bot information
All fields marked with an asterisk (*) are required. Bots must be published on the Bot Framework before they can be connected to Cortana.

![Provide general information](~/media/channels/cortana_generalInfo.png)

### Skill Icon 
Select a custom icon to represent this bot.
This icon is displayed in the Cortana canvas when this skill is invoked and anywhere skills are discoverable, such as the Microsoft store. The image must be a PNG, 60 x 60 pixels, and no larger than 30kb.

### Display Name
The name of this skill as displayed to the user at the top of Cortana's visual UI.

### Invocation Name 
The name that Cortana will recognize and use to invoke this skill when spoken aloud by the user.
See the [Invocation Name Guidelines][invocation] for more information on how to choose this phrase.

### Skill description 
Describe the skill. This is used where bots are discoverable, like the Microsoft store.

### Short description 
This summary will be used to describe the skill in Cortana’s Notebook.

> [!NOTE]
> For basic skills, this is all that is required. Click **Connect to Cortana** to complete the connection.
> For more advanced skills, add a connected account or configure access to user profile and contextual information.

## Manage user identity 
The default value is *Disabled*. If *Enabled*, the user will be required to log in before using this skill.
Cortana can manage this skill's user identity and tokens across devices if an OAuth2 identity provider is added as a [connected service][connected].

> [!NOTE]
> Only **Auth Code Grant** authentication is supported. **Implicit Grant** is currently not supported. 

![Define user profile request](~/media/channels/cortana-connectedAccount.png)

|Field|Description|
|-----|-----|
| Icon | The icon to display when the sign-in form is displayed. |
| Account Name | The name to be shown when the sign-in form is displayed. |
| Client ID for third-party services | The client id that Cortana needs as part of the OAuth flow. |
| Client secret/password for third party services | The client secret Cortana needs as part of the OAuth flow. |
| List of scopes | A list of scopes Cortana needs as part of the skill execution flow. |
| Authorization URL | The URI Cortana will call to authorize the user. |
| Token URL | The URI Cortana will call to fetch tokens. |
| Client authentication scheme | The mechanism by which Cortana will pass a bearer token. |
| Token option | How Cortana should obtain tokens. If unsure, select POST. |
| Intranet toggle | Select if this skill’s connected service requires *intranet* access to authenticate users. If unsure, leave this blank.)

## Request user profile data
Cortana provides access to several different types of user profile information, which is useful in customizing the bot for different users. 

![Define user profile request](~/media/channels/cortana-AddUserProfile.png)

1. Click **Add a user profile request**.
2. Click **Select Data** and then select the user profile information from the drop-down list. 
3. Add a **Friendly name** to identify this information in the `UserInfo` [entity][CortanaEntity].
4. Click **Remove** to remove this request for data or click **Add a user profile request** again to define another request.

Click **Save**. The bot will be deployed automatically as a Cortana skill.

## Enable speech recognition priming
If your bot uses a Language Understanding (LUIS) app, register the LUIS application ID. 

Click the **Settings** tab and then under **Configuration**, enter the LUIS application ID in the **Speech recognition priming with LUIS** text box. This helps your bot recognize spoken utterances that are defined in your LUIS model.
![Enable speech recognition priming](~/media/channels/cortana-speech-luis-priming.png)

## Next steps
* [Cortana Skills Kit](https://aka.ms/CortanaSkillsDocs)
* [Enable debugging](https://aka.ms/cortana-enable-debug)
* [Publish a Cortana skill][publish]

[invocation]: https://aka.ms/cortana-invocation-guidelines
[publish]: https://aka.ms/cortana-publish
[connected]: https://aka.ms/CortanaSkillsBotConnectedAccount
[CortanaEntity]: https://aka.ms/lgvcto
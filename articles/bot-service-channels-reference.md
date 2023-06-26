---
title: Channels reference
description: View reference information on bot channels. See which channels generate which events and support which cards. See the number of actions that channels support.
keywords: channels reference, bot builder channels, bot framework channels
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: bot-service
ms.topic: reference
ms.date: 11/10/2022
---

# Channels reference

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article outlines channel support for various Bot Framework features:

- The activity types each channel can send or receive.
- The card types each channel can display, including Adaptive Cards.
- Card action and suggested action support on each channel.
- A general classification of the different activity types.

For detailed information about the structure of activities and cards at the protocol level,
see the Bot Framework [activity](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md)
and [card](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-cards.md) schemas.

Adaptive Cards is a separate technology. For more information, see [adaptivecards.io](https://adaptivecards.io/).

## Activity support by channel

The following table indicates whether a given channel can send a given activity type to your bot.
Within the table, the following terms have the following meanings.

| Term         | Meaning                                               |
|:-------------|:------------------------------------------------------|
| Yes          | The bot can receive this activity from the channel.   |
| No           | The bot can't receive this activity from the channel. |
| Undetermined | Currently undetermined.                               |

| Channel                                     | Contact relation update | Conversation update | End of conversation | Event        | Installation update | Invoke | Message | Message reaction | Message update | Message delete | Typing |
|:--------------------------------------------|:------------------------|:--------------------|:--------------------|:-------------|:--------------------|:-------|:--------|:-----------------|:---------------|:---------------|:-------|
| Alexa                                       | No                      | No                  | Yes                 | Yes          | No                  | No     | Yes     | No               | No             | No             | No     |
| Azure Communication Services Chat (preview) | No                      | Yes                 | No                  | Yes          | No                  | No     | Yes     | No               | Yes            | Yes            | Yes    |
| Direct Line                                 | No                      | Yes                 | Yes                 | Yes          | Yes                 | No     | Yes     | No               | No             | No             | Yes    |
| Direct Line Speech                          |                         |                     |                     |              |                     |        | Yes     |                  |                |                |        |
| Email                                       | No                      | No                  | No                  | Undetermined | No                  | No     | Yes     | No               | No             | No             | No     |
| Facebook                                    | No                      | Yes                 | No                  | Yes          | No                  | No     | Yes     | Yes              | No             | No             | No     |
| GroupMe                                     | No                      | Yes                 | No                  | Undetermined | No                  | No     | Yes     | No               | No             | No             | No     |
| LINE                                        | No                      | Yes                 | No                  | Yes          | No                  | No     | Yes     | No               | No             | No             | No     |
| Microsoft Teams                             | No                      | Yes                 | No                  | Undetermined | No                  | Yes    | Yes     | Yes              | Yes            | Yes            | No     |
| Omnichannel                                 |                         |                     |                     |              |                     |        | Yes     |                  |                |                |        |
| Outlook (preview)                           |                         |                     |                     |              |                     |        | Yes     |                  |                |                |        |
| Search (preview)                            |                         |                     |                     |              |                     |        | Yes     |                  |                |                |        |
| Slack                                       | No                      | Yes                 | No                  | Undetermined | No                  | No     | Yes     | No               | Yes            | Yes            | No     |
| Telegram                                    | No                      | Yes                 | No                  | Undetermined | No                  | No     | Yes     | No               | Yes            | Undetermined   | No     |
| Twilio (SMS)                                | No                      | No                  | No                  | Undetermined | No                  | No     | Yes     | No               | No             | No             | No     |
| Web Chat                                    | No                      | Yes                 | Yes                 | Yes          | Yes                 | No     | Yes     | No               | No             | No             | Yes    |

Support for `event` and `invoke` activities varies by the activity's name and varies by channel.

## Card support by channel

The following table indicates whether a given channel can render a given card type.
Even if a channel can render a card type, the channel may not support all features on the card.
Before releasing your bot, test the behavior of each card your bot can send.

Within the table, the following terms have the following meanings.

| Term    | Meaning                                                                                                                                                              |
|:--------|:---------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Yes     | The card is supported on this channel; however, any given channel may only support a subset of card actions or may limit the number of actions allowed on each card. |
| No      | The card isn't supported on this channel.                                                                                                                            |
| Partial | Partial support. This channel might not display the card if the card contains inputs or buttons. Level of support varies by channel.                                 |
| Image   | Card is converted to image.                                                                                                                                          |
| Text    | Card is converted to unformatted text. Links may not be clickable, images may not display, and media may not be playable. Level of support varies by channel.        |

| Channel                                     | Adaptive Card  | Animation card | Audio card | Hero card | Receipt card | Sign-in card | Thumbnail card | Video card |
|:--------------------------------------------|:---------------|:---------------|:-----------|:----------|:-------------|:-------------|:---------------|:-----------|
| Alexa                                       | No             | No             | No         | Yes       | No           | Yes          | No             | No         |
| Azure Communication Services Chat (preview) | Yes* | Yes            | Yes        | Yes       | Yes          | Yes          | Yes            | Yes        |
| Email                                       | Image          | Text           | Text       | Yes       | Yes          | Yes          | Yes            | Text       |
| Facebook                                    | Image, partial | Yes            | Yes        | Yes       | Yes          | Yes          | Yes            | Yes        |
| GroupMe                                     | Image          | Text           | Text       | Text      | Text         | Text         | Text           | Text       |
| LINE                                        | Image, partial | Yes            | Text       | Yes       | Yes          | Yes          | Yes            | Text       |
| Microsoft Teams                             | Yes            | No             | No         | Yes       | Yes          | Yes          | Yes            | No         |
| Omnichannel                                 |                |                |            |           |              |              |                |            |
| Outlook (preview)                           |                |                |            |           |              |              |                |            |
| Search (preview)                            |                |                |            |           |              |              |                |            |
| Slack                                       | Image          | Yes            | Text       | Text      | Yes          | Yes          | Text           | Text       |
| Telegram                                    | Image, partial | Yes            | Text       | Yes       | Yes          | Yes          | Yes            | Yes        |
| Twilio (SMS)                                | Image          | Text           | No         | Text      | Text         | Text         | Text           | No         |
| Web Chat                                    | Yes            | Yes            | Yes        | Yes       | Yes          | Yes          | Yes            | Yes        |

> [!NOTE]
> 
> 

> [!NOTE]
>
> - The Direct Line channel technically supports all cards, but it's up to the client to implement them.
> - Kik converts card actions to suggested actions.
> - *For Azure Communication Services Chat, Adaptive cards are only supported within Azure Communication Services use cases, and not for Azure Communication Services to Teams use cases.

## Card action support by channel

The following table shows the maximum number of suggested actions and card actions that a given channel supports.
A value of "None" indicates that the action type isn't supported in the channel.

| Channel                                     | Suggested actions | Card actions |
|:--------------------------------------------|:-----------------:|:------------:|
| Alexa                                       | None              | None         |
| Azure Communication Services Chat (preview) |                   |              |
| Direct Line                                 | 100               | 100          |
| Direct Line Speech                          | 100               | 100          |
| Email                                       | None              | None         |
| Facebook                                    | 11                | 3            |
| GroupMe                                     | None              | None         |
| LINE                                        | 13                | 99           |
| Microsoft Teams                             | None              | 3            |
| Omnichannel                                 |                   |              |
| Outlook (preview)                           |                   |              |
| Search (preview)                            |                   |              |
| Slack                                       | None              | 100          |
| Telegram                                    | 100               | 100          |
| Twilio (SMS)                                | None              | None         |
| Web Chat                                    | 100               | 100          |

- For more information about card actions, see [Process events within rich cards](v4sdk/bot-builder-howto-add-media-attachments.md#process-events-within-rich-cards) in the _Add media to messages_ article.
- For more information about suggested actions, see how to [Use buttons for input](v4sdk/bot-builder-howto-add-suggested-actions.md).

## Activity categories

Activities can be split into separate categories.
For a detailed description of each type of activity, and the information that each type of activity contains, see the [Bot Framework activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md).

### Welcome

This category includes the `conversationUpdate` and `contactRelationUpdate` activities.

- Many channels send conversation update activities.
  - Often, bot _welcome_ behavior is triggered by the conversation update activity.
    However, producing reliable welcome behavior might require the use of conversation or user state.
- Some channels send contact relation update activities.
  - If your bot uses these channels, you may need to include logic for this activity in your bot's welcome behavior.

### Conversational

This category includes the `message`, `messageReaction`, and `endOfConversation` activities.

- All channels can send and receive message activities.
  - For bots that use dialogs, message activities should generally be passed into the dialog.
- Some channels can send and receive message reaction activities.
  - Depending on the design of your bot, you might pass message reaction activities into a dialog.
  - Message reaction activities reference previous messages by ID.
- End of conversation activities signal the end of a conversation from the sender's perspective.
  - End of conversation activities are used in bot-to-bot communication for skills.

> [!TIP]
> A _message reaction_ includes things like a _thumbs up_ on a previous comment.
> They can happen out of order, so they can be thought of as similar to buttons.
> This activity type can be sent by the Teams channel.

### Message update and delete

This category includes the `messageUpdate` and `messageDelete` activities.

- Teams supports the message update and delete activities.

### Application extensibility

This category includes the `event` and `invoke` activities.
The meaning of the activity is defined by its `name` field, which is meaningful within the scope of a channel.

- An application that owns both the client and server can use event activities to communicate programmatic information between the client and server.
  - Event activities, like most activity types, are asynchronous.
  - Direct Line and Web Chat use event activities as an extensibility mechanism.
- Invoke activities are specific to an application and not something a client would define.
  - Invoke activities, unlike other activity types, are synchronous.
    (Invoke is currently the only activity type that triggers a request-reply behavior on the bot.)
  - Microsoft Teams uses invoke activities and defines a few Teams-specific invoke activities.

#### Authentication

For the OAuth prompt to work with dialogs, the `TeamsVerification` invoke activity must be forwarded to the dialog.

### Uncategorized

The `installationUpdate`, `typing`, and `handoff` activities don't meaningfully fit into the other categories.

- Installation update activities represent an installation or uninstallation of a bot within an organizational unit of a channel.
- Typing activities represent ongoing input from a user or a bot.
- Handoff activities request or signal a change in focus between elements inside a bot. The _handoff_ activity is different from an _event_ activity that has the name "handoff".

### Out of use (includes payment specific invoke)

These activity types are no longer in use:

- `deleteUserData`
- `handoff`
- `ping`
- `Address` invoke
- `PaymentRequest` invoke

## Additional information

All channels can send and receive `message` activities.

> [!TIP]
> When adding support for a channel to your bot, familiarize yourself with the channel's developer docs.
> Each channel has different limitations on various aspects of a conversation. Some of the differences include:
>
> - How much time the bot has to handle each HTTP request.
> - Whether a bot can send an activity that's not in response to a specific user activity.
> - How many messages the bot can send within a given time frame.
> - How a card renders and which cards are supported.

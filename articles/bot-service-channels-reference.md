---
title: Channels reference
description: View reference information on bot channels. See which channels generate which events and support which cards. See the number of actions that channels support.
keywords: channels reference, bot builder channels, bot framework channels
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 07/06/2021
---

# Channels reference

## Categorized activities by channel

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

The following tables show what events (activities on the wire) can come from which channels.

These symbols are used in the tables:

| Symbol   | Meaning                                                   |
|:--------:|:----------------------------------------------------------|
| &#x2714; | The bot should expect to receive this activity.           |
| &#x274c; | The bot should **never** expect to receive this activity. |
| &#x2753; | Currently undetermined whether the bot can receive this.  |

Activities can meaningfully be split into separate categories. For each category, we have a table of possible activities.
See the [Bot Framework activity schema](https://github.com/Microsoft/botframework-sdk/blob/main/specs/botframework-activity/botframework-activity.md) for a detailed description of each type of activity, and the information that each type of activity contains.

### Conversational

| \                 | Direct Line | Direct Line (Web Chat) | Email    | Facebook | GroupMe  | Kik      | Teams    | Slack    | Skype    | Skype Business | Telegram | Twilio   |
|:------------------|:-----------:|:----------------------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------------:|:--------:|:--------:|
| `message`         | &#x2714;    | &#x2714;               | &#x2714; | &#x2714; | &#x2714; | &#x2714; | &#x2714; | &#x2714; | &#x2714; | &#x2714;       | &#x2714; | &#x2714; |
| `messageReaction` | &#x274c;    | &#x274c;               | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x2714; | &#x274c; | &#x274c; | &#x274c;       | &#x274c; | &#x274c; |

- All channels send message activities.
- If your bot uses a dialog, forward message activities to the dialog.
- Message reactions don't need to be forwarded to the dialog, even though they're very much part of the conversation.
- There are logically two types of message reactions: added and removed.

> [!TIP]
> Message reactions are things like a _thumbs up_ on a previous comment. They can happen out of order, and can be thought of as similar to buttons.

### Welcome

| \                       | Direct Line | Direct Line (Web Chat) | Email    | Facebook | GroupMe  | Kik      | Teams    | Slack    | Skype    | Skype Business | Telegram | Twilio   |
|:------------------------|:-----------:|:----------------------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------------:|:--------:|:--------:|
| `conversationUpdate`    | &#x2714;    | &#x2714;               | &#x274c; | &#x2753; | &#x2714; | &#x2714; | &#x2714; | &#x2714; | &#x274c; | &#x274c;       | &#x2714; | &#x274c; |
| `contactRelationUpdate` | &#x274c;    | &#x274c;               | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x2714; | &#x2714;       | &#x274c; | &#x274c; |

- It is common for channels to send conversation update activities.
- The main types of conversation updates are conversation members added and members removed.
- Some channels send the conversation update when the bot is added to a conversation, and some send it after the first message sent to the bot.
- To produce a reliable _Welcome_ behavior, include user state in your bots welcome logic.

### Application extensibility

| \                             | Direct Line | Direct Line (Web Chat) | Email    | Facebook | GroupMe  | Kik      | Teams    | Slack    | Skype    | Skype Business | Telegram | Twilio   |
|:------------------------------|:-----------:|:----------------------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------------:|:--------:|:--------:|
| `event`                       | &#x2714;    | &#x2714;               | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753;       | &#x2753; | &#x2753; |
| name = `CreateConversation`   | &#x2753;    | &#x2753;               | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753;       | &#x2753; | &#x2753; |
| name = `ContinueConversation` | &#x2753;    | &#x2753;               | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753;       | &#x2753; | &#x2753; |

- Event activities communicate programmatic information from a client or channel to a bot. The meaning of an event activity is defined by the `name` field, which is meaningful within the scope of a channel.
- An application that owns both the client and server can chose to tunnel their own events through the service using event activities.

#### Microsoft Teams

- Along with other activity types, Microsoft Teams defines a few Teams-specific `invoke` activities. See [How Microsoft Teams bots work](v4sdk/bot-builder-basics-teams.md) for more information.
- Invoke activities communicate programmatic information from a client or channel to a bot, and have a corresponding return payload for use within the channel. The meaning of an invoke activity is defined by the `name` field, which is meaningful within the scope of a channel.

### Message update

| \               | Direct Line | Direct Line (Web Chat) | Email    | Facebook | GroupMe  | Kik      | Teams    | Slack    | Skype    | Skype Business | Telegram | Twilio   |
|:----------------|:-----------:|:----------------------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------------:|:--------:|:--------:|
| `messageUpdate` | &#x274c;    | &#x274c;               | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x2714; | &#x2753; | &#x274c; | &#x274c;       | &#x274c; | &#x274c; |
| `messageDelete` | &#x274c;    | &#x274c;               | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x2714; | &#x2753; | &#x274c; | &#x274c;       | &#x274c; | &#x274c; |

- Message update is currently supported by Teams.

### OAuth

| \        | Direct Line | Direct Line (Web Chat) | Email    | Facebook | GroupMe  | Kik      | Teams    | Slack    | Skype    | Skype Business | Telegram | Twilio   |
|:---------|:-----------:|:----------------------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------------:|:--------:|:--------:|
| `invoke` | &#x2714;    | &#x2714;               | &#x274c; | &#x2753; | &#x2753; | &#x2753; | &#x274c; | &#x2753; | &#x2753; | &#x2753;       | &#x2753; | &#x2753; |

> [!IMPORTANT]
> For dialogs and OAuth prompts to work, you must forward the following invoke activities to the dialog:
>
> - `signin/verifyState`
> - `signin/tokenExchange`
> - `tokens/response`

### Uncategorized

| \                    | Direct Line | Direct Line (Web Chat) | Email    | Facebook | GroupMe  | Kik      | Teams    | Slack    | Skype    | Skype Business | Telegram | Twilio   |
|:---------------------|:-----------:|:----------------------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------------:|:--------:|:--------:|
| `endOfConversation`  | &#x2714;    | &#x2714;               | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c;       | &#x274c; | &#x274c; |
| `installationUpdate` | &#x2714;    | &#x2714;               | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c;       | &#x274c; | &#x274c; |
| `typing`             | &#x2714;    | &#x2714;               | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c;       | &#x274c; | &#x274c; |

### Out of use

- `deleteUserData`
- `handoff`
- `invoke`: payment request
- `invoke`: address
- `ping`

> [!NOTE]
> The handoff activity is different from the handoff-to-human scenario. See [Transition conversations from bot to human](bot-service-design-pattern-handoff-human.md) for more information.

## Summary of activities supported per channel

### Direct Line

- `conversationUpdate`
- `event`
  - `CreateConversation`
  - `ContinueConversation`
- `invoke`
  - `signin/tokenExchange`
  - `signin/verifyState`
  - `tokens/response`
- `message`

### Email

- `message`

### Facebook

- `invoke`
  - `tokens/response`
- `message`

### GroupMe

- `conversationUpdate`
- `invoke`
  - `tokens/response`
- `message`

### Kik

- `conversationUpdate`
- `invoke`
  - `tokens/response`
- `message`

### Teams

- `conversationUpdate`
- `invoke`
- `message`
- `messageDelete`
- `messageReaction`
- `messageUpdate`

### Slack

- `conversationUpdate`
- `invoke`
  - `tokens/response`
- `message`

### Skype

- `contactRelationUpdate`
- `invoke`
  - `tokens/response`
- `message`

### Skype Business

- `contactRelationUpdate`
- `invoke`
  - `tokens/response`
- `message`

### Telegram

- `conversationUpdate`
- `invoke`
  - `tokens/response`
- `message`

### Twilio

- `message`

## Summary table all activities to all channels

| \                        | Direct Line | Direct Line (Web Chat) | Email    | Facebook | GroupMe  | Kik      | Teams    | Slack    | Skype    | Skype Business | Telegram | Twilio   |
|:-------------------------|:-----------:|:----------------------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------:|:--------------:|:--------:|:--------:|
| `contactRelationUpdate`  | &#x274c;    | &#x274c;               | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x2714; | &#x2714;       | &#x274c; | &#x274c; |
| `conversationUpdate`     | &#x2714;    | &#x2714;               | &#x274c; | &#x2753; | &#x2714; | &#x2714; | &#x2714; | &#x2714; | &#x274c; | &#x274c;       | &#x2714; | &#x274c; |
| `endOfConversation`      | &#x2714;    | &#x2714;               | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c;       | &#x274c; | &#x274c; |
| `event`                  | &#x2714;    | &#x2714;               | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753;       | &#x2753; | &#x2753; |
| - `CreateConversation`   | &#x2753;    | &#x2753;               | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753;       | &#x2753; | &#x2753; |
| - `ContinueConversation` | &#x2753;    | &#x2753;               | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753; | &#x2753;       | &#x2753; | &#x2753; |
| `installationUpdate`     | &#x2714;    | &#x2714;               | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x274c;       | &#x274c; | &#x274c; |
| `message`                | &#x2714;    | &#x2714;               | &#x2714; | &#x2714; | &#x2714; | &#x2714; | &#x2714; | &#x2714; | &#x2714; | &#x2714;       | &#x2714; | &#x2714; |
| `messageDelete`          | &#x274c;    | &#x274c;               | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x2714; | &#x2753; | &#x274c; | &#x274c;       | &#x274c; | &#x274c; |
| `messageReaction`        | &#x274c;    | &#x274c;               | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x2714; | &#x274c; | &#x274c; | &#x274c;       | &#x274c; | &#x274c; |
| `messageUpdate`          | &#x274c;    | &#x274c;               | &#x274c; | &#x274c; | &#x274c; | &#x274c; | &#x2714; | &#x2753; | &#x274c; | &#x274c;       | &#x274c; | &#x274c; |
| `typing`                 | &#x2714;    | &#x2714;               | &#x274c; | &#x2714; | &#x274c; | &#x274c; | &#x2714; | &#x2714; | &#x274c; | &#x274c;       | &#x2714; | &#x274c; |

Support for `event` and `invoke` activities varies by the activity's name and varies by channel.

## Action support by channel

The following table shows the maximum number of suggested actions and card actions that are supported in each channel.  The &#x274c; indicates that the action is not supported at all in the specified channel.

| \                 | Direct Line | Direct Line (Web Chat) | Email    | Facebook | GroupMe  | Kik | Line | Teams    | Slack    | Skype | Skype Business | Telegram | Twilio   |
|:------------------|:-----------:|:----------------------:|:--------:|:--------:|:--------:|:---:|:----:|:--------:|:--------:|:-----:|:--------------:|:--------:|:--------:|
| Suggested actions | 100         | 100                    | &#x274c; | 10       | &#x274c; | 20  | 13   | &#x274c; | &#x274c; | 10    | &#x274c;       | 100      | &#x274c; |
| Card actions      | 100         | 100                    | &#x274c; | 3        | &#x274c; | 20  | 99   | 3        | 100      | 3     | &#x274c;       | &#x274c; | &#x274c; |

For more information about the numbers shown in the above table, refer to channel support code listed [here](https://github.com/microsoft/botbuilder-dotnet/blob/master/libraries/Microsoft.Bot.Builder.Dialogs/Choices/Channel.cs).

For more information on _suggested actions_, see how to [Use button for input](./v4sdk/bot-builder-howto-add-suggested-actions.md) article.

For more information on _card actions_, see the [Send a hero card](./v4sdk/bot-builder-howto-add-media-attachments.md#send-a-hero-card) section of the _Add media to messages_ article.

## Card support by channel

| Channel         | Adaptive Card     | Animation card | Audio card | Hero card | Receipt card | Sign-in card | Thumbnail card | Video card |
|:---------------:|:-----------------:|:--------------:|:----------:|:---------:|:------------:|:-----------:|:--------------:|:----------:|
| Email           | &#x1f5bc;         | &#x1f4c4;      | &#x1f4c4;  | &#x2714;  | &#x2714;     | &#x2714;    | &#x2714;       | &#x1f4c4;  |
| Facebook        | &#x26a0;&#x1f5bc; | &#x2714;       | &#x274c;   | &#x2714;  | &#x2714;     | &#x2714;    | &#x2714;       | &#x274c;   |
| GroupMe         | &#x1f5bc;         | &#x1f4c4;      | &#x1f4c4;  | &#x1f4c4; | &#x1f4c4;    | &#x1f4c4;   | &#x1f4c4;      | &#x1f4c4;  |
| Kik             | &#x1f5bc;         | &#x2714;       | &#x2714;   | &#x274c;  | &#x1f4c4;    | &#x274c;    | &#x2714;       | &#x1f4c4;  |
| Line            | &#x26a0;&#x1f5bc; | &#x2714;       | &#x1f4c4;  | &#x2714;  | &#x2714;     | &#x2714;    | &#x2714;       | &#x1f4c4;  |
| Microsoft Teams | &#x2714;          | &#x274c;       | &#x274c;   | &#x2714;  | &#x2714;     | &#x2714;    | &#x2714;       | &#x274c;   |
| Skype           | &#x274c;          | &#x2714;       | &#x2714;   | &#x2714;  | &#x2714;     | &#x2714;    | &#x2714;       | &#x2714;   |
| Slack           | &#x1f5bc;         | &#x2714;       | &#x1f4c4;  | &#x1f4c4; | &#x2714;     | &#x2714;    | &#x1f4c4;      | &#x1f4c4;  |
| Telegram        | &#x26a0;&#x1f5bc; | &#x2714;       | &#x1f4c4;  | &#x2714;  | &#x2714;     | &#x2714;    | &#x2714;       | &#x2714;   |
| Twilio          | &#x1f5bc;         | &#x1f4c4;      | &#x274c;   | &#x1f4c4; | &#x1f4c4;    | &#x1f4c4;   | &#x1f4c4;      | &#x274c;   |
| Web Chat        | &#x2714;          | &#x2714;       | &#x2714;   | &#x2714;  | &#x2714;     | &#x2714;    | &#x2714;       | &#x2714;   |

> [!NOTE]
> The Direct Line channel technically supports all cards, but it's up to the client to implement them.

- &#x2714;: Supported - card is supported fully with the exception that some channels only support a subset of card actions or may limit the number of actions allowed on each card. Varies by channel.
- &#x26a0;: Partial support - card may not be displayed at all if it contains inputs or buttons. Varies by channel.
- &#x274c;: No support
- &#x1f5bc;: Card is converted to an image
- &#x1f4c4;: Card is converted to unformatted text - links may not be clickable, images may not display, and media may not be playable. Varies by channel.

These categories are intentionally broad and don't fully explain how every card feature is supported in each channel due to the many possible combinations of cards, features, and channels. Use this table as a base reference, but test each of your cards in the desired channel.

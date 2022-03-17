---
title: Build Microsoft Teams bots with Bot Framework SDK
description: A continuation of the article on How bots work, specific to Microsoft Teams bots.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: overview
ms.date: 07/19/2021
---

# How Microsoft Teams bots work

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article builds on what you learned in [How bots work](bot-builder-basics.md) and [Event-driven conversations](bot-activity-handler-concept.md); you should be familiar with these articles before you continue.

The primary difference in bots developed for Microsoft Teams is in how activities are handled. The _Teams activity handler_ derives from the _activity handler_ and processes Teams-specific activity types before processing more general activity types.

## Teams activity handler

To create a bot for Teams, derive your bot from the _Teams activity handler_ class. When such a bot receives an activity, it routes the activity through various *activity handlers*. The initial, base handler is the *turn handler*, and it routes the activity to a handler based on the activity's type. The *turn handler* calls the handler that is designed to handle the specific type of activity that was received. The _Teams activity handler_ class is derived from the _activity handler_ class. In addition to the activity types that the _activity handler_ can process, the Teams activity handler class includes additional handlers for Teams-specific activities.

A bot that derives from the Teams activity handler is similar to a bot that derives directly from the activity handler class.
However, Teams includes additional information in `conversationUpdate` activities and sends Teams-specific `invoke` and `event` activities.

<!-- The Teams activity handler is found at:
- https://github.com/microsoft/botbuilder-dotnet/blob/main/libraries/Microsoft.Bot.Builder/Teams/TeamsActivityHandler.cs
- https://github.com/microsoft/botbuilder-js/blob/main/libraries/botbuilder/src/teamsActivityHandler.ts
- https://github.com/microsoft/botbuilder-python/blob/main/libraries/botbuilder-core/botbuilder/core/teams/teams_activity_handler.py
- https://github.com/microsoft/botbuilder-java/blob/main/libraries/bot-builder/src/main/java/com/microsoft/bot/builder/teams/TeamsActivityHandler.java
-->

### [C#](#tab/csharp)

When your Teams activity handler&ndash;bot receives a message activity, its turn handler routes the incoming message activity to its `OnMessageActivityAsync` handler, similar to how an activity handler&ndash;based bot would. However, when your Teams bot receives a conversation update activity, the Teams version of the `OnConversationUpdateActivityAsync` handler processes the activity.

There is no base implementation for most of the Teams-specific activity handlers. You will need to override these handlers and provide appropriate logic for your bot.

### [JavaScript](#tab/javascript)

When your Teams activity handler&ndash;bot receives a message activity, its turn handler routes the incoming message activity to its `onMessage` handler, similar to how an activity handler&ndash;based bot would. However, when your Teams bot receives a conversation update activity, the Teams version of the `dispatchConversationUpdateActivity` handler processes the activity.

There is no base implementation for most of the Teams-specific activity handlers. You will need to override these handlers and provide appropriate logic for your bot.

When overriding these Teams-specific activity handlers, define your bot logic, then **be sure to call `next()` at the end**. By calling `next()`, you ensure that the next handler is run.

### [Java](#tab/java)

When your Teams activity handler&ndash;bot receives a message activity, its turn handler routes the incoming message activity to its `onMessageActivity` handler, similar to how an activity handler&ndash;based bot would. However, when your Teams bot receives a conversation update activity, the Teams version of the `onConversationUpdateActivity` handler processes the activity.

There is no base implementation for most of the Teams-specific activity handlers. You will need to override these handlers and provide appropriate logic for your bot.

### [Python](#tab/python)

When your Teams activity handler&ndash;bot receives a message activity, its turn handler routes the incoming message activity to its `on_message_activity` handler, similar to how an activity handler&ndash;based bot would. However, when your Teams bot receives a conversation update activity, the Teams version of the `on_conversation_update_activity` handler processes the activity.

There is no base implementation for most of the Teams-specific activity handlers. You will need to override these handlers and provide appropriate logic for your bot.

---

All of the activity handlers described in the [activity handling](bot-activity-handler-concept.md#activity-handling) section of the _Event-driven conversations using an activity handler_ article will continue to work as they do with a non-Teams bot, except for handling the members added and removed activities, these activities will be different in the context of a team, where the new member is added to the team as opposed to a message thread. For more information, see [Teams conversation update activities](#teams-conversation-update-activities).

To implement your logic for these Teams-specific activity handlers, you will override methods in your bot.

## Teams-bot logic

The bot logic processes incoming activities from one or more of your bots channels and generates outgoing activities in response.  This is still true of bot derived from the Teams activity handler class, which first checks for Teams activities, then passes all other activities to the Bot Framework's activity handler.

### Teams installation update activities

Add a handler for the _installation update_ event to let your bot:

- Send an introductory message when it is installed on a conversation thread.
- Clean up user and thread data when it is uninstalled from a thread.

See [Installation update event](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#installation-update-event) in the Teams docs for more information.

### Teams conversation update activities

The following table lists the Teams events that generate a _conversation update_ activity in a bot.
The Microsoft Teams [Conversation update events](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events) article describes how to use each of these events.

<!-- Ignoring the teamHardDeleted event type for now, which is not currently documented in Teams. -->
<!-- Teams docs have a questionable description of `teamRestored`. (https://github.com/MicrosoftDocs/msteams-docs/issues/2830) Using what I think makes sense for now. -->

### [C#](#tab/csharp)

Below is a list of all of the Teams activity handlers called from the `OnConversationUpdateActivityAsync` method of the _Teams_ activity handler.

| EventType | Handler | Condition | Teams documentation |
|:-|:-|:-|:-|
| channelCreated | `OnTeamsChannelCreatedAsync` | Sent whenever a new channel is created in a team your bot is installed in. | [Channel created](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-created). |
| channelDeleted | `OnTeamsChannelDeletedAsync` | Sent whenever a channel is deleted in a team your bot is installed in. | [Channel deleted](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-deleted). |
| channelRenamed | `OnTeamsChannelRenamedAsync` | Sent whenever a channel is renamed in a team your bot is installed in. | [Channel renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-renamed). |
| channelRestored | `OnTeamsChannelRestoredAsync` | Sent whenever a channel that was previously deleted is restored in a team that your bot is already installed in. | [Channel restored](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-restored). |
| membersAdded | `OnTeamsMembersAddedAsync` | By default, calls the `ActivityHandler.OnMembersAddedAsync` method. Sent the first time your bot is added to a conversation and every time a new user is added to a team or group chat that your bot is installed in. | [Team members added](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-members-added). |
| membersRemoved | `OnTeamsMembersRemovedAsync` | By default, calls the `ActivityHandler.OnMembersRemovedAsync` method. Sent if your bot is removed from a team and every time any user is removed from a team that your bot is a member of. | [Team members removed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-members-removed). |
| teamArchived | `OnTeamsTeamArchivedAsync` | Sent when the team your bot is installed in is archived. | [Team archived](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-archived). |
| teamDeleted | `OnTeamsTeamDeletedAsync` | Sent when the team your bot is in has been deleted. | [Team deleted](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-deleted). |
| teamRenamed | `OnTeamsTeamRenamedAsync` | Sent when the team your bot is in has been renamed. | [Team renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-renamed). |
| teamRestored | `OnTeamsTeamRestoredAsync` | Sent when a previously deleted team your bot is in is restored. | [Team restored](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-restored). |
| teamUnarchived | `OnTeamsTeamUnarchivedAsync` | Sent when the team your bot is installed in is unarchived.| [Team unarchived](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-unarchived). |

### [JavaScript](#tab/javascript)

Developers may handle conversation update activities sent from Microsoft Teams via two methods:

1. To pass in a callback, use methods that begin with `on` _and_ end with `Event` (for example, the `onTeamsMembersAddedEvent` method).
1. When creating a derived class, override methods that begin with `on` and _don't_ end with `Event` (for example, the `onTeamsMembersAdded` method).

Developers should use only one of these options: either 1 or 2, and not _both_ for the same activity. Meaning, developers should either pass a callback to the `onTeamsMembersAddedEvent` method *or* override the `onTeamsMembersAdded` method in a derived class, and not do both.

#### Methods for registering a callback

Below is a list of all of the Teams activity emitters called from the `dispatchConversationUpdateActivity` method of the _Teams_ activity handler.

| EventType | Registration method | Condition | Teams documentation |
|:-|:-|:-|:-|
| channelCreated | `onTeamsChannelCreatedEvent` | Sent whenever a new channel is created in a team your bot is installed in. | [Channel created](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-created). |
| channelDeleted | `onTeamsChannelDeletedEvent` | Sent whenever a channel is deleted in a team your bot is installed in. | [Channel deleted](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-deleted). |
| channelRenamed | `onTeamsChannelRenamedEvent` | Sent whenever a channel is renamed in a team your bot is installed in. | [Channel renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-renamed). |
| channelRestored | `onTeamsChannelRestoredEvent` | Sent whenever a channel that was previously deleted is restored in a team that your bot is already installed in. | [Channel restored](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-restored). |
| membersAdded | `onTeamsMembersAddedEvent` | Sent the first time your bot is added to a conversation and every time a new user is added to a team or group chat that your bot is installed in. | [Team members added](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-members-added). |
| membersRemoved | `onTeamsMembersRemovedEvent` | Sent if your bot is removed from a team and every time any user is removed from a team that your bot is a member of. | [Team members removed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-members-removed). |
| teamArchived | `onTeamsTeamArchivedEvent` | Sent when the team your bot is installed in is archived. | [Team archived](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-archived). |
| teamDeleted | `onTeamsTeamDeletedEvent` | Sent when the team your bot is in has been deleted. | [Team deleted](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-deleted). |
| teamRenamed | `onTeamsTeamRenamedEvent` | Sent when the team your bot is in has been renamed. | [Team renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-renamed). |
| teamRestored | `onTeamsTeamrestoredEvent` | Sent when a previously deleted team your bot is in is restored. | [Team restored](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-restored). |
| teamUnarchived | `onTeamsTeamUnarchivedEvent` | Sent when the team your bot is installed in is unarchived.| [Team unarchived](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-unarchived). |

#### Methods to override in a derived class

Below is a list of all of the Teams activity handlers that can be overridden to handle Teams conversation update activities.

| EventType | Handler | Condition | Teams documentation |
|:-|:-|:-|:-|
| channelCreated | `onTeamsChannelCreated` | Sent whenever a new channel is created in a team your bot is installed in. | [Channel created](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-created). |
| channelDeleted | `onTeamsChannelDeleted` | Sent whenever a channel is deleted in a team your bot is installed in. | [Channel deleted](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-deleted). |
| channelRenamed | `onTeamsChannelRenamed` | Sent whenever a channel is renamed in a team your bot is installed in. | [Channel renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-renamed). |
| channelRestored | `onTeamsChannelRestored` | Sent whenever a channel that was previously deleted is restored in a team that your bot is already installed in. | [Channel restored](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-restored). |
| membersAdded | `onTeamsMembersAdded` | By default, calls the `ActivityHandler.onMembersAdded` method. Sent the first time your bot is added to a conversation and every time a new user is added to a team or group chat that your bot is installed in. | [Team members added](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-members-added). |
| membersRemoved | `onTeamsMembersRemoved` | By default, calls the `ActivityHandler.onMembersRemoved` method. Sent if your bot is removed from a team and every time any user is removed from a team that your bot is a member of. | [Team members removed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-members-removed). |
| teamArchived | `onTeamsTeamArchived` | Sent when the team your bot is installed in is archived. | [Team archived](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-archived). |
| teamDeleted | `onTeamsTeamDeleted` | Sent when the team your bot is in has been deleted. | [Team deleted](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-deleted). |
| teamRenamed | `onTeamsTeamRenamed` | Sent when the team your bot is in has been renamed. | [Team renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-renamed). |
| teamRestored | `onTeamsTeamRestored` | Sent when a previously deleted team your bot is in is restored. | [Team restored](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-restored). |
| teamUnarchived | `onTeamsTeamUnarchived` | Sent when the team your bot is installed in is unarchived.| [Team unarchived](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-unarchived). |

### [Java](#tab/java)

Below is a list of all of the Teams activity handlers called from the `onConversationUpdateActivity` method of the _Teams_ activity handler.

| EventType | Handler | Condition | Teams documentation |
|:-|:-|:-|:-|
| channelCreated | `onTeamsChannelCreated` | Sent whenever a new channel is created in a team your bot is installed in. | [Channel created](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-created). |
| channelDeleted | `onTeamsChannelDeleted` | Sent whenever a channel is deleted in a team your bot is installed in. | [Channel deleted](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-deleted). |
| channelRenamed | `onTeamsChannelRenamed` | Sent whenever a channel is renamed in a team your bot is installed in. | [Channel renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-renamed). |
| channelRestored | `onTeamsChannelRestored` | Sent whenever a channel that was previously deleted is restored in a team that your bot is already installed in. | [Channel restored](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-restored). |
| membersAdded | `onTeamsMembersAdded` | By default, calls the `ActivityHandler.onMembersAdded` method. Sent the first time your bot is added to a conversation and every time a new user is added to a team or group chat that your bot is installed in. | [Team members added](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-members-added). |
| membersRemoved | `onTeamsMembersRemoved` | By default, calls the `ActivityHandler.onMembersRemoved` method. Sent if your bot is removed from a team and every time any user is removed from a team that your bot is a member of. | [Team members removed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-members-removed). |
| teamArchived | `onTeamsTeamArchived` | Sent when the team your bot is installed in is archived. | [Team archived](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-archived). |
| teamDeleted | `onTeamsTeamDeleted` | Sent when the team your bot is in has been deleted. | [Team deleted](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-deleted). |
| teamRenamed | `onTeamsTeamRenamed` | Sent when the team your bot is in has been renamed. | [Team renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-renamed). |
| teamRestored | `onTeamsTeamRestored` | Sent when a previously deleted team your bot is in is restored. | [Team restored](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-restored). |
| teamUnarchived | `onTeamsTeamUnarchived` | Sent when the team your bot is installed in is unarchived.| [Team unarchived](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-unarchived). |

### [Python](#tab/python)

Below is a list of all of the Teams activity handlers called from the `on_conversation_update_activity` method of the _Teams_ activity handler.

| EventType | Handler | Condition | Teams documentation |
|:-|:-|:-|:-|
| channelCreated | `on_teams_channel_created` | Sent whenever a new channel is created in a team your bot is installed in. | [Channel created](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-created). |
| channelDeleted | `on_teams_channel_deleted` | Sent whenever a channel is deleted in a team your bot is installed in. | [Channel deleted](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-deleted). |
| channelRenamed | `on_teams_channel_renamed` | Sent whenever a channel is renamed in a team your bot is installed in. | [Channel renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-renamed). |
| channelRestored | `on_teams_channel_restored` | Sent whenever a channel that was previously deleted is restored in a team that your bot is already installed in. | [Channel restored](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-restored). |
| membersAdded | `on_teams_members_added` | By default, calls the base class `on_members_added_activity` method. Sent the first time your bot is added to a conversation and every time a new user is added to a team or group chat that your bot is installed in. | [Team members added](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-members-added). |
| membersRemoved | `on_teams_members_removed` | By default, calls the base class `on_members_removed_activity` method. Sent if your bot is removed from a team and every time any user is removed from a team that your bot is a member of. | [Team members removed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-members-removed). |
| teamArchived | `on_teams_team_archived` | Sent when the team your bot is installed in is archived. | [Team archived](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-archived). |
| teamDeleted | `on_teams_team_deleted` | Sent when the team your bot is in has been deleted. | [Team deleted](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-deleted). |
| teamRenamed | `on_teams_team_renamed` | Sent when the team your bot is in has been renamed. | [Team renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-renamed). |
| teamRestored | `on_teams_team_restored` | Sent when a previously deleted team your bot is in is restored. | [Team restored](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-restored). |
| teamUnarchived | `on_teams_team_unarchived` | Sent when the team your bot is installed in is unarchived.| [Team unarchived](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-unarchived). |

---

### Teams event activities

The following table lists the Teams-specific event activities Teams sends to a bot.
The event activities listed are for conversational bots in Teams.

### [C#](#tab/csharp)

These are the Teams-specific event activity handlers called from the `OnEventActivityAsync` _Teams_ activity handler.

| Event types                            | Handler                    | Description                                             |
|:---------------------------------------|:---------------------------|:--------------------------------------------------------|
| application/vnd.microsoft.meetingEnd   | `OnTeamsMeetingEndAsync`   | The bot is associated with a meeting that just ended.   |
| application/vnd.microsoft.meetingStart | `OnTeamsMeetingStartAsync` | The bot is associated with a meeting that just started. |

### [JavaScript](#tab/javascript)

These are the Teams-specific event activity handlers called from the `onEventActivity` _Teams_ activity handler.

| Event types                            | Handler               | Description                                             |
|:---------------------------------------|:----------------------|:--------------------------------------------------------|
| application/vnd.microsoft.meetingEnd   | `onTeamsMeetingEnd`   | The bot is associated with a meeting that just ended.   |
| application/vnd.microsoft.meetingStart | `onTeamsMeetingStart` | The bot is associated with a meeting that just started. |

### [Java](#tab/java)

These are the Teams-specific event activity handlers called from the `onEventActivity` _Teams_ activity handler.

| Event types                            | Handler               | Description                                             |
|:---------------------------------------|:----------------------|:--------------------------------------------------------|
| application/vnd.microsoft.meetingEnd   | `onTeamsMeetingEnd`   | The bot is associated with a meeting that just ended.   |
| application/vnd.microsoft.meetingStart | `onTeamsMeetingStart` | The bot is associated with a meeting that just started. |

### [Python](#tab/python)

These are the Teams-specific event activity handlers called from the `on_event_activity` _Teams_ activity handler.

| Event types                            | Handler                        | Description                                             |
|:---------------------------------------|:-------------------------------|:--------------------------------------------------------|
| application/vnd.microsoft.meetingEnd   | `on_teams_meeting_end_event`   | The bot is associated with a meeting that just ended.   |
| application/vnd.microsoft.meetingStart | `on_teams_meeting_start_event` | The bot is associated with a meeting that just started. |

---

### Teams invoke activities

The following table lists the Teams-specific invoke activities Teams sends to a bot.
The invoke activities listed are for conversational bots in Teams. The Bot Framework SDK also supports invokes specific to messaging extensions. For more information, see the Teams [What are messaging extensions](/microsoftteams/platform/messaging-extensions/what-are-messaging-extensions) article.

### [C#](#tab/csharp)

Here is a list of all of the Teams activity handlers called from the `OnInvokeActivityAsync` _Teams_ activity handler:

| Invoke types                    | Handler                               | Description                       |
|:--------------------------------|:--------------------------------------|:----------------------------------|
| actionableMessage/executeAction | `OnTeamsO365ConnectorCardActionAsync` | Teams O365 Connector Card Action. |
| CardAction.Invoke               | `OnTeamsCardActionInvokeAsync`        | Teams Card Action Invoke.         |
| fileConsent/invoke              | `OnTeamsFileConsentAcceptAsync`       | Teams File Consent Accept.        |
| fileConsent/invoke              | `OnTeamsFileConsentAsync`             | Teams File Consent.               |
| fileConsent/invoke              | `OnTeamsFileConsentDeclineAsync`      | Teams File Consent.               |
| signin/verifyState              | `OnTeamsSigninVerifyStateAsync`       | Teams Sign in Verify State.       |
| task/fetch                      | `OnTeamsTaskModuleFetchAsync`         | Teams Task Module Fetch.          |
| task/submit                     | `OnTeamsTaskModuleSubmitAsync`        | Teams Task Module Submit.         |

### [JavaScript](#tab/javascript)

Here is a list of all of the Teams activity handlers called from the `onInvokeActivity` _Teams_ activity handler:

| Invoke types                    | Handler                              | Description                       |
|:--------------------------------|:-------------------------------------|:----------------------------------|
| actionableMessage/executeAction | `handleTeamsO365ConnectorCardAction` | Teams O365 Connector Card Action. |
| CardAction.Invoke               | `handleTeamsCardActionInvoke`        | Teams Card Action Invoke.         |
| fileConsent/invoke              | `handleTeamsFileConsentAccept`       | Teams File Consent Accept.        |
| fileConsent/invoke              | `handleTeamsFileConsent`             | Teams File Consent.               |
| fileConsent/invoke              | `handleTeamsFileConsentDecline`      | Teams File Consent.               |
| signin/verifyState              | `handleTeamsSigninVerifyState`       | Teams Sign in Verify State.       |
| task/fetch                      | `handleTeamsTaskModuleFetch`         | Teams Task Module Fetch.          |
| task/submit                     | `handleTeamsTaskModuleSubmit`        | Teams Task Module Submit.         |

### [Java](#tab/java)

Here is a list of all of the Teams activity handlers called from the `onInvokeActivity` _Teams_ activity handler:

| Invoke types                    | Handler                               | Description                       |
|:--------------------------------|:--------------------------------------|:----------------------------------|
| fileConsent/invoke              | `onTeamsFileConsent`                  | Teams File Consent.               |
| actionableMessage/executeAction | `onTeamsO365ConnectorCardAction`      | Teams O365 Connector Card Action. |
| task/fetch                      | `onTeamsTaskModuleFetch`              | Teams Task Module Fetch.          |
| task/submit                     | `onTeamsTaskModuleSubmit`             | Teams Task Module Submit.         |
| tab/fetch                       | `onTeamsTabFetch`                     | Teams Tab Fetch.                  |
| tab/submit                      | `onTeamsTabSubmit`                    | Teams Tab Submit.                 |
| CardAction.Invoke               | `onTeamsCardActionInvoke`             | Teams Card Action Invoke.         |
| fileConsent/invoke              | `onTeamsFileConsentAccept`            | Teams File Consent Accept.        |
| fileConsent/invoke              | `onTeamsFileConsentDecline`           | Teams File Consent.               |
| signin/verifyState              | `onTeamsSigninVerifyState`            | Teams Sign in Verify State.       |

### [Python](#tab/python)

Here is a list of all of the Teams activity handlers called from the `on_invoke_activity` _Teams_ activity handler:

| Invoke types                    | Handler                               | Description                       |
|:--------------------------------|:--------------------------------------|:----------------------------------|
| actionableMessage/executeAction | `on_teams_o365_connector_card_action` | Teams O365 Connector Card Action. |
| CardAction.Invoke               | `on_teams_card_action_invoke`         | Teams Card Action Invoke.         |
| fileConsent/invoke              | `on_teams_file_consent_accept`        | Teams File Consent Accept.        |
| fileConsent/invoke              | `on_teams_file_consent`               | Teams File Consent.               |
| fileConsent/invoke              | `on_teams_file_consent_decline`       | Teams File Consent Decline.       |
| signin/verifyState              | `on_teams_signin_verify_state`        | Teams Sign in Verify State.       |
| task/fetch                      | `on_teams_task_module_fetch`          | Teams Task Module Fetch.          |
| task/submit                     | `on_teams_task_module_submit`         | Teams Task Module Submit.         |

---

## Next steps

For building Teams bots, refer to Microsoft Teams Developer [documentation](/microsoftteams/platform/bots/how-to/create-a-bot-for-teams).

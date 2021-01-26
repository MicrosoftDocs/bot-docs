---
title: How bots for Microsoft Teams work
description: A continuation of the article on How bots work specific to Microsoft Teams bots
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: overview
ms.service: bot-service
ms.date: 01/25/2021
---

# How Microsoft Teams bots work

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This is an introduction that builds on what you learned in the article [How bots work](https://docs.microsoft.com/azure/bot-service/bot-builder-basics), you should be familiar with that article before reading this.

The primary differences in bots developed for Microsoft Teams is in how activities are handled. The Microsoft Teams activity handler derives from the Bot Framework's activity handler to route all teams activities before allowing any non-Teams-specific activities to be handled.

## Teams Activity handlers

Just like any other bot, when a bot is designed for Microsoft Teams receives an activity, it passes it on to its *activity handlers*. Under the covers, there is one base handler called the *turn handler*, that all activities are routed through. The *turn handler* calls the required activity handler to handle whatever type of activity was received. Where a bot designed for Microsoft Teams differs is that it is derived from a _Teams activity handler_ class that is derived from the Bot Framework's _activity handler_ class.  The Teams activity handler class includes various Microsoft Teams-specific activity handlers that will be discussed in this article.

### [C#](#tab/csharp)

As with any bot created using the Microsoft Bot Framework, if the bot receives a message activity, the turn handler would see that incoming activity and send it to the `OnMessageActivityAsync` activity handler. This functionality remains the same, however if the bot receives a conversation update activity, the turn handler would see that incoming activity and send it to the `OnConversationUpdateActivityAsync` _Teams_ activity handler that will first check for any Teams-specific events and pass it along to the Bot Framework's activity handler if none are found.

In the Teams activity handler class there are two primary Teams activity handlers, `OnConversationUpdateActivityAsync` that routes all conversation update activities, and `OnInvokeActivityAsync` that routes all Teams invoke activities.

There is no base implementation for most of these Teams-specific activity handlers. To implement your logic for these Teams-specific activity handlers add the logic that you want in your override.

### [JavaScript](#tab/javascript)

As with any bot created using the Microsoft Bot Framework, if the bot receives a message activity, the turn handler would see that incoming activity and send it to the `onMessage` activity handler. This functionality remains the same, however if the bot receives a conversation update activity, the turn handler would see that incoming activity and send it to the `dispatchConversationUpdateActivity` _Teams_ activity handler that will first check for any Teams-specific events and pass it along to the Bot Framework's activity handler if none are found.

In the Teams activity handler class there are two primary Teams activity handlers, `dispatchConversationUpdateActivity` that routes all conversation update Activities, and `onInvokeActivity` that routes all Teams invoke  activities.

When overriding these Teams-specific activity handlers, define your bot logic, then **be sure to call `next()` at the end**. By calling `next()` you ensure that the next handler is run.

### [Python](#tab/python)

As with any bot created using the Microsoft Bot Framework, if the bot receives a message activity, the turn handler would see that incoming activity and send it to the `on_message_activity` activity handler. This functionality remains the same, however if the bot receives a conversation update activity, the turn handler would see that incoming activity and send it to the `on_conversation_update_activity` _Teams_ activity handler that will first check for any Teams-specific events and pass it along to the Bot Framework's activity handler if none are found.

In the Teams activity handler class there are two primary Teams activity handlers, `on_conversation_update_activity` that routes all conversation update Activities, and `on_invoke_activity` that routes all Teams invoke  activities.

There is no base implementation for most of these Teams-specific activity handlers. To implement your logic for these Teams-specific activity handlers add the logic that you want in your override.

---

All of the activity handlers described in the [activity handling](bot-activity-handler-concept.md#activity-handling) section of the _Event-driven conversations using an activity handler_ article will continue to work as they do with a non-Teams bot, with the exception of handling the members added and removed activities, these will be different in the context of a team, where the new member is added to the team as opposed to a message thread. See the _Teams conversation update activities_ table in the [Teams-bot logic](#teams-bot-logic) section for more details.

To implement your logic for these Teams-specific activity handlers, you will override these methods in your bot as shown in the [Teams-bot logic](#teams-bot-logic) section below.

## Teams-bot logic

The bot logic processes incoming activities from one or more of your bots channels and generates outgoing activities in response.  This is still true of bot derived from the Teams activity handler class, which first checks for Teams activities, then passes all other activities to the Bot Framework's activity handler.

### [C#](#tab/csharp)

### Teams conversation update activities

Below is a list of all of the Teams activity handlers called from the `OnConversationUpdateActivityAsync` _Teams_ activity handler. The [Conversation update events](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events) article describes how to use each of these events in a bot.

| Event | Handler | Description |
| :-- | :-- | :-- |
| channelCreated | `OnTeamsChannelCreatedAsync` | Override this to handle a Teams channel being created. For more information see [Channel created](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-created). |
| channelDeleted | `OnTeamsChannelDeletedAsync` | Override this to handle a Teams channel being deleted. For more information see [Channel deleted](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-deleted). |
| channelRenamed | `OnTeamsChannelRenamedAsync` | Override this to handle a Teams channel being renamed. For more information see [Channel renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-renamed). |
| teamRenamed | `OnTeamsTeamRenamedAsync` | `return Task.CompletedTask;` Override this to handle a Teams Team being Renamed. For more information see [Team renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-renamed). |
| MembersAdded | `OnTeamsMembersAddedAsync` | Calls the `OnMembersAddedAsync` method in `ActivityHandler`. Override this to handle members joining a team. For more information see [Team member added](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-members-added).|
| MembersRemoved | `OnTeamsMembersRemovedAsync` | Calls the `OnMembersRemovedAsync` method in `ActivityHandler`. Override this to handle members leaving a team. For more information see [Team member removed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-member-removed).|

### Teams invoke activities

Here is a list of all of the Teams activity handlers called from the `OnInvokeActivityAsync` _Teams_ activity handler:

| Invoke types                    | Handler                              | Description                                                  |
| :-----------------------------  | :----------------------------------- | :----------------------------------------------------------- |
| CardAction.Invoke               | `OnTeamsCardActionInvokeAsync`       | Teams Card Action Invoke. |
| fileConsent/invoke              | `OnTeamsFileConsentAcceptAsync`      | Teams File Consent Accept. |
| fileConsent/invoke              | `OnTeamsFileConsentAsync`            | Teams File Consent. |
| fileConsent/invoke              | `OnTeamsFileConsentDeclineAsync`     | Teams File Consent. |
| actionableMessage/executeAction | `OnTeamsO365ConnectorCardActionAsync` | Teams O365 Connector Card Action. |
| signin/verifyState              | `OnTeamsSigninVerifyStateAsync`      | Teams Sign in Verify State. |
| task/fetch                      | `OnTeamsTaskModuleFetchAsync`        | Teams Task Module Fetch. |
| task/submit                     | `OnTeamsTaskModuleSubmitAsync`       | Teams Task Module Submit. |

The invoke activities listed above are for conversational bots in Teams. The Bot Framework SDK also supports invokes specific to messaging extensions. For more information see [What are messaging extensions](/microsoftteams/platform/messaging-extensions/what-are-messaging-extensions)

### [JavaScript](#tab/javascript)

### Teams conversation update activities

Developers may handle conversation update activities sent from Microsoft Teams via two methods:

1. To pass in a callback, use methods that begin with `on` _and_ end with `Event` (for example, the `onTeamsMembersAddedEvent` method).
1. When creating a derived class, override methods that begin with `on` and _don't_ end with `Event` (for example, the `onTeamsMembersAdded` method).

Developers should use only one of these options: either 1 or 2, and not _both_ for the same activity. Meaning, developers should either pass a callback to the `onTeamsMembersAddedEvent` method *or* override the `onTeamsMembersAdded` method in a derived class, and not do both.

**Methods for passing in a callback**

Below is a list of all of the Teams activity handlers called from the `dispatchConversationUpdateActivity` _Teams_ activity handler. The [Conversation update events](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events) article describes how to use each of these events in a bot.

| Event | Handler | Description |
| :-- | :-- | :-- |
| channelCreated | `OnTeamsChannelCreatedEvent` | Pass in a callback to this method to handle a Teams channel being created. For more information see [Channel created](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-created). |
| channelDeleted | `OnTeamsChannelDeletedEvent` | Pass in a callback to this method to handle a Teams channel being deleted. For more information see [Channel deleted](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-deleted).|
| channelRenamed | `OnTeamsChannelRenamedEvent` | Pass in a callback to this method to handle a Teams channel being renamed. For more information see [Channel renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-renamed). |
| teamRenamed | `OnTeamsTeamRenamedEvent` | `return Task.CompletedTask;` Pass in a callback to this method to handle a Teams Team being Renamed. For more information see [Team renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-renamed). |
| MembersAdded | `OnTeamsMembersAddedEvent` | Calls the `OnMembersAddedEvent` method in `ActivityHandler`. Pass in a callback to this method to handle members joining a team. For more information see [Team member added](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#Team-Member-Added). |
| MembersRemoved | `OnTeamsMembersRemovedEvent` | Calls the `OnMembersRemovedEvent` method in `ActivityHandler`. Pass in a callback to this method to handle members leaving a team. For more information see [Team member removed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#Team-Member-Removed). |

**Methods to override in a derived class**

Below is a list of all of the Teams activity handlers that can be overridden to handle Teams Conversation Update activities.

| Method | Handler | Description |
| :-- | :-- | :-- |
| channelCreated | `OnTeamsChannelCreated` | Override this to handle a Teams channel being created.|
| channelDeleted | `OnTeamsChannelDeleted` | Override this to handle a Teams channel being deleted.|
| channelRenamed | `OnTeamsChannelRenamed` | Override this to handle a Teams channel being renamed.|
| teamRenamed | `OnTeamsTeamRenamed` | `return Task.CompletedTask;` Override this to handle a Teams team being renamed.|
| MembersAdded | `OnTeamsMembersAdded` | Calls the `OnMembersAddedEvent` method in `ActivityHandler`. Override this to handle members joining a team.|
| MembersRemoved | `OnTeamsMembersRemoved` | Calls the `OnMembersRemovedEvent` method in `ActivityHandler`. Override this to handle members leaving a team.|

### Teams invoke  activities

Here is a list of all of the Teams activity handlers called from the `onInvokeActivity` _Teams_ activity handler:

| Invoke types                    | Handler                              | Description                                                  |
| :-----------------------------  | :----------------------------------- | :----------------------------------------------------------- |
| CardAction.Invoke               | `handleTeamsCardActionInvoke`       | Teams Card Action Invoke. |
| fileConsent/invoke              | `handleTeamsFileConsentAccept`      | Teams File Consent Accept. |
| fileConsent/invoke              | `handleTeamsFileConsent`            | Teams File Consent. |
| fileConsent/invoke              | `handleTeamsFileConsentDecline`     | Teams File Consent. |
| actionableMessage/executeAction | `handleTeamsO365ConnectorCardAction` | Teams O365 Connector Card Action. |
| signin/verifyState              | `handleTeamsSigninVerifyState`      | Teams Sign in Verify State. |
| task/fetch                      | `handleTeamsTaskModuleFetch`        | Teams Task Module Fetch. |
| task/submit                     | `handleTeamsTaskModuleSubmit`       | Teams Task Module Submit. |

The invoke activities listed above are for conversational bots in Teams. The Bot Framework SDK also supports invokes specific to messaging extensions. For more information see [What are messaging extensions](/microsoftteams/platform/messaging-extensions/what-are-messaging-extensions)

### [Python](#tab/python)

### Teams conversation update activities

Below is a list of all of the Teams activity handlers called from the `on_conversation_update_activity` _Teams_ activity handler. The [Conversation update events](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events) article describes how to use each of these events in a bot.

| Event | Handler | Description |
| :-- | :-- | :-- |
| channelCreated | `on_teams_channel_created` | Override this to handle a Teams channel being created. For more information see [Channel created](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-created). |
| channelDeleted | `on_teams_channel_deleted` | Override this to handle a Teams channel being deleted. For more information see [Channel deleted](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-deleted). |
| channelRenamed | `on_teams_team_renamed_activity` | Override this to handle a Teams channel being renamed. For more information see [Channel renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#channel-renamed). |
| teamRenamed | `on_teams_channel_renamed` | `return Task.CompletedTask;` Override this to handle a Teams Team being Renamed. For more information see [Team renamed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#team-renamed). |
| MembersAdded | `on_teams_members_added` | Calls the `OnMembersAddedAsync` method in `ActivityHandler`. Override this to handle members joining a team. For more information see [Team member added](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#Team-Member-Added).|
| MembersRemoved | `on_teams_members_removed` | Calls the `OnMembersRemovedAsync` method in `ActivityHandler`. Override this to handle members leaving a team. For more information see [Team member removed](/microsoftteams/platform/bots/how-to/conversations/subscribe-to-conversation-events#Team-Member-Removed).|

### Teams invoke activities

Here is a list of all of the Teams activity handlers called from the `on_invoke_activity` _Teams_ activity handler:

| Invoke types                    | Handler                              | Description
| :-----------------------------  | :----------------------------------- | :-
| CardAction.Invoke               | `on_teams_card_action_invoke`        | Teams Card Action Invoke.
| fileConsent/invoke              | `on_teams_file_consent_accept`       | Teams File Consent Accept.
| fileConsent/invoke              | `on_teams_file_consent`              | Teams File Consent.
| fileConsent/invoke              | `on_teams_file_consent_decline`      | Teams File Consent Decline.
| actionableMessage/executeAction | `on_teams_o365_connector_card_action`| Teams O365 Connector Card Action.
| signin/verifyState              | `on_teams_signin_verify_state`       | Teams Sign in Verify State.
| task/fetch                      | `on_teams_task_module_fetch`         | Teams Task Module Fetch.
| task/submit                     | `on_teams_task_module_submit`        | Teams Task Module Submit.

The invoke activities listed above are for conversational bots in Teams. The Bot Framework SDK also supports invokes specific to messaging extensions. For more information see [What are messaging extensions](/microsoftteams/platform/messaging-extensions/what-are-messaging-extensions)

---

## Next steps

For building Teams bots, refer to Microsoft Teams Developer [documentation](/microsoftteams/platform/bots/how-to/create-a-bot-for-teams).

---
title: How bots for Microsoft Teams work
description: A continuation of the article on How bots work specific to Microsoft Teams bots
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: overview
ms.service: bot-service
ms.date: 10/31/2019
monikerRange: 'azure-bot-service-4.0'
---

# How Microsoft Teams bots work

This is an introduction that builds on what you learned in the article [How bots work](https://docs.microsoft.com/azure/bot-service/bot-builder-basics), you should be familiar with that article before reading this.

The primary differences in bots developed for Microsoft Teams is in how activities are handled. The Microsoft Teams activity handler derives from the Bot Framework's activity handler to route all teams activities before allowing any non-teams specific activities to be handled.

## Teams Activity handlers

Just like any other bot, when a bot is designed for Microsoft Teams receives an activity, it passes it on to its *activity handlers*. Under the covers, there is one base handler called the *turn handler*, that all activities are routed through. The *turn handler* calls the required activity handler to handle whatever type of activity was received. Where a bot designed for Microsoft Teams differs is that it is derived from a _Teams_ `Activity Handler` class that is derived from the Bot Framework's `Activity Handler` class.  The _Teams_ `Activity Handler` class includes various Microsoft Teams specific activity handlers that will be discussed in this article.

# [C#](#tab/csharp)

As with any bot created using the Microsoft Bot Framework, if the bot receives a message activity, the turn handler would see that incoming activity and send it to the `OnMessageActivityAsync` activity handler. This functionality remains the same, however if the bot receives a conversation update activity, the turn handler would see that incoming activity and send it to the `OnConversationUpdateActivityAsync` _Teams_ activity handler that will first check for any Teams specific events and pass it along to the Bot Framework's activity handler if none are found.

In the Teams activity handler class there are two primary Teams activity handlers, `OnConversationUpdateActivityAsync` that routes all conversation update activities, and `OnInvokeActivityAsync` that routes all Teams invoke  activities.  All of the activity handlers described in the [Bot logic](https://aka.ms/how-bots-work#bot-logic) section of the `How bots work` article will continue to work as they do with a non-Teams bot, with the exception of handling the members added and removed activities, these will be different in the context of a team, where the new member is added to the team as opposed to a message thread.  See the _Teams conversation update activities_ table in the [Bot logic](#bot-logic) section for more details.

To implement your logic for these Teams specific activity handlers, you will override these methods in your bot as shown in the [Bot logic](#bot-logic) section below. For each of these handlers, there is no base implementation, so just add the logic that you want in your override.

# [JavaScript](#tab/javascript)

As with any bot created using the Microsoft Bot Framework, if the bot receives a message activity, the turn handler would see that incoming activity and send it to the `onMessage` activity handler. This functionality remains the same, however if the bot receives a conversation update activity, the turn handler would see that incoming activity and send it to the `dispatchConversationUpdateActivity` _Teams_ activity handler that will first check for any Teams specific events and pass it along to the Bot Framework's activity handler if none are found.

In the Teams activity handler class there are two primary Teams activity handlers, `dispatchConversationUpdateActivity` that routes all conversation update Activities, and `onInvokeActivity` that routes all Teams invoke  activities.  All of the activity handlers described in the [Bot logic](https://aka.ms/how-bots-work#bot-logic) section of the `How bots work` article will continue to work as they do with a non-Teams bot, with the exception of handling the members added and removed activities, these will be different in the context of a team, where the new member is added to the team as opposed to a message thread.  See the _Teams conversation update activities_ table in the [Bot logic](#bot-logic) section for more details.

As with any bot handler, to implement your logic for the Teams handlers, you will override the methods in your bot as seen in the [Bot logic](#bot-logic) section below. For each of these handlers, define your bot logic, then **be sure to call `next()` at the end**. By calling `next()` you ensure that the next handler is run.

---

### Bot logic

The bot logic processes incoming activities from one or more of your bots channels and generates outgoing activities in response.  This is still true of bot derived from the Teams activity handler class, which first checks for Teams activities, then passes all other activities to the Bot Framework's activity handler.

# [C#](#tab/csharp)

#### Teams conversation update activities

Here is a list of all of the Teams activity handlers called from the `OnConversationUpdateActivityAsync` _Teams_ activity handler:

<!--
| Event | Handler | Description |
| :-- | :-- | :-- |
| channelCreated | `OnTeamsChannelCreatedAsync` | Override this to handle a Teams channel being created. For more information see [Channel created](https://aka.ms/azure-bot-subscribe-to-conversation-events#channel-created) in [Conversation update events](https://aka.ms/azure-bot-subscribe-to-conversation-events). |
| channelDeleted | `OnTeamsChannelDeletedAsync` | Override this to handle a Teams channel being deleted. For more information see [Channel deleted](https://aka.ms/azure-bot-subscribe-to-conversation-events#channel-deleted) in [Conversation update events](https://aka.ms/azure-bot-subscribe-to-conversation-events).|
| channelRenamed | `OnTeamsChannelRenamedAsync` | Override this to handle a Teams channel being renamed. For more information see [Channel renamed](https://aka.ms/azure-bot-subscribe-to-conversation-events#channel-renamed) in [Conversation update events](https://aka.ms/azure-bot-subscribe-to-conversation-events).|
| teamRenamed | `OnTeamsTeamRenamedAsync` | `return Task.CompletedTask;` Override this to handle a Teams Team being Renamed. For more information see [Team renamed](https://aka.ms/azure-bot-subscribe-to-conversation-events#team-renamed) in [Conversation update events](https://aka.ms/azure-bot-subscribe-to-conversation-events).|
| MembersAdded | `OnTeamsMembersAddedAsync` | Calls the `OnMembersAddedAsync` method in `ActivityHandler`. Override this to handle members joining a team. For more information see [Team member added](https://aka.ms/azure-bot-subscribe-to-conversation-events#Team-Member-Added) in [Conversation update events](https://aka.ms/azure-bot-subscribe-to-conversation-events).|
| MembersRemoved | `OnTeamsMembersRemovedAsync` | Calls the `OnMembersRemovedAsync` method in `ActivityHandler`. Override this to handle members leaving a team. For more information see [Team member removed](https://aka.ms/azure-bot-subscribe-to-conversation-events#Team-Member-Removed) in [Conversation update events](https://aka.ms/azure-bot-subscribe-to-conversation-events).|
-->


| Event | Handler | Description |
| :-- | :-- | :-- |
| channelCreated | `OnTeamsChannelCreatedAsync` | Override this to handle a Teams channel being created. |
| channelDeleted | `OnTeamsChannelDeletedAsync` | Override this to handle a Teams channel being deleted. |
| channelRenamed | `OnTeamsChannelRenamedAsync` | Override this to handle a Teams channel being renamed. |
| teamRenamed | `OnTeamsTeamRenamedAsync` | `return Task.CompletedTask;` Override this to handle a Teams Team being Renamed. |
| MembersAdded | `OnTeamsMembersAddedAsync` | Calls the `OnMembersAddedAsync` method in `ActivityHandler`. Override this to handle members joining a team. |
| MembersRemoved | `OnTeamsMembersRemovedAsync` | Calls the `OnMembersRemovedAsync` method in `ActivityHandler`. Override this to handle members leaving a team. |


#### Teams invoke  activities

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

The invoke activities listed above are for conversational bots in Teams. The Bot Framework SDK also supports invokes specific to messaging extensions. For more information see [What are messaging extensions](https://aka.ms/azure-bot-what-are-messaging-extensions)

# [JavaScript](#tab/javascript)

#### Teams conversation update activities

Here is a list of all of the Teams activity handlers called from the `dispatchConversationUpdateActivity` _Teams_ activity handler:

<!--
| Event | Handler | Description |
| :-- | :-- | :-- |
| channelCreated | `OnTeamsChannelCreatedEvent` | Override this to handle a Teams channel being created. For more information see [Channel created](https://aka.ms/azure-bot-subscribe-to-conversation-events#channel-created) in [Conversation update events](https://aka.ms/azure-bot-subscribe-to-conversation-events). |
| channelDeleted | `OnTeamsChannelDeletedEvent` | Override this to handle a Teams channel being deleted. For more information see [Channel deleted](https://aka.ms/azure-bot-subscribe-to-conversation-events#channel-deleted) in [Conversation update events](https://aka.ms/azure-bot-subscribe-to-conversation-events).|
| channelRenamed | `OnTeamsChannelRenamedEvent` | Override this to handle a Teams channel being renamed. For more information see [Channel renamed](https://aka.ms/azure-bot-subscribe-to-conversation-events#channel-renamed) in [Conversation update events](https://aka.ms/azure-bot-subscribe-to-conversation-events). |
| teamRenamed | `OnTeamsTeamRenamedEvent` | `return Task.CompletedTask;` Override this to handle a Teams Team being Renamed. For more information see [Team renamed](https://aka.ms/azure-bot-subscribe-to-conversation-events#team-renamed) in [Conversation update events](https://aka.ms/azure-bot-subscribe-to-conversation-events). |
| MembersAdded | `OnTeamsMembersAddedEvent` | Calls the `OnMembersAddedEvent` method in `ActivityHandler`. Override this to handle members joining a team. For more information see [Team member added](https://aka.ms/azure-bot-subscribe-to-conversation-events#Team-Member-Added) in [Conversation update events](https://aka.ms/azure-bot-subscribe-to-conversation-events). |
| MembersRemoved | `OnTeamsMembersRemovedEvent` | Calls the `OnMembersRemovedEvent` method in `ActivityHandler`. Override this to handle members leaving a team. For more information see [Team member removed](https://aka.ms/azure-bot-subscribe-to-conversation-events#Team-Member-Removed) in [Conversation update events](https://aka.ms/azure-bot-subscribe-to-conversation-events). |
-->

| Event | Handler | Description |
| :-- | :-- | :-- |
| channelCreated | `OnTeamsChannelCreatedEvent` | Override this to handle a Teams channel being created. |
| channelDeleted | `OnTeamsChannelDeletedEvent` | Override this to handle a Teams channel being deleted. |
| channelRenamed | `OnTeamsChannelRenamedEvent` | Override this to handle a Teams channel being renamed. |
| teamRenamed | `OnTeamsTeamRenamedEvent` | `return Task.CompletedTask;` Override this to handle a Teams Team being Renamed. |
| MembersAdded | `OnTeamsMembersAddedEvent` | Calls the `OnMembersAddedEvent` method in `ActivityHandler`. Override this to handle members joining a team. |
| MembersRemoved | `OnTeamsMembersRemovedEvent` | Calls the `OnMembersRemovedEvent` method in `ActivityHandler`. Override this to handle members leaving a team. |

#### Teams invoke  activities

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

The invoke activities listed above are for conversational bots in Teams. The Bot Framework SDK also supports invokes specific to messaging extensions. For more information see [What are messaging extensions](https://aka.ms/azure-bot-what-are-messaging-extensions)

---

## Next steps
For building Teams bots, refer to Microsoft Teams Developer [documentation](https://aka.ms/teams-docs).

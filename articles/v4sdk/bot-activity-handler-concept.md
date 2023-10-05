---
title: Event-driven conversations and activity handlers
description: Become familiar with the bot activity handler. Learn about managing bot reasoning based on the type of activity received from a user.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: conceptual
ms.date: 10/26/2022
monikerRange: 'azure-bot-service-4.0'
---

# Event-driven conversations using an activity handler

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

An _activity handler_ is an event-driven way to organize the conversational logic for your bot.
Each different type or subtype of activity represents a different type of conversational event.
Under the covers, the bot's _turn handler_ calls the individual activity handler for whatever type of activity it received.

For example, if the bot receives a message activity, the turn handler would see that incoming activity and send it to the _on message activity_ activity handler. When building your bot, your bot logic for handling and responding to messages will go in this _on message activity_ handler. Likewise, your logic for handling members being added to the conversation will go in your _on members added_ handler, which is called whenever a member is added to the conversation.

For other ways to organize your bot logic, see the [bot logic](bot-builder-basics.md#bot-logic) section in **how bots work**.

[!INCLUDE [java-python-sunset-alert](../includes/java-python-sunset-alert.md)]

# [C#](#tab/csharp)

To implement your logic for these handlers, you'll override these methods in your bot, such as in the [sample activity handler](#sample-activity-handler) section below. For each of these handlers, there's no base implementation, so just add the logic that you want in your override.

There are certain situations where you'll want to override the base turn handler, such as [saving state](bot-builder-concept-state.md) at the end of a turn. When doing so, be sure to first call `await base.OnTurnAsync(turnContext, cancellationToken);` to make sure the base implementation of `OnTurnAsync` is run before your additional code. That base implementation is, among other things, responsible for calling the rest of the activity handlers such as `OnMessageActivityAsync`.

# [JavaScript](#tab/javascript)

The JavaScript `ActivityHandler` uses an event emitter and listener pattern.
For example, use the `onMessage` method to register an event listener for message activities. You can register more than one listener. When the bot receives a message activity, the activity handler would see that incoming activity and send it each of the `onMessage` activity listeners, in the order in which they were registered.

When building your bot, your bot logic for handling and responding to messages will go in the `onMessage` listeners. Likewise, your logic for handling members being added to the conversation will go in your `onMembersAdded` listeners, which are called whenever a member is added to the conversation.
To add these listeners, you'll register them in your bot as seen in the [Bot logic](bot-builder-basics.md#bot-logic) section below. For each listener, include your bot logic, then **be sure to call `next()` at the end**. By calling `next()`, you ensure that the next listener is run.

Make sure to [save state](bot-builder-concept-state.md) before the turn ends. You can do so by overriding the activity handler `run` method and saving state after the parent's `run` method completes.

There are no common situations where you'll want to override the base turn handler, so be careful if you try to do so.
There's a special handler called `onDialog`. The `onDialog` handler runs at the end, after the rest of the handlers have run, and isn't tied to a certain activity type. As with all the above handlers, be sure to call `next()` to ensure the rest of the process wraps up.

# [Java](#tab/java)

To implement your logic for these handlers, you'll override these methods in your bot, such as in the [sample activity handler](#sample-activity-handler) section below. There's no base implementation for each of these handlers, so add the logic you want in your override.

There are certain situations where you'll want to override the base turn handler, such as [saving state](bot-builder-concept-state.md) at the end of a turn. When doing so, be sure first to call `super.onTurn(turnContext);` to make sure the base implementation of `onTurn` is run before your additional code. That base implementation is, among other things, responsible for calling the rest of the activity handlers such as `onMessageActivity`.

# [Python](#tab/python)

When building your bot, your bot logic for handling and responding to messages will go in this `on_message_activity` handler. Likewise, your logic for handling members being added to the conversation will go in your `on_members_added` handler, which is called whenever a member is added to the conversation.

For example, if the bot receives a message activity, the turn handler would see that incoming activity and send it to the `on_message_activity` activity handler.

To implement your logic for these handlers, you'll override these methods in your bot, such as in the [sample activity handler](#sample-activity-handler) section below. For each of these handlers, there's no base implementation, so just add the logic that you want in your override.

There are certain situations where you'll want to override the base turn handler, such as [saving state](bot-builder-concept-state.md) at the end of a turn. When doing so, be sure to first call `await super().on_turn(turnContext);` to make sure the base implementation of `on_turn` is run before your additional code. That base implementation is, among other things, responsible for calling the rest of the activity handlers such as `on_message_activity`.

---

## Activity handling

The bot logic processes incoming activities from one or more channels and generates outgoing activities in response.

### [C#](#tab/csharp)

The main bot logic is defined in the bot code. To implement a bot as an activity handler, derive your bot class from `ActivityHandler`, which implements the `IBot` interface. `ActivityHandler` defines various handlers for different types of activities, such as `OnMessageActivityAsync`, and `OnMembersAddedAsync`. These methods are protected, but can be overridden, since we're deriving from `ActivityHandler`.

The handlers defined in `ActivityHandler` are:

| Event | Handler | Description |
| :-- | :-- | :-- |
| Any activity type received | `OnTurnAsync` | Calls one of the other handlers, based on the type of activity received. |
| Message activity received | `OnMessageActivityAsync` | Override this to handle a `message` activity. |
| Conversation update activity received | `OnConversationUpdateActivityAsync` | On a `conversationUpdate` activity, calls a handler if members other than the bot joined or left the conversation. |
| Non-bot members joined the conversation | `OnMembersAddedAsync` | Override this to handle members joining a conversation. |
| Non-bot members left the conversation | `OnMembersRemovedAsync` | Override this to handle members leaving a conversation. |
| Event activity received | `OnEventActivityAsync` | On an `event` activity, calls a handler specific to the event type. |
| Token-response event activity received | `OnTokenResponseEventAsync` | Override this to handle token response events. |
| Non-token-response event activity received | `OnEventAsync` | Override this to handle other types of events. |
| Message reaction activity received | `OnMessageReactionActivityAsync` | On a `messageReaction` activity, calls a handler if one or more reactions were added or removed from a message. |
| Message reactions added to a message | `OnReactionsAddedAsync` | Override this to handle reactions added to a message. |
| Message reactions removed from a message | `OnReactionsRemovedAsync` | Override this to handle reactions removed from a message. |
| Installation update activity received | `OnInstallationUpdateActivityAsync` | On an `installationUpdate` activity, calls a handler based on whether the bot was installed or uninstalled. |
| Bot installed | `OnInstallationUpdateAddAsync` | Override this to add logic for when the bot is installed within an organizational unit. |
| Bot uninstalled | `OnInstallationUpdateRemoveAsync` | Override this to add logic for when the bot is uninstalled within an organizational unit. |
| Other activity type received | `OnUnrecognizedActivityTypeAsync` | Override this to handle any activity type otherwise unhandled. |

These different handlers have a `turnContext` that provides information about the incoming activity, which corresponds to the inbound HTTP request. Activities can be of various types, so each handler provides a strongly-typed activity in its turn context parameter; in most cases, `OnMessageActivityAsync` will always be handled, and is generally the most common.

As in previous 4.x versions of this framework, there's also the option to implement the public method `OnTurnAsync`. Currently, the base implementation of this method handles error checking and then calls each of the specific handlers (like the two we define in this sample) depending on the type of incoming activity. In most cases, you can leave that method alone and use the individual handlers, but if your situation requires a custom implementation of `OnTurnAsync`, it's still an option.

> [!IMPORTANT]
> If you do override the `OnTurnAsync` method, you'll need to call `base.OnTurnAsync` to get the base implementation to call all the other `On<activity>Async` handlers or call those handlers yourself. Otherwise, those handlers won't be called and that code won't be run.

### [JavaScript](#tab/javascript)

The main bot logic is defined in the bot code. To implement a bot as an activity handler, extend `ActivityHandler`. `ActivityHandler` defines various events for different types of activities, and you can modify your bot's behavior by registering event listeners, such as with `onMessage` and `onConversationUpdate`.

Use these methods to register listeners for each type of event:

| Event | Registration method | Description |
| :-- | :-- | :-- |
| Any activity type received | `onTurn` | Registers a listener for when any activity is received. |
| Message activity received | `onMessage` | Registers a listener for when a `message` activity is received. |
| Conversation update activity received | `onConversationUpdate` | Registers a listener for when any `conversationUpdate` activity is received. |
| Members joined the conversation | `onMembersAdded` | Registers a listener for when members joined the conversation, including the bot. |
| Members left the conversation | `onMembersRemoved` | Registers a listener for when members left the conversation, including the bot. |
| Message reaction activity received | `onMessageReaction` | Registers a listener for when any `messageReaction` activity is received. |
| Message reactions added to a message | `onReactionsAdded` | Registers a listener for when reactions are added to a message. |
| Message reactions removed from a message | `onReactionsRemoved` | Registers a listener for when reactions are removed from a message. |
| Event activity received | `onEvent` | Registers a listener for when any `event` activity is received. |
| Token-response event activity received | `onTokenResponseEvent` | Registers a listener for when a `tokens/response` event is received. |
| Installation update activity received | `onInstallationUpdate` | Registers a listener for when any `installationUpdate` activity is received. |
| Bot installed | `onInstallationUpdateAdd` | Registers a listener for when the bot is installed within an organizational unit. |
| Bot uninstalled | `onInstallationUpdateRemove` | Registers a listener for when the bot is uninstalled within an organizational unit. |
| Other activity type received | `onUnrecognizedActivityType` | Registers a listener for when a handler for the specific type of activity isn't defined. |
| Activity handlers have completed | `onDialog` | Called after any applicable handlers have completed. |

Call the `next` continuation function from each handler to allow processing to continue. If `next` isn't called, processing of the activity ends.

### [Java](#tab/java)

The main bot logic is defined in the bot code. To implement a bot as an activity handler, derive your bot class from `ActivityHandler`, which implements the `Bot` interface. `ActivityHandler` defines various handlers for different types of activities, such as `onMessageActivity`, and `onMembersAdded`. These methods are protected, but can be overridden, since we're deriving from `ActivityHandler`.

The handlers defined in `ActivityHandler` are:

| Event | Handler | Description |
| :-- | :-- | :-- |
| Any activity type received | `onTurn` | Calls one of the other handlers, based on the type of activity received. |
| Message activity received | `onMessageActivity` | Override this to handle a `message` activity. |
| Conversation update activity received | `onConversationUpdateActivity` | On a `conversationUpdate` activity, calls a handler if members other than the bot joined or left the conversation. |
| Non-bot members joined the conversation | `onMembersAdded` | Override this to handle members joining a conversation. |
| Non-bot members left the conversation | `onMembersRemoved` | Override this to handle members leaving a conversation. |
| Event activity received | `onEventActivity` | On an `event` activity, calls a handler specific to the event type. |
| Token-response event activity received | `onTokenResponseEvent` | Override this to handle token response events. |
| Non-token-response event activity received | `onEvent` | Override this to handle other types of events. |
| Message reaction activity received | `onMessageReactionActivity` | On a `messageReaction` activity, calls a handler if one or more reactions were added or removed from a message. |
| Message reactions added to a message | `onReactionsAdded` | Override this to handle reactions added to a message. |
| Message reactions removed from a message | `onReactionsRemoved` | Override this to handle reactions removed from a message. |
| Installation update activity received | `onInstallationUpdate` | On an `installationUpdate` activity, calls a handler based on whether the bot was installed or uninstalled. |
| Bot installed | `onInstallationUpdateAdd` | Override this to add logic for when the bot is installed within an organizational unit. |
| Bot uninstalled | `onInstallationUpdateRemove` | Override this to add logic for when the bot is uninstalled within an organizational unit. |
| Other activity type received | `onUnrecognizedActivityType` | Override this to handle any activity type otherwise unhandled. |

These different handlers have a `turnContext` that provides information about the incoming activity, which corresponds to the inbound HTTP request. Activities can be of various types, so each handler provides a strongly-typed activity in its turn context parameter; in most cases, `onMessageActivity` will always be handled, and is generally the most common.

There's also the option to implement the public method `onTurn`. Currently, the base implementation of this method handles error checking and then calls each of the specific handlers (like the two we define in this sample) depending on the type of incoming activity. In most cases, you can leave that method alone and use the individual handlers, but if your situation requires a custom implementation of `onTurn`, it's still an option.

> [!IMPORTANT]
> If you do override the `onTurn` method, you'll need to call `super.onTurn` to get the base implementation to call all the other `on<activity>` handlers or call those handlers yourself. Otherwise, those handlers won't be called and that code won't be run.

### [Python](#tab/python)

The main bot logic is defined in the bot code. To implement a bot as an activity handler, derive your bot class from `ActivityHandler`, which in turn derives from the abstract `Bot` class. `ActivityHandler` defines various handlers for different types of activities, such as `on_message_activity` and `on_members_added`. These methods are protected, but can be overridden, since we're deriving from `ActivityHandler`.

The handlers defined in `ActivityHandler` are:

| Event | Handler | Description |
| :-- | :-- | :-- |
| Any activity type received | `on_turn` | Calls one of the other handlers, based on the type of activity received. |
| Message activity received | `on_message_activity` | Override this to handle a `message` activity. |
| Conversation update activity received | `on_conversation_update_activity` | On a `conversationUpdate` activity, calls a handler if members other than the bot joined or left the conversation. |
| Non-bot members joined the conversation | `on_members_added_activity` | Override this to handle members joining a conversation. |
| Non-bot members left the conversation | `on_members_removed_activity` | Override this to handle members leaving a conversation. |
| Event activity received | `on_event_activity` | On an `event` activity, calls a handler specific to the event type. |
| Token-response event activity received | `on_token_response_event` | Override this to handle token response events. |
| Non-token-response event activity received | `on_event_activity` | Override this to handle other types of events. |
| Message reaction activity received | `on_message_reaction_activity` | On a `messageReaction` activity, calls a handler if one or more reactions were added or removed from a message. |
| Message reactions added to a message | `on_reactions_added` | Override this to handle reactions added to a message. |
| Message reactions removed from a message | `on_reactions_removed` | Override this to handle reactions removed from a message. |
| Installation update activity received | `on_installation_update` | On an `installationUpdate` activity, calls a handler based on whether the bot was installed or uninstalled. |
| Bot installed | `on_installation_update_add` | Override this to add logic for when the bot is installed within an organizational unit. |
| Bot uninstalled | `on_installation_update_remove` | Override this to add logic for when the bot is uninstalled within an organizational unit. |
| Other activity type received | `on_unrecognized_activity_type` | Override this to handle any activity type otherwise unhandled. |

These different handlers have a `turn_context` that provides information about the incoming activity, which corresponds to the inbound HTTP request. Activities can be of various types, so each handler provides a strongly-typed activity in its turn context parameter; in most cases, `on_message_activity` will always be handled, and is generally the most common.

As in previous 4.x versions of this framework, there's also the option to implement the public method `on_turn`. Currently, the base implementation of this method handles error checking and then calls each of the specific handlers (like the two we define in this sample) depending on the type of incoming activity. In most cases, you can leave that method alone and use the individual handlers, but if your situation requires a custom implementation of `on_turn`, it's still an option.

> [!IMPORTANT]
> If you do override the `on_turn` method, you'll need to call `super().on_turn` to get the base implementation to call all the other `on_<activity>` handlers or call those handlers yourself. Otherwise, those handlers won't be called and that code won't be run.

---

## Sample activity handler

For example, you can handle _on members added_ to welcome users to a conversation, and handle _on message_ to echo back messages they send to the bot.

### [C#](#tab/csharp)

[!code-csharp[C# activity handler](~/../botbuilder-samples/samples/csharp_dotnetcore/02.echo-bot/Bots/EchoBot.cs?range=12-31)]

### [JavaScript](#tab/javascript)

[!code-javascript[JavaScript activity handler](~/../botbuilder-samples/samples/javascript_nodejs/02.echo-bot/bot.js?range=6-29)]

### [Java](#tab/java)

[!code-java[Java activity handler](~/../botbuilder-samples/samples/java_springboot/02.echo-bot/src/main/java/com/microsoft/bot/sample/echo/EchoBot.java?range=25-47)]

### [Python](#tab/python)

[!code-python[Python activity handler](~/../botbuilder-samples/samples/python/02.echo-bot/bots/echo_bot.py?range=8-19)]

---

## Next steps

- The Microsoft Teams channel introduces some Teams-specific activities that your bot will need to support to work properly with Teams. To understand key concepts of developing bots for Microsoft Teams, see [How Microsoft Teams bots work](bot-builder-basics-teams.md)
- An activity handler is a good way to design a bot that doesn't need to track conversational state between turns. The [dialogs library](bot-builder-concept-dialog.md) provides ways to manage a long-running conversation with the user.

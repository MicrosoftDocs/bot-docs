---
title: About bot activity handlers - Bot Service
description: Become familiar with the bot activity handler. Learn about managing bot reasoning based on the type of activity received from a user.
author: JonathanFingold
ms.author: v-jofin
manager: kamrani
ms.topic: conceptual
ms.service: bot-service
ms.date: 09/15/2020
monikerRange: 'azure-bot-service-4.0'
---

# About activity handlers

[!INCLUDE[applies-to](../includes/applies-to.md)]

An activity handler is one way to organize the conversational logic for your bot.

When the bot receives an activity, it passes it on to its *activity handlers*. Under the covers, there is one base handler called the *turn handler*. All activities get routed through there. That turn handler then calls the individual activity handler for whatever type of activity it received.

For example, if the bot receives a message activity, the turn handler would see that incoming activity and send it to the _on message activity_ activity handler.When building your bot, your bot logic for handling and responding to messages will go in this _on message activity_ handler. Likewise, your logic for handling members being added to the conversation will go in your _on members added_ handler, which is called whenever a member is added to the conversation.

# [C#](#tab/csharp)

To implement your logic for these handlers, you will override these methods in your bot as seen in the [Bot logic](#bot-logic) section below. For each of these handlers, there is no base implementation, so just add the logic that you want in your override.

There are certain situations where you will want to override the base turn handler, such as [saving state](bot-builder-concept-state.md) at the end of a turn. When doing so, be sure to first call `await base.OnTurnAsync(turnContext, cancellationToken);` to make sure the base implementation of `OnTurnAsync` is run before your additional code. That base implementation is, among other things, responsible for calling the rest of the activity handlers such as `OnMessageActivityAsync`.

# [JavaScript](#tab/javascript)

The JavaScript `ActivityHandler` uses an event emitter and listener pattern.
For example, use the `onMessage` method to register an event listener for message activities. You can register more than one listener. When the bot receives a message activity, the activity handler would see that incoming activity and send it each of the `onMessage` activity listeners, in the order in which they were registered.

When building your bot, your bot logic for handling and responding to messages will go in the `onMessage` listeners. Likewise, your logic for handling members being added to the conversation will go in your `onMembersAdded` listeners, which are called whenever a member is added to the conversation.
To add these listeners, you will register them in your bot as seen in the [Bot logic](#bot-logic) section below. For each listener, include your bot logic, then **be sure to call `next()` at the end**. By calling `next()` you ensure that the next listener is run.

Make sure to [save state](bot-builder-concept-state.md) before the turn ends. You can do so by overriding the activity handler `run` method and saving state after the parent's `run` method completes.

There aren't any common situations where you will want to override the base turn handler, so be careful if you try to do so.
There is a special handler called `onDialog`. The `onDialog` handler runs at the end, after the rest of the handlers have run, and is not tied to a certain activity type. As with all the above handlers, be sure to call `next()` to ensure the rest of the process wraps up.

# [Python](#tab/python)

When building your bot, your bot logic for handling and responding to messages will go in this `on_message_activity` handler. Likewise, your logic for handling members being added to the conversation will go in your `on_members_added` handler, which is called whenever a member is added to the conversation.

For example, if the bot receives a message activity, the turn handler would see that incoming activity and send it to the `on_message_activity` activity handler.

To implement your logic for these handlers, you will override these methods in your bot as seen in the [Bot logic](#bot-logic) section below. For each of these handlers, there is no base implementation, so just add the logic that you want in your override.

There are certain situations where you will want to override the base turn handler, such as [saving state](bot-builder-concept-state.md) at the end of a turn. When doing so, be sure to first call `await super().on_turn(turnContext);` to make sure the base implementation of `on_turn` is run before your additional code. That base implementation is, among other things, responsible for calling the rest of the activity handlers such as `on_message_activity`.

---

## Bot logic

The bot logic processes incoming activities from one or more channels and generates outgoing activities in response.

# [C#](#tab/csharp)

The main bot logic is defined in the bot code, here called `Bots/EchoBot.cs`. `EchoBot` derives from `ActivityHandler`, which in turn derives from the `IBot` interface. `ActivityHandler` defines various handlers for different types of activities, such as the two defined here: `OnMessageActivityAsync`, and `OnMembersAddedAsync`. These methods are protected, but can be overwritten since we're deriving from `ActivityHandler`.

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
| Other activity type received | `OnUnrecognizedActivityTypeAsync` | Override this to handle any activity type otherwise unhandled. |

These different handlers have a `turnContext` that provides information about the incoming activity, which corresponds to the inbound HTTP request. Activities can be of various types, so each handler provides a strongly-typed activity in its turn context parameter; in most cases, `OnMessageActivityAsync` will always be handled, and is generally the most common.

As in previous 4.x versions of this framework, there is also the option to implement the public method `OnTurnAsync`. Currently, the base implementation of this method handles error checking and then calls each of the specific handlers (like the two we define in this sample) depending on the type of incoming activity. In most cases, you can leave that method alone and use the individual handlers, but if your situation requires a custom implementation of `OnTurnAsync`, it is still an option.

> [!IMPORTANT]
> If you do override the `OnTurnAsync` method, you'll need to call `base.OnTurnAsync` to get the base implementation to call all the other `On<activity>Async` handlers or call those handlers yourself. Otherwise, those handlers won't be called and that code won't be run.

In this sample, we welcome a new user or echo back the message the user sent using the `SendActivityAsync` call. The outbound activity corresponds to the outbound HTTP POST request.

```cs
public class MyBot : ActivityHandler
{
    protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
    {
        await turnContext.SendActivityAsync(MessageFactory.Text($"Echo: {turnContext.Activity.Text}"), cancellationToken);
    }

    protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
    {
        foreach (var member in membersAdded)
        {
            await turnContext.SendActivityAsync(MessageFactory.Text($"welcome {member.Name}"), cancellationToken);
        }
    }
}
```

# [JavaScript](#tab/javascript)

The main bot logic is defined in the bot code, here called `bots\echoBot.js`. `EchoBot` derives from `ActivityHandler`. `ActivityHandler` defines various events for different types of activities, and you can modify your bot's behavior by registering event listeners, such as with `onMessage` and `onConversationUpdate` here.

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
| Other activity type received | `onUnrecognizedActivityType` | Registers a listener for when a handler for the specific type of activity is not defined. |
| Activity handlers have completed | `onDialog` | Called after any applicable handlers have completed. |

Call the `next` continuation function from each handler to allow processing to continue. If `next` is not called, processing of the activity ends.

For example, this bot registers listeners for messages and conversation updates. When it receives a message from the user, it echoes back the message they sent.

```javascript
const { ActivityHandler } = require('botbuilder');

class MyBot extends ActivityHandler {
    constructor() {
        super();
        // See https://aka.ms/about-bot-activity-message to learn more about the message and other activity types.
        this.onMessage(async (context, next) => {
            await context.sendActivity(`You said '${ context.activity.text }'`);
            // By calling next() you ensure that the next BotHandler is run.
            await next();
        });
        this.onConversationUpdate(async (context, next) => {
            await context.sendActivity('[conversationUpdate event detected]');
            // By calling next() you ensure that the next BotHandler is run.
            await next();
        });
    }
}

module.exports.MyBot = MyBot;
```

# [Python](#tab/python)

The main bot logic is defined in the bot code, here called `bots/echo_bot.py`. `EchoBot` derives from `ActivityHandler`, which in turn derives from the `Bot` interface. `ActivityHandler` defines various handlers for different types of activities, such as the two defined here: `on_message_activity`, and `on_members_added`. These methods are protected, but can be overwritten since we're deriving from `ActivityHandler`.

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
| Other activity type received | `on_unrecognized_activity_type` | Override this to handle any activity type otherwise unhandled. |

These different handlers have a `turn_context` that provides information about the incoming activity, which corresponds to the inbound HTTP request. Activities can be of various types, so each handler provides a strongly-typed activity in its turn context parameter; in most cases, `on_message_activity` will always be handled, and is generally the most common.

As in previous 4.x versions of this framework, there is also the option to implement the public method `on_turn`. Currently, the base implementation of this method handles error checking and then calls each of the specific handlers (like the two we define in this sample) depending on the type of incoming activity. In most cases, you can leave that method alone and use the individual handlers, but if your situation requires a custom implementation of `on_turn`, it is still an option.

> [!IMPORTANT]
> If you do override the `on_turn` method, you'll need to call `super().on_turn` to get the base implementation to call all the other `on_<activity>` handlers or call those handlers yourself. Otherwise, those handlers won't be called and that code won't be run.

In this sample, we welcome a new user or echo back the message the user sent using the `send_activity` call. The outbound activity corresponds to the outbound HTTP POST request.

```py
class MyBot(ActivityHandler):
    async def on_members_added_activity(
        self, members_added: [ChannelAccount], turn_context: TurnContext
    ):
        for member in members_added:
            if member.id != turn_context.activity.recipient.id:
                await turn_context.send_activity("Hello and welcome!")

    async def on_message_activity(self, turn_context: TurnContext):
        return await turn_context.send_activity(
            f"Echo: {turn_context.activity.text}"
        )
```

---

---
title: Middleware | Microsoft Docs
description: Understand middleware and it's uses within the bot SDK.
keywords: middleware, middleware pipeline, short circuit, middleware uses
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 05/24/2018
monikerRange: 'azure-bot-service-4.0'
---

# Middleware

[!INCLUDE [pre-release-label](~/includes/pre-release-label.md)]

Middleware is simply a class that sits between the adapter and your bot logic, added to your adapter's middleware collection during initialization. The SDK allows you to write your own middleware or add reusable components of middleware created by others. What can you do in middleware? Just about anything... Every activity coming into or out of your bot flows through middleware.

The adapter processes and directs incoming activities in through the bot middleware pipeline to your bot’s logic and then back out again. As each activity flows in and out of the bot, each piece of middleware can inspect or act upon the activity, both before and after the bot logic runs.

Before jumping into middleware, it is important to understand [bots in general](~/v4sdk/bot-builder-basics.md) and [how they process activities](~/v4sdk/bot-builder-concept-activity-processing.md).

## Uses for middleware

The question that often comes up: what should go in middleware versus your bot logic? Middleware is pretty flexible, so ultimately that is up to you. Let's cover a couple of good uses for middleware.

### Looking at or acting on every activity

There are plenty of situations that require our bot to do something on every activity, or for every activity of a certain type. For example, we may want to log every message activity our bot receives, or provide a fallback response if the bot has not otherwise generated a response this turn. Middleware is a great place for this, with its ability to act both before and after the rest of the bot logic has executed.

### Modifying or enhancing the turn context

Certain conversations can be much more fruitful if the bot has more information than what is provided in the activity. Middleware in this case could look at the conversation state information it has so far, query an external data source, and append that to the context object before passing execution on to the bot logic.
For example, middleware could identify the conversation details, such as the conversation ID and state, and then query a directory service for information. The middleware could add the user object received from that external query to the context object and pass it on, providing more data about the user and allowing the bot to better handle the request.

Middleware may fall into both of the uses above, or into another use completely;
it all depends on how you want your bot to be structured and what your bot is trying to achieve.
Middleware can establish or persist state, react to incoming requests, or short circuit the pipeline.
Some candidates for middleware include:

- **storage or persistence**: Use middleware to store or persist values after a turn, or update some value based off what happened that turn.
- **error handling or failing gracefully**: If an exception is thrown, middleware can send a user friendly message for that exception.
- **translation**: This middleware could detect and translate incoming messages, and set up outgoing messages to be translated back into the detected incoming language.

You can define your own middleware, or the SDK defines some middleware that represents application-independent components, such as:

- State middleware that persists and restores state information on the context object. The SDK provides conversation and user state middleware.
- Translation middleware that can recognize the input language and translate to another language, such as one that your application understands.
- Logging middleware that can record incoming and outgoing activities.

## The bot middleware pipeline

For each activity, the adapter calls middleware in the **order in which you added it**. The adapter passes in the context object for the turn and a _next_ delegate, and the middleware calls the delegate to pass control to the next middleware in the pipeline. Middleware also has an opportunity to do things after the _next_ delegate returns before completing the method. You can think of it as each middleware object has the first-and-last chance to act with respect to the middleware objects that follow it in the pipeline.

For example:

- 1st middleware object’s turn handler executes code before calling _next_.
  - 2nd middleware object’s turn handler executes code before calling _next_.
    - The bot’s turn handler executes and returns.
  - 2nd middleware object’s turn handler executes any remaining code before returning.
- 1st middleware object’s turn handler executes any remaining code before returning.

If middleware doesn’t call the next delegate, the adapter does not call any of the subsequent middleware or bot turn handlers, and the pipeline short circuits.

Once the bot middleware pipeline completes, the turn is over, and the turn context goes out of scope.

Middleware or the bot can generate responses and register response event handlers, but keep in mind that responses are handled in separate processes.

## Order of middleware

Since the order in which middleware is added determines the order in which the middleware processes an activity, it's important to decide the sequence that middleware should be added.

> [!NOTE]
> This is meant to give you a common pattern that works for most bots, but be sure to consider how each piece of middleware will interact with the others for your situation.

The first things in your middleware pipeline should likely be those that take care of the lowest-level tasks that are used every time. Examples include logging, exception handling, state management, and translation. Ordering these can vary depending on your needs, such as whether you want the incoming message to be translated first, before any exceptions can be handled, or if exception handling should be first, which can mean exception messages wouldn't be translated.

The last things in your middleware pipeline should be bot-specific middleware, which is middleware you implement to do some processing on every message sent to your bot. If your middleware uses state information or other information set in the bot context, add it to the middleware pipeline after the middleware that modifies state or context.

## Short circuiting

An important idea around middleware (and [event handlers](~/v4sdk/bot-builder-concept-activity-processing.md#response-event-handlers)) is _short circuiting_. If execution is to continue through the layers that follow it, middleware (or a handler) is required to pass execution on by calling it's _next_ delegate.  If the next delegate is not called within that middleware (or handler), the current pipeline will short circuit and subsequent layers are not executed. This means all bot logic, and any middleware later down the pipeline, are skipped.

For event handlers, not calling _next_ means that the event is cancelled, which is a significantly different result than middleware skipping logic. By not processing the rest of the event, the adapter never sends it.

> [!TIP]
> If you do short-circuit an event, such as `SendActivities`, be sure it's the behavior you intend. Otherwise, it can result in very confusing bugs.

## Next steps

Now that you're familiar with some key concepts of a bot, let's dive into the details of how a bot can send proactive messages.

> [!div class="nextstepaction"]
> [Proactive Messaging](~/v4sdk/bot-builder-proactive-messages.md)

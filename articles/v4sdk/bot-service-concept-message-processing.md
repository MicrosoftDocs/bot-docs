---
title: Activity processing | Microsoft Docs
description: Understand activity processing in the bot SDK.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 03/22/2018
monikerRange: 'azure-bot-service-4.0'
---

# Activity processing

Each time information is exchanged between a bot and the user, that is called an activity. That activity is then processed through our bot via the adapter, which handles giving activity information to our bot as well as any responses from our bot.

> [!IMPORTANT]
> Activities, particularly those that [we generate](#generating-responses) during a bot turn, are asynchronous. It's a necessary part of building a bot; if you need to brush up on how that all works, check out [async for .NET](https://docs.microsoft.com/en-us/dotnet/csharp/async) or [async for JavaScript](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Statements/async_function) depending on your language choice.

## The bot adapter

A bot is directed by it's **adapter**, which can be thought of as the conductor for your bot. The adapter is responsible for directing incoming and outgoing communication, authentication, and so on. The adapter differs based on it's environment (the adapter internally works differently locally versus on Azure) but in each instance it achieves the same goal. In most situations we don't work with the adapter directly, such as when creating a bot from a template, but it's good to know it's there and what it does. 

The bot adapter encapsulates authentication processes, sends activities to and receives activities from the Bot Connector Service. When your bot receives an activity the adapter wraps up everything about that activity, creates a [context object](#turn-context), passes it to your bot's application logic, and sends responses back to the user's channel.

## Authentication

The adapter authenticates each incoming activity the application receives, using information from the activity and the `Authentication` header from the REST request.

The adapter uses a connector object and your application's credentials to authenticate the outbound activities to the user.

Bot Connector Service authentication uses JWT (JSON Web Token) `Bearer` tokens and the **MicrosoftAppID** and **MicrosoftAppPassword** that Azure creates for you when you create a bot service or register your bot. Your application will need these credentials at initialization time, so that the adapter can authenticate traffic.

> [!NOTE]
> If you are running or testing your bot locally, for example, using the Bot Framework Emulator, you can do so without configuring the adapter to authenticate traffic to and from your bot.

## Turn context

When an adapter receives an activity, it generates a **turn context** object, which provides information about the incoming activity, the sender and receiver, the channel, the conversation, and other data needed to process the activity. The adapter then passes this context object to the bot. The context object persists for the length of a turn, and provides information on the following:

* Conversation - Identifies the conversation and includes information about the bot and the user participating in the conversation. 
* Activity - The requests and replies in a conversation are all types of activities. This context provides information about the incoming activity, including routing information, information about the channel, the conversation, the sender, and the receiver. 
* Intent - Represents what the bot thinks the user wants to do, if your bot is set up to provide this. Intent is determined by an intent recognizer, such as [LUIS](bot-builder-concept-luis.md), and can be used it to change conversation topics. 
* State - Your bot can use conversation properties these to keep track of where the bot is in a conversation or its [state](bot-builder-storage-concept.md) and user properties to save information about a user.  
* Custom information – If you extend your bot either by implementing middleware or within your bot logic, you can make additional information available in each turn.

The bot consists of middleware, which are layers of plug-ins added to your bot detailed in the next section, and your core bot logic.

The middleware and the bot logic use the context object to retrieve information about the activity and act accordingly. The middleware and the bot can also update or add information to the context object, such as to track state for a turn, a conversation, or other scope. The SDK provides some _state middleware_ that you can use to add state persistence to your bot.

The context object can also be used to send a response to the user, and get a reference to the adapter to create a new conversation or continue an existing one.

> [!NOTE]
> Your application and the adapter will handle requests asynchronously; however, your business logic does not need to be request-response driven.

## Middleware

Middleware is simply a class that sits between the adapter and your bot logic, added to your adapter's middleware collection during initialization. The SDK allows you to write your own middleware or add reusable components of middleware created by others. What can you do in middleware? Just about anything... Every activity coming into or out of your bot flows through middleware. 

The adapter processes and directs incoming activities in through the bot middleware pipeline to your bot’s logic and then back out again. As each activity flows in and out of the bot, each piece of middleware can inspect or act upon the activity, both before and after the bot logic runs.

The question that often comes up: what should go in middleware versus your bot logic? Middleware is pretty flexible, so ultimately that is up to you. Let's cover a couple of good uses for middleware.

#### Looking or acting on every activity

There are plenty of situations that require our bot to do something on every activity, or for every activity of a certain type. For example, we may want to log every message activity our bot receives, or provide a fallback by examining the result of each turn and sending a reply if we haven't yet sent one. Middleware is a great place for this with its ability to act both before and after the rest of the bot logic has executed.

#### Modifying or enhancing the turn context

Certain conversations can be much more fruitful if the bot has more information than what is provided in the activity. Middleware in this case could look at the conversation state information it has so far, query an external data source, and append that to the context object before passing execution on to the bot logic. For example, middleware identifies the conversation details, such as the conversation ID and state, and then queries a directory service for information. The user object received from that external query is then added to the context object and passed on, providing more data about the user and allowing the bot to better handle the request.

Some other good candidates for middleware may include:
-	**storage or persistence**: Use middleware to store or persist values after a turn, or update some value based off what happened that turn.
-   **error handling or failing gracefully**: If an exception is thrown, middleware can send a user friendly message for that exception.
-	**translation**: This middleware could detect and translate incoming messages, and set up outgoing messages to be translated back into the detected incoming language.

Middleware may fall into both of the uses above, or into another use completely; it all depends on how you want your bot to be structured and what your bot is trying to achieve. Middleware can establish or persist state, react to incoming requests, or short circuit the pipeline. 

You can define your own middleware, or the SDK defines some middleware that represents application-independent components, such as:
-	State middleware that persist and restore state information on the context object. The SDK includes conversation and user state middleware.
-	Intent recognizers that analyze user messages to extrapolate the user's intent. The SDK includes LUIS, QnA Maker, and RegEx recognizers.
-	Translation middleware that can recognize the input language and translate to another language, such as one that your application understands.

## The bot middleware pipeline

For each activity, the adapter calls middleware in the **order in which you added it**. The adapter passes in the context object for the turn and a _next_ delegate, and the middleware calls the delegate to pass control to the next middleware in the pipeline. Middleware also has an opportunity to do things after the _next_ delegate returns before completing the method. You can think of it as each middleware object has the first-and-last chance to act with respect to the middleware objects that follow it in the pipeline.

For example:
- 1st middleware object’s request handler executes code before calling _next_.
    - 2nd middleware object’s request handler executes code before calling _next_.
        - The bot’s receive handler executes and returns
    - 2nd middleware object’s request handler executes any remaining code before returning.
- 1st middleware object’s request handler executes any remaining code before returning.

If middleware doesn’t call the next delegate, the adapter does not call any of the subsequent middleware’s request handlers or the bot’s receive handler, and the pipeline short circuits.

Once the bot middleware pipeline completes, the turn is not necessarily over. Middleware or the bot can generate responses, and middleware and the bot can register response event handlers. Responses are handled in separate processes, and once the pipeline and all responses are complete, the turn is over and the associated context object goes out of scope.

### Short circuiting

An important idea around middleware (and event handlers that you'll see below) is **short circuiting**. If execution is to continue through the layers that follow it, middleware (or a handler) is required to pass execution on by calling it's _next_ delegate.  If the _next_ delegate is not called within that middleware (or handler), that current pipeline will be short circuited and subsequent layers will not be executed.

## Generating responses

The context object provides activity response methods to allow code to respond to an activity:
-	The _send activity_ method sends one or more activities to the conversation.
-	If supported by the channel, the _update activity_ method updates an activity within the conversation.
-	If supported by the channel, the _delete activity_ method removes an activity from the conversation.

Each response method runs in an asynchronous process. When it is called, the activity response method clones the associated event handler list before starting to call the handlers, which means it will contain every handler added up to this point but will not contain anything added after the process starts.

This also means the order of your responses is not guaranteed, particularly when one task is more complex than another. If your bot can generate multiple responses to an incoming activity, make sure that they make sense in whatever order they are received by the user.

> [!IMPORTANT]
> The thread handling the primary bot turn deals with disposing of the context object when it is done. If a response (including its handlers) take any significant amount of time and try to act on the context object, they may get a `Context was disposed` error. **Be sure to `await` any activity calls** so the primary thread will wait on the generated activity before finishing it's processing and dispose of the `context`.

## Response event handlers

In addition to the bot and middleware logic, response handlers (also sometimes referred to as activity handlers) can be added to the context object. These handlers are called when the associated response happens on the current context object, before executing the actual response. These handlers are useful when you know you'll want to do something, either before or after the actual event, for every activity of that type for the rest of the current response.

> [!WARNING]
> Be careful to not call an activity response method from within it's respective response event handler, for example, calling the send activity method from within an _on send activity_ handler. Doing so can generate an infinite loop.

Each new activity gets a new thread to execute on. When the thread to process the activity is created, the list of handlers for that activity is copied to that new thread. No handlers added after that point will be executed for that specific activity event.

The adapter manages the handlers registered on a context object very similarly to how it manages the middleware pipeline.

For each response event, the adapter calls handlers in the order in which they were added. The adapter passes in the context object for the turn and a _next_ delegate and the handler calls the delegate to pass control to the next registered event handler. If a handler doesn’t call the next delegate, the adapter does not call any of the subsequent event handlers; the event **short circuits**, and the adapter does not send the response to the channel.

## Next steps

Now that you're familiar with some key concepts of a bot, let's dive into the details of how a bot can send proactive messages.

> [!div class="nextstepaction"]
> [Proactive Messaging](bot-service-proactive-messages.md)
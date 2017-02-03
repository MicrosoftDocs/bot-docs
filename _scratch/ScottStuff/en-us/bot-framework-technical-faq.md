---
layout: page
title: Technical FAQ
permalink: /en-us/faq/technical-faq/
weight: 4047
parent1: FAQ
---

This FAQ answers technical questions about Bot Framework. For answers to more general questions about the framework, see [General FAQ](/en-us/general-faq/).

* TOC
{:toc}

## My bot is stuck!  How can I reset the conversation?

You can use the command "/deleteprofile" to delete the User/PrivateConversation bot data bag state, which will reset your bot.  Note that some channels interpret slash-commands natively, so you may be need to send the command with a space in front (for example, " /deleteprofile").

<span style="color:red"><< There needs to be more context. Use the command where? >></span>

<span style="color:red"><< Is this a fix for node.js only? >></span>

<span style="color:red"><< "data bag state"? This is the first reference to "data bag state" that I've seen in the doc. >></span>

## How can I reset the Bot Framework Emulator's settings?

You can remove the emulator.service file in %temp% with "erase %temp%\emulator.service".

<span style="color:red"><< Is this relevant with the updated emulator? >></span>

<span style="color:red"><< This surprised me: A new version is available. Downloading it now. You will be notified when download completes. >></span>

## How can I troubleshoot my Bot?

### Try your Bot's web service in the emulator first.

The emulator allows local debugging of your Bot logic, before deploying to the cloud.

1. To remove authentication as a possible issue, don't set "Microsoft App Id" and "Microsoft App Password" in the emulator and web.config (while keeping the BotAuthentication attribute).
2. Since the exchange of chat messages will likely result in several HTTPS method calls, it's important to make sure that they all succeed.  You can see the result of every HTTPS method call by selecting the different items in the list view on the left-hand side of the emulator window.

<span style="color:red"><< regarding #1, isn't web.config and BotAuthentication unique to .NET, so it would probably just confuse the node guy? >></span>

<span style="color:red"><< regarding #2, what "list view"? >></span>

### Deploy your code to Azure next.

Test your connection to the Bot Framework in the [developer dashboard](https://docs.botframework.com/en-us/csharp/builder/sdkreference/gettingstarted.html#testing).  If your bot web service encounters issues as you move from running in your development environment to the cloud, you might consider using this guide: [Troubleshoot a web app in Azure App Service using Visual Studio](https://azure.microsoft.com/en-us/documentation/articles/web-sites-dotnet-troubleshoot-visual-studio/).

<span style="color:red"><< Does this guidance apply to node guys too? >></span> 

### Finally, try your Bot on Skype.

This will help you validate the end-to-end user experience.

<span style="color:red"><< Why Skype and not something simple like Web Chat? >></span>

### Consider adding channels with additional authentication requirements.

After you get your bot working with Skype, try other channels such as Direct Line or Web Chat.

## How can I troubleshoot my Bot's authentication?

Take a look at [Troubleshooting Bot Framework Authentication](/en-us/support/troubleshooting-bot-framework-authentication/).

## How can I troubleshoot my Bot using the C# Bot Builder SDK?

### Look for exceptions.

In Visual Studio 2015, go to <code>Debug|Windows|Exception Settings</code> and select the "Break When Thrown" checkbox next to "Common Language Runtime Exceptions".  You may also see diagnostics output in your "Output Window" when there are thrown or unhandled exceptions.

### Look at the call stack.

In Visual Studio, you can choose whether you're debugging [Just My Code](https://msdn.microsoft.com/en-us/library/dn457346.aspx) or not.  Seeing the full call stack may provide more insight into any issues.

### Ensure all dialog methods end with a plan to handle the next message.

All IDialog methods should complete with IDialogStack.Call, IDialogStack.Wait, or IDialogStack.Done.  These IDialogStack Call/Wait/Done methods are exposed through IDialogContext passed to every IDialog method.  Calling IDialogStack.Forward and using the system prompts through the PromptDialog static methods will call one of these methods in their implementation.

<span style="color:red"><< While .Call is a method of IDialogStack, and you do mention IDialogContext, I don't understand why we just don't use IDialogContext.Call which is what they'll use in their code? Why add the complexity, and possible confusion? >></span>

### Ensure that all dialogs are serializable.

All IDialog implementations should include the [Serializable] attribute. Note that anonymous method closures are not serializable if they reference their outside environment to capture variables. We also support a reflection-based serialization surrogate to help serialize types not marked as serializable.

<span style="color:red"><< I don't understand the second sentence. "anonymous method closures"? "outside environment"? >></span>

## What is the model for how user messages relate to HTTPS method calls?

When the user sends a message over a channel, the Bot Framework web service will make an HTTPS post to your bot's web service endpoint.  During this HTTPS method call, your Bot's code may send zero, one, or many messages back to the user on that channel.  Each one of these messages is a separate HTTPS post back to the Bot Framework.

<span style="color:red"><< In the first sentence, is the Bot Framework web service the Bot Connector? >></span>

<span style="color:red"><< The use of "over a channel" seems odd to me. Isn't a channel a chat service (i.e., Kik)? "over" makes it sound like a wire. >></span>

When the conversation channel sends the user's message to your bot, it goes through the Bot Connector which sends an HTTPS POST requst to your bot's web service endpoint. In response, your bot may send zero, one, or more HTTPS POST responses back to the connector which passes them on to the channel.

## How can I guarantee message delivery order?

From the framework's perspective, we will preserve message ordering to the extent possible. For example, if you wait for the completion of message A before sending message B, we will respect the ordering that A comes before B. But, in general, because the channel manages message delivery, we cannot guarantee the delivery order. It is possible that the channel may reorder messages. For example, you have likely seen email and text messages that are delivered out of order. As a mitigation, you might consider putting a time delay between your messages.

## What is the difference between the "Bot Url" and "Emulator Url" in the emulator?

<span style="color:red"><< Is this relevant anymore with the new emulator? >></span>

The Bot Url" is the web service url that executes your Bot's code.  The "Emulator Url" is a *different* web service url that emulates the Bot Framework Connector service.  They should not be the same.  Generally, you will want to leave the "Emulator Url" as the default value, and update the "Bot Url" to the url for your Bot web service.


## What is the difference between the Connector and Builder library in the SDK?

The Connector library exposes the framework's REST API, and the Builder library adds the conversational dialog programming model and other features such as prompts, waterfalls, chains, guided conversations, and access to cognitive services such as LUIS.

## How can I get access to the user ID?

SMS and email will provide the raw user ID in the `from.Id` field. However, Skype generates a unique ID for the user which is different from the Skype ID. If you need to connect to an existing account you can use a sign in card and implement your own OAuth flow to connect the user ID to your own service's user ID.

## How can I limit access to my bot to a pre-determined list of users?

Some channels (for example, SMS and email) provide unscoped addresses where the `from.Id` feld contains the user's actual, raw ID. However, other channels (for example, Skype, Facebook, and Slack) give you either scoped or tenanted addresses, and they typically do so in a way that prevents the bot from predicting the user’s ID ahead of time. For these channels, you need to authenticate the user on your own by using a login link or shared secret before you know whether or not they are authorized to use the bot.

## How can I intercept all messages between the user and my bot?

In C#, you can provide implementations of the IPostToBot and IBotToUser interfaces to the Autofac dependency injection container. In Node, you can use middleware to accomplish the same purpose.

## Why are my Facebook user names not showing anymore?

Did you change your Facebook password? This will invalidate the access token, and you will have to update the Facebook channel registration.

## Why is my Kik bot replying, <code>I'm sorry, I can't talk right now</code>?

By default, bots in development on Kik are allowed 50 subscribers. After 50 unique users have chatted with your bot, any new user attempting to chat will get the <code>I'm sorry, I can't talk right now</code> response. Kik's [Getting Started](https://dev.kik.com/#/docs/getting-started) topic mentions this restriction.

## Why doesn't the Typing activity do anything?

Some channels don't support transient typing updates in their client.

## Why are parts of my message text being dropped?

The Bot Framework and many channels interpret text as Markdown. Check to see if your text uses reserved Markdown characters.

<span style="color:red"><< This question doesn't have anything to do with text that is too long and being truncated? >></span>

## How can I use authenticated services from my bot?

* The [AuthBot NuGet](https://www.nuget.org/packages/AuthBot) library is extremely helpful for Azure Active Directory authentication.
* The [Builder GitHub samples](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples) has examples for Facebook authentication.

## How can I reference non-serializable services from my C# dialogs?

The following are some options that may help.

1. Don't store that dependency so it won't be serialized (probably not helpful, but it's an option)
2. Use the reflection serialization surrogate (still may not work and risks serializing too much)
3. Resolve the dependency through Autofac and [FiberModule.Key_DoNotSerialize](https://github.com/Microsoft/BotBuilder/blob/master/CSharp/Library/Fibers/FiberModule.cs#L59) (cleanest solution)
4. Use [NonSerialized](https://msdn.microsoft.com/en-us/library/system.nonserializedattribute(v=vs.110).aspx) and [OnDeserialized](https://msdn.microsoft.com/en-us/library/system.runtime.serialization.ondeserializedattribute(v=vs.110).aspx) attributes to restore the dependency on deserialization (simplest solution)

## What is the difference between "proactive" and "reactive"?

*Reactive* is the typical scenario where the user starts the conversation by sending the first message to the bot and the bot responds.  *Proactive* is where the bot starts the conversation by sending the first message to the user.

## Where is conversation state stored?

<span style="color:red"><< Because we say that we're stateless, and then we start talking about conversation state, I think we need to be very clear about what conversation state is. >></span>

<span style="color:red"><< We also need to be very consistent about how we refer to it: is it a store, a bag, a ??? Is it conversation state, user state, conversation data, user data, ???>></span>

A bot stores conversation state by using the Connector's IBotState interface. The bot may store state that's specific to a user (user state), a conversation (group conversation state), or a private conversation (private conversation state). The user state store is keyed by user ID, the conversation state store is keyed by conversation ID, and the private conversation state store is keyed by both user ID and conversation ID. Given the same keys, you'll get the same state store back.

In the Node and C# bot builder SDKs, the dialog stack and dialog data are both stored in the private conversation state store. The C# implementation uses binary serialization, and the Node implementation uses JSON serialization.

<span style="color:red"><< Why is the dialog stack stored in the private conversation store? If it's a group conversation, which user ID is used? >></span>

This IBotState REST interface is implemented by two services.

1. The Bot Connector provides a cloud service that implements this interface and stores data in Azure. This data is encrypted and does not intentionally expire.

2. The Bot Framework Emulator provides an in-memory implementation of this interface for debugging your bot. This data expires when the emulator process exits.

If you want to store this data within your data centers, you can provide a custom implementation of the state service. For example, by:

1. Implementing a custom IBotState service at the REST layer
2. the language (Node or C#) layer, with the builder interfaces

<span style="color:red"><< for #2, they're creating their own builder interfaces? >></span>

<span style="color:red"><< #2 probably needs more context. >></span>

## What is an ETag?  How does it relate to bot data bag storage?

An [ETag](https://en.wikipedia.org/wiki/HTTP_ETag) is a mechanism for [optimistic concurrency control](https://en.wikipedia.org/wiki/Optimistic_concurrency_control). The bot state stores use ETags to prevent conflicting updates to the data. If you encounter an ETag "precondition failed" HTTP error, it means that there were multiple "read-modify-write" sequences executing concurrently for that bot state store.

The dialog stack and state are stored in these bot state stores. For example, you might see the ETag precondition failed error if your bot is still processing a previous message when it receives a new message for that conversation.

## Is there a limit on the amount of data I can store using the State API?

Yes, each state store that the bot uses (for example, the user store or conversation store) may contain up to 32 KB of data ([see Bot State API](https://docs.botframework.com/en-us/restapi/state/)).

<span style="color:red"><< each store can contain up to 32 KB or you may store up to 32 KB in the store for each user? >></span> 

## How do I version the bot data stored through the State API?

<span style="color:red"><< all of these state related Q&A should be in the doc >></span>

<span style="color:red"><< state service? In this paragraph it says state service and state api, which is it? >></span>

The State Service allows you to persist progress through a conversation's dialogs, so if a user returns to a conversation days later, they don't lose their position. But what if you change your bot's code and the state data is no longer compatible with the prior version? The following are options for how you might handle this case.

1. During development of your bot, if you want to manually reset the conversation's dialog stack and state, you can use the " /deleteprofile" command (with the leading space so it's not interpreted by the channel) to clear out the state.
2. During production usage of your bot, you can version your bot's state data so that if you bump the version, the associated data is cleared.  This can be accomplished with the exiting middleware in Node or an IPostToBot implementation in C#.

<span style="color:red"><< #1 needs more context. where do i use the command? why would this be going to the channel, i thought state was part of the connector?  can't i do #1 in production too? >></span>

<span style="color:red"><< #2 "exiting" middleware? the middleware or iposttobot implementation gets the state data, checks the version, and then clears the data or maps it to the latest version, if applicable? How do you clear state data - do you just write empty state to the store? >></span>

If the dialog stack cannot be deserialized correctly (due to serialization format changes or because the code has changed too much), the conversation state will be reset.

<span style="color:red"><< why would the dialog stack not be deserialized correctly? Don't we guarantee backwards compatibility? >></span.>

<span style="color:red"><< which code has changed too much? >></span>

<span style="color:red"><< is reset the same as cleared? >></span>

## Why does my Direct Line conversation start over after every message?

If your Direct Line conversation appears to start over after every message, you are likely forgot to include the `from` field in the messages you sent from your Direct Line client. Direct Line automatically allocates the `from` ID when the `from` field is null, so every message sent from your client appears to your bot to be a new user.

To fix this, set the `from` field to a stable value that represents the user. The value of the field is up to you. If you already have a signed-in user in your webpage or app, you can use the existing user ID. If not, you could generate a random user ID on page/app load, optionally store that ID in a cookie or device state, and use that ID.

## What are the possible machine-readable resolutions of the LUIS built-in date, time, duration, and set entities?

There is a list of examples available in the [Pre-built entities section](https://www.luis.ai/Help/#PreBuiltEntities) of the LUIS documentation.

## How can I use more than the maximum number of LUIS intents?

You might consider splitting up your model and calling the LUIS service in series or parallel.

## How can I use more than one LUIS model?

Both the Node and C# versions of the SDK support calling multiple LUIS models from a single LUIS intent dialog. But there are few caveats to keep in mind:

1. Using multiple LUIS models assumes the LUIS models have non-overlapping sets of intents.
2. Using multiple LUIS models assumes the scores from different models are comparable, to select the “best matched intent” across multiple models.
3. Using multiple LUIS models means that if an intent matches one model, it will also strongly match the "none" intent of the other models. You can avoid selecting the "none" intent in this situation (the Node SDK will automatically scale down the score for "none" intents to avoid this issue).

## Where can I get more help on LUIS?

Here are some videos about LUIS:

* [Introduction to Language Understanding Intelligent Service (LUIS) - Microsoft Cognitive Services](https://www.youtube.com/watch?v=jWeLajon9M8) 
* [Advanced Learning Session for Language Understanding Intelligent Service (LUIS) ](https://www.youtube.com/watch?v=39L0Gv2EcSk)

You can access LUIS experts at [Language Understanding Intelligent Service Forum](https://social.msdn.microsoft.com/forums/azure/en-US/home?forum=LUIS).

## What are some community-authored dialogs?

* [AuthBot](https://www.nuget.org/packages/AuthBot) - Demonstrates Azure Active Directory authentication
* [BestMatchDialog](http://www.garypretty.co.uk/2016/08/01/bestmatchdialog-for-microsoft-bot-framework-now-available-via-nuget/) - Demonstrates regular expression-based dispatch of user text to dialog methods

## What are some community-authored templates?

* [ES6 BotBuilder](https://github.com/brene/botbuilder-es6-template)

## Where can I get more help?

* [GitHub issues](https://github.com/Microsoft/BotBuilder/issues) has an active forum.
* [Gitter](https://gitter.im/Microsoft/BotBuilder) has active discussions. 
* [Stack Overflow](http://stackoverflow.com/questions/tagged/botframework) has a list of tagged Bot Framework questions.

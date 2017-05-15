---
title: Bot Framework Troubleshooting Guide | Microsoft Docs
description: Assistance with troubleshooting bots and the Bot Framework. Includes the Troubleshooting Guide and Technical FAQ.
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 5/11/2017
ms.reviewer: 
---

# Bot Framework troubleshooting guide
These Frequently Asked Questions (FAQ) for the Bot Framework can help you with troubleshooting.

## General questions

### How can I troubleshoot my Bot?

1. Try your Bot's web service in the emulator first. The emulator allows local debugging of your Bot logic, before deploying to the cloud.
    * Using a blank "Microsoft App Id" and "Microsoft App Password" in the emulator and web.config (while keeping the BotAuthentication attribute) will allow you to rule out authentication as a possible issue.
    * Since the exchange of chat messages will likely result in several HTTPS method calls, it's important to make sure that they all succeed.  You can see the result of every HTTPS method call by selecting the different items in the list view on the left-hand side of the emulator window.

2. Deploy your code to Azure next. Test your connection to the Bot Framework in the developer dashboard.  If your bot web service encounters issues as you move from running on your development machine to the cloud, you might consider using this guide: [Troubleshoot a web app in Azure App Service using Visual Studio](https://azure.microsoft.com/en-us/documentation/articles/web-sites-dotnet-troubleshoot-visual-studio/).

3. Finally, try your Bot on Skype. This will help you validate the end-to-end user experience.

4. Consider adding channels with additional authentication requirements. Once channels like Skype work, you can try additional channels like Direct Line or Web chat.

### My bot is stuck!  How can I reset the conversation?

You can use the command `/deleteprofile` to delete the `User/PrivateConversation` bot data bag state and reset your bot.  Note that some channels interpret slash-commands natively, so it may be necessary to send the command with a space in front (`" /deleteprofile"`).

### How can I reset the Bot Framework Emulator's settings?

You can remove the emulator.service file in `%temp%` with `erase %temp%\emulator.service`.

### How can I troubleshoot my Bot's authentication?

Take a look at [Troubleshooting Bot Framework Authentication][TroubleshootingAuth].

### I'm using the C# Bot Builder SDK. How can I troubleshoot my Bot?

* **Look for exceptions**. In Visual Studio 2017, go to **Debug** > **Windows** > **Exception Settings**. In the **Exceptions Settings** window, select the **Break When Thrown** checkbox next to **Common Language Runtime Exceptions**.  You may also see diagnostics output in your "Output Window" when there are thrown or unhandled exceptions.

* **Look at the call stack**. In Visual Studio, you can choose whether you're debugging [Just My Code](https://msdn.microsoft.com/en-us/library/dn457346.aspx) or not.  Seeing the full call stack may provide more insight into any issues.

* **Ensure all dialog methods end with a plan to handle the next message**. All IDialog methods should complete with IDialogStack.Call, IDialogStack.Wait, or IDialogStack.Done.  These IDialogStack Call/Wait/Done methods are exposed through the IDialogContext passed to every IDialog method.  Calling IDialogStack.Forward and using the system prompts through the PromptDialog static methods will call one of these methods in their implementation.

* **Ensure that all dialogs are serializable**. This can be as simple as using the [Serializable] attribute on your IDialog implementations.  But beware that anonymous method closures are not serializable if they reference their outside environment to capture variables.  We also support a reflection-based serialization surrogate to help serialize types not marked as serializable.

### Why doesn't the Typing activity do anything?
Some channels don't support transient typing updates in their client.

### What is the difference between the "Bot Url" and "Emulator Url" in the emulator?

The "Bot Url" is the web service url that executes your Bot's code.  The "Emulator Url" is a *different* web service url that emulates the Bot Framework Connector service.  They should not be the same.  Generally, you will want to leave the "Emulator Url" as the default value, and update the "Bot Url" to the url for your Bot web service.

### What is the difference between the Connector and Builder library in the SDK?

The Connector library is the exposition of the REST API.  The Builder library adds the conversational dialog programming model and other features (e.g. prompts, waterfalls, chains, guided form filling) and access to cognitive services (e.g. LUIS).

### What causes 429 "too many requests" HTTP errors?

One possible source for HTTP status code 429 is [ngrok](https://ngrok.com/).  Ngrok is used to provide a secure tunnel to localhost.  For example, you might be configuring ngrok with the Bot Framework Emulator to locally debug your Bot in the cloud.

### How can I run background tasks in ASP.NET? 

To initiate an asynchronous task that waits for a few seconds and then executes some code to clear the user profile or reset conversation/dialog state, for example, read [How to run Background Tasks in ASP.NET](https://www.hanselman.com/blog/HowToRunBackgroundTasksInASPNET.aspx).  In particular, consider using [HostingEnvironment.QueueBackgroundWorkItem](https://msdn.microsoft.com/en-us/library/dn636893(v=vs.110).aspx). 

## Transmitting messages

### How do user messages relate to HTTPS method calls?

When the user sends a message over a channel, the Bot Framework web service will make an HTTPS post to your Bot's web service endpoint.  During this HTTPS method call, your Bot's code may send zero, one, or many messages back to the user on that channel.  Each one of these messages is a separate HTTPS post back to the Bot Framework.

### My bot is slow to respond to the first message it receives. How can I make it faster?

Bots are web services and some hosting platforms (Azure and others) will automatically put the service to sleep if it does not receive traffic for a period of time. When the next message reaches the bot, the bot's code must be started from scratch, which is typically much slower than if the bot was already running.

Some hosting platforms allow you to configure a service so it does not go to sleep.

In Azure, you can enable a Web App or API App to be "always on" by navigating to your bot's Service in the [Azure Portal](https://portal.azure.com), selecting "**Application settings**," and selecting "**Always on**." This option is available in most service plans.

### How can I guarantee message delivery order?

The Bot Framework will preserve message ordering to the extent possible, such that if you wait for the completion of the http operation to send message A before initiating another http operation to send message B, we will respect the ordering that A comes before B .  In general, however, you cannot guarantee message delivery order, as delivery is done by the channel, and the channel may reorder messages.  For example, you have likely seen email and text messages being delivered out of order.  You might choose to put a time delay between your messages as a mitigation.

### How do identifiers work in the Bot Framework? 

Take a look at the [Bot Framework resource identifiers guide](resources-identifiers-guide.md). 

### How can I intercept all messages between the user and my bot?

In C#, you can provide implementations of the IPostToBot and IBotToUser interfaces to the Autofac dependency injection container.  In Node, you can use middleware for much the same purpose. The [BotBuilder-Azure](https://github.com/Microsoft/BotBuilder-Azure) repo contains C# and Node.js libraries that will log this data to an Azure table.

### Why are parts of my message text being dropped?

The Bot Framework and many Channels interpret text as Markdown.  Check to see if your text uses reserved Markdown characters.

### How can I support multiple bots at the same bot service endpoint? 

Take a look at this [sample](https://github.com/Microsoft/BotBuilder/issues/2258#issuecomment-280506334). It explains how to configure the `Conversation.Container` with the right MicrosoftAppCredentials and use a simple `MultiCredentialProvider` to authenticate multiple MicrosoftAppIds and MicrosoftAppPasswords.

## Identifiers

### How do identifiers work in the Bot Framework?

Take a look at the [Bot Framework IDs Guide][BotFrameworkIDGuide].

### How can I get access to the user id?

SMS and email will provide the user id raw in the from.Id field.  In Skype we give you a unique ID for the user which is different from the Skype ID. If you need to connect to an existing account you can use a sign in card and implement your own oauth flow to connect the user ID to your own service's user ID.

## Working with channels
### Why are my Facebook user names not showing anymore?

Did you change your Facebook password?  This will invalidate the access token, and you will have to update the Facebook channel registration.

### Why is my Kik bot replying "I'm sorry, I can't talk right now"?

Be default bots in development on Kik are allowed 50 subscribers. After 50 unique users have chatted with your bot, any new user attempting to chat will get the "I'm sorry, I can't talk right now" message. This is mentioned in Kik's [Getting Started](https://dev.kik.com/#/docs/getting-started).

### How can I use authenticated services from my Bot?

* The [AuthBot NuGet](https://www.nuget.org/packages/AuthBot) library is extremely helpful for Azure Active Directory authentication.
* The [Builder GitHub samples](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples) include examples for Facebook authentication.

**Note**: when adding authentication and security functionality, make sure the patterns you implement in your code meet security standards appropriate for your application.

### How can I limit access to my bot to a pre-determined list of users?

Some channels provide unscoped addresses. SMS and email are examples. The ID appears raw in the from.Id feld.

Other channels give you either scoped or tenanted addresses (e.g. Skype, Facebook, Slack), and they typically do so in a way that prevents the bot from predicting the user’s ID ahead of time.  For these channels, you need to authenticate the user on your own (via a login link or shared secret) before you know whether or not they are authorized to use the bot.

### Why does my Direct Line 1.1 conversation start over after every message?

If your Direct Line conversation appears to start over after every message, you are likely omitting the "from" field on the messages you sent from your Direct Line client. Direct Line auto-allocates IDs when the "from" property is null, so every message sent from your client appears to your bot to be a new user.

To fix this, set the "from" field to a stable value that represents the user.

The value of the field is up to you. If you already have a signed-in user in your webpage or app, you can use the existing user ID. If not, you could generate a random user ID on page/app load, optionally store that ID in a cookie or device state, and use that ID.

### Why am I seeing HTTP 502 errors from the Direct Line service? 
Direct Line 3.0 returns HTTP 502 when it tries to contact your bot but the request does not complete successfully. This can happen if the bot returns an error or if it times out. You can find more information about your bot's errors by visiting the Bot Framework developer portal and checking the "Issues" column next to the affected channel. If you have Application Insights configured for your bot, you can find detailed error messages there. 

##<a id="implement-dialogs"></a> Implementing dialogs and conversations

### What is IDialog.Stack Forward in the C# Builder SDK?

The primary purpose of IDialog.Forward is to reuse an existing child dialog that is often "reactive", where the child dialog (in IDialog.StartAsync) waits for an object T with some ResumeAfter handler. In particular, if you have a child dialog that waits for an IMessageActivity T (e.g. the LuisDialog), you can decide to forward the incoming IMessageActivity (already received by some parent dialog) by using the IDialogStack.Forward method (e.g. which in the example of forwarding to a LuisDialog, IDialogStack.Forward will push the LuisDialog on the dialog stack, run the code in LuisDialog.StartAsync until it schedules a wait for the next message, and then immediately satisfy that wait with the forwarded IMessageActivity).

It's common that T is an IMessageActivity, because almost nobody writes an IDialog.StartAsync that waits for other types.  You might use IDialogStack.Forward to a LuisDialog as a mechanism to intercept messages from the user for
some processing before forwarding the message to an existing LuisDialog. Separately, you can also use DispatchDialog with ContinueToNextGroup for that purpose.

You would expect to find the forwarded item in the first ResumeAfter handler (e.g. LuisDialog.MessageReceived) that is scheduled by StartAsync.

### What is the difference between "proactive" and "reactive"?

The term "reactive" refers to the more common situation in which the user sends the first message in a conversation to the bot and the bot responds to that message, so that the he user's initial message creates the conversation.  
The term "proactive" means there isn’t an initial message from the user to the bot, so the bot creates the conversation.

### How can I send proactive messages to the user?

Take a look at these [Proactive Messages Samples](https://github.com/MicrosoftDX/botFramework-proactiveMessages).

### How can I reference non-serializable services from my C# dialogs?

There are a few options:

* Don't store that dependency so it won't be serialized. This probably isn't helpful, but it's an option.
* Use the reflection serialization surrogate. This still may not work and risks serializing too much.
* Resolve the dependency through Autofac and `FiberModule.Key_DoNotSerialize`. This is the cleanest solution.
* Use [NonSerialized](https://msdn.microsoft.com/en-us/library/system.nonserializedattribute(v=vs.110).aspx) and [OnDeserialized](https://msdn.microsoft.com/en-us/library/system.runtime.serialization.ondeserializedattribute(v=vs.110).aspx) attributes to restore the dependency on deserialization This is the simplest solution.


##<a id="state"></a> State and data storage

### Where is conversation state stored?

Bot data (that is, the user, conversation, and private conversation property bags) are stored using the Connector's `IBotState` interface.  All of these property bags are scoped by your Bot's ID.  The user property bag is keyed by user ID,
the conversation property bag is keyed by conversation ID, and the private conversation property bag is keyed by both user ID and conversation ID.  Given the same keys, you'll get the same property bags back.

In the Node and C# builder SDKs, the dialog stack and dialog data are both stored as entries in the private conversation property bag.  The C# implementation uses binary serialization, and the Node implementation uses JSON serialization.

This `IBotState` REST interface is implemented by two services.

* The Bot Framework Connector provides a cloud service that implements this interface and stores data in Azure.  This data is encrypted at rest and does not intentionally expire.
* The Bot Framework Emulator provides an in-memory implementation of this interface for debugging your bot.  This data expires when the emulator process exits.

If you want to store this data within your data centers, you can provide a custom implementation of the state service.  This can be done at least two ways:

* Use the the REST layer to provide a custom IBotState service.
* Use the builder interfaces in the language (Node.js or C#) layer.


### What is an ETag?  How does it relate to bot data bag storage?

An [ETag](https://en.wikipedia.org/wiki/HTTP_ETag) is a mechanism for [optimistic concurrency control](https://en.wikipedia.org/wiki/Optimistic_concurrency_control).  The bot data bag storage uses ETags to prevent conflicting updates to the data.  If you encounter an ETag "precondition failed" http error, it means that there were multiple "read-modify-write" sequences executing concurrently for that bot data bag.

The dialog stack and state are stored in these bot data bags.  For example, you might see the ETag precondition failed error if your bot is still processing a previous message when it receives a new message for that conversation.

### What causes "precondition failed" (412) or "conflict" (409) HTTP errors?

The Connector's `IBotState` service is used to store the bot data bags (the user, conversation, and private bot data bags, where the private bot data bag includes the dialog stack "control flow" state).  
Concurrency control in the IBotState service is managed by optimistic concurrency (through ETags).

If there is an update conflict (due to a concurrent update to a single bot data bag) during a "read-modify-write" sequence, then:

* If ETags are preserved, then a "precondition failed" (412) HTTP error is thrown from the IBotState service. This is the default behavior in the C# SDK.
* If the ETags are not preserved (ETag is set to "\*"), then the "last write wins" policy will be in effect, which will prevent "precondition failed" (412) at the risk of data loss. This is the default behavior in the Node.js SDK.

### How can I fix "precondition failed" (412) or "conflict" (409) HTTP errors?

The "precondition failed" error message indicates that your bot processed multiple messages for the same conversation at once. If your bot is connected to services that require precisely ordered messages,
you should consider locking the conversation state to make sure messages are not processed in parallel.  In C#, there exists a mechanism (class `LocalMutualExclusion` which implements `IScope`) to
pessimistically serialize the handling of a single conversations with an in-memory semaphore.  You could extend this implementation to use a Redis lease, scoped by the conversation address.

If your bot is not connected to external services or if processing messages in parallel from the same conversation is acceptable, you can add this code to ignore any collisions that occur in the Bot State API. This will allow the last reply to set the conversation state.

```cs
var builder = new ContainerBuilder();
builder
    .Register(c => new CachingBotDataStore(c.Resolve<ConnectorStore>(), CachingBotDataStoreConsistencyPolicy.LastWriteWins))
    .As<IBotDataStore<BotData>>()
    .AsSelf()
    .InstancePerLifetimeScope();
builder.Update(Conversation.Container);
```

### Is there a limit on the amount of data I can store using the State API?

Yes, each state store (that is, the User store, Conversation store, etc.) may be up to 32kb [see Bot State API][StateAPI]

### How do I version the bot data stored through the State API?

The State Service allows you to persist progress through the dialogs in a conversation, so that a user can return to a conversation with a bot days later without losing their position.  But if you change your bot's code, the bot data property bags stored through the State API are not automatically cleared.  You will have to decide whether the bot data should be cleared based on whether your newer code is compatible with older versions of your data.  You can accomplish this in a few ways:

* During development of your bot, if you want to manually reset the conversation's dialog stack and state, you can use the ` /deleteprofile` command (with the leading space so it's not interpreted by the channel) to clear out the state.
* During production usage of your bot, you can version your bot data so that if you bump the version, the associated data is cleared.  This can be accomplished in Node using the exiting middleware or in C# using an IPostToBot implementation.

If the dialog stack cannot be deserialized correctly (due to serialization format changes or because the code has changed too much), the conversation state will be reset.


## LUIS

### What are the possible machine-readable resolutions of the LUIS builtin date, time, duration, and set entities?

There is a list of examples available in the [Pre-built entities section][LUISPreBuiltEntities] of the LUIS documentation.

### How can I use more than the maximum number of LUIS intents?

You might consider splitting up your model and calling the LUIS service in series or parallel.

### How can I use more than one LUIS model?

Both the Node and C# versions of the SDK support calling multiple LUIS models from a single LUIS intent dialog.

There are few caveats to keep in mind:

* Using multiple LUIS models assumes the LUIS models have non-overlapping sets of intents.
* Using multiple LUIS models assumes the scores from different models are comparable, to select the “best matched intent” across multiple models.
* Using multiple LUIS models means that if an intent matches one model, it will also strongly match the "none" intent of the other models.  You can avoid selecting the "none" intent in this situation (the Node SDK will automatically scale down the score for "none" intents to avoid this issue).

### Where can I get more help on LUIS?

Here are some videos about LUIS:

* [Introduction to Language Understanding Intelligent Service (LUIS) - Microsoft Cognitive Services](https://www.youtube.com/watch?v=jWeLajon9M8)
* [Advanced Learning Session for Language Understanding Intelligent Service (LUIS) ](https://www.youtube.com/watch?v=39L0Gv2EcSk)

You can access LUIS experts at the [Language Understanding Intelligent Service Forum](https://social.msdn.microsoft.com/forums/azure/en-US/home?forum=LUIS).

## Community resources

### What are some community-authored dialogs?

* [AuthBot](https://www.nuget.org/packages/AuthBot) - Azure Active Directory authentication
* [BestMatchDialog](http://www.garypretty.co.uk/2016/08/01/bestmatchdialog-for-microsoft-bot-framework-now-available-via-nuget/) - Regular expression-based dispatch of user text to dialog methods

### What are some community-authored templates?

* [ES6 BotBuilder](https://github.com/brene/botbuilder-es6-template) - ES6 Bot Builder template

### Where can I get more help?

The [GitHub issues](https://github.com/Microsoft/BotBuilder/issues) has an active forum.  [Gitter](https://gitter.im/Microsoft/BotBuilder) has active discussions.  [Stack Overflow](http://stackoverflow.com/questions/tagged/botframework) has a list of tagged Bot Framework questions.




[LUISPreBuiltEntities]: https://docs.microsoft.com/en-us/azure/cognitive-services/luis/pre-builtentities
[BotFrameworkIDGuide]: resources-identifiers-guide.md
[StateAPI]: https://docs.botframework.com/en-us/restapi/state/
[TroubleshootingAuth]: troubleshoot-authentication-problems.md

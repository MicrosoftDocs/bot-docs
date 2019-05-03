---
title: Troubleshooting bots | Microsoft Docs
description: Troubleshoot general problems in bot development using technical frequently asked questions.
author: DeniseMak
ms.author: v-demak
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 04/30/2019
---

# Troubleshooting general problems
These frequently asked questions can help you to troubleshoot common bot development or operational issues.

## How can I troubleshoot issues with my bot?

1. Debug your bot's source code with [Visual Studio Code](debug-bots-locally-vscode.md) or [Visual Studio](https://docs.microsoft.com/en-us/visualstudio/debugger/navigating-through-code-with-the-debugger?view=vs-2017).
1. Test your bot using the [emulator](bot-service-debug-emulator.md) before you deploy it to the cloud.
1. Deploy your bot to a cloud hosting platform such as Azure and then test connectivity to your bot by using the built-in web chat control on your bot's dashboard in the <a href="https://portal.azure.com" target="_blank">Azure Portal</a>. If you encounter issues with your bot after you deploy it to Azure, you might consider using this blog article: [Understanding Azure troubleshooting and support](https://azure.microsoft.com/en-us/blog/understanding-azure-troubleshooting-and-support/).
1. Rule out [authentication][TroubleshootingAuth] as a possible issue.
1. Test your bot on Skype. This will help you to validate the end-to-end user experience.
1. Consider testing your bot on channels that have additional authentication requirements such as Direct Line or Web Chat.
1. Review the how-to [debug a bot](bot-service-debug-bot.md) and the other debugging articles in that section.

## How can I troubleshoot authentication issues?

For details about troubleshooting authentication issues with your bot, see [troubleshooting][TroubleshootingAuth] Bot Framework authentication.

## I'm using the Bot Framework SDK for .NET. How can I troubleshoot issues with my bot?

**Look for exceptions.**  
In Visual Studio 2017, go to **Debug** > **Windows** > **Exception Settings**. In the **Exceptions Settings** window, select the **Break When Thrown** checkbox next to **Common Language Runtime Exceptions**. You may also see diagnostics output in your Output window when there are thrown or unhandled exceptions.

**Look at the call stack.**  
In Visual Studio, you can choose whether or you are debugging [Just My Code](https://msdn.microsoft.com/en-us/library/dn457346.aspx) or not. Examining the full call stack may provide additional insight into any issues.

**Ensure all dialog methods end with a plan to handle the next message.**  
All dialog steps need to feed into the next step of the waterfall, or end the current dialog to pop it off the stack. If a step is not correctly handled, the conversation will not continue like you expect. Take a look at the concept article for [dialogs](v4sdk/bot-builder-concept-dialog.md) for more on dialogs.

## Why doesn't the Typing activity do anything?
Some channels do not support transient typing updates in their client.

## What is the difference between the Connector library and Builder library in the SDK?

The Connector library is the exposition of the REST API. The Builder library adds the conversational dialog programming model and other features such as prompts, waterfalls, chains, and guided form completion. The Builder library also provides access to cognitive services such as LUIS.

## What causes an error with HTTP status code 429 "Too Many Requests"?

An error response with HTTP status code 429 indicates that too many requests have been issued in a given amount of time. The body of the response should include an explanation of the problem and may also specify the minimum required interval between requests. One possible source for this error is [ngrok](https://ngrok.com/). If you are on a free plan and running into ngrok's limits, go to the pricing and limits page on their website for more [options](https://ngrok.com/product#pricing). 

## Why aren't my bot messages getting received by the user?

The message activity generated in response must be correctly addressed, otherwise it won’t arrive at its intended destination. In the vast majority of cases you will not need to handle this explicitly; the SDK takes care of addressing the message activity for you. 

Correctly addressing an activity means including the appropriate *conversation reference* details along with details about the sender and the recipient. In most cases, the message activity is sent in response to one that had arrived. Therefore, the addressing details can be taken from the inbound activity. 

If you examine traces or audit logs, you can check to make sure your messages are correctly addressed. If they aren't, set a breakpoint in your bot and see where the IDs are being set for your message.

## How can I run background tasks in ASP.NET? 

In some cases, you may want to initiate an asynchronous task that waits for a few seconds and then executes some code to clear the user profile or reset conversation/dialog state. For details about how to achieve this, see [How to run Background Tasks in ASP.NET](https://www.hanselman.com/blog/HowToRunBackgroundTasksInASPNET.aspx). In particular, consider using [HostingEnvironment.QueueBackgroundWorkItem](https://msdn.microsoft.com/en-us/library/dn636893(v=vs.110).aspx). 


## How do user messages relate to HTTPS method calls?

When the user sends a message over a channel, the Bot Framework web service will issue an HTTPS POST to the bot's web service endpoint. The bot may send zero, one, or many messages back to the user on that channel, by issuing a separate HTTPS POST to the Bot Framework for each message that it sends.

## My bot is slow to respond to the first message it receives. How can I make it faster?

Bots are web services and some hosting platforms, including Azure, automatically put the service to sleep if it does not receive traffic for a certain period of time. If this happens to your bot, it must restart from scratch the next time it receives a message, which makes its response much slower than if it was already running.

Some hosting platforms enable you to configure your service so that it will not be put to sleep. To do this in Azure, navigate to your bot's service in the [Azure Portal](https://portal.azure.com), select **Application settings**, and then select **Always on**. This option is available in most, but not all, service plans.

## How can I guarantee message delivery order?

The Bot Framework will preserve message ordering as much as possible. For example, if you send message A and wait for the completion of that HTTP operation before you initiate another HTTP operation to send message B, the Bot Framework will automatically understand that message A should precede message B. However, in general, message delivery order cannot be guaranteed since the channel is ultimately responsible for message delivery and may reorder messages. To mitigate the risk of messages being delivered in the wrong order, you might choose to implement a time delay between messages.

## How can I intercept all messages between the user and my bot?

Using the Bot Framework SDK for .NET, you can provide implementations of the `IPostToBot` and `IBotToUser` interfaces to the `Autofac` dependency injection container. Using the Bot Framework SDK for any language, you can use middleware for much the same purpose. The [BotBuilder-Azure](https://github.com/Microsoft/BotBuilder-Azure) repository contains C# and Node.js libraries that will log this data to an Azure table.

## Why are parts of my message text being dropped?

The Bot Framework and many channels interpret text as if it were formatted with [Markdown](https://en.wikipedia.org/wiki/Markdown). Check to see if your text contains characters that may be interpreted as Markdown syntax.

## How can I support multiple bots at the same bot service endpoint? 

This [sample](https://github.com/Microsoft/BotBuilder/issues/2258#issuecomment-280506334) shows how to configure the `Conversation.Container` with the right `MicrosoftAppCredentials` and use a simple `MultiCredentialProvider` to authenticate multiple App IDs and passwords.

## Identifiers

## How do identifiers work in the Bot Framework?

For details about identifiers in the Bot Framework, see the Bot Framework [guide to identifiers][BotFrameworkIDGuide].

## How can I get access to the user ID?

SMS and email messages will provide the raw user ID in the `from.Id` property. In Skype messages, the `from.Id` property will contain a unique ID for the user which differs from the user's Skype ID. If you need to connect to an existing account, you can use a sign-in card and implement your own OAuth flow to connect the user ID to your own service's user ID.

## Why are my Facebook user names not showing anymore?

Did you change your Facebook password? Doing so will invalidate the access token, and you will need to update your bot's configuration settings for the Facebook Messenger channel in the <a href="https://portal.azure.com" target="_blank">Azure Portal</a>.

## Why is my Kik bot replying "I'm sorry, I can't talk right now"?

Bots in development on Kik are allowed 50 subscribers. After 50 unique users have interacted with your bot, any new user that attempts to chat with your bot will receive the message "I'm sorry, I can't talk right now." For more information, see [Kik documentation](https://botsupport.kik.com/hc/en-us/articles/225764648-How-can-I-share-my-bot-with-Kik-users-while-in-development-).

## How can I use authenticated services from my bot?

For Azure Active Directory authentication, see the adding authentication [V3](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-tutorial-authentication?view=azure-bot-service-3.0&tabs=csharp) | [V4](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-tutorial-authentication?view=azure-bot-service-4.0&tabs=csharp). 

> [!NOTE] 
> If you add authentication and security functionality to your bot, you should ensure that the patterns you implement in your code comply with the security standards that are appropriate for your application.

## How can I limit access to my bot to a pre-determined list of users?

Some channels, such as SMS and email, provide unscoped addresses. In these cases, messages from the user will contain the raw user ID in the `from.Id` property.

Other channels, such as Skype, Facebook, and Slack, provide either scoped or tenanted addresses in a way that prevents a bot from being able to predict a user’s ID ahead of time. In these cases, you will need to authenticate the user via a login link or shared secret in order to determine whether or not they are authorized to use the bot.

## Why does my Direct Line 1.1 conversation start over after every message?

If your Direct Line conversation appears to start over after every message, the `from` property is likely missing or `null` in messages that your Direct Line client sent to the bot. When a Direct Line client sends a message with the `from` property either missing or `null`, the Direct Line service automatically allocates an ID, so every message that the client sends will appear to originate from a new, different user.

To fix this, set the `from` property in each message that the Direct Line client sends to a stable value that uniquely represents the user who is sending the message. For example, if a user is already signed-in to a webpage or app, you might use that existing user ID as the value of the `from` property in messages that the user sends. Alternatively, you might choose to generate a random user ID on page-load or on application-load, store that ID in a cookie or device state, and use that ID as the value of the `from` property in messages that the user sends.

## What causes the Direct Line 3.0 service to respond with HTTP status code 502 "Bad Gateway"?
Direct Line 3.0 returns HTTP status code 502 when it tries to contact your bot but the request does not complete successfully. This error indicates that either the bot returned an error or the request timed out. For more information about errors that your bot generates, go to the bot's dashboard within the <a href="https://portal.azure.com" target="_blank">Azure Portal</a> and click the "Issues" link for the affected channel. If you have Application Insights configured for your bot, you can also find detailed error information there. 

::: moniker range="azure-bot-service-3.0"

## What is the IDialogStack.Forward method in the Bot Framework SDK for .NET?

The primary purpose of `IDialogStack.Forward` is to reuse an existing child dialog that is often "reactive", where the child dialog (in `IDialog.StartAsync`) waits for an object `T` with some `ResumeAfter` handler. In particular, if you have a child dialog that waits for an `IMessageActivity` `T`, you can forward the incoming `IMessageActivity` (already received by some parent dialog) by using the `IDialogStack.Forward` method. For example, to forward an incoming `IMessageActivity` to a `LuisDialog`, call `IDialogStack.Forward` to push the `LuisDialog` onto the dialog stack, run the code in `LuisDialog.StartAsync` until it schedules a wait for the next message, and then immediately satisfy that wait with the forwarded `IMessageActivity`.

`T` is usually an `IMessageActivity`, since `IDialog.StartAsync` is typically constructed to wait for that type of activity. You might use `IDialogStack.Forward` to a `LuisDialog` as a mechanism to intercept messages from the user for
some processing before forwarding the message to an existing `LuisDialog`. Alternatively, you can also use `DispatchDialog` with `ContinueToNextGroup` for that purpose.

You would expect to find the forwarded item in the first `ResumeAfter` handler (e.g. `LuisDialog.MessageReceived`) that is scheduled by `StartAsync`.

::: moniker-end

## What is the difference between "proactive" and "reactive"?

From the perspective of your bot, "reactive" means that the user initiates the conversation by sending a message to the bot, and the bot reacts by responding to that message. In contrast, "proactive" means that the bot initiates the conversation by sending the first message to the user. For example, a bot may send a proactive message to notify a user when a timer expires or an event occurs.

## How can I send proactive messages to the user?

For examples that show how to send proactive messages, see the [C# V4 samples](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/16.proactive-messages) and [Node.js V4 samples](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/16.proactive-messages) within the BotBuilder-Samples repository on GitHub.

## How can I reference non-serializable services from my C# dialogs?

There are multiple options:

* Resolve the dependency through `Autofac` and `FiberModule.Key_DoNotSerialize`. This is the cleanest solution.
* Use [NonSerialized](https://msdn.microsoft.com/en-us/library/system.nonserializedattribute(v=vs.110).aspx) and [OnDeserialized](https://msdn.microsoft.com/en-us/library/system.runtime.serialization.ondeserializedattribute(v=vs.110).aspx) attributes to restore the dependency on deserialization. This is the simplest solution.
* Do not store that dependency, so that it won't be serialized. This solution, while technically feasible, is not recommended.
* Use the reflection serialization surrogate. This solution may not be feasible in some cases and risks serializing too much.

::: moniker range="azure-bot-service-3.0"

## Where is conversation state stored?

Data in the user, conversation, and private conversation property bags is stored using the Connector's `IBotState` interface. Each property bag is scoped by the bot's ID. The user property bag is keyed by user ID, the conversation property bag is keyed by conversation ID, and the private conversation property bag is keyed by both user ID and conversation ID. 

If you use the Bot Framework SDK for .NET or the Bot Framework SDK for Node.js to build your bot, the dialog stack and dialog data will both automatically be stored as entries in the private conversation property bag. The C# implementation uses binary serialization, and the Node.js implementation uses JSON serialization.

The `IBotState` REST interface is implemented by two services.

* The Bot Framework Connector provides a cloud service that implements this interface and stores data in Azure.  This data is encrypted at rest and does not intentionally expire.
* The Bot Framework Emulator provides an in-memory implementation of this interface for debugging your bot. This data expires when the emulator process exits.

If you want to store this data within your data centers, you can provide a custom implementation of the state service. This can be done at least two ways:

* Use the REST layer to provide a custom `IBotState` service.
* Use the Builder interfaces in the language (Node.js or C#) layer.

> [!IMPORTANT]
> The Bot Framework State Service API is not recommended for production environments, and may be deprecated in a future release. It is recommended that you update your bot code to use the in-memory storage for testing purposes or use one of the **Azure Extensions** for production bots. For more information, see the **Manage state data** topic for [.NET](~/dotnet/bot-builder-dotnet-state.md) or [Node](~/nodejs/bot-builder-nodejs-state.md) implementation.

::: moniker-end

## What is an ETag?  How does it relate to bot data bag storage?

An [ETag](https://en.wikipedia.org/wiki/HTTP_ETag) is a mechanism for [optimistic concurrency control](https://en.wikipedia.org/wiki/Optimistic_concurrency_control). The bot data bag storage uses ETags to prevent conflicting updates to the data. An ETag error with HTTP status code 412 "Precondition Failed" indicates that there were multiple "read-modify-write" sequences executing concurrently for that bot data bag.

The dialog stack and state are stored in bot data bags. For example, you might see the "Precondition Failed" ETag error if your bot is still processing a previous message when it receives a new message for that conversation.

## What causes an error with HTTP status code 412 "Precondition Failed" or HTTP status code 409 "Conflict"?

The Connector's `IBotState` service is used to store the bot data bags (i.e., the user, conversation, and private bot data bags, where the private bot data bag includes the dialog stack "control flow" state). Concurrency control in the `IBotState` service is managed by optimistic concurrency via ETags. If there is an update conflict (due to a concurrent update to a single bot data bag) during a "read-modify-write" sequence, then:

* If ETags are preserved, an error with HTTP status code 412 "Precondition Failed" is thrown from the `IBotState` service. This is the default behavior in the Bot Framework SDK for .NET.
* If ETags are not preserved (i.e., ETag is set to `\*`), then the "last write wins" policy will be in effect, which prevents the "Precondition Failed" error but risks data loss. This is the default behavior in the Bot Framework SDK for Node.js.

## How can I fix "Precondition Failed" (412) or "Conflict" (409) errors?

These errors indicate that your bot processed multiple messages for the same conversation at once. If your bot is connected to services that require precisely ordered messages,
you should consider locking the conversation state to make sure messages are not processed in parallel. 

::: moniker range="azure-bot-service-3.0"

The Bot Framework SDK for .NET provides a mechanism (class `LocalMutualExclusion` which implements `IScope`) to
pessimistically serialize the handling of a single conversations with an in-memory semaphore. You could extend this implementation to use a Redis lease, scoped by the conversation address.

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

## How do I version the bot data stored through the State API?

> [!IMPORTANT]
> The Bot Framework State Service API is not recommended for production environments or v4 bots, and may be fully deprecated in a future release. It is recommended that you update your bot code to use the in-memory storage for testing purposes or use one of the **Azure Extensions** for production bots. For more information, see the [Manage state data](v4sdk/bot-builder-howto-v4-state.md) topic.

The State service enables you to persist progress through the dialogs in a conversation so that a user can return to a conversation with a bot later without losing their position. To preserve this, the bot data property bags that are stored via the State API are not automatically cleared when you modify the bot's code. You should decide whether or not the bot data should be cleared, based upon whether your modified code is compatible with older versions of your data. 

* If you want to manually reset the conversation's dialog stack and state during development of your bot, you can use the ` /deleteprofile` command to delete state data. Make sure to include the leading space in this command, to prevent the channel from interpreting it.
* After your bot has been deployed to production, you can version your bot data so that if you bump the version, the associated state data is cleared. With the Bot Framework SDK for Node.js, this can be accomplished using middleware and with the Bot Framework SDK for .NET, this can be accomplished using an `IPostToBot` implementation.

> [!NOTE]
> If the dialog stack cannot be deserialized correctly, due to serialization format changes or because the code has changed too much, the conversation state will be reset.

::: moniker-end

## What are the possible machine-readable resolutions of the LUIS built-in date, time, duration, and set entities?

For a list of examples, see the [Pre-built entities section][LUISPreBuiltEntities] of the LUIS documentation.

## How can I use more than the maximum number of LUIS intents?

You might consider splitting up your model and calling the LUIS service in series or parallel.

## How can I use more than one LUIS model?

Both the Bot Framework SDK for Node.js and the Bot Framework SDK for .NET support calling multiple LUIS models from a single LUIS intent dialog. Keep in mind the following caveats:

* Using multiple LUIS models assumes the LUIS models have non-overlapping sets of intents.
* Using multiple LUIS models assumes the scores from different models are comparable, to select the "best matched intent" across multiple models.
* Using multiple LUIS models means that if an intent matches one model, it will also strongly match the "none" intent of the other models. You can avoid selecting the "none" intent in this situation; the Bot Framework SDK for Node.js will automatically scale down the score for "none" intents to avoid this issue.

## Where can I get more help on LUIS?

* [Introduction to Language Understanding (LUIS) - Microsoft Cognitive Services](https://www.youtube.com/watch?v=jWeLajon9M8) (video)
* [Advanced Learning Session for Language Understanding (LUIS) ](https://www.youtube.com/watch?v=39L0Gv2EcSk) (video)
* [LUIS documentation](/azure/cognitive-services/LUIS/Home)
* [Language Understanding Forum](https://social.msdn.microsoft.com/forums/azure/en-US/home?forum=LUIS) 


## What are some community-authored dialogs?

* [BotAuth](https://www.nuget.org/packages/BotAuth) - Azure Active Directory authentication
* [BestMatchDialog](http://www.garypretty.co.uk/2016/08/01/bestmatchdialog-for-microsoft-bot-framework-now-available-via-nuget/) - Regular expression-based dispatch of user text to dialog methods

## What are some community-authored templates?

* [ES6 BotBuilder](https://github.com/brene/botbuilder-es6-template) - ES6 Bot Builder template

## Why do I get an Authorization_RequestDenied exception when creating a bot?

Permission to create Azure Bot Service bots are managed through the Azure Active Directory (AAD) portal. If permissions are not properly configured in the [AAD portal](http://aad.portal.azure.com), users will get the **Authorization_RequestDenied** exception when trying to create a bot service.

First check whether you are a "Guest" of the directory:

1. Sign-in to [Azure portal](http://portal.azure.com).
2. Click **All services** and search for *active*.
3. Select **Azure Active Directory**.
4. Click **Users**.
5. Find the user from the list and ensure that the **User Type** is not a **Guest**.

![Azure Active Directory User-type](~/media/azure-active-directory/user_type.png)

Once you verified that you are not a **Guest**, then to ensure that users within an active directory can create bot service, the directory administrator needs to configure the following settings:

1. Sign-in to [AAD portal](http://aad.portal.azure.com). Go to **Users and groups** and select **User settings**.
2. Under **App registration** section, set **Users can register applications** to **Yes**. This allows users in your directory to create bot service.
3. Under the **External users** section, set **Guest users permissions are limited** to **No**. This allows guest users in your directory to create bot service.

![Azure Active Directory Admin Center](~/media/azure-active-directory/admin_center.png)

## Why can't I migrate my bot?

If you are having issues migrating your bot, it might be because the bot belongs to a directory other than your default directory. Try these steps:

1. From the target directory, add a new user (via email address) that is not a member of the default directory, grant the user contributor role on the subscriptions that are the target of the migration.

2. From [Dev Portal](https://dev.botframework.com), add the user’s email address as co-owners of the bot that should be migrated. Then sign out.

3. Sign in to [Dev Portal](https://dev.botframework.com) as the new user and proceed to migrate the bot.

## Where can I get more help?

* Leverage the information in previously answered questions on [Stack Overflow](https://stackoverflow.com/questions/tagged/botframework), or post your own questions using the `botframework` tag. Please note that Stack Overflow has guidelines such as requiring a descriptive title, a complete and concise problem statement, and sufficient details to reproduce your issue. Feature requests or overly broad questions are off-topic; new users should visit the [Stack Overflow Help Center](https://stackoverflow.com/help/how-to-ask) for more details.
* Consult [BotBuilder issues](https://github.com/Microsoft/BotBuilder/issues) in GitHub for information about known issues with the Bot Framework SDK, or to report a new issue.
* Leverage the information in the BotBuilder community discussion on [Gitter](https://gitter.im/Microsoft/BotBuilder).




[LUISPreBuiltEntities]: /azure/cognitive-services/luis/pre-builtentities
[BotFrameworkIDGuide]: bot-service-resources-identifiers-guide.md
[StateAPI]: ~/rest-api/bot-framework-rest-state.md
[TroubleshootingAuth]: bot-service-troubleshoot-authentication-problems.md


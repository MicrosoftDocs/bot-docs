---
title: Troubleshooting bots - Bot Service
description: Troubleshoot general problems in bot development using technical frequently asked questions.
author: DeniseMak
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 02/20/2020
---

# Troubleshoot general

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

These frequently asked questions can help you to troubleshoot common bot development or operational issues.

## How can I troubleshoot issues with my bot?

1. Debug your bot's source code with [Visual Studio Code](debug-bots-locally-vscode.md) or [Visual Studio](https://docs.microsoft.com/visualstudio/debugger/navigating-through-code-with-the-debugger).
1. Test your bot using the [emulator](bot-service-debug-emulator.md) before you deploy it to the cloud.
1. Deploy your bot to a cloud hosting platform such as Azure and then test connectivity to your bot by using the built-in web chat control on your bot's dashboard in the <a href="https://portal.azure.com" target="_blank">Azure Portal</a>. If you encounter issues with your bot after you deploy it to Azure, you might consider using this blog article: [Understanding Azure troubleshooting and support](https://azure.microsoft.com/blog/understanding-azure-troubleshooting-and-support/).
1. Rule out [authentication][TroubleshootingAuth] as a possible issue.
1. Test your bot on Web Chat, Teams, or any other channel you intend to use with your bot. This will help you to validate the end-to-end user experience.
1. Consider testing your bot on channels that have additional authentication requirements such as Direct Line or Web Chat.
1. Review the how-to [debug a bot](bot-service-debug-bot.md) and the other debugging articles in that section.

## How can I troubleshoot authentication issues?

For details about troubleshooting authentication issues with your bot, see [troubleshooting][TroubleshootingAuth] Bot Framework authentication.

## I'm using the Bot Framework SDK for .NET. How can I troubleshoot issues with my bot?

**Look for exceptions.**
In Visual Studio 2019, go to **Debug** > **Windows** > **Exception Settings**. In the **Exceptions Settings** window, select the **Break When Thrown** checkbox next to **Common Language Runtime Exceptions**. You may also see diagnostics output in your Output window when there are thrown or unhandled exceptions.

**Look at the call stack.**
In Visual Studio, you can choose whether or you are debugging [Just My Code](https://msdn.microsoft.com/library/dn457346.aspx) or not. Examining the full call stack may provide additional insight into any issues.

**Ensure all dialog methods end with a plan to handle the next message.**
All dialog steps need to feed into the next step of the waterfall, or end the current dialog to pop it off the stack. If a step is not correctly handled, the conversation will not continue like you expect. Take a look at the concept article for [dialogs](v4sdk/bot-builder-concept-dialog.md) for more on dialogs.


## What causes an error with HTTP status code 429 "Too Many Requests"?

An error response with HTTP status code 429 indicates that too many requests have been issued in a given amount of time. The body of the response should include an explanation of the problem and may also specify the minimum required interval between requests. One possible source for this error is the [ngrok](https://ngrok.com/) tool. If you are on a free plan and running into ngrok's limits, go to the pricing and limits page on their website for more [options](https://ngrok.com/product#pricing).

## Why aren't my bot messages getting received by the user?

The message activity generated in response must be correctly addressed, otherwise it won't arrive at its intended destination. In the vast majority of cases you will not need to handle this explicitly; the SDK takes care of addressing the message activity for you.

Correctly addressing an activity means including the appropriate *conversation IDs* details along with details about the sender. In most cases, the message activity is sent in response to one that had arrived. Therefore, the addressing details can be taken from the inbound activity.

If you examine traces or audit logs, you can check to make sure your messages are correctly addressed. If they aren't, set a breakpoint in your bot and see where the IDs are being set for your message.

## How can I run background tasks in ASP.NET?

In some cases, you may want to initiate an asynchronous task that waits for a few seconds and then executes some code to clear the user profile or reset conversation/dialog state. For details about how to achieve this, see [How to run Background Tasks in ASP.NET](https://www.hanselman.com/blog/HowToRunBackgroundTasksInASPNET.aspx). In particular, consider using [HostingEnvironment.QueueBackgroundWorkItem](https://msdn.microsoft.com/library/dn636893(v=vs.110).aspx).

## My bot is slow to respond to the first message it receives. How can I make it faster?

Bots are web services and some hosting platforms, including Azure, automatically put the service to sleep if it does not receive traffic for a certain period of time. If this happens to your bot, it must restart from scratch the next time it receives a message, which makes its response much slower than if it was already running.

Some hosting platforms enable you to configure your service so that it will not be put to sleep. If your bot is hosted on Azure Bot Service Web Apps, navigate to your bot's service in the [Azure Portal](https://portal.azure.com), select **Application settings**, and then select **Always on**. This option is available in most, but not all, service plans.

## How can I guarantee message delivery order?

The Bot Framework will preserve message ordering as much as possible. For example, if you send message A and wait for the completion of that HTTP operation before you initiate another HTTP operation to send message B. Some channels, such as SMS and email, do not guarentee ordering to the user's device.

## Why are parts of my message text being dropped?

The Bot Framework and many channels interpret text as if it were formatted with [Markdown](https://en.wikipedia.org/wiki/Markdown). Check to see if your text contains characters that may be interpreted as Markdown syntax.

## How can I support multiple bots at the same bot service endpoint?

This [sample](https://github.com/Microsoft/BotBuilder/issues/2258#issuecomment-280506334) shows how to configure the `Conversation.Container` with the right `MicrosoftAppCredentials` and use a simple `MultiCredentialProvider` to authenticate multiple App IDs and passwords.

## Identifiers
___

## How do identifiers work in the Bot Framework?

For details about identifiers in the Bot Framework, see the Bot Framework [guide to identifiers][BotFrameworkIDGuide].

## How can I get access to the user ID?

Bot Framework channels present the user's ID in the `from.Id` field of any Activity sent by the user. SMS and email messages will provide the raw user ID in this property. Some channels obscure the `from.Id` property so it contains unique ID for the user which differs from the user's ID in the channel. If you need to connect to an existing account, you can use a sign-in card and implement your own OAuth flow to connect the user ID to your own service's user ID.

## Why are my Facebook user names not showing anymore?

Did you change your Facebook password? Doing so will invalidate the access token, and you will need to update your bot's configuration settings for the Facebook Messenger channel in the <a href="https://portal.azure.com" target="_blank">Azure Portal</a>.

## Why is my Kik bot replying "I'm sorry, I can't talk right now"?

Bots in development on Kik are allowed 50 subscribers. After 50 unique users have interacted with your bot, any new user that attempts to chat with your bot will receive the message "I'm sorry, I can't talk right now." For more information, see [Kik documentation](https://botsupport.kik.com/hc/articles/225764648-How-can-I-share-my-bot-with-Kik-users-while-in-development-).

## How can I use authenticated services from my bot?

For Azure Active Directory authentication, see the [Add authentication to your bot](v4sdk/bot-builder-authentication.md) tutorial.

> [!NOTE]
> If you add authentication and security functionality to your bot, you should ensure that the patterns you implement in your code comply with the security standards that are appropriate for your application.

## How can I limit access to my bot to a pre-determined list of users?

Some channels, such as SMS and email, provide unscoped addresses. In these cases, messages from the user will contain the raw user ID in the `from.Id` property.

Other channels, such as Facebook and Slack, provide either scoped or tenanted addresses in a way that prevents a bot from being able to predict a user's ID ahead of time. In these cases, you will need to authenticate the user via a login link or shared secret in order to determine whether or not they are authorized to use the bot.

## Why does my Direct Line 1.1 conversation start over after every message?

> [!NOTE]
>
> This sections does not apply to the latest version of the Direct Line protocol, 3.0

If your Direct Line conversation appears to start over after every message, the `from` property is likely missing or `null` in messages that your Direct Line client sent to the bot. When a Direct Line client sends a message with the `from` property either missing or `null`, the Direct Line service automatically allocates an ID, so every message that the client sends will appear to originate from a new, different user.

To fix this, set the `from` property in each message that the Direct Line client sends to a stable value that uniquely represents the user who is sending the message. For example, if a user is already signed-in to a webpage or app, you might use that existing user ID as the value of the `from` property in messages that the user sends. Alternatively, you might choose to generate a random user ID on page-load or on application-load, store that ID in a cookie or device state, and use that ID as the value of the `from` property in messages that the user sends.

## What causes the Direct Line 3.0 service to respond with HTTP status code 502 "Bad Gateway"?
Direct Line 3.0 returns HTTP status code 502 when it tries to contact your bot but the request does not complete successfully. This error indicates that either the bot returned an error or the request timed out. For more information about errors that your bot generates, go to the bot's dashboard within the <a href="https://portal.azure.com" target="_blank">Azure Portal</a> and click the "Issues" link for the affected channel. If you have Application Insights configured for your bot, you can also find detailed error information there.

## Why do I get an Authorization_RequestDenied exception when creating a bot?

Permission to create Azure Bot Service bots are managed through the Azure Active Directory (AAD) portal. If permissions are not properly configured in the [AAD portal](https://aad.portal.azure.com), users will get the **Authorization_RequestDenied** exception when trying to create a bot service.

First check whether you are a "Guest" of the directory:

1. Sign-in to [Azure portal](https://portal.azure.com).
2. Click **All services** and search for *active*.
3. Select **Azure Active Directory**.
4. Click **Users**.
5. Find the user from the list and ensure that the **User Type** is not a **Guest**.

![Azure Active Directory User-type](~/media/azure-active-directory/user_type.png)

Once you verified that you are not a **Guest**, then to ensure that users within an active directory can create bot service, the directory administrator needs to configure the following settings:

1. Sign-in to [AAD portal](https://aad.portal.azure.com). Go to **Users and groups** and select **User settings**.
2. Under **App registration** section, set **Users can register applications** to **Yes**. This allows users in your directory to create bot service.
3. Under the **External users** section, set **Guest users permissions are limited** to **No**. This allows guest users in your directory to create bot service.

![Azure Active Directory Admin Center](~/media/azure-active-directory/admin_center.png)

## Why can't I migrate my bot?

If your bot is registered in dev.botframework.com, and you want to migrate it to Azure, but are having issues migrating your bot, it might be because the bot belongs to a directory other than your default directory. Try these steps:

1. From the target directory, add a new user (via email address) that is not a member of the default directory, grant the user contributor role on the subscriptions that are the target of the migration.

2. From [Dev Portal](https://dev.botframework.com), add the user's email address as co-owners of the bot that should be migrated. Then sign out.

3. Sign in to [Dev Portal](https://dev.botframework.com) as the new user and proceed to migrate the bot.

## Where can I get more help?

* Leverage the information in previously answered questions on [Stack Overflow](https://stackoverflow.com/questions/tagged/botframework), or post your own questions using the `botframework` tag. Please note that Stack Overflow has guidelines such as requiring a descriptive title, a complete and concise problem statement, and sufficient details to reproduce your issue. Feature requests or overly broad questions are off-topic; new users should visit the [Stack Overflow Help Center](https://stackoverflow.com/help/how-to-ask) for more details.
* Consult [BotBuilder issues](https://github.com/Microsoft/BotBuilder/issues) in GitHub for information about known issues with the Bot Framework SDK, or to report a new issue.
* Leverage the information in the BotBuilder community discussion on [Gitter](https://gitter.im/Microsoft/BotBuilder).



[LUISPreBuiltEntities]: /azure/cognitive-services/luis/pre-builtentities
[BotFrameworkIDGuide]: bot-service-resources-identifiers-guide.md
[StateAPI]: ~/rest-api/bot-framework-rest-state.md
[TroubleshootingAuth]: bot-service-troubleshoot-authentication-problems.md


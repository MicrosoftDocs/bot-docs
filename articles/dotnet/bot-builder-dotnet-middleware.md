---
title: Intercept messages | Microsoft Docs
description: Learn how to intercept messages between user and bot using the Bot Framework SDK for .NET.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 03/01/2019
monikerRange: 'azure-bot-service-3.0'
---
# Intercept messages

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-middleware.md)
> - [Node.js](../nodejs/bot-builder-nodejs-intercept-messages.md)

[!INCLUDE [Introducton to message logging](../includes/snippet-message-logging-intro.md)]

## Intercept and log messages

The following code sample shows how to intercept messages that are exchanged between user and bot 
using the concept of **middleware** in the Bot Framework SDK for .NET. 

First, create a `DebugActivityLogger` class and define a `LogAsync` method to specify what action is taken for each intercepted message. This example just prints some information about each message.

```cs
public class DebugActivityLogger : IActivityLogger
{
    public async Task LogAsync(IActivity activity)
    {
        Debug.WriteLine($"From:{activity.From.Id} - To:{activity.Recipient.Id} - Message:{activity.AsMessageActivity()?.Text}");
    }
}
```

Then, add the following code to `Global.asax.cs`.  Every message that is exchanged between user and bot (in either direction) will now trigger the `LogAsync` method in the `DebugActivityLogger` class. 

```cs
	public class WebApiApplication : System.Web.HttpApplication
	{
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DebugActivityLogger>().AsImplementedInterfaces().InstancePerDependency();
            builder.Update(Conversation.Container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
```

Although this example simply prints some information about each message, you can update the `LogAsync` method to specify the actions that you want to take for each message. 

## Sample code 

For a complete sample that shows how to intercept and log messages using the Bot Framework SDK for .NET, see the <a href="https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/CSharp/core-Middleware" target="_blank">Middleware sample</a> in GitHub. 

## Additional resources

- <a href="/dotnet/api/?view=botbuilder-3.11.0" target="_blank">Bot Framework SDK for .NET Reference</a>
- <a href="https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/CSharp/core-Middleware" target="_blank">Middleware sample (GitHub)</a>

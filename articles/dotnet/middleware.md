---
title: Intercept messages using the Bot Builder SDK for .NET | Microsoft Docs
description: Learn how to intercept messages that are exchanged between user and bot by using the Bot Builder SDK for .NET.
keywords: Bot Framework, dotnet, .NET, Bot Builder, SDK, message logging, intercept message, inspect message, middleware
author: kbrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 02/21/2017
ms.reviewer:
#ROBOTS: Index
---

# Intercept messages

<!--
> [!div class="op_single_selector"]
> * [.NET](~/dotnet/howto-middleware.md)
> * [Node.js](~/nodejs/middleware.md)
>
-->

[!include[Introducton to message logging](~/includes/snippet-message-logging-intro.md)]
This article describes how to intercept messages that are exchanged between user and bot by using the Bot Builder SDK for .NET. 

## Intercept messages

The following code sample shows how to intercept messages that are exchanged between user and bot, 
by using the concept of **middleware** in the Bot Builder SDK for .NET. 

First, create a `DebugActivityLogger` class and define a `LogAsync` method that specifies the action to take for each message that is intercepted. 

```cs
public class DebugActivityLogger : IActivityLogger
{
    public async Task LogAsync(IActivity activity)
    {
        Debug.WriteLine($"From:{activity.From.Id} - To:{activity.Recipient.Id} - Message:{activity.AsMessageActivity()?.Text}");
    }
}
```

Then, add the following code to `Global.asax.cs`. 

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

Now, every message that is exchanged between user and bot (in either direction) will trigger the 
`LogAsync` method in the `DebugActivityLogger` class. 
In this example, we're simply printing some information about each message, but you can 
update the `LogAsync` method as necessary to define the actions that you want to take for each message. 

## Additional resources

- [Bot capabilities](~/design/bot-design-capabilities.md)
- <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html" target="_blank">Builder library</a>
---
title: Intercept messages using the Bot Builder SDK for .NET | Microsoft Docs
description: Learn how to intercept messages that are exchanged between user and bot by using the Bot Builder SDK for .NET.
keywords: Bot Framework, dotnet, Bot Builder, SDK, message logging, intercept message, inspect message
author: kbrandl
manager: rstand
ms.topic: develop-dotnet-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/21/2017
ms.reviewer: rstand
#ROBOTS: Index
---

# Intercept messages using the Bot Builder SDK for .NET
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-middleware.md)
> * [Node.js](bot-framework-nodejs-howto-middleware.md)
>

## Introduction

[!include[Application configuration settings](../includes/snippet-message-logging-intro.md)]
In this article, we'll discuss how to intercept messages that are exchanged between user and bot by using the Bot Builder SDK for .NET. 

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

In this article, we discussed how to intercept the messages that are exchanged between user and bot by using the Bot Builder SDK for .NET. 
To learn more, see:

> [!NOTE]
> To do: Add links to related content (link to 'detailed readme' and 'full C# code' that Matt refers to)
---
title: Intercept messages | Microsoft Docs
description: Learn how to intercept information exchanges between user and bot using the Bot Builder SDK for .NET.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 02/21/2017
ms.reviewer:

---

# Intercept messages

<!--
> [!div class="op_single_selector"]
> * [.NET](~/dotnet/bot-builder-dotnet-howto-middleware.md)
> * [Node.js](~/nodejs/bot-builder-nodejs-intercept-messages.md)
>
-->

[!include[Introducton to message logging](~/includes/snippet-message-logging-intro.md)]

The following code sample shows how to intercept messages that are exchanged between user and bot 
using the concept of **middleware** in the Bot Builder SDK for .NET. 

Create a `DebugActivityLogger` class and define a `LogAsync` method to specify what action is taken for each intercepted message. This example just prints some information about each message.

```cs
public class DebugActivityLogger : IActivityLogger
{
    public async Task LogAsync(IActivity activity)
    {
        Debug.WriteLine($"From:{activity.From.Id} - To:{activity.Recipient.Id} - Message:{activity.AsMessageActivity()?.Text}");
    }
}
```

Then add the following code to `Global.asax.cs`.  Every message that is exchanged between user and bot in either direction will now trigger the `LogAsync` method in the `DebugActivityLogger` class. 

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

## Sample code 

For a complete sample that shows how to intercept and log messages using the Bot Builder SDK for .NET, see the <a href="https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-Middleware" target="_blank">Middleware sample</a> in GitHub. 

## Additional resources

- [Builder library][builderLibrary]

[builderLibrary]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html

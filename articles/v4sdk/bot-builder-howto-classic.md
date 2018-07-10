---
title: How to run .NET SDK V3 bot in SDK V4 | Microsoft Docs
description: Learn how to convert a bot from 3.x to 4.0 by using the classic NuGet package.
keywords: migration, classic bot, convert v3, v3 to v4
author: v-royhar
ms.author: v-royhar
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 4/25/18
monikerRange: 'azure-bot-service-4.0'
---

# How to run .NET SDK V3 bots in SDK 4.0

The **Microsoft.Bot.Builder.Classic** NuGet package eases migration of your bots from version 3.x to version 4.0 of the Microsoft Bot Framework.

**NOTE:** This process only works for **ASP.NET Web Application (.NET Framework)** bots. This does not work on **ASP.NET Core Web Application** bots.

## The process

The process is relatively simple:

- Add the **Microsoft.Bot.Builder.Classic** NuGet package to the project.
    - You may also need to update the **Autofac** NuGet package.
- Update the **Microsoft.Bot.Builder.Classic** namespaces.
- Invoke dialogs using the 4.0 **ITurnContext** based **Conversation.SendAsync()**.

### Add the Microsoft.Bot.Builder.Classic NuGet package

To add the **Microsoft.Bot.Builder.Classic** NuGet package, you use **Manage NuGet Packages** to add the package.

### Update the namespaces

To update the namespaces, delete any of the following `using` statements you find:

```csharp
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Scorables;
```

Then add the following `using` statements in their place:

```csharp
using Microsoft.Bot.Builder.Adapters;
using Microsoft.Bot.Builder.Classic.Dialogs;
using Microsoft.Bot.Builder.Classic.Dialogs.Internals;
using Microsoft.Bot.Builder.Classic.FormFlow;
using Microsoft.Bot.Builder.Classic.Scorables;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Schema;
```

If you have a `[BotAuthentication]` line, delete the line or comment it out.

### Invoke your 3.x dialog

To invoke your 3.x dialog you still use `Conversation.SendAsync`, only now it takes a 4.0 **ITurnContext** instead of an **Activity**.

```csharp
// invoke a Classic V3 IDialog 
await Conversation.SendAsync(turnContext, () => new EchoDialog());
```

If you do not have the **ITurnContext** object, but you have an **Activity** object, you can obtain the **ITurnContext** object this way:

```csharp
BotFrameworkAdapter adapter = new BotFrameworkAdapter("", "");

await adapter.ProcessActivity(this.Request.Headers.Authorization?.Parameter,
        activity,
        async (context) =>
        {
            // Do something with context here. For example, the body of your Post() method may go here.
        });
```

## Fix assembly conflicts

Bots made with the classic NuGet Package will have conflicts with their assemblies. This will appear in the Error List window in Visual Studio after a build.

### If you see "Warning: Found conflicts between different versions of the same dependent assembly"

If you find a warning that begins with the following text: **Found conflicts between different versions of the same dependent assembly**:

- Double-click the warning message. A dialog box appears which asks, "Do you want to fix these conflicts by adding binding redirect records in the application configuration file?"
- Click "Yes".
- Build the project again.

### If you see "Error: Missing Method Exception on startup"

There is a bug with .NET Standard that seems to happen when you take an older .NET 4.6 project and upgrade to 4.6.1 and then attempt to use a .NET standard library with it. Essentially, there are 2 different System.Net.Http assemblies that try to dynamic swap out. The workaround is to add a binding redirect to your Web.config for System.Net.Http. 

If you receive this error, add the follwing to your Web.config file:

```xml
<dependentAssembly>
    <assemblyIdentity name="System.Net.Http" publicKeyToken="B03F5F7F11D50A3A" culture="neutral" />
    <bindingRedirect oldVersion="0.0.0.0-4.2.0.0" newVersion="4.2.0.0" />
</dependentAssembly>
```

For further details on this issue, see [System.Net.Http v4.2.0.0 being copied/loaded from MSBuild tooling #25773](https://github.com/dotnet/corefx/issues/25773).

## Sample of a converted bot

For a bot that has already been converted, you can check out the [EchoBot-Classic](https://github.com/Microsoft/botbuilder-dotnet/tree/master/samples/Microsoft.Bot.Samples.EchoBot-Classic) sample which shows a 3.x bot converted to work with 4.0.

## Limitations
The Microsoft.Bot.Builder.Classic is a .NET 4.61 library only, it does not work on .NET core.

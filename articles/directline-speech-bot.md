---
title: Develop DirectLine Speech Bot | Microsoft Docs
description: Develop DirectLine Speech Bot
keywords: develop driect line speech bot, speech bot
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: get-started-article
ms.service: bot-service
ms.subservice: abs
ms.date: 05/23/2019
monikerRange: 'azure-bot-service-4.0'
---

## Use Direct Line Speech in your bot 

[!INCLUDE [applies-to-v4](includes/applies-to.md)]

Direct Line Speech uses a new WebSocket based streaming capability of Bot Framework to exchange messages between the Direct Line Speech 
channel and your bot. After enabling the Direct Line Speech channel in the Azure Portal, you will need to update your bot to listen for 
and accept these WebSocket connections. These instructions explain how to do this.

## Add the NuGet package
For the Direct Line Speech Preview there are additional NuGet packages you need to add to your bot. These packages are hosted on a myget.org NuGet feed:
1.	Open your bot’s project in Visual Studio.

2.	Go to Manage Nuget Packages under the properties for your bot project.

3.	If you don’t already have it as a source, add `https://botbuilder.myget.org/F/experimental/api/v3/index.json` as a feed from the 
NuGet feed settings in the upper right.

4.	Select this NuGet source and add one of the `Microsoft.Bot.Protocol.StreamingExtensions.NetCore` package.

5.	Accept any prompts to finish adding the package to your project.

## Option #1: Update your .NET Core bot code _if your bot has a BotController.cs_
When you create a new bot from the Azure Portal using one of the templates such as EchoBot, you will get a bot that includes an ASP.NET MVC controller that exposes a single POST endpoint. These instructions explain how to expand that to also expose an endpoint to accept the WebSocket streaming endpoint which is a GET endpoint.
1.	Open BotController.cs under the Controllers folder in your solution

2.	Find the PostAsync method in the class and update its decoration from [HttpPost] to [HttpPost, HttpGet]:
```cs
[HttpPost, HttpGet]
public async Task PostAsync()
{ 
    await _adapter.ProcessAsync(Request, Response, _bot);
}
```

3.	Save and close BotController.cs

4.	Open Startup.cs in the root of your solution.

5.	Add a new namespace:

```cs
using Microsoft.Bot.Protocol.StreamingExtensions.NetCore;
```

6.	In the ConfigureServices method, replace the use of AdapterWithErrorHandler with WebSocketEnabledHttpAdapter in the 
appropriate services.AddSingleton call:

```cs
public void ConfigureServices(IServiceCollection services)
{
    ...    

    // Create the Bot Framework Adapter.
    services.AddSingleton<IBotFrameworkHttpAdapter, WebSocketEnabledHttpAdapter>();

    services.AddTransient<IBot, EchoBot>();

    ...
}
```

7. Still in Startup.cs, navigate to the bottom of the Configure method. Before the call app.UseMvc(); 
call (this is important as the order of Use calls matters), add app.UseWebSockets();. The end of the method should look 
something like the below:

```cs
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    ...

    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.UseWebSockets();
    app.UseMvc();

    ...
}
```

8.	The remainder of your bot code stays the same!

## Option #2: Update your .NET Core bot code _if your bot uses AddBot and UseBotFramework instead of a BotController_

If you have created a bot using v4 of the Bot Builder SDK prior to version 4.3.2, your bot likely does not include a BotController but instead uses the AddBot() and UseBotFramework() methods in the Startup.cs file to expose the POST endpoint where the bot receives messages. To expose the new streaming endpoint, you will need to add a BotController and remove the AddBot() and UseBotFramework() methods. These instructions walk through the changes that need to be made.

1.	Add a new MVC controller to your bot project by adding a file called BotController.cs. Add the controller code to this file:

```cs
[Route("api/messages")]
[ApiController]
public class BotController : ControllerBase
{
    private readonly IBotFrameworkHttpAdapter _adapter;
    private readonly IBot _bot;

    public BotController(IBotFrameworkHttpAdapter adapter, IBot bot)
    {
        _adapter = adapter;
        _bot = bot;
    }

    [HttpPost, HttpGet]
    public async Task ProcessMessageAsync()
    {
        await _adapter.ProcessAsync(Request, Response, _bot);
    }
}
```
2.	In the Startup.cs file, locate the Configure method. Remove the UseBotFramework() line and make sure you have these lines:

```cs
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    ...

    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.UseWebSockets();
    app.UseMvc();

    ...
}
```

3.	Also in the Startup.cs file, locate the ConfigureServices method. Remove the AddBot() line and make sure you have these lines:

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

    services.AddSingleton<ICredentialProvider, ConfigurationCredentialProvider>();

    services.AddSingleton<IChannelProvider, ConfigurationChannelProvider>();

    // Create the Bot Framework Adapter.
    services.AddSingleton<IBotFrameworkHttpAdapter, WebSocketEnabledHttpAdapter>();

    // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
    services.AddTransient<IBot, EchoBot>();
}
```
4.	The remainder of your bot code stays the same!

## Next Steps
> [!div class="nextstepaction"]
> [Connect a bot to Direct Line Speech (Preview)](./bot-service-channel-connect-directlinespeech.md)

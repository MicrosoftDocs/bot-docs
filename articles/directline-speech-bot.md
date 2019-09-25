---
title: Develop DirectLine Speech Bot | Microsoft Docs
description: Develop DirectLine Speech Bot
keywords: develop Direct Line speech bot, speech bot
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 07/15/2019
monikerRange: 'azure-bot-service-4.0'
---

# Use Direct Line Speech in your bot

[!INCLUDE [applies-to-v4](includes/applies-to.md)]

Direct Line Speech uses a new WebSocket based streaming capability of Bot Framework to exchange messages between the Direct Line Speech 
channel and your bot. After enabling the Direct Line Speech channel in the Azure Portal, you will need to update your bot to listen for 
and accept these WebSocket connections. These instructions explain how to do this.

## Add additional NuGet packages

For the Direct Line Speech Preview there are additional NuGet packages you need to add to your bot.

- **Microsoft.Bot.Builder.StreamingExtensions** 4.5.1-preview1

This will also install the following package:

- **Microsoft.Bot.StreamingExtensions** 4.5.1-preview1

If you do not find these at first, make sure that you are including prerelease packages in your search.

## Set the Speak field on Activities you want spoken to the user

You must set the Speak field of any Activity sent from the bot that you want spoken to the user.

```cs
public IActivity Speak(string message)
{
    var activity = MessageFactory.Text(message);
    string body = @"<speak version='1.0' xmlns='https://www.w3.org/2001/10/synthesis' xml:lang='en-US'>
        <voice name='Microsoft Server Speech Text to Speech Voice (en-US, JessaRUS)'>" +
        $"{message}" + "</voice></speak>";
    activity.Speak = body;
    return activity;
}
```

The following snippet shows how to use the previous *Speak* function:

```cs
protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
{
    await turnContext.SendActivityAsync(Speak($"Echo: {turnContext.Activity.Text}"), cancellationToken);
}
```

## Option #1: Update your .NET Core bot code _if your bot has a BotController.cs_

When you create a new bot from the Azure Portal using one of the templates such as EchoBot, you will get a bot that includes an ASP.NET MVC controller that exposes a single POST endpoint. These instructions explain how to expand that to also expose an endpoint to accept the WebSocket streaming endpoint which is a GET endpoint.

1. Open BotController.cs under the Controllers folder in your solution

2. Find the PostAsync method in the class and update its decoration from [HttpPost] to [HttpPost, HttpGet]:

    ```cs
    [HttpPost, HttpGet]
    public async Task PostAsync()
    {
        await _adapter.ProcessAsync(Request, Response, _bot);
    }
    ```

3. Save and close BotController.cs

4. Open Startup.cs in the root of your solution.

5. Add a new namespace:

    ```cs
    using Microsoft.Bot.Builder.StreamingExtensions;
    ```

6. In the ConfigureServices method, replace the use of AdapterWithErrorHandler with WebSocketEnabledHttpAdapter in the 
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

7. Still in Startup.cs, navigate to the bottom of the Configure method. Before the call to `app.UseMvc()`, add a call to `app.UseWebSockets()`. This is important as the order of these _use_ calls matters. The end of the method should look something like this:

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

8. The remainder of your bot code stays the same!

## Option #2: Update your .NET Core bot code _if your bot uses AddBot and UseBotFramework instead of a BotController_

If you have created a bot using v4 of the Bot Builder SDK prior to version 4.3.2, your bot likely does not include a BotController but instead uses the AddBot() and UseBotFramework() methods in the Startup.cs file to expose the POST endpoint where the bot receives messages. To expose the new streaming endpoint, you will need to add a BotController and remove the AddBot() and UseBotFramework() methods. These instructions walk through the changes that need to be made.

1. Add a new MVC controller to your bot project by adding a file called BotController.cs. Add the controller code to this file:

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

2. In the Startup.cs file, locate the Configure method. Remove the UseBotFramework() line and make sure you have these lines:

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

3. Also in the Startup.cs file, locate the ConfigureServices method. Remove the AddBot() line and make sure you have these lines:

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

4. The remainder of your bot code stays the same!

## Additional information

- For a complete example of creating and using a voice enabled bot, see
[Tutorial: Voice-enable your bot using the Speech SDK](https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/tutorial-voice-enable-your-bot-speech-sdk).

- For more information on working with activities, see [how bots work](v4sdk/bot-builder-basics.md) how to [send and receive text messages](v4sdk/bot-builder-howto-send-messages.md).

## Next Steps

> [!div class="nextstepaction"]
> [Connect a bot to Direct Line Speech](./bot-service-channel-connect-directlinespeech.md)

---
title: Develop DirectLine Speech Bot - Bot Service
description: Develop DirectLine Speech Bot
keywords: develop Direct Line speech bot, speech bot
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 11/01/2019
monikerRange: 'azure-bot-service-4.0'
---

# Use Direct Line Speech in your bot

[!INCLUDE [applies-to-v4](includes/applies-to.md)]

Direct Line Speech uses a new WebSocket based streaming capability of Bot Framework to exchange messages between the Direct Line Speech channel and your bot. After enabling the Direct Line Speech channel in the Azure Portal, you will need to update your bot to listen for and accept these WebSocket connections. These instructions explain how to do this.  

## Step 1: Upgrade to the 4.6 SDK 

For Direct Line Speech ensure you are using version 4.6 or above the Bot Builder SDK. 

## Step 2: Update your .NET Core bot code if your bot uses AddBot and UseBotFramework instead of a BotController 

If you have created a bot using v4 of the Bot Builder SDK prior to version 4.3.2, your bot likely does not include a BotController but instead uses the AddBot() and UseBotFramework() methods in the Startup.cs file to expose the POST endpoint where the bot receives messages. To expose the new streaming endpoint, you will need to add a BotController and remove the AddBot() and UseBotFramework() methods. These instructions walk through the changes that need to be made. If you already have these changes, continue to the next step. 

Add a new MVC controller to your bot project by adding a file called BotController.cs. Add the controller code to this file: 

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

In the **Startup.cs** file, locate the Configure method. Remove the `UseBotFramework()` line and make sure you have these lines to `UseWebSockets`: 

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

Also in the Startup.cs file, locate the ConfigureServices method. Remove the `AddBot()` line and make sure you have lines for adding your `IBot` and a `BotFrameworkHttpAdapter`: 

```cs

public void ConfigureServices(IServiceCollection services) 
{ 
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1); 
    services.AddSingleton<ICredentialProvider, ConfigurationCredentialProvider>(); 
    services.AddSingleton<IChannelProvider, ConfigurationChannelProvider>(); 
    
    // Create the Bot Framework Adapter. 
    services.AddSingleton<IBotFrameworkHttpAdapter, BotFrameworkHttpAdapter>(); 

    // Create the bot as a transient. In this case the ASP Controller is expecting an IBot. 
    services.AddTransient<IBot, EchoBot>(); 
} 
```

The remainder of your bot code stays the same! 

## Step3: Ensure WebSockets are enabled 

When you create a new bot from the Azure Portal using one of the templates such as EchoBot, you will get a bot that includes an ASP.NET MVC controller that exposes a GET and POST endpoint and will also use WebSockets. These instructions explain how to add these elements to your bot if you are upgrading or did not make your bot from a template. 

Open **BotController.cs** under the Controllers folder in your solution 

Find the `PostAsync` method in the class and update its decoration from [HttpPost] to [HttpPost, HttpGet]: 

```cs

[HttpPost, HttpGet] 
public async Task PostAsync() 
{ 
    await _adapter.ProcessAsync(Request, Response, _bot); 
} 
```

Save and close BotController.cs 

Open **Startup.cs** in the root of your solution. 

In Startup.cs, navigate to the bottom of the Configure method. Before the call to `app.UseMvc()`, add a call to `app.UseWebSockets()`. This is important as the order of these use calls matters. The end of the method should look something like this: 

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
The remainder of your bot code stays the same! 

 

Step 4: Optionally set the Speak field on Activities to customize what is spoken to the user 

By default, all messages sent through Direct Line Speech to the user will be spoken.  

You can optionally customize how the message is spoken by setting the Speak field of any Activity sent from the bot: 

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

The following snippet shows how to use the previous Speak function: 

```cs

protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken) 
{ 
    await turnContext.SendActivityAsync(Speak($"Echo: {turnContext.Activity.Text}"), cancellationToken); 
} 
``` 

## Additional information 

- For a complete example of creating and using a voice enabled bot, see [Tutorial: Voice-enable your bot using the Speech SDK](https://docs.microsoft.com/azure/cognitive-services/speech-service/tutorial-voice-enable-your-bot-speech-sdk). 

- For more information on working with activities, see [how bots work](https://docs.microsoft.com/azure/bot-service/bot-builder-basics) ans [how to send and receive text messageshttps://docs.microsoft.com/azure/bot-service/bot-builder-howto-send-messages?view=azure-bot-service-4.0](). 

 

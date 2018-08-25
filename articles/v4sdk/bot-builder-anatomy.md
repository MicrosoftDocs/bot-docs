---
title: Anatomy of a bot | Microsoft Docs
description: Breaking down the various parts of a bot and how they work
keywords: 
author: ivorb
ms.author: ivorb
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 06/25/2018
monikerRange: 'azure-bot-service-4.0'
---



# Anatomy of a bot

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

A bot, in the most [basic sense](bot-builder-basics.md), is a conversational app that users communicate with using messages. It follows the basic structure of a web app in it's respective language, and builds on top of that with the Bot Framework SDK.

A bot is composed of several different components, allowing you to [send messages](./bot-builder-howto-send-messages.md) and keep track of [state](./bot-builder-storage-concept.md). You can communicate with your bot through the [emulator](../bot-service-debug-emulator.md), [WebChat](../bot-service-manage-test-webchat.md), or in production through one of the [channels](bot-concepts.md). 

Here, we'll walk through a basic Echo Bot, step by step, and examine each piece that comes together to make it work.

## Files created for Echo Bot

Each programming language creates different files for the Echo bot, which in this example we named **BasicEcho**, and these files fall into three different sections: the system section, the helper items and the core of the bot. The table below lists the main files for both C# and JavaScript.

| C# | JavaScript |
| --- | --- |
| `Properties > launchSettings.json` <br> `wwwroot > default.htm` <br> `appsettings.json` <br> `BasicEcho.bot` <br> `EchoBot.cs` <br> `EchoState.cs` <br> `Program.cs` <br> `readme.md` <br> `Startup.cs` | `.env` <br> `app.js` <br> `package.json` <br> `README.md` <br> `basicEcho.bot` |

Below we will walk you through some key features in these files by section. Some of these files have its own set of includes, which are both bot specific and general programming libraries. These includes provide a set of functions you need, as well as some that you may want in your application. While we will not go over the details of these includes, feel free to use Visual Studio to peek the definitions and see which namespaces they are a part of if you are curious.

## System section

The system section has what you need to make your bot function as a standard application. It includes the configuration files, the adapter, and some JSON files. These items can (and often should) be left alone, except some configuration files that may need your specific information.

# [C#](#tab/cs)

A bot is a type of [ASP.NET Core web](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.1) application framework. If you look at the [ASP.NET fundamentals](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/index?view=aspnetcore-2.1&tabs=aspnetcore2x), you'll see similar code in files such as appsettings.json, Program.cs and Startup.cs discussed below. These files are required for all web apps and are not bot specific. Code in some of these files won't be copied here, but you will see it when you run the bot.

### appsettings.json

This file simply contains the settings information for your bot, mainly the app ID and password, in a basic JSON format.  When testing with the [emulator](../bot-service-debug-emulator.md) these information aren't needed and can be left blank, but they are necessary in production.

### Program.cs

This file is necessary for your ASP.NET web app to work, and specifies the `Startup` class to run.

### Startup.cs

**Startup.cs** has other interesting parts that will be covered later, but here we'll get the system sections of it out of the way.

The constructor for `Startup` specifies the app settings and environment variables, and creates the configuration for your web app.

The `Configure` method finishes the configuration of your app by specifying that the app use the Bot Framework and a few other files. All bots using the Bot Framework will need that configuration call.

```cs
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Builder.TraceExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BasicEcho
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // ...
        // Definition of ConfigureServices covered in the next section
        // ...

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseBotFramework();
        }
    }
}

```

### launchSettings.json and readme.md

**launchSettings.json** simply contains some set up configuration for web apps, and **readme.md** is a simple one line explanation about the echo bot. The contents of these files aren't important for your understanding of this bot. 

# [JavaScript](#tab/js)

The system section mainly contains **package.json**, the **.env** file, **app.js** and **README.md**. Code in some files won't be copied here, but you will see it when you run the bot.

### package.json

**package.json** specifies dependencies and their associated versions for your bot. This is all set up by the template and your system.

### .env file

The **.env** file specifies the configuration information for your bot, such as the port number, app ID, and password among other things. If using certain technologies or using this bot in production, you will need to add your specific keys or URL to this configuration. For this Echo bot, however, you don't need to do anything here right now; the app ID and password may be left undefined at this time. 

To use the **.env** configuration file, the template needs an extra package included.  First, get the `dotenv` package from npm:

`npm install dotenv`

Then add the following line to your bot with the other required libraries:

```javascript
const dotenv = require('dotenv');
```

### app.js

The top of your `app.js` file has the server and adapter that allow your bot to communicate with the user and send responses. The server will listen on the specified port from the **.env** configuration, or fall back to 3978 for connection with your emulator. The adapter will act as the conductor for your bot, directing incoming and outgoing communication, authentication, and so on. 

```javascript
// Create server
let server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
    console.log(`${server.name} listening to ${server.url}`);
});

// Create adapter
const adapter = new BotFrameworkAdapter({ 
    appId: process.env.MICROSOFT_APP_ID, 
    appPassword: process.env.MICROSOFT_APP_PASSWORD 
});
``` 

### README.md

**README.md** provides some helpful information on some aspects of building a bot, such as basic bot structure or what *Dialogs* are, but isn't important for understanding your bot.

---


## Helper items

Like any program, having helper methods and other definitions can simplify our code. There are a few parts of our bot that fall into that category, such as the `.bot` file, that are important for our bot but not necessarily part of the core bot logic.

### .bot file

The `.bot` file contains information, including the endpoint, app ID, and password, that allows [channels](bot-concepts.md) to connect to your bot. This file gets created for you when you start building a bot from a template, but you can create your own through the emulator or other tools.

Your file content should match what you see here unless you name your bot something other than *BasicEcho*. You may need to change and update this information depending on how your bot is used, but for running this Echo Bot no changes are needed right now.

```json
{
  "name": "BasicEcho",
  "secretKey": "",
  "services": [
    {
      "appId": "",
      "id": "http://localhost:3978/api/messages",
      "type": "endpoint",
      "appPassword": "",
      "endpoint": "http://localhost:3978/api/messages",
      "name": "BasicEcho"
    }
  ]
}
```

# [C#](#tab/cs)

### EchoState.cs

**EchoState.cs** contains a simple class that our bot uses to maintain our current state. It contains only an `int` that we use to increment your turn counter. We'll get into state more in the next section, but for now you only need to understand that `EchoState` is our class containing the turn count.

```cs
namespace BasicEcho
{
    /// <summary>
    /// Class for storing conversation state.
    /// </summary>
    public class EchoState
    {
        public int TurnCount { get; set; } = 0;
    }
}
```

### default.htm

**default.htm** is the webpage that is displayed when you run your bot, written in `html`. It displays helpful information for connecting to your bot and how to interact with it, however, the content of the page doesn't impact the behavior of your bot. You will see the code pop up when you run the bot.

# [JavaScript](#tab/js)

### Required libraries

At the very top of your `app.js` file you will find a series of modules or libraries that are being required. These modules will give you access to a set of functions that you may want to include in your application. 

```javascript
const { BotFrameworkAdapter, MemoryStorage, ConversationState } = require('botbuilder');
const restify = require('restify');
```

---

## Core of the bot

Finally, what you're really interested in! The core of the bot defines how the bot interacts with the user, which includes middleware and the bot logic.

# [C#](#tab/cs)

### Middleware

Within **Startup.cs**, there is one method called `ConfigureServices`. This method sets up your credential provider and adds  middleware.

[Middleware](bot-builder-concept-middleware.md) added here does two things. The first being exception handling which allows your bot to fail gracefully. It's defined inline as a lambda expression, simply printing out the exception to the terminal and telling the user something went wrong.

The second middleware maintains your state, using `MemoryStorage` that was defined right before it. This state is defined as `ConversationState`, which just means it's keeping the state of your conversation. It uses `EchoState`, the class we've defined with your turn counter, to store the information you're interested in.

``` cs
// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
    services.AddBot<EchoBot>(options =>
    {
        options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);

        // The CatchExceptionMiddleware provides a top-level exception handler for your bot.
        // Any exceptions thrown by other Middleware, or by your OnTurn method, will be 
        // caught here. To facilitate debugging, the exception is sent out, via Trace, 
        // to the emulator. Trace activities are NOT displayed to users, so in addition
        // an "Ooops" message is sent.
        options.Middleware.Add(new CatchExceptionMiddleware<Exception>(async (context, exception) =>
        {
            await context.TraceActivity("EchoBot Exception", exception);
            await context.SendActivity("Sorry, it looks like something went wrong!");
        }));

        // The Memory Storage used here is for local bot debugging only. When the bot
        // is restarted, anything stored in memory will be gone. 
        IStorage dataStore = new MemoryStorage();

        // The File data store, shown here, is suitable for bots that run on 
        // a single machine and need durable state across application restarts.

        // IStorage dataStore = new FileStorage(System.IO.Path.GetTempPath());

        // For production bots use the Azure Table Store, Azure Blob, or 
        // Azure CosmosDB storage provides, as seen below. To include any of 
        // the Azure based storage providers, add the Microsoft.Bot.Builder.Azure 
        // Nuget package to your solution. That package is found at:
        //      https://www.nuget.org/packages/Microsoft.Bot.Builder.Azure/

        // IStorage dataStore = new Microsoft.Bot.Builder.Azure.AzureTableStorage("AzureTablesConnectionString", "TableName");
        // IStorage dataStore = new Microsoft.Bot.Builder.Azure.AzureBlobStorage("AzureBlobConnectionString", "containerName");

        options.Middleware.Add(new ConversationState<EchoState>(dataStore));
    });
}
```

### Bot logic

The main bot logic is defined within the bot's `OnTurn` handler. `OnTurn` is provided a [context](./bot-builder-concept-activity-processing.md#turn-context) variable as an argument, which provides information about the incoming [activity](bot-builder-concept-activity-processing.md), the conversation, etc.

Incoming activities can be of [various types](../bot-service-activities-entities.md#activity-types), so we first check to see if your bot has received a message. If it is a message, we create a state variable that holds the current information of your bot's conversation. We then increment the `int` that keeps track of your turn count before echoing back to the user the count along with the message they sent.

```cs
using System.Threading.Tasks;
using Microsoft.Bot;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Schema;

namespace BasicEcho
{
    public class EchoBot : IBot
    {
        /// <summary>
        /// Every Conversation turn for our EchoBot will call this method. In here
        /// the bot checks the Activty type to verify it's a message, bumps the 
        /// turn conversation 'Turn' count, and then echoes the users typing
        /// back to them. 
        /// </summary>
        /// <param name="context">Turn scoped context containing all the data needed
        /// for processing this conversation turn. </param>        
        public async Task OnTurn(ITurnContext context)
        {
            // This bot is only handling Messages
            if (context.Activity.Type == ActivityTypes.Message)
            {
                // Get the conversation state from the turn context
                var state = context.GetConversationState<EchoState>();

                // Bump the turn count. 
                state.TurnCount++;

                // Echo back to the user whatever they typed.
                await context.SendActivity($"Turn {state.TurnCount}: You sent '{context.Activity.Text}'");
            }
        }
    }    
}
```

# [JavaScript](#tab/js)

### Middleware

Within **app.js**, we use [middleware](bot-builder-concept-middleware.md) in this bot to maintain your state, using `MemoryStorage` that was defined as one of our required modules at the top from `botbuilder`. This state is defined as `ConversationState`, which just means it's keeping the state of your conversation. `ConversationState` will store the information you're interested in, which in this case is simply a turn counter, in memory.

```javascript
// Add conversation state middleware
const conversationState = new ConversationState(new MemoryStorage());
adapter.use(conversationState);
```

### Bot Logic

The third parameter within *processActivity* is a function handler that will be called to perform the bot’s logic after the received [activity](bot-builder-concept-activity-processing.md) has been pre-processed by the adapter and routed through any middleware. The [context](./bot-builder-concept-activity-processing.md#turn-context) variable, passed as an argument to the function handler, can be used to provide information about the incoming activity, the sender and receiver, the channel, the conversation, etc.

We first check to see if the bot has received a message. If the bot did not receive a message, we will echo back the activity type received. Next, we create a state variable that holds the information of your bot's conversation. We then set your count variable to 0 if undefined (which will occur when you start the bot) or increment it with every new message. Finally, we echo back to the user the count along with the message they sent.

```javascript
// Listen for incoming requests 
server.post('/api/messages', (req, res) => {
    // Route received request to adapter for processing
    adapter.processActivity(req, res, (context) => {
        // This bot is only handling Messages
        if (context.activity.type === 'message') {

            // Get the conversation state
            const state = conversationState.get(context);

            // If state.count is undefined set it to 0, otherwise increment it by 1
            const count = state.count === undefined ? state.count = 0 : ++state.count;

            // Echo back to the user whatever they typed.
            return context.sendActivity(`${count}: You said "${context.activity.text}"`);
        } else {
            // Echo back the type of activity the bot detected if not of type message
            return context.sendActivity(`[${context.activity.type} event detected]`);
        }
    });
});
```

---

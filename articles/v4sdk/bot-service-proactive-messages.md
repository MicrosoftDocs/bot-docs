When you think about the exchange of messages between your bot and the user, you're probably thinking about the scenario where the user sends a message to your bot and your bot then replies to the user with a message of its own. We call this _reactive messaging_ and it's by far the most common flow that you should optimize your bot's code for.

It is possible, however, for your bot to initiate a conversation with the user by sending them a message first. We call this _proactive messaging_ and while the code you'll write to send a proactive message is very similar to what you'd write in the reactive case, there are a few differences that are worth exploring.

The first thing to note is that before you can send a proactive message to a user, the user will have to send at least one reactive style message to your bot. There are two reasons for this.

1. You need to get the user's `ConversationReference` and save it somewhere for future use. You can think of the conversation reference as the user's address, as it contains information about the channel they came in on, their user ID, the conversation ID, and even the server that should receive any future messages. This object is simple JSON and should be saved whole without tampering.

1. Most channels by policy won't let a bot initiate conversations with users they've never spoken to before. Depending on the channel the user might need to explicitly add the bot to a conversation or at a minimum send an initial message to the bot.

> ![NOTE]
> This bot currently runs properly only when deployed to Azure. However, you can test the bot without publishing it.

## Set up your development environment

The features used in this article aren't available in the Bot Builder SDK v4.0.0-alpha2018031301 NuGet packages.

1. Clone the repo and build the packages locally (see [Building the SDK](Building-the-SDK)).

1. Create an empty ASP.NET Core Web Application project.

1. Add references to the following packages:
    ```text
    Microsoft.Bot.Builder
    Microsoft.Bot.Builder.Core
    Microsoft.Bot.Builder.Core.Extensions
    Microsoft.Bot.Builder.Integration.AspNet.Core
    Microsoft.Bot.Connector
    Microsoft.Bot.Schema
    ```

1. Add a `default.html` file under the `wwwroot` folder.

1. In the `Program.cs` file, change the namespace to `Microsoft.Bot.Samples`.

## Define your bot

The following bot definition uses the conversation reference to send a proactive message to the user.
- When the user enters `subscribe`, the bot immediately confirms the request, and then sends a proactive message after a 2 second delay.
- When the users types anything else, the bot prompts the user to type `subscribe`.

Create a `ProactiveBot.cs` class file and update the file to this:

```csharp
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Adapters;
using Microsoft.Bot.Schema;
using System.Threading.Tasks;

namespace Microsoft.Bot.Samples
{
    public class ProactiveBot : IBot
    {
        public async Task OnReceiveActivity(ITurnContext context)
        {
            if (context.Request.Type is ActivityTypes.Message)
            {
                var messageText = context.Request?.AsMessageActivity()?.Text;
                if (messageText?.Trim().ToLowerInvariant() is "subscrie")
                {
                    // Confirm the request.
                    await context.SendActivity("Thank You! We will message you shortly.");

                    // Send the proactive message.
                    var reference = TurnContext.GetConversationReference(context.Request);
                    SubscribeUser((BotFrameworkAdapter)context.Adapter,
                        ((BotFrameworkTurnContext)context).BotAppId, reference);
                }
                else
                {
                    // Prompt the user to type "subscribe".
                    await context.SendActivity("Type `subscribe` to receive a proactive message.");
                }
            }
        }

        private async void SubscribeUser(BotFrameworkAdapter adapter, string appId, ConversationReference reference)
        {
            await adapter.ContinueConversation(appId, reference, async context =>
            {
                await Task.Delay(2000);
                await context.SendActivity("You've been notified!");
            });
        }
    }
}
```

The bot does not store the conversation reference for later, but you can save this data in your actual code. The bot calls the `SubscribeUser` method and notifies the user that they're now subscribed, and you're then free to do pretty much anything you would normally do in your bot's `OnReceiveActivity` handler.

The `SubscribeUser` method passes the conversation reference, along with the bot's application ID, to the adapter's `ContinueConversation` method. The adapter creates a `context` object and passes it to your callback method, which uses the context to send the message.

> ![NOTE]
> Within the callback, no activity was received so `context.Request` is null.

## Update your Startup.cs file

We still need to add the bot and use the Bot Framework in the `Startus.cs` file. Replace its contents:

```csharp
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Builder.BotFramework;
using Microsoft.Bot.Builder.Core.Extensions;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Microsoft.Bot.Samples
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_ => Configuration);
            services.AddBot<ProactiveBot>(options =>
            {
                options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);
                options.EnableProactiveMessages = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseBotFramework();
        }
    }
}
```

## Test your bot

To test your bot, deploy it to Azure as a registration only bot, and test it in Web Chat.

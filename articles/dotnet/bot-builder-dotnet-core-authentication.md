---
title: Authenticating activities using .NET Core | Microsoft Docs
description: Learn how to authenticate bots activities using .NET Core.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 12/13/17
---

# Authenticating activities using .NET Core

If you choose to develop your bot using [.NET Core](/dotnet/core/index), you can use the [Bot Framework Connector](bot-builder-dotnet-connector.md) to send and receive [activity](/dotnet/api/microsoft.bot.connector.activity) messages from your bot. To use the Connector service, you need to set up the proper authentication model for the framework version you are targeting.

The Bot Framework Connector.AspNetCore supports the following versions of ASP.NET:
* For .NET Core v1.1/AspNetCore1.x use **Microsoft.Bot.Connector.AspNetCore 1.x**
* For .NET Core v2.0/AspNetCore2.x use **Microsoft.Bot.Connector.AspNetCore 2.x**

This article will show you how to set up authentication model for the specific framework that your bot targets.

## Prerequisites

* [Visual Studio 2017](https://www.visualstudio.com/downloads/)
* [.NET Core](https://www.microsoft.com/net/download/windows). Install .NET Core version you target (e.g.: .NET Core v1.1 or .NET Core v 2.0).
* [Register a bot](~/bot-service-quickstart-registration.md). Register your bot to obtain an AppID and Password that is needed for the authentication process.

## Create a .NET Core project

To create your .NET Core project, do the following:

1. Open Visual Studio 2017 and click **File > New > Project...**.
2. Expand the **Visual C#** node and click **.NET Core**.
3. Choose **ASP.NET Core Web Application** project type and fill in the project information (e.g.: Name, Location, and Solution name fields).
4. Click **OK**.
5. Ensure that the project is targeting *.NET Core* and the *ASP.NET Core* version you want. For example, the screenshot below shows the project is targeting **.NET Core** and **ASP.NET Core 2.0**:

![Create an ASP.NET Core v2.0 project](~/media/dotnet-core-authentication/create-asp-net-core-2x-project.png)
 
  > [!NOTE]
  > If you are targeting **ASP.NET Core 2.0**, make sure the installed version on our machine is at least 2.x and above.

6. Choose **Web API** project type.
7. Click **OK** to create the project.

## Download the NuGet package

To use the **Bot Framework Connector**, install the NuGet package appropriate for your .NET Core version. To install the NuGet package, do the following:

1. From **Solution Explorer**, right-click the project name and **Manage NuGet Packages...**
2. Click **Browse** and search for **Connector.ASPNetCore**. 
3. Choose the version you want to target. For example, the screenshot below shows the version **2.0.0.3** selected. To install version 1.1.3.2, click the **Version** drop-down and choose version **1.1.3.2** instead.
![NuGet Package for ASP Net Core version 2.0.0.3](~/media/dotnet-core-authentication/nuget-package-net-core-version.png)
4. Click **Install**.

## Update the appsettings.json

The Bot Framework Connector requires your AppID and Password to authenticate the bot. You can set these values in the **appsettings.json** of your web app project.

> [!NOTE]
> To find your bot's **AppID** and **AppPassword**, see [MicrosoftAppID and MicrosoftAppPassword](~/bot-service-manage-overview.md#microsoftappid-and-microsoftapppassword).

### appsettings.json for .NET Core v1.1:

```json
{
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "MicrosoftAppId": "<MicrosoftAppId>",
  "MicrosoftAppPassword": "<MicrosoftAppPassword>"
}
```

### appsettings.json for .NET Core v2.0:

```json
{
  "Logging": {
    "IncludeScopes": false,
    "Debug": {
      "LogLevel": {
        "Default": "Warning"
      }
    },
    "Console": {
      "LogLevel": {
        "Default": "Warning"
      }
    }
  },
  "MicrosoftAppId": "<MicrosoftAppId>",
  "MicrosoftAppPassword": "<MicrosoftAppPassword>"
}
```

## Update the startup.cs class

Update the **startup.cs** class. Depending on your project version, update the appropriate code fragment to allow authentication to work.

### startup.cs for .NET Core v1.1

```cs
public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_ => Configuration);

            services.AddSingleton(typeof(ICredentialProvider), new StaticCredentialProvider(
                Configuration.GetSection(MicrosoftAppCredentials.MicrosoftAppIdKey)?.Value,
                Configuration.GetSection(MicrosoftAppCredentials.MicrosoftAppPasswordKey)?.Value));

            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(TrustServiceUrlAttribute));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseStaticFiles();

            ICredentialProvider credentialProvider = serviceProvider.GetService<ICredentialProvider>();

            app.UseBotAuthentication(credentialProvider);

            app.UseMvc();
        }
    }
```

### startup.cs for .NET Core v2.0:

```cs
public class Startup
    {
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
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_ => Configuration);

            var credentialProvider = new StaticCredentialProvider(Configuration.GetSection(MicrosoftAppCredentials.MicrosoftAppIdKey)?.Value,
                Configuration.GetSection(MicrosoftAppCredentials.MicrosoftAppPasswordKey)?.Value);

            services.AddAuthentication(
                    options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                .AddBotAuthentication(credentialProvider);

            services.AddSingleton(typeof(ICredentialProvider), credentialProvider);

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(TrustServiceUrlAttribute));
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
```

## Create the MessagesController.cs class

From **Solution Explorer** add a new empty class called **MessagesController.cs**. In the **MessagesController.cs** class, update the **Post** method with the code below. This will allow your bot to send and receive messages for the user.

```cs
private IConfiguration configuration;

public MessagesController(IConfiguration configuration)
{
    this.configuration = configuration;
}


[Authorize(Roles = "Bot")]
[HttpPost]
public async Task<OkResult> Post([FromBody] Activity activity)
{
    if (activity.Type == ActivityTypes.Message)
    {
        //MicrosoftAppCredentials.TrustServiceUrl(activity.ServiceUrl);
        var appCredentials = new MicrosoftAppCredentials(configuration);
        var connector = new ConnectorClient(new Uri(activity.ServiceUrl), appCredentials);

        // return our reply to the user
        var reply = activity.CreateReply("HelloWorld");
        await connector.Conversations.ReplyToActivityAsync(reply);
    }
    else
    {
        //HandleSystemMessage(activity);
    }
    return Ok();
}
```

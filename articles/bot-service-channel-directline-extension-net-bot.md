---
title: Configure .NET bots for the Direct Line App Service extension in the Bot Framework SDK
description: Configure .NET bots to work with named pipes. Enable the Direct Line App Service extension and configure bots to use the extension.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: Daniel.Evans
ms.service: bot-service
ms.topic: how-to
ms.date: 10/20/2022
ms-custom: abs-meta-21q1
ms.custom:
  - evergreen
---

# Configure .NET bot for extension

**Commencing September 1, 2023, it is strongly advised to employ the [Azure Service Tag](/azure/virtual-network/service-tags-overview#available-service-tags) method for network isolation. The utilization of DL-ASE should be limited to highly specific scenarios. Prior to implementing this solution in a production environment, we kindly recommend consulting your support team for guidance.**

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to update a .NET bot to work with named pipes and how to enable the Direct Line App Service extension in the Azure App Service resource in which you deployed your bot.

## Prerequisites

- An Azure account. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A .NET bot deployed in Azure.
- Bot Framework SDK for .NET, 4.14.1 or later.

## Enable Direct Line App Service extension

This section describes how to enable the Direct Line App Service extension using the App Service extension key from your bot's Direct Line channel configuration.

### Update bot code

> [!NOTE]
> The **Microsoft.Bot.Builder.StreamingExtensions** NuGet preview packages have been deprecated. Starting with v4.8, the SDK contains a `Microsoft.Bot.Builder.Streaming` namespace. If a bot previously made use of the preview packages, they must be removed before following the steps below.

1. In Visual Studio, open your bot project.
1. Allow your app to use named pipes:
    1. Open the **Startup.cs** file.
    1. Add a reference to the **Microsoft.Bot.Builder.Integration.AspNet.Core** NuGet package.

        ```csharp
        using Microsoft.Bot.Builder.Integration.AspNet.Core;
        ```

    1. In the `Configure` method, add a call to the `UseNamedPipes` method.

        ```csharp
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseWebSockets()
                // Allow the bot to use named pipes.
                .UseNamedPipes(System.Environment.GetEnvironmentVariable("APPSETTING_WEBSITE_SITE_NAME") + ".directline")
                .UseRouting()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            
            // app.UseHttpsRedirection();
        }
        ```

    1. Save the **Startup.cs** file.
1. Deploy your updated bot to Azure.

### Enable bot Direct Line App Service extension

[!INCLUDE [enable Bot Direct Line App Service extensions](includes/directline-enable-dl-asp.md)]

## Confirm the Direct Line extension and the bot are configured

[!INCLUDE [Confirm the extension and the bot are configured](includes/directline-confirm-extension-bot-config.md)]

## Troubleshooting

[!INCLUDE [Troubleshoot Direct Line extension](includes/directline-troubleshoot.md)]

- Enable the bot to use the out of process hosting model; otherwise, you'll receive an *HTTP Error 500.34 - ANCM Mixed Hosting* error (where *ANCM* stands for *ASP.NET Core Module*). This error occurs because the bot template is using the `InProcess` hosting model by default. To configure out of process hosting, see [Out-of-process hosting model](/aspnet/core/host-and-deploy/aspnet-core-module?view=aspnetcore-3.1&preserve-view=true#out-of-process-hosting-model).
For more information, see [Attributes of the aspNetCore element](/aspnet/core/host-and-deploy/aspnet-core-module?view=aspnetcore-3.1&preserve-view=true#attributes-of-the-aspnetcore-element) and [Configuration with web.config](/aspnet/core/host-and-deploy/aspnet-core-module?view=aspnetcore-3.1&preserve-view=true#configuration-with-webconfig).

- If you attempt to use OAuth with the Direct Line App Service extension and encounter the error "Unable to get the bot AppId from the audience claim", set `ClaimsIdentity` to `AudienceClaim` on the `BotFrameworkHttpAdapter`. To do so, you can subclass the adapter. For example:

    ```csharp
    public class AdapterWithStaticClaimsIdentity : BotFrameworkHttpAdapter
    {
        public AdapterWithStaticClaimsIdentity(IConfiguration configuration, ILogger<BotFrameworkHttpAdapter> logger, ConversationState conversationState = null)
            : base(configuration, logger)
        {
            // Manually create the ClaimsIdentity and create a Claim with a valid AudienceClaim and the AppID for a bot using the Direct Line App Service extension.
            var appId = configuration.GetSection(MicrosoftAppCredentials.MicrosoftAppIdKey)?.Value;
            ClaimsIdentity = new ClaimsIdentity(new List<Claim>{
                new Claim(AuthenticationConstants.AudienceClaim, appId)
            });
        }
    }
    ```

## Next steps

> [!div class="nextstepaction"]
> [Use Web Chat with the Direct Line App Service extension](./bot-service-channel-directline-extension-webchat-client.md)

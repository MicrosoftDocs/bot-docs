---
title: Configure .NET bots for the Direct Line App Service extension in the Bot Framework SDK
description: Configure .NET bots to work with named pipes. Enable the Direct Line App Service extension and configure bots to use the extension.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: Daniel.Evans
ms.service: bot-service
ms.topic: how-to
ms.date: 11/30/2021
ms-custom: abs-meta-21q1
---

# Configure .NET bot for extension

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to update a .NET bot to work with named pipes and how to enable the Direct Line App Service extension in the Azure App Service resource where the bot is hosted.

## Prerequisites

- A .NET bot deployed in Azure
- Bot Framework SDK for .NET, 4.14.1 or later

## Enable Direct Line App Service extension

This section describes how to enable the Direct Line App Service extension using the App Service extension key from your bot's Direct Line channel configuration.

### Update bot code

> [!NOTE]
> The **Microsoft.Bot.Builder.StreamingExtensions** NuGet preview packages have been deprecated. Starting with v4.8, the SDK contains a `Microsoft.Bot.Builder.Streaming` namespace. If a bot previously made use of the preview packages they must be removed before following the steps below.

1. In Visual Studio, open your bot project.
1. Allow your app to use named pipes:
    - Open the **Startup.cs** file.
    - In the `Configure` method, add a call to the `UseNamedPipes` method.

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
            .UseNamedPipes(System.Environment.GetEnvironmentVariable("APPSETTING_WEBSITE_SITE_NAME") + ".directline");
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

1. In the Azure portal, go to your **Azure Bot** resource.
    1. From the left panel menu under **Bot management** select **Channels** to configure the **Azure Bot Service** channels your bot accepts messages from.
    1. If it is not already enabled, select the **Direct Line** channel and follow instructions to enable the channel.
    1. In the **Connect to channels** table select the **Edit** link on the **Direct Line** row.
    1. Scroll down to the **App Service extension Keys** section.
    1. Select the **Show** link to reveal one of the keys. Copy this value for use later.
1. Go to the home page, select **App Services** at the top of the page. Alternatively, display the portal menu and then select the **App Services** menu item, in the left panel. Azure displays the **App Services** page.
1. In the search box enter your **Azure Bot** resource name. Your resource will be listed.

    Notice that if you hover over the icon or the menu item, you get the list of the last resources you viewed. Chances are your **Azure Bot** resource will be listed.

1. Select your resource link.
    1. In the **Settings** section, select the **Configuration** menu item.
    1. In the right panel, add the following settings:

        |Name|Value|
        |---|---|
        |DirectLineExtensionKey|The value of the App Service extension key you copied earlier.|
        |DIRECTLINE_EXTENSION_VERSION|latest|

    1. If your bot is hosted in a sovereign or otherwise restricted Azure cloud, where you don't access Azure via the [public portal](https://portal.azure.com), you will also need to add the following setting:

        |Name|Value|
        |---|---|
        |DirectLineExtensionABSEndpoint|The endpoint specific to the Azure cloud your bot is hosted in. For the USGov cloud for example, the endpoint is `https://directline.botframework.azure.us/v3/extension`.|

    1. Still within the **Configuration** section, select the **General** settings section and turn on **Web sockets**.
    1. Select **Save** to save the settings. This restarts the Azure App Service.

## Confirm the extension and the bot are configured

In your browser, navigate to `https://<your_app_service>.azurewebsites.net/.bot`.
If everything is correct, the page will return this JSON content: `{"v":"123","k":true,"ib":true,"ob":true,"initialized":true}`. This is the information you obtain when *everything works correctly*, where

- **v** displays the build version of the Direct Line App Service extension.
- **k** determines whether the extension can read an extension key from its configuration.
- **initialized** determines whether the extension can use the extension key to download the bot metadata from Azure Bot Service.
- **ib** determines whether the extension can establish an inbound connection with the bot.
- **ob** determines whether the extension can establish an outbound connection with the bot.

## Troubleshooting

- If the **ib** and **ob** values displayed by the **.bot endpoint** are false this means the bot and the Direct Line App Service extension are unable to connect to each other.
    1. Double check the code for using named pipes has been added to the bot.
    1. Confirm the bot is able to start up and run at all. Useful tools are **Test in WebChat**, connecting an additional channel, remote debugging, or logging.
    1. Restart the entire **Azure App Service** the bot is hosted within, to ensure a clean start up of all processes.

- If the **initialized** value of the **.bot endpoint** is false it means the Direct Line App Service extension is unable to validate the App Service extension key added to the bot's **Application Settings** above.
    1. Confirm the value was correctly entered.
    1. Switch to the alternate extension key shown on your bot's **Configure Direct Line** page.

- Enable the bot to use the out of process hosting model; otherwise you will receive an *HTTP Error 500.34 - ANCM Mixed Hosting*. Where *ANCM* stands for *ASP.NET Core Module*. The error is caused because the bot template is using the `InProcess` hosting model by default. To configure out of process hosting, see [Out-of-process hosting model](/aspnet/core/host-and-deploy/aspnet-core-module?view=aspnetcore-3.1&preserve-view=true#out-of-process-hosting-model).
See also [Attributes of the aspNetCore element](/aspnet/core/host-and-deploy/aspnet-core-module?view=aspnetcore-3.1&preserve-view=true#attributes-of-the-aspnetcore-element) and [Configuration with web.config](/aspnet/core/host-and-deploy/aspnet-core-module?view=aspnetcore-3.1&preserve-view=true#configuration-with-webconfig).

- If you attempt to use OAuth with the Direct Line App Service extension and encounter the error "Unable to get the bot AppId from the audience claim." A `ClaimsIdentity` with the `AudienceClaim` assigned needs to be set on the `BotFrameworkHttpAdapter`. In order to accomplish this a developer may subclass the adapter. For example:

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

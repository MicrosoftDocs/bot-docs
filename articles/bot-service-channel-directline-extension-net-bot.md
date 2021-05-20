---
title: .NET bot with Direct Line App Service extension
titleSuffix: Bot Service
description: Learn how to configure .NET bots to work with named pipes. See how to enable the Direct Line App Service extension and how to configure bots to use the extension.
services: bot-service
manager: kamrani
ms.service: bot-service
ms.topic: conceptual
ms.author: kamrani
ms.date: 05/06/2021
---

# Configure .NET bot for extension

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to update a bot to work with named pipes, and how to enable the Direct Line App Service extension in the Azure App Service resource where the bot is hosted.

## Prerequisites

To perform the steps described next, you need to have a bot deployed in Azure.

## Enable Direct Line App Service extension

This section describes how to enable the Direct Line App Service extension using the App Service extension key from your bot's Direct Line channel configuration.

### Update bot code

> [!NOTE]
> The **Microsoft.Bot.Builder.StreamingExtensions** NuGet preview packages have been deprecated. Starting with v4.8, the SDK contains a `Microsoft.Bot.Builder.Streaming` namespace. If a bot previously made use of the preview packages they must be removed before following the steps below.

1. In Visual Studio, open your bot project.
1. Make sure the project uses version 4.8 or later of the Bot Framework SDK.
1. Allow your app to use named pipes:
    - Open the **Startup.cs** file.
    - In the `Configure` method, add a call to the `UseNamedPipes` method.

    ```csharp
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseHsts();
        }

        app.UseDefaultFiles();
        app.UseStaticFiles();

        // Allow the bot to use named pipes.
        app.UseNamedPipes(System.Environment.GetEnvironmentVariable("APPSETTING_WEBSITE_SITE_NAME") + ".directline");

        app.UseMvc();
    }
    ```

1. Save the **Startup.cs** file.

1. Publish the bot to your Azure web app bot resource to deploy the updated code.

### Enable bot Direct Line App Service extension

1. In the Azure portal, locate your **Web App Bot** resource.
1. From the left panel menu under **Bot management** click on **Channels** to configure the **Azure Bot Service** channels your bot accepts messages from.
1. If it is not already enabled, click on the **Direct Line** channel and follow instructions to enable the channel.
1. In the **Connect to channels** table click on the **Edit** link on the **Direct Line** row.
1. Scroll down to the **App Service extension Keys** section.
1. Click on the **Show** link to reveal one of the keys. Copy this value for use later.

    ![App Service extension keys](./media/channels/direct-line-extension-extension-keys.png)

1. Navigate to the home page, click the **App Services** icon at the top of the page. You can also display the portal menu, and then click the **App Services** menu item, in the left panel. The **App Services** page is displayed.
1. In the search box enter your **Web App Bot** resource name. Your resource will be listed.
Notice that if you hover over the icon or the menu item, you get the list of the last resources you viewed. Chances are your **Web App Bot** resource will be listed.
1. Click your resource link.
1. In the **Settings** section, click the **Configuration** menu item.
1. In the right panel, add the following new settings:

    |Name|Value|
    |---|---|
    |DirectLineExtensionKey|The value of the App Service extension key you copied earlier.|
    |DIRECTLINE_EXTENSION_VERSION|latest|

1. If your bot is hosted in a sovereign or otherwise restricted Azure cloud, where you don't access Azure via the [public portal](https://portal.azure.com), you will also need to add the following new setting:

    |Name|Value|
    |---|---|
    |DirectLineExtensionABSEndpoint|The endpoint specific to the Azure cloud your bot is hosted in. For the USGov cloud for example, the endpoint is `https://directline.botframework.azure.us/v3/extension`.|

1. Still within the **Configuration** section, click on the **General** settings section and turn on **Web sockets**.
1. Click on **Save** to save the settings. This restarts the Azure App Service.

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

- If you receive an "HTTP Error 500.34 - ANCM Mixed Hosting", your bot is attempting to use the `InProcess` Hosting Model. This is remedied by explicitly setting the bot to run `OutOfProcess` instead. See [Out-of-process hosting model](/aspnet/core/host-and-deploy/aspnet-core-module?view=aspnetcore-3.1&preserve-view=true#out-of-process-hosting-model) in the AZP.NET Core documentation for more information.

- If the **initialized** value of the **.bot endpoint** is false it means the Direct Line App Service extension is unable to validate the App Service extension key added to the bot's **Application Settings** above.
    1. Confirm the value was correctly entered.
    1. Switch to the alternate extension key shown on your bot's **Configure Direct Line** page.

- If you attempt to use OAuth with the Direct Line App Service extension and encounter the error "Unable to get the bot AppId from the audience claim." A `ClaimsIdentity` with the `AudienceClaim` assigned needs to be set on the `BotFrameworkHttpAdapter`. In order to accomplish this a developer may subclass the adapter similar to the example below:

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

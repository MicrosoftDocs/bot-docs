---
title: .NET bot with direct line app service extension
titleSuffix: Bot Service
description: Enable .NET bot to work with direct line app service extension
services: bot-service
manager: kamrani
ms.service: bot-service
ms.topic: conceptual
ms.author: kamrani
ms.date: 01/16/2020
---

# Configure .NET bot for extension

[!INCLUDE[applies-to-v4](includes/applies-to.md)]

This article describes how to update a bot to work with **named pipes**, and how to enable the direct line app service extension in the **Azure App Service** resource where the bot is hosted. Please, also read this companion article [Create .NET Client to Connect to Direct Line App Service Extension](bot-service-channel-directline-extension-net-client.md).


## Prerequisites

To perform the steps described next, you need to have the **Bot Channel Registration** resource and the related **Bot App Service** (your bot) in Azure.

## Enable Direct Line app service extension

This section describes how to enable the Direct Line app service extension using the app service extension key from your bot’s Direct Line channel configuration.

### Update bot code

> [!NOTE]
> `Microsoft.Bot.Builder.StreamingExtensions` preview packages have been deprecated. The SDK v4.8 now contains the [streaming code](https://github.com/microsoft/botbuilder-dotnet/tree/master/libraries/Microsoft.Bot.Builder/Streaming). If a bot previously made use of the preview packages they must be removed before following the steps below.

1. In Visual Studio, open your bot project.
1. Make sure the project uses version 4.8 or higher of the Bot Builder SDK. Also ensure that the package [Microsoft.Bot.Connector.Directline](https://www.nuget.org/packages/Microsoft.Bot.Connector.DirectLine/3.0.3-Preview1) version v3.0.3-Preview1 or later is installed.
1. Allow your app to use the **Bot Framework NamedPipe**:
    - Open the `Startup.cs` file.
    - In the ``Configure`` method, add code to ``UseNamedPipes``

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

1. Save the `Startup.cs` file.

1. Open the `appsettings.json` file and enter the following values:
    1. `"MicrosoftAppId": "<secret Id>"`
    2. `"MicrosoftAppPassword": "<secret password>"`

    The values are the **appid** and the **appSecret** associated with the bot channels registration.

1. **Publish** the bot to your Azure App Service.

### Enable bot Direct Line app service extension

1. In your browser, navigate to the [Azure portal](https://portal.azure.com/)
1. In the Azure portal, locate your **Azure Bot Service** resource
1. Click on **Channels** to configure the bot’s channels
1. If it is not already enabled, click on the **Direct Line** channel to enable it.
1. If it is already enabled, in the Connect to channels table click on the **Edit** link on the Direct Line row.
1. Scroll down to the App Service Extension Keys section.
1. Click on the **Show** link to reveal one of the keys, then copy and save its value. You will use this value in the steps below.

    ![App service extension keys](./media/channels/direct-line-extension-extension-keys.png)

1. In the Azure portal, locate the **bot app service** resource page.
1. In the left panel, in the *Application settings* section, click the **Configuration** item.
1. In the right panel, add the following new settings:

    |Name|Value|
    |---|---|
    |DirectLineExtensionKey|<App_Service_Extension_Key>|
    |DIRECTLINE_EXTENSION_VERSION|latest|

    Where the *App_Service_Extension_Key* is the value you saved earlier.

1. Within the *Configuration* section, click on the **General** settings section and turn on **Web sockets**
1. Click on **Save** to save the settings. This restarts the Azure App Service.

## Confirm Direct Line app extension and the bot are configured

In your browser, navigate to https://<your_app_service>.azurewebsites.net/.bot.
If everything is correct, the page will return this JSON content: `{"v":"123","k":true,"ib":true,"ob":true,"initialized":true}`. This is the information you obtain when **everything works correctly**, where

- **v** displays the build version of the Direct Line App Service Extension (ASE).
- **k** determines whether Direct Line ASE can read an App Service Extension Key from its configuration.
- **initialized** determines whether Direct Line ASE can use the App Service Extension Key to download the bot metadata from Azure Bot Service
- **ib** determines whether Direct Line ASE can establish an inbound connection with the bot.
- **ob** determines whether Direct Line ASE can establish an outbound connection with the bot.

## Next steps

> [!div class="nextstepaction"]
> [Create .NET Client](./bot-service-channel-directline-extension-net-client.md)

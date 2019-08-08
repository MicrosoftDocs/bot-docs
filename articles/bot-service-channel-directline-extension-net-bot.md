---
title: .NET bot with direct line app service extension
titleSuffix: Bot Service
description: Enable .NET bot to work with direct line app service extension
services: bot-service
manager: kamrani
ms.service: bot-service
ms.topic: conceptual
ms.author: kamrani 
ms.date: 07/25/2019
---

# Configure .NET bot for extension

[!INCLUDE[applies-to-v4](includes/applies-to.md)]

This article describes how to update a bot to work with **named pipes**, and how to enable the direct line app service extension in the **Azure App Service** resource where the bot is hosted.  

## Prerequisites

In order to perform the steps described next, you must have **Azure App Service** resource and related **App Service** in Azure.

## Enable Direct Line App Service Extension

This section describes how to enable the direct line app service extension using keys from your bot’s channel configuration and the **Azure App Service** resource where your bot is hosted.

## Update .NET Bot to use Direct Line App Service Extension

1. In Visual Studio, open your bot project.
1. Add the **Streaming Extension NuGet** package to your project:
    1. In your project, right click on **Dependencies** and select **Manage NuGet Packages**.
    1. Under the *Browse* tab, click **Include prerelease** to show the preview packages.
    1. Select the package **Microsoft.Bot.Builder.StreamingExtensions**.
    1. Click the **Install** button to install the package; read and agree to the license agreement.
1. Allow your app to use the **Bot Framework NamedPipe**:
    - Open the `Startup.cs` file.
    - In the ``Configure`` method, add code to ``UseBotFrameworkNamedPipe``

    ```csharp

    using Microsoft.Bot.Builder.StreamingExtensions;

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

        // Allow bot to use named pipes.
        app.UseBotFrameworkNamedPipe();

        app.UseMvc();
    }
    ```

1. Save the `Startup.cs` file.
1. Open the `appsettings.json` file and enter the following values:
    1. `"MicrosoftAppId": "<secret Id>"`
    1. `"MicrosoftAppPassword": "<secret password>"`

    The values are the **appid** and the **appSecret** associated with the service registration group.

1. **Publish** the bot to your Azure App Service.
1. In your browser, navigate to https://<your_app_service>.azurewebsites.net/.bot. 
If everything is correct, the page will return this JSON content: `{"k":true,"ib":true,"ob":true,"initialized":true}`.

### Gather your Direct Line Extension keys

1. In your browser, navigate to the [Azure portal](https://portal.azure.com/)
1. In the Azure portal, locate your **Azure Bot Service** resource
1. Click on **Channels** to configure the bot’s channels
1. If it is not already enabled, click on the **Direct Line** channel to enable it. 
1. If it is already enabled, in the Connect to channels table click on the **Edit** link on the Direct Line row.
1. Scroll down to the App Service Extension Keys section. 
1. Click on the **Show link** to reveal one of the keys, then copy its value.

![App service extension keys](./media/channels/direct-line-extension-extension-keys.png)

### Enable the Direct Line App Service Extension

1. In your browser, navigate to the [Azure portal](https://portal.azure.com/)
1. In the Azure portal, locate the **Azure App Service** resource page for the Web App where your bot is or will be hosted
1. Click on **Configuration**. Under the *Application settings* section, add the following new settings:

    |Name|Value|
    |---|---|
    |DirectLineExtensionKey|<App_Service_Extension_Key_From_Section_1>|
    |DIRECTLINE_EXTENSION_VERSION|latest|

1. Within the *Configuration* section, click on the **General** settings section and turn on **Web sockets**
1. Click on **Save** to save the settings. This restarts the Azure App Service.

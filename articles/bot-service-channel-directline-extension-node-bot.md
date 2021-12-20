---
title: Configure Node.js bots for Direct Line App Service extension in the Bot Framework SDK
description: Configure Node.js bots to work with named pipes. Enable the Direct Line App Service extension and configure bots to use the extension.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: Daniel.Evans
ms.service: bot-service
ms.topic: how-to
ms.date: 11/30/2021
---

# Configure Node.js bot for extension

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to update a Node.js bot to work with named pipes and how to enable the Direct Line App Service extension in the Azure App Service resource where the bot is hosted.

## Prerequisites

- A Node.js bot deployed in Azure
- Bot Framework SDK for Node.js, 4.7 or later

## Enable Direct Line App Service extension

This section describes how to enable the Direct Line App Service extension using the App Service extension key from your bot's Direct Line channel configuration.

## Update Node.js Bot to use Direct Line App Service extension

1. Allow your app to use the **Direct Line App Service Extension Named Pipe**:

    Update the bot's index.js (below the assignment of the adapter and bot) to include the following code that pulls the App Service name from the environment and instructs the adapter to connect to the appropriate named pipe:

    ```Node.js
    
    adapter.useNamedPipe(async (context) => {
        await myBot.run(context);
        },
        process.env.APPSETTING_WEBSITE_SITE_NAME + '.directline'
    );
    ```

1. Save the `index.js` file.
1. Update the `Web.Config` file to add the `AspNetCore` handler and rule needed by Direct Line App Service extension to service requests:

    Locate the `Web.Config` file in the `wwwroot` directory of your bot and modify the contents to include the following entries to the `Handlers` and `Rules` sections:

    ```XML
    <handlers>      
          <add name="aspNetCore" path="*/.bot/*" verb="*" modules="AspNetCoreModule" resourceType="Unspecified" />
    </handlers>
    
    <rewrite>
      <rules>
        <!-- Do not interfere with Direct Line App Service extension requests. (This rule should be as high in the rules section as possible to avoid conflicts.) -->
        <rule name ="DLASE" stopProcessing="true">
          <conditions>
            <add input="{REQUEST_URI}" pattern="^/.bot"/>
          </conditions>
        </rule>
      </rules>
    </rewrite>
    ```

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

## Confirm Direct Line App Service extension and the bot are configured

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

## Next steps

> [!div class="nextstepaction"]
> [Use Web Chat with the Direct Line App Service extension](./bot-service-channel-directline-extension-webchat-client.md)
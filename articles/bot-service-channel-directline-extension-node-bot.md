---
title: Node.js bot with direct line app service extension
titleSuffix: Bot Service
description: Enable Node.js bot to work with direct line app service extension
services: bot-service
manager: kamrani
ms.service: bot-service
ms.topic: conceptual
ms.author: dev 
ms.date: 01/15/2020
---

# Configure Node.js bot for extension

[!INCLUDE[applies-to-v4](includes/applies-to.md)]

This article describes how to update a bot to work with **named pipes**, and how to enable the Direct Line app service extension in the **Azure App Service** resource where the bot is hosted.

## Prerequisites

To perform the steps described next, you need to have a **Web App Bot**(your bot) created in Azure.

## Enable Direct Line app service extension

This section describes how to enable the Direct Line app service extension using the app service extension key from your bot’s Direct Line channel configuration.

## Update Node.js Bot to use Direct Line App Service Extension

1. BotBuilder v4.7.0 or higher is required to use a Node.js bot with Direct Line App Service Extension.
1. Allow your app to use the **Direct Line App Service Extension Named Pipe**:
    - Update the bot's index.js (below the assignment of the adapter and bot) to include the following code that pulls the App Service name from the environment and instructs the adapter to connect to the appropriate named pipe:
    
    ```Node.js
    
    adapter.useNamedPipe(async (context) => {
        await myBot.run(context);
        },
        process.env.APPSETTING_WEBSITE_SITE_NAME + '.directline'
    );
    ```   

1. Save the `index.js` file.
1. Update the `Web.Config` file to add the `AspNetCore` handler and rule needed by Direct Line App Service Extension to service requests:
    - Locate the `Web.Config` file in the `wwwroot` directory of your bot and modify the contents to include the following entries to the `Handlers` and `Rules` sections:
    
    ```XML
    <handlers>      
          <add name="aspNetCore" path="*/.bot/*" verb="*" modules="AspNetCoreModule" resourceType="Unspecified" />
    </handlers>
    
    <rewrite>
      <rules>
        <!-- Do not interfere with Direct Line App Service Extension requests. -->
        <rule name ="DLASE" stopProcessing="true">
          <conditions>
            <add input="{REQUEST_URI}" pattern="^/.bot"/>
          </conditions>
        </rule>
      </rules>
    </rewrite>
    ```
    
1. Open the `appsettings.json` file and enter the following values:
    1. `"MicrosoftAppId": "<secret Id>"`
    1. `"MicrosoftAppPassword": "<secret password>"`

    The values are the **appid** and the **appSecret** associated with the service registration group.

1. **Publish** the bot to your Azure Web App Bot resource.

### Enable bot Direct Line app service extension

1. In the Azure portal, locate your **Web App Bot** resource.
1. From the left panel menu under *Bot management* click on **Channels** to configure the **Azure Bot Service** channels your bot accepts messages from.
1. If it is not already enabled, click on the **Direct Line** channel and follow instructions to enable the channel.
1. In the **Connect to channels** table click on the **Edit** link on the Direct Line row.
1. Scroll down to the **App Service Extension Keys** section.
1. Click on the **Show** link to reveal one of the keys. You will use this value in the steps below.

    ![App service extension keys](./media/channels/direct-line-extension-extension-keys.png)

1. From the left panel menu under *Application settings* section, click the **Configuration** item.
1. In the right panel, add the following new settings:

    |Name|Value|
    |---|---|
    |DirectLineExtensionKey|<App_Service_Extension_Key>|
    |DIRECTLINE_EXTENSION_VERSION|latest|

    Where the *App_Service_Extension_Key* is the value you saved earlier.

1. If your bot is hosted in a sovereign or otherwise restricted Azure cloud (i.e. you do not access Azure via the [public portal](https://portal.azure.com)) you will also need to add the following new setting:

    |Name|Value|
    |---|---|
    |DirectLineExtensionABSEndpoint|<URL_of_Direct_Line_App_Gateway>|

    Where *URL_of_Direct_Line_App_Gateway* is specific to the Azure cloud your bot is hosted in.

1. Still within the *Configuration* section, click on the **General** settings section and turn on **Web sockets**
1. Click on **Save** to save the settings. This restarts the Azure App Service.

## Confirm Direct Line app extension and the bot are configured

In your browser, navigate to https://<your_app_service>.azurewebsites.net/.bot.
If everything is correct, the page will return this JSON content: `{"v":"123","k":true,"ib":true,"ob":true,"initialized":true}`. This is the information you obtain when **everything works correctly**, where

- **v** displays the build version of the Direct Line App Service Extension (ASE).
- **k** determines whether Direct Line ASE can read an App Service Extension Key from its configuration.
- **initialized** determines whether Direct Line ASE can use the App Service Extension Key to download the bot metadata from Azure Bot Service
- **ib** determines whether Direct Line ASE can establish an inbound connection with the bot.
- **ob** determines whether Direct Line ASE can establish an outbound connection with the bot.

## Troubleshooting

- If the *ib* and *ob* values displayed by the **.bot endpoint* are false this means the bot and the Direct Line app service extension are unable to connect to each other. 
    1. Double check the code for using named pipes has been added to the bot.
    1. Confirm the bot is able to start up and run at all. Useful tools are **Test in WebChat**, connecting an additional channel, remote debugging, or logging.
    1. Restart the entire **Azure App Service** the bot is hosted within, to ensure a clean start up of all processes.

- If the *initialized* value of the **.bot endpoint** is false it means the Direct Line app service extension is unable to validate the **App Service Extension Key** added to the bot's *Application Settings* above. 
    1. Confirm the value was correctly entered.
    1. Switch to the alternative **App Service Extension Key** shown on your bot's **Direct Line channel configuration page**.

## Next steps

> [!div class="nextstepaction"]
> [Use Web Chat with the Direct Line app service extension](./bot-service-channel-directline-extension-webchat-client.md)
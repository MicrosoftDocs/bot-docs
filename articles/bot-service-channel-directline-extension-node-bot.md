---
title: Configure Node.js bots for Direct Line App Service extension in the Bot Framework SDK
description: Configure Node.js bots to work with named pipes. Enable the Direct Line App Service extension and configure bots to use the extension.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: Daniel.Evans
ms.service: bot-service
ms.topic: how-to
ms.date: 03/30/2022
---

# Configure Node.js bot for extension

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to update a Node.js bot to work with named pipes and how to enable the Direct Line App Service extension in the Azure App Service resource where the bot is hosted.

## Prerequisites

- An Azure account. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A Node.js bot deployed in Azure.
- Bot Framework SDK for Node.js, 4.7 or later.

## Enable Direct Line App Service extension

This section describes how to enable the Direct Line App Service extension using the App Service extension key from your bot's Direct Line channel configuration.

## Update bot code

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

[!INCLUDE [enable Bot Direct Line App Service extensions](includes/directline-enable-dl-asp.md)]

## Confirm the Direct Line extension and the bot are configured

[!INCLUDE [Confirm the extension and the bot are configured](includes/directline-confirm-extension-bot-config.md)]

## Troubleshooting

[!INCLUDE [Troubleshoot Direct Line extension](includes/directline-troubleshoot.md)]

## Next steps

> [!div class="nextstepaction"]
> [Use Web Chat with the Direct Line App Service extension](./bot-service-channel-directline-extension-webchat-client.md)
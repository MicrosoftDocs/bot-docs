---
title: Use Web Chat with the direct line app service extension
titleSuffix: Bot Service
description: Learn how to use Web Chat with a direct line app service extension. View code that shows how to set up a direct line URL for a bot and obtain a token.
services: bot-service
manager: kamrani
ms.service: bot-service
ms.topic: conceptual
ms.author: kamrani
ms.date: 07/25/2019
---

# Use Web Chat with the direct line app service extension

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to use Web Chat with the Direct Line app service extension. Web Chat version 4.9.1 or later is required for native Direct Line app service extension support.

## Integrate Web Chat client

> [!NOTE]
> Adaptive Cards sent through the Direct Line App Service Extension do not undergo the same processing as those sent through other versions of the Direct Line channel. Due to this the JSON representation of the Adaptive Card sent to Web Chat from the Direct Line App Service Extension will not have default values added by the channel if the fields are omitted by the bot when the card is created.

Generally speaking, the approach is the same as before. With the exception that in version 4.9.1 or later of **Web Chat** there is built in support for establishing a two-way **WebSocket**, which instead of connecting to [https://directline.botframework.com/](https://directline.botframework.com/) connects directly to the Direct Line app service extension hosted with your bot.
The Direct Line URL for your bot will be `https://<your_app_service>.azurewebsites.net/.bot/`, the Direct Line **endpoint** on your app service extension.
If you configure your own domain name, or your bot is hosted in a sovereign Azure cloud, substitute in the appropriate URL and append the `/.bot/` path to access the Direct Line app service extension's REST APIs.

1. Exchange the secret for a token by following the instructions in the [Authentication](https://docs.microsoft.com/azure/bot-service/rest-api/bot-framework-rest-direct-line-3-0-authentication?view=azure-bot-service-4.0&preserve-view=true) article. Instead of obtaining a token at `https://directline.botframework.com/v3/directline/tokens/generate`, you will generate the token directly from your Direct Line App Service Extension at  `https://<your_app_service>.azurewebsites.net/.bot/v3/directline/tokens/generate`.

1. For an example that shows how to fetch a token see [Web Chat Samples](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/01.getting-started/i.protocol-direct-line-app-service-extension).

```html
<!DOCTYPE html>
<html lang="en-US">
  <head>
    <title>Web Chat</title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <script
      crossorigin="anonymous"
      src="https://cdn.botframework.com/botframework-webchat/latest/webchat-minimal.js"
    ></script>
    <style>
      html,
      body {
        background-color: #f7f7f7;
        height: 100%;
      }

      body {
        margin: 0;
      }

      #webchat {
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.05);
        height: 100%;
        margin: auto;
        max-width: 480px;
        min-width: 360px;
      }
    </style>
  </head>
  <body>
    <div id="webchat" role="main"></div>
    <script>
      (async function() {
        <!-- NOTE: It is highly recommended to replace the below fetch with a call to your own token service as described in step 2 above, and to avoid exposing your channel secret in client side code. -->
        const res = await fetch('https://<your_app_service>.azurewebsites.net/.bot/v3/directline/tokens/generate', { method: 'POST', headers:{'Authorization':'Bearer ' + '<Your Bot's Direct Line channel secret>'}});
        const { token } = await res.json();

        window.WebChat.renderWebChat(
          {
            directLine: await window.WebChat.createDirectLineAppServiceExtension({
              domain: 'https://<your_app_service>.azurewebsites.net/.bot/v3/directline',
              token
            })
          },
          document.getElementById('webchat')
        );

        document.querySelector('#webchat > *').focus();
      })().catch(err => console.error(err));
    </script>
  </body>
</html>
```

> [!TIP]
> In the `JavaScript` bot implementation, make sure that `websockets` are enabled in the `web.config` file, as shown below.

```xml
<configuration>
    <system.webServer>
        <webSocket enabled="true"/>
        ...
    </system.webServer>
</configuration>
```

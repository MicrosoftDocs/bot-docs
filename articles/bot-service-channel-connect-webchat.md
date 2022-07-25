---
title: Connect a bot to Web Chat in the Bot Framework SDK
description: Learn how to use the Web Chat control to connect to a bot that uses the Web Chat channel.
keywords: web chat, bot channel, web page, secret key, HTML
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: mainguy
ms.service: bot-service
ms.topic: how-to
ms.date: 03/30/2022
---

# Connect a bot to Web Chat

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

When you [create a bot](bot-service-quickstart.md) with Azure, the Web Chat channel is automatically configured for you. The Web Chat channel includes the [Web Chat control](https://github.com/microsoft/BotFramework-WebChat], which provides the ability for users to interact with the bot directly in a web page.

The Web Chat channel contains everything you need to embed the Web Chat control in a web page. All you have to do to use the Web Chat control is get your bot's secret key and embed the control in a web page.

## Prerequisites

- An Azure account. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- An existing bot published to Azure.

## Web Chat security considerations

When you use Azure Bot Service authentication with Web Chat, there are some important security considerations you must keep in mind. For more information, see [Security considerations](rest-api/bot-framework-rest-direct-line-3-0-authentication.md#security-considerations).

## Embed the Web Chat control in a web page

The following image shows the components involved when embedding the Web Chat control in a web page.

:::image type="content" source="./media/bot-service-channel-webchat/webchat-control.png" alt-text="Web Chat control components":::

> [!IMPORTANT]
> Use Direct Line (with enhanced authentication) to mitigate security risks when connecting to a bot using the Web Chat control. For more information, see [Direct Line enhanced authentication](v4sdk\bot-builder-security-enhanced.md).

### Get your bot secret key

1. Go to [Azure Portal](https://portal.azure.com) and open your bot.
1. Under **Settings**, select **Channels**. Then select **Web Chat**.
1. The **Web Chat** page will open. Select the **Default Site** from the list of **Sites**.
1. Copy the first **Secret key** and the **Embed code**.

### Development embedding options

#### Option 1 - Exchange your secret for a token, and generate the embed

This is a good option if:

- you can execute a server-to-server request to exchange your web chat secret for a temporary token
- you want to make it difficult for other developers to embed your bot in their websites

Using this option won't completely prevent other developers from embedding your bot in their websites, but it does make it difficult for them to do so.

To exchange your secret for a token and generate the embed:

1. Issue a **GET** request to `https://webchat.botframework.com/api/tokens` and pass your web chat secret via the `Authorization` header. The `Authorization` header uses the `BotConnector` scheme and includes your secret.

2. The response to your **GET** request will contain the token (surrounded with quotation marks) that can be used to start a conversation by rendering the Web Chat control. A token is valid for one conversation only; to start another conversation, you need to generate a new token.

3. Within the **Embedded code** that you copied from the Web Chat channel earlier, change the `s=` parameter to `t=` and replace "YOUR_SECRET_HERE" with your token.

> [!NOTE]
> Tokens will automatically be renewed before they expire.

##### Example request

```http
requestGET https://webchat.botframework.com/api/tokens
Authorization: BotConnector YOUR_SECRET_HERE
```

> [!NOTE]
> Please note that for Azure Government, the token exchange URL is different.

```http
requestGET https://webchat.botframework.azure.us/api/tokens
Authorization: BotConnector YOUR_SECRET_HERE
```

##### Example response

```response
"IIbSpLnn8sA.dBB.MQBhAFMAZwBXAHoANgBQAGcAZABKAEcAMwB2ADQASABjAFMAegBuAHYANwA.bbguxyOv0gE.cccJjH-TFDs.ruXQyivVZIcgvosGaFs_4jRj1AyPnDt1wk1HMBb5Fuw"
```

##### Example HTML code

```html
<!DOCTYPE html>
<html>
<body>
  <iframe id="chat" style="width: 400px; height: 400px;" src=''></iframe>
</body>
<script>

    var xhr = new XMLHttpRequest();
    xhr.open('GET', "https://webchat.botframework.com/api/tokens", true);
    xhr.setRequestHeader('Authorization', 'BotConnector ' + 'YOUR SECRET HERE');
    xhr.send();
    xhr.onreadystatechange = processRequest;

    function processRequest(e) {
      if (xhr.readyState == 4  && xhr.status == 200) {
        var response = JSON.parse(xhr.responseText);
        document.getElementById("chat").src="https://webchat.botframework.com/embed/<botname>?t="+response
      }
    }

  </script>
</html>
```

<a id="option-2"></a>

#### Option 2 - Embed the web chat control in your website using the secret

Use this option if you want to allow other developers to easily embed your bot into their websites.

> [!WARNING]
> With this option, the Web Chat channel secret key is exposed in the client web page. Use this option only for development purposes and not in a production environment.

To embed your bot in a web page by specifying the secret within the **Embedded code**:

1. Copy the **Embedded code** from the Web Chat channel within the Azure portal (described in [Get your bot secret key](#get-your-bot-secret-key) above).

1. Within that **Embedded code**, replace "YOUR_SECRET_HERE" with the **Secret key** value that you copied from the same page.

### Production embedding option

#### Keep your secret hidden, exchange your secret for a token, and generate the embed

This option doesn't expose the Web Chat channel secret key in the client web page.

The client needs to provide a token to talk to the bot. To learn about the differences between secrets and tokens
and to understand the risks associated with using secrets, see [Direct Line authentication](rest-api/bot-framework-rest-direct-line-3-0-authentication.md).

The following client web page shows how to use a token with the Web Chat. If you use Azure Gov, adjust the URLs from public to government.

```html
<!DOCTYPE html>
<html>
  <head>
    <script src="https://cdn.botframework.com/botframework-webchat/latest/webchat.js"></script>
  </head>
  <body>
    <h2>Web Chat bot client using Direct Line</h2>

    <div id="webchat" role="main"></div>

    <script>

     // "styleSet" is a set of CSS rules which are generated from "styleOptions"
     const styleSet = window.WebChat.createStyleSet({
         bubbleBackground: 'rgba(0, 0, 255, .1)',
         bubbleFromUserBackground: 'rgba(0, 255, 0, .1)',

         botAvatarImage: '<your bot avatar URL>',
         botAvatarInitials: 'BF',
         userAvatarImage: '<your user avatar URL>',
         userAvatarInitials: 'WC',
         rootHeight: '100%',
         rootWidth: '30%'
      });

      // After generated, you can modify the CSS rules
      styleSet.textContent = {
         ...styleSet.textContent,
         fontFamily: "'Comic Sans MS', 'Arial', sans-serif",
         fontWeight: 'bold'
      };

      const res = await fetch('https:<YOUR_TOKEN_SERVER/API>', { method: 'POST' });
      const { token } = await res.json();

      window.WebChat.renderWebChat(
        {
          directLine: window.WebChat.createDirectLine({ token }),
          userID: 'WebChat_UserId',
          locale: 'en-US',
          username: 'Web Chat User',
          locale: 'en-US',
          // Passing 'styleSet' when rendering Web Chat
          styleSet
        },
        document.getElementById('webchat')
      );
    </script>
  </body>
</html>
```

For examples on how to generate a token, see:

- [Single sign-on demo](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/07.advanced-web-chat-apps/e.sso-on-behalf-of-authentication)
- [Direct Line token](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/01.getting-started/k.direct-line-token)

## Additional information

- [Web Chat overview](./v4sdk/bot-builder-webchat-overview.md)
- [Web Chat customization](./v4sdk/bot-builder-webchat-customization.md)
- [Enable speech in Web Chat](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/03.speech/a.direct-line-speech)
- [Use Web Chat with the Direct Line App Service Extension](./bot-service-channel-directline-extension-webchat-client.md)
- [Connect a bot to Direct Line Speech](./bot-service-channel-connect-directlinespeech.md)
- [Add single sign-on to Web Chat](./v4sdk/bot-builder-webchat-sso.md)

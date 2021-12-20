---
title: Connect a bot to Web Chat in the Bot Framework SDK
description: Learn how to use the Web Chat control to connect to a bot that uses the Web Chat channel.
keywords: web chat, bot channel, web page, secret key, iframe, HTML
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: mainguy
ms.service: bot-service
ms.topic: how-to
ms.date: 09/21/2021
---

# Connect a bot to Web Chat

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

When you [create a bot](bot-service-quickstart.md) with the Framework Bot Service, the Web Chat channel is automatically configured for you. The Web Chat channel includes the [Web Chat control](https://github.com/microsoft/BotFramework-WebChat), which provides the ability for users to interact with the bot directly in a web page.

![Web chat sample](./media/bot-service-channel-webchat/create-a-bot.png)

The Web Chat channel in the Bot Framework Portal contains everything you need to embed the Web Chat control in a web page. All you have to do to use the web chat control is get your bot's secret key and embed the control in a web page.

## Web Chat security considerations

When you use Azure Bot Service authentication with Web Chat there are some important security considerations you must keep in mind. Please, refer to [Security considerations](rest-api/bot-framework-rest-direct-line-3-0-authentication.md#security-considerations).

## Embed the Web Chat control in a web page

The following picture shows the components involved when embedding the Web Chat control in a web page.

  ![bot embed components](./media/bot-service-channel-webchat/webchat-control.png)

> [!IMPORTANT]
> As the previous picture implies, you need to use Direct Line (with enhanced authentication) to mitigate security risks when connecting to a bot using the Web Chat control. For more information, see [Direct Line enhanced authentication](v4sdk\bot-builder-security-enhanced.md).

### Get your bot secret key

1. Open your bot in the [Azure Portal](https://portal.azure.com) and click **Channels** blade.

2. Click **Edit** for the **Web Chat** channel.

    ![Web chat channel](./media/bot-service-channel-webchat/bot-service-channel-list.png)

3. Under **Secret keys**, click **Show** for the first key.

    ![Secret key](./media/bot-service-channel-webchat/secret-key.png)

4. Copy the **Secret key** and the **Embed code**.

5. Click **Done**.

### Development embedding options

#### Option 1 - Exchange your secret for a token, and generate the embed

Use this option if you can execute a server-to-server request to exchange your web chat secret for a temporary token,
and if you want to make it difficult for other developers to embed your bot in their websites.
Although using this option will not absolutely prevent other developers from embedding your bot in their websites,
it does make it difficult for them to do so.

To exchange your secret for a token and generate the embed:

1. Issue a **GET** request to `https://webchat.botframework.com/api/tokens` and pass your web chat secret via the `Authorization` header. The `Authorization` header uses the `BotConnector` scheme and includes your secret, as shown in the example request below.

2. The response to your **GET** request will contain the token (surrounded with quotation marks) that can be used to start a conversation by rendering the web chat control within an **iframe**. A token is valid for one conversation only; to start another conversation, you must generate a new token.

3. Within the `iframe` **Embed code** that you copied from the Web Chat channel within the Bot Framework Portal (as described in [Get your bot secret key](#get-your-bot-secret-key) above), change the `s=` parameter to `t=` and replace "YOUR_SECRET_HERE" with your token.

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

##### Example iframe (using token)

```html
<iframe src="https://webchat.botframework.com/embed/YOUR_BOT_ID?t=YOUR_TOKEN_HERE"></iframe>
```

> [!NOTE]
> Please note that for Azure Government, the example iframe looks different.

```html
<iframe src="https://webchat.botframework.azure.us/embed/YOUR_BOT_ID?t=YOUR_TOKEN_HERE"></iframe>
```

##### Example html code

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

To embed your bot in a web page by specifying the secret within the `iframe` tag, perform the steps described below.

1. Copy the `iframe` **Embed code** from the Web Chat channel within the Bot Framework Portal (as described in [Get your bot secret key](#get-your-bot-secret-key) above).

2. Within that **Embed code**, replace "YOUR_SECRET_HERE" with the **Secret key** value that you copied from the same page.

##### Example iframe (using secret)

```html
<iframe style="height:480px; width:402px" src="https://webchat.botframework.com/embed/YOUR_BOT_ID?s=YOUR_SECRET_HERE"></iframe>
```

> [!NOTE]
> Please note that for Azure Government, the example iframe looks different.

```html
<iframe style="height:480px; width:402px" src="https://webchat.botframework.azure.us/embed/YOUR_BOT_ID?s=YOUR_SECRET_HERE"></iframe>
```

![Web Chat client](media/bot-service-channel-webchat/webchat-client.png)

### Production embedding option

#### Keep your secret hidden, exchange your secret for a token, and generate the embed

This option does not expose the Web Chat channel secret key in the client web page, as it is required in a production environment.

The client must provide a token to talk to the bot. To learn about the differences between secrets and tokens
and to understand the risks associated with using secrets, visit [Direct Line authentication](rest-api/bot-framework-rest-direct-line-3-0-authentication.md).

The following client web page shows how to use a token with the Web Chat. If you use Azure Gov, please adjust the URLs from public to government.

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

## Additional resources

- [Web Chat overview](./v4sdk/bot-builder-webchat-overview.md)
- [Web Chat customization](./v4sdk/bot-builder-webchat-customization.md)
- [Enable speech in Web Chat](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/03.speech/a.direct-line-speech)
- [Use Web Chat with the Direct Line App Service Extension](./bot-service-channel-directline-extension-webchat-client.md)
- [Connect a bot to Direct Line Speech](./bot-service-channel-connect-directlinespeech.md)
- [Add single sign-on to Web Chat](./v4sdk/bot-builder-webchat-sso.md)

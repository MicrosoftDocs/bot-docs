---
title: Connect a bot to the Web Chat channel | Microsoft Docs
description: Learn how to use the web chat control control in your web page for a bot connected to the Web Chat channel.
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---

# Connect a bot to Web Chat
When you register a bot with the Bot Framework, the Web Chat channel is automatically configured for you. The Web Chat channel includes the web chat control, which provides the ability for users to interact with your bot directly in a web page.

![Web chat sample](~/media/channel-webchat/webchat-sample.png)

The Web Chat channel in the Bot Framework Portal contains everything you need to embed the web chat control in a web page. All you have to do to use the web chat control is get your bot's secret key and embed the control in a web page.

##<a id="step-1"></a> Get your bot secret key

1. Sign in to the <a href="https://dev.botframework.com/" target="_blank">Bot Framework Portal</a>.
2. Click **My bots**.
3. Select your bot.
4. Under **Channels** on the bot dashboard, click **Edit** for the **Web Chat** channel.
![Web chat channel](~/media/channel-webchat/channel-list.png)
5. Click **Add new site**, name your site, and click **Done**.
6. Under **Secret keys**, click **Show** for the first key.
![Secret key](~/media/channel-webchat/secret-key.png)
7. Copy the **Secret key** and the **Embed code**.
8. Click **Done**.

## Embed the web chat control in your website

You can embed the web chat control in your website by using one of two options.

### Option 1 - Keep your secret hidden, exchange your secret for a token, and generate the embed

Use this option if you can execute a server-to-server request to exchange your web chat secret for a temporary token,
and if you want to make it difficult for other developers to embed your bot in their websites. 
Although using this option will not absolutely prevent other developers from embedding your bot in their websites, 
it does make it difficult for them to do so.

To exchange your secret for a token and generate the embed:

1. Issue a **GET** request to `https://webchat.botframework.com/api/tokens` and pass your web chat secret via the `Authorization` header. The `Authorization` header uses the `BotConnector` scheme and includes your secret, as shown in the example request below.

2. The response to your **GET** request will contain the token (surrounded with quotation marks) that can be used to start a conversation by rendering the web chat control within an **iframe**. A token is valid for one conversation only; to start another conversation, you must generate a new token.

3. Within the `iframe` **Embed code** that you copied from the Web Chat channel within the Bot Framework Portal (as described in [Step 1](#step-1) above), change the `s=` parameter to `t=` and replace "YOUR_SECRET_HERE" with your token. 

> [!NOTE]
> Tokens will automatically be renewed before they expire. 

##### Example request

```
GET https://webchat.botframework.com/api/tokens
Authorization: BotConnector YOUR_SECRET_HERE
```

##### Example response 

```
"IIbSpLnn8sA.dBB.MQBhAFMAZwBXAHoANgBQAGcAZABKAEcAMwB2ADQASABjAFMAegBuAHYANwA.bbguxyOv0gE.cccJjH-TFDs.ruXQyivVZIcgvosGaFs_4jRj1AyPnDt1wk1HMBb5Fuw"
```

##### Example iframe (using token)

```html
<iframe src="https://webchat.botframework.com/embed/YOUR_BOT_ID?t=YOUR_TOKEN_HERE"></iframe>
```

###<a id="option-2"></a> Option 2 - Embed the web chat control in your website using the secret

Use this option if you want to allow other developers to easily embed your bot into their websites. 

> [!WARNING]
> If you use this option, other developers can embed your bot into their websites 
> by simply copying your embed code.

To embed your bot in your website by specifying the secret within the `iframe` tag:

1. Copy the `iframe` **Embed code** from the Web Chat channel within the Bot Framework Portal (as described in [Step 1](#step-1) above).

2. Within that **Embed code**, replace "YOUR_SECRET_HERE" with the **Secret key** value that you copied from the same page.

##### Example iframe (using secret)

```html
<iframe src="https://webchat.botframework.com/embed/YOUR_BOT_ID?s=YOUR_SECRET_HERE"></iframe>
```

## Style the web chat control

You may change the size of the web chat control by using the `style` attribute of the `iframe` to specify `height` and `width`.

```html
<iframe style="height:480px; width:402px" src="... SEE ABOVE ..."></iframe>
```

![Chat control Client](~/media/chatwidget-client.png)

## Additional resources

You can [download the source code](https://github.com/Microsoft/BotFramework-WebChat) for the web chat control on GitHub.
---
title: Add the bot chat control to a web site | Microsoft Docs
description: Learn how to embed the Bot Framework chat control in your web page and conrol whether other developers can embed the bot on their pages.
author: RobStand
ms.author: rstand


manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/01/2017
ms.reviewer:

#REVIEW
---
# Add the bot chat control to a web site

The Bot Framework chat control allows you to chat with your bot through the Bot Connector. The chat widget may be enabled on the bot's landing page in the directory, and you may embed the web chat control in your own page.

The chat widget supports [Markdown](https://en.wikipedia.org/wiki/Markdown) and images.

## Flow
![Chat widget Overview](~/media/chatwidget-overview.png)

##<a id="step-1"></a> Step 1 - Get your bot secret key

1. Sign in to the <a href="https://dev.botframework.com/" target="_blank">Bot Framework Portal</a>.
2. Click **My bots**.
3. Select your bot. 
4. Under **Channels** on the bot dashboard, click **Edit** for the **Web Chat** channel.
    ![Chat widget channel](~/media/chatwidget-channel.png)
5. Click **Add new site**, name your site, and click **Done**.
6. Under **Secret keys**, click **Show** for the first key.
7. Copy the **Secret key** and the **Embed code**. 
8. Click **I'm done configuring Web Chat**.

## Step 2 - Embed the chat widget in your website

You can embed the chat widget in your website by using one of two options.

### Option 1 - Keep your secret hidden, exchange your secret for a token, and generate the embed

Use this option if you can execute a server-to-server request to exchange your web chat secret for a temporary token,
and if you want to make it difficult for other developers to embed your bot in their websites. 
Although using this option will not absolutely prevent other developers from embedding your bot in their websites, 
it does make it difficult for them to do so.

To exchange your secret for a token and generate the embed:

1. Issue a **GET** request to `https://webchat.botframework.com/api/tokens` and pass your web chat secret via the `Authorization` header. The `Authorization` header uses the `BotConnector` scheme and includes your secret, as shown in the example request below.

2. The response to your **GET** request will contain the token (surrounded with quotation marks) that can be used to start a conversation by rendering the chat widget within an **iframe**. A token is valid for one conversation only; to start another conversation, you must generate a new token.

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

###<a id="option-2"></a> Option 2 - Embed the chat widget in your website using the secret

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

## Step 3 - Style the chat control

You may change the size of the chat control by using the `style` attribute of the `iframe` to specify `height` and `width`.

```html
<iframe style="height:480px; width:402px" src="... SEE ABOVE ..."></iframe>
```

![Chat widget Client](~/media/chatwidget-client.png)


layout: page
title: Embed the Chat Control
permalink: /en-us/support/embed-chat-control2/
weight: 9300
parent1: Bot Framework Resources


## Overview

The Bot Framework chat widget allows you to chat with your bot through the Bot Connector. The chat widget may be enabled on the bot's landing page in the directory, and you may embed the web chat control in your own page.

The Chat widget supports [Markdown](https://en.wikipedia.org/wiki/Markdown) and images.

## Flow
![Chat widget Overview](/en-us/images/chatwidget/chatwidget-overview.png)

## Step 1 - Get your bot secret key
1. Go to [https://dev.botframework.com/#/bots](https://dev.botframework.com/#/bots)
2. Click on your bot
3. Look for “Web Chat” in the Channels section

![Chat widget channel](/en-us/images/chatwidget/chatwidget-channel.png)

Click on Edit for Web chat and press Generate Web Chat Secret

![Chat widget Token](/en-us/images/chatwidget/chatwidget-token.PNG)

Copy the generated secret and embed tag and press “I'm done configuring Web Chat”

## Step 2 - Embed the chat widget in your website

You can embed the chat control in either of these ways:

### Option 1 - Keep your secret hidden, exchange your secret for a token, and generate the embed

Use this option if you can make a server-to-server call to exchange your web chat secret for a temporary token,
and if you want to discourage other websites from embedding your bot in their pages.

Note that this does not prevent another website from embedding your bot in their pages, but it makes it substantially
harder than option 2 below.

To exchange your secret for a token and generate the embed:

1. Issue a server-to-server GET request to "https://webchat.botframework.com/api/tokens" and pass your web chat secret as the Authorization header.
2. The Authorization header uses the "BotConnector" scheme and includes your secret. (This auth scheme may also be used with a token but for now we're using our secret to generate a token.) See example below.
3. The call will return a token good for one conversation. If you want to start a new conversation, you must generate a new token.
4. Change the "s=" parameter in your iframe embed to "t=". The "t=" form works with tokens and automatically renews them before they expire.

Example request:

    -- connect to webchat.botframework.com --
    GET /api/tokens
    Authorization: BotConnector RCurR_XV9ZA.cwA.BKA.iaJrC8xpy8qbOF5xnR2vtCX7CZj0LdjAPGfiCpg4Fv0
    
Note, the above secret is a sample and will not work. Use your own secret.

{% highlight html %}

<iframe src="https://webchat.botframework.com/embed/YOUR_BOT_ID?t=YOUR_TOKEN_HERE"></iframe>

{% endhighlight %}

### Option 2 - Embed the chat widget in your website using secret

Use this option if you are OK with other websites embedding your bot into their page. This option provides no protection against
other developers copying your embed code.

If you do not want other websites to host web chat with your bot, option 1 makes it more difficult to do so.

To embed your bot in your web site by include your secret on your web page:

1. Copy the iframe embed code from the channel. See Step 1, above.
2. Replace YOUR_SECRET_HERE with the embed secret from the same page.

{% highlight html %}

<iframe src="https://webchat.botframework.com/embed/YOUR_BOT_ID?s=YOUR_SECRET_HERE"></iframe>

{% endhighlight %}

## Step 3 - Style the chat control

You may change the chat control's size by adding height and width to the style element.

{% highlight html %}

<iframe style="height:480px; width:402px" src="... SEE ABOVE ..."></iframe>

{% endhighlight %}

![Chat widget Client](/en-us/images/chatwidget/chatwidget-client.png)

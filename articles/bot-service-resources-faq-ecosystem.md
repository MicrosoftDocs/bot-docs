---
title: Bot Framework Frequently Asked Questions Ecosystem - Bot Service
description: Frequently Asked Questions about Bot Framework ecosystem.
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 06/08/2020
---

# Ecosystem

<!-- Attention writers!!
     1 - This article contains FAQs regarding Bot Framework ecosystem.
     1 - When you create a new FAQ, please add the related link in the bot-service-resources-bot-framework-faq.md proper section. -->

## When will you add more conversation experiences to the Bot Framework?

We plan on making continuous improvements to the Bot Framework, including additional channels, but cannot provide a schedule at this time.
If you would like a specific channel added to the framework, [let us know][Support].

## I have a communication channel I'd like to be configurable with Bot Framework. Can I work with Microsoft to do that?

We have not provided a general mechanism for developers to add new channels to Bot Framework, but you can connect your bot to your app via the [Direct Line API][DirectLineAPI]. If you are a developer of a communication channel and would like to work with us to enable your channel in the Bot Framework [we'd love to hear from you][Support].

## If I want to create a bot for Microsoft Teams, what tools and services should I use?

The Bot Framework is designed to build, connect, and deploy high quality, responsive, performant and scalable bots for Teams and many other channels. The SDK can be used to create text/sms, image, button and card-capable bots (which constitute the majority of bot interactions today across conversation experiences) as well as bot interactions which are Teams-specific such as rich audio and video experiences.

If you already have a great bot and would like to reach the Teams audience, your bot can easily be connected to Teams (or any supported channel) via the Bot Framework for REST API (provided it has an internet-accessible REST endpoint).

## How do I create a bot that uses the US Government data center?

There are 2 major steps required to create a bot that uses a US Government data center.

1. Add a "channel provider" setting in your appsettings.json (or the App Service Settings). This needs to be specifically set to this name/value constant: ChannelService = "https://botframework.azure.us". An example using appsetting.json is shown below.

```json
{
  "MicrosoftAppId": "",
  "MicrosoftAppPassword": "",
  "ChannelService": "https://botframework.azure.us"
}
```

1. If you are using .NET core, you will need to add a ConfigurationChannelProvider in your startup.cs file. How you do this varies based on which version of the SDK you are using.

- For versions 4.3 and above, in your ConfigureServices method, you need to create a ConfigurationChannelProvider instance. When using the BotFrameworkHttpAdapter class, you inject this as singleton into the service collection like this:

```csharp
services.AddSingleton<IChannelProvider, ConfigurationChannelProvider>();
```

- For versions prior to 4.3, in your ConfigureServices method, find the AddBot method. When setting the options, make sure you add:

```csharp
options.ChannelProvider = new ConfigurationChannelProvider();
```

You can find more information concerning Government Services [here](https://docs.microsoft.com/azure/azure-government/documentation-government-services-aiandcognitiveservices#azure-bot-service)

## What is the Direct Line channel?

Direct Line is a REST API that allows you to add your bot into your service, mobile app, or webpage.

You can write a client for the Direct Line API in any language. Simply code to the [Direct Line protocol][DirectLineAPI], generate a secret in the Direct Line configuration page, and talk to your bot from wherever your code lives.

Direct Line is suitable for:

- Mobile apps on iOS, Android, and Windows Phone, and others
- Desktop applications on Windows, OSX, and more
- Webpages where you need more customization than the [embeddable Web Chat channel][WebChat] offers
- Service-to-service applications

## What are the steps to configure Web Chat and Direct Line for Azure Government?

The steps to configure Web Chat and Direct Line for Azure Government are similar to the those used for public Azure. In Azure Government, you set the [domain](https://github.com/microsoft/BotFramework-WebChat/blob/master/packages/bundle/src/createDirectLine.js#L6) to the Azure Government URL because the [default domain](https://github.com/microsoft/BotFramework-DirectLineJS/blob/master/src/directLine.ts#L456) applies to public Azure, not Azure Government. Please, also notice that the public Azure URL (`https://webchat.botframework.com/v3/directline`) is different from Azure Government URL (`https://webchat.botframework.azure.us/v3/directline`) for the Web Chat and Direct Line configuration.

The following example shows how to set the domain to the Azure Government URL:

```html
<body>
    <div id="webchat" role="main"></div>
    <script>
      window.WebChat.renderWebChat(
        {
          directLine: window.WebChat.createDirectLine({
          token: 'YOUR_TOKEN_SECRET',
		  domain: 'https://webchat.botframework.azure.us/v3/directline'
          }),
          userID: 'YOUR_USER_ID',
          username: 'Web Chat User',
          locale: 'en-US',
          botAvatarInitials: 'WC',
          userAvatarInitials: 'WW'
        },
        document.getElementById('webchat')
      );
    </script>
  </body>

```
Learn more from the following docs:
* [Connect a bot to Web Chat](https://docs.microsoft.com/azure/bot-service/bot-service-channel-connect-webchat?view=azure-bot-service-4.0)
* [Connect a bot to Direct Line](https://docs.microsoft.com/azure/bot-service/bot-service-channel-connect-directline?view=azure-bot-service-4.0)
* For programmatic approach to exchange your secret for a token, use the code snippet provided [here](https://docs.microsoft.com/azure/bot-service/bot-service-channel-connect-webchat?view=azure-bot-service-4.0#production-embedding--option) and adjust the URLs from public Azure to Azure Government.


## How does the Bot Framework relate to Cognitive Services?

Both the Bot Framework and [Cognitive Services](https://www.microsoft.com/cognitive) are built from years of research and use in popular Microsoft products. These capabilities enable every organization to take advantage of the power of data, the cloud and intelligence to build their own intelligent systems that unlock new opportunities, increase their speed of business and lead the industries in which they serve their customers.

## What are the possible machine-readable resolutions of the LUIS built-in date, time, duration, and set entities?

For a list of examples, see the [Pre-built entities section](/azure/cognitive-services/LUIS/luis-reference-prebuilt-entities) of the LUIS documentation.

## How can I use more than the maximum number of LUIS intents?

You might consider splitting up your model and calling the LUIS service in series or parallel.

## How can I use more than one LUIS model?

Both the Bot Framework SDK for Node.js and the Bot Framework SDK for .NET support calling multiple LUIS models from a single LUIS intent dialog. Keep in mind the following caveats:

* Using multiple LUIS models assumes the LUIS models have non-overlapping sets of intents.
* Using multiple LUIS models assumes the scores from different models are comparable, to select the "best matched intent" across multiple models.
* Using multiple LUIS models means that if an intent matches one model, it will also strongly match the "none" intent of the other models. You can avoid selecting the "none" intent in this situation; the Bot Framework SDK for Node.js will automatically scale down the score for "none" intents to avoid this issue.

## Where can I get more help on LUIS?

- [Introduction to Language Understanding (LUIS) - Microsoft Cognitive Services](https://www.youtube.com/watch?v=jWeLajon9M8) (video)
- [Advanced Learning Session for Language Understanding (LUIS)](https://www.youtube.com/watch?v=39L0Gv2EcSk) (video)
- [LUIS documentation](/azure/cognitive-services/luis/)
- [Language Understanding Forum](https://social.msdn.microsoft.com/forums/azure/home?forum=LUIS)

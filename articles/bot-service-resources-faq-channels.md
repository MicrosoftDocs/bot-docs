---
title: Bot Framework Frequently Asked Questions Channels - Bot Service
description: Frequently Asked Questions about Bot Framework channels.
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 06/08/2020
---

# Channels

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
2. If you are using .NET core, you will need to add a ConfigurationChannelProvider in your startup.cs file. How you do this varies based on which version of the SDK you are using.

- For versions 4.3 and above, in your ConfigureServices method, you need to create a ConfigurationChannelProvider instance. When using the BotFrameworkHttpAdapter class, you inject this as singleton into the service collection like this:

```csharp
services.AddSingleton<IChannelProvider, ConfigurationChannelProvider>();
```
- For versions prior to 4.3, in your ConfigureServices method, find the AddBot method. When setting the options, make sure you add:

```csharp
options.ChannelProvider = new ConfigurationChannelProvider();
```
You can find more information concerning Govenment Services [here](https://docs.microsoft.com/azure/azure-government/documentation-government-services-aiandcognitiveservices#azure-bot-service)

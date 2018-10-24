---
title: Internet of Things bot scenario | Microsoft Docs
description: Explore the Internet of Things bot scenario with the Bot Framework.
author: BrianRandell
ms.author: v-brra
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---
# Internet of Things (IoT) Bot Scenario

[!INCLUDE [pre-release-label](includes/pre-release-label-v3.md)]

This Internet of Things (IoT) Bot makes it easy for you to control devices around your home, such as a Philips Hue light using voice or interactive chat commands.

People love to talk to their things. Since the days of the first TV remote, people have loved not having to move to affect their environment. This IoT bot allows a person to manage a Philips Hue by simple chat commands or voice. In addition, when using chat, a person can be given visual choices related to colors to pick.

![The Internet of Things bot diagram](~/media/scenarios/bot-service-scenario-iot-bot.png)

Here is the logic flow of an IoT bot:

1. The user logs into Skype and accesses the IoT bot.
2. Using voice, the user asks the bot to turn on the lights via the IoT device.
3. The request is relayed to a 3rd party service that has access to the IoT device network.
4. The results of the command are returned to the user.
5. Application insights gathers runtime telemetery to help development with bot performance and usage.

## Sample bot
The IoT bot will allow you to quickly use chat commands from channels like Skype or Slack to control your Hue. To facilitate remote access, you'll call IFTTT applets predefined to work with Hue.

You can download or clone the source code for this sample bot from [Samples for Common Bot Framework Scenarios](https://aka.ms/bot/scenarios).

## Components you'll use
The Internet of Things (IoT) Bot uses the following components:
-   Philips Hue
-   If This Then That (IFTTT)
-   Application Insights

### Philips Hue
Philips Hue connected bulbs and bridge let you to take full control of your lighting. Whatever you want to do with your lighting, Hue can. Hue has an API you can use from your local network. However, you want to be able to access your Hue controlled devices and lights from anywhere using a friendly Bot interface. Thus you'll access Hue via IFTTT.

### IFTTT
IFTTT is a free web-based service that people use to create chains of simple conditional statements, called applets. You can trigger an applet from your Bot to have it do something on your behalf. There are a number of predefined Hue applets available to turn lights on and off, change the scene, and more.

### Application Insights
Application Insights helps you get actionable insights through application performance management (APM) and instant analytics. Out of the box you get rich performance monitoring, powerful alerting, and easy-to-consume dashboards to help ensure your Bot is available and performing as you expect. You can quickly see if you have a problem, then perform a root cause analysis to find and fix the issue.

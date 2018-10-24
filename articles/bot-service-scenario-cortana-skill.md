---
title: Cortana Skills bot scenario | Microsoft Docs
description: Explore the Cortana Skills bot scenario with the Bot Framework.
author: BrianRandell
ms.author: v-brra
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: cognitive-services
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---
# Cortana Skills Bot Scenario

[!INCLUDE [pre-release-label](includes/pre-release-label-v3.md)]

The Cortana Skills Bot extends Cortana to make it easy to book a mobile auto maintenance appointment using voice with context from your calendar.

Cortana is your personal assistant. Using the natural interface of your voice and a custom Cortana Skill Bot, you can ask Cortana to speak to an organization, such as an auto shop, to help you make an appointment. The service can provide a list of services, times available, and duration. Cortana can look at your calendar to see if you have something at a conflicting time and if not, create the appointment and add it to your calendar.

![The Cortana Skill bot diagram](~/media/scenarios/bot-service-scenario-cortana-skill.png)

Here is the logic flow of a Cortana Skills bot for an auto shop:

1. The user accesses Cortana from their PC or mobile device.
2. Using either text or voice commands, the user asks for an automobile maintenance appointment.
3. Because the bot is integrated with Cortana, it has access to the user's calendar and applies logic to the request.
4. With that information, the bot can query the auto service for valid appointments.
5. Presented with contextual options, the user can book the appointment.
6. Application insights gathers runtime telemetery to help development with bot performance and usage.

## Sample bot
With a Cortana Skills Bot, it's all about personal context. Using Cortana you could use your voice to ask for "Bob's Mobile Maintenance" to come work on your car based on your location. Using personal information exposed via Cortana your bot can confirm the location based on where the user is at when they're talking to the bot.

You can download or clone the source code for this sample bot from [Samples for Common Bot Framework Scenarios](https://aka.ms/bot/scenarios).

## Components you'll use
The Cortana Bot uses the following components:
-   Cortana
-   Application Insights

### Cortana
Now you can add support to your Bot by creating a Cortana Skill. You use the Cortana skills kit to build new features (called skills) for Cortana. A skill is a construct that allows Cortana to do more. You build skills to integrate with your Bots allowing Cortana to complete tasks and get things done. As part of the invocation process, Cortana can (with the user's consent) pass information about the user to a skill at runtime, so that the skill can customize its experience accordingly. Cortana's contextual knowledge allows your Bot to be useful and possibly even clever for them. Once invoked, certain types of skills can manipulate Cortana's interface to have a conversation between the skill and the end user. Once published, users can see and use your skill on Cortana for Windows 10 Anniversary Update+ (Desktop and Mobile), iOS, and Android.

### Application Insights
Application Insights helps you get actionable insights through application performance management (APM) and instant analytics. Out of the box you get rich performance monitoring, powerful alerting, and easy-to-consume dashboards to help ensure your Bot is available and performing as you expect. You can quickly see if you have a problem, then perform a root cause analysis to find and fix the issue.

## Next steps
Next, learn about the Enterprise Productivity bot scenario.

> [!div class="nextstepaction"]
> [Enterprise Productivity bot scenario](bot-service-scenario-enterprise-productivity.md)

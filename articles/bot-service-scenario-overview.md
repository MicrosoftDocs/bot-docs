---
title: Bot Service scenarios overview | Microsoft Docs
description: Explore key scenarios for powerful and successful bots built with Bot Service.
author: BrianRandell
ms.author: v-brra
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---
# Bot scenarios

[!INCLUDE [pre-release-label](includes/pre-release-label-v3.md)]

This topic explores key scenarios for powerful and successful bots built using Bot Service.

You can download or clone the source code for all the scenarios bot samples from [Samples for Common Bot Framework Scenarios](https://aka.ms/bot/scenarios).

## Commerce bot scenario
The [Commerce bot](bot-service-scenario-commerce.md) scenario describes a bot that replaces the traditional e-mail and phone call interactions that people typically have with a hotel's concierge service. The bot takes advantage of Cognitive Services to better process customer requests via text and voice with context gathered from integration with backend services.

In the Commerce bot scenario, a customer can make a request for concierge services with a hotel. She is authenticated via an Azure Active Directory v2 authentication endpoint. The bot can look up the customer's reservations and provide different service options. For example, the customer might book a cabana by the pool. The bot uses Language Understanding Intelligent Services (LUIS)  to parse the request and then the bot walks the user through the process of booking a cabana for an existing reservation.

## Cortana Skill bot scenario
The [Cortana Skill bot](bot-service-scenario-cortana-skill.md) scenario takes advantage of Cortana. Using the natural interface of your voice and a custom Cortana Skill bot, you can ask Cortana to speak to a organization, such as a mobile auto detailing company, to help you make an appointment based on where you’re at when you make the call. The bot can provide a list of services, times available, and duration.

## Enterprise Productivity bot scenario
The [Enterprise Productivity bot](bot-service-scenario-enterprise-productivity.md) scenario shows you how to integrate a bot with your Office 365 calendar and other services to increase your productivity.

The bot integrates with Office 365 to make it quicker and easier to create a meeting request with another person. In the process of doing so you could access additional services like Dynamics CRM. This sample provides the code necessary to integrate with Office 365 with authentication via Azure Active Directory. It provides mock entry points for external services as an exercise for the reader.

## Information bot scenario
This [Information bot](bot-service-scenario-informational.md) can answer questions defined in a knowledge set or FAQ using Cognitive Services QnA Maker and answer more open-ended questions Azure Search.

Often information is buried in structured data stores like SQL Server that can be easily surfaced via search. Imagine looking up a customer's order status by simple conversational commands. Using Cognitive Services QnA Maker, the user is presented with a set of valid search options like, lookup a customer, review customer's most recent order, etc. With the QnA format defined the user can easily ask questions that are backed by Azure Search which can look up data stored in a SQL Database.

## IoT bot scenario
This [Internet of Things (IoT) bot](bot-service-scenario-internet-things.md) bot makes it easy for you to control devices around your home, such as a Philips Hue light using interactive chat commands.

Using this simple bot, you can control your Philips Hue lights in conjunction with the free If This Then That (IFTTT) service. As an IoT device, the Philips Hue can be controlled locally via their exposed API. However, this API is not exposed for general access from outside the local network. However, IFTTT is a "[Friend of Hue](http://www2.meethue.com/en-us/friends-of-hue/ifttt/)" and thus has exposed a number of control commands that you can issue such as turning lights on and off, changing the light color, or the light intensity.

## Next steps
Now that you have an overview of the scenarios, dive deeper into each scenario.

> [!div class="nextstepaction"]
> [Commerce bot](bot-service-scenario-commerce.md)

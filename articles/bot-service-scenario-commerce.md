---
title: Commerce bot scenario | Microsoft Docs
description: Explore the Commerce bot scenario with the Bot Framework.
author: BrianRandell
ms.author: v-brra
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---
# Commerce bot scenario

[!INCLUDE [pre-release-label](includes/pre-release-label-v3.md)]

The [Commerce bot](bot-service-scenario-commerce.md) scenario describes a bot that replaces the traditional e-mail and phone call interactions that people typically have with a hotel's concierge service. The bot takes advantage of Cognitive Services to better process customer requests via text and voice with context gathered from integration with backend services.

![The application bot diagram](~/media/scenarios/bot-service-scenario-commerce-bot.png)

Here is the logic flow of a Commerce bot that functions as a concierge for a hotel:

1. The customer uses the hotel mobile app.
2. Using Azure AD B2C, the user authenticates.
3. Using the custom Application Bot, user requests information. 
4. Cognitive Services helps process the natural language request.
5. Response is reviewed by customer who can refine the question using natural conversation.
6. After the user is happy with the results, the Application Bot updates the customer’s reservation.
7. Application insights gathers runtime telemetery to help development with bot performance and usage.

## Sample bot
The sample Commerce bot is designed around a fictitious hotel concierge service. Written in C#, customers access the Bot once they've authenticated Azure AD B2C with a hotel via the chain's member services mobile app. The chain stores reservations in a SQL Database. A customer can use natural phrase questions like "How much to rent a pool cabana for my stay". The Bot in turn has context about what hotel and the duration of the guest's stay. In addition, Language Understanding (LUIS) Service makes it easy for the bot to get context from even a simple phrase like "pool cabana". The Bot provides the answer and then can offer to book a cabana for the guest, providing choices around the number of days and type of cabana. Once the Bot has all the necessary data, it books the request. The guest can also use their voice to make the same request.

You can download or clone the source code for this sample bot from [Samples for Common Bot Framework Scenarios](https://aka.ms/bot/scenarios).

## Components you'll use
The Commerce bot uses the following components:
-   Azure AD for Authentication
-   Cognitive Services: LUIS
-   Application Insights

### Azure Active Directory (Azure AD)
Azure Active Directory (Azure AD) is Microsoft’s multi-tenant cloud based directory and identity management service. As a Bot developer, Azure AD lets you focus on building your Bot by making it fast and simple to integrate with a world class identity management solution used by millions of organizations around the world. Azure AD supports a B2C connector allowing you to identify individuals using external IDs such as Google, Facebook, or a Microsoft Account. Azure AD removes the responsibility from you having to manage the user's credentials and instead focus your Bot's solution knowing you can correlate the user of the Bot with the correct data exposed by your application.

### Cognitive Services: LUIS
As a member of the Cognitive Services family of technologies, Language Understanding (LUIS) brings the power of machine learning to your apps. Currently, LUIS supports several languages that enables your Bot to understand what a person wants. When integrating with LUIS, you express intent and define the entities your Bot understands. You then teach your Bot to understand those intents and entities by training it with example utterances. You have the ability to tweak your integration using phrase lists and regex features so that your Bot is as fluid as possible for your particular conversation needs.

### Application Insights
Application Insights helps you get actionable insights through application performance management (APM) and instant analytics. Out of the box you get rich performance monitoring, powerful alerting, and easy-to-consume dashboards to help ensure your Bot is available and performing as you expect. You can quickly see if you have a problem, then perform a root cause analysis to find and fix the issue.

## Next steps
Next, learn about the Cortana Skill bot scenario.

> [!div class="nextstepaction"]
> [Cortana Skills bot scenario](bot-service-scenario-cortana-skill.md)

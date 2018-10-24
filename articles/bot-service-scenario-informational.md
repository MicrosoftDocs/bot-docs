---
title: Information bot scenario | Microsoft Docs
description: Explore the Information bot scenario with the Bot Framework.
author: BrianRandell
ms.author: v-brra
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---
# Information Bot Scenario

[!INCLUDE [pre-release-label](includes/pre-release-label-v3.md)]

This Information Bot could answer questions defined in a knowledge set or FAQ using Cognitive Services QnA Maker and answer more open-ended questions by using Azure Search.

Often information is buried in structured data stores like SQL Server that can be easily surfaced via search. Imagine looking up a customer's order status by simple conversational commands. Using Cognitive Services QnA Maker, the user is presented with a set of valid search options like, lookup a customer, review a customer's most recent order, etc. With the QnA format defined the user can easily ask questions that are backed by Azure Search which can look up data stored in a SQL Database.

![The Information bot diagram](~/media/scenarios/bot-service-scenario-informational-bot.png)

Here is the logic flow of an Information bot:

1. The employee starts the Information bot.
2. Azure Active Directory validates the employee's identity.
3. The employee can ask the bot what type of queries are supported.
4. Cognitive Services returns a FAQ bot built with the QnA Maker.
5. The employee defines a valid query.
6. The bot submits the query to Azure Search which returns information about the application data.
7. Application insights gathers runtime telemetery to help development with bot performance and usage.

## Sample bot
The sample Bot, written in C#, runs in Microsoft Azure working with data indexed by Azure Search from a SQL Database instance. The Bot exposes a list of questions that can be asked with information on how to phrase the question (the answer) using Cognitive Services: QnA Maker. The user of the Bot can then type a query that looks up data via Azure Search in a broad or specific area of the database that is indexed. The sample provides a simple database with customers and order information. Application Insights tracks Bot usage and helps you monitor the Bot for exceptions. The Bot is published under as an Azure AD app so that you can restrict who has access to the information.

You can download or clone the source code for this sample bot from [Samples for Common Bot Framework Scenarios](https://aka.ms/bot/scenarios).

## Components you'll use
The Information Bot uses the following components:
-   Azure AD for Authentication
-   Cognitive Services: QnA Maker
-   Azure Search
-   Application Insights

### Azure Active Directory (Azure AD)
Azure Active Directory (Azure AD) is Microsoft’s multi-tenant cloud based directory and identity management service. As a Bot developer, Azure AD lets you focus on building your Bot by making it fast and simple to integrate with a world class identity management solution used by millions of organizations around the world. By defining an Azure AD app, you can control who has access to your Bot and the data it exposes, without implementing your own complex authentication and authorization system.

### Cognitive Services: QnA Maker
Cognitive Services QnA Maker helps you provide an FAQ data source which your users can query from your Bot. When approaching vast amounts of information stored in different systems, it can be useful to help users filter down the information source and set. A single SQL database can have enormous amounts of information that when a free form search is applied brings back too much information. By first using QnA Maker, you can define a road map for your Bot users so they know how to ask intelligent questions that can then be retrieved via Azure Search.

### Azure Search
Azure Search is a cloud search service for apps that let you get your search indices up and running quickly. Running on top of Microsoft Azure, you can easily scale up and down as your usage demands. You can connect search results to business goals with great control over search ranking and surface data hidden in your databases.

### Application Insights
Application Insights helps you get actionable insights through application performance management (APM) and instant analytics. Out of the box you get rich performance monitoring, powerful alerting, and easy-to-consume dashboards to help ensure your Bot is available and performing as you expect. You can quickly see if you have a problem, then perform a root cause analysis to find and fix the issue.

## Next steps
Next, learn about the Internet of Things bot scenario.

> [!div class="nextstepaction"]
> [Internet of Things bot scenario](bot-service-scenario-internet-things.md)

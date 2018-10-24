---
title: Enterprise Productivity bot scenario | Microsoft Docs
description: Explore the Enterprise Productivity bot scenario with the Bot Framework.
author: BrianRandell
ms.author: v-brra
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Enterprise Productivity Bot Scenario

[!INCLUDE [pre-release-label](includes/pre-release-label-v3.md)]

The Enterprise Bot shows how you can increase your productivity by integrating a bot with your Office 365 calendar and other services.

Quickly accessing customer information without having to have a bunch of windows open is what the Enterprise Productivity Bot is all about. Using simple chat commands, a sales rep can look up a customer and check their next appointment via the Graph API and Office 365. From there they can access customer specific information stored in Dynamics CRM such as get a case or create a new one.

![The Enterprise bot diagram](~/media/scenarios/bot-service-scenario-enterprise-bot.png)

Here is the logic flow of an Enterprise Productivity bot:

1. The employee accesses the Enterprise Productivity bot.
2. Azure Active Directory validates the employee's identity.
3. The Enterprise Productivity bot is able to query the employee's Office 365 calendar via the Azure Graph.
4. Using data gathered from the calendar, the bot accesses case information in Dynamics CRM.
5. The information is returned to the employee who can filter down the data without leaving the bot.
6. Application insights gathers runtime telemetery to help development with bot performance and usage.

You can download or clone the source code for this sample bot from [Samples for Common Bot Framework Scenarios](https://aka.ms/bot/scenarios).

## Sample bot
Because Bots are accessible from a variety of channels, you could use it at your desk from a corporate portal or from Skype while on the go--you just need to be authenticated. With Azure AD integration your Enterprise Productivity Bot knows that that if you're able to access it, you've been authenticated by Azure AD. From there you can ask the bot to check when your next appointment is with a particular customer. The Bot gets this information by querying Office 365 via the Graph API. Then, if there's an appointment in the next seven days, the Bot queries CRM looking for any recent cases for the customer. The Bot responds with either no cases found or the number of open and closed cases. From there you can ask the Bot to list out the cases by type and drill into individual cases.

## Components you'll use
The Enterprise Productivity Bot uses the following components:
-   Azure AD for Authentication
-   Graph API to Office 365
-   Dynamics CRM
-   Application Insights

### Azure Active Directory (Azure AD)
Azure Active Directory (Azure AD) is Microsoft’s multi-tenant cloud based directory and identity management service. As a Bot developer, Azure AD lets you focus on building your Bot by making it fast and simple to integrate with a world class identity management solution used by millions of organizations around the world. By defining an Azure AD app, you can control who has access to your Bot and the data it exposes, without implementing your own complex authentication and authorization system.

### Graph API to Office 365
The Microsoft Graph exposes multiple APIs from Office 365 and other Microsoft cloud services through a single endpoint at https://graph.microsoft.com. Microsoft Graph makes it easier for you and Bot to execute queries. The API exposes data from  multiple Microsoft cloud services, including Exchange Online as part of Office 365, Azure Active Directory, SharePoint, and more. You can use the API to navigate between entities and relationships. You can use the API from your Bots using the SDK or REST endpoints as well as from your other apps with native support Android, iOS, Ruby, UWP, Xamarin and more.

### Dynamics CRM
Dynamics CRM is a customer engagement platform. Using Bots and CRM's APIs, you can build rich interactive Bots that can access the rich data stored in CRM. The power of Dynamics CRM is available to your Bot to create cases, check on status, knowledge management searches and more.

### Application Insights
Application Insights helps you get actionable insights through application performance management (APM) and instant analytics. Out of the box you get rich performance monitoring, powerful alerting, and easy-to-consume dashboards to help ensure your Bot is available and performing as you expect. You can quickly see if you have a problem, then perform a root cause analysis to find and fix the issue.

## Next steps
Next, learn about the Information bot scenario.

> [!div class="nextstepaction"]
> [Information bot scenario](bot-service-scenario-informational.md)

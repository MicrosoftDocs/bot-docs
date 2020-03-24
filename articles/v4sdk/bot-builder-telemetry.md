---
title: Add telemetry to your bot - Bot Service
description: Learn how to integrate your bot with the new telemetry features.
keywords: telemetry, appinsights, monitor bot
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 07/17/2019
monikerRange: 'azure-bot-service-4.0'
---

# Add telemetry to your bot

[!INCLUDE[applies-to](../includes/applies-to.md)]


Telemetry logging was added to version 4.2 of the Bot Framework SDK.  This enables bot applications to send event data to telemetry services such as [Application Insights](https://aka.ms/appinsights-overview). Telemetry offers insights into your bot by showing which features are used the most, detects unwanted behavior and offers visibility into availability, performance, and usage.

***Note: In version 4.6, the standard method for implementing telemetry into a bot has been updated in order to ensure telemetry is logged correctly when using a custom adapter. This article has been updated to show the updated method. The changes are backwards compatible and bots using the previous method will continue to log telemetry correctly.***


In this article you will learn how to implement telemetry into your bot using Application Insights. It will cover:

* The code required to wire up telemetry in your bot and connect to Application Insights.

* Enabling telemetry in your bot's [Dialogs](bot-builder-concept-dialog.md).

* Enabling telemetry to capture usage data from other services like [LUIS](bot-builder-howto-v4-luis.md) and [QnA Maker](bot-builder-howto-qna.md).

* Visualizing your telemetry data in Application Insights.

<!-- Prerequisites-->
## Prerequisites

# [C#](#tab/csharp)

* The [CoreBot sample code](https://aka.ms/cs-core-sample)
* The [Application Insights sample code](https://aka.ms/csharp-corebot-app-insights-sample)
* A subscription to [Microsoft Azure](https://portal.azure.com/)
* An [Application Insights key](../bot-service-resources-app-insights-keys.md)
* Familiarity with [Application Insights](https://aka.ms/appinsights-overview)
* [git](https://git-scm.com/)

> [!NOTE]
> The [Application Insights sample code](https://aka.ms/csharp-corebot-app-insights-sample) was built on top of the [CoreBot sample code](https://aka.ms/cs-core-sample). This article will step you through modifying the CoreBot sample code to incorporate telemetry. If you are following along in Visual Studio you will have the Application Insights sample code by the time you are finished.

# [JavaScript](#tab/javascript)

- The [CoreBot sample code](https://aka.ms/js-core-sample)
- The [Application Insights sample code](https://aka.ms/js-corebot-app-insights-sample)
- A subscription to [Microsoft Azure](https://portal.azure.com/)
- An [Application Insights key](../bot-service-resources-app-insights-keys.md)
- Familiarity with [Application Insights](https://aka.ms/appinsights-overview)
- [Visual Studio Code](https://www.visualstudio.com/downloads)
- [Node.js](https://nodejs.org/) version 10.14 or higher. Use command `node --version` to determine the version of node you have installed. 
- [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme)

> [!NOTE]
> The [Application Insights sample code](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/21.corebot-app-insights) was built on top of the [CoreBot sample code](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/13.core-bot). This article will step you through modifying the CoreBot sample code to incorporate telemetry. If you are following along in Visual Studio you will have the Application Insights sample code by the time you are finished.


<!--

    # [Python](#tab/python)


    * The [CoreBot sample code](https://aka.ms/py-core-sample)
    * The [Application Insights sample code](https://aka.ms/py-corebot-app-insights-sample)
    * A subscription to [Microsoft Azure](https://portal.azure.com/)
    * An [Application Insights key](../bot-service-resources-app-insights-keys.md)
    * Familiarity with [Application Insights](https://aka.ms/appinsights-overview)

    > [!NOTE]
    > The [Application Insights sample code](https://aka.ms/py-corebot-app-insights-sample) was built on top of the [CoreBot sample code](https://aka.ms/py-core-sample). This article will step you through modifying the CoreBot sample code to incorporate telemetry. If you are following along in Visual Studio you will have the Application Insights sample code by the time you are finished.
-->

---


## Wiring up telemetry in your bot
<!-- Main article on implementing telemetry--->
# [C#](#tab/csharp)

[!INCLUDE [dotnet telemetry section](../includes/telemetry-dotnet-main.md)]

# [JavaScript](#tab/javascript)

[!INCLUDE [javascript telemetry section](../includes/telemetry-javascript-main.md)]

<!--
    # [Python](#tab/python)
    [!INCLUDE [python telemetry section](../includes/telemetry-python.md)]
-->

---


## Enabling / disabling activity event and personal information logging

# [C#](#tab/csharp)

[!INCLUDE [dotnet telemetry-Enabling disabling activity section](../includes/telemetry-dotnet-enabling-disabling-activity-event-personal-information-logging.md)]

# [JavaScript](#tab/javascript)

[!INCLUDE [javascript telemetry section](../includes/telemetry-javascript-enabling-disabling-activity-event-personal-information-logging.md)]

<!--
    # [Python](#tab/python)

    [!INCLUDE [python telemetry section](../includes/telemetry-python-enabling-disabling-activity-event-personal-information-logging.md)]
-->

---

## Enabling telemetry to capture usage data from other services like LUIS and QnA Maker

# [C#](#tab/csharp)

[!INCLUDE [dotnet telemetry-luis section](../includes/telemetry-dotnet-luis.md)]

# [JavaScript](#tab/javascript)

[!INCLUDE [javascript telemetry-luis section](../includes/telemetry-javascript-luis.md)]

<!--
    # [Python](#tab/python)

    [!INCLUDE [python telemetry-luis section](../includes/telemetry-python-luis.md)]
-->

---


## Visualizing your telemetry data in Application Insights
Application Insights monitors the availability, performance, and usage of your bot application whether it's hosted in the cloud or on-premises. It leverages the powerful data analysis platform in Azure Monitor to provide you with deep insights into your application's operations and diagnose errors without waiting for a user to report them. There are a few ways to see the telemetry data collected by Application Insights, two of the primary ways are through queries and the dashboard. 

### Querying your telemetry data in Application Insights using Kusto Queries
Use this section as a starting point to learn how to use log queries in Application Insights. It demonstrates two useful queries and provides links to other documentation with additional information.

To query your data

1. Go to the [Azure portal](https://portal.azure.com)
2. Navigate to your Application Insights. Easiest way to do so is click on **Monitor > Applications** and find it there. 
3. Once in your Application Insights, you can click on _Logs (Analytics)_ on the navigation bar.

    ![Logs (Analytics)](media/AppInsights-LogView.png)

4. This will bring up the Query window.  Enter the following query and select _Run_:

    ```sql
    customEvents
    | where name=="WaterfallStart"
    | extend DialogId = customDimensions['DialogId']
    | extend InstanceId = tostring(customDimensions['InstanceId'])
    | join kind=leftouter (customEvents | where name=="WaterfallComplete" | extend InstanceId = tostring(customDimensions['InstanceId'])) on InstanceId    
    | summarize starts=countif(name=='WaterfallStart'), completes=countif(name1=='WaterfallComplete') by bin(timestamp, 1d), tostring(DialogId)
    | project Percentage=max_of(0.0, completes * 1.0 / starts), timestamp, tostring(DialogId) 
    | render timechart
    ```
5. This will return the percentage of waterfall dialogs that run to completion.

    ![Logs (Analytics)](media/AppInsights-Query-PercentCompleteDialog.png)


> [!TIP]
> You can pin any query to your Application Insights dashboard   by selecting the button on the top right of the **Logs (Analytics)** blade. Just select the dashboard you want it pinned to, and it will be available next time you visit that dashboard.


## The Application Insights dashboard

Anytime you create an Application Insights resource in Azure, a new dashboard will automatically be created and associated with it.  You can see that dashboard by selecting the button at the top of your Application Insights blade, labeled **Application Dashboard**. 

![Application Dashboard Link](media/Application-Dashboard-Link.png)


Alternatively, to view the data, go to the Azure portal. Click **Dashboard** on the left, then select the dashboard you want from the drop-down.

There, you'll see some default information about your bot performance and any additional queries that you've pinned to your dashboard.



## Additional Information

* [Add telemetry to your QnAMaker bot](bot-builder-telemetry-qnamaker.md)

* [What is Application Insights?](https://aka.ms/appinsights-overview)

* [Using Search in Application Insights](https://aka.ms/search-in-application-insights)

* [Create custom KPI dashboards using Azure Application Insights](https://aka.ms/custom-kpi-dashboards-application-insights)
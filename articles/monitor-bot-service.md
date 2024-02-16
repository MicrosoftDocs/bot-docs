---
title: Monitor Azure AI Bot Service
description: Start here to learn how to monitor Azure AI Bot Service.
ms.date: 02/15/2024
ms.custom: horz-monitor
ms.topic: conceptual
author: iaanw
ms.author: iawilt
ms.service: bot-service
---

<!-- 
IMPORTANT 
To make this template easier to use, first:
1. Search and replace Bot Service with the official name of your service.
2. Search and replace bot-service with the service name to use in GitHub filenames.-->

<!-- VERSION 3.0 2024_01_07
For background about this template, see https://review.learn.microsoft.com/en-us/help/contribute/contribute-monitoring?branch=main -->

<!-- Most services can use the following sections unchanged. The sections use #included text you don't have to maintain, which changes when Azure Monitor functionality changes. Add info into the designated service-specific places if necessary. Remove #includes or template content that aren't relevant to your service.

At a minimum your service should have the following two articles:

1. The primary monitoring article (based on this template)
   - Title: "Monitor Bot Service"
   - TOC title: "Monitor"
   - Filename: "monitor-bot-service.md"

2. A reference article that lists all the metrics and logs for your service (based on the template data-reference-template.md).
   - Title: "Bot Service monitoring data reference"
   - TOC title: "Monitoring data reference"
   - Filename: "monitor-bot-service-reference.md".
-->

# Monitor Azure AI Bot Service

<!-- Intro. Required. -->
[!INCLUDE [horz-monitor-intro](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-intro.md)]

<!-- ## Insights. Optional section. If your service has insights, add the following include and information. -->
[!INCLUDE [horz-monitor-insights](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-insights.md)]
<!-- Insights service-specific information. Add brief information about what your Azure Monitor insights provide here. You can refer to another article that gives details or add a screenshot. -->
Telemetry logging enables bot applications to send event data to telemetry services such as Application Insights. Telemetry offers insights into your bot by showing which features are used the most, detects unwanted behavior, and offers visibility into availability, performance, and usage. For more information about implementing telemetry in your bot using Application Insights, see [Add telemetry to your bot](v4sdk/bot-builder-telemetry.md).

<!-- ## Resource types. Required section. -->
[!INCLUDE [horz-monitor-resource-types](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-resource-types.md)]
For more information about the resource types for Azure AI Bot Service, see [Bot Service monitoring data reference](monitor-bot-service-reference.md).

<!-- ## Data storage. Required section. Optionally, add service-specific information about storing your monitoring data after the include. -->
[!INCLUDE [horz-monitor-data-storage](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-data-storage.md)]
<!-- Add service-specific information about storing monitoring data here, if applicable. For example, SQL Server stores other monitoring data in its own databases. -->

<!-- METRICS SECTION START ------------------------------------->

<!-- ## Platform metrics. Required section.
  - If your service doesn't collect platform metrics, use the following include: [!INCLUDE [horz-monitor-no-platform-metrics](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-no-platform-metrics.md)]
  - If your service collects platform metrics, add the following include, statement, and service-specific information as appropriate. -->
[!INCLUDE [horz-monitor-platform-metrics](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-platform-metrics.md)]
For a list of available metrics for Bot Service, see [Bot Service monitoring data reference](monitor-bot-service-reference.md#metrics).
<!-- Platform metrics service-specific information. Add service-specific information about your platform metrics here.-->

<!-- ## Prometheus/container metrics. Optional. If your service uses containers/Prometheus metrics, add the following include and information. 
[!INCLUDE [horz-monitor-container-metrics](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-container-metrics.md)]
<!-- Add service-specific information about your container/Prometheus metrics here.-->

<!-- ## System metrics. Optional. If your service uses system-imported metrics, add the following include and information. 
[!INCLUDE [horz-monitor-system-metrics](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-system-metrics.md)]
<!-- Add service-specific information about your system-imported metrics here.-->

<!-- ## Custom metrics. Optional. If your service uses custom imported metrics, add the following include and information. 
[!INCLUDE [horz-monitor-custom-metrics](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-custom-metrics.md)]
<!-- Custom imported service-specific information. Add service-specific information about your custom imported metrics here.-->

<!-- ## Non-Azure Monitor metrics. Optional. If your service uses any non-Azure Monitor based metrics, add the following include and information. 
[!INCLUDE [horz-monitor-custom-metrics](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-non-monitor-metrics.md)]
<!-- Non-Monitor metrics service-specific information. Add service-specific information about your non-Azure Monitor metrics here.-->

<!-- METRICS SECTION END ------------------------------------->

<!-- LOGS SECTION START -------------------------------------->

<!-- ## Resource logs. Required section.
  - If your service doesn't collect resource logs, use the following include [!INCLUDE [horz-monitor-no-resource-logs](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-no-resource-logs.md)]
  - If your service collects resource logs, add the following include, statement, and service-specific information as appropriate. -->
[!INCLUDE [horz-monitor-resource-logs](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-resource-logs.md)]
For the available resource log categories, their associated Log Analytics tables, and the logs schemas for Bot Service, see [Bot Service monitoring data reference](monitor-bot-service-reference.md#resource-logs).
<!-- Resource logs service-specific information. Add service-specific information about your resource logs here.
NOTE: Azure Monitor already has general information on how to configure and route resource logs. See https://learn.microsoft.com/azure/azure-monitor/platform/diagnostic-settings. Ideally, don't repeat that information here. You can provide a single screenshot of the diagnostic settings portal experience if you want. -->

<!-- ## Activity log. Required section. Optionally, add service-specific information about your activity log after the include. -->
[!INCLUDE [horz-monitor-activity-log](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-activity-log.md)]
<!-- Activity log service-specific information. Add service-specific information about your activity log here. -->

<!-- ## Imported logs. Optional section. If your service uses imported logs, add the following include and information. 
[!INCLUDE [horz-monitor-imported-logs](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-imported-logs.md)]
<!-- Add service-specific information about your imported logs here. -->

<!-- ## Other logs. Optional section.
If your service has other logs that aren't resource logs or in the activity log, add information that states what they are and what they cover here. You can describe how to route them in a later section. -->

<!-- LOGS SECTION END ------------------------------------->

<!-- ANALYSIS SECTION START -------------------------------------->

<!-- ## Analyze data. Required section. -->
[!INCLUDE [horz-monitor-analyze-data](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-analyze-data.md)]

<!-- ### External tools. Required section. -->
[!INCLUDE [horz-monitor-external-tools](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-external-tools.md)]

<!-- ### Sample Kusto queries. Required section. If you have sample Kusto queries for your service, add them after the include. -->
[!INCLUDE [horz-monitor-kusto-queries](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-kusto-queries.md)]
<!-- Add sample Kusto queries for your service here. -->
You can use the following queries to help monitor your bot resource.

For more information and a collection of Kusto query examples for analyzing bot behavior, see [Analyze your bot's telemetry data](v4sdk/bot-builder-telemetry-analytics-queries.md).

- Logs of Clients to Direct Line channel requests.

  ```kusto
  ABSBotRequests
  | where OperationName contains "ClientToDirectLine"
  | sort by TimeGenerated desc
  | limit 100
  ```

- Logs of requests from the Bot to channels.

  ```kusto
  ABSBotRequests
  | where OperationName contains "BotToChannel"
  | sort by TimeGenerated desc
  | limit 100
  ```

- Logs of requests from Channels to the bot.

  ```kusto
  ABSBotRequests
  | where OperationName contains "ChannelToBot"
  | sort by TimeGenerated desc
  | limit 100
  ```

- Logs of requests from Facebook to Azure AI Bot Service Facebook Channel.

  ```kusto
  ABSBotRequests
  | where OperationName contains "FacebookToChannel"
  | sort by TimeGenerated desc
  ```

- Logs of requests from Azure AI Bot Service Facebook Channel to Facebook API.

  ```kusto
  ABSBotRequests
  | where OperationName contains "ChannelToFacebookAPI"
  | sort by TimeGenerated desc
  ```

- Logs of requests to send activities from a client to Direct Line channel.

  ```kusto
  ABSBotRequests
  | where OperationName == 'SendAnActivity:ClientToDirectLine'
  | sort by TimeGenerated desc
  ```

- List of logs of unsuccessful requests.

  ```kusto
  ABSBotRequests
  | where ResultCode < 200 or ResultCode >= 300
  | sort by TimeGenerated desc
  ```

- Line Chart showing Direct Line channel requests response codes.

  ```kusto
  ABSBotRequests
  | where Channel == "directline"
  | summarize Number_Of_Requests = count() by tostring(ResultCode), bin(TimeGenerated, 5m)
  | render timechart
  ```

- Line Chart showing requests response times/duration per operation.

  ```kusto
  ABSBotRequests
  | summarize DurationMs = avg(DurationMs)  by bin(TimeGenerated, 5m), OperationName
  | render timechart
  ```

- Line Chart showing requests response status codes.

  ```kusto
  ABSBotRequests
  | summarize Number_Of_Requests = count() by tostring(ResultCode), bin(TimeGenerated, 5m)
  | render timechart
  ```

- Pie Chart showing requests response status codes.

  ```kusto
  ABSBotRequests
  | summarize count() by tostring(ResultCode)
  | render piechart
  ```

- Pie Chart showing requests operations.

  ```kusto
  ABSBotRequests
  | summarize count() by tostring(OperationName)
  | render piechart
  ```

<!-- ### Bot Service service-specific analytics. Optional section.
Add short information or links to specific articles that outline how to analyze data for your service. -->

<!-- ANALYSIS SECTION END ------------------------------------->

<!-- ALERTS SECTION START -------------------------------------->

<!-- ## Alerts. Required section. -->
[!INCLUDE [horz-monitor-alerts](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-alerts.md)]

<!-- ONLY if applications run on your service that work with Application Insights, add the following include. -->
[!INCLUDE [horz-monitor-insights-alerts](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-insights-alerts.md)]

<!-- ### Bot Service alert rules. Required section.
**MUST HAVE** service-specific alert rules. Include useful alerts on metrics, logs, log conditions, or activity log.
Fill in the following table with metric and log alerts that would be valuable for your service. Change the format as necessary for readability. You can instead link to an article that discusses your common alerts in detail.
Ask your PMs if you don't know. This information is the BIGGEST request we get in Azure Monitor, so don't avoid it long term. People don't know what to monitor for best results. Be prescriptive. -->

### Bot Service alert rules
The following table lists common and recommended alert rules for Bot Service.

| Alert type | Condition | Description  |
|:---|:---|:---|
| Metrics | Request Latency milliseconds exceed threshold | Signal source: Platform metrics |
| Metrics | Requests Traffic count is greater than threshold % | Signal source: Platform metrics |
| Activity log | A Bot Service is deleted | Signal source: Administrative |

<!-- ### Advisor recommendations. Required section. -->
[!INCLUDE [horz-monitor-advisor-recommendations](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-advisor-recommendations.md)]
<!-- Add any service-specific advisor recommendations or screenshots here. -->

<!-- ALERTS SECTION END -------------------------------------->

## Related content
<!-- You can change the wording and add more links if useful. -->

- For more information about implementing telemetry in your bot using Application Insights, see [Add telemetry to your bot](v4sdk/bot-builder-telemetry.md).
- For a collection of Kusto query examples for analyzing bot behavior, see [Analyze your bot's telemetry data](v4sdk/bot-builder-telemetry-analytics-queries.md).
- For a reference of the metrics, logs, and other important values created for Bot Service, see [Bot Service monitoring data reference](monitor-bot-service-reference.md).
- For general details on monitoring Azure resources, see [Monitoring Azure resources with Azure Monitor](/azure/azure-monitor/essentials/monitor-azure-resource).

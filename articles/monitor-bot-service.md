---
title: Monitor Azure AI Bot Service
description: Start here to learn how to monitor Azure AI Bot Service.
ms.date: 02/27/2024
ms.custom:
  - horz-monitor
  - evergreen
ms.topic: conceptual
author: iaanw
ms.author: iawilt
ms.service: azure-ai-bot-service
---

# Monitor Azure AI Bot Service

[!INCLUDE [horz-monitor-intro](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-intro.md)]

[!INCLUDE [horz-monitor-insights](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-insights.md)]

Telemetry logging enables bot applications to send event data to telemetry services such as Application Insights. Telemetry offers insights into your bot by showing which features are used the most, detects unwanted behavior, and offers visibility into availability, performance, and usage. For more information about implementing telemetry in your bot using Application Insights, see [Add telemetry to your bot](v4sdk/bot-builder-telemetry.md).

[!INCLUDE [horz-monitor-resource-types](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-resource-types.md)]
For more information about the resource types for Azure AI Bot Service, see [Bot Service monitoring data reference](monitor-bot-service-reference.md).

[!INCLUDE [horz-monitor-data-storage](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-data-storage.md)]

<!-- METRICS SECTION START ------------------------------------->

[!INCLUDE [horz-monitor-platform-metrics](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-platform-metrics.md)]
For a list of available metrics for Bot Service, see [Bot Service monitoring data reference](monitor-bot-service-reference.md#metrics).

[!INCLUDE [horz-monitor-resource-logs](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-resource-logs.md)]
For the available resource log categories, their associated Log Analytics tables, and the logs schemas for Bot Service, see [Bot Service monitoring data reference](monitor-bot-service-reference.md#resource-logs).

[!INCLUDE [horz-monitor-activity-log](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-activity-log.md)]

[!INCLUDE [horz-monitor-analyze-data](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-analyze-data.md)]

[!INCLUDE [horz-monitor-external-tools](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-external-tools.md)]

[!INCLUDE [horz-monitor-kusto-queries](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-kusto-queries.md)]

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

[!INCLUDE [horz-monitor-alerts](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-alerts.md)]

[!INCLUDE [horz-monitor-insights-alerts](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-insights-alerts.md)]

### Bot Service alert rules
The following table lists common and recommended alert rules for Bot Service.

| Alert type | Condition | Description  |
|:---|:---|:---|
| Metrics | Request Latency milliseconds exceed threshold | Signal source: Platform metrics |
| Metrics | Requests Traffic count is greater than threshold % | Signal source: Platform metrics |
| Activity log | A Bot Service is deleted | Signal source: Administrative |

[!INCLUDE [horz-monitor-advisor-recommendations](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-advisor-recommendations.md)]

## Related content

- For more information about implementing telemetry in your bot using Application Insights, see [Add telemetry to your bot](v4sdk/bot-builder-telemetry.md).
- For a collection of Kusto query examples for analyzing bot behavior, see [Analyze your bot's telemetry data](v4sdk/bot-builder-telemetry-analytics-queries.md).
- For a reference of the metrics, logs, and other important values created for Bot Service, see [Bot Service monitoring data reference](monitor-bot-service-reference.md).
- For general details on monitoring Azure resources, see [Monitoring Azure resources with Azure Monitor](/azure/azure-monitor/essentials/monitor-azure-resource).

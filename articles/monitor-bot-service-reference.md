---
title: Monitoring Azure Bot Service data reference
description: Important reference material needed when you monitor Azure Bot Service.
ms.service: bot-service
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: reference
ms.date: 01/04/2023
ms.custom: subject-monitoring
---

# Monitoring Azure Bot Service data reference

See [Monitoring Azure Bot Service](monitor-bot-service.md) for details on collecting and analyzing monitoring data for Azure Bot Service.

## Metrics

This section lists all the automatically collected platform metrics collected for Azure Bot Service.  

| Metric Type | Resource Provider |
|--|--|
| Azure Bot Service | [Microsoft.BotService/botservices](/azure/azure-monitor/essentials/metrics-supported#microsoftbotservicebotservices) |

For more information, see a list of [all platform metrics supported in Azure Monitor](/azure/azure-monitor/platform/metrics-supported).

## Metric Dimensions

For more information on what metric dimensions are, see [Multi-dimensional metrics](/azure/azure-monitor/platform/data-platform-metrics#multi-dimensional-metrics).

For the dimensions associated with Azure Bot Service metrics, see [Microsoft.BotService/botservices](/azure/azure-monitor/essentials/metrics-supported#microsoftbotservicebotservices).

## Resource logs

This section lists the types of resource logs you can collect for Azure Bot Service.

| Resource Log Type | Resource Provider |
|--|--|
| Azure Bot Service | [Microsoft.botservice/botservices](/azure/azure-monitor/essentials/resource-logs-categories#microsoftbotservicebotservices) |

For reference, see a list of [all resource logs category types supported in Azure Monitor](/azure/azure-monitor/platform/resource-logs-schema).

## Azure Monitor Logs tables

For the Azure Monitor Logs Kusto tables relevant to Azure Bot Service and available for query by Log Analytics, see [Azure Bot Service](/azure/azure-monitor/reference/tables/tables-resourcetype#bot-services).

## Activity log

For the operations that Azure Bot Service may record in the Activity log, see [Microsoft.BotService/botservices](/azure/role-based-access-control/resource-provider-operations#microsoftbotservice) and [all the possible resource provider operations in the activity log](/azure/role-based-access-control/resource-provider-operations).  

For more information on the schema of Activity Log entries, see [Activity  Log schema](/azure/azure-monitor/essentials/activity-log-schema).

## See Also

- See [Monitoring Azure Azure Bot Service](monitor-bot-service.md) for a description of monitoring Azure Azure Bot Service.
- See [Monitor Azure resources with Azure Monitor](/azure/azure-monitor/essentials/monitor-azure-resource) for details on monitoring Azure resources.

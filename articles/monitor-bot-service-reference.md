---
title: Monitoring data reference for Azure AI Bot Service
description: This article contains important reference material you need when you monitor Azure AI Bot Service.
ms.date: 02/27/2024
ms.custom: horz-monitor
ms.topic: reference
author: iaanw
ms.author: iawilt
ms.service: bot-service
---

# Azure AI Bot Service monitoring data reference

[!INCLUDE [horz-monitor-ref-intro](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-intro.md)]

See [Monitor Azure AI Bot Service](monitor-bot-service.md) for details on the data you can collect for Azure AI Bot Service and how to use it.

[!INCLUDE [horz-monitor-ref-metrics-intro](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-intro.md)]

### Supported metrics for microsoft.botservice/botservices
The following table lists the metrics available for the microsoft.botservice/botservices resource type.
[!INCLUDE [horz-monitor-ref-metrics-tableheader](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-tableheader.md)]
[!INCLUDE [microsoft.botservice/botservices](~/../azure-reference-other-repo/azure-monitor-ref/supported-metrics/includes/microsoft-botservice-botservices-metrics-include.md)]

### Supported metrics for Microsoft.BotService/botServices/channels
The following table lists the metrics available for the Microsoft.BotService/botServices/channels resource type.
[!INCLUDE [horz-monitor-ref-metrics-tableheader](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-tableheader.md)]
[!INCLUDE [Microsoft.BotService/botServices/channels](~/../azure-reference-other-repo/azure-monitor-ref/supported-metrics/includes/microsoft-botservice-botservices-channels-metrics-include.md)]

### Supported metrics for Microsoft.BotService/botServices/connections
The following table lists the metrics available for the Microsoft.BotService/botServices/connections resource type.
[!INCLUDE [horz-monitor-ref-metrics-tableheader](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-tableheader.md)]
[!INCLUDE [Microsoft.BotService/botServices/connections](~/../azure-reference-other-repo/azure-monitor-ref/supported-metrics/includes/microsoft-botservice-botservices-connections-metrics-include.md)]

### Supported metrics for Microsoft.BotService/checknameavailability
The following table lists the metrics available for the Microsoft.BotService/checknameavailability resource type.
[!INCLUDE [horz-monitor-ref-metrics-tableheader](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-tableheader.md)]
[!INCLUDE [Microsoft.BotService/checknameavailability](~/../azure-reference-other-repo/azure-monitor-ref/supported-metrics/includes/microsoft-botservice-checknameavailability-metrics-include.md)]

### Supported metrics for Microsoft.BotService/hostsettings
The following table lists the metrics available for the Microsoft.BotService/hostsettings resource type.
[!INCLUDE [horz-monitor-ref-metrics-tableheader](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-tableheader.md)]
[!INCLUDE [microsoft.botservice/botservices](~/../azure-reference-other-repo/azure-monitor-ref/supported-metrics/includes/microsoft-botservice-hostsettings-metrics-include.md)]

### Supported metrics for Microsoft.BotService/listauthserviceproviders
The following table lists the metrics available for the Microsoft.BotService/listauthserviceproviders resource type.
[!INCLUDE [horz-monitor-ref-metrics-tableheader](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-tableheader.md)]
[!INCLUDE [Microsoft.BotService/listauthserviceproviders](~/../azure-reference-other-repo/azure-monitor-ref/supported-metrics/includes/microsoft-botservice-listauthserviceproviders-metrics-include.md)]

### Supported metrics for Microsoft.BotService/listqnamakerendpointkeys
The following table lists the metrics available for the Microsoft.BotService/listqnamakerendpointkeys resource type.
[!INCLUDE [horz-monitor-ref-metrics-tableheader](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-tableheader.md)]
[!INCLUDE [Microsoft.BotService/listqnamakerendpointkeys](~/../azure-reference-other-repo/azure-monitor-ref/supported-metrics/includes/microsoft-botservice-listqnamakerendpointkeys-metrics-include.md)]

[!INCLUDE [horz-monitor-ref-metrics-dimensions-intro](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-dimensions-intro.md)]
[!INCLUDE [horz-monitor-ref-metrics-dimensions](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-metrics-dimensions.md)]

The **Dimensions** columns in the preceding metrics tables list the dimensions associated with Bot Service metrics.

[!INCLUDE [horz-monitor-ref-resource-logs](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-resource-logs.md)]

### Supported resource logs for microsoft.botservice/botservices
[!INCLUDE [Microsoft.Storage/storageAccounts/blobServices](~/../azure-reference-other-repo/azure-monitor-ref/supported-logs/includes/microsoft-botservice-botservices-logs-include.md)]

[!INCLUDE [horz-monitor-ref-logs-tables](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-logs-tables.md)]

### Bot Services
Microsoft.BotService/botServices
- [AzureActivity](/azure/azure-monitor/reference/tables/azureactivity#columns)
- [ABSBotRequests](/azure/azure-monitor/reference/tables/absbotrequests#columns)

[!INCLUDE [horz-monitor-ref-activity-log](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-activity-log.md)]

- [Microsoft.BotService resource provider operations](/azure/role-based-access-control/resource-provider-operations#microsoftbotservice)

[!INCLUDE [horz-monitor-ref-other-schemas](~/../articles/reusable-content/ce-skilling/azure/includes/azure-monitor/horizontals/horz-monitor-ref-other-schemas.md)]

For a listing of the most common fields that bots log telemetry data into, see [Schema of bot analytics instrumentation](v4sdk/bot-builder-telemetry-analytics-queries.md#schema-of-bot-analytics-instrumentation).

## Related content

- See [Monitor Bot Service](monitor-bot-service.md) for a description of monitoring Bot Service.
- See [Monitor Azure resources with Azure Monitor](/azure/azure-monitor/essentials/monitor-azure-resource) for details on monitoring Azure resources.

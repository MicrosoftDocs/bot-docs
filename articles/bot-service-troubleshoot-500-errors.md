---
title: Troubleshoot HTTP 500 Internal Service Errors with Azure Bot Service
description: Learn how to troubleshoot HTTP 500 errors. See how to enable Application Insights, retrieve information on exceptions, and check logs and configuration files.
keywords: troubleshoot, HTTP 500, problems.
author: jonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 09/01/2021
ms.custom: abs-meta-21q1
---

# Troubleshoot HTTP 500 Internal Service Errors

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

<!-- Attention writers!!
     1 - When you create a new FAQ, please add the related link to the proper section in bot-service-troubleshoot-index.md.-->

The first step in troubleshooting 500 errors is enabling Application Insights.

For AppInsights samples, see the luis-with-appinsights [C# sample](https://github.com/Microsoft/BotBuilder-Samples/blob/main/samples/csharp_dotnetcore/21.corebot-app-insights/) and [JS sample](https://github.com/Microsoft/BotBuilder-Samples/blob/main/samples/javascript_nodejs/21.corebot-app-insights).

 <!-- qna-with-appinsights ([C# sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/11.qnamaker) / [JS sample](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/javascript_nodejs/11.qnamaker)) samples demonstrate bots that support Azure Application Insights.-->

See [conversation analytics telemetry](./v4sdk/bot-builder-telemetry.md) for information about how to add Application Insights to an existing bot.

## Enable Application Insights for ASP.NET

For basic Application Insights support, see how to [set up Application Insights for your ASP.NET website](/azure/application-insights/app-insights-asp-net). The Bot Framework (starting with v4.2) provides an additional level of Application Insights telemetry, but it is not required for diagnosing HTTP 500 errors.

## Enable Application Insights for Node.js

For basic Application Insights support, see how to [monitor your Node.js services and apps with Application Insights](/azure/azure-monitor/learn/nodejs-quick-start). The Bot Framework (starting with v4.2) provides an additional level of Application Insights telemetry, but it is not required for diagnosing HTTP 500 errors.

## Query for exceptions

The easiest method of analyzing HTTP status code 500 errors is to begin with exceptions.

The following queries will tell you the most recent exceptions:

```sql
exceptions
| order by timestamp desc
| project timestamp, operation_Id, appName
```

From the first query, select a few of the operation IDs and look for more information:

```sql
let my_operation_id = "d298f1385197fd438b520e617d58f4fb";
let union_all = () {
    union
    (traces | where operation_Id == my_operation_id),
    (customEvents | where operation_Id == my_operation_id),
    (requests | where operation_Id == my_operation_id),
    (dependencies | where operation_Id  == my_operation_id),
    (exceptions | where operation_Id == my_operation_id)
};

union_all
    | order by timestamp desc
```

If you have only `exceptions`, analyze the details and see if they correspond to lines in your code. If you only see exceptions coming from the Channel Connector (`Microsoft.Bot.ChannelConnector`) then see [No Application Insights events](#no-application-insights-events) to ensure that Application Insights is set up correctly and your code is logging events.

## No Application Insights events

If you are receiving 500 errors and there are no further events within Application Insights from your bot, check the following:

### Ensure the bot runs locally

Make sure your bot runs locally first with the Emulator.

### Ensure configuration files are being copied (.NET only)

Make sure your `appsettings.json` and any other configuration files are being packaged correctly during the deployment process.

#### Application assemblies

Ensure the Application Insights assemblies are being packaged correctly during the deployment process.

- Microsoft.ApplicationInsights
- Microsoft.ApplicationInsights.TraceListener
- Microsoft.AI.Web
- Microsoft.AI.WebServer
- Microsoft.AI.ServeTelemetryChannel
- Microsoft.AI.PerfCounterCollector
- Microsoft.AI.DependencyCollector
- Microsoft.AI.Agent.Intercept

Make sure your `appsettings.json` and any other configuration files are being packaged correctly during the deployment process.

#### appsettings.json

Within your `appsettings.json` file ensure the Instrumentation Key is set.

## [ASP.NET Web API](#tab/dotnetwebapi)

```json
{
    "Logging": {
        "IncludeScopes": false,
        "LogLevel": {
            "Default": "Debug",
            "System": "Information",
            "Microsoft": "Information"
        },
        "Console": {
            "IncludeScopes": "true"
        }
    }
}
```

## [ASP.NET Core](#tab/dotnetcore)

```json
{
    "ApplicationInsights": {
        "InstrumentationKey": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
    }
}
```

---

### Verify config file

Ensure there's an Application Insights key included in your config file.

```json
{
    "ApplicationInsights": {
        "type": "appInsights",
        "tenantId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
        "subscriptionId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
        "resourceGroup": "my resource group",
        "name": "my appinsights name",
        "serviceName": "my service name",
        "instrumentationKey": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
        "applicationId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
        "apiKeys": {},
        "id": ""
    }
},
```

### Check logs

Bot ASP.NET and Node will emit logs at the server level that can be inspected.

#### Set up a browser to watch your logs

1. Open your bot in the [Azure Portal](https://portal.azure.com/).
1. Open the **App Service Settings / All App service settings** page to see all service settings.
1. Open the **Monitoring / Diagnostics Logs** page for the app service.
   - Ensure that **Application Logging (Filesystem)** is enabled. Be sure to click **Save** if you change this setting.
1. Switch to the **Monitoring / Log Stream** page.
   - Select **Web server logs** and ensure you see a message that you are connected. It should look something like the following:

     ```bash
     Connecting...
     2018-11-14T17:24:51  Welcome, you are now connected to log-streaming service.
     ```

     Keep this window open.

#### Set up browser to restart your bot service

1. Using a separate browser, open your bot in the Azure Portal.
1. Open the **App Service Settings / All App service settings** page to see all service settings.
1. Switch to the **Overview** page for the app service and click **Restart**.
   - It will prompt if you are sure; select **yes**.
1. Return to the first browser window and watch the logs.
1. Verify that you are receiving new logs.
   - If there is no activity, redeploy your bot.
   - Then switch to the **Application logs** page and look for any errors.


## Next steps

* [General troubleshooting for Azure Bot Service bots](./bot-service-troubleshoot-general-problems.md)
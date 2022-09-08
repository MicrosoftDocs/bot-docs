---
title: Add telemetry features to your QnA Maker bot
description: Learn how to integrate telemetry features into your QnA Maker enabled bot and send event data to telemetry services like Application Insights.
keywords: telemetry, appinsights, Application Insights, monitor bot, QnA Maker
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 09/01/2022
monikerRange: 'azure-bot-service-4.0'
ms.custom: abs-meta-21q1
---

# Add telemetry to your QnA Maker bot

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

[!INCLUDE [qnamaker-sunset-alert](../includes/qnamaker-sunset-alert.md)]

Telemetry logging lets bot applications send event data to telemetry services such as [Application Insights](/azure/azure-monitor/app/app-insights-overview/). Telemetry offers insights into your bot by showing which features are used the most, detects unwanted behavior and offers visibility into availability, performance, and usage.

The `TelemetryLoggerMiddleware` and `QnAMaker` classes in the Bot Framework SDK enable telemetry logging in QnA Maker enabled bots. `TelemetryLoggerMiddleware` is a middleware component that logs telemetry every time messages are received, sent, updated, or deleted, and the `QnAMaker` class provides custom logging that extends telemetry capabilities.

In this article you'll learn about:

- The code required to wire up telemetry in your bot
- The code required to enable the out-of-the-box QnA Maker logging and reports that use the standard event properties.
- Modifying or extending the SDK's default event properties to enable a wide range of reporting needs.

## Prerequisites

- The [QnA Maker sample code](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/11.qnamaker)
- A subscription to [Microsoft Azure](https://portal.azure.com/)
- An [Application Insights key](../bot-service-resources-app-insights-keys.md)
- Familiarity with [QnA Maker](https://qnamaker.ai/) is helpful.
- A [QnA Maker](/azure/cognitive-services/qnamaker/how-to/set-up-qnamaker-service-azure) account.
- An _existing_ and published QnA Maker knowledge base.

> [!NOTE]
> This article builds on the [QnA Maker sample code](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/11.qnamaker) by stepping you through the steps required to incorporate telemetry.

## Add telemetry code to your QnA Maker bot

We'll start with the [QnA Maker sample app](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/11.qnamaker) and add the code required to integrate telemetry into a bot that uses the QnA Maker service. This will enable Application Insights to track requests.

1. Open the [QnA Maker sample app](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/11.qnamaker) in Visual Studio.
1. Add the `Microsoft.Bot.Builder.Integration.ApplicationInsights.Core` NuGet package. For more information on using NuGet, see [Install and manage packages in Visual Studio](/nuget/tools/package-manager-ui):
1. Include the following statements in `Startup.cs`:

    ```csharp
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.Bot.Builder.ApplicationInsights;
    using Microsoft.Bot.Builder.Integration.ApplicationInsights.Core;
    ```

    > [!NOTE]
    > If you're following along by updating the QnA Maker sample code you'll notice that the using statement for `Microsoft.Bot.Builder.Integration.AspNet.Core` already exists in the QnA Maker sample.

1. Add the following code to the `ConfigureServices()` method in `Startup.cs`. This makes telemetry services available to your bot via [dependency injection (DI)](/aspnet/core/fundamentals/dependency-injection):

    ```csharp
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        ...
        // Create the Bot Framework Adapter with error handling enabled.
        services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

        // Add Application Insights services into service collection
        services.AddApplicationInsightsTelemetry();

        // Add the standard telemetry client
        services.AddSingleton<IBotTelemetryClient, BotTelemetryClient>();

        // Create the telemetry middleware to track conversation events
        services.AddSingleton<TelemetryLoggerMiddleware>();

        // Add the telemetry initializer middleware
        services.AddSingleton<IMiddleware, TelemetryInitializerMiddleware>();

        // Add telemetry initializer that will set the correlation context for all telemetry items
        services.AddSingleton<ITelemetryInitializer, OperationCorrelationTelemetryInitializer>();

        // Add telemetry initializer that sets the user ID and session ID (in addition to other bot-specific properties, such as activity ID)
        services.AddSingleton<ITelemetryInitializer, TelemetryBotIdInitializer>();
        ...
    }
    ```

    > [!NOTE]
    > If you're following along by updating the QnA Maker sample code, you'll notice that `services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();` already exists.

1. Instruct the adapter to use the middleware code that was added to the `ConfigureServices()` method. Open `AdapterWithErrorHandler.cs` and add `IMiddleware middleware` to the constructors parameter list. Add the `Use(middleware);` statement as the last line in the constructor:

    ```csharp
    public AdapterWithErrorHandler(ICredentialProvider credentialProvider, ILogger<BotFrameworkHttpAdapter> logger, IMiddleware middleware, ConversationState conversationState = null)
            : base(credentialProvider)
    {
        ...

        Use(middleware);
    }
    ```

1. Add the Application Insights instrumentation key in your `appsettings.json` file. The `appsettings.json` file contains metadata about external services the bot uses while running, such as connection and metadata for Cosmos DB, Application Insights, and QnA Maker. The addition to your `appsettings.json` file must be in this format:

    ```json
    {
        "MicrosoftAppId": "",
        "MicrosoftAppPassword": "",
        "QnAKnowledgebaseId": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
        "QnAEndpointKey": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx",
        "QnAEndpointHostName": "https://xxxxxxxx.azurewebsites.net/qnamaker",
        "ApplicationInsights": {
            "InstrumentationKey": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
        }
    }
    ```

    > [!NOTE]
    >
    > - Details on getting the _Application Insights instrumentation key_ can be found in the article [Application Insights keys](../bot-service-resources-app-insights-keys.md).
    > - You should already have a [QnA Maker account](/azure/cognitive-services/qnamaker/how-to/set-up-qnamaker-service-azure). For information on getting the QnA Maker knowledge base ID, endpoint key and host values, see the [Publish to get GenerateAnswer endpoint](/azure/cognitive-services/qnamaker/how-to/metadata-generateanswer-usage#publish-to-get-generateanswer-endpoint) section of QnA Maker's **Get an answer with the GenerateAnswer API** article.

At this point, the preliminary work to enable telemetry using Application Insights is done. You can run your bot locally using the Bot Framework Emulator and then go into Application Insights to see what is being logged such as response time, overall app health, and general running information.

> [!TIP]
> For information about personal information, see [Enable or disable activity event and personal information logging](bot-builder-telemetry.md#enable-or-disable-activity-event-and-personal-information-logging).

Next we'll see what needs to be included to add telemetry functionality to the QnA Maker service.

## Enable telemetry to capture usage data from the QnA Maker service

The QnA Maker service has built-in telemetry logging available, so there's little you need to do to start getting telemetry data from QnA Maker. First we'll see how to incorporate telemetry into the QnA Maker code to enable the built-in telemetry logging, then we'll learn how to replace or add properties to the existing event data to satisfy a wide range of reporting needs.

### Enable default QnA Maker logging

1. Create a private readonly field of type `IBotTelemetryClient` in your `QnABot` class in `QnABot.cs`:

    ```cs
    public class QnABot : ActivityHandler
        {
            private readonly IBotTelemetryClient _telemetryClient;
            ...
   }
    ```

1. Add an `IBotTelemetryClient` parameter to your `QnABot` class constructor in `QnABot.cs` and assign its value to the private field created in the previous step:

    ```cs
    public QnABot(IConfiguration configuration, ILogger<QnABot> logger, IHttpClientFactory httpClientFactory, IBotTelemetryClient telemetryClient)
    {
        ...
        _telemetryClient = telemetryClient;
    }
    ```

1. The _`telemetryClient`_ parameter is required when instantiating the new QnAMaker object in `QnABot.cs`:

    ```cs
    var qnaMaker = new QnAMaker(new QnAMakerEndpoint
                {
                    KnowledgeBaseId = _configuration["QnAKnowledgebaseId"],
                    EndpointKey = _configuration["QnAEndpointKey"],
                    Host = _configuration["QnAEndpointHostName"]
                },
                null,
                httpClient,
                _telemetryClient);
    ```

    > [!TIP]
    > Make sure that the property names that you use in the `_configuration` entries match the property names you used in the AppSettings.json file and the values for those properties are obtained by selecting the _View Code_ button on the [My knowledge bases](https://www.qnamaker.ai/Home/MyServices) page in the QnA Maker portal:

    ![AppSettings](media/AppSettings.json-QnAMaker.png)

#### View telemetry data logged from the QnA Maker default entries

You can view the results of your QnA Maker bot usage in Application Insights after running your bot in the Bot Framework Emulator by taking the following steps:

1. In the [Azure portal](https://portal.azure.com/), go to the Application Insights resource for your bot.
1. Under **Monitoring**, select **Logs**.
1. Enter the following Kusto query, then select **Run**.

    ```kusto
    customEvents
    | where name == 'QnaMessage'
    | extend answer = tostring(customDimensions.answer)
    | summarize count() by answer
    ```

1. Leave this page open in your browser; we'll come back to it after adding a new custom property.

> [!TIP]
> If you're new to the Kusto query language that's used to write log queries in Azure Monitor, but are familiar with SQL query language, you may find the [SQL to Azure Monitor log query cheat sheet](/azure/azure-monitor/log-query/sql-cheatsheet) useful.

### Modify or extend default event properties

If you need properties that aren't defined in the `QnAMaker` class there are two ways of handling this, both require creating your own class derived from the `QnAMaker` class. The first is explained in the section below titled [Adding properties](#add-properties) in which you add properties to the existing `QnAMessage` event. The second method allows you to create new events to which you can add properties as described in [Adding new events with custom properties](#add-new-events-with-custom-properties).

> [!NOTE]
> The `QnAMessage` event is part of the Bot Framework SDK and provides all of the out-of-the-box event properties that are logged to Application Insights.

#### Add properties

The following demonstrates how you can derive from the `QnAMaker` class. The example shows adding the property "MyImportantProperty" to the `QnAMessage` event. The `QnAMessage` event is logged every time a QnA [GetAnswers](/dotnet/api/microsoft.bot.builder.ai.qna.qnamaker.getanswersasync?view=botbuilder-dotnet-stable&preserve-view=true) call is performed.

After learning how to add custom properties we'll learn how to create a new custom event and associate properties with it, then we'll run the bot locally using the Bot Framework Emulator and see what is being logged in Application Insights using the Kusto query language.

1. Create a new class named `MyQnAMaker` in the `Microsoft.BotBuilderSamples` namespace that inherits from the `QnAMaker` class and save it as `MyQnAMaker.cs`. To inherit from the `QnAMaker` class, you'll need to add the `Microsoft.Bot.Builder.AI.QnA` using statement. Your code should appear as follows:

    ```csharp
    using Microsoft.Bot.Builder.AI.QnA;

    namespace Microsoft.BotBuilderSamples
    {
        public class MyQnAMaker : QnAMaker
        {

        }
    }
    ```

1. Add a class constructor to `MyQnAMaker`. Note that you'll need two more using statements for the constructor parameters for `System.Net.Http` and `Microsoft.Bot.Builder`:

    ```csharp
    using Microsoft.Bot.Builder.AI.QnA;
    using System.Net.Http;
    using Microsoft.Bot.Builder;

    namespace Microsoft.BotBuilderSamples
    {
        public class MyQnAMaker : QnAMaker
        {
            public MyQnAMaker(
                QnAMakerEndpoint endpoint,
                QnAMakerOptions options = null,
                HttpClient httpClient = null,
                IBotTelemetryClient telemetryClient = null,
                bool logPersonalInformation = false)
                : base(endpoint, options, httpClient, telemetryClient, logPersonalInformation)
            {

            }
        }
    }
    ```

1. Add the new property to the QnAMessage event after the constructor and include the statements `System.Collections.Generic`, `System.Threading`, and `System.Threading.Tasks`:

    ```csharp
    using Microsoft.Bot.Builder.AI.QnA;
    using System.Net.Http;
    using Microsoft.Bot.Builder;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    namespace Microsoft.BotBuilderSamples
    {
            public class MyQnAMaker : QnAMaker
            {
            ...

            protected override async Task OnQnaResultsAsync(
                                QueryResult[] queryResults,
                                Microsoft.Bot.Builder.ITurnContext turnContext,
                                Dictionary<string, string> telemetryProperties = null,
                                Dictionary<string, double> telemetryMetrics = null,
                                CancellationToken cancellationToken = default(CancellationToken))
            {
                var eventData = await FillQnAEventAsync(
                                        queryResults,
                                        turnContext,
                                        telemetryProperties,
                                        telemetryMetrics,
                                        cancellationToken)
                                    .ConfigureAwait(false);

                // Add new property
                eventData.Properties.Add("MyImportantProperty", "myImportantValue");

                // Log QnAMessage event
                TelemetryClient.TrackEvent(
                                QnATelemetryConstants.QnaMsgEvent,
                                eventData.Properties,
                                eventData.Metrics
                                );
            }

        }
    }
    ```

1. Modify your bot to use the new class, instead of creating a `QnAMaker` object you'll create a `MyQnAMaker` object in `QnABot.cs`:

    ```csharp
    var qnaMaker = new MyQnAMaker(new QnAMakerEndpoint
                {
                    KnowledgeBaseId = _configuration["QnAKnowledgebaseId"],
                    EndpointKey = _configuration["QnAEndpointKey"],
                    Host = _configuration["QnAEndpointHostName"]
                },
                null,
                httpClient,
                _telemetryClient);
    ```

##### View telemetry data logged from the new property _MyImportantProperty_

After running your bot in the Emulator, you can view the results in Application Insights by doing the following:

1. Switch back to your browser that has the _Logs (Analytics)_ view active.
1. Enter the following Kusto query and then select **Run**. This will give a count of the number of times the new property was executed:

    ```kusto
    customEvents
    | where name == 'QnaMessage'
    | extend MyImportantProperty = tostring(customDimensions.MyImportantProperty)
    | summarize count() by MyImportantProperty
    ```

1. To show details instead of the count, remove the last line and rerun the query:

    ```kusto
    customEvents
    | where name == 'QnaMessage'
    | extend MyImportantProperty = tostring(customDimensions.MyImportantProperty)
    ```

### Add new events with custom properties

If you need to log data to a different event than `QnaMessage`, you can create your own custom event with its own properties. To do this, we'll add code to the end of the `MyQnAMaker` class as follows:

```csharp
public class MyQnAMaker : QnAMaker
{
    ...

    // Create second event.
    var secondEventProperties = new Dictionary<string, string>();

    // Create new property for the second event.
    secondEventProperties.Add(
                        "MyImportantProperty2",
                        "myImportantValue2");

    // Log secondEventProperties event
    TelemetryClient.TrackEvent(
                    "MySecondEvent",
                    secondEventProperties);

}
```

## The Application Insights dashboard

Anytime you create an Application Insights resource in Azure, Azure creates a new dashboard associated with your resource. To display the dashboard from the Application Insights blade, select **Application Dashboard**.

Alternatively, to view the data, go to the Azure portal, expand the portal menu, then select **Dashboard**. Then, select the dashboard you want from the drop-down menu.

The dashboard displays some default information about your bot performance and any other queries that you've pinned to your dashboard.

## Additional Information

- [Add telemetry to your bot](bot-builder-telemetry.md)
- [What is Application Insights?](/azure/azure-monitor/app/app-insights-overview/)
- [Using Search in Application Insights](/azure/azure-monitor/app/diagnostic-search/)
- [Create custom KPI dashboards using Azure Application Insights](/azure/azure-monitor/learn/tutorial-app-dashboards/)

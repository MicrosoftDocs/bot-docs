---
title: Add telemetry to your bot
description: Learn how to view information on bot availability, performance, usage, and behavior. See how to turn on telemetry tracking for Application Insights.
keywords: telemetry, appinsights, monitor bot
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 10/11/2022
monikerRange: 'azure-bot-service-4.0'
---

# Add telemetry to your bot

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Telemetry logging enables bot applications to send event data to telemetry services such as [Application Insights](/azure/azure-monitor/app/app-insights-overview/). Telemetry offers insights into your bot by showing which features are used the most, detects unwanted behavior, and offers visibility into availability, performance, and usage.

This article describes how to implement telemetry in your bot using Application Insights. This article covers:

* The code required to wire up telemetry in your bot and connect to Application Insights.
* How to enable telemetry in your bot's [Dialogs](bot-builder-concept-dialog.md).
* How to enable telemetry to capture usage data from other services, like Azure Cognitive Services.
* How to visualize your telemetry data in Application Insights.

> [!IMPORTANT]
> For a regional bot that might collect personally identifiable information (PII) in telemetry, your Application Insights resource and your Azure Bot resource should be in the same region with the bot. If the resources are in different regions, the PII might leave the geographic region of the bot.

## Prerequisites

# [C#](#tab/csharp)

* The [CoreBot sample code](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/13.core-bot)
* The [Application Insights sample code](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/21.corebot-app-insights)
* A subscription to [Microsoft Azure](https://portal.azure.com/)
* An [Application Insights key](../bot-service-resources-app-insights-keys.md)
* Familiarity with [Application Insights](/azure/azure-monitor/app/app-insights-overview/)
* [git](https://git-scm.com/)

> [!NOTE]
> The [Application Insights sample code](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/21.corebot-app-insights) was built on top of the [CoreBot sample code](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/13.core-bot). This article will step you through modifying the CoreBot sample code to incorporate telemetry. If you're following along in Visual Studio you will have the Application Insights sample code by the time you're finished.

# [JavaScript](#tab/javascript)

* The [CoreBot sample code](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/13.core-bot)
* The [Application Insights sample code](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/21.corebot-app-insights)
* A subscription to [Microsoft Azure](https://portal.azure.com/)
* An [Application Insights key](../bot-service-resources-app-insights-keys.md)
* Familiarity with [Application Insights](/azure/azure-monitor/app/app-insights-overview/)
* [Visual Studio Code](https://www.visualstudio.com/downloads)
* [Node.js](https://nodejs.org/) version 10.14 or later. Use command `node --version` to determine the version of node you have installed.
* [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md)

> [!NOTE]
> The [Application Insights sample code](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/21.corebot-app-insights) was built on top of the [CoreBot sample code](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/13.core-bot). This article will step you through modifying the CoreBot sample code to incorporate telemetry. If you're following along in Visual Studio Code you will have the Application Insights sample code by the time you're finished.

---

## Enable telemetry in your bot

# [C#](#tab/csharp)

This article starts from the [CoreBot sample app](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/13.core-bot) and adds the code required to integrate telemetry into any bot. This will enable Application Insights to begin tracking requests.

> [!IMPORTANT]
> If you haven't setup your [Application Insights](/azure/azure-monitor/app/app-insights-overview) account and created your [Application Insights key](../bot-service-resources-app-insights-keys.md), do that before proceeding.

1. Open the [CoreBot sample app](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/13.core-bot) in Visual Studio.

1. Add  the `Microsoft.Bot.Builder.Integration.ApplicationInsights.Core` NuGet package. For more information on using NuGet, see [Install and manage packages in Visual Studio](/nuget/consume-packages/install-use-packages-visual-studio):

1. Include the following statements in `Startup.cs`:

    ```csharp
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.Bot.Builder.ApplicationInsights;
    using Microsoft.Bot.Builder.Integration.ApplicationInsights.Core;
    using Microsoft.Bot.Builder.Integration.AspNet.Core;
    ```

    > [!TIP]
    > If you're following along by updating the CoreBot sample code, you will notice that the using statement for `Microsoft.Bot.Builder.Integration.AspNet.Core` already exists in the CoreBot sample.

1. Include the following code in the `ConfigureServices()` method in `Startup.cs`. This will make telemetry services available to your bot via [dependency injection (DI)](/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2&preserve-view=true):

    ```csharp
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        ...
            // Create the Bot Framework Adapter with error handling enabled.
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

            // Add Application Insights services into service collection
            services.AddApplicationInsightsTelemetry();

            // Create the telemetry client.
            services.AddSingleton<IBotTelemetryClient, BotTelemetryClient>();

            // Add telemetry initializer that will set the correlation context for all telemetry items.
            services.AddSingleton<ITelemetryInitializer, OperationCorrelationTelemetryInitializer>();

            // Add telemetry initializer that sets the user ID and session ID (in addition to other bot-specific properties such as activity ID)
            services.AddSingleton<ITelemetryInitializer, TelemetryBotIdInitializer>();

            // Create the telemetry middleware to initialize telemetry gathering
            services.AddSingleton<TelemetryInitializerMiddleware>();

            // Create the telemetry middleware (used by the telemetry initializer) to track conversation events
            services.AddSingleton<TelemetryLoggerMiddleware>();
        ...
    }
    ```

    > [!TIP]
    > If you're following along by updating the CoreBot sample code, you will notice that `services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();` already exists.

1. Instruct the adapter to use the middleware code that was added to the `ConfigureServices()` method. You do this in `AdapterWithErrorHandler.cs` with the parameter TelemetryInitializerMiddleware telemetryInitializerMiddleware in the constructor's parameter list, and the `Use(telemetryInitializerMiddleware);` statement in the constructor as shown here:

    ```csharp
        public AdapterWithErrorHandler(IConfiguration configuration, ILogger<BotFrameworkHttpAdapter> logger, TelemetryInitializerMiddleware telemetryInitializerMiddleware, ConversationState conversationState = null)
            : base(configuration, logger)
    {
        ...
        Use(telemetryInitializerMiddleware);
    }
    ```

1. You will also need to add `Microsoft.Bot.Builder.Integration.ApplicationInsights.Core` to your list of using statements in `AdapterWithErrorHandler.cs`.

1. Add the Application Insights instrumentation key in your `appsettings.json` file. The `appsettings.json` file contains metadata about external services the bot uses while running. For example, CosmosDB, Application Insights, and Azure Cognitive Services connection and metadata is stored there. The addition to your `appsettings.json` file must be in this format:

    ```json
    {
        "MicrosoftAppId": "",
        "MicrosoftAppPassword": "",
        "ApplicationInsights": {
            "InstrumentationKey": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
        }
    }
    ```

    > [!NOTE]
    > Details on getting the _Application Insights instrumentation key_ can be found in the article [Application Insights keys](../bot-service-resources-app-insights-keys.md).

At this point, the preliminary work to enable telemetry using Application Insights is done. You can run your bot locally using the Emulator and then go into Application Insights to see what is being logged, such as response time, overall app health, and general running information.

### Enable telemetry in your bot's dialogs

When adding a new dialog to any ComponentDialog, it will inherit the Microsoft.Bot.Builder.IBotTelemetryClient of its parent dialog.  For example, In the CoreBot sample application all dialogs are added to the MainDialog, which is a ComponentDialog.  Once you set the TelemetryClient property to the MainDialog, all dialogs added to it will automatically inherit the telemetryClient from it, so it doesn't need to be explicitly set when adding dialogs.

Follow the steps below to update your CoreBot example:

1. In `MainDialog.cs`, update the constructor's parameter list to include the `IBotTelemetryClient` parameter, then set the MainDialog's TelemetryClient property to that value as shown in the following code snippet:

    ```csharp
    public MainDialog(IConfiguration configuration, ILogger<MainDialog> logger, IBotTelemetryClient telemetryClient)
        : base(nameof(MainDialog))
    {
        // Set the telemetry client for this and all child dialogs.
        this.TelemetryClient = telemetryClient;
        ...
    }
    ```

> [!TIP]
> If you're following along and updating the CoreBot sample code, you can refer to the [Application Insights sample code](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/21.corebot-app-insights) if you run into any problems.

That's all there is to adding telemetry to your bots dialogs, at this point if you ran your bot you should see things being logged in Application Insights, however if you have any integrated technology such as a Cognitive Service you will need to add the `TelemetryClient` to that code as well.

# [JavaScript](#tab/javascript)

This article starts with the [CoreBot sample app](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/13.core-bot) and adds the code required to integrate telemetry into any bot. This will enable Application Insights to begin tracking requests.

> [!IMPORTANT]
> If you haven't setup your [Application Insights](/azure/azure-monitor/app/app-insights-overview) account and created your [Application Insights key](../bot-service-resources-app-insights-keys.md), do that before proceeding.

1. Open the [CoreBot sample app](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/13.core-bot) in Visual Studio Code.

1. Add the [Application Insights key](../bot-service-resources-app-insights-keys.md) to your `.env` file: `InstrumentationKey=<EnterInstrumentationKeyHere>`. The `.env` file contains metadata about external services the bot uses while running. For example, Application Insights and Azure Cognitive Services connection and metadata is stored there. The addition to your `.env` file must be in this format:

    [!code-ini[.env file](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/.env?highlight=8)]

    > [!NOTE]
    > Details on getting the _Application Insights instrumentation key_ can be found in the article [Application Insights keys](../bot-service-resources-app-insights-keys.md).

1. Add a reference to the modules `ApplicationInsightsTelemetryClient` and `TelemetryInitializerMiddleware`  that are located in `botbuilder-applicationinsights` in the Bot Framework SDK. To do this, add the following code starting near the top of `index.js`, just after the code to import required packages:

    [!code-javascript[Import](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=16-17)]

    > [!TIP]
    > The [JavaScript Bot Samples](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs) use Node.js, which follows the CommonJS module system, and the built in `require` function to include modules that exist in separate files.

1. Create a new function at the end of `index.js` named `getTelemetryClient` that takes your instrumentation key as a parameter and returns a _telemetry client_ using the `ApplicationInsightsTelemetryClient` module you previously referenced. This  _telemetry client_ is where your telemetry data will be sent to, in this case Application Insights.

    [!code-javascript[getTelemetryClient](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=129-135)]

1. Next, you need to add the _telemetry middleware_ to the [adapter middleware pipeline](../v4sdk/bot-builder-concept-middleware.md#the-bot-middleware-pipeline). To do this, add the following code, starting just after the error handling code:  

    [!code-javascript[telemetryClient](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=82-86)]

1. In order for your dialog to report telemetry data, its `telemetryClient` must match the one used for the telemetry middleware, that is, `dialog.telemetryClient = telemetryClient;`

    [!code-javascript[dialog.telemetryClient](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=104-109&highlight=6)]

1. After creating the restify HTTP web server object, instruct it to use the `bodyParser` handler. <!--Need better/more detail-->

    [!code-javascript[dialog.telemetryClient](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=125-127)]

    > [!TIP]
    > This uses the _restify_ `bodyParser` function. _restify_ is a "A Node.js web service framework optimized for building semantically correct RESTful web services ready for production use at scale. restify optimizes for introspection and performance, and is used in some of the largest Node.js deployments on Earth." See the [restify](http://restify.com) web site for more information.

    Node.js which follows the CommonJS module system, and the built in `require` function to include modules that exist in separate files.

At this point, the preliminary work to enable telemetry using Application Insights is done. You can run your bot locally using the Emulator and then go into Application Insights to see what is being logged, such as response time, overall app health, and general running information.

---

## Enable or disable activity event and personal information logging

# [C#](#tab/csharp)

### Enable or disable activity logging

By default, the `TelemetryInitializerMiddleware` will use the `TelemetryLoggerMiddleware` to log telemetry when your bot sends / receives activities. Activity logging creates custom event logs in your Application Insights resource. If you wish, you can disable activity event logging by setting  `logActivityTelemetry` to false on the `TelemetryInitializerMiddleware` when registering it in **Startup.cs**.

```cs
public void ConfigureServices(IServiceCollection services)
{
    ...
    // Add the telemetry initializer middleware
    services.AddSingleton<TelemetryInitializerMiddleware>(sp =>
            {
                var loggerMiddleware = sp.GetService<TelemetryLoggerMiddleware>();
                return new TelemetryInitializerMiddleware(loggerMiddleware, logActivityTelemetry: false);
            });
    ...
}
```

### Enable or disable personal information logging

By default, if activity logging is enabled, some properties on the incoming / outgoing activities are excluded from logging as they are likely to contain personal information, such as user name and the activity text. You can choose to include these properties in your logging by making the following change to **Startup.cs** when registering the `TelemetryLoggerMiddleware`.

```cs
public void ConfigureServices(IServiceCollection services)
{
    ...
    // Add the telemetry initializer middleware
    services.AddSingleton<TelemetryLoggerMiddleware>(sp =>
            {
                var telemetryClient = sp.GetService<IBotTelemetryClient>();
                return new TelemetryLoggerMiddleware(telemetryClient, logPersonalInformation: true);
            });
    ...
}
```

Next we will see what needs to be included to add telemetry functionality to the dialogs. This will enable you to get additional information such as what dialogs run, and statistics about each one.

# [JavaScript](#tab/javascript)

### Enable or disable activity logging

By default, the `TelemetryInitializerMiddleware` will use the `TelemetryLoggerMiddleware` to log telemetry when your bot sends or receives activities. Activity logging creates custom event logs in your Application Insights resource.  If you wish, you can disable activity event logging by setting  `logActivityTelemetry` to false on the `TelemetryInitializerMiddleware` when registering it in **index.js**.

The following code snippet comes from sample `21.corebot-app-insights`, and shows the call to `TelemetryInitializerMiddleware`:

[!code-javascript[dialog.telemetryClient](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=82-86&highlight=4)]

The code snippet below shows the change needed in sample `21.corebot-app-insights`, in the call to `TelemetryInitializerMiddleware` to disable activity logging:

```javascript
// Add telemetry middleware to the adapter middleware pipeline
var telemetryClient = getTelemetryClient(process.env.InstrumentationKey);
var telemetryLoggerMiddleware = new TelemetryLoggerMiddleware(telemetryClient);
var initializerMiddleware = new TelemetryInitializerMiddleware(telemetryLoggerMiddleware, false);
adapter.use(initializerMiddleware);
```

### Enable or disable personal information logging

When activity logging is enabled, some properties on the incoming / outgoing activities are excluded from logging by default as they are likely to contain personal information, such as user name and the activity text. You can choose to include these properties in your logging by changing the `logPersonalInformation` parameter from `false` to `true` when registering the `TelemetryLoggerMiddleware` in **index.js**.

[!code-javascript[dialog.telemetryClient](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=82-86&highlight=4)]

Next we will see what needs to be included to add telemetry functionality to the dialogs. This will enable you to get additional information such as what dialogs run, and statistics about each one.

---

## Enabling telemetry to capture usage data from other services like LUIS and QnA Maker

[!INCLUDE [qnamaker-sunset-alert](../includes/qnamaker-sunset-alert.md)]

[!INCLUDE [luis-sunset-alert](../includes/luis-sunset-alert.md)]

# [C#](#tab/csharp)

We will next implement telemetry functionality in your LUIS service. The LUIS service has built-in telemetry logging available so there's little you need to do to start getting telemetry data from LUIS.  If you're interested in enabling telemetry in a QnA Maker enabled bot, see [Add telemetry to your QnA Maker bot](../v4sdk/bot-builder-telemetry-QnAMaker.md)

1. The _`IBotTelemetryClient telemetryClient`_ parameter is required in the `FlightBookingRecognizer` constructor in `FlightBookingRecognizer.cs`:

    ```cs
    public FlightBookingRecognizer(IConfiguration configuration, IBotTelemetryClient telemetryClient)
    ```

1. Next you will need to enable the `telemetryClient` when creating your `LuisRecognizer` in the `FlightBookingRecognizer` constructor. You do this by adding the `telemetryClient` as a new _LuisRecognizerOption_:

    ```cs
    if (luisIsConfigured)
    {
        var luisApplication = new LuisApplication(
            configuration["LuisAppId"],
            configuration["LuisAPIKey"],
            "https://" + configuration["LuisAPIHostName"]);

        // Set the recognizer options depending on which endpoint version you want to use.
        // More details can be found in /azure/cognitive-services/luis/luis-migration-api-v3
        var recognizerOptions = new LuisRecognizerOptionsV3(luisApplication)
        {
            TelemetryClient = telemetryClient,
        };
        _recognizer = new LuisRecognizer(recognizerOptions);
    }
    ```

That's it, you should have a functional bot that logs telemetry data into Application insights. You can use the [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md) to run your bot locally. You shouldn't see any changes in the bot's behavior, but it will be logging information into Application Insights. Interact with the bot by sending multiple messages and in the next section we will review the telemetry results in Application Insights.

For information on testing and debugging your bot, you can refer to the following articles:

* [Debug a bot](../bot-service-debug-bot.md)
* [Testing and debugging guidelines](../v4sdk/bot-builder-testing-debugging.md)
* [Debug with the Emulator](../bot-service-debug-emulator.md)

# [JavaScript](#tab/javascript)

We will next implement telemetry functionality in your LUIS service. The LUIS service has built-in telemetry logging available so there's little you need to do to start getting telemetry data from LUIS.

To enable the telemetry client in your LUIS recognizer:

1. Open `FlightBookingRecognizer.js`

1. Pass the `telemetryClient` parameter to the `FlightBookingRecognizer` constructor:

    [!code-javascript[FlightBookingRecognizer](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/dialogs/flightBookingRecognizer.js?range=7)]

1. Set the `telemetryClient` field of the `recognizerOptions` object to the `telemetryClient` property that is passed into the `FlightBookingRecognizer` constructor, once done your constructor will appear as follows:

    [!code-javascript[FlightBookingRecognizer](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/dialogs/flightBookingRecognizer.js?range=9-18&highlight=6)]

1. And finally you need to include the `telemetryClient` when creating an instance of the `FlightBookingRecognizer` in `index.js`:

    [!code-javascript[FlightBookingRecognizer](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=86)]

That's it; you should have a functional bot that logs telemetry data into Application insights. You can use the [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md) to run your bot locally. You shouldn't see any changes in the bot's behavior, but it will be logging information into Application Insights. Interact with the bot by sending multiple messages, and the next section describes how to review the telemetry results in Application Insights.

For information on testing and debugging your bot, you can refer to the following articles:

* [Debug a bot](../bot-service-debug-bot.md)
* [Testing and debugging guidelines](../v4sdk/bot-builder-testing-debugging.md)
* [Debug with the Emulator](../bot-service-debug-emulator.md)

---

## Visualizing your telemetry data in Application Insights

Application Insights monitors the availability, performance, and usage of your bot application whether it's hosted in the cloud or on-premises. It leverages the powerful data analysis platform in Azure Monitor to provide you with deep insights into your application's operations and diagnose errors without waiting for a user to report them. There are a few ways to see the telemetry data collected by Application Insights, two of the primary ways are through queries and the dashboard.

### Querying your telemetry data in Application Insights using Kusto Queries

Use this section as a starting point to learn how to use log queries in Application Insights. It demonstrates two useful queries and provides links to other documentation with additional information.

To query your data

1. Go to the [Azure portal](https://portal.azure.com)
1. Navigate to your Application Insights. Easiest way to do so is click on **Monitor > Applications** and find it there.
1. Once in your Application Insights, you can click on _Logs (Analytics)_ on the navigation bar.

    ![Logs (Analytics) LogView](media/AppInsights-LogView.png)

1. This will bring up the Query window.  Enter the following query and select _Run_:

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

1. This will return the percentage of waterfall dialogs that run to completion.

    ![App Insights Query Percent Complete](media/AppInsights-Query-PercentCompleteDialog.png)

> [!TIP]
> You can pin any query to your Application Insights dashboard   by selecting the button on the top right of the **Logs (Analytics)** blade. Just select the dashboard you want it pinned to, and it will be available next time you visit that dashboard.

## The Application Insights dashboard

Anytime you create an Application Insights resource in Azure, a new dashboard will automatically be created and associated with it.  You can see that dashboard by selecting the button at the top of your Application Insights blade, labeled **Application Dashboard**.

![Application Dashboard Link](media/Application-Dashboard-Link.png)

Alternatively, to view the data, go to the Azure portal. Click **Dashboard** on the left, then select the dashboard you want from the drop-down.

There, you'll see some default information about your bot performance and any additional queries that you've pinned to your dashboard.

## Additional Information

* [Add telemetry to your QnA Maker bot](bot-builder-telemetry-qnamaker.md)
* [What is Application Insights?](/azure/azure-monitor/app/app-insights-overview/)
* [Using Search in Application Insights](/azure/azure-monitor/app/diagnostic-search/)
* [Create custom KPI dashboards using Azure Application Insights](/azure/azure-monitor/learn/tutorial-app-dashboards/)

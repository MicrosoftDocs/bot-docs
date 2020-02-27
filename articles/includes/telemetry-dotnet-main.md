
We will start with the [CoreBot sample app](https://aka.ms/cs-core-sample) and add the code required to integrate telemetry into any bot. This will enable Application Insights to begin tracking requests.

> [!IMPORTANT]
> If you have not setup your [Application Insights](https://aka.ms/appinsights-overview) account and created your [Application Insights key](../bot-service-resources-app-insights-keys.md), do that before proceeding.

1. Open the [CoreBot sample app](https://aka.ms/cs-core-sample) in Visual Studio.

2. Add  the `Microsoft.Bot.Builder.Integration.ApplicationInsights.Core ` NuGet package. For more information on using NuGet, see [Install and manage packages in Visual Studio](https://aka.ms/install-manage-packages-vs):


3. Include the following statements in `Startup.cs`:
    ```csharp
    using Microsoft.ApplicationInsights.Extensibility;
    using Microsoft.Bot.Builder.ApplicationInsights;
    using Microsoft.Bot.Builder.Integration.ApplicationInsights.Core;
    using Microsoft.Bot.Builder.Integration.AspNet.Core;
    ```

    Note: If you're following along by updating the CoreBot sample code you will notice that the using statement for `Microsoft.Bot.Builder.Integration.AspNet.Core` already exists in the CoreBot sample.

4. Include the following code in the `ConfigureServices()` method in `Startup.cs`. This will make telemetry services available to your bot via [dependency injection (DI)](https://aka.ms/asp.net-core-dependency-interjection):
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

    Note: If you're following along by updating the CoreBot sample code you will notice that `services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();` already exists 

5. Instruct the adapter to use the middleware code that was added to the `ConfigureServices()` method. You do this in `AdapterWithErrorHandler.cs` with the parameter TelemetryInitializerMiddleware telemetryInitializerMiddleware in the constructor's parameter list, and the `Use(telemetryInitializerMiddleware);` statement in the contructor as shown here:
    ```csharp
        public AdapterWithErrorHandler(IConfiguration configuration, ILogger<BotFrameworkHttpAdapter> logger, TelemetryInitializerMiddleware telemetryInitializerMiddleware, ConversationState conversationState = null)
            : base(configuration, logger)
    {
        ...
        Use(telemetryInitializerMiddleware);
    }
    ```

6. You will also need to add `Microsoft.Bot.Builder.Integration.ApplicationInsights.Core` to your list of using statements in `AdapterWithErrorHandler.cs`.

7. Add the Application Insights instrumentation key in your `appsettings.json` file. The `appsettings.json` file contains metadata about external services the bot uses while running. For example, CosmosDB, Application Insights and the Language Understanding (LUIS) service connection and metadata is stored there. The addition to your `appsettings.json` file must be in this format:

    ```json
    {
        "MicrosoftAppId": "",
        "MicrosoftAppPassword": "",
        "ApplicationInsights": {
            "InstrumentationKey": "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"
        }
    }
    ```
    Note: Details on getting the _Application Insights instrumentation key_ can be found in the article [Application Insights keys](../bot-service-resources-app-insights-keys.md).

At this point the preliminary work to enable telemetry using Application Insights is done.  You can run your bot locally using the bot emulator and then go into Application Insights to see what is being logged, such as response time, overall app health, and general running information. 


## Enabling telemetry in your bots Dialogs

When adding a new dialog to any ComponentDialog, it will inherit the Microsoft.Bot.Builder.IBotTelemetryClient of its parent dialog.  For example, In the CoreBot sample application all dialogs are added to the MainDialog which is a ComponentDialog.  Once you set the TelemetryClient property to the MainDialog all dialogs added to it will automatically inherit the telemetryClient from it, so it does not need to be explicitly set when adding dialogs.
 
 Follow the steps below to update your CoreBot example:

1.  In `MainDialog.cs`, update the constructor's parameter list to include the `IBotTelemetryClient` parameter, then set the MainDialog's TelemetryClient property to that value as shown in the following code snippet:

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
> If you are following along and updating the CoreBot sample code, you can refer to the [Application Insights sample code](https://aka.ms/csharp-corebot-app-insights-sample) if you run into any problems.

That's all there is to adding telemetry to your bots dialogs, at this point if you ran your bot you should see things being logged in Application Insights, however if you have any integrated technology such as LUIS and QnA Maker you will need to add the `TelemetryClient` to that code as well.
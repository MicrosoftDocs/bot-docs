This article starts with the [CoreBot sample app](https://aka.ms/js-core-sample) and adds the code required to integrate telemetry into any bot. This will enable Application Insights to begin tracking requests.

> [!IMPORTANT]
> If you have not setup your [Application Insights](https://aka.ms/appinsights-overview) account and created your [Application Insights key](../bot-service-resources-app-insights-keys.md), do that before proceeding.

1. Open the [CoreBot sample app](https://aka.ms/js-core-sample) in Visual Studio Code.

2. Add the [Application Insights key](../bot-service-resources-app-insights-keys.md) to your `.env` file: `InstrumentationKey=<EnterInstrumentationKeyHere>`. The `.env` file contains metadata about external services the bot uses while running. For example, Application Insights and the Language Understanding (LUIS) service connection and metadata is stored there. The addition to your `.env` file must be in this format:

    [!code-json[env](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/.env?range=1-6&highlight=6)]

    <!-- This is the code block that the code snippet link should point to:
    ```json
    MicrosoftAppId=
    MicrosoftAppPassword=
    LuisAppId=
    LuisAPIKey=
    LuisAPIHostName=
    InstrumentationKey=
    ```
    -->

    Note: Details on getting the _Application Insights instrumentation key_ can be found in the article [Application Insights keys](../bot-service-resources-app-insights-keys.md).

3. Add a reference to the modules `ApplicationInsightsTelemetryClient` and `TelemetryInitializerMiddleware`  that are located in `botbuilder-applicationinsights` in the Bot Framework SDK. To do this, add the following code starting near the top of `index.js`, just after the code to import required packages:

    [!code-javascript[Import](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=16-17)]

    <!-- This is the code block that the code snippet link should point to:
    ```javascript
    // Import required services for bot telemetry
    const { ApplicationInsightsTelemetryClient, TelemetryInitializerMiddleware } = require('botbuilder-applicationinsights');
    const { TelemetryLoggerMiddleware } = require('botbuilder-core');
    ```
    -->

    > [!TIP]
    > The [JavaScript Bot Samples](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs) use Node.js which follows the CommonJS module system, and the built in `require` function to include modules that exist in separate files.

4. Create a new function at the end of `index.js` named `getTelemetryClient` that takes your instrumentation key as a parameter and returns a _telemetry client_ using the `ApplicationInsightsTelemetryClient` module you previously referenced. This  _telemetry client_ is where your telemetry data will be sent to, in this case Application Insights.

    [!code-javascript[getTelemetryClient](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=116-122)]

    <!-- This is the code block that the code snippet link should point to:
    ```javascript
    // Creates a new TelemetryClient based on a instrumentation key
    function getTelemetryClient(instrumentationKey) {
        if (instrumentationKey) {
            return new ApplicationInsightsTelemetryClient(instrumentationKey);
        }
        return new NullTelemetryClient();
    }
    ```
    -->

5. Next, you need to add the _telemetry middleware_ to the [adapter middleware pipeline](../v4sdk/bot-builder-concept-middleware.md#the-bot-middleware-pipeline). To do this, add the following code, starting just after the error handling code:  

    <!-- This level of detail may be too much:
        - The first step is to create a new telemetry client, in this case you are using Application Insights as the telemetry client using the module `ApplicationInsightsTelemetryClient` referenced in the previous step. This line of code will call the function `getTelemetryClient` that you will soon create, passing in the Application Insights key and that function will return a new telemetry client: `var telemetryClient = getTelemetryClient(process.env.InstrumentationKey);`. 
        - You will pass the telemetry client you just created to the `TelemetryLoggerMiddleware` function: `var telemetryLoggerMiddleware = new TelemetryLoggerMiddleware(telemetryClient, true);` which creates a TelemetryLoggerMiddleware object that you will use to create
        - 
    -->

    [!code-javascript[telemetryClient](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=66-70)]

    <!-- This is the code block that the code snippet link should point to:
    ```javascript
    // Add telemetry middleware to the adapter middleware pipeline
    var telemetryClient = getTelemetryClient(process.env.InstrumentationKey);
    var telemetryLoggerMiddleware = new TelemetryLoggerMiddleware(telemetryClient, true);
    var initializerMiddleware = new TelemetryInitializerMiddleware(telemetryLoggerMiddleware, true);
    adapter.use(initializerMiddleware);
    ```
    -->

6. In order for your dialog to report telemetry data, its `telemetryClient` must match the one used for the telemetry middleware, that is, `dialog.telemetryClient = telemetryClient;`

    [!code-javascript[dialog.telemetryClient](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=88-93&highlight=6)]

    <!-- This is the code block that the code snippet link should point to:
    ```javascript
    // Create the main dialog.
    const bookingDialog = new BookingDialog(BOOKING_DIALOG);
    const dialog = new MainDialog(luisRecognizer, bookingDialog);
    const bot = new DialogAndWelcomeBot(conversationState, userState, dialog);

    dialog.telemetryClient = telemetryClient; //This should be highlighted
    ```
    -->

7. After creating the restify HTTP web server object, instruct it to use the `bodyParser` handler. <!--Need better/more detail-->

    [!code-javascript[dialog.telemetryClient](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=112-114)]

    <!-- This is the code block that the code snippet link should point to:
    ```javascript
    // Enable the Application Insights middleware, which helps correlate all activity
    // based on the incoming request.
    server.use(restify.plugins.bodyParser());
    ```
    -->

    > [!TIP]
    > This uses the _restify_ `bodyParser` function. _restify_ is a "A Node.js web service framework optimized for building semantically correct RESTful web services ready for production use at scale. restify optimizes for introspection and performance, and is used in some of the largest Node.js deployments on Earth." See the [restify](http://restify.com) web site for more information.

    Node.js which follows the CommonJS module system, and the built in `require` function to include modules that exist in separate files.

At this point the preliminary work to enable telemetry using Application Insights is done.  You can run your bot locally using the bot Emulator and then go into Application Insights to see what is being logged, such as response time, overall app health, and general running information.

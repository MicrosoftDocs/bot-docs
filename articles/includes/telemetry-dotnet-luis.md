
We will next implement telemetry functionality in your LUIS service. The LUIS service has built-in telemetry logging available so there is very little you need to do to start getting telemetry data from LUIS.  If you are interested in enabling telemetry in a QnA Maker enabled bot, see [Add telemetry to your QnAMaker bot](bot-builder-telemetry-QnAMaker.md)

1. The _`IBotTelemetryClient telemetryClient`_ parameter is required in the `FlightBookingRecognizer` constructor in `FlightBookingRecognizer.cs`:

    ```cs
    public FlightBookingRecognizer(IConfiguration configuration, IBotTelemetryClient telemetryClient)
    ```

2. Next you will need to enable the `telemetryClient` when creating your `LuisRecognizer` in the `FlightBookingRecognizer` constructor. You do this by adding the `telemetryClient` as a new _LuisPredictionOption_:

    ```cs
    if (luisIsConfigured)
    {
        var luisApplication = new LuisApplication(
            configuration["LuisAppId"],
            configuration["LuisAPIKey"],
            "https://" + configuration["LuisAPIHostName"]);

        // Set the recognizer options depending on which endpoint version you want to use.
        // More details can be found in https://docs.microsoft.com/azure/cognitive-services/luis/luis-migration-api-v3
        var recognizerOptions = new LuisRecognizerOptionsV3(luisApplication)
        {
            TelemetryClient = telemetryClient,
        };
        _recognizer = new LuisRecognizer(recognizerOptions);
    }
    ```

That's it, you should have a functional bot that logs telemetry data into Application insights. You can use the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme) to run your bot locally. You shouldn't see any changes in the bot's behavior, but it will be logging information into Application Insights. Interact with the bot by sending multiple messages and in the next section we will review the telemetry results in Application Insights.

For information on testing and debugging your bot, you can refer to the following articles:

 * [Debug a bot](../bot-service-debug-bot.md)
 * [Testing and debugging guidelines](bot-builder-testing-debugging.md)
 * [Debug with the emulator](../bot-service-debug-emulator.md)


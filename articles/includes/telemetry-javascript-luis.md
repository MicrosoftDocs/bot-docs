We will next implement telemetry functionality in your LUIS service. The LUIS service has built-in telemetry logging available so there is very little you need to do to start getting telemetry data from LUIS.  <!---If you are interested in enabling telemetry in a QnA Maker enabled bot, see [Add telemetry to your QnAMaker bot](../v4sdk/bot-builder-telemetry-QnAMaker.md).-->

1. The _`IBotTelemetryClient telemetryClient`_ parameter is required in the `FlightBookingRecognizer` constructor in `FlightBookingRecognizer.js`:

    [!code-javascript[FlightBookingRecognizer](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/dialogs/flightBookingRecognizer.js?range=6-16&highlight=6)]


    <!-- This is the code block that the code snippet link should point to:
    ```javascript
    class FlightBookingRecognizer {
        constructor(config, telemetryClient) {
            const luisIsConfigured = config && config.applicationId && config.endpointKey && config.endpoint;
            if (luisIsConfigured) {
                // Set the recognizer options depending on which endpoint version you want to use e.g v2 or v3.
                // More details can be found in https://docs.microsoft.com/azure/cognitive-services/luis/luis-migration-api-v3
                const recognizerOptions = {
                    apiVersion: 'v3',
                    telemetryClient: telemetryClient
                };

                this.recognizer = new LuisRecognizer(config, recognizerOptions);
            }
        }
    ```
    -->

2. Next you will need to enable the `telemetryClient` when creating your `LuisRecognizer` in the `FlightBookingRecognizer` constructor. You do this by adding the `telemetryClient` as a new _LuisPredictionOption_ and passing it in when creating the new LuisRecognizer:

    [!code-javascript[FlightBookingRecognizer](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/dialogs/flightBookingRecognizer.js?range=9-18&highlight=14)]


    <!-- This is the code block that the code snippet link should point to:
    ```javascript
        if (luisIsConfigured) {
            // Set the recognizer options depending on which endpoint version you want to use e.g v2 or v3.
            // More details can be found in https://docs.microsoft.com/azure/cognitive-services/luis/luis-migration-api-v3
            const recognizerOptions = {
                apiVersion: 'v3',
                telemetryClient: telemetryClient
            };

            this.recognizer = new LuisRecognizer(config, recognizerOptions);
        }
    ```
    -->

3. And finally you need to include the `telemetryClient` when creating an instance of the `FlightBookingRecognizer` in `index.js`


    [!code-javascript[FlightBookingRecognizer](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=82)]


    <!-- This is the code block that the code snippet link should point to:
    ```javascript
    const luisRecognizer = new FlightBookingRecognizer(luisConfig, telemetryClient); 
    ```
    -->

That's it, you should have a functional bot that logs telemetry data into Application insights. You can use the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme) to run your bot locally. You shouldn't see any changes in the bot's behavior, but it will be logging information into Application Insights. Interact with the bot by sending multiple messages and in the next section we will review the telemetry results in Application Insights.

For information on testing and debugging your bot, you can refer to the following articles:

 * [Debug a bot](../bot-service-debug-bot.md)
 * [Testing and debugging guidelines](../v4sdk/bot-builder-testing-debugging.md)
 * [Debug with the emulator](../bot-service-debug-emulator.md)


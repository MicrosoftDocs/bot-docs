<!--Work on this hasn't started, this is just a placeholder-->
We will next implement telemetry functionality in your LUIS service. The LUIS service has built-in telemetry logging available so there is very little you need to do to start getting telemetry data from LUIS.  If you are interested in enabling telemetry in a QnA Maker enabled bot, see [Add telemetry to your QnAMaker bot](../v4sdk/bot-builder-telemetry-QnAMaker.md).

1. The _`IBotTelemetryClient telemetryClient`_ parameter is required in the `FlightBookingRecognizer` constructor in `FlightBookingRecognizer.js`:
<!--"The _`IBotTelemetryClient telemetryClient`_ parameter" needs updated as soon as the sample is updated-->

    [!code-javascript[FlightBookingRecognizer](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/dialogs/flightBookingRecognizer.js?range=6-16)]

    ```javascript
    class FlightBookingRecognizer {
        constructor(config) {
            const luisIsConfigured = config && config.applicationId && config.endpointKey && config.endpoint;
            if (luisIsConfigured) {
                this.recognizer = new LuisRecognizer(config, {}, true);
            }
        }

        get isConfigured() {
            return (this.recognizer !== undefined);
        }
        ...
    ```

2. Next you will need to enable the `telemetryClient` when creating your `LuisRecognizer` in the `FlightBookingRecognizer` constructor. You do this by adding the `telemetryClient` as a new _LuisPredictionOption_:
<!--This needs updated as soon as the sample is updated-->


    ```javascript

    ```

That's it, you should have a functional bot that logs telemetry data into Application insights. You can use the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme) to run your bot locally. You shouldn't see any changes in the bot's behavior, but it will be logging information into Application Insights. Interact with the bot by sending multiple messages and in the next section we will review the telemetry results in Application Insights.

For information on testing and debugging your bot, you can refer to the following articles:

 * [Debug a bot](../bot-service-debug-bot.md)
 * [Testing and debugging guidelines](../v4sdk/bot-builder-testing-debugging.md)
 * [Debug with the emulator](../bot-service-debug-emulator.md)


We will next implement telemetry functionality in your LUIS service. The LUIS service has built-in telemetry logging available so there is very little you need to do to start getting telemetry data from LUIS.  <!---If you are interested in enabling telemetry in a QnA Maker enabled bot, see [Add telemetry to your QnAMaker bot](../v4sdk/bot-builder-telemetry-QnAMaker.md).-->

To enable the telemetry client in your LUIS recognizer:

1. Open `FlightBookingRecognizer.js`

2. Pass the `telemetryClient` parameter to the `FlightBookingRecognizer` constructor:

    [!code-javascript[FlightBookingRecognizer](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/dialogs/flightBookingRecognizer.js?range=7)]

    <!-- This is the code block that the code snippet link should point to:
    ```javascript
        constructor(config, telemetryClient) {
    ```
    -->

3. Set the `telemetryClient` field of the `recognizerOptions` object to the `telemetryClient` property that is passed into the `FlightBookingRecognizer` constructor, once done your constructor will appear as follows:

    [!code-javascript[FlightBookingRecognizer](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/dialogs/flightBookingRecognizer.js?range=9-15&highlight=14)]

    <!-- This is the code block that the code snippet link should point to:
    ```javascript
     if (luisIsConfigured) {
            // Set the recognizer options depending on which endpoint version you want to use e.g v2 or v3.
            // More details can be found in https://docs.microsoft.com/azure/cognitive-services/luis/luis-migration-api-v3
            const recognizerOptions = {
                apiVersion: 'v3',
                telemetryClient: telemetryClient
            };
    ```
    -->

4. And finally you need to include the `telemetryClient` when creating an instance of the `FlightBookingRecognizer` in `index.js`:

    [!code-javascript[FlightBookingRecognizer](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=84)]

    <!-- This is the code block that the code snippet link should point to:
    ```javascript
    const luisRecognizer = new FlightBookingRecognizer(luisConfig, telemetryClient);
    ```
    -->

That's it; you should have a functional bot that logs telemetry data into Application insights. You can use the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme) to run your bot locally. You shouldn't see any changes in the bot's behavior, but it will be logging information into Application Insights. Interact with the bot by sending multiple messages, and the next section describes how to review the telemetry results in Application Insights.

For information on testing and debugging your bot, you can refer to the following articles:

* [Debug a bot](../bot-service-debug-bot.md)
* [Testing and debugging guidelines](../v4sdk/bot-builder-testing-debugging.md)
* [Debug with the emulator](../bot-service-debug-emulator.md)

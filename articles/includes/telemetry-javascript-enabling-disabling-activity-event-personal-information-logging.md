### Enable or disable activity logging

By default, the `TelemetryInitializerMiddleware` will use the `TelemetryLoggerMiddleware` to log telemetry when your bot sends or receives activities. Activity logging creates custom event logs in your Application Insights resource.  If you wish, you can disable activity event logging by setting  `logActivityTelemetry` to false on the `TelemetryInitializerMiddleware` when registering it in **index.js**.

The following code snippet comes from sample `21.corebot-app-insights`, and shows the call to `TelemetryInitializerMiddleware`:

[!code-javascript[dialog.telemetryClient](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=64-68)]

The code snippet below shows the change needed in sample `21.corebot-app-insights`, in the call to `TelemetryInitializerMiddleware` to disable activity logging:

```javascript
// Add telemetry middleware to the adapter middleware pipeline
var telemetryClient = getTelemetryClient(process.env.InstrumentationKey);
var telemetryLoggerMiddleware = new TelemetryLoggerMiddleware(telemetryClient);
var initializerMiddleware = new TelemetryInitializerMiddleware(telemetryLoggerMiddleware, false);
adapter.use(initializerMiddleware);
```

### Enable or disable logging personal information

When activity logging is enabled, some properties on the incoming / outgoing activities are excluded from logging by default as they are likely to contain personal information, such as user name and the activity text. You can choose to include these properties in your logging by changing the `logPersonalInformation` parameter from `false` to `true` when registering the `TelemetryLoggerMiddleware` in **index.js**.

[!code-javascript[dialog.telemetryClient](~/../botbuilder-samples/samples/javascript_nodejs/21.corebot-app-insights/index.js?range=64-68&highlight=3)]

<!-- This is the code block that the code snippet link should point to:
```javascript
// Add telemetry middleware to the adapter middleware pipeline
var telemetryClient = getTelemetryClient(process.env.InstrumentationKey);
var telemetryLoggerMiddleware = new TelemetryLoggerMiddleware(telemetryClient, true);  //This line should be highlighted
var initializerMiddleware = new TelemetryInitializerMiddleware(telemetryLoggerMiddleware);
adapter.use(initializerMiddleware);
```
-->

Next we will see what needs to be included to add telemetry functionality to the dialogs. This will enable you to get additional information such as what dialogs run, and statistics about each one.

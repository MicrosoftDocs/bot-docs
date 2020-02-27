<!--Work on this hasn't started, this is just a placeholder-->
## Enabling / disabling activity event and personal information logging

### Enabling or disabling Activity logging

By default, the `TelemetryInitializerMiddleware` will use the `TelemetryLoggerMiddleware` to log telemetry when your bot sends / receives activities. Activity logging creates custom event logs in your Application Insights resource.  If you wish, you can disable activity event logging by setting  `logActivityTelemetry` to false on the `TelemetryInitializerMiddleware` when registering it in **index.js**.

<!--Original C#-->
```cs
            // Create the telemetry middleware to initialize telemetry gathering
            services.AddSingleton<TelemetryInitializerMiddleware>();

            // Create the telemetry middleware (used by the telemetry initializer) to track conversation events
            services.AddSingleton<TelemetryLoggerMiddleware>();
```
<!--Updated C#-->
```cs
public void ConfigureServices(IServiceCollection services)
{
    ...
    // Add the telemetry initializer middleware
    services.AddSingleton<TelemetryInitializerMiddleware>(sp =>
            {
                var httpContextAccessor = sp.GetService<IHttpContextAccessor>();
                var loggerMiddleware = sp.GetService<TelemetryLoggerMiddleware>();
                return new TelemetryInitializerMiddleware(httpContextAccessor, loggerMiddleware, logActivityTelemetry: false);
            });
    ...
}
```
<!--Original javascript-->
```javascript
// Add telemetry middleware to the adapter middleware pipeline
var telemetryClient = getTelemetryClient(process.env.InstrumentationKey);
var telemetryLoggerMiddleware = new TelemetryLoggerMiddleware(telemetryClient, true);
var initializerMiddleware = new TelemetryInitializerMiddleware(telemetryLoggerMiddleware, true);
adapter.use(initializerMiddleware);
```

<!--Updated javascript ?????-->
```javascript
// Add telemetry middleware to the adapter middleware pipeline
var telemetryClient = getTelemetryClient(process.env.InstrumentationKey);
var telemetryLoggerMiddleware = new TelemetryLoggerMiddleware(telemetryClient, true);
var initializerMiddleware = new TelemetryInitializerMiddleware(telemetryLoggerMiddleware, false);
adapter.use(initializerMiddleware);
```

The addition of `IHttpContextAccessor` will also require that you reference the  Microsoft ASPNetCore HTTP library, which you achieve with the addition of this using statement:

```javascript
using Microsoft.AspNetCore.Http; 
```

### Enable or disable logging personal information

By default, if activity logging is enabled, some properties on the incoming / outgoing activities are excluded from logging as they are likely to contain personal information, such as user name and the activity text. You can choose to include these properties in your logging by making the following change to **Startup.cs** when registering the `TelemetryLoggerMiddleware`.

<!--Original C#-->
```csharp
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

<!--Updated javascript ?????-->
```javascript
// Add telemetry middleware to the adapter middleware pipeline
var telemetryClient = getTelemetryClient(process.env.InstrumentationKey);
var telemetryLoggerMiddleware = new TelemetryLoggerMiddleware(telemetryClient, true);
var initializerMiddleware = new TelemetryInitializerMiddleware(telemetryLoggerMiddleware, true);
adapter.use(initializerMiddleware);
```

Next we will see what needs to be included to add telemetry functionality to the dialogs. This will enable you to get additional information such as what dialogs run, and statistics about each one.
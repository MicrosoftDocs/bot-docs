
### Enabling or disabling Activity logging

By default, the `TelemetryInitializerMiddleware` will use the `TelemetryLoggerMiddleware` to log telemetry when your bot sends / receives activities. Activity logging creates custom event logs in your Application Insights resource.  If you wish, you can disable activity event logging by setting  `logActivityTelemetry` to false on the `TelemetryInitializerMiddleware` when registering it in **Startup.cs**.

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

### Enable or disable logging personal information

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
<!--Updating for Python not yet started - this is just a placeholder-->

## Enabling / disabling activity event and personal information logging

### Enabling or disabling Activity logging

By default, the `TelemetryInitializerMiddleware` will use the `TelemetryLoggerMiddleware` to log telemetry when your bot sends / receives activities. Activity logging creates custom event logs in your Application Insights resource.  If you wish, you can disable activity event logging by setting  `logActivityTelemetry` to false on the `TelemetryInitializerMiddleware` when registering it in **Startup.cs**.

```py

```

The addition of `IHttpContextAccessor` will also require that you reference the  Microsoft ASPNetCore HTTP library, which you achieve with the addition of this using statement:

```py

```

### Enable or disable logging personal information

By default, if activity logging is enabled, some properties on the incoming / outgoing activities are excluded from logging as they are likely to contain personal information, such as user name and the activity text. You can choose to include these properties in your logging by making the following change to **app.py** when registering the `TelemetryLoggerMiddleware`.

```py
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
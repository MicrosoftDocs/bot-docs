---
title: Add telemetry to your bot | Microsoft Docs
description: Learn how to integrate your bot with the new telemetry features.
keywords: telemetry, appinsights, monitor bot
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 05/23/2019
monikerRange: 'azure-bot-service-4.0'
---

# Add telemetry to your bot

[!INCLUDE[applies-to](../includes/applies-to.md)]

In version 4.2 of the Bot Framework SDK, telemetry logging was added into the product.  This enables bot applications to send event data to services such as Application Insights. The first section covers these methods, with more extensive telemetry features after that.

This document covers how to integrate your bot with the new telemetry features. 

## Basic telemetry options

### Basic Application insights

First, let us add basic telemetry to your bot, using Application Insights. For additional information on set up, see the first few sections of [getting started with Application Insights](https://github.com/Microsoft/ApplicationInsights-aspnetcore/wiki/Getting-Started-with-Application-Insights-for-ASP.NET-Core).   

If you want "stock" Application Insights, with no additional Application Insights-specific configuration required (for example Telemetry Initializers), add the following to your `ConfigureServices()` method.   This is the easiest method to initialize and will configure Application Insights to begin tracking Requests, external calls to other services, and correlating events across services.

You'll need to add the NuGet packages included in the snippet below.

**Startup.cs**
```csharp
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Bot.Builder.ApplicationInsights;
using Microsoft.Bot.Builder.Integration.ApplicationInsights.Core;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
 
// This method gets called by the runtime. Use this method to add services to the container.
public void ConfigureServices(IServiceCollection services)
{
    ...
    // Add Application Insights services into service collection
    services.AddApplicationInsightsTelemetry();

    // Add the standard telemetry client
    services.AddSingleton<IBotTelemetryClient, BotTelemetryClient>();

    // Add ASP middleware to store the HTTP body, mapped with bot activity key, in the httpcontext.items
    // This will be picked by the TelemetryBotIdInitializer
    services.AddTransient<TelemetrySaveBodyASPMiddleware>();

    // Add telemetry initializer that will set the correlation context for all telemetry items
    services.AddSingleton<ITelemetryInitializer, OperationCorrelationTelemetryInitializer>();

    // Add telemetry initializer that sets the user ID and session ID (in addition to other 
    // bot-specific properties, such as activity ID)
    services.AddSingleton<ITelemetryInitializer, TelemetryBotIdInitializer>();
    ...
}

// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    ...
    app.UseBotApplicationInsights();
}
```

Then, you'll need to store your Application Insights instrumentation key in your `appsettings.json` file or as an enviroment variable. The `appsettings.json` file contains metadata about external services the Bot uses while running.  For example, CosmosDB, Application Insights and the Language Understanding (LUIS) service connection and metadata is stored here. The instrumentaion key can be found on the Azure portal in the **Overview** section (under your service's `Essentials` drop down on that page, if it's collapsed). Details on getting the keys can be found [here](~/bot-service-resources-app-insights-keys.md).

The framework will find the key for you, if correctly formatted. Formatting of your `appsettings.json` entries should be as follows:

```json
    "ApplicationInsights": {
        "InstrumentationKey": "putinstrumentationkeyhere"
    },
    "Logging": {
        "LogLevel": {
            "Default": "Warning"
        }
    }
```

For more information on adding Application Insights to an ASP.NET Core application, see [this article](https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core-no-visualstudio). 

### Customize your telemetry client

If you want to customize your Application Insights client, or you want to log into a completely separate service, you have to configure the system differently. Download package `Microsoft.Bot.Builder.ApplicationInsights` via nuget, or use npm to install `botbuilder-applicationinsights`. Details on getting the Application Insights keys can be found [here](~/bot-service-resources-app-insights-keys.md).

**Modify Application Insights Configuration**

To modify your configuration, include `options` when adding your Application Insights. Otherwise, everything is the same as above.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    ...
    // Add Application Insights services into service collection
    services.AddApplicationInsightsTelemetry(options);
    ...
}
```

The `options` object is of the type `ApplicationInsightsServiceOptions`. Details on what those options are [can be found here]().

**Use Custom Telemetry**
If you want to log telemetry events generated by the Bot Framework into a completely separate system, create a new class derived from the base interface `IBotTelemetryClient` and configure. Then, when adding your telemetry client as above, just inject your custom client. 

```csharp
public void ConfigureServices(IServiceCollection services)
{
    ...
    // Add the telemetry client.
    services.AddSingleton<IBotTelemetryClient, CustomTelemetryClient>();
    ...
}
```

### Add custom logging to your bot

Once the Bot has the new telemetry logging support configured, you can begin adding telemetry to your bot.  The `BotTelemetryClient`(in C#, `IBotTelemetryClient`) has several methods to log distinct types of events.  Choosing the appropriate type of event enables you to take advantage of Application Insights existing reports (if you are using Application Insights).  For general scenarios `TraceEvent` is typically used.  The data logged using `TraceEvent` lands in the `CustomEvent` table in Kusto.

If using a Dialog within your Bot, every Dialog-based object (including Prompts) will contain a new `TelemetryClient` property.  This is the `BotTelemetryClient` that enables you to perform logging.  This is not just a convenience, we'll see later in this article if this property is set, `WaterfallDialogs` will generate events.

#### Identifiers and Custom Events

When logging events into Application Insights, the events generated contain default properties that you won't have to fill.  For example, `user_id` and `session_id`properties are contained in each Custom Event (generated with the `TraceEvent` API).  In addition, `activitiId`, `activityType` and `channelId` are also added.

>Note: Custom telemetry clients will not be provided these values.

Property |Type | Details
--- | --- | ---
`user_id`| `string` | [ChannelID](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#channel-id) + [From.Id](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#from)
`session_id`| `string`|  [ConversationID](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#conversation)
`customDimensions.activityId`| `string` | [The bot activity ID](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#id)
`customDimensions.activityType` | `string` | [The bot activity type ](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#channel-id)
`customDimensions.channelId` | `string` |  [The bot activity channel ID ](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#channel-id)

## In-depth telemetry

There are three new components added to the SDK in version 4.4.  All components log using the `IBotTelemetryClient`  (or `BotTelemetryClient` in node.js) interface which can be overridden with a custom implementation.

- A  Bot Framework Middleware component (*TelemetryLoggerMiddleware*) that will log when messages are received, sent, updated or deleted. You can override for custom logging.
- *LuisRecognizer* class.  You can override for custom logging in two ways - per invocation (add/replace properties) or derived classes.
- *QnAMaker*  class.  You can override for custom logging in two ways - per invocation (add/replace properties) or derived classes.

### Telemetry Middleware

|C#  | JavaScript |
|:-----|:------------|
|**Microsoft.Bot.Builder.TelemetryLoggerMiddleware** | **botbuilder-core** |

#### Out of box usage

The TelemetryLoggerMiddleware is a Bot Framework component that can be added without modification, and it will peform logging that enables out of the box reports that ship with the Bot Framework SDK. 

```csharp
// Create the telemetry middleware to track conversation events
services.AddSingleton<IMiddleware, TelemetryLoggerMiddleware>();

// Create the Bot Framework Adapter with error handling enabled.
services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();
```

#### Adding properties
If you decide to add additional properties, the TelemetryLoggerMiddleware class can be derived.  For example, if you would like to add the property "MyImportantProperty" to the `BotMessageReceived` event.  `BotMessageReceived` is logged when the user sends a message to the bot.  Adding the additional property can be accomplished in the following way:

```csharp
class MyTelemetryMiddleware : TelemetryLoggerMiddleware
{
    ...
    public Task OnReceiveActivityAsync(
                  Activity activity,
                  CancellationToken cancellation)
    {
        // Fill in the "standard" properties for BotMessageReceived
        // and add our own property.
        var properties = FillReceiveEventProperties(activity, 
                    new Dictionary<string, string>
                    { {"MyImportantProperty", "myImportantValue" } } );
                    
        // Use TelemetryClient to log event
        TelemetryClient.TrackEvent(
                        TelemetryLoggerConstants.BotMsgReceiveEvent,
                        properties);
    }
    ...
}
```

And in Startup, we would add the new class:

```csharp
// Create the telemetry middleware to track conversation events
services.AddSingleton<IMiddleware, MyTelemetryMiddleware>();
```

#### Completely replacing properties / Additional event(s)

If you decide to completely replace properties being logged, the `TelemetryLoggerMiddleware` class can be derived (like above when extending properties).   Similarly, logging new events is performed in the same way.

For example, if you would like to completely replace the`BotMessageSend` properties and send multiple events, the following demonstrates how this could be performed:

```csharp
class MyTelemetryMiddleware : TelemetryLoggerMiddleware
{
    ...
    public Task<RecognizerResult> OnLuisRecognizeAsync(
                  Activity activity,
                  string dialogId = null,
                  CancellationToken cancellation)
    {
        // Override properties for BotMsgSendEvent
        var botMsgSendProperties = new Dictionary<string, string>();
        properties.Add("MyImportantProperty", "myImportantValue");
        // Log event
        TelemetryClient.TrackEvent(
                        TelemetryLoggerConstants.BotMsgSendEvent,
                        botMsgSendProperties);
                        
        // Create second event.
        var secondEventProperties = new Dictionary<string, string>();
        secondEventProperties.Add("activityId",
                                   activity.Id);
        secondEventProperties.Add("MyImportantProperty",
                                   "myImportantValue");
        TelemetryClient.TrackEvent(
                        "MySecondEvent",
                        secondEventProperties);
    }
    ...
}
```
Note: When the standard properties are not logged, it will cause the out of box reports shipped with the product to stop working.

#### Events Logged from Telemetry Middleware
[BotMessageSend](#customevent-botmessagesend)
[BotMessageReceived](#customevent-botmessagereceived)
[BotMessageUpdate](#customevent-botmessageupdate)
[BotMessageDelete](#customevent-botmessagedelete)

### Telemetry support LUIS 

|C#  | JavaScript |
|:-----|:------------|
| **Microsoft.Bot.Builder.AI.Luis** | **botbuilder-ai** |

#### Out of box usage
The LuisRecognizer is an existing Bot Framework component, and telemetry can be enabled by passing a IBotTelemetryClient interface through `luisOptions`.  You can override the default properties being logged and log new events as required.

During construction of `luisOptions`, an `IBotTelemetryClient` object must be provided for this to work.

```csharp
var luisOptions = new LuisPredictionOptions(
      ...
      telemetryClient,
      false); // Log personal information flag. Defaults to false.

var client = new LuisRecognizer(luisApp, luisOptions);
```

#### Adding properties
If you decide to add additional properties, the `LuisRecognizer` class can be derived.  For example, if you would like to add the property "MyImportantProperty" to the `LuisResult` event.  `LuisResult` is logged when a LUIS prediction call is performed.  Adding the additional property can be accomplished in the following way:

```csharp
class MyLuisRecognizer : LuisRecognizer 
{
   ...
   override protected Task OnRecognizerResultAsync(
           RecognizerResult recognizerResult,
           ITurnContext turnContext,
           Dictionary<string, string> properties = null,
           CancellationToken cancellationToken = default(CancellationToken))
   {
       var luisEventProperties = FillLuisEventProperties(result, 
               new Dictionary<string, string>
               { {"MyImportantProperty", "myImportantValue" } } );
        
        TelemetryClient.TrackEvent(
                        LuisTelemetryConstants.LuisResultEvent,
                        luisEventProperties);
        ..
   }    
   ...
}
```

#### Add properties per invocation
Sometimes it's necessary to add additional properties during the invocation:
```csharp
var additionalProperties = new Dictionary<string, string>
{
   { "dialogId", "myDialogId" },
   { "conversationInfo", "myConversationInfo" },
};

var result = await recognizer.RecognizeAsync(turnContext,
     additionalProperties,
     CancellationToken.None).ConfigureAwait(false);
```

#### Completely replacing properties / Additional event(s)
If you decide to completely replace properties being logged, the `LuisRecognizer` class can be derived (like above when extending properties).   Similarly, logging new events is performed in the same way.

For example, if you would like to completely replace the`LuisResult` properties and send multiple events, the following demonstrates how this could be performed:

```csharp
class MyLuisRecognizer : LuisRecognizer
{
    ...
    override protected Task OnRecognizerResultAsync(
             RecognizerResult recognizerResult,
             ITurnContext turnContext,
             Dictionary<string, string> properties = null,
             CancellationToken cancellationToken = default(CancellationToken))
    {
        // Override properties for LuisResult event
        var luisProperties = new Dictionary<string, string>();
        properties.Add("MyImportantProperty", "myImportantValue");
        
        // Log event
        TelemetryClient.TrackEvent(
                        LuisTelemetryConstants.LuisResult,
                        luisProperties);
                        
        // Create second event.
        var secondEventProperties = new Dictionary<string, string>();
        secondEventProperties.Add("MyImportantProperty2",
                                   "myImportantValue2");
        TelemetryClient.TrackEvent(
                        "MySecondEvent",
                        secondEventProperties);
        ...
    }
    ...
}
```
Note: When the standard properties are not logged, it will cause the Application Insights out of box reports shipped with the product to stop working.

#### Events Logged from TelemetryLuisRecognizer
[LuisResult](#customevent-luisevent)

### Telemetry QnA Recognizer

|C#  | JavaScript |
|:-----|:------------|
| **Microsoft.Bot.Builder.AI.QnA** | **botbuilder-ai** |


#### Out of box usage
The QnAMaker class is an existing Bot Framework component that adds two additional constructor parameters which enable logging that enable out of the box reports that ship with the Bot Framework SDK. The new `telemetryClient` references a `IBotTelemetryClient` interface which performs the logging.  

```csharp
var qna = new QnAMaker(endpoint, options, client, 
                       telemetryClient: telemetryClient,
                       logPersonalInformation: true);
```
#### Adding properties 
If you decide to add additional properties, there are two methods of doing this - when properties need to be added during the QnA call to retrieve answers or deriving from the `QnAMaker` class.  

The following demonstrates deriving from the `QnAMaker` class.  The example shows adding the property "MyImportantProperty" to the `QnAMessage` event.  The`QnAMessage` event is logged when a QnA `GetAnswers`call is performed.  In addition, we log a second event "MySecondEvent".

```csharp
class MyQnAMaker : QnAMaker 
{
   ...
   protected override Task OnQnaResultsAsync(
                 QueryResult[] queryResults, 
                 ITurnContext turnContext, 
                 Dictionary<string, string> telemetryProperties = null, 
                 Dictionary<string, double> telemetryMetrics = null, 
                 CancellationToken cancellationToken = default(CancellationToken))
   {
            var eventData = await FillQnAEventAsync(queryResults, turnContext, telemetryProperties, telemetryMetrics, cancellationToken).ConfigureAwait(false);

            // Add my property
            eventData.Properties.Add("MyImportantProperty", "myImportantValue");

            // Log QnaMessage event
            TelemetryClient.TrackEvent(
                            QnATelemetryConstants.QnaMsgEvent,
                            eventData.Properties,
                            eventData.Metrics
                            );

            // Create second event.
            var secondEventProperties = new Dictionary<string, string>();
            secondEventProperties.Add("MyImportantProperty2",
                                       "myImportantValue2");
            TelemetryClient.TrackEvent(
                            "MySecondEvent",
                            secondEventProperties);       }    
    ...
}
```

#### Adding properties during GetAnswersAsync
If you have properties that need to be added during runtime, the `GetAnswersAsync` method can provide properties and/or metrics to add to the event.

For example, if you want to add a `dialogId` to the event, it can be done like the following:
```csharp
var telemetryProperties = new Dictionary<string, string>
{
   { "dialogId", myDialogId },
};

var results = await qna.GetAnswersAsync(context, opts, telemetryProperties);
```
The `QnaMaker` class provides the capability of overriding properties, including PersonalInfomation properties.

#### Completely replacing properties / Additional event(s)
If you decide to completely replace properties being logged, the `TelemetryQnAMaker` class can be derived (like above when extending properties).   Similarly, logging new events is performed in the same way.

For example, if you would like to completely replace the`QnAMessage` properties, the following demonstrates how this could be performed:

```csharp
class MyLuisRecognizer : TelemetryQnAMaker
{
    ...
    protected override Task OnQnaResultsAsync(
         QueryResult[] queryResults, 
         ITurnContext turnContext, 
         Dictionary<string, string> telemetryProperties = null, 
         Dictionary<string, double> telemetryMetrics = null, 
         CancellationToken cancellationToken = default(CancellationToken))
    {
        // Add properties from GetAnswersAsync
        var properties = telemetryProperties ?? new Dictionary<string, string>();
        // GetAnswerAsync properties overrides - don't add if already present.
        properties.TryAdd("MyImportantProperty", "myImportantValue");

        // Log event
        TelemetryClient.TrackEvent(
                           QnATelemetryConstants.QnaMsgEvent,
                            properties);
    }
    ...
}
```
Note: When the standard properties are not logged, it will cause the out of box reports shipped with the product to stop working.

#### Events Logged from TelemetryLuisRecognizer
[QnAMessage](#customevent-qnamessage)


## WaterfallDialog events

In addition to generating your own events, the `WaterfallDialog` object within the SDK now generates events. The following section describes the events generated from within the Bot Framework. By setting the `TelemetryClient` property on the `WaterfallDialog` these events will be stored.

Here's an example of modifying a sample (CoreBot) which employs the `WaterfallDialog`, to log telemetry events.  CoreBot employs a common pattern used where a `WaterfallDialog` is placed within a `ComponentDialog` (`GreetingDialog`).

```csharp
// IBotTelemetryClient is direct injected into our Bot
public CoreBot(BotServices services, UserState userState, ConversationState conversationState, IBotTelemetryClient telemetryClient)
...

// The IBotTelemetryClient passed to the GreetingDialog
...
Dialogs = new DialogSet(_dialogStateAccessor);
Dialogs.Add(new GreetingDialog(_greetingStateAccessor, telemetryClient));
...

// The IBotTelemetryClient to the WaterfallDialog
...
AddDialog(new WaterfallDialog(ProfileDialog, waterfallSteps) { TelemetryClient = telemetryClient });
...

```

Once the `WaterfallDialog` has a configured `IBotTelemetryClient`, it will begin logging events.

## Events generated by the Bot Framework Service

In addition to `WaterfallDialog`, which generates events from your bot code, the Bot Framework Channel service also logs events.  This helps you diagnose issues with Channels or overall bot failures.

### CustomEvent: "Activity"
**Logged From:** Channel Service
Logged by the Channel Service when a message received.

### Exception: "Bot Errors"
**Logged From:** Channel Service
Logged by the channel when a call to the Bot returns a non-2XX Http Response.

## All events generated

### CustomEvent: "WaterfallStart" 

When a WaterfallDialog begins, a `WaterfallStart` event is logged.

- `user_id`  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `session_id` ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.activityId`  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.activityType`  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.channelId` ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.DialogId` (This is the dialogId (string) passed into your Waterfall.  You can consider this the "waterfall type")
- `customDimensions.InstanceID` (unique per instance of the dialog)

### CustomEvent: "WaterfallStep" 

Logs individual steps from a Waterfall Dialog.

- `user_id`  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `session_id` ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.activityId`  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.activityType`  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.channelId` ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.DialogId` (This is the dialogId (string) passed into your Waterfall.  You can consider this the "waterfall type")
- `customDimensions.StepName` (either method name or `StepXofY` if lambda)
- `customDimensions.InstanceID` (unique per instance of the dialog)

### CustomEvent: "WaterfallDialogComplete"

Logs when a Waterfall Dialog completes.

- `user_id`  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `session_id` ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.activityId`  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.activityType`  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.channelId` ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.DialogId` (This is the dialogId (string) passed into your Waterfall.  You can consider this the "waterfall type")
- `customDimensions.InstanceID` (unique per instance of the dialog)

### CustomEvent: "WaterfallDialogCancel" 

Logs when a Waterfall Dialog is canceled.

- `user_id`  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `session_id` ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.activityId`  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.activityType`  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.channelId` ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- `customDimensions.DialogId` (This is the dialogId (string) passed into your Waterfall.  You can consider this the "waterfall type")
- `customDimensions.StepName` (either method name or `StepXofY` if lambda)
- `customDimensions.InstanceID` (unique per instance of the dialog)

### CustomEvent: BotMessageReceived 
Logged when bot receives new message from a user.

When not overridden, this event is logged from `Microsoft.Bot.Builder.TelemetryLoggerMiddleware` using the `Microsoft.Bot.Builder.IBotTelemetry.TrackEvent()` method.

- Session Identifier  
  - When using Application Insights, this is logged from the `TelemetryBotIdInitializer`  as the  **session** identifier (*Temeletry.Context.Session.Id*) used within Application Insights.  
  - Corresponds to the [Conversation ID](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#conversation) as defined by Bot Framework protocol..
  - The property name logged is `session_id`.

- User Identifier
  - When using Application Insights, this is logged from the `TelemetryBotIdInitializer`  as the  **user**  identifier (*Telemetry.Context.User.Id*) used within Application Insights.  
  - The value of this property is a combination of the [Channel Identifier](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#channel-id) and the [User ID](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#from) (concatenated together) properties as defined by the Bot Framework protocol.
  - The property name logged is `user_id`.

- ActivityID 
  - When using Application Insights, this is logged from the `TelemetryBotIdInitializer` as a Property to the event.
  - Corresponds to the [Activity ID](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#Id) as defined by Bot Framework protocol..
  - The property name is `activityId`.

- Channel Identifier
  - When using Application Insights, this is logged from the `TelemetryBotIdInitializer` as a Property to the event.  
  - Corresponds to the [Channel Identifier](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#id) of the Bot Framework protocol.
  - The property name logged is `channelId`.

- ActivityType 
  - When using Application Insights, this is logged from the `TelemetryBotIdInitializer` as a Property to the event.  
  - Corresponds to the [Activity Type](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#type) of the Bot Framework protocol.
  - The property name logged is `activityType`.

- Text
  - **Optionally** logged when the `logPersonalInformation` property is set to `true`.
  - Corresponds to the [Activity Text](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#text) field of the Bot Framework protocol.
  - The property name logged is `text`.

- Speak

  - **Optionally** logged when the `logPersonalInformation` property is set to `true`.
  - Corresponds to the [Activity Speak](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#speak) field of the Bot Framework protocol.
  - The property name logged is `speak`.

  - 

- FromId
  - Corresponds to the [From Identifier](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#from) field of the Bot Framework protocol.
  - The property name logged is `fromId`.

- FromName
  - **Optionally** logged when the `logPersonalInformation` property is set to `true`.
  - Corresponds to the [From Name](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#from) field of the Bot Framework protocol.
  - The property name logged is `fromName`.

- RecipientId
  - Corresponds to the [From Name](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#from) field of the Bot Framework protocol.
  - The property name logged is `fromName`.

- RecipientName
  - Corresponds to the [From Name](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#from) field of the Bot Framework protocol.
  - The property name logged is `fromName`.

- ConversationId
  - Corresponds to the [From Name](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#from) field of the Bot Framework protocol.
  - The property name logged is `fromName`.

- ConversationName
  - Corresponds to the [From Name](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#from) field of the Bot Framework protocol.
  - The property name logged is `fromName`.

- Locale
  - Corresponds to the [From Name](https://github.com/Microsoft/BotBuilder/blob/master/specs/botframework-activity/botframework-activity.md#from) field of the Bot Framework protocol.
  - The property name logged is `fromName`.

### CustomEvent: BotMessageSend 
**Logged From:** TelemetryLoggerMiddleware 

Logged when bot sends a message.

- UserID  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- SessionID ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- ActivityID  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- Channel  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- ActivityType  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- ReplyToID
- RecipientId
- ConversationName
- Locale
- RecipientName (Optional for PII)
- Text (Optional for PII)
- Speak (Optional for PII)


### CustomEvent: BotMessageUpdate
**Logged From:** TelemetryLoggerMiddleware
Logged when a message is updated by the bot (rare case)
- UserID  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- SessionID ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- ActivityID ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- Channel  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- ActivityType  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- RecipientId
- ConversationId
- ConversationName
- Locale
- Text (Optional for PII)


### CustomEvent: BotMessageDelete
**Logged From:** TelemetryLoggerMiddleware
Logged when a message is deleted by the bot (rare case)
- UserID  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- SessionID ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- ActivityID  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- Channel  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- ActivityType  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- RecipientId
- ConversationId
- ConversationName

### CustomEvent: LuisEvent
**Logged From:** LuisRecognizer

Logs results from LUIS service.

- UserID  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- SessionID ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- ActivityID ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- Channel ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- ActivityType ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- ApplicationId
- Intent
- IntentScore
- Intent2 
- IntentScore2 
- FromId
- SentimentLabel
- SentimentScore
- Entities (as json)
- Question (Optional for PII)

## CustomEvent: QnAMessage
**Logged From:** QnAMaker

Logs results from QnA Maker service.

- UserID ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- SessionID ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- ActivityID ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- Channel ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- ActivityType  ([From Telemetry Initializer](https://aka.ms/telemetry-initializer))
- Username (Optional for PII)
- Question (Optional for PII)
- MatchedQuestion
- QuestionId
- Answer
- Score
- ArticleFound

## Querying the data
When using Application Insights, all the data is correlated together (even across services).  We can see by querying a successful request and see all the associated events for that request.  
The following queries will tell you the most recent requests:
```sql
requests 
| where timestamp > ago(3d) 
| where resultCode == 200
| order by timestamp desc
| project timestamp, operation_Id, appName
| limit 10
```

From the first query, select a few `operation_Id`'s and then look for more information:

```sql
let my_operation_id = "<OPERATION_ID>";
let union_all = () {
    union
    (traces | where operation_Id == my_operation_id),
    (customEvents | where operation_Id == my_operation_id),
    (requests | where operation_Id == my_operation_id),
    (dependencies | where operation_Id  == my_operation_id),
    (exceptions | where operation_Id == my_operation_id)
};
union_all
    | order by timestamp asc
    | project itemType, name, performanceBucket
```

This will give you the chronological breakdown of a single request, with the duration bucket of each call.
![Example Call](media/performance_query.png)

> Note: The "Activity" `customEvent` event timestamp is out of order, since these events are logged asynchronously.

## Create a dashboard

The easiest way to test is by creating a dashboard using [Azure portal's template deployment page](https://portal.azure.com/#create/Microsoft.Template).  
- Click ["Build your own template in the editor"]
- Copy and paste either one of these .json file that is provided to help you create the dashboard:
  - [System Health Dashboard](https://aka.ms/system-health-appinsights)
  - [Conversation Health Dashboard](https://aka.ms/conversation-health-appinsights)
- Click "Save"
- Populate `Basics`: 
   - Subscription: <your test subscription>
   - Resource group: <a test resource group>
   - Location: <such as West US>
- Populate `Settings`:
   - Insights Component Name: <like `core672so2hw`>
   - Insights Component Resource Group: <like `core67`>
   - Dashboard Name:  <like `'ConversationHealth'` or `SystemHealth`>
- Click `I agree to the terms and conditions stated above`
- Click `Purchase`
- Validate
   - Click on [`Resource Groups`](https://ms.portal.azure.com/#blade/HubsExtension/Resources/resourceType/Microsoft.Resources%2Fsubscriptions%2FresourceGroups)
   - Select your Resource Group from above (like `core67`).
   - If you don't see a new Resource, then look at "Deployments" and see if any have failed.
   - Here's what you typically see for failures:
     
```json
{"code":"DeploymentFailed","message":"At least one resource deployment operation failed. Please list deployment operations for details. Please see https://aka.ms/arm-debug for usage details.","details":[{"code":"BadRequest","message":"{\r\n \"error\": {\r\n \"code\": \"InvalidTemplate\",\r\n \"message\": \"Unable to process template language expressions for resource '/subscriptions/45d8a30e-3363-4e0e-849a-4bb0bbf71a7b/resourceGroups/core67/providers/Microsoft.Portal/dashboards/Bot Analytics Dashboard' at line '34' and column '9'. 'The template parameter 'virtualMachineName' is not found. Please see https://aka.ms/arm-template/#parameters for usage details.'\"\r\n }\r\n}"}]}
```

To view the data, go to the Azure portal. Click **Dashboard** on the left, then select the dashboard you created from the drop-down.

## Additional resources
You can refer to these samples that implement telemetry:
- C#
  - [LUIS with AppInsights](https://aka.ms/luis-with-appinsights-cs)
  - [QnA with AppInsights](https://aka.ms/qna-with-appinsights-cs)
- JS
  - [LUIS with AppInsights](https://aka.ms/luis-with-appinsights-js)
  - [QnA with AppInsights](https://aka.ms/qna-with-appinsights-js)


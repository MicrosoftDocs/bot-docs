---
title: Connect a bot to Twilio - Bot Service
description: Learn how to configure a bot's connection to Twilio.
keywords: Twilio, bot channels, SMS, App, phone, configure Twilio, cloud communication, text
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 10/9/2018
---

# Connect a bot to Twilio

You can configure your bot to communicate with people using the Twilio cloud communication platform.

## Log in to or create a Twilio account for sending and receiving SMS messages

If you don't have a Twilio account, <a href="https://www.twilio.com/try-twilio" target="_blank">create a new account</a>.

## Create a TwiML Application

<a href="https://support.twilio.com/hc/articles/223180928-How-Do-I-Create-a-TwiML-App-" target="_blank">Create a TwiML application</a> following the instructions.

![Create app](~/media/channels/twi-StepTwiml.png)

Under **Properties**, enter a **FRIENDLY NAME**. In this tutorial we use "My TwiML app" as an example. The **REQUEST URL** under Voice can be left empty. Under **Messaging**, the **Request URL** should be `https://sms.botframework.com/api/sms`.

## Select or add a phone number

Follow the instructions <a href = "https://support.twilio.com/hc/articles/223180048-Adding-a-Verified-Phone-Number-or-Caller-ID-with-Twilio" target="_blank">here</a> to add a verified caller ID via the console site. After you finish, you will see your verified number in **Active Numbers** under **Manage Numbers**.

![Set phone number](~/media/channels/twi-StepPhone.png)

## Specify application to use for Voice and Messaging

Click the number and go to **Configure**. Under both Voice and Messaging, set **CONFIGURE WITH** to be TwiML App and set **TWIML APP** to be My TwiML app. After you finish, click **Save**.

![Specify application](~/media/channels/twi-StepPhone2.png)

Go back to **Manage Numbers**, you will see the configuration of both Voice and Messaging are changed to TwiML App.

![Specified number](~/media/channels/twi-StepPhone3.png)


## Gather credentials

Go back to the [console homepage](https://www.twilio.com/console/), you will see your Account SID and Auth Token on the project dashboard, as shown below.

![Gather app credentials](~/media/channels/twi-StepAuth.png)

## Submit credentials

In a separate window, return to the Bot Framework site at https://dev.botframework.com/. 

- Select **My bots** and choose the Bot that you want to connect to Twilio. This will direct you to the Azure portal.
- Select **Channels** under **Bot Management**. Click the Twilio (SMS) icon.
- Enter the Phone Number, Account SID, and Auth Token you record earlier. After you finish, click **Save**.

![Submit credentials](~/media/channels/twi-StepSubmit.png)

When you have completed these steps, your bot will be successfully configured to communicate with users using Twilio.

## Connect a bot to Twilio using the Twilio adapter

As well as the channel available in the Azure Bot Service to connect your bot with Twilio, you can also use the Twilio adapter. In this article you will learn how to connect a bot to Twilio using the adapter.  This article will walk you through modifying the EchoBot sample to connect it to Twilio.

> [!NOTE]
> The instructions below cover the C# implementation of the Twilio adapter. For instructions on using the JS adapter, part of the BotKit libraries, [see the BotKit Twilio documentation](https://botkit.ai/docs/v4/platforms/twilio.html).

### Prerequisites

* The [EchoBot sample code](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/02.echo-bot)

* A Twilio account. If you do not have a Twilio account, you can [create one here](https://www.twilio.com/try-twilio).

### Get a Twilio number and gather account credentials

1. Log into [Twilio](https://twilio.com/console). On the right hand side of the page you will see the **ACCOUNT SID** and **AUTH TOKEN** for your account, make a note of these as you will need them later when configuring your bot application.

2. Choose **Programmable Voice** from the options under **Get Started with Twilio**.

![Get started with Programmable Voice](~/media/bot-service-channel-connect-twilio/get-started-voice.png)

3. On the next page, click the **Get your first Twilio number** button.  A pop-up window will show you a new number, which you can accept by clicking **Choose this number** (alternatively you can search for a different number by following the on screen instructions).

4. Once you have chosen your number, make a note of it, as you will need this when configuring your bot application in a later step.

### Wiring up the Twilio adapter in your bot

Now that you have your Twilio number and account credentials, you need to configure your bot application.

#### Install the Twilio adapter NuGet package

Add  the [Microsoft.Bot.Builder.Adapters.Twilio](https://www.nuget.org/packages/Microsoft.Bot.Builder.Adapters.Twilio/) NuGet package. For more information on using NuGet, see [Install and manage packages in Visual Studio](https://aka.ms/install-manage-packages-vs).

#### Create a Twilio adapter class

Create a new class that inherits from the ***TwilioAdapter*** class. This class will act as our adapter for the Twilio channel and include error handling capabilities (similar to the ***BotFrameworkAdapterWithErrorHandler*** class already in the sample, used for handling other requests from Azure Bot Service).

```csharp
public class TwilioAdapterWithErrorHandler : TwilioAdapter
{
    public TwilioAdapterWithErrorHandler(IConfiguration configuration, ILogger<BotFrameworkHttpAdapter> logger)
            : base(configuration, logger)
        {
            OnTurnError = async (turnContext, exception) =>
            {
                // Log any leaked exception from the application.
                logger.LogError(exception, $"[OnTurnError] unhandled error : {exception.Message}");

                // Send a message to the user
                await turnContext.SendActivityAsync("The bot encountered an error or bug.");
                await turnContext.SendActivityAsync("To continue to run this bot, please fix the bot source code.");

                // Send a trace activity, which will be displayed in the Bot Framework Emulator
                await turnContext.TraceActivityAsync("OnTurnError Trace", exception.Message, "https://www.botframework.com/schemas/error", "TurnError");
            };
        }
}
```

#### Create a new controller for handling Twilio requests

Create a new controller which will handle requests from Twilio, on a new endpoing 'api/twilio' instead of the default 'api/messages' used for requests from Azure Bot Service Channels.  By adding an additional endpoint to your bot, you can accept requests from Bot Service channels, as well as from Twilio, using the same bot.

```csharp
[Route("api/twilio")]
[ApiController]
public class TwilioController : ControllerBase
{
    private readonly TwilioAdapter _adapter;
    private readonly IBot _bot;

    public TwilioController(TwilioAdapter adapter, IBot bot)
    {
        _adapter = adapter;
        _bot = bot;
    }

    [HttpPost]
    [HttpGet]
    public async Task PostAsync()
    {
        // Delegate the processing of the HTTP POST to the adapter.
        // The adapter will invoke the bot.
        await _adapter.ProcessAsync(Request, Response, _bot);
    }
}
```

#### Inject the Twilio adapter in your bot startup.cs

Add the following line to the ***ConfigureServices*** method within your startup.cs file. This will register your Twilio adapter and make it available for your new controller class.  The configuration settings you added in the previous step will be automatically used by the adapter.

```csharp
services.AddSingleton<TwilioAdapter, TwilioAdapterWithErrorHandler>();
```

Once added, your ***ConfigureServices*** method shold look like this.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

    // Create the default Bot Framework Adapter (used for Azure Bot Service channels and emulator).
    services.AddSingleton<IBotFrameworkHttpAdapter, BotFrameworkAdapterWithErrorHandler>();

    // Create the Twilio Adapter
    services.AddSingleton<TwilioAdapter, TwilioAdapterWithErrorHandler>();

    // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
    services.AddTransient<IBot, EchoBot>();
}
```

#### Obtain a URL for your bot

Now that you have wired up the adapter in your bot project, you need to identify the correct endpoint to provide to Twilio in order to ensure your bot receives messages. You also require this URL to complete configuration of your bot application.

To complete this step, [deploy your bot to Azure](https://aka.ms/bot-builder-deploy-az-cli) and make a note of the URL of your deployed bot.

> [!NOTE]
> If you are not ready to deploy your bot to Azure, or wish to debug your bot when using the Twilio adapter, you can use a tool such as [ngrok](https://www.ngrok.com) (which you will likely already have installed if you have used the Bot Framework emulator previously) to tunnel through to your bot running locally and provide you with a publicly accessible URL for this. 
> 
> If you wish create an ngrok tunnel and obtain a URL to your bot, use the following command in a terminal window (this assumes your local bot is running on port 3978, alter the port numbers in the command if your bot is not).
> 
> ```
> ngrok.exe http 3978 -host-header="localhost:3978"
> ```

#### Add Twilio app settings to your bot's configuration file

Add the settings shown below to your appSettings.json file in your bot project. You populate **TwilioNumber**, **TwilioAccountSid** and **TwilioAuthToken** using the values you gathered when creating your Twilio number. **TwilioValidationUrl** should be your bot's URL, plus the `api/twilio` endpoint you specified in your newly created controller. For example, `https://yourboturl.com/api/twilio`.


```json
  "TwilioNumber": "",
  "TwilioAccountSid": "",
  "TwilioAuthToken": "",
  "TwilioValidationUrl", ""
```

Once you have populated the settings above, you should redeploy (or restart if running locally with ngrok) your bot.

### Complete configuration of your Twilio number

The final step is to configure your new Twilio number's messaging endpoint, to ensure your bot receives messages.

1. Navigate to the Twilio [Active Numbers page](https://www.twilio.com/console/phone-numbers/incoming).

2. Click the phone number you created in the earlier step.

3. Within the **Messaging** section, complete the **A MESSAGE COMES IN** section by chooisng **Webhook** from the drop down and populating the text box with your bot's endpoint that you used to populate the **TwilioValidationUrl** setting in the previous step, such as `https://yourboturl.com/api/twilio`.

![Configure Twilio number webhook](~/media/bot-service-channel-connect-twilio/twilio-number-messaging-settings.png)

4. Click the **Save** button.

### Test your bot with adapter in Twilio

You can now test whether your bot is connected to Twilio correctly by sending an SMS message to your Twilio number.  Once the message is received by your bot it will send a message back to you, echoing the text from your message.

You can also test this feature using the [sample bot for the Twilio adapter](https://aka.ms/csharp-63-twilio-adapter-sample) by populating the appSettings.json with the same values described in the steps above.

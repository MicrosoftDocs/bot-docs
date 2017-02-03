---
layout: page
title: Skype Calling Bot Tutorial
permalink: /en-us/csharp/builder/calling-tutorial/
weight: 2520
parent1: Building your Bot Using Bot Builder for .NET
parent2: Skype Calling Bots
---

<span style="color:red">This needs to be under Bot Framework, .NET, skype calling bots</span>

This section walks you through the steps of building a Calling Bot by using the Bot Builder Calling SDK.

### Create an ASP.NET web application   

When you select a template, select the Empty template. In the same window, choose **Web API** option from **Add folders and core references** menu.

### Configure your bot

The web.config file of your project contains the settings. Set the **appSettings** settings as follows:

{% highlight xml %}
<configuration>
  <appSettings>
    <add key="Microsoft.Bot.Builder.Calling.CallbackUrl" value="https://put your service URL here/api/calling/callback" />
  </appSettings>
</configuration>
{% endhighlight %}

### Add the Microsoft.Bot.Builder.Calling NuGet package

The Bot Builder Calling SDK is available as a NuGet package. To get the SDK:

1. Right-click on your project and select **Manage NuGet Packages**  
2. In the **Browse** tab, type "Microsoft.Bot.Builder.Calling"
3. Click the **Install** button and accept the changes

At this point, the builder is installed and you may begin using it.

### Create the controller class

Create a controller class that derives from the **ApiController** class. Add the `BotAuthentication` class attribute to your class. The `BotAuthentication` attribute is used to validate your bot's credentials over HTTPS.

In the controller's constructor, register the factory method that creates your bot with the CallingConversation module.

Set the Route attribute for the following methods in Controllers\CallingController.cs

-   **ProcessIncomingCallAsync**: The route depends on the calling URL that you specified during registration. For example, if you specified https://ivrtest.azurewebsites.net/api/calling/call, the route would be **api/calling/call**. The example sets the RoutePrefix to **api/calling** and the Route attribute to **call**. 
-   **ProcessCallingEventAsync**: The route needs to match the callback URL specified in the web.config file. For example, if you set the callback URL in the configuration file to https://ivrtest.azurewebsites.net/api/calling/callback, the route would be **api/calling/callback**. The example sets the RoutePrefix to **api/calling** and the Route attribute to **callback**. 

Instead of specifying the RoutePrefix class attribute and the Route method attributes, the example could have simply specified the Route method attributes. For example, the Route attribute for the **ProcessIncomingCallAsync** method could have been set to **api/calling/call**, and the Route attribute for the **ProcessCallingEventAsync** method could have been set to **api/calling/callback**.

The following example shows a simple controller class.

{% highlight csharp %}

using Microsoft.Bot.Builder.Calling;
using Microsoft.Bot.Connector;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Microsoft.Bot.Sample.SimpleIVRBot
{
    [BotAuthentication]
    [RoutePrefix("api/calling")]
    public class CallingController : ApiController
    {
        public CallingController() : base()
        {
            CallingConversation.RegisterCallingBot(c => new SimpleIVRBot(c));
        }

        [Route("callback")]
        public async Task<HttpResponseMessage> ProcessCallingEventAsync()
        {
            return await CallingConversation.SendAsync(Request, CallRequestType.CallingEvent);
        }

        [Route("call")]
        public async Task<HttpResponseMessage> ProcessIncomingCallAsync()
        {
            return await CallingConversation.SendAsync(Request, CallRequestType.IncomingCall);
        }

    }
}

{% endhighlight %}


### Define text messages for the menus

Next, define the text messages that will be read and used for the menu prompts.

{% highlight csharp %}

public static class IvrOptions
{
    internal const string WelcomeMessage = "Hello, you have successfully contacted XY internet service provider.";
    internal const string MainMenuPrompt = "If you are a new client press 1, for technical support press 2, if you need information about payments press 3, to hear more about the company press 4. To repeat the options press 5.";
    internal const string NewClientPrompt = "To check our latest offer press 1, to order a new service press 2. Press the hash key to return to the main menu";
    internal const string SupportPrompt = "To check our current outages press 1, to contact the technical support consultant press 2. Press the hash key to return to the main menu";
    internal const string PaymentPrompt = "To get the payment details press 1, press 2 if your payment is not visible in the system. Press the hash key to return to the main menu";
    internal const string MoreInfoPrompt = "XY is the leading Internet Service Provider in Prague. Our company was established in 1995 and currently has 2000 employees.";
    internal const string NoConsultants = "Unfortunately there are no consultants available at this moment. Please leave your name, and a brief message after the signal. You can press the hash key when finished. We will call you as soon as possible.";
    internal const string Ending = "Thank you for leaving the message, goodbye";
    internal const string Offer = "You can sign up for 100 megabit connection just for 10 euros per month till the end of month";
    internal const string CurrentOutages = "There is currently 1 outage in Prague 5, we are working on fixing the issue";
    internal const string PaymentDetailsMessage = "You should do the wire transfer till the 5th day of month to account number 3983815";
} 

{% endhighlight %}

<span style="color:red">Does CurrentOutages contain a typo? "currently 1 outage in Prague 5"</span>

<span style="color:red">The wording for Offer and PaymentDetailsMessage seem odd.</span>


### Implementing the main bot class

The following example shows the implementation of the SimpleIVRBotService class that the controller previously registered as the calling bot. The SimpleIVRBotService class needs to accept IBotService as one of its arguments and implement the ICallingBot interface. The class also registers delegates for handling call actions.

<span style="color:red">The IBotService and ICallingBot interfaces don't match what's in the below example. Shouldn't IBotService be ICallingBotService? Shouldn't ICallingBot be ICallingBotService?</span>

{% highlight csharp %}

public ICallingBotService CallingBotService { get; private set; }

public SimpleIVRBot(ICallingBotService callingBotService)
{
    if (callingBotService == null)
        throw new ArgumentNullException(nameof(callingBotService));
        
    CallingBotService = callingBotService;

    CallingBotService.OnIncomingCallReceived += OnIncomingCallReceived;
    CallingBotService.OnPlayPromptCompleted += OnPlayPromptCompleted;
    CallingBotService.OnRecordCompleted += OnRecordCompleted;
    CallingBotService.OnRecognizeCompleted += OnRecognizeCompleted;
    CallingBotService.OnHangupCompleted += OnHangupCompleted;
}

{% endhighlight %}

The following constants map the user's DTMF choices to actions. The IvrOptions class above contains the list of choices that the user can make. For example, the MainMenuPrompt prompt contains four choices that map to the NewClient, Support, Payments, and MoreInfo contants below. The remaining constants define the choices for the second level menus. For example, if the user presses 3 for payment information, the PaymentPrompt prompt specifies two choices that map to PaymentDetails and PaymentNotVisible.

<span style="color:red">What's DTMF? Is it Dual-tone multi-frequency signaling?</span>

{% highlight csharp %}

private const string NewClient = "1";
private const string Support = "2";
private const string Payments = "3";
private const string MoreInfo = "4";
private const string NewClientOffer = "1";
private const string NewClientOrder = "2";
private const string SupportOutages = "1";
private const string SupportConsultant = "2";
private const string PaymentDetails = "1";
private const string PaymentNotVisible = "2";

{% endhighlight %}

During the call the bot needs to remember the choices that the user has made. This example will save the state of the main menu selection. For example, if the user chooses the Payments Support menu (presses 3) and then presses 1 for payment details, the bot knows that the user wants to reach the *PaymentDetails* section. The example uses the following helper class to keep the state.

<span style="color:red">This is confusing: we say that we want to keep track of the main menu choice but then we also talk about the submenu choice - which is it?</span>


{% highlight csharp %}
private class CallState
{
    public string InitiallyChosenMenuOption { get; set; }
}
{% endhighlight %}

The example uses a dictionary to keep the state information, and uses the Call ID as the key.

{% highlight csharp %}
private readonly Dictionary<string, CallState> _callStateMap = new Dictionary<string, CallState>();
{% endhighlight %}

  
### Define the rest of the helper methods

The first method creates a simple Action that reads the provided text.

{% highlight csharp %}
private static PlayPrompt GetPromptForText(string text)
{
    var prompt = new Prompt { Value = text, Voice = VoiceGender.Male };

    return new PlayPrompt { OperationId = Guid.NewGuid().ToString(), Prompts = new List<Prompt> { prompt } };
}
{% endhighlight %}

The next method automates the creation of the Recognize action. When this action is sent to the Calling Platform, the `textToBeRead` is read and the user can use the number pad to make a choice. The `numberOfOptions` parameter defines the available options. For example, `CreateIvrOptions("test", 3, true)` will let the user choose an option from `{'1', '2', '3', '\#'}`.

<span style="color:red">This should link to Recognize and RecognitionOption and define them.</span>

{% highlight csharp %}
private static Recognize CreateIvrOptions(string textToBeRead, int numberOfOptions, bool includeBack)
{
    if (numberOfOptions > 9)
        throw new Exception("too many options specified");

    var id = Guid.NewGuid().ToString();
    var choices = new List<RecognitionOption>();

    for (int i = 1; i <= numberOfOptions; i++)
    {
        choices.Add(new RecognitionOption { Name = Convert.ToString(i), DtmfVariation = (char)('0' + i) });
    }

    if (includeBack)
        choices.Add(new RecognitionOption { Name = "#", DtmfVariation = '#' });

    var recognize = new Recognize
    {
        OperationId = id,
        PlayPrompt = GetPromptForText(textToBeRead),
        BargeInAllowed = true,
        Choices = choices
    };

    return recognize;
}
{% endhighlight %}

 
The following method sets up the recording for the user.

<span style="color:red">What does it mean by "set up the recording", and why does this hardcode the prompt?</span>

<span style="color:red">Why is this workflow.Actions assignment different from the one in SetupInitialMenu?</span>

{% highlight csharp %}
private static void SetupRecording(Workflow workflow)
{
    var id = Guid.NewGuid().ToString();
    var prompt = GetPromptForText(IvrOptions.NoConsultants);

    var record = new Record
        {
            OperationId = id,
            PlayPrompt = prompt,
            MaxDurationInSeconds = 10,
            InitialSilenceTimeoutInSeconds = 5,
            MaxSilenceTimeoutInSeconds = 2,
            PlayBeep = true,
            StopTones = new List<char> { '#' }
        };

    workflow.Actions = new List<ActionBase> { record };
}
{% endhighlight %}

Next, add the methods that are responsible for creating menus for the different states.

<span style="color:red">Doesn't state mean the user's selection/choice?</span>

<span style="color:red">Need to explain why CreateNewClientMenu is different from SetupInitialMenu which is different from SetupRecording.</span>

{% highlight csharp %}
private void SetupInitialMenu(Workflow workflow)
{
    workflow.Actions = new List<ActionBase> { CreateIvrOptions(IvrOptions.MainMenuPrompt, 5, false) };
}

private Recognize CreateNewClientMenu()
{
    return CreateIvrOptions(IvrOptions.NewClientPrompt, 2, true);
}

private Recognize CreateSupportMenu()
{
    return CreateIvrOptions(IvrOptions.SupportPrompt, 2, true);
}

private Recognize CreatePaymentsMenu()
{
    return CreateIvrOptions(IvrOptions.PaymentPrompt, 2, true);
}
{% endhighlight %}


### Event handling

When the bot receives the incoming call, the bot answers the user and plays the welcome message prompt. A new state entry is created for the user.

<span style="color:red">This creates a state entry but doesn't set the state?</span>

<span style="color:red">What code actually plays the prompt?</span>

{% highlight csharp %}
private Task OnIncomingCallReceived(IncomingCallEvent incomingCallEvent)
{
    var id = Guid.NewGuid().ToString();
    _callStateMap[incomingCallEvent.IncomingCall.Id] = new CallState();

    incomingCallEvent.ResultingWorkflow.Actions = new List<ActionBase>
        {
            new Answer { OperationId = id },
            GetPromptForText(IvrOptions.WelcomeMessage)
        };

    return Task.FromResult(true);
}
{% endhighlight %}

Next, add the handler for the result of the PlayPrompt action. The initial menu is presented to the user.

<span style="color:red">So this plays the message and not OnIncomingCallReceived?</span>

{% highlight csharp %}
private Task OnPlayPromptCompleted(PlayPromptOutcomeEvent playPromptOutcomeEvent)
{
    var callStateForClient = _callStateMap[playPromptOutcomeEvent.ConversationResult.Id];

    callStateForClient.InitiallyChosenMenuOption = null;
    SetupInitialMenu(playPromptOutcomeEvent.ResultingWorkflow);

    return Task.FromResult(true);
}
{% endhighlight %}


This handler handles the result of the Recognize option, which is the value that user specified based on his initial choice. The null case reflects the beginning of call.

<span style="color:red">What Recognize option? Need to tie this back to the Recognize code.</span>

{% highlight csharp %}
private Task OnRecognizeCompleted(RecognizeOutcomeEvent recognizeOutcomeEvent)
{
    var callStateForClient = _callStateMap[recognizeOutcomeEvent.ConversationResult.Id];

    switch (callStateForClient.InitiallyChosenMenuOption)
    {
        case null:
            ProcessMainMenuSelection(recognizeOutcomeEvent, callStateForClient);
            break;
        case NewClient:
            ProcessNewClientSelection(recognizeOutcomeEvent, callStateForClient);
            break;
        case Support:
            ProcessSupportSelection(recognizeOutcomeEvent, callStateForClient);
            break;
        case Payments:
            ProcessPaymentsSelection(recognizeOutcomeEvent, callStateForClient);
            break;
        default:
            SetupInitialMenu(recognizeOutcomeEvent.ResultingWorkflow);
            break;
    }

    return Task.FromResult(true);
}
{% endhighlight %}


The following methods analyze the user's choice. After choosing the option from initial menu, the choice is saved in the state object. If the user chooses to contact a consultant, the method sets up the recording of the message.

{% highlight csharp %}
private void ProcessMainMenuSelection(RecognizeOutcomeEvent outcome, CallState callStateForClient)
{
    if (outcome.RecognizeOutcome.Outcome != Outcome.Success)
    {
        SetupInitialMenu(outcome.ResultingWorkflow);
        return;
    }

    switch (outcome.RecognizeOutcome.ChoiceOutcome.ChoiceName)
    {
        case NewClient:
            callStateForClient.InitiallyChosenMenuOption = NewClient;
            outcome.ResultingWorkflow.Actions = new List<ActionBase> { CreateNewClientMenu() };
            break;
        case Support:
            callStateForClient.InitiallyChosenMenuOption = Support;
            outcome.ResultingWorkflow.Actions = new List<ActionBase> { CreateSupportMenu() };
            break;
        case Payments:
            callStateForClient.InitiallyChosenMenuOption = Payments;
            outcome.ResultingWorkflow.Actions = new List<ActionBase> { CreatePaymentsMenu() };
            break;
        case MoreInfo:
            callStateForClient.InitiallyChosenMenuOption = MoreInfo;
            outcome.ResultingWorkflow.Actions = new List<ActionBase> { GetPromptForText(IvrOptions.MoreInfoPrompt) };
            break;
        default:
            SetupInitialMenu(outcome.ResultingWorkflow);
            break;
    }
}

private void ProcessNewClientSelection(RecognizeOutcomeEvent outcome, CallState callStateForClient)
{
    if (outcome.RecognizeOutcome.Outcome != Outcome.Success)
    {
        outcome.ResultingWorkflow.Actions = new List<ActionBase> { CreateNewClientMenu() };
        return;
    }

    switch (outcome.RecognizeOutcome.ChoiceOutcome.ChoiceName)
    {
        case NewClientOffer:
            outcome.ResultingWorkflow.Actions = new List<ActionBase>
                {
                    GetPromptForText(IvrOptions.Offer),
                    CreateNewClientMenu()
                };
            break;
        case NewClientOrder:
            SetupRecording(outcome.ResultingWorkflow);
            break;
        default:
            callStateForClient.InitiallyChosenMenuOption = null;
            SetupInitialMenu(outcome.ResultingWorkflow);
            break;
    }
}

private void ProcessSupportSelection(RecognizeOutcomeEvent outcome, CallState callStateForClient)
{
    if (outcome.RecognizeOutcome.Outcome != Outcome.Success)
    {
        outcome.ResultingWorkflow.Actions = new List<ActionBase> { CreateSupportMenu() };
        return;
    }

    switch (outcome.RecognizeOutcome.ChoiceOutcome.ChoiceName)
    {
        case SupportOutages:
            outcome.ResultingWorkflow.Actions = new List<ActionBase>
                {
                    GetPromptForText(IvrOptions.CurrentOutages),
                    CreateSupportMenu()
                };
            break;
        case SupportConsultant:
            SetupRecording(outcome.ResultingWorkflow);
            break;
        default:
           callStateForClient.InitiallyChosenMenuOption = null;
            SetupInitialMenu(outcome.ResultingWorkflow);
            break;
    }
}

private void ProcessPaymentsSelection(RecognizeOutcomeEvent outcome, CallState callStateForClient)
{
    if (outcome.RecognizeOutcome.Outcome != Outcome.Success)
    {
        outcome.ResultingWorkflow.Actions = new List<ActionBase> { CreatePaymentsMenu() };
        return;
    }

    switch (outcome.RecognizeOutcome.ChoiceOutcome.ChoiceName)
    {
        case PaymentDetails:
            outcome.ResultingWorkflow.Actions = new List<ActionBase>
                {
                    GetPromptForText(IvrOptions.PaymentDetailsMessage),
                    CreatePaymentsMenu()
                };
            break;
        case PaymentNotVisible:
            SetupRecording(outcome.ResultingWorkflow);
            break;
        default:
            callStateForClient.InitiallyChosenMenuOption = null;
            SetupInitialMenu(outcome.ResultingWorkflow);
            break;
    }
}
{% endhighlight %}
  
After the recording finishes, the call ends after playing a simple thank you message. This example does not actually use the recording, but you can easily change the code to use it. If the recording was successful, the `recordOutcomeEvent.RecordedContent` field would contain the stream to the recording. To determine whether the recording was successful, check the `recordOutcomeEvent.RecordOutcome.Outcome` flag.
 

<span style="color:red">Kind of leaving the user hanging without them how they can easily change to use the recording.</span>


{% highlight csharp %}
private Task OnRecordCompleted(RecordOutcomeEvent recordOutcomeEvent)
{
    var id = Guid.NewGuid().ToString();

    recordOutcomeEvent.ResultingWorkflow.Actions = new List<ActionBase>
        {
            GetPromptForText(IvrOptions.Ending),
            new Hangup { OperationId = id }
        };

    recordOutcomeEvent.ResultingWorkflow.Links = null;
    _callStateMap.Remove(recordOutcomeEvent.ConversationResult.Id);

    return Task.FromResult(true);
}
{% endhighlight %}


## Finishing Up

Compile the code, run it locally, and confirm that it's not throwing exceptions. If your bot works locally, deploy it to Azure (see [Deploying a Bot to the Cloud](/en-us/deployment/)). Next, add the bot to your contacts list, and you're finished.

If you encounter issues on service startup, setting the **Remove additional files at destination** publishing setting may help.

### Testing with ngrok

<span style="color:red">Is ngrok the only option for test calling bots - you can't use the emulator?</span>

There are tools, such as [ngrok](https://ngrok.com/), that let you create a public URL to your local webserver on your computer.  

To use ngrok to test your bot running locally over Skype, download ngrok and modify your bot's registration. Then, start ngrok on your computer and map it to a local port (for example, port 12345).

<span style="color:red">Don't we use 3978, shouldn't we show what they'd actually use?</span>

```
\> ngrok http 12345
```

This will create a new tunnel from a public URL to localhost:12345 on your computer. After you start the command, you can see the status of the tunnel.

```
ngrok by \@inconshreveable (Ctrl+C to quit)

Tunnel Status online  
Update update available (version 2.0.24, Ctrl-U to update)  
Version 2.0.19/2.0.25  
Web Interface <http://127.0.0.1:4040>  
Forwarding http://78191649.ngrok.io -\> localhost:12345  
Forwarding https://78191649.ngrok.io -\> localhost:12345  

Connections ttl opn rt1 rt5 p50 p90  
            0 0 0.00 0.00 0.00 0.00
```

Note the "Forwarding" lines. In this case, ngrok created two endpoints: <http://78191649.ngrok.io> and <https://78191649.ngrok.io> for HTTP and HTTPS traffic.

Next, configure IIS Express to run the service on port 12345. In Visual Studio, right click on the IvrSample project and choose Properties. Set the port that will be used by IIS Express for local runs as shown below (creation of A virtual directory may be required) and save the changes.  

![](/en-us/images/ivr/image2.png)

Now configure IIS Express to serve requests coming from outside the network. Navigate to the project file, IvrSample\.vs\config\applicationhost.config. In the configuration file, locate the Ivr website inside the \<sites\> section. It should contain the following lines:

{% highlight xml %}
<bindings>
    <binding protocol="http" bindingInformation="*:12345:localhost" />
</bindings>
{% endhighlight %}

Add the following second binding:  

{% highlight xml %}
<bindings>
    <binding protocol="http" bindingInformation="*:12345:localhost" />
    <binding protocol="http" bindingInformation="*:12345:*" />
</bindings>
{% endhighlight %}

Check if IIS Express is running (if there is IIS Express icon in system tray). If yes, right click on the icon and choose Exit.

The next step configures your bot in the portal to use ngrok endpoints.

Don't forget to append your route when updating the messaging URL. The new URL should look like: https://78191649.ngrok.io/v1/call.  

Update the CallbackUrl setting in Web.config file (it should be https://78191649.ngrok.io/v1/callback).

Start your server locally and send messages to your bot over Skype. Bot Platform will send the messages to https://78191649.ngrok.io/v1/call and ngrok will forward them to your computer (you just need to keep ngrok running).

<span style="color:red">Bot Platform is new, what is it?</span>

You will see each request logged in the ngrok's tunnel status table.

```
HTTP Requests
-------------
POST /api/calling/call                  200 OK
```

If you are done testing, you can stop ngrok by entering Ctrl+C. Your bot will stop working because there is nothing to forward the requests to your local server.

Note that the free version of ngrok will create a new unique URL for you every time you start it. That means you always need to go back and update the messaging URL for your bot.







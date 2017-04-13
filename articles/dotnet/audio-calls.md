---
title: Conduct audio calls with Skype by using the Bot Builder SDK for .NET | Microsoft Docs
description: Learn how to conduct audio calls with Skype by using the Bot Builder SDK for .NET.
keywords: Bot Framework, dotnet, .NET, Bot Builder, SDK, Skype, IVR, audio call
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 02/21/2017
ms.reviewer:
ROBOTS: Index, Follow
---

# Conduct audio calls with Skype

[!include[Introduction to conducting audio calls](~/includes/snippet-audio-call-intro.md)]
This article describes how to enable support for audio calls via Skype by using the Bot Builder SDK for .NET. 

## Conduct an audio call

The architecture for a bot that supports audio calls is very similar to that of a typical bot. 
The following code samples show how to enable support for audio calls via Skype by using the Bot Builder SDK for .NET. 

First, define the `CallingController`.

```cs
[BotAuthentication]
[RoutePrefix("api/calling")]
public class CallingController : ApiController
{
    public CallingController() : base()
    {
        CallingConversation.RegisterCallingBot(callingBotService => new IVRBot(callingBotService));
    }

    [Route("callback")]
    public async Task<HttpResponseMessage> ProcessCallingEventAsync()
    {
        return await CallingConversation.SendAsync(this.Request, CallRequestType.CallingEvent);
    }

    [Route("call")]
    public async Task<HttpResponseMessage> ProcessIncomingCallAsync()
    {
        return await CallingConversation.SendAsync(this.Request, CallRequestType.IncomingCall);
    }
}
```

> [!NOTE]
> In addition to the `CallingController` (to support audio calls) a bot may also contain a `MessagesController` 
> (to support messages). Providing both options allows users to interact with the bot in the way that they prefer.

While the constructor registers the `IVRBot` class, `ProcessIncomingCallAsync` will execute whenever 
the user initiates a call to this bot from Skype. 
The `IVRBot` has a predefined handler for that event:

```cs
private Task OnIncomingCallReceived(IncomingCallEvent incomingCallEvent)
{
    this.callStateMap[incomingCallEvent.IncomingCall.Id] = new CallState(incomingCallEvent.IncomingCall.Participants);

    incomingCallEvent.ResultingWorkflow.Actions = new List<ActionBase>
    {
        new Answer { OperationId = Guid.NewGuid().ToString() },
        GetPromptForText(WelcomeMessage)
    };

    return Task.FromResult(true);
}
```

As its first action within the workflow, the bot should specify to whether to answer or reject the incoming call. 
If the bot decides to answer the call, subsequent actions specified within the workflow will instruct the 
**Skype Bot Platform for Calling** to play prompt, record audio, recognize speech, or collect digits from a dial pad. 
The final action of the workflow might be to end the call. 
The **Skype Bot Platform for Calling** will execute the actions in the order that they are specified by the workflow. 

In the previous code sample, the workflow instructs the bot to answer the incoming call and play a welcome message. 

Next, the following code sample defines a handler that will execute after the welcome message completes, 
to setup a menu.

```cs
private Task OnPlayPromptCompleted(PlayPromptOutcomeEvent playPromptOutcomeEvent)
{
    var callState = this.callStateMap[playPromptOutcomeEvent.ConversationResult.Id];
    SetupInitialMenu(playPromptOutcomeEvent.ResultingWorkflow);
    return Task.FromResult(true);
}
```

The `CreateIvrOptions` method defines that menu that will be presented to the user.

```cs
private static Recognize CreateIvrOptions(string textToBeRead, int numberOfOptions, bool includeBack)
{
    if (numberOfOptions > 9)
    {
        throw new Exception("too many options specified");
    }

    var choices = new List<RecognitionOption>();

    for (int i = 1; i <= numberOfOptions; i++)
    {
        choices.Add(new RecognitionOption { Name = Convert.ToString(i), DtmfVariation = (char)('0' + i) });
    }

    if (includeBack)
    {
        choices.Add(new RecognitionOption { Name = "#", DtmfVariation = '#' });
    }

    var recognize = new Recognize
    {
        OperationId = Guid.NewGuid().ToString(),
        PlayPrompt = GetPromptForText(textToBeRead),
        BargeInAllowed = true,
        Choices = choices
    };

    return recognize;
}
```

The `RecognitionOption` class defines both the spoken answer as well as the corresponding Dual-Tone Multi-Frequency (DTMF) variation (the user may answer by typing the corresponding digits on the keypad).

The `OnRecognizeCompleted` method processes the user's selection (input parameter `recognizeOutcomeEvent` contains 
the user's selection).

```cs
private Task OnRecognizeCompleted(RecognizeOutcomeEvent recognizeOutcomeEvent)
{
    var callState = this.callStateMap[recognizeOutcomeEvent.ConversationResult.Id];
    ProcessMainMenuSelection(recognizeOutcomeEvent, callState);
    return Task.FromResult(true);
}
```

The bot can also be designed to support free natural language by using the **Bing Speech API** to recognize words in the user's spoken response:

```cs
private async Task OnRecordCompleted(RecordOutcomeEvent recordOutcomeEvent)
{
    recordOutcomeEvent.ResultingWorkflow.Actions = new List<ActionBase>
    {
        GetPromptForText(EndingMessage),
        new Hangup { OperationId = Guid.NewGuid().ToString() }
    };

    // Convert the audio to text
    if (recordOutcomeEvent.RecordOutcome.Outcome == Outcome.Success)
    {
        var record = await recordOutcomeEvent.RecordedContent;
        string text = await this.GetTextFromAudioAsync(record);

        var callState = this.callStateMap[recordOutcomeEvent.ConversationResult.Id];

        await this.SendSTTResultToUser("We detected the following audio: " + text, callState.Participants);
    }

    recordOutcomeEvent.ResultingWorkflow.Links = null;
    this.callStateMap.Remove(recordOutcomeEvent.ConversationResult.Id);
}
```

## Additional resources

- [Builder library][builderLibrary]

[builderLibrary]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html
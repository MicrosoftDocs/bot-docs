---
title: Conduct audio calls with Skype | Microsoft Docs
description: Learn how to conduct audio calls with Skype using the Bot Framework SDK for .NET.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---

# Conduct audio calls with Skype

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

[!INCLUDE [Introduction to conducting audio calls](../includes/snippet-audio-call-intro.md)]

The architecture for a bot that supports audio calls is very similar to that of a typical bot. 
The following code samples show how to enable support for audio calls via Skype with the Bot Framework SDK for .NET. 

## Enable support for audio calls

To enable a bot to support audio calls, define the `CallingController`.

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
> In addition to the `CallingController`, which supports audio calls, a bot may also contain a 
> `MessagesController` to support messages. Providing both options allows users to interact with
> the bot in the way that they prefer. <!-- docs on MessagesController are where? -->

##  Answer the call

The `ProcessIncomingCallAsync` task will execute whenever a user initiates a call to this bot from Skype.
The constructor registers the `IVRBot` class, which has a predefined handler for the `incomingCallEvent`.

The first action within the workflow should determine if the bot answers or rejects the incoming call. This workflow instructs the bot to answer the incoming call and then play a welcome message. 

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

## After the bot answers

If the bot answers the call, subsequent actions specified within the workflow will instruct the 
**Skype Bot Platform for Calling** to play prompt, record audio, recognize speech, or collect digits from a dial pad. 
The final action of the workflow might be to end the call. 

This code sample defines a handler that will set up a menu after the welcome message completes.

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

The `RecognitionOption` class defines both the spoken answer as well as the corresponding Dual-Tone Multi-Frequency (DTMF) variation. DTMF enables the user to answer by typing the corresponding digits on the keypad instead of speaking.

The `OnRecognizeCompleted` method processes the user's selection, and the input parameter `recognizeOutcomeEvent` contains the value of the user's selection.

```cs
private Task OnRecognizeCompleted(RecognizeOutcomeEvent recognizeOutcomeEvent)
{
    var callState = this.callStateMap[recognizeOutcomeEvent.ConversationResult.Id];
    ProcessMainMenuSelection(recognizeOutcomeEvent, callState);
    return Task.FromResult(true);
}
```

## Support natural language
The bot can also be designed to support natural language responses. The **Bing Speech API** enables the bot to recognize words in the user's spoken reply.

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

## Sample code

For a complete sample that shows how to support audio calls with Skype using the Bot Framework SDK for .NET, see the <a href="https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/skype-CallingBot" target="_blank">Skype Calling Bot sample</a> in GitHub.

## Additional resources

- <a href="/dotnet/api/?view=botbuilder-3.11.0" target="_blank">Bot Framework SDK for .NET Reference</a>
- <a href="https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/skype-CallingBot" target="_blank">Skype Calling Bot sample (GitHub)</a>

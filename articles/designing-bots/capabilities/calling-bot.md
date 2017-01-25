---
title: Designing Bots - Calling and IVR Bots | Bot Framework
description: Bots that work as IVR solutions
services: Bot Framework
documentationcenter: BotFramework-Docs
author: matvelloso
manager: larar

ms.service: required
ms.devlang: may be required
ms.topic: article
ms.tgt_pltfrm: may be required
ms.workload: required
ms.date: 01/19/2017
ms.author: mat.velloso@microsoft.com

---
# Bot Design Center - Calling and IVR Bots



##Bots that answer the phone

When building bots for Skype, an additional capability that can be explored is the ability to make audio calls: A bot may, either exclusively or also in additional to other UI elements such as text and rich UIs, establish audio calls with the user.

This becomes particularly useful when the user does not want to - or simply can't - type or click at buttons.

The architecture for a calling bot is not too different from a typical bot. In fact, they can be the same code and leverage the same web service deployment. 

It all starts with an API controller.

In C#:

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

Note that this bot may have the MessagesControler **as well** as this. So it becomes capable of both messages and calls. This is generally recommended so users can interact with it the way that is more convenient to them.

While the constructor registers the IVRBot class, the ProcessIncomingCallAsync will execute whenever the user places a call to this bot from Skype. The IVRBot has a predefined handler for that event:


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


In the first action of the workflow the bot should decide if it’s interested in answering or rejecting the call. Should the bot decide to answer the call, the subsequent actions instruct the Skype Bot Platform for Calling to either play prompt, record audio, recognize speech, or collect digits from a dial pad. The last action of the workflow could be to hang up the voice call. Skype Bot Platform for Calling then takes the workflow and attempts to execute actions in order given by bot.

In the code above, we are instructing the bot to answer and play a welcome message. Once that message if finished, we will have a handler, which will setup a menu to be presented:

	private Task OnPlayPromptCompleted(PlayPromptOutcomeEvent playPromptOutcomeEvent)
	{
    	var callState = this.callStateMap[playPromptOutcomeEvent.ConversationResult.Id];
    	SetupInitialMenu(playPromptOutcomeEvent.ResultingWorkflow);
    	return Task.FromResult(true);
	}

The menu will be defined later on by the CreateIvrOptions method:

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

The RecognitionOption class allows us to define both the spoken answer as well as the corresponding Dtmf variation (the user may just type the corresponding digit on the keypad).


The user's selection is handled by the OnRecognizeCompleted method, where the recognizeOutcomeEvent contains the selection from the user:

	private Task OnRecognizeCompleted(RecognizeOutcomeEvent recognizeOutcomeEvent)
	{
    	var callState = this.callStateMap[recognizeOutcomeEvent.ConversationResult.Id];
    	ProcessMainMenuSelection(recognizeOutcomeEvent, callState);
    	return Task.FromResult(true);
	}

Note the bot can also allow free natural language, leaving the user to speak freely and handling the input with Bing Speech API:

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

##Show me the code!

You can read [the detailed readme here](https://trpp24botsamples.visualstudio.com/_git/Code?path=%2FCSharp%2Fskype-CallingBot%2FREADME.md&version=GBmaster&_a=contents) and see the [full C# code mentioned above here](https://trpp24botsamples.visualstudio.com/_git/Code?path=%2FCSharp%2Fskype-CallingBot&version=GBmaster&_a=contents).


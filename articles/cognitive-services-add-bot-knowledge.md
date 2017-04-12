---
title: Adding Knowledge Capabilities with Cognitive Services | Microsoft Docs
description: Add knowledge capabilities to your bot with the Bot Framework and Cognitive Services.
keywords: intelligence, knowledge, entity, linking, recommendations
author: RobStand
manager: rstand
ms.topic: intelligence-knowledge-article

ms.prod: bot-framework
ms.service: Cognitive Services
ms.date: 
ms.reviewer: rstand

ROBOTS: Index, Follow
---

# Add knowledge extraction to your bot

> [!TIP]
> You can find detailed documentation about each API, including developer guides and API references by visiting the Microsoft Cognitive Services <a href="https://www.microsoft.com/cognitive-services/en-us/documentation" target="_blank">documentation site</a> and selecting the API you need on the left.

## Knowledge API bot examples
The Knowledge APIs can arm your bots with your own knowledge base and user history, allowing them to efficiently navigate your product catalogs to provide recommendations.

> [!IMPORTANT]
> Before you get started, you need to obtain your own subscription key from the <a href="https://www.microsoft.com/cognitive-services/" target="_blank">Microsoft Cognitive Services site</a>.

### QnA Maker example
This is a very simple Bot which uses simple heuristics to determine whether the incoming user message has a question intent, and if that's the case simply forwards the message to the QnA Maker service.

To get started with QnA Maker, go to <a href="https://qnamaker.ai" target="_blank">qnamaker.ai</a> and build your own knowledge base. The <a href="https://qnamaker.ai/Documentation" target="_blank">documentation</a> describes the various flows in the tool to create your knowledge base.

To create the bot, you will use the [Bot Application .NET template](~/dotnet/getstarted.md#prerequisites) as a starting point.

After you set up your knowledge base, create your project with the Bot Application template, and add the following class to handle the integration with your QnA Maker service.

QnAMaker Dialog is distributed in a separate NuGet package called `Microsoft.Bot.Builder.CognitiveServices` for C# and npm module called `botbuilder-cognitiveservices` for Node.js. Make sure you install these.


```cs
using System.Web;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using System.Text.RegularExpressions;

namespace FAQBot.Dialogs
{
    // Inherit from the QnAMakerDialog
    [Serializable]
    public class BasicQnAMakerDialog : QnAMakerDialog
    {        
        // Parameters to QnAMakerService are:
        // Compulsory: subscriptionKey, knowledgebaseId,
        // Optional: defaultMessage, scoreThreshold[Range 0.0 – 1.0]
        public BasicQnAMakerDialog() : base(new QnAMakerService(new QnAMakerAttribute("<YOUR_QNAMAKER_SUBSCRIPTION_KEY>", "<YOUR_KNOWLEDGE_BASE_ID>", "No good match in FAQ.", 0.5)))
        {
        }
    }

    /// <summary>
    /// Simple Dialog, that invokes the QnAMaker if the incoming message is a question
    /// </summary>
    [Serializable]
    public class FAQDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;

            // Call the QnAMaker Dialog if the message is a question.
            if (IsQuestion(message.Text))
            {
                await context.Forward(new BasicQnAMakerDialog(), AfterQnA, message, CancellationToken.None);
            }
            else
                await context.PostAsync("This doesn't look like a question.");

            context.Wait(MessageReceivedAsync);
        }

        // Callback, after the QnAMaker Dialog returns a result.
        public async Task AfterQnA(IDialogContext context, IAwaitable<object> argument)
        {
            context.Wait(MessageReceivedAsync);
        }

        // Simple check if the message is a potential question.
        private bool IsQuestion(string message)
        {
            // List of common question words
            List<string> questionWords = new List<string>(){"who","what","why", "how", "when"};

            // Question word present in the message
            Regex questionPattern = new Regex(@"\b(" + string.Join("|", questionWords.Select(Regex.Escape).ToArray()) + @"\b)", RegexOptions.IgnoreCase);

            // Return true if a question word present, or the message ends with "?"
            if (questionPattern.IsMatch(message) || message.EndsWith("?"))
                return true;
            else
                return false;
        }
    }
}
```

Next, go to **MessagesController.cs** and add the following namespaces.

```cs
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FAQBot.Dialogs;
```

Finally, within the same file, replace the code in the `Post` task with this code.  

```cs
[ResponseType(typeof(void))]
public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
{
	if (activity.Type == ActivityTypes.Message)
	{
		await Conversation.SendAsync(activity, () => new FAQDialog());
	}
	else
	{
		// Add code to handle errors, or non-messaging activities
	}

	var response = Request.CreateResponse(HttpStatusCode.OK);
	return response;
}
```

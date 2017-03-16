---
title: Adding Knowledge Capabilities with Cognitive Services | Microsoft Docs
description: Add knowledge capabilities to your bot with the Bot Framework and Cognitive Services.
keywords: intelligence, knowledge, entity, linking, recommendations
author: RobStand
manager: rstand
ms.topic: intelligence-knowledge-article

ms.prod: botframework
ms.service: Cognitive Services
ms.date: 
ms.reviewer: v-tosisk

# Include the following line commented out
#ROBOTS: Index

---

# Add knowledge extraction to your bot
Our five knowledge APIs enable you to identify named entities or phrases in unstructured text, add personalized recommendations, provide auto-complete suggestions based on natural interpretation of user queries, and search academic papers and other research like a personalized FAQ service.

## Entity Linking Intelligence Service
The <a href="https://www.microsoft.com/cognitive-services/en-us/entity-linking-intelligence-service" target="_blank">Entity Linking Intelligence Service</a> annotates unstructured text with the relevant entities mentioned in the text. Depending on the context, the same word or phrase may refer to different things. This service understands the context of the supplied text and will identify each entity in your text.    

## Recommendations API
The <a href="https://www.microsoft.com/cognitive-services/en-us/bing-image-search-api" target="_blank">Recommendations API</a> provides "frequently bought together" recommendations to a product, as well as personalized recommendations based on a user's history. Use this service to build and train a model based on data that you provide, and then use this model to add recommendations to your application.

## Knowledge Exploration Service
The <a href="https://www.microsoft.com/cognitive-services/en-us/knowledge-exploration-service" target="_blank">Knowledge Exploration Service</a> provides natural language interpretation of user queries and returns annotated interpretations to enable rich search and auto-completion experiences that anticipate what the user is typing. Instant query completion suggestions and predictive query refinements are based on your own data and application-specific grammars to enable your users to perform fast queries.    

## Academic Knowledge API
The  <a href="https://www.microsoft.com/cognitive-services/en-us/academic-knowledge-api" target="_blank">Academic Knowledge API</a> returns academic research papers, authors, journals, conferences, topics, and universities from the <a href="https://www.microsoft.com/en-us/research/project/microsoft-academic-graph/" target="_blank">Microsoft Academic Graph</a>. Built as a domain-specific example of the Knowledge Exploration Service, the Academic Knowledge API provides a knowledge base using a graph-like dialog with search capabilities over hundreds of millions of research-related entities. Search for a topic, a professor, a university, or a conference, and the API will provide relevant publications and related entities. The grammar also supports natural queries like "papers by Michael Jordan about machine learning after 2010".

## QnA Maker
The  <a href="https://qnamaker.ai" target="_blank">QnA Maker</a> is a free, easy-to-use, REST API and web-based service that trains AI to respond to users’ questions in a natural, conversational way. With optimized machine learning logic and the ability to integrate industry-leading language processing, QnA Maker distills semi-structured data like question and answer pairs into distinct, helpful answers.

> [!TIP]
> You can find detailed documentation about each API, including developer guides and API references by visiting the Microsoft Cognitive Services <a href="https://www.microsoft.com/cognitive-services/en-us/documentation" target="_blank">documentation site</a> and selecting the API you need on the left.

## Knowledge API bot examples
The Knowledge APIs can arm your bots with your own knowledge base and user history, allowing them to efficiently navigate your product catalogs to provide recommendations.

> [!IMPORTANT]
> Before you get started, you need to obtain your own subscription key from the <a href="https://www.microsoft.com/cognitive-services/" target="_blank">Microsoft Cognitive Services site</a>.

### QnA Maker example
This is a very simple Bot which uses simple heuristics to determine whether the incoming user message has a question intent, and if that's the case simply forwards the message to the QnA Maker service.

To get started with QnA Maker, go to <a href="https://qnamaker.ai" target="_blank">qnamaker.ai</a> and build your own knowledge base. Our <a href="https://qnamaker.ai/Documentation" target="_blank">documentation</a> describes the various flows in the tool to create your knowledge base.

To create the bot, we will use the <a href="http://docs.botframework.com/connector/getstarted/#getting-started-in-net" target="_blank">Bot Application .NET template</a> as our starting point.

After you set up your knowledge base, create your project with the Bot Application template, and add the following class to handle the integration with your QnA Maker service.

QnAMaker Dialog is distributed in a separate NuGet package called *Microsoft.Bot.Builder.CognitiveServices* for C# and npm module called *botbuilder-cognitiveservices* for Node.js. Make sure you install these.


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
    //Inherit from the QnAMakerDialog
    [Serializable]
    public class BasicQnAMakerDialog : QnAMakerDialog
    {        
        //Parameters to QnAMakerService are:
        //Compulsory: subscriptionKey, knowledgebaseId,
        //Optional: defaultMessage, scoreThreshold[Range 0.0 – 1.0]
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

            //Call the QnAMaker Dialog if the message is a question.
            if (IsQuestion(message.Text))
            {
                await context.Forward(new BasicQnAMakerDialog(), AfterQnA, message, CancellationToken.None);
            }
            else
                await context.PostAsync("This doesn't look like a question.");

            context.Wait(MessageReceivedAsync);
        }

        //Callback, after the QnAMaker Dialog returns a result.
        public async Task AfterQnA(IDialogContext context, IAwaitable<object> argument)
        {
            context.Wait(MessageReceivedAsync);
        }

        //Simple check if the message is a potential question.
        private bool IsQuestion(string message)
        {
            //List of common question words
            List<string> questionWords = new List<string>(){"who","what","why", "how", "when"};

            //Question word present in the message
            Regex questionPattern = new Regex(@"\b(" + string.Join("|", questionWords.Select(Regex.Escape).ToArray()) + @"\b)", RegexOptions.IgnoreCase);

            //Return true if a question word present, or the message ends with "?"
            if (questionPattern.IsMatch(message) || message.EndsWith("?"))
                return true;
            else
                return false;
        }
    }
}
```

Next, go to *MessagesController.cs*, and add the following namespaces.

```cs
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FAQBot.Dialogs;


Finally, on the same file, replace the code in the Post task with the one below.  

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
		//add code to handle errors, or non-messaging activities
	}

	var response = Request.CreateResponse(HttpStatusCode.OK);
	return response;
}
```

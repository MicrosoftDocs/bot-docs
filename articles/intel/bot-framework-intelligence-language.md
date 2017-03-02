---
title: Adding Language Capabilities with Cognitive Services | Microsoft Docs
description: Add language understanding capabilities to your bot with the Bot Framework and Cognitive Services.
keywords: intelligence, language, understanding, nlp
author: RobStand
manager: rstand
ms.topic: intelligence-language-article

ms.prod: botframework
ms.service: Cognitive Services
ms.date: 03/01/2017
ms.reviewer: rstand

# Include the following line commented out
#ROBOTS: Index
---
# Use the Cognitive Services Language APIs
The Language APIs enable you to build smart bots that are able to understand and process natural language. This is a particularly important skill for bots as, in most cases, the interaction users have with bots is free-form. Thus, bots must be able to understand language the way people speak it - naturally and contextually. The Language APIs use powerful language models to determine what users want, identify concepts and entities in a given sentence, and ultimately allow bots to respond with the appropriate action. Furthermore, they support several text analytics capabilities, such as spell checking, sentiment detection, language modeling, and more, to extract more accurate and richer insights from text.   

## API Overview
There are 5 language APIs available in Cognitive Services to understand and process natural language:

- The [Language Understanding Intelligent Service (LUIS)](https://www.microsoft.com/cognitive-services/en-us/language-understanding-intelligent-service-luis){:target="_blank"} is able to process natural language using pre-built or custom-trained language models.
- The [Text Analytics API](https://www.microsoft.com/cognitive-services/en-us/text-analytics-api){:target="_blank"}  detects sentiment, key phrases, topics, and language from text.
- The [Bing Spell Check API](https://www.microsoft.com/cognitive-services/en-us/bing-spell-check-api){:target="_blank"}  provides powerful spell check capabilities, and is able to recognize the difference between names, brand names, and slang.
- The [Linguistic Analysis API](https://www.microsoft.com/cognitive-services/en-us/linguistic-analysis-api){:target="_blank"} uses advanced linguistic analysis algorithms to process text, and perform operations such as breaking down the structure of the text, or performing part-of-speech tagging and parsing.
- The [Web Language Model (WebLM) API](https://www.microsoft.com/cognitive-services/en-us/web-language-model-api){:target="_blank"} can be used to automate a variety of natural language processing tasks, such as word frequency or next-word prediction, using advanced language modeling algorithms.


## Example: Weather Bot
For our first example, we will build a weather bot that is able to understand and respond to various hypothetical commands, such as "What's the weather like in Paris", "What's the temperature next week in Seattle" and so on. The bot is using LUIS to identify the intent of the user, and reply with the appropriate prompt.

To get started with LUIS, go to [LUIS.ai](http://www.luis.ai){:target="_blank"} and build your own custom language model. Our [Getting Started](https://www.microsoft.com/cognitive-services/en-us/luis-api/documentation/getstartedwithluis-basics){:target="_blank"} guide describes in details how to build your first model through the LUIS user interface, or programatically via the LUIS APIs. We encourage you to watch our [basic](https://www.youtube.com/watch?v=jWeLajon9M8&index=4&list=PLD7HFcN7LXRdHkFBFu4stPPeWJcQ0VFLx){:target="_blank"} video tutorial.

To create the bot, we will use the [Bot Application .NET template](http://docs.botframework.com/connector/getstarted/#getting-started-in-net){:target="_blank"} as our starting point. Note that you need to build the language model for the weather bot in LUIS first. To accomplish this, follow the steps in this [video](https://www.youtube.com/watch?v=39L0Gv2EcSk&index=5&list=PLD7HFcN7LXRdHkFBFu4stPPeWJcQ0VFLx){:target="_blank"}.

After you set up your language model, create your project with the Bot Application template, and add the following class to handle the integration with your LUIS language model.


```
namespace Microsoft.Bot.Sample.WeatherBot
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Builder.Dialogs;
    using Builder.Luis;
    using Builder.Luis.Models;

    [LuisModel("<YOUR_LUIS_APP_ID>", "<YOUR_LUIS_SUBSCRIPTION_KEY>")]
    [Serializable]
    public class TravelGuidDialog : LuisDialog<object>
    {
        public const string Entity_location = "Location";

        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry I did not understand: "
                + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        enum City { Paris, London, Seattle, Munich };

        [LuisIntent("GetWeather")]
        public async Task GetWeather(IDialogContext context, LuisResult result)
        {
            var cities = (IEnumerable<City>)Enum.GetValues(typeof(City));
            EntityRecommendation location;

            if (!result.TryFindEntity(Entity_location, out location))
            {
                PromptDialog.Choice(context,
                                    SelectCity,
                                    cities,
                                    "In which city do you want to know the weather forecast?");
            }
            else
            {
                //Add code to retrieve the weather
                await context.PostAsync($"The weather in {location} is ");
                context.Wait(MessageReceived);
            }
        }

        private async Task SelectCity(IDialogContext context, IAwaitable<City> city)
        {
            var message = string.Empty;
            switch (await city)
            {
                case City.Paris:
                case City.London:
                case City.Seattle:
                case City.Munich:
                    message = $"The weather in {city} is ";
                    break;
                default:
                    message = $"Sorry!! I don't have know the weather in {city}";
                    break;
            }
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
    }
}
```

Next, go to *MessagesController.cs*, and add the following namespaces.

```

    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Builder.Luis.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
```

Finally, on the same file, replace the code in the Post task with the one below.  

```

    [ResponseType(typeof(void))]
    public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
    {
        if (activity.Type == ActivityTypes.Message)
        {
            await Conversation.SendAsync(activity, () => new TravelGuidDialog());
        }
        else
        {
            //add code to handle errors, or non-messaging activities
        }

        return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
    }

```

## Example: Emotional Bot
For our next example, we will use the Text Analytics API to determine the sentiment behind a user's message, i.e. whether it is positive or negative. The Text Analytics API returns a sentiment score between 0 and 1, where 0 is very negative and 1 is very positive. For example, if the user types "That was really helpful", the API will classify it with a highly positive score, whereas if he types "That didn't help at all", the API will return a negative score. The example that follows shows how the bot's response can be customized according to the sentiment score calculated by the Text Analytics API. For more information about the Text Analytics API, see the [C# and Python sample code](https://text-analytics-demo.azurewebsites.net/Home/SampleCode){:target="_blank"} for the service, or our [Getting Started guide](http://go.microsoft.com/fwlink/?LinkID=760860){:target="_blank"}.

For this example, we will use the [Bot Application .NET template](http://docs.botframework.com/connector/getstarted/#getting-started-in-net){:target="_blank"} as our starting point. Note that the *Newtonsoft.JSON* package is also required, which can be obtained via NuGet. After you create your project with the Bot Application template, you will create some classes to hold the input and output from the API. Create a new C# class file (*TextAnalyticsCall.cs*) with the following code. The class will serve as our model for the JSON input/output of the Text Analytics API.    

{% highlight c# %}

using System.Collections.Generic;

// Classes to store the input for the sentiment API call
public class BatchInput
{
    public List<DocumentInput> documents { get; set; }
}
public class DocumentInput
{
    public double id { get; set; }
    public string text { get; set; }
}

// Classes to store the result from the sentiment analysis
public class BatchResult
{
    public List<DocumentResult> documents { get; set; }
}
public class DocumentResult
{
    public double score { get; set; }
    public string id { get; set; }
}

{% endhighlight %}

Next, go to *MessagesController.cs* and add the following namespaces.

{% highlight c# %}

using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;

{% endhighlight %}

Finally, replace the code in the Post task with the one in the code snippet below. The code receives the user message, calls the sentiment analysis endpoint and responds accordingly to the user.

{% highlight c# %}
public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
{
	var connector = new ConnectorClient(new Uri(activity.ServiceUrl));

	if (activity.Type == ActivityTypes.Message)
	{
		const string apiKey = "<YOUR API KEY FROM MICROSOFT.COM/COGNITIVE>";
		const string queryUri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";

		var client = new HttpClient {
			DefaultRequestHeaders = {
				{"Ocp-Apim-Subscription-Key", apiKey},
				{"Accept", "application/json"}
			}
		};
		var sentimentInput = new BatchInput {
			Documents = new List<DocumentInput> {
				new DocumentInput {
					Id = 1,
					Text = activity.Text,
				}
			}
		};
		var json = JsonConvert.SerializeObject(sentimentInput);
		var sentimentPost = await client.PostAsync(queryUri, new StringContent(json, Encoding.UTF8, "application/json"));
		var sentimentRawResponse = await sentimentPost.Content.ReadAsStringAsync();
		var sentimentJsonResponse = JsonConvert.DeserializeObject<BatchResult>(sentimentRawResponse);
		var sentimentScore = sentimentJsonResponse?.Documents?.FirstOrDefault()?.Score ?? 0;

		string message;
		if (sentimentScore > 0.7)
		{
			message = $"That's great to hear!";
		}
		else if (sentimentScore < 0.3)
		{
			message = $"I'm sorry to hear that...";
		}
		else
		{
			message = $"I see...";
		}
		var reply = activity.CreateReply(message);
		await connector.Conversations.ReplyToActivityAsync(reply);
	}
	else
	{
		//add code to handle errors, or non-messaging activities
	}
	var response = Request.CreateResponse(HttpStatusCode.OK);
	return response;
}
{% endhighlight %}

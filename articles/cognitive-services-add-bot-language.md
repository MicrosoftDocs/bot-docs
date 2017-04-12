---
title: Adding Language Capabilities with Cognitive Services | Microsoft Docs
description: Add language understanding capabilities to your bot with the Bot Framework and Cognitive Services.
keywords: intelligence, language, understanding, nlp
author: RobStand
manager: rstand
ms.topic: intelligence-language-article

ms.prod: bot-framework
ms.service: Cognitive Services
ms.date: 
ms.reviewer: rstand

ROBOTS: Index, Follow
---

# Add language understanding to your bot

## Language understanding bot examples
### LUIS example
In this example, you will build a weather bot that is able to understand and respond to various hypothetical commands, such as "What's the weather like in Paris", "What's the temperature next week in Seattle", and so on. The bot is using LUIS to identify the intent of the user, and then reply with the appropriate prompt.

To get started with LUIS, go to <a href="http://www.luis.ai" target="_blank">LUIS.ai</a> and build your own custom language model. The <a href="https://www.microsoft.com/cognitive-services/en-us/LUIS-api/documentation/Plan-your-app" target="_blank">Getting Started</a> guide describes in details how to build your first model through the LUIS user interface, or programatically via the LUIS APIs. You can watch a  <a href="https://www.youtube.com/watch?v=jWeLajon9M8&index=4&list=PLD7HFcN7LXRdHkFBFu4stPPeWJcQ0VFLx" target="_blank">basic</a> video tutorial.

To create the bot, you will use the <a href="http://aka.ms/bf-bc-vstemplate" target="_blank">Bot Application .NET template</a> as a starting point. Note that you need to build the language model for the weather bot in LUIS first. To accomplish this, follow the steps in this <a href="https://www.youtube.com/watch?v=39L0Gv2EcSk&index=5&list=PLD7HFcN7LXRdHkFBFu4stPPeWJcQ0VFLx" target="_blank">video</a>.

After you set up your language model, create your project with the Bot Application template, and add the following class to handle the integration with your LUIS language model.

```cs
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
                // Add code to retrieve the weather
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

Next, go to **MessagesController.cs** and add the following namespaces.

```cs
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
```

Finally, in the same file, replace the code in the `Post` task with this code.  

```cs
[ResponseType(typeof(void))]
public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
{
    if (activity.Type == ActivityTypes.Message)
    {
        await Conversation.SendAsync(activity, () => new TravelGuidDialog());
    }
    else
    {
        // Add code to handle errors, or non-messaging activities
    }

    return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
}
```

### Text Analytics example

In this example, you will use the Text Analytics API to determine the sentiment behind a user's message, i.e. whether it is positive or negative. The Text Analytics API returns a sentiment score between 0 and 1, where 0 is very negative and 1 is very positive. For example, if the user types "That was really helpful", the API will classify it with a highly positive score, whereas a phrase like "That didn't help at all" will return a negative score. 

The following example shows how the bot's response can be customized according to the sentiment score calculated by the Text Analytics API. You will use the <a href="http://aka.ms/bf-bc-vstemplate" target="_blank">Bot Application .NET template</a> as a starting point. Note that the `Newtonsoft.JSON` package is also required, which can be obtained via <a href="https://www.nuget.org/packages/Microsoft.ProjectOxford.Vision/" target="_blank">NuGet</a>. 

After you create your project with the Bot Application template, you will create some classes to hold the input and output from the API. First, create a new C# class file (**TextAnalyticsCall.cs**) with the following code. The class will serve as a model for the JSON input/output of the Text Analytics API.    

```cs
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
```

Next, go to **MessagesController.cs** and add the following namespaces.

```cs
using System.Collections.Generic;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
```

Finally, replace the code in the `Post` task with this code, which receives the user message, calls the sentiment analysis endpoint, and responds accordingly to the user.

```cs
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
		// Add code to handle errors, or non-messaging activities
	}
	var response = Request.CreateResponse(HttpStatusCode.OK);
	return response;
}
```

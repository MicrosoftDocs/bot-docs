---
layout: page
title: Search
permalink: /en-us/bot-intelligence/search/
weight: 8260
parent1: Bot Intelligence
---


* TOC
{:toc}

## Summary
The Bing Search APIs enable you to add intelligent web search capabilities to your bots. With a few lines of code, you can access billions of webpages, images, videos, news and other result types. You can configure the APIs to return results by geographical location, market, or language for better relevance. You can further customize your search using the supported search parameters. Examples of search parameters include *safesearch*, to filter out adult content, and *freshness*, to get back results according to the date they were indexed by Bing. 

## API Overview
There are 5 Bing Search APIs available in Cognitive Services to retrieve web, image, video, news and autosuggest results respectively: 

- The [Web Search API](https://www.microsoft.com/cognitive-services/en-us/bing-web-search-api){:target="_blank"} provides web, image, video, news and related search results with a single API call.
- The [Image Search API](https://www.microsoft.com/cognitive-services/en-us/bing-image-search-api){:target="_blank"} returns image results with enhanced metadata (dominant color, image kind, etc.) and supports several image filters to customize the results.
- The [Video Search API](https://www.microsoft.com/cognitive-services/en-us/bing-video-search-api){:target="_blank"} retrieves video results with rich metadata (video size, quality, price, etc.), video previews, and supports several video filters to customize the results. 
- The [News Search API](https://www.microsoft.com/cognitive-services/en-us/bing-news-search-api){:target="_blank"} finds news articles around the world that match your search query, or are currently trending on the Internet.
- Finally, the [Autosuggest API](https://www.microsoft.com/cognitive-services/en-us/bing-autosuggest-api){:target="_blank"} offers instant query completion suggestions to complete your search query faster with less typing.  

## Use Cases for Bots
The Search APIs offer a great way to access the vast information available on the web. They are ideal for any bot that needs to embed search results directly into their messages, or leverage them as input for other interesting applications. Besides plain search, the APIs support several intelligent features that can be used across a broad array of scenarios. For example, the Image Search API includes image understanding features, such as celebrity recognition, product search (where to buy), and visually similar search, whereas the News Search API is able to extract mentioned entities and other useful article metadata. 

Here a few examples of bots that are using the Search APIs today:  

- The [Bing News Bot](https://bots.botframework.com/bot?id=BingNews){:target="_blank"} uses the News Search API to find the latest news and display it to users. 
- The [Caption Bot](https://bots.botframework.com/bot?id=captionbot){:target="_blank"} leverages the Image Search API to identify celebrities, or discover visually similar celebrities, in submitted images. 
- The [Murphy Bot](https://bots.botframework.com/bot?id=MorphiBot){:target="_blank"} uses the Image Search API to find photos with faces, which are then used to generate creative "what if" photos. 

## Getting Started
To get started, you need to obtain your own subscription key from the Microsoft Cognitive Services site. Our [Getting Started](https://msdn.microsoft.com/en-US/library/mt712546.aspx){:target="_blank"} guide describes how to obtain the key and start making calls to the APIs. If you already have a subscription key, try our [API Testing Console](https://bingapis.portal.azure-api.net/docs/services/56b43eeccf5ff8098cef3807/operations/56b4447dcf5ff8098cef380d){:target="_blank"} to craft test API requests in a sandbox environment. You can find detailed documentation about each API, including developer guides and API references by navigating to the Cognitive Services [documentation site](https://www.microsoft.com/cognitive-services/en-us/documentation){:target="_blank"} and selecting the API you are interested in from the navigation bar on the left side of the screen. 

## Example: GIF Search Bot
Let's build a few bots that use the Search APIs to help you get started. For our first example, we will build a simple bot that searches the web for animated GIF images, via the Image Search API, and displays them to users. We will use the [Bot Application .NET template](http://docs.botframework.com/connector/getstarted/#getting-started-in-net){:target="_blank"} as our starting point. Note that this example, as well as subsequent ones, require the *Newtonsoft.JSON* package, which can be obtained via NuGet. 

After you create your project with the Bot Application template, add the Newtonsoft.JSON package and create a new C# class file (*BingImageSearchResponse.cs*) with the following code. The class will serve as our model for the JSON response returned by the Image Search API. 

{% highlight c# %}

public class BingImageSearchResponse
{
    public string _type { get; set; }
    public int totalEstimatedMatches { get; set; }
    public string readLink { get; set; }
    public string webSearchUrl { get; set; }
    public ImageResult[] value { get; set; }
}

public class ImageResult
{
    public string name { get; set; }
    public string webSearchUrl { get; set; }
    public string thumbnailUrl { get; set; }
    public object datePublished { get; set; }
    public string contentUrl { get; set; }
    public string hostPageUrl { get; set; }
    public string contentSize { get; set; }
    public string encodingFormat { get; set; }
    public string hostPageDisplayUrl { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public string accentColor { get; set; }
}

{% endhighlight %}

Next, go to *MessagesController.cs*. First, make sure you add the Newtonsoft.Json namespace. 

{% highlight c# %}
using Newtonsoft.Json;

{% endhighlight %}

Then, on the same file, replace the code in the Post task with the one in the code snippet below. The code calls the Image API, de-serializes the response into the model class we just created, and finally retrieves the first image result, and sends it to the user as an image attachment.   

{% highlight c# %}

public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
{
    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

    if (activity == null || activity.GetActivityType() != ActivityTypes.Message)
    {
        //add code to handle errors, or non-messaging activities
    }

    const string apiKey = "<'YOUR API KEY FROM MICROSOFT.COM/COGNITIVE'>";
    string queryUri = "https://api.cognitive.microsoft.com/bing/v5.0/images/search"
                      + "?q=" + activity.Text
                      + "&imageType=AnimatedGif"; //parameter to filter by GIF image type
    
    HttpClient client = new HttpClient();
    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiKey); //authentication header to pass the API key
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    string bingRawResponse = null; 
    BingImageSearchResponse bingJsonResponse = null; 

    try
    {
        bingRawResponse = await client.GetStringAsync(queryUri);
        bingJsonResponse = JsonConvert.DeserializeObject<BingImageSearchResponse>(bingRawResponse);
    }
    catch (Exception e)
    {
        //add code to handle exceptions while calling the REST endpoint and/or deserializing the object
    }

    ImageResult [] imageResult = bingJsonResponse.value;
    if (imageResult == null || imageResult.Length == 0)
    {
        //add code to handle the case where results are null or zero
    }
    string firstResult = imageResult[0].contentUrl; //we only need the first result for this example
    
    var replyMessage = activity.CreateReply();
    replyMessage.Recipient = activity.From;
    replyMessage.Type = ActivityTypes.Message; 
    replyMessage.Text = $"Here is what i found:";
    replyMessage.Attachments = new System.Collections.Generic.List<Attachment>();
    replyMessage.Attachments.Add(new Attachment()
    {
        ContentUrl = firstResult,
        ContentType = "image/png"
    });
    
    //Reply to user message with image attachment
    await connector.Conversations.ReplyToActivityAsync(replyMessage);
}  

{% endhighlight %}

## Example: Trending News Bot
For our second example, we will build a bot that fetches the top trending news on the web, and displays it to users as an image carousel. We will rely on the Bot Application template again. The coding steps are similar to the previous example, but this one is using the new carousel attachment type that is supported in the latest version of the Bot Builder SDK.   

To begin, create a new project with the Bot Application template, and add the Newtonsoft.JSON package via NuGet. Then, create a new C# class file (BingTrendingNewsResults.cs) that will host the model classes for the JSON response returned by the News Search API this time.

{% highlight c# %}
public class BingTrendingNewsResults
{
    public string _type { get; set; }
    public string readLink { get; set; }
    public NewsResult[] value { get; set; }
}
public class NewsResult
{
    public string name { get; set; }
    public string url { get; set; }
    public Image image { get; set; }
    public string description { get; set; }
    public string category { get; set; }
}
public class Image
{
    public Thumbnail thumbnail { get; set; }
}
public class Thumbnail
{
    public string contentUrl { get; set; }
    public int width { get; set; }
    public int height { get; set; }
}
{% endhighlight %}

Next, go to *MessagesController.cs* and add the following namespaces. 

{% highlight c# %}
using Newtonsoft.Json;
using System.Collections.Generic;
{% endhighlight %}

On the same file, replace the code in the Post task with the one that follows. The code fetches the latest trending news via the News Search API and stores it in the newsResult object.  

{% highlight c# %}

ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

if (activity == null || activity.GetActivityType() != ActivityTypes.Message)
{
    //add code to handle errors, or non-messaging activities
}

const string apiKey = "<'YOUR API KEY FROM MICROSOFT.COM/COGNITIVE'>";
string queryUri = "https://api.cognitive.microsoft.com/bing/v5.0/news/search";

//Helper objects to call the News Search API and store the response
HttpClient httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", bingAPIkey); //authentication header to pass the API key
httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
string bingRawResponse; //raw response from REST endpoint
BingTrendingNewsResults bingJsonResponse = null; //Deserialized response 

try
{
    bingRawResponse = await httpClient.GetStringAsync(queryUri);
    bingJsonResponse = JsonConvert.DeserializeObject<BingTrendingNewsResults>(bingRawResponse);
}
catch (Exception e)
{
    //add code to handle exceptions while calling the REST endpoint and/or deserializing the object
}

NewsResult [] newsResult = bingJsonResponse.value; 

if (newsResult == null || newsResult.Length == 0)
{
    //add code to handle the case where results are null are zero
}

{% endhighlight %}

Finally, add the following code to the Post task to present the top 5 trending news as an image carousel to users: 

{% highlight c# %}
int newsResultCount = Math.Min(5, newsResult.Length); // show up to 5 trending news 
Activity replyMessage = activity.CreateReply("Here are the top trending news I found:");
replyMessage.Recipient = activity.From;
replyMessage.Type = ActivityTypes.Message;
replyMessage.AttachmentLayout = "carousel";
replyMessage.Attachments = new List<Attachment>();

for (int i=0; i < newsResultCount; i++)
{
    Attachment attachment = new Attachment();
    attachment.ContentType = "application/vnd.microsoft.card.hero";

    //Construct Card
    HeroCard card = new HeroCard();
    card.Title = newsResult[i].name;
    card.Subtitle = newsResult[i].description;

    //Add Card Image
    card.Images = new List<CardImage>();
    cardImage img = new CardImage();
    img.Url = newsResult[i].image.thumbnail.contentUrl;
    card.Images.Add(img);

    //Add Card Buttons
    card.Buttons = new List<CardAction>();
    CardAction btnArticle = new CardAction();

    //Go to article button
    btnArticle.Title = "Go to article";
    btnArticle.Type = "openUrl";
    btnArticle.Value = newsResult[i].url;
    card.Buttons.Add(btnArticle);
    attachment.Content = card;

    replyMessage.Attachments.Add(attachment);
}
//Reply to user message with the news carousel attachment
await connector.Conversations.ReplyToActivityAsync(replyMessage);
var response = Request.CreateResponse(HttpStatusCode.OK);
return response;

} //end of MessagesController.cs

{% endhighlight %}


## Example: Product Bot
For our last example, we will build a bot that receives a product image url and finds visually similar products along with links to online merchants that have these products. The bot is calling the Image Search API to find visually similar products and the online merchants. More specifically, it's calling a feature of the Image Search API called [image insights](https://msdn.microsoft.com/en-us/library/mt712790.aspx){:target="_blank"}, which returns several interesting insights about images indexed by Bing, such as visually similar images or products, similar image collections, recognized entities (people) and more. 

The coding steps are very similar to the previous example. Start by creating a new project with the Bot Application template, and add the Newtonsoft.JSON package via NuGet. Then, create a new C# class file (BingImageInsights.cs) that will host the model classes for the JSON response returned by the image insights module that is part of the Image Search API. 


{% highlight c# %}

public class BingImageInsights
{
    public VisuallysimilarImage[] visuallySimilarImages { get; set; }
}

public class VisuallysimilarImage
{
    public string name { get; set; }
    public string webSearchUrl { get; set; }
    public string thumbnailUrl { get; set; }
    public DateTime datePublished { get; set; }
    public string contentUrl { get; set; }
    public string hostPageUrl { get; set; }
    public string contentSize { get; set; }
    public string encodingFormat { get; set; }
    public string hostPageDisplayUrl { get; set; }
    public int width { get; set; }
    public int height { get; set; }
    public Thumbnail thumbnail { get; set; }
    public string imageInsightsToken { get; set; }
    public Insightssourcessummary insightsSourcesSummary { get; set; }
    public string imageId { get; set; }
    public string accentColor { get; set; }
}

public class Thumbnail
{
    public int width { get; set; }
    public int height { get; set; }
}

{% endhighlight %}

Next, go to *MessagesController.cs* and add the following namespaces. 

{% highlight c# %}

using System.IO;
using System.Web;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;

{% endhighlight %}

On the same file, replace the code in the Post task with the one that follows. The code reads the image attachment or url, and sends it to the Image Search API to retrieve visually similar products, along with online merchant locations.   

{% highlight c# %}

ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

if (activity == null || activity.GetActivityType() != ActivityTypes.Message)
{
    //add code to handle errors, or non-messaging activities
}

const string bingAPIkey = "<YOUR API KEY FROM MICROSOFT.COM/COGNITIVE>"; 
Activity reply = activity.CreateReply("Something went wrong. Did you upload an image? I'm more of a visual bot. " +
                                       "Try sending me an image or an image url"); //default reply

//Objects to call the Image Search API, and store the deserialized response
HttpClient httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", bingAPIkey); //Authentication header to pass the API key
httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
string bingRawResponse; //raw response from REST endpoint
BingImageInsights bingJsonResponse = null; //Deserialized response 

//Uri to call the Image Search API
string queryUri = "https://api.cognitive.microsoft.com/bing/v5.0/images/search"
                                 + "?q="
                                 + "&modulesRequested=All"
                                 + "&mkt=en-us";
                                 + "&imgurl=" + activity.Text;

try
{
    bingRawResponse = await httpClient.GetStringAsync(queryUri);
    bingJsonResponse = JsonConvert.DeserializeObject<BingImageInsights>(bingRawResponse);
}
catch (Exception e)
{
    //toDo: Handle uncaught exceptions
}

if (bingJsonResponse.visuallySimilarImages.Length <= 0)
{
    Activity errorReplyToConv = activity.CreateReply("Sorry, I could not find any similar products :(");
    await connector.Conversations.ReplyToActivityAsync(errorReplyToConv);
    return null;
}


{% endhighlight %}

Finally, add the following code to present the first 10 visually similar products as an image carousel. Each item in the image carousel contains two action buttons that point to the online merchant's site, and the bing image details page respectively.   

{% highlight c# %}

int productCount = Math.Min(10, bingJsonResponse.visuallySimilarImages.Length); //show up to 10 products

//Construct Reply Message
Activity replyToConv = activity.CreateReply("Here are some visually similar products I found");
replyToConv.Recipient = activity.From;
replyToConv.Type = ActivityTypes.Message;
replyToConv.AttachmentLayout = "carousel";

 //Construct Attachment
 replyToConv.Attachments = new List<Attachment>();

for (int i = 0; i < productCount; i++)
{
    Attachment plAttachment = new Attachment();
    plAttachment.ContentType = "application/vnd.microsoft.card.hero";

    //Construct Card
    HeroCard plCard = new HeroCard();
    plCard.Title = bingJsonResponse.visuallySimilarImages[i].name;
    plCard.Subtitle = bingJsonResponse.visuallySimilarImages[i].hostPageDisplayUrl;

    //Add Card Image
    plCard.Images = new List<CardImage>();
    CardImage img = new CardImage();
    img.Url = bingJsonResponse.visuallySimilarImages[i].thumbnailUrl;
    plCard.Images.Add(img);

    //Add Card Buttons
    plCard.Buttons = new List<CardAction>();
    CardAction plButtonBuy = new CardAction();
    CardAction plButtonSearch = new CardAction();

    //Buy Button
    plButtonBuy.Title = "Buy from merchant";
    plButtonBuy.Type = "openUrl";
    plButtonBuy.Value = bingJsonResponse.visuallySimilarImages[i].hostPageUrl;

    //Search More button 
    plButtonSearch.Title = "Find more in Bing";
    plButtonSearch.Type = "openUrl";
    plButtonSearch.Value = bingJsonResponse.visuallySimilarImages[i].webSearchUrl;

    plCard.Buttons.Add(plButtonBuy);
    plCard.Buttons.Add(plButtonSearch);
    plAttachment.Content = plCard;

    //Add attachment to attachments list
    replyToConv.Attachments.Add(plAttachment);
}
//Respond to user with message and attachment list
await connector.Conversations.ReplyToActivityAsync(replyToConv);
return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
          
{% endhighlight %}

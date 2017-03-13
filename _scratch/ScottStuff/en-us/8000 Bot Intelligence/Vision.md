---
layout: page
title: Vision
permalink: /en-us/bot-intelligence/vision/
weight: 8210
parent1: Bot Intelligence
---

* TOC
{:toc}

## Summary
The Vision APIs bring advanced image and video understanding skills to your bots. They are powered by state-of-the-art algorithms, which allow you to process images or videos and get back information you can transform into actions. For example, you can use them to recognize objects, people's faces, age, gender or even feelings. The Vision APIs support a variety of image understanding features, such as identifying mature or explicit content, estimating dominant and accent colors, categorizing the content of images, performing optical character recognition, as well as describing an image with complete english sentences. Additionally, the Vision APIs support several image and video processing capabilities, such as intelligently generating image or video thumbnails, or stabilizing the output of a video. You can play with the popular [CaptionBot.ai](https://www.captionbot.ai/){:target="_blank"} to see some of the Vision APIs in action, or read the examples below for step-by-step instructions to get started. 

## API Overview
There are 4 APIs available in Cognitive Services that can process images or videos: 

- The [Computer Vision API](https://www.microsoft.com/cognitive-services/en-us/computer-vision-api){:target="_blank"} extracts rich information about objects and people in images, determines if the image contains mature or explicit content, and also processes text (OCR) in images. 
- The [Emotion API](https://www.microsoft.com/cognitive-services/en-us/emotion-api){:target="_blank"} analyzes human faces and recognizes their emotion across eight possible categories of human emotions. 
- The [Face API](https://www.microsoft.com/cognitive-services/en-us/face-api){:target="_blank"} detects human faces, compares them to similar faces, and can even organize people into groups according to visual similarity. 
- The [Video API](https://www.microsoft.com/cognitive-services/en-us/video-api){:target="_blank"} analyzes and processes video to stabilize video output, detect motion, track faces, as well as intelligently generate a motion thumbnail summary of the video.    

## Use Cases for Bots
The Vision APIs are useful for any bot that receives images as input from users and wants to distill actionable information from them. Here are a few examples:

- You can use the Computer Vision API to understand objects or even celebrities in an image. For example, [CaptionBot.ai](https://www.captionbot.ai/){:target="_blank"} is using the Computer Vision API to identify objects, people (celebrities), to generate a human-readable caption of the image.
- You can use the Face API to detect faces, along with infomation about people's age, gender and facial landmarks, and even match faces to similar ones. So your bot can respond appropriately according to a user's unique facial attributes.  
- You can use the Emotion API to identify people's emotions. So, if a user uploads a sad selfie, the bot can reply with an appropriate message.

## Getting Started
Before you get started, you need to obtain your own subscription key from the [Microsoft Cognitive Services](https://www.microsoft.com/cognitive-services/){:target="_blank"} site. Our Getting Started guides (available for [C#](https://www.microsoft.com/cognitive-services/en-us/computer-vision-api/documentation/getstarted/getstartedvisionapiforwindows){:target="_blank"} and [Python](https://www.microsoft.com/cognitive-services/en-us/computer-vision-api/documentation/getstarted/getstartedwithpython){:target="_blank"}) describe how to obtain the key and start making calls to the APIs. You can find detailed documentation about each API, including developer guides and API references by navigating to the Cognitive Services [documentation site](https://www.microsoft.com/cognitive-services/en-us/documentation){:target="_blank"} and selecting the API you are interested in from the navigation bar on the left side of the screen. 

## Example: Vision Bot
Let's build a few bots that use the Vision APIs to help you get started. For our first example, we will build a simplified version of CaptionBot.ai. The Vision Bot can receive an image, either as an attachment or url, and then return a computer-generated caption of the image via the Computer Vision API. We will use the [Bot Application .NET template](http://docs.botframework.com/connector/getstarted/#getting-started-in-net){:target="_blank"} as our starting point. 

After you create your project with the Bot Application.NET template, install the *Microsoft.ProjectOxford.Vision* package from [nuGet](https://www.nuget.org/packages/Microsoft.ProjectOxford.Vision/){:target="_blank"}. Next, go to *MessagesController.cs* class file and add the following namespaces.

{% highlight c# %}

using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.IO;
using System.Web;

{% endhighlight c# %}

On the same file, replace the code in the Post task with the one in the snippet below. The code initializes the Computer Vision SDK classes that take care most of the hard work.  

{% highlight c# %}

ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
const string visionApiKey = "<YOUR API KEY FROM MICROSOFT.COM/COGNITIVE>"; 
           
//Vision SDK classes
VisionServiceClient visionClient = new VisionServiceClient(visionApiKey);
VisualFeature[] visualFeatures = new VisualFeature[] { 
                                        VisualFeature.Adult, //recognize adult content
                                        VisualFeature.Categories, //recognize image features
                                        VisualFeature.Description //generate image caption
                                        }; 
AnalysisResult analysisResult = null;
{% endhighlight c# %}

Continue by adding the code below that reads the image sent by the user as an attachment or url and sends it to the Computer Vision API for analysis.   

{% highlight c# %}

if (activity == null || activity.GetActivityType() != ActivityTypes.Message)
{
    //add code to handle errors, or non-messaging activities
}

//If the user uploaded an image, read it, and send it to the Vision API
if (activity.Attachments.Any() && activity.Attachments.First().ContentType.Contains("image"))
{
   //stores image url (parsed from attachment or message)
   string uploadedImageUrl = activity.Attachments.First().ContentUrl; ;
   uploadedImageUrl = HttpUtility.UrlDecode(uploadedImageUrl.Substring(uploadedImageUrl.IndexOf("file=") + 5));

   using (Stream imageFileStream = File.OpenRead(uploadedImageUrl))
   {
       try
       {
            analysisResult = await visionClient.AnalyzeImageAsync(imageFileStream, visualFeatures);
       }
       catch (Exception e)
       {
            analysisResult = null; //on error, reset analysis result to null
       }
   }
}
//Else, if the user did not upload an image, determine if the message contains a url, and send it to the Vision API
else
{
    try
    {
        analysisResult = await visionClient.AnalyzeImageAsync(activity.Text, visualFeatures);
    }
    catch (Exception e)
    {
        analysisResult = null; //on error, reset analysis result to null
    }
}           

{% endhighlight c# %}

Finally, add the following code to read the analysis results from the Computer Vision API and respond to the user.

{% highlight c# %}

Activity reply = activity.CreateReply("Did you upload an image? I'm more of a visual person. " +
                                      "Try sending me an image or an image url"); //default reply

if (analysisResult != null)
{
    string imageCaption = analysisResult.Description.Captions[0].Text;
    reply = activity.CreateReply("I think it's " + imageCaption);
}
await connector.Conversations.ReplyToActivityAsync(reply);
return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);

{% endhighlight c# %}

## Example: Emotion Bot
For our second example, we will build an Emotion Bot that receives an image url, detects if there's at least one face in the image, and finally responds back with the dominant emotion of that face. To keep the example simple, the bot will only return the emotion for only one face, and ignore other faces in the image. The example requires the *Microsoft.ProjectOxford.Emotion* package, which can be obtained via NuGet. 

Create a new project with the [Bot Application .NET template](http://docs.botframework.com/connector/getstarted/#getting-started-in-net){:target="_blank"}. Install the Microsoft.ProjectOxford.Emotion package from [nuGet](https://www.nuget.org/packages/Microsoft.ProjectOxford.Emotion/){:target="_blank"}. Next, go to *MessagesController.cs* class file and add the following namespaces.

{% highlight c# %}
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System.Collections.Generic;
{% endhighlight c# %}

Then, replace the code in the Post task with the one in the snippet below. The code reads the image url from the user, sends it to the Emotion API and finally replies back to the user with the dominant emotion it recognized for a face in the image, including also the confidence score. 

{% highlight c# %}

public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
{
    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
    const string emotionApiKey = "<YOUR API KEY FROM MICROSOFT.COM/COGNITIVE>"; 

    //Emotion SDK objects that take care of the hard work
    EmotionServiceClient emotionServiceClient = new EmotionServiceClient(emotionApiKey);
    Emotion[] emotionResult = null;

    try
    {
        emotionResult = await emotionServiceClient.RecognizeAsync(activity.Text);
    }
    catch (Exception e)
    {
        emotionResult = null;
    }
    
    Activity reply = activity.CreateReply("Could not find a face, or something went wrong. " + 
                                          "Try sending me a photo with a face");

    if (emotionResult != null)
    {
        Scores emotionScores = emotionResult[0].Scores;

        //Retrieve list of emotions for first face detected and sort by emotion score (desc)
        IEnumerable<KeyValuePair<string, float>> emotionList = new Dictionary<string, float>()
        {
            { "angry", emotionScores.Anger},
            { "contemptuous", emotionScores.Contempt },
            { "disgusted", emotionScores.Disgust },
            { "frightened", emotionScores.Fear },
            { "happy", emotionScores.Happiness},
            { "neutral", emotionScores.Neutral},
            { "sad", emotionScores.Sadness },
            { "surprised", emotionScores.Surprise}   
        }
        .OrderByDescending(kv => kv.Value)
        .ThenBy(kv => kv.Key)
        .ToList();

        KeyValuePair<string, float> topEmotion = emotionList.ElementAt(0);
        string topEmotionKey = topEmotion.Key;
        float topEmotionScore = topEmotion.Value;

        reply = activity.CreateReply("I found a face! I am " + (int)(topEmotionScore*100) + 
                                     "% sure the person seems " + topEmotionKey);
    }   
    await connector.Conversations.ReplyToActivityAsync(reply);
    return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
}

{% endhighlight c# %}

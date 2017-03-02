---
title: Adding Vision Capabilities with Cognitive Services | Microsoft Docs
description: Add vision capabilities to your bot with the Bot Framework and Cognitive Services.
keywords: intelligence, vision, image, video, understanding
author: RobStand
manager: rstand
ms.topic: intelligence-vision-article

ms.prod: botframework
ms.service: Cognitive Services
ms.date: 03/01/2017
ms.reviewer: rstand

# Include the following line commented out
#ROBOTS: Index
---
# Add image and video understanding to your bot
The Vision APIs bring advanced image and video understanding skills to your bots. They are powered by state-of-the-art algorithms, which allow you to process images or videos and get back information you can transform into actions. For example, you can use them to recognize objects, people's faces, age, gender or even feelings. The Vision APIs support a variety of image understanding features, such as identifying mature or explicit content, estimating dominant and accent colors, categorizing the content of images, performing optical character recognition, as well as describing an image with complete english sentences. Additionally, the Vision APIs support several image and video processing capabilities, such as intelligently generating image or video thumbnails, or stabilizing the output of a video.

> [!TIP]
> You can play with the popular [CaptionBot.ai](https://www.captionbot.ai/) to see some of the Vision APIs in action.

There are 4 APIs available in Cognitive Services that can process images or videos:

## Computer Vision API
The [Computer Vision API](https://www.microsoft.com/cognitive-services/en-us/computer-vision-api) extracts rich information about objects and people in images, determines if the image contains mature or explicit content, and also processes text (OCR) in images.

## Emotion API
The [Emotion API](https://www.microsoft.com/cognitive-services/en-us/emotion-api) analyzes human faces and recognizes their emotion across eight possible categories of human emotions.

## Face API
The [Face API](https://www.microsoft.com/cognitive-services/en-us/face-api) detects human faces, compares them to similar faces, and can even organize people into groups according to visual similarity.

## Video API
The [Video API](https://www.microsoft.com/cognitive-services/en-us/video-api) analyzes and processes video to stabilize video output, detect motion, track faces, as well as intelligently generate a motion thumbnail summary of the video.    

## Vision API examples for bots
The Vision APIs are useful for any bot that receives images as input from users and wants to distill actionable information from them. Here are a few examples:

- You can use the Computer Vision API to understand objects or even celebrities in an image. For example, [CaptionBot.ai](https://www.captionbot.ai/) is using the Computer Vision API to identify objects, people (celebrities), in order to generate a human-readable caption of the image.
- You can use the Face API to detect faces, along with information about people's age, gender and facial landmarks, and even match faces to similar ones. So your bot can respond appropriately according to a user's unique facial attributes.  
- You can use the Emotion API to identify people's emotions. So, if a user uploads a sad selfie, the bot can reply with an appropriate message.

> [!IMPORTANT]
Before you get started with these examples, you need to obtain your own subscription key from the [Microsoft Cognitive Services](https://www.microsoft.com/cognitive-services/) site. The Getting Started guides (available for [C#](https://www.microsoft.com/cognitive-services/en-us/computer-vision-api/documentation/getstarted/getstartedvisionapiforwindows) and [Python](https://www.microsoft.com/cognitive-services/en-us/computer-vision-api/documentation/getstarted/getstartedwithpython)) describe how to obtain the key and start making calls to the APIs.

> [!TIP]
>You can find detailed documentation about each API, including developer guides and API references by navigating to the Cognitive Services [documentation site](https://www.microsoft.com/cognitive-services/en-us/documentation) and selecting the API you are interested in from the navigation bar on the left side of the screen.

### Vision API example
This example builds a simplified version of CaptionBot.ai. The Vision Bot can receive an image, either as an attachment or url, and then return a computer-generated caption of the image via the Computer Vision API. We will use the [Bot Application .NET template](http://docs.botframework.com/connector/getstarted/#getting-started-in-net) as our starting point.

```html
<div align="center">
<br>
<h4>Chat with Vision bot</h4>
<iframe width="700" height="500" src='https://webchat.botframework.com/embed/visionbot?s=PHyAulBypcw.cwA.my0.pPuhVC0VqtOR4yIVkVjFXwjc9HUTsrQ2WHcYvQkFjGE'></iframe>
<br><br><br>
</div>
```

After you create your project with the Bot Application.NET template, install the *Microsoft.ProjectOxford.Vision* package from [nuGet](https://www.nuget.org/packages/Microsoft.ProjectOxford.Vision/). Next, go to *MessagesController.cs* class file and add the following namespaces.

```cs

using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System.IO;
using System.Web;

```

In the same file, replace the code in the Post task with the one in the snippet below. The code initializes the Computer Vision SDK classes that take care most of the hard work.  

```cs

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
```

Continue by adding the code below that reads the image sent by the user as an attachment or url and sends it to the Computer Vision API for analysis.   

```cs

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
```

Finally, add the following code to read the analysis results from the Computer Vision API and respond to the user.

```cs

Activity reply = activity.CreateReply("Did you upload an image? I'm more of a visual person. " +
                                      "Try sending me an image or an image url"); //default reply

if (analysisResult != null)
{
    string imageCaption = analysisResult.Description.Captions[0].Text;
    reply = activity.CreateReply("I think it's " + imageCaption);
}
await connector.Conversations.ReplyToActivityAsync(reply);
return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);

```

### Emotion API example
For our second example, we will build an Emotion Bot that receives an image url, detects if there's at least one face in the image, and finally responds back with the dominant emotion of that face. To keep the example simple, the bot will only return the emotion for only one face, and ignore other faces in the image. The example requires the *Microsoft.ProjectOxford.Emotion* package, which can be obtained via NuGet.

Create a new project with the [Bot Application .NET template](http://docs.botframework.com/connector/getstarted/#getting-started-in-net). Install the Microsoft.ProjectOxford.Emotion package from [nuGet](https://www.nuget.org/packages/Microsoft.ProjectOxford.Emotion/). Next, go to *MessagesController.cs* class file and add the following namespaces.

```cs
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System.Collections.Generic;
```

Then, replace the code in the Post task with the one in the snippet below. The code reads the image url from the user, sends it to the Emotion API and finally replies back to the user with the dominant emotion it recognized for a face in the image, including also the confidence score.

```cs

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

```

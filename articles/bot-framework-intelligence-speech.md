---
title: Adding Speech Capabilities with Cognitive Services | Microsoft Docs
description: Add speech capabilities to your bot with the Bot Framework and Cognitive Services.
keywords: intelligence, speech, acoustic, recognition, cris, recognition
author: RobStand
manager: rstand
ms.topic: intelligence-speech-article

ms.prod: botframework
ms.service: Cognitive Services
ms.date: 
ms.reviewer: rstand

# Include the following line commented out
#ROBOTS: Index

---


# Add speech recognition and conversion to your bot


> [!TIP]
> You can find detailed documentation, developer guides, and API references on the Cognitive Services <a href="https://www.microsoft.com/cognitive-services/en-us/documentation" target="_blank">documentation site</a> and then on the left selecting the API you are interested in.

## Speech API examples for bots
The Speech APIs enable your bots to parse audio and extract useful information from voice input. For example, bots can identify the presence of certain words or access the transcribed text to perform an action. 

On messaging channels that support voice input, bots can leverage the Speech APIs to recognize what users are saying, rather than relying on text messages. 

The Speaker Recognition APIs can be used as a means to identify or even authenticate users through their unique voiceprint.

> [!IMPORTANT]
> Before you get started, you need to obtain your own subscription key from the Microsoft Cognitive Services site. The <a href="https://www.microsoft.com/cognitive-services/en-us/speech-api/documentation/getstarted/getstartedcsharpdesktop" target="_blank">Getting Started</a> guide for the Speech API describes how to obtain the key and start making calls to the APIs.

### Speech-To-Text example
In this example, you will build a simple bot that leverages the Speech API to perform speech-to-text conversion. This bot receives an audio file and either responds with the transcribed text or provides some interesting information about the audio it received, such as word, character and vowel count. This example will use the <a href="http://docs.botframework.com/connector/getstarted/#getting-started-in-net" target="_blank">Bot Application .NET template</a> as a starting point. Note that this example requires the **Newtonsoft.JSON** package, which can be obtained via <a href="https://www.nuget.org/packages/Microsoft.ProjectOxford.Vision/" target="_blank">nuGet</a>.

After you create your project with the Bot Application template, add the **Newtonsoft.JSON** package, and then open the **MessagesController.cs** file. Start by adding the following namespaces.

```cs
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Threading;
using System.Web;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.ComponentModel;
using Newtonsoft.Json.Linq;
```

Next, you will add required classes to handle the access token and authentication for the Speech API.

```cs
[DataContract]
public class AccessTokenInfo
{
    [DataMember]
    public string access_token { get; set; }
    [DataMember]
    public string token_type { get; set; }
    [DataMember]
    public string expires_in { get; set; }
    [DataMember]
    public string scope { get; set; }
}

public class Authentication
{
    public static readonly string AccessUri = "https://oxford-speech.cloudapp.net/token/issueToken";
    private string clientId;
    private string clientSecret;
    private string request;
    private AccessTokenInfo token;
    private Timer accessTokenRenewer;

    //Access token expires every 10 minutes. Renew it every 9 minutes only.
    private const int RefreshTokenDuration = 9;

    public Authentication(string clientId, string clientSecret)
    {
        this.clientId = clientId;
        this.clientSecret = clientSecret;

        //If clientid or client secret has special characters, encode before sending request
        this.request = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope={2}",
                                          HttpUtility.UrlEncode(clientId),
                                          HttpUtility.UrlEncode(clientSecret),
                                          HttpUtility.UrlEncode("https://speech.platform.bing.com"));

        this.token = HttpPost(AccessUri, this.request);

        // renew the token every specfied minutes
        accessTokenRenewer = new Timer(new TimerCallback(OnTokenExpiredCallback),
                                       this,
                                       TimeSpan.FromMinutes(RefreshTokenDuration),
                                       TimeSpan.FromMilliseconds(-1));
    }

    //Return the access token
    public AccessTokenInfo GetAccessToken()
    {
        return this.token;
    }

    //Renew the access token
    private void RenewAccessToken()
    {
        AccessTokenInfo newAccessToken = HttpPost(AccessUri, this.request);
        //swap the new token with old one
        //Note: the swap is thread unsafe
        this.token = newAccessToken;
        Console.WriteLine(string.Format("Renewed token for user: {0} is: {1}",
                          this.clientId,
                          this.token.access_token));
    }
    //Call-back when we determine the access token has expired
    private void OnTokenExpiredCallback(object stateInfo)
    {
        try
        {
            RenewAccessToken();
        }
        catch (Exception ex)
        {
            Console.WriteLine(string.Format("Failed renewing access token. Details: {0}", ex.Message));
        }
        finally
        {
            try
            {
                accessTokenRenewer.Change(TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Failed to reschedule timer to renew access token. Details: {0}", ex.Message));
            }
        }
    }

    //Helper function to get new access token
    private AccessTokenInfo HttpPost(string accessUri, string requestDetails)
    {
        //Prepare OAuth request
        WebRequest webRequest = WebRequest.Create(accessUri);
        webRequest.ContentType = "application/x-www-form-urlencoded";
        webRequest.Method = "POST";
        byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
        webRequest.ContentLength = bytes.Length;
        using (Stream outputStream = webRequest.GetRequestStream())
        {
            outputStream.Write(bytes, 0, bytes.Length);
        }
        using (WebResponse webResponse = webRequest.GetResponse())
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AccessTokenInfo));
            //Get deserialized object from JSON stream
            AccessTokenInfo token = (AccessTokenInfo)serializer.ReadObject(webResponse.GetResponseStream());
            return token;
        }
    }
}

```

Next, write the function that implements the speech-to-text conversion. Note that the function requires a working Speech API key, which can be obtained via your Cognitive Services <a href="https://www.microsoft.com/cognitive-services/en-US/subscriptions" target="_blank">subscription page</a>.

```cs

private string DoSpeechReco(Attachment attachment)
{
    AccessTokenInfo token;
    string headerValue;
    // Note: Sign up at https://microsoft.com/cognitive to get a subscription key.  
    // Use the subscription key as Client secret below.
    Authentication auth = new Authentication("YOURUSERID", "<YOUR API KEY FROM MICROSOFT.COM/COGNITIVE");
    string requestUri = "https://speech.platform.bing.com/recognize";

    //URI Params. Refer to the Speech API documentation for more information.
    requestUri += @"?scenarios=smd";                                // websearch is the other main option.
    requestUri += @"&appid=D4D52672-91D7-4C74-8AD8-42B1D98141A5";   // You must use this ID.
    requestUri += @"&locale=en-US";                                 // read docs, for other supported languages.
    requestUri += @"&device.os=wp7";
    requestUri += @"&version=3.0";
    requestUri += @"&format=json";
    requestUri += @"&instanceid=565D69FF-E928-4B7E-87DA-9A750B96D9E3";
    requestUri += @"&requestid=" + Guid.NewGuid().ToString();

    string host = @"speech.platform.bing.com";
    string contentType = @"audio/wav; codec=""audio/pcm""; samplerate=16000";
    var wav = HttpWebRequest.Create(attachment.ContentUrl);
    string responseString = string.Empty;

    try
    {
        token = auth.GetAccessToken();
        Console.WriteLine("Token: {0}\n", token.access_token);

        //Create a header with the access_token property of the returned token
        headerValue = "Bearer " + token.access_token;
        Console.WriteLine("Request Uri: " + requestUri + Environment.NewLine);

        HttpWebRequest request = null;
        request = (HttpWebRequest)HttpWebRequest.Create(requestUri);
        request.SendChunked = true;
        request.Accept = @"application/json;text/xml";
        request.Method = "POST";
        request.ProtocolVersion = HttpVersion.Version11;
        request.Host = host;
        request.ContentType = contentType;
        request.Headers["Authorization"] = headerValue;

        using (Stream wavStream = wav.GetResponse().GetResponseStream())
        {
            byte[] buffer = null;
            using (Stream requestStream = request.GetRequestStream())
            {
                int count = 0;
                do
                {
                    buffer = new byte[1024];
                    count = wavStream.Read(buffer, 0, 1024);
                    requestStream.Write(buffer, 0, count);
                } while (wavStream.CanRead && count > 0);
                // Flush
                requestStream.Flush();
            }
            //Get the response from the service.
            Console.WriteLine("Response:");
            using (WebResponse response = request.GetResponse())
            {
               Console.WriteLine(((HttpWebResponse)response).StatusCode);
               using (StreamReader sr = new StreamReader(response.GetResponseStream()))
               {
                    responseString = sr.ReadToEnd();
               }
               Console.WriteLine(responseString);
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        Console.WriteLine(ex.Message);
    }
    dynamic data = JObject.Parse(responseString);
    return data.header.name;
}
```

Finally, replace the code in the `Post` task with the one below. The code parses the voice attachment sent to the bot, calls the speech-to-text conversion function, and then responds back to the user with the transcribed text and related metadata (such as character or word count) for the user's request.  

```cs
public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
{
    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

    var text = message.Text;

    if (activity.Type == ActivityTypes.Message)
    {
        if (activity.Attachments.Any())
        {
            var reco = DoSpeechReco(activity.Attachments.First());

            if (activity.Text.ToUpper().Contains("WORD"))
            {
                text = "You said : " + reco + " Word Count: " + reco.Split(' ').Count();
            }
            else if (activity.Text.ToUpper().Contains("CHARACTER"))
            {
                var nospacereco = reco.ToCharArray().Where(c => c!= ' ').Count();
                text = "You said : " + reco + " Character Count: " + nospacereco;
            }
            else if (activity.Text.ToUpper().Contains("SPACE"))
            {
                var spacereco = reco.ToCharArray().Where(c => c == ' ').Count();
                text = "You said : " + reco + " Space Count: " + spacereco;
            }
            else if (activity.Text.ToUpper().Contains("VOWEL"))
            {
                var vowelreco = reco.ToUpper().ToCharArray().Where(c => c == 'A' || c=='E' ||
                                                                   c=='O' || c=='I' || c=='U').Count();
                text = "You said : " + reco + " Vowel Count: " + vowelreco;
            }
            else if (!String.IsNullOrEmpty(activity.Text))
            {
                var keywordreco = reco.ToUpper().Split(' ').Where(w => w == activity.Text.ToUpper()).Count();
                text = "You said : " + reco + " Keyword " +activity.Text + " found " + keywordreco + " times.";
            }
            else
            {
                text = "You said : " + reco;
            }
        }
        Activity reply = activity.CreateReply(text);
        await connector.Conversations.ReplyToActivityAsync(reply);
    }
    else
    {
        return HandleSystemMessage(activity);
    }
    var response = Request.CreateResponse(HttpStatusCode.OK);
    return response;
}

```

### Speaker recognition example
For this example, you will build a bot that leverages the Speaker Recognition API. The code allows you to use voice for authentication scenarios. The bot receives the audio file, compares it against the sender’s voiceprint, and then responds back with an accept or reject decision as well as a confidence score. This example will use the <a href="http://docs.botframework.com/connector/getstarted/#getting-started-in-net)" target="_blank">Bot Application .NET template</a> as a starting point. Note that the example requires the **Microsoft.ProjectOxford.SpeakerRecognition** package, which can be obtained via <a href="https://www.nuget.org/packages/Microsoft.ProjectOxford.Vision/" target="_blank">nuGet</a>.

Before you begin, you need to enroll your voice by saying one of the <a href="https://dev.projectoxford.ai/docs/services/563309b6778daf02acc0a508/operations/5652c0801984551c3859634d" target="_blank">preselected passphrases</a>. The Speaker Verification service requires at least 3 enrollments, so the bot will ask for three enrollment audio files, and send a confirmation when the enrollment is completed.

After you create your project with the Bot Application template, add the **Microsoft.ProjectOxford.SpeakerRecognition** package, and then open the **MessagesController.cs** file. 

Start by adding the following namespaces.

```cs
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using Microsoft.ProjectOxford.SpeakerRecognition;
using Microsoft.ProjectOxford.SpeakerRecognition.Contract;
using Microsoft.ProjectOxford.SpeakerRecognition.Contract.Verification;
using System.IO;
```

Next, write the function that implements the speaker verification logic. Note that the function requires a working Speaker Recognition API key, which can be found on the Cognitive Services <a href="https://www.microsoft.com/cognitive-services/en-US/subscriptions" target="_blank">subscription page</a>.

```cs
ISpeakerVerificationServiceClient client = new SpeakerVerificationServiceClient("<YOUR API KEY>");

Profile profile = null;
```

Finally, replace the code in the `Post` task with the one below. The code parses the voice attachment sent to the bot, calls the speaker verification service, and then responds back to the user with an accept or reject decision along with the confidence score.  

```cs
public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
{
    ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

    if (activity == null || activity.GetActivityType() != ActivityTypes.Message)
    {
        //add code to handle errors, or non-messaging activities
    }

    string replyString = "";

    // If this is a new user
    if (profile == null)
    {
        // Create the profile
        CreateProfileResponse createProfile = await client.CreateProfileAsync("en-us");
        profile = await client.GetProfileAsync(createProfile.ProfileId);
        replyString += "Welcome to the Speaker Recognition Bot!";
    }

    if(activity.Attachments.Any())
    {
        Stream file = File.Open(activity.Attachments.First().ContentUrl, FileMode.Open);

        switch (profile.EnrollmentStatus)
        {
            // If the profile is in the enrolling state
            case EnrollmentStatus.Enrolling:
                await client.EnrollAsync(file, profile.ProfileId);
                profile = await client.GetProfileAsync(profile.ProfileId);

                if (profile.EnrollmentStatus == EnrollmentStatus.Enrolling)
                    replyString += $"Great! let's repeat this for {profile.RemainingEnrollmentsCount} time(s)!";
                else
                    replyString += "Perfect! let's proceed to testing. Please send a test phrase!";                
                break;

            // If the profile is ready for testing
            case EnrollmentStatus.Enrolled:
                Verification result = await client.VerifyAsync(file, profile.ProfileId);
                replyString += $"This file is {result.Result} with a {result.Confidence} confidence. Send me another test phrase!";             
                break;
        }
    }
    else
    {
        VerificationPhrase[] phrasesResponse = await client.GetPhrasesAsync("en-us");
        replyString += " Please send me a recording for one of the phrases \"" + phrasesResponse[0].Phrase + "\"";
        foreach (VerificationPhrase phrase in phrasesResponse)
        {
            replyString += ", \"" + phrase.Phrase + "\"";
        }
    }
    // return our reply to the user
    Activity reply = activity.CreateReply(replyString);
    await connector.Conversations.ReplyToActivityAsync(reply);
    var response = Request.CreateResponse(HttpStatusCode.OK);
    return response;
}
```

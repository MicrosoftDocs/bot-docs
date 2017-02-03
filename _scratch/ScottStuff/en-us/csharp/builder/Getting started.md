---
layout: page
title: Getting Started
permalink: /en-us/csharp/builder/getting-started/
weight: 2050
parent1: Building your Bot Using Bot Builder for .NET
---

### Get the SDK

The SDK is available as a NuGet package. To get the SDK:

1. Right-click on your project and select "Manage NuGet Packages".  
2. In the "Browse" tab, type "Microsoft.Bot.Builder".  
3. Click the "Install" button and accept the changes.  

At this point, the builder is installed and you may begin using it.

To get up and running faster, you should install the Bot Application template, which is a fully functional echo bot that takes the user's text utterance as input and returns it as output.

1. Install prerequisite software

   - Visual Studio 2015 (latest update). You can download the free community version at <a href="https://www.visualstudio.com/" target="_blank">www.visualstudio.com</a>

   - Important: Update all VS extensions to their latest versions. Click **Tools**, **Extensions an Updates**, and then **Updates**  
  
2. Download and install the Bot Application template

   - Download the template from <a href="http://aka.ms/bf-bc-vstemplate" target="_blank">here</a>

   - Save the zip file to your Visual Studio 2015 templates directory, which is traditionally in "%USERPROFILE%\Documents\Visual Studio 2015\Templates\ProjectTemplates\Visual C#\"



Another option is to clone our GitHub repository using Git. This option provides you with numerous example code fragments and bots.

```
git clone https://github.com/Microsoft/BotBuilder.git
cd BotBuilder/CSharp
```



### Write the Hello, World! bot

This example uses the Bot Application template that you installed in the previous section.

1. Open Visual Studio

2. Create a new C# project using the new Bot Application template.

 ![Create a new C\# project using the new %Bot Application template.](/en-us/images/connector/connector-getstarted-create-project.png)  
  
The template is a fully functional Echo Bot that takes the user's text utterance as input and returns it as output. 

{% highlight csharp %}
[BotAuthentication]
public class MessagesController : ApiController
{
        <summary>
        POST: api/Messages
        Receive a message from a user and reply to it
        </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            if (activity.Type == ActivityTypes.Message)
            {
                // calculate something for us to return
                int length = (activity.Text ?? string.Empty).Length;

                // return our reply to the user
                Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }
}
{% endhighlight %}


The core functionality of the template is in the Post function within Controllers\MessagesController.cs. The code uses the **CreateReply** function to create a reply message that returns the user's text message. 

Note that the **BotAuthentication** class decoration validates your Bot Connector credentials over HTTPS using your MicrosoftAppID and MicrosoftAppPassword that you specify in the web.config file of your project. As an option to specifying the ID and password in the configuration file, you may specify them as parameters of the attribute.

{% highlight csharp %}
[BotAuthentication(MicrosoftAppId = "_MicrosoftappId_")]
public class MessagesController : ApiController
{
    . . .
}
{% endhighlight %}

<span style="color:red">Is the example parameters complete/accurate? What about the password?</span>


### Test your Hello, World! bot

To test the bot, use Bot Framework Emulator. The emulator is a desktop application that lets you test and debug your bot on localhost. For details about installing the emulator, see [Bot Framework Emulator](/en-us/tools/bot-framework-emulator/){:target="_blank"}.

After installing the emulator, see [If you use the Bot Application Template in Visual Studio...](/en-us/tools/bot-framework-emulator#usingdotnet) for details about running your bot in the emulator.

Start the emulator and say "hello" to your bot.

## Publish your bot

If the Hello, World! bot was useful and you wanted to share it with others, the following are the next steps. 

* [Deploy](/en-us/deploy/) your bot to the cloud
* [Register](/en-us/registration/) your bot with the framework
* [Configure](/en-us/channels/) your bot to run on one or more conversation channels
* [Publish](/en-us/directory/publishing/) your bot to Bot Directory

NOTE: After registering your bot with Bot Framework, you'll need to update the bot's `appId` and `appPassword` environment variables with the ID and password you were given during the registration process.

## Dive deeper 

To dive deeper and learn how to build great bots, see:

* [Core Concepts Guide](/en-us/node/builder/guides/core-concepts/)
* [Chat SDK Reference](/en-us/node/builder/chat-reference/modules/_botbuilder_d_.html)
* [Calling SDK Reference](/en-us/node/builder/calling-reference/modules/_botbuilder_d_.html)
* [Bot Builder on GitHub](https://github.com/Microsoft/BotBuilder)

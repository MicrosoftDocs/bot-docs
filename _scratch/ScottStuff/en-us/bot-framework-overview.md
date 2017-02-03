---
layout: page
title: Bot Framework
permalink: /en-us/
weight: 100
parent1: none
---


<span style="color:red">This needs to be the top-level topic.</span>

Microsoft Bot Framework is a comprehensive offering that you can use to build and deploy bots wherever your users are talking. A bot is simply a web service that interacts with users in a conversational format. Users start conversations with your bot from any conversation channel (chat service) that you've configured your bot to work on (for example, Text/SMS, Skype, Slack, Facebook Messenger, and other popular services). You can design conversations to be free-form, natural language interactions or more guided ones where you provide the user choices or actions. The conversation can utilize simple text strings or something more complex such as rich cards that contain text, images, and action buttons.
 
The following conversation shows a bot that schedules solon appointments. The bot understands the user's intent, presents appointment options using action buttons, displays the user's selection when they tap an appointment, and then sends a thumbnail card that contains the appointment's specifics. 
 
![solon bot example](/en-us/images/connector/salon_bot_example.png)


### What tools do I use to build my bot?

The Bot Connector exposes a [REST API](/en-us/connector/overview/) that you can use to send messages to and receive messages from a channel. Because it's a REST API you can write your bot using any tool that works with REST and JSON. However, if you use C# or Node.js, you should use the more powerful Bot Builder SDK that's built on top of the REST API. The SDK provides feature such as dialogs and built-in prompts that make interacting with users much simpler. The Bot Builder SDK is provided as open source on GitHub (see [BotBuilder](https://github.com/Microsoft/BotBuilder)). For details about using the SDK in C#, see [Bot Builder SDK for .NET](/en-us/csharp/builder/sdkreference/). For details about using the SDK in Node.js, see [Bot Builder SDK for Node.js](/en-us/node/builder/overview/).

### How do I make my bot smarter?

To give your bot more human-like senses, you can incorporate LUIS for natural language understanding, Cortana for voice, and the Bing APIs for search. For more information about adding intelligence to your bot, see [Bot Intelligence](/en-us/bot-intelligence/getting-started/).

### How do I test my bot?

The framework provides an emulator that you can use to test your bot, independent of a channel. For information about using the emulator, see [Emulator](/en-us/tools/bot-framework-emulator/).

### I finished writing and testing my bot, now what?

When you finish writing your bot, you need to register it, connect it to channels, and publish it. [Registering your bot](https://dev.botframework.com/bots/new){:target="_blank"} describes it to the framework, and it's how you get the bot's app ID and password that's used for authentication. Bots that you register are located at [My bots]( https://dev.botframework.com/bots){:target="_blank"} in the portal. 

After registering your bot, you need to configure it to work on channels that your users use. The configuration process is unique per channel, and some channels are preconfigured for you (for example, Skype and Web Chat). For information about configuring channels, see [Configuring Channels](/en-us/csharp/builder/sdkreference/gettingstarted.html). For information about which channels support which Bot Connector features, see [Channel Inspector](/en-us/channel-inspector/). The framework also provides the [Direct Line](/en-us/restapi/directline/) REST API, which you can use to host your bot in an app or website.

For most channels, you can share your bot with users as soon as you configure the channel. If you configured your bot to work with Skype, you must publish your bot to the Bot Directory and Skype apps (see [Publishing your bot](/en-us/directory/publishing/)) before users can start using it. Although Skype is the only channel that requires you to publish your bot to the directory, you are encouraged to always publish your bot because it makes it more discoverable. Publishing the bot submits it for review. For information about the review process, see [Bot review guidelines](/en-us/directory/review-guidelines/). If your bot passes review, it’s added to the [Bot Directory](https://bots.botframework.com/){:target="_blank"}. The directory is a public directory of all bots that were registered and published with Microsoft Bot Framework. Users can select your bot in the directory and add it to one or more of the configured channels that they use.

### More to read

|**Section**|**Description**
|[General FAQ](/en-us/faq/)<br/><br/>[Technical FAQ](/en-us/technical-faq/)|Contains frequently asked questions about the framework.
|[Support](/en-us/support/)|Provides a list of resources where you can get help resolving issues that you have with using the framework.
|[Downloads](/en-us/downloads/)|Provides the locations where you can download the .NET and Node.js SDKs from.
|[Samples](https://github.com/Microsoft/BotBuilder-Samples){:target="_blank"}|Provides the locations where you can get the C# and Node.js code samples from.
|[Emulator](https://github.com/Microsoft/BotFramework-Emulator){:target="_blank"}|Provides details about getting and using the framework’s emulator, which you can use to test your bot.
|[User Experience Guidelines](/en-us/directory/best-practices/)|Provides guidelines and best practices for crafting a bot that provides the best experience for the user.
|[Bot Intelligence](/en-us/bot-intelligence/getting-started/)|Identifies the APIs that you can incorporate into your bot to provide a better user experience. Adding intelligence to your bot can make it seem more human-like and provide a better user experience. For example, the Vision APIs can identify emotions in peoples’ faces or extract information about objects and people in images; the Speech APIs can covert text-to-speech or speech-to-text; the Language APIs can process natural language and detect sentiment; and you can use the Search APIs to find information that may be of interest to the user.
|[Bot Builder for Node.js](/en-us/node/builder/overview/)|Provides details for building bots using the Node.js SDK.
|[Bot Builder for .NET](/en-us/csharp/builder/sdkreference/)|Provides details for building bots using the .NET SDK.

{%comment%}
Add after User Experience Guidelines
|[Channel and Feature Matrix](/en-us/matrix/)|Provides the list of supported framework features by channel.
{%endcomment%}



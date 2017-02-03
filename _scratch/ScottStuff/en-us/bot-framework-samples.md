---
layout: page
title: Bot Framework Samples
permalink: /en-us/samples/
weight: 4055
parent1: none
---

The Bot Framework provides many C# and Node.js samples on GitHub that illustrate features of each Bot Builder SDK. For a complete list of C# samples, see [C# Samples](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples), and for Node.js, see [Node Samples](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples).

For a summary of some of the available samples, see:

* [C# samples summary](#csharpsamples)
* [Node.js samples summary](#nodesamples)

<a id="nodesamples" />

### Summary of available Node.js samples

To use the samples, clone the framework's GitHub repository using Git.

    git clone https://github.com/Microsoft/BotBuilder.git
    cd BotBuilder/Node
    npm install

The node examples are found under the "Node/examples" directory. 


The following samples show a simple "Hello World" example of each bot type that the Bot Builder for Node SDK supports. 

|**Sample**|**Description**                                   
| ---------|---------------
|[hello-ConsoleConnector](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/hello-ConsoleConnector)|A simple "Hello World" example that shows using the [ConsoleConnector](/en-us/node/builder/chat/UniversalBot/#consoleconnector) class.      
|[hello-ChatConnector](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/hello-ChatConnector)|A simple "Hello World" example that shows using the [ChatConnector](/en-us/node/builder/chat/UniversalBot/#chatconnector) class.  


The following samples show the basic techniques needed to build a great bot.  

|**Sample**|**Description**                                   
| ---------|---------------
|[basics-waterfall](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/basics-waterfall)|Shows how to use a [waterfall](/en-us/node/builder/chat/dialogs/#waterfall) to prompt the user with a series of questions.
|[basics-loops](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/basics-loops)|Shows how to use session.replaceDialog() to create loops. 
|[basics-menus](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/basics-menus)|Shows how to create a simple menu system for a bot. 
|[basics-naturalLanguage](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/basics-naturalLanguage)|Shows how to use a [LuisDialog](/en-us/node/builder/chat/IntentDialog/#intent-recognizers) to add natural language support to a bot.
|[basics-multiTurn](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/basics-multiTurn)|Shows how to implement simple multi-turns using waterfalls. Multi-turns is a scenario where the user asks a question about something and then wants to ask a series of follow-up questions.
|[basics-firstRun](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/basics-firstRun)|Shows how to create a First Run  experience using a piece of middleware. First run is a scenario where a dialog is run only the first time that the user uses your bot.
|[basics-logging](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/basics-logging)|Shows how to add logging/filtering of incoming messages using a piece of middleware.
|[basics-localization](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/basics-localization)|Shows how to implement [multiple language support](/en-us/node/builder/chat/localization/) for a bot.
|[basics-customPrompt](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/basics-customPrompt)|Shows how to create a custom prompt of arbitrary complexity. 
|[basics-libraries](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/basics-libraries)|Shows how to package a set of dialogs as a library that can be shared across multiple bots. 


The following samples showcase what's possible on specific channels. They're great sources of code fragments if you're looking to have your bot lightup specific features for a channel.

|**Sample**|**Description**                                   
| ---------|---------------
|[demo-skype](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/demo-skype)|Shows what's possible on Skype.
|[demo-skype-calling](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/demo-skype-calling)|Shows how to build a calling bot for Skype.
|[demo-facebook](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/demo-facebook)|Shows what's possible on Facebook.


<a id="csharpsamples" />

### Summary of available C# samples

|**Sample**|**Description**                                   
| ---------|---------------
|[SimpleEchoBot](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/SimpleEchoBot/)|Shows a simple bot that echos what the user says.
|[EchoBot](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/EchoBot/)|Adds user state to the SimpleEchoBot sample.
|[SimpleSandwichBot](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/SimpleSandwichBot/)|Shows how to use [FormFlow](/en-us/csharp/builder/formflow/) to create guided conversation. 
|[AnnotatedSandwichBot](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/AnnotatedSandwichBot/)|Adds attributes, messages, confirmation and business logic to the SimpleSandwichBot sample.
[SimpleAlarmBot](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/SimpleAlarmBot/)|Shows how to [integrate natural language](/en-us/csharp/builder/sdkreference/dialogs.html#alarmBot) into a bot.
|[AlarmBot](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/AlarmBot/)|Adds logic to the SimpleAlarmBot sample to proactively send alarms.
|[PizzaBot](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/PizzaBot/)|Shows how to integrate natural language with FormFlow.
|[GraphBot](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/GraphBot/Microsoft.Bot.Sample.GraphBot)|Shows how to integrate [Microsoft Graph Api](https://graph.microsoft.io) with dialogs.
|[SimpleFacebookAuthBot](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/SimpleFacebookAuthBot/)|Shows how to use OAuth authentication with Facebook's Graph API.
|[SimpleIVRBot](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/SimpleIVRBot/)|Shows an interactive voice response (IVR) bot using the Skype calling API.
|[Stock_Bot](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/Stock_Bot/)|Shows calling a web service from a bot.
|[SearchPoweredBots](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/SearchPoweredBots)|Shows an integration of [Azure Search](https://azure.microsoft.com/en-us/services/search/) with dialogs.


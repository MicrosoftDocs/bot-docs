---
title: Bot Framework Connector Service | Microsoft Docs
description: Learn about the Bot Framework Connector Service.
keywords: Bot Framework, connector, core concept, bot, web service
author: DeniseMak
manager: rstand
ms.topic: key-concepts-article
ms.prod: bot-framework

ms.date: 02/14/2017
ms.reviewer:
ROBOTS: Index, Follow
---

# The Bot Framework Connector Service
> [!NOTE]
> This content will be updated.

The Bot Framework Connector Service is a web service which allows your bot to communicate with multiple channels such as Skype, Facebook, Slack and SMS. If you write a conversational Bot or agent and expose a Microsoft Bot Framework-compatible API on the Internet, the Bot Framework Connector service will forward messages from your Bot to a user, and will send user messages back to your Bot.
The service is made up of a REST API, a common JSON schema for communicating information between the user and the bot, 
and client libraries in .NET and Node.JS which encapsulate the REST API to make programming easier.

To use the Microsoft Bot Framework Connector Service, you must have:
* A Microsoft Account (Hotmail, Live, Outlook.com) to log into the Bot Framework developer portal, which you will use to register your Bot.
* An Azure-accessible REST endpoint exposing a callback for the Connector service.
* Developer accounts on one or more communication services(such as Skype) where your Bot will communicate.

See [Publish a bot](~/publish-bot-overview.md) for the registration and deployment steps required to communicate with your bot using the connector.
See Channels for an overview of the available conversation channels.

## Core principles
### Message format
The Connector service sends messages as JSON to your bot in POST requests. The .NET and Node.js Bot Builder SDKs encapsulate the messages. 
For more information on message format, see Sending and Receiving Messages.

### Bot State API
The Connector Service includes a service for storing bot state. This lets your bot track things like the last question the bot asked the user. For more information, see Saving User State Data.

### Authentication 
The Bot Connector service uses OAuth 2.0 client credentials for bot authentication from the Microsoft Account (MSA) server. 

If you use the Bot Builder SDK for C# or the Bot Builder SDK for Node.js, the SDK will handle authentication for you; all you need to do is 
configure your project with your bot's Microsoft App ID and Password and the SDK does the rest. 

If you use the REST API, After getting the access token, you include the token in the Authorization header of all requests that your bot sends to the Bot Connector.
For details, see Getting an Access Token and Calling the Bot Connector Service.

## Implementation basics

### Initializing your bot to use the Connector Service
The following Node.js code demonstrates how the ChatConnector class is used to connect to the Bot Framework Connector Service and listen for incoming POST requests.

    var restify = require('restify');
    var builder = require('botbuilder');

    // Setup Restify Server
    var server = restify.createServer();
    server.listen(process.env.port || process.env.PORT || 3978, function () {
        console.log('%s listening to %s', server.name, server.url); 
    });
  
    // Create chat bot
    var connector = new builder.ChatConnector({
        appId: process.env.MICROSOFT_APP_ID,
        appPassword: process.env.MICROSOFT_APP_PASSWORD
    });

    // The UniversalBot class forms the brains of your bot. It’s responsible for managing all of the conversations your bot has with a user.
    var bot = new builder.UniversalBot(connector);
    server.post('/api/messages', connector.listen());


In the following C# code, the Post function connects to the Connector Service and takes the message text for the user, then creates a reply message using the CreateReplyMessage function. 
The BotAuthentication decoration on the method is used to validate your %Bot %Connector credentials over HTTPS.

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


## Next Steps
> [!NOTE]
> TODO: Add remaining links. 


[Get Started](~/nodejs/getstarted.md)





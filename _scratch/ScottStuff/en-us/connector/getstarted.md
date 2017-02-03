---
layout: page
title: Quick Start
permalink: /en-us/connector/getstarted/
weight: 3010
parent1: Building your Bot Using the Bot Connector REST API
---

The following sections layout the steps to building and registering a bot, and configuring it to work on one or more channels.

- [Getting an authentication token](#token)
- [Replying to a user's message](#message)
- [Publishing your bot](#publish)
- [Registering your bot](#register)
- [Configuring your bot to work on a channel](#configure)


<a id="token" />

### Getting an authentication token

To use the Bot Framework, you must get an authenication token and pass it in the Authentication header with each API request. For information about getting a token, see [Authentication](../authentication).

Note that to get a token, you must have an app ID and password, and to get the app ID and password, you must [register your bot](#register).

The following shows the call to get an authentication token.

```cmd
POST https://login.microsoftonline.com/d6d49420-f39b-4df7-a1dc-d59a935871db/oauth2/v2.0/token HTTP/1.1
Host: login.microsoftonline.com
Content-Type: application/x-www-form-urlencoded
 
grant_type=client_credentials&client_id=<YOUR MICROSOFT APP ID>&client_secret=<YOUR MICROSOFT APP PASSWORD>&scope=https%3A%2F%2Fapi.botframework.com%2F.default
```

The following shows the JSON response to the above request.

```cmd
HTTP/1.1 200 OK
(other headers) 

{
    "token_type":"Bearer",
    "expires_in":3600,
    "ext_expires_in":3600,
    "access_token":"eyJhbGciOiJIUzI1Ni..."
}
```

<a id="message" />

### Replying to a user's message

A conversation is a series of messages exchanged between a user and your bot. When the user sends a message, the connector POSTs a request to your bot's endpoint that you specified when you registered your bot. The body of the request is an [Activity](../reference/#activity) object. Access the **type** property to determine the type of message that the user sent. 

If the user sent a message of type `message`, create a new **Activity** object and set its properties as follows:

1. Set the **conversation** property to the contents of the **conversation** property in the user's message
2. Set the **from** property to the contents of the **recipient** property in the user's message
3. Set the **recipient** property to the contents of the **from** property in the user's message.
4. Set the **text** and **attachments** properties as appropriate.

The following shows a message from a user.

```cmd
{
    "type": "message",
    "id": "bf3cc9a2f5de...",
    "timestamp": "2016-10-19T20:17:52.2891902Z",
    "serviceUrl": "channel's service URL",
    "channelId": "channel's name/id",
    "from": {
        "id": "1234abcd",
        "name": "user's name"
    },
    "conversation": {
        "id": "abcd1234",
        "name": "conversation's name"
   },
   "recipient": {
        "id": "12345678",
        "name": "bot's name"
    },
    "text": "Haircut on Saturday"
}
```

The following shows a reply to the user's message that prompts them to select an available appointment.

```cmd
POST https://api.botframework.com/v3/conversations/abcd1234/activities/bf3cc9a2f5de... HTTP/1.1
Authorization: Bearer eyJhbGciOiJIUzI1Ni...
Content-Type: application/json

{
    "type": "message",
    "from": {
        "id": "12345678",
        "name": "bot's name"
    },
    "conversation": {
        "id": "abcd1234",
        "name": "conversation's name"
   },
   "recipient": {
        "id": "1234abcd",
        "name": "user's name"
    },
    "text": "I have these times available:",
    "replyToId": "bf3cc9a2f5de..."
}
```

The following shows the second reply that contains the available times.

```cmd
POST https://api.botframework.com/v3/conversations/abcd1234/activities/bf3cc9a2f5de... HTTP/1.1
Authorization: Bearer eyJhbGciOiJIUzI1Ni...
Content-Type: application/json

{
    "type": "message",
    "from": {
        "id": "12345678",
        "name": "bot's name"
    },
    "conversation": {
        "id": "abcd1234",
        "name": "conversation's name"
   },
   "recipient": {
        "id": "1234abcd",
        "name": "user's name"
    },
    "attachmentLayout": "list",
    "attachments": [
      {
        "contentType": "application/vnd.microsoft.card.thumbnail",
        "content": {
          "buttons": [
            {
              "type": "imBack",
              "title": "10:30",
              "value": "10:30"
            },
            {
              "type": "imBack",
              "title": "11:30",
              "value": "11:30"
            },
            {
              "type": "openUrl",
              "title": "See more",
              "value": "http://www.contososalon.com/scheduling"
            }
          ]
        }
      }
    ],
    "replyToId": "bf3cc9a2f5de..."
}
```   

For information about adding attachments to messages, see [Adding Attachments to a Message](../attachments).

For information about adding channel-specific data to messages, see [Adding Channel Data to a Message](../channeldata).

For information about storing user data, see [Saving User Data](../userdata).

<a id="publish" />

### Publishing your bot

You can host your bot on any reachable service such as Azure. For information about hosting your bot on Azure, see [Publishing your Bot to Microsoft Azure](/en-us/csharp/builder/sdkreference/gettingstarted.html).


<a id="register" />

### Registering your bot

To use Bot Framework, you need to register your bot. To register your bot, go to [Register a bot](https://dev.botframework.com/bots/new) and provide your bot's name, handle, description, and the endpoint that it uses to receive messages. Registration also gathers information about you, the publisher. When you register your bot you also request an app ID and password, which you use to get an authentication token. [Read more](/en-us/registration/)


<a id="configure" />

### Configuring your bot to work on a channel

Bot Framework supports multiple popular channels such as SMS, Skype, Slack, and Facebook. To configure your bot to work on these channels, go to [My bots](https://dev.botframework.com/bots) and sign in. Select the bot that you want to configure, and then select the channel by clicking **Add**. Follow the configuration steps. [Read more](/en-us/channels/) 


### Quick starts for .NET and Node.js

To get started using the .NET SDK or the Node SDK, see [Getting Started with the .NET SDK](/en-us/csharp/builder/sdkreference/gettingstarted.html) and [Getting Started with the Node SDK](/en-us/node/builder/overview/#navtitle). 

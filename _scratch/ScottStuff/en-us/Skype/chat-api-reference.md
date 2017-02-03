---
layout: page
title: Skype Chat API Reference
permalink: /en-us/skype/chat/
weight: 5120
parent1: Channels
parent2: Skype bots
---

* TOC
{:toc}

## Resource model

The Skype Bot platform contains objects of different types. A unique identifier is allocated by the platform and represents every object. For example, every conversation object is assigned a string identifier named “conversationId”.

![Resource Model](/en-us/images/skype/skype-chat-api-resource-model.png)

* User – a Skype user interacting with the bot
* Conversation - a conversation associated with the user
* Activity object within each conversation (text, notification of conversation changes or attachments)
* Attachment – identifiable resources for that conversation (images, videos)

###	Activity

This object represents activity associated with the conversation, such as:

* Text messages
* Notifications of updates to conversation properties e.g. new conversation or new attachment
* Shared images and videos

Supported properties:

|    Field    |    Meaning                                                      |    Type      |
|-------------|-----------------------------------------------------------------|--------------|
|    id       |    A resource   identifier for the activity                     |    string    |
|    from     |    The ID instantiated from the user/conversation   activity    |    string    |
|    to       |    The targeted   activity of the user/conversation ID.         |    string    |


### Conversation

This object represents a group of users who share the same activity stream.

Supported properties:

|    Field    |    Meaning                                         |    Type      |
|-------------|----------------------------------------------------|--------------|
|    id       |    A resource identifier for the conversation    |    string    |

### User

This object represents the user.

Supported user properties:

|    Field    |    Meaning                                       |    Type      |
|-------------|--------------------------------------------------|--------------|
|    id       |    The unique user ID    |    string    |

<div class="docs-text-note">In the V3 version of the API a user is represented by a unique user ID per bot (and not for example the Skype ID).</div>

## Authentication

### Sending messages
{:.no_toc}

All calls to the Skype REST API should supply a Microsoft Online OAuth2 token. This token can be obtained by issuing a POST call to login.microsoftonline.com with parameters passed in the request body (x-www-form-urencoded).

* The Grant Type should be “client_credentials”
* The **scope** should be https://graph.microsoft.com/.default
* The **client** is the MSA App ID, and client_secret is corresponding secret.

    POST /common/oauth2/v2.0/token HTTP/1.1
    Host: login.microsoftonline.com
    Content-Type: application/x-www-form-urlencoded
    
    client_id=<MSAAppId>f&client_secret=<secret>&grant_type=client_credentials&scope=https%3A%2F%2Fgraph.microsoft.com%2F.default

The obtained token should be passed in the Authorization header with a Bearer auth scheme.

    Authorization: Bearer <oauth2-token>

For more information on obtaining an OAuth2 token see the [OAuth 2.0 Authorization Code Flow](https://azure.microsoft.com/en-us/documentation/articles/active-directory-v2-protocols-oauth-code/).

###	Receiving messages
{:.no_toc}

With all calls from Skype to a bot Skype will supply a JWT token. We strongly recommend that your bot verifies the token to make sure it is Skype issuing this call.

1. Your bot should periodically monitor the OpenId metadata endpoint https://api.aps.skype.com/v1/keys and retrieve a list of valid signing certificates with assigned key ids (kid). The recommended poll interval is one day.

2. Once your bot  receives the signed JWT, it should parse the first part of the JWT which contains information about signature algorithm (X509SHA2) and “kid” identifier corresponding to X509 certificate from OpenId metadata.

3. Your bot should then use the correct public key for the “kid” cert to verify a signature over JWT content and compare the resultant byte array against the signature in the JWT token.

4. Once the signature is verified your bot will need to verify other properties of token: the **audience** should match the bot MSA App Id, the **token** should not be expired and the **issuer** should match issuer published in OpenId metadata.


## REST API

###	Domain names
{:.no_toc}
* **apis.skype.com or api.skype.net** A production domain that may contain all recent changes.
* **df-apis.skype.com or df-api.skype.net** A production pre-release domain containing the most recent changes.

###	Version
{:.no_toc}
Activities APIs have ‘v3’ version identifier.

###	Error Model
{:.no_toc}
On any error, Skype will respond with a body containing the following error model:

{% highlight json %}
    {
        "error": 
            {
                "code":"string",
                "message":"string" 
            }
    }
{% endhighlight %}

###	HTTP Redirects
{:.no_toc}

The Skype Bot platform uses HTTP redirection where appropriate. Redirect responses will have a Location header field, which contains the URL of the resource where the client should repeat the requests.

* Bots should assume any request might result in a redirection. 
* Bots should follow HTTP redirects.

Status code| Description
|-------------|--------------------------------------------------
**302**| The request should be repeated verbatim to the URL specified in the Location header field but clients should continue to use the original URL for future requests.
**301 or 307**| The request should be repeated verbatim to the URL specified in the Location header field preserving the HTTP method as originally sent.

You may use other redirection status codes (in accordance with the HTTP 1.1 spec).

###	ContextId header
{:.no_toc}
The ContextId header provided by server in all messages response is a generated unique string identifier of each client request, so it can differentiate user requests in logs and can be used for troubleshooting. E.g.

    HTTP/1.1 200 OK
    ContextId: tcid=8695243588097561400,server=CO2SCH020010627
    Date: Mon, 21 Mar 2016 15:26:03 GMT

## Sending messages 
{:.no_toc}

A call to POST /v3/conversations/conversationId/activities/ sends activities to the conversation stream. 

Conversation_id is either the unique user ID or the ID of a group conversation, depending on whether you are sending the message to the addressee or conversation thread id for group conversation.

The body (json) can be used with the following fields:

|     Field           |     Meaning                                                                                  |     Type                          |
|---------------------|----------------------------------------------------------------------------------------------|-----------------------------------|
|    content          |    The text of the message.   It can contain Skype rich text objects (such as emoticons).    |    string                         |
|    type             |    Defines type of notification and name   of feature                                        |    string                         |
|    attachments[]    |    This is placeholder for structured objects   attached to this message                     |    Array of Attachment Objects    |

The following HTTP status codes can be returned:

|    Code                |    Description                                                                                                                                                        |     Type                          |
|------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------------------------|
|    201 Created         |    The request has been fulfilled and resulted in a new   resource.                                                                                                   |    string                         |
|    400 Bad Request     |    The request can't be fulfilled because of bad syntax.                                                                                                              |    string                         |
|    401 Unauthorized    |    The authentication information is not provided or is   invalid.                                                                                                    |    Array of Attachment Objects    |
|    403 Forbidden       |    The provided credentials do not grant the client permission to access   the resource.  For example: a recognized   user attempted to access restricted content.    |                                   |

**Example: Post to user**

    POST /v3/conversations/29:f2ca6a4a-93bd-434a-9ca5-5f81f8f9b455/activities HTTP/1.1
    Host: apis.skype.com
    Authorization: Bearer <redacted oauth2 token>

    {
    "type": "message/text",
    "text": "Hi! (wave)"
    }

    HTTP/1.1 201 Created
    Cache-Control: no-store, must-revalidate, no-cache
    Pragma: no-cache
    Content-Length: 0
    ContextId: tcid=6292595202568151987,server=CO2SCH020010627
    Date: Mon, 21 Mar 2016 15:44:12 GMT

**Example: Post to group conversation**

    POST /v3/conversations/19:031cb0f20f414db8b5d18ead0af68911@thread.skype/activities HTTP/1.1
    Host: api.skype.net
    Authorization: Bearer <redacted oauth2 token>

    {
    "type": "message/text",
    "text": "Hi! (wave)"
    }

    HTTP/1.1 201 Created
    Cache-Control: no-store, must-revalidate, no-cache
    Pragma: no-cache
    Content-Length: 0
    ContextId: tcid=6411156774585686840,server=CO2SCH020010627
    Date: Mon, 21 Mar 2016 15:43:05 GMT


**Example: Post image to group conversation**

    POST /v3/conversations/19:031cb0f20f414db8b5d18ead0af68911@thread.skype/activities HTTP/1.1
    Host: apis.skype.com
    Authorization: Bearer <redacted oauth2 token>

    {
    "type": "message/image",
    "attachments": [
        {
            "contentUrl": "<base64 encoded image>",
            "thumbnailUrl": "<base64 encoded thumbnail>",          // optional
            "filename": "bear.jpg"                                 // optional
        }
    ]
    }

    HTTP/1.1 201 Created
    Cache-Control: no-store, must-revalidate, no-cache
    Pragma: no-cache
    Content-Length: 0
    ContextId: tcid=6411156774585686840,server=CO2SCH020010627
    Date: Mon, 21 Mar 2016 15:43:05 GMT

**Example: Post card to group conversation**

    POST /v3/conversations/19:031cb0f20f414db8b5d18ead0af68911@thread.skype/activities HTTP/1.1
    Host: apis.skype.com
    Authorization: Bearer <redacted oauth2 token>

    {
    "type":"message/card.carousel",
    "summary":"Several hotel offers in Paris",
    "text":"Here you have hotels you're looking for",
    "attachments":[
        {
        "contentType":"application/vnd.microsoft.card.hero",
        "content":{
            
                "title":"Hotel Radisson Blu Hotel at Disneyland (r) Paris.",
                "subtitle":"<a href=\"https://disney.radisson.com\">$71 Today up to 27% off</a><br>Booked in the last 2 hours",
                "text":"Disneyland paris. 40 Aliee De la Mare dian Houleuse, Magny-le-Hongre, Seine-Marne.",
                "images":[
                {
                    "image":"https://foo.com/path/image.jpg",
                    "alt":"hello thumb"
                }
                ]
            ,
            "buttons":[
            {
                "type":"imBack",
                "title":"Reserve",
                "value":"Reserve <context offer=\"...\"/>"
            },
            {
                "type":"opneUrl",
                "title":"Website",
                "value":"https://disney.radisson.com"
            },
            {
                "type":"call",
                "title":"Skype support",
                "value":"skype:radisson.disney"
            }
            ]
        }
        }
    ]
    }

    HTTP/1.1 201 Created
    Cache-Control: no-store, must-revalidate, no-cache
    Pragma: no-cache
    Content-Length: 0
    ContextId: tcid=6411156774585686840,server=CO2SCH020010627
    Date: Mon, 21 Mar 2016 15:43:05 GMT

## Receiving messages to your webhook

Your bot will receive messages to the webhook configured for your bot in the Microsoft Bot Framework.

Supported notifications are:

* New message notification. The message sent to the bot (1:1 or via group conversation).
* New attachment notification. The attachment sent to bot (1:1 or via group conversation).
* Conversation event notification

Notifications are sent in case:

* Members are added or removed from the conversation.
* The conversation's topic name changed.
* A contact was added to or removed from the bot's contact list.

POST is issued (with json-formatted body) for all notifications.

### Response
{:.no_toc}

The expected response is a “201 Created” without body content. 
Redirects will be ignored. Operations will time out after 5 seconds.

### Payload format
{:.no_toc}

All Webhooks are called with JSON-formatted bodies (array or JSON objects). Every JSON object indicates some update and has the following set of common fields.

#### Message
{:.no_toc}

|     Property     |     Type     |     Description     |
|----------------------|-------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
|    id    |    String    |    Incoming   Message ID    |
|    from    |    Address Object    |    Sender address    |
|    to    |    Address   Object    |    Recipient   address    |
|    timestamp    |    String    |    Timestamp of message   given by chat service    |
|    recipient    |    Address   Object    |    Will   be added by Bot Framework to identify conversation    |
|    channelId    |    String    |    Skype conversation   platform should specify 'skype' in this property    |
|    serviceUrl    |    String    |    Skype   conversation platform should specify URL to: post messages back, comment,   annotate, delete    |
|    type    |    String    |    Defines type   of notification and name of feature.   In case of incoming messages following template will be used: "message[/*]"    |
|    summary    |    String    |    Text   to be displayed by as fall-back and as short description of the message   content in e.g. list of recent conversations    |
|    text    |    Rich Text    |    Message text     |
|    attachments:[]    |    Array   of Attachment Objects    |    This   is placeholder for structured objects attached to this message    |
|    entities:[]    |    Array of Objects    |    This property is   intended to keep structured data objects intended for Client application   e.g.: Contacts, Reservation, Booking, Tickets. Structure of these object   objects should be known to Client application.   Client application   may provide extra actions for known data objects (e.g. add to contacts, plan   route, save to Calendar, …)    |

**Example message from user to bot**

{% highlight json %}

    {
    "channelId":"skype",
    "serviceUrl":"https://apis.skype.com",
    "conversation": {
    "id":"29:f2ca6a4a-93bd-434a-9ca5-5f81f8f9b455",
    "name":"Display Name"
    },
        "from": {
            "id":"29:f2ca6a4a-93bd-434a-9ca5-5f81f8f9b455",
            "name":"Display Name"
    },
        "recipient": {
            "id":"28:ad35d471-ae65-4626-af00-c01ffbfc581f",
            "name":"Trivia Master"
    },
        "id":"1234567890",
        "type":"message/text",
        "text":"Hello World!"
    }
    
{% endhighlight %}

**Example message from user to bot in a group conversation**

{% highlight json %}
    {
        "channelId":"skype",
        "serviceUrl":"https://apis.skype.com",
        "conversation": {
            "id":"19:031cb0f20f414db8b5d18ead0af68911@thread.skype",
            "isGroup":true
    },
        "from": {
                "id":"29:f2ca6a4a-93bd-434a-9ca5-5f81f8f9b455",
                "name":"Display Name"
        },
        "recipient": {
            "id":"28:ad35d471-ae65-4626-af00-c01ffbfc581f",
            "name":"Trivia Master"
        },
        "id":"1234567890",
        "type":"message/text",
        "text":"Hello World!"
    }
{% endhighlight %}

**Example message with an image attachment**


{% highlight json %}
    {
        "channelId":"skype",
        "serviceUrl":"https://apis.skype.com",
        "conversation": {
            "id":"8:alice",
            "name":"Alice"
    },
        "from": {
            "id":"8:alice",
            "name":"Alice Smith"
        },
        "recipient": {
            "id":"28:agentId",
            "name":"Agent Murphy"
        },
        "id":"1234567890",
        "type":"message/image",
        "attachments": [
            {
                "contentType":"application/vnd.skype.image",
                "contentUrl":"https://df-apis.skype.com/v2/attachments/0-weu-d2-f39063e875fbee9ac0ec28b70a706245/views/original",
                "thumbnailUrl":"https://df-apis.skype.com/v2/attachments/0-weu-d2-f39063e875fbee9ac0ec28b70a706245/views/thumbnail",
                "filename":"bear.jpg"
            }
        ]
    }
{% endhighlight %}

#### Contact Relation Update

This notification is delivered when the bot is added to or removed from the user’s contact list.

|     Property     |     Type     |     Description     |
|------------------|------------------------|------------------------------------------------------------------------------------------------------------------------------------------------|
|    from    |    Address   Object    |    Sender   address    |
|    to    |    Address Object    |    Recipient address    |
|    recipient    |    Address   Object    |    Will   be added by Bot Framework to identify conversation    |
|    channelId    |    String    |    Skype conversation platform   should specify 'skype' in this property    |
|    serviceUrl    |    String    |    Skype   conversation platform should specify URL to: post messages back, comment,   annotate, delete    |
|    type    |    String    |    Defines type   of notification and name of feature.   In case of incoming notifications following template will be used: "activity</*>".    |
|    action    |    String    |    May   constants ‘add’ or  ‘remove’   which indicates whether the Bot was added or removed to/from user’s contact   list.    |

**Example**

{% highlight json %}
    {
        "channelId":"skype",
        "serviceUrl":"https://apis.skype.com",
        "conversation": {
            "id":"29:f2ca6a4a-93bd-434a-9ca5-5f81f8f9b455",
            "name":"Display Name"
        },
        "from": {
            "id":"29:f2ca6a4a-93bd-434a-9ca5-5f81f8f9b455",
            "name":"Display Name"
        },
        "recipient": {
            "id":"28:ad35d471-ae65-4626-af00-c01ffbfc581f",
            "name":"Trivia Master"
        },
        "type":"activity/contactRelationUpdate",
        "action":"add"
    }
{% endhighlight %}

#### Conversation Update

This notification is delivered when conversation properties change:

|     Property     |     Type     |     Description     |
|------------------------|--------------------------|------------------------------------------------------------------------------------------------------------------------------------------------|
|    from    |    Address   Object    |    Sender   address    |
|    to    |    Address Object    |    Recipient address    |
|    timestamp    |    String    |    Timestamp   of message given by chat service    |
|    recipient    |    Address Object    |    Will be added by Bot   Framework to identify conversation    |
|    channelId    |    String    |    Skype   conversation platform should specify 'skype' in this property    |
|    serviceUrl    |    String    |    Skype conversation   platform should specify URL to: post messages back, comment, annotate, delete    |
|    type    |    String    |    Defines   type of notification and name of feature.   In case of incoming notifications following template will be used: "activity</*>".    |
|    topicName    |    String    |    The conversation’s   new topic name    |
|    historyDisclosed    |    Boolean    |    true if history was disclosed, and false otherwise    |
|    membersAdded    |    Array of Strings    |    List of user id’s   which were added to conversation    |
|    membersRemoved    |    Array   of Strings    |    List   of user id’s which were removed from conversation    |

**Example**

{% highlight json %}
    {
        "channelId":"skype",
        "serviceUrl":"https://apis.skype.com",
        "conversation": {
            "id":"19:031cb0f20f414db8b5d18ead0af68911@thread.skype",
            "isGroup":true
    },
        "from": {
            "id":"29:f2ca6a4a-93bd-434a-9ca5-5f81f8f9b455",
            "name":"Display Name"
        },
        "recipient": {
            "id":"28:ad35d471-ae65-4626-af00-c01ffbfc581f",
            "name":"Trivia Master"
        },
        "type":"activity/conversationUpdate",
        "membersAdded":[ "8:bill", "8:tom" ],
        "membersRemoved":[ "8:david", "8:erin" ],
        "topicName": "<new topic name>"
    }
{% endhighlight %}

#### Address object

|     Property     |     Type     |     Description     |
|------------------|---------------|------------------------------------------------------------------------|
|    id    |    String    |    String   with id using following template <schema>:<id string>    |
|    name    |    String    |    Friendly name    |
|    isGroup    |    Boolean    |    (optional)   informs bot if this is group conversation ID or not    |

#### Attachment

|     Property     |     Type     |     Description     |
|-------------------|--------------|----------------------------------------------------------------|
|    contentType    |    String    |    MIME   type string which describes type of attachment    |
|    content    |    Object    |    (Optional) object   structure of attachment    |
|    contentUrl    |    URL    |    (Optional)   reference to location of attachment content    |

#### Accessing Attachments

Notifications can be grouped and delivered in a single post to webhook API.

Notification messages may contain URls pointing to content.
Bot should attach Authorization header to all GET request to following URIs: *apis.skype.com 

**Example*
    GET /v2/attachments/0-weu-d2-f39063e875fbee9ac0ec28b70a706245/views/original  HTTP/1.1
    Host: api.skype.com
    Authorization: Bearer <redacted oauth2 token>

    HTTP/1.1 200 Ok
    ContextId: tcid=901717545203126072, server=CO2SCH020010536
    Content-Length: *

    <Raw binary data>   
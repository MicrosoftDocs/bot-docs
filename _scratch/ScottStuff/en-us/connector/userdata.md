---
layout: page
title: Saving User State Data
permalink: /en-us/connector/userdata/
weight: 3050
parent1: Building your Bot Using the Bot Connector REST API
---

Bot Framework offers three stores that you can use to store user state: [User Data store](#savinguserdata), [Conversation store](#savingconversationdata), and [Private Conversation store](#savingprivateconversationdata). If you want to save state data for a user on a channel that is NOT specific to a conversation, use the User Data store. If you want to save state data for a user on a channel that IS specific to a conversation, use the Private User Data store. If you want to save general data about a conversation on a channel, use the Conversation store. 

You may store personally identifiable information (PII) in the User Data store and the Private Conversation store but not in the Conversation store. To delete PII from the stores, see [Deleting User Data](#deletinguserdata).

Typically, you use the stores to save a user’s preferences so you can tailor the conversation to them the next time you chat. For example, you can use information to alert the user to a news article about a topic that interests them or alert them when an appointment become available. A hiking bot might want to save previous hike information so it can suggest new hikes. It’s up to you how you use the stores.

The maximum amount of data that you can store in each store for a user or conversation is 32 KB. For example, you can store 32 KB of data for User A on channel ABC, 32 KB of data for User A in a private conversation on channel ABC, and 32 KB of data for Conversation 1 on channel ABC.

To control concurrency of the data, the framework uses entity tags (ETag) for POST requests. The framework does not use the standard headers for ETags. Instead, the body of the request and response contains the ETag. Typically, you get the user’s data, change it, and then save it. If the ETag matches the ETag from the store, the server saves the user’s data and the response contains the new ETag. If you specify an ETag and it doesn’t match, the POST fails with 412. This means that the user’s data was changed since the last time you retrieved or saved the user’s data.

If concurrency is not an issue for your bot, then don’t include the **eTag** property.

A GET request will return the user data and ETag from the last POST request. The ETag is not used to get the user’s data from the store. If you haven’t previously saved the user’s data, the **eTag** property in the response will contain an asterisk (*).  


<a id="savinguserdata" />

### Saving user data

To save state data for a user on a specific channel, send a POST request to https://api.botframework.com/v3/botstate/{channelId}/users/{userId}. Set {channelId} to the channel’s ID and {userId} to the ID of the user on that channel. The message’s **channelId** and **from** properties contain the IDs. You can also save the IDs in a secured location so you can access the user’s data the next time.

The body of the POST request is a [UserData](../reference/#userdata) object. Set the **data** property to the data you want to save. If you use entity tags for concurrency control, set the **eTag** property to the ETag value that you received the last time you saved or retrieved the user’s data (whichever is the most recent). If you don’t use entity tags for concurrency, then don’t include the **eTag** property.

The following shows an example POST request that saves the user’s data to the User Data store. The POST request overwrites the user’s data only if the ETag matches the server’s ETag value, or if you don’t specify an ETag.

```cmd
POST https://api.botframework.com/v3/botstate/abcd1234/users/12345678 HTTP/1.1
Authorization: Bearer eyJhbGciOiJIUzI1Ni...
Content-Type: application/json

{
    "data": [
        {
            "trail": "Lake Serene",
            "miles": 8.2,
            "difficulty": "Difficult",
        },
        {
            "trail": "Rainbow Falls",
            "miles": 6.3,
            "difficulty": "Moderate",
        }
    ],
    "eTag": "a1b2c3d4"
}
```

The response will contain the **UserData** object with a new ETag value.


<a id="gettinguserdata" />

### Getting user data

To get the user’s data for a specific channel, send a GET request to https://api.botframework.com/v3/botstate/{channelId}/users/{userId}. Set {channelId} to the channel’s ID and {userId} to the ID of the user on that channel. The message’s **channelId** and **from** properties contain the IDs. You can also save the IDs in a secured location so you can access the user’s data the next time.

The body of the response contains a [UserData](#userdata) object. The **data** property contains the user’s data that you’ve previously saved, and the **eTag** property contains the ETag that you’d use on a subsequent call to save the user’s data. If you haven’t previously saved the user’s data, the **data** property will be null and the **eTag** property will contain an asterisk (*). 

The following shows an example GET request that gets the user’s data. 

```cmd
GET https://api.botframework.com/v3/botstate/abcd1234/users/12345678 HTTP/1.1
Authorization: Bearer eyJhbGciOiJIUzI1Ni...
Content-Type: application/json
```

The following shows the body of the response to the previous request.

```cmd
{
    "data": [
        {
            "trail": "Lake Serene",
            "miles": 8.2,
            "difficulty": "Difficult",
        },
        {
            "trail": "Rainbow Falls",
            "miles": 6.3,
            "difficulty": "Moderate",
        }
    ],
    "eTag": "xyz12345"
}
```


<a id="savingconversationdata" />

### Saving conversation data

To save data for a conversation on a specific channel, send a POST request to https://api.botframework.com/v3/botstate/{channelId}/conversations/{conversationId}. Set {channelId} to the channel’s ID and {conversationId} to the ID of a conversation on that channel. The message’s **channelId** and **conversation** properties contain the IDs. You can also save the IDs in a secured location so you can access the users’ data the next time.

For details about passing the conversation’s data, see [Saving user data](#savinguserdata).

<div class="docs-text-note">Note that because the delete user data endpoint does not delete data from this store, you must not store a user’s personally identifiable information in this store.</div>

### Getting conversation data

To get data for a conversation on a specific channel, send a GET request to https://api.botframework.com/v3/botstate/{channelId}/conversations/{conversationId}. Set {channelId} to the channel’s ID and {conversationId} to the ID of the conversation on that channel. The message’s **channelId** and **conversation** properties contain the IDs. You can also save the IDs in a secured location so you can access the users’ data the next time.

For details about getting the conversation’s data from the response, see [Getting user data](#gettinguserdata).


<a id="savingprivateconversationdata" />

### Saving private conversation data

To save data for a user in a conversation on a specific channel, send a POST request to https://api.botframework.com/v3/botstate/{channelId}/conversations/{conversationId}/users/{userId}. Set {channelId} to the channel’s ID, {conversationId} to the ID of the conversation on that channel, and {userId} to the ID of a user in the conversation. The message’s **channelId**, **conversation**, and **from** properties contain the IDs. You can also save the IDs in a secured location so you can access the user’s data the next time.

For details about passing the user’s data, see [Saving user data](#savinguserdata).


### Getting private conversation data

To get data for a user in a conversation on a specific channel, send a GET request to https://api.botframework.com/v3/botstate/{channelId}/conversations/{conversationId}/users/{userId}. Set {channelId} to the channel’s ID, {conversationId} to the ID of the conversation on that channel, and {userId} to the ID of a user in the conversation. The message’s **channelId**, **conversation**, and **from** properties contain the IDs. You can also save the IDs in a secured location so you can access the user’s data the next time.

For details about getting the user’s data from the response, see [Getting user data](#gettinguserdata).


<a id="deletinguserdata" />

### Deleting the user’s state data

To delete user data from the User Data store and the Private Conversation store, send a DELETE request to https://api.botframework.com/v3/botstate/{channelId}/users/{userId}. Set {channelId} to the channel’s ID and {userId} to the ID of the user on that channel. The message’s **channelId** and **from** properties contain the IDs.

Typically, you delete the user’s data when the user sends your bot a message asking that you delete their data. The [message’s](../reference/#activity) **type** property is set to deleteUserData. After deleting the user's data, send a message confirming that you deleted the data.


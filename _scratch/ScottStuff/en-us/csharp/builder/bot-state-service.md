---
layout: page
title: Using the Bot State Service to Save User State
permalink: /en-us/csharp/builder/stateservice/
weight: 2200
parent1: Building your Bot Using Bot Builder for .NET
---


<span style="color:red">This comment applies to all .NET content: figure out how to link to reference content for all object/methods/properties.</span>


The key to designing a good bot is to make the web service stateless so that it scales. The bot should also be able to track the context of a conversation. Bot Framework helps with these design elements by providing a service for storing bot state. You can use this service to track things such as  _what was the last question I asked them?_ 

For more information about bot state, see [Saving User State Data](/en-us/connector/userdata/).


### Bot state methods

The **BotState** object includes the following methods that you use to store different state context. 


| **Method**                       | **Scoped to**              | **Use cases**                                                
|----------------------------------|----------------------------| ------
| **GetUserData()**                | User                       | Get state data saved in the user's context
| **GetConversationData()**        | Conversation               | Get state data saved in the conversation's context
| **GetPrivateConversationData()** | User & Conversation        | Get state data saved in the context of a user and conversation
| **SetUserData()**                | User                       | Save state data in the context of the user
| **SetConversationData()**        | Conversation               | Save state data in the context of the conversation
| **SetPrivateConversationData()** | User & Conversation        | Save state data in the context of the user and conversation
| **DeleteStateForUser()**         | User                       | Delete the user's state data when the user requests that you delete it or the user removes your bot from their contacts list

When your bot replies to a message, it uses one of the methods to set the state data. When the bot receives another message in the same context, the message will contain the state data that you saved previously. Your bot may store data for a user, a conversation, or a single user within a conversation (called "private" data). Each payload may contain up to 32 kilobytes of data. 

If you call one of the Get methods and the state data doesn't exist, the **Data** field of the **BotData** object will be null and the **ETag** field will be set to "*".

The calling sequence should be Get, update the state data, and Set. This ensures that the **eTag** field is correct.


<span style="color:red">Can I set eTag to "*" to force an overwrite (and not have to worry about a precondition error)? </span>


### Creating a state service client

The **Activity** object contains the **GetStateClient**, which you use to get a state client.

{% highlight csharp %}
    StateClient stateClient = activity.GetStateClient();
{% endhighlight %}


<span style="color:red">This needs more information: For channels that provide a state API such as ???, you should consider using it instead of the framework's state service. Why?</span>


### Setting typed data

The **BotData** object contains the state data. To set the state data, call the object's **SetProperty** method.

{% highlight csharp %}
    BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
    userData.SetProperty<bool>("SentGreeting", true);
    await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);
{% endhighlight %}



### Getting typed data

The **BotData** object contains the state data. To get the state data, call the **GetProperty** method.

{% highlight csharp %}
    BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
    if (userData.GetProperty<bool>("SentGreeting"))
        ... do something ...;
{% endhighlight %}



### Setting complex typed data

<span style="color:red">This example needs more information: Why is this showing another way to do it? If you use complex types, do you need to do it this way? Why is it setting eTag to "*"? Why is the SetProperty's type BotState and not the complex type? Where is myUserData defined? What do I do with the BotData response? </span>

{% highlight csharp %}
    BotState botState = new BotState(stateClient);
    BotData botData = new BotData(eTag: "*");
    botData.SetProperty<BotState>("UserData", myUserData);
    BotData response = await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, botData);
{% endhighlight %}



### Getting complex typed data

<span style="color:red">Why is this define an instance of MyUserData? Shouldn't the first line be deleted, and the third line contain var or the MyUserData type? </span>

{% highlight csharp %}
    MyUserData addedUserData = new MyUserData();
    BotData botData = await botState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
    myUserData = botData.GetProperty<MyUserData>("UserData");
{% endhighlight %}




### Concurrency

Storing state data will fail if another instance of your bot has already changed the object.

<span style="color:red">We should use the same coding patterns unless there is a reason not to in which case we should say why we're doing something differently. Where's botState? </span>    

{% highlight csharp %}
    try
    {
        // get the user data object
        BotData userData = await botState.GetUserDataAsync(activity.ChannelId, activity.From.Id);

        // modify it...
        userData.Data = ...modify...;

        // save it
        await botState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);
    }
    catch (HttpOperationException err)
    {
        // handle precondition failed error if the state was changed by another instance
    }
{% endhighlight %}



### Data that you may want to cache yourself

The following are **Activity** fields that you may want to cache.

<span style="color:red">Why? Is this about initiating conversations?</span>

| **Property**                  | **Description**                                    | **Use cases**                                      
|------------------------------ |----------------------------------------------------|----------------------------------------------------------
| **From**                      | The user's address on a channel | Remembering context for a user on a channel        
| **Conversation**              | The conversation's unique ID    | Remembering context of all users in a conversation    
| **From** and **Conversation** | One of the users in a conversation  | Remembering context of a user in a conversation   

You can use these keys to store information in your own database as appropriate to your needs.

<span style="color:red">I don't understand the above line: their own database and not the state service?</span>



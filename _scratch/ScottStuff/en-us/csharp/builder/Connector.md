---
layout: page
title: Using the Connector Service to Send and Receive Messages
permalink: /en-us/csharp/builder/connector/
weight: 2100
parent1: Building your Bot Using Bot Builder for .NET
---


Bot Connector is the component that ties your bot to the channel. For details about the Connector service, see [Bot Connector REST API](/en-us/connector/overview/). This section provides information about using the Connector-related components of the Bot Builder SDK. While it's important to understand these concepts, most of your work will use [Dialogs](/en-us/csharp/builder/dialogs/) and [FormFlow](/en-us/csharp/builder/formflow/).


### Creating a connector client

The **ConnectorClient** class contains the methods that you use to communicate with the user on the channel. The **Activity** object that the connector passes you contains the channel's endpoint, which you use to pass back messages. 

{% highlight csharp %}
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            . . .
        }
{% endhighlight %}

Because the channel's endpoint may not be stable, you should always use the endpoint that the connector passes you instead of using a cached copy of the endpoint. The only exception is if your bot needs to initiate the conversation, in which case you'll need to use a cached copy (but you should update the cached copy often). 


### Creating the response message

The Connector uses an **Activity** object to pass information between the bot and the channel (user). Every activity contains information used for routing the message to the appropriate destination along with who created the message (**From** field), the context of the message, and the recipient of the message (**Recipient** field).

When you receive a message, the **Recipient** field contains your bot's ID. Because some channels (for example, Slack) assign new identities when it adds the user to the conversation, it is important that you create a reply message using the incoming **Recipient** field as the outgoing **From** field.

Instead of creating and initializing the **Activity** object yourself, you should use the activity's **CreateReply** method. This method correctly initializes the **Recipient**, **From**, and **Conversation** fields for you. The constructor takes the message text that you want to send the user.

{% highlight csharp %}
                Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");
{% endhighlight %}


### Replying to the message

To reply to a message, call the connector client's **ReplyToActivity** method. The Connector service will take care of the details of delivering your message using the appropriate channel semantics.  

{% highlight csharp %}
                await connector.Conversations.ReplyToActivityAsync(reply);
{% endhighlight %}


If you're part of a conversation and you want to send a message instead of replying to someone else's message, call the **SendToConversation** method. You can still use the **CreateReply** method to initialize the message or you can use the **CreateMessageActivity** method to create a message from scratch.


{% highlight csharp %}
                await connector.Conversations.SendToConversationAsync((Activity)newMessage);
{% endhighlight %}


If you're replying to someone else's message, always use the **ReplyToActivity** method.

You can send as many messages as you want, but most channels have built-in throttling levels and you will be subject to whatever limits the channel sets. Also, if you send multiple messages consecutively, the channel may not render them in the correct order.


### Initiating the conversation

There may be times when you want to start the conversation with the user. To initiate a conversation, you need to call the **CreateConversation** or **CreateDirectConversation** method to get a **ConversationAccount** object from the channel. You use the **CreateDirectConversation** method to create a private conversation with a single user, and the **CreateConversation** method to create a conversation with multiple users. Not all channels support group conversations; see the channel's documentation to determine whether they support group conversations.

To call either of these methods, you need to have previously cached the channel's service URL, and the bot's and user's account information.

{% highlight csharp %}
    var userAccount = new ChannelAccount(name: "Larry", id: "@UV357341");
    var connector = new ConnectorClient(new Uri(actvity.ServiceUrl));
    var conversationId = await connector.Conversations.CreateDirectConversationAsync(botAccount, userAccount);

    IMessageActivity message =  Activity.CreateMessageActivity();
    message.From = botAccount;
    message.Recipient = userAccount;
    message.Conversation = new ConversationAccount(id: conversationId.Id);
    message.Text = "Hello";
    message.Locale = "en-Us";
    await connector.Conversations.SendToConversationAsync((Activity)message); 
{% endhighlight %}
 
<span style="color:red">Is the following code correct - doesn't look correct to me. ConversationParameters is being passed message.Recipient but it's not set until later. So the message goes to any one of the participants?</span>

{% highlight csharp %}
    var connector = new ConnectorClient(new Uri(incomingMessage.ServiceUrl));

    List<ChannelAccount> participants = new List<ChannelAccount>();
    participants.Add(new ChannelAccount("joe@contoso.com", "Joe the Engineer"));
    participants.Add(new ChannelAccount("sara@contso.com", "Sara in Finance"));

    ConversationParameters cpMessage = new ConversationParameters(message.Recipient, true, participants, "Quarter End Discussion");
    var conversationId = await connector.Conversations.CreateConversationAsync(cpMessage);

    IMessageActivity message = Activity.CreateMessageActivity();
    message.From = botAccount;
    message.Recipient = new ChannelAccount("lydia@contoso.com", "Lydia the CFO"));
    message.Conversation = new ConversationAccount(id: conversationId.Id);
    message.ChannelId = incomingMessage.ChannelId;
    message.Text = "Hey, what's up everyone?";
    message.Locale = "en-Us";

    await connector.Conversations.SendToConversationAsync((Activity)message); 
{% endhighlight %}



### Adding attachments to messages

A message exchange between the user and bot can be as simple as an exchange of text strings or as complex as a multiple card carousel with buttons and actions. For most bots, the **Text** field is the only **Activity** field that you need to worry about. A person sent you some text or your bot is sending back some text. If your bot converses in a language other than English, you'd also need to set the **Locale** field.

The **Activity** object also includes the **Attachments** field, which may contain an array of **Attachment** objects. Attachments can be a simple media type such as an image or video, or they can be rich card types such as a HeroCard or Receipt Card. A rich card is made up of a title, link, description and image. Additionally, you can use the **AttachmentLayout** field to specify whether to display the cards as a list or a carousel.


To pass a simple media attachment (for example, an image, audio, video or file) to an activity, you create an **Attachment** object and set the **ContentType**, **ContentUrl**, and **Name** fields as shown below.

{% highlight csharp %}
    replyMessage.Attachments.Add(new Attachment()
    {
        ContentUrl = "https://upload.wikimedia.org/wikipedia/en/a/a6/Bender_Rodriguez.png",
        ContentType = "image/png",
        Name = "Bender_Rodriguez.png"      
    });
{% endhighlight %}


The following example shows how to add a HeroCard as an attachment to a reply message.

<span style="color:red">Please consider removing the C-style naming (for example, the "pl"  part of plCard). </span>

{% highlight csharp %}
    Activity replyToConversation = message.CreateReply("Should go to conversation, with a carousel");
    replyToConversation.AttachmentLayout = AttachmentLayouts.Carousel;
    replyToConversation.Attachments = new List<Attachment>();

    Dictionary<string, string> cardContentList = new Dictionary<string, string>();
    cardContentList.Add("PigLatin", "https://<ImageUrl1>");
    cardContentList.Add("Pork Shoulder", "https://<ImageUrl2>");
    cardContentList.Add("Bacon", "https://<ImageUrl3>");

    foreach(KeyValuePair<string, string> cardContent in cardContentList)
    {
        List<CardImage> cardImages = new List<CardImage>();
        cardImages.Add(new CardImage(url:cardContent.Value ));

        List<CardAction> cardButtons = new List<CardAction>();

        CardAction plButton = new CardAction()
        {
            Value = $"https://en.wikipedia.org/wiki/{cardContent.Key}",
            Type = "openUrl",
            Title = "WikiPedia Page"
        };

        cardButtons.Add(plButton);

        HeroCard plCard = new HeroCard()
        {
            Title = $"I'm a hero card about {cardContent.Key}",
            Subtitle = $"{cardContent.Key} Wikipedia Page",
            Images = cardImages,
            Buttons = cardButtons
        };

        Attachment plAttachment = plCard.ToAttachment();
        replyToConversation.Attachments.Add(plAttachment);
    }

    var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
{% endhighlight %}





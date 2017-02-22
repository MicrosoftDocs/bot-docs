---
title: Send proactive messages by using the Bot Builder SDK for .NET | Microsoft Docs
description: Learn how to send proactive messages by using the Bot Builder SDK for .NET.
keywords: Bot Framework, dotnet, .NET, Bot Builder, SDK, proactive message, ad hoc message, dialog-based message
author: kbrandl
manager: rstand
ms.topic: develop-dotnet-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/22/2017
ms.reviewer: rstand
#ROBOTS: Index
---
# Send proactive messages using with the Bot Builder SDK for .NET
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-proactive-messages.md)
> * [Node.js](bot-framework-nodejs-howto-proactive-messages.md)
>

## Introduction

[!include[Introduction to proactive messages - part 1](../includes/snippet-proactive-messages-intro-1.md)] 

[!include[Introduction to proactive messages - part 2](../includes/snippet-proactive-messages-intro-2.md)] 

In this article, we'll discuss how to send proactive messages by using the Bot Builder SDK for .NET. 

## Send an ad hoc proactive message

The following code samples show how to send an ad hoc proactive message by using the Bot Builder SDK for .NET.

To be able to send an ad hoc message to a user, the bot must first collect (and save) information about the user from the current conversation. 

```cs
public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
{
    var message = await result;
    
    // Extract data from the user's message that the bot will need later to send an ad hoc message to the user. 
    // Store the extracted data in a custom class "ConversationStarter" (not shown here).

    ConversationStarter.toId = message.From.Id;
    ConversationStarter.toName = message.From.Name;
    ConversationStarter.fromId = message.Recipient.Id;
    ConversationStarter.fromName = message.Recipient.Name;
    ConversationStarter.serviceUrl = message.ServiceUrl;
    ConversationStarter.channelId = message.ChannelId;
    ConversationStarter.conversationId = message.Conversation.Id;

    // (Save this information somewhere that it can be accessed later, such as in a database.)

    await context.PostAsync("Hello user, good to meet you! I now know your address and can send you notifications in the future.");
    context.Wait(MessageReceivedAsync);
}
```

> [!NOTE]
> For simplicity, this example does not specify how to store the user data. 
> The bot can store the user data in any manner, so long as it can be accessed later when the bot is ready to send the ad hoc message.


After the bot has collected information about the user, it can send an ad hoc proactive message to the user at any time. 
To do so, it simply retrieves the user data it stored previously, constructs the message, and sends it. 

```cs
// Use the data stored previously to create the required objects.
var userAccount = new ChannelAccount(toId,toName);
var botAccount = new ChannelAccount(fromId, fromName);
var connector = new ConnectorClient(new Uri(serviceUrl));

// Create a new message.
IMessageActivity message = Activity.CreateMessageActivity();
if (!string.IsNullOrEmpty(conversationId) && !string.IsNullOrEmpty(channelId))	
{
    // If conversation ID and channel ID was stored previously, use it.
    message.ChannelId = channelId;
}
else
{
    // Conversation ID was not stored previously, so create a conversation. 
    // Note: If the user has an existing conversation in a channel, this will likely create a new conversation window.
    conversationId = (await connector.Conversations.CreateDirectConversationAsync( botAccount, userAccount)).Id;
}

// Set the address-related properties in the message and send the message.
message.From = botAccount;
message.Recipient = userAccount;
message.Conversation = new ConversationAccount(id: conversationId);
message.Text = "Hello, this is a notification";
message.Locale = "en-Us";
await connector.Conversations.SendToConversationAsync((Activity)message);
```

> [!NOTE]
> If the bot specifies a conversation ID that was stored previously, the message will likely be delivered to the user in the existing conversation window on the client. 
> If the bot generates a new conversation ID, the message will be delivered to the user in a new conversation window on the client (as long as the client allows multiple conversation windows). 

> [!TIP]
> An ad hoc proactive message can be initiated like from 
> asynchronous triggers such as http requests, timers, queues or from anywhere else that the developer chooses.

## Send a dialog-based proactive message

The following code samples show how to send a dialog-based proactive message by using the Bot Builder SDK for .NET.

####Dialog-based proactive messages

The following code samples show how to send a dialog-based message by using the Bot Builder SDK for .NET.

To be able to send a dialog-based proactive message to a user, the bot must first collect (and save) information from the current conversation. 
The **resumption cookie** contains information that the bot can use in the process of sending a dialog-based proactive message. 

```cs
public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
{
    var message = await result;
    
    // Store the resumption cookie so that the bot can resume this conversation later.
    var resumptionCookie = new ResumptionCookie(message).GZipSerialize();

    await context.PostAsync("Greetings, user! I now know how to start a proactive message to you."); 
    context.Wait(MessageReceivedAsync);
}
```




Now the real change happens on how we trigger the proactive message: This time we are not just sending a message, but creating a whole dialog and adding it to the current stack of dialogs. In other words, we will take the existing conversation, no matter how many dialogs are currently involved and we will add one more, making it the one in control. That new dialog is not only going to carry the notification but also decide when to get back to the original one.

In C#:

	public static async Task Resume() 
	{
		//Recreate the message from the resumption cookie we've had stored previously
		var message = ResumptionCookie.GZipDeserialize(resumptionCookie).GetMessage();
		var client = new ConnectorClient(new Uri(message.ServiceUrl));

		//Create a scope so we can work with state from bot framework
		using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
		{
			var botData = scope.Resolve<IBotData>();
			await botData.LoadAsync(CancellationToken.None);

			//This is our dialog stack
			var stack = scope.Resolve<IDialogStack>();
                
			//interrupt the stack. This means that we're stopping whatever conversation that is currently happening with the user
			//Then adding this stack to run and once it's finished, we will be back to the original conversation
			
			//This is the new dialog we will be injecting into the stack.
			var dialog =new SurveyDialog();
			//Note here we could have very well decided to reset the stack.
			//So it would "forget" the current conversation
			stack.Call(dialog.Void<object, IMessageActivity>(), null);
			await stack.PollAsync(CancellationToken.None);

			//flush dialog stack back to its state store
			await botData.FlushAsync(CancellationToken.None);
           
		}
	}

This is evidently more complex than the first example: We are not gaining access to the bot framework's store where the dialog stack is saved and changing it by adding our new "SurveyDialog" on top. The resumption cookie gives us a simple way of serializing and deserializing the entire message we received from the user.

In Node:

	function startProactiveDialog(addr) {
		// set resume:false to resume at the root dialog
		// else true to resume the previous dialog
		bot.beginDialog(savedAddress, "*:/survey", {}, { resume: true });  
	}

Although the code for Node is much simpler, it does require a custom file implemented with it, not listed here. [A copy of this botadapter.js file can be found here instead](https://trpp24botsamples.visualstudio.com/_git/Code?path=%2FNode%2Fcore-proactiveMessages%2FstartNewDialog%2Fbotadapter.js&version=GBmaster&_a=contents).

Despite of the implementation differences in Node and C#, they are both achieving the same: Taking the stored data from the message, creating a new dialog and injecting into the dialog stack stored in the Bot Framework, directing the user to that new dialog.

Now how does that SurveyDialog manages the exit back to the original conversation?

In C#:

	[Serializable]
    public class SurveyDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            await context.PostAsync("Hello, I'm the survey dialog. I'm interrupting your conversation to ask you a question. Type \"done\" to resume");

            context.Wait(this.MessageReceivedAsync);
        }
        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            if ((await result).Text == "done")
            {
                await context.PostAsync("Great, back to the original conversation!");
                context.Done(String.Empty); //Finish this dialog
            }
            else
            {
                await context.PostAsync("I'm still on the survey until you type \"done\"");
                context.Wait(MessageReceivedAsync); //Not done yet
            }
        }
    }

Note this dialog is very simplified for the sake of demonstrating the concept: The key point is how it has its own logic and decides when it is finished by calling context.Done(). This is in no way different than [what discussed previously about how dialogs work](bot-framework-design-core-dialogs.md). When a dialog is finished and ready to be removed from the stack, it calls context.Done()

Now in Node:

	bot.dialog('/survey', [
		function (session, args, next) {
    		if (session.message.text=="done"){
      			session.send("Great, back to the original conversation");
      			session.endDialog();
    		}else{
          		session.send('Hello, I\'m the survey dialog. I\'m interrupting your conversation to ask you a question. Type "done" to resume');
    		}
		},
		function (session, results) {
		}
	]);

Similarly, in Node we have the same control by using session.endDialog().

## Additional resources

In this article, we discussed how to send proactive messages by using the Bot Builder SDK for .NET. 
To learn more, see:

> [!NOTE]
> To do: Add links to related content (link to 'detailed readme' and 'full C# code' that Matt refers to)
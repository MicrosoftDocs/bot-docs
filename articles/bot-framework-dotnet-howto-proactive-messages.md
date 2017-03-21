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
ms.reviewer:
#ROBOTS: Index
---
# Send proactive messages

<!--
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-proactive-messages.md)
> * [Node.js](bot-framework-nodejs-howto-proactive-messages.md)
>
-->


[!include[Introduction to proactive messages - part 1](../includes/snippet-proactive-messages-intro-1.md)] 
This article describes how to send proactive messages by using the Bot Builder SDK for .NET. 

## Types of proactive messages 

[!include[Introduction to proactive messages - part 2](../includes/snippet-proactive-messages-intro-2.md)] 

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
To do so, it simply retrieves the user data that it stored previously, constructs the message, and sends it. 

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

To be able to send a dialog-based proactive message to a user, the bot must first collect (and save) information from the current conversation. 

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

When it is time to send the message, the bot creates a new dialog, thereby adding this dialog to the top of the dialog stack and giving it control of the conversation. 
The new dialog will deliver the proactive message and will also decide when to close and return control to the prior dialog in the stack. 
The resumption cookie provides a simple way of serializing and deserializing the entire message received from the user.

```cs
public static async Task Resume() 
{
    // Recreate the message from the resumption cookie that was saved previously.
    var message = ResumptionCookie.GZipDeserialize(resumptionCookie).GetMessage();
    var client = new ConnectorClient(new Uri(message.ServiceUrl));

    // Create a scope that can be used to work with state from bot framework.
    using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, message))
    {
        var botData = scope.Resolve<IBotData>();
        await botData.LoadAsync(CancellationToken.None);

        // This is the dialog stack.
        var stack = scope.Resolve<IDialogStack>();

        // Create the new dialog and add it to the stack.
        var dialog =new SurveyDialog();
        stack.Call(dialog.Void<object, IMessageActivity>(), null);
        await stack.PollAsync(CancellationToken.None);

        // Flush the dialog stack back to its state store.
        await botData.FlushAsync(CancellationToken.None);        
    }
}
```

The SurveyDialog controls the conversation until it finishes. 
Then, it closes (by calling `context.Done`), thereby returning control back to the previous dialog. 

```cs
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
            context.Done(String.Empty); // Finish this dialog.
        }
        else
        {
            await context.PostAsync("I'm still on the survey until you type \"done\"");
            context.Wait(MessageReceivedAsync); // Not done yet.
        }
    }
}
```

## Additional resources

- [Designing conversation flow](bot-framework-design-core-dialogs.md)
- [Bot capabilities](bot-framework-design-capabilities.md)
- <a href="https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html" target="_blank">Builder library</a>
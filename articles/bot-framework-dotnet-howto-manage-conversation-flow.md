---
title: Manage conversation flow using dialogs with the Bot Builder SDK for .NET | Microsoft Docs
description: Learn how to manage conversation flow using dialogs and the Bot Builder SDK for .NET.
keywords: Bot Framework, dialog, conversation flow, conversation, dotnet, .NET, Bot Builder, SDK
author: kbrandl
manager: rstand
ms.topic: develop-dotnet-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/17/2017
ms.reviewer:
#ROBOTS: Index
---
# Manage conversation flow using dialogs
> [!div class="op_single_selector"]
> * [.NET](bot-framework-dotnet-howto-manage-conversation-flow.md)
> * [Node.js](bot-framework-nodejs-howto-manage-conversation-flow.md)
>

## Introduction

In this article, we'll walk through an example of managing conversation flow using dialogs and the Bot Builder SDK for .NET. 

> [!NOTE]
> To do: add introductory content -- image of example bot dialog flow (from: media/designing-bots/core/dialogs-screens.png) 
> and text describing the steps of that dialog flow.

## Invoke the root dialog

First, the bot controller invokes the "root dialog". 
The following example shows how to wire the basic HTTP GET call to a controller and then invoke the root dialog. 

```cs
public class MessagesController : ApiController
{
    public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
    {
            // Redirect to the root dialog.
            await Conversation.SendAsync(activity, () => new RootDialog()); 
            ...
    }
}
```

## Invoke the 'New Order' dialog

Next, the root dialog invokes the 'New Order' dialog. 

```cs
[Serializable]
public class RootDialog : IDialog<object>
{
    public async Task StartAsync(IDialogContext context)
    {
        // Root dialog initiates and waits for the next message from the user. 
        // When a message arrives, call MessageReceivedAsync.
        context.Wait(this.MessageReceivedAsync); 
    }

    public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
    {
        var message = await result; // We've got a message!
        if (message.Text.ToLower().Contains("order"))
        {
            // User said 'order', so invoke the New Order Dialog and wait for it to finish.
            // Then, call ResumeAfterNewOrderDialog.
            await context.Forward(new NewOrderDialog(), this.ResumeAfterNewOrderDialog, message, CancellationToken.None);
        }
        // User typed something else; for simplicity, ignore this input and wait for the next message.
        context.Wait(this.MessageReceivedAsync);
    }

    private async Task ResumeAfterNewOrderDialog(IDialogContext context, IAwaitable<string> result)
    {
        // Store the value that NewOrderDialog returned. 
        // (At this point, new order dialog has finished and returned some value to use within the root dialog.)
        var resultFromNewOrder = await result;

        await context.PostAsync($"New order dialog just told me this: {resultFromNewOrder}");

        // Again, wait for the next message from the user.
        context.Wait(this.MessageReceivedAsync);
    }
}
```

##<a id="dialog-lifecycle"></a> Dialog lifecycle

When a dialog is invoked, it takes control of the conversation flow. 
Every new message will be subject to processing by that dialog until it either closes or redirects to another dialog. 

In C#, you can use `context.Wait()` to specify the callback to invoke the next time the user sends a message. 
To close a dialog and remove it from the stack (thereby sending the user back to the prior dialog in the stack), use `context.Done()`. 
You must end every dialog method with `context.Wait()`, `context.Fail()`, `context.Done()`, 
or some redirection directive such as `context.Forward()` or `context.Call()`. 
A dialog method that does not end with one of these will result in an error 
(because the framework does not know what action to take the next time the user sends a message).

## Additional resources

In this article, we walked through an example of managing conversation flow using dialogs and the Bot Builder SDK for .NET. 
To learn more, see:

> [!NOTE]
> To do: Add links to related articles



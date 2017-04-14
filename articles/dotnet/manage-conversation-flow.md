---
title: Manage conversation flow with dialogs | Microsoft Docs
description: Learn how to model and manage conversation flow using dialogs and the Bot Builder SDK for .NET.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 02/17/2017
ms.reviewer:
ROBOTS: Index, Follow
---
# Manage conversation flow with dialogs

<!--
> [!div class="op_single_selector"]
> * [.NET](~/dotnet/howto-manage-conversation-flow.md)
> * [Node.js](~/nodejs/manage-conversation-flow.md)
>
-->

[!include[Dialog flow example](~/includes/snippet-dotnet-manage-conversation-flow-intro.md)]

This article describes how to model this conversation flow by using [dialogs](~/dotnet/dialogs.md) and the Bot Builder SDK for .NET. 

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

- [Dialogs](~/dotnet/dialogs.md)
- [Designing conversation flow](~/bot-design-conversation-flow.md)
- [Builder library][builderLibrary]

[builderLibrary]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html

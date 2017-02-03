---
layout: page
title: Basic bot
permalink: /en-us/azure-bot-service/templates/basic/
weight: 12000
parent1: Azure Bot Service
parent2: Templates
---


The basic bot template shows how to use dialogs to respond to user input. For information about using dialogs, see [Dialogs](/en-us/csharp/builder/sdkreference/dialogs.html).

When a user posts a message, it’s sent to the bot’s **Run** method in the Run.csx file. Before the bot processes the message, it first authenticates the request (see [Authentication](/en-us/restapi/authentication/)). If the validation fails, the bot responds with Unauthorized. The following shows the code that the template uses to authenticate the request. 

{% highlight csharp %}
    // Authenticates the incoming request. If the request is authentic, it adds the activity.ServiceUrl to 
    // MicrosoftAppCredentials.TrustedHostNames. Otherwise, it returns Unauthorized.

    if (!await botAuthenticator.Value.TryAuthenticateAsync(req, new [] {activity}, CancellationToken.None))
    {
        return BotAuthentication.GenerateUnauthorizedResponse(req);
    }
{% endhighlight %}    


If the request is authentic, the bot processes the message. The message includes an activity type, which the bot uses to decide what action to take. You would update this code if you want to respond to the other activity types. For example, if you save user state, you’ll need to respond to the DeleteUserData message.

{% highlight csharp %}
    switch (activity.GetActivityType())
    {
        case ActivityTypes.Message:
            // Processes the user’s message.
            break;
        case ActivityTypes.ConversationUpdate:
            // Welcomes the users to the conversation.
            break;
        case ActivityTypes.ContactRelationUpdate:
        case ActivityTypes.Typing:
        case ActivityTypes.DeleteUserData:
        case ActivityTypes.Ping:
        default:
            log.Error($"Unknown activity type ignored: {activity.GetActivityType()}");
            break;
    }
{% endhighlight %}

The first message that the channel sends to the bot is a ConversationUpdate message that contains the users that are in the conversation. The template shows how you’d use this message to welcome the users to the conversation. Because the bot is considered a user in the conversation, the list of users will include the bot. The code identifies the bot and ignores it when welcoming the other users.

{% highlight csharp %}
    case ActivityTypes.ConversationUpdate:
        IConversationUpdateActivity update = activity;
        using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, activity))
        {
            var client = scope.Resolve<IConnectorClient>();
            if (update.MembersAdded.Any())
            {
                var reply = activity.CreateReply();
                var newMembers = update.MembersAdded?.Where(t => t.Id != activity.Recipient.Id);
                foreach (var newMember in newMembers)
                {
                    reply.Text = "Welcome";
                    if (!string.IsNullOrEmpty(newMember.Name))
                    {
                        reply.Text += $" {newMember.Name}";
                    }
                    reply.Text += "!";
                    await client.Conversations.ReplyToActivityAsync(reply);
                }
            }
        }
        break;
{% endhighlight %}

Most messages will have a Message activity type, and will contain the text and attachments that the user sent. If the message’s activity type is Message, the template posts the message to **EchoDialog** in the context of the current message (see EchoDialog.csx). 

{% highlight csharp %}
    switch (activity.GetActivityType())
    {
        case ActivityTypes.Message:
            await Conversation.SendAsync(activity, () => new EchoDialog());
            break;
{% endhighlight %}

The EchoDialog.csx file contains the dialog that controls the conversation with the user. When the dialog’s instantiated, the dialog’s **StartAsync** method runs and calls IDialogContext.Wait with the continuation delegate that’s called when there is a new message. In the initial case, there is an immediate message available (the one that launched the dialog) and the message is immediately passed to the **MessageReceivedAsync** method.

{% highlight csharp %}
[Serializable]
public class EchoDialog : IDialog<object>
{
    protected int count = 1;

    public Task StartAsync(IDialogContext context)
    {
        try
        {
            context.Wait(MessageReceivedAsync);
        }
        catch (OperationCanceledException error)
        {
            return Task.FromCanceled(error.CancellationToken);
        }
        catch (Exception error)
        {
            return Task.FromException(error);
        }

        return Task.CompletedTask;
    }
{% endhighlight %}

The **MessageReceivedAsync** method echoes the user’s input and counts the number of interactions with the user. If the user’s input is the word, *reset*, the method uses [PromptDialog](/en-us/csharp/builder/sdkreference/d9/d03/class_microsoft_1_1_bot_1_1_builder_1_1_dialogs_1_1_prompt_dialog.html) to confirm that the user wants to reset the counter. The method calls the IDialogContext.Wait method which suspends the bot until it receives the next message.

{% highlight csharp %}
    public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
    {
        var message = await argument;
        if (message.Text == "reset")
        {
            PromptDialog.Confirm(
                context,
                AfterResetAsync,
                "Are you sure you want to reset the count?",
                "Didn't get that!",
                promptStyle: PromptStyle.Auto);
        }
        else
        {
            await context.PostAsync($"{this.count++}: You said {message.Text}");
            context.Wait(MessageReceivedAsync);
        }
    }
{% endhighlight %}

The **AfterResetAsync** method processes the user’s response to **PromptDialog**. If the user confirms the request, the counter is reset and a message is sent to the user confirming the action. The method then calls the IDialogContext.Wait method to suspend the bot until it receives the next message, which gets processed by **MessageReceivedAsync**. 

{% highlight csharp %}
    public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
    {
        var confirm = await argument;
        if (confirm)
        {
            this.count = 1;
            await context.PostAsync("Reset count.");
        }
        else
        {
            await context.PostAsync("Did not reset count.");
        }
        context.Wait(MessageReceivedAsync);
    }
{% endhighlight %}

The following are the project files used by the function; you should not have to modify any of them.

|**File**|**Description**
|function.json|This file contains the function’s bindings. You should not modify this file.
|project.json|This file contains the project’s NuGet references. You should only have to change this file if you add a new reference.
|project.lock.json|This file contains the list of packages. You should not modify this file.
|host.json|A metadata file that contains the global configuration options affecting the function.

## Next steps

 You might update this code if you want to change the conversation with the user. For example, by adding other prompts to get information from the user, adding [attachments](/en-us/csharp/builder/sdkreference/attachments.html) such as cards to provide a richer user experience, or chaining together other dialogs (see [Dialog Chains](/en-us/csharp/builder/sdkreference/dialogs.html#Fluent)).

### Resources
* [Bot Builder Samples GitHub Repo](https://github.com/Microsoft/BotBuilder-Samples)
* [Bot Builder SDK C# Reference](https://docs.botframework.com/en-us/csharp/builder/sdkreference/)
* [Bot Builder SDK](https://github.com/Microsoft/BotBuilder-Samples)
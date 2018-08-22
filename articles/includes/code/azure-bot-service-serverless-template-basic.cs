// <authenticateRequest>
// Authenticates the incoming request. If the request is authentic, it adds the activity.ServiceUrl to 
// MicrosoftAppCredentials.TrustedHostNames. Otherwise, it returns Unauthorized.

if (!await botAuthenticator.Value.TryAuthenticateAsync(req, new [] {activity}, CancellationToken.None))
{
    return BotAuthentication.GenerateUnauthorizedResponse(req);
}
// </authenticateRequest>



// <processMessage>
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
    default:
        log.Error($"Unknown activity type ignored: {activity.GetActivityType()}");
        break;
}
// </processMessage>



// <conversationUpdate>
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
// </conversationUpdate>



// <message>
switch (activity.GetActivityType())
{
    case ActivityTypes.Message:
        await Conversation.SendAsync(activity, () => new EchoDialog());
        break;
    ...
}
// </message>



// <echoDialog>
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
    ...
}
// </echoDialog>



// <messageReceivedAsync>
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
// </messageReceivedAsync>



// <afterResetAsync>
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
// </afterResetAsync>

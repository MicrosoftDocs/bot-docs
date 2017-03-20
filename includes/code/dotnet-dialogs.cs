// <usingStatement>
using Microsoft.Bot.Builder.Dialogs;
// </usingStatement>


// <echobot1>
[Serializable]
public class EchoDialog : IDialog<object>
{
    public async Task StartAsync(IDialogContext context)
    {
        context.Wait(MessageReceivedAsync);
    }

    public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
    {
        var message = await argument;
        await context.PostAsync("You said: " + message.Text);
        context.Wait(MessageReceivedAsync);
    }
}
// </echobot1>


// <echobot2>
public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
{
    // Check if activity is of type message
    if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
    {
        await Conversation.SendAsync(activity, () => new EchoDialog());
    }
    else
    {
        HandleSystemMessage(activity);
    }
    return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
}
// </echobot2>


// <echobot3>
[Serializable]
public class EchoDialog : IDialog<object>
{
    protected int count = 1;

    public async Task StartAsync(IDialogContext context)
    {
        context.Wait(MessageReceivedAsync);
    }

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
                promptStyle: PromptStyle.None);
        }
        else
        {
            await context.PostAsync($"{this.count++}: You said {message.Text}");
            context.Wait(MessageReceivedAsync);
        }
    }

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
}
// </echobot3>


// <serialization>
var builder = new ContainerBuilder();
builder.RegisterModule(new DialogModule());
builder.RegisterModule(new ReflectionSurrogateModule());
// </serialization>


// <chain1>
var query = from x in new PromptDialog.PromptString(Prompt, Prompt, attempts: 1)
            let w = new string(x.Reverse().ToArray())
            select w;
// </chain1>


// <chain2>
var query = from x in new PromptDialog.PromptString("p1", "p1", 1)
            from y in new PromptDialog.PromptString("p2", "p2", 1)
            select string.Join(" ", x, y);
// </chain2>


// <chain3>
query = query.PostToUser();
// </chain3>


// <chain4>
var logic =
    toBot
    .Switch
    (
        new RegexCase<string>(new Regex("^hello"), (context, text) =>
        {
            return "world!";
        }),
        new Case<string, string>((txt) => txt == "world", (context, text) =>
        {
            return "!";
        }),
        new DefaultCase<string, string>((context, text) =>
        {
            return text;
        }
    )
);
// </chain4>


// <chain5>
var joke = Chain
    .PostToChain()
    .Select(m => m.Text)
    .Switch
    (
        Chain.Case
        (
            new Regex("^chicken"),
            (context, text) =>
                Chain
                .Return("why did the chicken cross the road?")
                .PostToUser()
                .WaitToBot()
                .Select(ignoreUser => "to get to the other side")
        ),
        Chain.Default<string, IDialog<string>>(
            (context, text) =>
                Chain
                .Return("why don't you like chicken jokes?")
        )
    )
    .Unwrap()
    .PostToUser().
    Loop();
// </chain5>
// <processMessage>
switch (activity.GetActivityType())
{
    case ActivityTypes.Message:
        await Conversation.SendAsync(activity, () => new MainDialog());
        break;
    ...
}
// </processMessage>



// <mainDialog>
public class MainDialog : IDialog<BasicForm>
{
    public MainDialog()
    {
    }

    public Task StartAsync(IDialogContext context)
    {
        context.Wait(MessageReceivedAsync);
        return Task.CompletedTask;
    }
    ...
}
// </mainDialog>



// <collectUserInput>
public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
{
    var message = await argument;
    context.Call(BasicForm.BuildFormDialog(FormOptions.PromptInStart), FormComplete);
}

private async Task FormComplete(IDialogContext context, IAwaitable<BasicForm> result)
{
    try
    {
        var form = await result;
        if (form != null)
        {
            await context.PostAsync("Thanks for completing the form! Just type anything to restart it.");
        }
        else
        {
            await context.PostAsync("Form returned empty response! Type anything to restart it.");
        }
    }
    catch (OperationCanceledException)
    {
        await context.PostAsync("You canceled the form! Type anything to restart it.");
    }

    context.Wait(MessageReceivedAsync);
}
// </collectUserInput>

    

// <basicForm>
public enum CarOptions { Convertible=1, SUV, EV };
public enum ColorOptions { Red=1, White, Blue };

[Serializable]
public class BasicForm
{
    [Prompt("Hi! What is your {&}?")]
    public string Name { get; set; }

    [Prompt("Please select your favorite car type {||}")]
    public CarOptions Car { get; set; }

    [Prompt("Please select your favorite {&} {||}")]
    public ColorOptions Color { get; set; }

    public static IForm<BasicForm> BuildForm()
    {
        // Builds an IForm<T> based on BasicForm
        return new FormBuilder<BasicForm>().Build();
    }

    public static IFormDialog<BasicForm> BuildFormDialog(FormOptions options = FormOptions.PromptInStart)
    {
        // Generate a new FormDialog<T> based on IForm<BasicForm>
        return FormDialog.FromForm(BuildForm, options);
    }
}
// </basicForm>

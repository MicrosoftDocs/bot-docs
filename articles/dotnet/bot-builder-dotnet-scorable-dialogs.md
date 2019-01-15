---
title: Global message handlers using scorables
description: Create more flexible dialogs using scorables within the  Bot Framework SDK for .NET.
author: matthewshim-ms
ms.author: v-shimma
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'
---
# Global message handlers using scorables

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

Users attempt to access certain functionality within a bot by using words like "help," "cancel," or "start over" in the middle of a conversation when the bot is expecting a different response. You can design your bot to gracefully handle such requests using scorable dialogs.

Scorable dialogs monitor all incoming messages and determine whether a message is actionable in some way. Messages that are scorable are assigned a score between [0 – 1] by each scorable dialog. The scorable dialog that determines the highest score is added to the top of the dialog stack and then hands the response to the user. After the scorable dialog completes execution, the conversation continues from where it left off.

Scorables enable you to create more flexible conversations by allowing your users to 'interrupt' the normal conversation flow you find in regular dialogs.

## Create a scorable dialog

First, define a new [dialog](bot-builder-dotnet-dialogs.md). The following code uses a dialog that is derived from the `IDialog` interface.

```cs
public class SampleDialog : IDialog<object>
{
    public async Task StartAsync(IDialogContext context)
    {
        await context.PostAsync("This is a Sample Dialog which is Scorable. Reply with anything to return to the prior prior dialog.");

        context.Wait(this.MessageReceived);
    }

    private async Task MessageReceived(IDialogContext context, IAwaitable<IMessageActivity> result)
    {
        var message = await result;

        if ((message.Text != null) && (message.Text.Trim().Length > 0))
        {
            context.Done<object>(null);
        }
        else
        {
            context.Fail(new Exception("Message was not a string or was an empty string."));
        }
    }
}
```
To make a scorable dialog, create a class that inherits from the `ScorableBase` abstract class. The following code shows a `SampleScorable` class.

```cs
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Scorables.Internals;

public class SampleScorable : ScorableBase<IActivity, string, double>
{
    private readonly IDialogTask task;

    public SampleScorable(IDialogTask task)
    {
        SetField.NotNull(out this.task, nameof(task), task);
    }
}
```
The `ScorableBase` abstract class inherits from the `IScorable` interface. You will need to implement the following `IScorable` methods in your class:

- `PrepareAsync` is the first method that is called in the scorable instance. It accepts incoming message activity, analyzes and sets the dialog's state, which is passed to all the other methods of the `IScorable` interface.

```cs
protected override async Task<string> PrepareAsync(IActivity item, CancellationToken token)
{
        // TODO: insert your code here
}
```

- The `HasScore` method checks the state property to determine if the scorable dialog should provide a score for the message. If it returns false, the message will be ignored by the scorable dialog.

```cs
protected override bool HasScore(IActivity item, string state)
{
        // TODO: insert your code here
}
```

- `GetScore` will only trigger if `HasScore` returns true. You’ll provision the logic in this method to determine the score for a message between 0 - 1.

```cs
protected override double GetScore(IActivity item, string state)
{
        // TODO: insert your code here
}
```
- In the `PostAsync` method, define core actions to be performed for the scorable class. All scorable dialogs will monitor incoming messages, and assign scores to valid messages based on the scorables' GetScore method. The scorable class which determines the highest score (between 0 - 1.0) will then trigger that scorable's `PostAsync` method.

```cs
protected override Task PostAsync(IActivity item, string state, CancellationToken token)
{
        //TODO: insert your code here
}
```

- `DoneAsync` is called after the scoring process is complete. Use this method to dispose of any scoped resources.

```cs
protected override Task DoneAsync(IActivity item, string state, CancellationToken token)
{
        //TODO: insert your code here
}
```

## Create a module to register the IScorable service

Next, define a `Module` that will register the `SampleScorable` class as a component. This will provision the `IScorable` service.

```cs
public class GlobalMessageHandlersBotModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder
            .Register(c => new SampleScorable(c.Resolve<IDialogTask>()))
            .As<IScorable<IActivity, double>>()
            .InstancePerLifetimeScope();
    }
}
```
## Register the module  

The last step in the process is to apply the `SampleScorable` to the bot's Conversation Container. This will register the scorable service within the Bot Framework's message handling pipeline. The following code shows to update the `Conversation.Container` within the bot app's initialization in **Global.asax.cs**:

```cs
public class WebApiApplication : System.Web.HttpApplication
{
    protected void Application_Start()
    {
        this.RegisterBotModules();
        GlobalConfiguration.Configure(WebApiConfig.Register);
    }

    private void RegisterBotModules()
    {
        var builder = new ContainerBuilder();
        builder.RegisterModule(new ReflectionSurrogateModule());

        //Register the module within the Conversation container
        builder.RegisterModule<GlobalMessageHandlersBotModule>();

        builder.Update(Conversation.Container);
    }
}
```

## Additional resources
* [Global Message Handlers sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-GlobalMessageHandlers)
* [Simple Scorable Bot sample](https://github.com/Microsoft/BotFramework-Samples/tree/master/blog-samples/CSharp/ScorableBotSample)
* [Dialogs overview](bot-builder-dotnet-dialogs.md)
* [AutoFac](https://autofac.org/)

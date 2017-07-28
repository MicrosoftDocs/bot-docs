---
title: Global message handlers using Scorables 
description: Create more flexible dialogs using scorables within the  Bot Builder SDK for .NET.
author: matthewshim-ms
ms.author: v-shimma
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/12/2017
---
# Global message handlers using Scorables

Within the Bot Framework .NET SDK, `Scorables` allow you to create dialogs as flexible components which act as global message handlers, in lieu of the traditional dialog model. 

Scorable dialogs within a bot monitor all incoming messages, and determine wether or not that message is actionable in some way. Messages that are scorable are assigned a score between 0 - 1 (one being the highest) by each scorable dialog in the bot. The scorable dialog which determines the highest score then handles the response to the user. 

When a user triggers a scorable dialog, that dialogue is then added to the top of the [dialog stack](../bot-design-conversation-flow.md#dialog-stack). After the scorable dialog is resolved, the conversation will continue from where it left off. This allows you to create more flexible conversations by allowing your users to 'interrupt' the normal hierarchal dialog structure. 

>[!NOTE]
> Scorables is one of the ways that the .NET SDK uses [AutoFac](https://autofac.org/). AutoFac is used in the .NET BotBuilder SDK 
> for [inversion of control and dependency injection](https://martinfowler.com/articles/injection.html). You can learn more about
> Autofac in this [quick start guide](http://autofac.readthedocs.io/en/latest/getting-started/index.html). 

## Creating a Scorable dialog

First, you can define a new [dialog](bot-builder-dotnet-dialogs.md). If you've created a bot before, the following example will look very familiar, it is a typical dialog which uses the `IDialog` interface. 

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
To make a dialog scorable, you create a class which provides implementation of the abstract `ScorableBase` class. The `ScorableBase` class implements the `IScorable` interface that's defined in the Scorables class in the Bot Builder SDK for .NET. 

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

The **ScorableBase** abstract class uses the **IScorable** interface which requires that this class must include the following methods: 

```cs
public interface IScorable<in Item, out Score>
{
    Task<object> PrepareAsync(Item item, CancellationToken token);

    bool HasScore(Item item, object state);

    Score GetScore(Item item, object state);

    Task PostAsync(Item item, object state, CancellationToken token);

    Task DoneAsync(Item item, object state, CancellationToken token);
}
```
* **PrepareAsync**: This first method in the scorable instance accepts incoming message activity, analyzes and sets the dialog's `state`, which is passed to all the other methods in the interface.

* **HasScore**: This boolean method checks the state property to determine if the Scorable Dialog should provide a score for the message. If it returns false, the message will be ignored by the Scorable dialog.  

* **GetScore**: Will only trigger if `HasScore` returns true, provision the logic here to determine the score for a message between 0 - 1.0  

* **PostAsync**: In this method you define core actions to be performed for the scorable class. All scorable dialogs will monitor all incoming messages, and assign scores to valid messages based on the scorables' `GetScore` method. The scorable class which determines the highest score (between 0 - 1.0) will then trigger that scorable's `PostAsync` method. 

* **DoneAsync**: Here you should dispose of any scoped resources as it is called when the scoring process has completed.

## Create a module to register the IScorable service 

Define a `Module` which will register the `SampleScorable` as a component which provisions the `IScorable` service. 

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
## Register the module with 

Lastly, you need to apply the `SampleScorable` to the bot's Conversation Container. This will register the scorable service within the bot builder framework's message handling pipeline. 

Simply update the `Conversation.Container` as shown below within the bot app's initialization in **Global.asax.cs** as shown: 

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
* [Simple Scorable Bot sample](https://github.com/Microsoft/BotFramework-Samples/tree/master/CSharp/ScorableBotSample)
* [Dialogs overview](bot-builder-dotnet-dialogs.md)
* [AutoFac](https://autofac.org/)

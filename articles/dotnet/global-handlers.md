---
title: Implement global message handlers | Microsoft Docs
description:  Teach your bot to listen for and handle user input containing certain keywords using the Bot Builder SDK for .NET.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 02/21/2017
ms.reviewer:
ROBOTS: Index, Follow
---

# Implement global message handlers

<!--
> [!div class="op_single_selector"]
> * [.NET](~/dotnet/howto-global-handlers.md)
> * [Node.js](~/nodejs/global-handlers.md)
>
-->

[!include[Introduction to global message handlers](~/includes/snippet-global-handlers-intro.md)]

The following walk through shows how to implement global message handlers by using the Bot Builder SDK for .NET.

First, `Global.asax.cs` registers `GlobalMessageHandlersBotModule`, which is implemented as shown here. 
In this example, the module registers two scorables: one for managing a request to change settings (`SettingsScorable`) 
and another for managing a request to cancel (`CancelScoreable`).

```cs
public class GlobalMessageHandlersBotModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        base.Load(builder);

        builder
            .Register(c => new SettingsScorable(c.Resolve<IDialogTask>()))
            .As<IScorable<IActivity, double>>()
            .InstancePerLifetimeScope();

        builder
            .Register(c => new CancelScorable(c.Resolve<IDialogTask>()))
            .As<IScorable<IActivity, double>>()
            .InstancePerLifetimeScope();
    }
}
```

The `CancelScorable` contains a `PrepareAsync` method that defines the trigger: 
if the message text is "cancel", this scorable will be triggered.

```cs
protected override async Task<string> PrepareAsync(IActivity activity, CancellationToken token)
{
    var message = activity as IMessageActivity;
    if (message != null && !string.IsNullOrWhiteSpace(message.Text))
    {
        if (message.Text.Equals("cancel", StringComparison.InvariantCultureIgnoreCase))
        {
            return message.Text;
        }
    }
    return null;
}
```

When a "cancel" request is received, the `PostAsync` method within `CancelScoreable` 
resets the dialog stack. 

```cs
protected override async Task PostAsync(IActivity item, string state, CancellationToken token)
{
    this.task.Reset();
}
```

When a "change settings" request is received, the `PostAsync` method within `SettingsScorable` 
invokes the `SettingsDialog` (passing the request to that dialog), thereby adding the `SettingsDialog` 
to the top of the dialog stack and putting it in control of the conversation.

```cs
protected override async Task PostAsync(IActivity item, string state, CancellationToken token)
{
    var message = item as IMessageActivity;
    if (message != null)
    {
        var settingsDialog = new SettingsDialog();
        var interruption = settingsDialog.Void<object, IMessageActivity>();
        this.task.Call(interruption, null);
        await this.task.PollAsync(token);
    }
}
```

## Additional resources

- [Designing conversation flow](~/bot-design-conversation-flow.md)
- [Builder library][builderLibrary]

[builderLibrary]: https://docs.botframework.com/en-us/csharp/builder/sdkreference/d3/ddb/namespace_microsoft_1_1_bot_1_1_builder.html

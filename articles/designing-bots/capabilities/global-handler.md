---
title: Designing Bots - Global Handlers | Bot Framework
description: Global Message Handlers - Preparing bots for when users change topics in the conversation 
services: Bot Framework
documentationcenter: BotFramework-Docs
author: matvelloso
manager: larar

ms.service: required
ms.devlang: may be required
ms.topic: article
ms.tgt_pltfrm: may be required
ms.workload: required
ms.date: 01/19/2017
ms.author: mat.velloso@microsoft.com

---
# Bot Design Center - Global Handlers


##When users change the topic of the conversation

We discussed [earlier on the topic of dialogs](../core/dialogs#dialogs-stacks-and-humans) about the challenges of detecting when the user doesn't answer what the bot is expecting but instead asks for something else, outside the flow.

![how users talk](../../media/designing-bots/capabilities/trigger-actions.png)


In the scenario above, the bot had a simple yes/no question. That dialog is not expecting for any answer other than that. But suddenly the user switches topics or asks for more help. That "help" message could have happened anywhere along the conversation, in any dialog. How do we build a "catch all" handler for it?

##Scorables and triggerActions

The C# and Node libraries differ on how these global message handlers can be implemented today. We will discuss how each does it.

First, in C#. The Global.asax.cs registers a GlobalMessageHandlersBotModule, implemented as below:

	public class GlobalMessageHandlersBotModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register(c => new SettingsScorable(c.Resolve<IDialogStack>()))
                .As<IScorable<IActivity, double>>()
                .InstancePerLifetimeScope();

            builder
                .Register(c => new CancelScorable(c.Resolve<IDialogStack>()))
                .As<IScorable<IActivity, double>>()
                .InstancePerLifetimeScope();
        }
    }

This module registers two scorables, one for managing the request for changing settings and another for managing the requests to cancel.

The CancelScorable has a PrepareAsync method where the trigger is defined. Here we are saying that if the message text is "cancel", this scorable will be triggered:

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

In this particular scenario, we want "cancel" to cause the dialog stack to be reset:

	protected override async Task PostAsync(IActivity item, string state, CancellationToken token)
    {
    	this.stack.Reset();
    }

Note this is different than the SettingsScorable, which directs the command to a different Dialog:

	protected override async Task PostAsync(IActivity item, string state, CancellationToken token)
    {
    	var message = item as IMessageActivity;
        if (message != null)
        {
        	var settingsDialog = new SettingsDialog();
            var interruption = settingsDialog.Void<object, IMessageActivity>();
            this.stack.Call(interruption, null);
            await this.stack.PollAsync(token);
        }
	}

So in this second scenario we don't reset the dialog stack but instead just add the SettingsDialog on top of it. 

This is the essence of how global handlers can be wired up in C#.

Now let us see the case with Node:

On Node we will be using triggerActions in order to tell the framework what are the triggers that will lead to the activation of certain dialogs.

For example, for the help dialog:

	// global help
	bot.dialog('help', [
    	(session, args, next) => {
        	// args.action is the name of the action being called
        	// this is a very useful technique to centralize logic
        	switch(args.action) {
            	default:
            	    // no action, provide default help message
            	    session.endDialog(`I'm a simple calculator bot. I can add numbers if you type "add".`);
            	case 'addHelp':
            	    // addHelp action. Provide help for add
            	    session.endDialog('Adds numbers. You can type "help" to get this message or "total" to see the total and start over.');
        	}
    	}
	]).triggerAction({ 
    	// registered to respond globally to the word "help"
    	matches: /^help/,
    	onSelectAction: (session, args, next) => {
    	    // By default, the flow is interrupted and dialog stack is reset
    	    // This allows us to push a new dialog onto the stack and resume
    	    session.beginDialog('help', args);
    	}
	});

Note how the dialog has a triggerAction that defines that if the user says 'help', the help dialog will be activated.



##Show me the code!

Follow [this link for a complete implementation in C#](https://trpp24botsamples.visualstudio.com/_git/Code?path=%2FCSharp%2Fcore-GlobalMessageHandlers&version=GBmaster&_a=contents)

Follow this link for a complete implementation in Node ( TODO )


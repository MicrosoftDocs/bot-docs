---
title: Designing Bots - Message Logging | Bot Framework
description: Message logging - Basic concepts around intercepting messages between bots and users
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
# Bot Design Center - Message Logging


##We don't keep data, but you may


It is a common scenario for bot developers to save conversation logs in some sort of store. This is not done by default in the Microsoft Bot Framework and there is a good reason: In the conversation between the user and the bot, very personal and sensitive details about the user may be included and automatically saving that could have implications, for example around privacy.

Therefore it is up to the bot developer to make the decision whether to store that data and how, and also to communicate this to their users.

This article discusses how we can intercept all messages between bots and users so we can not only save them but also inspect them globally if needed.

##The middleware


The Bot Builder SDK offers a concept named *middleware*, which addresses the need of intercepting all messages so we can run tasks such as logging. 

How to set it up in C#:

	//Global.asax.cs code:
	public class WebApiApplication : System.Web.HttpApplication
	{
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
			//Node: Here is where we hook up our DebugActivityLogger
            builder.RegisterType<DebugActivityLogger>().AsImplementedInterfaces().InstancePerDependency();
            builder.Update(Conversation.Container);

            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }

	//DebugActivityLogger.cs code
	public class DebugActivityLogger : IActivityLogger
    {
        public async Task LogAsync(IActivity activity)
        {
            Debug.WriteLine($"From:{activity.From.Id} - To:{activity.Recipient.Id} - Message:{activity.AsMessageActivity()?.Text}");
        }
    }

This is all that it is needed: We start by registering with the bot builder our created activity logger class and implementing it from the IActivityLogger. From that point, every message being sent or received to/from the user will trigger the LogAsync method and from there we can use whatever mechanism needed to store/inspect these.

Now in Node:


	server.post('/api/messages', connector.listen());
	var bot = new builder.UniversalBot(connector);
	// Middleware for logging
	bot.use({
    	botbuilder: function (session, next) {
        	myMiddleware.logIncomingMessage(session, next);
    	},
    	send: function (event, next) {
        	myMiddleware.logOutgoingMessage(event, next);
    	}
	})

We start by setting up handlers for incoming (botbuilder) and outgoing (send) handlers, and then implement them:

	module.exports = {
    	logIncomingMessage: function (session, next) {
        	console.log(session.message.text);
        	next();
    	},
    	logOutgoingMessage: function (event, next) {
        	console.log(event.text);
        	next();
    	}
	}

##Show me the code!

You can read [the detailed readme here](https://trpp24botsamples.visualstudio.com/_git/Code?path=%2FNode%2Fcapability-middlewareLogging%2FREADME.md&version=GBmaster&_a=contents) and see the [full C# code mentioned above here](https://trpp24botsamples.visualstudio.com/_git/Code?path=%2FCSharp%2Fcore-Middleware&version=GBmaster&_a=contents) and [the full Node sample here](https://trpp24botsamples.visualstudio.com/_git/Code?path=%2FNode%2Fcapability-middlewareLogging&version=GBmaster&_a=contents).



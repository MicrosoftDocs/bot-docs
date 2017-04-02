---
title: Bot Framework dialogs | Microsoft Docs
description: Learn about dialogs in the Bot Framework.
keywords: Bot Framework, dialog, core concept, bot
author: DeniseMak
manager: rstand
ms.topic: key-concepts-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/14/2017
ms.reviewer:
#ROBOTS: Index
---

# Dialogs
> [!NOTE]
> This content will be updated. 

<!-- -->
Dialogs provide the UI for your bot, just as screens provide the UI for a traditional app or web page. 
Dialogs may or may not have graphical interfaces. They may have buttons, text or they may even be completely speech based. One dialog can call another, just like screens in apps.


Dialogs let you break your bots logic into logical components designed to perform a single task, and they are composable for reuse within your bot and from other bots. 
The Bing Location Control is an example of a community-supplied pre-built dialog that your bot can use.

<!-- ## What the Bot Framework provides -->
<!-- Here's where you talk about which classes to use and point to further info -->
Whether you are using .NET / C#, Node.js or a REST API, the SDK supplies Dialog classes to model the conversations your bot can have with the outside world.  

The different types of Dialog you can create include the following:
* Yes / No prompts
* Accepting a simple string
* Selecting a choice from a number of options (and even displaying the options as buttons in supported clients, such as Facebook and Slack)
* Model a multi-stage, back-and-forth conversation. <!-- FormFlow for C# --> <!-- multi-turn, form completion, like missing all the info for an airline ticket -->

<!-- The Bot Buider SDK also implements logic for disambiguation --> 
<!-- In C#, each dialog implements the IDialog class.
In Node.JS, not every dialog is a class. They are named. -->
<!-- Summarize subsections in bullet points here -->

## Core Principles

Dialogs can be *root dialogs* or _child dialogs_. The root dialog is where the UI for your bot begins. If your root dialog doesn’t provide logic to handle some case of user input,
it can call a child dialog that handles those responses. A root dialog and its series of child dialogs are composed together to handle all the input for one stage of a bot's conversation flow.<!-- like greetings or goodbyes to check if there are any general responses it should be providing.-->

When one dialog invokes another, the new dialog is added to the *stack*. When a dialog finishes, it's removed from the stack.
The Bot Builder SDK provides methods for accessing the stack, which tracks the order in which dialogs invoke other dialogs.

This conversation state (the stack of active dialogs and each dialog's state) is stored in the [state service][BotStateCS] provided by the Bot Connector service, 
making the bot implementation stateless between requests. This is much like a web application that does not store session state in the web server's memory. 
For examples of how to access the bot's state see [Managing State]

Sometimes, users say something unexpected that isn't handled in the current dialog of the stack. The bot needs to make sure 
that a user doesn't get lost in a conversation. Can a user navigate "back" in a chat? How does the bot return to the "main menu"? How do you "cancel" an operation?
There are two ways to handle this: [Navigation][DesigningNavigation] <!-- link to Navigation --> and [Global message handlers][GlobalMessageHandlers].
<!--
* Dialogs can contain rich cards or controls or text
--> 

<!--
## What is a dialog? (or Introductory Concepts or, Principles, or "what do you need to know first?" "What are capabilities")
What do you need to know about this concept (1) before you dive into designing your bot (2) development? 
The second point probably primarily goes under Implementing, but there are general things about 
the capabilities of an SDK or a service you'll need to know before designing a bot as well. 
For example, if you want an app with a SMS interface you want to know about card normalization

## How does a dialog relate to other parts of the BotFramework?
-->




## Implementation basics
<!-- Design principes? -->
<!-- Programmatically, what is it? General discussion of development patterns and pointers to Getting Started and development
 For example, a dialog can be thought of as a message loop that just waits for input and responds -->
This section outlines basic steps for using dialogs in your bot and provides pointers to more information.

### Invoking the root dialog
Your bot starts by invoking a root dialog that accepts the first incoming message. To invoke the root dialog, you connect the incoming HTTP POST call to an object that accepts input and sends application state updates. <!-- similar to MVC model -->

In Node:

	var server = restify.createServer();
	server.listen(process.env.port || process.env.PORT || 3978, function () { });

	var connector = new builder.ChatConnector({
    	appId: process.env.MICROSOFT_APP_ID,
		appPassword: process.env.MICROSOFT_APP_PASSWORD
	});

	var bot = new builder.UniversalBot(connector);
	server.post('/api/messages', connector.listen());

	// Root dialog
	bot.dialog('/', ...

In C#:

	public class MessagesController : ApiController
	{
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
				//controller redirects to RootDialog
                await Conversation.SendAsync(activity, () => new RootDialog()); 

### Invoking a child dialog
To add a 'New Order' dialog to the bot, we can start with the code above and add the following.

In Node:

	bot.dialog('/', new builder.IntentDialog()
	//Did the user type 'order'?
    .matchesAny([/order/i], [ 
        function (session) {
			//Let's invoke then the new order dialog
            session.beginDialog('/newOrder');
        },

        function (session, result) {
			//This will get us whatever the new order dialog decided to return to us
			var resultFromNewOrder = result.response;

            session.send('New order dialog just told me this: %s', resultFromNewOrder');
            //We are now done with the root dialog
			session.endDialog(); 
        }
    ])

In C#:


    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
			//Root Dialog initiates and now waits for the next message from the user. 
			//When that arrives we will fall into MessageReceivedAsync
            context.Wait(this.MessageReceivedAsync); 
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result; //We've got a message!
            if (message.Text.ToLower().Contains("order"))
            {
				//User said 'order'. Let's invoke the New Order Dialog and wait for it to finish
				//Then, we will call the ResumeAfterNewOrderDialog
                await context.Forward(new NewOrderDialog(), this.ResumeAfterNewOrderDialog, message, CancellationToken.None);
            }
			//User typed something else so for simplicity we will just ignore 
			//and keep waiting for the next message
            context.Wait(this.MessageReceivedAsync);
        }


        private async Task ResumeAfterNewOrderDialog(IDialogContext context, IAwaitable<string> result)
        {
			//This will get us whatever the NewOrderDialog decided to return to us. 
			//At this point, new order dialog finished and gave us back some value to work with
			//on the root dialog
            var resultFromNewOrder = await result;

            await context.PostAsync($"New order dialog just told me this: {resultFromNewOrder}");

			//Again, we will now just wait for the next message from the user
            context.Wait(this.MessageReceivedAsync);

        }


<!-- 
####  Waiting for input -->
Programmatically, a dialog can be thought of as a message loop that waits for user input and responds.
Once a dialog is invoked, it will be in control of the flow. Every new message will automatically fall again at that dialog until it tells us it is done. 
In C# you control that by saying context.Wait(), to set up the callback that is invoked the next time the user sends a message. 
In fact, in C# you must always finish the code either with context.Wait(), context.Fail() or some new redirection such as context.Forward() or context.Call(). 
Not doing so will cause an error, because your code is confusing the framework by not telling it what it should do the next time the user sends us a message.
<!-- revise this -->
<!-- 
All IDialog methods should complete with IDialogStack.Call, IDialogStack.Wait, or IDialogStack.Done. 
These IDialogStack Call/Wait/Done methods are exposed through the IDialogContext passed to every IDialog method. 
Calling IDialogStack.Forward and using the system prompts through the PromptDialog static methods will call one of these methods in their implementation. -->

In Node, these flows have a little more automation built in. 
A dialog invokes another by doing session.beginDialog(). And when a dialog is "done", it tells us by saying session.endDialog(). 
So session.endDialog() in Node is similar to context.Done() in C#. They basically remove the dialog from the stack, sending the user to whatever dialog is now on top of that stack.

### Invoking a global message handler

It can be a challenge to detect when the user doesn't answer what the bot is expecting but instead asks for something else, outside the flow. 
A global message handler can handle those unexpected messages so that your dialog doesn't have to implement specific logic for them.
For example, we can design a ‘help’ dialog to provide the user with help anytime they ask for it.
The global message handler (triggerAction in Node.js) defines the rules for when this dialog should be started, in this case anytime the user says “help”.

    // Create your bot with a function to echo back messages from the user
    var bot = new builder.UniversalBot(connector, function (session) {
        // Echo back user's text
        session.send("You said: %s", session.message.text);
        
    });

    // Add help dialog
    bot.dialog('help', function (session) {
        session.send("I'll repeat back anything you say or send.").endDialog();
    }).triggerAction({ matches: /^help/i });

For a more detailed example of global message handlers in C# and Node.js, see [Global Message Handlers][GlobalMessageHandlers].

### Send Cards with buttons
Several chat clients, like Skype & Facebook, support sending rich graphical cards with interactive buttons that users click to initiate some action. 
The Bot Builder SDK provides a rich set of message and card builder classes which can be used to send cards. The Bot Framework Service will render these cards using 
the schema native to the channel they are sent to. For channels that don’t support cards, like SMS, the Bot Framework will do its best to render a reasonable experience to users. 

For a code example of a dialog containing buttons, see Send a card with buttons.<!-- [Send a card with buttons][ButtonsExample]. -->

### Track conversation state
For an example of how to track conversation state, see Managing state.

### Integrate LUIS Intents into your dialog
For an example of how to use LUIS to intelligently parse user input and extract entities in your dialog, see LUIS integration.

## Next Steps
> [!NOTE]
> TODO: Add more links as they become available. 

Before you get started designing dialogs for your bot, read the [Design Guide](~/design/core-dialogs.md)


<!-- reference-style links -->
[GettingStartedNodeJS]:(~/nodejs/getstarted.md)
[ButtonsControlsDesign]:(https://review.docs.microsoft.com/en-us/botframework/~/design/core-ux-elements#buttons-language-and-speech)
[LocationControl]:(https://github.com/Microsoft/BotBuilder-Location)

[DesigningNavigation]:(https://review.docs.microsoft.com/en-us/botframework/~/design/core-navigation)
[GlobalMessageHandlers]:(https://review.docs.microsoft.com/en-us/botframework/~/design/capabilities#global-message-handlers)
[MessageLoggingMiddleware]:(https://review.docs.microsoft.com/en-us/botframework/~/design/capabilities#message-logging)
[BotStateCS]:(https://docs.botframework.com/en-us/csharp/builder/sdkreference/stateapi.html)
[Downloads]:(bot-framework-downloads.md)
[ButtonsExample]:(bot-framework-send-card-buttons.md)


---
title: Designing Bots - Dialogs | Bot Framework
description: Dialogs - Fundamental concepts of working with dialogs in the Microsoft Bot Framework
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
# Bot Design Center - Dialogs


##UI: Apps have them, so do bots 

In a traditional application, UI is represented as screens. A single app can use one or more screens as needed in order to exchange data with the user. You would likely have a main screen where all navigation starts from and then different screens for, let’s say, browsing products, looking for help and so on. Same principles apply to websites.

Again, bots are no different. You will have a UI. It just may look… different. As your bot grows in complexity, you will need to separate concerns. The place where you help the user browse for products will be different than the place where the user creates a new order or browses for help. We break those things into what we call “dialogs”. 

Dialogs may or may not have graphical interfaces. They may have buttons, text or they may even be completely speech based. One dialog can call another, just like screens in apps.

![bot](../../media/designing-bots/core/dialogs-screens.png)

Developers are used with the concept of screens that "stack" on top of each other: The "main screen" invokes the "new order screen". At that point, "new order screen" takes over, as if it was on top of the main screen. The experience now is controlled by "new order screen" until it is done. It may even call other screens, which will then take over as well. But at some point, "new order screen" will be done with whatever it had to do so it will close, sending the user back to the main screen.

Let us switch to bots now: Our controller invokes the "main screen", which in this case we typically call the "root dialog".

In other words, in C#:


	public class MessagesController : ApiController
	{
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
				//controller redirects to RootDialog
                await Conversation.SendAsync(activity, () => new RootDialog()); 


And in Node:

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

Despite of the obvious differences due to the nature of every language, both snippets above show how we wire the basic HTTP GET call to a controller and then hook it to our root dialog.

Then, from root dialog we can invoke the New Order dialog:

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

Now in Node:

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


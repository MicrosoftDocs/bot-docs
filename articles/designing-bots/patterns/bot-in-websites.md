---
title: Designing Bots - Bot in Websites | Bot Framework
description: Embedding bots into websites
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
# Bot Design Center - Bots in Websites 



##When bots live in a website 


A very common scenario for bots is to have their user interface embedded into websites. There are many reasons companies may want to do that. For example, the bots can offer a quick way of finding resources that would otherwise not be so easily found in complex website structures by using [concepts discussed in the knowledge base patterns](./kb.md). They may also become first points of interaction for finding solutions to a problem or even handing off a conversation to a help desk agent, using the [hand off patterns](./human-handoff.md).

Microsoft offers both a Skype web control and an open source web control as options for such scenarios.

##Skype web control

Skype web control is essentially a Skype client, embedded as web. Some of the key benefits of this control are:

- It offers built in Skype authentication. In other words, the developer does not need to wire any additional code to authenticate and recognize users. Skype will automatically recognize Microsoft Accounts used in its web client
- Since this is effectively another front end to Skype, once the user starts chatting with a bot on a web page, the bot is also added to the user's Skype client without any additional steps and can continue interacting withe the user after when the web browser is closed. In fact, even the context of the conversation, its state and dialogs are all kept across

##Open source web control

The open source web control gives developers full control over the user experience: They can basically change anything in any way they want, since by the end of the day this is just a canvas based on ReactJS that relies on the Bot Framework's DirectLine to interact with the Bot Framework service.

The benefits of the open source web control

- Full control over the user experience and behaviors. Anything the developer doesn't like can be coded differently (which also leads to more effort form the developer's side)
- Back-channel: The open source web control offers a mechanism in which the bot can send an "event message", which is a message not meant for the user to see, but for the JavaScript code to acknowledge as an event. For example, the bot may want to change the web page's color, so it sends an "event" message asking the page's JavaScript to do it. The page then executes the change. The page may also want to send an event notification to the bot. For example, the page may want to inform the bot that the user has clicked at some button, so it generates a message of the type "event" with this information and use the web control to notify the bot. This gives the bot full awareness and control over the web page.

##Back-channel pattern


The flow for back-channel can use either or both directions: Bot to client and/or client to bot:

![Back-channel](../../media/designing-bots/patterns/back-channel.png)


The steps are:


1. Bot wants to send a system notification to the JavaScript at the page. So bot creates a message of the type "event" and sends it.
2. Web control receives the "event" message and instead of rendering it, the message is just forwarded to the host page by invoking a JavaScript event. The JavaScript then decides what it wants to do with the message and its content.
3. The client may also or instead want to send a message to the bot web service. In this case, the client JavaScript creates a message of type "event" and requests the web control to relay it to the bo.
4. The bot then receives a message just like it would receive a message from the user, but in this case the type of the message is "event" so it can differentiate whether it actually came from the user or from the JavaScript code


##Code

We suggest to start by looking at the [Web Control GitHub repo](https://github.com/Microsoft/BotFramework-WebChat) and familiarizing with the [sample for hosting it](https://github.com/Microsoft/BotFramework-WebChat/blob/master/samples/index.html).

We have an end to end sample of the back channel as well in [this GitHub repo](https://github.com/ryanvolum/backChannelBot).

Client side:

The sample above notifies the bot upon the click of a button on the web page. This is done via a simple JavaScript code:

	const postButtonMessage = () => {
		botConnection
			.postActivity({type: "event", value: "", from: {id: "me" }, name: "buttonClicked"})
            .subscribe(id => console.log("success"));
        }

Note the creation of a message of type "event" and how it is sent with postActivity. Also note that the content of the message which can be anything defined by the developer.

Also note how the client JavaScript listens for events from the bot:

	botConnection.activity$
		.filter(activity => activity.type === "event" && activity.name === "changeBackground")
		.subscribe(activity => changeBackgroundColor(activity.value))

The bot, in this example, can request the page to change the background color via a specific event type message with the content "changeBackground".

Server side:

The bot code, in Node, basically creates a message and defines it as of being the type "event":

	bot.dialog('/', [
    	function (session) {
        	var reply = createEvent("changeBackground", session.message.text, session.message.address);
        	session.endDialog(reply);
    	}
	]);

Likewise, the bot service also listens for "event" messages from the client:

	bot.on("event", function (event) {
	    var msg = new builder.Message().address(event.address);
	    msg.data.textLocale = "en-us";
	    if (event.name === "buttonClicked") {
	        msg.data.text = "I see that you just pushed that button";
	    }
	    bot.send(msg);
	})

And this completes the flow. Essentially the back-channel allows client and server exchange any data needed, from requesting the client's timezone to reading a GPS location or what the user is doing on a web page. The bot can even "guide" the user by automatically filling out parts of a form and so on. The back-channel closes the gap between client JavaScript and bots.


##Show me examples!


- The open source web control is located [this GitHub repo](https://github.com/Microsoft/BotFramework-WebChat) 
- The back-channel sample discussed above is located in [this GitHub repo](https://github.com/ryanvolum/backChannelBot).
 
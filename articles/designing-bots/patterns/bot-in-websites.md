---
title: Designing Bots - Bot in Websites | Microsoft Docs
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

The benefits of the open source web control:

- Full control over the user experience and behaviors. Anything the developer doesn't like can be coded differently (which also leads to more effort form the developer's side)
- Backchannel: a mechanism by which the web page hosting the control can communicate directly with the bot, invisible to the user. This enables a number of useful scenarios:
    - The web page can send relevant data to the bot, e.g. GPS location
	- The web page can advise the bot about user actions ("user just clicked on a dropdown")
	- The web page can send the auth token for a logged-in user to the bot
	- The bot can send relevant data to the web page, e.g. current value of user's portfolio
	- The bot can send "commands" to the web page, e.g. change background color

##Backchannel pattern

The WebChat control communicates with bots via the Direct Line API.

The Direct Line API allows "activities" to be sent between the client and the bot. The most common activity type is "message", but there are others, e.g. "typing" which indicates that a user is typing or the bot is thinking. The Bot Framework supports a general-purpose activity called "event", which the WebChat UI is guaranteed to ignore.

WebChat accesses Direct Line using a JavaScript class called [DirectLine](https://github.com/microsoft/botframework-directlinejs). WebChat can either create its own instance of DirectLine, or it can share one with the hosting page. In the shared case, WebChat and/or the page can send and/or receive activities. If they are "event" activities, WebChat will not display them. This is how the backchannel works.

![Backchannel](../../media/designing-bots/patterns/back-channel.png)

##Code

Start by looking at the [Web Control GitHub repo](https://github.com/Microsoft/BotFramework-WebChat) and familiarize yourself with the [backchannel sample](https://github.com/Microsoft/BotFramework-WebChat/blob/master/samples/backchannel/index.html).

Client side:

In the sample above, the web page creates a DirectLine object:

	var botConnection = new BotChat.DirectLine(...);

It shares this when creating the WebChat instance:

	BotChat.App({
		botConnection: botConnection,
		user: user,
		bot: bot
	}, document.getElementById("BotChatGoesHere"));

It notifies the bot upon the click of a button on the web page:

	const postButtonMessage = () => {
		botConnection
			.postActivity({type: "event", value: "", from: {id: "me" }, name: "buttonClicked"})
            .subscribe(id => console.log("success"));
        }

Note the creation of an activity of type 'event' and how it is sent with `postActivity`. Also note that the `name` and `value` of the event can be anything defined by the developer. It is simply a contract between the web page and the bot.

The client JavaScript also listens for a specific event from the bot:

	botConnection.activity$
		.filter(activity => activity.type === "event" && activity.name === "changeBackground")
		.subscribe(activity => changeBackgroundColor(activity.value))

The bot, in this example, can request the page to change the background color via a specific event type message with the content "changeBackground". The web page can respond to this in any way it wants, including ignoring it. In this case it cooperates by changing the background color as passed in the `value` field of the activity.

Server side:

The [bot code](https://github.com/ryanvolum/backChannelBot), in Node, creates an event using a helper function:

	bot.dialog('/', [
    	function (session) {
        	var reply = createEvent("changeBackground", session.message.text, session.message.address);
        	session.endDialog(reply);
    	}
	]);

	const createEvent = (eventName, value, address) => {
		var msg = new builder.Message().address(address);
		msg.data.type = "event";
		msg.data.name = eventName;
		msg.data.value = value;
		return msg;
	}

Likewise, the bot also listens for events from the client:

	bot.on("event", function (event) {
	    var msg = new builder.Message().address(event.address);
	    msg.data.textLocale = "en-us";
	    if (event.name === "buttonClicked") {
	        msg.data.text = "I see that you just pushed that button";
	    }
	    bot.send(msg);
	})

And this completes the flow. Essentially the backchannel allows client and server to exchange any data needed, from requesting the client's timezone to reading a GPS location or what the user is doing on a web page. The bot can even "guide" the user by automatically filling out parts of a form and so on. The backchannel closes the gap between client JavaScript and bots.

##Show me examples!

- The open source web control is located at [this GitHub repo](https://github.com/Microsoft/BotFramework-WebChat) 
- The backchannel bot sample discussed above is located in [this GitHub repo](https://github.com/ryanvolum/backChannelBot).
 
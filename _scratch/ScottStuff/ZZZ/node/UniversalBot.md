
layout: page
title: UniversalBot
permalink: /en-us/node/builder/chat/UniversalBot/
weight: 2620
parent1: Bot Builder for Node.js
parent2: Chat Bots


* TOC
{:toc}

## Overview
The [UniversalBot](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot.html) class forms the brains of your bot. It's responsible for managing all of the conversations your bot has with a user.  You first initialize your bot with a [connector](#connectors) that connects your bot to either the [Bot Framework](http://www.botframework.com){:target="_blank"} or the console.  Next you can configure your bot with [dialogs]( /en-us/node/builder/chat/dialogs/) that implement the actual conversation logic for your bot.

> __NOTE:__ For users of Bot Builder v1.x the [BotConnectorBot](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.botconnectorbot), [TextBot](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.textbot), and [SkypeBot](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.skypebot) class have been deprecated. They will continue to function in most cases but developers are encouraged to migrate to the new UniversalBot class at their earliest convenience.  The old [SlackBot](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.slackbot) class has been removed from the SDK and unfortunately, at this time the only option is for developers to migrate their native Slack bots to the [Bot Framework](http://www.botframework.com){:target="_blank"}.

## Connectors
The UniversalBot class supports an extensible connector system the lets you configure the bot to receive messages & events and sources. Out of the box, Bot Builder includes a [ChatConnector](#chatconnector) class for connecting to the [Bot Framework](http://www.botframework.com){:target="_blank"} and a [ConsoleConnector](#consoleconnector) class for interacting with a bot from a console window. 

### ChatConnector
The [ChatConnector](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.chatconnector) class configures the UniversalBot to communicate with either the [emulator](/en-us/tools/bot-framework-emulator/) or any of the channels supported by the [Bot Framework](http://www.botframework.com){:target="_blank"}. Below is an example of a "hello world" bot that's configured to use the ChatConnector:

{% highlight JavaScript %}
var restify = require('restify');
var builder = require('botbuilder');

//=========================================================
// Bot Setup
//=========================================================

// Setup Restify Server
var server = restify.createServer();
server.listen(process.env.port || process.env.PORT || 3978, function () {
   console.log('%s listening to %s', server.name, server.url); 
});
  
// Create chat bot
var connector = new builder.ChatConnector({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});
var bot = new builder.UniversalBot(connector);
server.post('/api/messages', connector.listen());

//=========================================================
// Bots Dialogs
//=========================================================

bot.dialog('/', function (session) {
    session.send("Hello World");
});
{% endhighlight %}

The [appId](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ichatconnectorsettings#appid) & [appPassword](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ichatconnectorsettings#apppassword) settings are generally required and will be generated when registering your bot in the [developer portal](http://www.botframework.com){:target="_blank"}. The one exception to that rule is when running locally against the emulator. When you're first developing your bot you can leave the “Microsoft App Id” & “Microsoft App Password” blank in the emulator and no security will be enforced between the bot and emulator.  When deployed, however, these values are required for proper operation.

The example is coded to retrieve its appId & appPassword settings from environment variables. This sets up the bot to support storing these values in a config file when deployed to a hosting service like [Microsoft Azure](https://azure.microsoft.com). When testing your bots security settings locally you'll need to either manually set the environment variables in the console window you're using to run your bot or if you're using VSCode you can add them to the “env” section of your [launch.json](https://code.visualstudio.com/Docs/editor/debugging#_launch-configurations) file.

### ConsoleConnector
The [ConsoleConnector](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.consoleconnector) class configures the UniversalBot to interact with the user via the console window.  This connector is primarily useful for quick testing of a bot or for testing on a Mac where you can’t easily run the emulator.  Below is an example of a “hello world” bot that’s configured to use the ConsoleConnector:

{% highlight JavaScript %}
var builder = require('botbuilder');

var connector = new builder.ConsoleConnector().listen();
var bot = new builder.UniversalBot(connector);
bot.dialog('/', function (session) {
   session.send('Hello World'); 
});
{% endhighlight %}

If you’re debugging your bot using VSCode you’ll want to start your bot using a command similar to node `--debug-brk app.js` and then you’ll want to start the debugger using [attach mode](https://code.visualstudio.com/docs/editor/debugging#_node-debugging).

## Proactive Messaging
The SDK supports two modes of message delivery. 

* __Reactive Messages__  are messages your bot sends in response to a message received from the user. This is the most common message delivery mode and can be achieved using the [session.send()](https://docs.botframework.com/en-us/node/builder/chat/session/#sending-messages) method. 
* __Proactive Messages__ are messages  your bot sends in response to some sort of external event like a timer firing or a notification being triggered.  Good examples of proactive messages are a notification that an item has shipped or a daily poll asking team members for their status.  

### Saving Users Address
The `UniversalBot` class exposes [bot.send()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot#send) and [bot.beginDialog()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot#begindialog) methods for communicating with a user proactively. Before you can use either method you will need to save the [address](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iaddress) of the user you wish to communicate with. You can do that by serializing the [session.message.address](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage#address) property to a string which you then store for future use:

{% highlight JavaScript %}
bot.dialog('/createSubscription', function (session, args) {
    // Serialize users address to a string.
    var address = JSON.stringify(session.message.address);

    // Save subscription with address to storage.
    session.sendTyping();
    createSubscription(args.userId, address, function (err) {
        // Notify the user of success or failure and end the dialog.
        var reply = err ? 'unable to create subscription.' : 'subscription created';
        session.endDialog(reply);
    }); 
});
{% endhighlight %}

You shouldn't assume that an `address` object for a user will always be valid. Addresses returned by the `ChatConnector` class in particular contain a `serviceUrl` property which can theoretically change and prevent your bot from contacting the user. Because of that you should consider periodically updating the address object stored for a user. 

### Sending Messages
To proactively send a message to a user you’ll need to first add the web hook or other logic that will trigger your proactive notification. In the example below we’ve added a web hook to our bot that lets us trigger the delivery of a notification message to one of our bots users:

{% highlight JavaScript %}
server.post('/api/notify', function (req, res) {
    // Process posted notification
    var address = JSON.parse(req.body.address);
    var notification = req.body.notification;

    // Send notification as a proactive message
    var msg = new builder.Message()
        .address(address)
        .text(notification);
    bot.send(msg, function (err) {
        // Return success/failure
        res.status(err ? 500 : 200);
        res.end();
    });
});
{% endhighlight %}

Within this web hook we’re de-serializing the users address which we previously saved. Then we compose a message for the user and deliver it with [bot.send()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot#send). We can optionally provide a callback to receive the success or failure of the send.

### Starting Conversations
In addition to sending messages proactively you can use [bot.beginDialog()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot#begindialog) to start new conversations with a user. The differences between `bot.send()` and `bot.beginDialog()` are subtle. Calling `bot.send()` with a message won’t affect the state of any existing conversation between the bot & user so it’s generally safe to call at any time. Calling `bot.beginDialog()` will end any existing conversation between the bot & user and start a new conversation using the specified dialog.  As a general rule you should call `bot.send()` if you don’t need to wait for a reply from the user and `bot.beginDialog()` if you do.

Starting a proactive conversation is very similar to sending a proactive message.  In the example below we have a web hook that triggers running a standup with multiple team members. In the web hook we simply loop over all of the team members and call `bot.beginDialog()` with the address of each team member. The bot will then ask the team member their status and roll their answer up into a daily status report:

{% highlight JavaScript %}
server.post('/api/standup', function (req, res) {
    // Get list of team members to run a standup with.
    var members = req.body.members;
    var reportId = req.body.reportId;
    for (var i = 0; i < members.length; i++) {
        // Start standup for the specified team member
        var user = members[i];
        var address = JSON.parse(user.address);
        bot.beginDialog(address, '/standup', { userId: user.id, reportId: reportId });
    }
    res.status(200);
    res.end();
});

bot.dialog('/standup', [
    function (session, args) {
        // Remember the ID of the user and status report
        session.dialogData.userId = args.userId;
        session.dialogData.reportId = args.reportId;

        // Ask user their status
        builder.Prompts.text(session, "What is your status for today?");
    },
    function (session, results) {
        var status = results.response;
        var userId = session.dialogData.userId;
        var reportId = session.dialogData.reportId;

        // Save their repsonse to the daily status report.
        session.sendTyping();
        saveTeamMemberStatus(userId, reportId, status, function (err) {
            if (!err) {
                session.endDialog('Got it... Thanks!');
            } else {
                session.error(err);
            }
        });
    }
]);
{% endhighlight %}

### Proactive Messaging and Localization
For bots that support multiple languages you’ll need to __always__ use `bot.beginDialog()` to communicate with a user.  Thats because the users preferred locale is persisted as part of the [Session](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session) object which is currently only available within a dialog.  Our original notification example can easily be updated to use `bot.beginDialog()` instead of `bot.send()`:

{% highlight JavaScript %}
server.post('/api/notify', function (req, res) {
    // Process posted notification
    var address = JSON.parse(req.body.address);
    var notification = req.body.notification;
    var params = req.body.params;

    // Send notification as a proactive message
    bot.beginDialog(address, '/notify', { msgId: notification, params: params });
    res.status(200);
    res.end();
});

bot.dialog('/notify', function (session, args) {
    // Deliver notification to the user.
    session.endDialog(args.msgId, args.params);
});
{% endhighlight %}

The delivered notification will now use the SDK’s built-in [localization](https://docs.botframework.com/en-us/node/builder/chat/localization/) support to deliver the user a notification in their preferred language. The one disadvantage of using `bot.beginDialog()` over `bot.send()` is that any existing conversation between the bot & user will be ended before sending the user the notification. So for bots that support only a single language, the use of `bot.send()` is still preferred.


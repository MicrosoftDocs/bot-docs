---
layout: page
title: Sending Messages Proactively
permalink: /en-us/node/builder/chat/messaging/
weight: 1125
parent1: Building your Bot Using the Bot Builder for Node.js
parent2: Chat Bots
---

The SDK supports two modes of message delivery: 

* __Reactive Messages__  are messages that your bot sends in response to a message that the bot receives from the user. Most messages that your bot will handle are of this type. To send this type of message, you'd call the [session.send()](https://docs.botframework.com/en-us/node/builder/chat/session/#sending-messages) method. 
* __Proactive Messages__ are messages  your bot sends in response to some sort of external event such as a timer firing or a notification being triggered. For example, your bot might send proactive messages to notify a user that an item has shipped, or to request daily status from team members.  

### Saving the users address

The `UniversalBot` class exposes the [bot.send()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot#send) and [bot.beginDialog()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot#begindialog) methods for communicating with a user proactively. Both methods require the [address](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iaddress) of the user that you want to send the message to. Typically, you serialize the [session.message.address](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage#address) property from a previous conversation with the user.

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

You shouldn't assume that an `address` object for a user will always be valid. Addresses returned by the `ChatConnector` class contain a `serviceUrl` property which can theoretically change and prevent your bot from contacting the user. Because of thhis you should consider periodically updating the address object stored for a user. 

<span style="color:red"><< not sure what good this will do - isn't it possible that even if you store the address at the end of every conversation, the address could be invalid? >></span> 

### Sending messages

To proactively send a message to a user, you’ll need to first add the web hook or other logic that will trigger your proactive notification. The following example shows adding a web hook to the bot that triggers the delivery of a notification message to one of the bot's users.

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

This web hook de-serializes the user's address which we previously saved. It then composes a message for the user and sends it using [bot.send()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot#send). The `bot.send()` method can optionally include a callback to receive the method's success or failure.

### Starting conversations

In addition to sending messages proactively you can use [bot.beginDialog()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot#begindialog) to start new conversations with a user. The differences between `bot.send()` and `bot.beginDialog()` are subtle. Calling `bot.send()` with a message won’t affect the state of an existing conversation between the bot and user so it’s generally safe to call at any time. Calling `bot.beginDialog()` will end an existing conversation between the bot and user and start a new conversation using the specified dialog.  As a general rule, you should call `bot.send()` if you don’t need to wait for a reply from the user, and `bot.beginDialog()` if you do.

Starting a proactive conversation is very similar to sending a proactive message. The following example shows a web hook that triggers running a status session with multiple team members. The web hook loops over all of the team members and calls `bot.beginDialog()` with the address of each team member. The bot then asks the team member their status and rolls their answer up into a daily status report.

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

For bots that support multiple languages you’ll need to __always__ use `bot.beginDialog()` to communicate with a user.  Thats because the user's preferred locale is persisted as part of the [Session](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session) object which is currently only available within a dialog.  The following shows how to update the original notification example to use `bot.beginDialog()` instead of `bot.send()`:

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

The delivered notification will now use the SDK’s built-in [localization](/en-us/node/builder/chat/localization/) support to deliver the user a notification in their preferred language. The one disadvantage of using `bot.beginDialog()` over `bot.send()` is that any existing conversation between the bot and user will end before sending the user the notification. So for bots that support only a single language, using `bot.send()` is still preferred.

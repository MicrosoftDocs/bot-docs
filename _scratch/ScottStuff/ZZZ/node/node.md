
layout: page
title: Bot Connector Node.js
permalink: /en-us/connector/libraries/node/
weight: 4238
parent1: REST API


## Overview
The Bot Connector Service includes a node module which simplifies sending bot originated messages to the service. This module is only needed when a bot wants to initiate a new conversation with the user or wants to send a reply at some point in the future. Bots can receive messages and send immediate replies without the need of any special node module.

## Installation
Get the BotConnector module using npm.

    npm install --save botconnector

## Usage
The following example illustrates how to receive incoming messages from the Bot Connector Service and how to send outgoing messages to the Bot Connector Service. A verifyBotFramework() function is included to simplify verifying that incoming messages are from the Bot Framework. And a sendMessage() function is included to simplify sending a bot originated message to the Bot Connector Service. 
 
{% highlight JavaScript %}
var restify = require('restify');
var msRest = require('ms-rest');
var connector = require('botconnector');

// Initialize server
var server = restify.createServer();
server.use(restify.authorizationParser());
server.use(restify.bodyParser());

// Initialize credentials for connecting to Bot Connector Service
var appId = process.env.appId || 'your appId';
var appSecret = process.env.appSecret || 'your appSecret';
var credentials = new msRest.BasicAuthenticationCredentials(appId, appSecret);

// Handle incoming message
server.post('/v1/messages', verifyBotFramework(credentials), function (req, res) {
    var msg = req.body;
    if (/^delay/i.test(msg.text)) {
        // Delay sending the reply for 5 seconds
        setTimeout(function () {
            var reply = { 
                replyToMessageId: msg.id,
                to: msg.from,
                from: msg.to,
                text: 'I heard "' + msg.text.substring(6) + '"'
            };
            sendMessage(reply);
        }, 5000);
        res.send({ text: 'ok... sending reply in 5 seconds.' })
    } else {
        res.send({ text: 'I heard "' + msg.text + '". Say "delay {msg}" to send with a 5 second delay.' })
    }
});

// Start server
server.listen(process.env.PORT || 8080, function () {
    console.log('%s listening to %s', server.name, server.url); 
});

// Middleware to verfiy that requests are only coming from the Bot Connector Service
function verifyBotFramework(credentials) {
    return function (req, res, next) {
        if (req.authorization && 
            req.authorization.basic && 
            req.authorization.basic.username == credentials.userName &&
            req.authorization.basic.password == credentials.password) 
        {
            next();        
        } else {
            res.send(403);
        }
    };
}

// Helper function to send a Bot originated message to the user. 
function sendMessage(msg, cb) {
    var client = new connector(credentials);
    var options = { customHeaders: {'Ocp-Apim-Subscription-Key': credentials.password}};
    client.messages.sendMessage(msg, options, function (err, result, request, response) {
        if (!err && response && response.statusCode >= 400) {
            err = new Error('Message rejected with "' + response.statusMessage + '"');
        }
        if (cb) {
            cb(err);
        }
    });          
}
{% endhighlight %}
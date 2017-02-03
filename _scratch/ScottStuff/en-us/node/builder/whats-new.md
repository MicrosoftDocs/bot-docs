---
layout: page
title: What's new or changed in v3.3
permalink: /en-us/node/builder/whats-new/
weight: 1520
parent1: Building your Bot Using the Bot Builder for Node.js
---

<span style="color:red"><<
- I think that we need a comprehensive What's New story for BF. This section has a What's New topic and a Release Notes topic. There should be one place that the dev looks for release notes.
- I think it's odd that the title includes v3.3 but the topic includes prior versions.
- I think that we need to explain our versioning story so our users understand our process and what level of backwards compatibility we provide.
>></span>



* TOC
{:toc}

## v3.3
Version 3.3 includes general bug fixes & improvments along with improved localization support.

### Localization
Bot Builder now supports a rich file based [localization](/en-us/node/builder/chat/localization) system for building bots that support multiple languages. This new system also provides a way for English only bots to re-skin the SDK's built-in messages so should prove generally useful to all bot developers.

## v3.2
Version 3.2 of the Node SDK includes bug fixes as well as a few new convenience methods like [Session.sendTyping()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#sendtyping) & [IntentDialog.matchesAny()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.intentdialog#matchesany) and also includes support for g-zip `userData` & `conversationData` stored on the Bot Frameworks Bot State service. 

## v3.1
Version 3.1 of the Node SDK fixes a few bugs, makes a general improvement to the way your waterfalls interact with the built-in prompts, and adds a major new concept called Actions.

### Actions
Bot Builder includes a [DialogAction](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.dialogaction) which is a small set of _static_ functions that can be used to simplify implementing more coming tasks like sending a message, starting a dialog, or creating a new prompt with a built-in validation routine. In version 3.1 we’re extending Bot Builders action concept to include a new set of Named Dialog Actions. These new actions let you define code that should be run should some user initiated action occur like saying “help” or clicking a button on a [Persistent Menu](https://developers.facebook.com/docs/messenger-platform/thread-settings/persistent-menu){:target="_blank"} in Facebook.

Up to this point, bots based on Bot Builder were limited to conducting a single guided conversation. If the bot shows a card with a `postBack` or `imBack` button the user needs to click the button immediately because once it scrolls up the feed the bot is no longer listening for the button to be clicked and therefore can’t take any action in response to the click. There was also no clean way for a bot to listen for commands from the user like “help” or “cancel” as again the bot can generally only listen for responses to the last questioned it asked the user. 

With the addition of Actions all of that changes. Bots can now define global actions which can be triggered regardless of where the user is within the conversation. A global “help” action can easily be added that can be triggered from anywhere to provide the user with help.  A global “goodbye” action can be used to let the end the conversation with the bot from anywhere.  You can also define actions at the dialog level which will only be evaluated if the dialog is on the stack.  So tagging a complex order taking dialog as cancelable is now a simple one-line operation. All of these actions can either be bound to buttons so that they’ll be triggered anytime the button is clicked (regardless of where it is in the feed) or you can associate a regular expression with the action which will cause it to be triggered anytime the user says a given utterance.

### Prompt Improvements
To go along with the new Actions feature, the built-in prompts have received some improvements. In previous versions of Bot Builder you needed to be pretty defensive when coding your waterfall steps as there were a number of things that could cause a call to one of the built-in prompts to return a `result.response` that was null. The user could have asked to cancel the prompt or they could have simply failed to enter a valid response within the appropriate number of retries.  Both issues can lead to very buggy waterfall code if you don’t add a whole series of defensive checks to every single step of your waterfall.

To try and simplify building less buggy waterfalls you can now generally code them without having to first check to ensure that `result.response` has a value before using it. With two exceptions, if you now call a built-in prompt your waterfall will only advance to the next step once a user successfully enters a valid response. That means you can now just assume you got a value from the user and remove all of those nasty extra checks.  The changes the made this possible is the prompts no longer listen for the user to say “cancel”, there’s an action for that. And the prompts now by default re-prompt the user to enter a valid prompt before advancing.  The two exceptions to that rule are when you manually advance the waterfall by calling `next()` from a step or you explicitly set the `maxRetries` setting when you call the prompt. In both cases you’re in control of when this happens and therefore you can code to protext against a null value being returned for `result.response`. 

## v3.0
There are numerous changes to Bot Builder for Node.js in the 3.0 release. Before we get into the changes let’s answer “Why version 3.0 and not version 2.0?”. For the past few months the Bot Framework team has been working with Skype to create one unified bot platform for all of Microsoft.  This resulted in a new unified message schema and attachment format that we jointly called the v3 schema.  All of the new API’s from the Bot Framework are annotated with v3 so to avoid confusion on the SDK side we decided to adopt v3 for the SDK version number as well. 

To go along with the new message schema being introduced we also took the opportunity to overhaul a number of elements of the SDK. We’ve learned a lot over the last couple of months (this bot building stuff is new to us too) and so the SDK was definitely in need of a little restructuring to better position it moving forward.

The big question that most of you probably have is “Do I need to re-write my bot?” For most bots the answers “no”.  Your old bot should keep working although you may see a few warnings about using deprecated classes and attachment schemas logged to the console. But you’ll want to update your bot as soon as you can because all of the new stuff is richer and better.  It should be relatively straight forward to switch to the new stuff so it should probably only take a few minutes at most.  In a few cases, like if your bot is sending “proactive messages” or you’re using the `SlackBot` class you will need to rework your bot because we’ve significantly changed the way proactive messaging works (it’s way simpler now) and in the case of the SlackBot it’s unfortunately no longer supported  Options for that will be outlined below.

### New Schema
The message schema in v3 has changed quite a bit but this should have little effect on most existing bots as the fields that have changed aren’t ones that you’d normally touch. The exceptions to that are the old `channelData` field is now called [sourceEvent](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage#sourceevent) and all of the `from`, `to`, and `conversationId` fields have been moved onto a new [IAddress](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iaddress) object that hangs off [session.message.address](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage#address). This new address object dramatically simplifies sending proactive messages from your bot as now all you have to do is save the address object for some user you want to notify at a future point in time and then when you’re ready to contact them you read that in, compose a new message with that object for the address, and then call either [bot.send()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot#send) or [bot.beginDialog()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot#begindialog).

The bigger schema change is around attachments. There’s a whole new card schema that lets you send rich [hero cards](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.herocard), [thumbnail cards](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.thumbnailcard), and [receipt cards](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.receiptcard) in a cross channel way.  The SDK now includes a whole new set of card builder classes to make it trivial to build carousels and lists of cards that work across multiple channels. To keep from breaking a large number of bots the SDK still supports the old attachment format and will automatically convert your old style attachments to the new format but it will load your console up with warnings every time it does so you’ll want to convert to the new attachment schema as soon as you can.
    
### UniversalBot
The 1.x version of the SDK had a number of bot classes that would connect your bots dialogs to a range of sources. The issue with that model is that it you can really only easily connect your bots dialogs to one source at a time and there’s a lot of code duplication across these various bot classes. The new SDK has a single [UniversalBot](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot) class that you connect your dialogs to. This new class has a much lighter weight [connector](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iconnector) model for connecting your bot to different sources. The SDK comes out of the box with a [ChatConnector](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.chatconnector) & [ConsoleConnector](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.consoleconnector) class and you could connect your bot to both simultaneously if you wanted to.

The old [BotConnectorBot](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.botconnectorbot), [SkypeBot](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.skypebot), and [TextBot](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.textbot) classes are deprecated but largely still functional. You’ll receive a warning when you use them and an exception will be thrown if you try to use a feature that’s no longer supported. Converting to using the new UniversalBot class should be fairly straightforward. For the BotConnectorBot & SkypeBot classes switch to using the UniversalBot with the ChatConnector.

{% highlight JavaScript %}
var connector = new builder.ChatConnector({
    appId: process.env.MICROSOFT_APP_ID,
    appPassword: process.env.MICROSOFT_APP_PASSWORD
});
var bot = new builder.UniversalBot(connector);
server.post('/api/messages', connector.listen());
{% endhighlight %}

For the TextBot class switch to using the UniversalBot with a ConsoleConnector:

{% highlight JavaScript %}
var connector = new builder.ConsoleConnector().listen();
var bot = new builder.UniversalBot(connector);
{% endhighlight %}

The old `SlackBot` class for talking natively to Slack is unfortunately no longer supported and has been removed from the SDK all together. The reasoning for this is that [Bot Framework](http://www.botframework.com){:target="_blank"} already has a slack channel so over the long run you’ll get better support for cross channel messaging by going through the ChatConnector when talking to Slack. Someone in the community way elect to build a native connector for Slack and we’ll certainly point everyone to that if they do.

### IntentDialog
A lot of users have asked for more flexibility with regards to the [LuisDialog](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.luisdialog) class. They’d like the ability to chain multiple models together or the ability to add [CommandDialog](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.commanddialog)  regular expression based matching over the top of LUIS’s built-in Crotona Model.  In the new SDK we’ve completely reworked the [IntentDialog](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.intentdialog) class that the LuisDialog class derived from this.  This new IntentDialog class is now the only class you need to perform intent recognition. It lets you mix together regular expression based intent matching with a new system of pluggable cloud based intent recognizers.  Out of the box the SDK includes a new [LuisRecognizer](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.luisrecognizer) plugin but the community could easily add recognizers for other services. You can configure the IntentDialog to use multiple recognizers and then there are options to control whether the recognizers are evaluated in parallel or series. Extending LUIS’s built-in models is now a breeze.

The old CommandDialog & LuisDialog classes have been deprecated but are still completely functional. You’ll just get a warning logged to the console on startup. Migrating to use the new IntentDialog class is super straight forward:

{% highlight JavaScript %}
var recognizer = new builder.LuisRecognizer('<your models url>');
var intents = new builder.IntentDialog({ recognizers: [recognizer] });
bot.dialog('/', intents);
{% endhighlight %}

### Message and Card Builders
The 1.x version of the SDK included a [Message](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message) builder class that was useful for composing messages when you wanted your bot to return attachments.  In light of recent developments in the bot space we envision more and more bots will want to take advantage of all the various buttons, cards, and keyboard features being added by messaging clients. We want to make it as easy as possible to build rich replies from your bot so we’ve dramatically revamped the existing Message builder class and added a whole new suite of Card builder classes. The existing methods on the Message class still work but are mostly deprecated. The old attachment format still works but is also deprecated in favor of the new card formats.  Pretty much everything in the SDK that can send a reply to the user has been updated to take either strings or a Message object so even all of the built-in prompts let you prompt the user using cards that automatically adapt to the channel they’re being rendered to. 

### Middleware
Bot Builders existing middleware system has also received an overhaul.  Middleware can now install multiple [hooks](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imiddlewaremap) to capture incoming & outgoing messages as well as a hook that lets you manipulate Bot Builders dialog system.  We’re working with other SDK developers in the Node community to create standard interfaces and schema that let bot related middleware work across multiple SDK’s.  So very soon middleware written for Bot Builder will work in an SDK like [Botkit](https://howdy.ai/botkit/) and vice versa.

To go along with this the SDK now includes a couple of pieces of built-in middleware. There’s a new [dialogVersion()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.middleware#dialogversion) middleware that lets you version your dialogs and automatically reset active conversation with your bot when you deploy a major change to your bot. The new [firstRun](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.middleware#firstrun) middleware lets you create a really clean firstrun experience for your bot. You can run new users through a set of first run dialogs to collect things like profile information and accept your bots terms of use. Then when your terms of use change you can automatically run them back through just the terms of use part to have them re-accept the new terms. These are the first two pieces of middleware we're delivering but a lot more will be coming soon.

### Libraries
This is still a work in progress but there’s a whole new [Library](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.library) system design to let you package up parts of your bot into Node modules that can be easily reused within other bots.  We’re working up examples that show this in action and provide a recipe for how to build your own libraries. On top of that we have quite a few add-on libraries we’d like to ship ourselves.

### Error Handling
The new SDK contains several general improvements around error handling. Situations that would get users stuck in your bot for unknown reasons should now be resolved and there’s a new customizable error message that gets sent to the user anytime we detect an error like an exception being thrown in your bot.

### Calling SDK
Last but certainly not least we’re shipping a whole new [Calling SDK](/en-us/node/builder/calling-reference/modules/_botbuilder_d_.html) that lets you build bots exclusively for skype that you can call and have conversations with using voice. These bots borrow a lot of concepts from their chat counterparts so if you’re familiar with developing chat bots, building calling bots will feel like a natural extension of those skills.  You can even directly share code between your calling and chat bots in some cases.

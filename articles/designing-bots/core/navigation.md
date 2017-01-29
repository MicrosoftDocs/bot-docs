---
title: Designing Bots - Navigation | Bot Framework
description: Navigation - Managing user navigation in conversational applications 
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
# Bot Design Center - Navigation


##Where am I?

Websites have breadcrumbs, apps have menus, web browsers offer buttons to navigate forward and back and so on. Enter bots and we are now in a whole new world where those simple, tested and validated solutions for keeping the user aware of where they are haven't been widely established yet.

How do we ensure that a user doesn't get lost in a conversation with a bot? Can a user navigate "back" in a chat? How to go to the "main menu"? How do we "cancel" an operation?

Let us look at some common traps of conversational interfaces and how to overcome them. We will do that by describing some "personality disorders" that bots often display:

##The "stubborn bot"

Imagine this scenario:

![bot](../../media/designing-bots/core/stubborn-bot.png)

It is easy to imagine how an user could get very frustrated with this scenario. Users change their minds, they cancel things. They want to start over. It is a common mistake to build a dialog in such a way that it doesn't take into account that possibility and instead just keeps retrying the same question, over and over again.

Of course there are many ways to overcome this problem, but we will focus on the simplest one. The next topics further in this page will discuss more advanced solutions that may also be applied here. 

So the simplest way to stop a bot from ending in a loop asking the same question over and over again is simply to use prompts with specific retry attempt numbers:

In C#:


	PromptDialog.Choice(context, this.OnOptionSelected, new List<string>() { 
		FlightsOption, HotelsOption 
	}, "Are you looking for a flight or a hotel?", "Not a valid option", 3);

In Node:

	builder.Prompts.choice(session,'Are you looking for a flight or a hotel?',
		[Flights.Label, Hotels.Label],
        {
        	maxRetries: 3,
            retryPrompt: 'Not a valid option'	
		});

In this case we are not trying to do anything smart in terms of detecting whether the user is asking us explicitly to stop, but at least we will give up on retrying the same question after a given number of attempts. So the "stubbornness" is healed!

##The "clueless bot"

Imagine this scenario:

![bot](../../media/designing-bots/core/clueless-bot.png)

This scenario is similar to the previous one, but a little more complex: In this case "Help!" is a valid string. The prompt doesn't know the difference so it can't reject it. Now of course we could simply code a check for a few keywords after that and see whether the user is asking for things like "help", "cancel" or one of those basic navigation operations.

But the problem would be having to do this for every little question in every single dialog everywhere in the bot. Trust us on this one: You just don't want to have to do that.

In such cases we will override the standard prompts to add handlers for variations such as the use of expressions such as "cancel", "help" or "start over". We could go further and even use natural language calls in these prompts, but for the sake of simplicity, we will just do simple Regex match in this scenario.

In C#:

	[Serializable]
    public class PromptStringRegex : Prompt<string, string>
    {
        private readonly Regex regex;
        public PromptStringRegex(string prompt, string regexPattern, string retry = null, string tooManyAttempts = null, int attempts = 3)
            : base(new PromptOptions<string>(prompt, retry, tooManyAttempts, attempts: attempts))
        {
            this.regex = new Regex(regexPattern, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        }

        protected override bool TryParse(IMessageActivity message, out string result)
        {
            var quitCondition = message.Text.Equals("Cancel", StringComparison.InvariantCultureIgnoreCase);
            var validEmail = this.regex.Match(message.Text).Success;
            result = validEmail ? message.Text : null;
            return validEmail || quitCondition;
        }
    }

In this case we are creating a "PromptStringRegex" by inheriting from the standard Prompt class in the Bot Framework SDK. This allows us to prompt for a string just like before, but do an additional validation for the word "Cancel", as well as validating any entry against a given regex expression. When the prompt is actually used, we can define this regex expression:

	private const string PhoneRegexPattern = @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";

    public async Task StartAsync(IDialogContext context)
	{
    	var promptPhoneDialog = new PromptStringRegex(
        	"Please enter your phone number:", 
            PhoneRegexPattern, 
            "The value entered is not phone number. Please try again using the following format (xyz) xyz-wxyz:",
            "You have tried to enter your phone number many times. Please try again later.",
                attempts: 2);

            context.Call(promptPhoneDialog, this.ResumeAfterPhoneEntered);
	}

In this scenario we need to prompt for a phone number. We then provide the regex for validating the entry as well as a number of attempts to retry. If the user's entry differ from what is expected, the prompt will now know how to deal with it.


In Node, the approach is similar, although with different syntax:

	var builder = require('botbuilder');
	
	const PhoneRegex = new RegExp(/^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$/);
	const library = new builder.Library('validators');

	library.dialog('phonenumber',
    	builder.DialogAction.validatedPrompt(builder.PromptType.text, (response) =>
        	PhoneRegex.test(response)));

	module.exports = library;
	module.exports.PhoneRegex = PhoneRegex;

First we define a *phonenumber* dialog which will validate the prompt against a regex. 

We also define a cancelAction which will detect the word "cancel":

    .cancelAction('cancel', null, { matches: /^cancel/i });

And with that in mind, we will then invoke the prompt for phone number from the root:

	library.dialog('/', [
	    function (session) {
	        session.beginDialog('validators:phonenumber', {
	            prompt: 'Please enter your phone number:',
	            retryPrompt: 'The value entered is not phone number. Please try again using the following format (xyz) xyz-wxyz:',
	            maxRetries: 2
	        });
	    },
	    function (session, args) {
	        if (args.resumed) {
	            session.send('You have tried to enter your phone number many times. Please try again later.');
	            session.endDialogWithResult({ resumed: builder.ResumeReason.notCompleted });
	            return;
	        }

        session.dialogData.phoneNumber = args.response;
        session.send('The phone you provided is: ' + args.response);

Regardless of the differences between C# and Node SDKs, both are now enabling us to handle the free text entries with more validation and detection of escape words. 

##The "mysterious bot"

Imagine this scenario:

![bot](../../media/designing-bots/core/mysterious-bot.png)


Now it is difficult to guess what is happening with this bot. Maybe it is having an outage. It may be "stuck" somewhere. It may also - perfectly common case - just be taking a while to answer. But not replying to the user nor giving any visual cue of what is going on is still not a great idea. The user in this case has no idea of what is going on, whether they need to repeat the same question, how to cancel/start over... No cue is being given, at all.

There are a few things we can do to help here. 

	TODO: Add examples of scorables from Chris


##The "captain obvious bot"

Imagine this scenario:

![bot](../../media/designing-bots/core/captainobvious-bot.png)


	TODO: describe this one

##The "bot that can't forget"

Imagine this scenario:

![bot](../../media/designing-bots/core/rememberall-bot.png)


	TODO: describe this one

(to be continued, I'm tired...)
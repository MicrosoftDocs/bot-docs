---
layout: page
title: Using Prompts to Get User Input
permalink: /en-us/node/builder/calling/prompts/
weight: 1210
parent1: Building your Bot Using the Bot Builder for Node.js
parent2: Skype Calling Bots
---

Bot Builder comes with a number of built-in prompts that you can use to collect input from a user.  

|**Prompt Type**     | **Description**                                   
| -------------------| ---------------------------------------------
|[Prompts.choice](#promptschoice) | Asks the user to choose from a list of choices.       
|[Prompts.digits](#promptsdigits) | Asks the user to enter a sequence of digits.      
|[Prompts.confirm](#promptsconfirm) | Asks the user to confirm an action.  
|[Prompts.record](#promptsrecord) | Asks the user to record a message.
|[Prompts.action](#promptsaction) | Sends a raw [action](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.iaction) to the calling service and lets the bot manually process its outcome.

Because prompts are implemented as [dialogs](/en-us/node/builder/chat/dialogs/) they return the user's response through a call to [session.endDialogWithresult()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#enddialogwithresult). Any [DialogHandler](/en-us/node/builder/chat/dialogs/#dialog-handlers) can receive the result of a dialog but [waterfalls](/en-us/node/builder/chat/dialogs/#waterfall) tend to be the simplest way to handle a prompt result.  

Prompts return an [IPromptResult](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.ipromptresult.html) interface to the caller, which contains the user's response in the [results.response](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.ipromptresult.html#reponse) field. If the user fails to respond, the field may be null. 

### Prompts.choice()

The [Prompts.choice()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.prompts.html#choice) method asks the user to pick an option from a list. You can configure the prompt to use speech recognition to recognize the caller's choice.

To specify the choices, you can use an array of [IRecognitionChoice](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.irecognitionchoice) objects.


{% highlight JavaScript %}
calling.Prompts.choice(session, "Which department? support, billing, or claims?", [
    { name: 'support', speechVariation: ['support', 'customer service'] },
    { name: 'billing', speechVariation: ['billing'] },
    { name: 'claims', speechVariation: ['claims'] }
]);
{% endhighlight %}

Or, DTMF input using Skypes dialing pad

<span style="color:red">What's DTMF an acronym for?</span>


{% highlight JavaScript %}
calling.Prompts.choice(session, "Which department? Press 1 for support, 2 for billing, or 3 for claims", [
    { name: 'support', dtmfVariation: '1' },
    { name: 'billing', dtmfVariation: '2' },
    { name: 'claims', dtmfVariation: '3' }
]);
{% endhighlight %}

Or, both speech recognition and DTMF.

{% highlight JavaScript %}
bot.dialog('/departmentMenu', [
    function (session) {
        calling.Prompts.choice(session, "Which department? Press 1 for support, 2 for billing, 3 for claims, or star to return to previous menu.", [
            { name: 'support', dtmfVariation: '1', speechVariation: ['support', 'customer service'] },
            { name: 'billing', dtmfVariation: '2', speechVariation: ['billing'] },
            { name: 'claims', dtmfVariation: '3', speechVariation: ['claims'] },
            { name: '(back)', dtmfVariation: '*', speechVariation: ['back', 'previous'] }
        ]);
    },
    function (session, results) {
        if (results.response !== '(back)') {
            session.beginDialog('/' + results.response.entity + 'Menu');
        } else {
            session.endDialog();
        }
    },
    function (session) {
        // Loop menu
        session.replaceDialog('/departmentMenu');
    }
]);
{% endhighlight %}

The [IFindMatchResult](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.ifindmatchresult) object contains the user's choice in the [response.entity](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.ifindmatchresult#entity) property.



### Prompts.digits()

The [Prompts.digits()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.prompts.html#digits) method asks the user to enter a sequence of digits followed by an optional stop tone. The [IPromptDigitsResult](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.ipromptdigitsresult.html) object contains the user's response. 

{% highlight JavaScript %}
calling.Prompts.digits(session, "Please enter your account number followed by pound.", 10, { stopTones: ['#'] });
{% endhighlight %}

### Prompts.confirm()

The [Prompts.confirm()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.prompts.html#confirm) method asks the user to confirm some action. This prompt builds on the choices prompt by calling it with a standard set of yes and no choices. The user can reply by saying a range of responses or they can press 1 for yes or 2 for no.  The [IPromptConfirmResult](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.ipromptconfirmresult.html) object contains the user's response. 

{% highlight JavaScript %}
calling.Prompts.confirm(session, "Would you like to end the call?");
{% endhighlight %}

### Prompts.record()

The [Prompts.record()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.prompts.html#record) method asks the user to record a message. This prompt builds on the choices prompt by calling it with a standard set of yes and no choices. The [IPromptRecordResult](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.ipromptrecordresult.html) object contains the recorded message off the object as _{Buffer}_.

<span style="color:red">Need to show accessing the recording?</span>

{% highlight JavaScript %}
calling.Prompts.record(session, "Please leave a message after the beep.");
{% endhighlight %}

### Prompts.action()

The [Prompts.action()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.prompts.html#action) method lets you send the calling service a raw [action](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.iaction) object. The [IPromptActionResult](/en-us/node/builder/calling-reference/interfaces/_botbuilder_d_.ipromptactionresult.html) object contains the user's response. 

Typically, you won't need to call this prompt except to send a `playPrompt` action that plays a file to the user. For example, you might play the user hold music or silence while you periodically check for some long running operation to complete. Note that the normal [session.send()](/en-us/node/builder/calling-reference/classes/_botbuilder_d_.callsession#send) method you’d use to play a file will automatically end the call. To keep the call active, you need to follow the `playPrompt` action with a recognize or record action (you can  dynamically chain together play prompts). 
 
<span style="color:red">How come we don't show this usage?</span>

<span style="color:red">What do you mean by dynamically chain together?</span>

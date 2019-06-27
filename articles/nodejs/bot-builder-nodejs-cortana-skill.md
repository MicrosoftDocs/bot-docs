---
title: Build a speech-enabled bot with Cortana skills | Microsoft Docs
description: Learn how to build a speech-enabled bot with Cortana skills and the Bot Framework SDK for Node.js.
author: DeniseMak
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 02/10/2019
monikerRange: 'azure-bot-service-3.0'
---
# Build a speech-enabled bot with Cortana skills

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-cortana-skill.md)
> - [Node.js](../nodejs/bot-builder-nodejs-cortana-skill.md)

The Bot Framework SDK for Node.js enables you to a build speech-enabled bot by connecting it to the Cortana channel as a Cortana skill. 
Cortana skills let you provide functionality through Cortana in response to spoken input from a user.

> [!TIP]
> For more information on what a skill is, and what they can do, see [The Cortana Skills Kit][CortanaGetStarted].

Creating a Cortana skill using Bot Framework requires very little Cortana-specific knowledge and primarily consists of building a bot. One of the key differences from other bots that you may have created is that Cortana has both visual and audio components. For the visual component, Cortana provides an area of the canvas for rendering content such as cards. For the audio component, you provide text or SSML in your bot's messages, which Cortana reads to the user, giving your bot a voice. 

> [!NOTE]
> Cortana is available on many different devices. Some have a screen while others, like a standalone speaker, might not. You should make sure that your bot is capable of handling both scenarios. See [Cortana-specific entities][CortanaSpecificEntities] to learn how to check device information.

## Adding speech to your bot

Spoken messages from your bot are represented as Speech Synthesis Markup Language (SSML). The Bot Framework SDK lets you include SSML in your bot's responses to control what the bot says, in addition to what it shows.

### session.say

Your bot uses the **session.say** method to speak to the user, in place of **session.send**. It includes optional parameters for sending SSML output, as well as attachments like cards. 

The method has this format:

```session.say(displayText: string, speechText: string, options?: object)```

| Parameter | Description |
|------|------|
| **displayText** | A textual message to display in Cortana's UI.|
| **speechText** | The text or SSML that Cortana reads to the user. |
| **options** | An [IMessage][IMessage] object that can contain an attachment or input hint. Input hints indicate whether the bot is accepting, expecting, or ignoring input. Card attachments are displayed in Cortana’s canvas below the **displayText** information.   |

The **inputHint** property helps indicate to Cortana whether your bot is expecting input. If you're using a built-in prompt, this value is automatically set to the default of **expectingInput**.


| Value | Description |
|------|------|
| **acceptingInput** | Your bot is passively ready for input but is not waiting on a response. Cortana accepts input from the user if the user holds down the microphone button.|
| **expectingInput** | Indicates that the bot is actively expecting a response from the user. Cortana listens for the user to speak into the microphone.  |
||NOTE: Do _not_ use **expectingInput** on headless devices (devices without a display). See the [Cortana Skills Kit FAQ](https://review.docs.microsoft.com/en-us/cortana/skills/faq).|
| **ignoringInput** | Cortana is ignoring input. Your bot may send this hint if it is actively processing a request, and will ignore input from users until the request is complete.  |

The following example shows how Cortana reads plain text or SSML:

```javascript

// Have Cortana read plain text
session.say('This is the text that Cortana displays', 'This is the text that is spoken by Cortana.');

// Have Cortana read SSML
session.say('This is the text that Cortana displays', '<speak version="1.0" xmlns="http://www.w3.org/2001/10/synthesis" xml:lang="en-US">This is the text that is spoken by Cortana.</speak>');

```

This example shows how to let Cortana know that user input is expected. The microphone will be left open.

```javascript

// Add an InputHint to let Cortana know to expect user input
session.say('Hi there', 'Hi, what’s your name?', {
    inputHint: builder.InputHint.expectingInput
});

```
<!-- TODO: tip about time limit and batching -->

### Prompts

In addition to using the **session.say()** method you can also pass text or SSML to built-in prompts using the **speak** and **retrySpeak** options.  

```javascript

builder.Prompts.text(session, 'text based prompt', {                                    
    speak: 'Cortana reads this out initially',                                               
    retrySpeak: 'This message is repeated by Cortana after waiting a while for user input',  
    inputHint: builder.InputHint.expectingInput                                              
});

```

<!-- TODO: Link to SSML library -->

To present the user with a list of choices, use **Prompts.choice**. The **synonyms** option allows for more flexibility in recognizing user utterances. The **value** option is returned in **results.response.entity**. The **action** option specifies the label that your bot displays for the choice.

**Prompts.choice** supports ordinal choices. This means that the user can say "the first", "the second" or "the third" to choose an item in a list. For example, given the following prompt, if the user asked Cortana for "the second option", the prompt will return the value of 8.

```javascript

        var choices = [
            { value: '4', action: { title: '4 Sides' }, synonyms: 'four|for|4 sided|4 sides' },
            { value: '8', action: { title: '8 Sides' }, synonyms: 'eight|ate|8 sided|8 sides' },
            { value: '12', action: { title: '12 Sides' }, synonyms: 'twelve|12 sided|12 sides' },
            { value: '20', action: { title: '20 Sides' }, synonyms: 'twenty|20 sided|20 sides' },
        ];
        builder.Prompts.choice(session, 'choose_sides', choices, { 
            speak: speak(session, 'choose_sides_ssml') // use helper function to format SSML
        });

```

In the previous example, the SSML for the prompt's **speak** property is formatted by using strings stored in a localized prompts file with the following format. 

```json

{
    "choose_sides": "__Number of Sides__",
    "choose_sides_ssml": [
        "How many sides on the dice?",
        "Pick your poison.",
        "All the standard sizes are supported."
    ]
}

```

A helper function then builds the required root element of a Speech Synthesis Markup Language (SSML) document. 

```javascript

module.exports.speak = function (template, params, options) {
    options = options || {};
    var output = '<speak xmlns="http://www.w3.org/2001/10/synthesis" ' +
        'version="' + (options.version || '1.0') + '" ' +
        'xml:lang="' + (options.lang || 'en-US') + '">';
    output += module.exports.vsprintf(template, params);
    output += '</speak>';
    return output;
}
```

> [!TIP]
> You can find a small utility module (ssml.js) for building your bot's SSML-based responses in the [Roller sample skill](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/demo-RollerSkill).
> There are also several useful SSML libraries available through [npm](https://www.npmjs.com/search?q=ssml) which make it easy to create well formatted SSML.

## Display cards in Cortana

In addition to spoken responses, Cortana can also display card attachments. Cortana supports the following rich cards:
* [HeroCard](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.herocard.html)
* [ReceiptCard](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.receiptcard.html)
* [ThumbnailCard](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.thumbnailcard.html)

See [Card design best practices][CardDesign] to see what these cards look like inside Cortana. For an example of how to add a rich card to a bot, see [Send rich cards](bot-builder-nodejs-send-rich-cards.md). 

The following code demonstrates how to add the **speak** and **inputHint** properties to a message containing a Hero card.

```javascript

bot.dialog('HelpDialog', function (session) {
    var card = new builder.HeroCard(session)
        .title('help_title')
        .buttons([
            builder.CardAction.imBack(session, 'roll some dice', 'Roll Dice'),
            builder.CardAction.imBack(session, 'play yahtzee', 'Play Yahtzee')
        ]);
    var msg = new builder.Message(session)
        .speak(speak(session, 'I\'m roller, the dice rolling bot. You can say \'roll some dice\''))
        .addAttachment(card)
        .inputHint(builder.InputHint.acceptingInput); // Tell Cortana to accept input
    session.send(msg).endDialog();
}).triggerAction({ matches: /help/i });

/** This helper function builds the required root element of a Speech Synthesis Markup Language (SSML) document. */
module.exports.speak = function (template, params, options) {
    options = options || {};
    var output = '<speak xmlns="http://www.w3.org/2001/10/synthesis" ' +
        'version="' + (options.version || '1.0') + '" ' +
        'xml:lang="' + (options.lang || 'en-US') + '">';
    output += module.exports.vsprintf(template, params);
    output += '</speak>';
    return output;
}

```
## Sample: RollerSkill
The code in the following sections comes from a sample Cortana skill for rolling dice. Download the full code for the bot from the [BotBuilder-Samples repository](https://github.com/Microsoft/BotBuilder-Samples/tree/master/Node/demo-RollerSkill).

You invoke the skill by saying its [invocation name][InvocationNameGuidelines] to Cortana. For the roller skill, after you [add the bot to the Cortana channel][CortanaChannel] and register it as a Cortana skill, you can invoke it by telling Cortana "Ask Roller" or "Ask Roller to roll dice".

### Explore the code

The RollerSkill sample starts by opening a card with some buttons to tell the user which options are available to them.

```javascript

/**
 *   Create your bot with a default message handler that receive messages from the user.
 * - This function is be called anytime the user's utterance isn't
 *   recognized by the other handlers in the bot.
 */
var bot = new builder.UniversalBot(connector, function (session) {
    // Just redirect to our 'HelpDialog'.
    session.replaceDialog('HelpDialog');
});

//...

bot.dialog('HelpDialog', function (session) {
    var card = new builder.HeroCard(session)
        .title('help_title')
        .buttons([
            builder.CardAction.imBack(session, 'roll some dice', 'Roll Dice'),
            builder.CardAction.imBack(session, 'play craps', 'Play Craps')
        ]);
    var msg = new builder.Message(session)
        .speak(speak(session, 'help_ssml'))
        .addAttachment(card)
        .inputHint(builder.InputHint.acceptingInput);
    session.send(msg).endDialog();
}).triggerAction({ matches: /help/i });
```

### Prompt the user for input

The following dialog sets up a custom game for the bot to play.  It 
asks the user how many sides they want the dice to have and then
how many should be rolled. Once it has built the game structure
it will pass it to a separate 'PlayGameDialog'.

To start the dialog, the **triggerAction()** handler on this dialog allows a user to say
something like "I'd like to roll some dice". It uses a regular expression to match the user's input but you could just as easily use a [LUIS intent](./bot-builder-nodejs-recognize-intent-luis.md). 


```javascript
bot.dialog('CreateGameDialog', [
    function (session) {
        // Initialize game structure.
        // - dialogData gives us temporary storage of this data in between
        //   turns with the user.
        var game = session.dialogData.game = { 
            type: 'custom', 
            sides: null, 
            count: null,
            turns: 0
        };

        var choices = [
            { value: '4', action: { title: '4 Sides' }, synonyms: 'four|for|4 sided|4 sides' },
            { value: '6', action: { title: '6 Sides' }, synonyms: 'six|sex|6 sided|6 sides' },
            { value: '8', action: { title: '8 Sides' }, synonyms: 'eight|8 sided|8 sides' },
            { value: '10', action: { title: '10 Sides' }, synonyms: 'ten|10 sided|10 sides' },
            { value: '12', action: { title: '12 Sides' }, synonyms: 'twelve|12 sided|12 sides' },
            { value: '20', action: { title: '20 Sides' }, synonyms: 'twenty|20 sided|20 sides' },
        ];
        builder.Prompts.choice(session, 'choose_sides', choices, { 
            speak: speak(session, 'choose_sides_ssml') 
        });
    },
    function (session, results) {
        // Store users input
        // - The response comes back as a find result with index & entity value matched.
        var game = session.dialogData.game;
        game.sides = Number(results.response.entity);

        /**
         * Ask for number of dice.
         */
        var prompt = session.gettext('choose_count', game.sides);
        builder.Prompts.number(session, prompt, {
            speak: speak(session, 'choose_count_ssml'),
            minValue: 1,
            maxValue: 100,
            integerOnly: true
        });
    },
    function (session, results) {
        // Store users input
        // - The response is already a number.
        var game = session.dialogData.game;
        game.count = results.response;

        /**
         * Play the game we just created.
         * 
         * replaceDialog() ends the current dialog and start a new
         * one in its place. We can pass arguments to dialogs so we'll pass the
         * 'PlayGameDialog' the game we created.
         */
        session.replaceDialog('PlayGameDialog', { game: game });
    }
]).triggerAction({ matches: [
    /(roll|role|throw|shoot).*(dice|die|dye|bones)/i,
    /new game/i
 ]});

```

### Render results

 This dialog is our main game loop. The bot stores the game structure in
 **session.conversationData** so that should the user say "roll again" we
 can just re-roll the same set of dice again.

```javascript

bot.dialog('PlayGameDialog', function (session, args) {
    // Get current or new game structure.
    var game = args.game || session.conversationData.game;
    if (game) {
        // Generate rolls
        var total = 0;
        var rolls = [];
        for (var i = 0; i < game.count; i++) {
            var roll = Math.floor(Math.random() * game.sides) + 1;
            if (roll > game.sides) {
                // Accounts for 1 in a million chance random() generated a 1.0
                roll = game.sides;
            }
            total += roll;
            rolls.push(roll);
        }

        // Format roll results
        var results = '';
        var multiLine = rolls.length > 5;
        for (var i = 0; i < rolls.length; i++) {
            if (i > 0) {
                results += ' . ';
            }
            results += rolls[i];
        }

        // Render results using a card
        var card = new builder.HeroCard(session)
            .subtitle(game.count > 1 ? 'card_subtitle_plural' : 'card_subtitle_singular', game)
            .buttons([
                builder.CardAction.imBack(session, 'roll again', 'Roll Again'),
                builder.CardAction.imBack(session, 'new game', 'New Game')
            ]);
        if (multiLine) {
            //card.title('card_title').text('\n\n' + results + '\n\n');
            card.text(results);
        } else {
            card.title(results);
        }
        var msg = new builder.Message(session).addAttachment(card);

        // Determine bots reaction for speech purposes
        var reaction = 'normal';
        var min = game.count;
        var max = game.count * game.sides;
        var score = total/max;
        if (score == 1.0) {
            reaction = 'best';
        } else if (score == 0) {
            reaction = 'worst';
        } else if (score <= 0.3) {
            reaction = 'bad';
        } else if (score >= 0.8) {
            reaction = 'good';
        }

        // Check for special craps rolls
        if (game.type == 'craps') {
            switch (total) {
                case 2:
                case 3:
                case 12:
                    reaction = 'craps_lose';
                    break;
                case 7:
                    reaction = 'craps_seven';
                    break;
                case 11:
                    reaction = 'craps_eleven';
                    break;
                default:
                    reaction = 'craps_retry';
                    break;
            }
        }

        // Build up spoken response
        var spoken = '';
        if (game.turn == 0) {
            spoken += session.gettext('start_' + game.type + '_game_ssml') + ' ';
        } 
        spoken += session.gettext(reaction + '_roll_reaction_ssml');
        msg.speak(ssml.speak(spoken));

        // Increment number of turns and store game to roll again
        game.turn++;
        session.conversationData.game = game;

        /**
         * Send card and bot's reaction to user. 
         */

        msg.inputHint(builder.InputHint.acceptingInput);
        session.send(msg).endDialog();
    } else {
        // User started session with "roll again" so let's just send them to
        // the 'CreateGameDialog'
        session.replaceDialog('CreateGameDialog');
    }
}).triggerAction({ matches: /(roll|role|throw|shoot) again/i });

```

## Next steps
If you have a bot running locally or deployed in the cloud, you can invoke it from Cortana. See [Test a Cortana skill](../bot-service-debug-cortana-skill.md) for the steps required to try out your Cortana Skill.


## Additional resources
* [The Cortana Skills Kit][CortanaGetStarted]
* [Add speech to messages](bot-builder-nodejs-text-to-speech.md)
* [SSML Reference][SSMLRef]
* [Voice design best practices for Cortana][VoiceDesign]
* [Card design best practices for Cortana][CardDesign]
* [Cortana Dev Center][CortanaDevCenter]
* [Testing and debugging best practices for Cortana][Cortana-TestBestPractice]


[CortanaGetStarted]: /cortana/getstarted
[BFPortal]: https://dev.botframework.com/


[SSMLRef]: https://aka.ms/cortana-ssml
[IMessage]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage.html
[Send]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#send
[CortanaDevCenter]: https://developer.microsoft.com/en-us/cortana

[CortanaSpecificEntities]: https://aka.ms/lgvcto
[CortanaAuth]: https://aka.ms/vsdqcj

[InvocationNameGuidelines]: https://aka.ms/cortana-invocation-guidelines
[VoiceDesign]: https://aka.ms/cortana-design-voice
[CardDesign]: https://aka.ms/cortana-design-card
[Cortana-Debug]: https://aka.ms/cortana-enable-debug
[Cortana-Publish]: https://aka.ms/cortana-publish


[CortanaChannel]: https://aka.ms/bot-cortana-channel
[Cortana-TestBestPractice]: https://aka.ms/cortana-test-best-practice

---
title: Building a Cortana skill using Node.js | Microsoft Docs
description: Learn core concepts for building a Cortana skill in the Bot Framework SDK for Node.js.
keywords: Bot Framework, Cortana skill, speech, Node.js, Bot Builder, SDK, key concepts, core concepts
author: DeniseMak
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 02/10/2019
monikerRange: 'azure-bot-service-3.0'
#ROBOTS: Index
---

# Key concepts for building a bot for Cortana skills using Node.js
 
[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!NOTE]
> This article is preliminary content and will be updated.

This article introduces key concepts for building a Cortana skill in the Bot Framework SDK for Node.js. 

## What is a Cortana skill?
A Cortana skill is a bot you can invoke by using a Cortana client, like the one built in to Windows 10. The user launches the bot by saying some keywords or phrases associated with the bot. You use the Bot Framework Portal to configure which keywords are used to launch your bot. 

Cortana can be thought of as speech-enabled channel that can send and receive voice messages in addition to textual conversation. A bot that is published as a Cortana skill should be designed for speech as well as text. The Bot Framework provides methods for specifying Speech Synthesis Markup Language (SSML) to define spoken messages that your bot sends.

## Acknowledge user utterances 

<!-- Establishing conversational understanding -->
<!-- Placeholder: In this section, describe how you have to write your speech to sound natural -->


When you create a speech-enabled bot, you should try to establish common ground and mutual understanding in the conversation. 
The bot should "ground" the user's utterances by indicating that the user was heard and understood.

Users get confused if a system fails to ground their utterances. For example, the following conversation can be a bit confusing when the bot asks "What's next?":

> **Cortana**: Did you want to review some more of your profile?  
> **User**: No.  
> **Cortana**: What's next?

If the bot adds an "Okay" as acknowledgment, it's friendlier for the user:

> **Cortana**: Did you want to review some more of your profile?  
> **User**: No.  
> **Cortana**: **Okay**, what's next?

Degrees of grounding, from weakest to strongest:

1. Continued attention
2. Next relevant contribution
3. Acknowledgment: Minimal response or continuer: "yeah", "uh-huh", "okay", "great"
4. Demonstrate: Indicate understanding by reformulation, completion.
5. Display: Repeat all or part.

### Acknowledgement and next relevant contribution

> **User**: I need to travel in May.  
> **Cortana**: **Okay**. What day in May did you want to travel?  
> **User**: Well, I need to be there from the 12th to the 15th?  
> **Cortana**: **Okay**. What city are you flying into ?  

### Grounding by demonstration

> **User**: I need to travel in May.  
> **Cortana**: And, **what day** in May did you want to travel?  
> **User**: Okay, I need to be there from the 12th to the 15th?  
> **Cortana**: **And** you're flying into what city?  
    
### Closure

The bot performing an action should present evidence of successful performance. It's also important to indicate failure or understanding. 

* Non-speech closure: If you push an elevator button, its light turns on.  
This is awo step process:
    * Presentation (when you press the button)
    * Acceptance (when the button lights up)

## Differences in content presentation
Keep in mind that Cortana is supported on a variety of devices, only some of which have screens. One of the things you need to consider when designing your speech-enabled bot is that the spoken dialogue often will not be the same as the text messages your bot displays.
<!-- If there are differences in what the bot will say, in the text vs the speak fields of a prompt or in a waterfall, for example, discuss them here.

## Speech

You bot uses the **session.say** method to speak to the user. The speak method has three overloads:
* If you pass only one parameter to **session.say**, it can be a text parameter.
* If you pass two parameters to **session.say**, it can take text and SSML.
* If you pass three parameters, the third parameter takes an options structure that specifies all the options you can pass to build an **IMessage** object.

```javascript
var bot = new builder.UniversalBot(connector, function (session) {
    session.say("Hello... I'm a decision making bot.'.", 
        ssml.speak("Hello. I can help you answer all of life's tough questions."));
    session.replaceDialog('rootMenu');
});

```
## Speech in messages

The **IMessage** object provides a **speak** property for SSML. It can be used to play a .wav file.

The **inputHint** property helps indicate to Cortana whether your bot is expecting input. If you're using a built-in prompt, this value is automatically set to the default of **expectingInput**.

The **inputHint** property can take the following values: 
* **expectingInput**: Indicates that the bot is actively expecting a response from the user. Cortana listens for the user to speak into the microphone.
* **acceptingInput**: Indicates that the bot is passively ready for input but is not waiting on a response. Cortana accepts input from the user if the user holds down the microphone button.
* **ignoringInput**: Cortana is ignoring input. Your bot may send this hint if it is actively processing a request and will ignore input from users until the request is complete.

Prompts must use the `speak:` option.

```javascript
        builder.Prompts.choice(session, "Decision Options", choices, {
            listStyle: builder.ListStyle.button,
            speak: ssml.speak("How would you like me to decide?")
        });
```

Prompts.number has *ordinal support*, meaning that you can say "the last", "the first", "the next-to-last" to choose an item in a list.

## Using synonyms

<!-- Axl Rose example -->
```javascript   
         var choices = [
            { 
                value: 'flipCoinDialog',
                action: { title: "Flip A Coin" },
                synonyms: 'toss coin|flip quarter|toss quarter'
            },
            {
                value: 'rollDiceDialog',
                action: { title: "Roll Dice" },
                synonyms: 'roll die|shoot dice|shoot die'
            },
            {
                value: 'magicBallDialog',
                action: { title: "Magic 8-Ball" },
                synonyms: 'shake ball'
            },
            {
                value: 'quit',
                action: { title: "Quit" },
                synonyms: 'exit|stop|end'
            }
        ];
        builder.Prompts.choice(session, "Decision Options", choices, {
            listStyle: builder.ListStyle.button,
            speak: ssml.speak("How would you like me to decide?")
        });
```

## Configuring your bot

## Prompts

## Additional resources

Cortana documentation: [Cortana Skills Documentation](/cortana/skills/)

Cortana SSML reference: [Speech Synthesis Markup Language (SSML) reference](/cortana/skills/speech-synthesis-markup-language)

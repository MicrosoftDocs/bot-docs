---
title: Send welcome message to users | Microsoft Docs
description: Learn how to develop your bot to provide a welcoming user experience.
keywords: overview, develop, user experience, welcome, personalized experience, C#, JS, welcome message, bot, greet, greeting 
author: DanDev33
ms.author: v-dashel
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 02/19/2019
monikerRange: 'azure-bot-service-4.0'
---
# Send welcome message to users

[!INCLUDE[applies-to](../includes/applies-to.md)]

The primary goal when creating any bot is to engage your user in a meaningful conversation. One of the best ways to achieve this goal is to ensure that from the moment a user first connects, they understand your bot’s main purpose and capabilities, the reason your bot was created. This article provides code examples to help you welcome users to your bot.

## Prerequisites

- Understand [bot basics](bot-builder-basics.md). 
- A copy of the **Welcome user sample** in either [C#](https://aka.ms/bot-welcome-sample-cs) or [JS](https://aka.ms/bot-welcome-sample-js). The code from the sample is used to explain how to send welcome messages.

## Same welcome for different channels

A welcome message should be generated whenever your users first interacts with your bot. To achieve this, you can monitor your bot’s Activity types and watch for new connections. Each new connection can generate up to two conversation update activities depending on the channel.

- One when the user’s bot is connected to the conversation.
- One when the user joins the conversation.

It is tempting to simply generate a welcome message whenever a new conversation update is detected, but that can lead to unexpected results when your bot is accessed across a variety of channels.

Some channels create one conversation update when a user initially connects to that channel, and a separate conversation update only after an initial input message is received from the user. Other channels generate both these activities when the user initially connects to the channel. If you simply watch for a conversation update event and display a welcome message on a channel with two conversation update activities, your user could receive the following:

![Double Welcome Message](./media/double_welcome_message.png)

This duplicate message can be avoided by generating an initial welcome message for only the second conversation update event. The second event can be detected when both:

- A conversation update event has occurred.
- A new member (user) has been added to the conversation.

The code sample below watches for any new *conversation update activity* and sends only one welcome message per new user joining the conversation. Upon your bot's first _conversationUpdate_ event the bot itself is added as _Recipient_ of activity for your channel. The code the checks if an added Member == _turnContext.Activity.Recipient.Id_. If true, it has detected the initial conversation update event and skips the code that looks for newly connected users.

[!INCLUDE [alert-await-send-activity](../includes/alert-await-send-activity.md)]

## [C#](#tab/csharp)

Within the C# sample code, **Startup.cs** has defined 'WelcomeUserStateAccessors' as a service/singleton and added 'UserState' to the application state. We will now use these to create a state object for a given user in a conversation and its accessor.

```csharp
/// The state object is used to keep track of various state related to a user in a conversation.
/// In this example, we are tracking if the bot has replied to customer first interaction.
public class WelcomeUserState
{
    public bool DidBotWelcomeUser { get; set; } = false;
}

/// Initializes a new instance of the <see cref="WelcomeUserStateAccessors"/> class.
public class WelcomeUserStateAccessors
{
    public WelcomeUserStateAccessors(UserState userState)
    {
        UserState = userState ?? throw new ArgumentNullException(nameof(userState));
    }

    public static string WelcomeUserName { get; } = $"{nameof(WelcomeUserStateAccessors)}.WelcomeUserState";

    public IStatePropertyAccessor<WelcomeUserState> WelcomeUserState { get; set; }

    public UserState UserState { get; }
}
```

In **WelcomeUserBot**, we check for an activity update to see if a new user has been added to the conversation and then send them a welcome message.

```csharp
// Messages sent to the user.
private const string WelcomeMessage = @"This is a simple Welcome Bot sample.This bot will introduce you
                                        to welcoming and greeting users. You can say 'intro' to see the
                                        introduction card. If you are running this bot in the Bot Framework
                                        Emulator, press the 'Start Over' button to simulate user joining
                                        a bot or a channel";

private const string InfoMessage = @"You are seeing this message because the bot received at least one
                                    'ConversationUpdate' event, indicating you (and possibly others)
                                    joined the conversation. If you are using the emulator, pressing
                                    the 'Start Over' button to trigger this event again. The specifics
                                    of the 'ConversationUpdate' event depends on the channel. You can
                                    read more information at:
                                        https://aka.ms/about-botframework-welcome-user";

private const string PatternMessage = @"It is a good pattern to use this event to send general greeting
                                        to user, explaining what your bot can do. In this example, the bot
                                        handles 'hello', 'hi', 'help' and 'intro. Try it now, type 'hi'";

// The bot state accessor object. Use this to access specific state properties.
private readonly WelcomeUserStateAccessors _welcomeUserStateAccessors;

// Initializes a new instance of the <see cref="WelcomeUserBot"/> class.
public WelcomeUserBot(WelcomeUserStateAccessors statePropertyAccessor)
{
    _welcomeUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");
}

// Every conversation turn for our WelcomeUser Bot will call this method, including
// any type of activities such as ConversationUpdate or ContactRelationUpdate which
// are sent when a user joins a conversation.
// This bot doesn't use any dialogs; it's "single turn" processing, meaning a single
// request and response.
// This bot uses UserState to keep track of first message a user sends.
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = new CancellationToken())
{
    // use state accessor to extract the didBotWelcomeUser flag
    var didBotWelcomeUser = await _welcomeUserStateAccessors.WelcomeUserState.GetAsync(turnContext, () => new WelcomeUserState());

    if (turnContext.Activity.Type == ActivityTypes.Message)
    {
        // Your bot should proactively send a welcome message to a personal chat the first time
        // (and only the first time) a user initiates a personal chat with your bot.
        if (didBotWelcomeUser.DidBotWelcomeUser == false)
        {
            didBotWelcomeUser.DidBotWelcomeUser = true;
            // Update user state flag to reflect bot handled first user interaction.
            await _welcomeUserStateAccessors.WelcomeUserState.SetAsync(turnContext, didBotWelcomeUser);
            await _welcomeUserStateAccessors.UserState.SaveChangesAsync(turnContext);

            // the channel should sends the user name in the 'From' object
            var userName = turnContext.Activity.From.Name;

            await turnContext.SendActivityAsync(
                $"You are seeing this message because this was your first message ever to this bot.",
                cancellationToken: cancellationToken);
            await turnContext.SendActivityAsync(
                $"It is a good practice to welcome the user and provide personal greeting. For example, welcome {userName}.",
                cancellationToken: cancellationToken);
        }
    }

    // Greet when users are added to the conversation.
    else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
    {
        if (turnContext.Activity.MembersAdded != null)
        {
            // Iterate over all new members added to the conversation
            foreach (var member in turnContext.Activity.MembersAdded)
            {
                // Greet anyone that was not the target (recipient) of this message
                // the 'bot' is the recipient for events from the channel,
                // turnContext.Activity.MembersAdded == turnContext.Activity.Recipient.Id indicates the
                // bot was added to the conversation.
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync($"Hi there - {member.Name}. {WelcomeMessage}", cancellationToken: cancellationToken);
                    await turnContext.SendActivityAsync(InfoMessage, cancellationToken: cancellationToken);
                    await turnContext.SendActivityAsync(PatternMessage, cancellationToken: cancellationToken);
                }
            }
        }
    }
    else
    {
        // Default behavior for all other type of activities.
        await turnContext.SendActivityAsync($"{turnContext.Activity.Type} activity detected");
    }

    // save any state changes made to your state objects.
    await _welcomeUserStateAccessors.UserState.SaveChangesAsync(turnContext);
}
```

## [JavaScript](#tab/js)

This JavaScript code sends a welcome message when a user is added. This is done by checking the conversation activity and verify that a new member was added to the conversation.

```javascript
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// Import required Bot Framework classes.
const { ActivityTypes } = require('botbuilder');
const { CardFactory } = require('botbuilder');

// Adaptive Card content
const IntroCard = require('./resources/IntroCard.json');

// Welcomed User property name
const WELCOMED_USER = 'welcomedUserProperty';

class WelcomeBot {
    /**
     *
     * @param {UserState} User state to persist boolean flag to indicate
     *                    if the bot had already welcomed the user
     */
    constructor(userState) {
        // Creates a new user property accessor.
        // See https://aka.ms/about-bot-state-accessors to learn more about the bot state and state accessors.
        this.welcomedUserProperty = userState.createProperty(WELCOMED_USER);

        this.userState = userState;
    }
    /**
     *
     * @param {TurnContext} context on turn context object.
     */
    async onTurn(turnContext) {
        // See https://aka.ms/about-bot-activity-message to learn more about the message and other activity types.
        if (turnContext.activity.type === ActivityTypes.Message) {
            // Read UserState. If the 'DidBotWelcomedUser' does not exist (first time ever for a user)
            // set the default to false.
            const didBotWelcomedUser = await this.welcomedUserProperty.get(turnContext, false);

            // Your bot should proactively send a welcome message to a personal chat the first time
            // (and only the first time) a user initiates a personal chat with your bot.
            if (didBotWelcomedUser === false) {
                // The channel should send the user name in the 'From' object
                let userName = turnContext.activity.from.name;
                await turnContext.sendActivity('You are seeing this message because this was your first message ever sent to this bot.');
                await turnContext.sendActivity(`It is a good practice to welcome the user and provide personal greeting. For example, welcome ${ userName }.`);

                // Set the flag indicating the bot handled the user's first message.
                await this.welcomedUserProperty.set(turnContext, true);
            } else {
                // ...
            }
            // Save state changes
            await this.userState.saveChanges(turnContext);
        } else if (turnContext.activity.type === ActivityTypes.ConversationUpdate) {
            // Send greeting when users are added to the conversation.
            await this.sendWelcomeMessage(turnContext);
        } else {
            // Generic message for all other activities
            await turnContext.sendActivity(`[${ turnContext.activity.type } event detected]`);
        }
    }

    /**
     * Sends welcome messages to conversation members when they join the conversation.
     * Messages are only sent to conversation members who aren't the bot.
     * @param {TurnContext} turnContext
     */
    async sendWelcomeMessage(turnContext) {
        // Do we have any new members added to the conversation?
        if (turnContext.activity.membersAdded.length !== 0) {
            // Iterate over all new members added to the conversation
            for (let idx in turnContext.activity.membersAdded) {
                // Greet anyone that was not the target (recipient) of this message.
                // Since the bot is the recipient for events from the channel,
                // context.activity.membersAdded === context.activity.recipient.Id indicates the
                // bot was added to the conversation, and the opposite indicates this is a user.
                if (turnContext.activity.membersAdded[idx].id !== turnContext.activity.recipient.id) {
                    await turnContext.sendActivity(`Welcome to the 'Welcome User' Bot. This bot will introduce you to welcoming and greeting users.`);
                    await turnContext.sendActivity("You are seeing this message because the bot received at least one 'ConversationUpdate'" +
                                            'event,indicating you (and possibly others) joined the conversation. If you are using the emulator, ' +
                                            "pressing the 'Start Over' button to trigger this event again. The specifics of the 'ConversationUpdate' " +
                                            'event depends on the channel. You can read more information at https://aka.ms/about-botframework-welcome-user');
                    await turnContext.sendActivity(`It is a good pattern to use this event to send general greeting to user, explaining what your bot can do. ` +
                                            `In this example, the bot handles 'hello', 'hi', 'help' and 'intro. ` +
                                            `Try it now, type 'hi'`);
                }
            }
        }
    }
}

module.exports.WelcomeBot = WelcomeBot;
```

---

## Discard initial user input

It is also important to consider when your user’s input might actually contain useful information, and this too can vary per channel. To ensure your user has a good experience on all possible channels, we check the status flag _didBotWelcomeUser_ and if this is "false", we avoid processing this initial user input. We instead provide the user with an initial prompt for their response. The variable _didBotWelcomeUser_ is then set to "true" and our code processes the user input from all additional message activities.

## [C#](#tab/csharp)

```csharp
public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = new CancellationToken())
{
    // use state accessor to extract the didBotWelcomeUser flag
    var didBotWelcomeUser = await _welcomeUserStateAccessors.WelcomeUserState.GetAsync(turnContext, () => new WelcomeUserState());

    if (turnContext.Activity.Type == ActivityTypes.Message)
    {
        if (didBotWelcomeUser.DidBotWelcomeUser == false)
        {
            // See previous code sample.
        }
        else
        {
            // This example hard-codes specific utterances. You should use LUIS or QnA for more advanced language understanding.
            var text = turnContext.Activity.Text.ToLowerInvariant();
            switch (text)
            {
                case "hello":
                case "hi":
                    await turnContext.SendActivityAsync($"You said {text}.", cancellationToken: cancellationToken);
                    break;
                case "intro":
                case "help":
                default:
                    await turnContext.SendActivityAsync(WelcomeMessage, cancellationToken: cancellationToken);
                    break;
            }
        }
    }

    // Greet when users are added to the conversation.
    // Note that all channels do not send the conversation update activity.
    // If you find that this bot works in the emulator, but does not in
    // another channel the reason is most likely that the channel does not
    // send this activity.
    else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
    {
        // See previous code sample.
    }

    // save any state changes made to your state objects.
    await _welcomeUserStateAccessors.UserState.SaveChangesAsync(turnContext);
}
```

## [JavaScript](#tab/js)

```javascript
class MainDialog
{
    // Previous Code Sample
    async onTurn(turnContext)
    {
        // See https://aka.ms/about-bot-activity-message to learn more about the message and other activity types.
        if (turnContext.activity.type === ActivityTypes.Message) {
            // Previous Code Sample
            if (didBotWelcomeUser === false) {
                // Previous Code Sample
            } else  {
                // This example uses an exact match on user's input utterance.
                // Consider using LUIS or QnA for Natural Language Processing.
                let text = turnContext.activity.text.toLowerCase();
                switch (text) {
                case 'hello':
                case 'hi':
                    await turnContext.sendActivity(`You said "${ turnContext.activity.text }"`);
                    break;
                case 'intro':
                case 'help':
                    await turnContext.sendActivity({
                        text: 'Intro Adaptive Card',
                        attachments: [CardFactory.adaptiveCard(IntroCard)]
                    });
                    break;
                default :
                    await turnContext.sendActivity(`This is a simple Welcome Bot sample. You can say 'intro' to
                                                        see the introduction card. If you are running this bot in the Bot
                                                        Framework Emulator, press the 'Start Over' button to simulate user joining a bot or a channel`);
                }
            }
        }
       // Previous Sample Code
    }
}
```

---

## Using Adaptive Card Greeting

Another way to greeting users is using adaptive Card Greeting. You can learn more about Adaptive Card Greetings here [Send an Adaptive Card](./bot-builder-howto-add-media-attachments.md).

## [C#](#tab/csharp)

```csharp
// Sends an adaptive card greeting.
private static async Task SendIntroCardAsync(ITurnContext turnContext, CancellationToken cancellationToken)
{
    var response = turnContext.Activity.CreateReply();

    var card = new HeroCard();
    card.Title = "Welcome to Bot Framework!";
    card.Text = @"Welcome to Welcome Users bot sample! This Introduction card
                    is a great way to introduce your Bot to the user and suggest
                    some things to get them started. We use this opportunity to
                    recommend a few next steps for learning more creating and deploying bots.";
    card.Images = new List<CardImage>() { new CardImage("https://aka.ms/bf-welcome-card-image") };
    card.Buttons = new List<CardAction>()
    {
        new CardAction(ActionTypes.OpenUrl,
            "Get an overview", null, "Get an overview", "Get an overview",
            "https://docs.microsoft.com/en-us/azure/bot-service/?view=azure-bot-service-4.0"),
        new CardAction(ActionTypes.OpenUrl,
            "Ask a question", null, "Ask a question", "Ask a question",
            "https://stackoverflow.com/questions/tagged/botframework"),
        new CardAction(ActionTypes.OpenUrl,
            "Learn how to deploy", null, "Learn how to deploy", "Learn how to deploy",
            "https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-howto-deploy-azure?view=azure-bot-service-4.0"),
    };

    response.Attachments = new List<Attachment>() { card.ToAttachment() };
    await turnContext.SendActivityAsync(response, cancellationToken);
}
```

Next, we can send the card by using the following await command. Let's put this into the bots _switch (text) case "help"_.

```csharp
switch (text)
{
    case "hello":
    case "hi":
        await turnContext.SendActivityAsync($"You said {text}.", cancellationToken: cancellationToken);
        break;
    case "intro":
    case "help":
        await SendIntroCardAsync(turnContext, cancellationToken);
        break;
    default:
        await turnContext.SendActivityAsync(WelcomeMessage, cancellationToken: cancellationToken);
        break;
}
```

## [JavaScript](#tab/js)

First we want to add our Adaptive Card to the bot at the top of our _index.js_ right below our Imports.

```javascript
// Adaptive Card content
const IntroCard = require('./Resources/IntroCard.json');
```

Next we can simply use the below code in our _switch (text)_ _case "help"_ section of our bot to respond to the users prompt with the adaptive card.

```javascript
switch (text)
{
    case "hello":
    case "hi":
        await turnContext.sendActivity(`You said "${turnContext.activity.text}"`);
        break;
    case "intro":
    case "help":
        await turnContext.sendActivity({
            text: 'Intro Adaptive Card',
            attachments: [CardFactory.adaptiveCard(IntroCard)]
        });
        break;
    default :
        await turnContext.sendActivity(`This is a simple Welcome Bot sample. You can say 'intro' to 
                                        see the introduction card. If you are running this bot in the Bot 
                                        Framework Emulator, press the 'Start Over' button to simulate user joining a bot or a channel`);
}
```

---

## Test the bot

Refer to the [README](https://aka.ms/bot-welcome-sample-cs) file for instructions on running and testing the bot.

## Next steps

> [!div class="nextstepaction"]
> [Gather user input](bot-builder-prompts.md)

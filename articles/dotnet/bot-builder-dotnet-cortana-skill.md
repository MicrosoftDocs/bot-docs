---
title: Build a Cortana skill using .NET | Microsoft Docs
description: Learn core concepts for building a Cortana skill in the Bot Framework SDK for .NET.
keywords: Bot Framework, Cortana skill, speech, .NET, SDK, key concepts, core concepts
author: DeniseMak
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
monikerRange: 'azure-bot-service-3.0'

#ROBOTS: Index
---

# Build a speech-enabled bot with Cortana skills

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-cortana-skill.md)
> - [Node.js](../nodejs/bot-builder-nodejs-cortana-skill.md)


The Bot Framework SDK for .NET enables you to a build speech-enabled bot by connecting it to the Cortana channel as a Cortana skill. 


> [!TIP]
> For more information on what a skill is, and what they can do, see [The Cortana Skills Kit][CortanaGetStarted].

Creating a Cortana skill using Bot Framework requires very little Cortana-specific knowledge and primarily consists of building a bot. One of the likely key differences from other bots that you may have created in the past is that Cortana has both a visual and an audio component. For the visual component, Cortana provides an area of the canvas for rendering content such as cards. For the audio component, you provide text or SSML in your bot's messages, which Cortana reads to the user, giving your bot a voice. 

> [!NOTE]
> Cortana is available on many different devices. Some have a screen while others, like a standalone speaker, might not. You should make sure that your bot is capable of handling both scenarios. See [Cortana-specific entities][CortanaSpecificEntities] to learn how to check device information.

## Adding speech to your bot

Spoken messages from your bot are represented as Speech Synthesis Markup Language (SSML). The Bot Framework SDK lets you include SSML in your bot's responses to control what the bot says, in addition to what it shows.  You can also control the state of Cortana's microphone, by specifying whether your bot is accepting, expecting, or ignoring user input.

Set the `Speak` property of the `IMessageActivity` object to specify a message for Cortana to say. If you specify plain text, Cortana determines how the words are pronounced. 

```cs
Activity reply = activity.CreateReply("This is the text that Cortana displays."); 
reply.Speak = "This is the text that Cortana will say.";
```

If you want more control over pitch, tone, and emphasis, format the `Speak` property as [Speech Synthesis Markup Language (SSML)](http://www.w3.org/TR/speech-synthesis/).  

The following code example specifies that the word "text" should be spoken with a moderate amount of emphasis:
```cs
Activity reply = activity.CreateReply("This is the text that will be displayed.");
reply.Speak = "<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"en-US\">This is the <emphasis level=\"moderate\">text</emphasis> that will be spoken.</speak>";
```


The **InputHint** property helps indicate to Cortana whether your bot is expecting input. The default value is **ExpectingInput** for a prompt, and **AcceptingInput** for other types of responses.


| Value | Description |
|------|------|
| **AcceptingInput** | Your bot is passively ready for input but is not waiting on a response. Cortana accepts input from the user if the user holds down the microphone button.|
| **ExpectingInput** | Indicates that the bot is actively expecting a response from the user. Cortana listens for the user to speak into the microphone.  |
| **IgnoringInput** | Cortana is ignoring input. Your bot may send this hint if it is actively processing a request and will ignore input from users until the request is complete.  |

<!-- TODO: tip about time limit and batching -->

This example shows how to let Cortana know that user input is expected. The microphone will be left open.
```cs
// Add an InputHint to let Cortana know to expect user input
Activity reply = activity.CreateReply("This is the text that will be displayed."); 
reply.Speak = "This is the text that will be spoken.";
reply.InputHint = InputHints.ExpectingInput;
```



## Display cards in Cortana

In addition to spoken responses, Cortana can also display card attachments. Cortana supports the following rich cards:

| Card type | Description |
|----|----|
| [HeroCard][heroCard] | A card that typically contains a single large image, one or more buttons, and text. |
| [ThumbnailCard][thumbnailCard] | A card that typically contains a single thumbnail image, one or more buttons, and text. |
| [ReceiptCard][receiptCard] | A card that enables a bot to provide a receipt to the user. It typically contains the list of items to include on the receipt, tax and total information, and other text. |
| [SignInCard][signinCard] | A card that enables a bot to request that a user sign-in. It typically contains text and one or more buttons that the user can click to initiate the sign-in process. |


See [Card design best practices][CardDesign] to see what these cards look like inside Cortana. For an example of how to use a rich card in a bot, see [Add rich card attachments to messages](bot-builder-dotnet-add-rich-card-attachments.md). 

<!--
The following code demonstrates how to add the `Speak` and `InputHint` properties to a message containing a `HeroCard`.
-->


## Sample: RollerSkill
The code in the following sections comes from a sample Cortana skill for rolling dice. Download the full code for the bot from the [BotBuilder-Samples repository](https://github.com/Microsoft/BotBuilder-Samples/).

You invoke the skill by saying its [invocation name][InvocationNameGuidelines] to Cortana. For the roller skill, after you [add the bot to the Cortana channel][CortanaChannel] and register it as a Cortana skill, you can invoke it by telling Cortana "Ask Roller" or "Ask Roller to roll dice".

### Explore the code



To invoke the appropriate dialogs, the activity handlers defined in `RootDispatchDialog.cs` use regular expressions to match the user's input. For example, the handler in the following example is triggered if the user says something like "I'd like to roll some dice". Synonyms are included in the regular expression so that similar utterances will trigger the dialog.
```cs
        [RegexPattern("(roll|role|throw|shoot).*(dice|die|dye|bones)")]
        [RegexPattern("new game")]
        [ScorableGroup(1)]
        public async Task NewGame(IDialogContext context, IActivity activity)
        {
            context.Call(new CreateGameDialog(), AfterGameCreated);
        }
```

The `CreateGameDialog` dialog sets up a custom game for the bot to play. It uses a `PromptDialog` to ask the user how many sides they want the dice to have and then how many should be rolled. Note that the `PromptOptions` object that is used to initialize the prompt contains a `speak` property for the spoken version of the prompt.

```cs
    [Serializable]
    public class CreateGameDialog : IDialog<GameData>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.UserData.SetValue<GameData>(Utils.GameDataKey, new GameData());

            var descriptions = new List<string>() { "4 Sides", "6 Sides", "8 Sides", "10 Sides", "12 Sides", "20 Sides" };
            var choices = new Dictionary<string, IReadOnlyList<string>>()
             {
                { "4", new List<string> { "four", "for", "4 sided", "4 sides" } },
                { "6", new List<string> { "six", "sex", "6 sided", "6 sides" } },
                { "8", new List<string> { "eight", "8 sided", "8 sides" } },
                { "10", new List<string> { "ten", "10 sided", "10 sides" } },
                { "12", new List<string> { "twelve", "12 sided", "12 sides" } },
                { "20", new List<string> { "twenty", "20 sided", "20 sides" } }
            };

            var promptOptions = new PromptOptions<string>(
                Resources.ChooseSides,
                choices: choices,
                descriptions: descriptions,
                speak: SSMLHelper.Speak(Utils.RandomPick(Resources.ChooseSidesSSML))); // spoken prompt

            PromptDialog.Choice(context, this.DiceChoiceReceivedAsync, promptOptions);
        }

        private async Task DiceChoiceReceivedAsync(IDialogContext context, IAwaitable<string> result)
        {
            GameData game;
            if (context.UserData.TryGetValue<GameData>(Utils.GameDataKey, out game))
            {
                int sides;
                if (int.TryParse(await result, out sides))
                {
                    game.Sides = sides;
                    context.UserData.SetValue<GameData>(Utils.GameDataKey, game);
                }

                var promptText = string.Format(Resources.ChooseCount, sides);

                var promptOption = new PromptOptions<long>(promptText, choices: null, speak: SSMLHelper.Speak(Utils.RandomPick(Resources.ChooseCountSSML)));

                var prompt = new PromptDialog.PromptInt64(promptOption);
                context.Call<long>(prompt, this.DiceNumberReceivedAsync);
            }
        }

        private async Task DiceNumberReceivedAsync(IDialogContext context, IAwaitable<long> result)
        {
            GameData game;
            if (context.UserData.TryGetValue<GameData>(Utils.GameDataKey, out game))
            {
                game.Count = await result;
                context.UserData.SetValue<GameData>(Utils.GameDataKey, game);
            }

            context.Done(game);
        }
    }
```

The `PlayGameDialog` renders the results both by displaying them in a `HeroCard` and building a spoken message to say using the `Speak` method.

```cs
   [Serializable]
    public class PlayGameDialog : IDialog<object>
    {
        private const string RollAgainOptionValue = "roll again";

        private const string NewGameOptionValue = "new game";

        private GameData gameData;

        public PlayGameDialog(GameData gameData)
        {
            this.gameData = gameData;
        }

        public async Task StartAsync(IDialogContext context)
        {
            if (this.gameData == null)
            {
                if (!context.UserData.TryGetValue<GameData>(Utils.GameDataKey, out this.gameData))
                {
                    // User started session with "roll again" so let's just send them to
                    // the 'CreateGameDialog'
                    context.Done<object>(null);
                }
            }

            int total = 0;
            var randomGenerator = new Random();
            var rolls = new List<int>();

            // Generate Rolls
            for (int i = 0; i < this.gameData.Count; i++)
            {
                var roll = randomGenerator.Next(1, this.gameData.Sides);
                total += roll;
                rolls.Add(roll);
            }

            // Format rolls results
            var result = string.Join(" . ", rolls.ToArray());
            bool multiLine = rolls.Count > 5;

            var card = new HeroCard()
            {
                Subtitle = string.Format(
                    this.gameData.Count > 1 ? Resources.CardSubtitlePlural : Resources.CardSubtitleSingular,
                    this.gameData.Count,
                    this.gameData.Sides),
                Buttons = new List<CardAction>()
                {
                    new CardAction(ActionTypes.ImBack, "Roll Again", value: RollAgainOptionValue),
                    new CardAction(ActionTypes.ImBack, "New Game", value: NewGameOptionValue)
                }
            };

            if (multiLine)
            {
                card.Text = result;
            }
            else
            {
                card.Title = result;
            }

            var message = context.MakeMessage();
            message.Attachments = new List<Attachment>()
            {
                card.ToAttachment()
            };

            // Determine bots reaction for speech purposes
            string reaction = "normal";

            var min = this.gameData.Count;
            var max = this.gameData.Count * this.gameData.Sides;
            var score = total / max;
            if (score == 1)
            {
                reaction = "Best";
            }
            else if (score == 0)
            {
                reaction = "Worst";
            }
            else if (score <= 0.3)
            {
                reaction = "Bad";
            }
            else if (score >= 0.8)
            {
                reaction = "Good";
            }

            // Check for special craps rolls
            if (this.gameData.Type == "Craps")
            {
                switch (total)
                {
                    case 2:
                    case 3:
                    case 12:
                        reaction = "CrapsLose";
                        break;
                    case 7:
                        reaction = "CrapsSeven";
                        break;
                    case 11:
                        reaction = "CrapsEleven";
                        break;
                    default:
                        reaction = "CrapsRetry";
                        break;
                }
            }

            // Build up spoken response
            var spoken = string.Empty;
            if (this.gameData.Turns == 0)
            {
                spoken += Utils.RandomPick(Resources.ResourceManager.GetString($"Start{this.gameData.Type}GameSSML"));
            }

            spoken += Utils.RandomPick(Resources.ResourceManager.GetString($"{reaction}RollReactionSSML"));

            message.Speak = SSMLHelper.Speak(spoken);

            // Increment number of turns and store game to roll again
            this.gameData.Turns++;
            context.UserData.SetValue<GameData>(Utils.GameDataKey, this.gameData);

            // Send card and bots reaction to user.
            message.InputHint = InputHints.AcceptingInput;
            await context.PostAsync(message);

            context.Done<object>(null);
        }
    }
```
## Next steps

If your bot is running locally or deployed in the cloud, you can invoke it from Cortana. See [Test a Cortana skill](../bot-service-debug-cortana-skill.md) for the steps required to try out your Cortana skill.


## Additional resources
* [The Cortana Skills Kit][CortanaGetStarted]
* [Add speech to messages](bot-builder-dotnet-text-to-speech.md)
* [SSML Reference][SSMLRef]
* [Voice design best practices for Cortana][VoiceDesign]
* [Card design best practices for Cortana][CardDesign]
* [Cortana Dev Center][CortanaDevCenter]
* [Testing and debugging best practices for Cortana][Cortana-TestBestPractice]
* <a href="/dotnet/api/?view=botbuilder-3.11.0" target="_blank">Bot Framework SDK for .NET Reference</a>

[CortanaGetStarted]: /cortana/getstarted
[BFPortal]: https://dev.botframework.com/

[SSMLRef]: https://aka.ms/cortana-ssml
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

[heroCard]: /dotnet/api/microsoft.bot.connector.herocard

[thumbnailCard]: /dotnet/api/microsoft.bot.connector.thumbnailcard 

[receiptCard]: /dotnet/api/microsoft.bot.connector.receiptcard 

[signinCard]: /dotnet/api/microsoft.bot.connector.signincard 



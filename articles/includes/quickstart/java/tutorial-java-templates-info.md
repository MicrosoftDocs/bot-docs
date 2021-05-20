<!-- Include under "Prerequisites" header after the "quickstart-java-prerequisites.md" file include. in the file:
bot-builder-tutorial-create-basic-bot.md -->

### How to Choose a Template

| Template | When This Template is a Good Choice |
| -------- | -------- |
| Echo&nbsp;Bot  | You are new to Bot Framework v4 and want a working bot with minimal features. |
| Empty&nbsp;Bot  | You are a seasoned Bot Framework v4 developer.  You've built bots before, and want the minimum skeleton of a bot. |
| Core Bot | You are a medium to advanced user of Bot Framework v4 and want to start integrating language understanding as well as multi-turn dialogs in your bots. |

#### Echo Bot Template

The Echo Bot template is slightly more than the a classic "Hello World!" example, but not by much.  This template shows the basic structure of a bot, how a bot receives messages from a user, and how a bot sends messages to a user.  The bot will "echo" back to the user, what the user says to the bot.  It is a good choice for first time, new to Bot Framework v4 developers.

#### Empty Bot Template

The Empty Bot template is the minimal skeleton code for a bot.  It provides a stub `onTurn` handler but does not perform any actions.  If you are experienced writing bots with Bot Framework v4 and want the minimum scaffolding, the Empty template is for you.

#### Core Bot Template

The Core Bot template uses [LUIS](https://www.luis.ai) to implement core AI capabilities, a multi-turn conversation using Dialogs, handles user interruptions, and prompts for and validate requests for information from the user. This template implements a basic three-step waterfall dialog, where the first step asks the user for an input to book a flight, then asks the user if the information is correct, and finally confirms the booking with the user.  Choose this template if want to create an advanced bot that can extract information from the user's input.


<!-- Include under "Prerequisites" header in the files:
bot-builder-tutorial-create-basic-bot.md and bot-builder-java-quickstart.md -->

- Java 1.8 or later
- [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme)
- [Visual Studio Code](https://www.visualstudio.com/downloads) or your favorite IDE, if you want to edit the bot code.
- Install [Maven](https://maven.apache.org/)
- Install [node.js](https://nodejs.org/) version 12.10 or later.
- An Azure account if you want to deploy to [Azure](https://azure.microsoft.com/).

### Templates

Use the Yeoman generator to quickly set up a conversational AI bot using core AI capabilities in the [Bot Framework v4](https://dev.botframework.com). For more information, see [yeoman.io](https://yeoman.io).

The generator supports three different template options as shown below. 

|  Template  |  Description  |
| ---------- |  ---------  |
| Echo&nbsp;Bot | A good template if you want a little more than "Hello World!", but not much more.  This template handles the very basics of sending messages to a bot, and having the bot process the messages by repeating them back to the user.  This template produces a bot that simply "echoes" back to the user anything the user says to the bot. |
| Empty&nbsp;Bot | A good template if you are familiar with Bot Framework v4, and simply want a basic skeleton project.  Also a good option if you want to take sample code from the documentation and paste it into a minimal bot in order to learn. |
| Core Bot | A good template if you want to create advanced bots, as it uses multi-turn dialogs and [LUIS](https://www.luis.ai), an AI based cognitive service, to implement language understanding. This template creates a bot that can extract places and dates to book a flight. |


#### Install Yeoman

1. Assure that you have installed [node.js](https://nodejs.org/) version 12.10 or later.
1. Install latest [npm](https://www.npmjs.com).

   ```cmd
   npm install -g npm
   ```

1. Install [Yeoman](http://yeoman.io).

    ```bash
    # Make sure to install globally.
    npm install -g yo
    ```

2. Install *generator-botbuilder-java*.

    ```bash
    # Make sure to install globally.
    npm install -g generator-botbuilder-java
    ```

3. Verify that *Yeoman* and *generator-botbuilder-java* have been installed correctly.

    ```bash
    yo botbuilder-java --help
    ```


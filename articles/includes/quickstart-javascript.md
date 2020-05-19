## Prerequisites

- [Node.js](https://nodejs.org/)
- [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme)
- Knowledge of [restify](http://restify.com/) and asynchronous programming in JavaScript
- [Visual Studio Code](https://www.visualstudio.com/downloads) or your favorite IDE, if you want to edit the bot code.

> [!NOTE]
> The install of Windows build tools listed below is only required if you use Windows as your development operating system.
> For some installations the install step for restify is giving an error related to node-gyp.
> If this is the case you can try running this command with elevated permissions.
> This call may also hang without exiting if python is already installed on your system:

> ```bash
> # only run this command if you are on Windows. Read the above note.
> npm install -g windows-build-tools
> ```

## Create a bot

To create your bot and initialize its packages

1. Open a terminal or elevated command prompt.

1. Switch to the directory for your JavaScript bots. Create it first if you don't already have one.

   ```bash
   mkdir myJsBots
   cd myJsBots
   ```

1. Ensure your version of npm is up to date.

   ```bash
   npm install -g npm
   ```

1. Next, install or update Yeoman and the generator for JavaScript. (Yeoman is a tool for creating applications. For more information, see [yeoman.io](https://yeoman.io).)

   ```bash
   npm install -g yo generator-botbuilder
   ```

1. Then, use the generator to create an echo bot.

   ```bash
   yo botbuilder
   ```

   Yeoman prompts you for some information with which to create your bot. For this tutorial, use the default values.

   ```text
   ? What's the name of your bot? my-chat-bot
   ? What will your bot do? Demonstrate the core capabilities of the Microsoft Bot Framework
   ? What programming language do you want to use? JavaScript
   ? Which template would you like to start with? Echo Bot - https://aka.ms/bot-template-echo
   ? Looking good.  Shall I go ahead and create your new bot? Yes
   ```

Thanks to the template, your project contains all the code that's necessary to create the bot in this quickstart. You don't need any additional code to test your bot.

> [!NOTE]
> If you create a `Core` bot, you'll need a LUIS language model. (You can create a language model at [luis.ai](https://www.luis.ai)). After creating the model, update the configuration file.

## Start your bot

In a terminal or command prompt change directories to the one created for your bot, and start it with `npm start`.

```bash
cd my-chat-bot
npm start
```

At this point, your bot is running locally on port 3978.

## Start the Emulator and connect your bot

1. Start the Bot Framework Emulator.
2. Click **Open Bot** on the Emulator's **Welcome** tab.
3. Enter your bot's URL, which is the URL of the local port, with /api/messages added to the path, typically `http://localhost:3978/api/messages`.
4. Then click **Connect**.

Send a message to your bot, and the bot will respond back with a message.
![Emulator running](../media/emulator-v4/js-quickstart.png)

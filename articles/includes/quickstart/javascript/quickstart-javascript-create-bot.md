<!-- Include under ## Create a bot H2 header -->

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

<!-- Include under "Prerequisites" header in the files:
bot-builder-tutorial-create-basic-bot.md and bot-builder-javascript-quickstart.md -->

- [Node.js](https://nodejs.org/)
- [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme)
- Knowledge of [restify](http://restify.com/) and asynchronous programming in JavaScript
- [Visual Studio Code](https://www.visualstudio.com/downloads) or your favorite IDE, if you want to edit the bot code.

### Templates

To install Yeoman and the Yeoman generator for Bot Framework v4:

1. Open a terminal or elevated command prompt.

1. Switch to the directory for your JavaScript bots. Create it first if you don't already have one.

   ```console
   mkdir myJsBots
   cd myJsBots
   ```

1. Make sure you have the latest versions of npm and Yeoman.

   ```console
   npm install -g npm
   npm install -g yo
   ```

1. Install the Yeoman generator.
Yeoman is a tool for creating applications. For more information, see [yeoman.io](https://yeoman.io).

    ```console
        npm install -g generator-botbuilder
    ```

> [!NOTE]
> The install of Windows build tools listed below is only required if you use Windows as your development operating system.
> For some installations, the install step for restify is giving an error related to node-gyp.
> If this is the case you can try running this command with elevated permissions.
> This call may also hang without exiting if Python is already installed on your system:

> ```console
> # only run this command if you are on Windows. Read the above note.
> npm install -g windows-build-tools
> ```

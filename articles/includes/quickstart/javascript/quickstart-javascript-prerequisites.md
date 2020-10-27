<!-- Include under ## Prerequisites H2 header -->

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

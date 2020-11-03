<!-- Include under ## Prerequisites H2 header -->

# [C#](#tab/csharp)

- [.NET Core 3.1](https://dotnet.microsoft.com/download)
- [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme)
- Knowledge of [ASP.Net Core](https://docs.microsoft.com/aspnet/core/) and [asynchronous programming in C#](https://docs.microsoft.com/dotnet/csharp/programming-guide/concepts/async/index)

### Templates

# [Visual Studio](#tab/vs)

- [Visual Studio 2019 or later](https://www.visualstudio.com/downloads)
- [Bot Framework SDK v4 template for C#](https://aka.ms/bot-vsix)

To add the bot templates to Visual Studio, download and install the [Bot Framework v4 SDK Templates for Visual Studio](https://aka.ms/bot-vsix) VSIX file.

[!INCLUDE [dotnet vsix templates info](~/includes/vsix-templates-versions.md)]

# [Visual Studio Code / Command Line](#tab/vc+cl)

.NET Core Templates will help you to quickly build new conversational AI bots using Bot Framework v4. As of May 2020, these templates and the code they generate require .NET Core 3.1.

To install the Bot Framework templates:

1. Open a console window.

1. Install [.NET Core SDK](https://dotnet.microsoft.com/download) version 3.1 or higher.
1. You can use this command to determine which version of the .NET Core command-line interface you have installed.

   ```cmd
   dotnet --version
   ```

1. Install the 3 Bot Framework C# templates: the echo, core, and empty bot templates.

   ```cmd
   dotnet new -i Microsoft.Bot.Framework.CSharp.EchoBot
   dotnet new -i Microsoft.Bot.Framework.CSharp.CoreBot
   dotnet new -i Microsoft.Bot.Framework.CSharp.EmptyBot
   ```

1. Verify the templates have been installed correctly.

   ```cmd
   dotnet new --list
   ```

> [!NOTE]
> The above installation steps will install all 3 Bot Framework templates. You do not need to install all 3 templates and can install just the ones you will use. This article makes use of the echo bot template.


# [JavaScript](#tab/javascript)

- [Node.js](https://nodejs.org/)
- [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme)
- Knowledge of [restify](http://restify.com/) and asynchronous programming in JavaScript
- [Visual Studio Code](https://www.visualstudio.com/downloads) or your favorite IDE, if you want to edit the bot code.

### Templates

To install Yeoman and the Yeoman generator for Bot Framework v4:

1. Open a terminal or elevated command prompt.

1. Switch to the directory for your JavaScript bots. Create it first if you don't already have one.

   ```bash
   mkdir myJsBots
   cd myJsBots
   ```

1. Make sure you have the latest versions of npm and Yeoman.

   ```cmd
   npm install -g npm
   npm install -g yo
   ```

1. Install the Yeoman generator.
Yeoman is a tool for creating applications. For more information, see [yeoman.io](https://yeoman.io).
   ```cmd
   npm install -g generator-botbuilder
   ```

> [!NOTE]
> The install of Windows build tools listed below is only required if you use Windows as your development operating system.
> For some installations the install step for restify is giving an error related to node-gyp.
> If this is the case you can try running this command with elevated permissions.
> This call may also hang without exiting if python is already installed on your system:

> ```bash
> # only run this command if you are on Windows. Read the above note.
> npm install -g windows-build-tools
> ```

# [Python](#tab/python)

- Python [3.6](https://www.python.org/downloads/release/python-369/), [3.7](https://www.python.org/downloads/release/python-375/), or [3.8](https://www.python.org/downloads/release/python-383/)
- [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme)
- Knowledge of asynchronous programming in Python
### Templates

1. Install the necessary packages by running the following commands:

    ```cmd
    pip install botbuilder-core
    pip install asyncio
    pip install aiohttp
    pip install cookiecutter==1.7.0
    ```

    The last package, cookiecutter, will be used to generate your bot. Verify that cookiecutter was installed correctly by running `cookiecutter --help`.

1. To create your bot run:

    ```cmd
    cookiecutter https://github.com/microsoft/botbuilder-python/releases/download/Templates/echo.zip
    ```

    This command creates an Echo Bot based on the Python [echo template](https://github.com/microsoft/BotBuilder-Samples/tree/master/generators/python/app/templates/echo).

>[!NOTE]
>
> Some developers may find it useful to create Python bots in a [virtual envrionment](https://docs.python.org/3/library/venv.html). The steps below will work regardless if you're developing in a virtual environment or on your local machine.

---
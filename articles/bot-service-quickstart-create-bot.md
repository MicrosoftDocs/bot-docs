---
title: Create a bot with the Bot Framework SDK in C#, Java, JavaScript, or Python - Azure Bot Service
description: Create your first bot with the Bot Framework SDK using C#, Java, JavaScript or Python.
keywords: quickstart, create bot, web app bot, c#, python, javascript, java, emulator
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: quickstart
ms.service: bot-service
ms.date: 09/01/2021
ms.custom: mode-api, tab-zone-seo, abs-meta-21q1
---

# Create a bot with the Bot Framework SDK

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

In this quickstart, you'll learn how to build your first bot with the Bot Framework SDK for C#, Java, JavaScript or Python, and how to test your bot with the Bot Framework Emulator.

Creating your first bot doesn't require an Azure subscription or an Azure Bot Service resource. This quickstart focuses on creating your first bot locally. If you'd like to learn how to create a bot in Azure, see [Create an Azure Bot resource](./v4sdk/abs-quickstart.md).

## Prerequisites

### [C#](#tab/csharp)

- [ASP.NET Core Runtime 3.1](https://dotnet.microsoft.com/download)
- [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md)
- Knowledge of [ASP.NET Core](/aspnet/core/) and [asynchronous programming in C#](/dotnet/csharp/programming-guide/concepts/async/index)

### Templates

[!INCLUDE [Add templates in C#](includes/quickstart/dotnet/add-templates.md)]

### [Java](#tab/java)

- Java 1.8 or later
- [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md)
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

### Install Yeoman

1. Assure that you have installed [node.js](https://nodejs.org/) version 12.10 or later.
1. Install latest [npm](https://www.npmjs.com).

   ```console
   npm install -g npm
   ```

1. Install [Yeoman](http://yeoman.io). Make sure to install globally.

    ```console
    npm install -g yo
    ```

1. Install *generator-botbuilder-java*. Make sure to install globally.

    ```console
    npm install -g generator-botbuilder-java
    ```

1. Verify that *Yeoman* and *generator-botbuilder-java* have been installed correctly.

    ```console
    yo botbuilder-java --help
    ```

### [JavaScript](#tab/javascript)

- [Node.js](https://nodejs.org/)
- [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md)
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
    > For some installations, the install step for restify is giving an error related to `node-gyp`.
    > If this is the case you can try running this command with elevated permissions.
    > This call may also hang without exiting if Python is already installed on your system:
    >
    > Only run this command if you are on Windows.
    >
    > ```console
    > npm install -g windows-build-tools
    > ```

### [Python](#tab/python)

- Python [3.8+][]
- [Bot Framework Emulator][Emulator]
- Knowledge of asynchronous programming in Python

### Create and enable a virtual environment

A virtual environment is a combination of a specific Python interpreter and libraries that are different from your global settings. The virtual environment is specific to a project and is maintained in the project folder. A benefit to using a virtual environment is that as you develop a project over time, the virtual environment always reflects the project's exact dependencies. To learn more about virtual environments, see [Creation of virtual environments](https://docs.python.org/3/library/venv.html).

Navigate to the directory where you want to create your bot. Then run the following commands for your preferred platform. After you activate your virtual environment, your command line/terminal should be prefaced with `(venv)`. This lets you know that the virtual environment is active. You can deactivate your virtual environment at any time by typing: `deactivate`.

**macOS/Linux**

```commandline
python3 -m venv venv
source venv/bin/activate
```

**Windows**

```commandline
python -m venv venv
venv\Scripts\activate.bat
```

### Templates

Install the necessary packages by running the following `pip install` commands:

```console
pip install botbuilder-core
pip install asyncio
pip install aiohttp
pip install cookiecutter==1.7.0
```

>[!IMPORTANT]
>If you're using a 32-bit version of Python, make sure you also run `pip install cryptography==2.8`.

[3.8+]: https://www.python.org/downloads/release/python-383/
[Emulator]: https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md

---

## Create a bot

### [C#](#tab/csharp)

[!INCLUDE [Create a bot in C#](includes/quickstart/dotnet/create-bot.md)]

> [!div class="nextstepaction"]
> [I created an echo bot](#start-your-bot) [I ran into an issue](https://microsoft.qualtrics.com/jfe/form/SV_6D4KLPZc2jTIa2O?Product=BotSDK&Page=bot-service-quickstart-create-bot&Section=create-a-bot&PLanguage=Csharp)

### [Java](#tab/java)

Run the following command to create an echo bot from templates. The command uses default options for its parameters.

```console
yo botbuilder-java -T "echo"
```

Yeoman prompts you for some information with which to create your bot. For this tutorial, use the default values.

```text
? What's the name of your bot? (echo)
? What's the fully qualified package name of your bot? (com.mycompany.echo)
? Which template would you like to start with? (Use arrow keys) Select "Echo Bot"
? Looking good.  Shall I go ahead and create your new bot? (Y/n) Enter "y"
```

The generator supports a number of command line options that can be used to change the generator's default options or to pre-seed a prompt.

| Command&nbsp;line&nbsp;Option  | Description |
| ------------------- | ----------- |
| `--help, -h`        | List help text for all supported command-line options |
| `--botName, -N`     | The name given to the bot project |
| `--packageName, -P` | The Java package name to use for the bot |
| `--template, -T`    | The template used to generate the project. Options are `empty` or `echo`. See [https://github.com/Microsoft/BotBuilder-Samples/tree/master/generators/generator-botbuilder](https://github.com/Microsoft/BotBuilder-Samples/tree/master/generators/generator-botbuilder) for additional information regarding the different template options and their functional differences. |
| `--noprompt`       | The generator will not prompt for confirmation before creating a new bot. Any requirement options not passed on the command line will use a reasonable default value. This option is intended to enable automated bot generation for testing purposes. |

Thanks to the template, your project contains all the code that's necessary to create the bot in this quickstart. You don't need any additional code to test your bot.

> [!NOTE]
> If you create a `Core` bot, you'll need a LUIS language model. You can create a language model at [luis.ai](https://www.luis.ai). After creating the model, update the configuration file.

> [!div class="nextstepaction"]
> [I created an echo bot](#start-your-bot) [I ran into an issue](https://microsoft.qualtrics.com/jfe/form/SV_6D4KLPZc2jTIa2O?Product=BotSDK&Page=bot-service-quickstart-create-bot&Section=create-a-bot&PLanguage=Java)

### [JavaScript](#tab/javascript)

1. Use the generator to create an echo bot.

   ```console
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
> If you create a `Core` bot, you'll need a LUIS language model. You can create a language model at [luis.ai](https://www.luis.ai). After creating the model, update the configuration file.

> [!div class="nextstepaction"]
> [I created an echo bot](#start-your-bot) [I ran into an issue](https://microsoft.qualtrics.com/jfe/form/SV_6D4KLPZc2jTIa2O?Product=BotSDK&Page=bot-service-quickstart-create-bot&Section=create-a-bot&PLanguage=Javascript)

### [Python](#tab/python)

1. From your working directory, run the following command to download the [echo bot][echo-template] template and its dependencies:

   ```console
   cookiecutter https://github.com/microsoft/BotBuilder-Samples/releases/download/Templates/echo.zip
   ```

2. You'll be prompted to give your bot a name and description. Enter the following values:
   - **bot_name**: **echo-bot**
   - **bot_description**: **A bot that echoes back user response.**

   ![set name and description](media/python/quickstart/set-name-description.png)

> [!div class="nextstepaction"]
> [I created an echo bot](#start-your-bot) [I ran into an issue](https://microsoft.qualtrics.com/jfe/form/SV_6D4KLPZc2jTIa2O?Product=BotSDK&Page=bot-service-quickstart-create-bot&Section=create-a-bot&PLanguage=Python)

[echo-template]: https://github.com/microsoft/BotBuilder-Samples/tree/master/generators/python/app/templates/echo

---

## Start your bot

### [C#](#tab/csharp)

[!INCLUDE [Start your bot in C#](includes/quickstart/dotnet/start-bot.md)]

> [!div class="nextstepaction"]
> [I started the echo bot](#start-the-emulator-and-connect-your-bot) [I ran into an issue](https://microsoft.qualtrics.com/jfe/form/SV_6D4KLPZc2jTIa2O?Product=BotSDK&Page=bot-service-quickstart-create-bot&Section=start-your-bot&PLanguage=Csharp)

### [Java](#tab/java)

1. From a terminal, navigate to the directory where you saved your bot, then execute the commands listed below.

1. Build the Maven project and packages it into a *.jar* file (archive).

    ```console
    mvn package
    ```

1. Run the bot locally. Replace the *archive-name* with the actual name from the previous command.

    ```console
    java -jar .\target\<archive-name>.jar
    ```

You are now ready to start the Emulator.

> [!div class="nextstepaction"]
> [I started the echo bot](#start-the-emulator-and-connect-your-bot) [I ran into an issue](https://microsoft.qualtrics.com/jfe/form/SV_6D4KLPZc2jTIa2O?Product=BotSDK&Page=bot-service-quickstart-create-bot&Section=start-your-bot&PLanguage=Java)

### [JavaScript](#tab/javascript)

In a terminal or command prompt change directories to the one created for your bot, and start it with `npm start`.

```console
cd my-chat-bot
npm start
```

At this point, your bot is running locally on port 3978.

> [!div class="nextstepaction"]
> [I started the echo bot](#start-the-emulator-and-connect-your-bot) [I ran into an issue](https://microsoft.qualtrics.com/jfe/form/SV_6D4KLPZc2jTIa2O?Product=BotSDK&Page=bot-service-quickstart-create-bot&Section=start-your-bot&PLanguage=Javascript)

### [Python](#tab/python)

1. From the command line/terminal, change directories to `echo-bot`.

   ```console
   cd echo-bot
   ```

1. Install the dependencies for the echo bot template.

    ```console
    pip install -r requirements.txt 
    ```

1. After the dependencies are installed, run the following command to start your bot:

    ```console
    python app.py
    ```

    You will know your bot is ready to test when you see the last line shown in the screenshot below:

    ![bot running locally](media/python/quickstart/bot-running-locally.png)

1. Copy the last for digits in the address on the last line, usually **3978**; you'll need these when you use the Emulator to interact with your bot.

> [!div class="nextstepaction"]
> [I started the echo bot](#start-the-emulator-and-connect-your-bot) [I ran into an issue](https://microsoft.qualtrics.com/jfe/form/SV_6D4KLPZc2jTIa2O?Product=BotSDK&Page=bot-service-quickstart-create-bot&Section=start-your-bot&PLanguage=Python)

---

## Start the Emulator and connect your bot

1. Start the Bot Framework Emulator.
1. Select **Open Bot** on the Emulator's **Welcome** tab.
1. Enter your bot's URL, which is your local host and port, with `/api/messages` added to the path. The address is usually: `http://localhost:3978/api/messages`.

   <!--This is the same process in the Emulator for all three languages.-->

    :::image type="content" source="media/quickstart/emulator-open-bot.png" alt-text="open a bot":::

1. Then select **Connect**.

   Send a message to your bot, and the bot will respond back.

    :::image type="content" source="media/quickstart/emulator-hello-echo.png" alt-text="echo message":::

> [!div class="nextstepaction"]
> [I started the emulator and connected to my bot](#next-steps) [I ran into an issue](https://microsoft.qualtrics.com/jfe/form/SV_6D4KLPZc2jTIa2O?Product=BotSDK&Page=bot-service-quickstart-create-bot&Section=start-the-emulator-and-connect-your-bot)

## Additional Resources

- See [Debug a bot](bot-service-debug-channel-ngrok.md) for how to debug using Visual Studio or Visual Studio Code and the Bot Framework Emulator.
- See [Tunneling (ngrok)](https://github.com/Microsoft/BotFramework-Emulator/wiki/Tunneling-(ngrok)) for information on how to install ngrok.

## Next steps

> [!div class="nextstepaction"]
> [Deploy your bot to Azure](bot-builder-deploy-az-cli.md)

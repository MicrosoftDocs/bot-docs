<!-- Include under "Create a bot" header in the files:
bot-builder-tutorial-create-basic-bot.md and bot-builder-dotnet-sdk-quickstart.md -->

# [Visual Studio](#tab/vs)

### Build with Visual Studio

In Visual Studio, create a new bot project using the **Echo Bot (Bot Framework v4 - .NET Core 3.1)** template. Choose **AI Bots** from the project types to show only bot templates.

> [!div class="mx-imgBorder"]
> ![Visual Studio create a new project dialog](../../../media/azure-bot-quickstarts/bot-builder-dotnet-project-vs2019.png)

Thanks to the template, your project contains all the code that's necessary to create the bot in this quickstart. You don't need any additional code to test your bot.

> [!NOTE]
> If you create a _Core_ bot, you'll need a LUIS language model. You can create a language model at [luis.ai](https://www.luis.ai). After creating the model, update the configuration file.

<!--
> [!NOTE]
> If you see that the message cannot be sent, you might need to restart your machine as ngrok didn't get the needed privileges on your system yet (only needs to be done one time).
-->

# [Visual Studio Code](#tab/vc)

### Build with Visual Studio Code

Make sure that [.NET Core 3.1](https://dotnet.microsoft.com/download) is installed.

1. In Visual Studio Code, open a new terminal window.
1. Navigate to the directory in which you want to create your bot project.
1. Create a new echo bot project using the following command. Replace `<your-bot-name>` with the name to use for your bot project.

   ```cmd
   dotnet new echobot -n <your-bot-name>
   ```

# [Command Line](#tab/cl)

### Build with Command Line

1. Open a new terminal window.
1. Navigate to the directory in which you want to create your bot project.
1. Create a new echo bot project using the following command. Replace `<your-bot-name>` with the name to use for your bot project.

   ```cmd
   dotnet new echobot -n <your-bot-name>
   ```

---

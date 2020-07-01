## Prerequisites

- [Visual Studio 2019 or later](https://www.visualstudio.com/downloads)
- [Bot Framework SDK v4 template for C#](https://aka.ms/bot-vsix)
- [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme)
- Knowledge of [ASP.Net Core](https://docs.microsoft.com/aspnet/core/) and [asynchronous programming in C#](https://docs.microsoft.com/dotnet/csharp/programming-guide/concepts/async/index)

## Create a bot

Install [BotBuilderVSIX.vsix template](https://aka.ms/bot-vsix) that you downloaded in the prerequisites section.

In Visual Studio, create a new bot project using the **Echo Bot (Bot Framework v4 - .NET Core 3.1)** template. Choose **AI Bots** from the project types to show only bot templates.

> [!div class="mx-imgBorder"]
> ![Visual Studio create a new project dialog](../media/azure-bot-quickstarts/bot-builder-dotnet-project-vs2019.png)

Thanks to the template, your project contains all the code that's necessary to create the bot in this quickstart. You don't need any additional code to test your bot.

> [!NOTE]
> If you create a `Core` bot, you'll need a LUIS language model. (You can create a language model at [luis.ai](https://www.luis.ai)). After creating the model, update the configuration file.

[!INCLUDE [dotnet vsix templates info](~/includes/vsix-templates-versions.md)]

<!--
> [!NOTE]
> Both .NET Core 2.1 and .NET Core 3.1 versions of the C# templates are available.
> When creating new bots in Visual Studio 2019, you should use the .NET Core 3.1 templates.
> The current bot samples use .NET Core 3.1 templates. You can find the samples that use .NET Core 2.1 templates in the [4.7-archive](https://github.com/microsoft/BotBuilder-Samples/tree/4.7-archive/samples/csharp_dotnetcore) branch of the BotBuilder-Samples repository.
> For information about deploying .NET Core 3.1 bots to Azure, see [Deploy your bot](~/bot-builder-deploy-az-cli.md).
-->

## Start your bot in Visual Studio

Start your project in Visual Studio. This will build the application, deploy it to localhost, and launch the web browser to display the application's `default.htm` page.

At this point, your bot is running locally on port 3978.

## Start the Emulator and connect to your bot

Next, start the emulator and then connect to your bot in the emulator:

1. Start the Bot Framework Emulator.

2. Click **Open Bot** on the Emulator's **Welcome** tab.

3. Enter your bot's URL, which is the URL of the local port, with /api/messages added to the path, typically `http://localhost:3978/api/messages`.

   <!--This is the same process in the Emulator for all three languages.-->
   ![open a bot screen](../media/python/quickstart/open-bot.png)

4. Then click **Connect**.

   Send a message to your bot, and the bot will respond back with a message.

   > [!div class="mx-imgBorder"]
   > ![Emulator running](../media/emulator-v4/cs-quickstart.png)

<!--
> [!NOTE]
> If you see that the message cannot be sent, you might need to restart your machine as ngrok didn't get the needed privileges on your system yet (only needs to be done one time).
-->

## Prerequisites
- Visual Studio [2017](https://www.visualstudio.com/downloads)
- Bot Framework SDK v4 template for [C#](https://aka.ms/bot-vsix)
- Bot Framework [Emulator](https://aka.ms/Emulator-wiki-getting-started)
- Knowledge of [ASP.Net Core](https://docs.microsoft.com/aspnet/core/) and asynchronous programming in [C#](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/index)

## Create a bot
Install BotBuilderVSIX.vsix template that you downloaded in the prerequisites section.

In Visual Studio, create a new bot project using the **Bot Framework Echo Bot** V4 template.

![Visual Studio project](~/media/azure-bot-quickstarts/bot-builder-dotnet-project.png)

> [!TIP] 
> If needed, change the project build type to ``.Net Core 2.1``. Also if needed, update the `Microsoft.Bot.Builder` [NuGet packages](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio).

Thanks to the template, your project contains all of the code that's necessary to create the bot in this quickstart. You won't actually need to write any additional code.

## Start your bot in Visual Studio

When you click the run button, Visual Studio will build the application, deploy it to localhost, and launch the web browser to display the application's `default.htm` page. At this point, your bot is running locally.

## Start the emulator and connect your bot

Next, start the emulator and then connect to your bot in the emulator:

1. Click the **Open Bot** link in the emulator "Welcome" tab. 
2. Select the .bot file located in the directory where you created the Visual Studio solution.

## Interact with your bot

Send a message to your bot, and the bot will respond back with a message.

![Emulator running](~/media/emulator-v4/emulator-running.png)

> [!NOTE]
> If you see that the message can not be sent, you might need to restart your machine as ngrok didn't get the needed privileges on your system yet (only needs to be done one time).

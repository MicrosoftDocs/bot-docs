### <a id="debug-csharp-serverless"> Debug a Consumption plan C\# Functions bot

The Consumption plan serverless C\# environment in Bot Service has more in common with Node.js than a typical C\# application because it requires a runtime host, much like the Node engine. In Azure, the runtime is part of the hosting environment in the cloud, but you must replicate that environment locally on your desktop.

#### Prerequisites

Before you can debug your Consumption plan C# bot, you must complete these tasks.

- Download the source code for your bot (from Azure), as described in [Set up continuous deployment](../../bot-service-build-continuous-deployment.md).
- Download and install the [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/blob/master/README.md).
- Install the [Azure Functions CLI](https://www.npmjs.com/package/azure-functions-cli).
- Install the [DotNet CLI](https://github.com/dotnet/cli).

If you want to be able to debug your code by using breakpoints in Visual Studio 2017, you must also complete these tasks.

- Download and install [Visual Studio 2017](https://www.visualstudio.com/downloads/) (Community Edition or above).
- Download and install the [Command Task Runner Visual Studio Extension](https://visualstudiogallery.msdn.microsoft.com/e6bf6a3d-7411-4494-8a1e-28c1a8c4ce99).

> [!NOTE]
> Visual Studio Code is not currently supported.

#### Debug a Consumption plan C# Functions bot using the Emulator

The simplest way to debug your bot locally is to start the bot and then connect to it from Bot Framework Emulator.
First, open a command prompt and navigate to the folder where the **project.json** file is located in your repository. Then, run the command `dotnet restore` to restore the various packages that are referenced in your bot.

> [!NOTE]
> Visual Studio 2017 changes how Visual Studio handles dependencies.
> While Visual Studio 2015 uses **project.json** to handle dependencies,
> Visual Studio 2017 uses a **.csproj** model when loading in Visual Studio.
> If you are using Visual Studio 2017, download this [**.csproj** file](https://botframework.blob.core.windows.net/bf-v3/azure-bot-service/bf-dotnetpreview3-debug-csproj.zip)
> to the **/messages** folder in your repository before you run the `dotnet restore` command.

![Command prompt](~/media/bot-service-debug-bot/csharp-azureservice-debug-envconfig.png)

Next, run `debughost.cmd` to load and start your bot.

![Command prompt run debughost.cmd](~/media/bot-service-debug-bot/csharp-azureservice-debug-debughost.png)

At this point, the bot is running locally. From the console window, copy the endpoint that debughost is listening on (in this example, `http://localhost:3978`). Then, start the Bot Framework Emulator and paste the endpoint into the address bar of the Emulator. For this example, you must also append `/api/messages` to the endpoint. Since you do not need security for local debugging, you can leave the **Microsoft App ID** and **Microsoft App Password** fields blank. Click **Connect** to establish a connection to your bot using the specified endpoint.

![Configure Emulator](~/media/bot-service-debug-bot/mac-azureservice-emulator-config.png)

After you have connected the Emulator to your bot, send a message to your bot by typing some text into the textbox that is located at the bottom of the Emulator window (i.e., where **Type your message...** appears in the lower-left corner). By using the **Log** and **Inspector** panels on the right side of the Emulator window, you can view the requests and responses as messages are exchanged between the Emulator and the bot.

![test via Emulator](~/media/bot-service-debug-bot/mac-azureservice-debug-emulator.png)

Additionally, you can view log details in the console window.

![Console window](~/media/bot-service-debug-bot/csharp-azureservice-debug-debughostlogging.png)

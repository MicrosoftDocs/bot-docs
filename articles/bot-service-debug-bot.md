---
title: Debug a bot built using Bot Service | Microsoft Docs
description: Learn how to debug a bot built using Bot Service.
author: kaiqb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 12/13/2017
---

# Debug a Bot Service bot

Bot Service bots are built as Azure App Service web apps, or as code running in a Consumption plan upon the Azure Functions serverless architecture. This article describes how to debug your bot after you have set up a publishing process. By downloading a zip file that contains your bot source, you can develop and debug using an integrated development environment (IDE) such as Visual Studio or Visual Studio Code. From your computer, you can update your bot on Bot Service by publishing from Visual Studio, or by publishing with every check-in to your source control service through continuous deployment.

## Publish any bot source using continuous deployment

You can publish bot source to Azure using continuous deployment. To set up continuous deployment, [follow these steps](bot-service-continuous-deployment.md) before proceeding.  

## Debug a Node.js bot

Follow steps in this section to debug a bot written in Node.js.

### Prerequisites

Before you can debug your Node.js bot, you must complete these tasks.

- Download the source code for your bot (from Azure), as described in [Set up continuous deployment](bot-service-continuous-deployment.md).
- Download and install the [Bot Framework Emulator](bot-service-debug-emulator.md).
- Download and install a code editor such as <a href="https://code.visualstudio.com" target="_blank">Visual Studio Code</a>.

### Debug a Node.js bot using the Bot Framework Emulator

The simplest way to debug your bot locally is to start the bot in Node and then connect to it from Bot Framework Emulator. First, you must set the `NODE_ENV` environment variable. This screenshot shows how to set the `NODE_ENV` environment variable and start the bot.

![run bot](~/media/mac-azureservice-debug-config.png)

At this point, the bot is running locally. Copy the bot's endpoint from the terminal window (in this example, `http://localhost:3978/api/messages`), start the Bot Framework Emulator, and paste the endpoint into the address bar of the emulator. Since you do not need security for local debugging, you can leave the **Microsoft App ID** and **Microsoft App Password** fields blank. Click **Connect** to establish a connection to your bot using the specified endpoint.

![configure emulator](~/media/mac-azureservice-emulator-config.png)

After you have connected the emulator to your bot, send a message to your bot by typing some text into the textbox that is located at the bottom of the emulator window (i.e., where **Type your message...** appears in the lower-left corner). By using the **Log** and **Inspector** panels on the right side of the emulator window, you can view the requests and responses as messages are exchanged between the emulator and the bot.

![test via emulator](~/media/mac-azureservice-debug-emulator.png)

Additionally, you can view log details from the Node runtime in the terminal window.

![terminal window](~/media/mac-azureservice-debug-logging.png)

### Debug a Node.js bot using breakpoints in Visual Studio Code

If you need more than logs and request / response traces to debug your bot, you can use a local IDE such as Visual Studio Code to debug your code using breakpoints. Using VS Code to debug your bot can be beneficial because you can make changes locally within the editor while you are debugging, and when you push changes to the remote repository, those changes will automatically be applied to your bot in the cloud (since you have enabled continuous deployment). 

First, launch VS Code and open the local folder where the source code for your bot is located.

![open VS Code](~/media/mac-azureservice-debug-vs-config.png)

Switch to the debugging view, and click **run**. If you are prompted to select a runtime engine to run your code, select Node.js.

![VS Code debugging view](~/media/mac-azureservice-debug-vsruntime.png)

Next, depending on whether you have synced the repository or modified files, you may be prompted to configure the **launch.json** file. If you are prompted, add the `env` configuration setting to the **launch.json** file (to tell the template that you are going to work with the emulator). 

```json
"env": {
    "NODE\_ENV": "development"
}
```

![VS Code debugging view](~/media/mac-azureservice-debug-launchjson.png)

Save your changes to the **launch.json** file and click **run** again. Your bot should now be running in the VS Code environment with Node. You can open the debug console to see logging output and set breakpoints as needed.

![VS Code set breakpoints](~/media/mac-azureservice-debug-vsrunning.png)

At this point, the bot is running locally. Copy the bot's endpoint from the debug console in VS Code, start the Bot Framework Emulator, and paste the endpoint into the address bar of the emulator. Since you do not need security for local debugging, you can leave the **Microsoft App ID** and **Microsoft App Password** fields blank. Click **Connect** to establish a connection to your bot using the specified endpoint. 

After you have connected the emulator to your bot, send a message to your bot by typing some text into the textbox that is located at the bottom of the emulator window (i.e., where **Type your message...** appears in the lower-left corner). As messages are exchanged between the emulator and the bot, you should hit the breakpoints that you set in VS Code.

![Debug in VS Code](~/media/mac-azureservice-debug-vsbreakpoint.png)


## Debug an Azure App Service web app C# bot

You can debug a C# bot Bot Service web app locally in Visual Studio. 


### Prerequisites

Before you can debug your web app C# bot, you must complete these tasks.

- Download and install the [Bot Framework Channel Emulator](bot-service-debug-emulator.md).
- Download and install <a href="https://www.visualstudio.com/downloads/" target="_blank">Visual Studio 2017</a> (Community Edition or above).

### Get and debug your source code

Follow these steps to download your C# bot source. 

1. In the Azure Portal, click your Bot Service, click the **BUILD** tab, and click **Download zip file**.
3. Extract the contents of the downloaded zip file to a local folder.
4. In Explorer, double-click the .sln file.
5. Click **Debug**, and click **Start Debugging**. Your bot's `default.htm` page appears. Note the address in the location bar.
6. In the Bot Framework Channel Emulator, click the blue location bar, and enter an address similar to `http://localhost:3984/api/messages`. Your port number might be different.

You are now debugging locally. You can simulate user activity in the emulator, and set breakpoints on your code in Visual Studio. You can then [re-publish your bot to Azure](bot-service-continuous-deployment.md).

## <a id="debug-csharp-serverless"></a> Debug a Consumption plan C\# script bot

The Consumption plan serverless C\# environment in Bot Service has more in common with Node.js than a typical C\# application because it requires a runtime host, much like the Node engine. In Azure, the runtime is part of the hosting environment in the cloud, but you must replicate that environment locally on your desktop. 

### Prerequisites

Before you can debug your Consumption plan C# bot, you must complete these tasks.

- Download the source code for your bot (from Azure), as described in [Set up continuous deployment](bot-service-continuous-deployment.md).
- Download and install the [Bot Framework Emulator](bot-service-debug-emulator.md).
- Install the <a href="https://www.npmjs.com/package/azure-functions-cli" target="_blank">Azure Functions CLI</a>.
- Install the <a href="https://github.com/dotnet/cli" target="_blank">DotNet CLI</a>.
  
If you want to be able to debug your code by using breakpoints in Visual Studio 2017, you must also complete these tasks.
  
- Download and install <a href="https://www.visualstudio.com/downloads/" target="_blank">Visual Studio 2017</a> (Community Edition or above).
- Download and install the <a href="https://visualstudiogallery.msdn.microsoft.com/e6bf6a3d-7411-4494-8a1e-28c1a8c4ce99" target="_blank">Command Task Runner Visual Studio Extension</a>.

> [!NOTE]
> Visual Studio Code is not currently supported.

### Debug a Consumption plan C# script bot using the Bot Framework Emulator

The simplest way to debug your bot locally is to start the bot and then connect to it from Bot Framework Emulator. 
First, open a command prompt and navigate to the folder where the **project.json** file is located in your repository. Then, run the command `dotnet restore` to restore the various packages that are referenced in your bot.

> [!NOTE]
> Visual Studio 2017 changes how Visual Studio handles dependencies. 
> While Visual Studio 2015 uses **project.json** to handle dependencies, 
> Visual Studio 2017 uses a **.csproj** model when loading in Visual Studio. 
> If you are using Visual Studio 2017, <a href="https://aka.ms/bf-debug-project">download this **.csproj** file</a> 
> to the **/messages** folder in your repository before you run the `dotnet restore` command.

![Command prompt](~/media/csharp-azureservice-debug-envconfig.png)

Next, run `debughost.cmd` to load and start your bot. 

![Command prompt run debughost.cmd](~/media/csharp-azureservice-debug-debughost.png)

At this point, the bot is running locally. From the console window, copy the endpoint that debughost is listening on (in this example, `http://localhost:3978`). Then, start the Bot Framework Emulator and paste the endpoint into the address bar of the emulator. For this example, you must also append `/api/messages` to the endpoint. Since you do not need security for local debugging, you can leave the **Microsoft App ID** and **Microsoft App Password** fields blank. Click **Connect** to establish a connection to your bot using the specified endpoint.

![Configure emulator](~/media/mac-azureservice-emulator-config.png)

After you have connected the emulator to your bot, send a message to your bot by typing some text into the textbox that is located at the bottom of the emulator window (i.e., where **Type your message...** appears in the lower-left corner). By using the **Log** and **Inspector** panels on the right side of the emulator window, you can view the requests and responses as messages are exchanged between the emulator and the bot.

![test via emulator](~/media/mac-azureservice-debug-emulator.png)

Additionally, you can view log details the console window.

![Console window](~/media/csharp-azureservice-debug-debughostlogging.png)

### Debug a Consumption plan C# bot using breakpoints in Visual Studio

To debug your bot using breakpoints in Visual Studio 2017, stop the **DebugHost.cmd** script, and load the solution for your project (included as part of the repository) in Visual Studio. Then, click **Task Runner Explorer** at the bottom of the Visual Studio window.

![Visual Studio Task Runner Explorer](~/media/csharp-azureservice-debug-vsopen.png)

You will see the bot loading in the debug host environment in the **Task Runner Explorer** window. Your bot is now running locally. Copy the bot's endpoint from the **Task Runner Explorer** window, start the Bot Framework Emulator, and paste the endpoint into the address bar of the emulator. For this example, you must also append `/api/messages` to the endpoint. Since you do not need security for local debugging, you can leave the **Microsoft App Id** and **Microsoft App Password** fields blank. Click **Connect** to establish a connection to your bot using the specified endpoint.

After you have connected the emulator to your bot, send a message to your bot by typing some text into the textbox that is located at the bottom of the emulator window (i.e., where **Type your message...** appears in the lower-left corner). As messages are exchanged between the emulator and the bot, you will see the responses as well as logged output within **Task Runner Explorer** in Visual Studio.

![Debug in Visual Studio](~/media/csharp-azureservice-debug-logging.png)

You can also set breakpoints for your bot. The breakpoints are hit only after clicking **Start** in the Visual Studio environment, which will attach to the Azure Function host (`func` command from Azure Functions CLI). Chat with your bot again using the emulator and you should hit the breakpoints that you set in Visual Studio.

> [!TIP]
> If you cannot successfully set a breakpoint, a syntax error likely exists in your code. To troubleshoot, look for compile errors in the **Task Runner Explorer** window after you try to send messages to your bot.

![Debug in Visual Studio](~/media/csharp-azureservice-debug-breakpoint.png)

## Next steps

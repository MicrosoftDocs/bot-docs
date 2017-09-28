---
title: 為使用 Azure Bot Service 的 Bot 除錯| Microsoft Docs
description: 學習如何為使用 Azure Bot Service 的 Bot 除錯。
author: 
ms.author: 
manager: 
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer: 
---

# 調試Azure Bot Service機器人

Azure Bot Service機器人構建 為Azure App Service Web應用程序，或者作為 Azure Function Serverless 上的消費計劃中運行的代碼。本文介紹如何在設置發布流程之後調試您的機器人。通過下載包含您的bot源的壓縮文件，您可以使用集成開發環境（IDE）（如Visual Studio或Visual Studio Code）進行開發和調試。從您的計算機，您可以通過從Visual Studio發布，或通過持續部署通過每個簽入到源控制服務發布Azure Bot Service來更新您的機器人。

## 使用持續部署發布任何bot

您可以使用持續部署將bot源發佈到Azure。 要進行連續部署前先按照[以下步驟](azure-bot-service-continuous-deployment.md)操作。

## 為Node.js機器人除錯

按照本節中的步驟測試在Node.js中編寫的bot。

### Prerequisites

在測試Node.js bot之前，您必須完成這些任務。

- 下載您的機器人的源代碼（從Azure），如[設置連續部署](azure-bot-service-continuous-deployment.md)。
- 下載並安裝[Bot Framework Emulator](debug-bots-emulator.md).
- 下載並安裝文字編輯器，如 <a href="https://code.visualstudio.com" target="_blank">Visual Studio Code</a>.

### 使用Bot Framework模擬器測試Node.js bot

在本地測試您的機器人的最簡單的方法是在Node中啟動機器人，然後從Bot Framework Emulator連接到它。首先，您必須先設定 `NODE_ENV` 的環境變量。此屏幕截圖顯示如何設置 `NODE_ENV` 環境變量並啟動Bot。

![run bot](./media/mac-azureservice-debug-config.png)

此時，機器人在本地運行。從終端窗口複製機器人的端點(in this example, `http://localhost:3978/api/messages`), start the Bot Framework Emulator, and paste the endpoint into the address bar of the emulator. Since you do not need security for local debugging, you can leave the **Microsoft App ID** and **Microsoft App Password** fields blank. Click **Connect** to establish a connection to your bot using the specified endpoint.

![configure emulator](./media/mac-azureservice-emulator-config.png)

After you have connected the emulator to your bot, send a message to your bot by typing some text into the textbox that is located at the bottom of the emulator window (i.e., where **Type your message...** appears in the lower-left corner). By using the **Log** and **Inspector** panels on the right side of the emulator window, you can view the requests and responses as messages are exchanged between the emulator and the bot.

![test via emulator](./media/mac-azureservice-debug-emulator.png)

Additionally, you can view log details from the Node runtime in the terminal window.

![terminal window](./media/mac-azureservice-debug-logging.png)

### Debug a Node.js bot using breakpoints in Visual Studio Code

If you need more than logs and request / response traces to debug your bot, you can use a local IDE such as Visual Studio Code to debug your code using breakpoints. Using VS Code to debug your bot can be beneficial because you can make changes locally within the editor while you are debugging, and when you push changes to the remote repository, those changes will automatically be applied to your bot in the cloud (since you have enabled continuous deployment). 

First, launch VS Code and open the local folder where the source code for your bot is located.

![open VS Code](./media/mac-azureservice-debug-vs-config.png)

Switch to the debugging view, and click **run**. If you are prompted to select a runtime engine to run your code, select Node.js.

![VS Code debugging view](./media/mac-azureservice-debug-vsruntime.png)

Next, depending on whether you have synced the repository or modified files, you may be prompted to configure the **launch.json** file. If you are prompted, add the `env` configuration setting to the **launch.json** file (to tell the template that you are going to work with the emulator). 

```json
"env": {
    "NODE\_ENV": "development"
}
```

![VS Code debugging view](./media/mac-azureservice-debug-launchjson.png)

Save your changes to the **launch.json** file and click **run** again. Your bot should now be running in the VS Code environment with Node. You can open the debug console to see logging output and set breakpoints as needed.

![VS Code set breakpoints](./media/mac-azureservice-debug-vsrunning.png)

At this point, the bot is running locally. Copy the bot's endpoint from the debug console in VS Code, start the Bot Framework Emulator, and paste the endpoint into the address bar of the emulator. Since you do not need security for local debugging, you can leave the **Microsoft App ID** and **Microsoft App Password** fields blank. Click **Connect** to establish a connection to your bot using the specified endpoint. 

After you have connected the emulator to your bot, send a message to your bot by typing some text into the textbox that is located at the bottom of the emulator window (i.e., where **Type your message...** appears in the lower-left corner). As messages are exchanged between the emulator and the bot, you should hit the breakpoints that you set in VS Code.

![Debug in VS Code](./media/mac-azureservice-debug-vsbreakpoint.png)

##<a id="debug-csharp-serverless"></a> Debug a consumption plan C\# script bot

The consumption plan serverless C\# environment in Azure Bot Service has more in common with Node.js than a typical C\# application because it requires a runtime host, much like the Node engine. In Azure, the runtime is part of the hosting environment in the cloud, but you must replicate that environment locally on your desktop. 

### Prerequisites

Before you can debug your consumption plan C# bot, you must complete these tasks.

- Download the source code for your bot (from Azure), as described in [Set up continuous deployment](azure-bot-service-continuous-deployment.md).
- Download and install the [Bot Framework Emulator](debug-bots-emulator.md).
- Install the <a href="https://www.npmjs.com/package/azure-functions-cli" target="_blank">Azure Functions CLI</a>.
- Install the <a href="https://github.com/dotnet/cli" target="_blank">DotNet CLI</a>.
  
If you want to be able to debug your code by using breakpoints in Visual Studio 2017, you must also complete these tasks.
  
- Download and install <a href="https://www.visualstudio.com/downloads/" target="_blank">Visual Studio 2017</a> (Community Edition or above).
- Download and install the <a href="https://visualstudiogallery.msdn.microsoft.com/e6bf6a3d-7411-4494-8a1e-28c1a8c4ce99" target="_blank">Command Task Runner Visual Studio Extension</a>.

> [!NOTE]
> Visual Studio Code is not currently supported.

### Debug a consumption plan C# script bot using the Bot Framework Emulator

The simplest way to debug your bot locally is to start the bot and then connect to it from Bot Framework Emulator. 
First, open a command prompt and navigate to the folder where the **project.json** file is located in your repository. Then, run the command `dotnet restore` to restore the various packages that are referenced in your bot.

> [!NOTE]
> Visual Studio 2017 changes how Visual Studio handles dependencies. 
> While Visual Studio 2015 uses **project.json** to handle dependencies, 
> Visual Studio 2017 uses a **.csproj** model when loading in Visual Studio. 
> If you are using Visual Studio 2017, <a href="https://aka.ms/bf-debug-project">download this **.csproj** file</a> 
> to the **/messages** folder in your repository before you run the `dotnet restore` command.

![Command prompt](./media/csharp-azureservice-debug-envconfig.png)

Next, run `debughost.cmd` to load and start your bot. 

![Command prompt run debughost.cmd](./media/csharp-azureservice-debug-debughost.png)

At this point, the bot is running locally. From the console window, copy the endpoint that debughost is listening on (in this example, `http://localhost:3978`). Then, start the Bot Framework Emulator and paste the endpoint into the address bar of the emulator. For this example, you must also append `/api/messages` to the endpoint. Since you do not need security for local debugging, you can leave the **Microsoft App ID** and **Microsoft App Password** fields blank. Click **Connect** to establish a connection to your bot using the specified endpoint.

![Configure emulator](./media/mac-azureservice-emulator-config.png)

After you have connected the emulator to your bot, send a message to your bot by typing some text into the textbox that is located at the bottom of the emulator window (i.e., where **Type your message...** appears in the lower-left corner). By using the **Log** and **Inspector** panels on the right side of the emulator window, you can view the requests and responses as messages are exchanged between the emulator and the bot.

![test via emulator](./media/mac-azureservice-debug-emulator.png)

Additionally, you can view log details the console window.

![Console window](./media/csharp-azureservice-debug-debughostlogging.png)

### Debug a consumption plan C# bot using breakpoints in Visual Studio

To debug your bot using breakpoints in Visual Studio 2017, stop the **DebugHost.cmd** script, and load the solution for your project (included as part of the repository) in Visual Studio. Then, click **Task Runner Explorer** at the bottom of the Visual Studio window.

![Visual Studio Task Runner Explorer](./media/csharp-azureservice-debug-vsopen.png)

You will see the bot loading in the debug host environment in the **Task Runner Explorer** window. Your bot is now running locally. Copy the bot's endpoint from the **Task Runner Explorer** window, start the Bot Framework Emulator, and paste the endpoint into the address bar of the emulator. For this example, you must also append `/api/messages` to the endpoint. Since you do not need security for local debugging, you can leave the **Microsoft App Id** and **Microsoft App Password** fields blank. Click **Connect** to establish a connection to your bot using the specified endpoint.

After you have connected the emulator to your bot, send a message to your bot by typing some text into the textbox that is located at the bottom of the emulator window (i.e., where **Type your message...** appears in the lower-left corner). As messages are exchanged between the emulator and the bot, you will see the responses as well as logged output within **Task Runner Explorer** in Visual Studio.

![Debug in Visual Studio](./media/csharp-azureservice-debug-logging.png)

You can also set breakpoints for your bot. The breakpoints are hit only after clicking **Start** in the Visual Studio environment, which will attach to the Azure Function host (`func` command from Azure Functions CLI). Chat with your bot again using the emulator and you should hit the breakpoints that you set in Visual Studio.

> [!TIP]
> If you cannot successfully set a breakpoint, a syntax error likely exists in your code. To troubleshoot, look for compile errors in the **Task Runner Explorer** window after you try to send messages to your bot.

![Debug in Visual Studio](./media/csharp-azureservice-debug-breakpoint.png)

> [!NOTE]
> By following the steps that are described in this article, you will be able to debug a majority of bots 
> that you might build using Azure Bot Service. 
> However, if you create a bot using the [Proactive template](azure-bot-service-template-proactive.md), 
> you must do some additional work to enable queue storage that is used between the trigger function 
> and the bot function. More information on this topic will be made available soon.

## Debug an Azure App Service web app C# bot

You can debug a C# bot Azure Bot Service web app locally in Visual Studio. 


### Prerequisites

Before you can debug your web app C# bot, you must complete these tasks.

- Download and install the [Bot Framework Channel Emulator](debug-bots-emulator.md).
- Download and install <a href="https://www.visualstudio.com/downloads/" target="_blank">Visual Studio 2017</a> (Community Edition or above).

### Get and debug your source code

Follow these steps to download your C# bot source. 

1. In the Azure Portal, click your Bot Service, click the **BUILD** tab, and click **Download zip file**.
3. Extract the contents of the downloaded zip file to a local folder.
4. In Explorer, double-click the .sln file.
5. Click **Debug**, and click **Start Debugging**. Your bot's `default.htm` page appears. Note the address in the location bar.
6. In the Bot Framework Channel Emulator, click the blue location bar, and enter an address similar to `http://localhost:3984/api/messages`. Your port number might be different.

You are now debugging locally. You can simulate user activity in the emulator, and set breakpoints on your code in Visual Studio. You can then [re-publish your bot to Azure](azure-bot-service-continuous-deployment.md).

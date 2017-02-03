---
layout: page
parent1: Azure Bot Service
weight: 13025
permalink: /en-us/azure-bot-service/manage/debug/
parent2: Manage
title: Debugging your bot
---

The Azure Bot Service bots are built on the Azure Functions Serverless architecture. In the Azure Bot Service model, your bot's code starts out in Azure, but you'll typically set up Azure's continuous integration feature so you can work with it locally in your favorite tool chain.  This article details how to debug your bot locally after setting up continuous integration.


* TOC
{:toc}

## Debugging Node.js bots 

First, you need to set up your environment.  You'll need:

1.  A local copy of your Azure Bot Service code (see [Setting up Continuous Integration](/en-us/azure-bot-service/manage/setting-up-continuous-integration/)
2.  The Bot Framework Emulator for your platform (see [Mac, Linux, Windows](https://docs.botframework.com/en-us/downloads/))
3.  Node, or a code editor such as Visual Studio Code (see [Mac, Linux, Windows](https://code.visualstudio.com))

After setting up your environment, the simplest way to run the bot locally is to start your bot in Node, and then
connect to it from Bot Framework Emulator. But before you do that, you'll need to set the NODE_ENV environment
variable. The following Node terminal window example shows how you would set the environment variable and start running the bot on a Mac.

[![](/en-us/images/azure-bots/mac-azureservice-debug-config.png)](/en-us/images/azure-bots/mac-azureservice-debug-config.png)

At this point, the bot is running locally. Copy the endpoint that the bot is running on (in this example, http://localhost:3978/api/messages), start the framework's emulator, and paste the endpoint into the address bar as shown below.

[![](/en-us/images/azure-bots/mac-azureservice-emulator-config.png)](/en-us/images/azure-bots/mac-azureservice-emulator-config.png)

You don't need security for local debugging, so leave the **Microsoft App Id** and **Microsoft App Password** fields blank. Click **Connect** and start playing with the bot by typing a message to your bot (see **Type your message...** in the lower left corner).

[![](/en-us/images/azure-bots/mac-azureservice-debug-emulator.png)](/en-us/images/azure-bots/mac-azureservice-debug-emulator.png)


You can use the emulator's Log and the Inspector features to get a detailed understanding of the message traffic. You can also see the logs from the Node runtime in the terminal window.

[![](/en-us/images/azure-bots/mac-azureservice-debug-logging.png)](/en-us/images/azure-bots/mac-azureservice-debug-logging.png)

### Debugging Node.js bots using Visual Studio Code

If you need more than visual inspection and logs to diagnose your bot, you can use a local debugger such as Visual Studio Code.  All of the steps are the same, but instead of running the Node runtime, you'll start the VS Code debugger.

To get started, load VS Code and open the folder that your repo is in.

[![](/en-us/images/azure-bots/mac-azureservice-debug-vs-config.png)](/en-us/images/azure-bots/mac-azureservice-debug-vs-config.png)

Switch to the debugging view, and hit go. The first time it will ask you to pick a runtime engine to run your code (this example uses Node).

[![](/en-us/images/azure-bots/mac-azureservice-debug-vsruntime.png)](/en-us/images/azure-bots/mac-azureservice-debug-vsruntime.png)

Next, depending on whether you've synced the repo or changed any of it, you may get asked to configure the launch.json file. If you do, you'll need to add the following code to the launch.json file, which tells the template that you're going to work with the emulator.

{% highlight json %}
"env": {
    "NODE\_ENV": "development"
}
{% endhighlight %}

[![](/en-us/images/azure-bots/mac-azureservice-debug-launchjson.png)](/en-us/images/azure-bots/mac-azureservice-debug-launchjson.png)

Save your changes to the launch.json file and hit go again. Your bot should now be running in the VS Code environment with Node. You can open the debug console to see logging output, and set breakpoints as needed.

[![](/en-us/images/azure-bots/mac-azureservice-debug-vsrunning.png)](/en-us/images/azure-bots/mac-azureservice-debug-vsrunning.png)

To interact with the bot, use the framework's emulator again. Copy the endpoint from the debug console in VS Code, and connect to it in the emulator. Start chatting with your bot and you should hit your breakpoints.

[![](/en-us/images/azure-bots/mac-azureservice-debug-vsbreakpoint.png)](/en-us/images/azure-bots/mac-azureservice-debug-vsbreakpoint.png)

And that's it. VS Code is great because you can make changes locally within the editor while debugging, and most importantly, since you're using Azure Bot Service with continuous integration turned on, when you update the repo, your bot in the cloud will
automatically pick up and start running your changes.

## Debugging C\# bots built using the Azure Bot Service on Windows

The C\# environment in Azure Bot Service has more in common with Node.js than a typical C\# app because it requires a runtime host, much like the Node engine. In Azure, the runtime is part of the hosting environment in the cloud,
but you'll need to replicate that environment locally on your desktop.  

First, you need to set up your environment.  You'll need:

1.  A local copy of your Azure Bot Service code (see [Setting up Continuous Integration](/en-us/azure-bot-service/manage/setting-up-continuous-integration/))
2.  The Bot Framework Emulator ([Download](https://docs.botframework.com/en-us/downloads/))
3.  The Azure Functions CLI ([Download](https://www.npmjs.com/package/azure-functions-cli))
4.  DotNet CLI ([Download](https://github.com/dotnet/cli))  
  
and if you want breakpoint debugging in Visual Studio 15:  
  
5.  Visual Studio 15&mdash;the Community Edition will work fine ([Download](https://www.visualstudio.com/downloads/))
6.  The Command Task Runner Visual Studio Extension ([Download](https://visualstudiogallery.msdn.microsoft.com/e6bf6a3d-7411-4494-8a1e-28c1a8c4ce99))

<div class="docs-text-note"><strong>Note:</strong> Visual Studio Code is not currently supported, but stay tuned.</div>

After installing the tools above, you have everything you need to debug your C\# bot locally.  

Open a command prompt and navigate to the folder where your project.json file lives in your repository. Issue the command **dotnet restore** to restore the various packages referenced in your bot.

[![](/en-us/images/azure-bots/csharp-azureservice-debug-envconfig.png)](/en-us/images/azure-bots/csharp-azureservice-debug-envconfig.png)

Next, run debughost.cmd to load and run your bot. 

[![](/en-us/images/azure-bots/csharp-azureservice-debug-debughost.png)](/en-us/images/azure-bots/csharp-azureservice-debug-debughost.png)

After it's running, copy the endpoint that debughost is listening on (in this example, http://localhost:3978). Then, start the framework's emulator and paste the endpoint into the address bar as shown below. For this example, you'll need to append /api/messages to the endpoint. You don't need security for local debugging, so leave the **Microsoft App Id** and **Microsoft App Password** fields blank. Click **Connect** and start playing with the bot by typing a message to your bot (see **Type your message...** in the lower left corner).

[![](/en-us/images/azure-bots/mac-azureservice-emulator-config.png)](/en-us/images/azure-bots/mac-azureservice-emulator-config.png)

You can see the logs in the console window.

[![](/en-us/images/azure-bots/csharp-azureservice-debug-debughostlogging.png)](/en-us/images/azure-bots/csharp-azureservice-debug-debughostlogging.png)

If wou want to do breakpoint debugging in Visual Studio 2015, stop the DebugHost.cmd script, and load the solution for your project (included as part of the repo) in Visual Studio. Then, click **Task Runner Explorer** at the bottom of your Visual Studio window.

[![](/en-us/images/azure-bots/csharp-azureservice-debug-vsopen.png)](/en-us/images/azure-bots/csharp-azureservice-debug-vsopen.png)

You will see the bot loading up in the debug host environment in the Task Runner Explorer window. Your bot is now live. If you switch to the emulator and talk to your bot, you'll see the responses as well as logged output in Task Runner Explorer.

[![](/en-us/images/azure-bots/csharp-azureservice-debug-logging.png)](/en-us/images/azure-bots/csharp-azureservice-debug-logging.png)

You can also set breakpoints for your bot. The breakpoints are hit only after clicking **Start** in the Visual Studio environment, which will attach to the Azure Function host (func command from Azure Functions CLI). Chat with your bot again in the emulator and you should hit your breakpoint.

<div class="docs-text-note"><strong>Note:</strong> If you can't successfully set your breakpoint, you likely have a syntax error in your code. For a clue, look for compile errors in the Task Runner Explorer window after trying to talk to your bot.</div>

[![](/en-us/images/azure-bots/csharp-azureservice-debug-breakpoint.png)](/en-us/images/azure-bots/csharp-azureservice-debug-breakpoint.png)

The steps above will cover most scenarios. However, if you use the proactive template, these steps will enable debugging the bot, but you'll have to do some additional work to enable queue storage that's used between the trigger function and the bot function. More to come...




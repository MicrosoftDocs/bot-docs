---
layout: page
title: Testing your Bot Using the Bot Framework Emulator
permalink: /en-us/bot-framework-emulator/
weight: 4050
parent1: none
---

<span style="color:red"><< Jim, the problem with saying "new" in "We have released a new Bot..." is that time is relative. The user could be confused and think that we released a new one since the last time they saw this page. Content should reflect the currect state; we should avoid words such as new, current, future, etc. The Release Notes or What's New topic should contain release news. >></span>

<span style="color:red"><< I think you need to decide your documentation experience: either your documentation exists here or it exists in GitHub. If you're going to doc the emulator in GitHub, why not doc the SDKs in GitHub? If you do, then this basically becomes a navigation portal. >></span>

<span style="color:red"><<
We have released a new Bot Framework Emulator! Get it [here](https://emulator.botframework.com/) and for documentation, [here](https://github.com/microsoft/botframework-emulator/wiki/Getting-Started). 
>></span>

![Bot Framework Emulator running on Ubuntu](/en-us/images/emulator/newemulator-ubuntu.png)

<span style="color:red"><< This seems more like marketing material than technical docs. Our users are busy, they just want the succinct facts and to be told how to use it. They don't want to have to wade through marketing to get to the details. >></span>

<span style="color:red"><<
The Emulator is one of your most powerful tools in building your bot, and the new one is a great upgrade over our v1 emulator.

* **New** Support for Mac, Linux and Windows
* **New** All the Bot Framework card types are supported
* **New** Save multiple profiles for when you're working online and off
* **New** Simplifies configuration when you're working with ngrok
* **New** Uses the webchat control for higher fidelity layout and consistency with the webchat experience
* Send requests and receive responses to/from your bot endpoint on localhost
* Inspect the Json response
* Emulate a specific user and/or conversation

And - a much requested feature, the new Emulator is open source, please check it out on <a href="https://github.com/Microsoft/BotFramework-Emulator" target="_blank">GitHub</a></div>
>></span>

<span style="color:red"><< Jim, so the new version doesn't let you send "system messages" such as deleteUserData or conversationUpdate? >></span>

Bot Framework Emulator is a desktop application that lets you test and debug your bot on localhost or running remotely through a tunnel. You can use the emulator to ensure that the messages that you send are properly configured. The advatage of using the emulator is that you don't have register it with the framework or configure it on a channel before you can test it. 

The emulator uses the WebChat control in order to provide a consistent webchat experience. However, the experience may not reflect the actual experience that other channels provide especially when it comes to supporting and rendering rich cards (for example, a hero card), or if you're passing channel-specific data to take advantage of channel-specific features. For information about the types of messages that a channel supports, see [Channel Inspector](/en-us/channel-inspector/). Although the experience may not be the same, you can use the emulator to visually inspect the messages that your bot is passing to the channel.

### Download the emulator

The emulator is open source and is available on GitHub at <a href="https://github.com/Microsoft/BotFramework-Emulator" target="_blank">Microsoft/BotFramework-Emulator</a>. Or, you can simply download the app from [here](https://emulator.botframework.com/).   
### Supported platforms

* Windows
* Linux
* OS X

### Connecting to a bot running locally

To connect your bot to the emulator, enter your bot's endpoint into the address bar and click **Connect**. If you develop your bot using the C# or Node.js Bot Builder SDK, the default endpoint that the bot listens to when hosted locally is http://localhost:3978/api/messages.

Typically, when you run your bot locally, you don't need to specify **Microsoft App ID** and **Microsoft App Password** unless your bot requires them. (You will only have an app ID and password if you've [registered](/en-us/registration/) your bot with the framework.)

### Connecting to a bot running remotely

If you're running the emulator behind a firewall or other network boundary and you want to connect to a bot hosted remotely, you will need to install and configure tunneling software.

Computers running behind firewalls and home routers are not able to accept ad-hoc incoming requests from the outside world. Tunneling software provides a way around this by creating a bridge from outside the firewall to your local machine. An example of tunneling software is [ngrok](https://ngrok.com/), developed by [Alan Shreve](https://inconshreveable.com/). The emulator integrates tightly with ngrok and can launch it for you if it's installed.

To use ngrok:

1. Download version 2.1.18 or later to the computer that you're running the emulator on.
2. Tell the emulator where you installed ngrok. Click the menu icon and select **App Settings**. Click **Using ngrok?**. Then, specify or browse to the path where your ngrok installation is located.

If you already have ngrok installed, ensure that it's version 2.1.18 or later by using the `-v` command line parameter.

```
ngrok -v
```

If you do not want to use ngrok, you can still connect to bots running remotely by specifying a **Callback URL** in **App Settings**. The Callback URL is the endpoint where the bot sends you reply messages. The URL defaults to http://localhost:9002, but you can change it to point to the endpoint that your alternate tunneling software provides.

Note that when you run your bot remotely, you must specify **Microsoft App ID** and **Microsoft App Password**. To get an app ID and password, see [Registering a Bot](/en-us/registration/). 

### Connecting to a bot built using Azure Bot Service

If you use [Azure Bot Service](/en-us/azure-bot-service/) and want to use the emulator, see [Debugging your bot](/en-us/azure-bot-service/manage/debug/).


### If you use the Bot Application Template in Visual Studio...

If you use the Bot Application Template in Microsoft Visual Studio (see [Getting Started in .NET](/en-us/csharp/builder/getting-started/)), the following shows how you would start your bot and interact with it in the emulator.

First, start your bot in Visual Studio using a browser as the application host. The following image shows using Microsoft Edge.

![Start your Bot in VS2015 targeting the browser](/en-us/images/connector/connector-getstarted-start-bot-locally.png)

After the application is built and deployed, the web browser will open and display the Default.htm file as shown below. The Bot Application Template project includes the Default.htm file. If you want, you may modify the Default.htm file to match the name and description of your bot.

![Bot running the browser targeting localhost](/en-us/images/connector/connector-getstarted-bot-running-localhost.png)

Note the port number that the application is running on, which in this example is port 3978. You will need the port number to specify your bot's endpoint in the emulator. The endpoint that you will enter in the emulator is http://localhost:\<portnumber\>/api/messages.

<a id="usingnode" />

### If you use Node.js...

Simply start your bot in a console window.

```
node app.js
```

At this point, the bot is running locally. Copy the endpoint from the console window that the bot is running on. Then, start the emulator and paste the endpoint into the address bar. Click **Connect** and start testing your bot.

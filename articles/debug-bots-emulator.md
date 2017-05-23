---
title: Test and debug bots using the Bot Framework Emulator | Microsoft Docs
description: Learn how to inspect, test, and debug bots using the Bot Framework Emulator desktop application.
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 05/22/2017
ms.reviewer:
---

# Debug bots with the Bot Framework Emulator

The Bot Framework Emulator is a desktop application that allows bot developers to test and debug their bots, either locally or remotely. Using the emulator, you can chat with your bot and inspect the messages that your bot sends and receives. The emulator displays messages as they would appear in a web chat UI and logs JSON requests and responses as you exchange messages with your bot. 

> [!TIP] 
> Before you deploy your bot to the cloud, run it locally and test it using the emulator. 
> You can test your bot using the emulator even if you have not yet registered it with the Bot Framework or configured it to run on any channels.

![Emulator UI](~/media/emulator/emulator-ui-new.png)

## Prerequisites

### Download the Bot Framework Emulator

Download packages for Mac, Windows, and Linux are available via the <a href="https://github.com/Microsoft/BotFramework-Emulator/releases" target="_blank">GitHub releases page</a>. The latest Windows installer is available via the [emulator download page](https://emulator.botframework.com) (download starts immediately).

###<a id="ngrok"></a> Install and configure ngrok

If you are running the Bot Framework Emulator behind a firewall or other network boundary and want to connect to a bot that is hosted remotely, you must install and configure **ngrok** tunneling software. Computers running behind firewalls and home routers are not able to accept ad-hoc incoming requests from the outside world, but tunneling software provides a way around this by creating a bridge from outside the firewall to your local machine. The Bot Framework Emulator integrates tightly with [ngrok][ngrokDownload] tunnelling software (developed by [inconshreveable][inconshreveable]), and can launch it automatically when it is needed.

To install **ngrok** and configure the emulator to use it, complete these steps: 

1. Download the [ngrok][ngrokDownload] executable to your local machine.

2. Open the emulator's App Settings dialog, enter the path to ngrok, select whether or not to bypass ngrok for local addresses, and click **Save**.

![Setting the path to ngrok](~/media/emulator/emulator-configure_ngrok_path.png)

When the emulator connects to a bot that is hosted remotely, it displays messages in its log that indicate that ngrok has automatically been launched. If you've followed these steps but the emulator's log indicates that it is not able to launch ngrok, ensure that you have installed ngrok version `2.1.18` or later. (Earlier versions have been known to be incompatible.) To check ngrok's version, run this command from the command line:

<code>ngrok -v</code>

## <a id="localhost"></a> Connect to a bot that is running on localhost

Launch the Bot Framework Emulator and enter your bot's endpoint into the emulator's address bar. 

> [!TIP]
> If your bot was built using the Bot Builder SDK, the default endpoint for local debugging is `http://localhost:3978/api/messages`. This is where the bot will be listening for messages when hosted locally.
Next, if your bot is running with Microsoft Account (MSA) credentials, enter those values into the **Microsoft App ID** and **Microsoft App Password** fields. For localhost debugging, you will not typically need to populate these fields, although doing so is supported if your bot requires it.

Finally, click **Connect** to connect the emulator to your bot. After the emulator has connected to your bot, you can send and receive messages using the embedded chat control.

![Connecting to emulator using localhost](~/media/emulator/emulator-connect_localhost_credentials.png)

## <a id="remotehost"></a> Connect to a bot that is hosted remotely

Launch the Bot Framework Emulator and enter your bot's endpoint into the emulator's address bar. 

Next, populate the **Microsoft App ID** and **Microsoft App Password** fields with your bot's Microsoft Account (MSA) credentials. If you have registered your bot with the Bot Framework, you can retrieve its endpoint and MSA App ID from the bot's registration page in the <a href="https://dev.botframework.com/" target="_blank">Bot Framework Portal</a>. If you have registered your bot with the Bot Framework but do not know its MSA Password, you can [generate a new password](portal-register-bot.md#maintain).

Ensure that [ngrok](#ngrok) is installed and that the emulator's App Settings specify the path to the **ngrok** executable. **ngrok** enables the emulator to communicate with your remotely-hosted bot. 

Finally, click **Connect** to connect the emulator to your bot. After the emulator has connected to your bot, you can send and receive messages using the embedded chat control.

> [!TIP]
> When you create a bot using the Azure Bot Service, it is automatically registered with the Bot Framework. 
> You can retrieve its endpoint and MSA App ID from the bot's registration page in the 
> <a href="https://dev.botframework.com/" target="_blank">Bot Framework Portal</a>. 
> If you do not know your bot's MSA Password, you can [generate a new password](portal-register-bot.md#maintain).

## Enable speech recognition
The Bot Framework Emulator supports speech recognition via the [Cognitive Services Speech API](https://docs.microsoft.com/en-us/azure/cognitive-services/Speech/home). This allows you to exercise your speech-enabled bot, or Cortana skill, via speech in the emulator during development. The Bot Framework Emulator provides speech recognition free of charge for up to three hours per bot per day. 

## Send system activities

You can use the Bot Framework Emulator to emulate specific activities within the context of a conversation, by selecting from the available options under **Conversation** > **Send System Activity** in the emulator's settings menu:

* conversationUpdate (user added)
* conversationUpdate (user removed)
* contactRelationUpdate (bot added)
* contactRelationUpdate (bot removed)
* typing
* ping 
* deleteUserData

## Emulate payment processing
You can use the Bot Framework Emulator to emulate payment processing. In emulation mode, no real payment will be processed. Instead, the process payment logic simply returns a successful payment record. For payment processing, the emulator remembers your payment methods, shipping addresses, and it support form field validation. 

## Additional resources

To build a simple bot that you can test using the emulator, see:

* [Create a bot with the Bot Builder SDK for Node.js][NodeGetStarted]
* [Create a bot with the Bot Builder SDK for .NET][CSGetStarted]

The Bot Framework Emulator is open source. To contribute or file bugs and suggestions, see:

* [How to Contribute][EmulatorGithubContribute]
* [Submitting Bugs and Suggestions][EmulatorGithubBugs]

For information about troubleshooting issues with your bot, see:

* [Bot Framework troubleshooting guide][TroubleshootingGuide]
* [Troubleshooting Bot Framework authentication][TroubleshootingAuth]

[EmulatorGithub]: https://github.com/Microsoft/BotFramework-Emulator
[EmulatorGithubContribute]: https://github.com/Microsoft/BotFramework-Emulator/wiki/How-to-Contribute
[EmulatorGithubBugs]: https://github.com/Microsoft/BotFramework-Emulator/wiki/Submitting-Bugs-%26-Suggestions

[ngrokDownload]: https://ngrok.com/
[inconshreveable]: https://inconshreveable.com/
[BotFrameworkDevPortal]: https://dev.botframework.com/


[EmulatorConnectPicture]: ~/media/emulator/emulator-connect_localhost_credentials.png
[EmulatorNgrokPath]: ~/media/emulator/emulator-configure_ngrok_path.png
[EmulatorNgrokMonitor]: ~/media/emulator/emulator-testbot-ngrok-monitoring.png
[EmulatorUI]: ~/media/emulator/emulator-ui-new.png

[TroubleshootingGuide]: ~/troubleshoot-general-problems.md
[TroubleshootingAuth]: ~/troubleshoot-authentication-problems.md
[NodeGetStarted]: ~/nodejs/bot-builder-nodejs-quickstart.md
[CSGetStarted]: ~/dotnet/bot-builder-dotnet-quickstart.md



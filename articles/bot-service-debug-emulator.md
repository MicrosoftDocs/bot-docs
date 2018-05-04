---
title: Test and debug bots using the Bot Framework Emulator | Microsoft Docs
description: Learn how to inspect, test, and debug bots using the Bot Framework Emulator desktop application.
author: DeniseMak
ms.author: v-demak
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 04/30/2018
---

# Debug bots with the Bot Framework Emulator

The Bot Framework Emulator is a desktop application that allows bot developers to test and debug their bots, either locally or remotely. Using the emulator, you can chat with your bot and inspect the messages that your bot sends and receives. The emulator displays messages as they would appear in a web chat UI and logs JSON requests and responses as you exchange messages with your bot. 

> [!TIP] 
> Before you deploy your bot to the cloud, run it locally and test it using the emulator. 
> You can test your bot using the emulator even if you have not yet [registered](~/bot-service-quickstart-registration.md) it with the Bot Framework or configured it to run on any channels.

::: moniker range="azure-bot-service-4.0" 

![Emulator UI](media/emulator-v4/emulator-welcome.png)

## Download the Bot Framework Emulator

Download packages for Mac, Windows, and Linux are available via the [GitHub releases page](https://github.com/Microsoft/BotFramework-Emulator/releases).

## Connect to a bot running on local host

![Emulator local host settings](media/emulator-v4/emulator-localhost-settings.png)

To connect to a bot running locally, select the uppermost left (pages) tab. On the upper, click on the wheel icon on the edge of the blade to open the **Bot Settings** page of the emulator. Here you can specify your endpoint to the same port your bot is running on locally in order to connect to it. Click **Save & Connect**, and you will redirected to a live chat window where you can interact with your bot.

![Emulator Message Activity](media/emulator-v4/emulator-view-message-activity-02.png)

You can click on any message bubble within the conversation window and inspect the raw JSON message activity using the **INSPECTOR - JSON** feature to the right of the window. 


## Save and load conversations with bot transcripts

Message activity in the emulator can be saved as transcripts. From an open live chat window, you can select **Save Transcript As** to name and select the location of the output transacript file. 

>[!TIP]
> The **Start Over** button can be used any time to clear a conversation and restart a connection to the bot.  

![Emulator save transcripts](media/emulator-v4/emulator-live-chat.png)

To load transcripts, simply select **File** --> **Open Tranascript File** and select the transcript. A new Transcript window will open and render the message activity to the output window. 

![Emulator load transcripts](media/emulator-v4/emulator-load-transcript.png)

## Author transcripts with Chatdown

The [Chatdown](https://github.com/Microsoft/botbuilder-tools/tree/master/Chatdown) tool is a transcript generator which consumes a [markdown](https://daringfireball.net/projects/markdown/syntax) file to generate activity transcripts. You can author your own transcripts completely in markdown format, and save them as a **.chat** file to generate transcripts. This is useful for quickly creating mock conversation scenarios during bot development.  

### Prerequisites

- [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/releases) v.4 or greater 
- [Node.js](https://nodejs.org/en/)
 
Chatdown is available as an npm module which requires Node.js. To install Chatdown, globally install it to your machine. 

```
npm install -g chatdown
```
### Create and load transcript Transcript files ###

The following is an example of a how to author a **.chat** file. These files are markdown which contain 2 parts:
- Header which defines the conversation participants (user, bot)
- The back and forth conversation between the participants

```
user=John Doe
bot=Bot

bot: Hello!
user: hey
bot: [Typing][Delay=3000]
What can I do for you?
user: Actually nevermind, goodbye.
bot: bye!
```
[Click here](https://github.com/Microsoft/botbuilder-tools/tree/master/Chatdown/Examples) to view more samples of .chat files. 

To generate the transcript file, using the **chatdown** command in your CLI, enter the name of the .chat file, followed by '>' and the name of the output transcript file. 

```
chatdown sample.chat > sample.transcript
```
## Manage bot resources with the MSBot tool

The new [MSBot](https://github.com/Microsoft/botbuilder-tools/tree/master/MSBot) tool allows you to create a **.bot** file, which stores metadata about different services your bot consumes, all in one location. This file also enables your bot to connect to these services from the CLI.The tool is available as an npm module, to install it run:

```
npm install -g msbot 
```
![MSBot CLI window](media/emulator-v4/msbot-cli-window.png)


To create a bot file, from your CLI enter **msbot init** followed by the name of your bot, and the target URL endpoint, for example:

```shell
msbot init --name az-cli-bot --endpoint http://localhost:3984/api/messages
```
![Botfile](media/emulator-v4/botfile-generated.png)

>**Note:** The bot used for this guide is a simple echo bot, generated from the Azure CLI bot extension. [Click here](https://github.com/Microsoft/botbuilder-tools/tree/master/AzureCli) to learn more about building bots with Azure CLI. 

With the .bot file, you can now easily load your bot to the emulator. The .bot file is also required to register different endpoints and language components to your bot. 

![Bot-File-Dropdown](media/emulator-v4/bot-file-dropdown.png)

## Add Language Services 

You can easily register a LUIS app or QnA knowledge base to your .bot file directly from the emulator. When the .bot file is loaded, select the services button on the far left of the emulator window. You will see options under the **Services** menu to add LUIS, QnA Maker, Dispatch, endpoints, and the Azure Bot Service. 

To add a LUIS app, simply click on the **+** button on the LUIS menu, enter your LUIS app credentials, and click **Submit**. This will register the LUIS application to the .bot file, and connect the service to your bot application. 

![LUIS connect](media/emulator-v4/emulator-connect-luis-btn.png)

Similarly, to add a QnA knowledge base, simply click on the **+** button on the QnA menu, enter your QnA Maker knowledge base credentials, and click **Submit**. Your knowledge base will now be registered to the .bot file, and ready for use. 

![QnA connect](media/emulator-v4/emulator-connect-qna-btn.png)

When either service is connected, you can go back to a live chat window and verify that your services are connected and working. 

![QnA connected](media/emulator-v4/emulator-view-message-activity.png)

## Speech Recognition
The Bot Framework Emulator supports speech recognition via the [Cognitive Services Speech API](/azure/cognitive-services/Speech/home). This allows you to exercise your speech-enabled bot, or Cortana skill, via speech in the emulator during development. The Bot Framework Emulator provides speech recognition free of charge for up to three hours per bot per day. 

## <a id="ngrok"></a> Install and configure ngrok

If you are using Windows and you are running the Bot Framework Emulator behind a firewall or other network boundary and want to connect to a bot that is hosted remotely, you must install and configure **ngrok** tunneling software. The Bot Framework Emulator integrates tightly with [ngrok][ngrokDownload] tunnelling software (developed by [inconshreveable][inconshreveable]), and can launch it automatically when it is needed.

To install **ngrok** on Windows and configure the emulator to use it, complete these steps: 

1. Download the [ngrok][ngrokDownload] executable to your local machine.

2. Open the emulator's App Settings dialog, enter the path to ngrok, select whether or not to bypass ngrok for local addresses, and click **Save**.

![ngrok path](media/emulator-v4/emulator-ngrok-path.png)

::: moniker-end


::: moniker range="azure-bot-service-3.0" 

![Emulator UI](~/media/emulator/emulator-ui-new.png)

## Prerequisites

### Download the Bot Framework Emulator

Download packages for Mac, Windows, and Linux are available via the <a href="https://github.com/Microsoft/BotFramework-Emulator/releases" target="_blank">GitHub releases page</a>.

### <a id="ngrok"></a> Install and configure ngrok

If you are using Windows and you are running the Bot Framework Emulator behind a firewall or other network boundary and want to connect to a bot that is hosted remotely, you must install and configure **ngrok** tunneling software. The Bot Framework Emulator integrates tightly with [ngrok][ngrokDownload] tunnelling software (developed by [inconshreveable][inconshreveable]), and can launch it automatically when it is needed.

To install **ngrok** on Windows and configure the emulator to use it, complete these steps: 

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

Next, populate the **Microsoft App ID** and **Microsoft App Password** fields with your bot's credentials. 

> [!NOTE]
> 
> To find your bot's **AppID** and **AppPassword**, see [MicrosoftAppID and MicrosoftAppPassword](bot-service-manage-overview.md#microsoftappid-and-microsoftapppassword).

Ensure that [ngrok](#ngrok) is installed and that the emulator's App Settings specify the path to the **ngrok** executable. **ngrok** enables the emulator to communicate with your remotely-hosted bot. 

Finally, click **Connect** to connect the emulator to your bot. After the emulator has connected to your bot, you can send and receive messages using the embedded chat control.

## Enable speech recognition
The Bot Framework Emulator supports speech recognition via the [Cognitive Services Speech API](/azure/cognitive-services/Speech/home). This allows you to exercise your speech-enabled bot, or Cortana skill, via speech in the emulator during development. The Bot Framework Emulator provides speech recognition free of charge for up to three hours per bot per day. 

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

::: moniker-end 

## Additional resources

The Bot Framework Emulator is open source. You can [contribute][EmulatorGithubContribute] to the development and [submit bugs and suggestions][EmulatorGithubBugs].

You can use the [Channel Inspector](bot-service-channel-inspector.md) to preview supported features on specific channels.

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

[TroubleshootingGuide]: ~/bot-service-troubleshoot-general-problems.md
[TroubleshootingAuth]: ~/bot-service-troubleshoot-authentication-problems.md
[NodeGetStarted]: ~/nodejs/bot-builder-nodejs-quickstart.md
[CSGetStarted]: ~/dotnet/bot-builder-dotnet-quickstart.md

---
title: Debug a bot with inspection middleware in the Bot Framework SDK
description: Learn how to use inspection middleware to debug bots. See how to use the Bot Framework Emulator to inspect state data and message traffic.
author: JonathanFingold
ms.author: kamrani
keywords: Bot Framework SDK, debug bot, inspection middleware, bot emulator
manager: kamrani
ms.topic: how-to
ms.service: bot-service
ms.date: 10/19/2021
---

# Debug a bot with inspection middleware

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to debug a bot using inspection middleware. This feature allows the Bot Framework Emulator to debug traffic into and out of the bot in addition to looking at the current state of the bot. You can use a trace message to send data to the Emulator and then inspect the state of your bot in any given turn of the conversation.

We use an EchoBot built locally using the Bot Framework v4
[Create a bot](bot-service-quickstart-create-bot.md) to show how to debug and inspect the bot's message state. You can also [Debug a bot using IDE](./bot-service-debug-bot.md) or [Debug with the Bot Framework Emulator](./bot-service-debug-emulator.md), but to debug state you need to add inspection middleware to your bot. The Inspection bot samples are available here: [C#](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/47.inspection), [JavaScript](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/47.inspection), [Java](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/47.inspection), and [Python](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/47.inspection).

## Prerequisites

- Download and install the [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/blob/master/README.md)
- Knowledge of bot [Middleware](v4sdk/bot-builder-concept-middleware.md)
- knowledge of bot [Managing state](v4sdk/bot-builder-concept-state.md)
- Download and install [ngrok](https://ngrok.com/) (if you want to debug a bot configured in Azure to use other channels)

## Update your Emulator to the latest version

Before using the bot inspection middleware to debug your bot, you need to update your Emulator to be version 4.5 or newer. Check the [latest version](https://github.com/Microsoft/BotFramework-Emulator/releases) for updates.

To check the version of your Emulator, select **Help** > **About** in the menu. You will see the current version of your Emulator.

![current version](./media/bot-debug-inspection-middleware/bot-debug-check-emulator-version.png)

## Update your bot's code

### [C#](#tab/csharp)

Set up the inspection state and add the inspection middleware to the adapter in the **Startup.cs** file. The inspection state is provided through dependency injection. See the code update below or refer to the inspection sample here: [C#](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/47.inspection).

**Startup.cs**  
[!code-csharp [inspection bot sample](../botbuilder-samples/samples/csharp_dotnetcore/47.inspection/Startup.cs?range=17-37)]

**AdapterWithInspection.cs**  
[!code-csharp [inspection bot sample](../botbuilder-samples/samples/csharp_dotnetcore/47.inspection/AdapterwithInspection.cs?range=14-37)]

Update the bot class in the **EchoBot.cs** file.

**EchoBot.cs**  
[!code-csharp [inspection bot sample](../botbuilder-samples/samples/csharp_dotnetcore/47.inspection/Bots/EchoBot.cs?range=14-43)]

### [JavaScript](#tab/javascript)

Before updating your bot's code, update its packages to the latest versions by executing the following command in your terminal:

```console
npm install --save botbuilder@latest
```

Then you need to update the code of your JavaScript bot as follows. You can read more here: [Update your bot's code](https://github.com/Microsoft/BotFramework-Emulator/blob/master/content/CHANNELS.md#1-update-your-bots-code) or refer to the inspection sample here: [JavaScript](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/47.inspection).

**index.js**

Set up the inspection state and add the inspection middleware to the adapter in the **index.js** file.

[!code-javascript [inspection bot sample](../botbuilder-samples/samples/javascript_nodejs/47.inspection/index.js?range=10-43)]

**bot.js**

Update the bot class in the **bot.js** file.

[!code-javascript [inspection bot sample](../botbuilder-samples/samples/javascript_nodejs/47.inspection/bot.js?range=6-52)]

### [Java](#tab/java)

Set up the inspection state and add the inspection middleware to the adapter in the **Application.java** file. The inspection state is set by providing a new Spring @Bean to supply the BotFrameworkHttpAdapter that is set to be @Primary so it will override the default BotFrameworkHttpAdapter provided by the BotDependencyConfiguration base class. See the code update below or refer to the inspection sample here: [Java](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/47.inspection).

**Application.java**  
[!code-java [inspection bot sample](../botbuilder-samples/samples/java_springboot/47.inspection/src/main/java/com/microsoft/bot/sample/inspection/Application.java?range=66-97)]

AdapterWithInspection is implemented as part of the com.microsoft.bot.integration package and can be reviewed from the Java SDK source code.

Update the bot class in the **EchoBot.java** file.

**EchoBot.java**  
[!code-java [inspection bot sample](../botbuilder-samples/samples/java_springboot/47.inspection/src/main/java/com/microsoft/bot/sample/inspection/EchoBot.java?range=29-97)]

### [Python](#tab/python)

Before updating your bot's code, install the necessary PyPI packages by running the following commands in a terminal:

```console
pip install aiohttp
pip install botbuilder-core>=4.7.0
```

Set up the inspection state in the **app.py** file by adding a middleware to the adapter.

**app.py**

[!code-python [inspection bot sample](../botbuilder-samples/samples/python/47.inspection/app.py?range=76-84)]

Update the bot class in the **echo_bot.py** file.

**bots/echo_bot.py**

[!code-python [inspection bot sample](../botbuilder-samples/samples/python/47.inspection/bots/echo_bot.py?range=16-64)]

---

## Test your bot locally

After updating the code, you can run your bot locally and test the debugging feature using two Emulators: one to send and receive messages, and the other to inspect the state of messages in debugging mode. To test your bot locally:

1. Navigate to your bot's directory in a terminal and execute the following command to run your bot locally:

    ### [C#](#tab/csharp)

    ```console
    dotnet run
    ```

    ### [JavaScript](#tab/javascript)

    ```console
    npm start
    ```

    ### [Java](#tab/java)

    ```console
    mvn package
    java -jar .\target\bot-inspection-sample.jar 
    ```

    ### [Python](#tab/python)

    ```console
    python app.py
    ```

    ---

1. Open your Emulator. Click **Open Bot**. Fill in Bot URL with http://localhost:3978/api/messages and the **MicrosoftAppId** and **MicrosoftAppPassword** values. If you have a JavaScript bot, you can find these values in your bot's **.env** file. If you have a C# bot, you can find these values in the **appsettings.json** file. For a Java bot you, can find these values in the **application.properties** file. Click **Connect**.

1. Now open another Emulator window. This second Emulator window will work as a debugger. Follow the instructions as described in the previous step. Check **Open in debug mode** and then click **Connect**.

1. At this point, you will see a command with a unique identifier (`/INSPECT attach <identifier>`) in your debugging Emulator. Copy the whole command with the identifier from the debugging Emulator and paste it into the chat box of the first Emulator.

    > [!NOTE]
    > A unique identifier is generated every time when the Emulator is launched in debug mode after you add the inspection middleware in your bot's code.

1. Now you can send messages in the chat box of your first Emulator and inspect the messages in the debugging Emulator. To inspect the state of the messages click **Bot State** in the debugging Emulator and unfold **values** on the right **JSON** window. You will see the state of your bot in the debugging Emulator:

    :::image type="content" source="media/bot-debug-inspection-middleware/bot-debug-bot-state.png" alt-text="Bot state":::

## Inspect the state of a bot configured in Azure

If you want to inspect the state of your bot configured in Azure and connected to channels (like Teams), you will need to install and run [ngrok](https://ngrok.com/).

### Run ngrok

At this point, you have updated your Emulator to the latest version and added the inspection middleware in your bot's code. The next step is to run ngrok and configure your Azure bot resource to forward traffic to your local bot instance. Before running ngrok, you need to run your bot locally.

To run your bot locally:

1. Navigate to your bot's folder in a terminal and set your npm registration to use the [latest builds](https://botbuilder.myget.org/feed/botbuilder-v4-js-daily/package/npm/botbuilder-azure)

1. Run your bot locally. You will see your bot expose a port number like `3978`.

1. Open another command prompt and navigate to your bot's project folder. Run the following command:

    ```console
    ngrok http 3978
    ```

1. ngrok creates a forwarding URL, a public URL for your locally running bot. Copy the public IP address.

    :::image type="content" source="media/bot-debug-inspection-middleware/bot-debug-ngrok.png" alt-text="ngrok success":::

### Update channel registrations for your bot

Now that you have a forwarding URL for your bot, you can configure your bot resource in Azure to route traffic to your local bot.

1. Go to your Azure Bot resource in Azure. Select **Settings** on the left menu and under **Configuration**, set the **Messaging endpoint** to your forwarding URL plus `/api/messages`. For example, `https://e58549b6.ngrok.io/api/messages`. Select **Enable Streaming Endpoint** and **Save**.

    > [!TIP]
    > If **Save** is not enabled, you can uncheck **Enable Streaming Endpoint** and click **Save**, then check **Enable Streaming Endpoint** and click **Save** again. You need to make sure that **Enable Streaming Endpoint** is checked and the configuration of the endpoint is saved.

<!--@Emily: please review the sequence and fix the steps as necessary. There's a different way of managing secrets now. Removed the old image; add a new one, if needed.-->
1. Go to your bot's resource group, click **Deployment**, and select your bot resource that previously deployed successfully. Click **Inputs** on the left side to get the **appId** and **appSecret**. Update your bot's **.env** file (or **appsettings.json** file if you have a C# bot) with the **appId** and **appSecret**.

1. Start your Emulator, click **Open Bot**, and put http://localhost:3978/api/messages in the **Bot URL**. Fill **Microsoft App ID** and **Microsoft App password** with the same **appId** and **appSecret** you added to our bot's **.env** (**appsettings.json**) file. Then click **Connect**.

1. Your Azure bot resource is now connected to your bot running locally. You can test the web chat by clicking **Test in Web Chat** and sending messages in the chat box.

    :::image type="content" source="media/bot-debug-inspection-middleware/bot-debug-test-webchat.png" alt-text="Testing in Web Chat":::

1. Now let's enable the debugging mode in the Emulator. In your Emulator, select **Debug** > **Start Debugging**. Set the **Bot URL** to the messaging endpoint for your Azure resource (for example, `https://e58549b6.ngrok.io/api/messages`). For **Microsoft App ID**, enter your bot's app ID. For **Microsoft App password**, enter your bot's app secret. Make sure **Open in debug mode** is checked as well. Click **Connect**.

1. When the debugging mode is enabled, a UUID will be generated in your Emulator. A UUID is a unique ID generated every time you start the debugging mode in your Emulator. Copy and paste the UUID to the **Test in Web Chat** chat box or your channel's chat box. You will see the message "Attached to session, all traffic is being replicated for inspection" in the chat box.

You can start debugging your bot by sending messages in the configured channel's chat box. Your local Emulator will automatically update the messages with all the details for debugging. To inspect your bot's state of messages, click **Bot State** and unfold the **values** in the right JSON window.

:::image type="content" source="media/bot-debug-inspection-middleware/debug-state-inspection-channel-chat.gif" alt-text="Debugging using the Emulator and inspection middleware":::

## Next steps

- Try the inspection middleware bot sample in [C#](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/47.inspection), [JavaScript](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/javascript_nodejs/47.inspection), or [Python](https://github.com/microsoft/BotBuilder-Samples/blob/main/samples/python/47.inspection) .
- Read [troubleshoot general problems](bot-service-troubleshoot-bot-configuration.md) and the other troubleshooting articles in that section.
- Read the how to [Debug with the Emulator](bot-service-debug-emulator.md) article.

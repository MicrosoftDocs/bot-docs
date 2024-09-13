---
title: Debug a bot with inspection middleware in the Bot Framework SDK
description: Learn how to use inspection middleware to debug bots. See how to use the Bot Framework Emulator to inspect state data and message traffic.
keywords: Bot Framework SDK, debug bot, inspection middleware, bot emulator, Azure Bot
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: azure-ai-bot-service
ms.topic: how-to
ms.date: 10/26/2022

ms.custom:
  - evergreen
---

# Debug a bot with inspection middleware

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to debug a bot using inspection middleware. This feature allows the Bot Framework Emulator to debug traffic into and out of the bot, and to see the current state of the bot. You can use a trace message to send data to the Emulator and then inspect the state of your bot in any given turn of the conversation.

We use an EchoBot built locally using the Bot Framework v4 in the [Create a bot quickstart](bot-service-quickstart-create-bot.md) to show how to debug and inspect the bot's message state. You can also [Debug a bot using IDE](./bot-service-debug-bot.md) or [Debug with the Bot Framework Emulator](./bot-service-debug-emulator.md), but to debug state you need to add inspection middleware to your bot. The inspection bot samples are available for [C#](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/47.inspection), [JavaScript](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/47.inspection), [Java](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/47.inspection), and [Python](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/47.inspection).

[!INCLUDE [java-python-sunset-alert](includes/java-python-sunset-alert.md)]

## Prerequisites

- Knowledge of bot [Middleware](v4sdk/bot-builder-concept-middleware.md) and [Managing state](v4sdk/bot-builder-concept-state.md)
- Knowledge of how to [Debug an SDK-first bot](bot-service-debug-bot.md) and [Test and debug with the Emulator](bot-service-debug-emulator.md)
- An install of the [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/blob/master/README.md)
- An install [Dev Tunnel](https://aka.ms/devtunnels) (if you want to debug a bot configured in Azure to use other channels)
- A copy of the inspection bot sample for [C#](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/47.inspection), [JavaScript](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/47.inspection), [Java](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/47.inspection), or [Python](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/47.inspection)

## Update your Emulator to the latest version

Before using bot inspection middleware to debug your bot, update your Emulator to version 4.15 or later. Check the [latest version](https://github.com/Microsoft/BotFramework-Emulator/releases) for updates.

To check the version of your Emulator, select **Help**, then **About** in the menu. You'll see the current version of your Emulator.

## Update your bot code

### [C#](#tab/csharp)

The inspection state and inspection middleware are configured in the **Startup.cs** file and then used by the adapter.

**Startup.cs**  
[!code-csharp [inspection bot sample](../botbuilder-samples/samples/csharp_dotnetcore/47.inspection/Startup.cs?range=24-25,35-37)]

**AdapterWithInspection.cs**  
[!code-csharp [inspection bot sample](../botbuilder-samples/samples/csharp_dotnetcore/47.inspection/AdapterwithInspection.cs?range=13-36&highlight=20-21)]

Update the bot class in the **EchoBot.cs** file.

**EchoBot.cs**  
[!code-csharp [inspection bot sample](../botbuilder-samples/samples/csharp_dotnetcore/47.inspection/Bots/EchoBot.cs?range=31-43&highlight=3-7)]

### [JavaScript](#tab/javascript)

Before updating your bot's code, update its packages to the latest versions by executing the following command in your terminal:

```console
npm install --save botbuilder@latest
```

Then you need to update the code of your JavaScript bot as follows. You can read more here: [Update your bot's code](https://github.com/Microsoft/BotFramework-Emulator/blob/master/content/CHANNELS.md#1-update-your-bots-code) or refer to the [Inspection middleware](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/47.inspection) sample on GitHub.

**index.js**

Set up the inspection state and add the inspection middleware to the adapter in the **index.js** file.

[!code-javascript [inspection bot sample](../botbuilder-samples/samples/javascript_nodejs/47.inspection/index.js?range=13-26,40-61)]

**bot.js**

Update the bot class in the **bot.js** file.

[!code-javascript [inspection bot sample](../botbuilder-samples/samples/javascript_nodejs/47.inspection/bot.js?range=14-28)]

### [Java](#tab/java)

Set up the inspection state and add the inspection middleware to the adapter in the **Application.java** file. The inspection state is set by providing a new Spring @Bean to supply the BotFrameworkHttpAdapter that is set to be @Primary so it will override the default BotFrameworkHttpAdapter provided by the BotDependencyConfiguration base class. See the code update below or refer to the [Inspection middleware](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/47.inspection) sample on GitHub.

**Application.java**  
[!code-java [inspection bot sample](../botbuilder-samples/samples/java_springboot/47.inspection/src/main/java/com/microsoft/bot/sample/inspection/Application.java?range=66-97)]

AdapterWithInspection is implemented as part of the com.microsoft.bot.integration package and can be reviewed from the Java SDK source code.

Update the bot class in the **EchoBot.java** file.

**EchoBot.java**  
[!code-java [inspection bot sample](../botbuilder-samples/samples/java_springboot/47.inspection/src/main/java/com/microsoft/bot/sample/inspection/EchoBot.java?range=53-81)]

### [Python](#tab/python)

Before updating your bot's code, install the necessary PyPI packages by running the following commands in a terminal:

```console
pip install aiohttp
pip install botbuilder-core>=4.7.0
```

Set up the inspection state in the **app.py** file by adding a middleware to the adapter.

**app.py**

[!code-python [inspection bot sample](../botbuilder-samples/samples/python/47.inspection/app.py?range=12-23,29-33,70-85)]

Update the bot class in the **echo_bot.py** file.

**bots/echo_bot.py**

[!code-python [inspection bot sample](../botbuilder-samples/samples/python/47.inspection/bots/echo_bot.py?range=48-64)]

---

## Test your bot locally

After updating the code, you can run your bot locally and test the debugging feature using two Emulators: one to send and receive messages, and the other to inspect the state of messages in debugging mode. To test your bot locally:

1. Go to your bot's directory in a terminal and execute the following command to run your bot locally:

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

1. Open your Emulator. Select **Open Bot**. Fill in the **Bot URL** with `http://localhost:3978/api/messages` and the **MicrosoftAppId** and **MicrosoftAppPassword** values. If you have a JavaScript bot, you can find these values in your bot's **.env** file. If you have a C# bot, you can find these values in the **appsettings.json** file. For a Java bot you can find these values in the **application.properties** file. Select **Connect**.

1. Now open another Emulator window. This second Emulator window will work as a debugger. Follow the instructions as described in the previous step. Check **Open in debug mode** and then select **Connect**.

1. At this point you'll see a command with a unique identifier (`/INSPECT attach <identifier>`) in your debugging Emulator. Copy the whole command with the identifier from the debugging Emulator and paste it into the chat box of the first Emulator.

    > [!NOTE]
    > A unique identifier is generated every time when the Emulator is launched in debug mode after you add the inspection middleware in your bot's code.

1. Now you can send messages in the chat box of your first Emulator and inspect the messages in the debugging Emulator. To inspect the state of the messages, select **Bot State** in the debugging Emulator and unfold **values** on the right **JSON** window. You'll see the state of your bot in the debugging Emulator:

    :::image type="content" source="./media/bot-debug-inspection-middleware/bot-debug-bot-state.png" alt-text="bot state":::

## Inspect the state of a bot configured in Azure

If you want to inspect the state of your bot configured in Azure and connected to channels (like Teams) you'll need to install and run [Dev Tunnels](https://aka.ms/devtunnels).

### Run devtunnel

At this point, you've updated your Emulator to the latest version and added the inspection middleware in your bot's code. The next step is to run devtunnel and configure your local bot. Before running devtunnel you need to run your bot locally.

To run your bot locally:

1. Go to your bot's folder in a terminal and set your npm registration to use the [latest builds](https://botbuilder.myget.org/feed/botbuilder-v4-js-daily/package/npm/botbuilder-azure)

1. Run your bot locally. You'll see your bot expose a port number like `3978`.

1. Open another command prompt and go to your bot's project folder. Run the following command:

    ```console
   devtunnel host -a -p 3978
    ```

1. devtunnel is now connected to your locally running bot. Copy the secure (HTTPS) public URL.

    :::image type="content" source="./media/debug-devtunnel/devtunnel-forwarding-url.png" alt-text="devtunnel success":::

### Update your bot resource

Now that your local bot is connected to devtunnel, you can configure your bot resource in Azure to use the devtunnel URL.

1. Go to your bot resource in Azure. On the left menu, under **Settings**, select **Configuration**.
   1. Set the **Messaging endpoint** to the devtunnel URL address you copied. If necessary add **/api/messages** after the IP address. For example, `https://0qg12llz-3978.usw2.devtunnels.ms/api/messages`.
   1. Select **Enable Streaming Endpoint**.

       :::image type="content" source="./media/bot-debug-inspection-middleware/bot-debug-channels-setting-devtunnel.png" alt-text="Set endpoint":::

   1. Select **Apply** to save your changes.
       > [!TIP]
       > If **Apply** isn't enabled, you can uncheck **Enable Streaming Endpoint** and select **Apply**, then check **Enable Streaming Endpoint** and select **Apply** again. You need to make sure that **Enable Streaming Endpoint** is checked and the configuration of the endpoint is saved.

1. Go to your bot's resource group.
   1. Select **Deployment**, and then select the bot resource that previously deployed successfully. Select **Template** from the left menu to get the **MicrosoftAppId** and **MicrosoftAppPassword** for the web app associated with your bot.

       :::image type="content" source="./media/bot-debug-inspection-middleware/bot-debug-get-inputs-id-secret.png" alt-text="Get inputs":::

   1. Update your bot's configuration file (**appsettings.json** for C#, or **.env** for JavaScript) with the **MicrosoftAppId** and **MicrosoftAppPassword**.

1. Start your Emulator, select **Open Bot**, and enter `http://localhost:3978/api/messages` in the **Bot URL**. Fill **Microsoft App ID** and **Microsoft App password** with the same **MicrosoftAppId** and **MicrosoftAppPassword** you added to our bot's configuration file. Then select **Connect**.

1. Your running bot is now connected to your bot resource in Azure. To test your bot in Azure in Web Chat, go to your bot resources, select **Test in Web Chat**, and send messages to your bot.

### Enable debugging mode

1. In your Emulator, select **Debug**, then **Start Debugging**.
1. Enter the devtunnel URL (don't forget to add **/api/messages**) for the **Bot URL** (for example, `https://4jj51x75-51865.usw2.devtunnels.ms/api/messages`).
   1. For **Microsoft App ID**, enter your bot's app ID.
   1. For **Microsoft App password**, enter your bot's app secret.
   1. Make sure **Open in debug mode** is checked as well.
   1. Select **Connect**.

1. With the debugging mode enabled, the Emulator generates a UUID. A UUID is a unique ID generated every time you start the debugging mode in your Emulator.
1. Copy and paste the UUID to the **Test in Web Chat** chat box for your channel's chat box. You'll see the message "Attached to session, all traffic is being replicated for inspection" in the chat box.

You can start debugging your bot by sending messages in the configured channel's chat box. Your local Emulator will automatically update the messages with all the details for debugging. To inspect your bot's state of messages, select **Bot State** and unfold the **values** in the right JSON window.

:::image type="content" source="./media/bot-debug-inspection-middleware/debug-state-inspection-channel-chat.gif" alt-text="debug-inspection-middleware":::

## Next steps

- Learn how to [Debug your bot using transcript files](v4sdk/bot-builder-debug-transcript.md).
- Learn how to [Debug a skill or skill consumer](v4sdk/skills-debug-skill-or-consumer.md).

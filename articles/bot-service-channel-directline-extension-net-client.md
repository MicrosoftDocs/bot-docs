---
title: Create .NET client for Direct Line App Service extension
description: Learn how to create .NET clients that connect to Direct Line App Service extension. See how to set up C# clients that communicate with bots over WebSockets.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: Daniel.Evans
ms.service: bot-service
ms.topic: how-to
ms.date: 08/19/2021
---

# Create .NET Client to Connect to Direct Line App Service extension

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

This article describes how to create a .NET client in C# which connects to the Direct Line App Service extension.
Please, also read this companion article [Configure .NET bot for extension](bot-service-channel-directline-extension-net-bot.md).

## Get Direct Line secret key

1. In your browser, navigate to the [Azure portal](https://portal.azure.com/)
1. In the Azure portal, locate your **Azure Bot Service** resource
1. Click on **Channels** to configure the bot's channels
1. If it is not already enabled, click on the **Direct Line** channel to enable it.
1. If it is already enabled, in the Connect to channels table click on the **Edit** link on the Direct Line row.
1. Scroll to the Sites section. There is typically a Default Site unless you have deleted or renamed it.
1. Click on the **Show link** to reveal one of the keys, then copy and save its value. You will use this value in the section [Create a C# Direct Line client](#create-a-c-direct-line-client).

    ![App Service extension keys](./media/channels/direct-line-extension-extension-keys-net-client.png)

> [!NOTE]
> This value is your Direct Line client secret used to connect to Direct Line App Service extension.
> You can create additional sites if you'd like and use those secret values as well.

## Create a C# Direct Line client

Interactions with the Direct Line App Service extension happen differently than traditional Direct Line because most communication happens over a *WebSocket*. The updated Direct Line client includes helper classes for opening and closing a *WebSocket*, sending commands through the WebSocket, and receiving Activities back from the bot. This section describes how to create a simple C# client to interact with a bot.

1. In Visual Studio, create a new .NET Core console application project.
1. Clone the [Direct Line client](https://github.com/microsoft/BotFramework-DirectLine-DotNet) from GitHub repository and include it in your project.
1. Create a client and generate a token using a secret. This step is the same as building any other C# Direct Line client except the endpoint you need use in your bot, appended with the **.bot/** path as shown next. Do not forget the ending **/**.

    ```csharp
    string endpoint = "https://<your_bot_name>.azurewebsites.net/.bot/";
    string secret = "<your_bot_direct_line_secret_key>";

    var tokenClient = new DirectLineClient(
        new Uri(endpoint),
        new DirectLineClientCredentials(secret));
    var conversation = await tokenClient.Tokens.GenerateTokenForNewConversationAsync();
    ```

    Notice the following:
    - The endpoint value is the bot URL you obtained when you deployed the bot to Azure.  For more information, see [Configure .NET bot for extension](bot-service-channel-directline-extension-net-bot.md).
    - The secret value shown as *YOUR_BOT_SECRET* is the value you saved earlier from the *sites section*.

1. Once you have a conversation reference from generating a token, you can use this conversation ID to open a WebSocket with the new `StreamingConversations` property on the `DirectLineClient`. To do this you need to create a callback that will be invoked when the bot wants to send `ActivitySets` to the client:

    ```csharp
    public static void ReceiveActivities(ActivitySet activitySet)
    {
        if (activitySet != null)
        {
            foreach (var a in activitySet.Activities)
            {
                if (a.Type == ActivityTypes.Message && a.From.Id.Contains("bot"))
                {
                    Console.WriteLine($"<Bot>: {a.Text}");
                }
            }
        }
    }
    ```

1. Now you are ready to open the WebSocket on the `StreamingConversations` property using the conversation's token, `conversationId`, and your `ReceiveActivities` callback:

    ```csharp
    var client = new DirectLineClient(
        new Uri(endpoint),
        new DirectLineClientCredentials(conversation.Token));

    await client.StreamingConversations.ConnectAsync(
        conversation.ConversationId,
        ReceiveActivities);
    ```

1. The client can now be used to start a conversation and send `Activities` to the bot:

    ```csharp

    var startConversation = await client.StreamingConversations.StartConversationAsync();
    var from = new ChannelAccount() { Id = "123", Name = "Fred" };
    var message = Console.ReadLine();

    while (message != "end")
    {
        try
        {
            var response = await client.StreamingConversations.PostActivityAsync(
                startConversation.ConversationId,
                new Activity()
                {
                    Type = "message",
                    Text = message,
                    From = from
                });
        }
        catch (OperationException ex)
        {
            Console.WriteLine(
                $"OperationException when calling PostActivityAsync: ({ex.StatusCode})");
        }
        message = Console.ReadLine();
    }
    ```

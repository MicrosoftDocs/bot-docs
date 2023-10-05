---
title: Connect a bot to Direct Line in Bot Framework SDK
description: Learn how to configure bots to communicate with client applications using the Direct Line channel.
keywords: direct line, bot channels, custom client, connect to channels, configure
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: Daniel.Evans
ms.service: bot-service
ms.topic: how-to
ms.date: 11/15/2022
---

# Connect a bot to Direct Line

This article describes how to connect a bot to the **Direct Line** channel. Use this channel to communicate with a bot via your client application.

> [!NOTE]
> Direct Line is a standard channel over HTTPS protocol to allow communication between a client application and a bot. If you require network isolation instead, use the [Direct Line App Service Extension](bot-service-channel-directline-extension.md) over [WebSockets](https://tools.ietf.org/html/rfc6455).

## Prerequisites

- An Azure account. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- An existing bot published to Azure.

## Add the Direct Line channel

The first thing you need to do is to add the Direct Line channel to your bot.

1. Go to the [Azure portal](https://portal.azure.com/).
1. Go to your Azure Bot resource. Under **Bot Settings**, select **Channels**.
1. Select **Direct Line** from the list of **Available Channels**.

Your bot's now configured to use Direct Line using the **Default site**.

Alternatively, you can add a new site instead of using the default site. Select the **New site** button on the **Direct Line** channel page to create a new site.
    :::image type="content" source="media/bot-service-channel-connect-directline/directline-newsite.png" alt-text="Direct Line new site button in Azure portal":::

## Manage secret keys

When you add the Direct Channel, the Bot Framework generates secret keys. Your client application uses these keys to authenticate the Direct Line API requests that it issues to communicate with a bot. For more information, see [Authentication](rest-api/bot-framework-rest-direct-line-3-0-authentication.md).

1. To view a site's Direct Line secret in plain text, go to the **Direct Line** channel page.
1. Select the **Direct Line** tab, then the site you want to get the key for, such as **Default_Site**. Azure will open a **Configure the site** pane.
1. Under **Secret keys**, select the eye icon next to the corresponding key.

    :::image type="content" source="media/bot-service-channel-connect-directline/directline-showkey.png" alt-text="Show Direct Line keys":::

1. Copy and securely store the key. Use the key to [authenticate](rest-api/bot-framework-rest-direct-line-3-0-authentication.md) the Direct Line API requests that your client application issues to communicate with a bot.

    > [!NOTE]
    > Secrets shouldn't be exposed or embedded in client applications. See next step.

1. The best practice is to use the Direct Line API to [exchange the key for a token](rest-api/bot-framework-rest-direct-line-3-0-authentication.md#generate-token). The client application then will use the token to authenticate its requests within the scope of a single conversation.

## Configure settings

To configure your site settings:

1. On the Direct Line channel page, select the site you want to configure from the **Sites** list. The **Configure the site** pane will open, shown below:
    :::image type="content" source="media/bot-service-channel-connect-directline/directline-configure-site.png" alt-text="Configure site pane":::
1. Select the Direct Line protocol version that your client application will use to communicate with a bot.

    > [!TIP]
    > If you're creating a new connection between your client application and bot, use Direct Line API 3.0.

1. When finished, select **Apply** to save the site configuration. Repeat this process, beginning with a new site, for each client application that you want to connect to your bot.

### Configure enhanced authentication

One of the available site configurations is **Enhanced authentication options**, which helps to mitigate security risks when connecting to a bot (using the Web Chat control, for example). For more information, see [Direct Line enhanced authentication](v4sdk/bot-builder-security-enhanced.md).

To add enhanced authentication:

1. Enable **Enhance authentication options**. A message that says "You must have at least one trusted origin." will appear with an **Add a trusted origin** link. If you enable the enhanced authentication, you must specify at least one trusted origin.

    A trusted origin is a domain used by the system to authenticate users. In this case, Direct Line uses the domain to generate a token.

    - If you configure trusted origins as part of the configuration UI page, these settings will always be used as the only set for the generation of a token. Sending additional trusted origins (or setting trusted origins to none) when either generating a token or starting a conversation will be ignored (they aren't appended to the list or cross validated).
    - If you didn't enable enhanced authentication, any origin URL you send as part of the API calls will be used.
    :::image type="content" source="media/bot-service-channel-connect-directline/directline-enhanced-authentication.png" alt-text="Add trusted origin":::
1. After adding a trusted domain URL, select **Apply**.

## Direct Line example bot

You can download a .NET example from this location: [Direct Line Bot Sample](https://github.com/microsoft/BotFramework-DirectLine-DotNet/tree/master/samples/core-DirectLine).

The example contains two projects:

- [DirectLineBot](https://github.com/microsoft/BotFramework-DirectLine-DotNet/tree/master/samples/core-DirectLine/DirectLineBot). It creates a bot to connect via a Direct Line channel.
- [DirectLineClient](https://github.com/microsoft/BotFramework-DirectLine-DotNet/tree/master/samples/core-DirectLine/DirectLineClient). This is a console application that talks to the previous bot via Direct Line channel.

### Direct Line API

- Credentials for the Direct Line API must be obtained from the Azure Bot registration, and will only allow the caller to connect to the bot for which they were generated. In the bot project, update the `appsettings.json` file with these values.

    ```csharp
    {
    "MicrosoftAppId": "",
    "MicrosoftAppPassword": ""
    }
    ```

- In the Azure portal, enable Direct Line in the channels list, and then configure the Direct Line secret. Make sure that the checkbox for version 3.0 is checked. In the console client project, update the `App.config` file with the Direct Line secret key and the bot handle (Bot ID).

    ```xml
    <appSettings>
        <add key="DirectLineSecret" value="YourBotDirectLineSecret" />
        <add key="BotId" value="YourBotHandle" />
    </appSettings>
    ```

User messages are sent to the bot using the Direct Line Client `Conversations.PostActivityAsync` method using the `ConversationId` generated previously.

```csharp
while (true)
{
    string input = Console.ReadLine().Trim();

    if (input.ToLower() == "exit")
    {
        break;
    }
    else
    {
        if (input.Length > 0)
        {
            Activity userMessage = new Activity
            {
                From = new ChannelAccount(fromUser),
                Text = input,
                Type = ActivityTypes.Message
            };

            await client.Conversations.PostActivityAsync(conversation.ConversationId, userMessage);
        }
    }
}
```

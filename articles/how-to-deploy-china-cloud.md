---
title: Deploy Bots to Azure Mooncake
description: Learn how to configure a bot to operate in the Microsoft Azure Mooncake.
author: singhvikra-micro
ms.author: singhvikra
manager: kunsingh
ms.reviewer: kparihar
ms.service: azure-ai-bot-service
ms.topic: how-to
ms.date: 09/23/2024
ms.custom:
  - template-how-to
  - evergreen
---

# Configure Bot Framework bots for China customers

This article is for china customers who are deploying Bot Framework and Azure AI Bot Service bots to the Microsoft Azure Mooncake cloud.

## Prerequisites

- An account in the Azure Mooncake cloud.
- The C# or JavaScript bot project you want to configure.
- Bot Framework SDK version 4.14 or later.

## Use the cloud adapter

Make sure that your bot uses the _cloud adapter_, or an adapter that derives from the cloud adapter.
The cloud adapter lets you specify settings specific to the Azure Mooncake cloud.

### [C#](#tab/csharp)

The `ConfigurationBotFrameworkAuthentication` class reads authentication settings from your bot configuration file.
The cloud adapter, when it's created, will use these authentication settings.

Make sure that the `ConfigureServices` method in your **Startup.cs** file contains this line.

```csharp
services.AddSingleton<BotFrameworkAuthentication, ConfigurationBotFrameworkAuthentication>();
```

### [JavaScript](#tab/javascript)

The `ConfigurationBotFrameworkAuthentication` constructor reads authentication settings from your bot configuration file.

In your **index.js** file, the code to create your adapter should look like this:

```javascript
const botFrameworkAuthentication = new ConfigurationBotFrameworkAuthentication(process.env);

const adapter = new CloudAdapter(botFrameworkAuthentication);
```

---

## Configure UserAssignedMSI/SingleTenant Bot

Additional authentication settings are necessary for the bot to function correctly in the Mooncake environment. Ensure to replace "App-Tenant-Id" with the bot's tenant ID.

### [C#](#tab/csharp)

Add the following settings to your **appsettings.json** file.

```json
"OAuthUrl": "https://token.botframework.azure.cn/", 
"ToChannelFromBotLoginUrl": "https://login.partner.microsoftonline.cn/<App-Tenant-Id>",
"ToChannelFromBotOAuthScope": "https://api.botframework.azure.cn",
"ToBotFromChannelTokenIssuer": "https://api.botframework.azure.cn",
"ToBotFromChannelOpenIdMetadataUrl": "https://login.botframework.azure.cn/v1/.well-known/openidconfiguration",
"ToBotFromEmulatorOpenIdMetadataUrl": "https://login.partner.microsoftonline.cn/a55a4d5b-9241-49b1-b4ff-befa8db00269/v2.0/.well-known/openid-configuration",
"ValidateAuthority": true
```

### [JavaScript](#tab/javascript)

Add the following settings to your **.env** file.

```ini
OAuthUrl=https://token.botframework.azure.cn/, 
ToChannelFromBotLoginUrl=https://login.partner.microsoftonline.cn/<App-Tenant-Id>,
ToChannelFromBotOAuthScope=https://api.botframework.azure.cn,
ToBotFromChannelTokenIssuer=https://api.botframework.azure.cn,
ToBotFromChannelOpenIdMetadataUrl=https://login.botframework.azure.cn/v1/.well-known/openidconfiguration,
ToBotFromEmulatorOpenIdMetadataUrl=https://login.partner.microsoftonline.cn/a55a4d5b-9241-49b1-b4ff-befa8db00269/v2.0/.well-known/openid-configuration,
ValidateAuthority=true
```

---

## Configure MultiTenant Bot

For the MultiTenant bot use the following settings.

### [C#](#tab/csharp)

Add the following settings to your **appsettings.json** file.

```json
"OAuthUrl": "https://token.botframework.azure.cn/", 
"ToChannelFromBotLoginUrl": "https://login.partner.microsoftonline.cn/microsoftservices.partner.onmschina.cn",
"ToChannelFromBotOAuthScope": "https://api.botframework.azure.cn",
"ToBotFromChannelTokenIssuer": "https://api.botframework.azure.cn",
"ToBotFromChannelOpenIdMetadataUrl": "https://login.botframework.azure.cn/v1/.well-known/openidconfiguration",
"ToBotFromEmulatorOpenIdMetadataUrl": "https://login.partner.microsoftonline.cn/a55a4d5b-9241-49b1-b4ff-befa8db00269/v2.0/.well-known/openid-configuration",
"ValidateAuthority": true
```

### [JavaScript](#tab/javascript)

Add the following settings to your **.env** file.

```ini
OAuthUrl=https://token.botframework.azure.cn/, 
ToChannelFromBotLoginUrl=https://login.partner.microsoftonline.cn/microsoftservices.partner.onmschina.cn,
ToChannelFromBotOAuthScope=https://api.botframework.azure.cn,
ToBotFromChannelTokenIssuer=https://api.botframework.azure.cn,
ToBotFromChannelOpenIdMetadataUrl=https://login.botframework.azure.cn/v1/.well-known/openidconfiguration,
ToBotFromEmulatorOpenIdMetadataUrl=https://login.partner.microsoftonline.cn/a55a4d5b-9241-49b1-b4ff-befa8db00269/v2.0/.well-known/openid-configuration,
ValidateAuthority=true
```

--- 

## Add user authentication to your bot

Your bot can use various identity providers to access resources on behalf of a user, such as Microsoft Entra ID and many other OAuth providers.

The Mooncake environment uses a redirect URL that is different from the ones used for other environments.
When configuring your bot for authentication within the Mooncake environment, use `https://token.botframework.azure.cn/.auth/web/redirect` as the OAuth redirect URL and follow the steps in how to [add authentication to your bot](v4sdk/bot-builder-authentication.md).

---

## Configure a bot to run on one or more channels

To configure a bot to connect to a channel, complete the following steps:

1. Sign in to the [Azure portal](https://portal.azure.cn).
2. Select the bot that you want to configure.
3. In the left pane, select **Channels** under **Settings**.
4. In the right pane, select the icon of the channel you want to add to your bot. You may need to scroll down to see the list of all **Available Channels**.


The connection steps are different for each channel. See the related article in the table below more information. (Mooncake supported channels)


| Channel | Description |
|:-|:-|
| [Direct Line](bot-service-channel-directline.md) | Integrate a bot into a mobile app, web page, or other applications. |
| [Microsoft Teams](channel-connect-teams.md) | Configure a bot to communicate with users through Microsoft Teams. |
| [Web Chat](bot-service-channel-connect-webchat.md) | Automatically configured for you when you create a bot with the Bot Framework Service. |

---

## Next steps

With these steps your bot should be configured to work successfully in the Azure Mooncake cloud. Other useful references regarding Bot Service in Azure Mooncake.

- [Tutorial: Deploy a basic bot using Azure AI Bot Service](tutorial-publish-a-bot.md)
- [Add authentication to a bot in Bot Framework SDK](v4sdk/bot-builder-authentication.md)
- [Connect a bot to Web Chat in the Bot Framework SDK](bot-service-channel-connect-webchat.md)
- [Authenticate requests with the Bot Connector API](rest-api/bot-framework-rest-connector-authentication.md)
- [Compliance in the Azure AI Bot Service](bot-service-compliance.md)

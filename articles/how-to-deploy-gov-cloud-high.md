---
title: Deploy Bots to Azure Government and Office 365 GCC High
description: Learn how to configure a bot to operate in the Microsoft Azure Government cloud and the Microsoft Office 365 Government Community Cloud (GCC) High environment.
author: jameslew
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: bot-service
ms.topic: how-to
ms.date: 05/23/2022
ms.custom: template-how-to
---

# Configure Bot Framework bots for US Government customers

This article is for US government customers who are deploying Bot Framework and Azure Bot Service bots to the Microsoft Azure Government cloud.

> [!TIP]
> Bots in Azure Government that connect to Microsoft Teams must use the Microsoft Office 365 Government Community Cloud (GCC) High environment.

This article describes how to configure a bot to work with the Azure Government cloud and with the Office 365 GCC High environment.

## Prerequisites

- An account in the Azure Government cloud.
- To extend Teams, an Azure Bot resource created in the Office 365 GCC High environment.
- The C# or JavaScript bot project you want to configure.
- Bot Framework SDK version 4.14 or later.

## Use the cloud adapter

Make sure that your bot uses the _cloud adapter_, or an adapter that derives from the cloud adapter.
The cloud adapter lets you specify settings specific to the Azure Government cloud and the Office 365 GCC High environment.

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

## Configure for Azure Government

The Azure Government cloud uses `https://botframework.azure.us` for the channel service endpoint.
For most channels, setting the channel service endpoint is sufficient.

See the next section for additional settings required to support Microsoft Teams in the Office 365 GCC High environment.

### [C#](#tab/csharp)

Add the following setting to your **appsettings.json** file.

```json
"ChannelService": "https://botframework.azure.us", 
```

### [JavaScript](#tab/javascript)

Add the following setting to your **.env** file.

```ini
ChannelService=https://botframework.azure.us
```

---

## Configure for Office 365 GCC High

For Office 365 services, additional settings are required to handle user authentication correctly.
Currently, only the Microsoft Teams channel is available in the Office 365 GCC High environment.

### [C#](#tab/csharp)

Add the following settings to your **appsettings.json** file.

```json
"ChannelService": "https://botframework.azure.us", 
"OAuthUrl": "https://tokengcch.botframework.azure.us/", 
"ToChannelFromBotLoginUrl": "https://login.microsoftonline.us/MicrosoftServices.onmicrosoft.us",
"ToChannelFromBotOAuthScope": "https://api.botframework.us", 
"ToBotFromChannelTokenIssuer": "https://api.botframework.us", 
"ToBotFromChannelOpenIdMetadataUrl": "https://login.botframework.azure.us/v1/.well-known/openidconfiguration",
"ToBotFromEmulatorOpenIdMetadataUrl": "https://login.microsoftonline.us/cab8a31a-1906-4287-a0d8-4eef66b95f6e/v2.0/.well-known/openid-configuration",
"ValidateAuthority": true,
```

### [JavaScript](#tab/javascript)

Add the following settings to your **.env** file.

```ini
ChannelService=https://botframework.azure.us
OAuthUrl=https://tokengcch.botframework.azure.us/
ToChannelFromBotLoginUrl=https://login.microsoftonline.us/MicrosoftServices.onmicrosoft.us
ToChannelFromBotOAuthScope=https://api.botframework.us
ToBotFromChannelTokenIssuer=https://api.botframework.us
ToBotFromChannelOpenIdMetadataUrl=https://login.botframework.azure.us/v1/.well-known/openidconfiguration
ToBotFromEmulatorOpenIdMetadataUrl=https://login.microsoftonline.us/cab8a31a-1906-4287-a0d8-4eef66b95f6e/v2.0/.well-known/openid-configuration
ValidateAuthority=true
```

---

There is also a **DoD environment** which shares most (but not all) settings with the Office 365 GCC High environment. For the DoD environment use the following settings.

### [C#](#tab/csharp)

Add the following settings to your **appsettings.json** file.

```json
"ChannelService": "https://botframework.azure.us", 
"OAuthUrl": "https://apiDoD.botframework.azure.us", 
"ToChannelFromBotLoginUrl": "https://login.microsoftonline.us/MicrosoftServices.onmicrosoft.us",
"ToChannelFromBotOAuthScope": "https://api.botframework.us", 
"ToBotFromChannelTokenIssuer": "https://api.botframework.us", 
"ToBotFromChannelOpenIdMetadataUrl": "https://login.botframework.azure.us/v1/.well-known/openidconfiguration",
"ToBotFromEmulatorOpenIdMetadataUrl": "https://login.microsoftonline.us/cab8a31a-1906-4287-a0d8-4eef66b95f6e/v2.0/.well-known/openid-configuration",
"ValidateAuthority": true,
```

### [JavaScript](#tab/javascript)

Add the following settings to your **.env** file.

```ini
ChannelService=https://botframework.azure.us
OAuthUrl=https://apiDoD.botframework.azure.us
ToChannelFromBotLoginUrl=https://login.microsoftonline.us/MicrosoftServices.onmicrosoft.us
ToChannelFromBotOAuthScope=https://api.botframework.us
ToBotFromChannelTokenIssuer=https://api.botframework.us
ToBotFromChannelOpenIdMetadataUrl=https://login.botframework.azure.us/v1/.well-known/openidconfiguration
ToBotFromEmulatorOpenIdMetadataUrl=https://login.microsoftonline.us/cab8a31a-1906-4287-a0d8-4eef66b95f6e/v2.0/.well-known/openid-configuration
ValidateAuthority=true
```

---

## Add user authentication to your bot

Your bot can use various identity providers to access resources on behalf of a user, such as Azure Active Directory (Azure AD) and many other OAuth providers.

The Office 365 GCC High environment uses a redirect URL that is different from the ones used for other environments.
When configuring your bot for authentication within the Office 365 GCC High environment, use `https://tokengcch.botframework.azure.us/.auth/web/redirect` as the OAuth redirect URL and follow the steps in how to [add authentication to your bot](v4sdk/bot-builder-authentication.md).

## Additional information

For more information about Microsoft Azure Government and Office 365 Government High, see:

- [What is Azure Government?](/azure/azure-government/documentation-government-welcome)
- [Office 365 Government High and DoD](/office365/servicedescriptions/office-365-platform-service-description/office-365-us-government/gcc-high-and-dod)
- [Teams for Government](/microsoftteams/expand-teams-across-your-org/teams-for-government-landing-page)

## Next steps

With these steps your bot should be configured to work successfully in the Azure Government cloud and the Office 365 GCC High environment.
Other useful references regarding Bot Service in Azure Government.

- [Tutorial: Deploy a basic bot using Azure Bot Service](tutorial-publish-a-bot.md)
- [Add authentication to a bot in Bot Framework SDK](v4sdk/bot-builder-authentication.md)
- [Connect a bot to Web Chat in the Bot Framework SDK](bot-service-channel-connect-webchat.md)
- [Authenticate requests with the Bot Connector API](rest-api/bot-framework-rest-connector-authentication.md)
- [Compliance in the Azure Bot Service](bot-service-compliance.md)
- [Azure Government Documentation](/azure/azure-government/)

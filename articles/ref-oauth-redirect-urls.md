---
title: Supported OAuth URLs
description: Azure AI Bot Service provides various OAuth URLs. Choose a URL based on data residency requirements and which cloud your bot is in.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: reference
ms.service: azure-ai-bot-service
ms.date: 05/23/2022
ms.custom:
  - evergreen
---

# OAuth URL support in Azure AI Bot Service

Azure AI Bot Service provides different OAuth and OAuth redirect URLs to meet specific needs.

- The bot needs the OAuth URL at run time.
- You need to provide the OAuth redirect URL when you create or configure your OAuth identity provider.
- For more information, see [how to add authentication to your bot](v4sdk/bot-builder-authentication.md).

Choose the URLs to use with your bot and identity provider based on your data residency requirements and whether your bot is in the public cloud or the Microsoft Azure Government cloud or the Microsoft Azure operated by 21Vianet.

| Data residency | Cloud            | OAuth URL                                     | OAuth Redirect URL                                               |
|:---------------|:-----------------|:----------------------------------------------|:-----------------------------------------------------------------|
| None           | Public           | `https://token.botframework.com`              | `https://token.botframework.com/.auth/web/redirect`              |
| Europe         | Public           | `https://europe.token.botframework.com`       | `https://europe.token.botframework.com/.auth/web/redirect`       |
| United States  | Public           | `https://unitedstates.token.botframework.com` | `https://unitedstates.token.botframework.com/.auth/web/redirect` |
| India         | Public           | `https://india.token.botframework.com`       | `https://india.token.botframework.com/.auth/web/redirect`       |
| None           | Azure Government | `https://token.botframework.azure.us`         | `https://token.botframework.azure.us/.auth/web/redirect`         |
| None           | Azure operated by 21Vianet | `https://token.botframework.azure.cn`         | `https://token.botframework.azure.cn/.auth/web/redirect`         |

The default OAuth and OAuth redirect URLs are `https://token.botframework.com` and `https://token.botframework.com/.auth/web/redirect`, which can be used for public-cloud bots with no data residency requirements.

## To configure OAuthUrl in bot
Update appsettings.json to include OAuthUrl options:

```json
{
  "MicrosoftAppType": "",
  "MicrosoftAppId": "",
  "MicrosoftAppPassword": "",

  "BotOpenIdMetadata": "https://login.botframework.com/v1/.well-known/openidconfiguration",
  "CallerId": "urn:botframework:azure",
  "OAuthUrl": "https://europe.token.botframework.com/",
  "ToBotFromChannelOpenIdMetadataUrl": "https://login.botframework.com/v1/.well-known/openidconfiguration",
  "ToBotFromChannelTokenIssuer": "https://api.botframework.com",
  "ToBotFromEmulatorOpenIdMetadataUrl": "https://login.microsoftonline.com/botframework.com/v2.0/.well-known/openid-configuration",
  "ToChannelFromBotLoginUrl": "https://login.microsoftonline.com/{0}",
  "ToChannelFromBotOAuthScope": "https://api.botframework.com",
  "ValidateAuthority": true
}
```

## Additional information

Some environments use endpoints different than the ones listed here.

See these articles for related information.

- [Regionalization in Azure AI Bot Service](v4sdk/bot-builder-concept-regionalization.md)
- [Bot Framework authentication basics](v4sdk/bot-builder-authentication-basics.md)
- [Configure Bot Framework bots for US Government customers](how-to-deploy-gov-cloud-high.md)
- [Configure a bot in Microsoft Azure operated by 21Vianet](how-to-deploy-china-cloud.md)

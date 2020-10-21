---
title: Bot Framework Frequently Asked Questions Security and Privacy - Bot Service
description: Frequently Asked Questions about Bot Framework security and privacy.
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 06/09/2020
---

# Security and Privacy

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

<!-- Attention writers!!
     1 - This article contains FAQs regarding Bot Framework security and privacy.
     1 - When you create a new FAQ, please add the related link to the proper section in bot-service-resources-bot-framework-faq.md.-->

## Do the bots registered with the Bot Framework collect personal information? If yes, how can I be sure the data is safe and secure? What about privacy?

Each bot is its own service, and developers of these services are required to provide Terms of Service and Privacy Statements per their Developer Code of Conduct. For more information, see [bot review guidelines](https://docs.microsoft.com/azure/bot-service/bot-service-review-guidelines?view=azure-bot-service-4.0).

## Can I host my bot on my own servers?
Yes. Your bot can be hosted anywhere on the Internet. On your own servers, in Azure, or in any other datacenter. The only requirement is that the bot must expose a publicly-accessible HTTPS endpoint.

## How do you ban or remove bots from the service?

Users have a way to report a misbehaving bot via the bot's contact card in the directory. Developers must abide by Microsoft terms of service to participate in the service.

## Which specific URLs do I need to allow-list in my corporate firewall to access Bot Framework services?

If you have an outbound firewall blocking traffic from your bot to the Internet, you'll need to allow-list the following URLs in that firewall:

- `login.botframework.com` (Bot authentication)
- `login.microsoftonline.com` (Bot authentication)
- `westus.api.cognitive.microsoft.com` (for Luis.ai NLP integration)
- `cortanabfchanneleastus.azurewebsites.net` (Cortana channel)
- `cortanabfchannelwestus.azurewebsites.net` (Cortana Channel)
- `*.botframework.com` (channels)
- `state.botframework.com` (backward compatibility)
- `login.windows.net` (Windows login)
- `login.windows.com` (Windows login)
- `sts.windows.net` (Windows login)
- Additional URLs for specific Bot Framework channels

> [!NOTE]
> You may use `<channel>.botframework.com` if you'd prefer not to allow-list a URL with an asterisk. `<channel>` is equal to every channel your bot uses such as `directline.botframework.com`, `webchat.botframework.com`, and `slack.botframework.com`. It is also worthwhile to watch traffic over your firewall while testing the bot to make sure nothing else is getting blocked.

## Can I block all traffic to my bot except traffic from the Bot Framework Service?
Bot Framework Services are hosted in Azure datacenters world-wide and the list of Azure IPs is constantly changing. allow-listing certain IP addresses may work one day and break the next as the Azure IP Addresses change.

## Which RBAC role is required to create and deploy a bot?

Creating a bot in the Azure portal requires Contributor access either in the subscription or in a specific resource group. A user with the *Contributor* role in a resource group can create a new bot in that specific resource group. A user in the *Contributor* role for a subscription can create a bot in a new or existing resource group.

Using the Azure CLI, a role-based access control approach can support custom roles. If you want to make a custom role with more constrained permissions, the following set will allow the user to create and deploy a bot that also supports LUIS, QnA Maker, and Application Insights.

  "Microsoft.Web/*",
  "Microsoft.BotService/*",
  "Microsoft.Storage/*",
  "Microsoft.Resources/deployments/*",
  "Microsoft.CognitiveServices/*",
  "Microsoft.Search/searchServices/*",
   "Microsoft.Insights/*",
  "Microsoft.Insights/components/*"

LUIS and QnA Maker require Cognitive Services permissions. QnA Maker also requires Search permissions. When creating a custom role, remember that any inherited *deny* permissions will supercede these *allow* permissions.

## What keeps my bot secure from clients impersonating the Bot Framework Service?

1. All authentic Bot Framework requests are accompanied by a JWT token whoes cryptographic signature can be verified by following the [authentication](https://docs.microsoft.com/azure/bot-service/rest-api/bot-framework-rest-connector-authentication?view=azure-bot-service-3.0#bot-to-connector) guide. The token is designed so attackers cannot impersonate trusted services.
2. The security token accompanying every request to your bot has the ServiceUrl encoded within it, which means that even if an attacker gains access to the token, they cannot redirect the conversation to a new ServiceUrl. This is enforced by all implementations of the SDK and documented in our authentication [reference](https://docs.microsoft.com/azure/bot-service/rest-api/bot-framework-rest-connector-authentication?view=azure-bot-service-3.0#bot-to-connector) materials.
3. If the incoming token is missing or malformed, the Bot Framework SDK will not generate a token in response. This limits how much damage can be done if the bot is incorrectly configured.
4. Inside the bot, you can manually check the ServiceUrl provided in the token. This makes the bot more fragile in the event of service topology changes so this is possible but not recommended.

Note that these are outbound connections from the bot to the Internet. There is not a list of IP Addresses or DNS names that the Bot Framework Connector Service will use to talk to the bot. Inbound IP Address allow-listing is not supported.

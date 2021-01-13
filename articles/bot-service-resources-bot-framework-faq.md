---
title: Bot Framework Frequently Asked Questions Index - Bot Service
description: Frequently Asked Questions Bot Framework Index.
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/23/2020
---


# Bot Framework FAQ

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

<!-- Attention writers!! When you create a new FAQ, please add the related link in the proper section below.
Also, notice this topic is in markdown (.md) format because being an index it is simpler to maintain.
The actual FAQs are contained in separate files that are in yaml (.yml) format. The reasons for the yaml format
are explained in this article: Create FAQ content (https://review.docs.microsoft.com/en-us/help/contribute/contribute-how-to-faq-guide?branch=master).
-->


The following are some common questions you might have.
In case you don't find the answer you're looking for, you can post your questions on [Stack Overflow](https://stackoverflow.com/questions/tagged/botframework) using the `botframework` tag. If you're a new user, visit the [Stack Overflow Help Center](https://stackoverflow.com/help/how-to-ask) first.

## Background and availability

- [Why did Microsoft develop the Bot Framework?](bot-service-resources-faq-availability.md#why-did-microsoft-develop-the-bot-framework)
- [How to run a bot offline?](bot-service-resources-faq-availability.md#how-to-run-a-bot-offline)
- [How can I migrate Azure Bot Service from one region to another?](bot-service-resources-faq-availability.md#how-can-i-migrate-azure-bot-service-from-one-region-to-another)
- [What is the v4 SDK?](bot-service-resources-faq-availability.md#what-is-the-v4-sdk)
- [Bot Framework SDK Version 3 Lifetime Support and Deprecation Notice](bot-service-resources-faq-availability.md#bot-framework-sdk-version-3-lifetime-support-and-deprecation-notice)

## Bot Framework general

- [Why doesn't the Typing activity do anything?](bot-service-resources-faq-general.md#why-doesnt-the-typing-activity-do-anything)
- [What is the difference between the Connector library and Builder library in the SDK?](bot-service-resources-faq-general.md#what-is-the-difference-between-the-connector-library-and-builder-library-in-the-sdk)
- [How do user messages relate to HTTPS method calls?](bot-service-resources-faq-general.md#how-do-user-messages-relate-to-https-method-calls)
- [How can I intercept all messages between the user and my bot?](bot-service-resources-faq-general.md#how-can-i-intercept-all-messages-between-the-user-and-my-bot)
- [What is the IDialogStack.Forward method in the Bot Framework SDK for .NET?](bot-service-resources-faq-general.md#what-is-the-idialogstackforward-method-in-the-bot-framework-sdk-for-net)
- [What is the difference between "proactive" and "reactive"?](bot-service-resources-faq-general.md#what-is-the-difference-between-proactive-and-reactive)
- [How can I send proactive messages to the user?](bot-service-resources-faq-general.md#how-can-i-send-proactive-messages-to-the-user)
- [How can I reference non-serializable services from my C# dialogs in SDK v3?](bot-service-resources-faq-general.md#how-can-i-reference-non-serializable-services-from-my-c-dialogs-in-sdk-v3)
- [What is an ETag? How does it relate to bot data bag storage?](bot-service-resources-faq-general.md#what-is-an-etag--how-does-it-relate-to-bot-data-bag-storage)
- [What is rate limiting?](bot-service-resources-faq-general.md#what-is-rate-limiting)
- [How does rate limiting occur?](bot-service-resources-faq-general.md#how-does-rate-limiting-occur)
- [What are the rate limits?](bot-service-resources-faq-general.md#what-are-the-rate-limits)
- [How will I know if I'm impacted by rate limiting?](bot-service-resources-faq-general.md#how-will-i-know-if-im-impacted-by-rate-limiting)
- [How to implement human handoff?](bot-service-resources-faq-general.md#how-to-implement-human-handoff)
- [What is the size limit of a file transferred using a channel?](bot-service-resources-faq-general.md#what-is-the-size-limit-of-a-file-transferred-using-channels)


## Ecosystem

- [How do I enable the Emulator to connect to localhost while behind a corporate proxy?](bot-service-resources-faq-ecosystem.md#how-do-i-enable-the-emulator-to-connect-to-localhost-while-behind-a-corporate-proxy)
- [When will you add more conversation experiences to the Bot Framework?](bot-service-resources-faq-ecosystem.md#when-will-you-add-more-conversation-experiences-to-the-bot-framework)
- [I have a communication channel I'd like to be configurable with Bot Framework. Can I work with Microsoft to do that?](bot-service-resources-faq-ecosystem.md#i-have-a-communication-channel-id-like-to-be-configurable-with-bot-framework-can-i-work-with-microsoft-to-do-that)
- [If I want to create a bot for Microsoft Teams, what tools and services should I use?](bot-service-resources-faq-ecosystem.md#if-i-want-to-create-a-bot-for-microsoft-teams-what-tools-and-services-should-i-use)
- [How do I create a bot that uses the US Government data center?](bot-service-resources-faq-ecosystem.md#how-do-i-create-a-bot-that-uses-the-us-government-data-center)
- [What is the Direct Line channel?](bot-service-resources-faq-ecosystem.md#what-is-the-direct-line-channel)
- [How does the Bot Framework relate to Cognitive Services?](bot-service-resources-faq-ecosystem.md#how-does-the-bot-framework-relate-to-cognitive-services)
- [What are the steps to configure Web Chat and Direct Line for Azure Government?](bot-service-resources-faq-ecosystem.md#what-are-the-steps-to-configure-web-chat-and-direct-line-for-azure-government)
- [What are the possible machine-readable resolutions of the LUIS built-in date, time, duration, and set entities?](bot-service-resources-faq-ecosystem.md#what-are-the-possible-machine-readable-resolutions-of-the-luis-built-in-date-time-duration-and-set-entities)
- [How can I use more than the maximum number of LUIS intents?](bot-service-resources-faq-ecosystem.md#how-can-i-use-more-than-the-maximum-number-of-luis-intents)
- [How can I use more than one LUIS model?](bot-service-resources-faq-ecosystem.md#how-can-i-use-more-than-one-luis-model)
- [Where can I get more help on LUIS?](bot-service-resources-faq-ecosystem.md#where-can-i-get-more-help-on-luis)

## Security and Privacy

- [Do the bots registered with the Bot Framework collect personal information? If yes, how can I be sure the data is safe and secure? What about privacy?](bot-service-resources-faq-security.md#do-the-bots-registered-with-the-bot-framework-collect-personal-information-if-yes-how-can-i-be-sure-the-data-is-safe-and-secure-what-about-privacy)
- [Can I host my bot on my own servers?](bot-service-resources-faq-security.md#can-i-host-my-bot-on-my-own-servers)
- [How do you ban or remove bots from the service?](bot-service-resources-faq-security.md#how-do-you-ban-or-remove-bots-from-the-service)
- [Which specific URLs do I need to allow-list in my corporate firewall to access Bot Framework services?](bot-service-resources-faq-security.md#which-specific-urls-do-i-need-to-allow-list-in-my-corporate-firewall-to-access-bot-framework-services)
- [Can I block all traffic to my bot except traffic from the Bot Framework Service?](bot-service-resources-faq-security.md#can-i-block-all-traffic-to-my-bot-except-traffic-from-the-bot-framework-service)
- [Which RBAC role is required to create and deploy a bot?](bot-service-resources-faq-security.md#which-rbac-role-is-required-to-create-and-deploy-a-bot)
- [What keeps my bot secure from clients impersonating the Bot Framework Service?](bot-service-resources-faq-security.md#what-keeps-my-bot-secure-from-clients-impersonating-the-bot-framework-service)
- [What is the purpose of the magic code during authentication?](bot-service-resources-faq-security.md#what-is-the-purpose-of-the-magic-code-during-authentication)

## Azure

- [How do I create my own App Registration?](bot-service-resources-faq-azure.md#how-do-i-create-my-own-app-registration)
- [What files do I need to zip up for deployment?](bot-service-resources-faq-azure.md#what-files-do-i-need-to-zip-up-for-deployment)
- [What version of Azure CLI should I use to deploy a bot?](bot-service-resources-faq-azure.md#what-version-of-azure-cli-should-i-use-to-deploy-a-bot)
- [What should I do when getting Azure CLI deprecation errors?](bot-service-resources-faq-azure.md#what-should-i-do-when-getting-azure-cli-deprecation-errors)
- [What are the CLI deprecated commands related to `az deployment?`](bot-service-resources-faq-azure.md#what-are-the-cli-deprecated-commands-related-to-az-deployment)
- [How do I know whether the Azure CLI commands are deprecated?](bot-service-resources-faq-azure.md#how-do-i-know-whether-the-azure-cli-commands-are-deprecated)

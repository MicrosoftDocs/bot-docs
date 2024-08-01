---
title: Embed a bot in a website with Azure AI Bot Service
description: Learn how to embed bots in websites by using a web control. See how the backchannel mechanism facilitates private communication between web pages and bots.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: azure-ai-bot-service
ms.date: 11/01/2021
ms.custom:
  - evergreen
---

# Embed a bot in a website

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

Although bots commonly exist outside of websites, they can also be embedded within a website. For example, you may embed a [knowledge bot](bot-service-design-pattern-knowledge-base.md) within a website to enable users to quickly find information that might otherwise be challenging to locate within complex website structures. Or you might embed a bot within a help desk website to act as the first responder to incoming user requests. The bot could independently resolve simple issues and [handoff](bot-service-design-pattern-handoff-human.md) more complex issues to a human agent.

This article explores how to integrate bots with websites and the process of using the *backchannel* mechanism to facilitate private communication between a web page and a bot.

Microsoft provides two different ways to integrate a bot in a website: the [Skype web control](bot-service-channel-connect-skype.md) and an [open source web control](#open-source-web-control).

## Open source web control

The open source [Web Chat](https://github.com/Microsoft/BotFramework-WebChat) control is based upon ReactJS and uses the [Direct Line API][directLineAPI] to communicate with the Bot Framework. The Web Chat control provides a blank canvas for implementing the Web Chat, giving you full control over its behaviors and the user experience that it delivers.

The *backchannel* mechanism enables the web page that is hosting the control to communicate directly with the bot in a manner that is entirely invisible to the user. This capability enables a number of useful scenarios:

- The web page can send relevant data to the bot, such as GPS location.
- The web page can advise the bot about user actions, such as "user just selected Option A from the dropdown".
- The web page can send the bot the auth token for a logged-in user.
- The bot can send relevant data to the web page, such as the current value of user's portfolio.
- The bot can send "commands" to the web page, such as a change to the background color.

## Using the backchannel mechanism

The [open source WebChat control](https://github.com/Microsoft/BotFramework-WebChat) communicates with bots by using the [Direct Line API](rest-api/bot-framework-rest-direct-line-3-0-concepts.md#client-libraries), which allows `activities` to be sent back and forth between client and bot. The most common type of activity is `message`, but there are other types as well. For example, the activity type `typing` indicates that a user is typing or that the bot is working to compile a response.

You can use the backchannel mechanism to exchange information between client and bot without presenting it to the user by setting the activity type to `event`. The Web Chat control will automatically ignore any activities where `type="event"`.

## Sample code

The open source [Web Chat](https://github.com/Microsoft/BotFramework-WebChat) control is available via GitHub. For details about how you can implement the backchannel mechanism using the open source Web Chat control and the Bot Framework SDK for Node.js, see [Use the backchannel mechanism](nodejs/bot-builder-nodejs-backchannel.md).

## Additional resources

- [Direct Line API][directLineAPI]
- [Open source Web Chat control](https://github.com/Microsoft/BotFramework-WebChat)
- [Use the backchannel mechanism](nodejs/bot-builder-nodejs-backchannel.md)

[directLineAPI]: rest-api/bot-framework-rest-direct-line-3-0-concepts.md#client-libraries

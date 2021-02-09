---
title: Embed a bot in a website - Bot Service
description: Learn how to embed bots in websites by using a web control. See how the backchannel mechanism facilitates private communication between web pages and bots.
author: matvelloso
ms.author: mateusv
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/13/2017

---

# Embed a bot in a website

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

Although bots commonly exist outside of websites, they can also be embedded within a website.
For example, you may embed a [knowledge bot](~/bot-service-design-pattern-knowledge-base.md) within a website
to enable users to quickly find information that might otherwise be challenging to locate within complex website structures.
Or you might embed a bot within a help desk website to act as the first responder to incoming user requests.
The bot could independently resolve simple issues and [handoff](~/bot-service-design-pattern-handoff-human.md) more complex issues to a human agent.

This article explores integrating bots with websites and the process of using the *backchannel* mechanism to facilitate private communication between a web page and a bot.

Microsoft provides two different ways to integrate a bot in a website:
the Skype web control and an open source web control.

## Skype web control

>[!NOTE]
> As of October 31, 2019 the Skype channel no longer accepts new Bot publishing requests. This means that you can continue to develop bots using the Skype channel, but your bot will be limited to 100 users. You will not be able to publish your bot to a larger audience. Current Skype bots will continue to run uninterrupted. Read more about [why some features are not available in Skype anymore](https://support.skype.com/faq/fa12091/why-are-some-features-not-available-in-skype-anymore).


The [Skype web control](https://aka.ms/bot-skype-web-control) is essentially a Skype client in a web-enabled control. Built-in Skype authentication enables the bot to authenticate and recognize users, without requiring the
developer to write any custom code. Skype will automatically recognize Microsoft Accounts used in its web client.

Because the Skype web control simply acts as a front-end for Skype,
the user's Skype client automatically has access to the full context of any conversation that the web control facilitates.
Even after the web browser is closed, the user may continue to interact with the bot using the Skype client.

## Open source web control

The <a href="https://aka.ms/BotFramework-WebChat" target="_blank">open source web chat control</a>
is based upon ReactJS and uses the
[Direct Line API][directLineAPI]
to communicate with the Bot Framework. The web chat control provides a blank canvas for implementing the web chat,
giving you full control over its behaviors and the user experience that it delivers.

The *backchannel* mechanism enables the web page that is hosting the control
to communicate directly with the bot in a manner that is entirely invisible to the user.
This capability enables a number of useful scenarios:

- The web page can send relevant data to the bot (e.g., GPS location).
- The web page can advise the bot about user actions (e.g., "user just selected Option A from the dropdown").
- The web page can send the bot the auth token for a logged-in user.
- The bot can send relevant data to the web page (e.g., current value of user's portfolio).
- The bot can send "commands" to the web page (e.g., change background color).

## Using the backchannel mechanism

[!INCLUDE [Introduction to backchannel mechanism](~/includes/snippet-backchannel.md)]

## Sample code

The <a href="https://aka.ms/BotFramework-WebChat" target="_blank">open source web chat control</a> is available via GitHub. For details about how you can implement the backchannel mechanism using the open source web chat control and the Bot Framework SDK for Node.js, see [Use the backchannel mechanism](~/nodejs/bot-builder-nodejs-backchannel.md).

## Additional resources

- [Direct Line API][directLineAPI]
- [Open source web chat control](https://github.com/Microsoft/BotFramework-WebChat)
- [Use the backchannel mechanism](~/nodejs/bot-builder-nodejs-backchannel.md)

[directLineAPI]: rest-api/bot-framework-rest-direct-line-3-0-concepts.md#client-libraries

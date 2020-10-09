---
title: What is the Bot Framework SDK? - Bot Service
description: Learn about the Azure Bot Service and the Bot Framework SDK, for building, connecting, testing, deploying, monitoring, and managing bots.
keywords: overview, introduction, SDK, outline
author: Kaiqb
ms.author: kamrani
manager: kamrani
ms.topic: overview
ms.service: bot-service
ms.date: 11/15/2019
---

# What is the Bot Framework SDK?

<!-- Alternate titles:
# About the Bot Framework SDK and the Azure Bot Service
-->

[!INCLUDE [applies-to-both](includes/applies-to-both.md)]

The Bot Framework, along with the Azure Bot Service, provides tools to build, test, deploy, and manage intelligent bots, all in one place. The Bot Framework includes a modular and extensible SDK for building bots, as well as tools, templates, and related AI services. With this framework, developers can create bots that use speech, understand natural language, handle questions and answers, and more.

<!--
    - [Azure] Bot Service - The service that accelerates the process of developing a bot. It provisions a web host with one of five bot templates that can be modified in an integrated environment.
    - bot service - An instance created by the user using Azure Bot Service.
    - [Microsoft] Bot Framework - The comprehensive offering to build and deploy high quality bots for users to enjoy wherever they are talking. Users can start conversations with your bot from any channel that you’ve configured your bot to work on, such as SMS, Skype, Slack, Facebook, and other popular services.
-->

## What is a bot?

Bots provide an experience that feels less like using a computer and more like dealing with a person - or at least an intelligent robot. They can be used to shift simple, repetitive tasks, such as taking a dinner reservation or gathering profile information, on to automated systems that may no longer require direct human intervention. Users converse with a bot using text, interactive cards, and speech. A bot interaction can be a quick question and answer, or it can be a sophisticated conversation that intelligently provides access to services.

A bot can be thought of as a web application that has a conversational interface.
A user connects to a bot though a channel such as Facebook, Slack, or Microsoft Teams.

- The bot _reasons_ about input and performs relevant tasks. This can include asking the user for additional information or accessing services on behalf of the user.
- The bot performs recognition on the user's input to interpret what the user is asking for or saying.
- The bot generates responses to send to the user to communicate what the bot is doing or has done.
- Depending on how the bot is configured and how it is registered with the channel, users can interact with the bot through text or speech, and the conversation might include images and video.

> [!div class="mx-imgBorder"]
> ![A remote bot interacts with a user on a device via text, speech, images, or video](./media/architecture/what-is-a-bot.png)

Bots are a lot like modern web applications, living on the internet and use APIs to send and receive messages. What's in a bot varies widely depending on what kind of bot it is. Modern bot software relies on a stack of technology and tools to deliver increasingly complex experiences on a wide variety of platforms. However, a simple bot could just receive a message and echo it back to the user with very little code involved.

Bots can do the same things other types of software can do - read and write files, use databases and APIs, and do the regular computational tasks. What makes bots unique is their use of mechanisms generally reserved for human-to-human communication.

The Azure Bot Service and the Bot Framework offer:

- The Bot Framework SDK for developing bots
- Bot Framework Tools to cover end-to-end bot development workflow
- Bot Framework Service (BFS) to send and receive messages and events between bots and channels
- Bot deployment and channel configuration in Azure

Additionally, bots may use other Azure services, such as:

- Azure Cognitive Services to build intelligent applications
- Azure Storage for cloud storage solution

<!-- Bot Framework Service - The service that communicates with your bot to send and receive messages and events. -->
<!-- [Microsoft] Azure Storage - The Microsoft Azure service that lets you store binary data and text data in blobs, unstructured non-relational data in tables, and messages for workflow and communication in queues. -->
<!-- [Microsoft] Cognitive Services - The family of services to build apps with powerful algorithms using just a few lines of code, which work across devices and platforms such as iOS, Android, and Windows, are easy to set up and enable natural and contextual interaction with tools that augment users' experiences using the power of machine-based intelligence. Microsoft Translator is part of Cognitive Services. -->

## Building a bot

Azure Bot Service and Bot Framework offer an integrated set of tools and services to facilitate this process. Choose your favorite development environment or command line tools to create your bot. SDKs exist for C#, JavaScript, Typescript and Python (the SDK for Java is under development). We provide tools for various stages of bot development to help you design and build bots.

![Bot Overview](media/bot-service-overview.png)

### Plan

As with any type of software, having a thorough understanding of the goals, processes and user needs is important to the process of creating a successful bot. Before writing code, review the bot [design guidelines](bot-service-design-principles.md) for best practices and identify the needs for your bot. You can create a simple bot or include more sophisticated capabilities such as speech, natural language understanding, and question answering.

### Build

Your bot is a web service that implements a conversational interface and communicates with the Bot Framework Service to send and receive messages and events. Bot Framework Service is one of the components of the Azure Bot Service and Bot Framework. You can create bots in any number of environments and languages. You can start your bot development in the [Azure portal](bot-service-quickstart.md), or use [[C#](dotnet/bot-builder-dotnet-sdk-quickstart.md) | [JavaScript](javascript/bot-builder-javascript-quickstart.md) | [Python](python/bot-builder-python-quickstart.md)] templates for local development.

As part of the Azure Bot Service and Bot Framework, we offer additional components you can use to extend your bot's functionality:

| Feature | Description | Link |
| --- | --- | --- |
| Add natural language processing | Enable your bot to understand natural language, understand spelling errors, use speech, and recognize the user's intent | How to use [LUIS](~/v4sdk/bot-builder-howto-v4-luis.md)
| Answer questions | Add a knowledge base to answer questions users ask in a more natural, conversational way | How to use [QnA Maker](~/v4sdk/bot-builder-howto-qna.md)
| Manage multiple models | If using more than one model, such as for LUIS and QnA Maker, intelligently determine when to use which one during your bot's conversation | [Dispatch](~/v4sdk/bot-builder-tutorial-dispatch.md) tool|
| Add cards and buttons | Enhance the user experience with media other than text, such as graphics, menus, and cards | How to [add cards](v4sdk/bot-builder-howto-add-media-attachments.md) |

> [!NOTE]
> The table above is not a comprehensive list. Explore the articles on the left, starting with [sending messages](~/v4sdk/bot-builder-howto-send-messages.md), for more bot functionality.

Additionally, we provide command line tools to help you to create, manage, and test bot assets. These tools can configure LUIS apps, build a QnA knowledge base, build models to dispatch between components, mock a conversation, and more. You can find more details in the command line tools [readme](https://aka.ms/botbuilder-tools-readme).

You also have access to a variety of [samples](https://github.com/microsoft/botbuilder-samples) that showcase many of the capabilities available through the SDK. These are great for developers looking for a more feature rich starting point.

### Test
Bots are complex apps, with a lot of different parts working together. Like any other complex app, this can lead to some interesting bugs or cause your bot to behave differently than expected. Before publishing, test your bot. We provide several ways to test bots before they are released for use:

- Test your bot locally with the [emulator](bot-service-debug-emulator.md). The Bot Framework Emulator is a stand-alone app that not only provides a chat interface but also debugging and interrogation tools to help understand how and why your bot does what it does.  The emulator can be run locally alongside your in-development bot application.

- Test your bot on the [web](bot-service-manage-test-webchat.md). Once configured through the Azure portal your bot can also be reached through a web chat interface. The web chat interface is a great way to grant access to your bot to testers and other people who do not have direct access to the bot's running code.

- [Unit Test](https://docs.microsoft.com/azure/bot-service/unit-test-bots) your bot with the July update of Bot Framework SDK.

### Publish

When you are ready for your bot to be available on the web, publish your bot to [Azure](bot-builder-howto-deploy-azure.md) or to your own web service or data center. Having an address on the public internet is the first step to your bot coming to life on your site, or inside chat channels.

### Connect

Connect your bot to channels such as Facebook, Messenger, Kik, Slack, Microsoft Teams, Telegram, text/SMS, Twilio, and Cortana. Bot Framework does most of the work necessary to send and receive messages from all of these different platforms - your bot application receives a unified, normalized stream of messages regardless of the number and type of channels it is connected to. For information on adding channels, see [channels](bot-service-manage-channels.md) topic.

### Evaluate

Use the data collected in Azure portal to identify opportunities to improve the capabilities and performance of your bot. You can get service-level and instrumentation data like traffic, latency, and integrations. Analytics also provides conversation-level reporting on user, message, and channel data. For more information, see [how to gather analytics](bot-service-manage-analytics.md).

## Next steps

Check out these [case studies](https://azure.microsoft.com/services/bot-service/) of bots or click on the link below to create a bot.
> [!div class="nextstepaction"]
> [Create a bot](bot-service-quickstart.md)

---
title: What is the Bot Framework SDK?
description: The Bot Framework, along with the Azure Bot Service, provides tools to build, test, deploy, and manage intelligent bots, all in one place. The Bot Framework includes a modular and extensible SDK for building bots, as well as tools, templates, and related AI services. With this framework, developers can create bots that use speech, understand natural language, handle questions and answers, and more.
keywords: overview, introduction, SDK, outline
displayName: About the Bot Framework SDK, About the Azure Bot Service
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: overview
ms.date: 09/12/2021
ms.custom: abs-meta-21q1
---

# What is the Bot Framework SDK?

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

Microsoft Bot Framework and Azure Bot Service are a collection of libraries, tools, and services that let you build, test, deploy, and manage intelligent bots. The Bot Framework includes a modular and extensible SDK for building bots and connecting to AI services. With this framework, developers can create bots that use speech, understand natural language, answer questions, and more.

## What is a bot?

Bots provide an experience that feels less like using a computer and more like dealing with a person&mdash;or intelligent robot. You can use bots to shift simple, repetitive tasks&mdash;such as taking a dinner reservation or gathering profile information&mdash;onto automated systems that may no longer require direct human intervention.
Users converse with a bot using text, interactive cards, and speech.
A bot interaction can be a quick answer to a question or an involved conversation that intelligently provides access to services.

One way to think of a bot is as a web application that has a conversational interface.
Your users connect to your bot through a channel, such as Facebook, Slack, Microsoft Teams, or a custom application.

- Depending on how the bot is configured and how it's registered with the channel, interactions can be in text or speech and can include images and video.
- The bot processes the user's input to interpret what the user has asked for or said.
- The bot evaluates input and performs relevant tasks, such as ask the user for additional information or access services on behalf of the user.
- The bot responds to the user to let them know what the bot is doing or has done.

:::image type="content" source="media/architecture/what-is-a-bot.png" alt-text="A remote bot interacts with a user on a device via text, speech, images, or video.":::

Bots are often implemented as a web application, hosted in Azure and using APIs to send and receive messages.
What's in a bot varies widely depending on what kind of bot it is and what its purpose is.
A simple bot can receive a messages and echo them back to the user, with very little code involved.
A more complex bot can rely on various tools and services to deliver richer experiences on a wide variety of platforms.

Bots can do the same things other types of software can do&mdash;read from and write to files, use databases and APIs, and do the regular computational tasks.
What makes bots unique is their use of mechanisms generally reserved for human-to-human communication.

Azure Bot Service and the Bot Framework include:

- Bot Framework SDKs for developing bots in C#, JavaScript, Python, or Java
- CLI tools for help with end-to-end bot development
- Bot Connector Service, which relays messages and events between bots and channels
- Azure resources for bot management and configuration

Additionally, bots may use other Azure services, such as:

- Azure Cognitive Services to build intelligent applications
- Azure Storage for cloud storage solution

## How to build a bot

Azure Bot Service and Microsoft Bot Framework offer an integrated set of tools and services to help you design and build bots, through all stages of the bot life cycle.
SDKs exist for C#, Java, JavaScript, Typescript, and Python.
Choose your favorite development environment or command line tools to create your bot.

:::image type="content" source="media/bot-service-overview.png" alt-text="Illustration of the steps in the bot life cycle.":::

### Plan

As with any type of software, having a thorough understanding of the goals, processes and user needs is important to the process of creating a successful bot.
You can create a simple bot or include more sophisticated capabilities such as speech, natural language understanding, and question answering.

Before writing code, review the bot [design guidelines](bot-service-design-principles.md) for best practices and to identify the needs for your bot.

### Build

Typically, a bot is a web service hosted in Azure.
In Azure, you can configure your bot to send and receive messages and events from various channels.
You can create bots in any number of environments and languages. You can [create a bot](bot-service-quickstart-create-bot.md) for local development.

With Azure Bot Service and the Bot Framework, you can use other libraries and services to extend your bot's functionality. This table describes some of the features supported by the SDK.

| Feature | Description | More information |
|:-|:-|:-|
| Memory and storage | Persist user and conversation state | [Managing state](v4sdk/bot-builder-concept-state.md) |
| Natural language understanding | Interpret and extract information from user input | [Language understanding](v4sdk/bot-builder-concept-luis.md) |
| Rich cards | Combine text and other media, such as images, audio, video, and buttons | [How to add media and cards](v4sdk/bot-builder-howto-add-media-attachments.md) |

Command line tools to help you to create, manage, and test bot assets.
For more information, see [Azure CLI](/cli/azure/) and [Bot Framework Tools](https://github.com/microsoft/botframework-cli#readme).

For complete code samples, see the [Bot Framework Samples repo](https://github.com/microsoft/botbuilder-samples#readme).
The samples demonstrate many capabilities of the SDK.

### Test

Bots are complex apps with a lot of different parts working together. Like any other complex app, this can lead to some interesting bugs or cause your bot to behave differently than expected. Before publishing, test your bot. We provide several ways to test bots before they are released for use:

- Test your bot locally with the [Bot Framework Emulator](bot-service-debug-emulator.md). The Bot Framework Emulator is a stand-alone app that not only provides a chat interface but also debugging and interrogation tools to help understand how and why your bot does what it does. The Emulator can be run locally alongside your in-development bot application.

- Test your bot on the [web](bot-service-manage-test-webchat.md). Once configured through the Azure portal your bot can also be reached through a web chat interface. The web chat interface is a great way to grant access to your bot to testers and other people who do not have direct access to the bot's running code.

- [Unit Test](/azure/bot-service/unit-test-bots) your bot with the current Bot Framework SDK.

### Publish

When you're ready for your bot to be available on the web, [deploy your bot to Azure](provision-and-publish-a-bot.md) or deploy to your own web service or data center. Having an address on the public internet is the first step to your bot coming to life on your site, or inside chat channels.

### Connect

Connect your bot to channels such as Facebook, Messenger, Slack, Microsoft Teams, Telegram, and SMS via Twilio. Bot Framework does most of the work necessary to send and receive messages from all of these different platforms&mdash;your bot application receives a unified, normalized stream of messages regardless of the number and type of channels it's connected to. For information on adding channels, see [channels](bot-service-manage-channels.md) topic.

### Evaluate

Use the data collected in Azure portal to identify opportunities to improve the capabilities and performance of your bot. You can get service-level and instrumentation data like traffic, latency, and integrations. Analytics also provides conversation-level reporting on user, message, and channel data. For more information, see [how to gather analytics](bot-service-manage-analytics.md).

## Next steps

- [Read customer stories](https://azure.microsoft.com/services/bot-services/#customer-stories)
- [Create a Bot Framework bot](bot-service-quickstart.md)

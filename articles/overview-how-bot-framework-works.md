---
title: How the Bot Framework works | Microsoft Docs
description: Explore the core concepts of building, deploying, and publishing bots with the Bot Framework.
author: JaymeMPerlman
ms.author: v-jaype
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
---

# How the Bot Framework works
The Bot Framework provides tools and services to help you build, deploy, and publish bots, and connecting them to popular communication services.

## Building bots
Bots are apps that offer a conversational interface as the best solution to the user's needs. Like other apps, bot development starts with your business logic and data. Choose the model your bot will use to interact with users. Users can interact with your bot with inline forms and cards, simple menus, or natural language. You can build your bot with the Bot Builder SDK using C# or Node.js, or you can use the Azure Bot Service (currently in preview). Add artificial intelligence to your bot with Cognitive Services. Register your bot on the Developer Portal and connect it to users through the channels they use, such as Facebook, Kik, and Microsoft Teams. When you are ready to share your bot with the world, deploy it to a cloud service such as Microsoft Azure.

![Architecture overview diagram](~/media/how-it-works/architecture-resize.png)

### Bot Builder
To help you build bots with C# or Javascript, the Bot Framework includes Bot Builder SDKs. The SDKs provide libraries, samples, and tools to help you build and debug bots. The SDKs contain built-in dialogs to handle user interactions ranging from a basic Yes/No to complex disambiguation. Built-in recognizers and event handlers help guide the user through a conversation. 

The [Bot Builder SDK for .NET](~/dotnet/bot-builder-dotnet-overview.md) leverages C# to provide a familiar way for .NET developers to write bots. It is a powerful framework for constructing bots that can handle both free-form interactions and more guided conversations where the user selects from possible values.

The [Bot Builder SDK for Node.js](~/nodejs/index.md) provides a familiar way for Node.js developers to write bots. You can use it to build a wide variety of conversational user interfaces, from simple prompts to free-form conversations.
The conversational logic for your bot is hosted as a web service. The Bot Builder SDK for Node.js uses restify, a popular framework for building web services, to create the bot's web server. The SDK is also compatible with Express and the use of other web app frameworks is possible with some adaptation.

### Azure Bot Service
The Azure Bot Service provides an integrated environment purpose-built for bot development. You can write a bot, connect, test, deploy, and manage it from your web browser with no separate editor or source control required. For simple bots, you may not need to write code at all. It is powered by Microsoft Bot Framework and Azure Functions, which means that your bot will run in a serverless environment on Azure that will scale based upon demand.

## Core concepts
Understanding the core concepts of the Bot Framework will help you build bots that provide the features your users need. These concepts are covered in more detail in the [Develop with .NET](~/dotnet/bot-builder-dotnet-concepts.md) and [Develop with Node.js](~/nodejs/bot-builder-nodejs-concepts.md) sections of the documentation.

### Channel
A channel is the connection between the Bot Framework and communication apps such as Skype, Slack, Facebook Messenger, Office 365 mail, and others. Use the Developer Portal to configure each channel you want the bot to be available on. The Skype and web chat channels are automatically pre-configured.

### Bot Connector
The Bot Connector service connects a bot to one or more channels and handles the message exchange between them. This connecting service allows the bot to communicate over many channels without manually designing a specific message for each channel's schema.

### Activity
The Connector uses an `activity` object to exchange information between bot and channel. Any communication going back and forth is an `activity` of some type. Some activities are invisible to the user, such as the notification that the bot has been added to a user's contact list. 

### Message
A `message` is the most common type of `activity`. A message can be as simple as a text string or contain attachments, interactive elements, and rich cards. For example, adding a bot to the user's contact list could trigger the bot to respond with a `message` containing a string saying "Thank you!" and an image of a happy face.

### Dialog
Dialogs help organize the logic in your bot and manage [conversation flow](~/bot-design-conversation-flow.md). Dialogs are arranged in a [stack](~/bot-design-conversation-flow.md#dialog-stack), and the top dialog in the stack processes all incoming messages until it is closed or a different dialog is invoked. For example, a *BrowseProducts* dialog would contain only the logic and UI related to the user browsing the products; clicking the *Order* button would invoke the *PlaceOrder* dialog.

### Rich cards
A rich card comprises a title, description, link, and images. A message can contain multiple rich cards, displayed in either list format or carousel format.
The Bot Framework supports the following rich cards: 

| Card type | Description |
|----|----|
| Animation card | A card that can play animated GIFs or short videos. |
| Audio card | A card that can play an audio file. |
| Hero card | A card that typically contains a single large image, one or more buttons, and text. |
| Thumbnail card | A card that typically contains a single thumbnail image, one or more buttons, and text. |
| Receipt card | A card that enables a bot to provide a receipt to the user. It typically contains the list of items to include on the receipt, tax and total information, and other text. |
| SignIn card | A card that enables a bot to request that a user sign-in. It typically contains text and one or more buttons that the user can click to initiate the sign-in process. |
| Video card | A card that can play videos. |

Learn more about adding rich cards using the [Bot Builder SDK for .NET](~/dotnet/bot-builder-dotnet-add-rich-card-attachments.md) and the [Bot Builder SDK for Node.js](~/nodejs/bot-builder-nodejs-send-rich-cards.md).

## Test and debug
The [Bot Framework Emulator](~/debug-bots-emulator.md) is a desktop application that allows developers to test and debug their bots. The Emulator can communicate with a bot running on `localhost` or remotely through a tunnel. As you chat with your bot, the emulator displays messages as they would appear in the web chat UI and logs JSON requests and responses for later evaluation.

You may also use the debugger included in [Visual Studio Code](~/debug-bots-locally-vscode.md). Debugging some languages may require additional extensions and configuration.

## Deploy to the cloud
You can host your bot on any reachable service, such as Azure. If you build a bot with the Bot Builder SDK for .NET, you can deploy directly from [Visual Studio](~/deploy-bot-visual-studio.md). You can also deploy a bot with continuous integration from a git repository or Github.

## Register and publish
When you finish your bot, [register](~/portal-register-bot.md) on the Developer Portal. The [Developer Portal](https://dev.botframework.com/) provides a dashboard interface to perform many bot management and connectivity tasks such as configuring channels, managing credentials, connecting to Azure App Insights, or generating web embed codes. 

You can use the Developer Portal to provide channel configuration information to the target channel(s). Many channels require a bot to have an account on the channel; some also require an application.

**Registering** your bot with the Bot Framework generates unique credentials used for authentication. After registering, you can use the Developer Portal to perform maintenance and connectivity tasks. **Publishing** your bot makes it discoverable on the channels your bot is configured to support.

## Make your bot smarter
Connect the Microsoft Cognitive Services APIs to enhance your bot. Smart conversational bots respond more naturally, understand spoken commands, guide the user's search for data, determine user location, and even recognize a user's intention to interpret what the user meant to do. [Learn more][smartbots] about adding intelligence to a bot.

## Next steps
- [Design](~/bot-design-principles.md)
- [Quickstart](~/bot-builder-overview-getstarted.md)
- [Botbuilder SDK for .NET](~/dotnet/bot-builder-dotnet-overview.md)
- [Botbuilder SDK for Node.js](~/nodejs/index.md)

[smartbots]: ~/intelligent-bots.md

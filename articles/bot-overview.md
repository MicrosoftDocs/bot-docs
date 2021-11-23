---
title: Choose the right chatbot solution for your use case
description: Learn when to use Bot Framework Composer and SDK, Orchestrator, or Power Virtual Agents
keywords: 
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: overview
ms.service: bot-service
ms.date: 11/01/2021
ms.custom: mode-api, tab-zone-seo
---

# Choose the right chatbot solution for your use case

A chatbot is an application that makes use of written or spoken natural language as its user interface. In other words, conversation is the means through which questions are answered, requests are serviced, and so on. Drawing upon a broad portfolio of cognitive capabilities, chatbots understand natural language and its nuances from sentence constructs to sentiment.

<!-- Here's a sample flow for a customer service chatbot:

- Customers interact with the chatbot using their device of choice via spoken or typed natural language.
- To interact with the customer, the chatbot converts speech-to-text and text-to-speech as needed.  
- A bot service enables integration with capabilities external to the chatbot itself – such as product information that resides in a database, or an application that can be used to process data provided by the customer. The chatbot can be monitored to ensure it is responsive.
- Last but not least, customers can be authenticated. Once authenticated, higher-value experiences that might include use of private or confidential information can be safely transacted.  

:::image type="content" source="./media/bot-overview/commerce-chatbot-customer-service.png" alt-text="Schematic for a customer service chatbot":::

When built like this, a customer-service chatbot could respond to questions and facilitate a product purchase without the need for a human to intervene. However, chatbots can transfer conversations to human coworkers, if necessary.
-->

Chatbots can be developed as independent applications or integrated into business-application platforms.

Below you can read about the available options and when to make use of each.

|Option|Description|
|----|---|
|[Bot Framework Composer](#bot-framework-composer)|Open-source IDE to author, test, provision, and manage chatbots.|
|[Power Virtual Agents](#power-virtual-agents)|Business application platform that incorporates chatbot capability. |
|[Bot Framework SDK](#bot-framework-sdk)|SDK for building bots, as well as tools, templates, and related AI services.|
|[Bot Framework Orchestrator](#bot-framework-orchestrator)|Dispatches the right skill at the right time in support of a chatbot.|

## Bot Framework Composer

Bot Framework Composer is an open-source IDE for developers to author, test, provision, and manage conversational experiences. It provides a powerful visual authoring canvas for your bot logic. It let's you manage and edit:

- [Dialogs](/composer/concept-dialog).
- [Language-understanding models](/composer/concept-language-understanding).
- [Question-and-answer knowledge bases](/composer/how-to-add-qna-to-bot).
- [Bot responses](/composer/concept-language-generation).

With Bot Framework Composer, experiences are authored from within a single canvas. Because it is built on the [Microsoft Bot Framework](#bot-framework-sdk), existing capabilities can be easily extended with code to address requirements as needed. Resulting experiences can then be tested within Composer and provisioned on Azure along with any dependent resources.

Composer is available as [a desktop application](/composer/install-composer) for Windows, macOS, and Linux. If the desktop app is not suited to your needs, you can [build Composer from source](/composer/build-composer-from-source) or [host Composer in the cloud](/composer/how-to-host-composer).

As an authoring canvas, Bot Framework Composer is broadly analogous to [Power Virtual Agents](#power-virtual-agents). An important distinction, however, is that Bot Framework Composer is independent of the Microsoft Power Platform. In other words, you can make use of Bot Framework Composer to author, test, provision, and manage _standalone_ conversational experiences.

For more information about Bot Framework Composer, refer to the [introductory page](/composer/introduction). For details regarding pricing, refer to [Azure Bot Services pricing](/pricing/details/bot-services/). Once created and deployed on Azure, the chatbot service consumes resources. Costs associated with the consumption of resources on Azure are in addition to the cost of the chatbot service itself.

## Power Virtual Agents

Power Virtual Agents is the chatbot capability included in the [Microsoft Power Platform](https://powerplatform.microsoft.com/)&mdash;a business-application platform that incorporates data analysis, solution building, and process automation in addition to this chatbot capability. From an easy-to-use GUI, chatbots can be built without the need to write code or to understand any of the details of the underpinning AI technologies. Because these agents can leverage automation and other capabilities within the Power Platform, sophisticated chatbot experiences can be rapidly developed. These agents can also be leveraged by [Microsoft 365](https://www.microsoft.com/microsoft-365) and [Microsoft Dynamics 365](https://dynamics.microsoft.com/) for business-productivity use cases. Should there be a need, Power Virtual Agents can even tap into the SDK provided by the [Microsoft Bot Framework](#bot-framework-sdk) to handle more complex scenarios.

For more information about Power Virtual Agents, see the [product overview page](https://powervirtualagents.microsoft.com). For details regarding pricing, refer to [Power Virtual Agents pricing](https://powervirtualagents.microsoft.com/pricing/).

## Bot Framework SDK

The Bot Framework, along with the Azure Bot Service, provides tools to build, test, deploy, and manage intelligent bots. The Bot Framework includes a modular and extensible SDK for building bots, as well as tools, templates, and related AI services. With this framework, developers can create bots that use speech, understand natural language, handle questions and answers, and more.

The Azure Bot Service and the Bot Framework offer:

- The Bot Framework SDK for developing bots. This SDK supports C#, Java, JavaScript, Typescript, and Python.
- Bot Framework Tools to cover end-to-end bot development workflow.
- Bot Framework Connector service to send and receive messages and events between bots and channels.
- Bot deployment and channel configuration in Azure.

Additionally, bots may make use of other Azure services, such as:

- Azure Cognitive Services to build intelligent applications.
- Azure Storage for a cloud storage solution.

Both [Power Virtual Agents](#power-virtual-agents) and [Bot Framework Composer](#bot-framework-composer) make use of the Bot Framework SDK to extend their existing capabilities and deliver more customized conversational experiences.

For more information about the Bot Framework SDK, refer to the [introductory page](bot-service-overview.md).

## Bot Framework Orchestrator

Bot Framework SDK allows to build customized discrete and reusable components of conversational logic called **skills**. Skills may be implemented as a user-facing bots or act to support another bot. As modular components, skills are advantageous for the following reasons:

- Skills help to manage complexity. Complexity is inevitable as bot-based services gain traction and use cases broaden and deepen.
- Skills promote reuse. Existing bots can be enhanced with skills; new bots can be rapidly developed.
- Skills help to ensure maintainability. Sophisticated conversational experiences may the involvement of multiple developers or even multiple teams.

Even though it may be composed from a number of skills, a bot must deliver a seamless experience to the end user. This is the reason for having the **Bot Framework Orchestrator**. It seamlessly dispatches the right skill at the right time in support of a conversational experience. Triggered by utterances, the Bot Framework Orchestrator dispatches the appropriate skill by recognizing the intent of the conversation and responding accordingly.

Establishing the connection between an utterance and an intent requires a degree of natural language understanding and the Bot Framework Orchestrator benefits from state-of-the-art natural language understanding methods.

For more information about the Bot Framework Orchestrator, refer to the [introductory page](/composer/concept-orchestrator).

## Next Steps

- Build a standalone chatbot with [Bot Framework Composer](/composer/introduction)
- Build a chatbot with [Power Virtual Agents](https://powervirtualagents.microsoft.com) for the Power Platform
- Create a bot with the [Bot Framework SDK](bot-service-quickstart-create-bot.md)

---
title: Transition conversations from bot to human | Microsoft Docs
description: Learn how to design for situations where a user starts a conversation with a bot and then must be handed off to a human. 
author: matvelloso
ms.author: mateusv
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 5/2/2019
 
---

# Transition conversations from bot to human

Regardless of how much artificial intelligence a bot possesses, there may still be times when it needs to hand off the conversation to a human being. The bot should recognize when it needs to hand off and provide the user with a clear, smooth transition.

## Scenarios that require human involvement

A wide variety of scenarios may require that a bot transition control of the conversation to a human. A few of those scenarios are *triage*, *escalation*, and *supervision*. 

### Triage

A typical help desk call starts with some very basic questions that can easily be answered by a bot. As the first responder to inbound requests from users, a bot could collect the user's name, address, description of the problem, or any other pertinent information and then transition control of the conversation to an agent. Using a bot to triage incoming requests allows agents to devote their time to solving the problem instead of collecting information.

### Escalation

In the help desk scenario, a bot may be able to answer basic questions and resolve simple issues in addition to collecting information, such as resetting a user's password. However, if a conversation indicates that a user's issue is complex enough to require human involvement, the bot will need to escalate the issue to a human agent. To implement this type of scenario, a bot must be capable of differentiating between issues it can resolve independently and issues that must be escalated to a human. There are many ways that a bot may determine that it needs to transfer control of the conversation to a human. 

#### User-driven menus

Perhaps the simplest way for a bot to handle this dilemma is to present the user with a menu of options. Tasks that the bot can handle independently appear in the menu above a link labeled "Chat with an agent." This type of implementation requires no advanced machine learning or natural language understanding. The bot simply transfers control of the conversation to a human agent when the user selects the "Chat with an agent" option. 

#### Scenario-driven

The bot may decide whether or not to transfer control based upon whether or not it determines that it is capable of handling the scenario at hand. The bot collects some information about the user's request and then queries its internal list of capabilities to determine if it is capable of addressing that request. If the bot determines that it is capable of addressing the request, it does so, but if the bot determines that the request is beyond the scope of issues it can resolve it transfers control of the conversation to a human agent.

#### Natural language

Natural language understanding and sentiment analysis help the bot decide when to transfer control of the conversation to a human agent. This is particularly valuable when attempting to determine when the user is frustrated or wants to speak with a human agent. 
 
The bot analyzes the content of the user's messages 
by using the <a href="https://www.microsoft.com/cognitive-services/en-us/text-analytics-api" target="blank">Text Analytics API</a> 
to infer sentiment 
or by using the <a href="https://www.luis.ai" target="_blank">LUIS API</a>. 


> [!TIP]
> Natural language understanding may not always be the best method for determining when a bot 
> should transfer conversation control to a human being. Bots, like humans, don't always guess 
> correctly, and invalid responses will frustrate the user. If the user selects from a menu of 
> valid choices, however, the bot will always respond appropriately to that input. 

### Supervision

In some cases, the human agent will want to monitor the conversation instead of taking control.

For example, consider a help desk scenario where a bot is communicating with a user to diagnose computer problems. A machine learning model helps the bot determine the most likely cause of the issue, however before advising the user to take a specific course of action, the bot can privately confirm the diagnosis and remedy with the human agent and request authorization to proceed. The agent then clicks a button, the bot presents the solution to the user, and the problem is solved. The bot is still performing the majority of the work, but the agent retains control the final decision. 

## Transitioning control of the conversation 

When a bot decides to transfer control of a conversation to a human, it can inform the user that she is being transferred and put the conversation into a 'waiting' state until it confirms that an agent is available. 

When the bot is waiting for a human, it may automatically answer all incoming user messages with a default response such as "waiting in queue". Furthermore, you could have the bot remove the conversation from the 'waiting' state if the user sent certain messages such as "never mind" or "cancel".

You specify how agents will be assigned to waiting users when you design your bot. For example, the bot may implement a simple queue system: first in, first out. More complex logic would assign users to agents based upon geography, language, or some other factor. The bot could also present some type of UI to the agent that they can use to select a user. When an agent becomes available, she connects to the bot and joins the conversation.

> [!IMPORTANT]
> Even after an agent is engaged, the bot remains the behind-the-scenes facilitator of the conversation. 
> The user and agent never communicate directly with each other; they just route messages through the bot. 

## Routing messages between user and agent

After the agent connects to the bot, the bot begins to route messages between user and agent. Although it may appear to the user and the agent that they are chatting directly with each other, they are actually exchanging messages via the bot. The bot receives messages from the user and sends those messages to the agent, and in turn receives messages from the agent and sends those messages to the user. 

> [!NOTE]
> In more advanced scenarios, the bot can assume responsibility beyond merely routing messages 
> between user and agent. For example, the bot may decide which response is appropriate 
> and simply ask the agent for confirmation to proceed.

## Additional resources

::: moniker range="azure-bot-service-4.0"

- [Dialogs](v4sdk/bot-builder-dialog-manage-conversation-flow.md)
- <a href="https://www.microsoft.com/cognitive-services/en-us/text-analytics-api" target="blank">Text Analytics API</a>

::: moniker-end

::: moniker range="azure-bot-service-3.0"

- [Manage conversation flow with dialogs (.NET)](~/dotnet/bot-builder-dotnet-manage-conversation-flow.md)
- [Manage conversation flow with dialogs (Node.js)](~/nodejs/bot-builder-nodejs-manage-conversation-flow.md)
- <a href="https://www.microsoft.com/cognitive-services/en-us/text-analytics-api" target="blank">Text Analytics API</a>


::: moniker-end


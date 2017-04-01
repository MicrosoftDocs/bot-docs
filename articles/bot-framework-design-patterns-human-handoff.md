---
title: Handoff from bot to human | Microsoft Docs
description: Learn how to design a conversational application (bot) that requires handoff from bot to human.
keywords: bot framework, design, bot, scenario, use case, pattern, bot to human, handoff, transition to human
author: matvelloso
manager: rstand
ms.topic: design-patterns-human-handoff-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 
ms.reviewer: rstand
#ROBOTS: Index
---
# Handoff from bot to human

Regardless of how much artificial intelligence a bot possesses, there may still be times when it needs to 
handoff control of the conversation to a human. 
This article covers the types of scenarios that typically require human involvement 
and explores the process of transitioning control of a conversation from bot to human. 

## Scenarios that require human involvement

A wide variety of scenarios may require that a bot transition control of the conversation to a human. 
A few of those scenarios are *triage*, *escalation*, and *supervision*. 

### Triage

A typical help desk call starts with some very basic questions that can easily be asked by a bot. 
For example, as the first responder to inbound requests from users, a bot can 
collect the user's name, address, and description of the problem to be solved. 
Then it can transition control of the conversation to an agent and provide the information that's already been collected from the user. 
Using a bot to triage incoming requests allows agents to devote their time to higher cognitive tasks.

### Escalation

In the help desk scenario, a bot may be able to answer basic questions and resolving simple issues in addition to collecting information about the user and the nature 
of their issue. 
For example, a bot could reset a user's password. 
However, if a conversation indicates that a user's issue is complex enough to require human involvement, 
the bot will need to escalate the issue to a human agent. 
To implement this type of scenario, a bot must be capable of differentiating between issues it can resolve independently 
and issues that must be escalated to a human. 
There are many ways that a bot may determine that it needs to transfer control of the conversation to a human, 
including: 

#### User-driven menus
Perhaps the simplest way for a bot to handle this dilemma is to present the user with a menu of options, 
including an item for each of the tasks that a bot is capable of handling independently, and 
a final item labeled "Chat with an agent." This type of implementation requires no advanced machine learning or 
natural language understanding. The bot simply transfers control of the conversation to a human agent when 
the user selects the "Chat with an agent" option. 

#### Scenario-driven
The bot may decide whether or not to transfer control of the conversation to a human agent based upon whether 
or not it determines that it is capable of handling the scenario at hand. 
The bot collects some information about the user's request and then 
queries its internal list of capabilities to determine if it is capable of addressing that request. 
If the bot determines that it is capable of addressing the request, it does so independently. 
However, if the bot determines that the request is beyond the scope of issues it can resolve, 
it transfers control of the conversation to a human agent.

#### Natural language
The bot may may decide when to transfer control of the conversation to a human agent by using 
natural language understanding and sentiment analysis. 
The bot analyzes the content of the user's messages 
by using the <a href="https://www.microsoft.com/cognitive-services/en-us/text-analytics-api" target="blank">Text Analytics API</a> 
to infer sentiment 
or by using the <a href="https://www.luis.ai" target="_blank">LUIS API</a> 
in an attempt to identify when the user is frustrated or wants to speak with a human agent. 

> [!TIP]
> When deciding which of these methods your bot will use to 
> identify when a user's issue should be escalated to a human, 
> natural language understanding may not always be the best approach.
> Natural langauge understanding provides less certainty that the bot is handling the situation in the way that the user truly intends compred to presenting a user with a menu of valid choices. For example, a menu-driven experience ensures that if a user clicks the "Chat with an agent" button, the bot can respond correctly. 

### Supervision

Sometimes a human agent may not actually need to take control of the conversation. Instead, the agent will simply monitor a conversation between the bot and user and will only act when necessary. 
For example, consider a help desk scenario where a bot is communicating with a user to diagnose computer problems 
and uses a machine learning model to determine the most likely cause of the issue. 
Before advising the user to take a specific course of action, the bot can privately 
inform the agent of the situation, including the issue, diagnosis, and proposed remedy, and request 
authorization to proceed. The agent can approve the proposed course of action by simply clicking a button. 
This type of implementation allows the bot to do a majority of the work, while 
still enabling the agent to supervise and control the final decision. 

## Transitioning control of the conversation 

When a bot decides to transfer control of a conversation to a human, 
it can inform the user that she is being transfered to an agent 
and put the user in a 'waiting' state until it can confirm that an agent is available. 

You may choose to design your bot to automatically answer all incoming user messages with a default response such as "waiting in queue" 
as long as the user is in a 'waiting' state. 
Alternatively, you might choose to design your bot such that it will remove the user from the 'waiting' state 
in certain situations. For example, perhaps the user could send the message "never mind" or "cancel" to be removed from the 
queue and returned to the conversation with the bot. 

When designing your bot, you'll specify the logic that determines how agents will be assigned to waiting users. 
For example, the bot may implement a simple queue system (first in, first out), 
it might assign agents based upon geography, language, or some other factor, 
or it may present some type of UI to the agent that they can use to select a user to assist. 

When an agent becomes available, she connects to the bot. 

> [!IMPORTANT]
> Even after an agent is engaged, the bot remains the behind-the-scenes facilitator of the conversation. 
> The user and agent never communicate directly with each other, instead communicating by routing messages through the bot. 

## Routing messages between user and agent

After the agent connects to the bot, the bot begins to route messages between user and agent. 
Although it may appear to the user and the agent that they are chatting directly with each other, 
they are exchanging messages via the bot. 
The bot receives messages from the user and sends those messages to the agent. 
Likewise, it receives messages from the agent and sends those messages to the user. 

> [!NOTE]
> In more advanced scenarios, the bot can assume responsibility beyond that of merely routing messages 
> back and forth between user and agent. 
> For example, the bot may decide what to say next in a conversation and and simply ask the agent 
> for confirmation to proceed.

## Additional resources

- [Dialogs](~/dotnet/dialogs.md)
- [Manage conversation flow using .NET](~/dotnet/manage-conversation-flow.md)
- [Manage conversation flow using Node.js](bot-framework-nodejs-howto-manage-conversation-flow.md)
- [Add smarts to your bot with Microsoft Cognitive Services](intelligent-bots.md)
- [Add language understanding to your bot](bot-framework-intelligence-language.md)

<!-- 
This article discussed the types of scenarios that typically require human involvement, 
and explored the process of transitioning control of a conversation from bot to human.

To see sample code for bots that implement this handoff, review the following resources: 

> [!NOTE]
> To do: Add links to the C# and Node.js code samples that Mat refers to.-->
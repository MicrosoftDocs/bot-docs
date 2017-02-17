---
title: Designing Bots - Hand off to Human | Microsoft Docs
description: Handling scenarios where bots hand off the conversation to humans
services: Bot Framework
documentationcenter: BotFramework-Docs
author: matvelloso
manager: larar

ms.service: required
ms.devlang: may be required
ms.topic: article
ms.tgt_pltfrm: may be required
ms.workload: required
ms.date: 01/19/2017
ms.author: mat.velloso@microsoft.com

---
# Bot Design Center - Hand Off To Human



##Adding the human component to the loop


No matter how smart your bot is, there will be cases where you still need to let a human take control of the conversation. This is particularly common in help desk scenarios: The bot can be the first line response to users and try its best to answer some more basic questions, but depending on how the flow evolves, the bot might still need some help from a human agent.

Note that how the bot manages this process can vary largely depending on the business scenario:

- Triage scenarios: In some cases, every help desk call starts with some very basic questions. We may need the user to let us know their name, address, describe the problems they need solved and so on. Those first questions can be time consuming and they could be totally handled by bots. So the flow starts with the bot asking those basic questions and then automatically handing off the conversation to an agent with the answers already collected, sparing time for agents to focus on higher cognitive tasks. This also benefits the customer who will less likely have to wait too long until an agent can help them.
- Escalation scenarios: In such cases, the bot might go beyond just asking some basic questions but also try to solve some common requests. From every call from a customer, maybe a percentage of them are highly repeated scenarios that are quite simple to automate, so we don't need to waste valuable time from agents and instead let them dedicate their time on the more complex problems. Here the key challenge is knowing when the bot can't solve a problem anymore so it can escalate the issue to an agent. There are many ways this can be done: 
	- Menus: Again, let us not underestimate the power of menus. A simple way of solving this problem is simply offering the user a menu of options of things the bot can do and the last option would be "chat with an agent". No advanced machine learning or natural language required
	- Scenario based: The customer may start by asking what they want and the bot will check that request against an internal list of capabilities. If this is a problem it knows how to solve it will go ahead and run its pre-defined routine for addressing the user's problem. But if the question doesn't seem to fit into any of the bot's predefined capabilities, it can then automatically escalate to the agent.
	- Natural language based: This is probably the most difficult of the scenarios: In this case the bot will monitor the content of what the user says at any time during the conversation and either try to infer sentiment by using the [Text Analytics API](https://www.microsoft.com/cognitive-services/en-us/text-analytics-api) or by using natural language via the [LUIS API](https://www.luis.ai) in order to "guess" the user is frustrated and/or really desires to chat with another person. Note that developers tend to be attracted to this third option as it is a more fascinating, "human" way of dealing with this problem but it is important to keep in mind that it is also less certain: If we give the user a button called "chat with an agent" and they click at it, there's no question: The user wants, for sure, chat with the agent. When we try to detect sentiment in a sentence, it becomes a matter of probability and not certainty. We now need to constantly monitor these conversations to make sure that the bot is indeed detecting when the user wanted to speak with an agent. Also, betting that the user needs to type a negative sentence in order for us to escalate means we are going to wait for the user to become frustrated first and then act later. Probably not the best plan...
- Supervised scenarios: In some cases we may want the agent to "monitor" a conversation between the bot and the user and only act when necessary. Imagine the bot is trying to diagnose computer problems by asking a few questions from the user, and then it runs a machine learning model that will guess that the root cause is a driver issue. Before the bot goes ahead and tells the user what to do, the bot can secretly tell the agent that this is what is the likely cause and prompt for the agent to authorize it to proceed. The agent now only has to click a button. We are sparing the agent from all the typing and all the related questions but still in control of the final decision and procedure. This is a great way of leaving agents focused on what they are unique for - the high cognitive tasks - while the bot does all the manual work.



##Handing off 

One the decision process above has been established and whatever that process ends up being, at some point the bot will start the hand off. At that point, the user will be put in a "waiting" state. The reason is that we don't know yet whether an agent is available. The user will be informed they are waiting for an agent. New messages will be ignored and answer with that default message of "waiting in queue".

Of course, the bot may, if needed, want to remove the user from waiting state. Perhaps the user could say "never mind" and that would trigger the bot to get back to the original conversation. This is entirely up to the bot logic to decide.

An agent then connects to the bot (and never directly to the customer). This is an important principle: The bot will always relay the message from the user to the agent and back. In other words, the agent is never actually connected with the user directly. The reason is that we want the bot to always be in control. In the moment the user started chatting directly with the agent, the bot would be removed form the equation which adds far more complexity to the scenario. 

How the agent is assigned to a waiting (or non waiting) user is entirely up to the bot's logic: It may be a queue system, it may be a UI that is given to the agent to make the decision, could be based on geography, language or any other aspects decided by the bot developer. The point is that whatever logic applied will, at some point, decide to make the connection happen and then a message routing is established. 

##Message router

This is the hear of the hand off framework used in this scenario: The message router will receive messages from the user, then send them to the agent. It will also receive messages from the agent and send them to the user. Both user and agent are in fact chatting with the bot and not with each other. The bot will simply be working as a router of what it received from each side, to the other.

In more advanced scenarios, the bot can take more roles than just being a router: It may make decisions on what to say next and just ask for the agent's confirmation to proceed.

##Show me how!

For a sample of the hand off framework in the simple escalation pattern mentioned above, take a look at the [readme page here](https://trpp24botsamples.visualstudio.com/_git/Code?path=%2FNode%2Fbot-handoff%2FREADME.md&version=GBmaster&_a=contents) and the source code in [Node](https://trpp24botsamples.visualstudio.com/_git/Code?path=%2FNode%2Fbot-handoff&version=GBmaster&_a=contents) and C#





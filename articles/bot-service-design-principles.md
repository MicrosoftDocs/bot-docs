---
title: Principles of bot design | Microsoft Docs
description: Learn what makes a good conversational bot and how to plan and design bots to fit your needs and delight your users.
keywords: best practices, bot design 
author: matvelloso
ms.author: mateusv
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
 
---

# Principles of bot design

The Bot Framework enables developers to create compelling bot experiences that solve a variety of business problems. By learning the concepts described in this section, you'll become equipped to design a bot that aligns with best practices and capitalizes on lessons learned thus far in this relatively new arena. 

## Designing a bot

If you are building a bot, it's safe to assume that you are expecting users to use it. 
It is also safe to assume that you are hoping that users will prefer the bot experience over alternative experiences like apps, websites, phone calls, and other means of addressing their particular needs. 
In other words, your bot is competing for users' time against things like apps and websites. 
So, how can you maximize the odds that your bot will achieve its ultimate goal of attracting and keeping users? 
It's simply a matter of prioritizing the right factors when designing your bot.

## Factors that do not guarantee a bot's success

When designing your bot, be aware that none of the following factors necessarily guarantee a bot's success: 

- **How “smart” the bot is**: 
In most cases, it is unlikely that making your bot smarter will guarantee happy users or adoption of your platform. In reality, many bots have little advanced machine learning or natural language capabilities. A bot may include those capabilities if they're necessary to solve the problems that it's designed to address, however you should not assume any correlation between a bot's intelligence and user adoption of the bot.

- **How much natural language the bot supports**: 
Your bot can be great at conversations. 
It can have a vast vocabulary and can even make great jokes. 
But unless it addresses the problems that your users need to solve, these capabilities may contribute very little to making your bot successful. 
In fact, some bots have no conversational capability at all. And in many cases, that's perfectly fine.

- **Voice**: 
It isn’t always the case that enabling bots for speech will lead to great user experiences. 
Often, forcing users to use voice can result in a frustrating user experience. 
As you design your bot, always consider whether voice is the appropriate channel for the given problem. 
Is there going to be a noisy environment? 
Will voice convey the information that needs to be shared with the user? 

## Factors that do influence a bot's success

Most successful apps or websites have at least one thing in common: a great user experience. 
Bots are no different in that regard. 
Therefore, ensuring a great user experience should be your number one priority when designing a bot. 
Some key considerations include:

- Does the bot easily solve the user’s problem with the minimum number of steps?

- Does the bot solve the user’s problem better/easier/faster than any of the alternative experiences?

- Does the bot run on the devices and platforms the user cares about?

- Is the bot discoverable? Do the users naturally know what to do when using it?

None of these questions directly relates to factors such as how smart the bot is, how much natural language capability it has, whether it uses machine learning, or which programming language was used to create it. Users are unlikely to care about any of these things if the bot solves the problem that they need to address and delivers a great user experience. A great bot user experience does not require users to type too much, talk too much, repeat themselves several times, or explain things that the bot should automatically know.

> [!TIP]
> Regardless of the type of application you're creating (bot, website, or app), make user experience a top priority.

The process of designing a bot is like the process of designing an app or website, so
the lessons learned from decades of building UI and delivering UX for apps and websites still apply when it comes to designing bots. 

Whenever you are unsure about the right design approach for your bot, step back and ask yourself the following question: how would you solve that problem in an app or a website? Chances are, the same answer can be applied to bot design. 

## Next steps

Now that you're familiar with some basic principles of bot design, learn more about designing the user experience, and common patterns in the remainder of this section.
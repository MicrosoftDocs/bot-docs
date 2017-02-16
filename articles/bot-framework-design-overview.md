---
title: Bot Framework Design - Introduction | Microsoft Docs
description: Learn about important factors to consider when planning and designing conversational applications (bots).
keywords: bot framework, design, bot
author: matvelloso
manager: larar
ms.topic: Introduction to Bot Design 

ms.prod: botframework
# the ms.service should be the section of the IA that the article is in, with the suffix -article. Some examples:
# get-started article, sdk-reference-article
ms.service: design-article

# Date the article was updated
ms.date: 01/19/2017

# Alias of the document reviewer. Change to the appropriate person.
ms.reviewer: rstand

# Include the following line commented out
#ROBOTS: Index
---
# Introduction to bot design

## Overview

The Bot Framework enables developers to create compelling bot experiences that solve a variety of business problems. 
Designing a bot's UI/UX and planning its features and functionality requires a perspective 
that's significantly different from that of web development or app development. 
By familiarizing yourself with the content in this section, you'll be equipped to design a bot that 
aligns with best practices and capitalizes on lessons learned thus far in this relatively new arena. 

##<a id="design-guidance"></a> Designing a bot

If you are building a bot, it is safe to assume that you are expecting users to use it. 
It is also safe to assume that you are hoping that users will prefer the bot experience over alternative experiences like apps, websites, phone calls, and other means of addressing their particular needs. 
In other words, your bot is competing for users' time against things like apps and websites. 
So, how can you maximize the odds that your bot will achieve it's ultimate goal of attracting and keeping users? 
It's simply a matter of prioritizing the right factors when designing your bot.

## Factors that do not guarantee a bot's success

When designing your bot, be aware that none of the following factors necessarily guarantee a bot's success: 

- **How “smart” the bot is**: 
In most cases, it is unlikely that making your bot smarter will guarantee happy users and adoption of your platform. 
In reality, many bots have little advanced machine learning or natural language capabilities. 
Of course, a bot may include those capabilities if they're necessary to solve the problems that it's designed to address. 
However, you should not assume any correlation between a bot's intelligence and user adoption of the bot.

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
Will voice be capable of conveying the information that needs to be shared with the user? 

## Factors that do influence a bot's success

Most successful apps or websites have at least one thing in common: a great user experience. 
Bots are no different in that regard. 
Therefore, ensuring a great user experience should be your number one priority when designing a bot. 
Some key considerations include:

- Does the bot easily solve the user’s problem with the minimum number of steps?

- Does the bot solve the user’s problem better/easier/faster than any of the alternative experiences?

- Does the bot run on the devices and platforms the user cares about?

- Is the bot discoverable? Do the users naturally know what to do when using it?

Note that none of these questions directly relates to factors such as 
how smart the bot is, how much natural language capability it has, whether it uses machine learning, 
or which programming language was used to create it. Users are unlikely to care about any of these things as 
long as the bot solves the problem that they need to address and 
delivers a great user experience (i.e., does not require them to type too much, talk too much, 
repeat themselves several times, or explain things that the bot should automatically know).

> [!TIP]
> Regardless of the type of application you're creating (bot, website, or app), prioritize user experience above all else.

The process of designing a bot is very similar to the process of designing an app or website, and therefore 
the lessons learned from decades of building UI and delivering UX for apps and websites still apply 
when it comes to designing bots. Whenever you are unsure about the right design approach for your bot, 
step back and ask yourself the following question: How would you solve that problem in an app or a website? 
Chances are, the same answer can be applied to bot design. 

## Next steps

Now that you're familiar with some basic principles of bot design, learn more about the following topics in the 
remainder of this section:

- [UI and UX](bot-framework-design-core-greeting.md)
- [Bot capabilities](bot-framework-design-capabilities.md)
- [Common patterns](bot-framework-design-patterns-overview.md)
- [Resources](bot-framework-design-resources.md)

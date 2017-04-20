---
title: Design a bot's first user interaction | Microsoft Docs
description: Learn what makes a great first user experience and how to design your bots for immediate success.  
author: matvelloso
ms.author: mateusv
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date:
ms.reviewer: rstand
ROBOTS: Index, Follow
---
# Design a bot's first user interaction

## First impressions matter

The very first interaction between the user and bot is critical to the user experience. 
When designing your bot, keep in mind that there is more to that first message than just saying "hi." 
When you build an app, you design the first screen to provide important navigation cues. 
Users should intuitively understand things such as 
where the menu is located and how it works, where to go for help, what the privacy policy is, and so on.
When you design a bot, the user's first interaction with the bot should provide that same type of information. 
In other words, just saying "hi" won’t be enough.

## Language versus menus 

Consider the following two designs:

###Design 1

![bot](~/media/designing-bots/core/hello1.png)


###Design 2

![bot](~/media/designing-bots/core/hello2.png)

Starting the bot with an open-ended question such as "How can I help you?" is generally not recommended. 
If your bot has a hundred different things it can do, chances are users won’t be able to guess most of them. 
Your bot didn’t tell them what it can do, so how can they possibly know?

Menus provide a simple solution to that problem. 
First, by listing the available options, your bot is conveying its capabilities to the user. 
Second, menus spare the user from having to type too much. They can simply click.
Finally, the use of menus can significantly simplify your natural language models by narrowing the scope of input that the bot could receive from the user. 

> [!TIP]
> Menus are a valuable tool when designing bots for a great user experience. 
> Don’t dismiss them as not being “smart enough”. 
> You may design your bot to use menus while still supporting free form input. 
> If a user responds to the initial menu by typing rather than by selecting a menu option, your bot could attempt to parse the user's text input. 

## Other considerations

In addition to providing an intuitive and easily navigated first interaction, 
a well-designed bot provides the user with access to information about its privacy policy and terms of use. 

> [!TIP]
> If your bot collects personal data from the user, it's important to convey that and to describe what will be done with the data.

## Next steps

Now that you're familiar with some basic principles for designing the first interaction between user and bot, 
learn more about [designing the flow of conversation](~/bot-design-conversation-flow.md).
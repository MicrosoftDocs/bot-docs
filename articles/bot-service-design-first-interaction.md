---
title: Design a bot's first user interaction | Microsoft Docs
description: Learn what makes a great first user experience and how to design your bots for success.  
keywords: first impression, beginning, language vs menu 
author: matvelloso
ms.author: mateusv
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
---

# Design a bot's first user interaction

## First impressions matter

The very first interaction between the user and bot is critical to the user experience. When designing your bot, keep in mind that there is more to that first message than just saying "hi." When you build an app, you design the first screen to provide important [navigation](bot-service-design-navigation.md) cues. Users should intuitively understand things such as where the menu is located and how it works, where to go for help, what the privacy policy is, and so on. When you design a bot, the user's first interaction with the bot should provide that same type of information. 

## Language versus menus 

Consider the following two designs:

### Design 1

![bot](~/media/bot-service-design-first-interaction/hello1.png)


### Design 2

![bot](~/media/bot-service-design-first-interaction/hello2.png)

Starting the bot with an open-ended question such as "How can I help you?" is generally not recommended. If your bot has a hundred different things it can do, chances are users won’t be able to guess most of them. Your bot didn’t tell them what it can do, so how can they possibly know?

Menus provide a simple solution to that problem. First, by listing the available options, your bot is conveying its capabilities to the user. Second, menus spare the user from having to type too much, instead they can just click. Finally, the use of menus can significantly simplify your natural language models by narrowing the scope of input that the bot could receive from the user. 

> [!TIP]
> Menus are a valuable tool when designing bots for a great user experience; don’t dismiss them as not being "smart enough." 
> You can design your bot to use menus while still supporting free form input. 
> If a user responds to the initial menu by typing rather than selecting a menu option, your bot could attempt to parse the user's text input. 

Alternatively, you can ask more pointed questions to lead the user if the bot has a specific function. For example, if your bot is responsible for taking sandwich orders, your first interaction could be "Hi! I'm here to take your sandwich order. What kind of bread would you like? We have white, wheat, or rye." That way, the user knows how to respond and is given navigational cues through the conversation.

## Other considerations

In addition to providing an intuitive and easily navigated first interaction, 
a well-designed bot provides the user with access to information about its privacy policy and terms of use. 

> [!TIP]
> If your bot collects personal data from the user, it's important to convey that and to describe what will be done with the data.

## Next steps

Now that you're familiar with some basic principles for designing the first interaction between user and bot, 
learn more about [designing the flow of conversation](~/bot-service-design-conversation-flow.md).

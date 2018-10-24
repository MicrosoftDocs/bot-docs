---
title: Design bot navigation | Microsoft Docs
description: Learn how to design a good navigation structure for your bot and how to avoid the most common navigation design errors.
keywords: navigation, overview, stubborn bot, clueless bot, mysterious bot, captain obvious bot, bot that can't forget
author: matvelloso
ms.author: mateusv
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/2017
 
---

# Design bot navigation

Users can navigate websites using breadcrumbs, apps using menus, and web browsers using buttons like **forward** and **back**. However, none of these well-established navigation techniques entirely address navigation requirements within a bot. As discussed [previously](~/bot-service-design-conversation-flow.md#handle-interruptions), users often interact with bots in a non-linear fashion, making it challenging to design bot navigation that consistently delivers a great user experience. 

Consider the following dilemmas:

- How do you ensure that a user doesn't get lost in a conversation with a bot? 
- Can a user navigate "back" in a conversation with a bot? 
- How does a user navigate to the "main menu" during a conversation with a bot? 
- How does a user "cancel" an operation during a conversation with a bot? 

The specifics of your bot's navigation design will depend largely upon the features and functionality that your bot supports. Regardless of the type of bot you're developing, you'll want to avoid the common pitfalls of poorly designed conversational interfaces. This article describes these pitfalls in terms of five personalities: the "stubborn bot", the "clueless bot", the "mysterious bot", the "captain obvious bot", and the "bot that can't forget." 

> [!TIP]
> Mitgating each type of these personalities for your bot can often be done by correctly [handling user interruptions](v4sdk/bot-builder-howto-handle-user-interrupt.md).

## The "stubborn bot"

The stubborn bot insists upon maintaining the current course of conversation, 
even when the user attempts to steer things in a different direction. 

Consider the following scenario: 

![bot](~/media/bot-service-design-navigation/stubborn-bot-new.png)

Users often change their minds, decide to cancel or sometimes they want to start over altogether. 

> [!TIP]
> <b>Do</b>: Design your bot to consider that a user might attempt to change the course of the conversation at any time. 
>
> <b>Don't</b>: Design your bot to ignore user input and keep repeating the same question in an endless loop. 

There are many methods of avoiding this pitfall, but perhaps the easiest way to prevent a bot from asking the same question endlessly is to simply specify a maximum number of retry attempts for each question. If designed in this manner, the bot is not doing anything "smart" to understand the user's input and respond appropriately but will at least avoid asking the same question in an endless loop. 

## The "clueless bot"

The clueless bot responds in a nonsensical manner when it doesn't understand a user's attempt to access certain functionality. A user may try common keyword commands like "help" or "cancel" with reasonable expectations that the bot will respond appropriately.

Consider the following scenario: 

![bot](~/media/bot-service-design-navigation/clueless-bot.png)

Although you may be tempted to design every dialog within your bot to listen for, and respond appropriately to, certain keywords, this approach is not recommended. 

> [!TIP]
> <b>Do</b>: Implement [middleware](v4sdk/bot-builder-create-middleware.md) that will examine user input for the keywords that you specify (ex: "help", "cancel", "start over", etc.) and respond appropriately. 
> 
> <b>Don't</b>: Design every dialog to examine user input for a list of keywords. 

By defining the logic in your **middleware**, you're making it accessible to every exchange with the user. Using this approach, individual dialogs and prompts can be made to safely ignore the keywords, if necessary.

## The "mysterious bot"

The mysterious bot fails to immediately acknowledge the user's input in any way. 

Consider the following scenario: 

![bot](~/media/bot-service-design-navigation/mysterious-bot.png)

In some cases, this situation might be an indication that the bot is having an outage. 
However, it could just be that the bot is busy processing the user's input and hasn't yet finished compiling its response. 

> [!TIP]
> <b>Do</b>: Design your bot to immediately acknowledge user input, even in cases where the bot may take some time to compile its response. 
> 
> <b>Don't</b>: Design your bot to postpone acknowledgement of user input until the bot finishes compiling its response.

By immediately acknowledging the user's input, you eliminate any potential for confusion as to the state of the bot. If your response takes a long time to compile, consider sending a "typing" message to indicate your bot is working, and then following up with a [proactive message](v4sdk/bot-builder-howto-proactive-message.md)

## The "captain obvious bot"

The captain obvious bot provides unsolicited information that is completely obvious and therefore useless to the user. 

Consider the following scenario:

![bot](~/media/bot-service-design-navigation/captainobvious-bot.png)

> [!TIP]
> <b>Do</b>: Design your bot to provide information that will be useful to the user. 
> 
> <b>Don't</b>: Design your bot to provide unsolicited information that is unlikely to be useful to the user.

By designing your bot to provide useful information, you're increasing the odds that the user will engage with your bot.

## The "bot that can't forget"

The bot that can't forget inappropriately integrates information from past conversations into the current conversation. 

Consider the following scenario:

![bot](~/media/bot-service-design-navigation/rememberall-bot.png)

> [!TIP]
> <b>Do</b>: Design your bot to maintain the current topic of conversation, unless/until the user expresses a desire to revisit a prior topic. 
> 
> <b>Don't</b>: Design your bot to interject information from past conversations when it is not relevant to the current conversation.

By maintaining the current topic of conversation, you reduce the potential for confusion and frustration as well as increasing the odds that the user will continue to engage with your bot.

## Next steps

By designing your bot to avoid these common pitfalls of poorly designed conversational interfaces, you're taking an important step toward ensuring a great user experience. 

Next, learn more about the [UX elements](~/bot-service-design-user-experience.md) that bots most typically rely upon to exchange information with users. 

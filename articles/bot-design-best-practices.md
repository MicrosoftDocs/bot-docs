---
title: Best practices for designing bots | Microsoft Docs
description: Learn what makes a good conversational bot and how to design one to improve your user experience. 
author: RobStand
ms.author: rstand


manager: rstand
ms.topic: article

ms.prod: bot-framework

ms.date:
ms.reviewer:

# Include the following line commented out
ROBOTS: Index, Follow
#REVIEW
---
> [!WARNING]
> The content in this article is still under development. The article may have errors in content, formatting,
> and copy. The content may change dramatically as the article is developed.

# Principles for bot design

These principles and best practices will help you design more engaging experiences for users of your bots.

## Experience Principles
### Provide as much value to the user as you can in the conversation channel

Reduce bouncing the user out from the messaging canvas to destination pages & apps during task completion

### Participate in a productive conversation in a natural way with minimal complexity

Brief but meaningful responses with a bias towards text-based answers that are glanceable, card, image or button answers that are crisp, clear, actionable. Human-like typing speed.

### Utilize the interaction patterns of the messaging canvas and the mobile OS
Use themes, UI, and an IA familiar to users. Users should know what to expect when they engage with bot responses (e.g., tap on a photo.)

### Have a clear POV on what scenarios the bot will handle brilliantly
Know the user problem, the value proposition of your bot, and how your bot is better than other currently available solutions.

### Be nice and do the right things
Always strive for these key principles:

* Being respectful and considerate of everyone
* Respecting user privacy
* Not being rude in conversations
* Striving for quality
* Operating with integrity

## Best Practices

### Craft an engaging welcome message

Always introduce your bot to members of the chat.  

Let users know how they can interact with your Bot (e.g., list command interfaces your Bot supports).

Engage them by requesting a response and wait for the user to respond before continuing the conversation.


### Emotion, variety and personality are vital

Unless you want to deliberately project a robotic persona, make your Bot colorful and rich in expression. Try using different font styles, emoticons, or other media in your bot's script. Try to vary the responses when the same topic or question comes up multiple times in a conversation. Over time, based on the tone and character of your Bot, the user should build a personality for your Bot in their mind.


### Be judicious with data and message frequency

Don't send out too many messages in sequence when they can be grouped into a single message.

Be sensitive to the user's network-speed and bandwidth charges when sending images and/or videos.

If you want to do something unusual, such as send a large number of messages or a large file, request consent from the user before doing it.

Don't send out messages that might appear as spam, such as wishing a user "Good night" at 10 pm every night.

### Give feedback


It's always good to give a sense of awareness to the user. Let the user know if the Bot understood or didn't understand the user's response. Paraphrase and/or confirm if the Bot is uncertain about the user's intent. Verify before performing irrevocable actions like deleting something permanently.  
If the Bot needs to take time to perform complex actions, implement a "wait" notification so the user doesn't assume that the bot has crashed.


### Keep the user in control
The user must not feel constrained or forced by the bot's script. Let the user tailor the Bot to suit their needs. For example, if the Bot sends updates on five categories, but the user is only interested in two, let the user turn off updates for the other three. Let the user select from options in your script wherever appropriate.  

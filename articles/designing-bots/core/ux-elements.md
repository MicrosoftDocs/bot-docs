---
title: Designing Bots - UX Elements | Bot Framework
description: UX Elements - Combining rich controls, natural language and voice in conversational applications 
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
# Bot Design Center - UX Elements


##Buttons, language and speech

Bots typically rely on one or more of these 3 elements as their way of exchanging information with users:

- Rich user controls: Often named "cards" (but not necessarily restricted to just cards), bots can mimic apps (or even run embedded in those apps) by using well-known controls such as buttons, images, carousels and menus. When embedded in a custom app or website they can even go further and represent virtually any UI control by leveraging the power of the app hosting them.
- Text and often, natural language: Bots can allow free text input from users and use natural language understanding APIs such as [LUIS.ai](https://www.luis.ai) to understand what the user is asking for. Note that not all text input has to be a natural language input: Many bots accept commands instead and use simple mechanisms such as regex in order to parse them. This is perfectly acceptable in many scenarios.
- Speech: Some bots leverage speech input and/or output as part of their experience. They may even run on devices that won't have a keyboard or a monitor, which leaves them with speech as the only option for communicating with the user.

##Rich user controls

For decades application and website developers have relied on UI controls in order to enable users to interact with their applications. These UI controls, it turns out, work very well. A very common mistake by beginner bot developers is to dismiss the value of these elements as not being "AI enough". Again, let us recap [what we discussed earlier](../#what-makes-a-bot-great): Success for your bot is not to beat the Touring Test, but instead solve your user's needs in the best/quickest possible way. Whether such solution uses AI or a simple button isn't relevant at all. 

##Text and natural language

##Speech

##Which one should I choose?

That is the wrong question :)

Remember: In many cases bots can use these 3 elements combined. You don't need to pick one over another. Imagine a "cooking bot" that helps users with cooking recipes. Users may have their hands busy while cooking, in which case, speech becomes a key element. They can flip pages and ask questions without having to touch the device. But that may not be the case at all and they may actually prefer to touch the screen of a device instead of talking. They may not even be comfortable with speech. Think about it: Your user may not even be able to speak and listen at all. Likewise, as the bot instructs the user about how to cook a given recipe, it would be even better to display a video or some pictures to help explaining what needs to be done.

##Which one is more "natural"

That is also the wrong question :)

None of these communication elements, when isolated from others, can be truly considered "natural". Look at the world around you and how you communicate with others: You likely use gestures, voice and symbols. If you are playing chess with someone, using a chess board is very useful. Playing chess without a board, via voice only, is far from a natural experience to most of us. 

Many people just aren't comfortable talking on the phone. They prefer typing. 

To illustrate this with some humor, imagine these scenarios:

##OK, which ones would prefer in which scenarios?

Now that is a much better question to ask ourselves: How do users actually behave when presented to these 3 elements and how does that change given different scenarios?

 



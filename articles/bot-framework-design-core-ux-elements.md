---
title: Bot Framework Designing Bots - UX elements | Microsoft Docs
description: Provides guidance about designing the user experience by combining rich controls, natural language, and voice in conversational applications.
keywords: Bot Framework, Bot design, ux, ui, ux elements, ux controls, ui elements, ui controls
author: matvelloso
manager: larar
ms.topic: Designing the user experience by combining rich controls, natural language, and voice in conversational applications

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
# Designing the user experience


##Buttons, language and speech

Bots typically rely on one or more of these 3 elements as their way of exchanging information with users:

- Rich user controls: Often named "cards" (but not necessarily restricted to just cards), bots can mimic apps (or even run embedded in those apps) by using well-known controls such as buttons, images, carousels and menus. When embedded in a custom app or website they can even go further and represent virtually any UI control by leveraging the power of the app hosting them.
- Text and often, natural language: Bots can allow free text input from users and use natural language understanding APIs such as [LUIS.ai](https://www.luis.ai) to understand what the user is asking for. Note that not all text input has to be a natural language input: Many bots accept commands instead and use simple mechanisms such as regex in order to parse them. This is perfectly acceptable in many scenarios.
- Speech: Some bots leverage speech input and/or output as part of their experience. They may even run on devices that won't have a keyboard or a monitor, which leaves them with speech as the only option for communicating with the user.

##Rich user controls

For decades application and website developers have relied on UI controls in order to enable users to interact with their applications. These UI controls, it turns out, work very well. A very common mistake by beginner bot developers is to dismiss the value of these elements as not being "AI enough". Again, let us recap [what we discussed earlier](bot-framework-design-overview.md#designGuidance): Success for your bot is not to beat the Touring Test, but instead solve your user's needs in the best/quickest possible way. Whether such solution uses AI or a simple button isn't relevant at all. 

Skype, Slack, Microsoft Teams, Facebook Messenger, Slack and other channels are all investing in different ways developers can inject visual controls into the conversation. Buttons are probably the most common case: Whenever a simple choice is presented to a user, buttons tend to work great: Clicking at a button named "Hotels" is easier and quicker than making the user type "Hotels" instead. This becomes even more relevant on mobile devices: Typing a lot on a mobile device isn't what we can consider a very low friction experience. 

Because of that, when designing a bot we typically recommend developers to start with UI controls where they can solve the problem. Add other elements later where just those controls aren't enough anymore. Interestingly, developers at first tend to start with natural language. In our experience, this typically does not work very well, because it leads to over-complexity in some areas. 

##Text and natural language

Text and natural language are very common elements used by bots. In fact, they may be the most popular elements, which is a problem: It isn't uncommon to see developers relying on natural language for solving problems that can be better solved in other ways. We will discuss examples of that along these articles in different areas.

The first important aspect to highlight is that text input is not necessarily natural language input: We may or may not want users to type free form text that needs to be parsed by a natural language API, or even if they do type free form text, a natural language API may not be the right tool to use in that particular scenario for handling such input.

As far as text input goes, there are many different types of input we can expect from an user:


- Users may be just answering a very specific question, such as "What is your name?". The answer may be just the name, such as "John" or a sentence, such as "My name is John". The more guided the question is, the less free form text we are to expect in the answer. An example would be asking the user "How are you feeling?". This is a very broad, open ended question. Understanding all the permutations of answers a user can give to such a question is a very complex task. By the other hand, the bot could ask very specific questions such as "Are you feeling pain, yes/no?", "Where are you feeling pain, chest/head/members?". Those lead to more specific answers and therefore less need for actual natural language understanding. This is a common strategy bots use to get the answer they need. Remember: Users don't know what specific information your bot needs unless your bot if very clear about what it needs to know. 
- Users may be typing specific commands. For example, an operations bot that helps developers to manage virtual machines could have a very simple command based syntax. Commands such as "/STOP VM XYZ" or "/START VM XYZ" would not only be easy to learn by such users but actually preferred over natural language forms such as "Could you please start the virtual machine XYZ?". Hardly this sort of technical user would prefer to type so much in order to get the same desired result. 
- User may be asking general questions in a knowledge base or questions and answers bot. Again, these are likely not going to be solved by natural language or at least not by natural language alone. Imagine a bot that looks at thousands of documents and is able to answer questions based on the content of such documents. Trying to train a natural language API for all the permutations of possible questions users could come up with in such a scenario is a task deemed to failure. You will never be able to realistically predict all these questions and the amount of time required to train any natural language API that way is just unpractical. Instead, there are technologies built with search in mind, capable of handling that in a much better way. Two of these technologies are [QnA Maker](https://qnamaker.ai) and [Azure Search](https://azure.microsoft.com/en-us/services/search/). We discuss these in details in our [Knowledge Base section](designing-bots/patterns/kb.md). So if you are building a bot that is supposed to answer questions based on structured or unstructured data from databases, web pages or documents, you should definitely take a look at that session before trying to solve the problem with natural language.
- Users may actually be typing simple requests based on natural language. Anything from "I want a pepperoni pizza" to "Are there any vegetarian restaurants within 3 miles from my house open now?" are examples of that. This is where natural language APIs such as [LUIS.ai](https://www.luis.ai) shine: They will extract the key components of that text and give it to your bot. So your bot will know that the "intent" of the question is "FindRestaurant", the "Entity" named "distance" is "3 miles", the "Entity" named "reference" is "my house" and the "Entity" named "condition" is "open now". It is important though to be careful with expectations:


![how users talk](media/designing-bots/core/buy-house.png)

A common mistake when bot developers build natural language models is assuming users will just magically tell the bot every little thing it needs to know, right away. Unfortunately this isn't how humans communicate: They will give us fragments of information. "I want to buy a house", which is a very broad question, far from enough for us to narrow down to a specific criteria. The solution to this problem is to ask follow up questions, guide the user. Again, users won't guess what your bot needs to know if you bot doesn't specifically ask for it. 

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

 




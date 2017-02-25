---
title: Integration between bot and web browser | Microsoft Docs
description: Learn how to design a conversational application (bot) that requires integration between bot and web browser.
keywords: bot framework, design, bot, scenario, use case, pattern, bot to web, integrate bot with web
author: matvelloso
manager: rstand
ms.topic: design-patterns-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 02/24/2017
ms.reviewer: rstand
#ROBOTS: Index
---
# Integration between bot and web browser

## Introduction

Some scenarios require more than just a bot to fulfill a requirement. 
At times, a bot may need to send the user to a web browser to complete a task, 
and then will subsequently resume the conversation with the user, after the task has been completed. 
For example, consider the following scenarios:

- **Authentication and authorization**: If a bot wants the ability to read the user's calendar in Office 365, or perhaps 
even create appointments on behalf of that user, the user must first authenticate with Microsoft Azure Active Directory and 
authorize the bot to access its calendar data. The bot will redirect the user to a web browser to complete the 
authentication and authorization tasks, and then will subsequently resume the conversation with the user. 

- **Security and compliance**: Security and compliance requirements often restrict the type of information that a bot 
can exchange with a user. In some cases, it may be necessary for the user to send/receive data 
outside of the current conversation. 
For example, if a user wants to execute a payment using a third-party payment provider, credit card number should not 
be specified within the context of the conversation. 
Instead, the bot will direct the user to a web browser to complete the payment process, 
and then will subsequently resume the conversation with the user.

In this article, we'll explore the process of facilitating a user's transition from 
bot to web browser, and back again. 

> [!NOTE]
> The process of transitioning a user from chat to web browser and back again is not ideal,
> as it can be a confusing experience for the user to go from one application to another. 
> To address this concern and provide a better user experience, 
> many channels (i.e., chat applications) offer built-in HTML windows that a bot can use to 
> surface applications that are typically viewed in a web browser. 
> Implementing this technique allows the user to interact solely within the context of the chat 
> (even though they may be accessing external resources via the built-in HTML window). 
> This is conceptually similar to how mobile applications manage authorization flows by using OAuth 
> (within embedded web views).

## Bot to web browser, and back again

The following diagram shows the high-level flow for integration between bot and web browser. 

![Bot to web interaction](media/designing-bots/patterns/bot-to-web1.png)

Let's examine each step of the flow:

1. <a id="generate-hyperlink"></a>The bot generates and displays a hyperlink that (when clicked) will redirect the user to a website. 
This hyperlink typically includes data (via querystring parameters on the target URL) that specifies information about the context of the current conversation 
(conversation ID, channel ID, user ID in the channel, etc.). 

2. The user clicks the hyperlink and is redirected to the target URL within a web browser. 

3. The bot enters a 'waiting' state, 
awaiting communication from the website to indicate that the website flow is complete.  
> [!TIP]
> Design this flow such that the bot will not permanently remain in the 'waiting' state 
> (thereby [ignoring user input](bot-framework-design-core-navigation.md#the-mysterious-bot)), 
> if the user never completes the website flow. 
> In other words, if the user abandons the web browser and starts communicating with the bot again, 
> the bot should acknowledge those communications.

4. The user completes the necessary task(s) via the web browser. 
(This could be an OAuth flow or any custom sequence of events that are required by the scenario at hand.) 

5. <a id="generate-magic-number"></a>When the user completes the website flow, the website generates a '[magic number](#verify-identity)' 
and instructs the user to copy the value and paste it back into the chat with the bot. 

6. <a id="signal-to-bot"></a>The website [signals to the bot](#website-signal-to-bot) that the user has completed the website flow. 
It communicates the 'magic number' to the bot and provides
any additional data that is relevant to the scenario at hand. 
For example, in the case of an OAuth flow, the website would provide an access token to the bot.

7. The user returns to the bot and pastes the 'magic number' into the chat. 
The bot validates that 'magic number' provided by the user matches the expected value 
(thereby verifying that the current user is the same user who previously clicked the hyperlink to initiate the website flow). 

###<a id="verify-identity"></a> Verifying user identity using the 'magic number'

The generation of a 'magic number' during the bot-to-website flow ([step 5](#generate-magic-number) above) 
enables the bot to subsequently verify that the user who initiated the website flow is indeed the user 
for whom it was intended. 
For example, if a bot is conducting a group chat with multiple users, any one of them 
could have clicked the hyperlink to initiate the website flow. Without the 'magic number' validation process, 
the bot has no way of knowing which user completed the flow. 
One user could authenticate and inject access tokens in another user's session. 
This isn't just a risk within group chats; without the 'magic number' validation process, 
anyone who obtains the hyperlink to launch the website flow can spoof a user's identity. 

The magic number should be a random number generated using a strong cryptography library. 
For an example of the generation process (in C#), see 
<a href="https://github.com/MicrosoftDX/AuthBot/blob/master/AuthBot/Controllers/OAuthCallbackController.cs#L138" target="_blank">this code</a>
within the <a href="https://github.com/MicrosoftDX/AuthBot" target="_blank">AuthBot</a> library. 
AuthBot enables bots (that are built in Microsoft Bot Framework) to implement 
the bot-to-website flow to authenticate a user in a website and then to subsequently use the access token 
that was generated from the authentication process. 
AuthBot does not make any assumptions about the capabilities of various channels; 
therefore, such flows should function well with most channels (although there may be some exceptions). 

> [!NOTE]
> The need for the 'magic number' validation process should dissipate as channels build their own embedded web views.

###<a id="website-signal-to-bot"></a> How does the website 'signal' the bot?

When the bot [generates the hyperlink](#generate-hyperlink) that the user will click to initiate the website flow, 
it includes information (via querystring parameters in the target URL) about the context of the current conversation 
(conversation ID, channel ID, user ID in the channel, etc.). 
The website can subsequently use this information (in [step 6](#signal-to-bot) above) to read and write state variables for that user or conversation, 
by using the Microsoft Bot Framework APIs (i.e., the Bot Builder SDK for .NET, the Bot Builder SDK for Node.js, or the REST API). 
In this manner, the website can 'signal' to the bot that the user has completed the website flow.

## Additional resources

In this article, we explored the process of facilitating a user's transition from bot to web browser, and back again. 
To see sample code for bots that implement this flow, review the following resources: 

> [!NOTE]
> To do: Add links to the C# and Node.js code samples that Mat refers to.

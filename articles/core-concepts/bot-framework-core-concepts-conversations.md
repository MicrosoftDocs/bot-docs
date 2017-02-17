---
title: Conversations | Microsoft Docs
description: Bot Framework Core Concept Conversations
services: service-name
documentationcenter: BotFramework-Docs
author: Kbrandl
manager: rstand

ms.service: required
ms.devlang: may be required
ms.topic: article
ms.tgt_pltfrm: may be required
ms.workload: required
ms.date: 01/31/2017
ms.author: v-kibran@microsoft.com

---

#Conversations in the Bot Framework
Conversations can take many forms. For example, your bot can have a private conversation with a single user, or a group conversation with multiple users including other bots. Most channels support private conversations but not all channels support group conversations. To determine whether the channel supports group conversations, see the channel's documentation.

Most of the time, users will start the conversation. If the user starts the conversation, your bot simply responds to messages that the user sends (see [Sending and receiving messages](#)). But sometimes your bot may want to start the conversation. For example, if your bot knows about the user's interests, and it learns of a news event or article that's related to one of their interests, your bot can start the conversation with the user to alert them to the article.

In order to start a conversation, your bot needs to know its account information on that channel as well as the user's account information. If you're going to start conversations, make sure you cache the account information along with any other relevant information such as user preferences and locale (so the message uses the language of the user).

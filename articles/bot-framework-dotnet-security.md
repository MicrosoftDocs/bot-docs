---
title: Secure your bot with the Bot Builder SDK for .NET | Microsoft Docs
description: Learn how to enable security for a bot that is built using the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, Connector, Connector service, security, HTTPS endpoint
author: kbrandl
manager: rstand
ms.topic: develop-dotnet-article
ms.prod: botframework
ms.service: Bot Builder
ms.date: 03/09/2017
ms.reviewer:
#ROBOTS: Index
---

# Secure your bot

To ensure that your bot's endpoint can only be called by the Bot Framework Connector service, 
configure your bot's endpoint to use only HTTPS and 
enable Bot Framework authentication by [registering](bot-framework-publish-register.md) your bot 
to acquire its app Id and password.

After you have registered your bot, specify its app Id and password in the application's web.config file.

```xml
<appSettings>
    <add key="MicrosoftAppId" value="_appIdValue_" />
    <add key="MicrosoftAppPassword" value="_passwordValue_" />
</appSettings>
```

Then, when creating your bot using the Bot Builder SDK for .NET, use the **[BotAuthentication]** 
attribute to specify authentication credentials.

- To use the authentication credentials that are stored in the web.config file, 
simply specify the **[BotAuthentication]** attribute. For example:
```cs
[BotAuthentication]
public class MessagesController : ApiController
{
}
```

- Alternatively, to use specific values for authentication credentials, 
specify the **[BotAuthentication]** attribute and pass in those values. For example: 
```cs
[BotAuthentication(MicrosoftAppId = "_appIdValue_", MicrosoftAppPassword="_passwordValue_")]
public class MessagesController : ApiController
{
}
```

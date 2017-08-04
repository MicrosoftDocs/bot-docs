---
title: Secure your bot | Microsoft Docs
description: Learn how to secure your bot by using HTTPS and Bot Framework Authentication.
author: kbrandl
ms.author: kibrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 08/04/2017
ms.reviewer:
---

# Secure your bot

Your bot can be connected to many different communication channels (Skype, SMS, email, and others) through the Bot Framework Connector service. This article describes how to secure your bot by using HTTPS and Bot Framework authentication.

## Use HTTPS and Bot Framework authentication

To ensure that your bot's endpoint can only be accessed by the Bot Framework [Connector](bot-builder-dotnet-concepts.md#connector), configure your bot's endpoint to use only HTTPS and enable Bot Framework authentication by [registering](../portal-register-bot.md) your bot to acquire its app Id and password. 

## Configure authentication for your bot

After you have registered your bot, specify its app Id and password in your bot's web.config file. 

```xml
<appSettings>
    <add key="MicrosoftAppId" value="_appIdValue_" />
    <add key="MicrosoftAppPassword" value="_passwordValue_" />
</appSettings>
```

Then, use the `[BotAuthentication]` attribute to specify authentication credentials when using the Bot Builder SDK for .NET to create your bot. 

To use the authentication credentials that are stored in the web.config file, specify the `[BotAuthentication]` with no parameters.

[!code-csharp[Use botAuthentication attribute](../includes/code/dotnet-security.cs#attribute1)]

To use other values for authentication credentials, specify the `[BotAuthentication]` attribute and pass in those values.

[!code-csharp[Use botAuthentication attribute with parameters](../includes/code/dotnet-security.cs#attribute2)]

## Additional resources

- [Bot Builder SDK for .NET](bot-builder-dotnet-overview.md)
- [Key concepts in the bot Builder SDK for .NET](bot-builder-dotnet-concepts.md)
- [Register a bot with the Bot Framework](../portal-register-bot.md)
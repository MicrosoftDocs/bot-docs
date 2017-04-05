---
title: Secure your bot with the Bot Builder SDK for .NET | Microsoft Docs
description: Learn how to enable security for a bot that is built using the Bot Builder SDK for .NET.
keywords: Bot Framework, .NET, Bot Builder, SDK, Connector, Connector service, security, HTTPS endpoint
author: kbrandl
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 03/09/2017
ms.reviewer:
#ROBOTS: Index
---

# Secure your bot

This article describes how to secure your bot by using HTTPS and Bot Framework authentication.

## Use HTTPS and Bot Framework authentication

To ensure that your bot's endpoint can only be accessed by the Bot Framework [Connector](~/dotnet/concepts.md#connector), 
configure your bot's endpoint to use only HTTPS and 
enable Bot Framework authentication by [registering](~/portal-register-bot.md) your bot 
to acquire its app Id and password.

## Configure authentication for your bot

After you have registered your bot, specify its app Id and password in your bot's web.config file.

```xml
<appSettings>
    <add key="MicrosoftAppId" value="_appIdValue_" />
    <add key="MicrosoftAppPassword" value="_passwordValue_" />
</appSettings>
```

Then, use the `[BotAuthentication]` attribute to specify authentication credentials when 
using the Bot Builder SDK for .NET to create your bot.

To use the authentication credentials that are stored in the web.config file, 
simply specify the `[BotAuthentication]` attribute.

[!code-csharp[Use BotAuthentication attribute](~/includes/code/dotnet-security.cs#attribute1)]

To use other values for authentication credentials, 
specify the `[BotAuthentication]` attribute and pass in those values.

[!code-csharp[Use BotAuthentication attribute with parameters](~/includes/code/dotnet-security.cs#attribute2)]

## Additional resources

- [Bot Builder SDK for .NET](~/dotnet/index.md)
- [Key concepts in the Bot Builder SDK for .NET](~/dotnet/concepts.md)
- [Register a bot with the Bot Framework](~/portal-register-bot.md)
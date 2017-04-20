---
title: Secure your bot | Microsoft Docs
description: Learn how to enable available security protocols for a bot built with the Bot Builder SDK for .NET.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 03/09/2017
ms.reviewer:
ROBOTS: Index, Follow
---

# Secure your bot

* Configure your bot's endpoint to allow HTTPS only. This permits the Bot Framework [Connector](~/dotnet/concepts.md#connector) to access the endpoint while denying access to all other sources.

* Enable and configure Bot Framework authentication.

## Configure authentication for your bot

 [Register](~/portal-register-bot.md) your bot to acquire its app Id and password.
 After you have registered your bot, specify its app Id and password in your bot's web.config file.

```xml
<appSettings>
    <add key="MicrosoftAppId" value="_appIdValue_" />
    <add key="MicrosoftAppPassword" value="_passwordValue_" />
</appSettings>
```

Then use the `[BotAuthentication]` attribute to specify authentication credentials when 
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
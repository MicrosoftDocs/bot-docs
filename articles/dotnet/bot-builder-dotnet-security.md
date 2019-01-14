---
title: Secure your bot | Microsoft Docs
description: Learn how to secure your bot by using HTTPS and Bot Framework Authentication.
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/13/17
monikerRange: 'azure-bot-service-3.0'
---

# Secure your bot

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

Your bot can be connected to many different communication channels (Skype, SMS, email, and others) through the Bot Framework Connector service. This article describes how to secure your bot by using HTTPS and Bot Framework authentication.

## Use HTTPS and Bot Framework authentication

To ensure that your bot's endpoint can only be accessed by the Bot Framework [Connector](bot-builder-dotnet-concepts.md#connector), configure your bot's endpoint to use only HTTPS and enable Bot Framework authentication by [registering](~/bot-service-quickstart-registration.md) your bot to acquire its appID and password. 

## Configure authentication for your bot

Specify the bot's appID and password in your bot's web.config file. 

> [!NOTE]
> To find your bot's **AppID** and **AppPassword**, see [MicrosoftAppID and MicrosoftAppPassword](~/bot-service-manage-overview.md#microsoftappid-and-microsoftapppassword).

```xml
<appSettings>
    <add key="MicrosoftAppId" value="_appIdValue_" />
    <add key="MicrosoftAppPassword" value="_passwordValue_" />
</appSettings>
```

Then, use the `[BotAuthentication]` attribute to specify authentication credentials when using the Bot Framework SDK for .NET to create your bot. 

To use the authentication credentials that are stored in the web.config file, specify the `[BotAuthentication]` with no parameters.

[!code-csharp[Use botAuthentication attribute](../includes/code/dotnet-security.cs#attribute1)]

To use other values for authentication credentials, specify the `[BotAuthentication]` attribute and pass in those values.

[!code-csharp[Use botAuthentication attribute with parameters](../includes/code/dotnet-security.cs#attribute2)]

## Additional resources

- [Bot Framework SDK for .NET](bot-builder-dotnet-overview.md)
- [Key concepts in the bot Builder SDK for .NET](bot-builder-dotnet-concepts.md)
- [Register a bot with the Bot Framework](~/bot-service-quickstart-registration.md)

---
title: Secure your bot | Microsoft Docs
description: Learn how to enable available security protocols for a bot built with the bot Builder SDK for .NET.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 03/09/2017
ms.reviewer:
RObotS: Index, Follow
---

# Secure your bot

Your bot can be connected to many different communication channels (Skype, SMS, email, and others) through the Bot Framework Connector service. It is critical to ensure that your bot sends and receives only legitimate messages along these channels. To prevent spam and other potentially malicious communication, implement these security measures.

* Configure your bot's endpoint to allow HTTPS only. 

* Configure Bot Framework authentication.

## Configure endpoint

Make sure your bot's endpoint accepts HTTPS connections only. This permits the Bot Framework [Connector](~/dotnet/bot-builder-dotnet-concepts.md#connector) to access the endpoint while denying access to all unauthorized sources.

## Configure authentication

 If you haven't already, [register](~/portal-register-bot.md) your bot. During the registration process, the Bot Framework will generate and expose the bot's app Id and password.

 >[!NOTE]
 > The **MicrosoftAppId** is generated when your bot is registered with the Microsoft 
 > Bot Framework Connector and is used for authentication and channel configuration.
 > The **botId**, which you specify when you create your bot, is used to generate the URL
 > in the developer portal.
 
Specify the bot's app Id and password in your bot's `web.config` file.

```xml
<appSettings>
    <add key="MicrosoftAppId" value="_appIdValue_" />
    <add key="MicrosoftAppPassword" value="_passwordValue_" />
</appSettings>
```
When using the bot Builder SDK for .NET to create your bot, populate the `[botAuthentication]` attribute with the authentication credentials now stored in the `web.config` file, 

[!code-csharp[Use botAuthentication attribute](~/includes/code/dotnet-security.cs#attribute1)]

To use other values for authentication credentials, specify the `[botAuthentication]` attribute and pass in those values.

[!code-csharp[Use botAuthentication attribute with parameters](~/includes/code/dotnet-security.cs#attribute2)]

## Additional resources

- [bot Builder SDK for .NET](~/dotnet/index.md)
- [Key concepts in the bot Builder SDK for .NET](~/dotnet/bot-builder-dotnet-concepts.md)
- [Register a bot with the Bot Framework](~/portal-register-bot.md)
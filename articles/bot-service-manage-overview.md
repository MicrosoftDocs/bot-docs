---
title: Manage a bot | Microsoft Docs
description: Learn how to manage a bot through the bot service portal. 
keywords: azure portal, bot management, test in web chat, MicrosoftAppID, MicrosoftAppPassword, application settings
author: v-ducvo
ms.author: rstand
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: abs
ms.date: 4/13/2019
---
# Manage a bot

[!INCLUDE [applies-to-both](includes/applies-to-both.md)]

This topic explains how to manage a bot using the Azure portal.

## Bot settings overview

![Bot settings overview](~/media/azure-manage-a-bot/overview.png)

In the **Overview** blade, you can find high level information about your bot. For example, you can see your bot's **Subscription ID**, **pricing tier**, and **Messaging endpoint**.

## Bot management

 You can find most of your bot's management options under the **BOT MANAGEMENT** section. Below is a list of options to help you manage your bot.

![Bot management](~/media/azure-manage-a-bot/bot-management.png)

| Option |  Description |
| ---- | ---- |
| **Build** | The Build tab provides options for making changes to your bot. This option is not available for **Registration Only Bot**. |
| **Test in Web Chat** | Use the integrated Web Chat control to help you quickly test your bot. |
| **Analytics** | If analytics is turned on for your bot, you can view the analytics data that Application Insights has collected for your bot. |
| **Channels** | Configure the channels your bot uses to communicate with users. |
| **Settings** | Manage various bot profile settings such as display name, analytics, and messaging endpoint. |
| **Speech priming** | Manage the connections between your LUIS app and the Bing Speech service.. |
| **Bot Service pricing** | Manage the pricing tier for the bot service. |

## App service settings

![App Service Settings](~/media/azure-manage-a-bot/app-service-settings.png)

The **Application Settings** blade contains detailed information about your bot, such as the bot's environment, debug settings, and application settings keys.

### MicrosoftAppID and MicrosoftAppPassword

The **MicrosoftAppID** and **MicrosoftAppPassword** are kept within the bot's settings file(`appsettings.json` or `.env`), or Azure Key Vault. To retrieve them, either download your bot's setting or config file (for older bots, if it exists), or access Azure Key Vault. It may be necessary for you to test locally with the ID and password.

> [!NOTE]
> The **Bot Channels Registration** bot service comes with a *MicrosoftAppID* but because there is no app service associated with this type of service, there is no **Application Settings** blade for you to look up the *MicrosoftAppPassword*. To get the password, you must go generate one. To generate the password for a **Bot Channels Registration**, see [Bot Channels Registration password](bot-service-quickstart-registration.md#bot-channels-registration-password)

## Next steps
Now that you have explored the Bot Service blade in the Azure portal, learn how to use the Online Code Editor to customize your bot.
> [!div class="nextstepaction"]
> [Use the Online Code Editor](bot-service-build-online-code-editor.md)

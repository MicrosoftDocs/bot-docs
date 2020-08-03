---
title: Manage a bot - Bot Service
description: Learn how to manage a bot through the bot service portal.
keywords: azure portal, bot management, test in web chat, MicrosoftAppID, MicrosoftAppPassword, application settings
author: v-ducvo
ms.author: rstand
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 08/2/2020
---

# Manage a bot

[!INCLUDE [applies-to-both](includes/applies-to-both.md)]

In your browser, navigate to the [Azure portal](https://ms.portal.azure.com/), then select your resource application such as a **Bot Channels Registration**. In the left panel, you  wil the sections described below.

### Bot settings overview

These clickable links displays panels, on the right, that provide general information applicable to a resource (bot).

![Bot settings overview](~/media/azure-manage-a-bot/overview.png)

> [!div class="mx-tdBreakAll"]
> | Option |  Description |
> | ---- | ---- |
> |**Overview** <img width="300px"/>| The related panel contains high level information about the bot. For example, you can see your bot's **Subscription ID**, **Messaging endpoint** and so on.|
> |**Activity log**| Platform logs provide detailed diagnostic and auditing information for Azure resources and the Azure platform they depend on. They are automatically generated although you need to configure certain platform logs to be forwarded to one or more destinations to be retained. For more information, see [Overview of Azure platform logs](https://docs.microsoft.com/azure/azure-monitor/platform/platform-logs-overview).|
> |**Access control (IAM)**| View the access a user or another security principal has to Azure resources. For more information, see [View the access a user has to Azure resources](https://docs.microsoft.com/azure/role-based-access-control/check-access).|
> |**Tags**|Apply tags to Azure resources, resource groups, and subscriptions to logically organize them into a taxonomy. For more information, see [Use tags to organize your Azure resources](https://docs.microsoft.com/azure/azure-resource-manager/management/tag-resources). |

### Bot management

 You can find most of your bot's management options in the **Bot Management** section. Below is a list of options to help you manage your bot.

![Bot management](~/media/azure-manage-a-bot/bot-management.png)

> [!div class="mx-tdBreakAll"]
> | Option |  Description |
> | ---- | ---- |
> | **Build** <img width="300px"/>| The Build tab provides options for making changes to your bot. This option is not available for **Registration Only Bot**. |
> | **Test in Web Chat** | Use the integrated Web Chat control to help you quickly test your bot. |
> | **Analytics** | If analytics is turned on for your bot, you can view the analytics data that Application Insights has collected for your bot. |
> | **Channels** | Configure the channels your bot uses to communicate with users. |
> | **Settings** | Manage various bot profile settings such as display name, analytics, and messaging endpoint. |
> | **Speech priming** | Manage the connections between your LUIS app and the Bing Speech service. |
> | **Bot Service pricing** | Manage the pricing tier for the bot service. |

## Application service settings

A bot application, also known as application service, has a set of configurable items that you can access through the Azure portal as described below.

1. In your browser, navigate to the Azure portal

![App Service Settings](~/media/azure-manage-a-bot/app-service-settings.png)

The **Application Settings** blade contains detailed information about your bot, such as the bot's environment, debug settings, and application settings keys.

### MicrosoftAppID and MicrosoftAppPassword

The **MicrosoftAppID** and **MicrosoftAppPassword** are kept within the bot's settings file(`appsettings.json` or `.env`), or Azure Key Vault. To retrieve them, either download your bot's setting or config file (for older bots, if it exists), or access Azure Key Vault. It may be necessary for you to test locally with the ID and password.

> [!NOTE]
> The **Bot Channels Registration** bot service comes with a *MicrosoftAppID* but because there is no app service associated with this type of service, there is no **Application Settings** blade for you to look up the *MicrosoftAppPassword*. To get the password, you must go generate one. To generate the password for a **Bot Channels Registration**, see
[Bot Channels Registration password](bot-service-manage-settings.md#get-registration-password)

## Next steps
Now that you have explored the Bot Service blade in the Azure portal, learn how to use the Online Code Editor to customize your bot.
> [!div class="nextstepaction"]
> [Use the Online Code Editor](bot-service-build-online-code-editor.md)

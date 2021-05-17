---
title: Manage a bot - Bot Service
description: Learn how to manage bots. See how to use the Azure portal to find information on activity logs, build options, debug settings, and other properties.
keywords: azure portal, bot management, test in web chat, MicrosoftAppID, MicrosoftAppPassword, application settings
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 04/15/2021
---

# Manage a bot

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

In your browser, navigate to the [Azure portal](https://ms.portal.azure.com/). Select your resource application, such as an **Azure Bot**. On the navigation pane you'll see the sections described below.

## Azure Bot resource settings

The **Azure Bot** resource contains the settings described below.

### General

At the top of the navigation pane are links for general information applicable to a bot.

:::image type="content" source="media/azure-manage-a-bot/overview.png" alt-text="General information about a bot.":::

> [!div class="mx-tdBreakAll"]
> | Link |  Description |
> | ---- | ---- |
> |**Overview** <img width="200px"/>| Contains high level information about the bot, such as a bot's **Subscription ID** and **Messaging endpoint**. On the overview for a **Web App Bot**, you can also download the bot source code.|
> |**Activity log**| Provides detailed diagnostic and auditing information for Azure resources and the Azure platform they depend on. For more information, see [Overview of Azure platform logs](/azure/azure-monitor/platform/platform-logs-overview).|
> |**Access control (IAM)**| Displays the access users or other security principals have to Azure resources. For more information, see [View the access a user has to Azure resources](/azure/role-based-access-control/check-access).|
> |**Tags**|Displays the tags to your Azure resources, resource groups, and subscriptions to logically organize them into a taxonomy. For more information, see [Use tags to organize your Azure resources](/azure/azure-resource-manager/management/tag-resources). |

### Settings

In the **Settings** section are links for most of your bot's management options.

:::image type="content" source="media/azure-manage-a-bot/bot-management.png" alt-text="Common management options.":::

> [!div class="mx-tdBreakAll"]
> | Link |  Description |
> | ---- | ---- |
> | **Bot profile** <img width="200px"/>| Manages various bot profile settings, such as display name, icon, and description. |
> | **Configuration** | Manages various bot settings, such as analytics, messaging endpoint, and OAuth connection settings. |
> | **Channels** | Configures the channels your bot uses to communicate with users. |
> | **Pricing** | Manages the pricing tier for the bot service. |
> | **Test in Web Chat** | Uses the integrated Web Chat control to quickly test your bot. |
> | **Encryption** | Manages your encryption keys. |
> | **Properties** | Lists your bot resource properties, such as resource ID, subscription ID, and resource group ID. |
> | **Locks** | Manages your resource locks.|

### Monitoring

In the **Monitoring** section are links for most of your bot's monitoring options.

:::image type="content" source="media/azure-manage-a-bot/bot-monitoring.png" alt-text="Common monitoring options.":::

> [!div class="mx-tdBreakAll"]
> | Link |  Description |
> | ---- | ---- |
> | **Conversational analytics** <img width="140px"/> | Enables analytics to view the collected data with Application Insights. This Analytics blade will be deprecated. For more information, see [Add telemetry to your bot](/azure/bot-service/bot-builder-telemetry?view=azure-bot-service-4.0&tabs=csharp&WT.mc_id=Portal-Microsoft_Azure_BotService&preserve-view=true) and [Analyze your bot's telemetry data](/azure/bot-service/bot-builder-telemetry-analytics-queries?view=azure-bot-service-4.0&WT.mc_id=Portal-Microsoft_Azure_BotService&preserve-view=true). |
> | **Alerts** | Configure alert rules and attend to fired alerts to efficiently monitor your Azure resources. For more information, see [Overview of alerts in Microsoft Azure](/azure/azure-monitor/alerts/alerts-overview?toc=%2Fazure%2Fazure-monitor%2Ftoc.json).|
> |**Metrics**| Select a metric to see data in the proper chart.|
> |**Diagnostic settings**| Diagnostic settings are used to configure streaming export of platform logs and metrics for a resource to the destination of your choice. For more information,see [diagnostic settings](/azure/azure-monitor/essentials/diagnostic-settings?WT.mc_id=Portal-Microsoft_Azure_Monitoring&tabs=CMD). |
> |**Logs**| Produce insights from Azure Monitor logs. |

## Application service settings

A bot application, also known as an *application service (App Service)*, has a set of application settings that you can access through the Azure portal. They are environment variables passed to the bot application code. For more information, see [Configure an App Service app in the Azure portal](/azure/app-service/configure-common).

1. In your browser, navigate to the [Azure portal](https://ms.portal.azure.com/).
1. Search for your bot app service and select its name.
1. The bot app service information is displayed. The following picture is a partial view of the settings. 

    :::image type="content" source="media/azure-manage-a-bot/app-service-settings.png" alt-text="Navigation pane for an app service resource.":::

[!INCLUDE [azure bot appid password](~/includes/authentication/azure-bot-appid-password.md)]

## Next steps

Now that you have explored the Bot Service blade in the Azure portal, learn how to use the Online Code Editor to customize your bot.
> [!div class="nextstepaction"]
> [How bots work](v4sdk/bot-builder-basics.md)

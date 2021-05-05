---
title: Manage a bot - Bot Service
description: Learn how to manage bots. See how to use the Azure portal to find information on activity logs, build options, debug settings, and other properties.
keywords: azure portal, bot management, test in web chat, MicrosoftAppID, MicrosoftAppPassword, application settings
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 02/09/2021
---

# Manage a bot

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

In your browser, navigate to the [Azure portal](https://ms.portal.azure.com/). Select your resource application, such as a **Bot Channels Registration**. On the navigation pane you'll see the sections described below.

## Bot resource settings

There are a few different types of bot resources, such as a **Web App Bot** or a **Bot Channels Registration**.
The information available for each type of resource is largely the same.

### General

At the top of the navigation pane are links for general information applicable to a bot.

:::image type="content" source="media/azure-manage-a-bot/overview.png" alt-text="Links from the navigation pane to general information about a bot.":::

> [!div class="mx-tdBreakAll"]
> | Link |  Description |
> | ---- | ---- |
> |**Overview** <img width="200px"/>| Contains high level information about the bot, such as a bot's **Subscription ID** and **Messaging endpoint**. On the overview for a **Web App Bot**, you can also download the bot source code.|
> |**Activity log**| Provides detailed diagnostic and auditing information for Azure resources and the Azure platform they depend on. For more information, see [Overview of Azure platform logs](/azure/azure-monitor/platform/platform-logs-overview).|
> |**Access control (IAM)**| Displays the access users or other security principals have to Azure resources. For more information, see [View the access a user has to Azure resources](/azure/role-based-access-control/check-access).|
> |**Tags**|Displays the tags to your Azure resources, resource groups, and subscriptions to logically organize them into a taxonomy. For more information, see [Use tags to organize your Azure resources](/azure/azure-resource-manager/management/tag-resources). |

### Settings

In the **Settings** section are links for most of your bot's management options.

:::image type="content" source="media/azure-manage-a-bot/bot-management.png" alt-text="Links from the navigation pane to common management options.":::

> [!div class="mx-tdBreakAll"]
> | Link |  Description |
> | ---- | ---- |
> | **Bot profile** <img width="200px"/>| Manages various bot profile settings, such as display name, icon, and description. |
> | **Configuration** | Manages various bot settings, such as analytics, messaging endpoint, and OAuth connection settings. |
> | **Channels** | Configures the channels your bot uses to communicate with users. |
> | **Speech priming** | Manages the connections between your LUIS app and the Bing Speech service. |
> | **Pricing** | Manages the pricing tier for the bot service. |
> | **Test in Web Chat** | Uses the integrated Web Chat control to quickly test your bot. |
> | **Encryption** | Manages your encryption keys. |
> | **Properties** | Lists your bot resource properties, such as resource ID, subscription ID, and resource group ID. |
> | **Locks** | Manages your resource locks. |

### Monitoring

In the **Monitoring** section are links for most of your bot's monitoring options.

> [!div class="mx-tdBreakAll"]
> | Link |  Description |
> | ---- | ---- |
> | **Conversational analytics** <img width="100px"/> | Enables analytics to view the collected data with Application Insights. |

## Application service settings

A bot application, also known as an _application service_, has a set of application settings that you can access through the Azure portal. These app settings are variables passed as environment variables to the application code. For more information, see [Configure an App Service app in the Azure portal](/azure/app-service/configure-common).

1. In your browser, navigate to the [Azure portal](https://ms.portal.azure.com/).
1. Search for your app service and select its name.
1. The app service is displayed.

    :::image type="content" source="media/azure-manage-a-bot/app-service-settings.png" alt-text="Navigation pane for an app service resource.":::

<!-- TODO figure out what this next paragraph is trying to say, and rewrite it to be clear. -->
The **Application Settings** left panel contains detailed information about the bot, such as the bot's environment, debug settings, and application settings keys.

If you created a web app bot, you can find a shortcut to the related application service in the **Overview** pane.

:::image type="content" source="media/azure-manage-a-bot/app-service-shortcut.png" alt-text="Link to the web app service from the Overview pane.":::

### MicrosoftAppID and MicrosoftAppPassword

The **MicrosoftAppID** and **MicrosoftAppPassword** are kept within the bot's settings file(`appsettings.json` or `.env`), or Azure Key Vault. To retrieve them, either download your bot's setting or config file (for older bots, if it exists), or access Azure Key Vault. It may be necessary for you to test locally with the ID and password.

> [!NOTE]
> The **Bot Channels Registration** bot service comes with a *MicrosoftAppID*, but because there is no app service associated with this type of service there is also no **Application Settings** blade for you to look up the *MicrosoftAppPassword*. See [Bot Channels Registration password](bot-service-manage-settings.md#get-registration-password) to learn how to generate the password.

## Next steps

Now that you have explored the Bot Service blade in the Azure portal, learn how to use the Online Code Editor to customize your bot.
> [!div class="nextstepaction"]
> [How bots work](v4sdk/bot-builder-basics.md)

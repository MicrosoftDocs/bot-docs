---
title: Configure bot settings - Bot Service
description: Learn how to configure the various options for your bot using the Azure portal.
keywords: configure bot settings, Display Name, Icon, Application Insights, Settings blade
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/04/2021
---

# Configure bot registration settings

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

The Azure Bot resource settings, such as display name, icon, and description, can be viewed and modified in the **Bot profile** pane.
The Azure Bot resource settings, such as messaging endpoint, Microsoft app ID, and Application Insights, can be viewed and modified in the **Configuration** pane.

## Bot profile

:::image type="content" source="media/bot-service-portal-configure-settings/bot-service-profile.png" alt-text="Bot profile settings.":::

Below is the list of the **Bot profile** fields:

> [!div class="mx-tdBreakAll"]
> | Field <img width="400px"/>| Description |
> | :---  | :---        |
> | Icon | A custom icon to visually identify your bot in channels and as the icon for your bot in Microsoft Teams or other services. This icon must be in PNG format, and no larger than 30 KB in size. This value can be changed at any time. |
> | Display name | The name of your bot in channels and directories. The name can be up to 35 characters long. This value can be changed at any time. |
> | Bot handle | A unique identifier for your bot. This value cannot be changed after creating your bot with the Bot Service. |

To save your changes, select **Apply** at the bottom of the blade.

## Configuration

:::image type="content" source="media/bot-service-portal-configure-settings/bot-service-settings.png" alt-text="Bot configuration settings.":::

Below is the list of the **Configuration** fields:

> [!div class="mx-tdBreakAll"]
> | Field <img width="400px"/>| Description |
> | :---  | :---        |
> | Messaging endpoint | The endpoint to communicate with your bot. |
> | Microsoft App ID | Unique identifier for your bot. This value cannot be changed. You can generate a new password by clicking on the **Manage** link. |
> | Application Insights Instrumentation key | Unique key for bot telemetry. Copy your Azure Application Insights Key to this field if you want to receive bot telemetry for this bot. This value is optional. Bots created in the Azure Portal will have this key generated for them. For more details on this field, see [Application Insights keys](bot-service-resources-app-insights-keys.md). |
> | Application Insights API key | Unique key for bot analytics. Copy your Azure Application Insights API Key to this field if you want to view analytics about your bot in the Dashboard. This value is optional. For more details on this field, see [Application Insights keys](bot-service-resources-app-insights-keys.md). |
> | Application Insights Application ID | Unique ID for bot analytics. Copy your Azure Insights Application ID Key to this field if you want to view analytics about your bot in the Dashboard. This value is optional. Bots created in the Azure Portal will have this key generated for them. For more details on this field, see [Application Insights keys](bot-service-resources-app-insights-keys.md). |

To save your changes, select **Apply** at the bottom of the blade.

## Application ID and password

The Azure Bot resource application ID and password are assigned to the bot's variables `MicrosoftAppID` and `MicrosoftAppPassword` contained in your bot project configuration file. The file differs depending on the programming language you use to create the bot, as shown in the table below.

|Language|File Name                 |
|-------|---------------------------|   
|Csharp    |`appsettings.json`      |
|JavaScript|`.env`                  |
|Java      |`application.properties`| 
|Python    |`config.py`             |

> [!NOTE]
> The Bot Channels Registration has an application ID, but because there is no app service associated with it, there is no password. To generate the password follow the steps in the next section.

### Get registration password

An Azure Bot resource has an **application ID** (app ID) and a **password** associated with it.
The Azure Bot Service assigns a unique application ID to the application. You can obtain the password following the steps described below.

1. In your browser, navigate to the [Azure portal](https://ms.portal.azure.com).
1. In the resource list, click on the registration application name.
1. In the right panel go to the *Bot Management* section and click **Settings**. The registration application *Settings* page will display.
1. Click the **Manage** link next to *Microsoft App ID*.

    :::image type="content" source="media/azure-bot-quickstarts/bot-channels-registration-password.png" alt-text="bot registration settings password":::

1. In the *Certificates & secrets* displayed page, click the **New client secret** button.

    :::image type="content" source="media/azure-bot-quickstarts/bot-channels-registration-app-secrets.png" alt-text="bot registration app secrets":::

1. Add the description, select the expiration time, and click the **Add** button.

    :::image type="content" source="media/azure-bot-quickstarts/bot-channels-registration-app-secrets-create.png" alt-text="bot registration create password":::

    This will generate a new password for your bot. Copy this password and save it to a file. This is the only time you will see this password. If you do not have the full password saved, you will need to repeat the process to create a new password should you need it later.

## Additional Information

You can use [az bot update](/cli/azure/bot#az_bot_update) to update bot settings from the command line.

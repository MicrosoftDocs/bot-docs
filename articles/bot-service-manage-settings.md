---
title: Configure bot settings - Bot Service
description: Learn how to configure the various options for your bot using the Azure portal.
keywords: configure bot settings, Display Name, Icon, Application Insights, Settings blade
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: bot-service
ms.topic: how-to
ms.date: 11/19/2021
ms.custom:
  - evergreen
---

# Configure bot registration settings

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

The Azure Bot resource settings, such as display name, icon, and description, can be viewed and modified in the **Bot profile** pane.
The Azure Bot resource settings, such as messaging endpoint, Microsoft app ID, and Application Insights, can be viewed and modified in the **Configuration** pane.

## Bot profile

:::image type="content" source="media/bot-service-portal-configure-settings/bot-service-profile.png" alt-text="Bot profile settings.":::

Below is the list of the **Bot profile** fields:

| Field | Description |
|:-|:-|
| Icon | A custom icon to visually identify your bot in channels and as the icon for your bot in Microsoft Teams or other services. |
| Display name | The name of your bot in channels and directories. You can change this value later. |
| Bot handle | A unique identifier for your bot. This value can't be changed after creating your bot with the Bot Service. |
| Description | A description of your bot. Some channels display the description. You can change this value later. |

To save your changes, select **Apply** at the bottom of the blade.

## Configuration

:::image type="content" source="media/bot-service-portal-configure-settings/bot-service-settings.png" alt-text="Bot configuration settings.":::

Below is the list of the **Configuration** fields:

| Field | Description |
|:-|:-|
| Messaging endpoint | The endpoint to communicate with your bot. |
| Microsoft App ID | Unique identifier for your bot. This value can't be changed. You can generate a new password by clicking on the **Manage** link. |
| Schema Transformation Version | The bot schema transform version to use for this bot. For more information, see [Connect a bot to channels](bot-service-manage-channels.md). |
| Application Insights Instrumentation key | Unique key for bot telemetry. Copy your Azure Application Insights Key to this field if you want to receive bot telemetry for this bot. This value is optional. For more details on this field, see [Application Insights keys](bot-service-resources-app-insights-keys.md). |
| Application Insights API key | Unique key for bot analytics. Copy your Azure Application Insights API Key to this field if you want to view analytics about your bot in the Dashboard. This value is optional. For more details on this field, see [Application Insights keys](bot-service-resources-app-insights-keys.md). |
| Application Insights Application ID | Unique ID for bot analytics. Copy your Azure Insights Application ID Key to this field if you want to view analytics about your bot in the Dashboard. This value is optional. For more details on this field, see [Application Insights keys](bot-service-resources-app-insights-keys.md). |

To save your changes, select **Apply** at the bottom of the blade.

[!INCLUDE [app ID and password](includes/authentication/azure-bot-appid-password.md)]

## Additional Information

You can use [az bot update](/cli/azure/bot#az-bot-update) to update bot settings from the command line.

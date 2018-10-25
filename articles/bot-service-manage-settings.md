---
title: Configure bot settings | Microsoft Docs
description: Learn how to configure the various options for your bot using the Azure portal.
keywords: configure bot settings, Display Name, Icon, Application Insights, Settings blade  
author: v-royhar
ms.author: v-royhar
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: abs
ms.date: 12/13/2017
---
# Configure bot settings

The bot settings, such as Display name, Icon, and Application Insights, can be viewed and modified on its **Settings** blade.

![The bot settings blade](~/media/bot-service-portal-configure-settings/bot-settings-blade.png)

Below is the list of fields on the **Settings** blade:

| Field | Description |
| :---  | :---        |
| Icon | A custom icon to visually identify your bot in channels and as the icon for Skype, Cortana, and other services. This icon must be in PNG format, and no larger than 30K in size. This value can be changed at any time. |
| Display name | The name of your bot in channels and directories. This value can be changed at any time. 35 character limit. |
| Bot handle | Unique identifier for your bot. This value cannot be changed after creating your bot with the Bot Service. |
| Messaging endpoint | The endpoint to communicate with your bot. |
| Microsoft App ID | Unique identifier for your bot. This value cannot be changed. You can generate a new password by clicking on the **Manage** link. |
| Application Insights Instrumentation key | Unique key for bot telemetry. Copy your Azure Application Insights Key to this field if you want to receive bot telemetry for this bot. This value is optional. Bots created in the Azure Portal will have this key generated for them. For more details on this field, see [Application Insights keys](~/bot-service-resources-app-insights-keys.md). |
| Application Insights API key | Unique key for bot analytics. Copy your Azure Application Insights API Key to this field if you want to view analytics about your bot in the Dashboard. This value is optional. For more details on this field, see [Application Insights keys](~/bot-service-resources-app-insights-keys.md). |
| Application Insights Application ID | Unique key for bot analytics. Copy your Azure Insights Application ID Key to this field if you want to view analytics about your bot in the Dashboard. This value is optional. Bots created in the Azure Portal will have this key generated for them. For more details on this field, see [Application Insights keys](~/bot-service-resources-app-insights-keys.md). |

> [!NOTE]
> After you have changed settings for your bot, click the **Save** button at the top of the blade to save your new bot settings.

## Next steps
Now that you have learned how to configure settings for your bot service, learn about how to configure speech priming.
> [!div class="nextstepaction"]
> [Speech priming](bot-service-manage-speech-priming.md)
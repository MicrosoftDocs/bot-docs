---
title: Application Insights keys - Bot Service
description: Learn how to add telemetry to a bot. See how to create the keys that you need to view data that Azure Application Insights collects about your application.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 11/01/2021
---

# Application Insights keys

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

Azure **Application Insights** displays data about your application in a Microsoft Azure resource. To add telemetry to your bot, you need an Azure subscription and an Application Insights resource created for your bot. From this resource, you can obtain the three keys to configure your bot:

1. Instrumentation key
2. Application ID
3. API key

This topic will show you how to create these Application Insights keys.

> [!NOTE]
> During the bot creation or registration process, you were given the option to turn **Application Insights** *On* or *Off*. If you had turned it *On* then your bot already has all the necessary Application Insights keys it needs. However, if you had turned it *Off* then you can follow the instructions in this topic to help you manually create these keys.

## Instrumentation key

To get the Instrumentation key:

1. From the [Azure portal](https://portal.azure.com), under the Monitor section, create a new **Application Insights** resource (or use an existing one).

    :::image type="content" source="media/portal-app-insights-add-new.png" alt-text="Portal screen capture of Application Insights listing.":::

1. From the list of Application Insights resources, click the Application Insight resource you created.

1. Click **Overview**.

1. Expand the **Essentials** block and find the **Instrumentation Key**.

    :::image type="content" source="media/portal-app-insights-instrumentation-key-dropdown.png" alt-text="Portal screen capture of Overview":::

    :::image type="content" source="media/portal-app-insights-instrumentation-key.png" alt-text="Portal screen capture of the Instrumentation key":::

1. Copy the **Instrumentation Key** and paste it to the **Application Insights Instrumentation Key** field of your bot's settings.

## Application ID

To get the Application ID:

1. From your Application Insights resource, click **API Access**.

1. Copy the **Application ID** and paste it to the **Application Insights Application ID** field of your bot's settings.

    :::image type="content" source="media/portal-app-insights-appid.png" alt-text="Portal screen capture of the Application ID":::

## API key

To get the API key:

1. From the Application Insights resource, click **API Access**.
1. Click **Create API Key**.
1. Enter a short description, check the **Read telemetry** option, and click the **Generate key** button.

    :::image type="content" source="media/portal-app-insights-appid-apikey.png" alt-text="Portal screen capture of the Application ID and API Key":::

    > [!WARNING]
    > Copy this **API key** and save it because this key will never be shown to you again. If you lose this key, you have to create a new one.

1. Copy the API key to the **Application Insights API key** field of your bot's settings.

## Additional information

For more information on how to connect these fields into your bot's settings, see [Enable analytics](bot-service-manage-analytics.md#enable-analytics).

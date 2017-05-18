---
title: Application Insights keys | Microsoft Docs
description: Learn how to get Application Insights keys to add telemetry to a bot.
author: v-ducvo
ms.author: v-ducvo
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer: 
---

# Application Insights keys

Azure Application Insights displays data about your application in a Microsoft Azure resource. To add telemetry to your bot you need an Azure subscription and an Application Insights resource created for your bot. From this resource, you can obtain the three keys to configure your bot:

1. Instrumentation key
2. Application ID
3. API key

To configure these three keys, you must [register your bot](portal-register-bot.md). Then, locate these fields in your bot's settings page under the section labeled **Analytics**. The following screenshot shows the Analytics fields.

![Portal screen capture of bot settings](~/media/portal-app-insights-analytics.png)

## Instrumentation key

To get the Instrumentation key, do the following:
1. From the [portal.azure.com](http://portal.azure.com), under the Monitor section, create a new **Application Insights** resource (or use an existing one).
![Portal screen capture of Application Insights listing](~/media/portal-app-insights-add-new.png)

2. From the list of Application Insights resources, click the Application Insight resource you just created.

3. Click **Overview**.

4. Expand the **Essentials** block and find the **Instrumentation Key**. 
![Portal screen capture of the Instrumentation key](~/media/portal-app-insights-instrumentation-key.png)

5. Copy the **Instrumentation Key** and paste it to the **AppInsights Instrumentation Key** field of your bot's settings.

## Application ID

To get the Application ID, do the following:
1. From your Application Insights resource, click **API Access**.

2. Copy the **Application ID** and paste it to the **AppInsights Application ID** field of your bot's settings. 
![Portal screen capture of the Application ID](~/media/portal-app-insights-appid.png)

## API key

To get the API key, do the following:
1. From the Application Insights resource, click **API Access**.

2. Click **Create API Key**.

3. Enter a short description, check the **Read telementry** option, and click the **Generate key** button.
![Portal screen capture of the Application ID and API Key](~/media/portal-app-insights-appid-apikey.png)

   > [!WARNING]
   > Copy this **API key** and save it because this key will never be shown to you again. If you lose this key, you have to create a new one.

4. Copy the API key to the **AppInsights API key** field of your bot's settings.

## Additional resources
For more information on how to connect these fields into your bot's settings, see [Enable analytics](~/portal-analytics-overview.md).
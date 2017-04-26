---
title: Application Insights keys | Microsoft Docs
description: Learn how to get Application Insights keys to add telemetry to a bot.
author: v-ducvo
ms.author: v-ducvo
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 04/25/2017

---

# Application Insights keys

Azure Application Insights displays data about your application in a Microsoft Azure resource. To add telemetry to your bot you need an Azure subscription and an Application Insights resource created for your bot. From this resource, you can obtain three keys to configure your bot:

1. Instrumentation key
2. Application ID
3. API key

## Instrumentation key

To get the Instrumentation key, do the following:
1. From the [portal.azure.com](http://portal.azure.com), create a new **Application Insights** resource (or use an existing one).
2. From the list of Application Insights resource, click the Application Insight resource you just created.
3. Click **Overview**.
4. Expand the **Essentials** block and find the **Instrumentation Key**. See the screen capture below for reference.
5. Copy the **Instrumentation Key** and paste it to the **AppInsights Instrumentation Key** field of your bot's settings.

![Portal screen capture of the Instrumentation key.](~/media/portal-app-insights-instrumentation-key.png)

## Application ID

To get the Application ID, do the following:
1. From your Application Insights resource, click **API Access**.
2. Copy the **Application ID** and paste it to the **AppInsights Application ID** field of your bot's settings. See the screen capture in the next section for reference.

## API key

To get the API key, do the following:
1. From the Application Insights resource, click **API Access**.
2. Click **Create API Key**.
3. Enter a short description, check the **Read telementry** option, and click the **Generate key** button.

   > [!WARNING]
   > Copy this **API key** and save it because this key will never be shown to you again. If you lose this key, you have to create a new one.

4. Copy the API key to the **AppInsights API key** field of your bot's settings. See the screen capture below for reference.

![Portal screen capture of the Application ID and API Key.](~/media/portal-app-insights-appid-apikey.png)
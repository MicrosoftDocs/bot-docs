---
title: Configure bot settings - Bot Service
description: Learn how to configure the various options for your bot using the Azure portal.
keywords: configure bot settings, Display Name, Icon, Application Insights, Settings blade
author: v-royhar
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/13/2017
---
# Configure bot registration settings

The bot registration settings, such as display name, icon, and application insights, can be viewed and modified in the **Settings** panel.

![bot registration settings](media/bot-service-portal-configure-settings/bot-service-settings.png)

Below is the list of the **Settings** fields:

> [!div class="mx-tdBreakAll"]
> | Field <img width="400px"/>| Description |
> | :---  | :---        |
> | Icon | A custom icon to visually identify your bot in channels and as the icon for Cortana and other services. This icon must be in PNG format, and no larger than 30K in size. This value can be changed at any time. |
> | Display name | The name of your bot in channels and directories. This value can be changed at any time. 35 character limit. |
> | Bot handle | Unique identifier for your bot. This value cannot be changed after creating your bot with the Bot Service. |
> | Messaging endpoint | The endpoint to communicate with your bot. |
> | Microsoft App ID | Unique identifier for your bot. This value cannot be changed. You can generate a new password by clicking on the **Manage** link. |
> | Application Insights Instrumentation key | Unique key for bot telemetry. Copy your Azure Application Insights Key to this field if you want to receive bot telemetry for this bot. This value is optional. Bots created in the Azure Portal will have this key generated for them. For more details on this field, see [Application Insights keys](~/bot-service-resources-app-insights-keys.md). |
> | Application Insights API key | Unique key for bot analytics. Copy your Azure Application Insights API Key to this field if you want to view analytics about your bot in the Dashboard. This value is optional. For more details on this field, see [Application Insights keys](~/bot-service-resources-app-insights-keys.md). |
> | Application Insights Application ID | Unique key for bot analytics. Copy your Azure Insights Application ID Key to this field if you want to view analytics about your bot in the Dashboard. This value is optional. Bots created in the Azure Portal will have this key generated for them. For more details on this field, see [Application Insights keys](~/bot-service-resources-app-insights-keys.md). |

After you have changed settings for your bot, click the **Save** button at the top of the blade to save your new bot settings.

## Application ID and Password

The registration application ID and password are assigned to the bot's variables `MicrosoftAppID` and `MicrosoftAppPassword` in the files `appsettings.json` (.NET) and `.env` (Javascript).

> [!NOTE]
> The Bot Channels Registration has an application ID but because there is no app service associated with it, there is no password. You must generate one. To generate the password follow the steps described below.

### Get registration password

A registration application has an **application ID** (app ID) and a **password** associated with it.
The Azure Bot Service assigns a unique application ID to the application. You can obtain the password following the steps described below.

1. In your browser, navigate to the [Azure portal](https://ms.portal.azure.com).
1. In the resource list, click on the registration application name.
1. Im the right panel, in the *Bot Management* section, click  **Settings**. The registration application *Settings* page is displayed.
1. Click the **Manage** link by *Microsoft App ID*.

    ![bot registration settings password](media/azure-bot-quickstarts/bot-channels-registration-password.png)

1. In the *Certificates & secrets* displayed page, click the **New client secret** button.

    ![bot registration settings password](media/azure-bot-quickstarts/bot-channels-registration-app-secrets.png)

1. Add the description, select the expiration time, and click the **Add** button.

    ![bot registration settings password](media/azure-bot-quickstarts/bot-channels-registration-app-secrets-create.png)

    This will generate a new password for your bot. Copy this password and save it to a file. This is the only time you will see this password. If you do not have the full password saved, you will need to repeat the process to create a new password should you need it later.

## Additional Information

You can use [az bot update](https://docs.microsoft.com/cli/azure/bot?view=azure-cli-latest#az-bot-update) to update bot settings from the command line.

## Next steps

Now that you have learned how to configure settings for your bot service, learn about how to configure speech priming.
> [!div class="nextstepaction"]
> [Speech priming](bot-service-manage-speech-priming.md)

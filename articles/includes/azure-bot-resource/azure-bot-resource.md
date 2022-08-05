---
description: Procedure for creating an Azure Bot resource, in all identity-management flavors.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 07/22/2022
---

## Create the resource

Create the **Azure Bot** resource, which will allow you to register your bot with the Azure Bot Service.

[!INCLUDE [bot-resource-type-tip](../bot-resource-type-tip.md)]

1. Go to the [Azure portal](https://portal.azure.com/).
1. In the right pane, select **Create a resource**.
1. In the search box enter `bot`, then press **Enter**.
1. Select the **Azure Bot** card.

    :::image type="content" source="../../media/azure-manage-a-bot/azure-bot-resource.png" alt-text="Select Azure bot resource":::

1. Select **Create**.
1. Enter values in the required fields and review and update settings.

   1. Provide information under **Project details**. Select whether your bot will have global or local data residency. Currently, the local data residency feature is only available for resources in the "westeurope" region. For more information, see [Regionalization in Azure Bot Service](../../v4sdk/bot-builder-concept-regionalization.md).

      :::image type="content" source="../../media/azure-bot-resource/azure-bot-project-details.png" alt-text="The project details settings for an Azure Bot resource":::

   1. Provide information under **Microsoft App ID**. Select how your bot identity will be managed in Azure and whether to create a new identity or use an existing one.

      :::image type="content" source="../../media/azure-bot-resource/azure-bot-ms-app-id.png" alt-text="The Microsoft app ID settings for an Azure Bot resource":::

1. Select **Review + create**.
1. If the validation passes, select **Create**.
1. Once the deployment completes, select **Go to resource**. You should see the bot and related resources listed in the resource group you selected.
1. If you don't already have the Bot Framework SDK, select **Download from GitHub** to learn how to consume the packages for your preferred language.

    :::image type="content" source="../../media/azure-manage-a-bot/azure-bot-create-sdk.png" alt-text="Create bot in SDK":::

You're now ready to build your bot with the Bot Framework SDK.

> [!TIP]
> When Azure creates a new single-tenant or multi-tenant Azure Bot resource with a new app ID, it also generates a _password_.

[!INCLUDE [app ID and password](../authentication/azure-bot-appid-password.md)]

If you have an existing App Service resource (web app) for your bot and your bot is a _user-assigned managed identity_ application, you may need to update your bot's app service:

1. Go to the App Service blade for your bot's web app.
1. Under **Settings**, select **Identity**.
1. On the **Identity** blade, select the **User assigned** tab and **Add** (+).
1. On the **Add user assigned managed identity** blade:
    1. Select your subscription.
    1. For **User assigned managed identities**, select the managed identity for your bot. If the managed identity was auto-generated for you, it will have the same name as your bot.
    1. Select **Add** to use this identity for your bot.

        :::image type="content" source="../../media/how-to-create-single-tenant-bot/app-service-managed-identity.png" alt-text="The App Service Identity blade with the managed identity for the bot selected.":::

To get your bot's app or tenant ID:

1. Go to the Azure Bot resource blade for your bot.
1. Go to the bot's **Configuration** blade.
    From this blade, you can copy the bot's **Microsoft App ID** or **App Tenant ID**.

Single-tenant and multi-tenant bots have an app secret or password that you need for some operations.
Azure Bot Service hides your bot secret. However, the owner of the bot's App Service resource can generate a new password:

1. Go to the Azure Bot resource blade for your bot.
1. Go to the bot's **Configuration** blade.
1. Select **Manage**, next to **Microsoft App ID**, to go to the **Certificates + secrets** blade for the app service.
1. Follow the instructions on the blade to create a new client secret and record the value in a safe place.

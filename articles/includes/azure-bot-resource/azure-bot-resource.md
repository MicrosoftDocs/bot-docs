---
description: Procedure for creating an Azure Bot resource, in all identity-management flavors.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 02/11/2022
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
1. Enter values in the required fields.
    Choose which type of app to create and whether to use existing or create new identity information.

    :::image type="content" source="../../media/azure-manage-a-bot/create-new-user-assigned-managed-identity.png" alt-text="Create a user-assigned managed identity Azure Bot resource with a new app ID.":::

1. Select **Review + create**.
1. If the validation passes, select **Create**.
1. Select **Go to resource group**. You should see the bot and related resources listed in the resource group you selected.
1. Select **Get the SDK from GitHub** to build your bot with the Bot Framework SDK.

    :::image type="content" source="../../media/azure-manage-a-bot/azure-bot-create-sdk.png" alt-text="Create bot in SDK":::

<a id="azure-key-vault"></a>

> [!TIP]
> When Azure creates a new single-tenant or multi-tenant Azure Bot resource with a new app ID, it also generates a _password_ and stores the password in Azure Key Vault.
>
> Key Vault is a service that provides centralized secrets management, with full control over access policies and audit history. For more information, see [Use Key Vault references for App Service and Azure Functions](/azure/app-service/app-service-key-vault-references). You're charged a small fee for using the service. For more information, see [Key Vault pricing](https://azure.microsoft.com/pricing/details/key-vault/).

[!INCLUDE [app ID and password](../authentication/azure-bot-appid-password.md)]

If your bot is a _user-assigned managed identity_ application, update your bot's web app:

1. Go to the app service page for your bot's web app.
1. In the navigation pane under **Settings**, select **Identity**.
1. On the **Identity** pane, select the **User assigned** tab and **Add** (+).
1. On the **Add user assigned managed identity** pane:
    1. Select your subscription.
    1. For **User assigned managed identities**, select the managed identity for your bot. If the managed identity was auto-generated for you, it will have the same name as your bot.
    1. Select **Add** to use this identity for your bot.

        :::image type="content" source="../../media/how-to-create-single-tenant-bot/app-service-managed-identity.png" alt-text="The App Service Identity page with the managed identity for the bot selected.":::

To get your bot's app or tenant ID:

1. Go to the Azure Bot resource page for your bot.
1. Go to the bot's **Configuration** pane.
    From here you can copy the bot's **Microsoft App ID** or **App Tenant ID**.

> [!TIP]
>When Azure creates a single-tenant or multi-tenant bot, it stores the app password in Azure Key Vault.

To get the _bot's app password_ from Azure Key Vault, see:

- [About Azure Key Vault](/azure/key-vault/general/overview)
- [Use Key Vault references for App Service and Azure Functions](/azure/app-service/app-service-key-vault-references).
- [Assign a Key Vault access policy using the Azure portal](/azure/key-vault/general/assign-access-policy-portal)
- [Quickstart: Set and retrieve a secret from Azure Key Vault using the Azure portal](/azure/key-vault/secrets/quick-create-portal#retrieve-a-secret-from-key-vault)

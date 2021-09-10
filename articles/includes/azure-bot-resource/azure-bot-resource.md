<!-- Azure Bot Resource -->

## Create the resource

Create the **Azure Bot** resource, which will allow you to register your bot with the Azure Bot Service.

> [!WARNING]
> **Web App Bot** and **Bot Channels Registration** are deprecated but existing resources will continue to work. You should use **Azure Bot**, instead.

1. Go to the [Azure portal](https://portal.azure.com/).
1. In the right pane, select **Create a resource**.
1. In the search box enter *bot*, then press **Enter**.
1. Select the **Azure Bot** card.

    :::image type="content" source="../../media/azure-manage-a-bot/azure-bot-resource.png" alt-text="Select Azure bot resource":::

1. Select **Create**.

    :::image type="content" source="../../media/azure-manage-a-bot/azure-bot-resource-create.png" alt-text="Create Azure bot resource":::

1. Enter the required values. The following figure shows *Create new Microsoft App ID* selected.

    :::image type="content" source="../../media/azure-manage-a-bot/azure-bot-resource-create-values.png" alt-text="Create Azure bot resource values":::

    You can also select *Use existing app registration* and enter your existing **app ID** and **password**.

    :::image type="content" source="../../media/azure-manage-a-bot/azure-bot-resource-existing-values.png" alt-text="Create Azure bot resource existing values":::

1. Select **Review + create**.
1. If the validation passes, select **Create**.
1. Select **Go to resource group**. You should see the bot and the related **Azure Key Vault** resources listed in the resource group you selected.

    > [!TIP]
    > The app secret (password) is stored in the the key vault and there is one key vault per resource group. Using key vault is recommended instead of copying and storing sensitive data.  

1. Select **Get the SDK from Github** to build your bot with the Bot Framework SDK.

    :::image type="content" source="../../media/azure-manage-a-bot/azure-bot-create-sdk.png" alt-text="Create bot in SDK":::

### Azure Key Vault

When Azure creates the Azure Bot resource, it also generates an **app ID** and a **password** and stores the password in Azure Key Vault.

Key Vault is a service that provides centralized secrets management, with full control over access policies and audit history. For more information, see [Use Key Vault references for App Service and Azure Functions](/azure/app-service/app-service-key-vault-references). You're charged a small fee for using the service. For more information, see [Key Vault pricing](https://azure.microsoft.com/pricing/details/key-vault/).

[!INCLUDE [app ID and password](../authentication/azure-bot-appid-password.md)]

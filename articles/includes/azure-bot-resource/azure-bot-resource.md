<!-- Azure Bot Resource -->

## Create an Azure Bot resource

The Azure Bot resource enables you to register your Bot Framework Composer or SDK bot in Azure Bot Service. You can build, connect, and manage bots to interact with your users wherever they are, from your app or website to Teams, Messenger and many other channels.


[!INCLUDE [azure web app channel reg deprecation](../authentication/azure-webapp-chnlreg-deprecation.md)]


1. Go to the [Azure portal](https://portal.azure.com/).
1. In the right pane, select **Create a resource**.
1. In the search box enter *bot*, then press **Enter**.
1. Select the **Azure Bot** card. 

    :::image type="content" source="~/media/azure-manage-a-bot/azure-bot-resource.png" alt-text="Select Azure bot resource":::

1. Select **Create**.

    :::image type="content" source="~/media/azure-manage-a-bot/azure-bot-resource-create.png" alt-text="Create Azure bot resource":::

1. Enter the required values. The following figure shows *Create new Microsoft App ID* selected.

    :::image type="content" source="~/media/azure-manage-a-bot/azure-bot-resource-create-values.png" alt-text="Create Azure bot resource values":::

    You can also select *Use existing app registration* and enter your existing **app Id** and **password**.

    :::image type="content" source="~/media/azure-manage-a-bot/azure-bot-resource-existing-values.png" alt-text="Create Azure bot resource existing values":::

1. Select **Review + create**. 
1. If the validation passes, select **Create**. 
1. Select **Go to resource group**. You should see the bot and the related **Azure Key Vault** resources listed in the resource group you selected. 

    > [!TIP]
    > The app secret (password) is stored in the the key vault and there is one key vault per resource group. Using key vault is recommended instead of copying and storing sensitive data.  

1. Select **Open in Composer**. This is the recommended path, if you are new to bot development or if you are building a brand new bot. Follow the steps described in the [Create a bot in Composer](/composer/quickstart-create-bot-with-azure) article. 

    :::image type="content" source="~/media/azure-manage-a-bot/azure-bot-create-composer.png" alt-text="Create bot in Composer":::

    Optionally, select **Get the SDK from Github** to build your bot with the Bot Framework SDK that helps you create everything from a modest chatbot to world class, enterprise solutions.

    :::image type="content" source="~/media/azure-manage-a-bot/azure-bot-create-sdk.png" alt-text="Create bot in SDK":::

### About Azure Key Vault

**Azure Key Vault** is a service that provides centralized secrets management, with full control over access policies and audit history. For more information, see [Use Key Vault references for App Service and Azure Functions](/azure/app-service/app-service-key-vault-references). Note that you will be charged a small fee for using the service, For more information, see [Key Vault pricing](https://azure.microsoft.com/pricing/details/key-vault/).

[!INCLUDE [app id and password](../authentication/azure-bot-appid-password.md)]
---
title: Create a bot resource with Azure Bot Service - Bot Service
description: Learn how to create a bot resource with Azure Bot Service, an integrated, dedicated bot development environment.
keywords: Quickstart, create bot resource, bot service, web app bot
author: mmiele
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 06/23/2021
---

# Create an Azure Bot resource

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

The **Azure Bot** resource allows you to register your bot with the Azure Bot Service. You can build, connect, and manage bots to interact with your users wherever they are, from your app or website to Teams, Messenger and many other channels. To learn how to create a bot, see the quick start article applicable to your programming language.

[!INCLUDE [composer sdk tip](../includes/azure-bot-resource/composer-tip.md)]

## Prerequisites

- [Azure](https://portal.azure.com) account

[!INCLUDE [azure bot resource](../includes/azure-bot-resource/azure-bot-resource.md)]

<!-- Replaced by the previous include 
### Create a new bot service

1. Log in to the [Azure portal](https://portal.azure.com/).
1. Click **Create a resource** link found in the upper left-hand corner of the Azure portal.
1. In the search box enter *bot* and in the drop-down list select **Web App Bot**.

    ![web app bot](../media/azure-bot-quickstarts/web-app-bot.png)

1. In the **Web App Bot** page, click the **Create** button.

    ![web app bot create](../media/azure-bot-quickstarts/web-app-bot-create.png)

1. In the **Web App Bot** form, provide the requested information about your bot as specified in the table below the image.

    ![Create Web App Bot blade](../media/azure-bot-quickstarts/sdk-create-bot-service-blade.png)

    | Setting | Suggested value | Description |
    | ---- | ---- | ---- |
    | **Bot name** | Your bot's display name | The display name for the bot that appears in channels and directories. This name can be changed at anytime. |
    | **Subscription** | Your subscription | Select the Azure subscription you want to use. |
    | **Resource Group** | myResourceGroup | You can create a new [resource group](/azure/azure-resource-manager/resource-group-overview#resource-groups) or choose from an existing one. |
    | **Location** | The default location | Select the geographic location for your resource group. Your location choice can be any location listed, though it's often best to choose a location closest to your customer. The location cannot be changed once the bot is created. |
    | **Pricing tier** | F0 | Select a pricing tier. You may update the pricing tier at any time. For more information, see [Bot Service pricing](https://azure.microsoft.com/pricing/details/bot-service/). |
    | **App name** | A unique name | The unique URL name of the bot. For example, if you name your bot *myawesomebot*, then your bot's URL will be `http://myawesomebot.azurewebsites.net`. The name must use alphanumeric and underscore characters only. There is a 35 character limit to this field. The App name cannot be changed once the bot is created. |
    | **Bot template** | Echo bot | Choose **SDK v4**. Select either C# or Node.js for this quickstart, then click **Select**.
    | **App service plan/Location** | Your app service plan  | Select an [app service plan](https://azure.microsoft.com/pricing/details/app-service/plans/) location. Your location choice can be any location listed, though it's often best to choose the same location as the bot service. |
    | **LUIS Accounts** _Only available for Basic Bot template_ | LUIS Azure Resource Name | After [migrating LUIS Resources over to an Azure Resource](/azure/cognitive-services/luis/luis-migration-authoring), input the Azure Resource name to associate this LUIS Application with that Azure Resource.
    | **Application Insights** | On | Decide if you want to turn [Application Insights](/bot-framework/bot-service-manage-analytics) **On** or **Off**. If you select **On**, you must also specify a regional location. Your location choice can be any location listed, though it's often best to choose the same location as the bot service. |
    | **Microsoft App ID and password** | Auto create App ID and password | Use this option if you need to manually enter a Microsoft App ID and password. Otherwise, a new Microsoft App ID and password will be created for you in the bot creation process. When creating an app registration manually for the Bot Service, please ensure that the supported account types is set to **Accounts in any organizational directory** or **Accounts in any organizational directory and personal Microsoft accounts (e.g. Outlook.com, Xbox, etc.)** |

1. Click **Create** to create the service and deploy the bot to the cloud. This process may take several minutes.

    Confirm that the bot has been deployed by checking the **Notifications**. The notifications will change from **Deployment in progress...** to **Deployment succeeded**. Click **Go to resource** link to open the bot's resources page.

Now that your bot is created, test it in Web Chat.

## Test the bot

In the **Settings** section, click **Test in Web Chat**. Azure Bot Service will load the Web Chat control and connect to your bot.

:::image type="content" source="../media/azure-bot-quickstarts/web-app-bot-test.png" alt-text="The the bot on Azure in Web Chat.":::

Enter a message and your bot should respond.

## Download code

You can download the code to work on it locally.

1. Go to the **Overview** blade.
1. Select **Download bot source code** in the top toolbar or at the bottom of the pane.
1. Follow the prompts to download the code, and then unzip the folder.

    [!INCLUDE [download keys snippet](../includes/snippet-abs-key-download.md)]

:::image type="content" source="../media/azure-bot-quickstarts/web-app-bot-download.png" alt-text="Links on the Overview pane to download the bot source code.":::

-->

<!---
## Additional information

If you're new to bot development, we recommend using [Bot Framework Composer](/composer/).

### Manual app registration

You might need a manual registration when:

- You are unable to make the registrations in your organization and need another party to create the App ID for the bot you're building.
- You need to manually create your own app ID (and password).

See [FAQ - App Registration](../bot-service-resources-faq-azure.yml#how-do-i-create-my-own-app-registration).

--->

## Next steps

> [!div class="nextstepaction"]
> [Deploy your bot](../bot-builder-deploy-az-cli.md)

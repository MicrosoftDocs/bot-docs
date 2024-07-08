---
title: Connect a Bot Framework bot to Facebook
description: Learn how to configure bots to connect to Facebook Messenger and Facebook Workplace and communicate with users via Facebook.
keywords: Facebook Messenger, bot channel, Facebook App, Facebook bot
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: jameslew
ms.service: bot-service
ms.topic: how-to
ms.date: 01/31/2023
ms.custom:
  - evergreen
---

# Connect a bot to Facebook

You can configure your bot to communicate with people through Facebook Messenger or Facebook Workplace. This article describes how to create a Facebook app using the Meta for Developers site, connect your bot to your Facebook app in Azure, and test your bot on Facebook.

This article shows how to add the Facebook channel to your bot via Azure portal. For information on how to use a custom channel adapter, see [Additional information](#additional-information).

## Prerequisites

- An Azure subscription. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A bot published to Azure that you want to connect to Facebook.
- A Facebook for Developers account. If you don't have an account, you can create one at [developers.facebook.com](https://developers.facebook.com).
- A Facebook page from which users will access your bot. If you don't have one yet, [Create a new Page](https://www.facebook.com/help/104002523024878).
- To use Facebook Workplace with your bot, you must create a Workplace account and a custom integration to connect the bot to.

## Create a Facebook app

### [Facebook Messenger](#tab/messenger)

Users will access your bot from a Facebook Page. To connect the bot, you'll enable Facebook Messenger on the Facebook Page and then connect the bot to the Page.

### Create your app

1. Sign in to your [Meta for Developers](https://developers.facebook.com) account.
1. Go to [Create a new Facebook App](https://developers.facebook.com/apps/create).
1. On the **Select an app type** page, select **Business** and then **Next**.
1. On the **Provide basic information** page, enter a name for your app and select **Create app**.
    - If prompted, enter your password and select **Submit** to create your app.
    - After your app is created, the site goes to a page for your app.
1. Expand **Settings** and select  **Basic**.
    1. Copy and save the **App ID** and **App Secret**.
1. Now under **Settings**, select **Advanced**.
    1. In the resulting pane, scroll down to the **Security** settings, and enable **Allow API Access to app settings**.
    1. Select **Save Changes**.

### Enable Messenger

1. Select **Dashboard**.
1. In the resulting pane, scroll down to the **Messenger** tile and select **Set Up**.
1. The site adds Messenger settings to your app and displays the settings page.

### Add pages and generate tokens

1. Under **Messenger**, select **Settings**.
1. Scroll down to **Access Tokens** and select **Add or Remove Pages**.
    1. When prompted for the identity to associate with Messenger, either continue with your current account or sign in to another.
    1. When prompted for the Pages you want to use with your app, select the pages and then select **Next**.
    1. If prompted to submit the request for Login Review, review the information and select **Done**.
    1. On success, the site displays a success page. Select **OK** to continue.

1. The Page you added now appears in the **Pages** list.

    Copy and save the Page ID for later.

1. Select **Generate token** for the Page.
    1. The site displays security information and gives you a chance to copy the token.
    1. Read and acknowledge the warning.
    1. Copy the token and select **Done**.

You now have your app information and a token for the next step.
Leave the Facebook setting steps pending. You'll update them later.

### Configure your bot in Azure

To let your bot send messages and other events to Facebook Messenger, enable webhooks integration.

1. In a new browser window, go to the [Azure portal](https://portal.azure.com/).
1. Open the Azure Bot resource blade for your bot.
1. Open **Channels** and select **Facebook**.
1. In **Configure Facebook Channel**, enter the Facebook information you copied in the previous steps.
    1. Enter your **Facebook App ID** and **Facebook App Secret**.
    1. Enter your **Page ID** and **Access Token**.
    1. Copy the generated _callback URL_ and _verify token_ values.
    1. Select **Add**.

> [!TIP]
>
> - If you don't have a copy of your information from the previous steps, you can retrieve it for the Meta for Developers site.
> - If you need to, create a new web token for the page. For instructions, see [Add pages and generate tokens](#add-pages-and-generate-tokens).

### Enable webhooks

Go back to the Facebook settings to finish up the configuration process.

1. On the Meta for Developers site, go back to the Messenger settings page for your app.
1. In the resulting pane, scroll down to the **Webhooks** section and select **Add Callback URL**.
1. On the **Edit Callback URL** page:
    1. Enter the callback URL and verify token values that you copied from the Azure portal.
    1. Select **Verify and save**.
1. The Page you added now appears in the **Pages** list under **Webhooks**.
1. Select **Add subscriptions** for the Page.
    1. On the **Edit page subscriptions** page, select the following subscription fields:
        - **messages**
        - **messaging_postbacks**
        - **messaging_options**
        - **message_deliveries**
    1. Select **Save**.
1. The site displays the added subscription fields next to the Page for your bot.

### [Facebook Workplace](#tab/workplace)

> [!NOTE]
> On December 16, 2019, Workplace by Facebook changed its security model for custom integrations. Facebook is not accepting new app integrations for Facebook Workplace.

---

## Make your app public

Until an app is published, it's in [Development Mode](https://developers.facebook.com/docs/apps/managing-development-cycle). Plugin and API functionality will only work for admins, developers, and testers.

Only the creator (the Facebook Dev account that created the page and bot) can get a bot response. Normal Facebook users can't see the page or the bot. Give dev or test roles to target users, so they can also chat with bot.

Users to be added to tester roles must first register on the Meta for Developers site. The tester role is not available to Facebook users that don't have a Meta for Developers account. For more information about app roles and test users, see the [Meta for Developers developer documentation](https://developers.facebook.com/docs/).

### Submit for review

Facebook requires a Privacy Policy URL and Terms of Service URL on its basic app settings page. The [Code of Conduct](https://investor.fb.com/corporate-governance/code-of-conduct/default.aspx) page contains third party resource links to help create a privacy policy. The [Terms of Use](https://www.facebook.com/terms.php) page contains sample terms to help create an appropriate Terms of Service document.

After the bot is finished, Facebook has its own [review process](https://developers.facebook.com/docs/messenger-platform/app-review) for apps that are published to Messenger. The bot will be tested to ensure it's compliant with Facebook's [Platform Policies](https://developers.facebook.com/docs/messenger-platform/policy-overview).

### Make the app public and publish the Page

After the review is successful, in the App Dashboard under App Review, set the app to Public. Ensure that the Facebook Page associated with this bot is published. Status appears in Pages settings.

### Set the API version

If you receive a notification from Facebook about deprecation of a certain version of the Graph API:

1. Go to [Meta for Developers](https://developers.facebook.com).
1. Go to the app you created for your bot.
1. Under **Settings**, select **Advanced**.
1. Select **Upgrade API version**, then switch **Upgrade All Calls** to version 4.0.

Test the connection by following the steps described in the [Test your bot in Facebook](#test-your-bot-in-facebook) section.

## Test your bot in Facebook

You can now test whether your bot is connected to Facebook correctly by sending a message via the Facebook Page you associated with your new Facebook app.

1. Go Facebook and switch to the profile for your page.
1. Select more actions (**&hellip;**), then **Add Action Button**.
    1. In the **Customize your action button** dialog, select **Try it** and follow the instructions.
    1. On the **Action Button** page under **Get people to contact you**, select **Send Message**.
    1. Select **Next**, complete the dialog and save your changes.
1. Switch back to your personal profile.
1. Go to your page, and select **Message** to test the connection to your bot.

You can also test this feature using the [sample bot for the Facebook adapter](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/61.facebook-adapter) by populating the **appsettings.json** file with the same values described in the previous steps.

## Additional information

See the Bot Framework C# [Facebook-events sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/23.facebook-events) for a sample bot that supports Facebook Messenger communication.

For Facebook Messenger documentation, see:

- [Messenger platform documentation](https://developers.facebook.com/docs/messenger-platform).
- [Pre-launch guidelines](https://developers.facebook.com/docs/messenger-platform/product-overview/launch#app_public)
- [Quick start](https://developers.facebook.com/docs/messenger-platform/guides/quick-start)
- [Setup guide](https://developers.facebook.com/docs/messenger-platform/guides/setup)

For Facebook Workplace documentation, see:

- [Workplace Help Center](https://workplace.facebook.com/help/work/)
- [Workplace Developer Documentation](https://developers.facebook.com/docs/workplace)

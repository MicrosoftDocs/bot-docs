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
ms.date: 03/22/2022
---

# Connect a bot to Facebook

You can configure your bot to communicate with people through Facebook Messenger or Facebook Workplace. This article describes how to create a Facebook app using the Meta for Developers site, connect your bot to your Facebook app in Azure, and test your bot on Facebook.

This article shows how to add the Facebook channel to your bot via Azure portal. For information on how to use a custom channel adapter, see [Additional information](#additional-information).

## Prerequisites

- An Azure subscription. If you don't already have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A bot published to Azure that you want to connect to Facebook.
- A Facebook for Developers account. If you do not have an account, you can create one at [developers.facebook.com](https://developers.facebook.com).
- To use Facebook Workplace with your bot, you must create a Workplace account and a custom integration to connect the bot to.

## Create a Facebook app

### [Facebook Messenger](#tab/messenger)

Users will access your bot from a Facebook Page. To connect the bot, you'll enable Facebook Messenger on the Facebook Page and then connect the bot to the Page.

### Get the Page ID

1. Sign in to your [Meta for Developers](https://developers.facebook.com) account.
1. Go to the Page on which users will access your bot. If you don't have one yet, go to [Pages](https://developers.facebook.com/docs/pages) and select **Create new Page**.
1. Open the Page's **About** page, then copy and save the **Page ID**.

### Create your app

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
1. The site adds Messenger settings to your app and display the settings page.

### Add pages and generate tokens

1. Scroll down to the **Access Tokens** section and select **Add or Remove Pages**.
    1. When prompted for the identity to associate with Messenger, either continue with your current account or sign in to another.
    1. When prompted for the Pages you want to use with your app, select the pages and then select **Next**.
    1. If prompted to submit the request for Login Review, review the information and select **Done**.
    1. On success, the site displays a success pages. Select **OK** to continue.
1. The Page you added now appears in the **Pages** list.
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

[JS Workplace Adapter]: https://www.npmjs.com/package/botbuilder-adapter-facebook
[CS Workplace Adapter]: https://github.com/microsoft/botbuilder-dotnet/tree/main/libraries/Adapters/Microsoft.Bot.Builder.Adapters.Facebook

> [!NOTE]
> On December 16, 2019, Workplace by Facebook changed its security model for custom integrations. Prior integrations built using Microsoft Bot Framework v4 need to be updated to use the Bot Framework Facebook adapters per the instructions below prior to February 28, 2020.
>
> Facebook will only consider integrations with limited access to Workplace data (low sensitivity permissions) eligible for continued use until December 31, 2020 if such integrations have completed and passed Security RFI and if the developer reaches out before January 15, 2020 via [Direct Support](https://my.workplace.com/work/admin/direct_support) to request continued use of the app.
>
> Custom channel adapters are available for Facebook in [JavaScript/Node.js][JS Workplace Adapter] and [C#/.NET][CS Workplace Adapter].

Facebook Workplace is a business-oriented version of Facebook, which allows employees to easily connect and collaborate.

The following instructions describe how to create a custom Facebook Workplace integration.
You'll also need to modify your bot to use a custom channel adapter for Facebook Workplace.

### Create a Workplace Premium account

If you don't yet have an account, create one.

1. Submit an application to [Workplace](https://www.facebook.com/workplace) on behalf of your company.
1. Once your application has been approved, you will receive an email inviting you to join. The response may take a while.
1. From the e-mail invitation, select **Get Started**.
1. Enter your profile information.
    > [!TIP]
    > Set yourself as the system administrator. Remember that only system administrators can create custom integrations.
1. Select **Preview Profile** and verify the information is correct.
1. Access **Free Trial**.
1. Create a **password**.
1. Select **Invite Coworkers** to invite employees to sign-in. The employees you invited will become members as soon as they sign. They will go through a similar sign-in process as described in these steps.

### Create a custom integration

Create a [custom integration](https://developers.facebook.com/docs/workplace/custom-integrations-new) for your Workplace.
Once complete, the site creates an app with defined permissions and a page of type `Bot` (visible only within your Workplace community).

1. In the **Admin Panel**, open the **Integrations** tab.
1. Select **Create your own custom App**.

    :::image type="content" source="media/channels/fb-integration.png" alt-text="Workplace integration":::

1. Choose a display name and a profile picture for the app. Such information will be shared with the page of type `Bot`.
1. Set the **Allow API Access to App Settings** to "Yes".
1. Copy and safely store the App ID, App Secret and App Token that's shown to you.

    :::image type="content" source="media/channels/fb-keys.png" alt-text="Workplace keys":::

1. Now you have finished creating a custom integration. You can find the page of type `Bot` in your Workplace community, as shown below.

    :::image type="content" source="media/channels/fb-page.png" alt-text="Workplace page":::

### Add the Facebook adapter to your bot project

1. Modify your bot project to use a custom channel adapter for Facebook. The custom channel adapter for Facebook&mdash;and information on how to add it to your bot&mdash;is available for [JavaScript/Node.js][JS Workplace Adapter] or [C#/.NET][CS Workplace Adapter].
1. To your bot's configuration file, add the **Facebook App ID**, **Facebook App Secret** and **Page Access Token** values that you copied from Facebook Workplace previously. Instead of a traditional page ID, use the numbers following the integrations name on its **About** page.

---

## Make your app public

### Submit for review

Facebook requires a Privacy Policy URL and Terms of Service URL on its basic app settings page. The [Code of Conduct](https://investor.fb.com/corporate-governance/code-of-conduct/default.aspx) page contains third party resource links to help create a privacy policy. The [Terms of Use](https://www.facebook.com/terms.php) page contains sample terms to help create an appropriate Terms of Service document.

After the bot is finished, Facebook has its own [review process](https://developers.facebook.com/docs/messenger-platform/app-review) for apps that are published to Messenger. The bot will be tested to ensure it's compliant with Facebook's [Platform Policies](https://developers.facebook.com/docs/messenger-platform/policy-overview).

### Make the app public and publish the Page

> [!TIP]
> Until an app is published, it's in [Development Mode](https://developers.facebook.com/docs/apps/managing-development-cycle). Plugin and API functionality will only work for admins, developers, and testers.

After the review is successful, in the App Dashboard under App Review, set the app to Public. Ensure that the Facebook Page associated with this bot is published. Status appears in Pages settings.

### Set the API version

If you receive a notification from Facebook about deprecation of a certain version of the Graph API:

1. Go to [Meta for Developers](https://developers.facebook.com).
1. Go to the app your created for your bot.
1. Under **Settings**, select **Advanced**.
1. Select **Upgrade API version**, then switch **Upgrade All Calls** to version 4.0.

Test the connection by following the steps described in the [Test your bot in Facebook](#test-your-bot-in-facebook) section.

## Test your bot in Facebook

You can now test whether your bot is connected to Facebook correctly by sending a message via the Facebook Page you associated with your new Facebook app.

1. Go to your Facebook Page.
1. Select **Add a button**.
1. On the **Edit action button** page, select **Send message**.
    1. On the **Frequently asked questions** page, review the default question and answer, then select **Save**.
    1. The site replaces the previous **Edit action button** with a new **Edit Send message** button.
1. Select **Edit Send message** then select **Test button**.
    1. The site displays your Page with the sample question as a suggested action.
    1. Enter any message you want to test the connection to your bot.

You can also test this feature using the [sample bot for the Facebook adapter](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/61.facebook-adapter) by populating the **appsettings.json** file with the same values described in the steps above.

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

### Connect a bot to Facebook using the Facebook adapter

The custom channel adapter for Facebook is available for [JavaScript/Node.js][JS Workplace Adapter] or [C#/.NET][CS Workplace Adapter]. The README describes how to add it to your bot.

- To connect to Facebook Workplace, your bot must use the custom channel adapter.
- To connect to Facebook Messenger, your bot can use the Azure channel or the custom channel adapter.

To complete configuration of your Facebook app for a bot that uses the custom channel adapter:

1. Within the dashboard for your app, select **Messenger** and then select **Settings**.
1. In the **Webhooks** section, select **Add Callback URL**.
1. In the **Callback URL** text box enter the Facebook endpoint for your bot. For example, `https://yourboturl.com/api/facebook`.
1. In the **Verify Token** text box enter the verify token you created earlier and used in your bot application's **appsettings.json** file.
1. Make sure your bot is running, and then select **Verify and Save** to verify your callback URL.
1. Once your callback URL has been verified, select **Add Subscriptions** that is now shown. In the pop-up window, choose the following subscriptions and select **Save**.

    - **messages**
    - **messaging_postbacks**
    - **messaging_optins**
    - **messaging_deliveries**

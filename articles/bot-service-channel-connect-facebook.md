---
title: Connect a bot to Facebook Messenger - Bot Service
description: Learn how to connect a bot to Facebook Messenger and Facebook Workplace. Connect a bot to Facebook using the Facebook adapter.
keywords: Facebook Messenger, bot channel, Facebook App, App ID, App Secret, Facebook bot, credentials
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: mainguy
ms.service: bot-service
ms.topic: how-to
ms.date: 11/01/2021
---

# Connect a bot to Facebook

Your bot can be connected to both Facebook Messenger and Facebook Workplace, so that it can communicate with users on both platforms. The following instructions show how to connect a bot to these two channels.

Select one of the following ways to connect your bot to Facebook:

- [Connect a bot to Facebook Messenger](#connect-a-bot-to-facebook-messenger). Shows how to connect a bot to Facebook Messenger.
- [Connect a bot to Facebook Workplace](#connect-a-bot-to-facebook-workplace). Shows how to connect a bot to Facebook Workplace.
- [Connect a bot to Facebook using the Facebook adapter](#connect-a-bot-to-facebook-using-the-facebook-adapter). Shows how to connect a bot to Facebook using the adapter. Note that the Facebook adapter is only available for C#/.NET and JavaScript/Node.js bots.

> [!NOTE]
> The Facebook UI may appear slightly different depending on which version you are using.

## Connect a bot to Facebook Messenger

To learn more about developing for Facebook Messenger, see the [Messenger platform documentation](https://developers.facebook.com/docs/messenger-platform). You may wish to review Facebook's [pre-launch guidelines](https://developers.facebook.com/docs/messenger-platform/product-overview/launch#app_public), [quick start](https://developers.facebook.com/docs/messenger-platform/guides/quick-start), and [setup guide](https://developers.facebook.com/docs/messenger-platform/guides/setup).

To configure a bot to communicate using Facebook Messenger, enable Facebook Messenger on a Facebook page and then connect the bot.

### Copy the Page ID

The bot is accessed through a Facebook Page.

1. [Create a new Facebook Page](https://www.facebook.com/bookmarks/pages) or go to an existing Page.

1. Open the Facebook Page's **About** page and then copy and save the **Page ID**.

### Create a Facebook app

1. In your browser, navigate to [Create a new Facebook App](https://developers.facebook.com/quickstarts/?platform=web).
1. Enter the name of your app and select **Create New Facebook App ID**.

    ![Create App](media/channels/fb-create-messenger-bot-app.png)

1. In the displayed dialog, enter your email address and select **Create App ID**.

    ![Create an App ID](media/channels/fb-create-messenger-bot-app-id.png)

1. Go through the wizard steps.

1. Enter the required check information, then select **Skip Quick Start** in the upper right.

1. In the left pane of the next displayed window, expand **Settings** and select **Basic**.

1. In the right pane, copy and save the **App ID** and **App Secret**.

    ![Copy App ID and App Secret](media/channels/fb-messenger-bot-get-appid-secret.png)

1. In the left pane, under **Settings**, select **Advanced**.

1. In the right pane, switch the **Allow API Access to App Settings** slider to **Yes**.

    ![Allow API Access to App Settings](media/channels/fb-messenger-bot-api-settings.png)

1. In the page bottom right, select **Save Changes**.

### Enable Messenger

1. In the left pane, select **Dashboard**.
1. In the right pane, scroll down to the **Messenger** box and select **Set Up**. The Messenger entry is displayed under the **PRODUCTS** section in the left pane.

    ![Enable Messenger](media/channels/fb-messenger-bot-enable-messenger.png)

### Add pages and generate tokens

1. In the left pane, under the Messenger entry, select **Settings**.

1. In the right pane, scroll down to **Access Tokens** and select **Add or Remove Pages**.

    ![Interface for adding or removing a page](media/channels/fb-messenger-bot-add-or-remove-pages.png)

1. From the list that comes up in the next window, choose the pages you want to use with the app.

1. Select **Done**.

1. To generate a token for this page, select **Generate Token**.

### Enable webhooks

In order to send messages and other events from your bot to Facebook Messenger, you must enable webhooks integration. Leave the Facebook setting steps pending. You'll update them later.

1. In your browser open a new window and navigate to the [Azure portal](https://portal.azure.com/).

1. In the Resource list, select on the bot resource registration and in the related blade select **Channels**.

1. In the right pane, select the **Facebook** icon.

1. In the wizard, enter the Facebook information stored in the previous steps. If the information is correct, at the bottom of the wizard you should see the _callback URL_ and the _verify token_. Copy and store them. Note that for regional bots, there's a geo prefix in the _callback URL_; you'll need to update the _callback URL_ when you switch your bot from global to regional.  

    ![Interface to configure the Facebook Messenger channel](media/channels/fb-messenger-bot-config-channel.PNG)

1. Select **Save**.

1. Go back to the Facebook settings to finish up the configuration process.

1. Enter the callback URL and verify token values that you collected from the Azure portal.

1. In the Webhooks configuration section, enable the following subscriptions:
  **message\_deliveries**, **messages**, **messaging\_options**, and **messaging\_postbacks**.

### Submit for review

Facebook requires a Privacy Policy URL and Terms of Service URL on its basic app settings page. The [Code of Conduct](https://investor.fb.com/corporate-governance/code-of-conduct/default.aspx) page contains third party resource links to help create a privacy policy. The [Terms of Use](https://www.facebook.com/terms.php) page contains sample terms to help create an appropriate Terms of Service document.

After the bot is finished, Facebook has its own [review process](https://developers.facebook.com/docs/messenger-platform/app-review) for apps that are published to Messenger. The bot will be tested to ensure it's compliant with Facebook's [Platform Policies](https://developers.facebook.com/docs/messenger-platform/policy-overview).

### Make the App public and publish the Page

> [!NOTE]
> Until an app is published, it is in [Development Mode](https://developers.facebook.com/docs/apps/managing-development-cycle). Plugin and API functionality will only work for admins, developers, and testers.

After the review is successful, in the App Dashboard under App Review, set the app to Public. Ensure that the Facebook Page associated with this bot is published. Status appears in Pages settings.
Test the connection by following the steps described in the [Test your bot in Facebook](#test-your-bot-in-facebook) section.

## Connect a bot to Facebook Workplace

[JS Workplace Adapter]: https://www.npmjs.com/package/botbuilder-adapter-facebook
[CS Workplace Adapter]: https://github.com/microsoft/botbuilder-dotnet/tree/main/libraries/Adapters/Microsoft.Bot.Builder.Adapters.Facebook

> [!NOTE]
> On December 16, 2019, Workplace by Facebook changed its security model for custom integrations. Prior integrations built using Microsoft Bot Framework v4 need to be updated to use the Bot Framework Facebook adapters per the instructions below prior to February 28, 2020.
>
> Facebook will only consider integrations with limited access to Workplace data (low sensitivity permissions) eligible for continued use until December 31, 2020 if such integrations have completed and passed Security RFI and if the developer reaches out before January 15, 2020 via [Direct Support](https://my.workplace.com/work/admin/direct_support) to request continued use of the app.
>
> Bot Framework adapters are available for [JavaScript/Node.js][JS Workplace Adapter] and [C#/.NET][CS Workplace Adapter] bots.

Facebook Workplace is a business-oriented version of Facebook, which allows employees to easily connect and collaborate. It contains live videos, news feeds, groups, messenger, reactions, search, and trending posts. It also supports:

- Analytics and integrations. A dashboard with analytics, integration, single sign-on, and identity providers that companies use to integrate Workplace with their existing IT systems.
- Multi-company groups. Shared spaces in which employees from different organizations can work together and collaborate.

See the [Workplace Help Center](https://workplace.facebook.com/help/work/) to learn more about Facebook Workplace and [Workplace Developer Documentation](https://developers.facebook.com/docs/workplace) for guidelines about developing for Facebook Workplace.

To use Facebook Workplace with your bot, you must create a Workplace account and a custom integration to connect the bot.

### Create a Workplace Premium account

1. Submit an application to [workplace](https://www.facebook.com/workplace) on  behalf of your company.
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

Create a [custom integration](https://developers.facebook.com/docs/workplace/custom-integrations-new) for your Workplace following the steps described below. When you create a custom integration, an app with defined permissions and a page of type `Bot` (visible only within your Workplace community) are created.

1. In the **Admin Panel**, open the **Integrations** tab.
1. Select **Create your own custom App**.

    ![Workplace Integration](media/channels/fb-integration.png)

1. Choose a display name and a profile picture for the app. Such information will be shared with the page of type `Bot`.
1. Set the **Allow API Access to App Settings** to "Yes".
1. Copy and safely store the App ID, App Secret and App Token that's shown to you.

    ![Workplace Keys](media/channels/fb-keys.png)

1. Now you have finished creating a custom integration. You can find the page of type `Bot` in your Workplace community, as shown below.

    ![Workplace page](media/channels/fb-page.png)

### Update your bot code with Facebook adapter

Your bot's source code needs to be updated to include an adapter to communicate with Workplace by Facebook. Adapters are available for [JavaScript/Node.js][JS Workplace Adapter] and [C#/.NET][CS Workplace Adapter] bots.

### Provide Facebook credentials

 To your bot's **appsettings.json** file, add the **Facebook App ID**, **Facebook App Secret** and **Page Access Token** values that you copied from Facebook Workplace previously. Instead of a traditional page ID, use the numbers following the integrations name on its **About** page. Follow these instructions to update your bot source code in [JavaScript/Node.js][JS Workplace Adapter] or [C#/.NET][CS Workplace Adapter].

### Submit Workplace app for review

Please refer to the **Connect a bot to Facebook Messenger** section and [Workplace Developer Documentation](https://developers.facebook.com/docs/workplace) for details.

### Make the Workplace app public and publish the Page

Please refer to the **Connect a bot to Facebook Messenger** section for details.

### Setting the API version

If you receive a notification from Facebook about deprecation of a certain version of the Graph API, go to [Facebook developers page](https://developers.facebook.com). Navigate to your bot's **App Settings** and go to **Settings** > **Advanced** > **Upgrade API version**, then switch **Upgrade All Calls** to version 4.0.

Test the connection by following the steps described in the [Test your bot in Facebook](#test-your-bot-in-facebook) section.

## Connect a bot to Facebook using the Facebook adapter

Use the Bot Framework Facebook adapter to connect your bot with Facebook Workplace. To connect to Facebook Messenger, you can use the Facebook channel or the Facebook adapter.
Facebook adapters are available for [JavaScript/Node.js][JS Workplace Adapter] and [C#/.NET][CS Workplace Adapter] bots.

In this article you will learn how to connect a bot to Facebook using the adapter.  This article will walk you through modifying the Echo bot sample to connect it to Facebook.

The instructions below cover the C# implementation of the Facebook adapter. For instructions on using the JavaScript adapter, part of the BotKit libraries, [see the BotKit Facebook documentation](https://botkit.ai/docs/v4/platforms/facebook.html).

### Prerequisites

- The [EchoBot sample code](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/02.echo-bot)
- A Facebook for Developers account. If you do not have an account, you can [create one here](https://developers.facebook.com).

### Create a Facebook app, page and gather credentials

1. Log into [https://developers.facebook.com](https://developers.facebook.com). In the main menu, select **My Apps** > **Create App**.

    ![Create app](media/bot-service-channel-connect-facebook/create-app-button.png)

1. In the dialog that appears, enter a display name for your new app and then select **Create App ID**.

    ![Define app name](media/bot-service-channel-connect-facebook/app-name.png)

#### Set up Messenger and associate a Facebook page

1. Once your app has been created, you will see a list of products available to set up. Select **Set Up** next to the **Messenger** product.

1. You now need to associate your new app with a Facebook page&mdash;to create a page if you do not have an existing page you want to use, select **Create New Page** in the **Access Tokens** section. Select **Add or Remove Pages**, choose the page you want to associated with your app, and select **Next**. Leave the **Manage and access Page conversations on Messenger** setting enabled and select **Done**.

    ![Set up Messenger](media/bot-service-channel-connect-facebook/app-page-permissions.png)

1. Once you have associated your page, select **Generate Token** to generate a page access token. Make a note of this token as you will need it in a later step when configuring your bot application.

#### Obtain your app secret

1. In the left hand menu, select **Settings** and then select **Basic** to navigate to the basic settings page for your app.

1. On the basic settings page, select **Show** next to your **App Secret**. Make a note of this secret as you will need it in a later step when configuring your bot application.

### Wiring up the Facebook adapter in your bot

Now that you have your Facebook app, page and credentials, you need to configure your bot application.

#### Install the Facebook adapter NuGet package

Add the **Microsoft.Bot.Builder.Adapters.Facebook** NuGet package. For more information on using NuGet, see [Install and manage packages in Visual Studio](/nuget/consume-packages/install-use-packages-visual-studio).

#### Create a Facebook adapter class

Create a new class that inherits from the `FacebookAdapter` class. This class will act as our adapter for the Facebook channel and include error handling capabilities (similar to the `BotFrameworkAdapterWithErrorHandler` class already in the sample, used for handling other requests from Azure Bot Service).

```csharp
public class FacebookAdapterWithErrorHandler : FacebookAdapter
{
    public FacebookAdapterWithErrorHandler(IConfiguration configuration, ILogger<BotFrameworkHttpAdapter> logger)
            : base(configuration, logger)
        {
            OnTurnError = async (turnContext, exception) =>
            {
                // Log any leaked exception from the application.
                logger.LogError(exception, $"[OnTurnError] unhandled error : {exception.Message}");

                // Send a message to the user
                await turnContext.SendActivityAsync("The bot encountered an error or bug.");
                await turnContext.SendActivityAsync("To continue to run this bot, please fix the bot source code.");

                // Send a trace activity, which will be displayed in the Bot Framework Emulator
                await turnContext.TraceActivityAsync("OnTurnError Trace", exception.Message, "https://www.botframework.com/schemas/error", "TurnError");
            };
        }
}
```

#### Create a new controller for handling Facebook requests

Create a new controller which will handle requests from Facebook, on a new `api/facebook` endpoint instead of the default `api/messages` endpoint used for requests from Azure Bot Service channels. By adding an additional endpoint to your bot, you can accept requests from Bot Service channels, as well as from Facebook, using the same bot.

```csharp
[Route("api/facebook")]
[ApiController]
public class FacebookController : ControllerBase
{
    private readonly FacebookAdapter _adapter;
    private readonly IBot _bot;

    public FacebookController(FacebookAdapter adapter, IBot bot)
    {
        _adapter = adapter;
        _bot = bot;
    }

    [HttpPost]
    [HttpGet]
    public async Task PostAsync()
    {
        // Delegate the processing of the HTTP POST to the adapter.
        // The adapter will invoke the bot.
        await _adapter.ProcessAsync(Request, Response, _bot);
    }
}
```

#### Inject the Facebook adapter in your bot startup.cs

Add the following line to the `ConfigureServices` method within your **startup.cs** file. This will register your Facebook adapter and make it available for your new controller class.  The configuration settings you added in the previous step will be automatically used by the adapter.

```csharp
services.AddSingleton<FacebookAdapter, FacebookAdapterWithErrorHandler>();
```

Once added, your `ConfigureServices` method should look like this.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

    // Create the default Bot Framework adapter (used for Azure Bot Service channels and Emulator).
    services.AddSingleton<IBotFrameworkHttpAdapter, BotFrameworkAdapterWithErrorHandler>();

    // Create the Facebook adapter
    services.AddSingleton<FacebookAdapter, FacebookAdapterWithErrorHandler>();

    // Create the bot as a transient. In this case the ASP controller is expecting an IBot.
    services.AddTransient<IBot, EchoBot>();
}
```

#### Obtain a URL for your bot

Now that you have wired up the adapter in your bot project, you need to provide to Facebook the correct endpoint for your application, so that your bot will receive messages. You also need this URL to complete configuration of your bot application.

To complete this step, [deploy your bot to Azure](bot-builder-deploy-az-cli.md) and make a note of the URL of your deployed bot.

> [!NOTE]
> If you are not ready to deploy your bot to Azure, or wish to debug your bot when using the Facebook adapter, you can use a tool such as [ngrok](https://www.ngrok.com) (which you will likely already have installed if you have used the Bot Framework Emulator previously) to tunnel through to your bot running locally and provide you with a publicly accessible URL for this.
>
> If you wish create an ngrok tunnel and obtain a URL to your bot, use the following command in a terminal window (this assumes your local bot is running on port 3978, alter the port numbers in the command if your bot is not).
>
> ```cmd
> ngrok.exe http 3978 -host-header="localhost:3978"
> ```

#### Add Facebook app settings to your bot's configuration file

Add the settings shown below to your **appsettings.json** file in your bot project. You populate **FacebookAppSecret** and **FacebookAccessToken** using the values you gathered when creating and configuring your Facebook App. **FacebookVerifyToken** should be a random string that you create and will be used to ensure your bot's endpoint is authentic when called by Facebook.

```json
"FacebookVerifyToken": "",
"FacebookAppSecret": "",
"FacebookAccessToken": ""
```

Once you have populated the settings above, you should redeploy (or restart if running locally with ngrok) your bot.

### Complete configuration of your Facebook app

The final step is to configure your new Facebook app's Messenger endpoint, to ensure your bot receives messages.

1. Within the dashboard for your app, select **Messenger** in the left hand menu and then select **Settings**.

1. In the **Webhooks** section select **Add Callback URL**.

1. In the **Callback URL** text box enter your bot's URL, plus the `api/facebook` endpoint you specified in your newly created controller. For example, `https://yourboturl.com/api/facebook`. In the **Verify Token** text box enter the verify token you created earlier and used in your bot application's **appsettings.json** file.

    ![Edit callback url](media/bot-service-channel-connect-facebook/edit-callback-url.png)

1. Select **Verify and Save**. Ensure you bot is running, as Facebook will make a request to your application's endpoint and verify it using your **Verify Token**.

1. Once your callback URL has been verified, select **Add Subscriptions** that is now shown. In the pop-up window, choose the following subscriptions and select **Save**.

    - **messages**
    - **messaging_postbacks**
    - **messaging_optins**
    - **messaging_deliveries**

    ![Webhook subscriptions](media/bot-service-channel-connect-facebook/webhook-subscriptions.png)

## Test your bot in Facebook

You can now test whether your bot is connected to Facebook correctly by sending a message via the Facebook Page you associated with your new Facebook app.

1. Navigate to your Facebook Page.

1. Select **Add a Button**.

    ![Add a button](media/bot-service-channel-connect-facebook/add-button.png)

1. Choose **Contact You** and **Send Message**, then select **Next**.

    ![Choose the buttons to show](media/bot-service-channel-connect-facebook/button-settings.png)

1. When asked **Where would you like this button to send people to?** choose **Messenger**, then select **Finish**.

    ![Choose where the buttons send people](media/bot-service-channel-connect-facebook/button-settings-2.png)

1. Hover over the new **Send Message** button that is now shown on your Facebook Page and select **Test Button** from the pop-up menu.  This will start a new conversation with your app via Facebook Messenger, which you can use to test messaging your bot. Once the message is received by your bot, it will send a message back to you, echoing the text from your message.

You can also test this feature using the [sample bot for the Facebook adapter](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/61.facebook-adapter) by populating the **appsettings.json** file with the same values described in the steps above.

## Additional resources

[Facebook-events](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/23.facebook-events). Use this sample to explore the bot communication with Facebook Messenger.

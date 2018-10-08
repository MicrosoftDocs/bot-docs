---
title: Add authentication to your bot via Azure Bot Service | Microsoft Docs
description: Learn how to use the Azure Bot Service authentication features to add SSO to your bot.
author: JonathanFingold
ms.author: JonathanFingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 09/27/2018
monikerRange: 'azure-bot-service-3.0'
---

[!INCLUDE [pre-release-label](includes/pre-release-label-v3.md)]

# Add authentication to your bot via Azure Bot Service
This tutorial uses new bot authentication capabilities in Azure Bot Service, providing features to make it easier to develop a bot that authenticates users to various identity providers such as Azure AD (Azure Active Directory), GitHub, Uber, and so on. These updates also take steps towards an improved user experience by eliminating the _magic code verification_ for some clients.

Prior to this, your bot needed to include OAuth controllers and login links, store the target client IDs and secrets, and perform user token management.
<!--
These capabilities were bundled in the BotAuth and AuthBot samples that are on GitHub.
-->

Now, bot developers no longer need to host OAuth controllers or manage the token life-cycle, as all of this can now be done by the Azure Bot Service.

The features include:

- Improvements to the channels to support new authentication features, such as new WebChat and DirectLineJS libraries to eliminate the need for the 6-digit magic code verification.
- Improvements to the Azure Portal to  add, delete, and configure connection settings to various OAuth identity providers.
- Support for a variety of out-of-the-box identity providers including Azure AD (both v1 and v2 endpoints), GitHub, and others.
- Updates to the C# and Node.js Bot Builder SDKs to be able to retrieve tokens, create OAuthCards and handle TokenResponse events.
- Samples for how to make a bot that authenticates to Azure AD (v1 and v2 endpoints) and to GitHub.

You can extrapolate from the steps in this article to add such features to an existing bot. The following are sample bots that demonstrate the new authentication features

| Sample | BotBuilder version | Description |
|:---|:---:|:---|
| [AadV1Bot](https://github.com/Microsoft/BotBuilder-V3/tree/master/CSharp/Samples/AadV1Bot) | v3 | Demonstrates OAuthCard support in the v3 C# SDK, using the Azure AD v1 endpoint |
| [AadV2Bot](https://github.com/Microsoft/BotBuilder-V3/tree/master/CSharp/Samples/AadV2Bot) | v3 |  Demonstrates OAuthCard support in the v3 C# SDK, using the Azure AD v2 endpoint |
| [GitHubBot](https://github.com/Microsoft/BotBuilder-V3/tree/master/CSharp/Samples/GitHubBot) | v3 |  Demonstrates OAuthCard support in the v3 C# SDK, using GitHub |
| [BasicOAuth](https://github.com/Microsoft/BotBuilder-V3/tree/master/CSharp/Samples/Microsoft.Bot.Sample.BasicOAuth) | v3 |  Demonstrates OAuth 2.0 support in the v3 C# SDK |

> [!NOTE]
> The authentication features also work with Node.js with BotBuilder v3. However, this article covers just sample C# code.

For additional information and support, refer to [Bot Framework additional resources](https://docs.microsoft.com/azure/bot-service/bot-service-resources-links-help).

## Overview

This tutorial creates a sample bot that connects to the Microsoft Graph using an Azure AD v1 or v2 token. <!--verify this info and fix wording--> As part of this process, you'll use code from a GitHub repo, and this tutorial describes how to set that up, including the bot application.

- [Create your bot and an authentication application](#create-your-bot-and-an-authentication-application)
- [Prepare the bot sample code](#prepare-the-bot-sample-code)
- [Use the Emulator to test your bot](#use-the-emulator-to-test-your-bot)

To complete these steps, you will need Visual Studio 2017, npm, node, and git installed. You should also have some familiarity with Azure, OAuth 2.0, and bot development.

Once you finish, you will have a bot that can respond to a few simple tasks against an Azure AD application, such as checking and sending an email, or displaying who you are and who your manager is. To do this, your bot will use a token from an Azure AD application against the Microsoft.Graph library.

The final section breaks down some of the bot code

- [Notes on the token retrieval flow](#notes-on-the-token-retrieval-flow)

## Create your bot and an authentication application

You need to create a registration bot to which you'll publish your bot code, and you need to create an Azure AD (either v1 or v2) application to allow your bot to access Office 365.

> [!NOTE]
> These authentication features work with other types of bots. However this tutorial uses a registration only bot.

### Register an application in Azure AD

You need an Azure AD application that your bot can use to connect to the Microsoft Graph API, your own Azure AD-protected resources, and so on.

For this bot you can use Azure AD v1 or v2 endpoints.
For information about the differences between the v1 and v2 endpoints, see the [v1-v2 comparison](https://docs.microsoft.com/azure/active-directory/develop/active-directory-v2-compare) and the [Azure AD v2.0 endpoint overview](https://docs.microsoft.com/azure/active-directory/develop/active-directory-appmodel-v2-overview).

#### To create an Azure AD v1 application

1. Go to [Azure AD in the Azure portal](https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/Overview).
1. Click **App registrations**.
1. In the **App registrations** panel, click **New application registration**.
1. Fill in the required fields and create the app registration.
   1. Name your application.
   1. Set the **Application type** to **Web app / API**.
   1. Set the **Sign-on URL** to `https://token.botframework.com/.auth/web/redirect`.
   1. Click **Create**.
      - Once it is created, it is displayed in a **Registered app** pane.
      - Record the **Application ID** value. You will provide this later as the _Client ID_.
1. Click **Settings** to configure your application.
1. Click **Keys** to open the **Keys** panel.
   1. Under **Passwords**, create a `BotLogin` key.
   1. Set its **Duration** to **Never expires**.
   1. Click **Save** and record the key value. You provide this later for the _application secret_.
   1. Close the **Keys** panel.
1. Click **Required permissions** to open the **Required permissions** panel.
   1. Click **Add**.
   1. Click **Select an API**, then select **Microsoft Graph** and click **Select**.
   1. Click **Select permissions**. Choose the application permissions your application will use.

      > [!NOTE]
      > Any permission marked as **Requires Admin** will require both a user and a tenant admin to login, so for your bot tend to stay away from these.

      Select the following Microsoft Graph delegated permissions:
      - Read all users' basic profiles
      - Read user mail
      - Sign in and read user profile
      - Send mail as a user
      - View users' basic profile
      - View users' email address

   1. Click **Select**, then click **Done**.
   1. Close the **Required permissions** panel.

You now have an Azure AD v1 application configured.

#### To create an Azure AD v2 application

1. Go to the [Microsoft Application Registration Portal](https://apps.dev.microsoft.com).
1. Click **Add an app**
1. Give your Azure AD app a name, and click **Create**.

    Record the **Application Id** GUID. You will provide this later as your client ID for your connection setting.

1. Under **Application Secrets**, click **Generate New Password**.

    Record the password from the pop-up. You will provide this later as your client Secret for your connection setting.

1. Under **Platforms**, click **Add Platform**.
1. In the **Add Platform** pop-up, click **Web**.
    1. Leave **Allow Implicit Flow** checked.
    1. For **Redirect URL**, enter `https://token.botframework.com/.auth/web/redirect`.
    1. Leave **Logout URL** blank.
1. Under **Microsoft Graph Permissions**, you can add additional delegated permissions.
    - For this tutorial, add the
      **Mail.Read**, **Mail.Send**, **openid**, **profile**, **User.Read**, and **User.ReadBasic.All** permissions.
      The scope of the connection setting needs to have both **openid** and a resource in the Azure AD graph, such as **Mail.Read**.
    - Record the permissions you choose. You will provide this later as the scopes for your connection setting.

1. Click **Save** at the bottom of the page.

### Create your bot on Azure

Create a **Bot Channels Registration** using the [Azure Portal](https://portal.azure.com/).

### Register your Azure AD application with your bot

The next step is to register with your bot the Azure AD application that you just created.

#### To register an Azure AD v1 application

1. Navigate to your bot's resource page on the [Azure Portal](http://portal.azure.com/).
1. Click **Settings**.
1. Under **OAuth Connection Settings** near the bottom of the page, click **Add Setting**.
1. Fill in the form as follows:
    1. For **Name**, enter a name for your connection. You'll use in your bot code.
    1. For **Service Provider**, select **Azure Active Directory**. Once you select this, the Azure AD-specific fields will be displayed.
    1. For **Client id**, enter the application ID that you recorded for your Azure AD v1 application.
    1. For **Client secret**, enter the key that your recorded for your application's `BotLogin` key.
    1. For **Grant Type**, enter `authorization_code`.
    1. For **Login URL**, enter `https://login.microsoftonline.com`.
    1. For **Tenant ID**, enter the tenant ID for your Azure Active Directory, for example `microsoft.com` or `common`.

       This will be the tenant associated with the users who can be authenticated. To allow anyone to authenticate themselves via the bot, use the `common` tenant.

    1. For **Resource URL**, enter `https://graph.microsoft.com/`.
    1. Leave **Scopes** blank.
1. Click **Save**.

> [!NOTE]
> These values enable your application to access Office 365 data via the Microsoft Graph API.

You can now use this connection name in your bot code to retrieve user tokens.

#### To register an Azure AD v2 application

1. Navigate to your bot's Bot Channels Registration page on the [Azure Portal](http://portal.azure.com/).
1. Click **Settings**.
1. Under **OAuth Connection Settings** near the bottom of the page, click **Add Setting**.
1. Fill in the form as follows:
    1. For **Name**, enter a name for your connection. You'll use it in your bot code.
    1. For **Service Provider**, select **Azure Active Directory v2**. Once you select this, the Azure AD-specific fields will be displayed.
    1. For **Client id**, enter your Azure AD v2 application ID from application registration.
    1. For **Client secret**, enter your Azure AD v2 application password from application registration.
    1. For **Tenant ID**, enter the tenant ID for your Azure Active Directory, for example `microsoft.com` or `common`.

        This will be the tenant associated with the users who can be authenticated. To allow anyone to authenticate themselves via the bot, use the `common` tenant.

    1. For **Scopes**, enter the names of the permission you chose from application registration:
       `Mail.Read Mail.Send openid profile User.Read User.ReadBasic.All`.

        > [!NOTE]
        > For Azure AD v2, **Scopes** takes a case-sensitive, space-separated list of values.

1. Click **Save**.

> [!NOTE]
> These values enable your application to access Office 365 data via the Microsoft Graph API.

You can now use this connection name in your bot code to retrieve user tokens.

#### To test your connection

1. Open the connection you just created.
1. Click **Test Connection** at the top of the **Service Provider Connection Setting** pane.
1. The first time, this should open a new browser tab listing the permissions your app is requesting and prompt you to accept.
1. Click **Accept**.
1. This should then redirect you to a **Test Connection to `<your-connection-name>' Succeeded** page.

## Prepare the bot sample code

1. Clone the github repository at https://github.com/Microsoft/BotBuilder.
1. Open and build the solution, `BotBuilder\CSharp\Microsoft.Bot.Builder.sln`.
1. Close that solution and open, `BotBuilder\CSharp\Samples\Microsoft.Bot.Builder.Samples.sln`.
1. Set the start up project.
    - For a bot that uses the v1 Azure AD application, use the `Microsoft.Bot.Sample.AadV1Bot` project.
    - For a bot that uses the v2 Azure AD application, use the `Microsoft.Bot.Sample.AadV2Bot` project.
1. Open the `Web.config` file, and modify the app settings as follows:
    1. Set the `ConnectionName` to the value you used when you configured your bot's OAuth 2.0 connection setting.
    1. Set the `MicrosoftAppId` value to your bot's app ID.
    1. Set the `MicosoftAppPassword` value to your bot's secret.

    > [!IMPORTANT]
    > Depending on the characters in your secret, you may need to XML escape the password. For example, any ampersands (&) will need to be encoded as `&amp;`.

    ```xml
    <appSettings>
        <add key="ConnectionName" value="<your-AAD-connection-name>"/>
        <add key="MicrosoftAppId" value="<your-bot-appId>" />
        <add key="MicrosoftAppPassword" value="<your-bot-password>" />
    </appSettings>
    ```

    If you do not know how to get your **Microsoft app ID** and **Microsoft app password** values, look in the **ApplicationSettings** of the Azure app service that was provisioned for your bot on the Azure Portal.

    > [!NOTE]
    > You could now publish this bot code back to your Azure subscription (right-click on the project and choose **Publish**), but it is not necessary for this tutorial. You would need to set up a publishing configuration that uses the application and hosting plan that you used when configuration the bot in the Azure Portal.

## Use the Emulator to test your bot

You will need to install the [Bot Emulator](https://github.com/Microsoft/BotFramework-Emulator) to test your bot locally. You can use the v3 or v4 Emulator.

1. Start your bot (with or without debugging).
1. Note the localhost port number for the page. You will need this information to interact with your bot.
1. Start the Emulator.
1. Connect to your bot.

   If you haven't configured the connection already, provide the address and your bot's Microsoft app ID and password. Add `/api/messages` to the bot's URL. Your URL will look something like `http://localhost:portNumber/api/messages`.

1. Type `help` to see a list of available commands for the bot, and test the authentication features.
1. Once you've signed in, you don't need to provide your credentials again until you sign out.
1. To sign out, and cancel your authentication, type `signout`.

<!--To restart completely from scratch you also need to:
1. Navigate to the **AppData** folder for your account.
1. Go to the **Roaming/botframework-emulator** subfolder.
1. Delete the **Cookies** and **Coolies-journal** files.
-->

> [!NOTE]
> Bot authentication requires use of the Bot Connector Service. The service accesses the bot channels registration information for your bot, which is why you need to set your bot's messaging endpoint on the portal. Authentication also requires the use of HTTPS, which is why you needed to create an HTTPS forwarding address for your bot running locally.

<!--The following is necessary for WebChat:
1. Use the **ngrok** command-line tool to get a forwarding HTTPS address for your bot.
   - For information on how to do this, see [Debug any Channel locally using ngrok](https://blog.botframework.com/2017/10/19/debug-channel-locally-using-ngrok/).
   - Any time you exit **ngrok**, you will need to redo this and the following step before starting the Emulator.
1. On the Azure Portal, go to the **Settings** blade for your bot.
   1. In the **Configuration** section, change the **Messaging endpoint** to the HTTPS forwarding address generated by **ngrok**.
   1. Click **Save** to save your change.
-->

## Notes on the token retrieval flow

When a user asks the bot to do something that requires the bot to have the user logged in, the bot can use the `Microsoft.Bot.Builder.Dialogs.GetTokenDialog` to initiate retrieving a token for a given connection. The next couple of snippets are taken from the `GetTokenDialog` class.

### Check for a cached token

In this code, first the bot does a quick check to determine if the Azure Bot Service already has a token for the user (which is identified by the current Activity sender) and the given ConnectionName (which is the connection name used in configuration). Azure Bot Service will either already have a token cached or it will not. The call to GetUserTokenAsync performs this ‘quick check'. If Azure Bot Service has a token and returns it, the token can immediately be used. If Azure Bot Service does not have a token, this method will return null. In this case, the bot can send a customized OAuthCard for the user to login.

```csharp
// First ask Bot Service if it already has a token for this user
var token = await context.GetUserTokenAsync(ConnectionName).ConfigureAwait(false);
if (token != null)
{
    // use the token to do exciting things!
}
else
{
    // If Bot Service does not have a token, send an OAuth card to sign in
    await SendOAuthCardAsync(context, (Activity)context.Activity);
}
```

### Send an OAuthCard to the user

You can customize the OAuthCard with whatever text and button text you want. The important pieces are:

- Set the `ContentType` to `OAuthCard.ContentType`.
- Set the `ConnectionName` property to the name of the connection you want to use.
- Include one button with a `CardAction` of `Type` `ActionTypes.Signin`; note that you do not need to specify any value for the sign in link.

At the end of this call, the bot needs to "wait for the token" to come back. This waiting takes place on the main Activity stream because there could be a lot the user needs to do to sign-in.

```csharp
private async Task SendOAuthCardAsync(IDialogContext context, Activity activity)
{
    await context.PostAsync($"To do this, you'll first need to sign in.");

    var reply = await context.Activity.CreateOAuthReplyAsync(_connectionName, _signInMessage, _buttonLabel).ConfigureAwait(false);
    await context.PostAsync(reply);

    context.Wait(WaitForToken);
}
```

### Wait for a TokenResponseEvent

In this code the Bot's dialog class is waiting for a `TokenResponseEvent` (more about how this is routed to the Dialog stack is below). The `WaitForToken` method first determines if this event was sent. If it was sent, it can be used by the bot. If it was not, the `WaitForToken` method takes whatever text was sent to the bot and passes it to `GetUserTokenAsync`. The reason for this is that some clients (like WebChat) do not need the Magic Code verification code and can directly send the Token in the `TokenResponseEvent`. Other clients still require the magic code (like Facebook or Slack). The Azure Bot Service will present these clients with a six digit magic code and ask the user to type this into the chat window. While not ideal, this is the 'fall back' behavior and so if `WaitForToke`n receives a code, the bot can send this code to the Azure Bot Service and get a token back. If this call also fails, then you can decide to report an error, or do something else. In most cases though, the bot will now have a user token.

If you look in the **MessageController.cs** file, you'll see that `Event` activities of this type are also routed to the dialog stack.

```csharp
private async Task WaitForToken(IDialogContext context, IAwaitable<object> result)
{
    var activity = await result as Activity;

    var tokenResponse = activity.ReadTokenResponseContent();
    if (tokenResponse != null)
    {
        // Use the token to do exciting things!
    }
    else
    {
        if (!string.IsNullOrEmpty(activity.Text))
        {
            tokenResponse = await context.GetUserTokenAsync(ConnectionName,
                                                               activity.Text);
            if (tokenResponse != null)
            {
                // Use the token to do exciting things!
                return;
            }
        }
        await context.PostAsync($"Hmm. Something went wrong. Let's try again.");
        await SendOAuthCardAsync(context, activity);
    }
}
```

### Message controller

On subsequent calls to the bot, notice that the token is never cached by this sample bot. This is because the bot can always ask the Azure Bot Service for the token. This avoids the bot needing to manage the token life-cycle, refresh the token, etc, as Azure Bot Service does all of this for you.

```csharp
else if(message.Type == ActivityTypes.Event)
{
    if(message.IsTokenResponseEvent())
    {
        await Conversation.SendAsync(message, () => new Dialogs.RootDialog());
    }
}
```
## Additional resources
[Bot Builder SDK](https://github.com/microsoft/botbuilder)

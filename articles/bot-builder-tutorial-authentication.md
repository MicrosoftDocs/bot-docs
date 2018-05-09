---
title: Add authenticaion to your bot via Azure Bot Service | Microsoft Docs
description: Learn how to use the Azure Bot Service authentication features to add SSO to your bot.
author: JonathanFingold
ms.author: JonathanFingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 4/26/2018
monikerRange: 'azure-bot-service-3.0'
---

# Add authentication to your bot via Azure Bot Service

This tutorial uses new bot authentication capabilities in Azure Bot Service, providing features to make it easier to develop a bot that authenticates users to various identity providers such as AAD (Azure Active Directory), GitHub, Uber, and so on. These updates also take steps towards an improved user experience by eliminating the _magic code verification_ for some clients.

Prior to this, your bot needed to include OAuth controllers and login links, store the target client IDs and secrets, and perform user token management.
<!--
These capabilities were bundled in the BotAuth and AuthBot samples that are GitHub.
-->

Now, bot developers no longer need to host OAuth controllers or manage the token lifecycle, as all of this can now be done by the Azure Bot Service.

The features include:
- Improvements to the channels to support new authentication features.
- Improvements to the Azure Portal to  add, delete, and configure connection settings to various OAuth identity providers.
- Support for a variety of out-of-the-box identity providers including AAD v1, AAD v2, GitHub, and others.
- Updates to the C# and Node.js Bot Builder SDKs to be able to retrieve tokens, create OAuthCards and handle TokenResponse events.
- New Webchat and DirectLineJS libraries to eliminate the need for the 6-digit magic code verification.
- Samples for how to make a bot that authenticates to AAD (v1 and v2) and to GitHub.

You can extrapolate from the steps in this article to add such features to an existing bot. The following are sample bots that demonstrate the new authentication features

| Sample | BotBuilder version | Description |
|:---|:---:|:---|
| [AadV1Bot](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/AadV1Bot) | v3 | Demonstrates OAuthCard support in the v3 C# SDK, using AAD v1 |
| [AadV2Bot](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/AadV2Bot) | v3 |  Demonstrates OAuthCard support in the v3 C# SDK, using AAD v2 |
| [GitHubBot](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/GitHubBot) | v3 |  Demonstrates OAuthCard support in the v3 C# SDK, using GitHub |
| [BasicOAuth](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples/Microsoft.Bot.Sample.BasicOAuth) | v3 |  Demonstrates OAuth support in the v3 C# SDK |


> [!NOTE]
> The authentication features also work with Node.js with BotBuilder v3. However, this article covers just sample C# code.

For additional information and support, refer to [Bot Framework additional resources](https://docs.microsoft.com/en-us/azure/bot-service/bot-service-resources-links-help).

## Overview

This tutorial creates a sample bot that connects to the Microsoft Graph using an AAD v1 or v2 token. <!--verify this info and fix wording--> As part of this process, you'll use code from a GitHub repo, and the tutorial describes how to set that up, including the bot application.

- [Create your bot and an authentication application](#create-your-bot-and-an-authentication-application)
- [Prepare the bot sample code](#prepare-the-bot-sample-code)
- [Use the Emulator to test your bot](#use-the-emulator-to-test-your-bot)

To complete these steps, you will need Visual Studio 2017, npm, node, and git installed. You should also have some familiarity with Azure, OAuth, and bot development.

Once you finish, you will have a bot that can respond to a few simple tasks against an AAD application, such as checking and sending an email, or displaying who you are and who your manager is. To do this, your bot will use a token from an AAD application against the Microsoft.Graph library.

The final section breaks down some of the bot code

-   [Notes on the token retrieval flow](#notes-on-the-token-retrieval-flow)

## Create your bot and an authentication application

You need to create a registration bot to which you'll publish your bot code, and you need to create an AAD (either v1 or v2) application to allow your bot to access Office 365.

> [!NOTE]
> These authentication features work with other types of bots. However this tutorial uses a registration only bot.

### Create an AAD application

You need an AAD application that your bot can use to connect to the O365 Graph API, your own AAD resources, and so on.

For this bot you can use AAD v1 or v2.
For information about the differences between v1 and v2, see the [v1-v2 comparison](https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-v2-compare) and the [AAD v2.0 endpoint overview](https://docs.microsoft.com/en-us/azure/active-directory/develop/active-directory-appmodel-v2-overview).

#### To create an AAD v1 application

1.  Go to [Azure AD for Office 365](https://ms.portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/Overview).
1.  Click **App registrations**.
1.  In the **App registrations** panel, click **New application registration**.
1.  Fill in the required fields and create the app registration.

    1.  Name your application.
    1.  Set the **Application type** to **Web app / API**.
    1.  Set the **Sign-on URL** to `https://token.botframework.com/.auth/web/redirect`.
    1.  Click **Create**.

    Once it is created, it is displayed in the application list.

1.  Click the appliction to open the blade.

    Record the **Application ID** GUID. You will provide this later as the _Client ID_.

1.  Click **Settings** to configure your application.
1.  Click **Keys** to open the **Keys** panel.

    1.  Under **Passwords**, create a `BotLogin` key.
    1.  Set its **Durration** to **Never expires**.
    1.  Click **Save** and record the key value. You provide this later for the _application secret_.
    1.  Close the **Keys** panel.

1.  Click **Required permissions** to open the **Required permissions** panel.

    1.  Click **Add**.
    1.  Click **Select an API**, then select **Microsoft Graph** and click **Select**.
    1.  Click **Select permissions**. Choose the application permissions your application will use.

        > [!NOTE]
        > Any permission marked as **Requires Admin** will require both a user and a tenant admin to login, so for your bot tend to stay away from these.
        
        Fun ones for the Microsoft Graph include (all are Delegated Permissions): 
        -   Read user mail
        -   Sign in and read user profile
        -   Send mail as a user
        -   View users' basic profile
        -   View users' email address

    1.  Click **Select**, then click **Done**.
    1.  Click **Save**.
    1.  Close the **Required permissions** panel.

You now have an AAD v1 application configured. 

#### To create an AAD v2 application

1.	Go to the [Microsoft Application Registration Portal](https://apps.dev.microsoft.com).
1.	Click **Add an app**
1.	Give your AAD app a name, and click **Create**.

    Record the **Application Id** GUID. You will provide this later as your client ID for your connection setting.

1.	Under **Application Secrets**, click **Generate New Password**.

    Record the password from the pop-up. You will provide this later as your client Secret for your connection setting.

1.	Under **Platforms**, click **Add Platform**.
1.	In the **Add Platform** pop-up, click **Web**.

    1.	Leave **Allow Implicit Flow** checked.
    1.  For **Redirect URL**, enter `https://token.botframework.com/.auth/web/redirect`.
    1.  Leave **Logout URL** blank.

1.	Under **Microsoft Graph Permissions**, you can add additional delegated permissions, such as **email**, **Mail.Read**, **profile**, **User.Read**, and so on.

    Record the permissions you choose. You will provide this later as the scopes for your connection setting.

1.	Click **Save** at the bottom of the page.

### Create your bot on Azure

Create a **Bot Channels Registration** using the [Azure Portal](https://ms.portal.azure.com/).

### Register your AAD application with your bot

The next step is to register with your bot the AAD application that you just created.

#### To register an AAD v1 application

1.	Navigate to your bot's resource page on the [Azure Portal](http://portal.azure.com/).
1.	Click **Settings**.
1.	Under **OAuth Connection Settings** near the bottom of the page, click **Add Setting**.
1.	Fill in the form as follows:

    1.	For **Name**, enter a name for your connection. You'll use in your bot code.
    1.	For **Service Provider**, select **Azure Active Directory**. Once you select this, the AAD-specific fields will be displayed.
    1.	For **Client id**, enter the name that you gave to your AAD v1 application.
    1.	For **Client secret**, enter the key that your recorded for your application's `BotLogin` key.
    1.	For **Grant Type**, enter `authorization_code`.
    1.	For **Login URL**, enter `https://login.microsoftonline.com`.
    1.	For **Tenant ID**, enter the tenant ID for your Azure Active Directory, for example `microsoft.com` or `common`.
    1.	For **Resource URL**, enter `https://graph.microsoft.com/`.
    1.	Leave **Scopes** blank.

1.	Click **Save**.
    
> [!NOTE]
> These values enable your application to use the Graph API in O365.

You can now use this connection name in your bot code to retrieve user tokens.

#### To register an AAD v2 application

1.	Navigate to your bot's resource page on the [Azure Portal](http://portal.azure.com/).
1.	Click **Settings**.
1.	Under **OAuth Connection Settings** near the bottom of the page, click **Add Setting**.
1.	Fill in the form as follows:

    1.	For **Name**, enter a name for your connection. You'll use in your bot code.
    1.	For **Service Provider**, select **Azure Active Directory v2**. Once you select this, the AAD-specific fields will be displayed.
    1.	For **Client id**, enter your AAD v2 application ID from application registration.
    1.	For **Client secret**, enter your AAD v2 application password from application registration.
    1.	For **Tenant ID**, enter the tenant ID for your Azure Active Directory, for example `microsoft.com` or `common`.
    1.	For **Scopes**, enter the names of the permission you chose from application registration, for example `email Mail.Read profile User.Read`.

        >   [!NOTE]
        >   Scopes is a space-separated list of values for AAD v2.

1.	Click **Save**.
    
> [!NOTE]
> These values enable your application to use the Graph API in O365.

You can now use this connection name in your bot code to retrieve user tokens.

## Prepare the bot sample code

1.	Clone the github repository at https://github.com/Microsoft/BotBuilder.
1.	In this directory, open the solution file, `BotBuilder\CSharp\Samples\Microsoft.Bot.Builder.Samples.sln`.
1.	Set the start up project.

    -   For a bot that uses the v1 AAD application, use the `Microsoft.Bot.Sample.AadV1Bot` project.
    -   For a bot that uses the v2 AAD application, use the `Microsoft.Bot.Sample.AadV2Bot` project.

1.	Open the `Web.config` file, and modify the app settings as follows:

    1.	Set the `ConnectionName` to the value you used when you configured your bot's OAuth connection setting.
    1.	Set the `MicrosoftAppId` value to your bot's app ID.
    1.	Set the `MicosoftAppPassword` value to your bot's secret.

        Depending on the characters in your secret, you may need to XML escape the password. For example, any ampersands (&) will need to be encoded as `&amp;`.

    ```xml
    <appSettings>
        <add key="ConnectionName" value="YOUR_AAD_CONNECTION_NAME"/>
        <add key="MicrosoftAppId" value="YOUR_BOT_MS_APPID" />
        <add key="MicrosoftAppPassword" value="YOUR_BOT_MS_PASSWORD" />
    </appSettings>
    ```

    > [!TIP]
    > If you do not know how to get you `MicosoftAppId` and `MicrosoftPassword` values, look in the ApplicationSettings of the Azure App Service that was provisioned for your bot on the Azure Portal.

    > [!NOTE]
    > You could now publish this bot code back to your Azure subscription (right-click on the project and choose **Publish**), but it is not necesseray for this tutorial. You would need to set up a publishing configuration that uses the application and hosting plan that you used when configurating the bot in the Azure Portal.

## Use the Emulator to test your bot

You will need to install the [Bot Emulator](https://github.com/Microsoft/BotFramework-Emulator) to test your bot locally.

1.  Start your bot (with or without debugging).
1.  Note the localhost port number for the page. You will need this information to interact with your bot.
1.  Start the emulator and connect it to your bot by entering the URL from the previous step.
    Your URL will look something like `http://localhost:portNumber/api/messages`.

## Notes on the token retrieval flow

When a user asks the bot to do something that requires the bot to have the user logged in, the bot can execute the following code to initiate retrieving a token for a given connection.

### Check for a cached token

In this code, first the bot does a quick check to determine if the Azure Bot Service already has a token for the user (which is identified by the current Activity sender) and the given ConnectionName (which is the connection name used in configuration). Azure Bot Service will either already have a token cached or it will not. The call to GetUserTokenAsync peforms this ‘quick check'. If Azure Bot Service has a token and returns it, the token can immediately be used. If Azure Bot Service does not have a token, this method will return null. In this case, the bot can send a customized OAuthCard for the user to login.

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

-   That the ContentType be set to OAuthCard.ContentType
-   That you specify the ConnectionName property with the name of the connection you want to use
-   That there is one button with a CardAction of Type ActionTypes.Signin; note that you do not need to specify any value for the sign in link

At the end of this call, the bot needs to “wait for the token” to come back. This waiting takes place on the main Activity stream because there could be a lot the user needs to do to sign-in.

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

In this code the Bot's dialog class is waiting for a `TokenResponseEvent` (more about how this is routed to the Dialog stack is below). The WaitForToken method first determines if this event was sent. If it was sent, it can be used by the bot. If it was not, the WaitForToken method takes whatever text was sent to the bot and passes it to GetUserTokenAsync. The reason for this is that some clients (like WebChat) do not need the Magic Code verification code and can directly send the Token in the TokenResponseEvent. Other clients still require the magic code (like Facebook or Slack). The Azure Bot Service will present these clients with a six digit magic code and ask the user to type this into the chat window. While not ideal, this is the ‘fall back' behavior and so if WaitForToken receives a code, the bot can send this code to the Azure Bot Service and get a token back. If this call also fails, then you can decide to report an error, or do something else. In most cases though, the bot will now have a user token.

If you look in the MessageController.cs file, you'll see that Event activities of this type are also routed to the dialog stack.

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

On subsequent calls to the bot, notice that the token is never cached by this sample bot. This is because the bot can always ask the Azure Bot Service for the token. This avoids the bot needing to manage the token lifecycle, refresh the token, etc, as Azure Bot Service does all of this for you.

```csharp
else if(message.Type == ActivityTypes.Event)
{
    if(message.IsTokenResponseEvent())
    {
        await Conversation.SendAsync(message, () => new Dialogs.RootDialog());
    }
}
```

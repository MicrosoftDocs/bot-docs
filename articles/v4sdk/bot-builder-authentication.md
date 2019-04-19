---
title: Add authentication to your bot via Azure Bot Service | Microsoft Docs
description: Learn how to use the Azure Bot Service authentication features to add SSO to your bot.
author: JonathanFingold
ms.author: v-jofing
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: abs
ms.date: 04/09/2019
monikerRange: 'azure-bot-service-4.0'
---

# Add authentication to your bot via Azure Bot Service

[!INCLUDE [applies-to-v4](../includes/applies-to.md)]

The Azure Bot Service and the v4 SDK include new bot authentication capabilities, providing features to make it easier to develop a bot that authenticates users to various identity providers, such as Azure AD (Azure Active Directory), GitHub, Uber, and so on. These capabilities can improve the user experience by eliminating the _magic code verification_ for some clients.

Prior to this, your bot needed to include OAuth controllers and login links, store the target client IDs and secrets, and perform user token management. The bot would ask the user sign in on a website, which would then generate a _magic code_ the user could use to verify their identity.

Now, bot developers no longer need to host OAuth controllers or manage the token life-cycle, as all of this can now be done by the Azure Bot Service.

The features include:

- Improvements to the channels to support new authentication features, such as new WebChat and DirectLineJS libraries to eliminate the need for the 6-digit magic code verification.
- Improvements to the Azure Portal to  add, delete, and configure connection settings to various OAuth identity providers.
- Support for a variety of out-of-the-box identity providers including Azure AD (both v1 and v2 endpoints), GitHub, and others.
- Updates to the C# and Node.js Bot Framework SDKs to be able to retrieve tokens, create OAuthCards and handle TokenResponse events.
- Samples for how to make a bot that authenticates to Azure AD.

You can extrapolate from the steps in this article to add such features to an existing bot. These sample bots demonstrate the new authentication features.

> [!NOTE]
> The authentication features also work with BotBuilder v3. However, this article covers just the v4 sample code.

### About this sample

You need to create an Azure bot resource, and you need to create a new Azure AD (either v1 or v2) application to allow your bot to access Office 365. The bot resource registers your bot's credentials; you need these credentials to test the authentication features, even when running your bot code locally.

> [!IMPORTANT]
> Whenever you register a bot in Azure, it gets assigned an Azure AD app. However, this app secures channel-to-bot access.
You need an additional AAD app for each application that you want the bot to be able to authenticate on behalf of the user.

This article describes a sample bot that connects to the Microsoft Graph using an Azure AD v1 or v2 token. It also covers how to create and register the associated Azure AD app. As part of this process, you'll use code from the [Microsoft/BotBuilder-Samples](https://github.com/Microsoft/BotBuilder-Samples) GitHub repo. This article covers these processes.

- **Create your bot resource**
- **Create an Azure AD application**
- **Register your Azure AD application with your bot**
- **Prepare the bot sample code**

Once you finish, you will have a bot running locally that can respond to a few simple tasks against an Azure AD application, such as checking and sending an email, or displaying who you are and who your manager is. To do this, your bot will use a token from an Azure AD application against the Microsoft.Graph library. You do not need to publish your bot to test the OAuth sign-in features; however, your bot will need a valid Azure app ID and password.

These authentication features work with other types of bots, too. However, this article uses a registration-only bot.

### Web Chat and Direct Line considerations

<!-- Summarized from: https://blog.botframework.com/2018/09/25/enhanced-direct-line-authentication-features/ -->

There are a couple of important security issues to consider when you use Azure Bot Service authentication with Web Chat.

1. Prevent impersonation, where an attacker makes the bot think they're someone else. In Web Chat, an attacker can impersonate someone else by changing the user ID of his Web Chat instance.

    To prevent this, make the user ID unguessable. When you enable enhanced authentication options in the Direct Line channel, Azure Bot Service can detect and reject any user ID change. The user ID on messages from Direct Line to your bot will always be the same as the one you initialized Web Chat with. Note that this feature requires the user ID to start with `dl_`.

1. Ensure the correct user is signed in. The user has two identities: their identity in a channel and their identity with the identity provider. In Web Chat, Azure Bot Service can guarantee that the sign-in process is completed in the same browser session as Web Chat itself.

    To enable this protection, start Web Chat with a Direct Line token that contains a list of trusted domains that can host the bot's Web Chat client. Then, statically specify the trusted domain (origin) list in the Direct Line configuration page.

Use Direct Line's `/v3/directline/tokens/generate` REST endpoint to generate a token for the conversation, and specify the user ID in the request payload. For a code sample, see the [Enhanced Direct Line Authentication Features](https://blog.botframework.com/2018/09/25/enhanced-direct-line-authentication-features/) blog post.

<!-- The eventual article about this should talk about the tokens/generate endpoint and its parameters: user, trustedOrigins, and [maybe] eTag.
Sample payload
{
  "user": {
    "id": "string",
    "name": "string",
    "aadObjectId": "string",
    "role": "string"
  },
  "trustedOrigins": [
    "string"
  ],
  "eTag": "string"
}
 -->

## Prerequisites

- Knowledge of [bot basics][concept-basics] and [managing state][concept-state].
- Knowledge of Azure and OAuth 2.0 development.
- Visual Studio 2017 or later, Node.js, npm, and git.
- One of these samples.

| Sample | BotBuilder version | Demonstrates |
|:---|:---:|:---|
| **Bot authentication** in [**CSharp**][cs-auth-sample] or [**JavaScript**][js-auth-sample] | v4 | OAuthCard support |
| **Bot authentication MSGraph** in [**CSharp**][cs-msgraph-sample] or [**JavaScript**][js-msgraph-sample] | v4 |  Microsoft Graph API support with OAuth 2 |

## Create your bot resource on Azure

Create a **Bot Channels Registration** using the [Azure Portal](https://portal.azure.com/).

## Create and register an Azure AD application

You need an Azure AD application that your bot can use to connect to the Microsoft Graph API.

For this bot you can use Azure AD v1 or v2 endpoints.
For information about the differences between the v1 and v2 endpoints, see the [v1-v2 comparison](https://docs.microsoft.com/azure/active-directory/develop/active-directory-v2-compare) and the [Azure AD v2.0 endpoint overview](https://docs.microsoft.com/azure/active-directory/develop/active-directory-appmodel-v2-overview).

### Create your Azure AD application

Use these steps to create a new Azure AD application. You can use the v1 or v2 endpoints with the app that you create.

> [!TIP]
> You will need to create and register the Azure AD application in a tenant to which you have admin rights.

1. Open the [Azure Active Directory][azure-aad-blade] panel in the Azure portal.
    If you are not in the correct tenant, click **Switch directory** to switch to the correct tenant. (For instruction on creating a tenant, see [Access the portal and create a tenant](https://docs.microsoft.com/en-us/azure/active-directory/fundamentals/active-directory-access-create-new-tenant).)
1. Open the **App registrations** panel.
1. In the **App registrations** panel, click **New application registration**.
1. Fill in the required fields and create the app registration.

   1. Name your application.
   1. Set the **Application type** to **Web app / API**.
   1. Set the **Sign-on URL** to `https://token.botframework.com/.auth/web/redirect`.
   1. Click **Create**.

      - Once it is created, it is displayed in a **Registered app** pane.
      - Record the **Application ID** value. You will use this value later as the _Client id_ when you register your Azure AD application with your bot.

1. Click **Settings** to configure your application.
1. Click **Keys** to open the **Keys** panel.

   1. Under **Passwords**, create a `BotLogin` key.
   1. Set its **Duration** to **Never expires**.
   1. Click **Save** and record the key value. You will use this value later as the _Client secret_ when you register your Azure AD application with your bot.
   1. Close the **Keys** panel.

1. Click **Required permissions** to open the **Required permissions** panel.

   1. Click **Add**.
   1. Click **Select an API**, then select **Microsoft Graph** and click **Select**.
   1. Click **Select permissions**. Choose the delegated permissions your application will use.

      > [!NOTE]
      > Any permission marked as **Requires Admin** will require both a user and a tenant admin to login, so for your bot tend to stay away from these.

      Select the following Microsoft Graph delegated permissions:
      - Read all users' basic profiles
      - Read user mail
      - Send mail as a user
      - Sign in and read user profile
      - View users' basic profile
      - View users' email address

   1. Click **Select**, then click **Done**.
   1. Close the **Required permissions** panel.

You now have an Azure AD v1 application configured.

### Register your Azure AD application with your bot

The next step is to register with your bot the Azure AD application that you created.

# [Azure AD v1](#tab/aadv1)

1. Navigate to your bot's resource page on the [Azure Portal](http://portal.azure.com/).
1. Click **Settings**.
1. Under **OAuth Connection Settings** near the bottom of the page, click **Add Setting**.
1. Fill in the form as follows:

    1. For **Name**, enter a name for your connection. You'll use this name in your bot code.
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

# [Azure AD v2](#tab/aadv2)

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

---

### Test your connection

1. Click on the connection entry to open the connection you just created.
1. Click **Test Connection** at the top of the **Service Provider Connection Setting** pane.
1. The first time, this should open a new browser tab listing the permissions your app is requesting and prompt you to accept.
1. Click **Accept**.
1. This should then redirect you to a **Test Connection to \<your-connection-name> Succeeded** page.

You can now use this connection name in your bot code to retrieve user tokens.

## Prepare the bot sample code

Depending on the sample you've chosen, you'll be working with either C# or Node.

| Sample | BotBuilder version | Demonstrates |
|:---|:---:|:---|
| **Bot authentication** in [**CSharp**][cs-auth-sample] or [**JavaScript**][js-auth-sample] | v4 | OAuthCard support |
| **Bot authentication MSGraph** in [**CSharp**][cs-msgraph-sample] or [**JavaScript**][js-msgraph-sample] | v4 |  Microsoft Graph API support with OAuth 2 |

1. Click on one of the sample links above and clone the github repository.
1. Follow the instructions on the GitHub readme page for how to run that particular bot (C# or Node).
1. If you are using the C# Bot-Authentication sample:

    1. Set the `ConnectionName` variable in the `AuthenticationBot.cs` file to the value you  used when you configured your bot's OAuth 2.0 connection setting.
    1. Set the `appId` value in the `BotConfiguration.bot` file to your bot's app ID.
    1. Set the `appPassword` value in the `BotConfiguration.bot` file to your bot's secret.

1. If you are using the Node/JS Bot-Authentication sample:

    1. Set the `CONNECTION_NAME` variable in the `bot.js` file to the value you  used when you configured your bot's OAuth 2.0 connection setting.
    1. Set the `appId` value in the `bot-authentication.bot` file to your bot's app ID.
    1. Set the `appPassword` value in the `bot-authentication.bot` file to your bot's secret.

    > [!IMPORTANT]
    > Depending on the characters in your secret, you may need to XML escape the password. For example, any ampersands (&) will need to be encoded as `&amp;`.

    ```json
    {
        "name": "BotAuthentication",
        "secretKey": "",
        "services": [
            {
            "appId": "",
            "id": "http://localhost:3978/api/messages",
            "type": "endpoint",
            "appPassword": "",
            "endpoint": "http://localhost:3978/api/messages",
            "name": "BotAuthentication"
            }
        ]
    }
    ```

    If you do not know how to get your **Microsoft app ID** and **Microsoft app password** values, you can either create a new password as described here:

    [bot-channels-registration-password](../bot-service-quickstart-registration.md#bot-channels-registration-password)
  
  Or retrieve the **Microsoft app ID** and **Microsoft app password** provisioned with the **Bot Channels Registration** from the deployement described here:

    [find-your-azure-bots-appid-and-appsecret](https://blog.botframework.com/2018/07/03/find-your-azure-bots-appid-and-appsecret)

    > [!NOTE]
    > You could now publish this bot code to your Azure subscription (right-click on the project and choose **Publish**), but it is not necessary for this tutorial. You would need to set up a publishing configuration that uses the application and hosting plan that you used when configuration the bot in the Azure Portal.

## Use the Emulator to test your bot

You will need to install the [Bot Emulator](https://github.com/Microsoft/BotFramework-Emulator) to test your bot locally. You can use the v3 or v4 Emulator.

1. Start your bot (with or without debugging).
1. Note the localhost port number for the page. You will need this information to interact with your bot.
1. Start the Emulator.
1. Connect to your bot. Please make sure the bot configuration uses **Microsoft app ID** and **Microsoft app password** when using authentication
1. Ensure that in Emulator settings, **Use a sign-in verification code for OAuthCards** is ticked and **ngrok** is enabled so the Azure Bot Service can return the token to the emulator when it becomes available.

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

When a user asks the bot to do something that requires the bot to have the user logged in, the bot can use an `OAuthPrompt` to initiate retrieving a token for a given connection. The `OAuthPrompt` creates a token retrieval flow that consists of:

1. Checking to see if the Azure Bot Service already has a token for the current user and connection. If there is a token, the token is returned.
1. If Azure Bot Service does not have a cached token, an `OAuthCard` is created which is a sign in button the user can click on.
1. After the user clicks on the `OAuthCard` sign in button, Azure Bot Service will either send the bot the user's token directly or will present the user with a 6-digit authentication code to enter in the chat window.
1. If the user is presented with an authentication code, the bot then exchanges this authentication code for the user's token.

The next couple of code snippets are taken from the `OAuthPrompt` showing how these steps work in the prompt.

### Check for a cached token

In this code, first the bot does a quick check to determine if the Azure Bot Service already has a token for the user (which is identified by the current Activity sender) and the given ConnectionName (which is the connection name used in configuration). Azure Bot Service will either already have a token cached or it will not. The call to GetUserTokenAsync performs this quick check. If Azure Bot Service has a token and returns it, the token can immediately be used. If Azure Bot Service does not have a token, this method will return null. In this case, the bot can send a customized OAuthCard for the user to login.

# [C#](#tab/csharp)

```csharp
// First ask Bot Service if it already has a token for this user
var token = await adapter.GetUserTokenAsync(turnContext, connectionName, null, cancellationToken).ConfigureAwait(false);
if (token != null)
{
    // use the token to do exciting things!
}
else
{
    // If Bot Service does not have a token, send an OAuth card to sign in
}
```

# [JavaScript](#tab/javascript)

```javascript
public async getUserToken(context: TurnContext, code?: string): Promise<TokenResponse|undefined> {
    // Get the token and call validator
    const adapter: any = context.adapter as any; // cast to BotFrameworkAdapter
    return await adapter.getUserToken(context, this.settings.connectionName, code);
}
```

---

### Send an OAuthCard to the user

You can customize the OAuthCard with whatever text and button text you want. The important pieces are:

- Set the `ContentType` to `OAuthCard.ContentType`.
- Set the `ConnectionName` property to the name of the connection you want to use.
- Include one button with a `CardAction` of `Type` `ActionTypes.Signin`; note that you do not need to specify any value for the sign in link.

At the end of this call, the bot needs to "wait for the token" to come back. This waiting takes place on the main Activity stream because there could be a lot the user needs to do to sign-in.

# [C#](#tab/csharp)

```csharp
private async Task SendOAuthCardAsync(ITurnContext turnContext, IMessageActivity message, CancellationToken cancellationToken = default(CancellationToken))
{
    if (message.Attachments == null)
    {
        message.Attachments = new List<Attachment>();
    }

    message.Attachments.Add(new Attachment
    {
        ContentType = OAuthCard.ContentType,
        Content = new OAuthCard
        {
            Text = "Please sign in",
            ConnectionName = connectionName,
            Buttons = new[]
            {
                new CardAction
                {
                    Title = "Sign In",
                    Text = "Sign In",
                    Type = ActionTypes.Signin,
                },
            },
        },
    });

    await turnContext.SendActivityAsync(message, cancellationToken).ConfigureAwait(false);
}
```

# [JavaScript](#tab/javascript)

```javascript
private async sendOAuthCardAsync(context: TurnContext, prompt?: string|Partial<Activity>): Promise<void> {
    // Initialize outgoing message
    const msg: Partial<Activity> =
        typeof prompt === 'object' ? {...prompt} : MessageFactory.text(prompt, undefined, InputHints.ExpectingInput);
    if (!Array.isArray(msg.attachments)) { msg.attachments = []; }

    const cards: Attachment[] = msg.attachments.filter((a: Attachment) => a.contentType === CardFactory.contentTypes.oauthCard);
    if (cards.length === 0) {
        // Append oauth card
        msg.attachments.push(CardFactory.oauthCard(
            this.settings.connectionName,
            this.settings.title,
            this.settings.text
        ));
    }

    // Send prompt
    await context.sendActivity(msg);
}
```

---

### Wait for a TokenResponseEvent

In this code the Bot  is waiting for a `TokenResponseEvent` (more about how this is routed to the Dialog stack is below). The `WaitForToken` method first determines if this event was sent. If it was sent, it can be used by the bot. If it was not, the `RecognizeTokenAsync` method takes whatever text was sent to the bot and passes it to `GetUserTokenAsync`. The reason for this is that some clients (like WebChat) do not need the Magic Code verification code and can directly send the Token in the `TokenResponseEvent`. Other clients still require the magic code (like Facebook or Slack). The Azure Bot Service will present these clients with a six digit magic code and ask the user to type this into the chat window. While not ideal, this is the 'fall back' behavior and so if `RecognizeTokenAsync` receives a code, the bot can send this code to the Azure Bot Service and get a token back. If this call also fails, then you can decide to report an error, or do something else. In most cases though, the bot will now have a user token.

If you look in the bot code of each sample, you'll see that `Event` and `Invoke` activities are also routed to the dialog stack.

# [C#](#tab/csharp)

```csharp
// This can be called when the bot receives an Activity after sending an OAuthCard
private async Task<TokenResponse> RecognizeTokenAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
{
    if (IsTokenResponseEvent(turnContext))
    {
        // The bot received the token directly
        var tokenResponseObject = turnContext.Activity.Value as JObject;
        var token = tokenResponseObject?.ToObject<TokenResponse>();
        return token;
    }
    else if (IsTeamsVerificationInvoke(turnContext))
    {
        var magicCodeObject = turnContext.Activity.Value as JObject;
        var magicCode = magicCodeObject.GetValue("state")?.ToString();

        var token = await adapter.GetUserTokenAsync(turnContext, _settings.ConnectionName, magicCode, cancellationToken).ConfigureAwait(false);
        return token;
    }
    else if (turnContext.Activity.Type == ActivityTypes.Message)
    {
        // make sure it's a 6-digit code
        var matched = _magicCodeRegex.Match(turnContext.Activity.Text);
        if (matched.Success)
        {
            var token = await adapter.GetUserTokenAsync(turnContext, _settings.ConnectionName, matched.Value, cancellationToken).ConfigureAwait(false);
            return token;
        }
    }

    return null;
}

private bool IsTokenResponseEvent(ITurnContext turnContext)
{
    var activity = turnContext.Activity;
    return activity.Type == ActivityTypes.Event && activity.Name == "tokens/response";
}

private bool IsTeamsVerificationInvoke(ITurnContext turnContext)
{
    var activity = turnContext.Activity;
    return activity.Type == ActivityTypes.Invoke && activity.Name == "signin/verifyState";
}
```

# [JavaScript](#tab/javascript)

```javascript
private async recognizeToken(context: TurnContext): Promise<PromptRecognizerResult<TokenResponse>> {
    let token: TokenResponse|undefined;
    if (this.isTokenResponseEvent(context)) {
        token = context.activity.value as TokenResponse;
    } else if (this.isTeamsVerificationInvoke(context)) {
        const code: any = context.activity.value.state;
        await context.sendActivity({ type: 'invokeResponse', value: { status: 200 }});
        token = await this.getUserToken(context, code);
    } else if (context.activity.type === ActivityTypes.Message) {
        const matched: RegExpExecArray = /(\d{6})/.exec(context.activity.text);
        if (matched && matched.length > 1) {
            token = await this.getUserToken(context, matched[1]);
        }
    }

    return token !== undefined ? { succeeded: true, value: token } : { succeeded: false };
}

private isTokenResponseEvent(context: TurnContext): boolean {
    const activity: Activity = context.activity;
    return activity.type === ActivityTypes.Event && activity.name === 'tokens/response';
}

private isTeamsVerificationInvoke(context: TurnContext): boolean {
    const activity: Activity = context.activity;
    return activity.type === ActivityTypes.Invoke && activity.name === 'signin/verifyState';
}
```

---

### Message controller

On subsequent calls to the bot, notice that the token is never cached by this sample bot. This is because the bot can always ask the Azure Bot Service for the token. This avoids the bot needing to manage the token life-cycle, refresh the token, etc, as Azure Bot Service does all of this for you.

### Further reading

- [Bot Framework additional resources](https://docs.microsoft.com/azure/bot-service/bot-service-resources-links-help) includes links for additional support.
- The [Bot Framework SDK](https://github.com/microsoft/botbuilder) repo has more information about repos, samples, tools, and specs associated with the Bot Builder SDK.

<!-- Footnote-style links -->

[Azure portal]: https://ms.portal.azure.com
[azure-aad-blade]: https://ms.portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/Overview
[aad-registration-blade]: https://ms.portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/RegisteredApps

[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md

[simple-dialog]: bot-builder-dialog-manage-conversation-flow.md
[dialog-prompts]: bot-builder-prompts.md
[component-dialogs]: bot-builder-compositcontrol.md

[cs-auth-sample]: https://aka.ms/v4cs-bot-auth-sample
[js-auth-sample]: https://aka.ms/v4js-bot-auth-sample
[cs-msgraph-sample]: https://aka.ms/v4cs-auth-msgraph-sample
[js-msgraph-sample]: https://aka.ms/v4js-auth-msgraph-sample

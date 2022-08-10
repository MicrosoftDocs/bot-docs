---
title: Add single sign-on to a bot
description: Learn how to add single sign-on (SSO) to your bot to reduce the number of times your users need to sign in to other services.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 08/08/2022
monikerRange: 'azure-bot-service-4.0'
---

# Add single sign-on to a bot

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article shows how to use the single sign-on (SSO) feature in a bot.
To do so, this feature uses a _consumer_ bot&mdash;also known as the _root_ or _parent_ bot&mdash;to interact with a _skill_ or _child_ bot.
This article uses the terms root bot and skill bot.

If you include SSO support, a user can sign into the root bot using an identity provider and won't need to sign in again when control passes to a skill.

The root and skill bots are separate bots, running on potentially different servers, each with its own separate memory and state.
For more information about skills, see [Skills overview](skills-conceptual.md) and [Implement a skill](skill-implement-skill.md).
For more information about user authentication, see [Bot Framework authentication basics](bot-builder-authentication-basics.md), [User authentication](bot-builder-concept-authentication.md), and [Add authentication to a bot](bot-builder-authentication.md).

> [!IMPORTANT]
> When you use Azure Bot Service authentication with _Web Chat_, there are some important security considerations you must keep in mind. For more information, see the [security considerations](../rest-api/bot-framework-rest-direct-line-3-0-authentication.md#security-considerations) section in the REST authentication article.

## Prerequisites

- Knowledge of [Bot basics][concept-basics], [Managing state][concept-state], and [About single sign-on](bot-builder-concept-sso.md).
- Knowledge of [The dialogs library][concept-dialogs] and how to [implement sequential conversation flow][simple-dialog] and [reuse dialogs][component-dialogs]
- Knowledge of Azure and OAuth 2.0 development.
- Visual Studio 2017 or later for .NET.
- The SSO with simple skill consumer and skill in [C#][cs-auth-sample].

## About the sample

This article references two bots: the **RootBot** and the **SkillBot**. The **RootBot** forwards activities to the **SkillBot**. They model this _typical_ skill scenario:

- A _root_ bot calls one or more skill bots.
- Both the root and skill bots implement the basic authentication described in the [Add authentication to a bot](bot-builder-authentication.md) article.
- The user logs into root bot.
- Because of the SSO and being already logged into the root bot, they're logged into the skill bot without requiring user interaction again.

For an overview of how the Bot Framework handles authentication, see [User authentication](bot-builder-concept-authentication.md).
For SSO background information, see [Single sign-on](bot-builder-concept-sso.md).

The **RootBot** supports SSO. It communicates with the **SkillBot** on behalf of the user, without the user being required to authenticate again into the _SkillBot.

For each project in the sample, you need the following:

1. An Azure AD application to register a bot resource in Azure.
1. An Azure AD identity provider application for authentication.
    > [!NOTE]
    > Currently, only the [Azure AD v2](/azure/bot-service/bot-builder-concept-identity-providers?view=azure-bot-service-4.0&tabs=adv2%2Cga2#azure-ad-v2-1&preserve-view=true) identity provider is supported.

## Create the Azure RootBot resource

1. Create an Azure bot resource in the [Azure portal][azure-portal] for the `RootBot`. Follow the steps described in
[Create an Azure bot resource](bot-builder-authentication.md#create-the-resource).
1. Copy and save the bot registration **app ID** and the **client secret**.

## Create the Azure AD identity for RootBot

The Azure AD is a cloud identity service that allows you to build applications that securely sign in users using industry standard protocols like OAuth2.0.

1. Create an identity application for the `RootBot` that uses Azure AD v2  to authenticate the user. Follow the steps described in [Create the Azure AD identity provider](bot-builder-authentication.md#create-the-azure-ad-identity-provider).

1. In the left pane, select **Manifest**.
1. Set `accessTokenAcceptedVersion` to 2.
1. Select **Save**.
1. In the left pane, select **Expose an API**.
1. In the right pane, select **Add a scope**.
1. On the far right _Add a scope_ section, select **Save and continue**.
1. In the displayed window, under _Who can consent?_, select **Admins and users**.
1. Enter the remaining required information.
1. Select **Add scope**.
1. Copy and save the scope value.

<a id="create-an-oauth-connection-setting-1"></a>

### Create an OAuth connection setting for RootBot

1. Create an Azure AD v2 connection in the `RootBot` bot registration and enter values as described in [Azure AD v2](/azure/bot-service/bot-builder-concept-identity-providers?view=azure-bot-service-4.0&tabs=adv2%2Cga2#azure-ad-v2-1&preserve-view=true) and the value described below.

1. Leave the **Token Exchange URL** empty.
1. In the **Scopes** box, enter the `RootBot` scope value you saved in the previous steps.
    > [!NOTE]
    > _Scopes_ contains the URL that the user initially signs in into the root bot, while _token exchange URL_ is left empty.
    >
    > As an example, let's assume that the root bot _appid_ is _rootAppId_ and the skill bot _appid_ is _skillAppId_. The root bot's _scopes_ will look like _api://rootAppId/customScope_, which is used to login the user. This root bot's _scopes_ is then exchanged with _api://skillAppId/customscope_ during SSO.
1. Copy and save the name of the connection.

## Create the Azure SkillBot resource

1. Create an Azure bot resource in the [Azure portal][azure-portal] for the `SkillBot`. Follow the steps described in
[Create an Azure bot resource](bot-builder-authentication.md#create-the-resource).
1. Copy and save the bot registration **app ID** and the **client secret**.

## Create the Azure AD identity for SkillBot

The Azure AD is a cloud identity service that allows you to build applications that securely sign in users using industry standard protocols like OAuth2.0.

1. Create an identity application for the `SkillBot` that uses Azure AD v2  to authenticate the bot. Follow the steps described in [Create the Azure AD identity provider](bot-builder-authentication.md#create-the-azure-ad-identity-provider).

1. In the left pane, select **Manifest**.
1. Set `accessTokenAcceptedVersion` to 2.
1. Select **Save**.
1. In the left pane, select **Expose an API**.
1. In the right pane, select **Add a scope**.
1. In the far right **Add a scope** section, select **Save and continue**.
1. In the displayed window, under _Who can consent?_ select **Admins and users**.
1. Enter the remaining required information.
1. Select **Add scope**.
1. Copy and save the scope value.
1. Select **Add a client application**. In the far right section, in the **Client ID** box, enter the **RootBot identity** app ID you saved before. Make sure you use the _RootBot_ identity and not the registration app ID.
1. Under **Authorized scope**, check the box by the scope value.
1. Select **Add application**.
1. In the navigation pane on the left, select **API permissions**. It's a best practice to explicitly set the API permissions for the app.

   1. In the right pane, select **Add a permission**.
   1. Select **Microsoft APIs** then **Microsoft Graph**.
   1. Choose **Delegated permissions** and make sure the permissions you need are selected. This sample requires the permissions listed below.
      > [!NOTE]
      > Any permission marked as **ADMIN CONSENT REQUIRED** will require both a user and a tenant admin to login.

      - **openid**
      - **profile**
      - **User.Read**
      - **User.ReadBasic.All**

   1. Select **Add permissions**.

### Create an OAuth connection setting for SkillBot

1. Create an Azure AD v2 connection in the `SkillBot` bot registration and enter values as described in [Azure AD v2](/azure/bot-service/bot-builder-concept-identity-providers?view=azure-bot-service-4.0&tabs=adv2%2Cga2#azure-ad-v2-1&preserve-view=true) and the values described below.
1. In the **Token Exchange URL** box, enter the `SkillBot` scope value you saved in the previous steps.
1. In the **Scopes** box, enter the following values separated by blank space: `profile` `User.Read` `User.ReadBasic.All` `openid`.

1. Copy and save to a file the name of the connection.

## Test the connection

1. Select on the connection entry to open the connection you created.
1. Select **Test Connection** at the top of the **Service Provider Connection Setting** pane.
1. The first time, this should open a new browser tab listing the permissions your app is requesting and prompt you to accept.
1. Select **Accept**.
1. This should then redirect you to a **Test Connection to \<your-connection-name> Succeeded** page.

For more information, see the [Azure Active Directory for developers (v1.0) overview](/azure/active-directory/azuread-dev/v1-overview) and [Microsoft identity platform (v2.0) overview](/azure/active-directory/develop/active-directory-appmodel-v2-overview).
For information about the differences between the v1 and v2 endpoints, see [Why update to Microsoft identity platform (v2.0)?](/azure/active-directory/develop/active-directory-v2-compare). For complete information, see [Microsoft identity platform (formerly Azure Active Directory for developers)](/azure/active-directory/develop/).

## Prepare the samples code

You must update the `appsettings.json` file in both samples as described below.

1. Clone the [SSO with Simple Skill Consumer and Skill][cs-auth-sample] sample from the GitHub repository.
1. Open the `SkillBot` project `appsettings.json` file. Assign the following values from the saved file:

    ```json
    {
        "MicrosoftAppId": "<SkillBot registration app ID>",
        "MicrosoftAppPassword": "<SkillBot registration password>",
        "ConnectionName": "<SkillBot connection name>",
        "AllowedCallers": [ "<RootBot registration app ID>" ]
    }

1. Open the `RootBot` project `appsettings.json` file. Assign the following values from the saved file:

    ```json
    {
        "MicrosoftAppId": "<RootBot registration app ID>",
        "MicrosoftAppPassword": "<RootBot registration password>",
        "ConnectionName": "<RootBot connection name>",
        "SkillHostEndpoint": "http://localhost:3978/api/skills/",
        "BotFrameworkSkills": [
                {
                "Id": "SkillBot",
                "AppId": "<SkillBot registration app ID>",
                "SkillEndpoint": "http://localhost:39783/api/messages"
                }
            ]
    }
    ```

## Test the samples

Use the following for testing:

- `RootBot` commands

  - `login` allows the user to sign into the Azure AD registration using the `RootBot`. Once signed in, SSO takes care of the sign-in into the `SkillBot` also. The user doesn't have to sign in again.
  - `token` displays the user's token.
  - `logout` logs the user out of the `RootBot`.

- `SkillBot` commands

  - `skill login` allows the `RootBot` to sign into the `SkillBot`, on behalf of the user. The user isn't shown a sign-in card, if already signed in, unless SSO fails.
  - `skill token` displays the user's token from the `SkillBot`.
  - `skill logout` logs the user out of the `SkillBot`

> [!NOTE]
> The first time users try SSO on a skill, they may be presented with an OAuth card to log in. This is because they haven't yet given consent to the skill's Azure AD app. To avoid this, they can grant admin consent for any graph permissions requested by the Azure AD app.

### [Emulator](#tab/eml)

If you haven't done so already, install the [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md). See also [Debug with the Emulator](../bot-service-debug-emulator.md).

You'll need to configure the Emulator for the bot sample login to work. Use the steps below:
as shown in [Configure the Emulator for authentication](../bot-service-debug-emulator.md#configure-the-emulator-for-authentication).

After you've configured the authentication mechanism, you can perform the actual bot sample testing.

1. In Visual Studio, open the `SSOWithSkills.sln` solution and configure it to start [debugging with multiple processes](/visualstudio/debugger/debug-multiple-processes?view=vs-2019#start-debugging-with-multiple-processes&preserve-view=true).
1. Start debugging locally on your machine.
Notice that in the`RootBot` project `appsettings.json` file you've the following settings:

    ```json
    "SkillHostEndpoint": "http://localhost:3978/api/skills/"
    "SkillEndpoint": "http://localhost:39783/api/messages"
    ```

    > [!NOTE]
    > These settings imply that, with both `RootBot` and `SkillBot` are running on the local machine. The Emulator communicates with `RootBot` on port 3978 and `RootBot` communicates with `SkillBot` on port 39783. As soon as you start debugging, two default browser windows open. One on port 3978 and the other on port 39783.

1. Start the Emulator.
1. When you connect to the bot, enter your `RootBot` registration app ID and password.
1. Type `hi` to start the conversation.
1. Enter **login**.  The `RootBot` will display a _Sign In to AAD_ authentication card.

    :::image type="content" source="media/how-to-auth/auth-bot-sso-test-root-signin.PNG" alt-text="Example of a sign-in card.":::

1. Select **Sign In**. The pop-up dialog _Confirm Open URL_ is displayed.

    :::image type="content" source="media/how-to-auth/auth-bot-test-confirm-url.PNG" alt-text="Screenshot of the 'open URL' confirmation message.":::

1. Select **Confirm**. You'll be logged in and the `RootBot` token is displayed.
1. Enter **token** to display the token again.

    :::image type="content" source="media/how-to-auth/auth-bot-sso-test-token.PNG" alt-text="Example of a message displaying the root token.":::

    Now you're ready to communicate with the `SkillBot`. Once you've signed using the `RootBot`, you don't need to provide your credentials again until you sign out. This demonstrates that SSO is working.

1. Enter **skill login** in the Emulator box. You'll not be asked to log in again. Instead the SkillBot token is displayed.
1. Enter **skill token** to display the token again.
1. Now you can enter **skill logout** to sign out of the `SkillBot`. Then enter **logout** to sign out of the `SimpleRootBoot`.

### [Web Chat](#tab/wct)

1. Deploy the root bot and the skill bot to Azure. For more information, see [Tutorial: Provision a bot in Azure](../tutorial-provision-a-bot.md) and [Tutorial: Publish a basic bot](../tutorial-publish-a-bot.md).
1. In your code editor, for example Visual Studio, replace the localhost addresses in the `RootBot` project `appsetting.js` file with the actual Azure addresses as shown below.

    ```json
    "SkillHostEndpoint": "https://<your root bot deployed name>.azurewebsites.net/api/skills"
    "SkillEndpoint": "https://<your skill bot deployed name>.azurewebsites.net/api/messages"
    ````

1. In your browser, go to the [Azure portal][azure-portal].
1. Open your root bot registration. In the left pane, select **Test in Web Chat**. The dialog window with your root bot is displayed with the bot greeting message.
1. Start the conversation with the bot by entering _hi_ for example. The bot will echo your message back.
1. Enter **login**. The `RootBot` will display a _Sign In to AAD_ authentication card.

    :::image type="content" source="media/how-to-auth/auth-bot-sso-test-webchat-root-signin.PNG" alt-text="Example of a sign-in card.":::

1. Select **Sign In**. A web page with a validation code is displayed.
1. To finish signing in, copy the code and enter it in the input box. The `RootBot` token is displayed.
1. Enter **token** to display the token again.

    :::image type="content" source="media/how-to-auth/auth-bot-sso-test-webchat-token.PNG" alt-text="Example of a message displaying the root token.":::

    Now you're ready to communicate with the `SkillBot`. Once you've signed in the `RootBot`, you don't need to provide your credentials again until you sign out. This demonstrates that SSO is working.

1. Enter **skill login**.  The SkillBot token is displayed.
1. Enter **skill token** to display the token again. This tells you that you're communicating with the `SkillBot` without the need to sign in again. SSO in action!
1. Now you can enter **skill logout** to sign out of the `SkillBot`. Then enter **logout** to sign out of the `SimpleRootBoot`.

---

## Additional information

The following time-sequence diagram applies to the samples used in the article and shows the interaction between the various components involved. _ABS_ stands for _Azure Bot Service_.

:::image type="content" source="media/how-to-auth/auth-bot-sso-sample-flow-diagram.PNG" alt-text="Sequence diagram illustrating the skill token flow.":::

1. The first time, the user enters the `login` command for the **RootBot**.
1. The **RootBot** sends an **OAuthCard** asking the user to sign in.
1. The user enters the authentication credentials that are sent to the **ABS** (Azure Bot Service).
1. The **ABS** sends the authentication token, generated based on the user's credentials, to the **RootBot**.
1. The **RootBot** displays the root token for the user to see.
1. The user enters the `skill login` command for the **SkillBot**.
1. The **SkillBot** sends an **OAuthCard** to the **RootBot**.
1. The **RootBot** asks for an **exchangeable token** from **ABS**.
1. SSO sends the **SkillBot** **skill token** to the **RootBot**.
1. The **RootBot** displays the skill token for the user to see. Notice that the skill token was generated without the user having to sign in the **SKillBot**. This is because of the SSO.

To see how the token exchange happens, please refer to the example shown below. The function can be found in [TokenExchangeSkillHandler.cs](https://github.com/microsoft/BotBuilder-Samples/blob/master/experimental/sso-with-skills/RootBot/TokenExchangeSkillHandler.cs).

[!code-csharp[sso-token-exchange](~/../botbuilder-samples/experimental/sso-with-skills/RootBot/TokenExchangeSkillHandler.cs?range=92-136)]

[azure-portal]: https://ms.portal.azure.com
[azure-aad-blade]: https://ms.portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/Overview
[aad-registration-blade]: https://ms.portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/RegisteredAppsPreview

[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md

[simple-dialog]: bot-builder-dialog-manage-conversation-flow.md
[dialog-prompts]: bot-builder-prompts.md
[component-dialogs]: bot-builder-compositcontrol.md

[cs-auth-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/sso-with-skills
[js-auth-sample]: https://github.com/Microsoft/BotBuilder-Samples/blob/main/samples/javascript_nodejs/18.bot-authentication
[python-auth-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/18.bot-authentication

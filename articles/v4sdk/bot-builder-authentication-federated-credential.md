---
title: Implement Authentication with Federated Identity Credentials
description: Learn how to integrate user authentication using federated identity credentials
author: kparihar
ms.author: kparihar
manager: kunsingh
ms.reviewer: kunsingh
ms.topic: how-to
ms.service: azure-ai-bot-service
ms.date: 10/06/2024
ms.custom:
  - abs-meta-21q1
  - evergreen
monikerRange: 'azure-bot-service-4.0'
---



# Implement Authentication with Federated Identity Credentials


[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

For an overview of how the Bot Framework handles this kind of authentication, see [User authentication](bot-builder-concept-authentication.md).

This article covers how to:

- Create an Azure Bot resource.
- Create the Microsoft Entra ID identity provider and use federated credentials flow.
- Register the Microsoft Entra ID identity provider with the bot for user authentication using federated credential flow.
- Prepare the bot code.

Once you finish this article, you have a bot that can respond to a few simple tasks.

[!INCLUDE [java-python-sunset-alert](../includes/java-python-sunset-alert.md)]


## Prerequisites

- Knowledge of [bot basics][concept-basics], [managing state][concept-state], the [dialogs library][concept-dialogs], and
how to [implement sequential conversation flow][simple-dialog], and how to [reuse dialogs][component-dialogs].
- Knowledge of Azure and OAuth 2.0 development.
- Visual Studio 2017 or later for .NET.
- Node.js for JavaScript.
- Python [3.8+](https://www.python.org/downloads/release/python-383/) for Python.
- One of the samples listed.

  | Sample | BotBuilder version | Demonstrates |
  |:---|:---:|:---|
  | **Authentication** in [**C#**][cs-auth-sample]| v4 | OAuthCard support |

  To run the samples referenced in this article, you need:

  - A Microsoft Entra ID application with which to register a bot resource in Azure. This application allows the bot to access an external secured resource, such as Microsoft Graph. It also allows the user to communicate with the bot via several channels such as Web Chat.
  - A separate Microsoft Entra ID application to function as the identity provider. This application provides the credentials needed to establish an OAuth connection between the bot and the secured resource. Notice that this article uses Active Directory as an identity provider. Many other providers are also supported.

> [!IMPORTANT]
> Whenever you register a bot in Azure, it gets assigned an Microsoft Entra ID application. However, this application secures channel-to-bot access. You need an additional Microsoft Entra ID application for each external secured resource you want the bot to access on behalf of the user.


## Create the Azure Bot resource


Create the **Azure Bot** resource, which allows you to register your bot with the Azure AI Bot Service.

[!INCLUDE [bot-resource-type-tip](../includes/bot-resource-type-tip.md)]

1. Go to the [Azure portal](https://portal.azure.com/).
1. In the right pane, select **Create a resource**.
1. In the search box enter `bot`, then press **Enter**.
1. Select the **Azure Bot** card.

    :::image type="content" source="../media/azure-manage-a-bot/azure-bot-resource.png" alt-text="Select Azure bot resource":::

1. Select **Create**.
1. Enter values in the required fields and review and update settings.

   1. Provide information under **Project details**. Select whether your bot has global or local data residency. Currently, the local data residency feature is available for resources in the "westeurope" and "centralindia" region. For more information, see [Regionalization in Azure AI Bot Service](bot-builder-concept-regionalization.md).

      :::image type="content" source="../media/azure-bot-resource/azure-bot-project-details.png" alt-text="The project details settings for an Azure Bot resource":::

   1. Provide information under **Microsoft App ID**. Select how your bot identity is managed in Azure and whether to create a new identity or use an existing one.

      :::image type="content" source="../media/azure-bot-resource/azure-bot-ms-app-st.png" alt-text="The Microsoft app ID settings for an Azure Bot resource":::

1. Select **Review + create**.
1. If the validation passes, select **Create**.
1. Once the deployment completes, select **Go to resource**. You should see the bot and related resources listed in the resource group you selected.
1. If you don't already have the Bot Framework SDK, select **Download from GitHub** to learn how to consume the packages for your preferred language.

    :::image type="content" source="../media/azure-manage-a-bot/azure-bot-create-sdk.png" alt-text="Create bot in SDK":::

You're now ready to build your bot with the Bot Framework SDK.


### Create a User Assigned Managed Identity

Create the **User Assigned Managed Identity** resource, which allows you to set up an identity that is used as a trust mechanism to obtain access tokens from the Microsoft Entra application.


1. Go to the [Azure portal](https://portal.azure.com/).
1. In the right pane, select **Create a resource**.
1. In the search box enter `Managed Identities`, then press **Enter**.
1. Select the **User Assigned Managed Identity** card.

    :::image type="content" source="../media/azure-manage-a-bot/managed-identity.png" alt-text="Select User Assigned Managed Identity":::

1. Select **Create**.
1. Enter values in the required fields and review and update settings.

   1. Provide information under **Project details**. Select whether your identity has global or local data residency.

      :::image type="content" source="../media/azure-manage-a-bot/user-mi-create.png" alt-text="The project details settings for an Managed Identity":::

1. Select **Review + create**.
1. If the validation passes, select **Create**.
1. Once the deployment completes, select **Go to resource**. You should see the managed identity and related resources listed in the resource group you selected.


### To create a new Federated Credentials

The owner of the bot's App Service resource can add a new trust:

1. Go to the Azure Bot resource blade for your bot.
1. Go to the bot's **Configuration** blade.
1. Select **Manage**, next to **Microsoft App ID**, to go to the **Certificates + secrets** blade for the app service.
1. On the **Certificates + secrets** blade, select the **Federated Credentials** tab and Add Credentials (+).

    :::image type="content" source="../media/azure-manage-a-bot/entra-fic-creds.png" alt-text="Create a Federated Credentials.":::

1. On Add Credentials page, Choose the **Federated credential scenario** to **Customer Managed Keys**

   :::image type="content" source="../media/azure-manage-a-bot/entra-fic-creds-scenario-mi.png" alt-text="Select Federated Credentials Scenario.":::

1. Enter values in the required fields and review and update settings
    1. Provide information under **Select a managed identity**.
        :::image type="content" source="../media/azure-manage-a-bot/entra-fic-creds-scenario-select.png" alt-text="Select a Managed Identity Section":::
        1. On the **Select a managed identity** blade:
        1. Select your subscription.
        1. For **User assigned managed identities**, select the managed identity created earlier.
        1. Select **Select** to use this identity for your bot. 
            :::image type="content" source="../media/azure-manage-a-bot/entra-fic-creds-scenario-select-search.png" alt-text="Select a Managed Identity":::
    1. Provide information under **Credential details**.
        :::image type="content" source="../media/azure-manage-a-bot/entra-fic-creds-scenario-cred.png" alt-text="Enter Credential details":::

1. Select **Add** to add the credential.




### To update your Bot

> [!NOTE]
> The Bot Framework SDK version 4.22.8 or later supports the use of Federated credentials.


1. Upgrade the Bot Framework SDK version to 4.22.8 or later.
1. To enable the Federated Credential for your bot, add the following code to your Startup.cs file:

    ```csharp
    services.AddSingleton<ServiceClientCredentialsFactory>(new FederatedServiceClientCredentialsFactory(
        Configuration["MicrosoftAppId"],
        Configuration["MicrosoftAppClientId"],
        Configuration["MicrosoftAppTenantId"]));
    ```

1. Follow these steps to include identity information in your bot’s configuration file. The specific file and steps might vary depending on the programming language you're using to create the bot.

    > [!IMPORTANT]
    > The Java version of the Bot Framework SDK only supports multi-tenant bots.
    > The C#, JavaScript, and Python versions support all three application types for managing the bot's identity.
    
    | Language   | File name              | Notes                                                                                                               |
    |:-----------|:-----------------------|:--------------------------------------------------------------------------------------------------------------------|
    | C#         | appsettings.json       | Supports all three application types for managing your bot's identity.                                              |
    | JavaScript | .env                   | Supports all three application types for managing your bot's identity.                                              |
    | Java       | application.properties | Only supports multitenant bots.                                                                                    |
    | Python     | config.py              | Supports all three application types for managing your bot's identity. |
    
The identity information you need to add depends on the bot's application type.
Provide the following values in your configuration file.

| Property               | Value                    |
|:-----------------------|:-------------------------|
| `MicrosoftAppType`     | `SingleTenant/MultiTenant`           |
| `MicrosoftAppId`       | The bot's app ID.        |
| `MicrosoftAppClientId` | The User Managed Identity password.  |
| `MicrosoftAppTenantId` | The bot's app tenant ID. |



### To get your app or tenant ID

To get your bot's app or tenant ID:

1. Go to the Azure Bot resource blade for your bot.
1. Go to the bot's **Configuration** blade.
    From this blade, you can copy the bot's **Microsoft App ID** or **App Tenant ID**.


### To update your app service

If your bot uses an existing App Service resource (web app) and is a _single_ or _multitenant_ application, you might need to update its app service.

1. Go to the App Service blade for your bot's web app.
1. Under **Settings**, select **Identity**.
1. On the **Identity** blade, select the **User assigned** tab and **Add** (+).
1. On the **Add user assigned managed identity** blade:
    1. Select your subscription.
    1. For **User assigned managed identities**, select the managed identity created earlier.
    1. Select **Add** to use this identity for your bot.

        :::image type="content" source="../media/how-to-create-single-tenant-bot/app-service-managed-identity.png" alt-text="The App Service Identity blade with the managed identity for the bot selected.":::






## Microsoft Entra ID identity service

The Microsoft Entra ID is a cloud identity service that allows you to build applications that securely sign in users using industry standard protocols like OAuth 2.0.
1. Microsoft identity platform (v2.0). Also known as the **Microsoft Entra ID** endpoint, which is an evolution of the Azure AD platform (v1.0).It lets you build applications that sign in to all Microsoft identity providers and obtain tokens to call Microsoft APIs, like Microsoft Graph, or other developer-built APIs. For more information, see the [Microsoft identity platform (v2.0) overview](/azure/active-directory/develop/active-directory-appmodel-v2-overview).

For information about the differences between the v1 and v2 endpoints, see [Why update to Microsoft identity platform (v2.0)?](/azure/active-directory/develop/active-directory-v2-compare). For complete information, see [Microsoft identity platform (formerly Microsoft Entra ID for developers)](/azure/active-directory/develop/).



### Create the Microsoft Entra ID identity provider

This section shows how to create a Microsoft Entra ID identity provider that uses OAuth 2.0 to authenticate the bot. You can use Microsoft Entra ID endpoints.

> [!TIP]
> You'll need to create and register the Microsoft Entra ID application in a tenant in which you can consent to delegate permissions requested by an application.

1. Open the [Microsoft Entra ID][azure-aad-blade] panel in the Azure portal.
    If you aren't in the correct tenant, select **Switch directory** to switch to the correct tenant. (For information on how to create a tenant, see [Access the portal and create a tenant](/azure/active-directory/fundamentals/active-directory-access-create-new-tenant).)
1. Open the **App registrations** panel.
1. In the **App registrations** panel, select **New registration**.
1. Fill in the required fields and create the app registration.

   1. Name your application.
   1. Select the **Supported account types** for your application. (Any of these options work with this sample.)
   1. For the **Redirect URI**, select **Web** and set the URL to one of the [supported OAuth redirect URLs](../ref-oauth-redirect-urls.md).

   1. Select **Register**.

      - Once created, Azure displays the **Overview** page for the app.
      - Record the **Application (client) ID** value. You use this value later as the _client ID_ when you create the connection string and register the Microsoft Entra ID provider with the bot registration.
      - Record the **Directory (tenant) ID** value. You use this value to register this provider application with your bot.

1. In the navigation pane, select **Certificates & secrets** to create a secret for your application.
    1. Under **Federated Credentials**, select **Add Credentials**.

        :::image type="content" source="../media/azure-manage-a-bot/entra-fic-creds.png" alt-text="Create a Federated Credentials.":::
    
    1. On Add Credentials page, Choose the **Federated credential scenario** to **Other Issuer**

       :::image type="content" source="../media/azure-manage-a-bot/entra-fic-creds-scenario-others.png" alt-text="Select Federated Credentials Other Issuer Scenario.":::
    
    1. Enter values in the required fields and review and update settings
        1. Provide information under **Connect your account**.
          
            :::image type="content" source="../media/azure-manage-a-bot/entra-fic-creds-scenario-others-account.png" alt-text="Connect your account":::
            
            1. **_Issuer_** : `https://login.microsoftonline.com/{customer-tenant-ID}/v2.0`
            1. **_Subject Identifier_** : /eid1/c/pub/t/{base64 encoded customer tenant ID}/a/{base64 encoded 1-P app client ID}/{unique-identifier-for-projected-identity}
               - The following table contains Base64url encoded byte-array representation of supported First party application IDs. Use this value which represents our First party app.
               
                 | Encoded Value | Description |
                 |--|--|
                 |9ExAW52n_ky4ZiS_jhpJIQ |Base64url encoded of Bot Service Token Store|
                 |ND1y8_Vv60yhSNmdzSUR_A |Base64url encoded of Bot Framework Dev Portal|

               - The following table contains Base64url encoded byte-array representation of some supported tenant IDs. Use the value which represents tenant of your app.

                 | Encoded Value | Description |
                 |--|--|
                 | v4j5cvGGr0GRqy180BHbRw | Base64url encoded MSIT tenant ID (aaaabbbb-0000-cccc-1111-dddd2222eeee) |
                 | PwFflyR_6Een06vEdSvzRg |Base64url encoded PME tenant ID (bbbbcccc-1111-dddd-2222-eeee3333ffff)  |
                 | 6q7FzcUVtk2wefyt0lBdwg | Base64url encoded Torus tenant ID (ccccdddd-2222-eeee-3333-ffff4444aaaa)|
                 | IRngM2RNjE-gVVva_9XjPQ|Base64url encoded AME tenant ID (ddddeeee-3333-ffff-4444-aaaa5555bbbb)|
               
                The Primary Identity service owners calculate it once and provide it to their consumers.
            1. **_Audience_** : api://AzureADTokenExchange (Use Cloud specific values)
            1. **_Unique-identifier-for-projected-identity_** : The token has the same value specified as the Unique Identifier in the OAuth Connection Setting.
    
        1. Provide information under **Credential details**.
            
            :::image type="content" source="../media/azure-manage-a-bot/entra-fic-creds-scenario-cred.png" alt-text="Enter Credential details":::
        
        1. Select **Add** to add the credential.


1. In the navigation pane, select **API permissions** to open the **API permissions** panel. It's a best practice to explicitly set the API permissions for the app.

   1. Select **Add a permission** to show the **Request API permissions** pane.
   1. For this sample, select **Microsoft APIs** and **Microsoft Graph**.
   1. Choose **Delegated permissions** and make sure the permissions you need are selected. This sample requires theses permissions.

      > [!NOTE]
      > Any permission marked as **ADMIN CONSENT REQUIRED** will require both a user and a tenant admin to login, so for your bot tend to stay away from these.
        - **openid**
        - **profile**
        - **Mail.Read**
        - **Mail.Send**
        - **User.Read**
        - **User.ReadBasic.All**

   1. Select **Add permissions**. (The first time a user accesses this app through the bot, they need to grant consent.)

You now have a Microsoft Entra ID application configured.


> [!NOTE]
> You'll assign the **Application (client) ID**, when you create the connection string and register the identity provider with the bot registration. See next section.

### Register the Microsoft Entra ID identity provider with the bot

The next step is to register your identity provider with your bot.
> [!NOTE]
> Single Tenant Entra Application is only support for **AAD v2 with Federated Credentials** service provider.
> Support for multi-tenant apps will be added in future.

1. Open your bot's Azure Bot resource page in the [Azure portal][azure-portal].
1. Select **Settings**.
1. Under **OAuth Connection Settings** near the bottom of the page, select **Add Setting**.
1. Fill in the form as follows:

    1. **Name**. Enter a name for your connection. You use it in your bot code.
    1. **Service Provider**. Select **AAD v2 with Federated Credentials** to display service provider specific fields.
    1. **Client id**. Enter the application (client) ID you recorded for your Microsoft Entra ID identity provider(**Only Single Tenant Supported**).
    1. **Unique Identifier**. Enter the unique identifier you recorded for your Microsoft Entra ID identity provider while creating federated credentials.
    1. **Token Exchange URL**. Leave it blank because it's used for SSO in Microsoft Entra ID only.
    1. **Tenant ID**. Enter the **directory (tenant) ID** that you recorded earlier for your Microsoft Entra ID app or **common** depending on the supported account types selected when you created the Azure DD app. To decide which value to assign, follow these criteria:

        - When creating the Microsoft Entra ID app, if you selected **Accounts in this organizational directory only (Microsoft only - Single tenant)**, enter the tenant ID you recorded earlier for the Microsoft Entra ID app.
        - However, if you selected **Accounts in any organizational directory (Any Microsoft Entra ID directory - Multi tenant and personal Microsoft accounts e.g. Xbox, Outlook.com)** or **Accounts in any organizational directory(Microsoft Entra ID directory - Multi tenant)**, enter `common` instead of a tenant ID. Otherwise, the Microsoft Entra ID app verifies through the tenant whose ID was selected and exclude personal Microsoft accounts.

        This is the tenant associated with the users who can be authenticated. For more information, see [Tenancy in Microsoft Entra ID](/azure/active-directory/develop/single-and-multi-tenant-apps).

    1. For **Scopes**, enter the names of the permission you chose from the application registration. For testing purposes, you can just enter:
       `openid profile`.

        > [!NOTE]
        > For Microsoft Entra ID, **Scopes** field takes a case-sensitive, space-separated list of values.

1. Select **Save**.

---

### Test your connection

1. Select on the connection entry to open the connection you created.
1. Select **Test Connection** at the top of the **Service Provider Connection Setting** pane.
1. The first time, this should open a new browser tab listing the permissions your app is requesting and prompt you to accept.
1. Select **Accept**.
1. This should then redirect you to a **Test Connection to \<your-connection-name> Succeeded** page.

You can now use this connection name in your bot code to retrieve user tokens.

## Prepare the bot code

You need your bot's app ID and password to complete this process.

### [C#](#tab/csharp)

1. Clone from the GitHub repository the sample you want to work with: [**Bot authentication**][cs-auth-sample].
1. Update **appsettings.json**:

    - Set `ConnectionName` to the name of the OAuth connection setting you added to your bot.
    - Set `MicrosoftAppId` and `MicrosoftAppClientId` to your bot's app ID and app secret.
      
    [!code-json[appsettings](~/../botbuilder-samples/samples/csharp_dotnetcore/86.bot-authentication-fic/appsettings.json)]

    To use OAuth in bot with data-residency in public cloud, you must add the following configurations in your appsettings
    ```json
    "OAuthUrl": "<Regional-OAuth-Uri>",
    "ToChannelFromBotOAuthScope": "https://api.botframework.com",
    "ToChannelFromBotLoginUrlTemplate": "https://api.botframework.com",
    "PublicAzureChannel": "https://api.botframework.com",
    "ToBotFromChannelOpenIdMetadataUrl": "https://login.botframework.com/v1/.well-known/openidconfiguration",
    "ToBotFromEmulatorOpenIdMetadataUrl": "https://login.microsoftonline.com/common/v2.0/.well-known/openid-configuration",
    "ToBotFromChannelTokenIssuer": "https://api.botframework.com",
    "ToChannelFromBotLoginUrl": "https://login.microsoftonline.com/botframework.com",
    ```
    
    Where _\<Regional-OAuth-Url>_ is one of the following URIs:
    
    |URI|Description|
    |:-|:-|
    |`https://europe.api.botframework.com`|For public-cloud bots with data residency in Europe.|
    |`https://unitedstates.api.botframework.com`|For public-cloud bots with data residency in the United States.|
    |`https://india.api.botframework.com`|For public-cloud bots with data residency in India.|

1. Update **Startup.cs**:

    To use OAuth in _non-public Azure clouds_, like the government cloud, you must add the following code in the **Startup.cs** file.

    ```csharp
    string uri = "<uri-to-use>";
    MicrosoftAppCredentials.TrustServiceUrl(uri);
    OAuthClientConfig.OAuthEndpoint = uri;

    ```

    Where _\<uri-to-use>_ is one of the following URIs:

    |URI|Description|
    |:-|:-|
    |`https://api.botframework.azure.us`|For United States government-cloud bots without data residency.|
    |`https://api.botframework.com`|For public-cloud bots without data residency. This is the default URI and doesn't require a change to **Startup.cs**.|

> [!NOTE]
> You could now publish the bot code to your Azure subscription (right-select on the project and choose **Publish**), but it's not necessary for this article. You would need to set up a publishing configuration that uses the application and hosting plan that you used when configuration the bot in the Azure portal.


### Testing

After you configured the authentication mechanism, you can perform the actual bot sample testing.

> [!NOTE]
> You may be asked to enter a _magic code_, because the way the bot sample is implemented. This magic code is part of the [RFC#7636](https://tools.ietf.org/html/rfc7636#page-5) and is there to add an extra security element. By removing the magic code, there's an increased security risk. This can be mitigated using Direct Line with enhanced authentication enabled. For more information, see [Bot Framework enhanced authentication](bot-builder-security-enhanced.md).

## Authentication example

In the **Bot authentication** sample, the dialog is designed to retrieve the user token after the user is logged in.

:::image type="content" source="media/how-to-auth/auth-bot-test.png" alt-text="Sample conversation with the authentication sample bot.":::

---

## Additional information

When a user asks the bot to do something that requires the bot to have the user logged in, the bot can use an `OAuthPrompt` to initiate retrieving a token for a given connection. The `OAuthPrompt` creates a token retrieval flow that consists of:

1. Checking to see if the Azure AI Bot Service already has a token for the current user and connection. If there's a token, the token is returned.
1. If Azure AI Bot Service doesn't have a cached token, an `OAuthCard` is created which is a sign-in button the user can select on.
1. After the user selects on the `OAuthCard` sign-in button, Azure AI Bot Service will either send the bot the user's token directly or present the user with a six-digit authentication code to enter in the chat window.
1. If the user is presented with an authentication code, the bot then exchanges this authentication code for the user's token.

The following sections describe how the sample implements some common authentication tasks.

### Use an OAuth prompt to sign the user in and get a token

### [C#](#tab/csharp)

:::image type="content" source="media/how-to-auth/architecture.png" alt-text="Architecture diagram for the C# sample.":::

**Dialogs\MainDialog.cs**

Add an OAuth prompt to **MainDialog** in its constructor. Here, the value for the connection name was retrieved from the **appsettings.json** file.

[!code-csharp[Add OAuthPrompt](~/../botbuilder-samples/samples/csharp_dotnetcore/86.bot-authentication-fic/Dialogs/MainDialog.cs?range=23-31)]

Within a dialog step, use `BeginDialogAsync` to start the OAuth prompt, which asks the user to sign in.

- If the user is already signed in, a token response event is generated without prompting the user. Otherwise, the user is prompted to sign in. After the user attempts to sign in, the Azure AI Bot Service sends the token response event.

[!code-csharp[Use the OAuthPrompt](~/../botbuilder-samples/samples/csharp_dotnetcore/86.bot-authentication-fic/Dialogs/MainDialog.cs?range=49)]

Within the following dialog step, check for the presence of a token in the result from the previous step. If it's not null, the user successfully signed in.

[!code-csharp[Get the OAuthPrompt result](~/../botbuilder-samples/samples/csharp_dotnetcore/86.bot-authentication-fic/Dialogs/MainDialog.cs?range=54-56)]

---

### Wait for a TokenResponseEvent

When you start an OAuth prompt, it waits for a token response event, from which it retrieves the user's token.

### [C#](#tab/csharp)

**Bots\AuthBot.cs**

**AuthBot** derives from `ActivityHandler` and explicitly handles token response event activities. Here, we continue the active dialog, which allows the OAuth prompt to process the event and retrieve the token.

[!code-csharp[OnTokenResponseEventAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/86.bot-authentication-fic/Bots/AuthBotFIC.cs?range=31-37)]

---

### Log the user out

It's best practice to let users explicitly sign out, instead of relying on the connection to time out.

### [C#](#tab/csharp)

**Dialogs\LogoutDialog.cs**

[!code-csharp[Allow sign out](~/../botbuilder-samples/samples/csharp_dotnetcore/86.bot-authentication-fic/Dialogs/LogoutDialog.cs?range=45-63&highlight=11)]

---


### Further reading

- [Bot Framework other resources](../bot-service-resources-links-help.md) includes links for more support.
- The [Bot Framework SDK](https://github.com/microsoft/botbuilder) repo has more information about repos, samples, tools, and specs associated with the Bot Builder SDK.


[azure-portal]: https://ms.portal.azure.com
[azure-aad-blade]: https://ms.portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/Overview

[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md

[simple-dialog]: bot-builder-dialog-manage-conversation-flow.md
[component-dialogs]: bot-builder-compositcontrol.md

[cs-auth-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/86.bot-authentication-fic
[encoder-helper-code]: [https://dotnetfiddle.net/dpTlF6]

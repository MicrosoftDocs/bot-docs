---
title: Identity providers in Azure Bot Service
description: Learn about identity providers in Azure Bot Service.
keywords: azure bot service, authentication, bot framework token service
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 09/10/2021
monikerRange: 'azure-bot-service-4.0'
---

# Identity providers

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

An identity provider authenticates user or client identities and issues consumable security tokens. It provides user authentication as a service.

Client applications, such as web applications, delegate authentication to a trusted identity provider. Such client applications are said to be federated, that is, they use federated identity. For more information, see [Federated Identity pattern](/azure/architecture/patterns/federated-identity).

Using a trusted identity provider:

- Enables single sign-on (SSO) features, allowing an application to access multiple secured resources.
- Facilitates connections between cloud computing resources and users, decreasing the need for users to reauthenticate.

## Single sign-on

Single sign-on refers to an authentication process that lets a user log on to a system once with a single set of credentials to access multiple applications or services.

A user logs in with a single ID and password to gain access to any of several related software systems. For more information, see [Single sign on](./bot-builder-concept-sso.md).

Many identity providers support a sign-out operation that revokes the user token and terminates access to the associated applications and services.

> [!IMPORTANT]
> SSO enhances usability by reducing the number of times a user must enter credentials. It also provides better security by decreasing the potential attack surface.

## Azure Active Directory identity provider

Azure Active Directory (Azure AD) is the identity service in Microsoft Azure that provides identity management and access control capabilities. It allows you to securely sign in users using industry standard protocols like **OAuth2.0**.

You can choose from two Active Directory identity provider implementations, which have different settings as shown below.

> [!NOTE]
> You use the settings described here when configuring the **OAuth Connection Settings** in the Azure bot registration application. For more information, see [Add authentication to a bot](bot-builder-authentication.md).

# [Azure AD v1](#tab/adv1)

### Azure AD v1

You use the settings shown to configure the Azure AD developer platform (v1.0), also known as **Azure AD v1** endpoint. This allows you to build  apps that securely sign in users with a Microsoft work or school account.
For more information, see [Azure Active Directory for developers (v1.0) overview](/azure/active-directory/azuread-dev/v1-overview).

| Property               | Description                           | Value                                                |
|------------------------|---------------------------------------|------------------------------------------------------|
| **Name**               | The name of your connection           | \<Your name for the connection\> <img width="300px"> |
| **Service Provider**   | Azure AD Identity provider            | `Azure Active Directory`                             |
| **Client ID**          | Azure AD identity provider app ID     | \<Azure AD provider app ID\>                         |
| **Client secret**      | Azure AD identity provider app secret | \<Azure AD provider app secret\>                     |
| **Grant Type**         |                                       | `authorization_code`                                 |
| **Login URL**          |                                       | `https://login.microsoftonline.com`                  |
| **Tenant ID**          |                                       | <directory (tenant) ID> or `common`. See note.       |
| **Resource URL**       |                                       | `https://graph.microsoft.com/`                       |
| **Scopes**             |                                       | _leave this blank_                                   |
| **Token Exchange URL** | Used for SSO in Azure AD v2           | See note below                                       |

> [!NOTE]
>
> - Enter the **tenant ID** you recorded for the Azure AD identity provider app, if you selected one of the following:
>   - *Accounts in this organizational directory only (Microsoft only - Single tenant)*
>   - *Accounts in any organizational directory(Microsoft AAD directory - Multi tenant)*
> - Enter `common`  if you selected *Accounts in any organizational directory (Any Azure AD directory - Multi tenant and personal Microsoft accounts e.g. Skype, Xbox, Outlook.com)*. Otherwise, the Azure AD identity provider app will verify through the tenant whose ID was selected and exclude personal Microsoft accounts.
> - The **Token Exchange URL** is left blank for the root bot but is populated for the skill bot. See [Create an OAuth connection settings](../v4sdk/bot-builder-authentication-sso.md#create-an-oauth-connection-settings-1) to learn how to get its value.

# [Azure AD v2](#tab/adv2)

### Azure AD v2

You use the settings shown to configure the Microsoft identity platform (v2.0), also known as **Azure AD v2** endpoint, which is an evolution of the Azure AD platform (v1.0). It allows a bot to get tokens to call Microsoft APIs, such as Microsoft Graph or custom APIs.
For more information, see the [Microsoft identity platform (v2.0) overview](/azure/active-directory/develop/active-directory-appmodel-v2-overview).

The AD v2 settings enable a bot to access Office 365 data via the Microsoft Graph API.

| Property | Description | Value |
|--|--|--|
| **Name** | The name of your connection | \<Your name for the connection\> <img width="300px"> |
| **Service Provider** | Azure AD Identity provider | `Azure Active Directory v2` |
| **Client ID** | Azure AD identity provider app ID | \<Azure AD provider app ID\> |
| **Client secret** | Azure AD identity provider app secret | \<Azure AD provider app secret\> |
| **Tenant ID** |  | \<directory (tenant) ID\> or `common`. See note. |
| **Scopes** | Space separated list of the API permissions you granted Azure AD identity provider app | Values such as `openid`, `profile`, `Mail.Read`, `Mail.Send`, `User.Read`, and `User.ReadBasic.All` |
| **Token Exchange URL** | Used for SSO in Azure AD v2 | See note below |

> [!NOTE]
>
> - Enter the **tenant ID** you recorded for the Azure AD identity provider app, if you selected one of the following:
>   - *Accounts in this organizational directory only (Microsoft only - Single tenant)*
>   - *Accounts in any organizational directory(Microsoft AAD directory - Multi tenant)*
> - Enter `common`  if you selected *Accounts in any organizational directory (Any Azure AD directory - Multi tenant and personal Microsoft accounts, such as Skype, Xbox, or Outlook.com)*. Otherwise, the Azure AD identity provider app will verify through the tenant whose ID was selected and exclude personal MS accounts.
> - Scopes takes a case-sensitive, space-separated list of values.
> - The **Token Exchange URL** is left blank for the root bot but is populated for the skill bot. See [Create an OAuth connection settings](../v4sdk/bot-builder-authentication-sso.md#create-an-oauth-connection-settings-1) to learn how to get its value.

---

For more information, see:

- [Why update to Microsoft identity platform (v2.0)?](/azure/active-directory/develop/active-directory-v2-compare)
- [Microsoft identity platform (formerly Azure Active Directory for developers)](/azure/active-directory/develop/).

## Other identity providers

Azure supports several identity providers. You can get a complete list, along with the related details, by running the following Azure console commands:

```azurecli
az login
az bot authsetting list-providers
```

You can also see the list of these providers in the [Azure portal](https://ms.portal.azure.com/) when you define the OAuth connection settings for a bot registration app.

:::image type="content" source="media/concept-bot-authentication/bot-auth-identity-providers.png" alt-text="Azure identity providers":::

### OAuth generic providers

Azure supports generic OAuth2, which allows you to use your own identity provider.

You can choose from two generic identity provider implementations, which have different settings as shown below.

> [!NOTE]
> You use the settings described here when configuring the **OAuth Connection Settings** in the Azure bot registration application.

### [Generic OAuth 2](#tab/ga2)

### Generic OAuth 2

Use this provider to configure any generic OAuth2 identity provider that has similar expectations as Azure AD provider, particularly AD v2. You have a limited number of properties because the query strings and request body payloads are fixed. For the values you enter, you can see how parameters to the various URLs, query strings, and bodies are in curly braces {}.

| Property | Description | Value |
|--|--|--|
| **Name** | The name of your connection | \<Your name for the connection\> <img width="300px"> |
| **Service Provider** | Identity provider | From the drop-down list, select **Generic Oauth 2** |
| **Client ID** | Identity provider app ID | \<provider ID\> |
| **Client secret** | Identity provider app secret | <provider secret\> |
| **Authorization URL** |  | `https://login.microsoftonline.com/common/oauth2/v2.0/authorize` |
| *Authorization URL Query String* |  | *?client_id={ClientId}&response_type=code&redirect_uri={RedirectUrl}&scope={Scopes}&state={State}* |
| **Token URL** |  | `https://login.microsoftonline.com/common/oauth2/v2.0/token` |
| *Token Body* | Body to send for the token exchange | *code={Code}&grant_type=authorization_code&redirect_uri={RedirectUrl}&client_id={ClientId}&client_secret={ClientSecret}* |
| **Refresh URL** |  | `https://login.microsoftonline.com/common/oauth2/v2.0/token` |
| *Refresh Body Template* | Body to send with the token refresh | *refresh_token={RefreshToken}&redirect_uri={RedirectUrl}&grant_type=refresh_token&client_id={ClientId}&client_secret={ClientSecret}* |
| **Scopes** | Comma separated list of the API permissions you granted earlier to the Azure AD authentication app | Values such as `openid`, `profile`, `Mail.Read`, `Mail.Send`, `User.Read`, and `User.ReadBasic.All` |

### [OAuth 2 generic provider](#tab/a2gp)

### OAuth 2 generic provider

Use this provider to configure any generic OAuth 2 service provider when you need more flexibility. It requires more configuration parameters. With this configuration, you specify the URLs templates, the query string templates, and the body templates for authorization, refresh, and token conversion. For the values you enter, you can see how parameters to the various URls, query strings, and bodies are in curly braces {}.

| Property | Description | Value |
|--|--|--|
| **Name** | The name of your connection | \<Your name for the connection\> <img width="300px"> |
| **Service Provider** | Identity provider | From the drop-down list, select **Oauth 2 Generic Provider** |
| **Client ID** | Identity provider app ID | \<provider ID\> |
| **Client secret** | Identity provider app secret | <provider secret\> |
| *Scope List Delimiter* | The character to use between scope values (often a space or comma) | *,* \<enter comma\> |
| **Authorization URL Template** |  | `https://login.microsoftonline.com/common/oauth2/v2.0/authorize` |
| *Authorization URL Query String* | The query string to append to the authorization URL,templated with any wanted parameters: {ClientId} {ClientSecret} {RedirectUrl} {Scopes} {State} | *?client_id={ClientId}&response_type=code&redirect_uri={RedirectUrl}&scope={Scopes}&state={State}* |
| **Token URL Template** |  | `https://login.microsoftonline.com/common/oauth2/v2.0/token` |
| *Token URL Query String Template* | Body to send for the token exchange | *?* \<enter question mark\> |
| *Token Body Template* | Body to send for the token exchange | *code={Code}&grant_type=authorization_code&redirect_uri={RedirectUrl}&client_id={ClientId}&client_secret={ClientSecret}* |
| **Refresh URL Template** |  | `https://login.microsoftonline.com/common/oauth2/v2.0/token` |
| *Refresh URL Query String Template* | The query string to append to the refresh URL,templated with any wanted parameters: {ClientId} {ClientSecret} {RedirectUrl} {Scopes} {State} | *?* \<enter question mark\> |
| *Refresh Body Template* | Body to send with the token refresh | *refresh_token={RefreshToken}&redirect_uri={RedirectUrl}&grant_type=refresh_token&client_id={ClientId}&client_secret={ClientSecret}* |
| **Scopes** | Comma separated list of the API permissions you granted earlier to the Azure AD authentication app | Values such as `openid`, `profile`, `Mail.Read`, `Mail.Send`, `User.Read`, and `User.ReadBasic.All` |

---

## Next steps

> [!div class="nextstepaction"]
> [Identity providers proxy](bot-builder-concept-identity-providers-proxy.md)

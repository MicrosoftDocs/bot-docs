---
title: Identity providers in Bot Framework SDK
description: Learn about identity providers, which authenticate user or client identities and issue security tokens. They provide authentication as a service. 
keywords: azure bot service, authentication, bot framework token service
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: conceptual
ms.date: 01/25/2022
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

You can choose from two Active Directory identity provider implementations, which have different settings as shown below.

> [!NOTE]
> Use these settings when configuring **OAuth Connection Settings** in the Azure bot registration application. For more information, see [Add authentication to a bot](bot-builder-authentication.md).

# [Azure AD v2](#tab/adv2)

The Microsoft identity platform (v2.0)&mdash;also known as the Azure AD v2 endpoint&mdash;allows a bot to get tokens to call Microsoft APIs, such as Microsoft Graph or other APIs. The identity platformis an evolution of the Azure AD platform (v1.0).
For more information, see the [Microsoft identity platform (v2.0) overview](/azure/active-directory/develop/active-directory-appmodel-v2-overview).

Use the AD v2 settings below to enable a bot to access Office 365 data via the Microsoft Graph API.

| Property | Description or value |
|--|--|
| **Name** | A name for this identity provider connection. |
| **Service Provider** | The identity provider to use. Select **Azure Active Directory v2**. |
| **Client id** | The application (client) ID for your Azure identity provider app. |
| **Client secret** | The secret for your Azure identity provider app. |
| **Tenant ID** | Your directory (tenant) ID or `common`. For more information, see the note about [tenant IDs](#azure-ad-note). |
| **Scopes** | A space-separated list of the API permissions you granted the Azure AD identity provider app, such as `openid`, `profile`, `Mail.Read`, `Mail.Send`, `User.Read`, and `User.ReadBasic.All`. |
| **Token Exchange URL** | For an _SSO-enabled skill bot_ use the token exchange URL associated with the OAuth connection; otherwise, leave this empty. For information about the SSO token exchange URL, see [Create an OAuth connection settings](bot-builder-authentication-sso.md#create-an-oauth-connection-settings-1). |

# [Azure AD v1](#tab/adv1)

### Azure AD v1

Use the settings shown below to configure the Azure AD developer platform (v1.0), also known as **Azure AD v1** endpoint. This allows you to build apps that securely sign in users with a Microsoft work or school account.
For more information, see [Azure Active Directory for developers (v1.0) overview](/azure/active-directory/azuread-dev/v1-overview).

| Property | Description or value |
|--|--|
| **Name** | A name for this identity provider connection. |
| **Service Provider** | The identity provider to use. Select **Azure Active Directory**. |
| **Client id** | The application (client) ID for your Azure identity provider app. |
| **Client secret** | The secret for your Azure identity provider app. |
| **Grant Type** | `authorization_code` |
| **Login URL** | `https://login.microsoftonline.com` |
| **Tenant ID** | Your directory (tenant) ID or `common`. For more information, see the note below about [tenant IDs](#azure-ad-note). |
| **Resource URL** | `https://graph.microsoft.com/` |
| **Scopes** | Leave this empty. |
| **Token Exchange URL** | Leave this empty. |

---

<a id="azure-ad-note"></a>

> [!NOTE]
> If you selected one of the following, enter the **tenant ID** you recorded for the Azure AD identity provider app:
>
> - *Accounts in this organizational directory only (Microsoft only - Single tenant)*
> - *Accounts in any organizational directory(Microsoft AAD directory - Multi tenant)*
>
> If you selected *Accounts in any organizational directory (Any Azure AD directory - Multi tenant and personal Microsoft accounts e.g., Skype, Xbox, Outlook.com)*, enter `common`.
>
> Otherwise, the Azure AD identity provider app will use the tenant to verify the selected ID and exclude personal Microsoft accounts.

For more information, see:

- [Why update to Microsoft identity platform (v2.0)?](/azure/active-directory/develop/active-directory-v2-compare)
- [Microsoft identity platform (formerly Azure Active Directory for developers)](/azure/active-directory/develop/).

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

You can choose from two generic identity provider implementations, which have different settings as shown below.

> [!NOTE]
> Use the settings described here when configuring the **OAuth Connection Settings** in the Azure bot registration application.

### [Generic OAuth 2](#tab/ga2)

Use this provider to configure any generic OAuth2 identity provider that has similar expectations as Azure AD provider, particularly AD v2. For this connection type, the query strings and request body payloads are fixed.

| Property | Description or value |
|--|--|
| **Name** | A name for this identity provider connection. |
| **Service Provider** | The identity provider to use. Select **Generic Oauth 2**. |
| **Client id** | Your client ID obtained from the identity provider. |
| **Client secret** | Your client secret obtained from the identity provider registration. |
| **Authorization URL** | `https://login.microsoftonline.com/common/oauth2/v2.0/authorize` |
| **Token URL** | `https://login.microsoftonline.com/common/oauth2/v2.0/token` |
| **Refresh URL** | `https://login.microsoftonline.com/common/oauth2/v2.0/token` |
| **Token Exchange URL** | Leave this empty. |
| **Scopes** | A comma-separated list of the API permissions you granted to the identity provider app. |

### [OAuth 2 Generic Provider](#tab/a2gp)

This provider requires more configuration parameters, so use this version to configure any generic OAuth 2 service provider when you need more flexibility. With this configuration, you specify the URL templates, the query string templates, and the body templates for authorization, refresh, and token conversion.

| Property | Description or value |
|--|--|
| **Name** | A name for this identity provider connection. |
| **Service Provider** | The identity provider to use. Select **Oauth 2 Generic Provider**. |
| **Client id** | Your client ID obtained from the identity provider. |
| **Client secret** | Your client secret obtained from the identity provider registration. |
| **Scope List Delimiter** | The separator character for the scope list. Empty spaces ( ) are not supported in this field, but can be used in the **Scopes** field if required by the identity provider. In that case, use a comma (,) for this field, and spaces ( ) in the **Scopes** field. |
| **Authorization URL Template** | A URL template for authorization, defined by your identity provider. For example, `https://login.microsoftonline.com/common/oauth2/v2.0/authorize`. |
| **Authorization URL Query String Template** | A query template for authorization, provided by your identity provider. Keys in the query string template will vary depending on the identity provider. |
| **Token URL Template** | `https://login.microsoftonline.com/common/oauth2/v2.0/token` |
| **Token URL Query String Template** | The query string separator for the token URL. Usually a question mark (?). |
| **Token Body Template** | A template for the token body. |
| **Refresh URL Template** | `https://login.microsoftonline.com/common/oauth2/v2.0/token` |
| **Refresh URL Query String Template** | A refresh URL query string separator for the token URL. Usually a question mark (?). |
| **Refresh Body Template** | A template for the refresh body. |
| **Token Exchange URL** |Leave this empty. |
| **Scopes** | List of scopes you want authenticated users to have once signed in. Make sure you're only setting the necessary scopes, and follow the [Least privilege access control principle](/windows-server/identity/ad-ds/plan/security-best-practices/implementing-least-privilege-administrative-models). For example, `User.Read`. If you're using a custom scope, use the full URI including the exposed application ID URI. |

---

## Next steps

> [!div class="nextstepaction"]
> [Identity providers proxy](bot-builder-concept-identity-providers-proxy.md)

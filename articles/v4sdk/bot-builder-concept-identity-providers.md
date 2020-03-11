---
title: Identity providers Azure Bot Service - Bot Service
description: Learn about identity providers in the Azure Bot Service.
keywords: azure bot service, authentication, bot framework token service
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 03/11/2020
monikerRange: 'azure-bot-service-4.0'
---

# Identity providers

An identity provider (IdP or IDP) is a system entity that creates, maintains, and manages identity information for principals while providing authentication services to client applications in a federation or distributed network.

They provides user authentication as a service. Client applications, such as web applications, delegate authentication to a trusted identity provider. Such client applications are said to be federated, that is, they use federated identity.

Some of the advantages of the identity provider are:

- Trusted providers that allow single sign-on (SSO) to access multiple secured applications.
- Facilitate connections between cloud computing resources and users, thus decreasing the need for users to re-authenticate when using different platforms.


## Single sign-on

Single sign-on refers to systems where a single authentication provides access to multiple applications by passing the authentication token seamlessly to configured applications. A user logs in with a single ID and password to gain access to any of several related systems. This is often accomplished by using the Lightweight Directory Access Protocol (LDAP) and stored LDAP databases on (directory) servers.
Conversely, single sign-off or single log-out (SLO) is the property whereby a single action of signing out terminates access to multiple software systems.
> [!IMPORTANT]
> SSO enhances usability by reducing the number of times a user must enter a password. It also provides better security by decreasing the potential attack surface.

## Azure Active Directory identity provider

The Azure Active Directory is a cloud identity provider that allows to build applications that securely sign in users using industry  standard protocols like **OAuth2.0**.

You can use one of these identity provider versions:

# [Azure AD v1](#tab/adv1)

### Azure AD v1

[!INCLUDE [azure-ad-v1-settings](~/includes/authentication/auth-aad-v1-settings.md)]

# [Azure AD v2](#tab/adv2)

### Azure AD v2

[!INCLUDE [azure-ad-v2-settings](~/includes/authentication/auth-aad-v2-settings.md)]

---

See also

- [Why update to Microsoft identity platform (v2.0)?](https://docs.microsoft.com/azure/active-directory/develop/active-directory-v2-compare)

- [Microsoft identity platform (formerly Azure Active Directory for developers)](https://docs.microsoft.com/azure/active-directory/develop/).

## Other identity providers

Azure supports several identity providers. You can get a complete list, along with the related details, by running this Azure console command:

```cmd
az bot authsetting list-providers
```

You can also see the list of these providers in the [Azure portal](https://ms.portal.azure.com/), when you define the OAuth connection settings for a bot registration app.

![azure identity providers](media/concept-bot-authentication/bot-auth-identity-providers.png)


### OAuth generic providers

The connection settings require the selection of an identity service provider.  This section shows how to configure the connection for **Generic Oauth2** and **Oauth2 Generic Provider**.

# [Generic OAuth 2](#tab/ga2)

### Generic OAuth 2

This identity service provider can be used with any generic OAuth2 service that has similar expectations as Azure Active Directory provider, particularly AADv2. It has a limited number of properties because the query strings and request body payloads are fixed. For the values you enter, you can see how parameters to the various URls, query strings, and bodies are in curly braces {}.

1. In the left panel, click **All resources**.  In the right panel, search for the Azure registration app created earlier. Click on the name (link) of the app.
1. In the displayed panel on the left, click **Settings**.
1. In the displayed panel on the right, at the bottom under **OAuth Connection Settings**, click the **Add Setting** button.
1. The **New Connection Setting** panel is displayed. Enter the following information:

    [!INCLUDE [generic-oauth2-settings](~/includes/authentication/auth-generic-oauth2-settings.md)]

1. Click the **Save** button.

# [OAuth 2 generic provider](#tab/a2gp)

### OAuth 2 generic provider

This identity service provider can be used with any generic OAuth 2 service provider and has the most flexibility, but requires the most configuration parameters. With this configuration you specify the URL templates, the query string templates, and the body templates for authorization, refresh, and token conversion. For the values you enter, you can see how parameters to the various URls, query strings, and bodies are in curly braces {}.

1. In the left panel, click **All resources**.  In the right panel, search for the Azure registration app created earlier. Click on the name (link) of the app.
1. In the displayed blade, click **Settings**.
1. In the displayed panel on the right, at the bottom under **OAuth Connection Settings**, click the **Add Setting** button.
1. The **New Connection Setting** panel is displayed. Enter the following information:

    [!INCLUDE [generic-provider-oauth2-settings](~/includes/authentication/auth-generic-provider-oauth2-settings.md)]

1. Click the **Save** button.

---

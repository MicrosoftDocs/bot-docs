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

An identity provider (IdP or IDP) creates, maintains, and manages identity information for principals while providing authentication services to client applications in a federation or distributed network.
It provides user authentication as a service.

Client applications, such as web applications, delegate authentication to a trusted identity provider. Such client applications are said to be federated, that is, they use federated identity.

Some of the (trusted) identity providers' advantages are:

- Allow single sign-on (SSO) to access multiple secured applications.
- Facilitate connections between cloud computing resources and users, thus decreasing the need for users to re-authenticate.


## Single sign-on

Single sign-on refers to a single authentication which provides secured resources access to multiple applications by passing the authentication token seamlessly to configured applications.

A user logs in with a single ID and password to gain access to any of several related software systems. This is often accomplished by using the Lightweight Directory Access Protocol (LDAP) and stored LDAP databases on (directory) servers.

Conversely, single sign-off or single log-out (SLO) is the property whereby a single action of signing out terminates access to multiple software systems.

> [!IMPORTANT]
> SSO enhances usability by reducing the number of times a user must enter credentials. It also provides better security by decreasing the potential attack surface.

## Azure Active Directory identity provider

The Azure Active Directory is a cloud identity provider that allows to build applications that securely sign in users using industry  standard protocols like **OAuth2.0**.

You can choose from two AD identity provider implementations which have different settings as shown below.

> [!Note]
> You use the settings described here when configuring the **OAuth Connection Settings** in the Azure bot registration application. See [Add authentication to a bot](bot-builder-authentication.md).

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

Azure supports generic OAuth2 which allow you to use your own identity providers.

You can choose from two generic identity provider implementations which have different settings as shown below.

> [!Note]
> You use the settings described here when configuring the **OAuth Connection Settings** in the Azure bot registration application.


# [Generic OAuth 2](#tab/ga2)

### Generic OAuth 2

The settings shown can be used with any generic OAuth2 identity provider that has similar expectations as Azure Active Directory provider, particularly AADv2. You have a limited number of properties because the query strings and request body payloads are fixed. For the values you enter, you can see how parameters to the various URls, query strings, and bodies are in curly braces {}.

[!INCLUDE [generic-oauth2-settings](~/includes/authentication/auth-generic-oauth2-settings.md)]


# [OAuth 2 generic provider](#tab/a2gp)

### OAuth 2 generic provider

The settings shown can be used with any generic OAuth 2 service provider and provide the most flexibility, but require the most configuration parameters. With this configuration you specify the URL templates, the query string templates, and the body templates for authorization, refresh, and token conversion. For the values you enter, you can see how parameters to the various URls, query strings, and bodies are in curly braces {}.

[!INCLUDE [generic-provider-oauth2-settings](~/includes/authentication/auth-generic-provider-oauth2-settings.md)]

---

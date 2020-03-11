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

- **Trusted providers** that allow single sign-on (SSO) to access multiple secured applications.
    > [!IMPORTANT]
    > SSO enhances usability by reducing the number of times a user must enter a password. It also provides better security by decreasing the potential attack surface.
- Facilitate connections between cloud computing resources and users, thus decreasing the need for users to re-authenticate when using different platforms.

## Azure Active Directory identity provider

The Azure Active Directory is a cloud identity provider that allows to build applications that securely sign in users using industry  standard protocols like **OAuth2.0**.

You can use one of these identity services:

- Azure AD developer platform (v1.0) Also known as **Azure AD v1** endpoint, it allows to build  apps that securely sign in 
users with a Microsoft work or school account.
For more information, see [Azure Active Directory for developers (v1.0) overview](https://docs.microsoft.com/azure/active-directory/azuread-dev/v1-overview).

- Microsoft identity platform (v2.0). Also known as **Azure AD v2** endpoint, it is an evolution of the Azure AD platform (v1.0). 
It allows to build applications that sign in all Microsoft identities and
get tokens to call Microsoft APIs, such as Microsoft Graph
or APIs that developers have built. For more information, see the [Microsoft identity platform (v2.0) overview](https://docs.microsoft.com/azure/active-directory/develop/active-directory-appmodel-v2-overview).

For information about the differences between the v1 and v2 endpoints, see the [Why update to Microsoft identity platform (v2.0)?](https://docs.microsoft.com/azure/active-directory/develop/active-directory-v2-compare). 

For complete information, see [Microsoft identity platform (formerly Azure Active Directory for developers)](https://docs.microsoft.com/azure/active-directory/develop/).

## Other identity providers

Azure supports several identity providers. You can get a complete list, along with the related details, by running this Azure console command:

```cmd
az bot authsetting list-providers
```

You can also see the list of these providers in the [Azure portal](https://ms.portal.azure.com/), when you define the OAuth connection settings for a bot registration app.

![azure identity providers](media/concept-bot-authentication/bot-auth-identity-providers.png)


### OAuth generic providers

The connection settings require the selection of an identity service provider.  This section shows how to configure the connection for **Generic Oauth2** and **Oauth2 Generic Provider**.

# [Generic OAuth 2 ](#tab/ga2)
### Generic OAuth 2

This identity service provider can be used with any generic OAuth2 service that has similar expectations as Azure Active Directory provider, particularly AADv2. It has a limited number of properties because the query strings and request body payloads are fixed. For the values you enter, you can see how parameters to the various URls, query strings, and bodies are in curly braces {}.

1. In the left panel, click **All resources**.  In the right panel, search for the Azure registration app created earlier. Click on the name (link) of the app.
1. In the displayed panel on the left, click **Settings**.
1. In the displayed panel on the right, at the bottom under **OAuth Connection Settings**, click the **Add Setting** button.
1. The **New Connection Setting** panel is displayed. Enter the following information:

    [!INCLUDE [generic-oauth2-settings](~/includes/authentication/auth-generic-oauth2-settings.md)]

1. Click the **Save** button.

# [OAuth 2 generic provider ](#tab/a2gp)
### OAuth 2 generic provider

This identity service provider can be used with any generic OAuth 2 service provider and has the most flexibility, but requires the most configuration parameters. With this configuration you specify the URL templates, the query string templates, and the body templates for authorization, refresh, and token conversion. For the values you enter, you can see how parameters to the various URls, query strings, and bodies are in curly braces {}.

1. In the left panel, click **All resources**.  In the right panel, search for the Azure registration app created earlier. Click on the name (link) of the app.
1. In the displayed blade, click **Settings**.
1. In the displayed panel on the right, at the bottom under **OAuth Connection Settings**, click the **Add Setting** button.
1. The **New Connection Setting** panel is displayed. Enter the following information:

    [!INCLUDE [generic-provider-oauth2-settings](~/includes/authentication/auth-generic-provider-oauth2-settings.md)]

1. Click the **Save** button.




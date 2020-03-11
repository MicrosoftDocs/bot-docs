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

## Azure Active Directory (AAD) identity provider

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

This section describes the OAuth generic providers settings.



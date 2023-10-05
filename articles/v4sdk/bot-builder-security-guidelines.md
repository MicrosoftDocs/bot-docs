---
title: Bot Framework security guidelines - Bot Service
description: Learn about the security guidelines in the Bot Framework.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: conceptual
ms.service: bot-service
ms.date: 11/01/2021
---

# Bot Framework security guidelines

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Bots are more and more prevalent in key business areas like financial services, retail, travel, and so on. A bot might collect very sensitive data such as credit cards, SSN, bank accounts, and other personal information. So, it's important that bots are secure and protect against common threats and vulnerabilities.

You can take some standard preventative measures to improve your bot's security. Some security measures are similar to the ones used in other software systems, while some are specific to the Bot Framework. For the latter, refer to the [Azure Security Benchmark](../security-baseline.md). The benchmark provides recommendations on how you can secure your cloud solutions on Azure.

## Security issues in a nutshell

This article groups security issues into 2 categories:

- **Threats**: The tactics someone might use to compromise your bot, such as spoofing, tampering, disclosing information, denial of service, and so on.

- **Vulnerabilities**: The ways in which your bot or the management of your bot might be susceptible to such tactics, such as bugs, or lax security.

Reducing your vulnerabilities is a good way to mitigate threats, and a known way to reduce vulnerabilities is to implement security check points in the development and deployment process.

## Common security guidelines

The following areas are covered by standard security best practices common to applications.

### Securing network traffic

Protocols exist that provide cryptographic controls to address data tampering and disclosure during transmission. In this regard, bots should communicate only over secured channels.

To exchange data on the wire any secure system must use the **HTTPS** protocol, which transfers data over HTTP in encrypted connections protected by [Transport Layer Security](https://tools.ietf.org/html/rfc5246) (TLS) or [Secure Sockets Layer](https://tools.ietf.org/html/rfc6101) (SSL).  See also [RFC 2818 - HTTP Over TLS](https://tools.ietf.org/html/rfc2818).

> [!NOTE]
> All Bot Service channels require **HTTPS** and **TLS 1.2**.

### Self-destructing messages

Permanently delete any sensitive data as soon as it's no longer needed, usually after the message exchange ends, or after a certain amount of time. This can include personally-identifying information, IDs, PINs, passwords, security questions and answers, and so so.

### Data storage

The best practice calls for storing information in a secure state for a certain amount of time and then discarding it later after it served its purpose.

Some common security techniques are listed below.

#### Database firewalls

- Firewalls deny access to traffic by default. The only traffic allowed should originate from specific applications or web servers that need to access the data.
- You should also deploy a web application firewall. This is because attacks such as SQL injection attacks directed at a web application can be used to exfiltrate or delete data from the database.

#### Database hardening

- Make sure that the database is still supported by the vendor, and you're running the latest version of the database with all the security patches installed to remove known vulnerabilities.
- Uninstall or disable any features or services that you don't need and make sure you change the passwords of any default accounts from their default values; or better, delete any default accounts that you don't need.
- Make sure that all the database security controls provided by the database are enabled, unless there is a specific reason for any to be disabled.

#### Minimize valuable information

- Make sure that you're not storing any confidential information that doesn't need to be in the database.
- Data retained for compliance or other purposes can be moved to more secure storage, perhaps offline, which is less susceptible to database security threats.
- Make sure to delete any history files that are written by a server during the original installation procedure. If the installation is successful these files have no value but can contain information that can potentially be exploited.

### Education

Bots provide an innovative interaction tool between a company and its customers. But they could potentially provide a backdoor for tampering with a company's website. Therefore, a company must assure that its developers understand the importance of bot security as part of the website security. Moreover, users' errors can be a problem, too. This will require some education on how bots can be used securely, for example:

- For the developers, a strategy should include internal training on how to use the bot securely.
- Customers can be given guidelines detailing how to interact with the bot safely.

## Bot-specific security guidelines

The following areas are covered by some standard security best practices for Bot Framework applications.
The following guidelines describe the Bot Framework best practice security measures. For more information, see the [Security and Privacy FAQ](~/bot-service-resources-faq-security.md).

### Bot Connector authentication

The Bot Connector service natively uses HTTPS to exchange messages between a bot and channels (users). the Bot Framework SDK automates basic bot-to-channel authentication for you.

> [!WARNING]
> If you're writing your own authentication code, it's critical that you implement all security procedures correctly. By implementing all steps described in the [Authentication](~/rest-api/bot-framework-rest-connector-authentication.md) article, you can mitigate the risk of an attacker being able to read messages that are sent to your bot, send messages that impersonate your bot, and steal secret keys.

### User authentication

**Azure AI Bot Service authentication** enables you to authenticate users to and get **access tokens** from various identity providers such as *Azure Active Directory*, *GitHub*, *Uber* and so on. You can also configure authentication for a custom **OAuth2** identity provider. All this enables you to write **one piece of authentication code** that works across all supported identity providers and channels. To utilize these capabilities you need to perform the following steps:

1. Statically configure `settings` on your bot that contains the details of your application registration with an identity provider.
1. Use an `OAuthCard`, backed by the application information you supplied in the previous step, to sign-in a user.
1. Retrieve access tokens through **Azure AI Bot Service API**. A good practice is to place a time limit on how long an authenticated user can stay *logged in*.

For more information, see the [User authentication](~/v4sdk/bot-builder-concept-authentication.md) article.

### Web Chat

When you use the [Web Chat](~/bot-service-channel-connect-webchat.md) control you must keep in mind some important security considerations about impersonation and identity spoofing. For more information, see [Direct Line enhanced authentication](bot-builder-security-enhanced.md).

## Additional information

- [User authentication](~/v4sdk/bot-builder-concept-authentication.md)
- [Add authentication to your bot via Azure AI Bot Service](~/v4sdk/bot-builder-authentication.md)
- [Enable security and test on localhost](~/bot-service-troubleshoot-authentication-problems.md#step-3-enable-security-and-test-on-localhost-)
- [Secrets and tokens](~/rest-api/bot-framework-rest-direct-line-3-0-authentication.md#secrets-and-tokens)
- [Authentication technologies](~/rest-api/bot-framework-rest-connector-authentication.md#authentication-technologies)
- [Enhanced Direct Line Authentication Features](https://blog.botframework.com/2018/09/25/enhanced-direct-line-authentication-features)
- [Security recommendations in Azure Security Center](/azure/security-center/security-center-recommendations)
- [Threat protection in Azure Security Center](/azure/security-center/threat-protection)
- [Azure Security Center Data Security](/azure/security-center/security-center-data-security)
- [Container security in Security Center](/azure/security-center/container-security)

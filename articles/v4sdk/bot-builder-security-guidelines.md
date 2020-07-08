---
title: Bot Framework security guidelines - Bot Service
description: Learn about the security guidelines in the Bot Framework.
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 07/07/2020
---

# Bot Framework security guidelines

Bots are more and more prevalent in key business areas like financial services, retail, travel and so on. The bots might collect very sensitive data such as credit cards, SSN, bank accounts, and other personal information. So it is important that bots are **secure**.

Securing a bot can be complicated, even the most robust one could have **vulnerabilities** and be at risk for **threats**.

However, there are [security preventive measures](#security-preventive-measures) that can be taken to improve bot's security. They are similar to the ones used in any other software system handling sensitive data.

## Security issues in a nutshell

This is a vast area that cannot be covered in a few paragraphs. In a nutshell, the security issues can be grouped as follows:

- **Threats**. They refer to different ways a bot can be compromised. They include spoofing, tampering, disclosing information, denial of service as so forth.

- **Vulnerabilities**. They refer to ways a bot is compromised that cannot be identified and solved correctly and on time. Vulnerabilities might be cause by poor coding, *relaxed* security, or bugs. The most effective way to solve possible vulnerabilities is to implement security activities in the development and deployment process.

## Security preventive measures

1. **Protocols and Encryption**

    Data can be tampered with during transmission. Protocols exist that provide encryption to address problems of misuse and tampering.
    In this regard, companies must take measures to allow bots to access only encrypted channels to communicate. This can prevent anyone other than the receiver and sender from seeing any part of the message or transaction.

    Encryption is one of the most robust methods of ensuring bot security and companies must proactively guarantee its effectiveness by taking measures to de-identify and encrypt personal data.

    To exchange data on the wire any secure system must use the **HTTPS** protocol. Data is transferred over HTTP in encrypted connections protected by [Transport Layer Security](https://tools.ietf.org/html/rfc5246) (TLS) or [Secure Sockets Layer](https://tools.ietf.org/html/rfc6101) (SSL).  See also [RFC 2818 - HTTP Over TLS](https://tools.ietf.org/html/rfc2818).

1. **Authentication and Authorization**.

    A bot communicates with a Bot Connector service using HTTP over a secured channel (SSL/TLS). As a bot developer, you must implement the security procedures described in the [Authentication](~/rest-api/bot-framework-rest-connector-authentication.md) article to enable your bot to securely exchange messages with the Bot Connector service.

    A good practice is to place a time limit on how long an authenticated user can stay *logged in*.

1. **Self-destructing messages**

    This measure can improve bot security. Usually, after the message exchange ends, or after a certain amount of time, messages and any sensitive data are erased forever.

<!--
1. Personal Scan
-->

1. Data Storage

    The best practice calls for storing information in a secure state for a certain amount of time and discard it later after it served its purpose.

1. **Web Chat**

1. **Education**

    On one hand bots provide an innovative interaction tool between a company and its customers. On the other hand they could potentially provide a backdoor entry for hackers to tamper with a company's website.

    Therefore companies must assure that its developers understand that bot security is of out outmost importance, like all aspects of website security. It is important to implement layers of website security that makes harder for hackers to compromise the site.

    Despite security issues awareness, people can still be the weakest link and users' errors continue to be a problem. This will require education on how digital technologies such as bots can be used securely.

    To mitigate this problem, a development strategy should include internal training on how to use the bot securely.

    Even though customers cannot be trained as the internal personnel, they can be given guideline detailing how to interact with the bot safely.


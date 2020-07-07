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

Bots are more and more prevalent in key business areas like financial services, retail, travel and so on. The bots might collect very sensitive data such as credit cards, SSN, bank accounts, and other personal information. So it is important that a bot is not vulnerable to hacking exploitation.

## Security issues in a nutshell

This is a vast area that cannot be covered in a few paragraphs. In a nutshell, the security issues can be grouped as follows:

- **Threats**. They refer to different ways a bot can be compromised. They include spoofing, tampering, disclosing information, denial of service as so forth.

- **Vulnerabilities**. They refer to ways a bot is compromised that cannot be identified and solved correctly and on time. Vulnerabilities might be cause by poor coding, *relaxed* security, or bugs. The most effective way to solve possible vulnerabilities is to implement security activities in the development and deployment process.

## Security areas

1. Encryption

1. **Authentication and Authorization**.

    A bot communicates with the Bot Connector service using HTTP over a secured channel (SSL/TLS). As a bot developer, you must implement the security procedures described in the [Authentication](../rest-api\bot-framework-rest-connector-authentication.md) article to enable your bot to securely exchange messages with the Bot Connector service.

1. Self-destructing Messages

1. Personal Scan

1. Data Storage

1. Errors




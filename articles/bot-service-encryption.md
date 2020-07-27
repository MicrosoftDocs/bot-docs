---
title: Azure Bot Service encryption for data at rest | Microsoft Docs
description: Azure Bot Service protects your data by automatically encrypting it before persisting it to the cloud with Microsoft provided encryption keys.
ms.service: bot-service
ms.date: 07/21/2020
ms.topic: conceptual
ms.author: jameslew
---

# Azure Bot Service encryption for data at rest

Azure Bot Service automatically encrypts your data when persisting it to the cloud. Encryption protects your data and to help you to meet your organizational security and compliance commitments. Data in Azure Bot Service is encrypted and decrypted transparently using 256-bit [AES encryption](https://en.wikipedia.org/wiki/Advanced_Encryption_Standard), one of the strongest block ciphers available, and is FIPS 140-2 compliant. A checksum technique is also used to detect tampering of data that is not encrypted, such as GUIDs.

## Types of data encrypted in the Azure Bot Service

Azure Bot Service encrypts your data in the bot service. Examples include, but are not limited to:

1) Any developer account data such as name or email
2) Any sensitive data about your bot (including URL's, third party channel credentials, bot names and descriptions)
3) Any data temporarily stored while delivering activities including any conversation data and attachments

## About encryption key management

Azure Bot Service encrypts data with Microsoft provided keys which are rotated on a pre-defined basis.  

|                                        |    Microsoft-managed keys                             | 
|----------------------------------------|-------------------------------------------------------|
|    Encryption/decryption operations    |    Azure                                              |
|    Key storage                         |    Azure Key Vault                              |
|    Key rotation responsibility         |    Microsoft                                          |
|    Key usage                           |    Microsoft                                          |
|    Key access                          |    Microsoft only                                     |

The following sections describe each of the options for key management in greater detail.

## Microsoft-managed keys

Your Azure Bot Service resource uses Microsoft-managed encryption keys. Azure Bot Service conforms to Microsoft best practices for Key storage access, encryption algorithms, and key rotation.

## Customer-managed keys

Currently the Azure Bot Service does not offer customer-managed encryption keys capability for data at rest.
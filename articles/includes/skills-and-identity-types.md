---
description: Table of support for different combinations of skill and consumer identity-management flavors.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 11/19/2021
ms.custom:
  - evergreen
---

Some types of skill consumers are not able to use some types of skill bots.
The following table describes which combinations are supported.

| &nbsp;                                      | Multi-tenant skill | Single-tenant skill                          | User-assigned managed identity skill         |
|:--------------------------------------------|:-------------------|:---------------------------------------------|:---------------------------------------------|
| **Multi-tenant consumer**                   | Supported          | Not supported                                | Not supported                                |
| **Single-tenant consumer**                  | Not supported      | Supported if both apps belong to same tenant | Supported if both apps belong to same tenant |
| **User-assigned managed identity consumer** | Not supported      | Supported if both apps belong to same tenant | Supported if both apps belong to same tenant |

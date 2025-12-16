---
description: Procedure for adding bot identity information to the bot's configuration file.
author: kunsinghms
ms.author: kunsingh
manager: shellyha
ms.reviewer: pehecke
ms.topic: include
ms.custom:
  - evergreen
ms.update-cycle: 1095-days
---

<a id="app-id-and-password"></a>

### Bot Identity Information

Follow these steps to add identity information to your bot's configuration file. The file differs depending on the programming language used to create the bot.

> [!IMPORTANT]
>
> - The **Java** version of the Bot Framework SDK only supports **multi-tenant** bots.
> - The **C#**, **JavaScript**, and **Python** versions support all three application types for managing the bot's identity.

| Language   | File name              | Notes                                                                                                               |
|:-----------|:-----------------------|:--------------------------------------------------------------------------------------------------------------------|
| C#         | appsettings.json       | Supports all three application types for managing your bot's identity.                                              |
| JavaScript | .env                   | Supports all three application types for managing your bot's identity.                                              |
| Java       | application.properties | Only supports multi-tenant bots.                                                                                    |
| Python     | config.py              | Supports all three application types for managing your bot's identity.                                              |

The identity information you need to add depends on the bot's application type. Provide the following values in your configuration file.

#### [User-assigned managed identity](#tab/userassigned)

Available for C#, JavaScript, and Python bots.

| Property               | Value                                                                      |
|:-----------------------|:---------------------------------------------------------------------------|
| `MicrosoftAppType`     | `UserAssignedMSI`                                                          |
| `MicrosoftAppId`       | The client ID of the user-assigned managed identity.                       |
| `MicrosoftAppPassword` | Not applicable. Leave this blank for a user-assigned managed identity bot. |
| `MicrosoftAppTenantId` | The tenant ID of the user-assigned managed identity.                       |

#### [Single-tenant](#tab/singletenant)

Available for C#, JavaScript, and Python bots.

| Property               | Value                    |
|:-----------------------|:-------------------------|
| `MicrosoftAppType`     | `SingleTenant`           |
| `MicrosoftAppId`       | The bot's app ID.        |
| `MicrosoftAppPassword` | The bot's app password.  |
| `MicrosoftAppTenantId` | The bot's app tenant ID. |

#### [Multi-tenant](#tab/multitenant)

Available for bots in all programming languages: C#, JavaScript, Java, and Python.

> [!IMPORTANT]
>
> - **Multi-tenant bot creation will be deprecated after July 31, 2025.**
> - Existing multi-tenant bots will continue to function, but new multi-tenant bot creation will no longer be supported after that date.
> - To ensure continued support, use **single-tenant** or **user-assigned managed identity** going forward.

| Property               | Value                                                    |
|:-----------------------|:---------------------------------------------------------|
| `MicrosoftAppType`     | `MultiTenant`                                            |
| `MicrosoftAppId`       | The bot's app ID.                                        |
| `MicrosoftAppPassword` | The bot's app password.                                  |
| `MicrosoftAppTenantId` | Not applicable. Leave this blank for a multi-tenant bot. |

---

---
description: Procedure for adding bot identity information to the bot's configuration file.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 11/29/2021
---

<a id="app-id-and-password"></a>

### Bot identity information

You need to add identity information to your bot's configuration file. The file differs depending on the programming language you use to create the bot.

> [!IMPORTANT]
> The Java and Python versions of the Bot Framework SDK only support multi-tenant bots.
> The C# and JavaScript versions support all three application types for managing the bot's identity.

| Language | File name | Notes |
|:-|:-|:-|
| C# | appsettings.json | Supports all three application types for managing your bot's identity. |
| JavaScript | .env | Supports all three application types for managing your bot's identity. |
| Java | application.properties | Only supports multi-tenant bots. |
| Python | config.py | Only supports multi-tenant bots. Provide the identity properties as arguments to the `os.environ.get` method calls. |

The identity information you need to add depends on the bot's application type.
Provide the following values in your configuration file.

### [User-assigned managed identity](#tab/userassigned)

Only available for C# and JavaScript bots.

| Property               | Value                                                      |
|:-----------------------|:-----------------------------------------------------------|
| `MicrosoftAppType`     | `UserAssignedMSI`                                          |
| `MicrosoftAppId`       | The client ID of the user-assigned managed identity.       |
| `MicrosoftAppPassword` | Leave this blank for a user-assigned managed identity bot. |
| `MicrosoftAppTenantId` | The bot's app tenant ID.                                   |

### [Single-tenant](#tab/singletenant)

Only available for C# and JavaScript bots.

| Property               | Value                    |
|:-----------------------|:-------------------------|
| `MicrosoftAppType`     | `SingleTenant`           |
| `MicrosoftAppId`       | The bot's app ID.        |
| `MicrosoftAppPassword` | The bot's app password.  |
| `MicrosoftAppTenantId` | The bot's app tenant ID. |

### [Multi-tenant](#tab/multitenant)

Available for bots in all programming languages: C#, JavaScript, Java, and Python.

| Property               | Value                                    |
|:-----------------------|:-----------------------------------------|
| `MicrosoftAppType`     | `MultiTenant`                            |
| `MicrosoftAppId`       | The bot's app ID.                        |
| `MicrosoftAppPassword` | The bot's app password.                  |
| `MicrosoftAppTenantId` | Leave this blank for a multi-tenant bot. |

---

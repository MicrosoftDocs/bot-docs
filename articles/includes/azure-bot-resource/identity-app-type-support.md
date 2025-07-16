---
description: Note about product support for different identity management types in Azure Bot applications.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.custom:
  - evergreen
ms.update-cycle: 1095-days
---

Your bot's identity can be managed in Azure in several ways:

- As a **user-assigned managed identity**, so you don’t need to manage credentials manually.
- As a **single-tenant** app.
- As a **multi-tenant** app.

> [!NOTE]
>
> - Support for **user-assigned managed identity** and **single-tenant** app types is available in the Bot Framework SDK for C#, JavaScript, and Python.
> - These app types are **not supported** in other SDK languages, Bot Framework Composer, Bot Framework Emulator, or Dev Tunnels.
> [!IMPORTANT]
>
> - **Multi-tenant bot creation will be deprecated after July 31, 2025.**
> - Existing multi-tenant bots will continue to function, but new multi-tenant bot creation will no longer be supported after that date.
> - To ensure continued support, use **single-tenant** or **user-assigned managed identity** going forward.

### Supported App Types

| App Type                             | Supported In                                                                                                      |
|--------------------------------------|-------------------------------------------------------------------------------------------------------------------|
| User-assigned managed identity       | Azure AI Bot Service; C#, JavaScript, and Python SDKs                                                            |
| Single-tenant                        | Azure AI Bot Service; C#, JavaScript, and Python SDKs                                                            |
| Multi-tenant _(Deprecated – ends July 31, 2025)_ | Azure AI Bot Service; all Bot Framework SDK languages; Composer; Emulator; Dev Tunnels            |

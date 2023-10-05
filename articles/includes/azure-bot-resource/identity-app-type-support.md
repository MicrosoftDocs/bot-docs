---
description: Note about product support for the different identity-management Azure Bot app types.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 03/03/2022
---

Your bot identity can be managed in Azure in a few different ways.

- As a _user-assigned managed identity_, so that you don't need to manage the bot's credentials yourself.
- As a _single-tenant_ app.
- As a _multi-tenant_ app.

Support for the user-assigned managed identity and single-tenant app types was added to the Bot Framework SDK for C# and JavaScript in version 4.15.0.
These app types aren't supported in the other languages or in Bot Framework Composer, Bot Framework Emulator, or ngrok.

| App type                       | Support                                                                               |
|:-------------------------------|:--------------------------------------------------------------------------------------|
| User-assigned managed identity | Azure AI Bot Service and the C# and JavaScript SDKs                                      |
| Single-tenant                  | Azure AI Bot Service and the C# and JavaScript SDKs                                      |
| Multi-tenant                   | Azure AI Bot Service, all Bot Framework SDK languages, Composer, the Emulator, and ngrok |

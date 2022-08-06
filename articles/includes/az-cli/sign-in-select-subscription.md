---
description: Azure CLI instructions to sign into Azure and select a subscription.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 03/03/2022
---

1. Open a command window.

1. Sign in to Azure.

    ```azurecli
    az login
    ```

    - A browser window will open. Complete the sign-in process.
    - On success, the command outputs a list of the subscriptions your account has access to.

1. To set the subscription to use, run:

    ```azurecli
    az account set --subscription "<subscription>"
    ```

    For \<subscription>, use the ID or name of the subscription to use.

1. If you'll create a user-assigned managed identity or a single-tenant bot, record the `tenantId` for the subscription.
    You'll use the tenant ID in the following steps.

> [!TIP]
> If you need to work in a non-public cloud, see [Azure cloud management with the Azure CLI](/cli/azure/manage-clouds-azure-cli).

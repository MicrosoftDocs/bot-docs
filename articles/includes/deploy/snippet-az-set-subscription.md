---
description: Use Azure CLI to select the Azure subscription to use.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 11/19/2021
---

Set the default subscription to use.

```azurecli
az account set --subscription "<azure-subscription-id>"
```

If you aren't sure which subscription to use for deploying the bot, you can view the list of subscriptions for your account by using `az account list` command.

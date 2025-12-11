---
description: Use Azure CLI to create an application registration for a multi tenant bot.
author: kunsinghms
ms.author: kunsingh
manager: shellyha
ms.reviewer: pehecke
ms.topic: include
ms.custom:
  - devx-track-azurecli
  - evergreen
ms.update-cycle: 1095-days
---

### [C# / JavaScript / Python](#tab/csharp+javascript)

For Azure CLI 2.39.0 or later, use the following commands to create your app registration and set its password. On success, these commands generate JSON output.

1. Use the `az ad app create` command to create an Microsoft Entra ID app registration.
   This command generates an app ID that you'll need in later steps.

   ```azurecli
   az ad app create --display-name "<app-registration-display-name>" --sign-in-audience "AzureADandPersonalMicrosoftAccount"
    ```

   | Option           | Description                                                                               |
   |:-----------------|:------------------------------------------------------------------------------------------|
   | display-name     | The display name for your app registration.                                               |
   | sign-in-audience | The supported Microsoft accounts for the app. Use `AzureADandPersonalMicrosoftAccount` for a multi-tenant app. |

1. Use the `az ad app credential reset` command to generate a new password for your app registration.

   ```azurecli
   az ad app credential reset --id "<appId>"
   ```

1. Record values you'll need in later steps: the _app ID_ and _password_ from the command output.

For more information about `az ad app`, see the [command reference](/cli/azure/ad/app). For more information about the `sign-in-audience` parameter, see [sigInAudience values](/graph/api/resources/application#signinaudience-values).

---

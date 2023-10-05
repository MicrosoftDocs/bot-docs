---
description: Use Azure CLI to create an application registration for a multi tenant bot.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.custom: devx-track-azurecli
ms.date: 08/29/2022
---

### [C# / JavaScript](#tab/csharp+javascript)

For Azure CLI 2.39.0 or later, use the following commands to create your app registration and set its password. On success, these commands generate JSON output.

1. Use the `az ad app create` command to create an Azure Active Directory app registration.
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

### [Java / Python](#tab/java+python)

For Azure CLI 2.36.0 or earlier, use the following command to create your app registration and set its password. On success, this command generates JSON output.

1. Use the `az ad app create` command to create an Azure Active Directory app registration.

   ```azurecli
   az ad app create --display-name "<name>" --password "<password>" --available-to-other-tenants
   ```

   | Option | Description |
   |:-|:-|
   | display-name | The display name for the app registration. |
   | password | The password, or _client secret_, for the application. It must be at least 16 characters long and contain at least one upper-case or lower-case alphabetical character, at least one numeric character, and at least one special character. |
   | available-to-other-tenants | Include this flag to create a multi-tenant bot. It allows the application to be accessible from any Azure AD tenant. |

1. Record values you'll need in later steps.
   1. The password you used
   1. The _app ID_ from the command output

---

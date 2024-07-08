---
description: Use Azure CLI to create an application registration.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.custom:
  - devx-track-azurecli
  - evergreen
ms.date: 03/03/2022
---

[!INCLUDE [Note about support for each identity app type](../azure-bot-resource/identity-app-type-support.md)]

To create an Azure application registration:

### [User-assigned managed identity](#tab/userassigned)

> [!TIP]
> You need an existing resource group in which to create the managed identity.

1. If you don't already have an appropriate resource group, use the `az group create` command to create a new resource group.

    ```azurecli
    az group create --name "<group>" --location "<region>"
    ```

    | Option   | Description                                       |
    |:---------|:--------------------------------------------------|
    | name     | The name of the resource group to create.         |
    | location | The region in which to create the resource group. |

    For more information, see [How to manage Azure resource groups with the Azure CLI](/cli/azure/manage-azure-groups-azure-cli).

1. To create a user-assigned managed identity, use the `az identity create` command.
    On success, the command generates JSON output.

    ```azurecli
    az identity create --resource-group "<group>" --name "<identity>"
    ```

    | Option         | Description                                                     |
    |:---------------|:----------------------------------------------------------------|
    | resource-group | The name of the resource group in which to create the identity. |
    | name           | The name of the identity resource to create.                    |

    For more information, see the [az identity](/cli/azure/identity) reference.

1. Record the resource group and identity name you entered and the `clientId` from the command output.
    You'll use these values in following steps.

### [Single-tenant](#tab/singletenant)

1. Use the `az ad app create` command to create an Microsoft Entra ID app registration.
    On success, the command generates JSON output.

    ```azurecli
    az ad app create --display-name "<name>" --password "<password>"
    ```
  
    | Option | Description |
    |:-|:-|
    | display-name | The display name for the app registration. |
    | password | The password, or _client secret_, for the application. It must be at least 16 characters long, contain at least 1 upper or lower case alphabetical character, at least one numeric character, and contain at least 1 special character. |

    For more information, see the [az ad app](/cli/azure/ad/app) reference.

1. Record the password you entered in the command and the `appId` from the command output.
    You'll use these values in following steps.

### [Multi-tenant](#tab/multitenant)

1. Use the `az ad app create` command to create an Microsoft Entra ID app registration.
    On success, the command generates JSON output.

    ```azurecli
    az ad app create --display-name "<name>" --password "<password>" --available-to-other-tenants
    ```

    | Option | Description |
    |:-|:-|
    | display-name | The display name for the app registration. |
    | password | The password, or _client secret_, for the application. It must be at least 16 characters long, contain at least 1 upper or lower case alphabetical character, at least one numeric character, and contain at least 1 special character. |
    | available-to-other-tenants | Include this flag to create a multi-tenant bot. It allows the application to be accessible from any Microsoft Entra ID tenant. |

    For more information, see the [az ad app](/cli/azure/ad/app) reference.

1. Record the password you entered in the command and the `appId` from the command output.
    You'll use these values in following steps.

---

---
description: Use Azure CLI to create an application registration.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 11/19/2021
---

In this step you will create an Azure application registration, which will allow:

- The user to interact with the bot via a set of channels, such as Web Chat.

  > [!NOTE]
  > The Web Chat channel is configured by default. If you want to connect your bot to any other channel, you need to perform the related configuration steps. For example, this is how you [Connect a bot to Microsoft Teams](../../channel-connect-teams.md).  

- The definition of *OAuth Connection Settings* to authenticate a user and to create a *token* used by the bot to access protected resources on behalf of the user.

#### Create the Azure application registration

> [!NOTE]
> The Bot Framework SDK for C# or Javascript version 4.15.0 or later is required for user-assigned managed identity and single-tenant bots.

To create an Azure application registration, use the following command:

### [User-assigned managed identity](#tab/userassigned)

```azurecli
az identity create --group "resourceGroupName" --name "userAssignedManagedIdentityName"
```

| Option   | Description |
|:---------|:------------|
| group | The name of resource group name in which to create the identity. |
| name | The name of the identity resource to create |

### [Single-tenant](#tab/singletenant)

```azurecli
az ad app create --display-name "displayName" --password "AtLeastSixteenCharacters_0"
```

| Option   | Description |
|:---------|:------------|
| display-name | The display name of the application. It is listed in the Azure portal in the general resources list and in the resource group it belongs.|
| password | The password, also known as **client secret**, for the application. This is a password you create for this resource. It must be at least 16 characters long, contain at least 1 upper or lower case alphabetical character, at least one numeric character, and contain at least 1 special character.|

#### Record the appId and appSecret values

Copy and save the `appId` and `password` values. You will need them in the ARM deployment step.

### [Multi-tenant](#tab/multitenant)

```azurecli
az ad app create --display-name "displayName" --password "AtLeastSixteenCharacters_0" --available-to-other-tenants
```

| Option   | Description |
|:---------|:------------|
| display-name | The display name of the application. It is listed in the Azure portal in the general resources list and in the resource group it belongs.|
| password | The password, also known as **client secret**, for the application. This is a password you create for this resource. It must be at least 16 characters long, contain at least 1 upper or lower case alphabetical character, at least one numeric character, and contain at least 1 special character.|
| available-to-other-tenants| Indicates that the application can be used from any Azure AD tenant. Use this flag for a multi-tenant bot.|

#### Record the appId and appSecret values

Copy and save the `appId` and `password` values. You will need them in the ARM deployment step.

---

---
description: Use Azure CLI to create the application service in a new resource group.
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

### [User-assigned managed identity](#tab/userassigned)

For a user-assigned managed identity bot, run this command to provision resources in a new resource group and new app service plan.

```azurecli
az deployment sub create --template-file "<path>" --location <bot-region> --parameters appType="UserAssignedMSI" appId="<client-id>" tenantId="<tenant-id>" existingUserAssignedMSIName="<identity>" existingUserAssignedMSIResourceGroupName="<identity-group>" botId="<bot-id>" botSku=<tier> newAppServicePlanName="<plan-name>" newWebAppName="<service-name>" groupName="<group-name>" groupLocation="<group-region>" newAppServicePlanLocation="<plan-region>" --name "<deployment-name>"
```

### [Single-tenant](#tab/singletenant)

For a single-tenant bot, run this command to provision resources in a new resource group and new app service plan.

```azurecli
az deployment sub create --template-file "<path>" --location <bot-region> --parameters appType="SingleTenant" appId="<app-id>" appSecret="<password>" tenantId="<tenant-id>" botId="<bot-id>" botSku=<tier> newAppServicePlanName="<plan-name>" newWebAppName="<service-name>" groupName="<group-name>" groupLocation="<group-region>" newAppServicePlanLocation="<plan-region>" --name "<deployment-name>"
```

### [Multi-tenant](#tab/multitenant)

For a multi-tenant bot, run this command to provision resources in a new resource group and new app service plan.

```azurecli
az deployment sub create --template-file "<path>" --location <bot-region> --parameters appType="MultiTenant" appId="<app-id>" appSecret="<password>" botId="<bot-id>" botSku=<tier> newAppServicePlanName="<plan-name>" newWebAppName="<service-name>" groupName="<group-name>" groupLocation="<group-region>" newAppServicePlanLocation="<plan-region>" --name "<deployment-name>"
```

---

This table describes the command _options_ for the `az deployment sub create` command.

| Option        | Description                                                                                                                                        |
|:--------------|:---------------------------------------------------------------------------------------------------------------------------------------------------|
| location      | The region in which to create the Azure Bot resource.                                                                                              |
| name          | The deployment name.                                                                                                                               |
| parameters    | Deployment parameters for the ARM template, provided as a list of key-value pairs. See the table of parameters for descriptions.                   |
| template-file | The path to the `template-with-new-rg.json` ARM template in the bot project's _deployment templates_ folder. The path can be relative or absolute. |

This table describes the _deployment parameters_ to use with the `--parameters` command option.
Not all parameters apply to all app types.

| Parameter | Description |
|:-|:-|
| appId | The client or app ID from the identity resource you created earlier. This is used as the Microsoft app ID of the web app. |
| appSecret | For for single-tenant and multi-tenant app types, the password for the identity resource you created earlier. |
| appType | The type of app service to create: `UserAssignedMSI`, `SingleTenant`, or `MultiTenant`. Default is `MultiTenant`. |
| botId | The ID of the Azure Bot resource to create. The bot ID is immutable. |
| botSku | The pricing tier to use for the bot: `F0` (Free) or `S1` (Standard). |
| existingUserAssignedMSIName | For user-assigned managed identity app types, the name of the identity resource you created earlier. |
| existingUserAssignedMSIResourceGroupName | For user-assigned managed identity app types, the name of the resource group for your identity resource. |
| groupLocation | The region in which to create your new resource group. |
| groupName | A name for your new resource group. |
| newAppServicePlanLocation | The region in which to create the application service plan. |
| newAppServicePlanName | The name of the app service plan to create for the bot. |
| newAppServicePlanSku | Optional, the pricing tier to use for the app service plan. Default is `S1`. |
| newWebAppName | The name of the app service to create for the bot. Must be a globally-unique web app name. Default is the value for the `botId` parameter. |
| tenantId | For users-assigned managed identity and single-tenant app types, the Microsoft Entra ID tenant to use for bot authentication. Default is your subscription tenant ID. |

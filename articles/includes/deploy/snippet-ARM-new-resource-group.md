---
description: Use Azure CLI to create the application service in a new resource group.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 11/19/2021
---

In this step, you create a bot application service which sets the deployment stage for the bot. You use an ARM template, a new service plan and a new resource group. Run the following Azure CLI command to start a deployment at subscription scope from a local template file.

This step can take a few minutes to complete.

> [!IMPORTANT]
> **Web App Bot** and **Bot Channels Registration** are deprecated but existing resources will continue to work. Bots created with a version 4.14.1.2 or later template will generate an Azure Bot resource.

### [User-assigned managed identity](#tab/userassigned)

```azurecli
az deployment sub create --template-file "<path-to-template-with-new-rg.json" --location <region-location-name> --parameters appType="UserAssignedMSI" appId="<client-id-from-previous-step>" tenantId="<azure-ad-tenant-id>" existingUserAssignedMSIName="<user-assigned-managed-identity-name>" existingUserAssignedMSIResourceGroupName="<user-assigned-managed-identity-resource-group>" botId="<id or bot-app-service-name>" botSku=F0 newAppServicePlanName="<new-service-plan-name>" newWebAppName="<bot-app-service-name>" groupName="<new-group-name>" groupLocation="<region-location-name>" newAppServicePlanLocation="<region-location-name>" --name "<bot-app-service-name>"
```

### [Single-tenant](#tab/singletenant)

```azurecli
az deployment create --template-file "<path-to-template-with-new-rg.json" --location <region-location-name> --parameters appType="SingleTenant" appId="<app-id-from-previous-step>" appSecret="<password-from-previous-step>" tenantId="<azure-ad-tenant-id>" botId="<id or bot-app-service-name>" botSku=F0 newAppServicePlanName="<new-service-plan-name>" newWebAppName="<bot-app-service-name>" groupName="<new-group-name>" groupLocation="<region-location-name>" newAppServicePlanLocation="<region-location-name>" --name "<bot-app-service-name>"
```

### [Multi-tenant](#tab/multitenant)

```azurecli
az deployment sub create --template-file "<path-to-template-with-new-rg.json>" --location <region-location-name> --parameters appType="MultiTenant" appId="<app-id-from-previous-step>" appSecret="<password-from-previous-step>" botId="<id or bot-app-service-name>" botSku=F0 newAppServicePlanName="<new-service-plan-name>" newWebAppName="<bot-app-service-name>" groupName="<new-group-name>" groupLocation="<region-location-name>" newAppServicePlanLocation="<region-location-name>" --name "<bot-app-service-name>"
```

---

| Option | Description |
|:-|:-|
| location | Location. |
| name | The deployment name. |
| parameters | Deployment parameters, provided as a list of key-value pairs. See the table of parameters for descriptions. |
| template-file | The path to the ARM template. Usually, the `template-with-new-rg.json` file is provided in the `deploymentTemplates` folder of the bot project. This is a path to an existing template file. It can be an absolute path, or relative to the current directory. All bot templates generate ARM template files. |

> [!TIP]
> Use the ARM template for a _new_ resource group, **template-with-new-rg.json**.

| Parameter | Description |
|:-|:-|
| appId | The *client ID* or *app ID* value from the JSON output generated in the [create the application registration](#create-the-azure-application-registration) step. |
| appSecret | For single-tenant and multi-tenant bots, the password you provided in the [create the application registration](#create-the-azure-application-registration) step. |
| appType | The type of app service to create: "MultiTenant", "SingleTenant", or "UserAssignedMSI". |
| botId | The name of the Azure Bot resource your created earlier. |
| botSku | The pricing tier: F0 (Free), or S1 (Standard). |
| existingUserAssignedMSIName | For user-assigned managed identity bots, the name of the identity resource you created in the previous step. |
| existingUserAssignedMSIResourceGroupName | For user-assigned managed identity bots, the name of resource group name in which you created the identity. |
| groupLocation | The location of the Azure resource group. |
| groupName | A name for the new resource group. |
| newAppServicePlanLocation | The location of the application service plan. |
| newAppServicePlanName | The name of the new application service plan. |
| newWebAppName | A name for the bot application service. |
| tenantId | For user-assigned managed identity and single-tenant bots, the bot's app tenant ID. |

> [!TIP]
>
> - Use `az account list-locations` to list supported regions for the current subscription.
> - Use `az config set defaults.location=<location>` to set he default location to use for all commands.

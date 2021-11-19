---
description: Use Azure CLI to create an application service in an existing resource group.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 11/19/2021
---

In this step, you create a bot application service that sets the deployment stage for the bot. When using an existing resource group, you can either use an existing app service plan or create a new one. Choose the option that works best for you:

- [**Option 1: Existing App Service Plan**](#option-1-existing-app-service-plan)
- [**Option 2: New App Service Plan**](#option-2-new-app-service-plan)

This step can take a few minutes to complete.

> [!IMPORTANT]
> **Web App Bot** and **Bot Channels Registration** are deprecated but existing resources will continue to work. Bots created with a version 4.14.1.2 or later template will generate an Azure Bot resource.

##### **Option 1: Existing App Service Plan**

In this case, we are using an existing App Service Plan, but creating a new Web App and Azure Bot resource.

This command below sets the bot's ID and display name. The `botId` parameter should be globally unique and is used as the immutable bot ID. The bot's display name is mutable.

### [User-assigned managed identity](#tab/userassigned)

```azurecli
az group deployment create --resource-group "<name-of-resource-group>" --template-file "<path-to-template-with-preexisting-rg.json>" --parameters appId="<client-id-from-previous-step>" appType="UserAssignedMSI" tenantId="<azure-ad-tenant-id>" existingUserAssignedMSIName="<user-assigned-managed-identity-name>" existingUserAssignedMSIResourceGroupName="<user-assigned-managed-identity-resource-group>" botId="<id or bot-app-service-name>" newWebAppName="<bot-app-service-name>" existingAppServicePlan="<name-of-app-service-plan>" appServicePlanLocation="<region-location-name>" --name "<bot-app-service-name>"
```

### [Single-tenant](#tab/singletenant)

```azurecli
az group deployment create --resource-group "<name-of-resource-group>" --template-file "<path-to-template-with-preexisting-rg.json>" --parameters appId="<app-id-from-previous-step>" appSecret="<password-from-previous-step>" appType="SingleTenant" tenantId="<azure-ad-tenant-id>" botId="<id or bot-app-service-name>" newWebAppName="<bot-app-service-name>" existingAppServicePlan="<name-of-app-service-plan>" appServicePlanLocation="<region-location-name>" --name "<bot-app-service-name>"
```

### [Multi-tenant](#tab/multitenant)

```azurecli
az deployment group create --resource-group "<name-of-resource-group>" --template-file "<path-to-template-with-preexisting-rg.json>" --parameters appId="<app-id-from-previous-step>" appSecret="<password-from-previous-step>" botId="<id or bot-app-service-name>" newWebAppName="<bot-app-service-name>" existingAppServicePlan="<name-of-app-service-plan>" appServicePlanLocation="<region-location-name>" --name "<bot-app-service-name>"
```

---

##### **Option 2: New App Service Plan**

In this case, we are creating App Service Plan, Web App, and Azure Bot resource.

### [User-assigned managed identity](#tab/userassigned)

```azurecli
az group deployment create --resource-group "<name-of-resource-group>" --template-file "<path-to-template-with-preexisting-rg.json>" --parameters appId="<client-id-from-previous-step>" appType="UserAssignedMSI" tenantId="<azure-ad-tenant-id>" existingUserAssignedMSIName="<user-assigned-managed-identity-name>" existingUserAssignedMSIResourceGroupName="<user-assigned-managed-identity-resource-group>" botId="<id or bot-app-service-name>" newWebAppName="<bot-app-service-name>" newAppServicePlanName="<name-of-app-service-plan>" appServicePlanLocation="<region-location-name>" --name "<bot-app-service-name>"
```

### [Single-tenant](#tab/singletenant)

```azurecli
az group deployment create --resource-group "<name-of-resource-group>" --template-file "<path-to-template-with-preexisting-rg.json>" --parameters appId="<app-id-from-previous-step>" appSecret="<password-from-previous-step>" appType="SingleTenant" tenantId="<azure-ad-tenant-id>" botId="<id or bot-app-service-name>" newWebAppName="<bot-app-service-name>" newAppServicePlanName="<name-of-app-service-plan>" appServicePlanLocation="<region-location-name>" --name "<bot-app-service-name>"
```

### [Multi-tenant](#tab/multitenant)

```azurecli
az deployment group create --resource-group "<name-of-resource-group>" --template-file "<path-to-template-with-preexisting-rg.json>" --parameters appId="<app-id-from-previous-step>" appSecret="<password-from-previous-step>" botId="<id or bot-app-service-name>" newWebAppName="<bot-app-service-name>" newAppServicePlanName="<name-of-app-service-plan>" appServicePlanLocation="<region-location-name>" --name "<bot-app-service-name>"
```

---

| Option | Description |
|:-|:-|
| name | The deployment name. |
| parameters | Deployment parameters, provided as a list of key-value pairs. See the table of parameters for descriptions. |
| resource-group | Name of the Azure resource group. |
| template-file | The path to the ARM template. Usually, the  `template-with-preexisting-rg.json` file is provided in the `deploymentTemplates` folder of the project. This is a path to an existing template file. It can be an absolute path, or relative to the current directory. All bot templates generate ARM template files. |

> [!TIP]
> Use the ARM template for an _existing_ resource group, **template-with-preexisting-rg.json**.

| Parameter | Description |
|:-|:-|
| appId | The *client ID* or *app ID* value from the JSON output generated in the [create the application registration](#create-the-azure-application-registration) step. |
| appSecret | For single-tenant and multi-tenant bots, the password you provided in the [create the application registration](#create-the-azure-application-registration) step. |
| appServicePlanLocation | The location for the new *app service plan*. |
| appType | The type of *app service* to create: "MultiTenant", "SingleTenant", or "UserAssignedMSI". |
| botId | The name of the Azure Bot resource your created earlier. |
| existingAppServicePlan | The name of the existing *app service plan* to use. |
| existingUserAssignedMSIName | For user-assigned managed identity bots, the name of the identity resource you created in the previous step. |
| existingUserAssignedMSIResourceGroupName | For user-assigned managed identity bots, the name of resource group name in which you created the identity. |
| newAppServicePlanName | The name of the new *app service plan* to create. |
| newWebAppName | The name of the *app service* to create. |
| tenantId | For user-assigned managed identity and single-tenant bots, the bot's app tenant ID. |

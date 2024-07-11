---
description: Use Azure CLI to create an application service in an existing resource group.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.custom:
  - devx-track-azurecli
  - evergreen
ms.date: 10/11/2022
---

> [!IMPORTANT]
> Python bots can't be deployed to a resource group that contains Windows services or bots.
> Multiple Python bots can be deployed to the same resource group; however, you need to create other services (such as Azure AI services) in another resource group.

### [User-assigned managed identity](#tab/userassigned)

For a user-assigned managed identity bot, run one of the following commands to provision resources in an existing resource group.

- To use an _existing_ app service plan.

  ```azurecli
  az deployment group create --resource-group "<group-name>" --template-file "<path>" --parameters appId="<client-id>" appType="UserAssignedMSI" tenantId="<tenant-id>" existingUserAssignedMSIName="<identity>" existingUserAssignedMSIResourceGroupName="<identity-group>" botId="<bot-id>" newWebAppName="<service-name>" existingAppServicePlan="<plan-name>" appServicePlanLocation="<plan-region>" --name "<deployment-name>"
  ```

- To use a _new_ app service plan.

  ```azurecli
  az deployment group create --resource-group "<group-name>" --template-file "<path>" --parameters appId="<client-id>" appType="UserAssignedMSI" tenantId="<tenant-id>" existingUserAssignedMSIName="<identity>" existingUserAssignedMSIResourceGroupName="<identity-group>" botId="<bot-id>" newWebAppName="<service-name>" newAppServicePlanName="<plan-name>" appServicePlanLocation="<plan-region>" --name "<deployment-name>"
  ```

### [Single-tenant](#tab/singletenant)

For a single-tenant bot, run one of the following commands to provision resources in an existing resource group.

- To use an _existing_ app service plan.

  ```azurecli
  az deployment group create --resource-group "<group-name>" --template-file "<path>" --parameters appId="<app-id>" appSecret="<password>" appType="SingleTenant" tenantId="<tenant-id>" botId="<bot-id>" newWebAppName="<service-name>" existingAppServicePlan="<plan-name>" appServicePlanLocation="<plan-region>" --name "<deployment-name>"
  ```

- To use a _new_ app service plan.

  ```azurecli
  az deployment group create --resource-group "<group-name>" --template-file "<path>" --parameters appId="<app-id>" appSecret="<password>" appType="SingleTenant" tenantId="<tenant-id>" botId="<bot-id>" newWebAppName="<service-name>" newAppServicePlanName="<plan-name>" appServicePlanLocation="<plan-region>" --name "<deployment-name>"
  ```

### [Multi-tenant](#tab/multitenant)

For a multi-tenant bot, run one of the following commands to provision resources in an existing resource group.

- To use an _existing_ app service plan.

  ```azurecli
  az deployment group create --resource-group "<group-name>" --template-file "<path>" --parameters appId="<app-id>" appSecret="<password>" botId="<bot-id>" newWebAppName="<service-name>" existingAppServicePlan="<plan-name>" appServicePlanLocation="<plan-region>" --name "<deployment-name>"
  ```

- To use a _new_ app service plan.

  ```azurecli
  az deployment group create --resource-group "<group-name>" --template-file "<path>" --parameters appId="<app-id>" appSecret="<password>" botId="<bot-id>" newWebAppName="<service-name>" newAppServicePlanName="<plan-name>" appServicePlanLocation="<plan-region>" --name "<deployment-name>"
  ```

---

This table describes the command _options_ for the `az deployment group create` command.

| Option         | Description                                                                                                                                            |
|:---------------|:-------------------------------------------------------------------------------------------------------------------------------------------------------|
| name           | The deployment name.                                                                                                                                   |
| parameters     | Deployment parameters for the ARM template, provided as a list of key-value pairs. See the table of parameters for descriptions.                       |
| resource-group | Name of the Azure resource group.                                                                                                                      |
| template-file  | The path to the `template-with-preexisting-rg.json` ARM template in the project's _deployment templates_ folder. The path can be relative or absolute. |

This table describes the _deployment parameters_ to use with the `--parameters` command option.
Not all parameters apply to all app types, and some parameters are specific to using and existing app service or to creating a new app service.

| Parameter | Description |
|:-|:-|
| appId | The client or app ID from the identity resource you created earlier. This is used as the Microsoft app ID of the web app. |
| appSecret | For single-tenant and multi-tenant app types, the password for the identity resource you created earlier. |
| appServicePlanLocation | The region in which to create the application service plan. |
| appType | The type of app service to create: `UserAssignedMSI`, `SingleTenant`, or `MultiTenant`. Default is `MultiTenant`. |
| botId | The ID of the Azure Bot resource to create. The bot ID is immutable. |
| botSku | Optional, the pricing tier to use for the bot: `F0` (Free) or `S1` (Standard). Default is `F0`. |
| existingAppServicePlan | The name of the existing app service plan to use. |
| existingUserAssignedMSIName | For user-assigned managed identity app types, the name of the identity resource you created earlier. |
| existingUserAssignedMSIResourceGroupName | For user-assigned managed identity app types, the name of the resource group for your identity resource. |
| newAppServicePlanName | The name of the app service plan to create for the bot. |
| newAppServicePlanSku | Optional, the pricing tier to use for the app service plan. Default is `S1`. |
| newWebAppName | The name of the app service to create for the bot. Must be a globally-unique web app name. Default is the value for the `botId` parameter. |
| tenantId | For user-assigned managed identity or single-tenant bots, the bot's app tenant ID. |

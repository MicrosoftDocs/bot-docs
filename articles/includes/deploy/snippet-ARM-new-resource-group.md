In this step, you create a bot application service which sets the deployment stage for the bot. You use an ARM template, a new service plan and a new resource group. The result is a **registration subscription id**; copy it to use in the next step.

```cmd
az deployment create --name "<bot-app-service-name>" --template-file "template-with-new-rg.json" --location "region-location-name" --parameters appId="<app-id-from-previous-step>" appSecret="<password-from-previous-step>" botId="<bot-app-service-name>" botSku=F0 newAppServicePlanName="<new-service-plan-name>" newWebAppName="<bot-app-service-name>" groupName="<new-group-name>" groupLocation="region-location-name" newAppServicePlanLocation="region-location-name"
```

| Option   | Description |
|:---------|:------------|
| name | Bot application service name. It is listed in the Azure portal in the general resources list and in the resource group it belongs. |
| template-file | The path to the ARM template. Usually the `template-with-new-rg.json` file is provided in the `deploymentTemplates` folder of the bot project. |
| location |Location. Values from: `az account list-locations`. You can configure the default location using `az configure --defaults location=<location>`. |
| parameters | Enter the following deployment parameter values:

| Name   | Value |
|:---------|:------------|
|`appId`|The *app id* value you got in the previous step.|
|`appSecret`|The password you provided in the previous step.|
|`botId` | The name of the bot service application. It must be globally unique ans is used as the immutable bot ID. It is also used to configure the display name, which is mutable. |
|`botSku` |The pricing tier; it can be F0 (Free) or S1 (Standard).|
|`newAppServicePlanName` |The name of the new application service plan.|
|`newWebAppName` |The name of the bot application service.|
|`groupName` |The name of the new resource group.|
|`groupLocation` |The location of the Azure resource group.|
|`newAppServicePlanLocation` |The location of the application service plan. |

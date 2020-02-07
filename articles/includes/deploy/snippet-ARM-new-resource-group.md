In this step, you create a bot application service which sets the deployment stage for the bot. You use an ARM template, a new service plan and a new resource group. The result is a **registration subscription id**; copy it to use in the next step.

```cmd
az deployment create --name "<bot-app-service-name>" --template-file "template-with-new-rg.json" --location "region-location-name" --parameters appId="<app-id-from-previous-step>" appSecret="<password-from-previous-step>" botId="<bot-app-service-name>" botSku=F0 newAppServicePlanName="<new-service-plan-name>" newWebAppName="<bot-app-service-name>" groupName="<new-group-name>" groupLocation="region-location-name" newAppServicePlanLocation="region-location-name"
```

| Option   | Description |
|:---------|:------------|
| name | Bot application service name. It is listed in the Azure portal in the general resources list and in the resource group it belongs. |
| template-file | The path to the ARM template. Usually the `template-with-new-rg.json` file is provided in the `deploymentTemplates` folder of the bot project. |
| location |Location. Values from: `az account list-locations`. You can configure the default location using `az configure --defaults location=<location>`. |
| parameters | Provide deployment parameter values. `appId` value you got in the previous step. `appSecret` is the password you provided in the previous step. The `botId` is the name of the bot application  that is globally unique used as the immutable bot ID. It is also used to configure the display name of the bot app service, which is mutable. `botSku` is the pricing tier and can be F0 (Free) or S1 (Standard). `newAppServicePlanName` is the name of application service plan. `newWebAppName` is the name of the app service you are creating. `groupName` is the name of the Azure resource group you are creating. `groupLocation` is the location of the Azure resource group. `newAppServicePlanLocation` is the location of the app service plan. |

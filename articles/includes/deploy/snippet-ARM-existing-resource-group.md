When using an existing resource group, you can either use an existing App Service Plan or create a new one. Steps for both options are listed below.

**Option 1: Existing App Service Plan**

In this case, we are using an existing App Service Plan, but creating new a Web App and Bot Channels Registration.

> [!NOTE]
> This command sets the bot's ID and display name. The `botId` parameter should be globally unique and is used as the immutable bot ID. The bot's display name is mutable.

```cmd
az group deployment create --name "<bot-app-service-name>" --resource-group "<name-of-resource-group>" --template-file "template-with-preexisting-rg.json" --parameters appId="<app-id-from-previous-step>" appSecret="<password-from-previous-step>" botId="<id or bot-app-service-name>" newWebAppName="<bot-app-service-name>" existingAppServicePlan="<name-of-app-service-plan>" appServicePlanLocation="<region-location-name>"
```

**Option 2: New App Service Plan**

In this case, we are creating App Service Plan, Web App, and Bot Channels Registration.

```cmd
az group deployment create --name "<bot-app-service-name>" --resource-group "<name-of-resource-group>" --template-file "template-with-preexisting-rg.json" --parameters appId="<app-id-from-previous-step>" appSecret="<password-from-previous-step>" botId="<id or bot-app-service-name>" newWebAppName="<bot-app-service-name>" newAppServicePlanName="<name-of-app-service-plan>" appServicePlanLocation="<region-location-name>"
```

| Option   | Description |
|:---------|:------------|
| name | Bot application service name. It is listed in the Azure portal in the general resources list and in the resource group it belongs.|
| resource-group | Name of the azure resource group. |
| template-file | The path to the ARM template. Usually, the  `template-with-preexisting-rg.json` file is provided in the `deploymentTemplates` folder of the project. |
| location |Location. Values from: `az account list-locations`. You can configure the default location using `az configure --defaults location=<location>`. |
| parameters | Enter the following parameter values:

- `appId` - The *app id* value you got in the previous step.
- `appSecret` - The password you provided in the previous step.
- `botId` - The name of the bot service application. It must be globally unique. It is used as the immutable bot ID. It is also used to configure the display name, which is mutable.
- `newWebAppName` - The name of the bot application service.
- `newAppServicePlanName` - The name of the application service plan.
- `newAppServicePlanLocation` - The location of the application service plan.
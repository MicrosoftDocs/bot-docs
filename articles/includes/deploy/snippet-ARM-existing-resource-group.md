When using an existing resource group, you can either use an existing App Service Plan or create a new one. Steps for both options are listed below. 

**Option 1: Existing App Service Plan** 

In this case, we are using an existing App Service Plan, but creating new a Web App and Bot Channels Registration. 

> [!NOTE]
> This command sets the bot's ID and display name. The `botId` parameter should be globally unique and is used as the immutable bot ID. The bot's display name is mutable.

```cmd
az group deployment create --name "<name-of-deployment>" --resource-group "<name-of-resource-group>" --template-file "template-with-preexisting-rg.json" --parameters appId="<msa-app-guid>" appSecret="<msa-app-password>" botId="<id-or-name-of-bot>" newWebAppName="<name-of-web-app>" existingAppServicePlan="<name-of-app-service-plan>" appServicePlanLocation="<location>"
```

**Option 2: New App Service Plan**

In this case, we are creating App Service Plan, Web App, and Bot Channels Registration. 

```cmd
az group deployment create --name "<name-of-deployment>" --resource-group "<name-of-resource-group>" --template-file "template-with-preexisting-rg.json" --parameters appId="<msa-app-guid>" appSecret="<msa-app-password>" botId="<id-or-name-of-bot>" newWebAppName="<name-of-web-app>" newAppServicePlanName="<name-of-app-service-plan>" appServicePlanLocation="<location>"
```

| Option   | Description |
|:---------|:------------|
| name | Friendly name for the deployment. |
| resource-group | Name of the azure resource group. |
| template-file | The path to the ARM template. You can use `template-with-preexisting-rg.json` file provided in the `deploymentTemplates` folder of the project. |
| location |Location. Values from: `az account list-locations`. You can configure the default location using `az configure --defaults location=<location>`. |
| parameters | Provide deployment parameter values. `appId` value you got from running the `az ad app create` command. `appSecret` is the password you provided in the previous step. The `botId` parameter should be globally unique and is used as the immutable bot ID. It is also used to configure the display name of the bot, which is mutable. `newWebAppName` is the name of the Web App you are creating. `newAppServicePlanName` is the name of App Service Plan. `newAppServicePlanLocation` is the location of the App Service Plan. |

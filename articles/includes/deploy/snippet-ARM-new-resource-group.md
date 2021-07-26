In this step, you create a bot application service which sets the deployment stage for the bot. You use an ARM template, a new service plan and a new resource group. Run the following Azure cli command to start a deployment at subscription scope from a local template file.

> [!TIP]
> Use the ARM template for a _new_ resource group, **template-with-new-rg.json**.

```azurecli
az deployment sub create --template-file "<path-to-template-with-new-rg.json" --location <region-location-name> --parameters appId="<app-id-from-previous-step>" appSecret="<password-from-previous-step>" botId="<id or bot-app-service-name>" botSku=F0 newAppServicePlanName="<new-service-plan-name>" newWebAppName="<bot-app-service-name>" groupName="<new-group-name>" groupLocation="<region-location-name>" newAppServicePlanLocation="<region-location-name>" --name "<bot-app-service-name>"
```

This step can take a few minutes to complete.

> [!IMPORTANT]
> **Web App Bot** and **Bot Channels Registration** will be deprecated but existing resources will continue to work. Bots created with a version 4.13.0 or later template will generate an Azure Bot resource.

| Option   | Description |
|:---------|:------------|
| name | The deployment name.|
| template-file | The path to the ARM template. Usually, the `template-with-new-rg.json` file is provided in the `deploymentTemplates` folder of the bot project. This is a path to an existing template file. It can be an absolute path, or relative to the current directory. All bot templates generate ARM template files.|
| location |Location. Values from: `az account list-locations`. You can configure the default location using `az config set defaults.location=<location>`. |
| parameters | Deployment parameters, provided as a list of key=value pairs. Enter the following parameter values:

- `appId` - The *app id* value from the JSON output generated in the [create the application registration](#create-the-azure-application-registration) step.
- `appSecret` - The password you provided in the [create the application registration](#create-the-azure-application-registration) step.
- `botId` - A name for the  Bot Channels Registration resource to create. It must be globally unique. It is used as the immutable bot ID. It is also used as the default display name, which is mutable.
- `botSku` - The pricing tier; it can be F0 (Free) or S1 (Standard).
- `newAppServicePlanName` - The name of the new application service plan.
- `newWebAppName` - A name for the bot application service.
- `groupName` - A name for the new resource group.
- `groupLocation` - The location of the Azure resource group.
- `newAppServicePlanLocation` - The location of the application service plan.

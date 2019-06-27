1. Go to the [**Application Registration Portal**](https://portal.azure.com/#blade/Microsoft_AAD_RegisteredApps/ApplicationsListBlade).
1. Click on **Add an app** to register your application, create **Application Id**, and **Generate New Password**. If you already have an application and password but don't remember the password, you will have to generate a new password in the Application secrets section.
1. Save both application ID and the new password you just generated, so you that can use them with the `az bot create` command.  

```cmd
az bot create --kind webapp --name <bot-resource-name> --location <geographic-location> --version v4 --lang <language> --verbose --resource-group <resource-group-name> --appid "<application-id>" --password "<application-password>" --verbose
```

| Option | Description |
|:---|:---|
| --name | A unique name that is used to deploy the bot in Azure. It could be the same name as your local bot. DO NOT include spaces or underscores in the name. |
| --location | Geographic location used to create the bot service resources. For example, `eastus`, `westus`, `westus2`, and so on. |
| --lang | The language to use to create the bot: `Csharp`, or `Node`; default is `Csharp`. |
| --resource-group | Name of resource group in which to create the bot. You can configure the default group using `az configure --defaults group=<name>`. |
| --appid | The Microsoft account ID (MSA ID) to be used with the bot. |
| --password | The Microsoft account (MSA) password for the bot. |

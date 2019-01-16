```cmd
az bot create --kind webapp --name <bot-resource-name> --location <geographic-location> --version v4 --lang <language> --verbose --resource-group <resource-group-name>
```

| Option | Description |
|:---|:---|
| --name | A unique name that is used to deploy the bot in Azure. It could be the same name as your local bot. DO NOT include spaces or underscores in the name. |
| --location | Geographic location used to create the bot service resources. For example, `eastus`, `westus`, `westus2`, and so on. |
| --lang | The language to use to create the bot: `Csharp`, or `Node`; default is `Csharp`. |
| --resource-group | Name of resource group in which to create the bot. You can configure the default group using `az configure --defaults group=<name>`. |
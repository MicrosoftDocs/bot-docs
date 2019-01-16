Use a temporary directory outside of your current project directory. 

This command will create a subdirectory under the save-path; however, the specified path must already exist.

```cmd
az bot download --name <bot-resource-name> --resource-group <resource-group-name> --save-path "<path>"
```

| Option | Description |
|:---|:---|
| --name | The name of the bot in Azure. |
| --resource-group | Name of resource group the bot is in. |
| --save-path | An existing directory to download bot code to. |
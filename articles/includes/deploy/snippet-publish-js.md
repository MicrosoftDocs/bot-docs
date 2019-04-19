To publish your local JavaScript bot back to Azure, you must first manually create a single zipped file containing all files used to locally build and run your bot. This includes all npm libraries downloaded into your `node_modules` folder. When creating this zip file _make sure that the root directory you use is the same directory where your index.js file resides_.

Once a zip file containing all of your bot's source code has been created, open a command prompt window and run the following _Az cli_ command. 

This step might take a while.

```cmd
az webapp deployment source config-zip --resource-group <resource-group-name> --name <bot-resource-name> --src <directory-path>
```

| Option | Description |
|:---|:---|
| --resource-group | Name of resource group in Azure. |
| --name | The resource name of the bot in Azure. |
| --src | The full directory path to upload your zipped bot code from. For example `c:\my-local-repository\this-app-folder\my-zipped-code.zip` |

Once this completes successfully, your bot is deployed in Azure.

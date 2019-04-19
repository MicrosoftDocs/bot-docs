Publish your local bot to Azure. This step might take a while.

```cmd
az bot publish --name <bot-resource-name> --proj-file-path "<project-file-name>" --resource-group <resource-group-name> --code-dir <directory-path> --verbose --version v4
```

| Option | Description |
|:---|:---|
| --name | The resource name of the bot in Azure. |
| --proj-file-path | For C#, use the startup project file name (without the .csproj) that needs to be published. For example: `EnterpriseBot`. For Node.js, use the main entry point for the bot. For example, `index.js`. |
| --resource-group | Name of resource group. |
| --code-dir | The directory to upload bot code from. |

Once this completes with a "Deployment successful!" message, your bot is deployed in Azure.

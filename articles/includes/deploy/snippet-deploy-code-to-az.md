At this point we are ready to deploy the code to the Azure Web App. Run the following command from the command line to perform deployment using the kudu zip push deployment for a web app.

```cmd
az webapp deployment source config-zip --resource-group "<resource-group-name>" --name "<name-of-web-app>" --src <project-zip-path>
```

| Option   | Description |
|:---------|:------------|
| resource-group | The name of the Azure resource group that contains your bot. (This will be the resource group you used or created when creating the app registration for your bot.) |
| name | Name of the Web App you used earlier. |
| src  | The path to the zipped project file you created. |

> [!NOTE]
> This step can take a few minutes to complete.
> Also it can take a few more minutes between when the deployment finishes and when your bot is available to test.
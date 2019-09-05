Once you've created and tested a bot locally, you can deploy it to Azure. Open a command prompt to log in to the Azure portal.

```cmd
az login
```
A browser window will open, allowing you to sign in.

> [!NOTE]
> If you deploy your bot to a non-Azure cloud such as US Gov, you need to run `az cloud set --name <name-of-cloud>` before `az login`, where &lt;name-of-cloud> is the name of a registered cloud, such as `AzureUSGovernment`. If you want to go back to public cloud, you can run `az cloud set --name AzureCloud`. 

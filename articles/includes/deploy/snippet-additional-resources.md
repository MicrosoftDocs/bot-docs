When you deploy a bot, typically these resources are created in the Azure portal:

| Resources      | Description |
|----------------|-------------|
| Web App Bot | An Azure Bot Service bot that is deployed to an Azure App Service.|
| [App Service](https://docs.microsoft.com/en-us/azure/app-service/)| Enables you to build and host web applications.|
| [App Service plan](https://docs.microsoft.com/en-us/azure/app-service/azure-web-sites-web-hosting-plans-in-depth-overview)| Defines a set of compute resources for a web app to run.|

If you create your bot through the Azure portal, you are able to provision additional resources, like [Application Insights for telemetry](~/v4sdk/bot-builder-telemetry.md).

To see documentation on `az bot` commands, see the [reference](https://docs.microsoft.com/en-us/cli/azure/bot?view=azure-cli-latest) topic.

If you are unfamiliar with Azure resource group, see this [terminology](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-group-overview#terminology) topic.
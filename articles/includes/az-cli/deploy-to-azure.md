---
description: Use Azure CLI to deploy your bot files to Azure.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.custom:
  - devx-track-azurecli
  - evergreen
ms.date: 03/03/2022
---

At this point, you're ready to deploy code for your bot to your App Service resource.

> [!NOTE]
> This step can take a few minutes to complete.
> Also it can take a few more minutes between when the deployment finishes and when your bot is available to test.

### [C# / JavaScript / Python](#tab/csharp+javascript+python)

Run the following command from the command line to perform deployment using the Kudu zip push deployment for your app service (web app).

```azurecli
az webapp deployment source config-zip --resource-group "<resource-group-name>" --name "<name-of-app-service>" --src "<project-zip-path>"
```

| Option         | Description                                                           |
|:---------------|:----------------------------------------------------------------------|
| resource-group | The name of the Azure resource group that contains your bot.          |
| name           | Name of the app service you used earlier.                             |
| src            | The absolute or relative path to the zipped project file you created. |

> [!TIP]
> By default, this command deploys to the production slot. Use the optional `--slot` parameter to specify a different slot.
> For more information, see the [az webapp deployment source config-zip](/cli/azure/webapp/deployment/source) command reference.

### [Java](#tab/java)

In the project directory, run the following command from the command line.

```console
mvn azure-webapp:deploy -Dgroupname="<resource-group-name>" -Dbotname="<name-of-web-app>"
```

| Option     | Description                                                  |
|:-----------|:-------------------------------------------------------------|
| Dgroupname | The name of the Azure resource group that contains your bot. |
| Dbotname   | Name of the app service you used earlier.                    |

---

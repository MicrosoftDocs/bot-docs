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

Run the [`az webapp deploy` command](/cli/azure/webapp#az-webapp-deploy) from the command line to perform deployment using the Kudu zip push deployment for your app service (web app).

| Option         | Description                                                           |
|:---------------|:----------------------------------------------------------------------|
| resource-group | The name of the Azure resource group that contains your bot.          |
| name           | Name of the app service you used earlier.                             |
| src            | The absolute or relative path to the zipped project file you created. |

> [!TIP]
> By default, this command deploys to the production slot. Use the optional `--slot` parameter to specify a different slot.
> For more information, see the [`az webapp deploy` command reference documentation](/cli/azure/webapp#az-webapp-deploy).

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

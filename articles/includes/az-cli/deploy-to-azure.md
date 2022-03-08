---
description: Use Azure CLI to deploy your bot files to Azure.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 03/03/2022
---

At this point, we are ready to deploy the code to the Azure Web App.

### [C# / JavaScript / Python](#tab/csharp+javascript+python)

Run the following command from the command line to perform deployment using the Kudu zip push deployment for a web app.

```azurecli
az webapp deployment source config-zip --resource-group "<resource-group-name>" --name "<name-of-web-app>" --src "<project-zip-path>"
```

| Option         | Description                                                  |
|:---------------|:-------------------------------------------------------------|
| resource-group | The name of the Azure resource group that contains your bot. |
| name           | Name of the Web App you used earlier.                        |
| src            | The path to the zipped project file you created.             |

### [Java](#tab/java)

In the project directory, run the following command from the command line.

```console
mvn azure-webapp:deploy -Dgroupname="<resource-group-name>" -Dbotname="<name-of-web-app>"
```

| Option     | Description                                                  |
|:-----------|:-------------------------------------------------------------|
| Dgroupname | The name of the Azure resource group that contains your bot. |
| Dbotname   | Name of the Web App you used earlier.                        |

---

> [!NOTE]
> This step can take a few minutes to complete.
> Also it can take a few more minutes between when the deployment finishes and when your bot is available to test.

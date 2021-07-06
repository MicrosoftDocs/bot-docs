<!-- Azure Bot Resource App Id and Password -->

### App Id and password

You need the Azure bot resource **app Id** and **password** to configure your bot for deployment. You will assign their values to the related variables: `MicrosoftAppId` and `MicrosoftAppPassword` contained in your bot project configuration file. The file differs depending on the programming language you use to create the bot, as shown below.

### [C#](#tab/csharp)

The `appsettings.json` file contains these settings:

```json
{
  "MicrosoftAppId": "<your app id>",
  "MicrosoftAppPassword": "<your password>"
}
```

### [JavaScript](#tab/javascript)

The `.env` file contains these settings:

```javascript
MicrosoftAppId="<your app id>"
MicrosoftAppPassword="<your password>"
```

### [Java](#tab/java)

The `application.properties` file contains these settings:

```java
MicrosoftAppId="<your app id>"
MicrosoftAppPassword="<your password>"
```

### [Python](#tab/python)

The `config.py` file contains these settings:

```python
APP_ID = os.environ.get("MicrosoftAppId", "<your app id>")
APP_PASSWORD = os.environ.get("MicrosoftAppPassword", "<your password>")
```

---
 
<!-- If you downloaded the deployment details, you can get the App Id and password from there to configure your bot. Otherwise, you must perform additional steps as described below.--> 

<!-- 
#### Create a new password

1. In your browser, select the bot resource you just created and listed in the resource group.
1. In the left pane, in the *Settings* section, select **Configuration**. 
1. In the right pane, select the **Manage** link in parenthesis, on the right of *Microsoft App ID*. 
1. In the newly displayed panel on the right, in the *Client secrets* section, select **New client secret**
1. In the right pane, for the new client secret, enter a *description* and the *expiration date*.  
1. Select **Add**.
1. Copy and store in a safe place the **Value** (client secret or password) and the **ID** (App Id).

After the creation of the resource is completed, download the deployment details and keep them in a safe place. They contain the App Id and password. 

-->
#### Get Azure bot resource app Id
 
1. Go to the [Azure portal](https://portal.azure.com).
1. Select the Azure bot resource to obtain its app Id.
1. In the left pane, in the **Settings** section, select **Configuration**. 
1. Copy and save the value contained in the *Microsoft App ID* box.

#### Get Azure bot resource password from Azure Key Vault

When Azure creates the Azure Bot resource, it stores the app password in Azure Key Vault. For information on how to access the key vault to obtain your password, see:

- [Use Key Vault references for App Service and Azure Functions](/azure/app-service/app-service-key-vault-references).
- [About Azure Key Vault](/azure/key-vault/general/overview) 
- [Assign a Key Vault access policy using the Azure portal](/azure/key-vault/general/assign-access-policy-portal) 
- [Quickstart: Set and retrieve a secret from Azure Key Vault using the Azure portal](/azure/key-vault/secrets/quick-create-portal#retrieve-a-secret-from-key-vault)


<!-- Alternatively, you can perform the following steps, also 
To access the client secret stored in the Azure key vault follow the steps described below.

1. Go to the [Azure portal](https://portal.azure.com).
1. Select the Azure key vault resource you need to access.
1. In the left pane, select **Access polices**.
1. In the right pane, select **Add Access Policy**. 
1. In the **Add access policy** pane, enter the following values:
    1. **Secrets permissions**. From the drop-down list, select *Get*, and *List*. 
        :::image type="content" source="~/media/azure-bot-resource/key-vault-secret-permissions.png" alt-text="Set Azure key vault secret permissions":::
    1. **Select principal**. Select the **None selected** link. 
        1. In the **Principal** pane on the right, select a directory member by entering *object Id, name or email address*. 
        1. Select **Select**. 
            :::image type="content" source="~/media/azure-bot-resource/key-vault-principal.png" alt-text="Set Azure key vault principal":::
    1. Select **Add**.
1. Select **Save**.
1. In the left pane, select **Secrets**. 
1. In the right pane, the secret key name is displayed.
1. Select it to obtain its value. Copy and save this value.

Assign the saved values to `MicrosoftAppId` and `MicrosoftAppPassword`.
-->

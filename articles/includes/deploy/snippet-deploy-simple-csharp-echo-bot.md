### Deploy Csharp bot steps

In a terminal window, you can execute following steps to deploy a bot to Azure. You must substitute the place holders, in angle brackets,  with your actual values.

1. Log into Azure portal

    ```bash
    az login
    ```

1. Set subscription

    ```bash
    az account set --subscription "<azure subscription>"`
    ```

1. Create a bot channels registration

    Copy the app id obtained and the password you entered to use in the next step.

    ```bash
    az ad app create --display-name "<bot channels registration name>" --password "<your password>" --available-to-other-tenants
    ```

1. Set new deployment using ARM template, service plan and resource group

    This step creates a bot service application. You can verify that both the bot service registration and the bot service application are created in the [Azure portal](https://ms.portal.azure.com/).

    Copy the subscription id obtained to use in the next step. This is the numeric value in `"id::/subscription/numeric value/....`

    ```bash
    az deployment create --name "<bot channels registration name>" --template-file "<bot project dir>\DeploymentTemplates\template-with-new-rg.json" --location "westus2" --parameters appId="<app id from previous step>" appSecret="<password from previous step>" botId="<bot channels registration name>" botSku=F0 newAppServicePlanName="<your bot service plan>" newWebAppName="<bot channels registration name>" groupName="<your bot resource group>" groupLocation="westus2" newAppServicePlanLocation="westus2"
    ```

1. Optionally, check app Id and password

    Use the app id and the password values in the project `appsettings.json` file.

    ```bash
    az webapp config appsettings list -g <your bot resource group> -n <bot channels registration name> --subscription <subscription Id form previous step>
    ```

1. Before creating a `.deployment` file add the the bot channel registration app id, and the password to the project `appsettings.json` file.

1. Create a `.deployment` file within the bot project folder.

    ```bash
    az bot prepare-deploy --lang Csharp --code-dir "<bot project dir>" --proj-file-path "<project name>.csproj"

1. In the project directory zip up all the files and folders. This produces an `<projec name>.zip`. file.

1. Deploy the bot

    This final step actually deploys the bot  to Azure.

    ```bash
    az webapp deployment source config-zip --resource-group "mm-bot-resource-group" --name "<bot channels registration name>" --src "<bot project dir>\<projec name>.zip"
    ```
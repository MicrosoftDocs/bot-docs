Before beginning the deployment, make sure you have the latest version of [Azure cli](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli?view=azure-cli-latest) and [dotnet cli](https://dotnet.microsoft.com/download). If you don't have dotnet cli, install it using the .Net Core Runtime option from the link provided above. 

### Login to Azure CLI and set your subscription
You've already created and tested a bot locally, and now you want to deploy it to Azure. Open a command prompt to log in to the Azure portal.

```cmd
az login
```
### Set the subscription

Set the default subscription to use.

```cmd
az account set --subscription "<azure-subscription>"
```

If you are not sure which subscription to use for deploying the bot, you can view the list of subscriptions for your account by using `az account list` command.

Navigate to the bot folder.
`cd <local-bot-folder>`

### Create a Web App Bot in Azure 

If you don't already have a resource group to which to publish your local bot, create one:

```cmd
az group create --name <resource-group-name> --location <geographic-location> --verbose
```

| Option     | Description |
|:-----------|:---|
| name     | A unique name for the resource group. DO NOT include spaces or underscores in the name. |
| location | Geographic location used to create the resource group. For example, `eastus`, `westus`, `westus2`, and so on. Use `az account list-locations` for a list of locations. |

Then, create the bot resource into which you will publish your bot. This will provision the necessary resources in Azure and create a bot web app, which you will overwrite with your local bot. 

Before proceeding, read the instructions that apply to you based on the type of email account you use to log in to Azure.

#### MSA email account
If you are using an MSA email account, you will need to create the app ID and app password on the Application Registration Portal to use with `az bot create` command.
1. Go to the [**Application Registration Portal**](https://portal.azure.com/#blade/Microsoft_AAD_RegisteredApps/ApplicationsListBlade).
1. Click on **Add an app** to register your application, create **Application Id**, and **Generate New Password**. If you already have an application and password but don't remember the password, you will have to generate a new password in the Application secrets section.
1. Save both application ID and the new password you just generated, so you that can use them with the `az bot create` command.  

```cmd
az bot create --kind webapp --name <bot-name-in-azure> --location <geographic-location> --version v4 --lang <language> --verbose --resource-group <resource-group-name> --appid "<application-id>" --password "<application-password>" --verbose
```

| Option | Description |
|:---|:---|
| name | A unique name that is used to deploy the bot in Azure. It could be the same name as your local bot. DO NOT include spaces or underscores in the name. |
| location | Geographic location used to create the bot service resources. For example, `eastus`, `westus`, `westus2`, and so on. |
| resource-group | Name of resource group in which to create the bot. You can configure the default group using `az configure --defaults group=<name>`. |
| appid | The Microsoft account ID (MSA ID) to be used with the bot. |
| password | The Microsoft account (MSA) password for the bot. |

#### Business or school account

```cmd
az bot create --kind webapp --name <bot-name-in-azure> --location <geographic-location> --version v4 --lang <language> --verbose --resource-group <resource-group-name>
```
| Option | Description |
|:---|:---|
| name | A unique name that is used to deploy the bot in Azure. It could be the same name as your local bot. DO NOT include spaces or underscores in the name. |
| location | Geographic location used to create the bot service resources. For example, `eastus`, `westus`, `westus2`, and so on. |
| lang | The language to use to create the bot: `Csharp`, or `Node`; default is `Csharp`. |
| resource-group | Name of resource group in which to create the bot. You can configure the default group using `az configure --defaults group=<name>`. |

#### Update appsettings.json or .env file
After the bot is created, you should see the following information displayed in the console window: 

```JSON
{
  "appId": "as234-345b-4def-9047-a8a44b4s",
  "appPassword": "34$#w%^$%23@334343",
  "endpoint": "https://mybot.azurewebsites.net/api/messages",
  "id": "mybot",
  "name": "mybot",
  "resourceGroup": "botresourcegroup",
  "serviceName": "mybot",
  "subscriptionId": "234532-8720-5632-a3e2-a1qw234",
  "tenantId": "32f955bf-33f1-43af-3ab-23d009defs47",
  "type": "abs"
}
```

You'll need to copy the `appId` and `appPassword` values and paste them into the appsettings.json or .env file. For example:

```JSON
{
  MicrosoftAppId: "as234-345b-4def-9047-a8a44b4s",
  MicrosoftAppPassword: "34$#w%^$%23@334343"
}
```
Note that if your appsettings.json or .env file has additional keys for other services you've provisioned for your bot, don't delete those entries.

Save the file.

Next, depending on the programming langauge (**C#** or **JS**) you used to create the bot, follow the steps that apply to you.

**C# Bot:** 

Open a command-prompt, and navigate to the project folder. Run the following commands from the command line.

| Task | Command |
|:-----|:--------|
| 1. Restore project dependencies | `dotnet restore`|
| 2. Build the project     | `dotnet build` |
| 3. Zip project files | Use any utility to zip the project files. Go to the folder that has the .csproj file and select all the files and folder at this level to create the zipped folder. |
| 4. Set build deployment setting | `az webapp config appsettings set --resource-group <resource-group-name> --name <bot-name> --settings SCM_DO_BUILD_DEPLOYMENT=false`|
| 5. Set the script generator args | `az webapp config appsettings set --resource-group <resource-group-name> --name <bot-name> --settings SCM_SCRIPT_GENERATOR_ARGS="--aspNetCore mybot.csproj"`|

**JS Bot:**
1. Download web.config from [here](https://github.com/projectkudu/kudu/wiki/Using-a-custom-web.config-for-Node-apps) and save it into your project folder. 
1. Edit the file and replace all occurances of "server.js" with "index.js". 
1. Save the file.

Open a command-prompt, and navigate to the project folder. Run the following commands from the command line.

| Task | Command |
|:-----|:--------|
| 1. Install node modules | `npm install` |
| 2. Zip project files | Use any utility to zip the project files. Go to the folder that has the .csproj file and select all the files and folder at this level to create the zipped folder. |
| 3. Set build deployment setting | `az webapp config appsettings set --resource-group <resource-group-name> --name <bot-name> --settings SCM_DO_BUILD_DEPLOYMENT=false`|

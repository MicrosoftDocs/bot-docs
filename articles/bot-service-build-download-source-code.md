---
redirect_url: /bot-framework/bot-builder-howto-deploy-azure
---

<!--

---
title: Download and redeploy bot source code | Microsoft Docs
description: Learn how to download and publish a Bot Service.
keywords: download source code, redeploy, deploy, zip file, publish
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 12/01/2018
---
# Download and redeploy bot code
Azure Bot Service allows you to download the entire source project for your bot so that you can work locally using an IDE of your choice. After you are done updating code, you can publish your changes back to the Azure portal. We'll show you how to download code using the Azure portal and `az` cli. We'll also cover redeploying your updated bot code using Visual Studio and `az` cli tool. You can choose the method that works best for you.

## Prerequisites
- Install the latest version of the [Azure CLI](https://docs.microsoft.com/cli/azure/?view=azure-cli-latest) tool.

# [Azure portal](#tab/azportal)
### Download code using the Azure portal
To download bot code, do the following:
1. In the [Azure portal](https://portal.azure.com), open the blade for the bot.
1. Under the **Bot management** section, Click **Build**.
1. Under **Download source code**, click **Download zip file**.
1. Wait until Azure prepares your download URI, and then click **Download zip file** in the notification.
1. Save and extract the .zip file to a local directory.

**C# bot code**

If you have a C# bot, update the `appsettings.json` file to include .bot file information as shown below:

```
{
  "botFilePath": "yourbasicBot.bot",
  "botFileSecret": "ukxxxxxxxxxxxs="
}
```

**Node.js bot code**

If you have a node.js bot, add a `.env` file with the following entries:

```
botFilePath=yourbasicBot.bot
botFileSecret=ukxxxxxxxxxxxxs=
```

The `botFilePath` references the name of your bot, simply replace "yourbasicBot.bot" with your own bot name. To obtain the `botFileSecret` key, refer to [Bot File Encryption](https://aka.ms/bot-file-encryption) article on generating a key for your bot.

Next, make changes to your sources by either editing existing source files or adding new ones to your project. Test your code using the Emulator. When you are ready to redeploy modified code to the Azure portal, follow the instructions below.

### Publish code using Visual Studio
1. In Visual Studio, right-click your project name and click **Publish...**. The **Publish** window opens.

![Azure publish](~/media/azure-bot-build/azure-csharp-publish.png)

2. Select the profile for your project.
3. Copy the password listed in the _publish.cmd_ file in your project.
4. Click **Publish**.
5. When prompted, enter the password that you copied in step 3.

After your project configured, your project changes will be published to Azure.

Next, we'll take a look at downloading and redeploying code using the `az` cli.

# [AZ CLI](#tab/azcli)
### Download code using Azure CLI

First, log in to the Azure portal using the az cli tool.

```azcli
az login
```

You will be prompted with a unique temporary auth code. To sign in, use a web browser and visit Microsoft [device login](https://microsoft.com/devicelogin), and paste the code provided by the CLI to continue.

To download code using `az` cli, use the following command:
```azcli
az bot download --name "my-bot-name" --resource-group "my-resource-group"`
```
After the code is downloaded, do the following:
- For C# bot, update the appsettings.json file to include .bot file information as shown below:

```
{
  "botFilePath": "yourbasicBot.bot",
  "botFileSecret": "ukxxxxxxxxxxxs="
}
```

- For node.js bot, add a .env file with the following entries:

```
botFilePath=yourbasicBot.bot
botFileSecret=ukxxxxxxxxxxxxs=
```

Next, make changes to your sources by either editing existing source files or adding new ones to your project. Test your code using the Emulator. When you are ready to redeploy modified code to the Azure portal, follow the instructions below.

### Login to Azure CLI by running the following command.
You can skip this step if you are already logged in.

```azcli
az login
```
You will be prompted with a unique temporary auth code. To sign in, use a web browser and visit Microsoft [device login](https://microsoft.com/devicelogin), and paste the code provided by the CLI to continue.

### Publish code using Azure CLI
To publish code back to Azure using `az` cli, use the following command:
```azcli
az bot publish --name "my-bot-name" --resource-group "my-resource-group" --code-dir <path to directory>
```

You can use the `code-dir` option to indicate which directory to use. If it is not provided, the `az bot publish` command will use the local directory to publish.

---

## Next steps
Now that you know how to upload changes back to Azure, you can setup continuous deployment for your bot.

> [!div class="nextstepaction"]
> [Set up continuous deployment](bot-service-build-continuous-deployment.md)

-->

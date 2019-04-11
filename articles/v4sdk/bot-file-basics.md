---
title: Manage resources with a .bot file | Microsoft Docs
description: Describes the purpose and use of bot file.
keywords: bot file, .bot, .bot file, bot resources, manage bot resources
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 03/30/2019
monikerRange: 'azure-bot-service-4.0'
---

# Manage bot resources

Bots usually consume different services, such as [LUIS.ai](https://luis.ai) or [QnaMaker.ai](https://qnamaker.ai). When you are developing a bot, you need to be able to keep track of them all. You can use various methods such as appsettings.json, web.config, or .env. 

> [!IMPORTANT]
> Prior to the Bot Framework SDK 4.3 release, we offered the .bot file as a mechanism to manage resources. However, going forward we recommend that you use appsettings.json or .env file for managing these resources. Bots that use .bot file will continue to work for now even though the .bot file has been **_deprecated_**. If you've been using a .bot file to manage resources, follow the steps that apply to migrate the settings. 

## Migrating settings from .bot file
The following sections cover how to migrate settings from the .bot file. Follow the scenario that applies to you.

**Scenario 1: Local bot that has a .bot file**

In this scenario you have a local bot that uses a .bot file, but the _bot has not been migrated_ to the Azure portal. Follow the steps below to migrate settings from the .bot file to appsettings.json or .env file.

- If the .bot file is encrypted, you'll need to decrypt it using the following command:

```cli
msbot secret --bot <name-of-bot-file> --secret "<bot-file-secret>" --clear` command.
```

- Open the decrypted .bot file, copy the values and add them to the appsettings.json or .env file.
- Update the code to read settings from appsettings.json or .env file.

# [C#](#tab/csharp)

In the `ConfigureServices` method, use the configuration object that ASP.NET Core provides, for example: 

**Startup.cs**
```csharp
var appId = Configuration.GetSection("MicrosoftAppId").Value;
var appPassword = Configuration.GetSection("MicrosoftAppPassword").Value;
options.CredentialProvider = new SimpleCredentialProvider(appId, appPassword);
```
# [JavaScript](#tab/js)

In JavaScript, reference .env variables off of the `process.env` object, for example:
   
**index.js**

```js
const adapter = new BotFrameworkAdapter({
    appId: process.env.MicrosoftAppId,
    appPassword: process.env.MicrosoftAppPassword
});
```
---

If needed, provision resources and connect them to your bot using the appsettings.json or .env file.

**Scenario #2: Bot deployed to Azure with a .bot file**

In this scenario you have already deployed a bot to the Azure portal using the .bot file, and now you want to migrate settings from the .bot file to appsettings.json or .env file.

- Download the bot code from the Azure portal. When you download the code, you'll be prompted to include appsettings.json or .env file that will have your MicrosoftAppId and MicrosoftAppPassword and any additional settings in it. 
- Open the _downloaded_ appsettings.json or .env file, and copy the settings from it into your _local_ appsettings.json or .env file. Don't forget to remove the botSecret and botFilePath entries from your local appsettings.json or .env file.
- Update the code to read settings from appsettings.json or .env file.

# [C#](#tab/csharp)
In the `ConfigureServices` method, use the configuration object that ASP.NET Core provides, for example: 

**Startup.cs**
```csharp
var appId = Configuration.GetSection("MicrosoftAppId").Value;
var appPassword = Configuration.GetSection("MicrosoftAppPassword").Value;
options.CredentialProvider = new SimpleCredentialProvider(appId, appPassword);
```
# [JavaScript](#tab/js)
In JavaScript, reference .env variables off of the `process.env` object, for example:
   
**index.js**

```js
const adapter = new BotFrameworkAdapter({
    appId: process.env.MicrosoftAppId,
    appPassword: process.env.MicrosoftAppPassword
});
```
---

You’ll also need to remove the `botFilePath` and `botFileSecret` from the **Application Settings** section in the **Azure portal**.

_If needed_, provision resources and connect them to your bot using the appsettings.json or .env file.

**Scenario #3: For bots that use appsettings.json or .env file**

This scenario covers the case in which you are starting to develop bots using SDK 4.3 from scratch and don't have existing .bot files to migrate. All settings that you want to use in your bot are in the appsettings.json or .env file as shown below:

```JSON
{
  "MicrosoftAppId": "<your-AppId>",
  "MicrosoftAppPassword": "<your-AppPwd>"
}
```

# [C#](#tab/csharp)

To read the above settings in your C# code, you'll use the configuration object that ASP.,NET Core provides for example:
**Startup.cs**
```csharp
var appId = Configuration.GetSection("MicrosoftAppId").Value;
var appPassword = Configuration.GetSection("MicrosoftAppPassword").Value;
options.CredentialProvider = new SimpleCredentialProvider(appId, appPassword);
```

# [JavaScript](#tab/js)
In JavaScript, reference .env variables off of the `process.env` object for example:
**index.js**
```js
const adapter = new BotFrameworkAdapter({
    appId: process.env.MicrosoftAppId,
    appPassword: process.env.MicrosoftAppPassword
});
```

---

If needed, provision resources and connect them to your bot using the appsettings.json or .env file.


## FAQ
**Q:** I want to create a new V4 bot in Azure portal. How has the Azure portal experience changed with the removal of the .bot file?

**A:** When you create a bot in the Azure portal, .bot file will not be created. You can use the **Application Settings** section in the **Azure portal** to find IDs/Keys. When you download the code, these settings will be stored in the appsettings.json or .env file for you. In your bot, you can update the code to read the settings before making the call to the individual service. After you’ve updated your bot code, you can use az bot publish to deploy your bot.

**Q:** What about V3 bots?

**A:** The scenario for V3 bot is similar to the scenario for V4 bots without a bot file. The deployment process will continue to work. 

## Additional resources
- For steps to deploy the bot, see [deployment](../bot-builder-deploy-az-cli.md) topic.
- To protect keys and secrets, we recommend you use Azure Key Vault. Azure Key Vault is a tool for securely storing and accessing secrets, such as your bot's endpoint and authoring keys. It provides a [Key Management](https://docs.microsoft.com/en-us/azure/key-vault/key-vault-whatis) solution and makes it easy to create and control your encryption keys, so you have tight control over your secrets.


<!--

# Manage resources with a .bot file

Bots usually consume lots of different services, such as [LUIS.ai](https://luis.ai) or [QnaMaker.ai](https://qnamaker.ai). When you are developing a bot, there is no uniform place to store the metadata about the services that are in use.  This prevents us from building tooling that looks at a bot as a whole.

To address this problem, we have created a **.bot file** to act as the place to bring all service references together in one place to 
enable tooling.  For example, the Bot Framework Emulator ([V4](https://aka.ms/Emulator-wiki-getting-started)) uses a  .bot file to create a unified view over the connected services your bot consumes.  

With a .bot file, you can register services like:

* **Localhost** local debugger endpoints
* [**Azure Bot Service**](https://azure.microsoft.com/en-us/services/bot-service/) Azure Bot Service registrations.
* [**LUIS.AI**](https://www.luis.ai/) LUIS gives your bot the ability to communicate with people using natural language.. 
* [**QnA Maker**](https://qnamaker.ai/) Build, train and publish a simple question and answer bot based on FAQ URLs, structured documents or editorial content in minutes.
* [**Dispatch**](https://github.com/Microsoft/botbuilder-tools/tree/master/packages/Dispatch) models for dispatching across multiple services.
* [**Azure Application Insights**](https://azure.microsoft.com/en-us/services/application-insights/) for insights and bot analytics.
* [**Azure Blob Storage**](https://azure.microsoft.com/en-us/services/storage/blobs/) for bot state persistence. 
* [**Azure Cosmos DB**](https://azure.microsoft.com/en-us/services/cosmos-db/) - globally distributed, multi-model database service to persist bot state.

Apart from these, your bot might rely on other custom services. You can leverage the [generic service](https://github.com/Microsoft/botbuilder-tools/blob/master/packages/MSBot/docs/add-services.md) capability to connect a generic service configuration.

## When is a .bot file created? 
- If you create a bot using [Azure Bot Service](https://ms.portal.azure.com/#blade/Microsoft_Azure_Marketplace/GalleryResultsListBlade/selectedSubMenuItemId/%7B%22menuItemId%22%3A%22gallery%2FCognitiveServices_MP%2FBotService%22%2C%22resourceGroupId%22%3A%22%22%2C%22resourceGroupLocation%22%3A%22%22%2C%22dontDiscardJourney%22%3Afalse%2C%22launchingContext%22%3A%7B%22source%22%3A%5B%22GalleryFeaturedMenuItemPart%22%5D%2C%22menuItemId%22%3A%22CognitiveServices_MP%22%2C%22subMenuItemId%22%3A%22BotService%22%7D%7D), a .bot file is automatically created for you with list of connected services provisioned. The .bot is encrypted by default.
- If you create a bot using Bot Framework V4 SDK [Template](https://marketplace.visualstudio.com/items?itemName=BotBuilder.botbuilderv4) for Visual Studio or using Bot Builder [Yeoman Generator](https://www.npmjs.com/package/generator-botbuilder), a .bot file is automatically created. No connected services are provisioned in this flow and the bot file is not encrypted.
- If you are starting with [BotBuilder-samples](https://github.com/Microsoft/botbuilder-samples), every sample for Bot Framework V4 SDK includes a .bot file and the .bot file is not encrypted. 
- You can also create a bot file using the [MSBot](https://github.com/Microsoft/botbuilder-tools/blob/master/packages/MSBot/README.md) tool.

## What does a bot file look like? 
Take a look at a sample [.bot](https://github.com/Microsoft/botbuilder-tools/blob/master/packages/MSBot/docs/sample-bot-file.json) file.
To learn about encrypting and decrypting the .bot file, see [Bot Secrets](https://github.com/Microsoft/botbuilder-tools/blob/master/packages/MSBot/docs/bot-file-encryption.md).

## Why do I need a .bot file?

A .bot file is **not** a requirement to build bots with Bot Framework SDK. You can continue to use appsettings.json, web.config, env, 
keyvault or any mechanism you see fit to keep track of service references and keys that your bot depends on. However, to test
the bot using the Emulator, you'll need a .bot file. The good news is that Emulator can create a .bot file for testing. To do that, 
start the Emulator, click on the **create a new bot configuration** link on the Welcome page. In the dialog box that appears, type a **Bot name** and an **Endpoint URL**. Then connect.

The advantages of using .bot file are:
- Provides a standard way of storing resources regardless of the language/platform you use.   
- Bot Framework Emulator and CLI tools rely on and work great with tracking connected services in a consistent format (in a .bot file) 
- Elegant tooling solutions around services creation and management is harder without a well defined schema (.bot file).  


## Using .bot file in your Bot Framework SDK bot

You can use the .bot file to get service configuration information in your bot's code. The BotFramework-Configuration library available 
for [C#](https://www.nuget.org/packages/Microsoft.Bot.Configuration) and [JS](https://www.npmjs.com/package/botframework-config) helps you load a bot file and supports several methods to query and get the appropriate service configuration information.

## Additional resources
Refer to [MSBot](https://github.com/Microsoft/botbuilder-tools/blob/master/packages/MSBot/README.md) readme file for more information on using a bot file.

-->


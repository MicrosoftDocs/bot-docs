---
title: Manage bot resources | Microsoft Docs
description: Describes the purpose and use of bot file.
keywords: bot file, .bot, .bot file, msbot, bot resources, manage bot resources
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 05/23/2019
monikerRange: 'azure-bot-service-4.0'
---

# Manage bot resources

[!INCLUDE [applies-to-v4](../includes/applies-to.md)]

Bots usually consume different services, such as [LUIS.ai](https://luis.ai) or [QnaMaker.ai](https://qnamaker.ai). When you are developing a bot, you need to be able to keep track of them all. You can use various methods such as appsettings.json, web.config, or .env. 

> [!IMPORTANT]
> Prior to the Bot Framework SDK 4.3 release, we offered the .bot file as a mechanism to manage resources. However, going forward we recommend that you use appsettings.json or .env file for managing these resources. Bots that use .bot file will continue to work for now even though the .bot file has been **_deprecated_**. If you've been using a .bot file to manage resources, follow the steps that apply to migrate the settings. 

## Migrating settings from .bot file
The following sections cover how to migrate settings from the .bot file. Follow the scenario that applies to you.

**Scenario #1: Local bot that has a .bot file**

In this scenario you have a local bot that uses a .bot file, but the _bot has not been migrated_ to the Azure portal. Follow the steps below to migrate settings from the .bot file to appsettings.json or .env file.

- If the .bot file is encrypted, you'll need to decrypt it using the following command:

```cli
msbot secret --bot <name-of-bot-file> --secret "<bot-file-secret>" --clear
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

*If needed*, provision resources and connect them to your bot using the appsettings.json or .env file.

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

*If needed*, provision resources and connect them to your bot using the appsettings.json or .env file.

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

## Additional resources
- For steps to deploy the bot, see [deployment](../bot-builder-deploy-az-cli.md) topic.
- Learn how to use [Azure Key Vault](https://docs.microsoft.com/en-us/azure/key-vault/key-vault-overview) to safeguard and manage cryptographic keys and secrets used by cloud applications and services.

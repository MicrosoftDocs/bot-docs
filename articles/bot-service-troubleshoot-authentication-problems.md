---
title: Troubleshooting Bot Framework Authentication - Bot Service
description: Learn how to troubleshoot bot authentication errors, such as connectivity issues and problems with app IDs and passwords. View additional resources.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: azure-ai-bot-service
ms.topic: troubleshooting
ms.date: 10/19/2021
ms.custom:
  - evergreen
---

# Troubleshooting Bot Framework authentication

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

<!-- Attention writers!
     1 - When you create a new FAQ, please add the related link to the proper section in bot-service-troubleshoot-index.md.-->

This guide can help you to troubleshoot authentication issues with your bot by evaluating a series of scenarios to determine where the problem exists.

> [!NOTE]
> To complete all steps in this guide, you'll need to download and use the [Bot Framework Emulator][Emulator] and must have access to the bot's registration settings in the [Azure portal](https://portal.azure.com).

## App ID and password

Bot security is configured by the **Microsoft App ID** and **Microsoft App Password** that you obtain when you register your bot with the Bot Framework. These values are typically specified within the bot's configuration file and used to retrieve access tokens from the Microsoft Account service.

If you haven't yet done so, [deploy your bot to Azure](bot-builder-howto-deploy-azure.md) to obtain a **Microsoft App ID** and **Microsoft App Password** that it can use for authentication.

> [!NOTE]
> To find your bot's **AppID** and **AppPassword** for an already deployed bot, see [MicrosoftAppID and MicrosoftAppPassword](bot-service-manage-overview.md#bot-identity-information).

## Step 1: Disable security and test on localhost

In this step, you'll verify that your bot is accessible and functional on localhost when security is disabled.

> [!WARNING]
> Disabling security for your bot may allow unknown attackers to impersonate users. Only implement the following procedure if you're operating in a protected debugging environment.

<a id="disable-security-localhost"></a>

### Disable security

To disable security for your bot, edit its configuration settings to remove the values for app ID and password.

### [C#](#tab/csharp)

If you're using the Bot Framework SDK for .NET, add or edit the settings in your **appsettings.json** file:

```json
"MicrosoftAppId": "",
"MicrosoftAppPassword": ""
```

### [JavaScript](#tab/javascript)

If you're using the Bot Framework SDK for Node.js, add or edit the following values in your **.env** file:

```javascript
const adapter = new BotFrameworkAdapter({
    appId: null,
    appPassword: null
});
```

### [Python](#tab/python)

If you're using the Bot Framework SDK for Python, add or edit the following values in your **config.py** file:

```python
class DefaultConfig:
    """ Bot Configuration """
    APP_ID = None
    APP_PASSWORD = None
```

---

### Test your bot on localhost

Next, test your bot on localhost by using the Bot Framework Emulator.

1. Start your bot on localhost.
1. Start the Bot Framework Emulator.
1. Connect to your bot using the Emulator.
    - Type `http://localhost:port-number/api/messages` into the Emulator's address bar, where **port-number** matches the port number shown in the browser where your application is running.
    - Ensure that the **Microsoft App ID** and **Microsoft App Password** fields are both empty.
    - Click **Connect**.
1. To test connectivity to your bot, type some text into the Emulator and press Enter.

If the bot responds to the input and there are no errors in the chat window, you've verified that your bot is accessible and functional on localhost when security is disabled. Proceed to [Step 2](#step-2).

If one or more error(s) are indicated in the chat window, click the error(s) for details. Common issues include:

- The Emulator settings specify an incorrect endpoint for the bot. Make sure you've included the proper port number in the URL and the proper path at the end of the URL, such as `/api/messages`.
- The Emulator settings specify a bot endpoint that begins with `https`. On localhost, the endpoint should begin with `http`.
- The Emulator settings specify a value for the **Microsoft App ID** field and/or the **Microsoft App Password** field. Both fields should be empty.
- Security hasn't been disabled of for the bot. [Verify](#disable-security-localhost) that the bot doesn't specify a value for either app ID or password.

<a id="step-2"></a>

## Step 2: Verify your bot's app ID and password

In this step, you'll verify that the app ID and password that your bot will use for authentication are valid. (If you don't know these values, [obtain them](#app-id-and-password) now.)

> [!WARNING]
> The following instructions disable SSL verification for `login.microsoftonline.com`. Only perform this procedure on a secure network and consider changing your application's password afterward.

### Issue an HTTP request to the Microsoft login service

These instructions describe how to use [cURL](https://curl.haxx.se/download.html) to issue an HTTP request to the Microsoft login service. You may use an alternative tool such as Postman, just ensure that the request conforms to the Bot Framework [authentication protocol](rest-api/bot-framework-rest-connector-authentication.md).

To verify that your bot's app ID and password are valid, issue the following request using **cURL**, replacing `APP_ID` and `APP_PASSWORD` with your bot's app ID and password.

> [!TIP]
> Your password may contain special characters that make the following call invalid. If so, try converting your password to URL encoding.

```cmd
curl -k -X POST https://login.microsoftonline.com/botframework.com/oauth2/v2.0/token -d "grant_type=client_credentials&client_id=APP_ID&client_secret=APP_PASSWORD&scope=https%3A%2F%2Fapi.botframework.com%2F.default"
```

This request attempts to exchange your bot's app ID and password for an access token. If the request is successful, you'll receive a JSON payload that contains an `access_token` property, amongst others.

```json
{"token_type":"Bearer","expires_in":3599,"ext_expires_in":0,"access_token":"eyJ0eXAJKV1Q..."}
```

If the request is successful, you've verified that the app ID and password that you specified in the request are valid. Proceed to [Step 3](#step-3).

If you receive an error in response to the request, examine the response to identify the cause of the error. If the response indicates that the app ID or the password is invalid, [obtain the correct values](#app-id-and-password) from the Bot Framework Portal and reissue the request with the new values to confirm that they're valid.

<a id="step-3"></a>

## Step 3: Enable security and test on localhost

At this point, you've verified that your bot is accessible and functional on localhost when security is disabled and confirmed that the app ID and password that the bot will use for authentication are valid. In this step, you'll verify that your bot is accessible and functional on localhost when security is enabled.

<a id="enable-security-localhost"></a>

### Enable security

Your bot's security relies on Microsoft services, even when your bot is running only on localhost. To enable security for your bot, edit its configuration settings to populate app ID and password with the values that you verified in [Step 2](#step-2).  Additionally, make sure your packages are up to date, specifically `System.IdentityModel.Tokens.Jwt` and `Microsoft.IdentityModel.Tokens`.

If you're using the Bot Framework SDK for .NET, populate these settings in your `appsettings.config` or the corresponding values in your `appsettings.json` file:

```xml
<appSettings>
  <add key="MicrosoftAppId" value="APP_ID" />
  <add key="MicrosoftAppPassword" value="PASSWORD" />
</appSettings>
```

If you're using the Bot Framework SDK for Node.js, populate these settings (or update the corresponding environment variables):

```javascript
var connector = new builder.ChatConnector({
  appId: 'APP_ID',
  appPassword: 'PASSWORD'
});
```

> [!NOTE]
> To find your bot's **AppID** and **AppPassword**, see [MicrosoftAppID and MicrosoftAppPassword](bot-service-manage-overview.md#bot-identity-information).

### Test your bot on localhost

Next, test your bot on localhost by using the Bot Framework Emulator.

1. Start your bot on localhost.
1. Start the Bot Framework Emulator.
1. Connect to your bot using the Emulator.
    - Type `http://localhost:port-number/api/messages` into the Emulator's address bar, where **port-number** matches the port number shown in the browser where your application is running.
    - Enter your bot's app ID into the **Microsoft App ID** field.
    - Enter your bot's password into the **Microsoft App Password** field.
    - Click **Connect**.
1. To test connectivity to your bot, type some text into the Emulator and press Enter.

If the bot responds to the input and there are no errors in the chat window, you've verified that your bot is accessible and functional on localhost when security is enabled.  Proceed to [Step 4](#step-4-test-your-bot-in-the-cloud).

If one or more error(s) are indicated in the chat window, click the error(s) for details. Common issues include:

- The Emulator settings specify an incorrect endpoint for the bot. Make sure you've included the proper port number in the URL and the proper path at the end of the URL, such as `/api/messages`.
- The Emulator settings specify a bot endpoint that begins with `https`. On localhost, the endpoint should begin with `http`.
- In the Emulator settings, the **Microsoft App ID** field and/or the **Microsoft App Password** don't contain valid values. Both fields should be populated and each field should contain the corresponding value that you verified in [Step 2](#step-2).
- Security hasn't been enabled for the bot. [Verify](#enable-security-localhost) that the bot configuration settings specify values for both app ID and password.

## Step 4: Test your bot in the cloud

At this point, you've verified that your bot is accessible and functional on localhost when security is disabled, confirmed that your bot's app ID and password are valid, and verified that your bot is accessible and functional on localhost when security is enabled. In this step, you'll deploy your bot to the cloud and verify that it's accessible and functional there with security enabled.

### Deploy your bot to the cloud

The Bot Framework requires that bots be accessible from the internet, so you must deploy your bot to a cloud hosting platform such as Azure. Be sure to enable security for your bot prior to deployment, as described in [Step 3](#step-3).

> [!NOTE]
> If you don't already have a cloud hosting provider, you can register for a [free account](https://azure.microsoft.com/free/).

If you deploy your bot to Azure, SSL will automatically be configured for your application, thereby enabling the **HTTPS** endpoint that the Bot Framework requires. If you deploy to another cloud hosting provider, be sure to verify that your application is configured for SSL so that the bot will have an **HTTPS** endpoint.

### Test your bot

To test your bot in the cloud with security enabled, complete the following steps.

1. Ensure that your bot has been successfully deployed and is running.
1. Sign in to the [Azure portal](https://portal.azure.com).
1. Navigate to the **Azure Bot** resource for your bot within the portal.
1. Click **Test in Web Chat** in the **Bot management** pane on the left.
1. To test connectivity to your bot, type some text into the web chat control and press Enter.

If an error is indicated in the chat window, use the error message to determine the cause of the error. Common issues include:

- The **Messaging endpoint** specified on the **Settings** page for your bot in the Bot Framework Portal is incorrect. Make sure you've included the proper path at the end of the URL, such as `/api/messages`.
- The **Messaging endpoint** specified on the **Settings** page for your bot in the Bot Framework Portal doesn't begin with `https` or isn't trusted by the Bot Framework. Your bot must have a valid, chain-trusted certificate.
- The bot is configured with missing or incorrect values for app ID or password. [Verify](#enable-security-localhost) that the bot configuration settings specify valid values for app ID and password.

If the bot responds appropriately to the input, you've verified that your bot is accessible and functional in the cloud with security enabled. At this point, your bot is ready to securely [connect to a channel](bot-service-manage-channels.md) such as Facebook Messenger, Direct Line, and others.

## Additional resources

If you're still experiencing issues after completing the steps above, you can:

- Review how-to [debug a bot](bot-service-debug-bot.md) and the other debugging articles in that section.
- [Debug your bot in the cloud](bot-service-debug-emulator.md) using the Bot Framework Emulator and [Dev Tunnels](https://aka.ms/devtunnels tunneling software. 
- Use a proxying tool like [Fiddler](https://www.telerik.com/fiddler) to inspect HTTPS traffic to and from your bot. *Fiddler isn't a Microsoft product.*
- Review the [Bot Connector authentication guide][BotConnectorAuthGuide] to learn about the authentication technologies that the Bot Framework uses.
- Solicit help from others by using the Bot Framework [support][Support] resources.

[BotConnectorAuthGuide]: ./rest-api/bot-framework-rest-connector-authentication.md
[Support]: bot-service-resources-links-help.md
[Emulator]: bot-service-debug-emulator.md

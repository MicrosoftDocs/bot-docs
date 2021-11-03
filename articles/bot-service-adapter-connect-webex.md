---
title: Connect a bot to Webex Teams | Microsoft Docs
description: Learn how to configure a bot's connection to Webex via the Webex adapter.
keywords: bot adapter, Webex, Webex bot
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 11/01/2021
---

# Connect a bot to Webex Teams using the Webex adapter

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

In this article you will learn how to connect a bot to Webex using the adapter available in the SDK.  This article will walk you through modifying the EchoBot sample to connect it to a Webex app.

> [!NOTE]
> The instructions below cover the C# implementation of the Webex adapter. For instructions on using the JavaScript implementation, part of the BotKit libraries, [see the BotKit Webex documentation](https://botkit.ai/docs/v4/platforms/webex.html).

## Prerequisites

* The [EchoBot sample code](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/02.echo-bot)

* Access to a Webex team with sufficient permissions to login to create / manage applications at  [https://developer.webex.com/my-apps](https://developer.webex.com/my-apps). If you do not have access to a Webex team  you can create an account for free at [www.webex.com](https://www.webex.com).

## Create a Webex bot app

1. Log into the [Webex developer dashboard](https://developer.webex.com/my-apps) and then click the 'Create a new app' button.

1. On the next screen choose to create a Webex Bot by clicking 'Create a bot'.

1. On the next screen, enter an appropriate name, username and description for your bot, as well as choosing an icon or uploading an image of your own.

    ![bot setting](media/bot-service-adapter-connect-webex/create-bot.png)

    Click the 'Add bot' button.

1. On the next page you will be provided with an access token for your new Webex app, please make a note of this token as you will require it when configuring your bot.

    ![access token](media/bot-service-adapter-connect-webex/create-bot-settings.png)

## Wiring up the Webex adapter in your bot

Before you can complete the configuration of our Webex app, you need to wire up the Webex adapter into your bot.

### Install the Webex adapter NuGet package

Add  the [Microsoft.Bot.Builder.Adapters.Webex](https://www.nuget.org/packages/Microsoft.Bot.Builder.Adapters.Webex/) NuGet package. For more information on using NuGet, see [Install and manage packages in Visual Studio](/nuget/tools/package-manager-ui)

### Create a Webex adapter class

Create a new class that inherits from the ***WebexAdapter*** class. This class will act as our adapter for the Webex channel. It includes error handling capabilities (much like the ***BotFrameworkAdapterWithErrorHandler*** class already in the sample, used for handling requests from Azure Bot Service).

[!code-csharp[Webex Adapter With Error Handler](../botbuilder-samples/samples/csharp_dotnetcore/62.webex-adapter/Adapters/WebexAdapterWithErrorHandler.cs?range=11-29)]

### Create a new controller for handling Webex requests

We create a new controller which will handle requests from your Webex app, on a new endpoint 'api/webex' instead of the default 'api/messages' used for requests from Azure Bot Service Channels.  By adding an additional endpoint to your bot, you can accept requests from Bot Service channels (or additional adapters), as well as from Webex, using the same bot.

[!code-csharp[Webex Controller](../botbuilder-samples/samples/csharp_dotnetcore/62.webex-adapter/Controllers/WebexController.cs?range=12-32)]

### Inject Webex Adapter In Your Bot Startup.cs

Add the following line into the ***ConfigureServices*** method within your Startup.cs file, which will register your Webex adapter and make it available for your new controller class.  The configuration settings, described in the next step, will be automatically used by the adapter.

```csharp
services.AddSingleton<WebexAdapter, WebexAdapterWithErrorHandler>();
```

Once added, your ***ConfigureServices*** method should look like this.

[!code-csharp[Webex Controller](../botbuilder-samples/samples/csharp_dotnetcore/62.webex-adapter/Startup.cs?range=18-31)]

### Add Webex adapter settings to your bot's configuration file

1. Add the 4 settings shown below to your appSettings.json file in your bot project.

[!code-csharp[Webex Controller](../botbuilder-samples/samples/csharp_dotnetcore/62.webex-adapter/appsettings.json?range=1-6)]

1. Populate the **WebexAccessToken** setting within the Webex Bot Access Token, that was generated when creating your Webex bot app in the earlier steps. Leave the other 3 settings empty  at this time, until we gather the information needed for them in later steps.

## Complete configuration of your Webex app and bot

### Create and update a Webex webhook

Now that you have created a Webex app and wired up the adapter in your bot project, the final steps are to configure a Webex webhook, point it to the correct endpoint on your bot, and subscribe your app to ensure your bot receives messages and attachments. To do this your bot must be running, so that Webex can verify the URL to the endpoint is valid.

1. To complete this step, [deploy your bot to Azure](./bot-builder-deploy-az-cli.md) and make a note of the URL to your deployed bot. Your Webex messaging endpoint is the URL for your bot, which will be the URL of your deployed application (or ngrok endpoint), plus '/api/webex' (for example, `https://yourbotapp.azurewebsites.net/api/webex`).

    > [!NOTE]
    > If you are not ready to deploy your bot to Azure, or wish to debug your bot when using the Webex adapter, you can use a tool such as [ngrok](https://www.ngrok.com) (which you will likely already have installed if you have used the Bot Framework Emulator previously) to tunnel through to your bot running locally and provide you with a publicly accessible URL for this.
    >
    > If you wish create an ngrok tunnel and obtain a URL to your bot, use the following command in a terminal window (this assumes your local bot is running on port 3978, alter the port numbers in the command if your bot is not).
    >
    > ```cmd
    > ngrok.exe http 3978 -host-header="localhost:3978"
    > ```

1. Navigate to [https://developer.webex.com/docs/api/v1/webhooks](https://developer.webex.com/docs/api/v1/webhooks).

1. Click the link for the **POST** method `https://webexapis.com/v1/webhooks` (with the description *Create a webhook*). This will display a form that allows you to send a request to the endpoint.

    ![webhook endpoint](media/bot-service-adapter-connect-webex/webex-webhook-post-endpoint.png)

1. Populate the form with the following details:

    1. **Name** - The name for your webhook, for example *Messages Webhook*.
    1. **TargetUrl** - The full URL to your bot's Webex endpoint, such as `https://yourbotapp.azurewebsites.net/api/webex`).
    1. **resource** - Messages.
    1. **event** - Created.
    1. **filter** - Leave it blank.
    1. **secret** - A secret of your choice to secure your webhook. Later you will add it to your bot's `appsettings.json`.

    ![webhook form](media/bot-service-adapter-connect-webex/webex-webhook-form.png)

1. Click **Run**, which should create your webhook and provide you with a success message.

### Complete the remaining settings in your bot application

Complete the remaining 3 settings in your bot's appsettings.json file (you already populated **WebexAccessToken** in an earlier step).

* WebexPublicAddress (the full URL to your bot's Webex endpoint)
* WebexSecret (the secret you provided when creating your webhook in the previous step)
* WebexWebhookName (the name for your webhook you provided in the previous step)

## Re-deploy your bot in your Webex team

Now that you have completed the configuration of your bot's settings in appsettings.json, you should re-deploy your bot (or restart your bot if you are tunneling to a local endpoint using ngrok).  Configuration of you Webex app and bot are now complete.
You can now login to your Webex team at [https://www.webex.com](https://www.webex.com) and chat with your bot by sending it a message, in the same way you would contact another person.

![contact person](media/bot-service-adapter-connect-webex/webex-contact-person.png)

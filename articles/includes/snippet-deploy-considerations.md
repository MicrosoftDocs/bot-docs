## Configuration settings and endpoint

Regardless of which cloud service you choose to host your bot, you must address the application configuration settings and the HTTPS endpoint as part of the deployment process.

> [!NOTE]
> If you created a bot with the Azure Bot Service, your bot deployment was part of the Azure Bot Service bot 
> creation process.

###Application configuration settings
If you've already [registered](~/portal-register-bot.md) your bot with the Bot Framework,
update the Microsoft App Id and Microsoft App Password values in your application's configuration settings
as part of the deployment process.
Specify the **app ID** and **password** values that were generated for your bot during registration.

> [!TIP]
[!include[Application configuration settings](~/includes/snippet-tip-bot-config-settings.md)]

If you have not yet registered your bot with the Bot Framework (and therefore do not yet have an **app ID** and **password**),
you can deploy your bot with temporary placeholder values for these settings.
Then later, after you register your bot, update your deployed application's settings with the **app ID** and **password** values that were generated for your bot during registration.

###<a id="httpsEndpoint"></a> HTTPS endpoint
Your deployed bot must have an **HTTPS** endpoint that can receive messages from the Bot Framework Connector Service.

> [!NOTE]
> When you deploy your bot to Azure, SSL will automatically be configured for your application, thereby enabling the **HTTPS** endpoint that the Bot Framework requires.
> If you deploy to another cloud service, be sure to verify that your application is configured for SSL so that the bot will have an **HTTPS** endpoint.

This article provides detailed walkthroughs of the different options for deploying your bot to Azure.

> [!NOTE]
> You must have a Microsoft Azure subscription before you can deploy your bot to Microsoft Azure.
If you do not already have a subscription, you can register for a [free trial](https://azure.microsoft.com/en-us/free/).
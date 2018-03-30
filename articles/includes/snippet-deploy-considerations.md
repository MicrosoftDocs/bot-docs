## Application settings and Messaging endpoint

### Verify application settings

For your bot to function properly in the cloud, you must ensure that its application settings are correct. 
If you have an **appID** and **password**,
update the `Microsoft AppId` and `Microsoft App Password` values in your application's configuration settings as part of the deployment process. To find your bot's **AppID** and **AppPassword**, see [MicrosoftAppID and MicrosoftAppPassword](~/bot-service-manage-overview.md#microsoftappid-and-microsoftapppassword).

> [!TIP]
> [!INCLUDE [Application configuration settings](~/includes/snippet-tip-bot-config-settings.md)]

If you have not yet registered your bot with the Bot Framework (and therefore do not yet have an **appID** and **password**),
you can deploy your bot with temporary placeholder values for these settings.
Then later, after you [register your bot](~/bot-service-quickstart-registration.md), update your deployed application's settings with the **appID** and **password** values that were generated for your bot during registration.

### <a id="messagingEndpoint"></a> Verify Messaging endpoint

Your deployed bot must have an **Messaging endpoint** that can receive messages from the Bot Framework Connector Service.

> [!NOTE]
> When you deploy your bot to Azure, SSL will automatically be configured for your application, thereby enabling the **Messaging endpoint** that the Bot Framework requires.
> If you deploy to another cloud service, be sure to verify that your application is configured for SSL so that the bot will have a **Messaging endpoint**.

When you create a bot using the [Visual Studio template](https://docs.microsoft.com/azure/bot-service/dotnet/bot-builder-dotnet-sdk-quickstart?view=azure-bot-service-4.0), [Yeoman template](https://docs.microsoft.com/azure/bot-service/javascript/bot-builder-javascript-quickstart?view=azure-bot-service-4.0), or [Cookiecutter template](https://docs.microsoft.com/azure/bot-service/python/bot-builder-python-quickstart?view=azure-bot-service-4.0) the source code generated includes a `deploymentTemplates` folder that contains ARM templates. The deployment process documented here uses one of the ARM templates to provision required resources for the bot in Azure by using the Azure CLI.

> [!NOTE]
> With the release of Bot Framework SDK 4.3, we have _deprecated_ the use of a .bot file. Instead, we use an appsettings.json or .env file to manage bot resources. For information on migrating settings from the .bot file to appsettings.json or .env file, see [managing bot resources](https://docs.microsoft.com/azure/bot-service/bot-file-basics?view=azure-bot-service-4.0).

### Bot ready to deploy

This article assumes that you have a bot ready to be deployed and the **path** to the related project. You need the path to access the deployment templates and also to create a *zip* file to deploy.

For information on how to create a bot, see [.NET bot](~/bot-builder-dotnet-sdk-quickstart.md).

You can also use one of the examples provided in the [Bot Framework Samples](https://github.com/Microsoft/BotBuilder-Samples/blob/master/README.md) repository.
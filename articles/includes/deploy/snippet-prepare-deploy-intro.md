When you create a bot using a [Visual Studio template](https://docs.microsoft.com/azure/bot-service/dotnet/bot-builder-dotnet-sdk-quickstart?view=azure-bot-service-4.0), [Yeoman template](https://docs.microsoft.com/azure/bot-service/javascript/bot-builder-javascript-quickstart?view=azure-bot-service-4.0), or [Cookiecutter template](https://docs.microsoft.com/azure/bot-service/python/bot-builder-python-quickstart?view=azure-bot-service-4.0) the source code generated includes a `deploymentTemplates` folder that contains ARM templates. The deployment process documented here uses one of the ARM templates to provision required resources for the bot in Azure by using the Azure CLI.

> [!NOTE]
> With the release of Bot Framework SDK 4.3, we have _deprecated_ the use of a .bot file. Instead, we use an appsettings.json or .env file to manage bot resources. For information on migrating settings from the .bot file to appsettings.json or .env file, see [managing bot resources](https://docs.microsoft.com/azure/bot-service/bot-file-basics?view=azure-bot-service-4.0).\
> Both .NET Core 2.1 and .NET Core 3.1 versions of the C# templates are available.
> When creating new bots in Visual Studio 2019, you should use the .NET Core 3.1 templates.\
> The current bot samples use .NET Core 3.1 templates. You can find the samples that use .NET Core 2.1 templates in the [4.7-archive](https://github.com/microsoft/BotBuilder-Samples/tree/4.7-archive/samples/csharp_dotnetcore) branch of the BotBuilder-Samples repository.
> For information about deploying .NET Core 3.1 bots to Azure, see [Deploy your bot](../bot-builder-deploy-az-cli.md).

### Bot ready to deploy

This article assumes that you have a bot ready to be deployed. If you are deploying a C# bot make sure that it has been [built in Release mode](https://aka.ms/visualstudio-set-debug-release-configurations).

For information on how to create a simple echo bot, see the quick start [C# sample](~/dotnet/bot-builder-dotnet-sdk-quickstart.md), [JavaScript sample](~/javascript/bot-builder-javascript-quickstart.md) or [Python sample](~/python/bot-builder-python-quickstart.md). You can also use one of the samples provided in the [Bot Framework Samples](https://github.com/Microsoft/BotBuilder-Samples/blob/master/README.md) repository.

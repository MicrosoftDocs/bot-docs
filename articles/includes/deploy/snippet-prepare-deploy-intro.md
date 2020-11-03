
# [C#](#tab/csharp)

If you are deploying a C# bot make sure that it has been [built in Release mode](https://aka.ms/visualstudio-set-debug-release-configurations). In Visual Studio, make sure that the solution configuration is set to **Release** and perform a clean rebuild of the solution before continuing.

The deployment may fail if the solution configuration at build is set to **Debug**.
When you create a bot using a [Visual Studio template](https://docs.microsoft.com/azure/bot-service/dotnet/bot-builder-dotnet-sdk-quickstart?view=azure-bot-service-4.0), the source code generated includes a `deploymentTemplates` folder that contains ARM templates. The deployment process documented here uses one of the ARM templates to provision required resources for the bot in Azure by using the Azure CLI.

With the release of Bot Framework SDK 4.3, we have _deprecated_ the use of a .bot file. Instead, we use the `appsettings.json` configuration file to manage bot resources. For information on migrating settings from the .bot file to a configuration file, see [managing bot resources](https://docs.microsoft.com/azure/bot-service/bot-file-basics?view=azure-bot-service-4.0).

> [!NOTE]
> Both .NET Core 2.1 and .NET Core 3.1 versions of the C# templates are available.
> When creating new bots in Visual Studio 2019, you should use the .NET Core 3.1 templates.\
> The current bot samples use .NET Core 3.1 templates. You can find the samples that use .NET Core 2.1 templates in the [4.7-archive](https://github.com/microsoft/BotBuilder-Samples/tree/4.7-archive/samples/csharp_dotnetcore) branch of the BotBuilder-Samples repository.
> For information about deploying .NET Core 3.1 bots to Azure, see [Deploy your bot](~/bot-builder-deploy-az-cli.md).

# [JavaScript](#tab/javascript)

When you create a bot using a [Yeoman template](https://docs.microsoft.com/azure/bot-service/javascript/bot-builder-javascript-quickstart?view=azure-bot-service-4.0), the source code generated includes a `deploymentTemplates` folder that contains ARM templates. The deployment process documented here uses one of the ARM templates to provision required resources for the bot in Azure by using the Azure CLI.
With the release of Bot Framework SDK 4.3, we have _deprecated_ the use of a .bot file. Instead, we use the `appsettings.json` configuration file to manage bot resources. For information on migrating settings from the .bot file to a configuration file, see [managing bot resources](https://docs.microsoft.com/azure/bot-service/bot-file-basics?view=azure-bot-service-4.0).

# [Python](#tab/Python)

When you create a bot using a [Cookiecutter template](https://docs.microsoft.com/azure/bot-service/python/bot-builder-python-quickstart?view=azure-bot-service-4.0) the source code generated includes a `deploymentTemplates` folder that contains ARM templates. The deployment process documented here uses one of the ARM templates to provision required resources for the bot in Azure by using the Azure CLI.

With the release of Bot Framework SDK 4.3, we have _deprecated_ the use of a .bot file. Instead, we use the `.env` file, to manage bot resources. For information on migrating settings from the .bot file to a configuration file, see [managing bot resources](https://docs.microsoft.com/azure/bot-service/bot-file-basics?view=azure-bot-service-4.0).

---
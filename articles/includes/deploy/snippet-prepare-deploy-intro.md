<!--
 Add this include file under the "Prepare for deployment" header in the file "bot-builder-tutorial-deploy-basic-bot.md".
-->

# [C#](#tab/csharp)

If you are deploying a C#, bot make sure that it has been [built in Release mode](/visualstudio/debugger/how-to-set-debug-and-release-configurations). In Visual Studio, make sure that the solution configuration is set to **Release** and perform a clean rebuild of the solution before continuing. The deployment may fail if the solution configuration is set to **Debug**.

When you [Create a bot](../../bot-service-quickstart-create-bot.md), the source code generated includes a `DeploymentTemplates` folder that contains ARM templates. The deployment process documented here uses one of the ARM templates to provision required resources for the bot in Azure by using the Azure CLI.

With the release of Bot Framework SDK 4.3, we have _deprecated_ the use of a .bot file. Instead, we use the `appsettings.json` configuration file to manage bot resources. For information on migrating settings from the .bot file to a configuration file, see [managing bot resources](../../v4sdk/bot-file-basics.md).

[!INCLUDE [about .NET Core versions in the templates](../vsix-templates-versions.md)]

# [JavaScript / TypeScript](#tab/javascript+typescript)

When you create a bot using a [Yeoman template](../../javascript/bot-builder-javascript-quickstart.md), the source code generated includes a `deploymentTemplates` folder that contains ARM templates. The deployment process documented here uses one of the ARM templates to provision required resources for the bot in Azure by using the Azure CLI.

With the release of Bot Framework SDK 4.3, we have _deprecated_ the use of a .bot file. Instead, we use the `appsettings.json` configuration file to manage bot resources. For information on migrating settings from the .bot file to a configuration file, see [managing bot resources](../../v4sdk/bot-file-basics.md).

# [Java](#tab/java)

When you create a bot using a [Yeoman template](../../java/bot-builder-java-quickstart.md), the source code generated includes a `deploymentTemplates` folder that contains ARM templates. The deployment process documented here uses one of the ARM templates to provision required resources for the bot in Azure by using the Azure CLI.

# [Python](#tab/python)

When you create a bot using a [Cookiecutter template](../../python/bot-builder-python-quickstart.md), the source code generated includes a `deploymentTemplates` folder that contains ARM templates. The deployment process documented here uses one of the ARM templates to provision required resources for the bot in Azure by using the Azure CLI.

With the release of Bot Framework SDK 4.3, we have _deprecated_ the use of a .bot file. Instead, we use the `.env` file, to manage bot resources. For information on migrating settings from the .bot file to a configuration file, see [managing bot resources](../../v4sdk/bot-file-basics.md).

---

If you are using services such as LUIS, you will also need to pass `luisAuthoringKey`. If you want to use existing resource group in Azure, use the `groupName` argument with the above command.

It is highly recommended that you use the `verbose` option to help troubleshoot problems that might occur during the deployment of the bot. Additional options used with the `msbot clone services` command are described below:

| Arguments    | Description |
|--------------|-------------|
| `folder`     | Location of the `bot.recipe`  file. By default the recipe file is created in the `DeploymentsScript/MSBotClone`. DO NOT MODIFY this file.|
| `location`   | Geographic location used to create the bot service resources. For example, eastus, westus, westus2 etc.|
| `proj-file`  | For C# bot it is the .csproj file. For JS bot it is the startup project file name (e.g. index.js) of your local bot.|
| `name`       | A unique name that is used to deploy the bot in Azure. It could be the same name as your local bot. DO NOT include spaces or underscores in the name.|
| `luisAuthoringKey` | Your authoring key for the appropriate LUIS authoring region for the LUIS resources. |

Before Azure resources can be created, you'll be prompted to complete authentication. Follow the instructions that appear on the screen to complete this step.

Note that the above step takes _few seconds to minutes_ to complete, and the resource that are created in Azure have their names mangled. To learn more about name mangling, see [issue# 796](https://github.com/Microsoft/botbuilder-tools/issues/796) in the GitHub repo.

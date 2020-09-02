---
title: Update LUIS resources using the Bot Framework LUIS CLI commands - Bot Service
description: Describing how to automate the process of updating an existing LUIS application using the Bot Framework SDK LUIS CLI commands
keywords: LUIS, bot, inputs, adaptive dialogs, LUIS applications, LUIS Models, 
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: how-to
ms.service: bot-service
ms.date: 08/31/2020
monikerRange: 'azure-bot-service-4.0'
---

# Update LUIS resources using the Bot Framework LUIS CLI commands

[!INCLUDE [applies-to-v4](../includes/applies-to.md)]

The Bot Framework Command Line Interface (BF CLI) lets you automate the management of LUIS resources. From a command line or a script, you can create, update, and delete LUIS properties.

This article explains how to update an existing LUIS resource. For information on getting started, and how to deploy your LUIS resources using BF CLI, see how to [deploy LUIS resources using the Bot Framework LUIS CLI commands][how-to-deploy-using-luis-cli].

## Prerequisites

- Knowledge of [LU templates][lu-templates].
- Have a bot project with `.lu` files.
- If working with adaptive dialogs, you should have an understanding of:
    - [Natural language processing in adaptive dialogs][natural-language-processing-in-adaptive-dialogs].
    - [Language understanding in adaptive dialogs][language-understanding].
    - how the [LUIS recognizer][luis-recognizer] is used.

## Using the LUIS CLI commands to update the LUIS resources used in your bot

This article describes the following steps used to update your existing LUIS authoring resources in Azure using the Bot Framework CLI.

1. [Install the Bot Framework SDK CLI](#install-the-bot-framework-sdk-cli)
1. [Get settings from your LUIS app](#get-settings-from-your-luis-app)
1. [Create your LUIS Model](#create-your-luis-model)
1. [Delete backup version if it exists](#delete-backup-version)
1. [Save current version as backup](#save-current-version-as-backup)
1. [Import new version of LUIS model](#import-new-version-of-luis-model)
1. [Train your LUIS Application](#train-your-luis-application)
1. [Publish your LUIS Application](#publish-your-luis-application)
1. [Generate source code](#generate-source-code)

## Install the Bot Framework SDK CLI

If you already have already installed the Bot Framework SDK CLI, you can skip to [Get settings from your LUIS app](#get-settings-from-your-luis-app).

1. If you have **Node.js** installed, make sure you have the latest version by running the following from a command prompt:

    ```bash
    npm i -g npm
    ```

1. If you do not have **Node.js** installed, you can install it from the [Node.js download page](https://nodejs.org/download/).

1. Using Node.js, install the latest version of the Bot Framework CLI from the command line.

    ```bash
    npm i -g @microsoft/botframework-cli
    ```

For more information see [Bot Framework CLI tool][bf-cli-overview].

## Get settings from your LUIS app

You will need the `appId` that is returned when the `luis:application:import` command successfully completed when you first created your LUIS app, if you created it using the BF CLI command. You will also need the `versionId` of the active version. If you do not have this information, you can use the [luis:application:list][bf-luisapplicationlist] command to get it. This command will list all LUIS apps that have been created in the specified LUIS authoring resource.

``` cli
bf luis:application:list --endpoint <endpoint> --subscriptionKey <subscription-key>
```

The results returned by the `luis:application:list` command include an `id` that you will use as the value for the `appId` option when executing the `luis:application:show` command as well as `activeVersion` that you will use later when making a backup of your active version before creating the new version of your LUIS model. For additional information on using this command, see [bf luis:application:list][bf-luisapplicationlist] in the BF CLI LUIS readme.

If you know your `appId`, but need to get the active version, you can also use the `luis:application:show` command. This will only return information for the specified LUIS app, and can be used when automating this process using a scripting language.

``` cli
bf luis:application:show --appId <application-id> --endpoint <endpoint> --subscriptionKey <subscription-key>
```

For additional information on using this command, see [bf luis:application:show][bf-luisapplicationshow] in the BF CLI LUIS readme.

## Create your LUIS Model

Once you have updated all the individual `.lu` files needed in your project, you can combine them to create your LUIS model using the `luis:convert` command. This results in a JSON file that you will reference when updating your LUIS application hosted in Azure Cognitive Services in the _LUIS authoring resource_ you created previously.

``` cli
bf luis:convert -i <input-folder-name> -o <output-file-name> -r --name <name>
```

For additional information on using this command, see [bf luis:convert][bf-luisconvert] in the BF CLI LUIS readme.

<!--
In the example below, the command is run in a command line while in the root directory of your project. It will search for all `.lu` files in the _dialogs_ directory and because of the `-r` option, all of its sub-directories. It will save a file named LUISModel.json in the _output_ directory.

``` cli
bf luis:convert -i dialogs -o .\output\LUISModel.json -r --name LUISModel.json
```
-->

> [!TIP]
>
> The `name` option is not required, however if you do not include this option you will need to manually update your LUIS model JSON before you import it or you will get an error: `Failed to import app version: Error: Application name cannot be null or empty.`

## Delete backup version

Before creating a new version of your LUIS model, you can create a backup of the active version. The next time you create a new update of your LUIS model you may want to delete the old backup before creating your new backup. Use the `luis:version:delete` command to do this.

``` cli
bf luis:version:delete --appId <application-id> --versionId <version-id> --endpoint <endpoint> --subscriptionKey <subscription-key>
```

For additional information on using this command, see [bf luis:version:delete][bf-luisversiondelete] in the BF CLI LUIS readme.

> [!NOTE]
>
> Be careful not to mistake the `luis:application:delete` command with the `luis:version:delete` command. The `luis:application:delete` command will permanently delete the LUIS application, along with all versions of all LUIS models associated with it. The `luis:version:delete` command will only delete the specified version. This command will delete a version, without warning, even if it is the only version of the model.

## Save current version as backup

Before you import the new version of your LUIS model, you can backup your active version. You can do this using the `luis:version:rename` command. You will need the `versionId` of the active version that you got from the previous section [Get settings from your LUIS app](#get-settings-from-your-luis-app), and you can set the `newVersionId` value to "backup" to specify that it is your backup version.

``` cli
bf luis:version:rename --appId <application-id> --versionId <version-id> --newVersionId <new-version-id> --endpoint <endpoint> --subscriptionKey <subscription-key>
```

For additional information on using this command, see [bf luis:version:rename][bf-luisversionrename] in the BF CLI LUIS readme.

> [!TIP]
>
> You are not limited to numeric characters for a versionId. If you name your backup version "backup", it will simplify the process of deleting your backup version.

## Import new version of LUIS model

You are now ready to import the new version of your model that you created in the [Create your LUIS Model](#create-your-luis-model) section of this article. You do this using the `luis:version:import`.

To update your LUIS app:

``` cli
luis:version:import --in <luis-model-json-file> --endpoint <endpoint> --subscriptionKey <subscription-key> --appId <app-id> --versionId <version-id>
```

For more details and to see all options available for this command see the [bf luis:application:import][bf-luisapplicationimport] section of the LUIS CLI readme.

## Train your LUIS Application

Training is the process of teaching your LUIS app to improve its natural language understanding. You need to train your LUIS app after you have made any updates to the model. For additional information see the [Train your active version of the LUIS app][luis-how-to-train] article in the LUIS docs.

To train your LUIS app, use the `luis:train:run` command:

```cli
bf luis:train:run --appId <application-id> --versionId <version-id> --endpoint <endpoint> --subscriptionKey <subscription-key>
```

For additional information on using this command, see [bf luis:train:run][bf-luistrainrun] in the BF CLI LUIS readme.

> [!TIP]
>
> After training your LUIS app, you should [test][luis-concept-test] it with sample utterances to see if the intents and entities are recognized correctly. If they're not, make updates to the LUIS app, train, and test again. This testing can be done manually in the LUIS site, for more information see the article [Test an utterance][test-an-utterance].

## Publish your LUIS Application

When you finish building, training, and testing your active LUIS app, make it available to your client application by publishing it to the endpoint. You can do that using the `luis:application:publish` command.

```cli
bf luis:application:publish --appId <application-id> --versionId <version-id> --endpoint <endpoint> --subscriptionKey <subscription-key>
```

For additional information on using this command, see [bf luis:application:publish][luisapplicationpublish] in the BF CLI LUIS readme.

For information about publishing a LUIS application, see [Publish your active, trained app to a staging or production endpoint][luis-how-to-publish-app].

## Generate source code

### Generate a C# class for the model results

The `luis:generate:cs` command can be used to generate a strongly typed C# source code from a LUIS model (JSON).

Run the following command to create a .cs representation of your LUIS model:

```cli
bf luis:generate:cs -i <luis-model-file> -o <output-file-name> --className <class-name>
```

For additional information on using this command, see [bf luis:generate:cs][bf-luisgeneratecs] in the BF CLI LUIS readme.

### Generate a TypeScript type for the model results

The `luis:generate:ts` command can be used to generate a strongly typed typescript source code from a LUIS model (JSON).

Run the following command to create a .ts representation of your LUIS model:

```cli
bf luis:generate:ts -i <luis-model-file> -o <output-file-name> --className <class-name>
```

For additional information on using this command, see [bf luis:generate:ts][bf-luisgeneratets] in the BF CLI LUIS readme.

<!-------------------------------------------------------------------------------------------------->
[luis-recognizer]: bot-builder-concept-adaptive-dialog-recognizers.md#luis-recognizer
[natural-language-processing-in-adaptive-dialogs]: bot-builder-concept-adaptive-dialog-recognizers.md#introduction-to-natural-language-processing-in-adaptive-dialogs
[language-understanding]: bot-builder-concept-adaptive-dialog-recognizers.md#language-understanding
[lu-templates]: ../file-format/bot-builder-lu-file-format.md
[luis-how-to-azure-subscription]: /azure/cognitive-services/luis/luis-how-to-azure-subscription
[bf-cli-overview]: bf-cli-overview.md

[bf-luisapplicationimport]: https://aka.ms/botframework-cli-luis#bf-luisapplicationimport
[bf-luisapplicationcreate]: https://aka.ms/botframework-cli-luis#bf-luisapplicationcreate
[bf-luisapplicationlist]: https://aka.ms/botframework-cli-luis#bf-luisapplicationlist
[bf-luisapplicationshow]: https://aka.ms/botframework-cli-luis#bf-luisapplicationshow
[bf-luistrainrun]: https://aka.ms/botframework-cli-luis#bf-luistrainrun
[luisapplicationpublish]: https://aka.ms/botframework-cli-luis#bf-luisapplicationpublish
[bf-luisgeneratecs]: https://aka.ms/botframework-cli-luis#bf-luisgeneratecs
[bf-luisgeneratets]: https://aka.ms/botframework-cli-luis#bf-luisgeneratets
[bf-luisversionrename]: https://aka.ms/botframework-cli-luis#bf-luisversionrename
[bf-luisversiondelete]:  https://aka.ms/botframework-cli-luis#bf-luisversiondelete
[bf-luisconvert]: https://aka.ms/botframework-cli-luis#bf-luisconvert

[luis-how-to-add-intents]: /azure/cognitive-services/LUIS/luis-how-to-add-intents
[luis-how-to-start-new-app]: /azure/cognitive-services/LUIS/luis-how-to-start-new-app
[luis-how-to-train]: /azure/cognitive-services/LUIS/luis-how-to-train
[luis-concept-test]: /azure/cognitive-services/LUIS/luis-concept-test
[test-an-utterance]: /azure/cognitive-services/LUIS/luis-interactive-test#test-an-utterance
[luis-interactive-test]: /azure/cognitive-services/LUIS/luis-interactive-test
[luis-how-to-publish-app]: /azure/cognitive-services/LUIS/luis-how-to-publish-app

[how-to-deploy-using-luis-cli]: bot-builder-howto-bf-cli-deploy-luis.md
<!-------------------------------------------------------------------------------------------------->

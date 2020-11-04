---
title: Create a bot cross trained to use both LUIS and QnA Maker recognizers - Bot Service
description: Describing how to create a bot that is cross trained to use both LUIS and QnA Maker recognizers
keywords: LUIS, QnA Maker, qna, bot, cross-train, cross train, adaptive dialogs, language generation, lg, lu 
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: how-to
ms.service: bot-service
ms.date: 10/10/2020
monikerRange: 'azure-bot-service-4.0'
---

# Create a bot cross trained to use both LUIS and QnA Maker recognizers

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This how to article describes the steps required to create a bot that integrates the capabilities of both [LUIS][luis] and [QnA Maker][qnamaker] together in each of your adaptive dialogs enabling each to determine the best possible response to the user, using [language generation][language-generation], regardless of which dialog is active.

Integrating the capabilities of both LUIS and QnA Maker requires the use of either the `luis:cross-train` or `qnamaker:cross-train` CLI command to _cross-train_ your `.lu` and `.qna` files, as well as using the [cross-trained recognizer set][cross-trained-recognizer-set-concept] as the adaptive dialogs recognizer.

## Prerequisites

This article walks you through the steps required to create a fully functional bot using adaptive dialogs, demonstrated using the C# [todo bot with LUIS and QnA Maker](#the-todo-bot-with-luis-and-qna-maker-sample) sample. In order to accomplish this, you need to understand the various concepts involved. Before you begin, you should have a solid basic understanding of the following concepts:

- [Introduction to adaptive dialogs](bot-builder-adaptive-dialog-introduction.md)
- [Events and triggers in adaptive dialogs](bot-builder-concept-adaptive-dialog-triggers.md)
- [Actions in adaptive dialogs](bot-builder-concept-adaptive-dialog-actions.md)
- [Recognizers in adaptive dialogs](bot-builder-concept-adaptive-dialog-recognizers.md)
- [Cross training your LUIS and QnA Maker models](bot-builder-concept-cross-train.md)
- [Handling interruptions in adaptive dialogs](bot-builder-concept-adaptive-dialog-interruptions.md)
- [.lu file format][lu-templates]
- [.qna file format][qna-file-format]

You will also need an [Azure account][create-azure-account].

## What is covered

This article describes how to create a bot that is cross trained to use both LUIS and QnA Maker recognizers to respond to a user. This will entail the following steps:

1. Introduction to the [todo bot with LUIS and QnA Maker](#the-todo-bot-with-luis-and-qna-maker-sample) sample
1. [Create your LUIS authoring resource in Azure Cognitive Services](#create-your-luis-authoring-resource-in-azure)
1. [Create your QnA Maker resource in Azure Cognitive Services](#create-your-qna-maker-resource-in-azure-cognitive-services)
1. [Install the Bot Framework SDK CLI](#install-the-bot-framework-cli)
1. [Generate cross-trained LU models](#generate-cross-trained-lu-models)
1. [Create and publish LUIS applications using the build command](#create-and-publish-luis-applications-using-the-build-command)
1. [Create and publish QnA Maker knowledge bases using the build command](#create-and-publish-qna-maker-knowledge-bases-using-the-build-command)
1. [Update your project's configuration file to include connection information for LUIS and QnA Maker](#update-your-projects-configuration-file-to-include-connection-information-for-luis-and-qna-maker)
1. [Testing the bot using Bot Framework Emulator](#testing-the-bot-using-bot-framework-emulator)

## The todo bot with LUIS and QnA Maker sample

The adaptive **todo bot with LUIS and QnA Maker** ([**C#**][cs-sample-todo-bot]) sample has five `.lu` files and six `.qna` files for language understanding. When creating the cross-trained language understanding models you will combine the `.lu` and `.qna` files in a way that will enable you to utilize the capabilities of both LUIS and QnA Maker together in the same recognizer, enabling you to train the recognizer how best to interpret and respond to a user's request. After running the cross-train command you will have new copies of each file, but they will be updated so that the `.lu` files contain the required `.qna` information and the `.qna` files contain the required `.lu` information, enabling the [Cross-trained recognizer set][crosstrainedrecognizerset-ref-guide] to determine how to best handle a users request.

If you have not done it yet, clone the latest samples repository, enter the following git command from a console window: `git clone https://github.com/Microsoft/botbuilder-samples.git`

## Create your LUIS authoring resource in Azure

If you don't already have a LUIS authoring resource you want to use for this, you can follow the steps below to create one. For an in-depth explanation of this process, see the article [Deploy LUIS resources using the Bot Framework LUIS CLI commands][create-your-luis-authoring-resource-in-azure].

1. Go to the Azure [Create LUIS Cognitive Services][create-cognitive-services-luis] page.  
2. In the **Create options** section, select **Authoring** to create a LUIS authoring resource.

   ![Set Create options to Authoring image](./media/adaptive-dialogs/create-options-authoring.png)

3. Enter values for each of the fields, then select the **Review + create** button.

   ![Set Create cognitive services image](./media/adaptive-dialogs/create-cognitive-services-cross-trainsample.png)

4. Review the values to ensure they are correct, then select the **Create** button.

The LUIS authoring resource will contain the information needed when running the [build command](#create-and-publish-luis-applications-using-the-build-command) or updating your bots [configuration file](#update-your-projects-configuration-file-to-include-connection-information-for-luis-and-qna-maker). Once the resource is created, you can find the information shown below  in the **Keys and Endpoint** blade in your LUIS authoring resource. Copy and save the values for later use.

- **Keys**. Either of the two key values are used as the _subscriptionKey_ option in the [build command](#create-and-publish-luis-applications-using-the-build-command), as well as the _LuisAPIKey_ in your [configuration file](#update-your-projects-configuration-file-to-include-connection-information-for-luis-and-qna-maker).
- **Endpoint**. This is the value that is used as the _endpoint_ option in the [build command](#create-and-publish-luis-applications-using-the-build-command), as well as the _LuisAPIHostName_ in your [configuration file](#update-your-projects-configuration-file-to-include-connection-information-for-luis-and-qna-maker).
- **Location**.   This is the value that is used as the _region_ option in the [build command](#create-and-publish-luis-applications-using-the-build-command).

   ![Keys and endpoint for LUIS resource in Azure](./media/adaptive-dialogs/keys-and-endpoint-cross-train.png)

## Create your QnA Maker resource in Azure Cognitive Services

If you don't already have a QnA Maker resource, you can follow the steps below to create one. For an in-depth explanation of this process, see the article [Deploy QnA Maker knowledge base using the Bot Framework qnamaker CLI commands][create-your-qna-maker-resource-in-azure-cognitive-services].

1. Go to the Azure [Create Qna Maker Cognitive Services][create-cognitive-services-qnamaker] page.
2. Enter values for each of the fields, then select the **Review + create** button.

   ![Create your QnA Maker knowledge base in Azure](./media/adaptive-dialogs/create-qna-maker-cross-trainsample.png)

3. Review the values to ensure they are correct, then select the **Create** button.

The QnA Maker resource will contain the value needed for the _subscriptionKey_ option used when running the [build command](#create-and-publish-qna-maker-knowledge-bases-using-the-build-command). Copy and save the value for later use.
``

   ![Keys and endpoint for QnA Maker resource in Azure](./media/adaptive-dialogs/qna-maker-keys-and-endpoint-cross-trainsample.png)

## Install the Bot Framework CLI

[!INCLUDE [applies-to-v4](../includes/install-bf-cli.md)]

## Generate cross-trained LU models

Before running the build command to create your LUIS applications and QnA Maker knowledge base in Azure cognitive services, you need to _cross-train_ your `.lu` and `.qna` files to include the information required by your bot's recognizer to defer user input to either LUIS or QnA Maker for processing. To better understand the concept of cross training please refer to the [Cross train your bot to use both LUIS and QnA Maker recognizers](bot-builder-concept-cross-train.md) article. For details on the `cross-train` command, please refer to the _Cross-trained recognizer set_ section of the [Recognizers in adaptive dialogs - reference guide](../adaptive-dialog/adaptive-dialog-prebuilt-recognizers.md#cross-trained-recognizer-set).

#### Cross-train the _todo bot with LUIS and QnA Maker_ sample

To cross-train the **todo bot with LUIS and QnA Maker** sample:

1. From a terminal, navigate to `samples\csharp_dotnetcore\adaptive-dialog\08.todo-bot-luis-qnamaker`

1. Create a directory in which to store the output of the command. This article uses _generated_ as the name for this directory.

   ```cmd
      md generated
   ```

1. Run the `luis:cross-train` command.

   ```cmd
      bf luis:cross-train -i dialogs -o generated --config dialogs\DialogLuHierarchy.config.json
   ```

Once finished you will have cross-trained versions of the five `.lu` files and six `.qna` files. When running the build commands in the following sections, point to the generated directory for the input files.

> [!IMPORTANT]
>
> You need to run either the `luis:cross-train` command or the `qnamaker:cross-train` command, you do not need to run both. by running `luis:cross-train`, all of the required cross-train information will be included in the resulting `.lu` and `.qna` files. Running both will work, but is unnecessary.

## Create and publish LUIS applications using the build command

For each `.lu` file, including `.lu` files for each locale, the build command combines all the following actions into a single command:

1. Creates one LUIS model for every locale found using your existing `.lu` files.
1. Using that model, it creates a new LUIS app in the specified Azure Cognitive Services resource if none exists, otherwise it will update the existing LUIS app.
1. When updating an existing LUIS app, it will automatically increment the versionId and optionally delete the old version.
1. Trains the new or updated LUIS app, then publishes it.

For a detailed explanation on how to use the `luis:build` command, see [Deploy LUIS applications using the Bot Framework luis CLI commands][luis-build].

> [!IMPORTANT]
>
> This command will overwrite your previous LUIS model as well any content you might have in your [LUIS applications][https://www.luis.ai/applications].

### How to use the luis:build command

The LUIS build command showing its required parameters when entered in the command line:

``` cli
bf luis:build --in <input-file-or-folder> --out <output-file-or-folder> --authoringKey <subscription-key> --region <authoring-region>
```

The `luis:build` command will create all assets you need from your local `.lu` files.

### Required luis:build parameters

- `--in`: The directory that will be searched for `.lu` files.
- `--out`: The directory to save output files to.
- `--botName`: The name of your bot. This will be used as the prefix for the name of the LUIS applications generated.
- `--authoringKey`: This is the subscriptionKey.
- `--region`: This defines the region to publish your LUIS applications.

For information on the additional options, see [bf luis:build][bf-luisbuild] in the BF CLI readme.

Alternatively, you can include these required, as well as any of the other parameters in a configuration file and refer to them using the `--luConfig` option.

### LUIS build configuration file

The following is a sample of the **luconfig.json** file that you can reference using the `--luConfig` option.

```json
{
    "in": "generated",
    "out": "output",
    "botName":"<your-bot-name>",
    "authoringKey":"<your-32-digit-subscription-key>",
	 "endpoint":"<your-endpoint>",
    "region": "<your-region-default-is-westus>"
}
```

Once this configuration file is created, all you need to do is reference it in your `luis:build` command. For example:

``` cli
bf luis:build --luConfig luconfig.json
```

For a detailed explanation on how to use the `luis:build` command, see [Deploy LUIS applications using the Bot Framework luis CLI commands][luis-build].

#### Create the LUIS applications for the _todo bot with LUIS and QnA Maker_ sample

To create the LUIS applications for the **todo bot with LUIS and QnA Maker** sample:

1. From a terminal, navigate to `samples\csharp_dotnetcore\adaptive-dialog\08.todo-bot-luis-qnamaker`

1. Create a directory in which to store the output of the command. This article uses _output_ as the name for this directory.

   ```cmd
      md output
   ```

1. Create your **luconfig.json** file, as shown in the previous section and save it to the root directory of the sample: `samples\csharp_dotnetcore\adaptive-dialog\08.todo-bot-luis-qnamaker`. Make sure and update the entries for the "botName", "authoringKey" and "region" options:

    ```json
    {
        "in": "generated",
        "out": "output",
        "botName":"todo bot with LUIS and QnA Maker",
        "authoringKey":"<your-32-digit-subscription-key>",
    	  "endpoint":"<your-endpoint>",
        "region": "<your-region-default-is-westus>"
    }
    ```

1. Run the `luis:build` command, using the luConfig file.

   ```cmd
      bf luis:build --luConfig luconfig.json
   ```

Once finished you will have a LUIS application for each of the five `.lu` files in [LUIS](https://www.luis.ai/conversations/applications):

![LUIS My apps list](./media/adaptive-dialogs/luis-apps-list.png)

> [!TIP]
>
> The `luis:build` command will save a file named **luis.settings.YourUserName.westus.json** to the output directory. This file contains the LUIS application IDs that you will need to add to your configuration file, described in a [later section](#update-your-projects-configuration-file-to-include-connection-information-for-luis-and-qna-maker).

## Create and publish QnA Maker knowledge bases using the build command

The `qnamaker:build` command combines all the following actions into a single command:

1. Creates one QnA Maker model for every locale found using your existing `.qna` files.
1. Creates a new QnA Maker KB if none exists, otherwise it will overwrite the existing KB.
1. It trains your QnA Maker KB then publishes it to the production endpoint.

For a detailed explanation on how to use the `qnamaker:build` command, see [Deploy QnA Maker knowledge base using the Bot Framework qnamaker CLI commands][qnamaker-build].

> [!TIP]
>
> If you haven't done so already, you will need to [Migrate to an Azure resource authoring key][luis-migration-authoring]. If you don't, will will not see the LUIS Applications in [LUIS][luis] created using the `luis:build` command.

### How to use the qnamaker:build command

The QnA Maker build command showing its required parameters when entered in the command line:

``` cli
bf qnamaker:build --in <input-file-or-folder> --out <folder-to-save-files-to> --subscriptionKey <Subscription-Key> --botName <bot-name>
```

> [!IMPORTANT]
>
> This command will overwrite your previous QnA Maker model as well any content you might have in your QnA Maker KB, including any content created directly in [QnA Maker.ai](https://www.qnamaker.ai/).

#### The qnamaker:build parameters

- `--in`: The directory, including sub-directories, that will be searched for .qna files.
- `--out`: The directory to save output files to.
- `--botName`: The name of your bot. This will be used to generate the name of the QnA Maker KB, this is explained in more detail in the [Deploy QnA Maker knowledge base using the Bot Framework qnamaker CLI commands][qna-maker-knowledge-bases-created] article.
- `--subscriptionKey`: The same subscription key that is in your [initialization file][create-your-qna-maker-initialization-file].

For information on additional parameters, see [bf qnamaker:build][bf-qnamakerbuild] in the BF CLI qnamaker readme.

Alternatively, you can include these required parameters in a configuration file and provide them via the `qnaConfig` parameter.

#### The qnamaker configuration file

The qnamaker configuration file is a JSON file that can contain any valid `qnamaker:build` option.

```json
{
  "in": "<location-of-qna-files>",
  "out": "<location-to-save-output-files>",
  "subscriptionKey": "<Enter-subscription-key-here>",
  "botName": "<Enter-botName-here>"
}
```

Once created all you need to do is reference it in your `qnamaker:build` command, for example:

``` cli
bf qnamaker:build --qnaConfig qnaConfig.json
```

For a detailed explanation on how to use the `qnamaker:build` command, see [Deploy QnA Maker knowledge base using the Bot Framework qnamaker CLI commands][qnamaker-build].

#### Create the QnA Maker knowledge base for the _todo bot with LUIS and QnA Maker_ sample

To create the QnA Maker knowledge base for the **todo bot with LUIS and QnA Maker** sample:

1. From a terminal, navigate to `samples\csharp_dotnetcore\adaptive-dialog\08.todo-bot-luis-qnamaker`

1. If it does not already exist, create a new directory named _output_

   ```cmd
      md output
   ```

1. Create your **qnaConfig.json** file, as shown in the previous section and save it to the root directory of the sample: `samples\csharp_dotnetcore\adaptive-dialog\08.todo-bot-luis-qnamaker`. Make sure and update the entries for the "botName", "subscriptionKey" and "region" options:

    ```json
    {
        "in": "generated",
        "out": "output",
        "botName":"<todo-bot-with-LUIS-and-QnA-Maker>",
        "subscriptionKey":"<your-32-digit-subscription-key>",
        "region": "<your-region-default-is-westus>"
    }
    ```


1. Run the `qnamaker:build` command, using the qnaConfig file.

   ```cmd
      bf qnamaker:build --qnaConfig qnaConfig.json
   ```

Once finished you will have a QnA Maker knowledge base with all of the questions and answers from the five `.qna` files in [QnA Maker](https://www.qnamaker.ai/Home/MyServices):

![QnA Maker KB list](./media/adaptive-dialogs/qna-maker-kb-list.png)

> [!TIP]
>
> The `qnamaker:build` command will save a file named **qnamaker.settings.\<your-user-name>.westus.json** to the output directory. This file contains the knowledge base ID that you will need to add to your configuration file, described in the next section.

## Update your project's configuration file to include connection information for LUIS and QnA Maker

The configuration file is named **appsettings.json**. The following shows the configuration file for **Todo bot with LUIS and QnA Maker** sample:

```json
{
  "MicrosoftAppId": "",
  "MicrosoftAppPassword": "",
  "LuisAPIKey": "",
  "LuisAPIHostName": "",
  "luis": {
    "GetUserProfileDialog_en_us_lu": "",
    "ViewToDoDialog_en_us_lu": "",
    "DeleteToDoDialog_en_us_lu": "",
    "RootDialog_en_us_lu": "",
    "AddToDoDialog_en_us_lu": ""
  },
  "QnAHostName": "",
  "QnAEndpointKey": "",
  "qna": {
    "TodoBotWithLuisAnDQnA_en_us_qna": ""
  }
}
```

The configuration file **appsettings.json** explained:

![The appsettings.json file](./media/appsettings.json.png)

1. The [Bot channels registration][bot-channels-registration] article details how to get the `MicrosoftAppId` and `MicrosoftAppPassword` values. These values are not required to complete this article.
2. The ***LuisAPIKey*** is the `subscriptionKey`, and the ***LuisAPIHostName*** is the `ENDPOINT` value. Both values are found in _Keys and Endpoint_ blade in the Azure cognitive services LUIS authoring resource page as shown in the screen shot below:

   ![Keys and endpoint for LUIS resource in Azure. Values for LuisAPIKey and LuisAPIHostName.](./media/adaptive-dialogs/keys-and-endpoint-cross-train.png)

3. This is a list of LUIS application IDs. These values can be found in the _Application Settings_ page for a LUIS application in [www.luis.ai](https://www.luis.ai/), however they are also listed in the settings file created by the `luis:build` command, saved to the location provided as the `--out` option. This settings file contains a list of every LUIS application ID that was created for each locale. The full name of this JSON file is `luis.settings.<username>.<authoring-region>.json`. For example, if your logged in username is _YuuriTanaka_ and you are targeting authoring region **westus**, your filename would be **luis.settings.YuuriTanaka.westus.json**. This is where you will find all the values for the _luis_ section of your **appsettings.json** file.

4. `QnAHostName` is the _Host_ value and `QnAEndpointKey` is the _EndpointKey_ value, both found in QnA Maker, accessed by selecting the **view code** button when in the _My knowledge bases_ page as shown in the screen shot below:

    ![QnA Maker View Code](./media/qna-maker-view-code.png)

5. The `qnamaker:build` command will include one settings file, saved to the location provided as the `--out` option, that contains a list of every QnA Maker knowledge base ID that was created for each locale. The full name of this JSON file is `qnamaker.settings.<username>.<authoring-region>.json`. For example, if your logged in username is _YuuriTanaka_ and you are targeting authoring region **westus**, your filename would be **qnamaker.settings.YuuriTanaka.westus.json**. This is where you will find all the values for the `qna` section of your **appsettings.json** file.

## Testing the bot using Bot Framework Emulator

## Prerequisites for testing the bot using Bot Framework Emulator

- [Visual Studio 2019 or later](https://www.visualstudio.com/downloads) or [Visual Studio Code](https://code.visualstudio.com/Download)
- [.NET Core 3.1](https://dotnet.microsoft.com/download)
- A copy of the C# [todo bot with LUIS and QnA Maker][cs-sample-todo-bot] sample.
- [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme)

## Build and run the bot locally

To run your bot locally, execute the commands shown below.

1. From a terminal, navigate to `samples\csharp_dotnetcore\adaptive-dialog\08.todo-bot-luis-qnamaker`

1. Run the bot.

   ```cmd
      dotnet run
   ```

This will build the application, deploy it to localhost, and launch the web browser to display the application's `default.htm` page. At this point, your bot should be running locally on port 3978, unless the `applicationUrl` setting in **launchSettings.json** was modified.

## Start the Emulator and connect to your bot

1. Start the Bot Framework Emulator.

1. Click **Open Bot** on the Emulator's **Welcome** tab.

1. Enter your bot's URL, which is the URL of the local port, with /api/messages added to the path, typically `http://localhost:3978/api/messages`.

   <!--This is the same process in the Emulator for all three languages.-->
   ![open a bot screen](../media/python/quickstart/open-bot.png)

1. Then click **Connect**.

You can now interact with your bot.

<!------------------------------------------------------------------------------------------------------------------>
[create-azure-account]: https://azure.microsoft.com/free/?WT.mc_id=A261C142F
[luis]: https://aka.ms/luis-what-is-luis
[qnamaker]: /azure/cognitive-services/qnamaker/overview/overview
[cs-sample-todo-bot]: https://aka.ms/csharp-adaptive-dialog-08-todo-bot-luis-qnamaker-sample
[lu-templates]: ../file-format/bot-builder-lu-file-format.md
[qna-file-format]: ../file-format/bot-builder-qna-file-format.md
[create-your-luis-authoring-resource-in-azure]: bot-builder-howto-bf-cli-deploy-luis.md#create-your-luis-authoring-resource-in-azure
[create-your-qna-maker-resource-in-azure-cognitive-services]: bot-builder-howto-bf-cli-deploy-qna.md#create-your-qna-maker-resource-in-azure-cognitive-services

[bot-channels-registration]: ../bot-service-quickstart-registration.md

[cross-trained-recognizer-set-concept]: bot-builder-concept-adaptive-dialog-recognizers.md#cross-trained-recognizer-set

[crosstrainedrecognizerset-ref-guide]: ../adaptive-dialog/adaptive-dialog-prebuilt-recognizers.md#cross-trained-recognizer-set

[language-generation]: bot-builder-concept-adaptive-dialog-generators.md
[create-cognitive-services-luis]: https://portal.azure.com/#create/Microsoft.CognitiveServicesLUISAllInOne
[create-cognitive-services-qnamaker]: https://portal.azure.com/#create/Microsoft.CognitiveServicesQnAMaker

[bf-qnamakerbuild]: https://aka.ms/botframework-cli#bf-qnamakerbuild
[qnamaker-build]: bot-builder-howto-bf-cli-deploy-qna.md#create-a-qna-maker-knowledge-base-and-publish-it-to-production-using-the-build-command
[qna-maker-knowledge-bases-created]: bot-builder-howto-bf-cli-deploy-qna.md#qna-maker-knowledge-bases-created
[create-your-qna-maker-initialization-file]: bot-builder-howto-bf-cli-deploy-qna.md#create-your-qna-maker-initialization-file

[bf-luisbuild]: https://aka.ms/botframework-cli#bf-luisbuild
[luis-build]: bot-builder-howto-bf-cli-deploy-luis.md#create-and-train-a-luis-app-then-publish-it-using-the-build-command

[luis-migration-authoring]: /azure/cognitive-services/luis/luis-migration-authoring

<!---------------------------------------------------------------------------------------------------------------->

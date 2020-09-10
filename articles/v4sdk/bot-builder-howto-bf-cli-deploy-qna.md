---
title: Deploy QnA Maker knowledge base using the Bot Framework QnA Maker CLI commands - Bot Service
description: Describing how to automate the process of deploying QnA Maker knowledge base using the Bot Framework QnA Maker CLI commands
keywords: QnA Maker, knowledge base, KB,bf cli, qnamaker, QnA Maker Models, bot, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: how-to
ms.service: bot-service
ms.date: 09/10/2020
monikerRange: 'azure-bot-service-4.0'
---

# Deploy QnA Maker knowledge base using the Bot Framework qnamaker CLI commands

[!INCLUDE [applies-to-v4](../includes/applies-to.md)]

The Bot Framework Command Line Interface (CLI) lets you automate the management of QnA Maker knowledge base. It lets you create, update, and delete QnA Maker knowledge base from the command line or a script. This article explains how to deploy QnA Maker knowledge base to a QnA Maker knowledge base in Azure.

## Prerequisites

- Basic understanding of [QnA Maker][qna-overview]
- Knowledge of the [.qna file format][qna-file-format].
- Have a bot project with `.qna` files.
- If working with adaptive dialogs, you should have an understanding of:
    - [Natural Language Processing in adaptive dialogs][natural-language-processing-in-adaptive-dialogs].
    - how the [QnA Maker recognizer][QnA-maker-recognizer] is used.

## Using the qnamaker CLI commands to enable QnA Maker in your bot

This article describes how to perform some common tasks using the Bot Framework CLI.

1. [Create your QnA Maker resource in Azure Cognitive Services](#create-your-luis-authoring-resource-in-azure-cognitive-services)
1. [Install the Bot Framework SDK CLI](#install-the-bot-framework-sdk-cli)
1. [Create your QnA Maker model](#create-your-qna-maker-model)
1. [Create your QnA Maker knowledge base](#create-your-qna-maker-knowledge-base)
1. [Create your QnA Maker initialization file](#create-your-qna-maker-initialization-file)
1. [Publish your QnA Maker knowledge base](#publish-your-qna-maker-knowledge-base)

Once your bot project's QnA Maker knowledge base `.qna` files have been created, you are ready to follow the steps outlined in this article to create your QnA Maker knowledge base.

## Create your QnA Maker resource in Azure Cognitive Services

The QnA Maker resource is an [Azure Cognitive Services][cognitive-services-overview] resource that you create using Azure's [Create Cognitive Services][create-cognitive-services] page. This provides the security keys and endpoint needed to access your QnA Maker knowledge base in Azure.

1. Go to the Azure [Create Cognitive Services][create-cognitive-services] page.  
2. Enter values for each of the fields, then select the **Review + create** button.

   ![Create your QnA Maker knowledge base in Azure](./media/adaptive-dialogs/create-qna-maker.png)

    > [!NOTE]
    > When entering the **Resource Group** and **Name**, keep in mind that you cannot change these values later. Also note that the value you give for **Name** will be part of your **Endpoint URL**.

3. Review the values to ensure they are correct, then select the **Create** button.

The QnA Maker resource includes information your bot will use to access your QnA Maker knowledge base:

- **Keys**. These are called _subscription keys_ and are auto generated. You will need the subscription key when referencing your QnA Maker resource for any action, such as when creating or updating your QnA Maker knowledge base which will be detailed in this article. You can find the keys in the **Keys and Endpoint** blade in your QnA Maker resource.
- **Endpoint**. This is auto-generated using the QnA Maker resource name that you provide when creating it. It has the following format: `https://<qnamaker-resource-name>.cognitiveservices.azure.com/`. When referencing your QnA Maker resource for any action, such as when creating your QnA Maker knowledge base which will be detailed in this article. You can find the key in the **Keys and Endpoint** blade in your QnA Maker resource.
- **Location**.   This is the Azure region that contains your QnA Maker knowledge base. You select this when creating the QnA Maker resource.

   ![The Keys and endpoint blade in Azure](./media/adaptive-dialogs/qna-maker-keys-and-endpoint.png)

For more information on see [Create QnA Maker knowledge bases][luis-how-to-azure-subscription].

## Install the Bot Framework SDK CLI

If you have already installed the Bot Framework CLI you can skip ahead to [Create your QnA Maker model](#create-your-qna-maker-model).

[!INCLUDE [applies-to-v4](../includes/install-bf-cli.md)]

## Create your QnA Maker model

Once you have created the individual `.qna` files for your bot, you can convert them into a single _QnA Maker model_ using the `qnamaker:convert` command. The QnA Maker model is a JSON file used to create a QnA Maker knowledge base.

To create your QnA Maker model:

``` cli
bf qnamaker:convert -i <input-folder-name> -o <output-folder-name> --name <QnA-KB-Name> -r
```

In the following example, this command will recursively search for all `.qna` files in the _dialogs__ directory and any subdirectories and merge them into a single file named **converted.json** in the _output_ directory. This JSON file will contain all of the information needed to create a QnA Maker KB, including the name _MyQnAMakerBot_ which will be the name of the knowledge base.

> `bf qnamaker:convert -i dialogs -o output --name MyQnAMakerBot -r`

For additional information on using this command, see [`bf qnamaker:convert`][bf-qnamakerconvert] in the BF CLI QnA Maker readme.

## Create your QnA Maker knowledge base

The _QnA Maker resource_ you perviously created consists of two subscription keys and an endpoint. These are values that you need when creating your QnA Maker knowledge base (QnA Maker KB). You can have multiple QnA Maker KBs associated with a single QnA Maker resource, each QnA Maker KB will have its own ID, named `kbId`. This value will be returned as a part of the creation process. You will need this ID when referring to this QnA Maker KB in the future. This QnA Maker KB provides your bot with all functionality provided by QnA Maker.

To create your QnA Maker KB:

``` cli
bf qnamaker:kb:create -i <QnA-Maker-model-JSON-file> --subscriptionKey <Subscription-Key> --name <QnA-Maker-kb-name>
```

> [!NOTE]
>
> - The input file for this command is the file that is created by running the `qnamaker:convert` command as discussed in the previous step.
> - The `name` option is optional if the QnA Model JSON file has a value for the name property, otherwise it will be required.

For additional information on using this command, see [`bf qnamaker:kb:create`][bf-qnamakerkbcreate] in the BF CLI QnA Maker readme.

## Create your QnA Maker initialization file

You will use the knowledge base ID (`kbId`) returned when your created the QnA Maker KB in the previous step when creating your init file using the `qnamaker:init` command. This will create a JSON file containing the data that is required when running many of the QnAMaker BF CLI commands, these values include _subscriptionKey_, _kbId_, _endpointKey_ and _hostname_. Once this file is created, all future BF CLI commands will automatically get these values from this init file.

``` cli
bf qnamaker:init
```

To create the QnA Maker CLI init file:

1. From your console, enter `bf qnamaker:init`
1. You will be prompted for the subscription key to your QnA Maker Cognitive Services resource in Azure. You can find this in the _Keys and Endpoint_ blade:

    ![QnA Maker Keys and Endpoint in Azure](./media/adaptive-dialogs/keys-and-endpoint-qnamaker.png)

1. Next you will be prompted for your knowledge base ID (`kbId`). You can find it in [QnAMaker](https://www.qnamaker.ai/) in the _Deployment details_ section of the _SETTINGS_ page:

     ![QnA Maker Keys and Endpoint in Azure](./media/adaptive-dialogs/settings-deployment-details-qnamaker.png)

1. The values are gathered and written out to the screen for you to verify. If correct type `yes` or just press the **Enter** key.
1. The file is then created and saved to  _C:\Users\<unsername>\AppData\Local\@microsoft\botframework-cli\config.json_

> When you enter a `bf qnamaker` CLI command, it will automatically look for the _subscriptionKey_, _kbId_, _endpointKey_ and _hostname_ values in this init file unless you include them when entering the command, at which point the values entered will override the values from the init file.

For additional information on using this command, see [`bf qnamaker:init`][bf-qnamakerinit] in the BF CLI QnA Maker readme.

## Publish your QnA Maker knowledge base

Newly created QnA Maker KBs are automatically published to the _test_ endpoint where it can be tested prior to it going live. For general information about testing your KB, see [Test your knowledge base in QnA Maker][test-knowledge-base].

Once tested you can use the `qnamaker:kb:publish` to publish it to the _production_ endpoint.

To publish your QnA Maker knowledge base:

``` cli
bf qnamaker:kb:publish
```

To publish your QnA Maker knowledge base if you do not have an [init file](#create-your-qna-maker-initialization-file) file:

``` cli
bf qnamaker:kb:publish --subscriptionKey <Subscription-Key> --kbId <knowledge-base-id>
```

> [!TIP]
>
> If scripting this process from end to end you can also script the testing process. For additional information see [Test knowledge base with batch questions and expected answers][batch-testing].

<!-------------------------------------------------------------------------------------------------->
[cognitive-services-overview]: /azure/cognitive-services/Welcome
[create-cognitive-services]: https://portal.azure.com/#create/Microsoft.CognitiveServicesQnAMaker

[natural-language-processing-in-adaptive-dialogs]: bot-builder-concept-adaptive-dialog-recognizers.md#introduction-to-natural-language-processing-in-adaptive-dialogs

[bf-cli-overview]: bf-cli-overview.md

[bf-qnamakerconvert]: https://aka.ms/botframework-cli-qnamaker#bf-qnamakerconvert
[bf-qnamakerkbcreate]: https://aka.ms/botframework-cli-qnamaker#bf-qnamakerkbcreate
[bf-qnamakerkbpublish]: https://aka.ms/botframework-cli-qnamaker#bf-qnamakerkbpublish
[bf-qnamakerinit]: https://aka.ms/botframework-cli-qnamaker#bf-qnamakerinit

[test-knowledge-base]: /azure/cognitive-services/QnAMaker/how-to/test-knowledge-base
[batch-testing]: /azure/cognitive-services/QnAMaker/quickstarts/batch-testing

<!-------------------------------------------------------------------------------------------------->

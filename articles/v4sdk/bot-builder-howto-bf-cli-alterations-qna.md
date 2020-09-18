---
title: Create synonyms for your QnA Maker KB using alterations - Bot Service
description: Create customized lists of synonyms for your QnA Maker knowledge base using the Bot Framework CLI qnamaker:alterations command.
keywords: QnA Maker, knowledge base, KB, bf cli, qnamaker, synonyms, alterations, qnamaker:alterations, bot, adaptive dialogs
author: WashingtonKayaker
ms.author: kamrani
manager: kamrani
ms.topic: how-to
ms.service: bot-service
ms.date: 09/12/2020
monikerRange: 'azure-bot-service-4.0'
---

# Create customized lists of synonyms for your QnA Maker knowledge base

[!INCLUDE [applies-to-v4](../includes/applies-to.md)]

The Bot Framework CLI lets you automate the management of QnA Maker knowledge bases. It lets you create, update, and delete a QnA Maker knowledge base (KB) from the command line or a script. It also enables you to create a list of synonyms that applies to your KB. This article explains how to create synonyms for your QnA Maker KB using the CLI _alterations_ command.

## Prerequisites

- An existing QnA Maker KB. If you do not already have a QnA Maker KB, you can create one using the steps outlined in [Deploy QnA Maker knowledge base using the Bot Framework qnamaker CLI commands][deploy-qna-maker-knowledge-base-using-bf-cli-qnamaker] article.
- QnA Maker initialization file. If you do not already have a QnA Maker initialization file, you can create one using the steps outlined in [Create your QnA Maker initialization file][qnamaker-init-file] in the [Deploy QnA Maker knowledge base using the Bot Framework qnamaker CLI commands][deploy-qna-maker-knowledge-base-using-bf-cli-qnamaker] article.

## An introduction to alterations in QnA Maker

The alterations command lets you import customized lists of synonyms into your QnA Maker KB. Alterations are a list of words that have the same meaning. For example, a synonym for the word "gift" could be the word "present".

Alterations can be very useful for abbreviations as well. For example "GDPR" is a widely used term, but some people might call it "AVG", which is the Dutch abbreviation. Companies often have their own unique list of abbreviations referring to different features or components that their products offer.

While QnA Maker already has their own internal list of common pre-trained synonyms in several languages, many companies can still benefit from having additional synonyms.

Alterations can also help improve the quality of your KB while reducing the number of question/answer pairs as well as the time needed to train it.

> [!TIP]
>
> It's not possible to create customized lists of synonyms using the QnA Maker portal, however it is available using the Bot Framework CLI.

## Install the Bot Framework SDK CLI

If you have already installed the Bot Framework CLI you can skip ahead to [Use the qnamaker CLI commands to create a list of synonyms for your QnA Maker knowledge base](#use-the-qnamaker-cli-commands-to-create-a-list-of-synonyms-for-your-qna-maker-knowledge-base).

[!INCLUDE [applies-to-v4](../includes/install-bf-cli.md)]

## Use the qnamaker CLI commands to create a list of synonyms for your QnA Maker knowledge base

When you [create your QnA Maker model][create-your-qna-maker-model] two JSON files are created, the _QnAMaker model_ which is named **converted.json**, and the _alterations_ file which is named **alterations_converted.json**. While the QnAMaker model contains the data from all the `.qna` files in your project, all combined to form a single file, the alterations file contains only an empty alterations list, as shown below:

```json
{
  "wordAlterations": []
}
```

> [!NOTE]
>
> You do not have to create your QnA Maker model or KB using BF CLI command in order to use the alterations commands to create your list of synonyms.

The alterations file is a JSON file that contains an array of _wordAlterations_ which consists of an array of _alterations_, which is a list of synonyms, for example:

```json
{
  "wordAlterations": [
    {
      "alterations": [
        "qnamaker",
        "qna maker"
      ]
    },
    {
      "alterations": [
        "botframework",
        "bot framework"
      ]
    },
    {
      "alterations": [
        "bot framework command line interface",
        "bot framework cli",
        "bf cli"
      ]
    }
  ]
}
```

Once the alterations file is created, you can pass it to the `qnamaker:alterations:replace` command as the `input` property to replace the empty alterations list created by default when creating the QnA Maker KB. You will use the same command anytime you need to update the existing list.

``` cli
bf qnamaker:alterations:replace -i <input-file-name>
```

> [!NOTE]
>
> If you do not have an [init file][qnamaker-init-file], you will need to include the subscription key:
> `bf qnamaker:alterations:replace -i <input-file-name> --subscriptionKey <Subscription-Key>`

> [!IMPORTANT]
>
> You cannot incrementally add or remove items from the list of alterations in Azure. When you run the alterations replace command, the alterations list in azure is deleted and replaced with the file passed in.

For additional information on using this command, see [`bf qnamaker:alterations:replace`][bf-qnamakeralterationsreplace] in the BF CLI QnA Maker README.

## Download the list of alterations in your QnA Maker knowledge base

If you need to see what synonyms are in your QnA Maker KB, you can use the `qnamaker:alterations:list` command.

``` cli
bf qnamaker:alterations:list
```

> [!NOTE]
>
> If you do not have an [init file][qnamaker-init-file], you will need to include the subscription key:
> `bf qnamaker:alterations:list --subscriptionKey <Subscription-Key>`

For additional information on using this command, see [`bf qnamaker:alterations:list`][bf-qnamakeralterationslist] in the BF CLI QnA Maker README.

## Update the list of alterations in your QnA Maker knowledge base

While there is no command to directly update an existing alterations list in QnA Maker, you can [use the _alterations list_ command](#download-the-list-of-alterations-in-your-qna-maker-knowledge-base) to download the alterations list, make needed modifications, and then using that new list replace your alterations list in Azure.

1. Get the current list of alterations using the command `bf qnamaker:alterations:list`

    > [!TIP]
    > You can send the results directly to a file using a piping command such as the DOS `>` command, the following example will create a file named **alterations_converted.json** in the current directory:
    >
    > `bf qnamaker:alterations:list >alterations_converted.json`

1. Make any desired updates to the JSON file then save those changes.
1. Replace the alterations list that is in your QnA Maker KB using the command: `bf qnamaker:alterations:replace -i <input-file-name>`. If you saved the alterations list JSON file as alterations_converted.json in the current directory, the command will be: `bf qnamaker:alterations:replace -i alterations_converted.json`

<!-------------------------------------------------------------------------------------------------->
[deploy-qna-maker-knowledge-base-using-bf-cli-qnamaker]: bot-builder-howto-bf-cli-deploy-qna.md
[create-your-qna-maker-model]: bot-builder-howto-bf-cli-deploy-qna.md#create-your-qna-maker-model
[qnamaker-init-file]: bot-builder-howto-bf-cli-deploy-qna.md#create-your-qna-maker-initialization-file

[bf-qnamakeralterationsreplace]: https://aka.ms/botframework-cli#bf-qnamakeralterationsreplace
[bf-qnamakeralterationslist]: https://aka.ms/botframework-cli#bf-qnamakeralterationslist

[bf-qnamakerconvert]: https://aka.ms/botframework-cli#bf-qnamakerconvert
<!-------------------------------------------------------------------------------------------------->

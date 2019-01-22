---
title: Manage bots using CLI tools
description: Bot Framework Tools allows you to manage your bot resources directly from the command line
keywords: botbuilder templates, ludown, qna, luis, msbot, manage, cli, .bot, bot
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: tools
ms.date: 11/13/2018
monikerRange: 'azure-bot-service-4.0'
---

# Manage bots using CLI tools

Bot Framework Tools cover end-to-end bot development workflow that includes planning, building, testing, publishing, connecting, and evaluation phase. Let's see how these tools can help you with each phase of the development cycle.

## Plan

### Create mock conversations using Chatdown

Chatdown is a transcript generator which consumes a .chat file to generate mock transcripts. Generated mock transcript files are output to stdout.

A good bot, just like any successful application or a website, starts with clarity on supported scenarios. Creating mockups of conversations between bot and user is useful for:

- Framing the scenarios supported by the bot.
- Business decision makers to review, provide feedback.
- Defining a "happy path" (as well as other paths) through conversational flows between a user and a bot .chat file format helps you create mockups of conversations between a user and a bot. Chatdown CLI tool converts .chat files into conversation transcripts (.transcript files) that can be viewed in the [Bot Framework Emulator V4](https://github.com/microsoft/botframework-emulator).

Here's an example `.chat` file:

```markdown
user=Joe
bot=LulaBot

bot: Hi!
user: yo!
bot: [Typing][Delay=3000]
Greetings!
What would you like to do?
* update - You can update your account
* List - You can list your data
* help - you can get help

user: I need the bot framework logo.

bot:
Here you go.
[Attachment=bot-framework.png]
[Attachment=http://yahoo.com/bot-framework.png]
[AttachmentLayout=carousel]

user: thanks
bot:
Here's a form for you
[Attachment=card.json adaptivecard]
```

### Create a transcript file from .chat file

A Chatdown command looks like the following:

```bash
chatdown sample.chat > sample.transcript
```

This will consume `sample.chat` and output `sample.transcript`. See [Chatdown CLI][chatdown] for more information.

## Build

### Create a LUIS application with LUDown

The LUDown tool can be used to create new .json models for both LUIS and QnA.  
You can define [intents](https://docs.microsoft.com/en-us/azure/cognitive-services/luis/add-intents) and [entities](https://docs.microsoft.com/en-us/azure/cognitive-services/luis/add-entities) for a LUIS application just like you would from the LUIS portal.

'#\<intent-name\>' describes a new intent definition section. Each line afterwards lists the [utterances](https://docs.microsoft.com/en-us/azure/cognitive-services/luis/add-example-utterances) which describe that intent.

For example, you can create multiple LUIS intents in a single .lu file as follows:

```LUDown
# Greeting
- Hi
- Hello
- Good morning
- Good evening

# Help
- help
- I need help
- please help
```

### Create QnA pairs with LUDown

The .lu file format supports also QnA pairs using the following notation:

~~~LUDown
> comment
### ? question ?
  ```
    answer
  ```
~~~

The LUDown tool will automatically separate question and answers into a qnamaker JSON file that you can then use to create your new [QnaMaker.ai](http://qnamaker.ai) knowledge base.

~~~LUDown
### ? How do I change the default message for QnA Maker?
  ```
  You can change the default message if you use the QnAMakerDialog. 
  See [this link](https://docs.botframework.com/en-us/azure-bot-service/templates/qnamaker/#navtitle) for details.
  ```
~~~

You can also add multiple questions to the same answer by simply adding new lines of variations of questions for a single answer.

~~~LUDown
### ? What is your name?
- What should I call you?
  ```
    I'm the echoBot! Nice to meet you.
  ```
~~~

### Generate .json models with LUDown

After you've defined LUIS or QnA language components in the .lu format, you can publish out to a LUIS .json, QnA .json, or QnA .tsv file. When run, the LUDown tool will look for any .lu files within the same working directory to parse. Since the LUDown tool can target both LUIS or QnA with .lu files, we simply need to specify which language service to generate for, using the general command **ludown parse \<to-service-type> -- in \<lu-file-path>**.

In our sample working directory, we have two .lu files to parse, '1.lu' to create LUIS model, and 'qna1.lu' to create a QnA knowledge base.

#### Generate LUIS .json models

To generate a LUIS model using LUDown, in your current working directory simply enter the following:

```shell
ludown parse ToLuis --in <luFile>
```

#### Generate QnA Knowledge Base

Similarly, to create a QnA knowledge base, you only need to change the parse target.

```shell
ludown parse ToQna --in <luFile>
```

The resulting JSON files can be consumed by LUIS and QnA either through their respective portals, or via the new CLI tools. See [LUdown CLI][ludown] GitHub repo to learn more.

### Track service references using .bot file

The new [MSBot][msbotCli] tool allows you to create a **.bot** file, which stores metadata about different services your bot consumes, all in one location. This file also enables your bot to connect to these services from the CLI. The tool is available as an npm module, to install it run:

```shell
npm install -g msbot
```

To create a bot file, from your CLI enter **msbot init** followed by the name of your bot, and the target URL endpoint, for example:

```shell
msbot init --name TestBot --endpoint http://localhost:9499/api/messages
```
To connect your bot to a service, in your CLI enter **msbot connect** followed by the appropriate service:

```shell
msbot connect [Service]
```

To get the list of supported services, refer to the [readme][msbotCli] file.

### Create and manage LUIS applications using LUIS CLI

Included in the new tool set is a [LUIS extension][luisCli] which allows you to independently manage your LUIS resources. It is available as an npm module which you can download:

```shell
npm install -g luis-apis
```
The basic command usage for the LUIS tool from the CLI is:

```shell
luis <action> <resource> <args...>
```
To connect your bot to LUIS, you will need to create a **.luisrc** file. This is a configuration file which provisions your LUIS appID and password to the service endpoint when your application makes outbound calls. You can create this file by running **luis init** as follows:

```shell
luis init
```
You will be prompted in the terminal to enter your LUIS authoring key, region, and appID before the tool will generate the file.  

Once this file is generated, your application will be able to consume the LUIS .json file (generated from LUDown) using the following command from the CLI.

```shell
luis import application --in luis-app.json | msbot connect luis --stdin
```
See the [LUIS CLI][luisCli] GitHub repo to learn more.

### Create QnA Maker KB using QnA Maker CLI

Included in the new tool set is a [QnA extension][qnaCli] which allows you to independently manage your LUIS resources. It is available as an npm module which you can download.

```shell
npm install -g qnamaker
```
With the QnA maker tool, you can create, update, publish, delete, and train your knowledge base. You can use files generated via [ludown parse toqna](#generate-qna-knowledge-base) command to create/ replace a knowledge base.

```shell
qnamaker create --in qnaKB.json --msbot | msbot connect qna --stdin
```

See the [QnA Maker CLI][qnaCli] GitHub repo to learn more.

### Create dispatch model using Dispatch CLI

Dispatch is a tool to create and evaluate LUIS models used to dispatch intent across multiple bot modules such as LUIS models, QnA knowledge bases and others (added to dispatch as a file type).

Use the Dispatch model in cases when:

- Your bot consists of multiple modules and you need assistance in routing user's utterances to these modules and evaluate the bot integration.
- Evaluate quality of intents classification of a single LUIS model.
- Create a text classification model from text files.

Once you have assembled your .bot file with the [LUIS applications][msbotCli-luis] and [QnA Maker knowledge bases][msbotCli-qna] your bot relies on, you can simply build a dispatch model using: 

```shell
dispatch create -b <YOUR-BOT-FILE> | msbot connect dispatch --stdin
```
See [Disptach CLI][dispatchCli] to learn more.

## Test

The Bot Framework [Emulator](bot-service-debug-emulator.md) is a desktop application that allows bot developers to test and debug their bots on localhost or running remotely through a tunnel.

## Publish

You can use the Azure CLI to create, download, and publish your bot to Azure Bot Service.

With msbot 4.3.2 and later, you need Azure CLI version 2.0.53 or later. If you installed the botservice extension, remove it with this command.

```shell
az extension remove --name botservice
```

### Create Azure Bot Service bot

Note: You must use the latest version of the `az cli`. Please upgrade it, so that az cli can work with MSBot tool.

Login to your azure account via

```shell
az login
```

If you don't already have a resource group to which to publish your bot, create one:

```shell
az group create --name <resource-group-name> --location <geographic-location> --verbose
```

| Option | Description |
|:---|:---|
| --name | A unique name for the resource group. DO NOT include spaces or underscores in the name. |
| --location | Geographic location used to create the resource group. For example, `eastus`, `westus`, `westus2`, and so on. Use `az account list-locations` for a list of locations. |

Then, create the bot resource into which you will publish your bot.

```shell
az bot create [options]
```

To create a bot and update the .bot file with the bot configuration,  
```shell
az bot create [options] --msbot | msbot connect bot --stdin
```

If you have an existing bot,  
```shell
az bot show [options] --msbot | msbot connect bot --stdin
```

| Option                            | Description                                   |
|-----------------------------------|-----------------------------------------------|
| --kind -k [Required]              | The Kind of the Bot.  Allowed values: function, registration, webapp.|
| --name -n [Required]              | The Resource Name of the bot. |
| --appid                           | The msa account id to be used with the bot.   |
| --location -l                     | Location. You can configure the default location using `az configure --defaults location=<location>`.  Default: westus.|
| --msbot                           | Show the output as json compatible with a .bot file.  Allowed values: false, true.|
| --password -p                     | The msa password for the bot from developer portal. |
| --resource-group -g               | Name of resource group. You can configure the default group using `az configure --defaults group=<name>`.  Default: build2018. |
| --tags                            | Set of tags to add to the bot. |

### Configure channels

You can use the Azure CLI to manage channels for your bot.

```shell
>az bot -h
Group
   az bot: Manage Bot Services.
    Subgroups:
        directline: Manage Directline Channel on a Bot.
        email     : Manage Email Channel on a Bot.
        facebook  : Manage Facebook Channel on a Bot.
        kik       : Manage Kik Channel on a Bot.
        msteams   : Manage Msteams Channel on a Bot.
        skype     : Manage Skype Channel on a Bot.
        slack     : Manage Slack Channel on a Bot.
        sms       : Manage Sms Channel on a Bot.
        telegram  : Manage Telegram Channel on a Bot.
        webchat   : Manage Webchat Channel on a Bot.

    Commands:
        create    : Create a new Bot Service.
        delete    : Delete an existing Bot Service.
        download  : Download an existing Bot Service.
        publish   : Publish to an existing Bot Service.
        show      : Get an existing Bot Service.
        update    : Update an existing Bot Service.

```

## Additional information

- [Bot Framework Tools on GitHub][cliTools]
- [.lu file format](https://aka.ms/ludown-file-format)

<!-- Footnote links -->

[cliTools]: https://aka.ms/botbuilder-tools-readme
[azureCli]: https://aka.ms/botbuilder-tools-azureCli
[msbotCli]: https://aka.ms/botbuilder-tools-msbot-readme
[msbotCli-luis]: https://aka.ms/botbuilder-tools-msbot-readme#connecting-to-luis-application
[msbotCli-qna]: https://aka.ms/botbuilder-tools-msbot-readme#connecting-to-qna-maker-knowledge-base
[chatdown]: https://aka.ms/botbuilder-tools-chatdown
[ludown]: https://aka.ms/botbuilder-ludown
[luisCli]: https://aka.ms/botbuilder-luis-cli
[qnaCli]: https://aka.ms/botbuilder-tools-qnaMaker
[dispatchCli]: https://aka.ms/botbuilder-tools-dispatch

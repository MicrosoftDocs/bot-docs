---
title: Bot Service Frequently Asked Questions - Bot Service
description: A list of Frequently Asked Questions about deploy a bot to Azure.
author: zxyanliu
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 06/09/2020
---

# Deployment Frequently Asked Questions

This article contains answers to some frequently asked questions about how to deploy a bot to Azure covered in [this article](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-deploy-az-cli?view=azure-bot-service-4.0).

## What files do I need to zip up?

You will need to manually zip the files in the [zip up the code directory manually step](https://docs.microsoft.com/en-us/azure/bot-service/bot-builder-deploy-az-cli?view=azure-bot-service-4.0&tabs=csharp#52-zip-up-the-code-directory-manually). 

You need to select all the files and folders in your bot's project folder, and then, still in the bot's project folder, zip up all the selected files and folders.

For example:
![image](https://user-images.githubusercontent.com/32497439/83956487-98d28580-a813-11ea-92d0-21c7c2f1ead4.png)

Then you will have a `.zip` file (the name may differ) such as this one:

![image](https://user-images.githubusercontent.com/32497439/83956512-c15a7f80-a813-11ea-9c72-7441e32e0c7b.png)

After that, you can run the command to deploy your bot.

## What cli command should I use when getting errors showing Azure cli deprecation?

It is recommended that you use the most latest version of Azure cli. 
For Azure CLI version [2.2.0](https://github.com/MicrosoftDocs/azure-docs-cli/blob/master/docs-ref-conceptual/release-notes-azure-cli.md#march-10-2020) or later, users should use `az deployment sub create` and `az deployment group create` instead of respectively using `az deployment create` and `az group deployment create`.

## What should I do with validation issues when deploying to an existing app service plan?


## What should I fill the parameters in the create app service step?


## Additional information

### ARM

A consolidation of the commands to better fit the current command design of Azure CLI: az {command group} {?sub-command-group} {operation} {parameters}.

* `az resource`: Improve the examples of the resource module
* `az policy assignment list`: Support listing policy assignments at Management Group scope
* Add `az deployment group` and `az deploymnent operation group` for template deployment at resource groups. This is a duplicate of `az group deployment` and `az group deployment operation`. 
* Add `az deployment sub` and `az deployment operation sub` for template deployment at subscription scope. This is a duplicate of `az deployment` and `az deployment operation`.
* Add `az deployment mg` and `az deployment operation mg` for template deployment at management groups 
* Add `ad deployment tenant` and `az deployment operation tenant` for template deployment at tenant scope 
* az policy assignment create: Add a description to the `--location` parameter
* `az group deployment create`: Add parameter `--aux-tenants` to support cross tenants. 
---
layout: page
title: Setting up continuous integration
permalink: /en-us/azure-bot-service/manage/setting-up-continuous-integration/
weight: 13010
parent1: Azure Bot Service
parent2: Manage
---

If Azure's code editor does not meet your development needs, you can set up continuous integration using your favorite IDE. Follow these easy steps to get started.

1. [Create an empty repository in your favorite source control system](#createrepository)
2. [Download the bot code](#downloadbotcode)
3. [Choose the deployment source and connect your repository](#connectrepository)

If, after setting up continuous integration, you need to disconnect your deployment source from your bot, see [Disconnecting your deployment source](#disconnectintegration). 

<div class="docs-text-note"><strong>Note:</strong> When you setup continuous integration, the Azure's bot editor will be disabled. To re-enable it, you will need to disconnect your deployment source (see <a href="#disconnectintegration">Disconnecting your deployment source</a>).</div>

<div class="docs-text-note"><strong>Note:</strong> This document highlights the specific continuous integration features of Azure Bot Service. To get information about continuous integration as it relates to Azure App Services, see <a href="https://azure.microsoft.com/en-us/documentation/articles/app-service-continuous-deployment/" target="_blank">Continuous Deployment to Azure App Service</a>.</div>

<a id="createrepository" />

### Create an empty repository in your favorite source control system

The first step is to create an empty repository. At the time of this writing, Azure supports the following source control systems: 

[![Source control system](/en-us/images/azure-bots/continuous-integration-sourcecontrolsystem.png)](/en-us/images/azure-bots/continuous-integration-sourcecontrolsystem.png)

<div class="imagecaption"><span>Azure deployment sources</span></div>

<a id="downloadbotcode" />

### Download the bot code

1. Download the bot code zip file from the **Settings** tab of your Azure bot.

    [![Download the bot zip file](/en-us/images/azure-bots/continuous-integration-download.png)](/en-us/images/azure-bots/continuous-integration-download.png)

    <div class="imagecaption"><span>Downloading your bot code</span></div>

2. Unzip the bot code file to the local folder where you are planning to sync your deployment source.

<a id="connectrepository" />

### Choose the deployment source and connect your repository

1. Click the **Settings** tab within your Azure bot, and expand the **Continuous integration** section.
2. Click **Set up integration source**.

    [![Setup integration source](/en-us/images/azure-bots/continuous-integration-setupclick.png)](/en-us/images/azure-bots/continuous-integration-setupclick.png)

    <div class="imagecaption"><span>Accessing the continuous deployment Azure blade</span></div>

3. Click **Setup**, select your deployment source, and follow the steps to connect it. Make sure you select the repository type that you created in step 1.

    [![Setup integration source](/en-us/images/azure-bots/continuous-integration-sources.png)](/en-us/images/azure-bots/continuous-integration-sources.png)

    <div class="imagecaption"><span>Select your favorite deployment source</span></div>


<a id="disconnectintegration" />

### Disconnecting your deployment source

If for any reason you need to disconnect your deployment source from your bot, simply open the **Settings** tab, expand the **Continuous integration** section, click **Set up integration source**, and finally click **Disconnect** in the resulting blade.

[![Disconnect your deployment source](/en-us/images/azure-bots/continuous-integration-disconnect.png)](/en-us/images/azure-bots/continuous-integration-disconnect.png)

<div class="imagecaption"><span>Disconnecting your deployment source</span></div>

### Next steps

* Learn how to [debug your local code](/en-us/azure-bot-service/manage/debug/).
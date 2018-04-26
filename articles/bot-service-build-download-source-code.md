---
title: Download the source code for a Bot Service | Microsoft Docs
description: Learn how to download and publish a Bot Service.
author: v-ducvo
ms.author: v-ducvo
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 03/08/2018
---

# Download Bot Service source code

Bot Service allows you to download the entire source project for your bot. This allows you to work on your bot locally using an IDE of your choice. Once you are done making changes, you can publish your changes back to Azure. 

This topic will show you how to download your bot's source code and publishing the changes back to Azure. 

## Download bot source code

To develop your bot locally, do the following:

1. In the Azure portal and open the blade for the bot.
2. Under the **BOT MANAGEMENT** section, Click **Build**.
3. Click **Download zip file**. 

 ![Download source code](~/media/azure-bot-build/download-zip-file.png)

4. Extract the .zip file to a local directory.
5. Navigate to the extracted folder and open the source files in your favorite IDE.
6. Make changes to your sources. Either edit existing source files or add new ones to your project.

When you are ready, you can publish the sources back to Azure.

## Publish Node bot source code to Azure

To publish the sources back to Azure, run the following NPM command:

```console
npm run azure-publish
```

## Publish C# bot source code to Azure

Publishing C# code to Azure using Visual Studio is a two step process: First, you will need to configure publishing settings. Then, you can publish your changes.

To configure publishing from Visual Studio, do the following:

1. In Visual Studio, click **Solution Explorer**.
2. Right-click your project name and click **Publish...**. The **Publish** window opens.
3. Click **Create new profile**, click **Import profile**, and click **OK**.
4. Navigate to your project folder then navigate to the **PostDeployScripts** folder, and select the file that ends in **.PublishSettings**. Click **Open**.

Your project is now configured to publish changes to Azure.

Once your project is configured, you can publish your bot source code back to Azure by doing the following:

1. In Visual Studio, click **Solution Explorer**.
2. Right-click your project name and click **Publish...**.
3. Click the **Publish** button to publish your changes to Azure.

## Next steps
Now that you know how to build your bot locally, you can setup continuous deployment for your bot.

> [!div class="nextstepaction"]
> [Set up continuous deployment](bot-service-build-continuous-deployment.md)

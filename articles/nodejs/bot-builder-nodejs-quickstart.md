---
title: 用Node.js的Bot Builder SDK創建一個機器人 | Microsoft Docs
description: 使用Bot Builder SDK為Node.js創建一個機器人，這是一個強大的機器人構造框架。
author: 
ms.author: 
manager: 
ms.topic: get-started-article
ms.prod: bot-framework
ms.date: 
ms.reviewer:
---

# 用Node.js的Bot Builder SDK創建一個機器人
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-quickstart.md)
> - [Node.js](../nodejs/bot-builder-nodejs-quickstart.md)
> - [Azure Bot Service](../azure/azure-bot-service-quickstart.md)
> - [REST](../rest-api/bot-framework-rest-connector-quickstart.md)

Node.js的Bot Builder SDK是開發機器人的框架。它很容易使用其他框架，如Express＆restify，為JavaScript開發人員編寫機器人提供了一種熟悉的方式。

本教程將引導您使用Node.js的Bot Builder SDK構建機器人。您可以在終端機和Bot框架模擬器中測試機器人。

## 前置條件
完成以下前提再開始：

1. 安裝 [Node.js](https://nodejs.org)
2. 為你的Bot建立資料夾
3. 從終端機進入到剛創建的文件夾。
4. 執行下列 **npm** 指令：

```nodejs
npm init
```

按照屏幕上的提示輸入有關您的機器人的信息，npm將創建一個包含您提供的信息的 **package.json** 文件。

## 安裝SDK
接下來，通過運行以下 **npm** 命令為Node.js安裝Bot Builder SDK：

```nodejs
npm install --save botbuilder
```

一旦你安裝了SDK，你就可以寫下你的第一個機器人了。

對於您的第一個機器人，您將創建一個簡單的回覆任何用戶輸入的機器人。要創建機器人，請按照下列步驟操作：

1. 在您為您的機器人創建的文件夾中，創建一個名為 **app.js** 的新文件。
2. 在您選擇的文本編輯器或IDE中打開 **app.js**。將以下代碼添加到文件中：

   [!code-javascript[consolebot code sample Node.js](../includes/code/node-getstarted.js#consolebot)]

3. 保存文件。現在你準備好運行並測試你的機器人了。

### 開始你的 bot

在控制台窗口中導航到您的機器人目錄並啟動您的機器人：

```nodejs
node app.js
```

你的機器人現在在本地運行。通過在控制台窗口中輸入幾條消息來嘗試您的機器人。
您應該看到機器人回覆您發送的每個消息，回覆您的消息前綴為文本*“您說：”*。

## 安裝 Restify

本地機器人是良好的基於文本的客戶端，但為了使用任何Bot框架通道（或在模擬器中運行您的機器人），您的機器人將需要在API端點上運行。安裝<a href="http://restify.com/" target="_blank">restify</a> 透過 **npm** 指令:

```nodejs
npm install --save restify
```

一旦您進行了Restify，您就可以對機器人進行一些更改。

## 編輯你的 bot

您將需要對 **app.js** 文件進行一些更改。

1. 添加一行程式碼導入 `restify` 模組
2. 將 `ConsoleConnector` 改成 `ChatConnector`
3. 加入你的 Microsoft App ID 和金鑰
4. 連接器在API端點上進行偵聽

   [!code-javascript[echobot code sample Node.js](../includes/code/node-getstarted.js#echobot)]

3. 保存文件。現在你已經準備好在模擬器中運行並測試你的機器人了。

> [!NOTE] 
> 您不需要一個 **Microsoft應用程序ID** 或 **Microsoft應用程序密碼** 在Bot Framework Emulator中運行您的機器人。

請注意，Bot Framework Emulator中運行您的機器人時，不需要Microsoft應用程序ID或應用程序密碼。

## 測試你的機器人

接下來，使用[Bot Framework Emulator]（../ debug-bots-emulator.md）來測試你的機器人。模擬器是一個桌面應用程序，可讓您在本地主機上測試機器人，或通過隧道遠程運行。

首先，你需要[下載](https://emulator.botframework.com)並安裝模擬器。下載完成後，啟動可執行文件並完成安裝過程。

### 開始你的機器人

安裝模擬器後，在控制台窗口中導航到您的機器人目錄並啟動您的機器人：

```nodejs
node app.js
```
   
你的機器人現在在本地運行。

### 啟動模擬器並連接您的機器人

開始之後，連接到你的機器人在模擬器：

1. 在網址列輸入 `http://localhost:3978/api/messages`。（這是您的bot在本地託管時監聽的默認端點。）
2. 點擊 **連接**。您不需要指定 **Microsoft應用程序ID** 和 **Microsoft應用程序密碼**。現在可以將這些字段留空。在[註冊您的機器人](../portal-register-bot.md)時，您會收到輸入ID、金鑰的信息。

### 試試你的機器人

現在，您的機器人在本地運行，並連接到模擬器，嘗試在模擬器中鍵入一些消息。
您應該看到機器人響應您發送的每個消息，回覆您的消息前綴為文本 *“您說：”*。

您已經使用Node.js的Bot Builder SDK成功創建了第一個機器人！

## 下一步

> [!div class="nextstepaction"]
> [Node.js的Bot Builder SDK](bot-builder-nodejs-overview.md)

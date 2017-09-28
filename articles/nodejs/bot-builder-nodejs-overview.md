---
title: Node.js的Bot Builder SDK | Microsoft Docs
description: 探索Node.js的Bot Builder SDK，這是一個功能強大，易於使用的bot框架。
author: 
ms.author: 
manager: 
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer: 
---

# Node.js的Bot Builder SDK

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-overview.md)
> - [Node.js](../nodejs/bot-builder-nodejs-overview.md)
> - [REST](../rest-api/bot-framework-rest-overview.md)

Node.js的Bot Builder SDK是一個功能強大，易於使用的框架，為Node.js開發人員編寫機器人提供了一種熟悉的方法。
您可以使用它來構建各種會話用戶界面，從簡單的提示到自由格式的對話。

您的機器人的會話邏輯會作為網頁服務託管。Bot Builder SDK使用 <a href="http://restify.com">restify</a>，構建Web服務的流行框架，創建bot的Web服務器。
這個SDK也兼容 <a href="http://expressjs.com/">Express</a>，並且可以使用其他Web應用程序框架，是有可能。

使用SDK，您可以利用以下SDK功能： 

- 強大的系統建立對話封裝會話邏輯。
- 內置提示，例如是/否，字符串，數字和枚舉等簡單的內容，以及支持包含圖像和附件的消息以及包含按鈕的rich card。
- 內置支持強大的AI框架，如 <a href="http://luis.ai" target="_blank">LUIS</a>.
- 內置識別器和事件處理程序，用於指導用戶通過對話，根據需要提供幫助、導航、認證。

## 開始使用

如果您是新手編寫機器人，[使用Node.js創建您的第一個機器人](bot-builder-nodejs-quickstart.md)具有步驟說明，可以用來幫助您設置項目，安裝SDK並運行 你的第一個機器人。

如果您是Node.js的Bot Builder SDK的新手，您可以從幫助您了解Bot Builder SDK的主要組件的關鍵概念開始，參見[主要概念](bot-builder-nodejs-concepts.md)。

為了確保您的機器人流程適當，請查看[設計原則](../bot-design-principles.md)和[探索模式](../bot-design-pattern-task-automation.md)中的指南。

## 取得樣本

用於Node.js示例的[Bot Builder SDK](bot-builder-nodejs-samples.md)演示了以任務為中心的機器人，展示如何利用Bot Builder SDK中Node.js的功能。您可以使用示例來幫助您快速開始構建具有豐富功能的機器人。

## 下一步
> [!div class="nextstepaction"]
> [關鍵概念](bot-builder-nodejs-concepts.md)

## 額外的資源

以下以任務為中心的how-to指南演示了Node.js的Bot Builder SDK的各種功能。

* [回覆消息](bot-builder-nodejs-use-default-message-handler.md)
* [處理用戶操作](bot-builder-nodejs-dialog-actions.md)
* [識別用戶意圖](bot-builder-nodejs-recognize-intent-messages.md)
* [發一張 rich card](bot-builder-nodejs-send-rich-cards.md)
* [發送附件](bot-builder-nodejs-send-receive-attachments.md)
* [保存用戶數據](bot-builder-nodejs-save-user-data.md)


如果您遇到問題或有關於Node.js的Bot Builder SDK的建議，可以參閱[支援](../resources-support.md)中的資源。 


[DesignGuide]: ../bot-design-principles.md 
[DesignPatterns]: ../bot-design-pattern-task-automation.md 
[HowTo]: bot-builder-nodejs-use-default-message-handler.md 

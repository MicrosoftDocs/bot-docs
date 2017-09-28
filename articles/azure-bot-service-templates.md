---
title: Azure Bot Service 的模板 | Microsoft Docs
description: 學習如何使用 Azure Bot Service 的模板。
author: 
ms.author: 
manager: 
ms.topic: 
ms.prod: bot-framework
ms.date: 
ms.reviewer: 
---

# Azure Bot Service 的模板

Azure Bot Service 使您能夠通過使用五個模板之一快速輕鬆地在C＃或Node.js中創建消費計劃或應用程序服務計劃 bot。

[!include[Azure Bot Service 的模板](~/includes/snippet-abs-templates.md)] 

使用 Azure Bot Service 創建的所有機器人最初都將包含特定於開發語言和所選託管計劃的一組核心文件，以及包含特定於所選模板的代碼的其他文件。

## 在 App Service 計劃中創建的機器人文件

應用服務計劃中創建的機器人基於標準Web應用程序。 以下是您在壓縮文件中找到的一些重要文件的列表（除了特定於所選模板的文件）。

### 共同文件(both C# 和 Node.js)

| 資料夾 | 文件 | 說明 |
|----|----|----|
| / | **readme.md** | 此文件包含有關使用 Azure Bot Service 構建機器人的信息。 在使用或修改機器人之前，請先閱讀此文件。 |
| /PostDeployScripts | **&ast;.&ast;** | 執行一些後期部署任務所需的文件。不要修改這些文件。 |

### C# 專屬文件
| Folder | File | Description |
|----|----|----|
| / | **&ast;.sln** | Microsoft Visual Studio解決方案文件。 如果[設置連續部署](azure-bot-service-continuous-deployment.md)，則在本地使用。 |
| / | **build.cmd** | 當您通過 Azure App Service 編輯器在線編輯程式時，需要此文件。如果您在本地工作，則不需要此文件。 |
| /Dialogs | **&ast;.cs** | 定義你的機器人對話框的類別。 |
| /Controllers | **MessagesController.cs** | 您的機器人應用程序的主控制器。 |
| /PostDeployScripts | **&ast;.PublishSettings** | 您的機器人的發布配置文件。 您可以使用此文件直接從  Visual Studio發布。 |

### Node.js 專屬文件

| 資料夾 | 文件 | 說明 |
|----|----|----|
| / | **app.js** | 你的機器人的主要 .js文件。 |
| / | **package.json** | 此文件包含項目的npm引用。您可以修改此文件以添加新的引用。 |

## 在消費計劃中創建的機器人文件

消費計劃中的機器人基於Azure功能。以下是您在壓縮文件中找到的一些重要文件的列表（除了特定於所選模板的文件）。

### 共同文件(both C# 和 Node.js)

| 資料夾 | 文件 | 說明 |
|----|----|----|
| / | **readme.md** | 此文件包含有關使用Azure Bot服務構建機器人的信息。在使用或修改機器人之前，請先閱讀此文件。 |
| /messages | **function.json** | 此文件包含函數的綁定。不要修改此文件。 |
| /messages | **host.json** | 此元數據文件包含影響該功能的全局配置選項。 |
| /PostDeployScripts | **&ast;.&ast;** | 執行一些後期部署任務所需的文件。不要修改這些文件。 |

### C# 專屬文件
| 資料夾 | 文件 | 說明 |
|----|----|----|
| / | **Bot.sln** | Microsoft Visual Studio解決方案文件。 如果[設置連續部署](azure-bot-service-continuous-deployment.md)，則在本地使用。 |
| / | **commands.json** | 當您打開 **Bot.sln** 文件時，此文件包含在任務運行器資源管理器中啟動 **debughost** 的命令。如果不安裝Task Runner Explorer，可以刪除該文件。 |
  | / | **debughost.cmd** | 該文件包含加載和運行機器人的命令。如果[設置連續部署](azure-bot-service-continuous-deployment.md)並在本地調試您的漫遊器，則會在本地使用。詳細信息請參閱[使用 Windows 上的 Azure Bot Service 測試 C＃bot](azure-bot-service-debug-bot.md#debug-csharp-serverless)。此文件還包含您的機器人的應用程序ID和金鑰。要調試身份驗證，請在此文件中設置App ID和密碼，並在使用[emulator](debug-bots-emulator.md)測試您的bot時指定App ID和密碼。 |
| /messages | **project.json** | 該文件包含項目的NuGet引用。您可以修改此文件以添加新的引用。 |
| /messages | **project.lock.json** | 此文件自動生成。不要修改此文件。 |
| /messages | **run.csx** | 定義在傳入請求中執行的初始Run方法。 |

### Node.js 專屬文件

| 資料夾 | 文件 | 說明 |
|----|----|----|
| /messages | **index.js** | 你的機器人的主要 .js文件。 |
| /messages | **package.json** | 此文件包含項目的npm引用。您可以修改此文件以添加新的引用。 |

## 額外資源

- [使用基本模板創建機器人](azure-bot-service-serverless-template-basic.md)
- [使用表單模板創建機器人](azure-bot-service-serverless-template-form.md)
- [使用語言理解模板創建機器人](azure-bot-service-template-language-understanding.md)
- [使用主動模板創建機器人](azure-bot-service-template-proactive.md)
- [使用問答模板創建機器人](azure-bot-service-template-question-answer.md)

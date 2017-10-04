---
title: Azure Bot Service | Microsoft Docs
description: 認識 Azure Bot Service.
author: 
ms.author: 
manager: 
ms.topic: article
ms.prod: bot-framework
ms.date: 
ms.reviewer: 
---

# Azure Bot Service

Azure Bot Service加速了開發聊天機器人的過程。 它提供了一個網頁主機，可以在開發環境中五個模板選一個修改修改。

[!include[Azure Bot服務託管計劃](~/includes/snippet-abs-hosting-plans.md)] 

## 前置條件

您需要有 Microsoft Azure 訂閱，然後才能使用 Azure Bot Service。 如果您還沒有，要先註冊一個 <a href="https://azure.microsoft.com/en-us/free/" target="_blank">免費試用</a>.

## 在幾秒內建立一個 Bot

Azure Bot 服務使您能夠使用五個模板之一快速輕鬆地在C＃或Node.js中創建聊天機器人。

[!include[Azure Bot Service 模板](~/includes/snippet-abs-templates.md)] 

獲得更多模板相關資訊，可以參閱 [Azure Bot Service 的模板](azure-bot-service-templates.md). 
有關如何使用 Azure Bot Service 快速構建和測試簡單機器人的教程，請參閱[使用Azure Bot服務創建機器人](azure-bot-service-quickstart.md).

## 選擇開發工具

預設情況下，Azure Bot Service 使您可以使用 Azure 編輯器在瀏覽器中直接開發您的 bot，而無需任何額外（即本地編輯器和程式碼控制）。
整合的聊天視窗與 Azure 編輯器並排，可以讓您在瀏覽器中編寫程式時即時測試您的機器人。

雖然使用 Azure 編輯器不需要在本地計算機上編輯器和源代碼控制，Azure 編輯器不允許您管理文件（例如，添加文件，重命名文件或刪除文件）。如果您有Visual Studio Community 或以上版本，您可以在本地開發和測試您的 C＃bot，並將您的機器人發佈到Azure。此外，您可以使用您選擇的版本控制系統，通過 Visual Studio Online 和 GitHub 的簡單設置來[設置持續部署](azure-bot-service-continuous-deployment.md)。通過持續部署，您可以在本地 IDE 中開發和測試，並且您提交到源代碼控制的任何代碼更改都將自動部署到 Azure。

> [!NOTE]
> 啟用持續部署後，請務必僅通過持續部署來修改代碼，而不是通過其他機制來避免衝突。

## 管理你的機器人

在使用Azure Bot Service創建機器人的過程中，您可以為您的機器人指定一個名稱，並生成其應用程序ID和密碼。創建機器人後，您可以更改其設置，將其配置為在一個或多個平台上運行，或將其發佈到一個或多個平台。

## 下一步

> [!div class="nextstepaction"]
> [用 Azure Bot  服務創建一個機器人](azure-bot-service-quickstart.md)


## 額外的資源

如果您遇到問題或有關Azure Bot服務的建議，參閱[支援](resources-support.md)來獲取可用資源列表。

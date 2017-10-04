---
title: 透過 Azure Bot Service 構建選項卡功能 | Microsoft Docs
description: 在線代碼編輯器，源代碼下載和持續部署是Azure Bot Service的構建選項卡功能。 
author: 
ms.author: 
manager: 
ms.topic: article
ms.prod: bot-framework
ms.date: 
---
# 構建Azure Bot服務的選項卡功能

使用App Service的Azure Bot Service機器人可以在其 **Build** 選項卡上包含這些功能。

> [!NOTE]
> 使用消費計劃的Azure Bot Service機器人的 **Build** 選項卡僅提供基本的基於瀏覽器的代碼編輯器。
> 您可以下載源代碼，並使用位於的功能為消費計劃機器人配置持續部署 
> 在其 **設置** 選項卡的 **連續部署** 部分。

## 線上編輯器

在線代碼編輯器可以通過[Azure App Service Editor](https://github.com/projectkudu/kudu/wiki/App-Service-Editor)來更改機器人的源代碼。編輯器在C＃源文件中提供IntelliSense。

在線上編輯器是一個很好的開始，但如果您需要自行測試和文件管理，請通過下載來獲取程式碼，並可選擇通過設置持續部署。

### 從在線上編輯器部署

在在線代碼編輯器中更改源文件時，必須在更改生效之前運行部署腳本。按照以下步驟運行部署腳本。

 1. 在應用服務編輯器中，單擊打開控制台圖標。  
    ![Console Icon](./media/azure-bot-service-console-icon.png)

 2. 在控制台窗口中，輸入 **build.cmd** ，然後按確定鍵。

## 下載程式碼

您可以下載包含所有文件的壓縮文件、Visual Studio解決方案文件和允許從Visual Studio發布的配置文件。使用這些文件，您可以在Visual Studio或您最喜歡的IDE中修改和測試您的機器人，並使用Bot Framework模擬器進行測試。當你要發布時，您可以通過導入保存在PostDeployScripts文件夾中的`.PublishSettings`文件中的發布配置文件直接從Visual Studio進行發布。更多資訊請參閱 [從Visual Studio發布C＃bot到app service](azure-bot-service-continuous-deployment.md).

## 從源代碼管理持續部署

只要您檢查源代碼管理服務中的代碼更改，持續部署就可以重新發佈到Azure。如果您在團隊中工作，需要在源代碼管理系統中共享代碼，那麼您應該設置連續部署，並使用您選擇的集成開發環境（IDE）和源代碼控制系統。

> [!NOTE]
> Azure Bot Service提供的一些模板，特別是消費計劃的模板，
> 需要額外的設置步驟來[在本地計算機上進行調試](azure-bot-service-debug-bot.md). 

Azure Bot服務提供了一種快速方法，可以通過提供在這些網站上發給您的訪問金鑰來設置Visual Studio Online和GitHub的連續部署。對於其他源控制系統，選擇 **other** 並按照出現的步驟。有關在Visual Studio Online或Github以外的源代碼控制上設置連續部署的更多幫助，請參閱[設置連續部署](azure-bot-service-continuous-deployment.md)。

> [!WARNING]
> 當您使用持續部署時，請確保僅通過將其檢入源控制服務來修改代碼。
> 啟用持續部署時，不要使用在線代碼編輯器來更改源代碼。
> 對於消費計劃機器人，在啟用連續部署時，在線代碼編輯器是唯獨的。

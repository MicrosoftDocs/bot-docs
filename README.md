# Microsoft 開放原始碼管理辦法

此專案採用 [Microsoft 開放原始碼管理辦法](https://opensource.microsoft.com/codeofconduct/)。
如需詳細資訊，請參閱[管理辦法常見問題集](https://opensource.microsoft.com/codeofconduct/faq/)，如有任何其他問題或意見請連絡 [opencode@microsoft.com](mailto:opencode@microsoft.com)。


# Bot Framework 技術文件貢獻指南
您已經找到在 GitHub 上的存儲庫，其中包含發佈在[https://docs.microsoft.com/bot-framework](https://docs.microsoft.com/bot-framework)的 Bot Framework技術文檔的源始文件。

此存儲庫還包含指導文件，協助您幫助我們的技術文檔。 有關貢獻者指南中的文章列表，請參閱[目錄](contributor-guide/contributor-guide-index.md).

## 參與編輯 Bot Framework 技術文件
感謝您對Bot Framework文檔的關注！

* [編輯方法](#ways-to-contribute)
* [Code of conduct](#code-of-conduct)
* [關於您對 Bot Framework 內容的參與](#about-your-contributions-to-Bot-Framework-content)
* [儲存機制組織方式](#repository-organization)
* [使用GitHub、Git，或其他儲存庫](#use-github-git-and-this-repository)
* [如何在你的主題中使用 markdown](#how-to-use-markdown-to-format-your-topic)
* [更多資源](#more-resources)
* [所有貢獻者指南的目錄](contributor-guide/contributor-guide-index.md) (opens new page)

## <a name="ways-to-contribute"></a>編輯方法
您可以提交 [Bot Framework](https://docs.microsoft.com/bot-framework) 文檔的更新如下：

* 您可以輕鬆地在 GitHub 用戶界面中的技術文章做出貢獻。 包括在此儲存庫的文章， 或者是瀏覽在[https://docs.microsoft.com/bot-framework](https://docs.microsoft.com/bot-framework) 後，點擊文章上的連結前往該文章在 GitHub 上的文件。
* 如果您對現有文章內文更改、添加或更改圖像或貢獻新文章，則需要 fork 此存儲庫，安裝 Git Bash ， Markdown Pad 並學習一些git命令。

## <a name="about-your-contributions-to-Bot-Framework-content"></a>關於您對 Bot Framework 內容的參與
### 小幅度修正
您在此儲存機制中針對文件和程式碼範例所提交的小幅度修正或釐清，將受到 [docs.microsoft.com 使用規定](https://docs.microsoft.com/legal/termsofuse)的約束。

### 較大範圍的提交內容
如果您提交提取要求是為了對文件和程式碼範例進行全新或大幅度的變更，而且您不是 Microsoft 的員工，我們會在 GitHub 中傳送註解，要求您提交線上版的參與授權合約 (Contribution License Agreement, CLA)： 您必須先填寫線上表單，我們才會接受您的提取要求。

## <a name="repository-organization"></a>儲存機制組織方式
Bot Framework-docs 存放庫中的內容會遵循 https://docs.microsoft.com/bot-framework 上文件的組織方式。  此儲存機制包含兩個根資料夾：

### \articles
*\articles* 資料夾包含格式化為 Markdown 檔的文件文章，其副檔名為 *.md*。 文章通常會依 Bot Framework 服務分入群組。

文章需要遵循文件的命名指南 - 詳細情形, 請見 [our file naming guidance](contributor-guide/file-names-and-locations.md).

*\articles* 資料夾內有 *\media* 資料夾可供存放根目錄文章媒體檔，而在 \media 資料夾內則是存有各文章內圖片的子資料夾。  每個服務資料夾內都含有不同的媒體資料夾來存放文章。 文章圖片資料夾的名稱和文章檔案的名稱相同，但不會有 *.md* 副檔名。

### \includes
您也可以建立特定的內容區段，以便重複地用來加入一個或多個文章之中。  請見 [Custom extensions used in our technical content](contributor-guide/custom-markdown-extensions.md).

### \markdown templates
此文件夾包含我們的 markdown 模板，其中包含文章所需的基本標記格式。

### \contributor-guide
該文件夾包含作為我們貢獻者指南的一部分的文章。

## <a name="use-github-git-and-this-repository"></a>使用GitHub、Git，或其他儲存庫
有關如何貢獻的信息。如何使用GitHub UI提供小幅度的更改，以及如何 fork 和 clone 存儲庫以獲得更重要的貢獻，請參閱[安裝和設置在 GitHub 編輯的工具](contributor-guide/tools-and-setup.md).

如果您安裝 GitBash 並選擇在本地工作，則創建新的本地工作分支，進行更改後並將更改提交回主分支的步驟列在 [創建新文章或更新現有文章的 Git 命令](contributor-guide/git-commands-for-master.md)

### 分支
我們建議您創建針對特定變更範圍的本地工作分支。 每個分支應該被限制在一個概念/文章中，以簡化工作流程並減少合併衝突的可能性。 以下努力適用於新分支的適用範圍：

* 新文章（和相關圖片）
* 一篇文章的拼寫和文法編輯。
* 在大量文章（例如:copyright footer）上應用相同格式更改。

## <a name="how-to-use-markdown-to-format-your-topic"></a>如何在你的主題中使用 markdown
在此存儲庫中的所有文章都使用 GitHub 樣式的 markdown。 以下是資源列表。

* [Markdown basics](https://help.github.com/articles/markdown-basics/)
* [Printable markdown cheatsheet](./contributor-guide/media/documents/markdown-cheatsheet.pdf?raw=true)

## 文章元資料
文章元資料允許某些功能，例如作者歸屬，貢獻者歸屬，麵包屑，文章描述和SEO優化，以及Microsoft用於評估內容性能的報告。 所以元資料是很重要的！ [以下是確保您的元資料正確完成的指南](contributor-guide/article-metadata.md).

### 標籤
分配自動標籤以提取請求，以幫助我們管理拉取請求工作流程，並幫助您了解您的拉取請求發生了什麼：

* 貢獻協議相關
  * cla-not-required: 這種變化相對較小，不要求您簽署 CLA 。
  * cla-required: 變更的範圍比較大，要求您簽署 CLA 。
  * cla-signed: 貢獻者簽署了 CLA ，所以拉動請求現在可以向前進行審查。
* 支柱標籤: 諸如PnP，Modern Apps和TDC之類的標籤可以幫助內部組織對需要查看拉動請求的拉動請求進行分類。
* 更改發送給作者: 作者已被通知有待處理的請求。

## <a name="more-resources"></a>更多資源
詳閱我們所有的 [貢獻者指南](contributor-guide/contributor-guide-index.md)。

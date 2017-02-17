---
title: Bot Framework Getting Started | Microsoft Docs
description: Overview of the Bot Framework and its capabilities.
services: service-name
documentationcenter: BotFramework-Docs
author: RobStand
manager: manager-alias

ms.service: required
ms.devlang: may be required
ms.topic: article
ms.tgt_pltfrm: may be required
ms.workload: required
ms.date: mm/dd/yyyy
ms.author: rstand@microsoft.com

---
# Get started with Bot Builder and Node.js
> [!div class="op_single_selector"]
> * [C#](bot-framework-dotnet-getstarted.md)
> * [Node.js](bot-framework-nodejs-getstarted.md)
> * [Azure Bot Service](bot-framework-azure-getstarted.md)
> * [REST](bot-framework-rest-getstarted.md)
>

## Node.js
> [!NOTE]
> Content coming soon
> [!div class="tabbedCodeSnippets"]
```cs
  var outlookClient = await CreateOutlookClientAsync("Calendar");
  var events = await outlookClient.Me.Events
    .Take(10)
    .ExecuteAsync();
  foreach(var calendarEvent in events.CurrentPage)
  {
    System.Diagnostics.Debug.WriteLine("Event '{0}'.", calendarEvent.Subject);
  }
```
```javascript
  outlookClient.me.events.getEvents().fetch().then(function (result) {
      result.currentPage.forEach(function (event) {
  console.log('Event "' + event.subject + '"')
      });
  }, function(error) {
      console.log(error);
  });
```

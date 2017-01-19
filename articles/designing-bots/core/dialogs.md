---
title: Designing Bots - Dialogs | Bot Framework
description: Dialogs - Fundamental concepts of working with dialogs in the Microsoft Bot Framework
services: Bot Framework
documentationcenter: BotFramework-Docs
author: matvelloso
manager: larar

ms.service: required
ms.devlang: may be required
ms.topic: article
ms.tgt_pltfrm: may be required
ms.workload: required
ms.date: 01/19/2017
ms.author: mat.velloso@microsoft.com

---
# Bot Design Center - Dialogs



##UI: Apps have them, so do bots 

In a traditional application, UI is represented as screens. A single app can use one or more screens as needed in order to exchange data with the user. You would likely have a main screen where all navigation starts from and then different screens for, let’s say, browsing products, looking for help and so on. Same principles apply to websites.

Again, bots are no different. You will have a UI. It just may look… different. As your bot grows in complexity, you will need to separate concerns. The place where you help the user browse for products will be different than the place where the user creates a new order or browses for help. We break those things into what we call “dialogs”. 

Dialogs may or may not have graphical interfaces. They may have buttons, text or they may even be completely speech based. One dialog can call another, just like screens in apps.

![bot](../../media/designing-bots/core/dialogs-screens.png)





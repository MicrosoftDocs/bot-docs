---
title: Shared dialogs | Microsoft Docs
description: How to set up shared dialogs in Conversation Designer
author: vkannan
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/04/2017
ROBOTS: NoIndex, NoFollow
---

# Shared dialogs
> [!IMPORTANT]
> Conversation Designer is not available to all customers yet. More details on
> availability of Conversation Designer will come later this year.

Shared dialogs are reusable dialogs that you can share across multiple dialogs within your bot. For example, a Cafe bot could use the same order confirmation sub-flow in several different tasks. 

To setup dialogs so that they can be shared, add a new **Shared dialog** item from the left tree panel.

<!--TODO: Add a screenshot -->

A shared dialog's conversational flow is modeled like any other [dialog](conversation-designer-dialogs.md).

<!-- TODO: We should talk about how entities work for shared dialogs and how a shared dialog can/cant express entities it depends on/produces/mutates -->
 
## Next step
> [!div class="nextstepaction"]
> [Response templates](conversation-designer-response-templates.md)

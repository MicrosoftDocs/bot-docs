---
title: Define a reply as a Do action | Microsoft Docs
description: Learn how to define a reply as a Do action.
author: vkannan
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/04/2017
ROBOTS: NoIndex, NoFollow
---

# Define a reply as a Do action

A reply can be any combination of displayed text, spoken text, or rich card. With this action, the bot sends a reply to the user and completes the task. A reply is a single-turn conversation, meaning the task does not expect follow-up messages from the user. To set up a reply as the *reply* action, select **Give a reply** from **DO** action drop down. You can then enter simple responses into the **Bot's response to user** field.

You can optionally include an [adaptive card](conversation-designer-adaptive-cards.md) with the simple response. You can also optionally provide custom script function to execute before sending the response to the user. These options are available to you while you create or edit a task. 

## Next step
> [!div class="nextstepaction"]
> [Script action](conversation-designer-script-function.md)

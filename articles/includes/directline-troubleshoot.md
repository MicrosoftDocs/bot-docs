---
description: Troubleshoot Direct Line extension
author: kunsinghms
ms.author: kunsingh
manager: shellyha
ms.reviewer: pehecke
ms.topic: include
ms.custom:
  - evergreen
ms.update-cycle: 1095-days
---

- If the **ib** and **ob** values displayed by the **.bot endpoint** are false, the bot and the Direct Line App Service extension are unable to connect to each other.
    1. Double check the code for using named pipes has been added to the bot.
    1. Confirm the bot is able to start up and run. Useful tools are **Test in WebChat**, connecting an additional channel, remote debugging, or logging.
    1. Restart the entire **Azure App Service** the bot is hosted within, to ensure a clean start up of all processes.

- If the **initialized** value of the **.bot endpoint** is false, the Direct Line App Service extension is unable to validate the App Service extension key added to the bot's **Application Settings** above.
    1. Confirm the value was correctly entered.
    1. Switch to the alternate extension key shown on your bot's **Configure Direct Line** page.

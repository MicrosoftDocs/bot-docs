---
description: Confirm Direct Line App Service extension and the bot are configured
author: emgrol
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 03/30/2022
---

In your browser, go to `https://<your_app_service>.azurewebsites.net/.bot`. If everything is correct, the page will return the following JSON content:

```json
    {"v":"123","k":true,"ib":true,"ob":true,"initialized":true}`.
```

This is the information you obtain when everything works correctly, where:

- **v** displays the build version of the Direct Line App Service extension.
- **k** determines whether the extension can read an extension key from its configuration.
- **initialized** determines whether the extension can use the extension key to download the bot metadata from Azure Bot Service.
- **ib** determines whether the extension can establish an inbound connection with the bot.
- **ob** determines whether the extension can establish an outbound connection with the bot.

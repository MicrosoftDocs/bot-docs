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
    {"v":"123","k":true,"ib":true,"ob":true,"initialized":true}
```

- **v** shows the build version of the Direct Line App Service extension.
- **k** indicates whether the extension was able to read an extension key from its configuration.
- **initialized** indicates whether the extension was able to download bot metadata from Azure AI Bot Service.
- **ib** indicates whether the extension was able to establish an inbound connection to the bot.
- **ob** indicates whether the extension was able to establish an outbound connection from the bot.

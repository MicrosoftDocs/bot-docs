---
description: Enable bot Direct Line App Service extension
author: emgrol
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 03/30/2022
---

1. In the Azure portal, go to your **Azure Bot** resource.
    1. Under **Settings** select **Channels** to configure the channels your bot accepts messages from.
    1. If it isn't already enabled, select the **Direct Line** channel from the list of **Available channels** to enable the channel.
    1. After enabling **Direct Line**, select it again from the **Channels** page.
    1. Under **App Service extension Keys** select the **Show** button (eye icon) to reveal one of the keys. Copy this value for use later.
1. Go to the home page and select **App Services** at the top of the page. Alternatively, display the portal menu and then select the **App Services** menu item. Azure will display the **App Services** page.
1. In the search box, enter your **Azure Bot** resource name. Your resource will be listed.

    Notice that if you hover over the icon or the menu item, you get a list of your last viewed resources. Your **Azure Bot** resource will likely be listed.

1. Select your resource link.
    1. In the **Settings** section, select the **Configuration** menu item.
    1. In the right panel, add the following settings:

        |Name|Value|
        |---|---|
        |DirectLineExtensionKey|The value of the App Service extension key you copied earlier.|
        |DIRECTLINE_EXTENSION_VERSION|latest|

    1. If your bot's hosted in a sovereign or otherwise restricted Azure cloud, where you don't access Azure via the [public portal](https://portal.azure.com), you'll also need to add the following setting:

        |Name|Value|
        |---|---|
        |DirectLineExtensionABSEndpoint|The endpoint specific to the Azure cloud your bot is hosted in. For the USGov cloud for example, the endpoint is `https://directline.botframework.azure.us/v3/extension`.|

    1. From within the **Configuration** section, select the **General** settings section and turn on **Web sockets**.
    1. Select **Save** to save the settings. This restarts the Azure App Service.

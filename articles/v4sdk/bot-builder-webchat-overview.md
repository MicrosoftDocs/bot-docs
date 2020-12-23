---
title:  Web Chat overview - Bot Service
description: Become familiar with the Bot Framework Web Chat component. Learn how to use and customize this component. View available properties and other information.
keywords: bot framework, webchat, chat, samples, react, reference
author: mmiele
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/23/2020
---

# Web Chat overview

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

This article contains details of the [Bot Framework Web Chat](https://github.com/microsoft/BotFramework-WebChat) component. The Bot Framework Web Chat component is a highly customizable web-based client for the Bot Framework V4 SDK. The Bot Framework SDK v4 enables developers to model conversation and build sophisticated bot applications.

If you're looking to migrate from Web Chat v3 to v4, jump ahead to [the migration section](#migrating-from-web-chat-v3-to-v4).

## How to use

> [!NOTE]
> For previous versions of Web Chat (v3), visit the [Web Chat v3 branch](https://github.com/Microsoft/BotFramework-WebChat/tree/v3).

First, create a bot using [Azure Bot Service](https://azure.microsoft.com/services/bot-service/).
Once the bot is created, you will need to [obtain the bot's Web Chat secret](../bot-service-channel-connect-webchat.md#get-your-bot-secret-key) in Azure Portal. Then use the secret to [generate a token](../rest-api/bot-framework-rest-direct-line-3-0-authentication.md) and pass it to your Web Chat.

The following examples shows how to add a Web Chat control to a website.

```html
<!DOCTYPE html>
<html>
   <body>
      <div id="webchat" role="main"></div>
      <script src="https://cdn.botframework.com/botframework-webchat/latest/webchat.js"></script>
      <script>

         // Set style options.
         const styleOptions = {
            botAvatarInitials: 'BF',
            userAvatarInitials: 'WC'
         };

         window.WebChat.renderWebChat(
            {
               directLine: window.WebChat.createDirectLine({
                  token: 'YOUR_DIRECT_LINE_TOKEN'
               }),
               userID: 'YOUR_USER_ID',
               username: 'Web Chat User',
               locale: 'en-US',
               styleOptions
            },
            document.getElementById('webchat')
         );
      </script>
   </body>
</html>
```

> `userID`, `username`, `locale`, `botAvatarInitials`, and `userAvatarInitials` are all optional parameters to pass into the `renderWebChat` method.  For more information about style, see [Why styleOptions?](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/02.branding-styling-and-customization/a.branding-web-chat#why-stylesetoptions). To learn more about Web Chat properties, look at the [Web Chat API Reference](#web-chat-api-reference) section.


### Integrate with JavaScript

Web Chat is designed to integrate with your existing website using JavaScript or React. Integrating with JavaScript will give you some styling and customizability, for more information see the article [Integrate Web Chat into your website](https://aka.ms/integrate-webchat-into-site).

You can use the full, typical webchat package that contains the most typically used features.

```html
<!DOCTYPE html>
<html>
   <body>
      <div id="webchat" role="main"></div>
      <script src="https://cdn.botframework.com/botframework-webchat/latest/webchat.js"></script>
      <script>
         window.WebChat.renderWebChat(
            {
               directLine: window.WebChat.createDirectLine({
                  token: 'YOUR_DIRECT_LINE_TOKEN'
               }),
               userID: 'YOUR_USER_ID'
            },
            document.getElementById('webchat')
         );
      </script>
   </body>
</html>
```

See the working sample of the [full Web Chat bundle](https://github.com/Microsoft/BotFramework-WebChat/tree/master/samples/01.a.getting-started-full-bundle).

### Integrate with React

For full customizability, you can use [React](https://reactjs.org/) to recompose components of Web Chat.

To install the production build from npm, run `npm install botframework-webchat`.

```jsx
import { DirectLine } from 'botframework-directlinejs';
import React from 'react';
import ReactWebChat from 'botframework-webchat';

export default class extends React.Component {
  constructor(props) {
    super(props);

    this.directLine = new DirectLine({ token: 'YOUR_DIRECT_LINE_TOKEN' });
  }

  render() {
    return (
      <ReactWebChat directLine={ this.directLine } userID='YOUR_USER_ID' />
      element
    );
  }
}
```

> You can also run `npm install botframework-webchat@master` to install a development build that is synced with Web Chat's GitHub `master` branch.

See a working sample of [Web Chat rendered via React](https://github.com/Microsoft/BotFramework-WebChat/tree/master/samples/03.a.host-with-react/).

> [!TIP]
> If you are new to React and jsx you can find training on Reacts [Getting Started](https://reactjs.org/docs/getting-started.html) page.

## Customize Web Chat UI

Web Chat is designed to be customizable without forking the source code. The table below outlines what kind of customizations you can achieve when you are importing Web Chat in different ways. This list is not exhaustive.

| Customization                 | CDN bundle         | React              |
| ----------------------------- | ------------------ | ------------------ |
| Change colors                 | :heavy_check_mark: | :heavy_check_mark: |
| Change sizes                  | :heavy_check_mark: | :heavy_check_mark: |
| Update/replace CSS styles     | :heavy_check_mark: | :heavy_check_mark: |
| Listen to events              | :heavy_check_mark: | :heavy_check_mark: |
| Interact with hosting webpage | :heavy_check_mark: | :heavy_check_mark: |
| Custom render activities      |                    | :heavy_check_mark: |
| Custom render attachments     |                    | :heavy_check_mark: |
| Add new UI components         |                    | :heavy_check_mark: |
| Recompose the whole UI        |                    | :heavy_check_mark: |

See more about [customizing Web Chat](https://github.com/Microsoft/BotFramework-WebChat/blob/master/SAMPLES.md) to learn more on customization.

> [!NOTE]
> For information on Content Delivery Networks (CDNs) See [Content delivery networks (CDNs)](https://aka.ms/CDN-best-practices)

## Migrating from Web Chat v3 to v4

There are three possible paths that migration might take when migrating from v3 to v4. First, please compare your beginning scenario as described below.

For a list of related samples, see [Web Chat hosted samples](https://aka.ms/botframework-webchat-samples).


## Web Chat API Reference

There are several properties that you might pass into your Web Chat React Component (`<ReactWebChat>`) or the `renderWebChat()` method. For a short description of the available properties, see [Web Chat API Reference](https://github.com/microsoft/BotFramework-WebChat/blob/master/docs/API.md#web-chat-api-reference).
Also, feel free to examine the source code starting with [`packages/component/src/Composer.js`](https://github.com/Microsoft/BotFramework-WebChat/blob/master/packages/component/src/Composer.js#L378).

## Browser compatibility

Web Chat supports the latest 2 versions of modern browsers like Chrome, Edge, and FireFox.
If you need Web Chat in Internet Explorer 11, please see the [ES5 bundle](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/01.getting-started/c.es5-bundle) and [demo](https://microsoft.github.io/BotFramework-WebChat/01.getting-started/c.es5-bundle/).

Please note, however:
- Web Chat does not support Internet Explorer older than version 11
- Customization as shown in non-ES5 samples are not supported for Internet Explorer. Because IE11 is a non-modern browser, it does not support ES6, and many samples that use arrow functions and modern promises would need to be manually converted to ES5.  If you are in need of heavy customization for your app, we strongly recommend developing your app for a modern browser like Google Chrome or Edge.
- Web Chat has no plan to support samples for IE11 (ES5).
   - For customers who wish to manually rewrite our other samples to work in IE11, we recommend looking into converting code from ES6+ to ES5 using polyfills and transpilers like [`babel`](https://babeljs.io/docs/en/next/babel-standalone.html).

## How to test with Web Chat's latest bits

*Testing unreleased features is only available via MyGet packaging at this time.*

If you want to test a feature or bug fix that has not yet been released, you will want to point your Web Chat package to Web Chat's daily feed, as opposed the official npmjs feed.

Currently, you may access Web Chat's dailies by subscribing to our MyGet feed. To do this, you will need to update the registry in your project. **This change is reversible, and our directions include how to revert back to subscribing to the official release**.

### Subscribe to latest bits on `myget.org`

To do this you may add your packages and then change the registry of your project.

1. Add your project dependencies other than Web Chat.
1. In your project's root directory, create a `.npmrc` file
1. Add the following line to your file: `registry=https://botbuilder.myget.org/F/botframework-webchat/npm/`
1. Add Web Chat to your project dependencies `npm i botframework-webchat --save`
1. Note that in your `package-lock.json`, the registries pointed to are now MyGet. The Web Chat project has upstream source proxy enabled, which will redirect non-MyGet packages to `npmjs.com`.

### Re-subscribe to official release on `npmjs.com`
Re-subscribing requires that you reset your registry.

1. Delete your `.npmrc file`
1. Delete your root `package-lock.json`
1. Remove your `node_modules` directory
1. Reinstall your packages with `npm i`
1. Note that in your `package-lock.json`, the registries are pointing to https://npmjs.com/ again.


## Contributing

See our [Contributing page](https://github.com/Microsoft/BotFramework-WebChat/tree/master/.github/CONTRIBUTING.md) for details on how to build the project and our repository guidelines for Pull Requests.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/).
For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or
contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

## Reporting Security Issues

Security issues and bugs should be reported privately, via email, to the Microsoft Security Response Center (MSRC) at [secure@microsoft.com](mailto:secure@microsoft.com). You should receive a response within 24 hours. If for some reason you do not, please follow up via email to ensure we received your original message. Further information, including the [MSRC PGP](https://technet.microsoft.com/security/dn606155) key, can be found in the [Security TechCenter](https://technet.microsoft.com/security/default).

Copyright (c) Microsoft Corporation. All rights reserved.

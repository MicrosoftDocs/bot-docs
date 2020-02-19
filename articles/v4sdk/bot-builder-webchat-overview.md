---
title:  Web Chat overview - Bot Service
description: Learn how to configure Bot Framework Web Chat.
keywords: bot framework, webchat, chat, samples, react, reference
author: ivorb
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 06/07/2019
---

# Web Chat overview

This article contains details of the [Bot Framework Web Chat](https://github.com/microsoft/BotFramework-WebChat) component. The Bot Framework Web Chat component is a highly customizable web-based client for the Bot Framework V4 SDK. The Bot Framework SDK v4 enables developers to model conversation and build sophisticated bot applications.

If you're looking to migrate from Web Chat v3 to v4, jump ahead to [the migration section](#migrating-from-web-chat-v3-to-v4).

## How to use

> [!NOTE]
> For previous versions of Web Chat (v3), visit the [Web Chat v3 branch](https://github.com/Microsoft/BotFramework-WebChat/tree/v3).

First, create a bot using [Azure Bot Service](https://azure.microsoft.com/services/bot-service/).
Once the bot is created, you will need to [obtain the bot's Web Chat secret](../bot-service-channel-connect-webchat.md#get-your-bot-secret-key) in Azure Portal. Then use the secret to [generate a token](../rest-api/bot-framework-rest-direct-line-3-0-authentication.md) and pass it to your Web Chat.

Here is how how you can add a Web Chat control to your website:

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
               userID: 'YOUR_USER_ID',
               username: 'Web Chat User',
               locale: 'en-US',
               botAvatarInitials: 'WC',
               userAvatarInitials: 'WW'
            },
            document.getElementById('webchat')
         );
      </script>
   </body>
</html>
```

> `userID`, `username`, `locale`, `botAvatarInitials`, and `userAvatarInitials` are all optional parameters to pass into the `renderWebChat` method. To learn more about Web Chat properties, look at the [Web Chat API Reference](#web-chat-api-reference) section of this article.
> ![Screenshot of Web Chat](https://raw.githubusercontent.com/Microsoft/BotFramework-WebChat/master/media/weatherquery.png.jpg)

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

|                               | CDN bundle         | React              |
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

### My current website integrates Web Chat using an `<iframe>` element obtained from Azure Bot Services. I want to upgrade to v4.

Starting from May 2019, we are rolling out v4 to websites that integrate Web Chat using `<iframe>` element. Please refer to [the embed documentation](https://github.com/Microsoft/BotFramework-WebChat/tree/master/packages/embed) for information on integrating Web Chat using `<iframe>`.

### My website is integrated with Web Chat v3 and uses customization options provided by Web Chat, no customization at all, or very little of my own customization that was not available with Web Chat.

Please follow the implementation of sample [`00.migration/a.v3-to-v4`](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/00.migration/a.v3-to-v4) to convert your webpage from v3 to v4 of Web Chat.

### My website is integrated with a fork of Web Chat v3. I have implemented a lot of customization in my version of Web Chat, and I am concerned v4 is not compatible with my needs.

One of our team's favorite things about v4 of Web Chat is the ability to add customization **without the need to fork Web Chat**. Although this creates additional overhead for v3 users who forked Web Chat previously, we will do our best to support customers making the jump. Please use the following suggestions:

-  Take a look at the implementation of sample [`00.migration/a.v3-to-v4`](https://github.com/microsoft/BotFramework-WebChat/tree/master/samples/00.migration/a.v3-to-v4). This is a great starting place to get Web Chat up and running.
-  Next, please go through the [samples list](https://aka.ms/botframework-webchat-samples) to compare your customization requirements to what Web Chat has already provided support for. These samples are made up of commonly asked-for features for Web Chat.
-  If one or more of your features is not available in the samples, please look through our [open and closed issues](https://github.com/Microsoft/BotFramework-WebChat/issues?utf8=%E2%9C%93&q=is%3Aissue+), [Samples label](https://github.com/Microsoft/BotFramework-WebChat/issues?utf8=%E2%9C%93&q=is%3Aissue+is%3Aopen+label%3ASample), and the [Migration Support label](https://github.com/Microsoft/BotFramework-WebChat/issues?q=is%3Aissue+migrate+label%3A%22Migration+Support%22) to search for sample requests and/or customization support for a feature you are looking for. Adding your comment to open issues will help the team prioritize requests that are in high demand, and we encourage participation in our community.
-  If you did not find your feature in the list of open requests, please feel free to [file your own request](https://github.com/Microsoft/BotFramework-WebChat/issues/new). Just like the item above, other customers adding comments to your open issue will help us prioritize which features are most commonly needed across Web Chat users.
-  Finally, if you need your feature as soon as possible, we welcome [pull requests](https://github.com/Microsoft/BotFramework-WebChat/compare) to Web Chat. If you have the coding experience to implement the feature yourself, we would very much appreciate the additional support! Creating the feature yourself will mean that it is available for your use on Web Chat more quickly, and that other customers looking for the same or similar feature may utilize your contribution.
-  Make sure to check out the rest of this `README` to learn more about v4.


## Web Chat API Reference

There are several properties that you might pass into your Web Chat React Component (`<ReactWebChat>`) or the `renderWebChat()` method. Feel free to examine the source code starting with [`packages/component/src/Composer.js`](https://github.com/Microsoft/BotFramework-WebChat/blob/master/packages/component/src/Composer.js#L378). Below is a short description of the available props.

| Property                   | Description                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     |
| -------------------------- | ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| `activityMiddleware`       | A chain of middleware, modeled after [Redux middleware](https://medium.com/@jacobp100/you-arent-using-redux-middleware-enough-94ffe991e6), that allows the developer to add new DOM components on the currently existing DOM of Activities. The middleware signature is the following: `options => next => card => children => next(card)(children)`.                                                                                                                                                                                                                                           |
| `activityRenderer`         | The "flattened" version of `activityMiddleware`, similar to the [store enhancer](https://github.com/reduxjs/redux/blob/master/docs/Glossary.md#store-enhancer) concept in Redux.                                                                                                                                                                                                                                                                                                                                                                                                                |
| `adaptiveCardHostConfig`   | Pass in a custom Adaptive Cards host config. Be sure to verify your Host Config with the version of Adaptive Cards that is being used. See [Custom Host config](https://github.com/microsoft/BotFramework-WebChat/issues/2034#issuecomment-501818238) for more information.                                                                                                                                                                                                                                                                                                                                    |
| `attachmentMiddleware`     | A chain of middleware that allows the developer to add their own custom HTML Elements on attachments. The signature is the following: `options => next => card => next(card)`.                                                                                                                                                                                                                                                                                                                                                                                                                  |
| `attachmentRenderer`       | The "flattened" version of `attachmentMiddleware`.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              |
| `cardActionMiddleware`     | A chain of middleware that allows the developer to modify card actions, like Adaptive Cards or suggested actions. The middleware signature is the following: `cardActionMiddleware: () => next => ({ cardAction, getSignInUrl }) => next(cardAction)`                                                                                                                                                                                                                                                                                                                                           |
| `createDirectLine`         | A factory method for instantiating the Direct Line object. Azure Government users should use `createDirectLine({ domain: 'https://directline.botframework.azure.us/v3/directline', token });` to change the endpoint. The full list of parameters are: `conversationId`, `domain`, `fetch`, `pollingInterval`, `secret`, `streamUrl`, `token`, `watermark` `webSocket`.                                                                                                                                                                                                                         |
| `createStore`              | A chain of middleware that allows the developer to modify the store actions. The middleware signature is the following: `createStore: ({}, ({ dispatch }) => next => action => next(cardAction)`                                                                                                                                                                                                                                                                                                                                                                                                |
| `directLine`               | Specify the DirectLine object with DirectLine token.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
| `disabled`                 | Disable the UI (i.e. for presentation mode) of Web Chat.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| `grammars`                 | Specify a grammar list for Speech (Bing Speech or Cognitive Services Speech Services).                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          |
| `groupTimeStamp`           | Change default settings for timestamp groupings.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                |
| `locale`                   | Indicate the default language of Web Chat. Four letter codes (such as `en-US`) are strongly recommended.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        |
| `renderMarkdown`           | Change the default Markdown renderer object.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    |
| `sendTypingIndicator`      | Display a typing signal from the user to the bot to indicate that the user is not idling.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |
| `store`                    | Specify a custom store, e.g. for adding programmatic activity to the bot.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       |
| `styleOptions`             | Object that stores customization values for your styling of Web Chat. For the complete list of (frequently updated) default style options, please see the [defaultStyleOptions.js](https://github.com/Microsoft/BotFramework-WebChat/blob/master/packages/component/src/Styles/defaultStyleOptions.js) file.                                                                                                                                                                                                                                                                              |
| `styleSet`                 | The non-recommended way of overriding styles.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                   |
| `userID`                   | Specify a userID. There are two ways to specify the `userID`: in props, or in the token when generating the token call (`createDirectLine()`). If both methods are used to specify the userID, the token userID property will be used, and a `console.warn` will appear during runtime. If the `userID` is provided via props but is prefixed with `'dl'`, e.g. `'dl_1234'`, the value will be thrown and a new `ID` generated. If `userID` is not specified, it will default to a random user ID. Multiple users sharing the same user ID is not recommended; their user state will be shared. |
| `username`                 | Specify a username.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             |
| `webSpeechPonyFillFactory` | Specify the Web Speech object for text-to-speech and speech-to-text.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            |
## Browser compatibility
Web Chat supports the latest 2 versions of modern browsers like Chrome, Edge, and FireFox.
If you need Web Chat in Internet Explorer 11, please see the [ES5 bundle demo](https://microsoft.github.io/BotFramework-WebChat/01.b.getting-started-es5-bundle).

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

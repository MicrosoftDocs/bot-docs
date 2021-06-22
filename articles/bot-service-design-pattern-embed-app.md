---
title: Embed a bot in an app - Bot Service
description: Learn how to embed bots in apps. See how to integrate bots with native mobile apps, web-based mobile apps, IoT apps, and other app types. View sample code.
author: matvelloso
ms.author: mateusv
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 08/15/2018
 
---

# Embed a bot in an app

[!INCLUDE [applies-to-v4](includes/applies-to-v4-current.md)]

Although bots most commonly exist outside of apps, they can also be integrated with apps. For example, you could embed a [knowledge bot](~/bot-service-design-pattern-knowledge-base.md) within an app to help users find information that might otherwise be challenging to locate within complex app structures.

You can embed a bot within a help desk app to act as the first responder to incoming user requests.
The bot can independently resolve simple issues and [hand off](~/bot-service-design-pattern-handoff-human.md) more complex issues to a human agent.

## Integrating bot with app

The way to integrate a bot with an app varies depending on the type of app.

### Native mobile app

An app that is created in native code can communicate with the Bot Framework by using the [Direct Line API][directLineAPI], either via REST or web sockets.

### Web-based mobile app

A mobile app that is built by using web language and frameworks such as [Cordova](https://cordova.apache.org/) may communicate with the Bot Framework by using the same components that a [bot embedded within a website](~/bot-service-design-pattern-embed-web-site.md) would use, just encapsulated within a native app's shell.

### IoT app

An IoT app can communicate with the Bot Framework by using the [Direct Line API][directLineAPI].

In some scenarios, it may also use [Microsoft Cognitive Services](https://www.microsoft.com/cognitive-services/) like [Speech](/azure/cognitive-services/speech-service/), [Translator](/azure/cognitive-services/translator/), [Text Analytics](/azure/cognitive-services/text-analytics/), and [Computer Vision](/azure/cognitive-services/computer-vision/).

### Other types of apps and games

Other types of apps and games can communicate with the Bot Framework by using the [Direct Line API][directLineAPI]. 

## Creating a cross-platform mobile app that runs a bot

This example of creating a mobile app that runs a bot uses [Xamarin](https://www.xamarin.com/), a popular tool for building cross-platform mobile applications.

First, create a simple web view component and use it to host a [Web Chat](https://github.com/Microsoft/BotFramework-WebChat) control. Then, using Azure portal, add the Web Chat channel.

Next, specify the registered web chat URL as the source for the web view control in the Xamarin app:

```cs
public class WebPage : ContentPage
{
	public WebPage()
	{
		var browser = new WebView();
		browser.Source = "https://webchat.botframework.com/embed/<YOUR SECRET KEY HERE>";
		this.Content = browser;
	}
}
```

Using this process, you can create a cross-platform mobile application 
that renders the embedded web view with the web chat control.

![Back-channel](~/media/bot-service-design-pattern-embed-app/xamarin-apps.png)

<!-- TODO: No sample bot available
## Sample code

For a complete sample that shows how to create a cross-platform mobile app that runs a bot, see the [Bot in Apps sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/capability-BotInApps) in GitHub.
-->

## Additional resources

- [Direct Line API][directLineAPI]
- [Microsoft Cognitive Services](https://www.microsoft.com/cognitive-services/)

[directLineAPI]: https://docs.botframework.com/restapi/directline3/#navtitle

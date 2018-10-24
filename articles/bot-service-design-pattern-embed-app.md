---
title: Embed a bot in an app | Microsoft Docs
description: Learn how to design a bot that will be embedded within another app.
author: matvelloso
ms.author: mateusv
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 08/15/2018
 
---

# Embed a bot in an app

Although bots most commonly exist outside of apps, they can also be integrated with apps. For example, you could embed a [knowledge bot](~/bot-service-design-pattern-knowledge-base.md) within an app 
to help users find information that might otherwise be challenging to locate within complex app structures. 
You could embed a bot within a help desk app to act as the first responder to incoming user requests. 
The bot could independently resolve simple issues and [hand off](~/bot-service-design-pattern-handoff-human.md) more complex issues to a human agent. 

## Integrating bot with app

The way to integrate a bot with an app varies depending on the type of app. 

### Native mobile app

An app that is created in native code can communicate with the Bot Framework by using 
the [Direct Line API][directLineAPI], 
either via REST or websockets.

### Web-based mobile app

A mobile app that is built by using web language and frameworks such as <a href="https://cordova.apache.org/" target="_blank">Cordova</a> 
may communicate with the Bot Framework by using the same components that a 
[bot embedded within a website](~/bot-service-design-pattern-embed-web-site.md) would use, 
just encapsulated within a native app's shell.

### IoT app

An IoT app can communicate with the Bot Framework by using 
the [Direct Line API][directLineAPI]. 
In some scenarios, it may also use <a href="https://www.microsoft.com/cognitive-services/" target="_blank">Microsoft Cognitive Services</a> 
to enable capabilities such as image recognition and speech.

### Other types of apps and games

Other types of apps and games can communicate with the Bot Framework by using 
the [Direct Line API][directLineAPI]. 

## Creating a cross-platform mobile app that runs a bot

This example of creating a mobile app that runs a bot uses <a href="https://www.xamarin.com/" target="_blank">Xamarin</a>, a popular tool 
for building cross-platform mobile applications. 

First, create a simple web view component and use it to host a 
<a href="https://github.com/Microsoft/BotFramework-WebChat" target="_blank">web chat control</a>. 
Then, using Azure portal, add the Web Chat channel. 

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

For a complete sample that shows how to create a cross-platform mobile app that runs a bot (as described in this article), see the <a href="https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/capability-BotInApps" target="_blank">Bot in Apps sample</a> in GitHub.
-->

## Additional resources

- [Direct Line API][directLineAPI]
- <a href="https://www.microsoft.com/cognitive-services/" target="_blank">Microsoft Cognitive Services</a>

[directLineAPI]: https://docs.botframework.com/en-us/restapi/directline3/#navtitle

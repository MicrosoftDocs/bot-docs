---
title: Embed a bot in an app | Microsoft Docs
description: Learn how to design a conversational bot that will be embedded within another app.
author: matvelloso
ms.author: mateusv
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date:
ms.reviewer: rstand
ROBOTS: Index, Follow
---
# Embed a bot in an app

Although bots most commonly exist outside of apps, they can also be embedded within an app. 
For example, you may embed a [knowledge bot](~/bot-design-pattern-knowledge-base.md) within an app 
to enable users to quickly find information that might otherwise be challenging to locate within complex app structures. 
You may embed a bot within a help desk app to act as the first responder to incoming user requests. 
The bot could independently resolve simple issues and [handoff](~/bot-design-pattern-handoff-human.md) more complex issues to a human agent. 

This article explores integrating bots with apps and creating a cross-platform mobile app that runs a bot. 

## Integrating bot with app

The way to integrate a bot with an app varies depending on the type of app. 

### Native mobile app
An app that is created in native code can communicate with the Bot Framework by using 
the [Direct Line API][directLineAPI], 
either via REST or websockets.

### Web-based mobile app
A mobile app that is built by using web language and frameworks such as <a href="https://cordova.apache.org/" target="_blank">Cordova</a> 
may communicate with the Bot Framework by using the same components that a 
[bot embedded within a website](~/bot-design-pattern-embed-web-site.md) would use, 
just encapsulated within a native app's shell.

### IoT app
An IoT app can communicate with the Bot Framework by using 
the [Direct Line API][directLineAPI]. 
In some scenarios, it may also use <a href="https://www.microsoft.com/cognitive-services/" target="_blank">Microsoft Cognitive Services</a> 
to enable capabilities such as vision and speech.

### Other types of apps and games
Other types of apps and games can communicate with the Bot Framework by using 
the [Direct Line API][directLineAPI]. 

## Creating a cross-platform mobile app that runs a bot

This example of creating a mobile app that runs a bot uses <a href="https://www.xamarin.com/" target="_blank">Xamarin</a>, a popular tool 
for building cross-platform mobile applications. 

First, create a simple web view component and use it to host a 
<a href="https://github.com/Microsoft/BotFramework-WebChat" target="_blank">web chat control</a>. 
Then, using the Bot Framework Portal, [configure the bot](~/portal-configure-channels.md) to 
enable the Web Chat channel. 

![Bot configuration settings](~/media/designing-bots/patterns/webchat-channel.png)

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

![Back-channel](~/media/designing-bots/patterns/xamarin-apps.png)

## Additional resources

- [Direct Line API][directLineAPI]
- <a href="https://www.microsoft.com/cognitive-services/" target="_blank">Microsoft Cognitive Services</a>
- [Add smarts to your bot with Microsoft Cognitive Services](~/intelligent-bots.md)

<!--
This article covered ways to integrate bots with apps and 
explored the process of creating a cross-platform mobile app that runs a bot.
To see the complete sample code (and step-by-step implementation guidance) for the example discussed above, 
review the following resources: 

> [!NOTE]
> To do: Add links to the code sample (and readme) that Mat refers to.-->
 
[directLineAPI]: https://docs.botframework.com/en-us/restapi/DirectLine3/#navtitle
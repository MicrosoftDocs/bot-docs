---
layout: page
title: What's New in the Framework?
permalink: /en-us/whatsnew/
weight: 4070
parent1: none
---

<span style="color:red">We need to decide our Release Notes story. There needs to be one place where user's look for notes that describes what's changed in the release.

There's this file, Node has a What's New and a Release Notes topic, ...</span>

## November 16, 2016

This release contains the following updates, which are designed to help you build better bots faster. 

* Updated the Bot Framework Emulator and made it available as open source on GitHub. The emulator now runs on Mac, Linux, and Window. [Read more](https://github.com/Microsoft/BotFramework-Emulator){:target="_blank"}. 

* Updated Direct Line to support the latest Bot Framework card types and activities. Additionally, Direct Line now supports both sockets and polling for increased efficiency and responsiveness. [Read more](/en-us/restapi/directline3).

* Updated the Web Chat control to use Direct Line, and made the control available as open source on GitHub (see [microsoft/botframework-webchat](https://github.com/microsoft/botframework-webchat). If your bot is currently configured to use the old Web Chat control, you can use the new control by editing your Web Chat channel configuration in the dev portal (see **My bots**).

* Updated the Bot Builder SDKs to enable your bot to have more flexible conversations with users by allowing you to interrupt  in-progress dialogs and change to another dialog based on an action or trigger. Additionally, the .Net SDK now provides improved integrated language understanding dialogs, powered by Cognitive Services. For a list of all changes in this release, see [Node.js Release Notes](/en-us/node/builder/whats-new) and [.NET Release Notes](/en-us/csharp/builder/libraries/latest/)).  

* Improved the developer portal by supporting both Microsoft account (MSA) and Org ID authentication, streamlined bot registration, added the Web Chat control to your bot’s dashboard, as well as improvements to the documentation.

* Added the Azure Bot Service (Preview), which is a new public cloud bot service powered by the Microsoft Bot Framework and Azure’s trusted, global-scale platform. [Read more](/en-us/azure-bot-service/)

## November 2, 2016

* This release adds support for Microsoft Teams, the chat-based workspace for Office 365. Fulfilling on our promise to write once, run anywhere, your bot can join the conversation today on Teams just by tapping "Add" in the dashboard. [Read more](https://dev.office.com/blogs/microsoft-teams-developer-preview).


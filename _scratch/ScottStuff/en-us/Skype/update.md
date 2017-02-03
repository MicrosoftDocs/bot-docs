---
layout: page
title: Latest update
permalink: /en-us/skype/update/
weight: 5110
parent1: Channels
parent2: Skype bots
---

In July 16 Microsoft brought together the Skype Bot developer tools and the Microsoft Bot Framework into one environment called the Microsoft Bot Framework V3. You can now develop bots which use new Skype platform features – such as visual cards and group bots – and publish to multiple channels from one place.

Existing bots registered in the Skype Bot Portal and developed using the Skype Bot SDK are in the process of being deprecated and we recommend you move to the new environment as soon as possible to get access to the latest features and updates.

To update to the new environment you need to register a new bot with the Microsoft Bot Framework and update your bot to the latest API.

* TOC
{:toc}

## What's new

### Cards
{:.no_toc}

Create [visual cards](/en-us/skype/getting-started/#navtitle) for compelling user to bot interactions with images, carousels, receipts and action buttons.

![Carousel card](/en-us/images/skype/skype-bot-carousel-card.png)

### Sign in
{:.no_toc}

Create a [sign in card](/en-us/skype/getting-started/#navtitle) for authenticating a user with your service via OAuth or other login methods

![Sign in card](/en-us/images/skype/skype-bot-signin-card.png)

### Groups
{:.no_toc}

Make Skype Bots that are more productive - or just entertaining - for [groups](/en-us/skype/getting-started/#groups) of users.  Bots can now be a part of and respond to group conversations.

![Groups](/en-us/images/skype/skype-bot-at-mention.png)

### Plus
{:.no_toc}

* Publish your bot in Skype directly from the Microsoft Bot Framework - to remove bot user limits, and request promotion in the Skype and Microsoft bot directories
* Updates to the API schema unifying the Microsoft Bot Framework and Skype Bot Platform (this requires a few simple updates to your bot)
* Try out a preview of built-in [Bing Entity and Intent Detection](/en-us/skype/getting-started/#bing-entity-and-intent-detection-preview), which adds natural language understanding to messages sent from Skype to your bot
* Review the updated and combined [Terms of Use](https://aka.ms/bf-terms) and [Developer Code of Conduct](https://aka.ms/bf-conduct)

## How to update an existing bot

This guide applies to bots registered in the [Skype Bot Portal](https://developer.microsoft.com/en-us/skype/bots).

If your bot was already registered in the [Microsoft Bot Framework Portal](https://dev.botframework.com/) you can follow [this alternative guide](https://aka.ms/bf-migrate) to update your bot to the latest version of the SDK.

For bots registered in the [Skype Bot Portal](https://developer.microsoft.com/en-us/skype/bots):

### 1. Register a new bot in the Microsoft Bot Framework
{:.no_toc}

* Go to the Microsoft Bot Framework and tap “Register a bot”

![Microsoft Bot Framework](/en-us/images/skype/bot-framework.png)

* Register the bot and get a new Microsoft App ID and Secret

![Microsoft App ID](/en-us/images/skype/bot-framework-app-id.png)

### 2. Update your bot code to use the new V3 API
{:.no_toc}

* Update the code at your endpoint (which could be a new endpoint) to use the latest Microsoft Bot Framework API or SDK. See the [Skype Getting Started](/en-us/skype/getting-started) guide for details on the latest Skype bot platform features, plus the [C# SDK](/en-us/csharp/builder/sdkreference/index.html), [Node SDK](/en-us/node/builder/overview/#navtitle) or [Skype REST API](/en-us/skype/chat/#navtitle).

* Test using the [Microsoft Bot Framework Emulator](en-us/tools/bot-framework-emulator/) and the [latest public versions of Skype](http://www.skype.com/go/download).

### 3. Publish your bot
{:.no_toc}

When your new bot is tested, tap Publish in the Microsoft Bot Framework to submit it for review to remove the 100 contact preview limit and - if you want - submit for consideration in the Skype Bot Directory.

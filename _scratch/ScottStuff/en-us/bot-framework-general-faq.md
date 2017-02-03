---
layout: page
title: General FAQ
permalink: /en-us/faq/general-faq/
weight: 4046
parent1: FAQ
---

This FAQ answers general questions about Bot Framework. For answers to more technical questions about the framework, see [Technical FAQ](/en-us/technical-faq/).

* TOC
{:toc}

## What is the Microsoft Bot Framework?

Microsoft Bot Framework is a comprehensive offering that you use to build and deploy bots for your users to enjoy in their favorite conversation experiences. The framework provides just what you need to build, connect, manage and publish intelligent bots that interact naturally wherever your users are talking&mdash;from Text/SMS to Skype, Slack, Facebook Messenger, Kik, Office 365 mail and other popular services. [Read more](/en-us/).

The Bot Framework consists of a number of components including the Bot Builder SDK, Developer Portal and the Bot Directory.

### Bot Builder SDK
{:.no_toc}

The Bot Builder SDK is [an open source SDK hosted on GitHub](https://github.com/Microsoft/BotBuilder) that provides everything you need to build great bots. The framework provides an SDK for those using Node.js or C#. All other languages would use the REST API.

### Bot Framework Developer Portal
{:.no_toc}

Bot Framework Developer Portal lets you connect your bots seamlessly to Text/SMS, Skype, Slack, Facebook Messenger, Kik, Office 365 mail and other popular services. Simply register your bot, configure desired communication channels (services) and publish your bot in the Bot Directory. All bots registered with Bot Framework are automatically configured to work with Skype and Web Chat.

### Bot Directory
{:.no_toc}

Bot Directory is a public directory of all reviewed bots built and registered using the framework. Users will be able to discover, try, and add bots to their favorite conversation experiences from the directory. 

## Why should I write a bot?
The conversational user interface (CUI) has arrived. Conversation-driven UI now enables us to do everything from grabbing a taxi, to paying the electric bill or sending money to a friend. Offerings such as Siri, Google Now and Cortana demonstrate value to millions of people every day, particularly on mobile devices where the CUI can be superior to the graphical user interface (GUI), or complements it. 

Bots are rapidly becoming an integral part of people’s digital experience&mdash;they are as vital a way for users to interact with a service or application as is a web site or a mobile experience.

## Who are the target users for the Bot Framework? How will they benefit?

Bot Framework is targeted at developers who want to create a new service with a great bot interface or enable an existing service with a great bot interface. The framework provides:

* Preconfigured Skype and Web Chat communication channels
* Embeddable Web chat control
* Card normalization so your bot is responsive across communication channels
* Direct Line API which you can use to host your bot in your app or website 
* Debugging tools including the Bot Framework [Emulator](/en-us/tools/bot-framework-emulator/) (online/offline)
* Powerful service extensions to make your bot smarter using [Cognitive Services](http://www.microsoft.com/cognitive). For example, LUIS for language understanding, Translator for automatic translation to more than 30 languages, and FormFlow for reflection generated bots.

<span style="color:red"><< Jim, do we want to link to the cognitive website, the cognitive apis page (https://www.microsoft.com/cognitive-services/en-us/apis), or to the Bot Intelligence section (i think the latter)? >></span>

<span style="color:red"><< Also, why does service extensions list FormFlow ("and FormFlow for reflection generated bots"? Why "reflection generated", I thought you used formflow for guided conversations? >></span> 

## I'm a developer, what do I need to get started?

You'll need the following to get started building a bot using the framework.

* A Microsoft account (MSA). You need the account to register your bot. If you don't have an account, you can sign up for one on the [developer portal](http://botframework.com).
* The [Bot Builder SDK](http://github.com/Microsoft/BotBuilder) or the [REST API](/en-us/core-concepts/reference/#endpoints) endpoint. The SDK is open source and available on GitHub.  

To get started with the SDKs and REST API, see the following guides:

* [Get started with Bot Builder for Node.js](/en-us/node/builder/overview/)
* [Get started with Bot Builder for .NET](/en-us/csharp/builder/sdkreference/)
* [Get started with the REST API](/en-us/core-concepts/overview/)

## What does the Developer Portal provide to developers? How does it work?

The Bot Framework Developer Portal is where you register and connect your bot to many different conversation experiences. To make your bot available to your users through the Bot Framework, you must have:

* A bot. If you don’t have a bot, check out the [Bot Builder SDK](http://github.com/Microsoft/BotBuilder) on GitHub.
* A [Microsoft Account](https://signup.live.com), which you will use to register and manage your bot in the portal.
* An internet-accessible REST endpoint exposing the Bot Framework messages API.
* An account on one or more conversation channels where your bot will converse. This requirement is channel dependent.

<span style="color:red"><< Jim, so you don't have to build the bot with either our SDK or REST API (see the first bullet)? >></span>

<span style="color:red"><< I don't understand the third bullet. >></span>

#### Register
{:.no_toc}

To register your bot, sign in to [Bot Framework](http://botframework.com), click **Register a bot**, and provide the requested details about your bot, including a bot profile image.

After registering your bot, use the bot's dashboard (click **My bots** and then click your bot) to test the bot to ensure it is talking with the connector service. You can click **Test** or interact with the bot using the integrated Web Chat control.

#### Connect to Channels
{:.no_toc}

Connect your bot to the conversation channels of your choice by configuring the channel on the bot's dashboard. The Skype and Web Chat channels are preconfigured for you.

#### Test
{:.no_toc}

Test your bot's connection to the Bot Framework and try it out using the Web Chat control.

#### Publish
{:.no_toc}

After you complete your bot and are ready to make it available to users, you can click **Publish** on the bot's dashboard to have it published to the [Bot Directory](http://bots.botframework.com). Publishing to the directory lets users more easily discover your bot, try it out, and add the bot to their favorite conversation experiences.

#### Measure
{:.no_toc}

If you host your bot in Azure you can link to [Azure Application Insights](https://azure.microsoft.com/en-us/services/application-insights/) analytics directly from the bot dashboard in the Bot Framework website. Naturally, a variety of analytics tools exist in the market to help developers gain insight into bot usage (which is certainly advisable to do).

<span style="color:red"><< Jim, where in the dashboard can they link to Insights? >></span>

#### Manage
{:.no_toc}

After you register your bot, you can manage the bot by using the bot's dashboard in the developer portal. To get to your bot's dashboard, sign in to [Bot Framework](http://botframework.com), click **My bots**, and then click your bot.

## What does the Bot Builder SDK provide to developers? How does it work?

The [Bot Builder SDK](http://github.com/Microsoft/BotBuilder) is an open source SDK hosted on GitHub that provides just what you need to build a great bot using Node.js, .NET or REST. From simple prompt and command dialogs to simple-to-use yet sophisticated "FormFlow" dialogs that help with tricky issues such as multi-turn and disambiguation. The SDK provides the libraries, samples, and tools you need to get your bot up and running. To learn more, see the Bot Builder SDK [documentation](http://docs.botframework.com).

## What does the Bot Directory provide to developers? How does it work?

The [Bot Directory](http://bots.botframework.com) is a publicly accessible list of all the bots registered with Bot Framework that have been submitted and reviewed to appear in the directory. Each bot has its own contact card which includes the bot name, publisher, description, and the channels that the bot is available on. Users can view details about any bot, try them out using theW\ Web Chat control, and add the bot to any channels that they're configure to run on. Users can also use the directory to report abuse.

The Bot Directory includes featured bots and is searchable to aid discovery. Developers can choose whether or not to list their bot in the directory (see the **Publish** button in your bot's dashboard).

## Can I submit my bot to the Bot Directory?

You may submit for review any bot that you've registered with Bot Framework.  

<span style="color:red"><< Jim, does the bot have to be built using the REST API or one of the SDKs AND registered, or does it just need to be registered? >></span>

## Do I have to publish my bot to the Bot Directory in order for my bot to be available to users?

No, publishing your bot is an optional process. Users can use your bot on most channels without it being published to the Bot Directory; however, some channels do limit the number of users allowed to interact with the bot until it's reviewed. 

<span style="color:red"><< Jim, so channels other than Skype require their own, channel-specific review? >></span>

<span style="color:red"><< Does the framework also conduct a review? >></span>


## What channels does the Bot Framework currently support?

For a list of supported channels and features, see [Channel Inspector](/en-us/channel-inspector/).

## When will you add more conversation channels to the Bot Framework?

We're always making continuous improvements to the Bot Framework, including adding channels.  If you would like a specific conversation channel added to the framework, [let us know](/en-us/support/).

## I have a communication channel I’d like to be configurable with Bot Framework. Can I work with Microsoft to do that?

We have not provided a general mechanism for developers to add new channels to Bot Framework, but you can connect your bot to your app via the [Direct Line API](http://docs.botframework.com). If you are a developer of a conversation channel and would like to work with us to enable your channel in Bot Framework [we’d love to hear from you](/en-us/support/).

## Is it possible to build a bot using the Bot Framework that is a private or enterprise-only bot available only inside my company?

No, we do not have plans to enable a private instance of the Bot Directory, but we are interested in exploring ideas like this with the developer community.

<span style="color:red"><< Jim, why can't they? Can't they build it and make it available to just their people? >></span>

## Why did Microsoft develop the Bot Framework?

The conversational user interface (CUI) experience is new and there are few developers that have the expertise and tools needed to create new conversational experiences or enable existing applications and services with a conversational interface their users can enjoy. We have created the Bot Framework to make it easier for developers to build and connect great bots to users, wherever they converse including on Microsoft's premier channels.

## If I want to create a bot for Skype, what tools and services should I use?

The Bot Framework is designed to build, connect and publish high quality, responsive, performant and scalable bots for Skype and many other channels. If you already have a great bot and would like to reach the Skype audience, your bot can easily be connected to Skype (or any supported channel) via the Bot Builder for REST API (provided it has an internet-accessible REST endpoint).

<span style="color:red"><< Jim, please explain the last part of the last sentence. >></span>

## Who are the people behind the Bot Framework?

In the spirit of One Microsoft, the Bot Framework is a collaborative effort across many teams, including Microsoft Technology and Research, Microsoft’s Applications and Services Group and Microsoft’s Developer Experience teams.

<span style="color:red"><< Seems very odd to have this; do we really need it? >></span>

## When did work begin on the Bot Framework?

The core Bot Framework work has been underway since the summer of 2015.

<span style="color:red"><< Seems very odd to have this; do we really need it? >></span>

## Is the Bot Framework publicly available now?

Yes, the framework was released as a preview on March 30th, 2016 in conjunction with Microsoft’s annual developer conference ([/build](http://build.microsoft.com/)).

<span style="color:red"><< Still in a preview? >></span>

## How long will the Bot Framework be in preview? Can I start building/shipping products based on a preview framework?

The Bot Framework is currently as a preview. The expectation is to make it generally available by the end of 2016. As indicated at Build 2016, Microsoft is making significant investments in Conversation as a Platform. Among those investments is the Bot Framework. Shipping a product based on a preview offering is of course at your discretion.

<span style="color:red"><< Should we say something about rapid iteration of the framework until it's generally available? >></span>

## What is the roadmap for Bot Framework?

We provided a preview release of the framework at [/build 2016](http://build.microsoft.com/) and plan to continuously improve the framework with additional tools, samples, and channels. The [Bot Builder SDK](http://github.com/Microsoft/BotBuilder) is an open source SDK hosted on GitHub, and we look forward to contributions and suggestions from the community at large.

## The Bot Framework July 2016 Update saw some significant changes to the SDKs and service. Can you enumerate these changes?

<span style="color:red"><< We should not have this section. There should be one place where developers go to see changes with each release. If you want to include a question such as "Where can I see changes for each release of the framework?", that's fine. You can then link to the Release Notes topic. >></span>

The July 2016 update is largely in response to feedback received from the active Bot Framework community. The update is focused on quality, control and performance. Enhancements include:

* Automatic card normalization across channels
* More direct message handling; more control
* Additional dialog types and capabilities in the SDK
* Enhanced connection to Cognitive Services within the SDK
* Improvements to the Emulator and Direct Line API
* Skype channel auto-configured for any bot using Bot Framework

## Do I need to upgrade my bot with this service update?

<span style="color:red"><< This shouldn't be here. This should be in the release notes, and ideally this kind of thing should be big and bold on the frameworks site and docs site. >></span>

Yes. You will need to [upgrade your bot](https://aka.ms/bf-migrate) with this release of the service. All bots written prior to the July release will need to upgrade to the latest SDK (v3) in order to continue to function. Bots written to versions of the SDK prior to V3 will cease functioning in roughly 90 days post the July 7th release. 

## Do the bots registered with the Bot Framework collect personal information? If yes, how can I be sure the data is safe and secure? What about privacy?

Each bot is its own service, and developers of these services are required to provide Terms of Service and Privacy Statements per their Developer Code of Conduct.  You can access this information from the bot’s card in the Bot Directory.

<span style="color:red"><< What if the bot is not published to the directory? >></span>

In order to provide the I/O service, the framework transmits your message (including your ID) from the conversation channel (chat service) to the bot. 

<span style="color:red"><< What do you mean by I/O service? >></span> 

## How do you ban or remove bots from the service?

Users can report misbehaving bots by using the bot’s contact card in the directory. Developers must abide by Microsoft's terms of service to participate in the service.

<span style="color:red"><< What if the bot is not in the directory? >></span>

<span style="color:red"><< "terms of service" or code of conduct? >></span> 

## Where can I get more information and/or access the technology?

Visit [www.botframework.com](http://botframework.com) to learn more.

## How does the Bot Framework relate to Cognitive Services?

Both Bot Framework and [Cognitive Services](http://www.microsoft.com/cognitive) are new capabilities introduced at [Microsoft Build 2016](http://build.microsoft.com) that will also be integrated into Cortana Intelligence Suite when they become generally available. Both these services are built from years of research and use in popular Microsoft products. These capabilities combined with Cortana Intelligence enable every organization to take advantage of the power of data, the cloud and intelligence to build their own intelligent systems that unlock new opportunities, increase their speed of business and lead the industries in which they serve their customers.

## What is Cortana Intelligence?

[Cortana Intelligence](https://www.microsoft.com/en-us/cloud-platform/cortana-intelligence-suite) is a fully managed Big Data, Advanced Analytics and Intelligence suite that transforms your data into intelligent action. It is a comprehensive suite that brings together technologies founded upon years of research and innovation throughout Microsoft (spanning advanced analytics, machine learning, big data storage and processing in the cloud) and it:

* Allows you to collect, manage, and store all of your data that can seamlessly and cost effectively grow over time in a scalable and secure way.
* Provides easy and actionable analytics powered by the cloud that allow you to predict, prescribe, and automate decision making for the most demanding problems. 
* Enables intelligent systems through cognitive services and agents that allow you to see, hear, interpret, and understand the world around you in more contextual and natural ways.

With Cortana Intelligence, we hope to help our enterprise customers unlock new opportunities, increase their speed of business and be leaders in their industries.

## What is the Direct Line channel?

Direct Line is a REST API that allows you to add your bot into your service, app, or webpage. You can write a client for the Direct Line API in any language. Simply code to the [Direct Line protocol](/en-us/restapi/directline/), generate a secret in the Direct Line configuration page, and talk to your bot from wherever your code lives.

Direct Line is suitable for:

* Mobile apps on iOS, Android, and Windows Phone, and others
* Desktop applications on Windows, OSX, and more
* Webpages where you need more customization than the [embeddable Web Chat channel](/en-us/support/embed-chat-control2/) offers
* Service-to-service applications

## Why does my Direct Line conversation start over after every message?

<span style="color:red"><< Why isn't this in the technical faq? >></span>

If your Direct Line conversation appears to start over after every message, you're likely not setting the `from` field on the messages you sent from your Direct Line client. Because Direct Line will allocate an ID when the `from` property is null, every message sent from your client appears to your bot to be a new user.

To fix this, set the `from` field to a stable value that represents the user. The value of the field is up to you. If you already have a signed-in user in your webpage or app, you can use the existing user ID. If not, you could generate a random user ID on page/app load, optionally store that ID in a cookie or device state, and use that ID.

<span style="color:red"><< If direct line allocates the ID if you don't set it, is there anyway to get the ID and keep using it? >></span>


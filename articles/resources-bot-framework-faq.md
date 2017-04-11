---
title: Bot Framework Frequently Asked Questions | Microsoft Docs
description: Bot Framework frequently asked questions (FAQ), including questions about what's new, channels and framework availability.
keywords:
author: DeniseMak
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 02/21/2017
ms.reviewer:

# Include the following line commented out
#ROBOTS: Index
---
# Bot Framework Frequently Asked Questions
This article contains answers to some frequently asked questions about the Bot Framework.

## Background and availability
### Why did Microsoft develop the Bot Framework?

While the Conversation User Interface (CUI) is upon us, at this point few developers have the expertise and tools needed to create new conversational experiences or enable existing applications and services with a conversational interface their users can enjoy. We have created the Bot Framework to make it easier for developers to build and connect great bots to users, wherever they converse, including on Microsoft's premier channels.


### Who are the people behind the Bot Framework?

In the spirit of One Microsoft, the Bot Framework is a collaborative effort across many teams, including Microsoft Technology and Research, Microsoft’s Applications and Services Group and Microsoft’s Developer Experience teams.

### When did work begin on the Bot Framework?

The core Bot Framework work has been underway since the summer of 2015.

### Is the Bot Framework publicly available now?

Yes. The Bot Framework was released in preview on March 30th of 2016 in conjunction with Microsoft’s annual developer conference [/build](http://build.microsoft.com/).

### How long will the Bot Framework be in preview? Can I start building/shipping products based on a preview framework?

The Bot Framework is currently in preview. As indicated at Build 2016, Microsoft is making significant investments in Conversation as a Platform - among those investments is the Bot Framework. Building upon a preview offering is, of course, at your discretion.

### What is the future of the Bot Framework?

We are excited to provide initial availability of the Bot Framework at [/build 2016](http://build.microsoft.com/) and plan to continuously improve the framework with additional tools, samples, and channels. The [Bot Builder SDK](http://github.com/Microsoft/BotBuilder) is an open source SDK hosted on GitHub and we look forward to the contributions of the community at large. [Feedback][Support] as to what you’d like to see is welcome.

## Channels
### When will you add more conversation experiences to the Bot Framework?

We plan on making continuous improvements to the Bot Framework, including additional channels, but cannot provide a schedule at this time.  
If you would like a specific channel added to the framework, [let us know][Support].

### I have a communication channel I’d like to be configurable with Bot Framework. Can I work with Microsoft to do that?

We have not provided a general mechanism for developers to add new channels to Bot Framework, but you can connect your bot to your app via the [Direct Line API][DirectLineAPI]. If you are a developer of a communication channel and would like to work with us to enable your channel in the Bot Framework [we’d love to hear from you][Support].

### If I want to create a bot for Skype, what tools and services should I use?

The Bot Framework is designed to build, connect and publish high quality, responsive, performant and scalable bots for Skype and many other channels. The SDK can be used to create text/sms, image, button and card-capable bots (which constitute the majority of bot interactions today across conversation experiences) as well as bot interactions which are Skype-specific such as rich audio and video experiences.

If you already have a great bot and would like to reach the Skype audience, your bot can easily be connected to Skype (or any supported channel) via the Bot Builder for REST API (provided it has an internet-accessible REST endpoint).

### Is it possible for me to build a bot using the Bot Framework/SDK that is a “private or enterprise-only” bot that is only available inside my company?

At this point, we do not have plans to enable a private instance of the Bot Directory, but we are interested in exploring ideas like this with the developer community.

## Security and Privacy
### Do the bots registered with the Bot Framework collect personal information? If yes, how can I be sure the data is safe and secure? What about privacy?

Each bot is its own service, and developers of these services are required to provide Terms of Service and Privacy Statements per their Developer Code of Conduct.  You can access this information from the bot’s card in the Bot Directory.

to provide the I/O service, the Bot Framework transmits your message and message content (including your ID), from the chat service you used to the bot.

### How do you ban or remove bots from the service?

Users have a way to report a misbehaving bot via the bot’s contact card in the directory. Developers must abide by Microsoft terms of service to participate in the service.

## Related Services
### How does the Bot Framework relate to Cognitive Services?

Both the Bot Framework and [Cognitive Services](http://www.microsoft.com/cognitive) are new capabilities introduced at [Microsoft Build 2016](http://build.microsoft.com) that will also be integrated into Cortana Intelligence Suite at GA. Both these services are built from years of research and use in popular Microsoft products. These capabilities combined with ‘Cortana Intelligence’ enable every organization to take advantage of the power of data, the cloud and intelligence to build their own intelligent systems that unlock new opportunities, increase their speed of business and lead the industries in which they serve their customers.

### What is Cortana Intelligence?

Cortana Intelligence is a fully managed Big Data, Advanced Analytics and Intelligence suite that transforms your data into intelligent action.  
It is a comprehensive suite that brings together technologies founded upon years of research and innovation throughout Microsoft (spanning advanced analytics, machine learning, big data storage and processing in the cloud) and:

* Allows you to collect, manage and store all your data that can seamlessly and cost effectively grow over time in a scalable and secure way.
* Provides easy and actionable analytics powered by the cloud that allow you to predict, prescribe and automate decision making for the most demanding problems.
* Enables intelligent systems through cognitive services and agents that allow you to see, hear, interpret and understand the world around you in more contextual and natural ways.

With Cortana Intelligence, we hope to help our enterprise customers unlock new opportunities, increase their speed of business and be leaders in their industries.

### What is the Direct Line channel?

Direct Line is a REST API that allows you to add your bot into your service, mobile app, or webpage.

You can write a client for the Direct Line API in any language. Simply code to the [Direct Line protocol][DirectLineAPI], generate a secret in the Direct Line configuration page, and talk to your bot from wherever your code lives.

Direct Line is suitable for:

* Mobile apps on iOS, Android, and Windows Phone, and others
* Desktop applications on Windows, OSX, and more
* Webpages where you need more customization than the [embeddable Web Chat channel][WebChat] offers
* Service-to-service applications

<!-- TODO: Update links when new content is available -->
[DirectLineAPI]: http://docs.botframework.com/en-us/restapi/directline/
[Support]: https://docs.botframework.com/en-us/support/
[WebChat]: https://docs.botframework.com/en-us/support/embed-chat-control2/
[RestAuth]: https://docs.botframework.com/en-us/restapi/authentication/#changes

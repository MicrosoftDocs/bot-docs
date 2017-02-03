---
layout: page
title: Azure Bot Service Overview
permalink: /en-us/azure-bot-service/
weight: 10000
parent1: Azure Bot Service
parent2: Get Started
---

Use the Azure Bot Service to accelerate your bot’s development by working in an integrated environment that’s purpose-built for bot development. This integrated environment lets you build, connect, deploy and manage intelligent bots that interact naturally wherever your users are talking&mdash;from your app or website to Text/SMS, Skype, Slack, Facebook Messenger, Kik, Office 365 mail, and other popular services. You can also increase the value of your bots with a few lines of code by plugging into [Cognitive Services](/en-us/bot-intelligence/getting-started/) to enable your bots to see, hear, interpret and interact in more human ways.

Get started in seconds with out-of-the-box templates including basic bot, LUIS bot, form bot, and proactive bot. Azure Bot Service is powered by Microsoft Bot Framework and [Azure Functions](http://go.microsoft.com/fwlink/?linkID=747839). By using Azure Functions, your bot will run in a serverless environment on Azure that will scale based on demand.

You can write your bot in C# or Node.js directly in the browser using the Azure editor without any need for a tool chain (local editor and source control). The integrated chat window sits side-by-side with the Azure editor, which lets you test your bot on the fly as you write the code in the browser. 

The Azure editor does not allow you to manage the files by adding new files or renaming or deleting existing files. If need to manage the files, you should set up [continuous integration](/en-us/azure-bot-service/manage/setting-up-continuous-integration/). This would let you use the IDE and source control of your choice (for example, Visual Studio Team, GitHub, and Bitbucket). Continuous integration will automatically deploy to Azure the changes that you commit to source control. Note that after configuring continuous integration, you will no longer be able to update the bot in the Azure editor.

Once you have setup continuous integration, you can debug your bot locally by following [these instructions](/en-us/azure-bot-service/manage/debug/).

The following are the Bot App templates that you can create.

- Basic&mdash;A simple bot that uses dialogs to respond to user input. See [Basic bot template](/en-us/azure-bot-service/templates/basic).
- Form&mdash;A bot that uses a guided conversation to collect user input. The C# template uses FormFlow to collect user input, and the Node.js template uses waterfalls. See [Form bot template](/en-us/azure-bot-service/templates/form).
- Language understanding&mdash;A bot that uses natural language models (LUIS) to understand user intent. See [Nautral language bot template](/en-us/azure-bot-service/templates/luis).
- Proactive&mdash;A bot that uses Azure Functions to alert bot users of events. See [Proactive bot template](/en-us/azure-bot-service/templates/proactive).

For a walkthrough that shows creating a bot using Azure Bot Service, see [Creating your first bot](/en-us/azure-bot-service/build/first-bot/).

{% comment %}
- qnamaker bot&mdash;A bot that uses a knowledge base to answer the user’s questions. See [Knowledge base bot template](/en-us/azure-bot-service/templates/qna). 
{% endcomment %}

{% comment %}
To manage the files in the Azure editor, you need to use the Kudu service. The following steps show you how to access the Kudu service.

1. Open a new browswer window.
2. Enter your deployment URL in the browser after adding 'scm' as the first subdomain of your deployment URL. For example, if your deployment URL is https://myhikingtrailsbot.azurewebsites.net, enter https://myhikingtrailsbot.scm.azurewebsites.net.
3. Click the Debug console menu and select the CMD menu item.
4. In the navigation tree, navigate to your bot's folder by clicking Site, wwwroot, and your bot's folder.
5. You can now add new files, rename files, or delete files. 

 For more information about the Kudu service, see [Accessing the kudu service](https://github.com/projectkudu/kudu/wiki/Accessing-the-kudu-service). 
 {% endcomment %}
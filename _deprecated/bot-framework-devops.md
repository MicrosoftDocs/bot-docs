---
title: DevOps for the Bot Framework | Microsoft Docs
description: Overview of DevOps practices for the Bot Framework.
keywords: bot framework, devops, bot, continuous integration, continuous deployment, testing, telemetry
documentationcenter: BotFramework-Docs
author: nzthiago
manager: rstand
ms.service: Bot Framework
ms.topic: devops-article
ms.date: 02/27/2017
ms.author: thalme@microsoft.com
ROBOTS: Index, Follow
---
# DevOps for the Bot Framework

As you create Bots with the Microsoft Bot Framework and Microsoft Cognitive Services, you will start thinking about maturing your bots' development lifecycle. Some DevOps practices to consider are:
- **Infrastructure as Code** - use your software development processes to manage the deployment and configuration of your bot automatically.
- **Continuous Integration** - how to continuously build and unit test your bot.
- **Automated Testing** - how to implement unit, functional and load testing.
- **Continuous Deployment** - how to deploy your bot to multiple environments, coordinate with dependencies, and ensure no downtime.
- **Conversation Telemetry** - learning from production – how to add telemetry to your bot, what to track, and how to understand this data.

The good news is a lot of the continuous delivery practices for bots are the same as for regular web API applications. But there are a few considerations that are specific to the bot framework in this article.

## Why DevOps for Bots?

So, why should you worry about DevOps in a team that is creating bots? A great definition of DevOps by [Donovan Brown](http://www.donovanbrown.com) is
> “DevOps is the union of people, process, and products to enable continuous delivery of value to our end users.”
The key word in there is delivering value to customers. So, the question really is, why wouldn't you consider adopting DevOps for bots, or any other project? 

## Source control

Your bot code will be a regular web API project plus other projects to interact with external dependencies, so your current source control discipline will already work. [Git flow](http://nvie.com/posts/a-successful-git-branching-model/) with feature and other supporting branches works well for teams working on bots, with branches created for specific dialogs or features that your bot will support. You should focus on frequent merges via pull request to the main trunk(s) to trigger Continuous Integration with unit tests. [Visual Studio Team Services](https://www.visualstudio.com/learn/learn-git-with-team-services/) and others services support this flow.

Other common files to include into source control for your bot:
* Any infrastructure as code files. 
* Exported JSON files from your Language Understanding Intelligent Service (LUIS) model(s) if your bot uses language understanding. 

## Infrastructure as Code

If hosting your bot code on Azure, a common practice is to define an Azure ARM template that includes:
* An App Service Hosting plan
* A Web App, with an optional Pre-Release slot
* An Application Insights instance for telemetry
* Other Azure dependencies: Cognitive Services, databases, search, Redis, and other

This ARM template can be used during continuous deployment to first create a Beta hosting instance for your bot and then again for a Production instance with a different name and scale settings. For example, you might want to set the scale of the App Service Plan in production to be more than beta. Here is an [example ARM template](https://github.com/nzthiago/BotInsightsTests/blob/master/StateBot.template.json) that defines two web apps in one App Service Plan. It defines two web apps to highlight some App Settings differences between .NET and Node, but you can use this template as an example. Note the specific App Settings that you need to pass in to the template:
* For .NET based bots the Bot Builder SDK looks for MicrosoftAppId, MicrosoftAppPassword, and BotId. 
* For Node.js you can change the name of the App Settings, as long as you use the same names in code. They're typically MICROSOFT_APP_ID and MICROSOFT_APP_PASSWORD (no need for Bot Id).

These App Settings on Azure Web Apps become environment variables that you can use from your Node code, and if they're defined in a web.config file the values are updated on the web.config as well for .NET bots. This way your bot code will know which instance of the bot on the Bot Framework to use.

## Hosting, Scaling and Load Balancing bots

Your bot will be sending messages to the Bot Connector via the Node or .NET Bot Builder SDK, and making calls to other services like the Microsoft Cognitive Services, databases, Azure Search, and external APIs. Most of the computation should not be happening in your bot code but in these dependencies. User and conversation state is saved on the Bot Connector side. When hosting your bot, start small and scale up based on demand. Azure's [App Service Web Apps](https://azure.microsoft.com/en-us/services/app-service/web/) provide great support for this as discussed in the Infrastructure as Code section. Consider having at least one beta environment for your bot.

A common pattern when using Azure to host the bot is:
* Create both a Production and a Beta version of the bot in the Bot Framework dashboard, with their own different app id and secrets. Think about using a Microsoft account shared by those that will manage the bot, and set a team email group as the 2-factor authentication for it.
* Work as a team on shared source code repo(s), using branches as needed.
* Branches are constantly taken through continuous integration and unit testing to ensure the bot code builds and passes tests.
* When code is merged to main trunks like master, or when the team decides a deployment is needed, automated or continuous deployment deploys the ARM template to ensure consistency and with correct beta bot values being passed in, then deploys the bot code to the pre-release slot of the beta web app instance. Functional tests are run on it, then the pre-release slot is swapped with the main slot of the beta app. 
* Once the code is deployed and passes tests on the beta instance it is deployed to the pre-release of the production app service plan, functional tests run on it, then the slot is swapped. You can also introduce an approval process between beta and production as needed.

[![Source Code and Hosting](./media/bot-framework-devops/SourceWebAppsBotFramework.png)](./media/bot-framework-devops/SourceWebAppsBotFramework.png "Source Code and Hosting")

## Continuous Integration (CI)

Any system that supports automated builds of .NET and Node code can be used for continuous integration of your bot. [Visual Studio Team Services](https://www.visualstudio.com/team-services/continuous-integration/) is an example.

Some considerations for your bot in CI are:
* Any services and data sources that you own that are called by the bot can also be built and unit tested in this step (LUIS, Databases, APIs, etc.).
* Put bot configurations in environment variables. This way your bot code can be built only once, and during continuous deployment configured for each environment.
* Unit tests run during CI. See below in the Automated Testing section for more details. 

Here is a video covering this topic:
[Continuous Integration for the Bot Framework](https://channel9.msdn.com/Series/DevOps-for-the-Bot-Framework/Continuous-Integration-for-the-Bot-Framework)

## Automated Testing

There are Bot Framework specifics when it comes to unit and functional testing of bots.

### Unit Testing 

When unit testing bots it is important to mock the Bot Builder Connector and any LUIS models or other dependencies. During Continuous Integration, call these tests with mocks to make sure conversations with the bot are as expected. Ensure the development team write tests for any changes made to the bot dialogs and use the mocks in the tests. Here are some examples:
    - For Node.js bots [here is a blog post](https://www.microsoft.com/developerblog/real-life-code/2017/01/20/Bot-Framework-Unit-Testing.html) detailing one approach for mocking both the Bot Builder and LUIS. Here is a [bot example that includes unit tests for Node.js](https://github.com/nzthiago/BotInsightsTests/tree/master/Node). 
    - For .NET Here is a [bot example that includes unit tests and Functional Tests for .NET](https://github.com/nzthiago/BotInsightsTests/tree/master/CSharp).

### Functional Testing

Functional tests require your test code to access your deployed bot. The way to implement this is to use the Direct Line channel and it's [API](https://docs.botframework.com/en-us/restapi/directline3/) / [.NET SDK](https://www.nuget.org/packages/Microsoft.Bot.Connector.DirectLine). This means your tests will run against the deployed bot. Here is a [bot example that includes unit tests and Functional Tests for .NET](https://github.com/nzthiago/BotInsightsTests/tree/master/CSharp). Pay attention to the [From field of an Activity](https://github.com/nzthiago/BotInsightsTests/blob/master/CSharp/AppInsightsBot.FunctionalTests/BotHelper.cs#L57) - if you use the same value the bot will assume it's the same user going back every time. If you use different values, it will treat it as different users.
Your bot might also depend on an external OAuth or other types of authentication. You can create a simple app that uses Direct Line and use it to talk to the bot and authenticate once manually (until expiration) and save the OAuth token to the bot's private user data, like in [the AzureBot for example](https://github.com/Microsoft/AzureBot/tree/master/AzureBot.ConsoleConversation). In your functional tests you then set the same Activity From value as from the command line app. This way when the functional tests run, the OAuth for that user has been done already.

Here is a video covering this topic:
[Testing the Bot Framework](https://channel9.msdn.com/Series/DevOps-for-the-Bot-Framework/Testing-the-Bot-Framework)

## Continuous Deployment (CD)

Like Continuous Integration, Continuous Deployment is the same as for a regular Web API application written in .NET or Node. It should coordinate deploying the bot to web apps to multiple environments, as described in the Infrastructure as Code section, and using features of Azure or other providers for as little downtime as possible, then running functional tests. Keep in mind your bot will most likely have other external dependencies that should be taken into consideration. [Visual Studio Team Services' Release Management](https://www.visualstudio.com/team-services/release-management/) feature can be used to automate this for example. 
As discussed in the _Hosting, Scaling and Load Balancing Bots_ section, it is during Continuous Deployment that you can configure the web app environment with the right Bot Framework bot entry details, so your bot code knows to which bot in the Bot Framework it's connecting.
The bot user and conversation state ([C#](https://docs.botframework.com/en-us/csharp/builder/sdkreference/stateapi.html), [Node](https://docs.botframework.com/en-us/node/builder/guides/core-concepts/#adding-dialogs-and-memory)) between the user and the bot is stored within the bot framework services, and not locally within your own bot. The bot state will not be affected between deployments. You should therefore allow for clearing the bot state (like [/deleteprofile](https://docs.botframework.com/en-us/technical-faq/#my-bot-is-stuck--how-can-i-reset-the-conversation)) if you are changing the bot state values that your code depends on.
As part of CD you should also perform functional tests against each deployed version of your bot. See the _Functional Testing_ section for more details on this.

Here is a video covering this topic:
[Continuous Deployment and Release Management for the Bot Framework](https://channel9.msdn.com/Series/DevOps-for-the-Bot-Framework/Continuous-Deployment-and-Release-Management-for-the-Bot-Framework)

## Conversation Telemetry

If you don't collect usage data from your bot, you won't have a way to understand what it does well or not and how to improve it.  Make sure you include a link to your privacy statements on your bot's welcome message when collecting telemetry.
It is straightforward to add telemetry services to your bot, like [Application Insights](https://azure.microsoft.com/en-us/services/application-insights/). [Here](https://github.com/nzthiago/BotInsightsTests) is an example in C# and Node with telemetry collection. Just tracking the path users take inside the dialogs of your bot will already give you good insight, but you can define what a successful bot interactiion means and track it correctly in telemetry.
Once you have this data you can use tools like [Application Insights Analytics](https://docs.microsoft.com/en-us/azure/application-insights/app-insights-analytics), [Power BI](https://powerbi.microsoft.com/en-us/), or [create your own dashboards](https://github.com/CatalystCode/bot-fmk-dashboard) to report and understand how it's being used to then improve your bot.

Here is a video covering this topic:
[Telemetry for the Bot Framework](https://channel9.msdn.com/Series/DevOps-for-the-Bot-Framework/Telemetry-for-the-Bot-Framework)

## LUIS & Other Cognitive Services

You can [include cognitive services in your ARM templates](https://docs.microsoft.com/en-us/azure/templates/microsoft.cognitiveservices/accounts). Note that there are still some manual steps to make sure your LUIS model is using the right keys defined in Azure for example.

You can also export the source JSON of your LUIS model and check the JSON into version control with your bot code. Your development team can then modify the model and utterance lists, and in continuous deployment import it using the LUIS API and retrain the model. You can even have different versions of your bot pointing at different LUIS applications, so that the LUIS schema and the bot are in sync. Here is an [example class in C#](https://github.com/nzthiago/BotInsightsTests/blob/master/LuisClient.cs) to talk to the LUIS API to import/export/train a model.

Here is a video covering this and other topics:
[More Bot Demos and Advanced Considerations](https://channel9.msdn.com/Series/DevOps-for-the-Bot-Framework/More-Bot-Demos-and-Advanced-Considerations)

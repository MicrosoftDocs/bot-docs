
layout: page
title: Deploying to Azure
permalink: /en-us/node/builder/guides/deploying-to-azure/
weight: 2613
parent1: Bot Builder for Node.js
parent2: Guides


There are many ways to deploy your bot to Azure, depending on your situation. We're presenting three ways here that can get you started quickly.

* TOC
{:toc}


## Prerequisites

* In order to complete these steps, you need an [Azure Subscription](https://azure.microsoft.com/en-us/free/){:target="_blank"}.


## I want to setup continuous integration from my local git

This section assumes you have completed the [Core Concepts guide](/en-us/node/builder/guides/core-concepts/){:target="_blank"}. 

### Step 1: Install the Azure CLI

Open a command prompt, cd into your bot folder, and run the following command:

    npm install azure-cli -g

### Step 2: Create an Azure site, and configure it for Node.js/git

First of all, login into your Azure account. Type the following in your command prompt, and follow the instructions.

    azure login

Once logged in, type the following to create a new site, and configure it for Node.js and git

    azure site create --git <appname>

where *\<appname\>* is the name of the site you want to create. This will result in a url like *appname.azurewebsites.net*.

### Step 3: Commit your changes to git, and push to your Azure site

    git add .
    git commit -m "<your commit message>"
    git push azure master

You will be prompted to enter your deployment credentials. If you don't have them, you can configure them on the Azure Portal by following these simple steps:

1. Visit the [Azure Portal](http://portal.azure.com/){:target="_blank"}
2. Click on the site you've just created, and open the all settings blade
3. In the Publishing section, click on Deployment credentials, enter a username and password, and save  
![Deployment credentials](/en-us/images/builder/publishing-your-bot-deployment-credentials.png)
4. Go back to your command prompt, and enter the deployment credentials
5. Your bot will be deployed to your Azure site

### Step 4: Test the connection to your bot

[Test your bot with the Bot Framework Emulator](#test-your-azure-bot-with-the-bot-framework-emulator)

## I want to setup continuous integration from github

Let's use use the [sample bot](https://github.com/fuselabs/echobot){:target="_blank"} as a starter template for your own Node.js based bot.

*note: in the examples below, replace "echobotsample" with your bot ID for any settings or URLs.*

### Prerequisites

* Get a [Github](http://github.com) account

### Step 1: Get a github repo

For this tutorial we'll use the sample echobot repo. 
Fork the [echobot repo](https://github.com/fuselabs/echobot){:target="_blank"}.

### Step 2: Create an Azure web app

![Create an Azure web app](/en-us/images/builder/azure-create-webapp.png?raw=true)

### Step 3: Set up continuous deployment to Azure from your Github repo

You will be asked to authorize Azure access to your GitHub repo, and then choose your branch from which to deploy.

![Set up continuous deployment to Azure from your Github repo](/en-us/images/builder/azure-deployment.png?raw=true)

Verify the deployment has completed by visiting the web app. [http://echobotsample.azurewebsites.net/](https://echobotsample.azurewebsites.net/){:target="_blank"}. It may take a minute of two for the initial fetch and build from your repo.
![](/en-us/images/builder/azure-browse.png?raw=true)

### Step 4: Enter your temporary Bot Framework App ID and App Secret into Application settings

![Enter your Bot Framework App ID and App Secret into Azure settings](/en-us/images/builder/azure-secrets.png?raw=true)

* MICROSOFT_APP_ID
* MICROSOFT_APP_PASSWORD

*Note*: You'll change these values after you register your bot with the Bot Framework Developer Portal.

### Step 5: Test the connection to your bot

[Test your bot with the Bot Framework Emulator](#test-your-azure-bot-with-the-bot-framework-emulator)

## I want to deploy from Visual studio

### Step 1: Get the Bot Builder SDK samples

Clone the Bot Builder SDK Github repo, by opening a command prompt, choosing a location of your choice (e.g. c:\code), and typing the following:

    git clone https://github.com/Microsoft/BotBuilder/

### Step 2: Open the hello-AzureWebApp sample, install the missing npm packages, and configure the temporary appId and appSecret

Open the hello-AzureWebApp.sln solution in Visual Studio, right click on the npm folder, and click on "Install missing npm packages".

![Enter your Bot Framework App ID and App Secret into Azure settings](/en-us/images/builder/publishing-your-bot-install-npm.png)

When finished, open the Web.config, and edit it as follows:

{% highlight csharp %}
  <appSettings>
    <add key="BOTFRAMEWORK_APPID" value="appid" />
    <add key="BOTFRAMEWORK_APPSECRET" value="appsecret" />
  </appSettings>
{% endhighlight %}

*Note*: You'll change these values after you register your bot with the Bot Framework Developer Portal.

### Step 3: Publish to Azure

1. Right click on the hello-AzureWebApp project in solution explorer, and click on publish
2. Provide your Azure credentials, and then either create a new Web App or select an existing one (if you have created one through the portal)
3. Follow the publishing wizard, and click on Publish

### Step 4: Test the connection to your bot

[Test your bot with the Bot Framework Emulator](#test-your-azure-bot-with-the-bot-framework-emulator)

## Test your Azure bot with the Bot Framework Emulator

If you have not done it already, install the [Bot Framework Emulator](/en-us/tools/bot-framework-emulator/).
Start the Bot Framework Emulator, and paste the url of your newly deployed bot into the *Bot Url* field. Make sure that:

* The protocol is https 
* App Id and App Secret match the values you've set in code

![Bot Framework Emulator](/en-us/images/builder/publishing-your-bot-emulator.png)

[Learn more about the Bot Framework Emulator](/en-us/tools/bot-framework-emulator/).

## Next steps

* [Register your bot with the Bot Framework Developer Portal](/en-us/csharp/builder/sdkreference/gettingstarted.html) 

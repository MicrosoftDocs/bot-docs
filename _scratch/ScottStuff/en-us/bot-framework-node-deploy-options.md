---
layout: page
title: Setting up Continuous Integration with Azure
permalink: /en-us/node-deployment-options/
weight: 4026
parent1: Deploying your Bot to the Cloud
---


Azure allows continuous integration of your Git repository with your Azure deployment. With continuous integration, when you change and build your bot's code, the bot will automatically deploy to Azure. Here are a couple of options that can get you started quickly.

<span style="color:red">These options appear to be Node specific. Need to figure out .NET integration into this topic, and how the "Setting Up Continuous Integration" topic in Azure Bot Service fits into here.</span>

<div class="docs-text-note"><strong>IMPORTANT</strong>: You must have an Azure subscription before you can deploy your bot to Azure. For information about getting a free Azure subscription, see <a href="https://azure.microsoft.com/en-us/free/" target="_blank">Azure Subscription</a>.</div>


## Option 1: Setting up continuous integration from my local Git

### Step 1: Install the Azure Command-Line Interface

Open a command prompt and navigate to your bot's folder. Then, run the following command:

```
npm install azure-cli -g
```

### Step 2: Create an Azure site and configure it for Node.js and Git

First, run the following command to login to your Azure account.

```
azure login
```

Then, run the following command to create a new site and configure it for Node.js and Git.

    azure site create --git <appname>

Set *\<appname\>* to the name of the site you want to create. This will create an endpoint named  *appname.azurewebsites.net*.

### Step 3: Commit your changes to Git and push to your Azure site

    git add .
    git commit -m "<your commit message>"
    git push azure master

You will be prompted to enter your deployment credentials. If you don't have them, you can configure them on the Azure Portal by following these simple steps:

1. Visit the [Azure Portal](http://portal.azure.com/){:target="_blank"}
2. Click on the site you just created
3. Open the All Settings blade
4. In the **PUBLISHING** section, click **Deployment credentials** and enter a user name and password. Then, click **Save**.  
![Deployment credentials](/en-us/images/builder/publishing-your-bot-deployment-credentials.png)
5. Go back to your command prompt, and enter the deployment credentials
6. Your bot will be deployed to your Azure site

### Step 4: Test the connection to your bot

Test your deployment by using by using [Bot Framework Emulator](/en-us/emulator/). In the emulator, use the URL of the newly deployed bot. 

## Option 2: Setting up continuous integration from GitHub

This option uses the [echobot](https://github.com/fuselabs/echobot){:target="_blank"} sample as an example, and assumes that you have a [GitHub](http://github.com) account.

**NOTE: In the examples below, replace "echobotsample" with your bot's ID for any settings or URLs.**

### Step 1: Get a GitHub repo

Fork the [echobot](https://github.com/fuselabs/echobot){:target="_blank"} repo.

### Step 2: Create an Azure web app

![Create an Azure web app](/en-us/images/builder/azure-create-webapp.png?raw=true)

### Step 3: Set up continuous deployment to Azure from your GitHub repo

Authorize Azure access to your GitHub repo, and then choose the branch to deploy from.

![Set up continuous deployment to Azure from your Github repo](/en-us/images/builder/azure-deployment.png?raw=true)

To verify that the deployment completed, go to [http://echobotsample.azurewebsites.net/](https://echobotsample.azurewebsites.net/){:target="_blank"}. It may take a minute or two for the initial fetch and build from your repo.
![](/en-us/images/builder/azure-browse.png?raw=true)

### Step 4: Enter a temporary Bot Framework App ID and App Secret in Application settings

![Enter your Bot Framework App ID and App Secret into Azure settings](/en-us/images/builder/azure-secrets.png?raw=true)

* MICROSOFT_APP_ID
* MICROSOFT_APP_PASSWORD

**Note: You'll change these values after you register your bot with Bot Framework Developer Portal.**

### Step 5: Test the connection to your bot

Test your deployment by using [Bot Framework Emulator](/en-us/emulator/). In the emulator, use the URL of the newly deployed bot. 

## Option 3: Deploying from Visual Studio

### Step 1: Get the Bot Builder SDK samples

Create a folder for your local repo and navigate to it. Run the following command to clone the Bot Builder SDK GitHub repo.

```
git clone https://github.com/Microsoft/BotBuilder/
```

### Step 2: Open the hello-AzureWebApp sample, install the missing npm packages, and configure the temporary appId and appSecret

<span style="color:red"><< I don't see a hello-AzureWebApp sample, do you mean hello-ChatConnector? >></span>

In Visual Studio, navigate to BotBuilder/Node/examples/ and open the hello-AzureWebApp.sln solution. Right click on the npm folder and click **Install missing npm packages**.

![Enter your Bot Framework App ID and App Secret into Azure settings](/en-us/images/builder/publishing-your-bot-install-npm.png)

Next, open the Web.config and edit it as follows:

{% highlight csharp %}
  <appSettings>
    <add key="BOTFRAMEWORK_APPID" value="appid" />
    <add key="BOTFRAMEWORK_APPSECRET" value="appsecret" />
  </appSettings>
{% endhighlight %}

**NOTE: You'll change these values after you register your bot with Bot Framework Developer Portal.**

### Step 3: Publish to Azure

1. Right click on the hello-AzureWebApp project in solution explorer and click on publish
2. Provide your Azure credentials and then either create a new Web App or select an existing one (if you have created one through the portal)
3. Follow the publishing wizard and click on Publish

### Step 4: Test the connection to your bot

Test your deployment by using [Bot Framework Emulator](/en-us/emulator/). In the emulator, use the URL of the newly deployed bot. 

## Next steps

* Register your bot with Bot Framework Developer Portal (see [Registering a Bot](/en-us/registration)). 

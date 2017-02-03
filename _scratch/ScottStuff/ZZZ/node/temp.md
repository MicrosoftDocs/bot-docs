



## Option 3: Deploying from Visual Studio

### Step 1: Get the Bot Builder SDK samples

Create a folder for your local repo and navigate to it. Run the following command to clone the Bot Builder SDK GitHub repo.

```
git clone https://github.com/Microsoft/BotBuilder/
```

### Step 2: Open the hello-AzureWebApp sample, install the missing npm packages, and configure the temporary appId and appSecret

<< I don't see a hello-AzureWebApp sample, do you mean hello-ChatConnector? >>

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



&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&









# Payment Bot Sample

A sample bot showing how to integrate with Microsoft Wallet service for payment processing.


### Prerequisites

The minimum prerequisites to run this sample are:
* The latest update of Visual Studio 2015. You can download the community version [here](http://www.visualstudio.com) for free.
* Register your bot with the Microsoft Bot Framework. Please refer to [this](https://docs.botframework.com/en-us/csharp/builder/sdkreference/gettingstarted.html#registering) for the instructions. Once you complete the registration, update the [Bot's Web.config](PaymentsBot/Web.config#L9-L11) file with the registered config values (MicrosoftAppId and MicrosoftAppPassword).
* Make sure to keep your bot in preview mode.  Payments is NDA.
* Email your `Bot handle` to Bot Framework Paltform Team: `bfcoreplatform@microsoft.com` to have your bot whitelisted for payments.
* You need a Microsoft wallet merchant id. Once you have the merchant id, update the [Bot's Web.config](PaymentsBot/Web.config#L15) file with your designated merchat id.
* [Microsoft.Bot.Connector.Payments](https://fuselabs.visualstudio.com/PaymentSample/_packaging?feedName=packages&protocolType=NuGet&packageName=microsoft.bot.connector.payments&packageVersion=1.0.0&_a=view) nuget available from Fuse labs nuget feed: `https://fuselabs.pkgs.visualstudio.com/_packaging/packages/nuget/v3/index.json`. You can follow the instruction [here](https://go.microsoft.com/fwlink/?LinkID=698608) to setup this package source Visual Studio. NOTE: To access Fuse labs private nuget feed [here](https://fuselabs.visualstudio.com/PaymentSample/_packaging?feedName=packages&protocolType=NuGet&packageName=microsoft.bot.connector.payments&packageVersion=1.0.0&_a=view), view feed first via browser, then connect to feed and add URL in nuget package manager in VS


#### Microsoft Bot Builder

This sample has been developed based on Microsoft Bot Builder Dialog system. You can follow the following [sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/CSharp/core-MultiDialogs) to become familiar with different kind of dialogs and dialog stack in Bot Builder.

#### Microsoft Wallet Service

* Sign up with Stripe, and activate your account [here](https://dashboard.stripe.com/register).  Provide your personal info, including SSN, website URL, etc.
* Sign up at Seller Center [here](https://seller.microsoft.com/en-us/dashboard/registration/seller/?accountprogram=skypebots&setvar=fltsellerregistration:1)
* From Seller Center, connect with Stripe
* Navigate to your Dashboard in Seller Center (if not redirected) and note your MerchantID.  Use that MerchantID in your PaymentRequest

#### Publish
Also, in order to be able to run and test this sample you must [publish your bot, for example to Azure](https://docs.botframework.com/en-us/csharp/builder/sdkreference/gettingstarted.html#publishing). Alternatively, you can use [Ngrok to interact with your local bot in the cloud](https://blogs.msdn.microsoft.com/jamiedalton/2016/07/29/ms-bot-framework-ngrok/).

### Code Highlights

__[TBD]__


### Outcome

To run the sample, you'll need to publish Bot to Azure or use [Ngrok to interact with your local bot in the cloud](https://blogs.msdn.microsoft.com/jamiedalton/2016/07/29/ms-bot-framework-ngrok/).
* Running Bot app
    1. In the Visual Studio Solution Explorer window, right click on the **PaymentsBot** project.
    2. In the contextual menu, select Debug, then Start New Instance and wait for the _Web application_ to start.

You can use the webchat control in bot framework developer portal to interact with your bot.

![Sample Outcome](images/outcome.png)

### More Information

To get more information about how to get started in Bot Builder for .NET and Conversations please review the following resources:
* [Bot Builder for .NET](https://docs.botframework.com/en-us/csharp/builder/sdkreference/index.html)
* [Bot Framework FAQ](https://docs.botframework.com/en-us/faq/#i-have-a-communication-channel-id-like-to-be-configurable-with-bot-framework-can-i-work-with-microsoft-to-do-that)
* [Bot Builder samples](https://github.com/microsoft/botbuilder-samples)
* [Bot Framework Emulator](https://github.com/microsoft/botframework-emulator/wiki/Getting-Started)

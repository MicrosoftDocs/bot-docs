---
title: Designing Bots - Task Automation Bots | Bot Framework
description: Conversational applications that run simple task automation scenarios
services: Bot Framework
documentationcenter: BotFramework-Docs
author: matvelloso
manager: larar

ms.service: required
ms.devlang: may be required
ms.topic: article
ms.tgt_pltfrm: may be required
ms.workload: required
ms.date: 01/19/2017
ms.author: mat.velloso@microsoft.com

---
# Bot Design Center - Simple task automation bots



##When bots are just simple apps


We have previously discussed how bots can leverage elements such as rich user controls, text and voice. We also discussed that not all bots may use all these elements together. In fact, a large number of bots may not even have an actual "conversation" with the user at all: These bots resemble the most what typical apps and websites are today. They will likely have some user controls and help the user achieve a few automated tasks but whether that requires natural language - either in text or voice - is not necessarily a requirement.


##Hang on: A chatbot that can't have a conversation? Nonsense! 


Let us look at one theoretical scenario: The Contoso company receives several help desk calls from employees in need to reset their passwords. This is a problem because it overloads their help desk with a very repeatable task that could very easily be automated. In the meanwhile, other employees with more complex IT problems have to wait on the line to get help. Contoso would like to streamline the process of helping these employees who need to reset their password and someone suggested that a bot might be the right way to do that.

John, an experienced developer from Contoso, decided to write a "spec" of how this bot should work. As an app developer, John learned in the past that sometimes it is better to write a simple spec of what the user experience is supposed to be before trying to actually prototype the app. It turns out, that is not far from the truth when it comes to building bots as well, so John decided to give that a try and write a "bot spec document" using Microsoft Word.

In fact, you can download a [sample of the document John created here](https://trpp24botsamples.visualstudio.com/50bce30d-3609-423a-9337-b61cfbfea88f/_api/_versioncontrol/itemContent?repositoryId=110b267e-57e9-40d3-ba06-86aa2fae937f&path=%2FSpecs%2FSimple+Task+Automation+-+Design+Spec.docx&version=GBmaster&contentOnly=false&__v=5).

After discussing with the team, John realized he needed to define a navigation model, starting from a "RootDialog" where the users can request to reset their password, which would lead to a "ResetPasswordDialog". At that point, the bot will need to ask two pieces of information, which is the user's phone number and then the user's birth date. Again, this is just a theoretical example and very likely different companies would want apply a variety of more robust identity checks before going ahead resetting employees passwords.

In other words, John realized he needed a navigation map along these lines:

![Dialog Structure](../../media/designing-bots/capabilities/simple-task1.png)

As far as this point, one could argue that no big difference exists whether the solution to this problem will be a bot, an app or a website. 

John now needs to describe how each of these dialogs will look like. So he goes ahead and starts with the RootDialog:

![Dialog Structure](../../media/designing-bots/capabilities/simple-task2.png)

Here John decided that the experience will start with a simple menu offering two options: Change password (assuming the user knows the previous password) or reset password (in which case we need to validate the user in some way other than using the password before proceeding).

For the sake of simplicity, we are not going to implement the change password flow, but just the reset password flow for now.

Once the user chooses to reset the password, we are now redirected to the ResetPassword dialog:

![Dialog Structure](../../media/designing-bots/capabilities/simple-task3.png)

Note that the ResetPasswordDialog invokes two other dialogs, one for collecting the user's phone number and another for collecting the birth date. John decided to separate these tasks in their own dialogs. That doesn't have to be this way, but John imagined that this would not only simplify the code of every dialog but also increase the chances of having some of these dialogs to be reused in future scenarios and even other bots... just like John used to do with apps and websites. Again, little difference here.

Now for the PromptStringRegex dialog, we see that John added some alternate flows. Basically John is trying to protect the bot from the [stubborn bot scenario discussed earlier](../core/navigation.md#the-stubborn-bot) where the bot is lost at asking the same question in an eternal loop because the user is entering invalid data.

![Dialog Structure](../../media/designing-bots/capabilities/simple-task4.png)

As you can see, John applied the same experience he has with building for web and apps. John didn't introduce any advanced natural language processing to this point and the bot still does what is needed to help out the users.


##But this isn't smart enough :(


Let us revisit the topic of [what makes bots great](../index.md#what-makes-a-bot-great): 

Whether and how much the bot uses natural language or any other AI capability is less relevant to how useful the bot actually is. In fact, John may very well add natural language on top of the design he already did. The user could simply ask "I can't remember my password" and the bot could still direct the user to the same flows discussed above from there. But the key question John always had in mind is: "Am I solving the problem my users and my company asked me to solve?". User experience is what comes first.

The point of this sample was to show that not all bots may need natural language. They can, in fact, look a lot like apps. We also wanted to demonstrate how a developer can think about designing dialogs in a spec document. 


##Why not just building an app instead?


That is a great question to ask. Many developers may decide to build apps instead. In fact, many developers end up deciding to embed their bots into their apps, using [Bot Framework's DirectLine API](https://docs.botframework.com/en-us/restapi/directline3/#navtitle) or [Web Chat control](https://github.com/Microsoft/BotFramework-WebChat). This gets them the best of both worlds: A rich app experience and a conversational experience, all in one place.

But there is a cost: Building an app or website, making sure it runs on all clients and platforms, packaging, deploying, hoping users will download/install them... all these things add cost. A bot may be a much simpler way of solving the same problem.

A bot may also exist in places where apps can't reach: John may, in the future, add natural language and speech on top of these dialogs and have his bot answering phone calls. John may even have the bot work via text messages. The company may setup some kiosks in all the building floors and have the bot embedded into that experience to help out users who forgot their passwords. 

Having that as a bot gives John freedom to choose and extend.

So while we ask ourselves whether to build an application or a bot, perhaps in a not so distant future there will be no distinguishable difference between these options: Bots will look more like apps and apps will look more like bots.


##Show me the code!

If you want to see how the code for the design above looks like, take a look how John built a first prototype in [Node](https://trpp24botsamples.visualstudio.com/_git/Code?path=%2FNode%2Fcapability-SimpleTaskAutomation&version=GBmaster&_a=contents) and in [C#](https://trpp24botsamples.visualstudio.com/_git/Code?path=%2FCSharp%2Fcapability-SimpleTaskAutomation&version=GBmaster&_a=contents) 

You can read more about these samples [here](https://trpp24botsamples.visualstudio.com/_git/Code?path=%2FCSharp%2Fcapability-SimpleTaskAutomation%2FREADME.md&version=GBmaster&_a=contents) and see how the bot ended up looking like:

![Dialog Structure](../../media/designing-bots/capabilities/simple-task5.png)





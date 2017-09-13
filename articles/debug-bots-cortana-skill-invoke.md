---
title: Test a Cortana skill | Microsoft Docs
description: Learn how to test a Cortana bot by invoking a Cortana skill.
author: DeniseMak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 05/01/2017
ms.reviewer:
---

# Test a Cortana skill
 
If you've built a Cortana skill using the Bot Builder SDK, you can test it by invoking it from Cortana. The following instructions walk you through the steps required to try out your Cortana skill.

## Register your bot
Log in to the [Bot Framework Portal][BFPortal] using a [Microsoft account](https://account.microsoft.com/account). If you haven't [registered][Register] your bot yet, you can do so now.

> [!NOTE]
> To register a bot as Cortana skill, you must be signed in with a Microsoft account. 
> Signing up for an Outlook.com mailbox automatically creates a Microsoft account. 
> Support for work and school accounts is coming soon.

## Enable speech recognition priming
If your bot uses a Language Understanding Intelligent Service (LUIS) app, make sure you register the LUIS application ID. This helps your bot recognize spoken utterances that are defined in your LUIS model.

In the **Settings** panel, under **Configuration**, enter the LUIS application ID in the **Speech recognition priming with LUIS** text box. 
![Enable speech recognition priming](~/media/cortana/cortana-speech-luis-priming.png)

## Add the Cortana channel
Under **My bots**, select the bot you would like to connect to the Cortana channel. From the list of channels, click the button to add Cortana.

[Secure your bot](dotnet/bot-builder-dotnet-security.md) by configuring the **AppID** and **AppPassword** in the Bot Framework portal and in your **Web.config** file.

![Add the Cortana channel ](~/media/cortana/cortana-add.png)

> [!NOTE]
> You should set the messaging endpoint of your bot in the Bot Framework portal before connecting to Cortana. 

If you have already deployed your bot, make sure the **Messaging endpoint** is correctly set.

If you are running the bot locally, you can get an endpoint to use for testing by running tunneling software, such as [ngrok](https://ngrok.com). To use ngrok to get an endpoint, from a console window type: 
```cmd
 ngrok.exe http 3979 -host-header="localhost:3979"
``` 
This configures and displays an ngrok forwarding link that forwards requests to your bot, which is running on port 3978. The URL to the forwarding link should look something like this: `https://0d6c4024.ngrok.io`.  Append `/api/messages` to the link, to form an endpoint URL in this format: `https://0d6c4024.ngrok.io/api/messages`. Enter this endpoint URL in the **Configuration** section for your bot in the [Bot Framework Portal][BFPortal].

## Configure Cortana
When registering your bot with the Cortana channel, some basic information about your bot will be pre-filled into the registration form. Review this information carefully. This form consists of the following fields.

| Field | Description |
|------|------|
| **Bot icon** | An icon that is displayed in the Cortana canvas when your skill is invoked. This is also used where skills are discoverable (like the Microsoft store). The image should be a PNG that is 60 x 60 pixels and be no more than 30kb in size.|
| **Name** | The name of your Cortana skill is displayed to the user at the top of the visual UI. |
| **Invocation name** | This is the name users say when invoking a skill. It should be no more than three words and easy to pronounce. See the [Invocation Name Guidelines][InvocationNameGuidelines] for more information on how to choose this name.|
| **Description** | A description of your Cortana skill. This is used where skills are discoverable (like the Microsoft store). |
| **Short description** | A short description of your skill’s functionality, used to describe the skill in Cortana’s notebook. |

<!-- TODO: Update screenshot when new UI is available -->
![Fill out channel information](~/media/cortana/cortana-register.png)

## Request user profile data (Optional)
Cortana provides access to several different types of user profile information, that you can use to customize the bot for the user. 

> [!NOTE] 
> You can skip this step if you don't need to use user profile data in your bot.

To add user profile information, click the **Add a user profile request** link, then select the user profile information you want from the drop-down list. Add a friendly name to use to access this information from your bot's code. See [Cortana-specific entities][CortanaSpecificEntities] for more information on using these fields.

![Fill out user profile data](~/media/cortana/add-user-profile-data.png) 

## Manage user identity through connected services (Optional)
If your skill requires authentication, you can connect an account so that Cortana will require users to log in into your skill before they can use it. Currently, only **Auth Code Grant** authentication is supported, and **Implicit Grant** is not supported. See [Secure your skill with authentication][CortanaAuth] for more information. 

> [!NOTE] 
> You can skip this step if your bot doesn't require authentication.


## Connect to Cortana
When you are done filling out the registration form for your Cortana skill, click **Save**  to complete the connection. This brings you back to your bot's main page in the Bot Framework developer center and you should see that it is now connected to Cortana.

<!-- update image --> 
![Cortana is listed as a connected channel in the Bot Framework dashboard](~/media/cortana/cortana-edit.png)




## Test your Cortana skill using the Chat control
Now that your bot is available as a Cortana skill you should test it. At this point your bot has already been automatically deployed as a Cortana skill to your account. 

Click **Test** to open the **Chat** window in the Bot Framework portal and type a message to verify that your bot is working.

## Test your Cortana skill
You can invoke your Cortana skill by speaking an invocation phrase to Cortana. 
1. Open the Notebook within Cortana and click **About me** to see which account you're using for Cortana. Make sure you are signed in with the same Microsoft account that you used to register your bot. 
   ![Sign in to Cortana's notebook](~/media/cortana/cortana-notebook.png)
2. Click on the microphone button in the Cortana app or in the "Ask me anything" search box in Windows, and say your bot's [invocation phrase][InvocationNameGuidelines]. The invocation phrase includes an *invocation name*, which uniquely identifies the skill to invoke. For example, if a skill's invocation name is "Northwind Photo", a proper invocation phrase could include "Ask Northwind Photo to..." or "Tell Northwind Photo that...".

   You specify your bot's *Invocation Name* when you configure it for Cortana.
   ![Enter the invocation name when you configure the Cortana channel](~/media/cortana/cortana-invocation-name-callout.png)

3. If Cortana recognizes your invocation phrase, your bot launches in Cortana's canvas. 

## Troubleshoot

If your Cortana skill fails to launch, check the following:
* Make sure you are signed in to Cortana using the same Microsoft account that you used to register your bot in the Bot Framework Portal.
* Check if the bot is working by clicking **Test** to open the **Chat** window of the Bot Framework portal and typing a message to it.
* Check if your invocation name meets the [guidelines][InvocationNameGuidelines]. If your invocation name is longer than three words, hard to pronounce, or sounds like other words, Cortana might have difficulty recognizing it.
* If your skill uses a LUIS model, make sure you [enable speech recognition priming](https://aka.ms/prime-speech-luis).

See the [Enable Debugging of Cortana skills][Cortana-Debug] for additional troubleshooting tips and information on how to enable debugging of your skill in the Cortana dashboard. 



## Next steps

Once you have tested your Cortana skill and verified that it works the way you'd like it to, you can deploy it to a group of beta testers or release it to the public. See [Publishing Cortana Skills][Cortana-Publish] for more information.

## Additional resources
* [The Cortana Skills Kit][CortanaGetStarted]
* [Cortana Dev Center][CortanaDevCenter]
* [Testing and debugging best practices][Cortana-TestBestPractice]
* [Preview features with the Channel Inspector](portal-channel-inspector.md)

[CortanaGetStarted]: https://docs.microsoft.com/en-us/cortana/getstarted

[BFPortal]: https://dev.botframework.com/
[Register]: https://docs.microsoft.com/en-us/bot-framework/portal-register-bot
[CortanaDevCenter]: https://developer.microsoft.com/en-us/cortana

[CortanaSpecificEntities]: https://aka.ms/lgvcto
[CortanaAuth]: https://aka.ms/vsdqcj

[InvocationNameGuidelines]: https://aka.ms/cortana-invocation-guidelines 


[Cortana-Debug]: https://aka.ms/cortana-enable-debug
[Cortana-TestBestPractice]: https://aka.ms/cortana-test-best-practice
[Cortana-Publish]: https://aka.ms/cortana-publish









---
title: Test a Cortana skill | Microsoft Docs
description: Learn how to test a Cortana bot by invoking a Cortana skill.
keywords: Bot Framework SDK, register your bot, cortana
author: v-ducvo
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/01/18
monikerRange: 'azure-bot-service-3.0'
---

# Test a Cortana skill

[!INCLUDE [pre-release-label](includes/pre-release-label-v3.md)]
 
If you've built a Cortana skill using the Bot Framework SDK, you can test it by invoking it from Cortana. The following instructions walk you through the steps required to try out your Cortana skill.

## Register your bot
If you [created your bot](~/bot-service-quickstart.md) using Bot Service in Azure, then your bot is already registered and you can skip this step.

If you have deployed your bot elsewhere or if you want to test your bot locally, then you must [register](bot-service-quickstart-registration.md) your bot so that you can connect it to Cortana. During the registration process, you will need to provide your bot's **Messaging endpoint**. If you choose to test your bot locally, you will need to run a tunneling software such as [ngrok](http://ngrok.com) to get an endpoint for your local bot.

## Get messaging endpoint using ngrok

If you are running the bot locally, you can get an endpoint to use for testing by running tunneling software, such as [ngrok](https://ngrok.com). To use ngrok to get an endpoint, from a console window type: 

```cmd
 ngrok.exe http 3979 -host-header="localhost:3979"
``` 

This configures and displays an ngrok forwarding link that forwards requests to your bot, which is running on port 3978. The URL to the forwarding link should look something like this: `https://0d6c4024.ngrok.io`.  Append `/api/messages` to the link, to form a messaging endpoint URL in this format: `https://0d6c4024.ngrok.io/api/messages`. 

Enter this endpoint URL in the **Configuration** section of your bot's [Settings](~/bot-service-manage-settings.md) blade.

## Enable speech recognition priming
If your bot uses a Language Understanding (LUIS) app, make sure you associate the LUIS application ID with your registered bot service. This helps your bot recognize spoken utterances that are defined in your LUIS model. For more information, see [Speech priming](~/bot-service-manage-speech-priming.md).

## Add the Cortana channel
To add Cortana as a channel, follow steps listed in [Connect a bot to Cortana](bot-service-channel-connect-cortana.md).

## Test using Web Chat control

To test your bot using the integrated web chat control in Bot Service, click **Test in Web Chat** and type a message to verify that your bot is working.

## Test using emulator

To test your bot using the [emulator](~/bot-service-debug-emulator.md), do the following:

1. Run the bot.
2. Open the emulator and fill in the necessary information. To find your bot's **AppID** and **AppPassword**, see [MicrosoftAppID and MicrosoftAppPassword](bot-service-manage-overview.md#microsoftappid-and-microsoftapppassword). 
3. Click **Connect** to connect the emulator to your bot.
4. Type a message to verify that your bot is working.

## Test using Cortana
You can invoke your Cortana skill by speaking an invocation phrase to Cortana. 
1. Open Cortana.
2. Open the Notebook within Cortana and click **About me** to see which account you're using for Cortana. Make sure you are signed in with the same Microsoft account that you used to register your bot. 
   ![Sign in to Cortana's notebook](~/media/cortana/cortana-notebook.png)
2. Click on the microphone button in the Cortana app or in the "Ask me anything" search box in Windows, and say your bot's [invocation phrase][InvocationNameGuidelines]. The invocation phrase includes an *invocation name*, which uniquely identifies the skill to invoke. For example, if a skill's invocation name is "Northwind Photo", a proper invocation phrase could include "Ask Northwind Photo to..." or "Tell Northwind Photo that...".

   You specify your bot's *Invocation Name* when you configure it for Cortana.
   ![Enter the invocation name when you configure the Cortana channel](~/media/cortana/cortana-invocation-name-callout.png)

3. If Cortana recognizes your invocation phrase, your bot launches in Cortana's canvas. 

## Troubleshoot

If your Cortana skill fails to launch, check the following:
* Make sure you are signed in to Cortana using the same Microsoft account that you used to register your bot in the Bot Framework Portal.
* Check if the bot is working by clicking **Test in Web chat** to open the **Chat** window and typing a message to it.
* Check if your invocation name meets the [guidelines][InvocationNameGuidelines]. If your invocation name is longer than three words, hard to pronounce, or sounds like other words, Cortana might have difficulty recognizing it.
* If your skill uses a LUIS model, make sure you [enable speech recognition priming](~/bot-service-manage-speech-priming.md).

See the [Enable Debugging of Cortana skills][Cortana-TestBestPractice] for additional troubleshooting tips and information on how to enable debugging of your skill in the Cortana dashboard. 


## Next steps

Once you have tested your Cortana skill and verified that it works the way you'd like it to, you can deploy it to a group of beta testers or release it to the public. See [Publishing Cortana Skills][Cortana-Publish] for more information.

## Additional resources
* [The Cortana Skills Kit][CortanaGetStarted]

[CortanaGetStarted]: /cortana/getstarted

[BFPortal]: https://dev.botframework.com/
[CortanaDevCenter]: https://developer.microsoft.com/en-us/cortana

[CortanaSpecificEntities]: https://aka.ms/lgvcto
[CortanaAuth]: https://aka.ms/vsdqcj

[InvocationNameGuidelines]: https://aka.ms/cortana-invocation-guidelines 


[Cortana-Debug]: https://aka.ms/cortana-enable-debug
[Cortana-TestBestPractice]: https://aka.ms/cortana-test-best-practice
[Cortana-Publish]: /cortana/skills/publish-skill

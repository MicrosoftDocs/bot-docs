To fully test a bot that requests payment, you must first [deploy](~/publish-bot-overview.md) it 
to the cloud. Your bot will not be able to receive the HTTP callbacks if it is running locally. 
After you have deployed your bot to the cloud, [configure](~/portal-configure-channels.md) 
it to run on channels that support Bot Framework payments, like Web Chat and Skype. 
Since the [Bot Framework Emulator](~/debug-bots-emulator.md) does not currently support 
request payment functionality, you can test your bot using either Web Chat or Skype. 

> [!TIP]
> Callbacks are sent to your bot when a user changes data or clicks **Pay** during the payment web experience. 
> Therefore, you can test your bot's ability to receive and process callbacks by interacting with the payment web experience yourself.

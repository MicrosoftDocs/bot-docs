The <a href="https://github.com/Microsoft/BotFramework-WebChat" target="_blank">open source web chat control</a> 
communicates with bots by using the [Direct Line API](https://docs.botframework.com/en-us/restapi/directline3/#navtitle), 
which allows `activities` to be sent back and forth between client and bot. 
The most common type of activity is `message`, but there are other types as well. 
For example, the activity type `typing` indicates that a user is typing or that the bot is working to compile a response. 

You can use the backchannel mechanism to exchange information between client and bot without presenting it to the user by setting the activity type to `event`. The web chat control will automatically ignore any activities where `type="event"`.
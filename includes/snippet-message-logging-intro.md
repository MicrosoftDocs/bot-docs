By using the concept of **middleware** in the Bot Builder SDK, 
you can enable your bot to intercept all messages that are exchanged between user and bot. 
For each message that is intercepted, you may choose to do things such as 
save the message to a data store that you specify (thereby creating a conversation log) or 
inspect the message in some way and take whatever action your code specifies. 

> [!NOTE]
> The Bot Framework does not automatically save conversation details, 
> as doing so could potentially capture private information that bots and users do not wish to share 
> with outside parties. 
> If your bot saves conversation details, 
> it should communicate that to the user and describe what will be done with the data.
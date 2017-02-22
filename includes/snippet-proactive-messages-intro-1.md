Typically, each message that a bot sends to the user directly relates to the user's prior input. 
However, in some cases, a bot may need to send the user a message that is not directly related to the current topic of conversation. 
We call these messages **proactive messages**. 

Proactive messages can be useful in a variety of scenarios. 
If a bot sets a timer or reminder, it will need to notify the user when the time arrives. 
Or, if a bot receives a notification from an external system, it may need to communicate that information to the user. 
For example, if the user has previously asked the bot to monitor the price of a product, 
the bot will want to alert the user if it receives notification that the price of the product has dropped by 20%. 
Or, if a bot requires some time to compile a response to the user's question, it may inform the user of that and allow the conversation to continue in the meantime. 
Minutes (or even days) later when the bot finishes compiling a response to the question, it will want to share 
that information with the user. 

> [!NOTE] 
> When implementing proactive messages in your bot, consider the following best practices:
> <ul><li>Avoid sending several proactive messages within a short amount of time. Some channels enforce restrictions on how frequently a bot can send messages to the user, and will disable the bot if it violates those restrictions.</li>
> <li>Avoid sending proactive messages to users who have not previously interacted with the bot or solicited contact with the bot through another means such as e-mail or SMS.</li></ul>

Users commonly attempt to access certain functionality within a bot by using keywords like "help", "cancel", or "start over". 
Often, they will do so in the midst of a conversation, when the bot is expecting an altogether different response. 
By implementing **global message handlers**, you can design your bot to gracefully handle such requests.
The handlers will examine user input for the keywords that you specify (ex: "help", "cancel", "start over", etc.) and respond appropriately, 
as shown in the following example: 

![how users talk](~/media/designing-bots/capabilities/trigger-actions.png)

> [!NOTE]
> By defining the logic in <b>global message handlers</b>, you're making it accessible to all dialogs. 
> Using this approach, individual dialogs and prompts can be made to safely ignore the keywords, if necesssary.

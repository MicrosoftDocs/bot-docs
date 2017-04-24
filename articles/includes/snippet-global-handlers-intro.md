Users commonly attempt to access certain functionality within a bot by using keywords like "help", "cancel", or "start over". 
This often occurs in the middle of a conversation, when the bot is expecting a different response. 
By implementing **global message handlers**, you can design your bot to gracefully handle such requests.
The handlers will examine user input for the keywords that you specify, such as "help", "cancel", or "start over", and respond appropriately. 

![how users talk](~/media/designing-bots/capabilities/trigger-actions.png)

> [!NOTE]
> By defining the logic in global message handlers, you're making it accessible to all dialogs. 
> Individual dialogs and prompts can be configured to safely ignore the keywords.

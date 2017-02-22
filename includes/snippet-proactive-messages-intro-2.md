An **ad hoc proactive message** is the simplest type of proactive message. 
The bot simply interjects the message into the conversation whenever it is triggered, 
without any regard for whether the user is currently engaged in a separate 
topic of conversation with the bot and will not attempt to change the conversation in any way. 

A **dialog-based proactive message** is more complex than an ad hoc proactive message. 
Before it can inject this type of proactive message into the conversation, 
the bot must identify the context of the existing conversation and decide how 
it will handle that conversation when the message interrupts. 

For example, consider a bot that needs to initiate a survey at a given point in time. 
When that time arrives, the bot must stop the existing conversation with the user (if any), and 
redirect the user to a "SurveyDialog". The SurveyDialog is added to the top of the dialog stack and takes 
control of the conversation. 
When the user finishes all required tasks at the SurveyDialog, the SurveyDialog closes and is thereby removed 
from the dialog stack, returning the user to where they were before they were interupted by the survey. 

As shown in this example, a dialog-based proactive message is more than just simple notification. 
In sending the notifiction, the bot changes the topic of the existing conversation. 
It then must decide whether to resume that conversation later, or to abandon that conversation altogether (by reseting the dialog stack). 

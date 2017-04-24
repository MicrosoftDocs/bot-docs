This diagram shows the screen flow of a traditional application compared to the dialog flow of a bot. 

![bot](~/media/designing-bots/core/dialogs-screens.png)

In a traditional application, everything begins with the **main screen**.
The **main screen** invokes the **new order screen**.
The **new order screen** remains in control until it either closes or invokes other screens. 
If the **new order screen** closes, the user is returned to the **main screen**.

In a bot, everything begins with the **root dialog**. 
The **root dialog** invokes the **new order dialog**. 
At that point, the **new order dialog** takes control of the conversation and remains in control until it either closes or invokes other dialogs. 
If the **new order dialog** closes, control of the conversation is returned back to the **root dialog**.
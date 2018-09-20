Typically, each message that a bot sends to the user directly relates to the user's prior input.
In some cases, a bot may need to send the user a message that is not directly related to the current topic of conversation or to the last message the user sent. These types of messages are called **proactive messages**.

Proactive messages can be useful in a variety of scenarios.
If a bot sets a timer or reminder, it will need to notify the user when the time arrives.
Or, if a bot receives a notification from an external system, it may need to communicate that information to the user immediately.
For example, if the user has previously asked the bot to monitor the price of a product, the bot can alert the user if the price of the product has dropped by 20%. Or, if a bot requires some time to compile a response to the user's question, it may inform the user of the delay and allow the conversation to continue in the meantime. When the bot finishes compiling the response to the question, it will share that information with the user.

When implementing proactive messages in your bot:

- *Don't* send several proactive messages within a short amount of time. Some channels enforce restrictions on how frequently a bot can send messages to the user, and will disable the bot if it violates those restrictions.
- *Don't* send proactive messages to users who have not previously interacted with the bot or solicited contact with the bot through another means such as e-mail or SMS.

Proactive messages can create unexpected behavior. Consider the following scenario.

![how users talk](~/media/designing-bots/capabilities/proactive1.png)

In this example, the user has previously asked the bot to monitor prices of a hotel in Las Vegas.
The bot launched a background monitoring task, which has been running continuously for the past several days.
In the conversation, the user is currently booking a trip to London when the background task triggers a notification message about a discount for the Las Vegas hotel. The bot interjects this information into the conversation, making for a confusing user experience.

How should the bot have handled this situation?

- Wait for the current travel booking to finish, then deliver the notification. This approach would be minimally disruptive, but the delay in communicating the information might cause the user to miss out on the low-price opportunity for the Las Vegas hotel.
- Cancel the current travel booking flow and deliver the notification immediately. This approach delivers the information in a timely fashion but would likely frustrate the user by forcing them start over with their travel booking.
- Interrupt the current booking, clearly change the topic of conversation to the hotel in Las Vegas until the user responds, and then switch back to the in-progress travel booking and continue from where it was interrupted. This approach may seem like the best choice, but it introduces complexity both for the bot developer and the user.

Most commonly, your bot will use some combination of **ad hoc proactive messages** and other techniques to handle situations like this.

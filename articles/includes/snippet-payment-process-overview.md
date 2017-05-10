## Payment process overview

The payment process comprises three distinct parts:

1. The bot sends a payment request.

2. The user signs in with a Microsoft account to provide payment, shipping, and contact information. Callbacks are sent to the bot to indicate when the bot needs to perform certain operations (update shipping address, update shipping option, complete payment).

3. The bot processes the callbacks that it receives, including shipping address update, shipping option update, and payment complete. 

Your bot must implement only step one and step three of this process; step two takes place outside the context of your bot. 

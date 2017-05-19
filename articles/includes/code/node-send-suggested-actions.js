// <sendSuggestedActions>
var msg = new builder.Message(session)
	.text("Thank you for expressing interest in our premium golf shirt! What color of shirt would you like?")
	.suggestedActions(
		builder.SuggestedActions.create(
				session, [
					builder.CardAction.imBack(session, "productId=1&color=green", "Green"),
					builder.CardAction.imBack(session, "productId=1&color=blue", "Blue"),
					builder.CardAction.imBack(session, "productId=1&color=red", "Red")
				]
			));
session.send(msg);
// </sendSuggestedActions>


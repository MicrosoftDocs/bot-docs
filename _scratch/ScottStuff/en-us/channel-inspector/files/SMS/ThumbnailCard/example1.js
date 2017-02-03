function CreateHeroCard(session, builder, title, subtitle, text, url, buttons){
	var card = new builder.HeroCard(session)
		.title(title)
		.subtitle(subtitle)
		.text(text)		
		.images([builder.CardImage.create(session, url)])
		.buttons(buttons);		
	return card;
}

function BotSendThumbnail(session, builder){
	var buttons = [];
	buttons.push(builder.CardAction.imBack(session, "Thumbs Up", "I like it"));
	buttons.push(builder.CardAction.imBack(session, "Thumbs Down", "I don't like it"));	
	buttons.push(builder.CardAction.openUrl(session, "https://www.bing.com/images/search?q=bender&qpvt=bender&qpvt=bender&qpvt=bender&FORM=IGRE", "I feel lucky"));

	var card = CreateHeroCard(session, builder, "Surface Pro 4", "Surface Pro 4 is a powerful, versatile, lightweight laptop.", 
							"Surface does more. Just like you. For one device that does everything, you need more than a mobile OS.",
							"https://compass-ssl.surface.com/assets/b7/3c/b73ccd0e-0e08-42b5-9f8d-9143223eafd0.jpg?n=Hero-panel-image-gallery_03.jpg",
							buttons);
	card.tap(builder.CardAction.imBack(session, "Tapped it!", "Tapped it!"));

	var attachments = [];
	attachments.push(card);
	var msg = new builder.Message(session)
	.text("Thumbnail Card about Surface Pro 4")
	.textFormat(builder.TextFormat.markdown)
	.attachments(attachments);

	session.send(msg);
}

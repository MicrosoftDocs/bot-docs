function CreateHeroCard(session, builder, title, subtitle, text, url, buttons){
	var card = new builder.HeroCard(session)
		.title(title)
		.subtitle(subtitle)
		.text(text)		
		.images([builder.CardImage.create(session, url)])
		.buttons(buttons);		
	return card;
}

function BotSendHero(session, builder){
	var buttons = [];	
	buttons.push(builder.CardAction.imBack(session, "Thumbs Up", "Thumbs Up"));
	buttons.push(builder.CardAction.imBack(session, "Thumbs Down", "Thumbs Down"));
	buttons.push(builder.CardAction.openUrl(session, "https://www.bing.com/images/search?q=bender&qpvt=bender&qpvt=bender&qpvt=bender&FORM=IGRE", "I feel lucky"));	 
	
	var attachments = [];
	var card = CreateHeroCard(session, builder, "Surface Pro 4", "Surface Pro 4 is a powerful, versatile, lightweight laptop.", 
							"Surface does more. Just like you. For one device that does everything, you need more than a mobile OS. Surface Pro 4 has a laptop class keyboard,- full-size USB 3.0, microSD- card reader, and a Mini DisplayPort – and it runs full professional-grade software.",
							"https://compass-ssl.surface.com/assets/b7/3c/b73ccd0e-0e08-42b5-9f8d-9143223eafd0.jpg?n=Hero-panel-image-gallery_03.jpg",  
							buttons);
							
	attachments.push(card);
												
	var msg = new builder.Message(session)
	.attachments(attachments);
	session.send(msg);
}

function CreateHeroCard(session, builder, title, subtitle, text, url, buttons){
	var card = new builder.HeroCard(session)
		.title(title)
		.subtitle(subtitle)
		.text(text)		
		.images([builder.CardImage.create(session, url)])
		.buttons(buttons);		
	return card;
}

function BotSendCarousel(session, builder){
	var buttons = [];
	buttons.push(builder.CardAction.imBack(session, "Place to buy", "Places To Buy"));
	buttons.push(builder.CardAction.imBack(session, "Related Products", "Related Products"));
	
	var atts = [];
	atts.push(CreateHeroCard(session, builder, "Details about image 1", "",
	 								  "Price: $XXX.XX USD", 
								 	  "https://compass-ssl.surface.com/assets/b7/3c/b73ccd0e-0e08-42b5-9f8d-9143223eafd0.jpg?n=Hero-panel-image-gallery_03.jpg", 
								      buttons));
	atts.push(CreateHeroCard(session, builder, "Details about image 2", "", 
									  "Price: $XXX.XX USD", 
									  "https://compass-ssl.surface.com/assets/b5/82/b582f7f9-93ee-4ce8-971f-aacf5426decb.jpg?n=Hero-panel-image-gallery_02.jpg", 
									  buttons));
	atts.push(CreateHeroCard(session, builder, "Details about image 3", "",
									  "Price: $XXX.XX USD", 
									  "https://compass-ssl.surface.com/assets/9d/8f/9d8fcc7b-0b81-487a-9e2f-97d83a49666a.jpg?n=Hero-panel-image-gallery_06.jpg", 
									  buttons));
	atts.push(CreateHeroCard(session, builder, "Details about image 4", "",
									  "Price: $XXX.XX USD", 
									  "https://compass-ssl.surface.com/assets/d6/b1/d6b1b79f-db1d-44f1-98ba-b822b7744940.jpg?n=Hero-panel-image-gallery_01.jpg", 
									  buttons));	
	
	var msg = new builder.Message(session)
	.attachmentLayout(builder.AttachmentLayout.carousel)
	.attachments(atts);
	session.send(msg);
}

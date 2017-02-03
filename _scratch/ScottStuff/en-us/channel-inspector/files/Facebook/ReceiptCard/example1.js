function CreateReceiptItem(session, builder, price, title, subtitle, text, url){
	var receipt_item = new builder.ReceiptItem.create(session, price, title)
	.image(builder.CardImage.create(session, url))
	.subtitle(subtitle)
	.text(text);

	return receipt_item;
}

function CreateReceiptCard(session, builder, title, items, facts, buttons, tax, total){
	var receipt_card = 	new builder.ReceiptCard(session)
		.title(title)	
		.items(items)
		.facts(facts)
		.buttons(buttons)
		.tax(tax)
		.total(total);

	return receipt_card;
}

function BotSendReceipt(session, builder){
	var buttons = [];
	buttons.push(builder.CardAction.imBack(session, "Thumbs Up", "I like it"));
	buttons.push(builder.CardAction.imBack(session, "Thumbs Down", "I don't like it"));	

	var items = [];
	items.push(CreateReceiptItem(session, builder, "$XXX", "Surface Pro 4", 	
								"Surface Pro 4 is a powerful, versatile, lightweight laptop", 
								"Surface does more. Just like you. For one device that does everything, you need more than a mobile OS. ", 
								"https://compass-ssl.surface.com/assets/b7/3c/b73ccd0e-0e08-42b5-9f8d-9143223eafd0.jpg?n=Hero-panel-image-gallery_03.jpg"));    
	
	items.push(CreateReceiptItem(session, builder, "$XXX", "Label AAAA", "", "", "https://compass-ssl.surface.com/assets/b7/3c/b73ccd0e-0e08-42b5-9f8d-9143223eafd0.jpg?n=Hero-panel-image-gallery_03.jpg"));
	items.push(CreateReceiptItem(session, builder, "$XXX", "Label BBBB", "", "", "https://compass-ssl.surface.com/assets/b7/3c/b73ccd0e-0e08-42b5-9f8d-9143223eafd0.jpg?n=Hero-panel-image-gallery_03.jpg"));

	var facts = [];
	facts.push(builder.Fact.create(session, "XXXXXXXXXX", "Order Number"));
	facts.push(builder.Fact.create(session, "XXXX.XX.XX", "Expected Delivery Time"));
	facts.push(builder.Fact.create(session, "XXXX XXXX", "Payment Method"));
	facts.push(builder.Fact.create(session, "Address, XXXXX", "Delivery Address"));

	var receipt_card = CreateReceiptCard(session, builder, "Surface Pro 4", items, facts, buttons,"X.XX", "0.01");	//changing TOTAL price  FACEBOOK! "$XX.XX", "$0.01"

	var msg = new builder.Message(session)
	.attachments([receipt_card]);

	session.send(msg);
}

function CreateHeroCardWithButtons(session, builder, title, text, buttons){
	var card = new builder.HeroCard(session)
		.title(title)
		.text(text)
		.buttons(buttons);
	var msg = new builder.Message(session).attachments([card]);
	return msg;
}

function BotSendHelp(session, builder, _commands){
	var buttons = [];
	_commands.forEach(function(command){
		buttons.push(builder.CardAction.imBack(session, command, command));
	});
	var card  = CreateHeroCardWithButtons(session, builder, "Commands", "list of commands as buttons", buttons);
	session.send(card);
}

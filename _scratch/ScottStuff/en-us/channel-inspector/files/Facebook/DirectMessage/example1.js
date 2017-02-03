function BotSendDirectMessage(session, builder){
	var user = (session.message.address.user.name || session.message.address.user.id);
	var msg =  new builder.Message(session)
	.text("This is a direct message to " + user + " : " + session.message.text);
	session.send(msg);
}

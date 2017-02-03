function BotSendList(session, _commands){
	var sb = "";
	_commands.forEach(function(command){
		sb += "* **" + command + "** - " + command + "\n";
	});
	session.send(sb);
}

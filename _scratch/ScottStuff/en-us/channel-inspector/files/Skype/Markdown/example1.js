function BotSendMarkdown(session, builder){
	fs.readFile('sample.md', function (err, data) {
		if (err) {
			return console.error(err);
		}
		var msg = new builder.Message(session)
		.text(data.toString())
		.textFormat(builder.TextFormat.markdown);
		session.send(msg);
	});	
}

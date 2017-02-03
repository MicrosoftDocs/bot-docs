function BotSendDocument(session, builder){
	var msg = new builder.Message(session).attachments([{
		name: "Kodu Help.docx",
		contentType: "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
		contentUrl: "https://kodu.blob.core.windows.net/kodu/KODU%20Help.docx"
	}]);
	session.send(msg);
}

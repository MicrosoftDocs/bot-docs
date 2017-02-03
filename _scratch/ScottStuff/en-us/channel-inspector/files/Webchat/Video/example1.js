function BotSendVideo(session, builder){	
	var msg = new builder.Message(session)
	.text("Video 1")
	.attachments([{
		contentType: "video/mp4",
		name: "The New Microsoft Surface Book",
		contentUrl: "http://video.ch9.ms/ch9/36cb/c42dc883-9ed7-4f89-9a3b-b40296c036cb/thenewmicrosoftsurfacebook.mp4" 
	}]);
	session.send(msg);

	msg = new builder.Message(session)
	.text("Video 2")
	.attachments([{
		contentType: "video/mp4",
		name: "The New Microsoft Band",
		contentUrl: "http://video.ch9.ms/ch9/08e5/6a4338c7-8492-4688-998b-43e164d908e5/thenewmicrosoftband2_mid.mp4"
	}]);
	session.send(msg);
}

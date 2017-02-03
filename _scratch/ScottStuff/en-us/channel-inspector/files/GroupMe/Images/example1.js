function BotSendImages(session, builder){
	session.send("Here's information on Surface ");
	var msg = new builder.Message(session).attachments([
		{
			contentType: "image/jpeg",
			contentUrl: "https://compass-ssl.surface.com/assets/b7/3c/b73ccd0e-0e08-42b5-9f8d-9143223eafd0.jpg?n=Hero-panel-image-gallery_03.jpg"
		},
		{
			contentType: "image/png",
			contentUrl: "https://compass-ssl.surface.com/assets/12/99/12990c93-f90a-4e08-a609-de0e11713c2e.png?n=Surface2_80.png-new"			
		},
		{
			contentType: "image/gif",
			contentUrl: "https://media.giphy.com/media/nWuq1rhymfQUU/giphy.gif"
		}
	]);
	session.send(msg);
}

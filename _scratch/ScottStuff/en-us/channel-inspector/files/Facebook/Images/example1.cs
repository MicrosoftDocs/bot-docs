public async Task ExecuteCommand(ActivityContext activityContext, Activity activity)
{
	IMessageActivity iMessage = (IMessageActivity)activity;

	var reply = activity.CreateReply($"{activityContext.GetCounters()} {iMessage.Text}");
	reply.Text = "Here's information on Surface Pro 4 ";

	if (reply.Attachments == null)
		reply.Attachments = new List<Attachment>();

	// send image inline
	WebClient webClient = new WebClient();
	byte[] data = webClient.DownloadData("https://compass-ssl.surface.com/assets/b7/3c/b73ccd0e-0e08-42b5-9f8d-9143223eafd0.jpg?n=Hero-panel-image-gallery_03.jpg");
	var image64 = "data:image/jpeg;base64," + Convert.ToBase64String(data);

	reply.Attachments.Add(new Attachment()
	{
		ContentUrl = image64,
		ContentType = "image/jpeg",
	});

	reply.Attachments.Add(new Attachment()
	{
		ContentUrl = "https://compass-ssl.surface.com/assets/12/99/12990c93-f90a-4e08-a609-de0e11713c2e.png?n=Surface2_80.png-new",
		ContentType = "image/png"
	});
	await SendResponse(activityContext, reply).DropContext();
}

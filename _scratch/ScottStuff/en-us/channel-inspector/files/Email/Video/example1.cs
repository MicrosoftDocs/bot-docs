public async Task ExecuteCommand(ActivityContext activityContext, Activity activity)
{
	IMessageActivity iMessage = (IMessageActivity)activity;
	var reply = activity.CreateReply();
	if (reply.Attachments == null)
	{
		reply.Attachments = new List<Attachment>();
	}
	reply.Attachments.Add(new Attachment()
	{
		ContentUrl = "http://video.ch9.ms/ch9/36cb/c42dc883-9ed7-4f89-9a3b-b40296c036cb/thenewmicrosoftsurfacebook.mp4",
		ContentType = "video/mp4",
		Name = "The New Microsoft Surface Book"
	});
	await SendResponse(activityContext, reply).DropContext();


	reply = activity.CreateReply();
	if (reply.Attachments == null)
	{
		reply.Attachments = new List<Attachment>();
	}
	reply.Attachments.Add(new Attachment()
	{
		ContentUrl = "http://video.ch9.ms/ch9/08e5/6a4338c7-8492-4688-998b-43e164d908e5/thenewmicrosoftband2_mid.mp4",
		ContentType = "video/mp4",
		Name = "The New Microsoft Band"
	});
	await SendResponse(activityContext, reply).DropContext();
}

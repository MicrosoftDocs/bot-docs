public async Task ExecuteCommand(ActivityContext activityContext, Activity activity)
{
	IMessageActivity iMessage = (IMessageActivity)activity;
	var reply = activity.CreateReply($"{activityContext.GetCounters()} {iMessage.Text}");
	reply.Attachments = reply.Attachments ?? new List<Attachment>();
	reply.Attachments.Add(new Attachment()
	{
		ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
		ContentUrl = "https://kodu.blob.core.windows.net/kodu/KODU%20Help.docx",
		Name = "Kodu Help.docx"
	});

	await SendResponse(activityContext, reply).DropContext();
}

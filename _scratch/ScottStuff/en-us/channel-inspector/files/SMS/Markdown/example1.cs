public async Task ExecuteCommand(ActivityContext activityContext, Activity activity)
{
	IMessageActivity iMessage = (IMessageActivity)activity;
	var reply = activity.CreateReply(File.ReadAllText(activityContext.HttpContext.Server.MapPath("~/sample.md")));
	reply.TextFormat = TextFormatTypes.Markdown;

	await SendResponse(activityContext, reply).DropContext();
}
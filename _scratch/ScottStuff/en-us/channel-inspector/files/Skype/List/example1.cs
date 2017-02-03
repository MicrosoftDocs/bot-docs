public async Task ExecuteCommand(ActivityContext activityContext, Activity activity, List commands)
{
	IMessageActivity iMessage = (IMessageActivity)activity;
	StringBuilder sb = new StringBuilder();
	foreach (var command in commands)
	{
		sb.AppendLine($"* **{command.Keyword}** - {command.Description}");
	}
	var reply = activity.CreateReply(sb.ToString());
	await SendResponse(activityContext, reply).DropContext();
}

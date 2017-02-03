public async Task ExecuteCommand(ActivityContext activityContext, Activity activity)
{
	IMessageActivity iMessage = (IMessageActivity)activity;
	StringBuilder sb = new StringBuilder();
	if (iMessage.Text?.ToLower().Contains("markup") == true)
	{
		foreach (var em in EmojiConverter.Emoji2Markup.Values.Take(100))
			sb.Append(em);
	}
	else
	{
		foreach (var em in EmojiConverter.Markup2Emoji.Values.Take(100))
			sb.Append(em);
	}

	var reply = activity.CreateReply(sb.ToString());
	await SendResponse(activityContext, reply).DropContext();
}

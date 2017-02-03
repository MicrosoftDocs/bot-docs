public async Task ExecuteCommand(ActivityContext activityContext, Activity activity)
{
	IMessageActivity iMessage = (IMessageActivity)activity;
	ConnectorClient client = new ConnectorClient(new Uri(activity.ServiceUrl));

	var activityMembersTask = client.Conversations.GetActivityMembersAsync(activity.Conversation.Id, activity.Id);
	var conversationMembersTask = client.Conversations.GetConversationMembersAsync(activity.Conversation.Id);

	await Task.WhenAll(activityMembersTask, conversationMembersTask).DropContext();

	StringBuilder sb = new StringBuilder();
	sb.AppendLine("The activity members are:");
	sb.AppendLine();
	foreach (var channelAccount in (IList<ChannelAccount>)activityMembersTask.Result)
	{
		sb.AppendLine($"* {channelAccount.Name} [{channelAccount.Id}]");
	}

	sb.AppendLine();
	sb.AppendLine();
	sb.AppendLine("The conversation members are:");
	sb.AppendLine();
	foreach (var channelAccount in (IList<ChannelAccount>)conversationMembersTask.Result)
	{
		sb.AppendLine($"* {channelAccount.Name} [{channelAccount.Id}]");
	}

	var reply = activity.CreateReply(sb.ToString());
	await SendResponse(activityContext, reply).DropContext();
}

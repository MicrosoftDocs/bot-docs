public async Task ExecuteCommand(ActivityContext activityContext, Activity activity)
{
	IMessageActivity iMessage = (IMessageActivity)activity;
	ConnectorClient client = new ConnectorClient(new Uri(activity.ServiceUrl));

	var response = await client.Conversations.CreateDirectConversationAsync(activity.Recipient, activity.From);

	var reply = activity.CreateReply($"This is a direct message to {activity.From.Name ?? activity.From.Id} : {activity.Text}");
	reply.Conversation = new ConversationAccount(id: response.Id);
	reply.ReplyToId = null;

	await client.Conversations.SendToConversationAsync(reply).DropContext();
}

public Task ExecuteCommand(ActivityContext activityContext, Activity activity)
{
	Background.Run(async () =>
	{
		IMessageActivity iMessage = (IMessageActivity)activity;
		var reply = activity.CreateReply(String.Empty);
		reply.Type = ActivityTypes.Typing;
		await SendResponse(activityContext, reply).DropContext();
		await Task.Delay(2000).DropContext();
		await SendResponse(activityContext, reply).DropContext();
		await Task.Delay(3000).DropContext();
		reply.Type = ActivityTypes.Message;
		reply.Text = "My robot must rate as my favorite toy,  A wonderful, whirring, mechanical joy.  My robot can talk, but he'd much rather sing,  Or go to the park and play on the swings!";
		await SendResponse(activityContext, reply).DropContext();
	});
	
	return Task.CompletedTask;
}

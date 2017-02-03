public async Task ExecuteCommand(ActivityContext activityContext, Activity activity)
{
	IMessageActivity iMessage = (IMessageActivity)activity;
	var reply = activity.CreateReply("");
	reply.Summary = $"Request to sign in";
	reply.Attachments = new List<Attachment>();
	reply.Attachments.Add(new Attachment()
	{
		ContentType = SigninCard.ContentType,
		Content = new SigninCard()
		{
			Text = "Login to signin sample",
			Buttons = new List<CardAction>()
			{
				new CardAction()
				{
					Title = "Signin",
					Type = ActionTypes.Signin,
					Value = "https://login.live.com/login.srf?wa=wsignin1.0&ct=1464753247&rver=6.6.6556.0&wp=MBI_SSL&wreply=https:%2F%2Foutlook.live.com%2Fowa%2F&id=292841&CBCXT=out"
				}
			}
		}
	});

	await SendResponse(activityContext, reply).DropContext();
}

public async Task ExecuteCommand(ActivityContext activityContext, Activity activity)
{
	IMessageActivity iMessage = (IMessageActivity)activity;
	
	string codeSample =
		"```javascript\n"
		+"function fancyAlert(arg) {\n"
		+"  if(arg) {\n"
		+"    $.facebox({div:'#foo'})\n"
		+"  }\n"
		+"}\n"
		+"```";
	
	var reply = activity.CreateReply(codeSample);

	await SendResponse(activityContext, reply).DropContext();
}

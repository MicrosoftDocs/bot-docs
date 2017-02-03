public async Task ExecuteCommand(ActivityContext activityContext, Activity activity)
{
	IMessageActivity iMessage = (IMessageActivity)activity;
	var reply = activity.CreateReply("");
	reply.TextFormat = TextFormatTypes.Plain;
	reply.Summary = $"Receipt Card ";
	reply.Attachments = new List<Attachment>();
	reply.Attachments.Add(new Attachment()
	{
		ContentType = ReceiptCard.ContentType,
		Content = new ReceiptCard()
		{
			Title = "Surface Pro 4",
			Items = new List<ReceiptItem>()
			{
				new ReceiptItem()
				{
					Title = "Surface Pro 4",
					Subtitle = "Surface Pro 4 is a powerful, versatile, lightweight laptop",
					Text = "Surface does more. Just like you. For one device that does everything, you need more than a mobile OS. ",
					Image = new CardImage() { Url = "https://compass-ssl.surface.com/assets/b7/3c/b73ccd0e-0e08-42b5-9f8d-9143223eafd0.jpg?n=Hero-panel-image-gallery_03.jpg", Alt="Surface Pro 4"},
					Price = "$XX.XX",
				},
				new ReceiptItem()
				{
					Title = "Label AAAA",
					Price = "$XXX",
				},
				new ReceiptItem()
				{
					Title = "Label BBBB",
					Price = "$XXX"
				}
			},
			Facts = new List<Fact>()
			{
				new Fact() { Key= "Order Number",            Value="XXXXXXXXXX" },
				new Fact() { Key= "expected delivery time",  Value="XXXX.XX.XX" },
				new Fact() { Key ="Payment Method",          Value="XXXX XXXX" },
				new Fact() { Key = "Delivery Address",        Value="Address, XXXXX" }
			},
			Tax = "$XX.XX",
			Total = "$XXX.XX",
			Buttons = new List<CardAction>()
			{
				 new CardAction()
				 {
					  Title = "Thumbs Up",
					  Type=ActionTypes.ImBack,
					  Value="I like it"
				 },
				 new CardAction()
				 {
					  Title = "Thumbs Down",
					  Type=ActionTypes.ImBack,
					  Value="I don't like it"
				 },
			}
		}
	});

	await SendResponse(activityContext, reply).DropContext();
}

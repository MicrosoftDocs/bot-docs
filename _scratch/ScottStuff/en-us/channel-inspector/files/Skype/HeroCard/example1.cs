public async Task ExecuteCommand(ActivityContext activityContext, Activity activity)
{
	IMessageActivity iMessage = (IMessageActivity)activity;
	var reply = activity.CreateReply("");
	reply.TextFormat = TextFormatTypes.Plain;
	reply.Summary = $"Hero Card about bender";
	var card = new HeroCard()
	{
		Title = "Surface Pro 4",
		Subtitle = "Surface Pro 4 is a powerful, versatile, lightweight laptop.",
		Text = "Surface does more. Just like you. For one device that does everything, you need more than a mobile OS. Surface Pro 4 has a laptop class keyboard,- full-size USB 3.0, microSD- card reader, and a Mini DisplayPort – and it runs full professional-grade software.",
		Images = new List<CardImage>()
		{
			new CardImage() { Url =  "https://compass-ssl.surface.com/assets/b7/3c/b73ccd0e-0e08-42b5-9f8d-9143223eafd0.jpg?n=Hero-panel-image-gallery_03.jpg", Alt="Surface_Pro_4"}
		},
	};

	if (!iMessage.Text.Contains("nobuttons"))
	{
		card.Buttons = new List<CardAction>()
		{
				new CardAction()
				{
					Title = "Thumbs Up",
					Type=ActionTypes.ImBack,
					Image = "http://moopz.com/assets_c/2012/06/emoji-thumbs-up-150-thumb-autox125-140616.jpg",
					Value="I like it"
				},
				new CardAction()
				{
					Title = "Thumbs Down",
					Type=ActionTypes.ImBack,
					Image = "http://yourfaceisstupid.com/wp-content/uploads/2014/08/thumbs-down.png",
					Value="I don't like it"
				},
				new CardAction()
				{
					Title = "I feel lucky",
					Type=ActionTypes.OpenUrl,
					Image = "http://thumb9.shutterstock.com/photos/thumb_large/683806/148441982.jpg",
					Value="https://www.bing.com/images/search?q=bender&qpvt=bender&qpvt=bender&qpvt=bender&FORM=IGRE"
				}
		};

		card.Tap = new CardAction()
		{
			Type = ActionTypes.OpenUrl,
			Value = "http://www.botframework.com",
			Title = "Tapped it!"
		};
	}

	reply.Attachments = new List<Attachment>();
	reply.Attachments.Add(new Attachment()
	{
		ContentType = HeroCard.ContentType,
		Content = card
	});

	await SendResponse(activityContext, reply).DropContext();
}

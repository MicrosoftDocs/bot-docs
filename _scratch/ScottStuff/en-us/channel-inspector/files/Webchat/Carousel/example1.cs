public async Task ExecuteCommand(ActivityContext activityContext, Activity activity)
{
    IMessageActivity iMessage = (IMessageActivity)activity;
    var reply = activity.CreateReply("test carousel");
    reply.Type = ActivityTypes.Message;
    reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
    reply.TextFormat = TextFormatTypes.Plain;

    reply.Attachments = new List<Attachment>()
    {
        new Attachment()
        {
            ContentType = HeroCard.ContentType,
            Content = new HeroCard()
            {
                Title = "Details about image 1",
                Text = "Price: $XXX.XX USD",
                Images = new List<CardImage>()
                {
                    new CardImage() { Url = "https://compass-ssl.surface.com/assets/b7/3c/b73ccd0e-0e08-42b5-9f8d-9143223eafd0.jpg?n=Hero-panel-image-gallery_03.jpg" }
                },
                Buttons = new List<CardAction>()
                {
                    new CardAction(){ Title = "Places to buy", Type=ActionTypes.ImBack, Value="Places To Buy"},
                    new CardAction(){ Title = "Related Products", Type=ActionTypes.ImBack, Value="Related Products"},
                }
            }
        },
        new Attachment()
        {
            ContentType = HeroCard.ContentType,
            Content = new HeroCard()
            {
                Title = "Details about image 2",
                Text = "Price: $XXX.XX USD",
                Images = new List<CardImage>()
                {
                    new CardImage() { Url = "https://compass-ssl.surface.com/assets/b5/82/b582f7f9-93ee-4ce8-971f-aacf5426decb.jpg?n=Hero-panel-image-gallery_02.jpg" }
                },
                Buttons = new List<CardAction>()
                {
                    new CardAction(){ Title = "Places to buy", Type=ActionTypes.ImBack, Value="Places To Buy"},
                    new CardAction(){ Title = "Related Products", Type=ActionTypes.ImBack, Value="Related Products"},
                }
            }
        },
        new Attachment()
        {
            ContentType = HeroCard.ContentType,
            Content = new HeroCard()
            {
                Title = "Details about image 3",
                Text = "Price: $XXX.XX USD",
                Images = new List<CardImage>()
                {
                    new CardImage() { Url = "https://compass-ssl.surface.com/assets/9d/8f/9d8fcc7b-0b81-487a-9e2f-97d83a49666a.jpg?n=Hero-panel-image-gallery_06.jpg" }
                },
                Buttons = new List<CardAction>()
                {
                    new CardAction(){ Title = "Places to buy", Type=ActionTypes.ImBack, Value="Places To Buy"},
                    new CardAction(){ Title = "Related Products", Type=ActionTypes.ImBack, Value="Related Products"},
                }
            }
        },
        new Attachment()
        {
            ContentType = HeroCard.ContentType,
            Content = new HeroCard()
            {
                Title = "Details about image 4",
                Text = "Price: $XXX.XX USD",
                Images = new List<CardImage>()
                {
                    new CardImage() { Url = "https://compass-ssl.surface.com/assets/d6/b1/d6b1b79f-db1d-44f1-98ba-b822b7744940.jpg?n=Hero-panel-image-gallery_01.jpg" }
                },
                Buttons = new List<CardAction>()
                {
                    new CardAction(){ Title = "Places to buy", Type=ActionTypes.ImBack, Value="Places To Buy"},
                    new CardAction(){ Title = "Related Products", Type=ActionTypes.ImBack, Value="Related Products"},
                }
            }
        }
    };

    await SendResponse(activityContext, reply).DropContext();
}

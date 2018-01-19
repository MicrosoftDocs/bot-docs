// <addMediaAttachment>
replyMessage.Attachments.Add(new Attachment()
{
    ContentUrl = "https://upload.wikimedia.org/wikipedia/en/a/a6/Bender_Rodriguez.png",
    ContentType = "image/png",
    Name = "Bender_Rodriguez.png"      
});
// </addMediaAttachment>



// <addHeroCardAttachment>
Activity replyToConversation = message.CreateReply("Should go to conversation, in carousel format");
replyToConversation.AttachmentLayout = AttachmentLayoutTypes.Carousel;
replyToConversation.Attachments = new List<Attachment>();

Dictionary<string, string> cardContentList = new Dictionary<string, string>();
cardContentList.Add("PigLatin", "https://<ImageUrl1>");
cardContentList.Add("Pork Shoulder", "https://<ImageUrl2>");
cardContentList.Add("Bacon", "https://<ImageUrl3>");

foreach(KeyValuePair<string, string> cardContent in cardContentList)
{
    List<CardImage> cardImages = new List<CardImage>();
    cardImages.Add(new CardImage(url:cardContent.Value ));

    List<CardAction> cardButtons = new List<CardAction>();

    CardAction plButton = new CardAction()
    {
        Value = $"https://en.wikipedia.org/wiki/{cardContent.Key}",
        Type = "openUrl",
        Title = "WikiPedia Page"
    };

    cardButtons.Add(plButton);

    HeroCard plCard = new HeroCard()
    {
        Title = $"I'm a hero card about {cardContent.Key}",
        Subtitle = $"{cardContent.Key} Wikipedia Page",
        Images = cardImages,
        Buttons = cardButtons
    };

    Attachment plAttachment = plCard.ToAttachment();
    replyToConversation.Attachments.Add(plAttachment);
}

var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
// </addHeroCardAttachment>



// <addThumbnailCardAttachment>
Activity replyToConversation = message.CreateReply("Should go to conversation, in list format");
replyToConversation.AttachmentLayout = AttachmentLayoutTypes.List;
replyToConversation.Attachments = new List<Attachment>();

Dictionary<string, string> cardContentList = new Dictionary<string, string>();
cardContentList.Add("PigLatin", "https://<ImageUrl1>");
cardContentList.Add("Pork Shoulder", "https://<ImageUrl2>");

foreach(KeyValuePair<string, string> cardContent in cardContentList)
{
    List<CardImage> cardImages = new List<CardImage>();
    cardImages.Add(new CardImage(url:cardContent.Value ));

    List<CardAction> cardButtons = new List<CardAction>();

    CardAction plButton = new CardAction()
    {
        Value = $"https://en.wikipedia.org/wiki/{cardContent.Key}",
        Type = "openUrl",
        Title = "WikiPedia Page"
    };

    cardButtons.Add(plButton);

    ThumbnailCard plCard = new ThumbnailCard()
    {
        Title = $"I'm a thumbnail card about {cardContent.Key}",
        Subtitle = $"{cardContent.Key} Wikipedia Page",
        Images = cardImages,
        Buttons = cardButtons
    };

    Attachment plAttachment = plCard.ToAttachment();
    replyToConversation.Attachments.Add(plAttachment);
}

var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
// </addThumbnailCardAttachment>



// <addReceiptCardAttachment>
Activity replyToConversation = message.CreateReply("Should go to conversation");
replyToConversation.Attachments = new List<Attachment>();

List<CardImage> cardImages = new List<CardImage>();
cardImages.Add(new CardImage(url: "https://<imageUrl1>" ));

List<CardAction> cardButtons = new List<CardAction>();

CardAction plButton = new CardAction()
{
    Value = $"https://en.wikipedia.org/wiki/PigLatin",
    Type = "openUrl",
    Title = "WikiPedia Page"
};

cardButtons.Add(plButton);

ReceiptItem lineItem1 = new ReceiptItem()
{
    Title = "Pork Shoulder",
    Subtitle = "8 lbs",
    Text = null,
    Image = new CardImage(url: "https://<ImageUrl1>"),
    Price = "16.25",
    Quantity = "1",
    Tap = null
};

ReceiptItem lineItem2 = new ReceiptItem()
{
Title = "Bacon",
Subtitle = "5 lbs",
Text = null,
Image = new CardImage(url: "https://<ImageUrl2>"),
Price = "34.50",
Quantity = "2",
Tap = null
};

List<ReceiptItem> receiptList = new List<ReceiptItem>();
receiptList.Add(lineItem1);
receiptList.Add(lineItem2);

ReceiptCard plCard = new ReceiptCard()
{
    Title = "I'm a receipt card, isn't this bacon expensive?",
    Buttons = cardButtons,
    Items = receiptList,
    Total = "112.77",
    Tax = "27.52"
};

Attachment plAttachment = plCard.ToAttachment();
replyToConversation.Attachments.Add(plAttachment);

var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
// </addReceiptCardAttachment>



// <addSignInCardAttachment>
Activity replyToConversation = message.CreateReply("Should go to conversation");
replyToConversation.Attachments = new List<Attachment>();

List<CardAction> cardButtons = new List<CardAction>();

CardAction plButton = new CardAction()
{
    Value = $"https://<OAuthSignInURL",
    Type = "signin",
    Title = "Connect"
};

cardButtons.Add(plButton);

SigninCard plCard = new SigninCard(title: "You need to authorize me", button: plButton);

Attachment plAttachment = plCard.ToAttachment();
replyToConversation.Attachments.Add(plAttachment);

var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
// </addSignInCardAttachment>



// <addAdaptiveCardAttachment>
Activity replyToConversation = message.CreateReply("Should go to conversation");
replyToConversation.Attachments = new List<Attachment>();

AdaptiveCard card = new AdaptiveCard();

// Specify speech for the card.
card.Speak = "<s>Your  meeting about \"Adaptive Card design session\"<break strength='weak'/> is starting at 12:30pm</s><s>Do you want to snooze <break strength='weak'/> or do you want to send a late notification to the attendees?</s>";

// Add text to the card.
card.Body.Add(new TextBlock()
{
    Text = "Adaptive Card design session",
    Size = TextSize.Large,
    Weight = TextWeight.Bolder
});

// Add text to the card.
card.Body.Add(new TextBlock()
{
    Text = "Conf Room 112/3377 (10)"
});

// Add text to the card.
card.Body.Add(new TextBlock()
{
    Text = "12:30 PM - 1:30 PM"
});

// Add list of choices to the card.
card.Body.Add(new ChoiceSet()
{
    Id = "snooze",
    Style = ChoiceInputStyle.Compact,
    Choices = new List<Choice>()
    {
        new Choice() { Title = "5 minutes", Value = "5", IsSelected = true },
        new Choice() { Title = "15 minutes", Value = "15" },
        new Choice() { Title = "30 minutes", Value = "30" }
    }
});

// Add buttons to the card.
card.Actions.Add(new OpenUrlAction()
{
    Url = "http://foo.com",
    Title = "Snooze"
});

card.Actions.Add(new OpenUrlAction()
{
    Url = "http://foo.com",
    Title = "I'll be late"
});

card.Actions.Add(new OpenUrlAction()
{
    Url = "http://foo.com",
    Title = "Dismiss"
});

// Create the attachment.
Attachment attachment = new Attachment()
{
    ContentType = AdaptiveCard.ContentType,
    Content = card
};

replyToConversation.Attachments.Add(attachment);

var reply = await connector.Conversations.SendToConversationAsync(replyToConversation);
// </addAdaptiveCardAttachment>

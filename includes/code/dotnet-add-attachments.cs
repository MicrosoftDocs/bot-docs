// <addMediaAttachment>
replyMessage.Attachments.Add(new Attachment()
{
    ContentUrl = "https://upload.wikimedia.org/wikipedia/en/a/a6/Bender_Rodriguez.png",
    ContentType = "image/png",
    Name = "Bender_Rodriguez.png"      
});
// </addMediaAttachment>

// <addHeroCardAttachment>
Activity replyToConversation = message.CreateReply("Should go to conversation, with a carousel");
replyToConversation.AttachmentLayout = AttachmentLayouts.Carousel;
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
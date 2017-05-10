// <requestPayment>
var replyMessage = context.MakeMessage();

replyMessage.Attachments = new List<Attachment>();

var displayedItem = await new CatalogService().GetRandomItemAsync();

var cartId = displayedItem.Id.ToString();
context.ConversationData.SetValue(CART_KEY, cartId);
context.ConversationData.SetValue(cartId, context.Activity.From.Id);

var heroCard = new HeroCard
{
    Title = displayedItem.Title,
    Subtitle = $"{displayedItem.Currency} {displayedItem.Price.ToString("F")}",
    Text = displayedItem.Description,
    Images = new List<CardImage>
    {
        new CardImage
        {
            Url = displayedItem.ImageUrl
        }
    },
    Buttons = new List<CardAction>
    {
        new CardAction
        {
            Title = "Buy",
            Type = PaymentRequest.PaymentActionType,
            Value = BuildPaymentRequest(cartId, displayedItem, MethodData)
        }
    }
};

replyMessage.Attachments.Add(heroCard.ToAttachment());

await context.PostAsync(replyMessage);
// </requestPayment>


// <processCallback>
[MethodBind]
[ScorableGroup(1)]
private async Task OnInvoke(IInvokeActivity invoke, IConnectorClient connectorClient, IStateClient stateClient, HttpResponseMessage response, CancellationToken token)
{
    MicrosoftAppCredentials.TrustServiceUrl(invoke.RelatesTo.ServiceUrl);

    var jobject = invoke.Value as JObject;
    if (jobject == null)
    {
        throw new ArgumentException("Request payload must be a valid json object.");
    }
    
    // This is a temporary workaround for the issue that the channelId for "webchat" is mapped to "directline" in the incoming RelatesTo object
    invoke.RelatesTo.ChannelId = (invoke.RelatesTo.ChannelId == "directline") ? "webchat" : invoke.RelatesTo.ChannelId;

    if (invoke.RelatesTo.User == null)
    {
        // Bot keeps the userId in context.ConversationData[cartId]
        var conversationData = await stateClient.BotState.GetConversationDataAsync(invoke.RelatesTo.ChannelId, invoke.RelatesTo.Conversation.Id, token);
        var cartId = conversationData.GetProperty<string>(RootDialog.CARTKEY);

        if (!string.IsNullOrEmpty(cartId))
        {
            invoke.RelatesTo.User = new ChannelAccount
            {
                Id = conversationData.GetProperty<string>(cartId)
            };
        }
    }

    var updateResponse = default(object);

    switch (invoke.Name)
    {
        case PaymentOperations.UpdateShippingAddressOperationName:
            updateResponse = await ProcessShippingAddressUpdate(jobject.ToObject<PaymentRequestUpdate>(), token);
            break;

        case PaymentOperations.UpdateShippingOptionOperationName:
            updateResponse = await ProcessShippingOptionUpdate(jobject.ToObject<PaymentRequestUpdate>(), token);
            break;

        case PaymentOperations.PaymentCompleteOperationName:
            updateResponse = await ProcessPaymentComplete(invoke, jobject.ToObject<PaymentRequestComplete>(), token);
            break;

        default:
            throw new ArgumentException("Invoke activity name is not a supported request type.");
    }

    response.Content = new ObjectContent<object>(
        updateResponse,
        this.Configuration.Formatters.JsonFormatter,
        JsonMediaTypeFormatter.DefaultMediaType);

    response.StatusCode = HttpStatusCode.OK;
}
// </processCallback>
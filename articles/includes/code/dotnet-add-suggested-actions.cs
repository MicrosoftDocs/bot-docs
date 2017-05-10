// <addSuggestedActions>
var reply = activity.CreateReply("I have colors in mind, but need your help to choose the best one.");
reply.Type = ActivityTypes.Message;
reply.TextFormat = TextFormatTypes.Plain;

reply.SuggestedActions = new SuggestedActions()
{
    Actions = new List<CardAction>()
    {
        new CardAction(){ Title = "Blue", Type=ActionTypes.ImBack, Value="Blue" },
        new CardAction(){ Title = "Red", Type=ActionTypes.ImBack, Value="Red" },
        new CardAction(){ Title = "Green", Type=ActionTypes.ImBack, Value="Green" }
    }
};
// </addSuggestedActions>
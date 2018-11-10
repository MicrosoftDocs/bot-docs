// <createConnectorClient>
public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
{
    var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
    . . .
}
// </createConnectorClient>

// <createReply>
Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");
// </createReply>

// <sendReply>
await connector.Conversations.ReplyToActivityAsync(reply);
// </sendReply>

// <sendNonReplyMessage>
await connector.Conversations.SendToConversationAsync((Activity)newMessage);
// </sendNonReplyMessage>

// <startPrivateConversation>
var userAccount = new ChannelAccount(name: "Larry", id: "@UV357341");
var connector = new ConnectorClient(new Uri(activity.ServiceUrl));
var conversationId = await connector.Conversations.CreateDirectConversationAsync(botAccount, userAccount);

IMessageActivity message =  Activity.CreateMessageActivity();
message.From = botAccount;
message.Recipient = userAccount;
message.Conversation = new ConversationAccount(id: conversationId.Id);
message.Text = "Hello, Larry!";
message.Locale = "en-Us";
await connector.Conversations.SendToConversationAsync((Activity)message); 
// </startPrivateConversation>

// <startGroupConversation>
var connector = new ConnectorClient(new Uri(incomingMessage.ServiceUrl));

List<ChannelAccount> participants = new List<ChannelAccount>();
participants.Add(new ChannelAccount("joe@contoso.com", "Joe the Engineer"));
participants.Add(new ChannelAccount("sara@contoso.com", "Sara in Finance"));

ConversationParameters cpMessage = new ConversationParameters(message.Recipient, true, participants, "Quarter End Discussion");
var conversationId = await connector.Conversations.CreateConversationAsync(cpMessage);

IMessageActivity message = Activity.CreateMessageActivity();
message.From = botAccount;
message.Recipient = new ChannelAccount("lydia@contoso.com", "Lydia the CFO"));
message.Conversation = new ConversationAccount(id: conversationId.Id);
message.ChannelId = incomingMessage.ChannelId;
message.Text = "Hello, everyone!";
message.Locale = "en-Us";

await connector.Conversations.SendToConversationAsync((Activity)message); 
// </startGroupConversation>

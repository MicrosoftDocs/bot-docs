// <attribute1>
[BotAuthentication]
public class MessagesController : ApiController
{
}
// </attribute1>

// <attribute2>
[BotAuthentication(MicrosoftAppId = "_appIdValue_", MicrosoftAppPassword="_passwordValue_")]
public class MessagesController : ApiController
{
}
// </attribute2>
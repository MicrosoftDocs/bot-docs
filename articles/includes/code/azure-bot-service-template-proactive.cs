// <receiveMessage>       
public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
{
    var message = await argument;
    var queueMessage = new Message
    {
        ResumptionCookie = new ResumptionCookie(message),
        Text = message.Text
    };
    
    // AddMessageToQueue() is a utility method you can find in the template
    AddMessageToQueue(JsonConvert.SerializeObject(queueMessage));
    await context.PostAsync($"{this.count++}: You said {queueMessage.Text}. Message added to the queue.");
    context.Wait(MessageReceivedAsync);
}
// </receiveMessage>



// <queueTrigger>
using System;
using System.Net;
using System.Net.Http;
using Microsoft.Azure.WebJobs.Host;

public class BotMessage
{
    public string Source { get; set; } 
    public string Message { get; set; }
}

public static HttpResponseMessage Run(string myQueueItem, out BotMessage message, TraceWriter log)
{
    message = new BotMessage
    { 
        Source = "Azure Functions (C#)!", 
        Message = myQueueItem
    };

    return new HttpResponseMessage(HttpStatusCode.OK); 
}
// </queueTrigger>



// <handleMessageFromFunction>
switch (activity.GetActivityType())
{
    case ActivityTypes.Trigger:
        // handle proactive Message from function
        var jactivity = JsonConvert.DeserializeObject<JObject>(jsonContent);
        var jobjectvalue = JsonConvert.DeserializeObject<JObject>(jactivity.GetValue("value").ToString());
        var message = JsonConvert.DeserializeObject<Message>(jobjectvalue.GetValue("Message").ToString());
        var messageactivity = (Activity)message.ResumptionCookie.GetMessage();
            
        using (var scope = DialogModule.BeginLifetimeScope(Conversation.Container, messageactivity))
        {
            var client = scope.Resolve<IConnectorClient>();
            var reply = messageactivity.CreateReply();
            reply.Text = $"This is coming back from the trigger! {message.Text}";
            await client.Conversations.ReplyToActivityAsync(reply);
        }
        
        break;
    default:
        break;
}
// </handleMessageFromFunction>
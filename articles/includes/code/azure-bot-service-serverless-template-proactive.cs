// <receiveMessage>       
public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
{
    var message = await argument;

    // Create a queue Message
    var queueMessage = new Message
    {
        RelatesTo = context.Activity.ToConversationReference(),
        Text = message.Text
    };

    // Write the queue Message to the queue
    // AddMessageToQueue() is a utility method you can find in the template
    await AddMessageToQueue(JsonConvert.SerializeObject(queueMessage));
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
    case ActivityTypes.Event:
        // handle proactive Message from function
        IEventActivity triggerEvent = activity;
        var message = JsonConvert.DeserializeObject<Message>(((JObject) triggerEvent.Value).GetValue("Message").ToString());
        var messageactivity = (Activity)message.RelatesTo.GetPostToBotMessage();
        
        client = new ConnectorClient(new Uri(messageactivity.ServiceUrl));
        var triggerReply = messageactivity.CreateReply();
        triggerReply.Text = $"This is coming back from the trigger! {message.Text}";
        await client.Conversations.ReplyToActivityAsync(triggerReply);
        break;
    default:
        break;
}
// </handleMessageFromFunction>
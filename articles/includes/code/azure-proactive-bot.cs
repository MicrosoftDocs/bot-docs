public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
    {
        
 // <receiveMessage>       
var message = await argument;
var queueMessage = new Message
{
     ResumptionCookie = new ResumptionCookie(message),
    Text = message.Text
};
        
// AddMessageToQueue() is a utility method you can find in the template
AddMessageToQueue(JsonConvert.SerializeObject(queueMessage));
await context.PostAsync($"{this.count++}: You said {queueMessage.Text}. Message added to the queue.");
context.Wait(MessageReceivedAsync);}
// </receiveMessage>

// <receiveTrigger>
switch (activity.GetActivityType())
{
    case ActivityTypes.Trigger:
        // handle proactive Message from function
        ITriggerActivity trigger = activity;
        var message = JsonConvert.DeserializeObject<Message>(((JObject) trigger.Value).GetValue("Message").ToString());
        var messageactivity = (Activity)message.ResumptionCookie.GetMessage();

        client = new ConnectorClient(new Uri(messageactivity.ServiceUrl));
        var triggerReply = messageactivity.CreateReply();
        triggerReply.Text = $"This is coming back from the trigger! {message.Text}";
        await client.Conversations.ReplyToActivityAsync(triggerReply);
        break;
    default:
        break;
}
// </receiveTrigger>

// <returnMessage>

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
// </returnMessage>
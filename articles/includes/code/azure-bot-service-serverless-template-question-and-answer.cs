// <processMessage>
switch (activity.GetActivityType())
{
    case ActivityTypes.Message:
        await Conversation.SendAsync(activity, () => new BasicQnAMakerDialog());
        break;
    ...
}
// </processMessage>   



// <BasicQnAMakerDialog>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;

[Serializable]
public class BasicQnAMakerDialog : QnAMakerDialog
{        
	//Parameters to QnAMakerService are:
	//Required: subscriptionKey, knowledgebaseId, 
	//Optional: defaultMessage, scoreThreshold[Range 0.0 – 1.0]
	public BasicQnAMakerDialog() : base(new QnAMakerService(new QnAMakerAttribute(Utils.GetAppSetting("QnASubscriptionKey"), Utils.GetAppSetting("QnAKnowledgebaseId"), "No good match in FAQ.", 0.5)))
	{}
}
//</BasicQnAMakerDialog> 



      
---
title: Conversations within the Bot Builder SDK | Microsoft Docs
description: Describes what a conversation is within the Bot Builder SDK.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 02/20/2018
monikerRange: 'azure-bot-service-4.0'

---
A bot can be thought of as a conversational user interface. A conversation between bot and a user consists of a series of requests sent from the user, and replies from the bot. 

<!-- 
A conversation identifies a series of activities sent between a bot and a user on a specific channel and represents an interaction between one or more bots and either a _direct_ conversation with a specific user or a _group_ conversation with multiple users.
A bot communicates with a user on a channel by receiving activities from, and sending activities to the user.

- Each user has an ID that is unique per channel.
- Each conversation has an ID that is unique per channel.
- The channel sets the conversation ID when it starts the conversion.
- The bot cannot start a conversation; however, once it has a conversation ID, it can resume that conversation.
- Not all channels support group conversations.
-->




## Conversation flow

The conversational UI of a bot can be designed in many ways. 
The simplest conversational flow is single-turn. In a single-turn flow, the bot finishes its task in one turn, consisting of one message from the user and one reply from the bot. 



<!-- 
The EchoBot sample in the BotBuilder SDK is a single-turn bot. Here are other examples of single turn conversation flow:
* A bot for getting the weather report, that just tells the user what the weather is, when they say "What's the weather?".
* An IoT bot that responds to "turn on the lights" by calling an IoT service. -->

<!-- The following isn't always true, it's a generalization -->
The simplest kind of single-turn bot doesn't need to keep track of conversation state. Each time it receives a message, it responds based only on the context of the current incoming message, without knowledge of past conversational turns.

![Single-turn weather bot](./media/concept-conversation/weather-single-turn.png)

A weather bot has a single-turn flow, if it just gives the user a weather report, without going back and forth asking for the city or the date. All the logic for displaying the weather report is based on the message the bot just received.

# [C#](#tab/csharp)
```csharp
        protected async override Task OnReceiveActivity(IBotContext context)
        {
            var msgActivity = context.Request.AsMessageActivity();
            if (String.Equals(msgActivity.Text, "get weather"))
            {                
                // GetWeatherReport returns a string describing the weather
                context.Reply(GetWeatherReport());
            }
        }
    // ...

```

# [JavaScript](#tab/javascript)
```javascript
const bot = new Bot(adapter)
    .use(model)
    .onReceive((context) => {
        if (context.request.text == "get weather") {
            context.reply(GetWeatherForecast());
        }
    });
    
    // ...
}
```
---


### Conversation and context

In each turn of a conversation, the bot receives a context object. Your bot can use the context properties to determine what to do next and how the conversation flows:

| Property       | Description          | 
| ------------- |-------------| 
| context.ConversationReference     | Identifies the conversation, and includes information about the bot and the user participating in the conversation. | 
| context.Request.Activity     | The requests and replies in a conversation are all types of [activities](./bot-concepts.md#activity). This property describes the incoming activity, including routing information, along with information about the channel, the conversation, the sender, and the receiver.   |
| context.TopIntent | Represents what the bot thinks the user wants to do. The bot can use it to change conversation topics. Intent is determined by an [intent recognizer](#recognize-intent).| 
| context.State.Conversation| Contains conversation properties. Your bot can use these to keep track of where the bot is in a conversation or its [_state_](bot-builder-v4-concept-storage.md). These properties are cleared when the conversation ends.     | 
| context.State.User| Contains user properties, that the bot uses to save information about the user, that isn't specific to the current conversation. [_state_](bot-builder-v4-concept-storage.md).     | 

<!--
> [!TIP]
> The context object in your bot's receive handler has a lifetime of a single turn.  -->

### Multiple turns

A bot can also have a multi-turn conversation flow.<!--can also take multiple turns to get the information it needs, or to execute steps in a process. A multi-turn bot keeps track of what happened earlier in a conversation. The Alarm bot sample in the BotBuilder SDK is a multi-turn bot.--> Some scenarios that require multiple conversational turns include:

 * A bot that prompts the user for additional information that it needs to complete a task. The bot needs to track whether it has all the parameters for fulfilling the task.
 * A bot that guides the user through steps in a process, such as placing an order. The bot needs to track where the user is in the sequence of steps.

For example, a weather bot has a multi-turn flow, if the bot responds to "what's the weather?" by asking for the city.

![multi-turn weather bot](./media/concept-conversation/weather-multi-turn.png)

When the user replies to the bot's prompt for the city, the receive handler for the bot gets the utterance "Seattle", the bot needs to have some context saved, to understand that the current message is the response to a previous prompt and part of a request to get weather. Multi-turn bots keep track of state to respond appropriately to new messages.

```
// TBD: snippet showing receiving message and using ConversationProperties
```

See [Managing state](bot-builder-v4-concept-storage.md) for an overview of managing state, and see [How to use user and conversation properties](bot-builder-how-to-v4-state.md) for an example.

## Conversation topics
You might design your bot to handle more than one type of task. For example, you might have a bot that provides different conversation flows for greeting the user, placing an order, canceling, and getting help. One way to switch between conversation flows for different tasks or conversation topics, is to recognize the intent (what the user wants to do) from the current message. 

## Recognize intent
The BotBuilder SDK supplies _recognizers_ that process each incoming message to determine intent, so your bot can initiate the appropriate conversational flow. Before the _receive callback_, recognizers look at the message content from the user to determine intent, and then return the intent to the bot using the context object within the receive callback. In the following example, the bot starts different conversation flows based on the value of `context.TopIntent.Name`.

# [C#](#tab/csharp)
```csharp
        protected async override Task OnReceiveActivity(IBotContext context)
        {
            var msgActivity = context.Request.AsMessageActivity();
            if (msgActivity != null)
            {
                    switch (context.TopIntent.Name)
                    {
                        case "Weather":
                            StartWeatherTopic(context);
                            return;

                        case "Help":
                            StartHelpTopic(context);
                            return;

                        default:
                            // use help flow if we don't recognize the intent
                            StartHelpTopic(context);
                            return;
                    }


            }
        }
    // ...

    public Task<bool> StartWeatherTopic(IBotContext context)
    {
        // Reply with a forecast based on what the user said
        context.Reply(GetForecast(context.Request.Text));
    }

    public Task<bool> StartHelpTopic(IBotContext context)
    {
        context.Reply("This is the weather bot. Type `get weather` to get a weather forecast");
    }
```

# [JavaScript](#tab/javascript)
```javascript
const bot = new Bot(adapter)
    .use(model)
    .onReceive((context) => {
        const intentName = context.topIntent ? context.topIntent.name : 'None';
        switch (intentName) {
            case 'Weather':
                return startWeatherTopic(context);
            case 'Help':
                return startHelpTopic(context);
            // default to help topic if we don't recognize the intent
            default:
                return startHelpTopic(context);
        }
    });
    
    // ...
    function startWeatherTopic(context) {
        // Reply with a forecast based on what the user said
        context.reply(GetWeatherForecast(context.request.text));
    }
    return Promise.resolve();

    function startHelpTopic(context) {
        context.reply("This is the weather bot. Type `get weather` to get a weather forecast");
    }
    return Promise.resolve();
}
```
---


The recognizer that determines `context.topIntent` can simply use regular expressions, Language Understanding (LUIS), or other logic that you develop as middleware. The following could be examples of recognizers:
   
* You set up a recognizer using regular expressions to detect every time a user says the word help.
* You use Language Understanding (LUIS) to train a service with examples of ways user might ask for help, and map that to the "Help" intent.
* You create your own recognizer middleware that inspects incoming activities and returns the "translate" intent every time it detects a message in another language.

For more info on setting up a recognizer see [Recognizers] and [Language Understanding]. <!-- TODO: ADD THIS TOPIC OR SNIPPET-->

### Consider how to interrupt conversation flow or change topics
One way to keep track of where you are in a conversation is to use `context.State.ConversationProperties` to save information about the currently active topic or what steps in a sequence have been completed.

# [C#](#tab/csharp2)
```csharp
    public class OrderState : StoreItem
    {
        public string eTag { get; set; }

        public string OrderStatus { get; set; }
        
        public bool OrderComplete { get; set; }
    }

    // Update state information on placing an order
    public Task<bool> StartPlaceOrderTopic(IBotContext context)
    {
        var conversationState = context.GetConversationState<OrderState>() ?? new OrderState();
        conversationState.OrderStatus = "started"
        conversationState.OrderComplete = false;
        // Execute order logic based on what the user said
        await PlaceOrderLogic(context.Request.Text);
    }

    // TBD demonstrate persistence
```

# [JavaScript](#tab/javascript2)
```javascript
    
    // Update state information on placing an order
    function startPlaceOrderTopic(context) {
        context.state.conversationProperties["activeTopic"] = "PlaceOrder"
        context.state.conversationProperties["orderStatus"] = "started"
        context.state.conversationProperties["orderComplete"] = false;
        // Execute order logic based on what the user said
        PlaceOrderLogic(context.Request.Text);
    }
    return Promise.resolve();

}
```
---


When a bot becomes more complex, you might also imagine a sequence of conversations flows occurring in a stack -- for instance, the bot invokes the new order flow, and then invokes the product search flow. Then the user will select a product and confirm, completing the product search flow, and then complete the order.

Although it would be great if users always traveled such a linear, logical path, it seldom occurs. Users do not communicate in "stacks." They tend to frequently change their minds. Consider the following example:

![User says something unexpected](./media/concept-conversation/interruption.png)

While your bot may have logically constructed a stack of flows, the user may decide to do something entirely different or ask a question that may be unrelated to the current topic. In the example, the user asks a question rather than providing the yes/no response that the flow expects. How should your flow respond?

* Insist that the user answer the question first.
* Disregard everything that the user had done previously, reset the whole flow stack, and start from the beginning by attempting to answer the user's question.
* Attempt to answer the user's question and then return to that yes/no question and try to resume from there.

There is no right answer to this question, as the best solution will depend upon the specifics of your scenario and how the user would reasonably expect the bot to respond. 

> [!TIP]
> If you're using the Bot Builder SDK for Node.Js, you can use [Dialogs] to manage conversation flow.

## Conversation lifetime

A bot receives a _conversation update_ activity whenever it has been added to a conversation, other members have been added to or removed from a conversation, or conversation metadata has changed.
You may want to have your bot react to conversation update activities by greeting users or introducing itself.

A bot receives an _end of conversation_ activity to indicate that the user has ended the conversation. A bot may send an _end of conversation_ activity to indicate to the user that the conversation is ending. 
If you are storing information about the conversation, you may want to clear that information when the conversation ends.

<!--  Types of conversations

Your bot can support multi-turn interactions where it prompts users for multiple peices of information. It can be focused on a very specific task or support multiple types of tasks. 
The Bot Builder SDK has some built-in support for Language Understatnding (LUIS) and QnA Maker for adding natural language "question and answer" features to your bot.

<!--TODO: Add with links when these topics are available:
[Conversation flow] and other design articles.
[Using recognizers] [Using state and storage] and other how tos.
-->
## Conversations, channels, and users
Conversations can either a _direct_ conversation with a specific user or a _group_ conversation with multiple users.
A bot communicates with a user on a channel by receiving activities from, and sending activities to the user.

- Each user has an ID that is unique per channel.
- Each conversation has an ID that is unique per channel.
- The channel sets the conversation ID when it starts the conversion.
- The bot cannot start a conversation; however, once it has a conversation ID, it can resume that conversation.
- Not all channels support group conversations.


<!-- In addition, your bot can send activities back to the user, either _proactively_, in response to internal logic, or _reactively_, in response to an activity from the user or channel.-->
<!--TODO: Link to messaging how tos.-->

## See also

- Activities
- Adapter
- Context
- Proactive messaging
- State

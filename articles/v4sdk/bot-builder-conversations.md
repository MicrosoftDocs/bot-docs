---
title: Conversations within the Bot Builder SDK | Microsoft Docs
description: Describes what a conversation is within the Bot Builder SDK.
author: jonathanfingold
ms.author: jonathanfingold
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 04/11/2018
monikerRange: 'azure-bot-service-4.0'

---

# Conversation flow
[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

Since a bot can be thought of as a conversational user interface, the flow of conversation is how we interact with the user and can take different forms. Having the right conversation flow helps improve the user's interaction and the performance of your bot.

Designing a bot's conversation flow involves deciding how a bot responds when the user says something to it. A bot first recognizes the task or conversation topic based on a message from the user. To determine the task or topic (known as the *intent*) associated with a user's message, the bot can look for words or patterns in the text of the user's message, or it can take advantage of services like [Language Understanding (LUIS)](bot-builder-concept-luis.md) and QnA Maker. 

Once the bot has recognized the user's intent, depending on the scenario, the bot could fulfill the user's request with a single reply, completing the conversation in one turn, or it might require a series of turns. For multi-turn conversation flows, the Bot Builder SDK provides [state management](./bot-builder-howto-v4-state.md) for keeping track of a conversation, [prompts](bot-builder-prompts.md) for asking for information, and [dialogs](bot-builder-dialog-manage-conversation-flow.md) for encapsulating conversation flows. 

In a complex bot with multiple subsystems, it can be the case that you use multiple services to recognize intent, one for each subcomponent of the bot. The [Dispatch tool](bot-builder-tutorial-dispatch.md) gets the results of multiple services in one place when you combine conversational subsystems into one bot. 
<!-- 
A conversation identifies a series of activities sent between a bot and a user on a specific channel and represents an interaction between one or more bots and either a _direct_ conversation with a specific user or a _group_ conversation with multiple users.
A bot communicates with a user on a channel by receiving activities from, and sending activities to the user.

- Each user has an ID that is unique per channel.
- Each conversation has an ID that is unique per channel.
- The channel sets the conversation ID when it starts the conversion.
- The bot cannot start a conversation; however, once it has a conversation ID, it can resume that conversation.
- Not all channels support group conversations.
-->

## Single turn conversation

The simplest conversational flow is single-turn. In a single-turn flow, the bot finishes its task in one turn, consisting of one message from the user and one reply from the bot. 



<!-- 
The EchoBot sample in the BotBuilder SDK is a single-turn bot. Here are other examples of single turn conversation flow:
* A bot for getting the weather report, that just tells the user what the weather is, when they say "What's the weather?".
* An IoT bot that responds to "turn on the lights" by calling an IoT service. -->

<!-- The following isn't always true, it's a generalization -->
The simplest kind of single-turn bot doesn't need to keep track of conversation state. Each time it receives a message, it responds based only on the context of the current incoming message, without knowledge of past conversational turns.

![Single-turn weather bot](./media/concept-conversation/weather-single-turn.png)

A weather bot has a single-turn flow, if it just gives the user a weather report, without going back and forth asking for the city or the date. All the logic for displaying the weather report is based on the message the bot just received. In each turn of a conversation, the bot receives a turn context, which your bot can use to determine what to do next and how the conversation flows. 

## Multiple turns

Most types of conversation can't be completed in a single turn, so a bot can also have a multi-turn conversation flow. Some scenarios that require multiple conversational turns include:

 * A bot that prompts the user for additional information that it needs to complete a task. The bot needs to track whether it has all the parameters for fulfilling the task.
 * A bot that guides the user through steps in a process, such as placing an order. The bot needs to track where the user is in the sequence of steps.

For example, a weather bot has a multi-turn flow, if the bot responds to "what's the weather?" by asking for the city.

![multi-turn weather bot](./media/concept-conversation/weather-multi-turn.png)

When the user replies to the bot's prompt for the city, the receive handler for the bot gets the utterance "Seattle", the bot needs to have some context saved, to understand that the current message is the response to a previous prompt and part of a request to get weather. Multi-turn bots keep track of state to respond appropriately to new messages.

<!--
```
// TBD: snippet showing receiving message and using ConversationProperties
```
-->

See [Managing state](bot-builder-storage-concept.md) for an overview of managing state, and see [How to use user and conversation properties](bot-builder-howto-v4-state.md) for an example.

> [!NOTE]
> Multi-turn conversations with REST API clients will need to keep track of their own state, for example in a database or table storage. 

## Conversation topics

You might design your bot to handle more than one type of task. For example, you might have a bot that provides different conversation flows for greeting the user, placing an order, canceling, and getting help. One way to handle this switch between conversation for different tasks or conversation topics is to recognize the intent (what the user wants to do) from the current message. 

### Recognize intent

The Bot Builder SDK supplies _recognizers_ that process each incoming message to determine intent, so your bot can initiate the appropriate conversational flow. Before the _receive callback_, recognizers look at the message content from the user to determine intent, and then return the intent to the bot using the turn context object within the receive callback, stored as the **Top Intent** on the turn context object. 

The recognizer that determines **Top Intent** can simply use regular expressions, Language Understanding (LUIS), or other logic that you develop as middleware. The following could be examples of recognizers:
   
* You set up a recognizer using regular expressions to detect every time a user says the word help.
* You use Language Understanding (LUIS) to train a service with examples of ways user might ask for help, and map that to the "Help" intent.
* You create your own recognizer middleware that inspects incoming activities and returns the "translate" intent every time it detects a message in another language.

For more info [Language Understanding with LUIS](bot-builder-concept-luis.md). <!-- TODO: ADD THIS TOPIC OR SNIPPET-->

### Consider how to interrupt conversation flow or change topics

One way to keep track of where you are in a conversation is to use [conversation state](bot-builder-howto-v4-state.md) to save information about the currently active topic or what steps in a sequence have been completed.

When a bot becomes more complex, you can also imagine a sequence of conversation flows occurring in a stack; for instance, the bot will invoke the new order flow, and then invoke the product search flow. Then the user will select a product and confirm, completing the product search flow, and then complete the order.

However, conversation rarely follows such a linear, logical path. Users do not communicate in "stacks", instead they tend to frequently change their minds. Consider the following example:

![User says something unexpected](./media/concept-conversation/interruption.png)

While your bot may have logically constructed a stack of flows, the user may decide to do something entirely different or ask a question that may be unrelated to the current topic. In the example, the user asks a question rather than providing the yes/no response that the flow expects. How should your flow respond?

* Insist that the user answer the question first.
* Disregard everything that the user had done previously, reset the whole flow stack, and start from the beginning by attempting to answer the user's question.
* Attempt to answer the user's question and then return to that yes/no question and try to resume from there.

There is no right answer to this question, as the best solution will depend upon the specifics of your scenario and how the user would reasonably expect the bot to respond. 

> [!TIP]
> If you're using the Bot Builder SDK for Node.Js, you can use [Dialogs] to manage conversation flow.

## Conversation lifetime

<!-- Note: these activities are dependent on whether the channel actually sends them. Also, we should add links -->
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

## Next steps

For complex conversations, such as some of those highlighted above, we need to be able to persist information for longer than a turn. Lets look at state and storage next.

> [!div class="nextstepaction"]
> [State and Storage](bot-builder-storage-concept.md)

<!-- In addition, your bot can send activities back to the user, either _proactively_, in response to internal logic, or _reactively_, in response to an activity from the user or channel.-->
<!--TODO: Link to messaging how tos.-->

<!--  TODO: Change to next steps, one for each of LUIS and State
## See also

- Activities
- Adapter
- Context
- Proactive messaging
- State
-->

[QnAMaker]:(bot-builder-luis-and-qna.md#using-qna-maker)

<!-- TODO: Update when the Dispatch concept is pushed -->
[Dispatch]:(bot-builder-concept-luis.md)

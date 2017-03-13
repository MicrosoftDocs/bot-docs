---
layout: page
title: Using Dialogs to Manage the Conversation
permalink: /en-us/csharp/builder/dialogs/
weight: 2305
parent1: Building your Bot Using Bot Builder for .NET
parent2: Chat Bots
---


Dialogs model a conversation. Each dialog is an abstraction that encapsulates its own state in a C# class that implements **IDialog**. Dialogs can be composed with other dialogs to maximize reuse, and the dialog's context maintains the stack of dialogs that are active in the conversation. A conversation composed of dialogs is portable across computers, which makes it scale. The conversation state (the stack of active dialogs and each dialog's state) is stored in the state service provided by the Bot Connector service, making the bot implementation stateless between requests (much like a web application that does not store session state in the web server's memory).

The best way to understand using Dialogs is to work through an example. Let's start by changing the [Getting Started](/en-us/csharp/builder/getting-started/) example from using the Bot Application template to using dialogs.
The first step is to import the Dialogs namespace.

{% highlight csharp %}
using Microsoft.Bot.Builder.Dialogs;
{% endhighlight %}

Next, add a C# class to the MessagesController.cs file that will represent the conversation. See below for details about the **StartAsync** and **MessageReceivedAsync** methods.

{% highlight csharp %}
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            await context.PostAsync("You said: " + message.Text);
            context.Wait(MessageReceivedAsync);
        }
    }
{% endhighlight %}

Finally, wire the class into your Post method by calling the **SendAsync** method with an instance of EchoDialog.

{% highlight csharp %}
        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            // Check if activity is of type message
            if (activity != null && activity.GetActivityType() == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new EchoDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }
{% endhighlight %}

<span style="color:red">Which method is marked async, Post?</span>

The method is marked async because Bot Builder makes use of the C# facilities for handling asynchronous communication. The method returns a Task which represents the task responsible for sending replies for the passed in message. If there is an exception, the Task will contain the exception information. 

The **Conversation.SendAsync** method is the root method of the Bot Builder SDK. It follows the [dependency inversion principle](https://en.wikipedia.org/wiki/Dependency_inversion_principle) and performs the following steps:

- Instantiates the required components  
- Deserializes the dialog's state (the dialog stack and each dialog's state) from **IBotDataStore** (the default implementation uses the Bot Connector state API)
- Resumes the conversation processes where the Bot decided to suspend and wait for a message
- Sends the replies.
- Serializes the updated dialog's state and persists it back to **IBotDataStore**.

<span style="color:red">I don't understand the "Resumes" bullet.</span>

When the conversation first starts, the dialog does not contain state so **Conversation.SendAsync** constructs EchoDialog and calls its **StartAsync** method. The **StartAsync** method calls **IDialogContext.Wait** with the continuation delegate (our **MessageReceivedAsync** method) that's called when there is a new message. In the initial case, there is an immediate message available (the one that launched the dialog) and it is immediately passed To **MessageReceivedAsync**.  

The **MessageReceivedAsync** method waits for a message, posts a response, and waits for the next message. Every time we call **IDialogContext.Wait** our bot is suspended and can be restarted on any computer that receives the message.  

If you test this bot, it will behave exactly like the Bot Application example shown in Getting Started. It is a little more complicated, but it allows you to compose together multiple dialogs into complex conversations without having to explicitly manage state.   



### Echo bot with state

This example builds on the previous example by adding dialog state (the count property) and some commands to control the state. This version of the EchoDialog adds a number to each response and lets the user enter the "reset" command to reset the count.

{% highlight csharp %}
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        protected int count = 1;

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            if (message.Text == "reset")
            {
                PromptDialog.Confirm(
                    context,
                    AfterResetAsync,
                    "Are you sure you want to reset the count?",
                    "Didn't get that!",
                    promptStyle: PromptStyle.None);
            }
            else
            {
                await context.PostAsync($"{this.count++}: You said {message.Text}");
                context.Wait(MessageReceivedAsync);
            }
        }

        public async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> argument)
        {
            var confirm = await argument;
            if (confirm)
            {
                this.count = 1;
                await context.PostAsync("Reset count.");
            }
            else
            {
                await context.PostAsync("Did not reset count.");
            }
            context.Wait(MessageReceivedAsync);
        }
    }
{% endhighlight %}


<span style="color:red">Is it any dialog property that's persisted as state or only "protected" variables?</span>


The **MessageReceivedAsync** method adds a check to see if the user's message was "reset". If the user asks to reset the counter, the built-in Prompts.Confirm prompt spawns a sub-dialog confirming with the user that they want to reset the count. The sub-dialog has its own private state that does not interfere with the parent's state. The result of the sub-dialog is passed to the **AfterResetAync** method, which sends a message back to the user indicating whether the count was reset. The final step calls **IDialogContext.Wait** with a continuation back to **MessageReceivedAsync** on the next message.

<span style="color:red">We need a topic that covers prompts.</span>


### Dialog context

All of the dialogs take an **IDialogContext** interface, which provides the services that the dialog needs to save state and communicate with the channel. The interface is composed of three interfaces: **Internals.IBotData**, **Internals.IDialogStack**, and **Internals.IBotToUser**.

**Internals.IBotData** provides access to the per user, conversation, and private conversation state data that's maintained by Bot Connector. The per user state is useful for storing things about the user that cross conversations (for example, the last sandwich order so that you can use that as the default the next time they order a sandwich). You could also store user state in your own store and use the Message.From.Id as a key.  

<span style="color:red">Do they call the bot state methods (for example, GetUserData and SetUserData)?</span>

**Internals.IBotToUser** provides methods to post messages to the user, according to some policy. Messages may be sent inline with the response to the web API method call or directly using the Bot Connector client. Sending and receiving messages through the dialog context ensures the **Internals.IBotData** state is passed through the Bot Connector.

<span style="color:red">What policy? Where/how do you define the policy?</span>

<span style="color:red">Need to show example of sending message inline with the response versus directly using the connector client. By "connector client" do you mean connector.Conversations.ReplyToActivityAsync or SendToConversationAsync?</span>

<span style="color:red">Is state not saved if you use connector.Conversations.ReplyToActivityAsync?</span>

**Internals.IDialogStack** provides methods to:

- Call children dialogs and push the new child on the dialog stack
- Mark the current dialog as done, return the result to the calling dialog, and pop the current dialog from the dialog stack
- Wait for a message from the user and suspend the conversation until the message arrives.

The stack is usually automatically managed for you.

<span style="color:red">What do you mean by "usually" managed for you? When wouldn't it be?</span>


### Serialization

<span style="color:red">IBotDataBag? Above it's IBotData.</span>

<span style="color:red">Not sure what "per-user, per-conversation" means.</span>

The dialog stack and the state of all active dialogs are serialized to the per-user, per-conversation IBotDataBag. The serialized blob is persisted in the messages sent to and received from Bot Connector. to be serialized, **Dialog** classes must include the Serializable attribute. All of the **IDialog** implementations in the builder library are marked as serializable. 

<span style="color:red">What does the following mean?</span>

When custom serialization is desired, there is an ISerialization implementation and serialization constructor as well.

<span style="color:red">Does "Chain methods" refer to the methods in the Dialog Chain section?</span>

The [Chain methods](/en-us/csharp/builder/dialogs-chains/) provide a fluent interface to dialogs that is usable in LINQ query syntax. The compiled form of LINQ query syntax often leverages anonymous methods. If these anonymous methods do not reference the environment of local variables, then these anonymous methods have no state and are trivially serializable. However, if the anonymous method captures any local variable in the environment, the resulting closure object (generated by the compiler) is not marked as serializable. Bot Builder will detect this situation and throw a ClosureCaptureException to help diagnose the issue.

If you want to leverage reflection to serialize classes that are not marked as serializable, the Bot Builder library includes a reflection-based serialization surrogate that you can use to register with Autofac.

{% highlight csharp %}
    var builder = new ContainerBuilder();
    builder.RegisterModule(new DialogModule());
    builder.RegisterModule(new ReflectionSurrogateModule());
{% endhighlight %}



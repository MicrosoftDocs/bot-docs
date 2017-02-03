---
layout: page
title: Language understanding bot
permalink: /en-us/azure-bot-service/templates/luis/
weight: 12150
parent1: Azure Bot Service
parent2: Templates
---


The language understanding bot template shows how to use natural language models (LUIS) to understand user intent. When the user asks your bot to “get news about virtual reality companies,” your bot needs to understand what the user is asking for. [LUIS](https://www.luis.ai/){:target="_blank"} is designed to enable you to very quickly deploy an HTTP endpoint that will interpret the user’s input in terms of the intention it conveys (find news), and the key entities that are present (virtual reality companies). LUIS lets you custom design the set of intentions and entities that are relevant to the application, and then guides you through the process of building a language understanding application.

When you create the template, Azure Bot Service creates an empty LUIS application for you (that always returns None). You need to sign in to LUIS, click My applications, and select the application that the service created for you. Update your model by creating new intents, and then train and publish your LUIS app.

{% comment %}
??
What’s the naming convention of the LUIS apps that we create?
??
{% endcomment %}

For information about using LUIS in the Bot Framework, see [Alarm Bot](/en-us/csharp/builder/sdkreference/dialogs.html#alarmBot).

The routing of the message is identical to the one presented in the [Basic bot template](/en-us/azure-bot-service/templates/basic/), please refer to that document for more info.


Most messages will have a Message activity type, and will contain the text and attachments that the user sent. If the message’s activity type is Message, the template posts the message to **BasicLuisDialog** in the context of the current message (see BasicLuisDialog.csx). 

{% highlight csharp %}
        switch (activity.GetActivityType())
        {
            case ActivityTypes.Message:
                await Conversation.SendAsync(activity, () => new BasicLuisDialog());
                break;
{% endhighlight %}


The BaiscLuisDialog.csx file contains the root dialog that controls the conversation with the user. The **BasicLuisDialog** object defines an intent method handler for each intent that you define in your LUIS model. The naming convention for intent handlers is, \<intent name\>+Intent (for example, NoneIntent). In this example, the dialog will handle the None intent which LUIS returns if it cannot determine the intent, and the MyIntent intent, which you still need to define in your LUIS model. The LuisIntent method attribute defines the method as an intent handler. The name in the LuisIntent attribute must match the name that you used in your model. 

The **BasicLuisDialog** object inherits from the [LuisDialog](/en-us/csharp/builder/sdkreference/d8/df9/class_microsoft_1_1_bot_1_1_builder_1_1_dialogs_1_1_luis_dialog.html) object. The **LuisDialog** object contains the **StartAsync** and **MessageReceived** methods. When the dialog’s instantiated, the dialog’s StartAsync method runs and calls IDialogContext.Wait with the continuation delegate that’s called when there is a new message. In the initial case, there is an immediate message available (the one that launched the dialog) and the message is immediately passed to the **MessageReceived** method (in the **LuisDialog** object).

The **MessageReceived** method calls your LUIS application model to determine intent and then calls the appropriate intent handler in the **BasicLuisDialog** object. The handler processes the intent and then waits for the next message from the user.

{% highlight csharp %}
[Serializable]
public class BasicLuisDialog : LuisDialog<object>
{
    public BasicLuisDialog() : base(new LuisService(new LuisModelAttribute(Utils.GetAppSetting("LuisAppId"), Utils.GetAppSetting("LuisAPIKey"))))
    {
    }

    [LuisIntent("None")]
    public async Task NoneIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"You have reached the none intent. You said: {result.Query}"); //
        context.Wait(MessageReceived);
    }

    [LuisIntent("MyIntent")]
    public async Task MyIntent(IDialogContext context, LuisResult result)
    {
        await context.PostAsync($"You have reached the MyIntent intent. You said: {result.Query}"); //
        context.Wait(MessageReceived);
    }
}{% endhighlight %}

The following are the project files used by the function; you should not have to modify any of them.

|**File**|**Description**
|function.json|This file contains the function’s bindings. You should not modify this file.
|project.json|This file contains the project’s NuGet references. You should only have to change this file if you add a new reference.
|project.lock.json|This file contains the list of packages. You should not modify this file.
|host.json|A metadata file that contains the global configuration options affecting the function.

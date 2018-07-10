---
title: Create a bot with the Bot Builder SDK for .NET | Microsoft Docs
description: Create a bot with the Bot Builder SDK for .NET, a powerful bot construction framework.
keywords: Bot Builder SDK, create a bot, quickstart, getting started
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: get-started-article
ms.prod: bot-framework
ms.date: 04/27/2018
monikerRange: 'azure-bot-service-3.0'
---

# Create a bot with the Bot Builder SDK for .NET
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-quickstart.md)
> - [Node.js](../nodejs/bot-builder-nodejs-quickstart.md)
> - [Bot Service](../bot-service-quickstart.md)
> - [REST](../rest-api/bot-framework-rest-connector-quickstart.md)

The <a href="https://github.com/Microsoft/BotBuilder" target="_blank">Bot Builder SDK for .NET</a> is an easy-to-use framework for developing bots using Visual Studio and Windows. The SDK leverages C# to provide a familiar way for .NET developers to create powerful bots.


This tutorial walks you through building a bot by using
the Bot Application template and the Bot Builder SDK for .NET,
and then testing it with the Bot Framework Emulator.

## Prerequisites
1. Visual Studio [2017](https://www.visualstudio.com/).

2. In Visual Studio, update all [extensions](https://docs.microsoft.com/en-us/visualstudio/extensibility/how-to-update-a-visual-studio-extension) to their latest versions.

3. Bot template for [C#](https://marketplace.visualstudio.com/items?itemName=BotBuilder.BotBuilderV3).

> [!TIP]
> The Visual Studio 2017 project templates directory is typically located at `%USERPROFILE%\Documents\Visual Studio 2017\Templates\ProjectTemplates\Visual C#\` and the item templates directory is at `%USERPROFILE%\Documents\Visual Studio 2017\Templates\ItemTemplates\Visual C#\`

## Create your bot

Open Visual Studio and create a new C# project. Choose the **Simple Echo Bot Application** template for your new project.

![Visual Studio create project](../media/connector-getstarted-create-project.png)

> [!NOTE]
> Visual Studio might say you need to download and install [IIS Express](https://www.microsoft.com/en-us/download/details.aspx?id=48264). 

Thanks to the Bot Application template, your project contains all of the code that's necessary to create the bot in this tutorial. You won't actually need to write any additional code. However, before we move on to testing your bot,
take a quick look at some of the code that the Bot Application template provided.

> [!TIP] 
> If needed, update [NuGet packages](https://docs.microsoft.com/en-us/nuget/quickstart/install-and-use-a-package-in-visual-studio).

## Explore the code

First, the `Post` method within **Controllers\MessagesController.cs** receives the message from the user and invokes the root dialog.

```csharp
public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
{
    if (activity.GetActivityType() == ActivityTypes.Message)
    {
        await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
    }
    else
    {
        HandleSystemMessage(activity);
    }
    var response = Request.CreateResponse(HttpStatusCode.OK);
    return response;
}

```

The root dialog processes the message and generates a response. The `MessageReceivedAsync` method within **Dialogs\RootDialog.cs** sends a reply that echos back the user's message, prefixed with the text 'You sent' and ending in the text 'which was *##* characters', where *##* represents the number of characters in the user's message.

```csharp
public class RootDialog : IDialog<object>
{
    public Task StartAsync(IDialogContext context)
    {
        context.Wait(MessageReceivedAsync);

        return Task.CompletedTask;
    }

    private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
    {
        var activity = await result as Activity;

        // Calculate something for us to return
        int length = (activity.Text ?? string.Empty).Length;

        // Return our reply to the user
        await context.PostAsync($"You sent {activity.Text} which was {length} characters");

        context.Wait(MessageReceivedAsync);
    }
}
```

## Test your bot

[!INCLUDE [Get started test your bot](../includes/snippet-getstarted-test-bot.md)]

### Start your bot

After installing the emulator, start your bot in Visual Studio by using a browser as the application host.
This Visual Studio screenshot shows that the bot will launch in Microsoft Edge when the run button is clicked.

![Visual Studio run project](../media/connector-getstarted-start-bot-locally.png)

When you click the run button, Visual Studio will build the application, deploy it to localhost,
and launch the web browser to display the application's **default.htm** page.
For example, here's the application's **default.htm** page shown in Microsoft Edge:

![Visual Studio bot running localhost](../media/connector-getstarted-bot-running-localhost.png)

> [!NOTE]
> You can modify the **default.htm** file within your project
> to specify the name and description of your bot application.

### Start the emulator and connect your bot

At this point, your bot is running locally.
Next, start the emulator and then connect to your bot in the emulator:

1. Create a new bot configuration. Type `http://localhost:port-number/api/messages` into the address bar, where *port-number* matches the port number shown in the browser where your application is running.

2. Click **Save and connect**. You won't need to specify **Microsoft App ID** and **Microsoft App Password**. You can leave these fields blank for now. You'll get this information later when you [register your bot](~/bot-service-quickstart-registration.md).

### Test your bot

Now that your bot is running locally and is connected to the emulator, test your bot by typing a few messages in the emulator.
You should see that the bot responds to each message you send by echoing back your message prefixed with the text 'You sent'
and ending with the text 'which was *##* characters', where *##* is the total number of characters in the message that you sent.


> [!TIP]
> In the emulator, click on any speech bubble in your conversation. Details about the message will appear in the Details pane, in JSON format.

You've successfully created a bot by using the Bot Application template Bot Builder SDK for .NET!

## Next steps

In this quickstart, you created a simple bot by using the Bot Application template and the Bot Builder SDK for .NET and verified the bot's functionality by using the Bot Framework Emulator.

Next, learn about the key concepts of the Bot Builder SDK for .NET.

> [!div class="nextstepaction"]
> [Key concepts in the Bot Builder SDK for .NET](bot-builder-dotnet-concepts.md)

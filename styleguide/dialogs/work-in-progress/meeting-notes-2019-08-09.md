[8/9 3:37 PM] stevem@terawe.com

What are Dialogs?

- Conversational abstractions that encapsulate their own states
- Breaks up conversation into smaller pieces
- Portable and adhere to SRP

​[8/9 3:48 PM] v-liyan@microsoft.com

https://github.com/Microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog#readme

>microsoft/BotBuilder-Samples
>
>Welcome to the Bot Framework samples repository. Here you will find task-focused samples in C#, JavaScript and TypeScript to help you get started with the Bot Framework SDK!
><br>- microsoft/BotBuilder-...github.com​

[8/9 3:48 PM] stevem@terawe.com

In non-bulleted form:

In the context of a bot, dialogs are conversational abstractions that encapsulate their own states. What does this mean? It means that you can break up an entire conversation into smaller pieces. This makes things more portable making each dialog piece adhere to the single responsibility principle.

​[8/9 3:56 PM] v-mimiel@microsoft.com

Without confusing the issue, I found the following:

- A dialog encapsulates application logic like a function in a program.
- It allows you to perform a specific task, such as gathering the details of a user’s profile, and then possibly of reusing the code when needed.
- Dialogs can also be chained together in **DialogSets**.
- The **Microsoft Bot Builder SDK v4** includes two built-in features to help you manage conversations using dialogs:
  - DialogSets
    - They are a collection of dialogs. To use dialogs, you must first create a dialog set and add dialogs to it.
    - A dialog can contain only a single or multiple waterfall steps.
  - Prompts
    - They provide the methods you can use to ask users for different types of information. For example, a text input, a multiple choice, or a date or number input.
    - A prompt dialog uses at least two functions, one to prompt the user to input data, and another function to process and respond to the data.

​[8/9 4:08 PM] v-emolsh@microsoft.com

[DialogSet namespace](https://docs.microsoft.com/en-us/dotnet/api/microsoft.bot.builder.dialogs.dialogset?view=botbuilder-dotnet-stable)

> DialogSet Class (Microsoft.Bot.Builder.Dialogs)A related set of dialogs that can all call each other.
><br> docs.microsoft.com

​[8/9 4:15 PM] v-liyan@microsoft.com

Provides context for the current state of the dialog stack.

​[8/9 4:16 PM] v-emolsh@microsoft.com

Gets the context for the current turn of conversation.

​[8/9 4:24 PM] v-emolsh@microsoft.com

synonyms for "active": live, operational/operating, running, working

​[8/9 4:30 PM] stevem@terawe.com

Found online

Design Time is the process of creating an application, designing an interface, setting the properties, and writing the code.  This is analogous to what goes on behind the scenes of a puppet show.  A writer writes the script.  A set designer paints the scenery and puts it in place.  The puppeteer pulls the strings that make the puppets move.  The actors read the lines from the script and speak for the puppets.  All of this is hidden from the audience and the show doesn't start until the curtain goes up.

Run Time starts when you click the button to Start with or without Debugging.  This is analogous to what the audience sees when the curtain goes up and the show starts.  The audience sees only the scenery and the puppets performing on the stage.  They do not see the strings that the puppeteer pulls.  They only see the action of the puppets and hear the voices that have been prepared by the script that the actors speak for the puppets.

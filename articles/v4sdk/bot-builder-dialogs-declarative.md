---
title: Create a bot using declarative adaptive dialogs
description: Learn how to create a bot that incorporates adaptive using the declarative approach in the Bot Framework SDK.
keywords: declarative, adaptive
author: kamrani
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/25/2020
monikerRange: 'azure-bot-service-4.0'
---

# Create a bot using declarative adaptive dialogs  

[!INCLUDE[applies-to](../includes/applies-to.md)]

This article shows how to create a bot that incorporates **Adaptive dialog** using the declarative approach.

## Prerequisites

- Knowledge of [bot basics][concept-basics], [managing state][concept-state], and the [dialogs library][concept-dialogs].
- Knowledge of [adaptive dialogs][concept-adaptive] and [declarative dialogs][concept-declarative].
- A copy of the **EchoBot** sample in either [**C#**][cs-sample] or [**JavaScript** preview][js-sample].

### Preliminary steps to add a declarative adaptive dialog to a bot

You must follow these steps to load and use a declarative dialog in a bot.

1. Update all Bot Builder NuGet packages to version 4.9.x.
1. Create the declarative files.
1. Add the required packages and references to your bot project.
1. Register the Bot Framework's declarative components for adaptive dialogs.
1. Use a dialog manager in the bot code to start or continue the root dialog each turn.
1. Create a resource explorer in the bot code to load the declarative resources.

<!--
1. Update all Bot Builder NuGet packages to version 4.9.x.
1. Add the `Microsoft.Bot.Builder.Dialogs.Adaptive` package to your bot project.
1. Add the `Microsoft.Bot.Builder.Dialogs.Declarative` package to your bot project.
1. Create the declarative files.
1. Add references to declarative components.
1. Register the bot frameworks declarative components for adaptive dialogs.
1. Use a dialog manager in the bot code to start or continue the root dialog each turn.
1. Create a resource explorer in the bot code to load the declarative resources.
-->

## About the sample

This sample, the Bot Framework Adaptive Dialog declarative Echo bot, demonstrates using a declaratively-defined [adaptive dialog][concept-adaptive] that accepts input from the user and echoes it back.

## Create the declarative files

Declarative dialog files are JSON files that declare the elements of a dialog. They typically have an extension of `.dialog`. The **EchoBot** sample only contains one adaptive dialog with a trigger to handle the `UnknownIntent` event, which when fires it sends a message to the user that echos what they said: _"You said '${turn.activity.text}'"_.

The declarative file ([C#][main.dialog] | [JavaScript][echo.dialog]) for the **EchoBot** sample:

```json
{
  "$schema": "../app.schema",
  "$kind": "Microsoft.AdaptiveDialog",
  "triggers": [
    {
      "$kind": "Microsoft.OnUnknownIntent",
      "actions": [
        {
          "$kind": "Microsoft.SendActivity",
          "activity":  "You said '${turn.activity.text}'"
        }
      ]
    }
  ]
}
```

> [!NOTE]
> It is generally a good practice to keep your declarative files in a sub-folder named `dialogs`, however they can be anywhere. You load them into your bot using the `ResourceExplorer.GetResources` method.

## Add references to declarative components

# [C#](#tab/csharp)

Declarative only works with adaptive dialogs. To enable adaptive in your bot install the **Microsoft.Bot.Builder.Dialogs.Adaptive** NuGet package. Once installed you can enable declarative by installing the **Microsoft.Bot.Builder.Dialogs.Declarative** NuGet package, then create the following references in your code in `Startup.cs`:

[!code-CSharp[Startup.cs-Using-Statements](~/../botbuilder-samples/samples/csharp_dotnetcore/adaptive-dialog/20.EchoBot-declarative/Startup.cs?range=4-10&highlight=10-12)]

Also in `EchoBot.cs`:

[!code-CSharp[EchoBot.cs-Using-Statements](~/../botbuilder-samples/samples/csharp_dotnetcore/adaptive-dialog/20.EchoBot-declarative/echoBot.cs?range=4-10&highlight=9-10)]

<!--
```CS
using Microsoft.Bot.Builder.Dialogs.Adaptive;
using Microsoft.Bot.Builder.Dialogs.Declarative.Resources;
```
-->

# [JavaScript](#tab/javascript)

Declarative only works with adaptive dialogs. To use adaptive dialogs, your project needs to install the **botbuilder-dialogs-adaptive** npm package. Once installed you can enable declarative by installing the **botbuilder-dialogs-declarative** NuGet package, then create the following references in your code in `index.js`:

<!--[!code-JavaScript[AdaptiveDialogComponentRegistration](~/../botbuilder-samples/blob/master/experimental/adaptive-dialog/javascript_nodejs/20.echo-bot-declarative/index.js?range=4-10&highlight=7-8)]-->

```JavaScript
const { ResourceExplorer } = require('botbuilder-dialogs-declarative');
const { AdaptiveDialogComponentRegistration, LanguageGeneratorMiddleWare } = require('botbuilder-dialogs-adaptive');
const { DialogManager } = require('botbuilder-dialogs');
```

> [!TIP]
> `require()` is not part of the standard JavaScript API. But in `Node.js`, it's a built-in function used to load modules. You can find more information in the [Modules][nodejs-modules] section of the Node.js docs.

---

## Register declarative components for adaptive dialogs

# [C#](#tab/csharp)

[!code-CSharp[ConfigureServices](~/../botbuilder-samples/samples/csharp_dotnetcore/adaptive-dialog/20.EchoBot-declarative/Startup.cs?range=30-67&highlight=38-39,44-45,62-63)]

<!--
```CS
ComponentRegistration.Add(new DialogsComponentRegistration());
ComponentRegistration.Add(new DeclarativeComponentRegistration());
var resourceExplorer = new ResourceExplorer().LoadProject(this.HostingEnvironment.ContentRootPath);
services.AddSingleton(resourceExplorer);
```
-->

# [JavaScript](#tab/javascript)

<!--[!code-JavaScript[AdaptiveDialogComponentRegistration](~/../botbuilder-samples/blob/master/experimental/adaptive-dialog/javascript_nodejs/20.echo-bot-declarative/index.js?range=20-22)]-->

```JavaScript
const resourceExplorer = new ResourceExplorer().addFolder(__dirname, true, true);
resourceExplorer.addComponent(new AdaptiveDialogComponentRegistration(resourceExplorer));
```

---

## Create the dialog declaratively

Since declarative adaptive dialogs are created at run-time from `.dialog` files, you won't define them directly in your source code, but instead you will create them at run-time using `ResourceExplorer`.

# [C#](#tab/csharp)

First, create a private readonly parameter named `resourceExplorer` of type `ResourceExplorer` and in the `EchoBot` constructor set it to the `ResourceExplorer` object previously created in the `ConfigureServices` method in `Startup.cs`. You also need to create the `DialogManager` object that is required for all bots using adaptive dialogs.

[!code-CSharp[EchoBot-Constructor](~/../botbuilder-samples/samples/csharp_dotnetcore/adaptive-dialog/20.EchoBot-declarative/EchoBot.cs?range=14-23&highlight=17,18,20,23)]

<!--
```Csharp
    public class EchoBot : ActivityHandler
    {
        private IStatePropertyAccessor<DialogState> dialogStateAccessor;
        private readonly ResourceExplorer resourceExplorer;
        private DialogManager dialogManager;

        public EchoBot(ConversationState conversationState, ResourceExplorer resourceExplorer)
        {
            this.dialogStateAccessor = conversationState.CreateProperty<DialogState>("RootDialogState");
            this.resourceExplorer = resourceExplorer;
            ...
```
-->

You can optionally set the dialog to update with new settings anytime the underlying `.dialog` file changes using the `resourceExplorer.Changed` method:

[!code-CSharp[resourceExplorer.Changed](~/../botbuilder-samples/samples/csharp_dotnetcore/adaptive-dialog/20.EchoBot-declarative/EchoBot.cs?range=25-32)]

<!--
```Csharp
// auto reload dialogs when file changes
this.resourceExplorer.Changed += (e, resources) =>
{
    if (resources.Any(resource => resource.Id == ".dialog"))
    {
        Task.Run(() => this.LoadRootDialogAsync());
    }
};
```
-->

Finally, you create a declarative dialog resource that you load into the dialog manager like you would any other adaptive dialog.

[!code-CSharp[CreateDeclarativeDialog](~/../botbuilder-samples/samples/csharp_dotnetcore/adaptive-dialog/20.EchoBot-declarative/EchoBot.cs?range=42-45)]

<!--
```Csharp
var resource = this.resourceExplorer.GetResource("main.dialog");
dialogManager = new DialogManager(resourceExplorer.LoadType<AdaptiveDialog>(resource));
dialogManager.UseResourceExplorer(resourceExplorer);
dialogManager.UseLanguageGeneration();
```
-->

**var resource = this.resourceExplorer.GetResource("main.dialog");**

The method `GetResource` reads the declarative file into a resource object and assigns it to `resource`.

**dialogManager = new DialogManager(resourceExplorer.LoadType<AdaptiveDialog>(resource));**

The method `LoadType` returns the first top-level `AdaptiveDialog` object found in `resource`, which in the EchoBot sample is `main.dialog`.

This tells the resource explorer that the `resource` represents an `AdaptiveDialog` instance and to create the object from the file. This is comparable to statement `DialogManager = new DialogManager(Dialog);` used in non-declarative adaptive dialogs, except that the dialog is being read in and created from the declarative file instead of defined in code.

**dialogManager.UseResourceExplorer(resourceExplorer);**

This tells the `dialogManager` to use `resourceExplorer` as the resourceExplorer.

**dialogManager.UseLanguageGeneration();**

If you don't pass in the `LanguageGenerator` parameter in the `UseLanguageGeneration` method call, it defaults to "main.lg".  If "main.lg" does not exist, as is the case in the `EchoBot` sample, it creates a new LanguageGeneration template. In the EchoBot sample there is an in-line LG template used in the dialog. It is defined in `main.dialog':

[!code-json[main.dialog](~/../botbuilder-samples/samples/csharp_dotnetcore/adaptive-dialog/20.EchoBot-declarative/Dialogs/main.dialog?range=1-15&highlight=10)]

# [JavaScript](#tab/javascript)

<!--[!code-JavaScript[AdaptiveDialogComponentRegistration](~/../botbuilder-samples/blob/master/experimental/adaptive-dialog/javascript_nodejs/20.echo-bot-declarative/index.js?range=72-82)]-->

```javascript
let myBot;

const loadRootDialog = () => {
    console.log('(Re)Loading dialogs...');
    // Load root dialog
    let rootDialogResource = resourceExplorer.getResource('echo.dialog');
    myBot = new DialogManager();
    myBot.userState = userState;
    myBot.conversationState = conversationState;
    myBot.rootDialog = resourceExplorer.loadType(rootDialogResource);
}

loadRootDialog();
```

**let rootDialogResource = resourceExplorer.getResource('echo.dialog');**

The method `GetResource` loads the declarative file into the variable `rootDialogResource`.

**myBot.rootDialog = resourceExplorer.loadType(rootDialogResource);**

The method `loadType` returns the first top-level `AdaptiveDialog` object found in `rootDialogResource`, which in the EchoBot sample is `echo.dialog`.

This tells the resource explorer that the `resource` represents an `AdaptiveDialog` instance and to create the object from the file. This is comparable to statement `DialogManager = new DialogManager(Dialog);` used in non-declarative adaptive dialogs, except that the dialog is being read in and created from the declarative file instead of defined in code.

<!--
handle Resource Change when a .dialog or other resource file changes
```JavaScript
const handleResourceChange = (resources) => {
    if (Array.isArray(resources)) {
        if((resources || []).find(r => r.resourceId.endsWith('.dialog')) !== undefined) loadRootDialog();
    } else {
        if (resources.resourceId && resources.resourceId.endsWith('.dialog')) loadRootDialog()
    }
};
```
-->
---

## Test the bot

1. If you have not done so already, install the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme).
1. Run the sample locally on your machine.
1. Start the Emulator, connect to your bot, and send messages as shown below.

![Sample run of the EchoBot declarative dialog code sample](../media/emulator-v4/EchoBot-declarative.png)

<!-- Footnote-style links -->

[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md
[concept-adaptive]: bot-builder-adaptive-dialog-introduction.md
[concept-declarative]: bot-builder-concept-declarative.md

[prompting]: bot-builder-prompts.md
[component-dialogs]: bot-builder-compositcontrol.md

[cs-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/adaptive-dialog/20.EchoBot-declarative

[js-sample]: https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog/javascript_nodejs/20.echo-bot-declarative

[main.dialog]: https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/adaptive-dialog/20.EchoBot-declarative/Dialogs/main.dialog

[echo.dialog]: https://github.com/microsoft/BotBuilder-Samples/tree/master/experimental/adaptive-dialog/javascript_nodejs/20.echo-bot-declarative/dialogs/echo.dialog

[nodejs-modules]: https://nodejs.org/api/modules.html#modules_modules

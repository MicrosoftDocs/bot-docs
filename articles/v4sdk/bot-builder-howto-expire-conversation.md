---
title: Expire conversation guidance - Bot Service
description: Learn how to expire a user's conversation with a bot.
keywords: expire, timeout
author: erdahlva
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 07/14/2020
monikerRange: 'azure-bot-service-4.0'
---

# Expiring a conversation

[!INCLUDE[applies-to](../includes/applies-to.md)]

A bot sometimes needs to restart a conversation from the beginning.  For instance, if a user does not respond after a certain period of time.  This article describes three methods for expiring a conversation:

- Track the last time a message was received from a user, and clear state if the time is greater than a preconfigured length upon receiving the next message from the user. See [User Interaction Expiration](#User-Interaction-Expiration)
- Track the last time a message was received from a user, and run a Web Job or Azure Function to clear the state and/or proactively message the user. See [Proactive Expiration](#Proactive-Expiration)
- Use a storage layer feature, such as CosmosDb Time To Live, to automatically clear state after a preconfigured length of time.  See [Storage Expiration](#Storage-Expiration)

## Prerequisites

- If you don't have an Azure subscription, create a [free](https://azure.microsoft.com/free/) account before you begin.
- Knowledge of [bot basics][concept-basics], [managing state][concept-state], and the [dialogs library][concept-dialogs].
- A copy of the **multi-turn prompt** sample in either [**C#**][cs-sample], [**JavaScript**][js-sample], or [**Python**][python-sample].

## About this sample

The sample code in this article begins with the structure of a multi-turn bot, and extends that bot's functionality by adding additional code (provided below). This extended code demonstrates how to clear conversation state after a certain time period has passed.

## User Interaction Expiration

This type of expiring conversation is accomplished by adding a `LastAccessedTime` property to `MultiTurnPromptBot`'s `DialogBot` implementation. This property value is then compared to the current time within the `ActivityHandler` before processing activities.

# [C#](#tab/csharp)

First, add an `ExpireAfterSeconds` setting to appsettings.json:

```json
{
  "MicrosoftAppId": "",
  "MicrosoftAppPassword": "",
  "ExpireAfterSeconds": 30
}
```

Next, add fields to `DialogBot` and update the constructor. Add local fields for `ExpireAfterSeconds`, `LastAccessedTimeProperty` and `DialogStateProperty`.

**Bots\DialogBot.cs**

Add `IConfiguration` as a parameter to the constructor, retrieve the `ExpireAfterSeconds` and create the required `IStatePropertyAccessor`s:

```csharp
protected readonly int ExpireAfterSeconds;
protected readonly IStatePropertyAccessor<DateTime> LastAccessedTimeProperty;
protected readonly IStatePropertyAccessor<DialogState> DialogStateProperty;

[existing fields omitted]

public DialogBot(IConfiguration configuration, ConversationState conversationState, UserState userState, T dialog, ILogger<DialogBot<T>> logger)
{
    ConversationState = conversationState;
    UserState = userState;
    Dialog = dialog;
    Logger = logger;

    TimeoutSeconds = configuration.GetValue<int>("ExpireAfterSeconds");
    DialogStateProperty = ConversationState.CreateProperty<DialogState>(nameof(DialogState));
    LastAccessedTimeProperty = ConversationState.CreateProperty<DateTime>(nameof(LastAccessedTimeProperty));
}
```

Finally, add code to `DialogBot`'s `OnTurnAsync` method:

```csharp
public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
{
    // Retrieve the property value, and compare it to the current time.
    var lastAccess = await LastAccessedTimeProperty.GetAsync(turnContext, () => DateTime.UtcNow, cancellationToken).ConfigureAwait(false);
    if ((DateTime.UtcNow - lastAccess) >= TimeSpan.FromSeconds(ExpireAfterSeconds))
    {
        // Notify the user that the conversation is being restarted.
        await turnContext.SendActivityAsync("Welcome back!  Let's start over from the beginning.");

        // Clear conversation state.
        await ConversationState.ClearStateAsync(turnContext, cancellationToken).ConfigureAwait(false);
    }

    await base.OnTurnAsync(turnContext, cancellationToken);

    // Set LastAccessedTime to the current time.
    await LastAccessedTimeProperty.SetAsync(turnContext, DateTime.UtcNow, cancellationToken);

    // Save any state changes that might have occurred during the turn.
    await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken);
    await UserState.SaveChangesAsync(turnContext, false, cancellationToken);
}
```

# [JavaScript](#tab/javascript)

First, add an `ExpireAfterSeconds` setting to .env:

```json
MicrosoftAppId=
MicrosoftAppPassword=
ExpireAfterSeconds=30
```

Next, add fields to `DialogBot` and update the constructor. Add local fields for `expireAfterSeconds` and `lastAccessedTimeProperty`.

**bots\dialogBot.js**

Add `expireAfterSeconds` as a parameter to the constructor and create the required `StatePropertyAccessor`:

```javascript
constructor(expireAfterSeconds, conversationState, userState, dialog) {

    [existing code omitted]

    this.lastAccessedTimeProperty = this.conversationState.createProperty('LastAccessedTime');
    this.expireAfterSeconds = expireAfterSeconds;

    [existing code omitted]
}
```

Add code to `DialogBot`'s `run` method:

```javascript
async run(context) {
    // Retrieve the property value, and compare it to the current time.
    const now = new Date();
    const lastAccess = new Date(await this.lastAccessedTimeProperty.get(context, now.toISOString()));
    if (now.getTime() != lastAccess.getTime() && ((now.getTime() - lastAccess.getTime()) / 1000) >= this.expireAfterSeconds)
    {
        // Notify the user that the conversation is being restarted.
        await context.sendActivity("Welcome back!  Let's start over from the beginning.");

        // Clear conversation state.
        await this.conversationState.clear(context);
    }

    await super.run(context);

    // Set LastAccessedTime to the current time.
    await this.lastAccessedTimeProperty.set(context, now.toISOString());

    // Save any state changes. The load happened during the execution of the Dialog.
    await this.conversationState.saveChanges(context, false);
    await this.userState.saveChanges(context, false);
}
```

Lastly, update `index.js` to send the `ExpireAfterSeconds` parameter to `DialogBot`:

```javascript
const bot = new DialogBot(process.env.ExpireAfterSeconds, conversationState, userState, dialog);
```

## [Python](#tab/python)


First, add an `ExpireAfterSeconds` setting to config.py:

```python
PORT = 3978
APP_ID = os.environ.get("MicrosoftAppId", "")
APP_PASSWORD = os.environ.get("MicrosoftAppPassword", "")
EXPIRE_AFTER_SECONDS = os.environ.get("ExpireAfterSeconds", 30)
```

Next, add fields to `DialogBot` and update the constructor. Add local fields for `expire_after_seconds` and `last_accessed_time_property`.

**bots\dialog_bot.py**

Add `expire_after_seconds` as a parameter to the constructor and create the required `StatePropertyAccessor`:

```python
def __init__(
    self,
    expire_after_seconds: int,
    conversation_state: ConversationState,
    user_state: UserState,
    dialog: Dialog,
):
    [existing code omitted]

    self.expire_after_seconds = expire_after_seconds
    self.conversation_state = conversation_state
    self.user_state = user_state
    self.dialog = dialog
```

Add code to `DialogBot`'s `on_turn` method:

<!-- add python code for on_turn -->
```python

```

Lastly, update `app.py` to send the `EXPIRE_AFTER_SECONDS` parameter to `DialogBot`:

```python
BOT = DialogBot(CONFIG.EXPIRE_AFTER_SECONDS, CONVERSATION_STATE, USER_STATE, DIALOG)
```

---

## Proactive Expiration

<!-- add Azure Function with ConversationReference solution -->

## Storage Expiration

<!-- add CosmosDb TTL solution -->

## To test the bot

1. If you have not done so already, install the [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme).
1. Run the sample locally on your machine.
1. Start the emulator, connect to your bot, and send a messsage to it.
1. After one of the prompts, wait 30 seconds before responding.





<!-- Footnote-style links -->

[concept-basics]: bot-builder-basics.md
[concept-state]: bot-builder-concept-state.md
[concept-dialogs]: bot-builder-concept-dialog.md

[cs-sample]: https://aka.ms/cs-multi-prompts-sample
[js-sample]: https://aka.ms/js-multi-prompts-sample
[python-sample]: https://aka.ms/python-multi-prompts-sample


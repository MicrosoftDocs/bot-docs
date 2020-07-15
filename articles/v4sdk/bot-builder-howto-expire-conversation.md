---
title: Expire conversation guidance - Bot Service
description: Learn how to expire a user's conversation with a bot.
keywords: expire, timeout
author: ericdahlvang
ms.author: ericdahlvang
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 07/14/2020
monikerRange: 'azure-bot-service-4.0'
---

# Expiring a conversation

[!INCLUDE[applies-to](../includes/applies-to.md)]

A bot sometimes needs to restart a conversation from the beginning.  For instance, if a user does not respond after a certain period of time.  This article describes three methods for expiring a conversation:

- Track the last time a message was received from a user, and clear state if the time is greater than a preconfigured length upon receiving the next message from the user. See [User Interaction Expiration](#user-interaction-expiration)
- Track the last time a message was received from a user, and run a Web Job or Azure Function to clear the state and/or proactively message the user. See [Proactive Expiration](#proactive-expiration)
- Use a storage layer feature, such as CosmosDb Time To Live, to automatically clear state after a preconfigured length of time.  See [Storage Expiration](#storage-expiration)

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
        await turnContext.SendActivityAsync("Welcome back!  Let's start over from the beginning.").ConfigureAwait(false);

        // Clear state.
        await UserState.ClearStateAsync(turnContext, cancellationToken).ConfigureAwait(false);
        await ConversationState.ClearStateAsync(turnContext, cancellationToken).ConfigureAwait(false);
    }

    await base.OnTurnAsync(turnContext, cancellationToken).ConfigureAwait(false);

    // Set LastAccessedTime to the current time.
    await LastAccessedTimeProperty.SetAsync(turnContext, DateTime.UtcNow, cancellationToken).ConfigureAwait(false);

    // Save any state changes that might have occurred during the turn.
    await ConversationState.SaveChangesAsync(turnContext, false, cancellationToken).ConfigureAwait(false);
    await UserState.SaveChangesAsync(turnContext, false, cancellationToken).ConfigureAwait(false);
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
    if (now != lastAccess && ((now.getTime() - lastAccess.getTime()) / 1000) >= this.expireAfterSeconds)
    {
        // Notify the user that the conversation is being restarted.
        await context.sendActivity("Welcome back!  Let's start over from the beginning.");

        // Clear state.
        await this.userState.clear(context);
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
    self.dialog_state_property = conversation_state.create_property("DialogState")
    self.last_accessed_time_property = conversation_state.create_property("LastAccessedTime")
    self.conversation_state = conversation_state
    self.user_state = user_state
    self.dialog = dialog
```

Change `on_message_activity` so it uses the `dialog_state_property`:

```python
async def on_message_activity(self, turn_context: TurnContext):
    await DialogHelper.run_dialog(
        self.dialog,
        turn_context,
        self.dialog_state_property,
    )
```

Add code to `DialogBot`'s `on_turn` method:

```python
async def on_turn(self, turn_context: TurnContext):
    # Retrieve the property value, and compare it to the current time.
    now_seconds = int(time.time())
    last_access = int(
        await self.last_accessed_time_property.get(turn_context, now_seconds)
    )
    if now_seconds != last_access and (
        now_seconds - last_access >= self.expire_after_seconds
    ):
        # Notify the user that the conversation is being restarted.
        await turn_context.send_activity(
            "Welcome back!  Let's start over from the beginning."
        )

        # Clear state.
        await self.user_state.clear_state(turn_context)
        await self.conversation_state.clear_state(turn_context)
        await self.conversation_state.save_changes(turn_context, True)
        await self.user_state.save_changes(turn_context, True)

    await super().on_turn(turn_context)

    # Set LastAccessedTime to the current time.
    await self.last_accessed_time_property.set(turn_context, now_seconds)

    # Save any state changes that might have ocurred during the turn.
    await self.conversation_state.save_changes(turn_context)
    await self.user_state.save_changes(turn_context)
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


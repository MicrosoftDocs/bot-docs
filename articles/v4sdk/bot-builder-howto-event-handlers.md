---
redirect_url: /bot-framework/bot-builder-concept-middleware
---

<!-- 
# Using event handlers

[!INCLUDE [pre-release-label](../includes/pre-release-label.md)]

Event handlers are functions we can add to future activity events within a [turn](bot-builder-basics.md#defining-a-turn). Those activities are `SendActivity`, `UpdateActivity`, and `DeleteActivity`, and each has it's own handler. These handlers are useful when you need to do something on every future activity of that type for the current context object.

You can add multiple event handlers to each activity event, and they will process for every event **after** they are added, so where in the code you add them matters.

> [!NOTE]
> *Event handlers* in this article are not adhering to traditional language specific definitions of *events* and *handlers*. Instead, they refer to the more conceptual programming idea of *events* and *handlers*.

## Using the *next* delegate

By calling the *next* delegate, each handler passes execution to the following handler in the order in which the handlers were added. The last *next* delegate is a call to the adapter to actually send, update, or delete the activity.

Unless otherwise intended, it is important to call *next()* within your handler, otherwise you will [short circuit](bot-builder-create-middleware.md#short-circuit-routing) the activity. The difference between short circuiting an activity versus middleware is that short circuiting completely cancels the activity, whereas middleware changes what code is allowed to run, but the activity’s execution continues.

For send activities, if successful, the *next* delegate returns the ID(s) assigned to the sent message(s).

## Expected return value

For the return value, the event handler expects it to be the result of the next delegate. If needed, that result can be viewed and stored before returning it, or it can be returned directly as is seen below.

The return value of the `SendActivity` handler is the return value that gets passed back up through the chain as the return value of the corresponding next delegate.

# [C#](#tab/cseventhandler)

The three types of event handlers provided are `OnSendActivities()`, `OnUpdateActivity()`, and `OnDeleteActivity()`. Here, we're adding an `OnSendActivities()` handler within our bot code, however handlers can be added in middleware code as well.

Handlers are often added as lambda expressions, which you'll see in this example. Here, we will listen to the user's input activity when **help** is written.

```cs
public Task OnTurn(ITurnContext context)
{
    if (context.Activity.Type == ActivityTypes.Message)
    {
        // Get the conversation state from the turn context
        
        context.OnSendActivities(async (handlerContext, activities, handlerNext) =>
        {
            if (handlerContext.Activity.Text == "help")
            {
                Console.WriteLine("help!");
                // Do whatever logging you want to do for this help message
            }

            return await handlerNext();
        });
        // ...
    }
}
```

# [JavaScript](#tab/jseventhandler)

The three types of event handlers provided are `onSendActivities()`, `onUpdateActivity()`, and `onDeleteActivity()`. Here, we're adding an `onSendActivities()` handler within our bot code, however handlers can be added in middleware code as well.

This example will listen to the user's input activity when **help** is written.

```js
adapter.processActivity(req, res, async (context) => {

    if (context.activity.type === 'message') {

        context.onSendActivities(async (handlerContext, activities, handlerNext) => { 
            
            if(handlerContext.activity.text === 'help'){
                console.log('help!')
                // Do whatever logging you want to do for this help message
            }
            // Add handler logic here
        
            await handlerNext(); 
        });
        await context.sendActivity(`you said ${context.activity.text}`);
    }
});
```

---

It's important to differentiate between *send activity* and *update or delete activity* events, where the first creates an entirely new activity event and the later acts on a past activity. Also. not all channels support *update* or *delete* activity. It's suggested to add the appropriate exception handling around calls to these and their handlers to account for that possibility.

-->

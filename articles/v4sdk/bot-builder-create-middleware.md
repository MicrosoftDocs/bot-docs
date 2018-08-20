---
title: Create your own middleware | Microsoft Docs
description: Understand how to write your own middleware.
keywords: middleware, custom middleware, short circuit, fallback, activity handlers
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 03/21/2018
monikerRange: 'azure-bot-service-4.0'
---

# Create your own middleware

Middleware allows you to write rich plugins for your bots, that can then be used by others as well. Here we'll show how to add and implement basic middleware, and show how it works. The v4 SDK provides some middleware for you, for things such as state management, LUIS, QnAMaker, and translation. Take a look at the bot builder SDK for [.NET](https://github.com/Microsoft/botbuilder-dotnet) or [JavaScript](https://github.com/Microsoft/botbuilder-js) for more information.

## Adding middleware

In the example below, based on our basic bot sample created through the [Get Started](~/bot-service-quickstart.md) experience, two different pieces of middleware are added to our services with a new instance of each of those classes.

> [!IMPORTANT]
> Remember, the order in which they are added to the options determines the order in which they are executed. Be sure to consider how that will work if using more than one piece of middleware.

**Startup.cs**

# [C#](#tab/csaddmiddleware)

Add `options.Middleware.Add(new MyMiddleware());` method calls to your bot service options for each piece of middleware you want to add.

```csharp
public void ConfigureServices(IServiceCollection services)
{
    services.AddSingleton(_ => Configuration);
    services.AddBot<HelloBot>(options =>
    {
        options.CredentialProvider = new ConfigurationCredentialProvider(Configuration);
        options.Middleware.Add(new MyMiddleware());
        options.Middleware.Add(new MyOtherMiddleware());
    });
}
```
# [JavaScript](#tab/jsaddmiddleware)

Add `adapter.use(MyMiddleware());` to your adapter for each piece of middleware you want to add.

```javascript
// Create adapter
const adapter = new botbuilder.BotFrameworkAdapter({
    appId: process.env.MICROSOFT_APP_ID, 
    appPassword: process.env.MICROSOFT_APP_PASSWORD 
});

adapter.use(MyMiddleware());
adapter.use(MyOtherMiddleware());
```

---


## Implementing your Middleware

Each piece of middleware inherits from a middlware interface, and always implements it's processing handler, which is run on every activity that gets sent to your bot. For each piece of middleware added, the processing handler gets a chance to modify the context object or perform a task, such as logging, before allowing other middleware or bot logic to interact with the context object as it continues down the pipeline.

# [C#](#tab/csetagoverwrite)

Each piece of middleware inherits from `IMiddleware` and always implements `OnTurn()`.

**ExampleMiddleware.cs**
```csharp
public class MyMiddleware : IMiddleware
{
    public async Task OnTurn(ITurnContext context, MiddlewareSet.NextDelegate next)
    {            
        // This simple middleware reports the request type and if we responded
        await context.SendActivity($"Request type: {context.Activity.Type}");
        
        await next();            

        // Report if any responses were recorded
        string response = context.Responded ? "yes" : "no";
        await context.SendActivity($"Responded?  {response}");
    }
}

public class MyOtherMiddleware : IMiddleware
{
    public async Task OnTurn(ITurnContext context, MiddlewareSet.NextDelegate next)
    {
        // simple middleware to add an additional send activity
        await context.SendActivity($"My other middleware just saying Hi before the bot logic");

        await next();
    }
}

```

# [JavaScript](#tab/jsimplementmiddleware)

Each piece of middleware inherits from `MiddlewareSet` and always implements `onTurn()`.

**ExampleMiddleware.js**
```js
adapter.use({onTurn: async (context, next) =>{

    // This simple middleware reports the activity type and if we responded
    await context.sendActivity(`Activity type: ${context.activity.type}`); 
    await next();            

    // Report if any responses were recorded
    const response = context.activity.text ? "yes" : "no";
    await context.sendActivity(`Responded?  ${response}`);

}}, {onTurn: async (context, next) => {

     // simple middleware to add an additional send activity
     await context.sendActivity("My other middleware just saying Hi before the bot logic");

     await next();
}})
```

---

Calling `next()` causes execution to continue to the next piece of middleware. The ability to choose when execution is passed on allows you to write code that runs **after** the rest of the middleware stack has run. We can take action on the 'trailing edge' of the processing handler, after the bot logic and other middleware has run.  In the example above, the first middleware implemented did just that, reporting if we responded for this context object, before passing execution back up the pipeline.

## Short circuit routing

In some cases, you may want to stop any further processing of the received activity, which we call short-circuiting. This is useful for cases where the middleware completely takes care of a request, provides an easy response for specific commands, or otherwise can handle an incoming request without the bot logic needing to see it.

Let's create a piece of middleware that will send a reply and prevent any further routing of the request anytime the user says "ping":

# [C#](#tab/csmiddlewareshortcircuit)
```cs
public class ExampleMiddleware : IMiddleware
{
    public async Task OnTurn(ITurnContext context, MiddlewareSet.NextDelegate next)
    {
        var utterance = context.Activity?.AsMessageActivity()?.Text.Trim().ToLower();

        if (utterance == "ping") 
        {
            context.SendActivity("pong");
            return;
        } 
        else 
        {
            await next();
        }
    }
}
```
# [JavaScript](#tab/jsmiddlewareshortcircuit)
```JavaScript
adapter.use({onTurn: async (context, next) =>{
    const utterance = (context.activity.text || '').trim().toLowerCase();
        if (utterance == "ping") 
        {
            await context.sendActivity("pong");
            return;
        } 
        else 
        {
            await next();
        }

}})

```

---

## Fallback processing

Another thing you might need to do is respond to a request that has not been responded to yet. This is easily accomplished using the trailing edge of the processing handler by checking the `context.Responded` property. Let's create a simple piece of middleware that automatically says "I didn't understand," should the bot fail to handle the request:

# [C#](#tab/csfallback)
```cs
public async Task OnTurn(ITurnContext context, MiddlewareSet.NextDelegate next)
{
    await next();

    if (!context.Responded) 
    {
        context.SendActivity("I didn't understand.");
    }
}
```
# [JavaScript](#tab/jsfallback)
```JavaScript
adapter.use({onTurn: async (context, next) =>{
    await next();

    if (!context.responded) 
    {
       await context.sendActivity("I didn't understand.");
    }

}})
```

---

> [!NOTE] 
> This might not work in all cases, such as when other middleware might be able to respond to the user or when the bot receives a message correctly but does not reply. Responding with, "I don't understand" would be misleading to our user.



---
layout: page
title: Dialog Chains
permalink: /en-us/csharp/builder/dialogs-chains/
weight: 2350
parent1: Building your Bot Using Bot Builder for .NET
parent2: Chat Bots
---


<span style="color:red">This section needs more details. Why/when would you want to do this? I only see a couple of dialog examples that use this.</span>

Explicit management of the stack of active dialogs is possible through **IDialogStack.Call<R>** and **IDialogStack.Done<R>**, explicitly composing dialogs into a larger conversation. It is also possible to implicitly manage the stack of active dialogs through the fluent Chain methods.

The following are the possible chain methods that you can use.

|Name|Type|Notes
|----|----|-----
|Chain.Select<T, R>| LINQ | Supports "select" and "let" in LINQ query syntax.
|Chain.SelectMany<T, C, R>   | LINQ    | Supports successive "from" in LINQ query syntax.
|Chain.Where<T>              | LINQ    | Supports "where" in LINQ query syntax.
|Chain.From<T>               | Chains  | Instantiates a new instance of a dialog.
|Chain.Return<T>             | Chains  | Returns a constant value into the chain.
|Chain.Do<T>                 | Chains  | Allows for side-effects within the chain.
|Chain.ContinueWith<T, R>    | Chains  | Simple chaining of dialogs.
|Chain.Unwrap<T>             | Chains  | Unwrap a dialog nested in a dialog.
|Chain.DefaultIfException<T> | Chains  | Swallows an exception from the previous result and returns default(T).
|Chain.Loop<T>               | Branch  | Loops the entire chain of dialogs.
|Chain.Fold<T>               | Branch  | Folds results from an enumeration of dialogs into a single result.
|Chain.Switch<T, R>          | Branch  | Supports branching into different dialog chains.
|Chain.PostToUser<T>         | Message | Posts a message to the user.
|Chain.WaitToBot<T>          | Message | Waits for a message to the bot.
|Chain.PostToChain<T>        | Message | Starts a chain with a message from the user.


The LINQ query syntax uses the **Chain.Select<T, R>** method:

{% highlight csharp %}
            var query = from x in new PromptDialog.PromptString(Prompt, Prompt, attempts: 1)
                        let w = new string(x.Reverse().ToArray())
                        select w;
{% endhighlight %}

Or the **Chain.SelectMany<T, C, R>** method:

{% highlight csharp %}
            var query = from x in new PromptDialog.PromptString("p1", "p1", 1)
                        from y in new PromptDialog.PromptString("p2", "p2", 1)
                        select string.Join(" ", x, y);
{% endhighlight %}


The **Chain.PostToUser\<T\>** and **Chain.WaitToBot\<T\>** methods post messages from the bot to the user and vice versa:

{% highlight csharp %}
            query = query.PostToUser();
{% endhighlight %}

The **Chain.Switch<T, R>** method branches the conversation dialog flow:

{% highlight csharp %}
            var logic =
                toBot
                .Switch
                (
                    new RegexCase<string>(new Regex("^hello"), (context, text) =>
                    {
                        return "world!";
                    }),
                    new Case<string, string>((txt) => txt == "world", (context, text) =>
                    {
                        return "!";
                    }),
                    new DefaultCase<string, string>((context, text) =>
                   {
                       return text;
                   }
                )
            );
{% endhighlight %}


If **Chain.Switch<T, R>** returns a nested **IDialog<IDialog\<T\>>**, then the inner **IDialog\<T\>** can be unwrapped with **Chain.Unwrap\<T\>**. This allows branching conversations to different paths of chained dialogs, possibly of unequal length. The following shows a more complete example of branching dialogs written in the fluent chain style with implicit stack management:

{% highlight csharp %}
            var joke = Chain
                .PostToChain()
                .Select(m => m.Text)
                .Switch
                (
                    Chain.Case
                    (
                        new Regex("^chicken"),
                        (context, text) =>
                            Chain
                            .Return("why did the chicken cross the road?")
                            .PostToUser()
                            .WaitToBot()
                            .Select(ignoreUser => "to get to the other side")
                    ),
                    Chain.Default<string, IDialog<string>>(
                        (context, text) =>
                            Chain
                            .Return("why don't you like chicken jokes?")
                    )
                )
                .Unwrap()
                .PostToUser().
                Loop();
{% endhighlight %}



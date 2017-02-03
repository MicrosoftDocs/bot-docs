---
layout: page
title: Using a Dialog's Session to Send Messages and More
permalink: /en-us/node/builder/chat/session/
weight: 1120
parent1: Building your Bot Using the Bot Builder for Node.js
parent2: Chat Bots
---

You use the dialog's [session](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session) object to inspect the user's message, send a reply to the user, save state on behalf of the user, or redirect to another dialog. The **session** object is passed to your [dialog handlers](/en-us/node/builder/chat/dialogs/#dialog-handlers) anytime your bot receives a message from the user. 


### Sending messages

To send messages, attachments, and rich cards to the user, you can use the [session.send()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#send) method. Your bot is free to call `send()` as many times as it likes in response to a single message from the user. When sending multiple replies, the individual replies will be automatically grouped into a batch and delivered to the user as a set in an effort to preserve the original order of the messages. 

<span style="color:red"><< i thought there was another way of sending messages that was better than .send? >></span>

<span style="color:red"><< what about channel throttling considerations? >></span>

> __Auto Batching__
> 
> When a bot sends multiple replies to a user using `session.send()`, those replies will automatically be grouped into a batch using a feature called “auto batching.”  Auto batching works by waiting a default of 250ms after every call to `send()` for an additional call to `send()`. 

> To avoid a 250ms pause after the last call to `send()`, you can manually trigger delivery of the batch by calling [session.sendBatch()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#sendbatch). In practice, it’s rare that you will actually need to call sendBatch() because the 
> built-in [prompts](/en-us/node/builder/chat/prompts/) and [session.endConversation()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#endconversation) will automatically call `sendBatch()` for you.
>
> The goal of batching is to try and avoid multiple messages from the bot being displayed out of order. Unfortunately, not all chat clients can guarantee this. In particular, the clients tend to want to download images before displaying a message to the user so if you send a message 
> containing an image followed immediately by a message without images, you’ll sometimes see the messages flipped in the user's feed. 
>
>To minimize the chance of this, you can try to insure that your images are coming from CDNs and try to avoid the use of overly large images. In extreme cases, you may even need to insert a 1-2 second delay between the message with the image and the one without. You can make this delay feel a bit more natural to the user by calling [session.sendTyping()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#sendtyping) before starting 
> your delay.
>
> The auto batching delay is [configurable](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iuniversalbotsettings.html#autobatchdelay). To disable auto batching, you can set the default delay to a large number and then manually 
> call `sendBatch()` with a callback that will be invoked after the batch has been delivered.

<span style="color:red"><< Where/how do you configure it? >></span> 

### Text messages

To send a simple text message to the user, you can simply call `session.send("hello there")`. The message can also contain template parameters (for example, `session.send("hello there %s", name)`).  The SDK uses a library called [node-sprintf](https://github.com/maritz/node-sprintf) to implement the template functionality. For details about using templates, see the [sprintf documentation](http://www.diveintojavascript.com/projects/javascript-sprintf).

> __NOTE:__ One word of caution about using named parameters with `sprintf`: It’s a common mistake to forget the trailing format specifier when using named parameters (for example, the ‘s’ in `session.send("Hello there %(name)s", user)`). If it happens, you’ll get an invalid template exception at runtime. If you do, verify that your templates are correct.

### Attachments

Many chat services support sending image, video, and file attachments to the user. You can use `session.send()` for this as well but you’ll need to use it in conjunction with the SDK’s [Message](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message) builder class. You can use either the [attachments()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message#attachments) or [addAttachment()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message#addattachment) methods to create a message containing an image.

{% highlight JavaScript %}
bot.dialog('/picture', [
    function (session) {
        session.send("You can easily send pictures to a user...");
        var msg = new builder.Message(session)
            .attachments([{
                contentType: "image/jpeg",
                contentUrl: "http://www.theoldrobots.com/images62/Bender-18.JPG"
            }]);
        session.endDialog(msg);
    }
]);
{% endhighlight %}

### Cards

Several chat services support sending rich cards containing text, images, and even action buttons to the user. Not all channels support cards or have the same level of richness. To determine which channels support rich cards, see the [Channel Inspector](/en-us/channel-inspector). 

The BotBuilder SDK includes the [HeroCard](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.herocard) and [ThumbnailCard](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.thumbnailcard) classes. These cards can contain text, an image, and optional buttons. The difference between these two cards is the size of the image, but it's up to the channel to determine how to render the image.

{% highlight JavaScript %}
bot.dialog('/cards', [
    function (session) {
        var msg = new builder.Message(session)
            .textFormat(builder.TextFormat.xml)
            .attachments([
                new builder.HeroCard(session)
                    .title("Hero Card")
                    .subtitle("Space Needle")
                    .text("The <b>Space Needle</b> is an observation tower in Seattle, Washington, a landmark of the Pacific Northwest, and an icon of Seattle.")
                    .images([
                        builder.CardImage.create(session, "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/Seattlenighttimequeenanne.jpg/320px-Seattlenighttimequeenanne.jpg")
                    ])
                    .tap(builder.CardAction.openUrl(session, "https://en.wikipedia.org/wiki/Space_Needle"))
            ]);
        session.endDialog(msg);
    }
]);
{% endhighlight %}

Cards typically support tap actions which let you specify what should happen when the user taps on the card (for example, open a link or send a message).  You can use the [CardAction](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.cardaction) class to customize this behavior. There are several different types of actions you can specify but most channels only support a handful of actions. For maximum compatibility, you’ll generally want to use the [openUrl()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.cardaction#openurl), [postBack()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.cardaction#postback), and [dialogAction()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.cardaction#dialogaction) actions.

> __NOTE:__ The differences between [postBack()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.cardaction#postback) and [imBack()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.cardaction#imback) actions is subtle. The intention is that `imBack()` will show the message being sent to the bot in the user's feed whereas `postBack()` will hide the sent message from the user. 
>Note that if the channel doesn't support `postBack`, the channel will treat the message as `imBack`. This generally won't change how your bot behaves but it does mean that if you're including data such as an order ID in your `postBack`, it may be visible on certain channels when you didn't expect it to be.

The SDK also supports more specialized card types such as [ReceiptCard](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.receiptcard) and [SigninCard](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.signincard). It’s not practical for the SDK to support every card or attachment format that the underlying channels support. For those cards and attachments that the SDK does not support, you'd use the [Message.sourceEvent()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.message#sourceevent) method to send custom messages and attachments in the channel's native schema.

{% highlight JavaScript %}
bot.dialog('/receipt', [
    function (session) {
        session.send("You can send a receipts for facebook using Bot Builders ReceiptCard...");
        var msg = new builder.Message(session)
            .attachments([
                new builder.ReceiptCard(session)
                    .title("Recipient's Name")
                    .items([
                        builder.ReceiptItem.create(session, "$22.00", "EMP Museum").image(builder.CardImage.create(session, "https://upload.wikimedia.org/wikipedia/commons/a/a0/Night_Exterior_EMP.jpg")),
                        builder.ReceiptItem.create(session, "$22.00", "Space Needle").image(builder.CardImage.create(session, "https://upload.wikimedia.org/wikipedia/commons/7/7c/Seattlenighttimequeenanne.jpg"))
                    ])
                    .facts([
                        builder.Fact.create(session, "1234567898", "Order Number"),
                        builder.Fact.create(session, "VISA 4076", "Payment Method")
                    ])
                    .tax("$4.40")
                    .total("$48.40")
            ]);
        session.send(msg);

        session.send("Or using facebooks native attachment schema...");
        msg = new builder.Message(session)
            .sourceEvent({
                facebook: {
                    attachment: {
                        type: "template",
                        payload: {
                            template_type: "receipt",
                            recipient_name: "Stephane Crozatier",
                            order_number: "12345678902",
                            currency: "USD",
                            payment_method: "Visa 2345",        
                            order_url: "http://petersapparel.parseapp.com/order?order_id=123456",
                            timestamp: "1428444852", 
                            elements: [
                                {
                                    title: "Classic White T-Shirt",
                                    subtitle: "100% Soft and Luxurious Cotton",
                                    quantity: 2,
                                    price: 50,
                                    currency: "USD",
                                    image_url: "http://petersapparel.parseapp.com/img/whiteshirt.png"
                                },
                                {
                                    title: "Classic Gray T-Shirt",
                                    subtitle: "100% Soft and Luxurious Cotton",
                                    quantity: 1,
                                    price: 25,
                                    currency: "USD",
                                    image_url: "http://petersapparel.parseapp.com/img/grayshirt.png"
                                }
                            ],
                            address: {
                                street_1: "1 Hacker Way",
                                street_2: "",
                                city: "Menlo Park",
                                postal_code: "94025",
                                state: "CA",
                                country: "US"
                            },
                            summary: {
                                subtotal: 75.00,
                                shipping_cost: 4.95,
                                total_tax: 6.19,
                                total_cost: 56.14
                            },
                            adjustments: [
                                { name: "New Customer Discount", amount: 20 },
                                { name: "$10 Off Coupon", amount: 10 }
                            ]
                        }
                    }
                }
            });
        session.endDialog(msg);
    }
]);
{% endhighlight %}
 
### Typing indicator

Not all chat services support sending typing events, but for those that do, you can use [session.sendTyping()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#sendtyping) to tell the user that the bot is actively composing a reply. This is particular useful if you’re about to begin an asynchronous operation that could take a few seconds to complete. The amount of time the indicator is shown varies by service (Slack is 3 seconds and Facebook is 20 seconds) so as a general rule, if you need the indicator to be shown for more than a few seconds, you should add logic to call `sendTyping` periodically.

{% highlight JavaScript %}
bot.dialog('/countItems', function (session, args) {
    session.sendTyping();
    lookupItemsAsync(args, function (err, items) {
        if (!err) {
            session.send("%d items found", items.length);
        } else {
            session.error(err);
        }
    });
});
{% endhighlight %}



### Using session in callbacks

Inevitably you're going to want to make an asynchronous network call to retrieve data and then send those results to the user using the session object. This is completely fine but there are a few best practices you’ll want to follow.

#### When it's OK to use session

If you’re making an asynchronous call in the context of a message you received from the user, it's generally okay to call `session.send()`:

<span style="color:red"><< why does this dialog use 'listItems' but everywhere else we use '/listItems'? >></span>

{% highlight JavaScript %}
bot.dialog('listItems', function (session) {
    session.sendTyping();
    lookupItemsAsync(function (results) {
        // OK to call session.send() here.
        session.send(results.message);
    });
});
{% endhighlight %}

#### When it's not OK to use session

Here’s a common mistake that developers make: They start an asynchronous call and then immediately call something like `session.endDialog()` which changes their bot's conversation state.

{% highlight JavaScript %}
bot.dialog('listItems', function (session) {
    session.sendTyping();
    lookupItemsAsync(function (results) {
        // Calling session.send() here is dangerous because you've done an action that's
        // triggered a change in your bot's conversation state.
        session.send(results.message);
    });
    session.endDialog();
});
{% endhighlight %}

In general, this is a pattern you should avoid. The correct way to achieve the above behavior is to move the `endDialog()` call into your callback.

{% highlight JavaScript %}
bot.dialog('listItems', function (session) {
    session.sendTyping();
    lookupItemsAsync(function (results) {
        // Calling session.send() here is dangerous because you've done an action that's
        // triggered a change in your bots conversation state.
        session.endDialog(results.message);
    });
});
{% endhighlight %}

#### When it's dangerous to use session

The other case where you probably shouldn’t use session (or at least should be careful) is when you’re doing a long running task and you want to communicate with the user at the beginning and end of the task.

{% highlight JavaScript %}
bot.dialog('orderPizza', function (session, args) {
    session.send("Starting your pizza order...");
    queue.startOrder({ session: session, order: args.order });
});

queue.orderReady(function (session, order) {
    session.send("Your pizza is on its way!");
});
{% endhighlight %}

This can be dangerous because the bot's server could crash or the user could send other messages while the bot is doing the task and that could leave the bot in a bad conversational state. The better approach is to persist the user's [session.message.address](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage.html#address) object and then send them a proactive message using [bot.send()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.universalbot.html#send) after their order is ready. 

{% highlight JavaScript %}
bot.dialog('orderPizza', function (session, args) {
    session.send("Starting your pizza order...");
    queue.startOrder({ address: session.message.address, order: args.order });
});

queue.orderReady(function (address, order) {
    var msg = new builder.Message()
        .address(address)
        .text("Your pizza is on its way!");
    bot.send(msg);
});
{% endhighlight %}

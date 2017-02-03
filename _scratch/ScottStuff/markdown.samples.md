---
layout: page
title: Markdown Samples (Remove)
permalink: /en-us/bot-intelligence/markdownsamples/
weight: 180
parent1: none
---

A few markdown samples to get you started in our documentation. Refer to [the full Markdown reference](https://daringfireball.net/projects/markdown/syntax) for more tips.

* TOC
{:toc}


## Open link in a new window

[This link opens in a new window](https://dev.botframework.com/e){:target="_blank"}

{% highlight markdown %}
{% raw %}

[This link opens in a new window](https://dev.botframework.com/e){:target="_blank"}

{% endraw %}
{% endhighlight %}


## tables

|**Support type**                    | **Contact**                                                
|:----------------------------:|---------------------------------
|**General site/BotFramework feedback and suggestions**| <a href="http://feedback.botframework.com/" target="_blank">http://feedback.botframework.com/</a>
|**Using a bot** | Contact the bot's developer through their publisher e-mail                 
|**Building bots and connecting to the Bot Framework** | Use StackOverflow, with the hashtag [#botframework](https://stackoverflow.com/questions/tagged/botframework)
|**Issues with the Bot Builder SDK**| Use the issues tab on our <a href="https://github.com/Microsoft/BotBuilder/" target="_blank">Git repo</a>
|**Reporting Abuse**| Contact us at [bf-reports@microsoft.com](mailto://bf-reports@microsoft.com) 

{% highlight markdown %}
{% raw %}

|**Support type**                    | **Contact**                                                
|:----------------------------:|---------------------------------
|**General site/BotFramework feedback and suggestions**| <a href="http://feedback.botframework.com/" target="_blank">http://feedback.botframework.com/</a>
|**Using a bot** | Contact the bot's developer through their publisher e-mail                 
|**Building bots and connecting to the Bot Framework** | Use StackOverflow, with the hashtag [#botframework](https://stackoverflow.com/questions/tagged/botframework)
|**Issues with the Bot Builder SDK**| Use the issues tab on our <a href="https://github.com/Microsoft/BotBuilder/" target="_blank">Git repo</a>
|**Reporting Abuse**| Contact us at [bf-reports@microsoft.com](mailto://bf-reports@microsoft.com) 

{% endraw %}
{% endhighlight %}



## Text note

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer accumsan turpis quis lorem maximus posuere. Aenean maximus tristique tellus, eu vestibulum massa. Pellentesque viverra quam urna, et egestas elit pretium eget. Duis vulputate gravida est, eget condimentum arcu fermentum vitae. Nulla facilisi. Vivamus tempus, neque ut porttitor finibus, ex dui gravida turpis, eu finibus tortor odio sed lectus. Aliquam pretium, sapien in euismod maximus, purus risus volutpat tellus, a eleifend turpis nisl nec arcu.

<div class="docs-text-note"><strong>Note:</strong> This is an important note that needs to be highlighted!</div>

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer accumsan turpis quis lorem maximus posuere. Aenean maximus tristique tellus, eu vestibulum massa. Pellentesque viverra quam urna, et egestas elit pretium eget. Duis vulputate gravida est, eget condimentum arcu fermentum vitae. Nulla facilisi. Vivamus tempus, neque ut porttitor finibus, ex dui gravida turpis, eu finibus tortor odio sed lectus. Aliquam pretium, sapien in euismod maximus, purus risus volutpat tellus, a eleifend turpis nisl nec arcu.


{% highlight html %}

<div class="docs-text-note"><strong>Note:</strong> This is an important note that needs to be highlighted!</div>

{% endhighlight %}


## code

refer to https://github.com/jneen/rouge/wiki/List-of-supported-languages-and-lexers.

### C\#

{% highlight csharp %}
    var connector = new ConnectorClient();
    List<ChannelAccount> participants = new List<ChannelAccount>();
    ... add channelaccounts to participants....    
    
    Message message = new Message();
    message.From = botChannelAccount;
    message.To = new ChannelAddress() {ChannelId = "slack"};
    message.Participants = participants.ToArray();
    message.Text = "Hey, what's up everyone?";
    message.Language = "en";
    connector.Messages.SendMessage(message);
{% endhighlight %}


### javascript

{% highlight JavaScript %}
var dialog = new builder.CommandDialog();
bot.add('/', dialog);

dialog.matches('^version', function (session) {
    session.send('Bot version 1.2');
});
{% endhighlight %}

### json

{% highlight json %}
    {   
        ...
       "mentions": [{ "Mentioned": { "ChannelId": "slack", "Address":"B1332231" },"Text": "@ColorBot" }]
        ...
    }
{% endhighlight %}


### Console

    > hello
    Cookie:1 User:1 Conversation:1 PerUser:1 You said:hello

## image captions

![View your bot in the dashboard](/en-us/images/directory/1-reviews_no-channels-connected.png)

<div class="imagecaption"><span>This is a caption</span></div>

{% highlight markdown %}
{% raw %}

![View your bot in the dashboard](/en-us/images/directory/1-reviews_no-channels-connected.png)

<div class="imagecaption"><span>This is a caption</span></div>

{% endraw %}
{% endhighlight %}



## TOC: hide sections in TOC

### I'm first

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer accumsan turpis quis lorem maximus posuere. Aenean maximus tristique tellus, eu vestibulum massa. Pellentesque viverra quam urna, et egestas elit pretium eget. Duis vulputate gravida est, eget condimentum arcu fermentum vitae. Nulla facilisi. Vivamus tempus, neque ut porttitor finibus, ex dui gravida turpis, eu finibus tortor odio sed lectus. Aliquam pretium, sapien in euismod maximus, purus risus volutpat tellus, a eleifend turpis nisl nec arcu.

### I'm second. I'm visible in the content, but not in the TOC
{:.no_toc}

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer accumsan turpis quis lorem maximus posuere. Aenean maximus tristique tellus, eu vestibulum massa. Pellentesque viverra quam urna, et egestas elit pretium eget. Duis vulputate gravida est, eget condimentum arcu fermentum vitae. Nulla facilisi. Vivamus tempus, neque ut porttitor finibus, ex dui gravida turpis, eu finibus tortor odio sed lectus. Aliquam pretium, sapien in euismod maximus, purus risus volutpat tellus, a eleifend turpis nisl nec arcu.

### I'm third

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer accumsan turpis quis lorem maximus posuere. Aenean maximus tristique tellus, eu vestibulum massa. Pellentesque viverra quam urna, et egestas elit pretium eget. Duis vulputate gravida est, eget condimentum arcu fermentum vitae. Nulla facilisi. Vivamus tempus, neque ut porttitor finibus, ex dui gravida turpis, eu finibus tortor odio sed lectus. Aliquam pretium, sapien in euismod maximus, purus risus volutpat tellus, a eleifend turpis nisl nec arcu.

### You don't see the second in the top TOC, right? 

Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer accumsan turpis quis lorem maximus posuere. Aenean maximus tristique tellus, eu vestibulum massa. Pellentesque viverra quam urna, et egestas elit pretium eget. Duis vulputate gravida est, eget condimentum arcu fermentum vitae. Nulla facilisi. Vivamus tempus, neque ut porttitor finibus, ex dui gravida turpis, eu finibus tortor odio sed lectus. Aliquam pretium, sapien in euismod maximus, purus risus volutpat tellus, a eleifend turpis nisl nec arcu.

## tabbed code snippets

### Usage/Limitations

* The main div's id has to start with "thetabs"
* Since it works with div ids, all ids have to be different
* Selection of a tab will save the preference in the browser local storage
    * It assumes that all tabs in all pages have the same number of tabs. if not it might break. 
    * It assumes that the tabs will always contain the same type of content (i.e. tab1 = C#, tab2 = node)

### Samples

Code snippet 1.

<div id="thetabs1">
  <ul>
    <li><a href="#tab11">C#</a></li>
    <li><a href="#tab12">Node.js</a></li>
  </ul>

<div id="tab11">

{% highlight csharp %}
    var connector = new ConnectorClient();
    List<ChannelAccount> participants = new List<ChannelAccount>();
    ... add channelaccounts to participants....    
    
    Message message = new Message();
    message.From = botChannelAccount;
    message.To = new ChannelAddress() {ChannelId = "slack"};
    message.Participants = participants.ToArray();
    message.Text = "Hey, what's up everyone?";
    message.Language = "en";
    connector.Messages.SendMessage(message);
{% endhighlight %}

</div>
<div id="tab12">

{% highlight JavaScript %}
var dialog = new builder.CommandDialog();
bot.add('/', dialog);

dialog.matches('^version', function (session) {
    session.send('Bot version 1.2');
});
{% endhighlight %}

</div>  
</div>


Code snippet 2.

<div id="thetabs2">
  <ul>
    <li><a href="#tab21">C#</a></li>
    <li><a href="#tab22">Node.js</a></li>
  </ul>

<div id="tab21">

{% highlight csharp %}
    replyMessage.Attachments.Add(new Attachment()
    {
        ContentUrl = "https://upload.wikimedia.org/wikipedia/en/a/a6/Bender_Rodriguez.png",
        ContentType = "image/png"
    });
{% endhighlight %}

</div>
<div id="tab22">

{% highlight JavaScript %}
bot.add('/', [
    function (session) {
        builder.Prompts.choice(session, "Which region would you like sales for?", salesData); 
    },
    function (session, results) {
        if (results.response) {
            var region = salesData[results.response.entity];
            session.send("We sold %(units)d units for a total of %(total)s.", region); 
        } else {
            session.send("ok");
        }
    }
]);
{% endhighlight %}

</div>  
</div>

The Tabs above have been created with the following html/markdown markup:

{% highlight html %}
{% raw %}
Code snippet 1.

<div id="thetabs1">
    <ul>
        <li><a href="#tab11">C#</a></li>
        <li><a href="#tab12">Node.js</a></li>
    </ul>

    <div id="tab11">
    the C# code snippet
    </div>
    <div id="tab12">
    the Node.js code snippet
    </div>  
</div>

Code snippet 2.

<div id="thetabs2">
    <ul>
        <li><a href="#tab21">C#</a></li>
        <li><a href="#tab22">Node.js</a></li>
    </ul>

    <div id="tab21">
    the C# code snippet
    </div>
    <div id="tab22">
    the Node.js code snippet
    </div>  
</div>
{% endraw %}
{% endhighlight%}

---
layout: page
title: Configuring Conversation Channels
permalink: /en-us/channels/
weight: 4035
parent1: none
---


<span style="color:red">The channel inspector matrix needs to be a child of this topic>></span>

A conversation channel is simply a chat service such as [Skype](http://www.skype.com/) or [Kik](https://www.kik.com/). With the framework, you can write your bot once and it will work with any supported channel. The channel determines the conversational interface and the features that it supports. At a minimum, bots can exchange text strings with the channel, but the framework also lets your bot exchange attachments. For example, the framework supports sending images, videos, and rich cards among others to the channel. If a channel does not support a feature, the connector will try to normalize the message to fit the channel. For example, if you send a card with action buttons to SMS, the connector might normalize the message by including something similar to "Reply with 'Confirm' to... or 'Cancel' to..." in the text.

Some channels provide features that the framework doesn't support. For those channels, the framework lets you add channel-specific data to the message so you can take advantage of the feature. For details about specifying channel-specific data, see [Adding Channel Data](/en-us/core-concepts/channeldata/).

For a complete list of channels that the framework supports, and the framework features that the channel supports (for example, rich cards), see [Channel Inspector](/en-us/channel-inspector). 

To run your bot on most channels, you must provide channel configuration information to the framework. For example, most channels require that your bot have an account on the channel, and others require your bot to also have an application (for example, Facebook, GroupMe, and others).

The following channels are preconfigured for you.

* Skype
* Web Chat

Note that to take full advantage of Skype, you must publish your bot to Bot Directory (see [Publishing your Bot](/en-us/publishing/)).

To configure your bot to work on one or more channels:

1. Go to the [developer portal](https://dev.botframework.com) and click **My bots**. 
2. Select the bot that you want to configure from your list of registered bots.
3. Under **Add another channel** on the bot's dashboard, click **Add** next to the channel that you want your bot to run on.
4. Follow the configuration steps. After you've configured the channel, it will appear under **Channels** in the dashboard. 

Note that you can click **Edit** at any time to change the configuration or to disable the bot on the channel.

After you've configured the channel, users on that channel can start using your bot.




<span style="color:red">Feedback on channel configuration:

Direct Line:

- How would you name your site? Consider something more direct: What's your site's name?
- But what if you use Direct Line in an app?
- What does it mean by "Site name is for your reference"?
- Is site name abc123, mysite, mysite.com, http://mysite.com, ??? Or is it not an actual endpoint? It's just some moniker that you use to identify the app or website that the secret keys are associated with.
- It would be great if we added help to the configuration steps: what do i do with the secret keys?
- How can a bot have multiple direct line sites?
- Is the Connector involves in this at all? The doc just talks about the client calling direct line, so how does direct line call your bot - does the secret link direct line to your bot? 

Email:

- This asks for an email address and password - who's email address and password, the bot's? 
- What does "Submit email credentials" do?

Facebook:

- I assume that the webhooks are in their Facebook app?
- So there is no facebook chat service - the bot interacts with your Facebook app instead?
- And your facebook app defines the conversational experience?

GroupMe:

- What does "Submit GroupMe credentials" do?
- So there is no groupme chat service - the bot interact with your groupme app instead?
- And your groupme app defines the conversational experience?

Teams:

- What's Bing Entity and Intent Detection API? Is this LUIS?

Slack:

- What does "Submit Slack credentials" do?
- So there is no slack chat service - the bot interacts with your slack app instead?
- And your slack app defines the conversational experience?
- I'm confused by "slack application" and "slace bot"

General concern when the configuration steps call for creating a channel-specific bot (i.e., slack, telegram, etc.) because i thought the impression was that you can create a bot and run it on any of the channels.

</span>


{ %comment }

<span style="color:red"><< If we keep links to the channels, it would be great if they were included as part of the channel inspector. >></span>

Supported channels as of December, 2016:

1. Text/SMS
2. [Office 365 mail](http://www.office.com/)
3. [Skype](http://www.skype.com/) (auto-configured)
4. [Slack](http://slack.com/)
5. [GroupMe](http://groupme.com/)
6. [Telegram](http://telegram.org/)
7. [Facebook Messenger](http://www.messenger.com/)
8. [Kik](https://www.kik.com/)
9. Web Chat (preconfigured, embeddable)
10. Direct Line (an API to host your bot in your app or website)
11. Microsoft Teams

{ %endcomment }
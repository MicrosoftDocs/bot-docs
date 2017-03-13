---
layout: page
title: Upgrade your bot to V3
permalink: /en-us/support/upgrade-to-v3/
weight: 9150
parent1: none
---

At Build 2016 Microsoft announced the Microsoft Bot Framework and its initial iteration of the Bot Connector API, along with Bot Builder and Bot Connector SDK's.  Since Build we've been collecting your feedback and actively working to improve the REST API and SDK's to be ready for the future, better support for attachments, and improved performance.

We're now introducing a new iteration of our API (V3).  In this API, there are a number of small changes designed to make the API more adaptable to future requirements.

The good news is our overall model is the same, though the syntax of interacting with the Bot Connector has been updated.  Below are some highlights that are detailed in the Bot Framework documentation.

* TOC
{:toc}

## Initial recommendations

1. Read through this guide completely before starting, so you understand the whole process.
2. If you have current users using your bot, and don't want disruptions, we recommend migrating the code in a separate endpoint, and switch when everything works.

## Step 1. Get your App ID and password from the Developer Portal

Visit the [Bot Framework Developer Portal](https://dev.botframework.com/), look for your bot, visit its dashboard, and click on the Edit button.

Find the Configuration info panel and look at the App ID field. Follow these instructions depending on your case.

### Case 1: There is an App ID already

1. Click on "Manage App ID and password" 
    ![](/en-us/images/migration/manage-app-id.png)
2. Generate a new Password 
    ![](/en-us/images/migration/generate-new-password.png)
3. Copy and save the new password along with the MSA App Id, you will need them later on. 
    ![](/en-us/images/migration/new-password-generated.png)

### Case 2: There is no App ID

1. Click on "Generate App ID and password".
    ![](/en-us/images/migration/generate-appid-and-password.png)
    <div class="docs-text-note"><strong>Note:</strong> Do not check the Version 3.0 radio button until you have migrated your bot code.</div>
2. Click on "Generate a password to continue".
    ![](/en-us/images/migration/generate-a-password-to-continue.png)
3. Copy and save the new password along with the MSA App Id, you will need them later on. 
    ![](/en-us/images/migration/new-password-generated.png)
4. Click on "Finish and go back to the Bot Framework portal".
    ![](/en-us/images/migration/finish-and-go-back-to-bot-framework.png)
5. Once back in the Bot Framework portal, scroll all the way down and save your changes.
    ![](/en-us/images/migration/save-changes.png)

## Step 2. Update your bot code to Version 3.0

The changes below will require at least some code change to your bot.  to not disrupt your existing users, we suggest the following:

1. Create a new branch of your Bot's source code
2. Update to the new SDK for your bot's language
3. Make appropriate syntax changes
4. Test with the Bot Framework Channel Emulator on your desktop and then in the cloud
5. [Upgrade your bot registration in the Bot Framework Developer Portal](#step-3-update-your-bot-registration-in-the-bot-framework-developer-portal)

The goal of these steps are to ensure continued support for your current users.  If that's not an issue, you can just update your current deployment in place.


### BotBuilder and Connector are now one SDK

Instead of downloading separate SDKs for the builder and connector in separate NuGet (or NPM packages), both are included in the BotBuilder package.  On NuGet - Microsoft.Bot.Builder, and on NPM - botbuilder.  The standalone Microsoft.Bot.Connector SDK will not be updated going forward.

### Message is now Activity

The Message object has been replaced with the Activity object; which currently returns a number of types of activities, including but not limited to messages.  Learn more about working with [Activity](/en-us/csharp/builder/sdkreference/connector.html#replying) objects.

### Activity Types & Events

Some of the events have been renamed/refactored.  In addition, a new ActivityTypes enumeration has been added to the Connector to take away the need to remember specific message types.

- **conversationUpdated** - replaces Bot/User Added/Removed To/From Conversation with a single method
- New: **Typing** - lets your bot indicate whether the user or bot is typing
- New: **contactRelationUpdated** - lets your Bot know if a bot has been added or removed as a contact for a user

For conversationUpdated, the MembersRemoved and MembersAdded lists will now tell you who was added or removed.  For contactRelation, the new Action property will tell you whether the user was adding or removing the bot from their contact list.  Read here for more on and [ActivityTypes](/en-us/csharp/builder/sdkreference/connector.html#messagetypes).

### Addressing

Addressing Activity objects has been changed slightly (see table below).  You can learn more here: [Addresses in Activities](/en-us/csharp/builder/sdkreference/connector.html#addresses)

|V1 Field|	V3 Field|
|--------|--------|
|From Object|From Object|
|To Object|	Recipient Object|
|ChannelConversationID|	Conversation Object|
|ChannelId| ChannelId|

### Sending Replies

In Bot Framework API V3, all replies to the user will be sent asynchronously over a separately initiated HTTP request rather than inline with the HTTP POST for the incoming message to the bot.  Since no message will be returned inline to the user through the Connector, the return type of your bot's post method will be HttpResponseMessage.  This means that your bot doesn't synchronously "return" the string that you wish to send to the user, but instead sends a reply message at any point in your code instead of having to reply back as a response to the incoming POST:

- SendToConversation*
- ReplyToConversation*

The difference between the two is that for conversations that support it, ReplyToConversation will attempt to thread the conversation such as in e-mail.

### Bot Data Storage (Bot State)

In BotFramework V1 API, the Bot data APIs were folded into the Messaging API, which was somewhat confusing.  Instead, the Bot data APIs have been separated out into their own API, called BotState.  Read more on [Bot State](/en-us/csharp/builder/sdkreference/connector.html#trackingstate) here.

Action:

- Call to BotState API to get your state instead of assuming it will be on the message object
- Call to BotState API to store your state instead of passing it as part of the message object

### Creating New Conversations

Determining how to create new conversation with the user, and understanding how that user would be addressed in the V1 API was confusing.  The V3 API has made this more clear by adding the CreateDirectConversation and CreateConversation methods for direct messages (DMs) and open discussions respectively.  For example, to create a DM:

{% highlight csharp %}

var conversation = 
    await connector.Conversations.CreateDirectConversationAsync(
        incomingMessage.Recipient, incomingMessage.From);
addedMessage.Conversation = new conversationAccount(id: conversation.Id);
var reply = await connector.Conversations.SendToConversationAsync(addedMessage);

{% endhighlight %}

Read more about [Creating Conversations](/en-us/csharp/builder/sdkreference/connector.html#conversation) here.

### Attachments and Options

The V1 API had support for attachments and options, however the design was somewhat incomplete.  The V3 API has a cleaner implementation of attachments and cards.  The Options type itself has been removed and replaced with cards.  The full topic on [Attachments](/en-us/csharp/builder/sdkreference/connector.html#attachmentscardsactions) is here.

### Updates to the Auth properties in Web.Config

In V1, the authentication properties were stored with these keys:

- AppID
- AppSecret

In V3, to reflect changes to the underlying auth model, these keys have been changed to:

- MicrosoftAppID
- MicrosoftAppPassword


## Step 3. Update your bot registration in the Bot Framework Developer Portal

Once you have deployed your new V3 bot, you're ready to test. 

1. Visit the [Bot Framework Developer Portal](https://dev.botframework.com/), look for your bot, visit its dashboard, and click on the Edit button. Find the Configuration section.
2. Paste your new endpoint in the "Messaging endpoint" field of the "Version 3.0" section.
    ![](/en-us/images/migration/paste-new-v3-enpoint-url.png)
3. Click the Version 3.0 endpoint radio button, and save your changes.
    ![](/en-us/images/migration/switch-to-v3-endpoint.png)
    <div class="docs-text-note"><strong>Note:</strong> When you do this, your bot will switch to the new endpoint. If anything goes wrong, you can revert it back to Version 1.0, and iterate until Version 3.0 works properly.</div>
    <div class="docs-text-note"><strong>Important!</strong>  if you revert back to Version 1.0, and have an active Skype bot, <a href="mailto:botframework@microsoft.com?subject=Version 1.0 downgrade with an active Skype bot">contact us</a> to have your Skype bot reset.</div>
4. Once Version 3.0 works as expected, you're done.



---
title: Channels reference
description: Bot Framework Channels reference
keywords: channels reference, bot builder channels, bot framework channels
author: ivorb
ms.author: v-ivorb
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: tools
ms.date: 03/01/2019
---

# Categorized activities by channel

The following tables show what events (Activities on the wire) can come from which channels.

This is the key for the tables:

Symbol              | Meaning
:------------------:|:------------------------------------------------
:white_check_mark:  |The Bot should expect to receive this Activity
:x:                 |The Bot should **never** expect to receive this Activity
:white_large_square:|Currently undetermined whether the Bot can receive this

Activities can meaningfully be split into separate categories. For each category we have a table of possible Activities.

Conversational
--------------

 \                      | Cortana            | Direct Line        | Direct Line (Web Chat) | Email              | Facebook           | GroupMe            | Kik                | Teams              | Slack              | Skype   | Skype Business | Telegram | Twilio  
:---------------------- | :-----:            | :----------------: | :--------------------: |:----:              | :------:           | :-----:            | :-----:            | :---:              | :---:              | :---:   | :------------: | :------: | :----:  
Message                 | :white_check_mark: | :white_check_mark: | :white_check_mark:     | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark:        | :white_check_mark:  | :white_check_mark: 
MessageReaction         | :x:                | :x:                | :x:                    | :x:                | :x:                | :x:                | :x:                | :white_check_mark: | :x:                | :x:      | :x:             | :x:       | :x:      

- All Channels send Message Activities.
- When using a Dialog, Message Activities should generally always be passed onto the Dialog.
- This is probably not true of the MessageReaction although they are very much part of the conversation.
- There are logically two types of MessageReaction: Added and Removed


> [!TIP]
> "Message Reactions" are things like a "thumbs up" on a previous comment. They can happen out of order, so they can be thought of as similar to buttons. This Activity is currently sent by the Teams Channel.  


Welcome
-------

 \                         | Cortana            | Direct Line        | Direct Line (Web Chat) | Email   | Facebook             | GroupMe | Kik     | Teams   | Slack   | Skype   | Skype Business | Telegram | Twilio  
:----------------------    | :-----:            | :---------:        | :--------------------: |:----:   | :------:             | :-----: | :-----: | :---:   | :---:   | :---:   | :------------: | :------: | :----:  
ConversationUpdate         | :white_check_mark: | :white_check_mark: | :white_check_mark:     | :x:     | :white_large_square: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :x:      | :x:             | :white_check_mark:  | :x:     
ContactRelationUpdate      | :x:                | :x:                | :x:                    | :x:     | :x:                  | :x:      | :x:      | :x:      | :x:      | :white_check_mark: | :white_check_mark:        | :x:       | :x:     

- It is common for Channels to send ConversationUpdate Activities.
- There are logically two types of MessageReaction: Added and Removed
- It is very tempting to assume bot "Welcome" behavior can be simply implemented by wiring up ConversationUpdate.Added and this sometimes works.
- However, this is a simplification, in order to produce a reliable "Welcome" behavior the bot implementation may also need to use state.


Application Extensibility
-------------------------

 \                      | Cortana            | Direct Line        | Direct Line (Web Chat) | Email   | Facebook | GroupMe | Kik     | Teams   | Slack   | Skype   | Skype Business | Telegram | Twilio  
:---------------------- | :-----:            | :---------:        | :--------------------: |:----:   | :------: | :-----: | :-----: | :---:   | :---:   | :---:   | :------------: | :------: | :----:  
Event.*                    | :white_large_square: | :white_check_mark: | :white_check_mark:    | :white_large_square: | :white_large_square:  | :white_large_square: | :white_large_square: | :white_large_square:  | :white_large_square:  | :white_large_square:  | :white_large_square:           | :white_large_square:     | :white_large_square:  
Event.CreateConversation   | :white_large_square: | :white_large_square: | :white_large_square:   | :white_large_square: | :white_large_square:  | :white_large_square: | :white_large_square: | :white_large_square:  | :white_large_square:  | :white_large_square:  | :white_large_square:           | :white_large_square:     | :white_large_square:  
Event.ContinueConversation | :white_large_square: | :white_large_square: | :white_large_square:   | :white_large_square: | :white_large_square:  | :white_large_square: | :white_large_square: | :white_large_square:  | :white_large_square:  | :white_large_square:  | :white_large_square:           | :white_large_square:     | :white_large_square:  

- Event Activities are an extensibility mechanism in Direct Line (_aka Web Chat_).
- An application that owns both the client and server may chose to tunnel their own events through the service using this Event Activity.


Microsoft Teams
------------------

 \                      | Cortana            | Direct Line        | Direct Line (Web Chat) | Email   | Facebook | GroupMe | Kik     | Teams   | Slack   | Skype   | Skype Business | Telegram | Twilio  
:---------------------- | :-----:            | :---------:        | :--------------------: |:----:   | :------: | :-----: | :-----: | :---:   | :---:   | :---:   | :------------: | :------: | :----:  
Invoke.TeamsVerification   | :x:      | :x:          | :x: | :x:   | :x:       | :x:      | :x:      | :white_check_mark: | :x:    | :x:    | :x:             | :x:       | :x:     
Invoke.ComposeResponse     | :x:      | :x:          | :x: | :x:   | :x:       | :x:      | :x:      | :white_check_mark: | :x:    | :x:    | :x:             | :x:       | :x:     

- Along with a number of the other typed Activities, Microsoft Teams defines a few Teams specific Invoke Activities.
- Invoke Activities are specific to an application and not something a client would define.
- There is no general notion of Invoke specific subtypes of the activity.
- Invoke is currently the only Activity that triggers a request-reply behavior on the bot.

This is very important: if using Dialogs for the OAuth Prompt to work the Invoke.TeamsVerification Activity must be forwarded to the Dialog.


Message Update
--------------

 \                      | Cortana            | Direct Line        | Direct Line (Web Chat) | Email   | Facebook | GroupMe | Kik     | Teams   | Slack   | Skype   | Skype Business | Telegram | Twilio  
:---------------------- | :-----:            | :---------:        | :--------------------: |:----:   | :------: | :-----: | :-----: | :---:   | :---:   | :---:   | :------------: | :------: | :----:  
MessageUpdate | :x:      | :x:          | :x:    | :x: | :x:      | :x:      | :x:  | :white_check_mark: | :white_large_square:  | :x:    | :x:             | :x:       | :x:     
MessageDelete | :x:      | :x:          | :x:    | :x: | :x:      | :x:      | :x:  | :white_check_mark: | :white_large_square:  | :x:    | :x:             | :x:       | :x:     

- Message Update is currently supported by Teams.


OAuth
-------

 \                      | Cortana            | Direct Line        | Direct Line (Web Chat) | Email   | Facebook | GroupMe | Kik     | Teams   | Slack   | Skype   | Skype Business | Telegram | Twilio  
:---------------------- | :-----:            | :---------:        | :--------------------: |:----:   | :------: | :-----: | :-----: | :---:   | :---:   | :---:   | :------------: | :------: | :----:  
Event.TokenResponse| :white_large_square:  | :white_check_mark:   | :white_check_mark:    | :x:    | :white_large_square: | :white_large_square: | :white_large_square: | :x:    | :white_large_square: | :white_large_square: | :white_large_square:       | :white_large_square: | :white_large_square: 

This is very important: if using Dialogs for the OAuth Prompt to work the Event.TokenResponse Activity must be forwarded to the Dialog.


Uncategorized 
-------------

 \                      | Cortana  | Direct Line        | Direct Line (Web Chat) | Email | Facebook | GroupMe | Kik     | Teams | Slack | Skype | Skype Business | Telegram | Twilio  
:---------------------- | :-----:  | :---------:        | :--------------------: |:----: | :------: | :-----: | :-----: | :---: | :---: | :---: | :------------: | :------: | :----:  
EndOfConversation       | :x:      | :white_check_mark: | :white_check_mark:     | :x:   | :x:      | :x:     | :x:     | :x:   | :x:   | :x:   | :x:            | :x:      | :x:     
InstallationUpdate      | :x:      | :white_check_mark: | :white_check_mark:     | :x:   | :x:      | :x:     | :x:     | :x:   | :x:   | :x:   | :x:            | :x:      | :x:     
Typing                  | :x:      | :white_check_mark: | :white_check_mark:     | :x:   | :x:      | :x:     | :x:     | :x:   | :x:   | :x:   | :x:            | :x:      | :x:     
Handoff                 | :x:      | :x:                | :x:                    | :x:   | :x:      | :x:     | :x:     | :x:   | :x:   | :x:   | :x:            | :x:      | :x:     


Out of Use (includes Payment specific Invoke)
---------------------------------------------
- DeleteUserData 
- Invoke.PaymentRequest  
- Invoke.Address
- Ping

---

## Summary of Activities supported per Channel

Cortana
-------
- Message
- ConversationUpdate
- _Event.TokenResponse_
- _EndOfConversation (when the window closes?)_

Direct Line
--------
- Message
- ConversationUpdate
- Event.TokenResponse
- Event.*
- _Event.CreateConversation_
- _Event.ContinueConversation_

Email
-----
- Message

Facebook
--------
- Message
- _Event.TokenResponse_

GroupMe
-------
- Message
- ConversationUpdate
- _Event.TokenResponse_

Kik
---
- Message
- ConversationUpdate
- _Event.TokenResponse_

Teams
-----
- Message
- ConversationUpdate
- MessageReaction
- MessageUpdate
- MessageDelete
- Invoke.TeamsVerification
- Invoke.ComposeResponse


Slack
-----
- Message
- ConversationUpdate
- _Event.TokenResponse_


Skype
-----
- Message
- ContactRelationUpdate
- _Event.TokenResponse_


Skype Business
--------------
- Message
- ContactRelationUpdate 
- _Event.TokenResponse_


Telegram
--------
- Message
- ConversationUpdate
- _Event.TokenResponse_


Twilio
------
- Message

## Summary Table All Activities to All Channels

 \                         | Cortana              | Direct Line          | Direct Line (Web Chat) | Email                | Facebook             | GroupMe | Kik     | Teams   | Slack   | Skype   | Skype Business | Telegram | Twilio  
:----------------------    | :-----:              | :---------:          | :--------------------: |:----:                | :------:             | :-----: | :-----: | :---:   | :---:   | :---:   | :------------: | :------: | :----:  
Message                    | :white_check_mark:   | :white_check_mark:   | :white_check_mark:     | :white_check_mark:   | :white_check_mark:   | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark:        | :white_check_mark:  | :white_check_mark: 
MessageReaction            | :x:                  | :x:                  | :x:                    | :x:                  | :x:                  | :x:      | :x:      | :white_check_mark: | :x:      | :x:      | :x:             | :x:       | :x:      
ConversationUpdate         | :white_check_mark:   | :white_check_mark:   | :white_check_mark:     | :x:                  | :white_large_square: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :white_check_mark: | :x:      | :x:             | :white_check_mark:  | :x:     
ContactRelationUpdate      | :x:                  | :x:                  | :x:                    | :x:                  | :x:                  | :x:      | :x:      | :x:      | :x:      | :white_check_mark: | :white_check_mark:        | :x:       | :x:     
Event.*                    | :white_large_square: | :white_check_mark:   | :white_check_mark:     | :white_large_square: | :white_large_square: | :white_large_square: | :white_large_square: | :white_large_square:  | :white_large_square:  | :white_large_square:  | :white_large_square:           | :white_large_square:     | :white_large_square:  
Event.CreateConversation   | :white_large_square: | :white_large_square: | :white_large_square:   | :white_large_square: | :white_large_square: | :white_large_square: | :white_large_square: | :white_large_square:  | :white_large_square:  | :white_large_square:  | :white_large_square:           | :white_large_square:     | :white_large_square:  
Event.ContinueConversation | :white_large_square: | :white_large_square: | :white_large_square:   | :white_large_square: | :white_large_square: | :white_large_square: | :white_large_square: | :white_large_square:  | :white_large_square:  | :white_large_square:  | :white_large_square:           | :white_large_square:     | :white_large_square:  
Invoke.TeamsVerification   | :x:                  | :x:                  | :x:                    | :x:                  | :x:                  | :x:      | :x:      | :white_check_mark: | :x:    | :x:    | :x:             | :x:       | :x:     
Invoke.ComposeResponse     | :x:                  | :x:                  | :x:                    | :x:                  | :x:                  | :x:      | :x:      | :white_check_mark: | :x:    | :x:    | :x:             | :x:       | :x:     
MessageUpdate              | :x:                  | :x:                  | :x:                    | :x:                  | :x:                  | :x:      | :x:  | :white_check_mark: | :white_large_square:  | :x:    | :x:             | :x:       | :x:     
MessageDelete              | :x:                  | :x:                  | :x:                    | :x:                  | :x:                  | :x:      | :x:  | :white_check_mark: | :white_large_square:  | :x:    | :x:             | :x:       | :x:     
Event.TokenResponse        | :white_large_square: | :white_check_mark:   | :white_check_mark:     | :x:                  | :white_large_square: | :white_large_square: | :white_large_square: | :x:    | :white_large_square: | :white_large_square: | :white_large_square:       | :white_large_square: | :white_large_square: 
EndOfConversation          | :x:                  | :white_check_mark:   | :white_check_mark:     | :x:                  | :x:      | :x:     | :x:     | :x:   | :x:   | :x:   | :x:            | :x:      | :x:     
InstallationUpdate         | :x:                  | :white_check_mark:   | :white_check_mark:     | :x:                  | :x:      | :x:     | :x:     | :x:   | :x:   | :x:   | :x:            | :x:      | :x:     
Typing                     | :x:                  | :white_check_mark:   | :white_check_mark:     | :x:                  | :x:      | :x:     | :x:     | :x:   | :x:   | :x:   | :x:            | :x:      | :x:     
Handoff                    | :x:                  | :x:                  | :x:                    | :x:                  | :x:      | :x:     | :x:     | :x:   | :x:   | :x:   | :x:            | :x:      | :x:     

## Web Chat 
Web Chat will send:
- "message": with "text" and/or "attachments"
- "event": with "name" and "value" (as JSON/string)
- "typing": if the user set an option, namely "sendTypingIndicator"
Web Chat will not send "contactRelationUpdate". And Web Chat do not support "messageReaction", no one explicitly ask us to support this feature.

By default, Web Chat will render:
- "message": will render as either carousel or stacked, depends on the option in the activity
- "typing": will render for 5s and hide it, or until next activity come in
- "conversationUpdate": will hide
- "event": will hide
- Others: will show a warning box (we never see it in production)
You can modify this render pipeline to add, remove, or replace any custom render.

You can use Web Chat to send any activity type and payload, we neither document nor recommend this feature. 
You should use "event" activity instead.

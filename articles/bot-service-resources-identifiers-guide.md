---
title: Guide to IDs in the Bot Framework | Microsoft Docs
description: This guide describes the characteristics of ID fields present in the Bot Framework v3 protocol.
keywords: id, bots, protocol
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 04/30/2019

---

# ID fields in the Bot Framework

This guide describes the characteristics of ID fields in the Bot Framework.

## Channel ID

Every Bot Framework channel is identified by a unique ID.

Example: `"channelId": "skype"`

Channel IDs serve as namespaces for other IDs. Runtime calls in the Bot Framework protocol must take place
within the context of a channel; the channel gives meaning to the conversation and account IDs used when
communicating.

By convention all channel IDs are lowercase. Channels guarantee that the channel IDs they emit have consistent
casing, and thus bots may use ordinal comparisons to establish equivalence.

### Rules for channel IDs

- Channel IDs are case-sensitive.

## Bot Handle

Every bot that has been registered with the Azure Bot Service has a bot handle.

Example: `FooBot`

A bot handle represents a bot's registration with the online Azure Bot Service. This registration is associated
with an HTTP webhook endpoint and registrations with channels.

The Azure Bot Service ensures uniqueness of bot handles. The Azure portal performs a case-insensitive
uniqueness check (meaning that case variations of bot handle are treated as a single handle) although this is
a characteristic of the Azure portal, and not necessarily the bot handle itself.

### Rules for bot handles

* Bot handles are unique (case-insensitive) within the Bot Framework.

## App ID

Every bot that has been registered with the Azure Bot Service has an App ID.

> [!NOTE]
> Previously, apps were commonly referred to as "MSA Apps" or "MSA/AAD Apps." Apps are now more commonly referred to simply as "apps", but some protocol elements may refer to apps as "MSA Apps" in perpetuity.

Example: `"msaAppId": "353826a6-4557-45f8-8d88-6aa0526b8f77"`

An app represents a registration with the Identity team's App portal, and serves as the service-to-service
identity mechanism within the Bot Framework runtime protocol. Apps may have other non-bot associations, such
as websites and mobile/desktop applications.

Every registered bot has exactly one app. Although it's not possible for a bot owner to independently change which
app is associated with their bot, the Bot Framework team can do so in a small number of exceptional cases.

Bots and channels may use app IDs to uniquely identify a registered bot.

App IDs are guaranteed to be GUIDs. App IDs should be compared without case sensitivity.

### Rules for app IDs

* App IDs are unique (GUID comparison) within the Microsoft App platform.
* Every bot has exactly one corresponding app.
* Changing which app a bot is associated with requires the assistance of the Bot Framework team.

## Channel Account

Every bot and user has an account within each channel. The account contains an identifier (`id`) and other
informative bot non-structural data, like an optional name.

Example: `"from": { "id": "john.doe@contoso.com", "name": "John Doe" }`

This account describes the address within the channel where messages may be sent and received. In some
cases, these registrations exist within a single service (e.g., Skype, Facebook). In others, they are registered
across many systems (email addresses, phone numbers). In more anonymous channels (e.g., Web Chat), the registration
may be ephemeral.

Channel accounts are nested within channels. A Facebook account, for example, is simply a number. This
number may have a different meaning in other channels, and it doesn't have meaning outside all channels.

The relationship between channel accounts and users (actual people) depends on conventions associated with
each channel. For example, an SMS number typically refers to one person for a period of time, after which
the number may be transferred to someone else. Conversely, a Facebook account typically refers to one person
in perpetuity, although it is not uncommon for two people to share a Facebook account.

In most channels, it's appropriate to think of a channel account as a kind of mailbox where messages can be
delivered. It's typical for most channels to allow multiple address to map to a single mailbox; for example,
"jdoe@contoso.com" and "john.doe@service.contoso.com" may resolve to the same inbox. Some channels go
a step further and alter the account's address based on which bot is accessing it; for example, both Skype
and Facebook alter user IDs so every bot has a different address for sending and receiving messages.

While it's possible in some cases to establish equivalency between addresses, establishing equivalency
between mailboxes and equivalency between people requires knowledge of the conventions within the channel,
and is in many cases not possible.

A bot is informed of its channel account address via the `recipient` field on activities sent to the bot.

### Rules for channel accounts

* Channel accounts have meaning only within their associated channel.
* More than one ID may resolve to the same account.
* Ordinal comparison may be used to establish that two IDs are the same.
* There is generally no comparison that can be used to identify whether two different IDs resolve
  to the same account, bot or person.
* The stability of associations between IDs, accounts, mailboxes, and people depends on the channel.

## Conversation ID

Messages are sent and received in the context of a conversation, which is identifiable by ID.

Example: `"conversation": { "id": "1234" }`

A conversation contains an exchange of messages and other activities. Every conversation has zero or more
activities, and every activity appears in exactly one conversation. Conversations may be perpetual, or may
have distinct starts and ends. The process of creating, modifying, or ending a conversation occurs within
the channel (i.e., a conversation exists when the channel is aware of it) and the characteristics of these
processes are established by the channel.

The activities within a conversation are sent by users and bots. The definition for which users "participate"
in a conversation varies by channel, and can theoretically include present users, users who have ever
received a message, users who sent a message.

Several channels (e.g., SMS, Skype, and possibly others) have the quirk that the conversation ID assigned to a 1:1
conversation is the remote channel account ID. This quirk has two side-effects:
1. The conversation ID is subjective based on who is viewing it. If Participants A and B are talking,
   participant A sees the conversation ID to be "B" and participant B sees the conversation ID to be "A."
2. If the bot has multiple channel accounts within this channel (for example, if the bot has two SMS numbers),
   the conversation ID is not sufficient to uniquely identify the conversation within the bot's field of view.

Thus, a conversation ID does not necessarily uniquely identify a single conversation within a channel even
for a single bot.

### Rules for conversation IDs

* Conversations have meaning only within their associated channel.
* More than one ID may resolve to the same conversation.
* Ordinal equality does not necessarily establish that two conversation IDs are the same conversation, although
  in most cases, it does.

## Activity ID

Activities are sent and received within the Bot Framework protocol, and these are sometimes identifiable.

Example: `"id": "5678"`

Activity IDs are optional and employed by channels to give the bot a way to reference the ID in subsequent
API calls, if they are available:
* Replying to a particular activity
* Querying for the list of participants at the activity level

Because no further use cases have been established, there are no additional rules for the treatment of activity
IDs.

---
title: Bot Framework Spec | Microsoft Docs
description: Bot Framework Spec
author: RobStand
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 03/07/2018
---

# Bot Framework -- Activity

## Abstract

The Bot Framework Activity schema is an application-level representation of conversational actions made by humans and automated software. The schema includes provisions for communicating text, multimedia, and non-content actions like social interactions and typing indicators.

This schema is used within the Bot Framework protocol and is implemented by Microsoft chat systems and by interoperable bots and clients from many sources.

## Table of Contents

1. [Introduction](#introduction)
2. [Basic activity structure](#basic-activity-structure)
3. [Message activity](#message-activity)
4. [Contact relation update activity](#contact-relation-update-activity)
5. [Conversation update activity](#conversation-update-activity)
6. [End of conversation activity](#end-of-conversation-activity)
7. [Event activity](#event-activity)
8. [Invoke activity](#invoke-activity)
9. [Installation update activity](#installation-update-activity)
10. [Message delete activity](#message-delete-activity)
11. [Message update activity](#message-update-activity)
12. [Message reaction activity](#message-reaction-activity)
13. [Typing activity](#typing-activity)
14. [Complex types](#complex-types)
15. [References](#references)
16. [Appendix I - Changes](#appendix-i---changes)
17. [Appendix II - Non-IRI entity types](#appendix-ii---non-iri-entity-types)
18. [Appendix III - Protocols using the Invoke activity](#appendix-iii---protocols-using-the-invoke-activity)

## Introduction

### Overview

The Bot Framework Activity schema represents conversational behaviors made by humans and automated software within chat applications, email, and other text interaction programs. Each activity object includes a type field and represents a single action: most commonly, sending text content, but also including multimedia attachments and non-content behaviors like a "like" button or a typing indicator.

This document provides meanings for each type of activity, and describes the required and optional fields that may be included. It also defines the roles of the client and server, and provides guidance on which fields are mastered by each participant, and which may be ignored.

There are three roles of consequence in this specification: clients, which send and receive activities on behalf of users; bots, which send and receive activities and are typically automated; and the channel, which stores and forwards activities between clients and bots.

Although this specification requires activities to be transmitted between roles, the exact nature of that transmission is not described here.

For compactness, visual interactive cards are not defined in this specification. Instead, refer to [Adaptive Cards](https://adaptivecards.io) [[11](#references)] specification. This card, and other undefined card types, may be included as attachments within Bot Framework activities.

### Requirements

The key words "MUST", "MUST NOT", "REQUIRED", "SHALL", "SHALL NOT", "SHOULD", "SHOULD NOT", "RECOMMENDED", "MAY", and "OPTIONAL" in this document are to be interpreted as described in [RFC 2119](https://tools.ietf.org/html/rfc2119) [[1](#references)].

An implementation is not compliant if it fails to satisfy one or more of the MUST or REQUIRED level requirements for the protocols it implements. An implementation that satisfies all the MUST or REQUIRED level and all the SHOULD level requirements for its protocols is said to be "unconditionally compliant"; one that satisfies all the MUST level requirements but not all the SHOULD level requirements for its protocols is said to be "conditionally compliant."

### Numbered requirements

Lines beginning with markers of the form `RXXXX` are specific requirements designed to be referenced by number in discussion outside of this document. They do not carry any more or less weight than normative statements made outside of `RXXXX` lines.

`R1000`: Editors of this specification MAY add new `RXXXX` requirements. They SHOULD find numeric `RXXXX` values that preserve the document's flow.

`R1001`: Editors MUST NOT renumber existing `RXXXX` requirements.

`R1002`: Editors MAY delete or revise `RXXXX` requirements. If revised, editors SHOULD retain the existing `RXXXX` value if the topic of the requirement remains largely intact.

`R1003`: Editors SHOULD NOT reuse retired `RXXXX` values. A list of deleted values MAY be maintained at the end of this document.

### Terminology

activity
> An action expressed by a bot, a channel, or a client that conforms to the Activity schema.

channel
> Software that sends and receives activities, and transforms them to and from chat or application behaviors. Channels are the authoritative store for activity data.

bot
> Software that sends and receives activities, and generates automated, semi-automated, or entirely manual responses. Bots have endpoints that are registered with channels.

client
> Software that sends and receives activities, typically on behalf of human users. Clients do not have endpoints.

sender
> Software transmitting an activity.

receiver
> Software accepting an activity.

endpoint
> A programmatically addressable location where a bot or channel can receive activities.

address
> An identifier or address where a user or bot can be contacted.

field
> A named value within an activity or nested object.

### Overall organization

The activity object is a flat list of name/value pairs, some of which are primitive objects, and some of which are complex (nested). The activity object is commonly expressed in the JSON format, but can also be projected into in-memory data structures in .Net or JavaScript.

The activity `type` field controls the meaning of the activity and the fields contained within it. Depending on the role that a participant is playing (client, bot, or channel), each field is mandatory, optional, or ignored. For example, the `id` field is mastered by the channel, and is mandatory in some circumstances, but ignored if it is sent by a bot.

Fields that describe the identity of the activity and any participants, such as the `type` and `from` fields, are shared across all activities. In many programming languages, it is convenient to organize these fields on a core base type from which other, more specific, activity types derive.

When storing or transmitting activities, some fields may be duplicated within the transport mechanism. For example, if an activity is transmitted via HTTP POST to a URL that includes the conversation ID, the receiver may infer its value without requiring it to be present within the activity body. This document merely describes the abstract requirements for these fields, and it is up to the controlling protocol to establish whether the values must be explicitly declared or if implicit or inferred values are allowed.

When a bot or client sends an activity to a channel, it is typically a request for the activity to be recorded. When a channel sends an activity to a bot or client, it is typically a notification that the activity has already been recorded.

## Basic activity structure

This section defines the requirements for the basic structure of the activity object.

Activity objects include a flat list of name/value pairs, called fields. Fields may be primitive types. JSON is used as the common interchange format and although not all activities must be serialized to JSON at all times, they must be serializable to it. This allows implementations to rely on a simple set of conventions for handling known and unknown activity fields.

`R2001`: Activities MUST be serializable to the JSON format, including adherence to e.g. field uniqueness constraints.

`R2002`: Receivers MAY allow improperly-cased field names, although this is not required. Receivers MAY reject activities that do not include fields with the proper casing.

`R2003`: Receivers MAY reject activities that contain field values whose types do not match the value types described in this specification.

`R2004`: Unless otherwise noted, senders SHOULD NOT include empty string values for string fields.

`R2005`: Unless otherwise noted, senders MAY include additional fields within the activity or any nested complex objects. Receivers MUST accept fields they do not understand.

`R2006`: Receivers SHOULD accept events of types they do not understand.

### Type

The `type` field controls the meaning of each activity, and are by convention short strings (e.g. "`message`"). Senders may define their own application-layer types, although they are encouraged to choose values that are unlikely to collide with future well-defined values. If senders use URIs as type values, they SHOULD NOT implement URI ladder comparisons to establish equivalence.

`R2010`: Activities MUST include a `type` field, with string value type.

`R2011`: Two `type` values are equivalent only if they are ordinally identical.

`R2012`: A sender MAY generate activity `type` values not defined in this document.

`R2013`: A channel SHOULD reject activities of type it does not understand.

`R2014`: A bot or client SHOULD ignore activities of type it does not understand.

### Channel ID

The `channelId` field establishes the channel and authoritative store for the activity. The value of the `channelId` field is of type string.

`R2020`: Activities MUST include a `channelId` field, with string value type.

`R2021`: Two `channelId` values are equivalent only if they are ordinally identical.

`R2022`: A channel MAY ignore or reject any activity it receives without an expected `channelId` value.

### ID

The `id` field establishes the identity for the activity once it has been recorded in the channel. Activities in-flight that have not yet been recorded do not have identities. Not all activities are assigned identities (for example, a [typing activity](#typing-activity) may never be assigned an `id`.) The value of the `id` field is of type string.

`R2030`: Channels SHOULD include an `id` field if it is available for that activity.

`R2031`: Clients and bots SHOULD NOT include an `id` field in activities they generate.

For ease of implementation, it should be assumed that other participants do not have sophisticated knowledge of activity IDs, and that they will use only ordinal comparison to establish equivalency.

For example, a channel may use hex-encoded GUIDs for each activity ID. Even though GUIDs encoded in uppercase are logically equivalent to GUIDs encoded in lowercase, senders SHOULD NOT use these alternative encodings when possible. The normalized version of each ID is established by the authoritative store, the channel.

`R2032`: When generating `id` values, senders SHOULD choose values whose equivalency can be established by ordinal comparison. However, senders and receivers MAY allow logical equivalence of two values that are not ordinally equivalent if they have special knowledge of the circumstances.

The `id` field is designed to allow de-duplication, but this is prohibitive in most applications.

`R2033`: Receivers MAY de-duplicate activities by ID, however senders SHOULD NOT rely on receivers performing this de-duplication.

### Timestamp

The `timestamp` field records the exact UTC time when the activity occurred. Due to the distributed nature of computing systems, the important time is when the channel (the authoritative store) records the activity. The time when a client or bot initiated an activity may be transmitted separately in the `localTimestamp` field. The value of the `timestamp` field is an [ISO 8601](https://www.iso.org/iso-8601-date-and-time-format.html) [[2](#references)] encoded datetime within a string.

`R2040`: Channels SHOULD include a `timestamp` field if it is available for that activity.

`R2041`: Clients and bots SHOULD NOT include a `timestamp` field in activities they generate.

`R2042`: Clients and bots SHOULD NOT use `timestamp` to reject activities, as they may appear out-of-order. However, they MAY use `timestamp` to order activities within a UI or for downstream processing.

`R2043`: Senders SHOULD always use encode the value of `timestamp` fields as UTC, and they SHOULD always include Z as an explicit UTC mark within the value.

### Local timestamp

The `localTimestamp` field expresses the datetime and timezone offset where the activity was generated. This may be different from the UTC `timestamp` where the activity was recorded. The value of the `localTimestamp` field is an ISO 8601 [[3](#references)] encoded datetime within a string.

`R2050`: Clients and bots MAY include the `localTimestamp` field in their activities. They SHOULD explicitly list the timezone offset within the encoded value.

`R2051`: Channels SHOULD preserve `localTimestamp` when forwarding activities from a sender to recipient(s).

### From

The `from` field describes which client, bot, or channel generated an activity. The value of the `from` field is a complex object of the [Channel account](#channel-account) type.

The `from.id` field identifies who generated an activity. Most commonly, this is another user or bot within the system. In some cases, the `from` field identifies the channel itself.

`R2060`: Channels MUST include the `from` and `from.id` fields when generating an activity.

`R2061`: Bots and clients SHOULD include the `from` and `from.id` fields when generating an activity. A channel MAY reject an activity due to missing `from` and `from.id` fields.

The `from.name` field is optional and represents the display name for the account within the channel. Channels SHOULD include this value so clients and bots can populate their UIs and backend systems. Bots and clients SHOULD NOT send this value to channels that have a central record of this store, but they MAY send this value to channels that populate the value on every activity (e.g. an email channel).

`R2062`: Channels SHOULD include the `from.name` field if the `from` field is present and `from.name` is available.

`R2063`: Bots and clients SHOULD NOT include the `from.name` field unless it is semantically valuable within the channel.

### Recipient

The `recipient` field describes which client or bot is receiving this activity. This field is only meaningful when an activity is transmitted to exactly one recipient; it is not meaningful when it is broadcast to multiple recipients (as happens when an activity is sent to a channel). The purpose of the field is to allow the recipient to identify themselves. This is helpful when a client or bot has more than one identity within the channel. The value of the `recipient` field is a complex object of the [Channel account](#channel-account) type.

`R2070`: Channels MUST include the `recipient` and `recipient.id` fields when transmitting an activity to a single recipient.

`R2071`: Bots and clients SHOULD NOT include the `recipient` field when generating an activity.

The `recipient.name` field is optional and represents the display name for the account within the channel. Channels SHOULD include this value so clients and bots can populate their UIs and backend systems.

`R2072`: Channels SHOULD include the `recipient.name` field if the `recipient` field is present and `recipient.name` is available.

### Conversation

The `conversation` field describes the conversation in which the activity exists. The value of the `conversation` field is a complex object of the [Conversation account](#conversation-account) type.

`R2080`: Channels, bots, and clients MUST include the `conversation` and `conversation.id` fields when generating an activity.

The `conversation.name` field is optional and represents the display name for the conversation if it exists and is available.

`R2081`: Channels SHOULD include the `conversation.name` and `conversation.isGroup` fields if they are available.

`R2082`: Bots and clients SHOULD NOT include the `conversation.name` field unless it is semantically valuable within the channel.

`R2083`: Bots and clients SHOULD NOT include the `conversation.isGroup` field in activities they generate.

### Reply to ID

The `replyToId` field identifies the prior activity to which the current activity is a reply. This field allows threaded conversation and comment nesting to be communicated between participants. `replyToId` is valid only within the current conversation. (See [relatesTo](#relates-to) for references to other conversations.) The value of the `replyToId` field is a string.

`R2090`: Senders SHOULD include `replyToId` on an activity when it is a reply to another activity.

`R2091`: A channel MAY reject an activity if its `replyToId` does not reference a valid activity within the conversation.

`R2092`: Bots and clients MAY omit `replyToId` if it knows the channel does not make use of the field, even if the activity being sent is a reply to another activity.

### Entities

The `entities` field contains a flat list of metadata objects pertaining to this activity. Unlike attachments (see the [attachments](#attachments) field), entities do not necessarily manifest as user-interactable content elements, and are intended to be ignored if not understood. Senders may include entities they think may be useful to a receiver even if they are not certain the receiver can accept them. The value of each `entities` list element is a complex object of the [Entity](#entity) type.

`R2100`: Senders SHOULD omit the `entities` field if it contains no elements.

`R2101`: Senders MAY send multiple entities of the same type, provided the entities have distinct meaning.

`R2102`: Senders MUST NOT include two or more entities with identical types and contents.

`R2103`: Senders and receivers SHOULD NOT rely on specific ordering of the entities included in an activity.

### Channel data

Extensibility data in the activity schema is organized principally within the `channelData` field. This simplifies plumbing in SDKs that implement the protocol. The format of the `channelData` object is defined by the channel sending or receiving the activity.

`R2200`: Channels SHOULD NOT define `channelData` formats that are JSON primitives (e.g., strings, ints). Instead, they SHOULD define `channelData` as a complex type, or leave it undefined.

`R2201`: If the `channelData` format is undefined for the current channel, receivers SHOULD ignore the contents of `channelData`.

### Service URL

Activities are frequently sent asynchronously, with separate transport connections for sending and receiving traffic. The `serviceUrl` field is used by channels to denote the URL where replies to the current activity may be sent. The value of the `serviceUrl` field is of type string.

`R2300`: Channels MUST include the `serviceUrl` field in all activities they send to bots.

`R2301`: Channels SHOULD NOT include the `serviceUrl` field to clients who demonstrate they already know the channel's endpoint.

`R2302`: Bots and clients SHOULD NOT populate the `serviceUrl` field in activities they generate.

`R2302`: Channels MUST ignore the value of `serviceUrl` in activities sent by bots and clients.

`R2304`: Channels SHOULD use stable values for the `serviceUrl` field as bots may persist them for long periods.

## Message activity

Message activities represent content intended to be shown within a conversational interface. Message activities may contain text, speech, interactive cards, and binary or unknown attachments; typically channels require at most one of these for the message activity to be well-formed.

Message activities are identified by a `type` value of `message`.

### Text

The `text` field contains text content, either in the Markdown format, XML, or as plain text. The format is controlled by the [`textFormat`](#text-format) field as is plain if unspecified or ambiguous. The value of the `text` field is of type string.

`R3000`: The `text` field MAY contain an empty string to indicate sending text without contents.

`R3001`: Channels SHOULD handle `markdown`-formatted text in a way that degrades gracefully for that channel.

### Text format

The `textFormat` field denotes whether the [`text`](#text) field should be interpreted as [Markdown](https://daringfireball.net/projects/markdown/) [[4](#references)], plain text, or XML. The value of the `textFormat` field is of type string, with defined values of `markdown`, `plain`, and `xml`. The default value is `plain`. This field is not designed to be extended with arbitrary values.

The `textFormat` field controls additional fields within attachments etc. This relationship is described within those fields, elsewhere in this document.

`R3010`: If a sender includes the `textFormat` field, it SHOULD only send defined values.

`R3011`: Senders SHOULD omit `textFormat` if the value is `plain`.

`R3012`: Receivers SHOULD interpret undefined values as `plain`.

`R3013`: Bots and clients SHOULD NOT send the value `xml` unless they have prior knowledge that the channel supports it, and the characteristics of the supported XML dialect.

`R3014`: Channels SHOULD NOT send `markdown` or `xml` contents to bots.

`R3015`: Channels SHOULD accept `textformat` values of at least `plain` and `markdown`.

`R3016`: Channels MAY reject `textformat` of value `xml`.

### Locale

The `locale` field communicates the language code of the [`text`](#text) field. The value of the `locale` field is an [ISO 639](https://www.iso.org/iso-639-language-codes.html) [[5](#references)] code within a string.

`R3020`: Receivers SHOULD treat missing and unknown values of the `locale` field as unknown.

`R3021`: Receivers SHOULD NOT reject activities with unknown locale.

### Speak

The `speak` field indicates how the activity should be spoken via a text-to-speech system. The field is only used to customize speech rendering when the default is deemed inadequate. It replaces speech synthesis for any content within the activity, including text, attachments, and summaries. The value of the `speak` field is [SSML](https://www.w3.org/TR/speech-synthesis/) [[6](#references)] encoded within a string. Unwrapped text is allowed and is automatically upgraded to bare SSML.

`R3030`: The `speak` field MAY contain an empty string to indicate no speech should be generated.

`R3031`: Receivers unable to generate speech SHOULD ignore the `speak` field.

`R3032`: If no root SSML element is found, receivers MUST consider the included text to be the inner content of an SSML `<speak>` tag, prefaced with a valid [XML Prolog](https://www.w3.org/TR/xml/#sec-prolog-dtd) [[7](#references)], and otherwise upgraded to be a valid SSML document.

`R3033`: Receivers SHOULD NOT use XML DTD or schema resolution to include remote resources from outside the communicated XML fragment.

`R3034`: Channels SHOULD NOT send the `speak` field to bots.

### Input hint

The `inputHint` field indicates whether or not the generator of the activity is anticipating a response. This field is used predominantly within channels that have modal user interfaces, and is typically not used in channels with continuous chat feeds. The value of the `inputHint` field is of type string, with defined values of `accepting`, `expecting`, and `ignoring`. The default value is `accepting`.

`R3040`: If a sender includes the `inputHint` field, it SHOULD only send defined values.

`R3041`: If sending an activity to a channel where `inputHint` is used, bots SHOULD include the field, even when the value is `accepting`.

`R3042`: Receivers SHOULD interpret undefined values as `accepting`.

### Attachments

The `attachments` field contains a flat list of objects to be displayed as part of this activity. The value of each `attachments` list element is a complex object of the [Attachment](#attachment) type.

`R3050`: Senders SHOULD omit the `attachments` field if it contains no elements.

`R3051`: Senders MAY send multiple entities of the same type.

`R3052`: Receivers MAY treat attachments of unknown types as downloadable documents.

`R3053`: Receivers SHOULD preserve the ordering of attachments when processing content, except when rendering limitations force changes, e.g. grouping of documents after images.

### Attachment layout

The `attachmentLayout` field instructs user interface renderers how to present content included in the [`attachments`](#attachments) field. The value of the `attachmentLayout` field is of type string, with defined values of `list` and `carousel`. The default value is `list`.

`R3060`: If a sender includes the `attachmentLayout` field, it SHOULD only send defined values.

`R3061`: Receivers SHOULD interpret undefined values as `list`.

### Summary

The `summary` field contains text used to replace [`attachments`](#attachments) on channels that do not support them. The value of the `summary` field is of type string.

`R3070`: Receivers SHOULD consider the `summary` field to logically follow the `text` field.

`R3071`: Channels SHOULD NOT send the `summary` field to bots.

`R3072`: Channels able to process all attachments within an activity SHOULD ignore the `summary` field.

### Suggested actions

The `suggestedActions` field contains a payload of interactive actions that may be displayed to the user. Support for `suggestedActions` and their manifestation depends heavily on the channel. The value of the `suggestedActions` field is a complex object of the [Suggested actions](#suggested-actions-2) type.

### Value

The `value` field contains a programmatic payload specific to the activity being sent. Its meaning and format are defined in other sections of this document that describe its use.

`R3080`: Senders SHOULD NOT include `value` fields of primitive types (e.g. string, int). `value` fields SHOULD be complex types or omitted.

### Expiration

The `expiration` field contains a time at which the activity should be considered to be "expired" and should not be presented to the recipient. The value of the `expiration` field is an [ISO 8601](https://www.iso.org/iso-8601-date-and-time-format.html) [[2](#references)] encoded datetime within a string.

`R3090`: Senders SHOULD always use encode the value of `expiration` fields as UTC, and they SHOULD always include Z as an explicit UTC mark within the value.

### Importance

The `importance` field contains an enumerated set of values to signal to the recipient the relative importance of the activity.  It is up to the receiver to map these importance hints to the user experience. The value of the `importance` field is of type string, with defined values of `low`, `normal` and `high`. The default value is `normal`.

`R3100`: If a sender includes the `importance` field, it SHOULD only send defined values.

`R3101`: Receivers SHOULD interpret undefined values as `normal`.

### DeliveryMode

The `deliveryMode` field contains an one of an enumerated set of values to signal to the recipient alternate delivery paths for the activity.  The value of the `DeliveryMode` field is of type string, with defined values of `normal`, and `notification`. The default value is `normal`.

`R3110`: If a sender includes the `deliveryMode` field, it SHOULD only send defined values.

`R3111`: Receivers SHOULD interpret undefined values as `normal`.

## Contact relation update activity

Contact relation update activities signal a change in the relationship between the recipient and a user within the channel. Contact relation update activities generally do not contain user-generated content. The relationship update described by a contact relation update activity exists between the user in the `from` field (often, but not always, the user initiating the update) and the user or bot in the `recipient` field.

Contact relation update activities are identified by a `type` value of `contactRelationUpdate`.

### Action

The `action` field describes the meaning of the contact relation update activity. The value of the `action` field is a string. Only values of `add` and `remove` are defined, which denote a relationship between the users/bots in the `from` and `recipient` fields.

## Conversation update activity

Conversation update activities describe a change in a conversation's members, description, existence, or otherwise. Conversation update activities generally do not contain user-generated content. The conversation being updated is described in the `conversation` field.

Conversation update activities are identified by a `type` value of `conversationUpdate`.

`R4100`: Senders MAY include zero or more of `membersAdded`, `membersRemoved`, `topicName`, and `historyDisclosed` fields in a conversation update activity.

`R4101`: Each `channelAccount` (identified by `id` field) SHOULD appear at most once within the `membersAdded` and `membersRemoved` fields. An ID SHOULD NOT appear in both fields. An ID SHOULD NOT be duplicated within either field.

`R4102`: Channels SHOULD NOT use conversation update activities to indicate changes to a channel account's fields (e.g., `name`) if the channel account was not added to or removed from the conversation.

`R4103`: Channels SHOULD NOT send the `topicName` or `historyDisclosed` fields if the activity is not signaling a change in value for either field.

### Members added

The `membersAdded` field contains a list of channel participants (bots or users) added to the conversation. The value of the `membersAdded` field is an array of type [`channelAccount`](#channel-account).

### Members removed

The `membersRemoved` field contains a list of channel participants (bots or users) removed from the conversation. The value of the `membersRemoved` field is an array of type [`channelAccount`](#channel-account).

### Topic name

The `topicName` field contains the text topic or description for the conversation. The value of the `topicName` field is of type string.

### History disclosed

The `historyDisclosed` field is deprecated.

`R4110`: Senders SHOULD NOT include the `historyDisclosed` field.

## End of conversation activity

End of conversation activities signal the end of a conversation from the recipient's perspective. This may be because the conversation has been completely ended, or because the recipient has been removed from the conversation in a way that is indistinguishable from it ending. The conversation being ended is described in the `conversation` field.

End of conversation activities are identified by a `type` value of `endOfConversation`.

Both the `code` and the `text` fields are optional.

### Code

The `code` field contains a programmatic value describing why or how the conversation was ended. The value of the `code` field is of type string and its meaning is defined by the channel sending the activity.

### Text

The `text` field contains optional text content to be communicated to a user. The value of the `text` field is of type string, and its format is plain text.

## Event activity

Event activities communicate programmatic information from a client or channel to a bot. The meaning of an event activity is defined by the `name` field, which is meaningful within the scope of a channel. Event activities are designed to carry both interactive information (such as button clicks) and non-interactive information (such as a notification of a client automatically updating an embedded speech model).

Event activities are the asynchronous counterpart to [invoke activities](#invoke-activity). Unlike invoke, event is designed to be extended by client application extensions.

Event activities are identified by a `type` value of `event` and specific values of the `name` field.

`R5000`: Channels MAY allow application-defined event messages between clients and bots, if the clients allow application customization.

### Name

The `name` field controls the meaning of the event and the schema of the `value` field. The value of the `name` field is of type string.

`R5001`: Event activities MUST contain a `name` field.

`R5002`: Receivers MUST ignore event activities with `name` fields they do not understand.

### Value

The `value` field contains parameters specific to this event, as defined by the event name. The value of the `value` field is a complex type.

`R5100`: The `value` field MAY be missing or empty, if defined by the event name.

`R5101`: Extensions to the event activity SHOULD NOT require receivers to use any information other than the activity `type` and `name` fields to understand the schema of the `value` field.

### Relates to

The `relatesTo` field references another conversation, and optionally a specific activity within that activity. The value of the `relatesTo` field is a complex object of the Conversation reference type.

`R5200`: `relatesTo` SHOULD NOT reference an activity within the conversation identified by the `conversation` field.

## Invoke activity

Invoke activities communicate programmatic information from a client or channel to a bot, and have a corresponding return payload for use within the channel. The meaning of an invoke activity is defined by the `name` field, which is meaningful within the scope of a channel. 

Invoke activities are the synchronous counterpart to [event activities](#event-activity). Event activities are designed to be extensible. Invoke activities differ only in their ability to return response payloads back to the channel; because the channel must decide where and how to process these response payloads, Invoke is useful only in cases where explicit support for each invoke name has been added to the channel. Thus, Invoke is not designed to be a generic application extensibility mechanism.

Invoke activities are identified by a `type` value of `invoke` and specific values of the `name` field.

The list of defined Invoke activities is included in [Appendix III](#appendix-iii---protocols-using-the-invoke-activity).

`R5301`: Channels SHOULD NOT allow application-defined invoke messages between clients and bots.

### Name

The `name` field controls the meaning of the invocation and the schema of the `value` field. The value of the `name` field is of type string.

`R5401`: Invoke activities MUST contain a `name` field.

`R5402`: Receivers MUST ignore event activities with `name` fields they do not understand.

### Value

The `value` field contains parameters specific to this event, as defined by the event name. The value of the `value` field is a complex type.

`R5500`: The `value` field MAY be missing or empty, if defined by the event name.

`R5501`: Extensions to the event activity SHOULD NOT require receivers to use any information other than the activity `type` and `name` fields to understand the schema of the `value` field.

### Relates to

The `relatesTo` field references another conversation, and optionally a specific activity within that activity. The value of the `relatesTo` field is a complex object of the [Conversation reference](#conversation-reference) type.

`R5600`: `relatesTo` SHOULD NOT reference an activity within the conversation identified by the `conversation` field.

## Installation update activity

Installation update activities represent an installation or uninstallation of a bot within an organizational unit (such as a customer tenant or "team") of a channel. Installation update activities generally do not represent adding or removing a channel.

Installation update activities are identified by a `type` value of `installationUpdate`.

`R5700`: Channels MAY send installation activities when a bot is added to or removed from a tenant, team, or other organization unit within the channel.

`R5701`: Channels SHOULD NOT send installation activities when the bot is installed into or removed from a channel.

### Action

The `action` field describes the meaning of the installation update activity. The value of the `action` field is a string. Only values of `add` and `remove` are defined.

## Message delete activity

Message delete activities represent a deletion of an existing message activity within a conversation. The deleted activity is referred to by the `id` and `conversation` fields within the activity.

Message delete activities are identified by a `type` value of `messageDelete`.

`R5800`: Channels MAY elect to send message delete activities for all deletions within a conversation, a subset of deletions within a conversation (e.g. only deletions by certain users), or no activities within the conversation.

`R5801`: Channels SHOULD NOT send message delete activities for conversations or activities that the bot did not observe.

`R5802`: If a bot triggers a delete, the channel SHOULD NOT send a message delete activity back to that bot.

`R5803`: Channels SHOULD NOT send message delete activities corresponding to activities whose type is not `message`.

## Message update activity

Message update activities represent an update of an existing message activity within a conversation. The updated activity is referred to by the `id` and `conversation` fields within the activity, and the message update activity contains all fields in the revised message activity.

Message update activities are identified by a `type` value of `messageUpdate`.

`R5900`: Channels MAY elect to send messageUpdate  activities for all updates within a conversation, a subset of updates within a conversation (e.g. only updates by certain users), or no activities within the conversation.

`R5901`: If a bot triggers an update, the channel SHOULD NOT send a message update activity back to that bot.

`R5902`: Channels SHOULD NOT send message update activities corresponding to activities whose type is not `message`.

## Message reaction activity

Message reaction activities represent a social interaction on an existing message activity within a conversation. The original activity is referred to by the `id` and `conversation` fields within the activity. The `from` field represents the source of the reaction (i.e., the user that reacted to the message).

Message reaction activities are identified by a `type` value of `messageReaction`.

### Reactions added

The `reactionsAdded` field contains a list of reactions added to this activity. The value of the `reactionsAdded` field is an array of type [`messageReaction`](#message-reaction).

### Reactions removed

The `reactionsRemoved` field contains a list of reactions removed from this activity. The value of the `reactionsRemoved` field is an array of type [`messageReaction`](#message-reaction).

## Typing activity

Typing activities represent ongoing input from a user or a bot. This activity is often sent when keystrokes are being entered by a user, although it's also used by bots to indicate they're "thinking," and could also be used to indicate e.g. collecting audio from users.

Typing activities are intended to persist within UIs for three seconds.

Typing activities are identified by a `type` value of `typing`.

`R6000`: If able, clients SHOULD display typing indicators for three seconds upon receiving a typing activity.

`R6001`: Unless otherwise known for the channel, senders SHOULD NOT send typing activities more frequently than one every three seconds. (Senders MAY send typing activities every two seconds to prevent gaps from appearing.)

`R6002`: If a channel assigns an [`id`](#id) to a typing activity, it MAY allow bots and clients to delete the typing activity before its expiration.

`R6003`: If able, channels SHOULD send typing activities to bots.

## Complex types

This section defines complex types used within the activity schema, described above.

### Attachment

Attachments are content included within a [message activity](#message-activity): cards, binary documents, and other interactive content. They're intended to be displayed in conjunction with text content. Content may be sent via URL data URI within the `contentUrl` field, or inline in the `content` field.

`R7100`: Senders SHOULD NOT include both `content` and `contentUrl` fields in a single attachment.

#### Content type

The `contentType` field describes the [MIME media type](https://www.iana.org/assignments/media-types/media-types.xhtml) [[8](#references)] of the content of the attachment. The value of the `contentType` field is of type string.

#### Content

When present, the `content` field contains a structured JSON object attachment. The value of the `content` field is a complex type defined by the `contentType` field.

`R7110`: Senders SHOULD NOT include JSON primitives in the `content` field of an attachment.

#### Content URL

When present, the `contentUrl` field contains a URL to the content in the attachment. Data URIs, as defined in [RFC 2397](https://tools.ietf.org/html/rfc2397) [[9](#references)] are typically supported by channels. The value of the `contentUrl` field is of type string.

`R7120`: Receivers SHOULD accept HTTPS URLs.

`R7121`: Receivers MAY accept HTTP URLs.

`R7122`: Channels SHOULD accept data URIs.

`R7123`: Channels SHOULD NOT send data URIs to clients or bots.

#### Name

The `name` field contains an optional name or filename for the attachment. The value of the `name` field is of type string.

#### Thumbnail URL

Some clients have the ability to display custom thumbnails for non-interactive attachments or as placeholders for interactive attachments. The `thumbnailUrl` field identifies the source for this thumbnail. Data URIs, as defined in [RFC 2397](https://tools.ietf.org/html/rfc2397) [[9](#references)] are typically also allowed.

`R7140`: Receivers SHOULD accept HTTPS URLs.

`R7141`: Receivers MAY accept HTTP URLs.

`R7142`: Channels SHOULD accept data URIs.

`R7143`: Channels SHOULD NOT send `thumbnailUrl` fields to bots.

### Card action

A card action represents a clickable or interactive button for use within cards or as [suggested actions](#suggested-actions). They are used to solicit input from users. Despite their name, card actions are not limited to use solely on cards.

Card actions are meaningful only when sent to channels.

Channels decide how each action manifests in their user experience. In most cases, the cards are clickable. In others, they may be selected by speech input. In cases where the channel does not offer an interactive activation experience (e.g., when interacting over SMS), the channel may not support activation whatsoever. The decision about how to render actions is controlled by normative requirements elsewhere in this document (e.g. within the card format, or within the [suggested actions](#suggested-actions) definition).

#### Type

The `type` field describes the meaning of the button and behavior when the button is activated. Values of the `type` field are by convention short strings (e.g. "`openUrl`"). See subsequent sections for requirements specific to each action type.

* An action of type `messageBack` represents a text response to be sent via the chat system.
* An action of type `imBack` represents a text response that is added to the chat feed.
* An action of type `postBack` represents a text response that is not added to the chat feed.
* An action of type `openUrl` represents a hyperlink to be handled by the client.
* An action of type `downloadFile` represents a hyperlink to be downloaded.
* An action of type `showImage` represents an image that may be displayed.
* An action of type `signin` represents a hyperlink to be handled by the client's signin system.
* An action of type `playAudio` represents audio media that may be played.
* An action of type `playVideo` represents video media that may be played.
* An action of type `call` represents a telephone number that may be called.
* An action of type `payment` represents a request to provide payment.

#### Title

The `title` field includes text to be displayed on the button's face. The value of the `title` field is of type string, and does not contain markup.

This field applies to actions of all types.

`R7210`: Channels SHOULD NOT process markup within the `title` field (e.g. Markdown).

#### Image

The `image` field contains a URL referencing an image to be displayed on the button's face. Data URIs, as defined in [RFC 2397](https://tools.ietf.org/html/rfc2397) [[9](#references)] are typically supported by channels. The value of the `image` field is of type string.

This field applies to actions of all types.

`R7220`: Channels SHOULD accept HTTPS URLs.

`R7221`: Channels MAY accept HTTP URLs.

`R7222`: Channels SHOULD accept data URIs.

#### Text

The `text` field contains text content to be sent to a bot and included in the chat feed when the button is clicked. The contents of the `text` field may or may not be displayed, depending on the button type. The `text` field may contain markup, controlled by the [`textFormat`](#text-format) field in the activity root. The value of the `text` field is of type string.

This field is only used on actions of select types. Details on each type of action are included later in this document.

`R7230`: The `text` field MAY contain an empty string to indicate sending text without contents.

`R7231`: Channels SHOULD process the contents of the `text` field in accordance with the [`textFormat`](#text-format) field in the activity root.

#### Display text

The `displayText` field contains text content to be included in the chat feed when the button is clicked. The contents of the `displayText` field SHOULD always be displayed, when technically possible within the channel. The `displayText` field may contain markup, controlled by the [`textFormat`](#text-format) field in the activity root. The value of the `displayText` field is of type string.

This field is only used on actions of select types. Details on each type of action are included later in this document.

`R7240`: The `displayText` field MAY contain an empty string to indicate sending text without contents.

`R7241`: Channels SHOULD process the contents of the `displayText` field in accordance with the [`textFormat`](#text-format) field in the activity root.

#### Value

The `value` field contains programmatic content to be sent to a bot when the button is clicked. The contents of the `value` field are of any primitive or complex type, although certain activity types constrain this field.

This field is only used on actions of select types. Details on each type of action are included later in this document.

#### Message Back

A `messageBack` action represents a text response to be sent via the chat system. Message Back uses the following fields:
* `type` ("`messageBack`")
* `title`
* `image`
* `text`
* `displayText`
* `value` (of any type)

`R7350`: Senders SHOULD NOT include `value` fields of primitive types (e.g. string, int). `value` fields SHOULD be complex types or omitted.

`R7351`: Channels MAY reject or drop `value` fields not of complex type.

`R7352`: When activated, channels MUST send an activity of type `message` to all relevant recipients.

`R7353`: If the channel supports storing and transmitting text, the contents of the `text` field of the action MUST be preserved and transmitted in the `text` field of the generated message activity.

`R7352`: If the channel supports storing and transmitting additional programmatic values, the contents of the `value` field MUST be preserved and transmitted in the `value` field of the generated message activity.

`R7353`: If the channel supports preserving a different value in the chat feed than is sent to bots, it MUST include the `displayText` field in the chat history.

`R7354`: If the channel does not support `R7353` but does support recording text within the chat feed, it MUST include the `text` field in the chat history.

#### IM Back

An `imBack` action represents a text response that is added to the chat feed. Message Back uses the following fields:
* `type` ("`imBack`")
* `title`
* `image`
* `value` (of type string)

`R7360`: When activated, channels MUST send an activity of the type `message` to all relevant recipients.

`R7361`: If the channel supports storing and transmitting text, the contents of the `title` field MUST be preserved and transmitted in the `text` field of the generated message activity.

`R7362`: If the `title` field on an action is missing and the `value` field is of type string, the channel MAY transmit the contents of the `value` field in the `text` field of the generated message activity.

`R7363`: If the channel supports recording text within the chat feed, it MUST include the contents of the `title` field in the chat history.

#### Post Back

A `postBack` action represents a text response that is not added to the chat feed. Post Back uses the following fields:
* `type` ("`postBack`")
* `title`
* `image`
* `value` (of type string)

`R7370`: When activated, channels MUST send an activity of the type `message` to all relevant recipients.

`R7371`: Channels SHOULD NOT include text within the chat history when a Post Back action is activated.

`R7372`: Channels MUST reject or drop `value` fields not of string type.

`R7373`: If the channel supports storing and transmitting text, the contents of the `value` field MUST be preserved and transmitted in the `text` field of the generated message activity.

`R7374`: If the channel is unable to support transmitting to the bot without including history in the chat feed, it SHOULD use the `title` field as the display text.

#### Open URL actions

An `openUrl` action represents a hyperlink to be handled by the client. Open URL uses the following fields:
* `type` ("`openUrl`")
* `title`
* `image`
* `value` (of type string)

`R7380`: Senders MUST include a URL in the `value` field of an `openUrl` action.

`R7381`: Receivers MAY reject `openUrl` action whose `value` field is missing or not a string.

`R7382`: Receivers SHOULD reject or drop `openUrl` actions whose `value` field contains a data URI, as defined in [RFC 2397](https://tools.ietf.org/html/rfc2397) [[9](#references)].

`R7383`: Receivers SHOULD NOT reject `openUrl` actions whose `value` URI is of an otherwise unexpected URI scheme or value.

`R7384`: Clients with knowledge of particular URI schemes (e.g. HTTP) MAY handle `openUrl` actions within an embedded renderer (e.g., a browser control).

`R7385`: When available, clients SHOULD delegate handling of `openUrl` actions not handled by `R7354` to the operating-system- or shell-level URI handler.

#### Download File actions

An `downloadFile` action represents a hyperlink to be downloaded. Download File uses the following fields:
* `type` ("`downloadFile`")
* `title`
* `image`
* `value` (of type string)

`R7390`: Senders MUST include a URL in the `value` field of an `downloadFile` action.

`R7391`: Receivers MAY reject `downloadFile` action whose `value` field is missing or not a string.

`R7392`: Receivers SHOULD reject or drop `downloadFile` actions whose `value` field contains a data URI, as defined in [RFC 2397](https://tools.ietf.org/html/rfc2397) [[9](#references)].

#### Show Image File actions

An `showImage` action represents an image that may be displayed. Show Image uses the following fields:
* `type` ("`showImage`")
* `title`
* `image`
* `value` (of type string)

`R7400`: Senders MUST include a URL in the `value` field of an `showImage` action.

`R7401`: Receivers MAY reject `showImage` action whose `value` field is missing or not a string.

`R7402`: Receivers MAY reject `showImage` actions whole `value` field is a Data URI, as defined in [RFC 2397](https://tools.ietf.org/html/rfc2397) [[9](#references)].

#### Signin

A `signin` action represents a hyperlink to be handled by the client's signin system. Signin uses the following fields:
* `type` ("`signin`")
* `title`
* `image`
* `value` (of type string)

`R7410`: Senders MUST include a URL in the `value` field of an `signin` action.

`R7411`: Receivers MAY reject `signin` action whose `value` field is missing or not a string.

`R7412`: Receivers MUST reject or drop `signin` actions whose `value` field contains a data URI, as defined in [RFC 2397](https://tools.ietf.org/html/rfc2397) [[9](#references)].

#### Play Audio

A `playAudio` action represents audio media that may be played. Play Audio uses the following fields:
* `type` ("`playAudio`")
* `title`
* `image`
* `value` (of type string)

`R7420`: When activated, channels MAY send media events.

`R7421`: Channels MUST reject or drop `value` fields not of string type.

`R7422`: Senders SHOULD NOT send data URIs, as defined in [RFC 2397](https://tools.ietf.org/html/rfc2397) [[9](#references)], without prior knowledge that the channel supports them.

#### Play video

A `playAudio` action represents video media that may be played. Play Video uses the following fields:
* `type` ("`playVideo`")
* `title`
* `image`
* `value` (of type string)

`R7430`: When activated, channels MAY send media events.

`R7431`: Channels MUST reject or drop `value` fields not of string type.

`R7432`: Senders SHOULD NOT send data URIs, as defined in [RFC 2397](https://tools.ietf.org/html/rfc2397) [[9](#references)], without prior knowledge that the channel supports them.

#### Call

A `call` action represents a telephone number that may be called. Call uses the following fields:
* `type` ("`call`")
* `title`
* `image`
* `value` (of type string)

`R7440`: Senders MUST include a URL of scheme `tel` in the `value` field of an `signin` action.

`R7441`: Receivers MUST reject `signin` action whose `value` field is missing or not a string URI of the `tel` scheme.

#### Payment

A `payment` action represents a request to provide payment. Payment uses the following fields:
* `type` ("`payment`")
* `title`
* `image`
* `value` (of complex type [Payment Request](#payment-request))

`R7450`: Senders MUST include a complex object of type [Payment Request](#payment-request) in the `value` field of a `payment` action.

`R7451`: Channels MUST reject `payment` action whose `value` field is missing or not a complex object of type [Payment Request](#payment-request).

### Channel account

Channel accounts represent identities within a channel. The channel account includes an ID that can be used to identify and contact the account within that channel. Sometimes these IDs exist within a single namespace (e.g. Skype IDs); sometimes, they are federated across many servers (e.g. email addresses). In addition to the ID, channel accounts include display names and Azure Active Directory (AAD) object IDs.

#### Channel account ID

The `id` field is the identifier and address within the channel. The value of the `id` field is a string. An example `id` within a channel that uses email addresses is "name@example.com"

`R7510`: Channels SHOULD use the same values and conventions for account IDs regardless of their position within the schema (`from.id`, `recipient.id`, `membersAdded`, etc.). This allows bots and clients to use ordinal string comparisons to know when e.g. they are described in the `membersAdded` field of a `conversationUpdate` activity.

#### Channel account name

The `name` field is an optional, friendly name within the channel. The value of the `name` field is a string. An example `name` within a channel is "John Doe"

#### Channel account AAD Object ID

The `aadObjectId` field is an optional ID corresponding to the account's object ID within Azure Active Directory (AAD). The value of the `aadObjectId` field is a string.

### Conversation account

Conversation accounts represent the identity of conversations within a channel. In channels that support only a single conversation between two accounts (e.g. SMS), the conversation account is persistent and does not have a predetermined start or end. In channels that support multiple parallel conversations (e.g. email), each conversation will likely have a unique ID.

#### Conversation account ID

The `id` field is the identifier within the channel. The format of this ID is defined by the channel and is used as an opaque string throughout the protocol.

Channels SHOULD choose `id` values that are stable for all participants within a conversation. (For example, a poor example for the `id` field for a 1:1 conversation is to use the other participant's ID as the `id` value. This would result in a different `id` from each participant's perspective. A better choice is to sort the IDs of both participants and concatenate them together, which would be the same for both parties.)

#### Conversation account name

The `name` field is an optional, friendly name for the conversation within the channel. The value of the `name` field is a string.

#### Conversation account AAD Object ID

The `aadObjectId` field is an optional ID corresponding to the conversation's object ID within Azure Active Directory (AAD). The value of the `aadObjectId` field is a string.

#### Conversation account Is Group

The `isGroup` field indicates whether the conversation contains more than two participants at the time the activity was generated. The value of the `isGroup` field is a boolean; if omitted, the default value is `false`. This field typically controls the at-mention behavior for participants in the channel, and SHOULD be set to `true` if and only if more than two participants have the ability to both send and receive activities within the conversation.

### Entity

Entities carry metadata about an activity or conversation. Each entity's meaning and shape is defined by the `type` field, and additional type-specific fields sit as peers to the `type` field.

`R7600`: Receivers MUST ignore entities whose types they do not understand.

`R7601`: Receivers SHOULD ignore entities whose type they understand but are unable to process due to e.g. syntactic errors.

Parties projecting an existing entity type into the activity entity format are advised to resolve conflicts with the `type` field name and incompatibilities with serialization requirement `R2001` as part of the binding between the IRI and the entity schema.

#### Type

The `type` field is required, and defines the meaning and shape of the entity. `type` is intended to contain [IRIs](https://tools.ietf.org/html/rfc3987) [[3](#references)] although there are a small number on non-IRI entity types defined in [Appendix I](#appendix-ii---non-iri-entity-types).

`R7610`: Senders SHOULD use non-IRI types names only for types described in [Appendix II](#appendix-ii---non-iri-entity-types).

`R7611`: Senders MAY send IRI types for types described in [Appendix II](#appendix-ii---non-iri-entity-types) if they have knowledge that the receiver understands them.

`R7612`: Senders SHOULD use or establish IRIs for entity types not defined in [Appendix II](#appendix-ii---non-iri-entity-types).

### Suggested actions

Suggested actions may be sent within message content to create interactive action elements within a client UI.

`R7700`: Clients that do not support UI capable of rendering suggested actions SHOULD ignore the `suggestedActions` field.

`R7701`: Senders SHOULD omit the `suggestedActions` field if the `actions` field is empty.

#### To

The `to` field contains channel account IDs to whom the suggested actions should be displayed. This field may be used to filter actions to a subset of participants within the conversation.

`R7710`: If the `to` field is missing or empty, the client SHOULD display the suggested actions to all conversation participants.

`R7711`: If the `to` field contains invalid IDs, those values SHOULD be ignored.

#### Actions

The `actions` field contains a flat list of actions to be displayed. The value of each `actions` list element is a complex object of type `cardAction`.

### Message reaction

Message reactions represent a social interaction ("like", "+1", etc.). Message reactions currently only carry a single field: the `type` field.

#### Type

The `type` field describes the type of social interaction. The value of the `type` field is a string, and its meaning is defined by the channel in which the interaction occurs. Some common values such as `like` and `+1` although these are uniform by convention and not by rule.

## References

1. [RFC 2119](https://tools.ietf.org/html/rfc2119)
2. [ISO 8601](https://www.iso.org/iso-8601-date-and-time-format.html)
3. [RFC 3987](https://tools.ietf.org/html/rfc3987)
4. [Markdown](https://daringfireball.net/projects/markdown/)
5. [ISO 639](https://www.iso.org/iso-639-language-codes.html)
6. [SSML](https://www.w3.org/TR/speech-synthesis/)
7. [XML](https://www.w3.org/TR/xml/)
8. [MIME media types](https://www.iana.org/assignments/media-types/media-types.xhtml)
9. [RFC 2397](https://tools.ietf.org/html/rfc2397)
10. [ISO 3166-1](https://www.iso.org/iso-3166-country-codes.html)
11. [Adaptive Cards](https://adaptivecards.io)

# Appendix I - Changes

## 2018-02-07

* Initial draft

# Appendix II - Non-IRI entity types

Activity [entities](#entity) communicate extra metadata about the activity, such as a user's location or the version of the messaging app they're using. Activity types are intended to be IRIs, but a small list of non-IRI names are in common use. This appendix is an exhaustive list of the supported non-IRI entity types.

| Type           | URI equivalent                          | Description               |
| -------------- | --------------------------------------- | ------------------------- |
| GeoCoordinates | https://schema.org/GeoCoordinates       | Schema.org GeoCoordinates |
| Mention        | https://botframework.com/schema/mention | @-mention                 |
| Place          | https://schema.org/Place                | Schema.org place          |
| Thing          | https://schema.org/Thing                | Schema.org thing          |
| clientInfo     | N/A                                     | Skype client info         |

### clientInfo

The clientInfo entity contains extended information about the client software used to send a user's message. It contains three properties, all of which are optional.

`R9201`: Bots SHOULD NOT send the `clientInfo` entity.

`R9202`: Senders SHOULD include the `clientInfo` entity only when one or more fields are populated.

#### Locale (Deprecated)

The `locale` field contains the user's locale. This field duplicates the [`locale`](#locale) field in the Activity root. The value of the `locale` field is an [ISO 639](https://www.iso.org/iso-639-language-codes.html) [[5](#references)] code within a string.

The `locale` field within `clientInfo` is deprecated.

`R9211`: Receivers SHOULD NOT use the `locale` field within the `clientInfo` object.

`R9212`: Senders MAY populate the `locale` field within `clientInfo` for compatibility reasons. If compatibility with older receivers is not required, senders SHOULD NOT send the `locale` property.

#### Country

The `country` field contains the user's detected location. This value may differ from any [`locale`](#locale) data as the `country` is detected whereas `locale` is typically a user or application setting. The value of the `country` field is an [ISO 3166-1](https://www.iso.org/iso-3166-country-codes.html) [[10](#references)] 2- or 3-letter country code.

`R9220`: Channels SHOULD NOT allow clients to specify arbitrary values for the `country` field. Channels SHOULD use a mechanism like GPS, location API, or IP address detection to establish the country generating a request.

#### Platform

The `platform` field describes the messaging client platform used to generate the activity. The value of the `platform` field is a string and the list of possible values and their meaning is defined by the channel sending them.

Note that on channels with a persistent chat feed, `platform` is typically useful only in deciding which content to include, not the format of that content. For instance, if a user on a mobile device asks for product support help, a bot could generate help specific to their mobile device. However, the user may then re-open the chat feed on their PC so they can read it on that screen while making changes to their mobile device. In this situation, the `platform` field is intended to inform the content, but the content should be viewable on other devices.

`R9230`: Bots SHOULD NOT use the `platform` field to control how response data is formatted unless they have specific knowledge that the content they are sending may only ever be seen on the device in question.

# Appendix III - Protocols using the Invoke activity

The [invoke activity](#invoke-activity) is designed for use only within protocols supported by Bot Framework channels (i.e., it is not a generic extensibility mechanism). This appendix contains a list of all Bot Framework protocols using this activity.

## Payments

The Bot Framework payments protocol uses Invoke to calculate shipping and tax rates, and to communicate strong confirmation of completed payments.

The payments protocol defines three operations (defined in the `name` field of an invoke activity):
* `payment/shippingAddressChange`
* `payment/shppingOptionsChange`
* `payment/paymentResponse`

More detail can be found on the [Request payment](https://docs.microsoft.com/en-us/bot-framework/dotnet/bot-builder-dotnet-request-payment) page.

## Teams compose extension

The Microsoft Teams channel uses Invoke for [compose extensions](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/messaging-extensions). This use of Invoke is specific to Microsoft Teams.




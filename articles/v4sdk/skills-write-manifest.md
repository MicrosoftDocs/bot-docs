---
title: How to write a skill manifest | Microsoft Docs
description: Learn how to a Bot Framework skill manifest.
keywords: skills, manifest
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 04/17/2020
monikerRange: 'azure-bot-service-4.0'
---

# How to write a skill manifest

This article describes the format of the Bot Framework skill manifest schema. A _skill manifest_ is a JSON file that describes the activities the skill can perform, its input and output parameters, and the skill's endpoints. The manifest contains the information a developer would need to access the skill from another bot.

## Sample manifest

This is a sample manifest for a skill that exposes multiple activities.

[!code-json[Manifest](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogSkillBot/wwwroot/manifest/dialogchildbot-manifest-1.0.json)]

## Skill manifest

The following is the full schema for the Bot Framework skill manifest.

| Field | Type | Required | Description |
|:-|:-|:-|:-|
| $id | string | Required | The identifier for this skill manifest. |
| $schema | string | Optional | The HTTPS URI of a a JSON schema resource that describes the format of this manifest; optional but recommended. |
| activities | array of [eventActivity](#eventactivity), [invokeActivity](#invokeactivity), or [messageActivity](#messageactivity) objects | Required | The list of the activities accepted by the skill. |
| copyright | string | Optional | The copyright notice for the skill. |
| definitions | an object | Optional | Defines the structure of object payloads. |
| description | string | Optional | A description of the skill. |
| endpoints | array of [endpoint](#endpoint) objects | Required | The list of endpoints supported by the skill. |
| iconUrl | string | Optional | The URI of the icon to show for the skill. |
| license | string | Optional | The license agreement for the skill. |
| name | string | Required | The name of the skill. |
| version | string | Required | The skill's version. |
| privacyUrl | string | Optional | The URI of the privacy description for the skill. |
| publisherName | string | Required | The name of the skill publisher. |
| tags | string array | Optional | A set of tags for the skill. If present, each tag must be unique. |

## endpoint

Describes an endpoint supported by the skill.

| Field | Type | Required | Description
|:-|:-|:-|:-
| description | string | Optional | A description for the endpoint.
| endpointUrl | string | Required | The URI endpoint for the skill.
| msAppId | string | Required | The Microsoft AppId (GUID) for the skill, used to authenticate requests.
| name | string | Required | The unique name for the endpoint.
| protocol | string | Optional | The supported protocol. Default is "BotFrameworkV3".

## eventActivity

Describes an event activity accepted by the skill, where the `name` property indicates the task that the skill will perform.

| Field | Type | Required | Description
|:-|:-|:-|:-
| description | string | Optional | A description of the event.
| name | string | Required | The name of the event.
| resultValue | object | Optional | A JSON schema definition of the type of object that this activity can produce as output.
| type | string | Required | The activity type. Must be "event".
| value | object | Optional | A JSON schema definition of the type of object that this activity expects as input.

## invokeActivity

Describes an invoke activity accepted by the skill, where the `name` property indicates the task that the skill will perform.

| Field | Type | Required | Description |
|:-|:-|:-|:-|
| description | string | Optional | A description of the event.
| name | string | Required | The name of the event.
| resultValue | object | Optional | A JSON schema definition of the type of object that this activity can produce as output.
| type | string | Required | The activity type. Must be "invoke".
| value | object | Optional | A JSON schema definition of the type of object that this activity expects as input.

## messageActivity

Describes a message activity accepted by the skill, where the `text` property contains the user's utterance.

| Field | Type | Required | Description |
|:-|:-|:-|:-|
| description | string | Optional | A description of the activity.
| resultValue | object | Optional | A JSON schema definition of the type of object that this activity can produce as output.
| type | string | Required | The activity type. Must be "message".
| value | object | Optional | A JSON schema definition of the type of object that this activity expects as input.

## Additional information

The current skill manifest schema version is [skill-manifest-2.0.0.json](https://github.com/microsoft/botframework-sdk/blob/master/schemas/skills/skill-manifest-2.0.0.json).
This schema uses the draft 7 JSON schema vocabulary. For more information, see the [JSON Schema](http://json-schema.org/) site.

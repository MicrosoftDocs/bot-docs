---
title: How to write a skill manifest | Microsoft Docs
description: Learn how to a Bot Framework skill manifest.
keywords: skills, manifest
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 04/20/2020
monikerRange: 'azure-bot-service-4.0'
---

# How to write a skill manifest

This article describes the format of the Bot Framework skill manifest schema.

A _skill manifest_ is a JSON file that describes the actions the skill can perform, its input and output parameters, and the skill's endpoints. The manifest contains the information a developer would need to access the skill from another bot.

The current skill manifest schema version is [skill-manifest-2.0.0.json](https://schemas.botframework.com/schemas/skills/skill-manifest-2.0.0.json).
This schema uses draft 7 of the JSON schema vocabulary. For more information, see the [JSON Schema](http://json-schema.org/) site.

## The skill manifest

The skill manifest contains 4 categories of information:

- Metadata that describes the skill at a general level.
- A list of the endpoints that the skill provides.
- A list of the activities the the skill supports.
- An optional definitions object that describes the types of objects the activities can take as input or generate as output.

The following is the full schema for the Bot Framework skill manifest.

| Category/Field | Type | Required | Description |
|:-|:-|:-|:-|
| **Metadata**
| $id | string | Required | The identifier for this skill manifest. |
| $schema | string | Required | The HTTPS URI of a a JSON schema resource that describes the format of this manifest; optional but recommended. |
| copyright | string | Optional | The copyright notice for the skill. |
| description | string | Optional | A description of the skill. |
| iconUrl | string | Optional | The URI of the icon to show for the skill. |
| license | string | Optional | The license agreement for the skill. |
| name | string | Required | The name of the skill. |
| version | string | Required | The skill's version. |
| privacyUrl | string | Optional | The URI of the privacy description for the skill. |
| publisherName | string | Required | The name of the skill publisher. |
| tags | string array | Optional | A set of tags for the skill. If present, each tag must be unique. |
| **Endpoints**
| endpoints | array of [endpoint](#endpoint-object) objects | Required | The list of endpoints supported by the skill. |
| **Activities**
| activities | object containing named [activity objects](#activities) | Required | The set of initial activities accepted by the skill. |
| **Definitions**
| definitions | object | Optional | An object containing JSON schema definitions for the objects referenced by other parts of this schema. |

## Endpoints

Each endpoint object describes an endpoint supported by the skill.
See the [sample manifest](#sample-manifest) for an example endpoint list.

### endpoint object

| Field | Type | Required | Description
|:-|:-|:-|:-
| description | string | Optional | A description for the endpoint.
| endpointUrl | string | Required | The URI endpoint for the skill.
| msAppId | string | Required | The Microsoft AppId (GUID) for the skill, used to authenticate requests.
| name | string | Required | The unique name for the endpoint.
| protocol | string | Optional | The supported protocol. Default is "BotFrameworkV3".

## Activities

Each activity object describes an activity accepted by the skill.
The activity types allowed in the v2.0.0 skill manifest schema are: message, event, and invoke activities.
See the [sample manifest](#sample-manifest) for an example activity list.

### eventActivity object

Describes an event activity accepted by the skill, where the `name` property indicates the task that the skill will perform.

| Field | Type | Required | Description
|:-|:-|:-|:-
| description | string | Optional | A description of the event.
| name | string | Required | The name of the event.
| resultValue | object | Optional | A JSON schema definition of the type of object that this activity can produce as output.
| type | string | Required | The activity type. Must be "event".
| value | object | Optional | A JSON schema definition of the type of object that this activity expects as input.

### invokeActivity object

Describes an invoke activity accepted by the skill, where the `name` property indicates the task that the skill will perform.

| Field | Type | Required | Description |
|:-|:-|:-|:-|
| description | string | Optional | A description of the event.
| name | string | Required | The name of the event.
| resultValue | object | Optional | A JSON schema definition of the type of object that this activity can produce as output.
| type | string | Required | The activity type. Must be "invoke".
| value | object | Optional | A JSON schema definition of the type of object that this activity expects as input.

### messageActivity object

Describes a message activity accepted by the skill, where the `text` property contains the user's utterance.

| Field | Type | Required | Description |
|:-|:-|:-|:-|
| description | string | Optional | A description of the activity.
| resultValue | object | Optional | A JSON schema definition of the type of object that this activity can produce as output.
| type | string | Required | The activity type. Must be "message".
| value | object | Optional | A JSON schema definition of the type of object that this activity expects as input.

## Definitions

Each definition describes a schema that can be consumed by other parts of the document.
See the [sample manifest](#sample-manifest) for an example definition list.

## Sample manifest

This is a sample manifest for a skill that exposes multiple activities.

[!code-json[sample 2.0 manifest](~/../botframework-sdk/schemas/skills/samples/complex-skillmanifest-2.0.0.json)]

<!--
[!code-json[Manifest](~/../botbuilder-samples/samples/csharp_dotnetcore/81.skills-skilldialog/DialogSkillBot/wwwroot/manifest/dialogchildbot-manifest-1.0.json)]
-->

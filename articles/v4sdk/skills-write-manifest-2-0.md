---
title: How to write a skill manifest | Microsoft Docs
description: Learn how to a Bot Framework skill manifest.
keywords: skills, manifest
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 05/04/2020
monikerRange: 'azure-bot-service-4.0'
---

# How to write a v2.0 skill manifest

A _skill manifest_ is a JSON file that describes the actions the skill can perform, its input and output parameters, and the skill's endpoints. The manifest contains the information a developer would need to access the skill from another bot.

This article describes [version 2.0.0](https://schemas.botframework.com/schemas/skills/skill-manifest-2.0.0.json) of the Bot Framework skill manifest schema.
For a description of version 2.1.preview-1, see how to [Write a v2.1 skill manifest](skills-write-manifest-2-1.md).

The Bot Framework skill manifest schema uses draft 7 of the JSON schema vocabulary.

## Prerequisites

- Knowledge of [skills](skills-conceptual.md) and [skill bots](skills-about-skill-bots.md).
- Some familiarity with [JSON Schema](http://json-schema.org/) and the JSON format.

## The skill manifest

The skill manifest contains different categories of information:

- Metadata that describes the skill at a general level.
- A list of the endpoints that the skill provides.
- An optional list of the activities the the skill can receive.
- An optional definitions object that contains schemas for objects referenced by other parts of the document.

The following is the full schema for v2.0 of the Bot Framework skill manifest.

| Category/Field | Type | Required | Description |
|:-|:-|:-|:-|
| **Metadata**
| $id | string | Required | The identifier for the skill manifest. |
| $schema | string | Required | The HTTPS URI of a a JSON schema resource that describes the format of the manifest. For version 2.0.0, the URI is `https://schemas.botframework.com/schemas/skills/skill-manifest-2.0.0.json`. |
| copyright | string | Optional | The copyright notice for the skill. |
| description | string | Optional | A human-readable description of the skill. |
| iconUrl | string | Optional | The URI of the icon to show for the skill. |
| license | string | Optional | The license agreement for the skill. |
| name | string | Required | The name of the skill. |
| version | string | Required | The version of the skill the manifest describes. |
| privacyUrl | string | Optional | The URI of the privacy description for the skill. |
| publisherName | string | Required | The name of the skill publisher. |
| tags | string array | Optional | A set of tags for the skill. If present, each tag must be unique. |
| **Endpoints**
| endpoints | [endpoint](#endpoint-object) array | Required | The list of endpoints supported by the skill. At least one endpoint must be defined. Each endpoint must be unique. |
| **Activities**
| activities | object containing named [activity objects](#activities) | Required | The set of initial activities accepted by the skill. |
| **Definitions**
| definitions | object | Optional | An object containing subschemas for objects used in the manifest. |

## Endpoints

Each endpoint object describes an endpoint supported by the skill.

This example lists two endpoints for a skill.

[!code-json[sample endpoints](~/../botframework-sdk/schemas/skills/samples/complex-skillmanifest-2.0.0.json?range=17-32)]

### endpoint object

| Field | Type | Required | Description
|:-|:-|:-|:-
| description | string | Optional | A description for the endpoint.
| endpointUrl | string | Required | The URI endpoint for the skill.
| msAppId | string | Required | The Microsoft AppId (GUID) for the skill, used to authenticate requests.
| name | string | Required | The unique name for the endpoint.
| protocol | string | Optional | The supported protocol. Default is "BotFrameworkV3".

## Activities

Each activity object describes an activity accepted by the skill. The skill will initiate an action or task based on the initial activity received. The name associated with the activity object indicates the action or task the skill will perform.

Some activity types have a value property that can be used to provide additional input to the skill. When the skill ends (completes the action) it can provide a return value in the associated end-of-conversation activity's value property.

The activity types allowed in the v2.0.0 skill manifest schema are: message, event, and invoke activities.

This is a sample activity description.

[!code-json[sample activity](~/../botframework-sdk/schemas/skills/samples/complex-skillmanifest-2.0.0.json?range=34-44)]

### eventActivity object

Describes an event activity accepted by the skill.

| Field | Type | Required | Description
|:-|:-|:-|:-
| description | string | Optional | A description of the action.
| name | string | Required | The value of the event activity's name property.
| resultValue | object | Optional | A JSON schema definition of the type of object that the associated action can return.
| type | string | Required | The activity type. Must be "event".
| value | object | Optional | A JSON schema definition of the type of object that this action expects as input.

### invokeActivity object

Describes an invoke activity accepted by the skill.

| Field | Type | Required | Description |
|:-|:-|:-|:-|
| description | string | Optional | A description of the action.
| name | string | Required | The value of the invoke activity's name property.
| resultValue | object | Optional | A JSON schema definition of the type of object that the associated action can return.
| type | string | Required | The activity type. Must be "invoke".
| value | object | Optional | A JSON schema definition of the type of object that this action expects as input.

### messageActivity object

Describes a message activity accepted by the skill. The message activity's text property contains the user's utterance.

| Field | Type | Required | Description |
|:-|:-|:-|:-|
| description | string | Optional | A description of the action.
| resultValue | object | Optional | A JSON schema definition of the type of object that the associated action can return.
| type | string | Required | The activity type. Must be "message".
| value | object | Optional | A JSON schema definition of the type of object that this action expects as input.

## Definitions

Each definition describes a subschema that can be consumed by other parts of the document.

This is a sample subschema for flight booking information.

[!code-json[sample definition](~/../botframework-sdk/schemas/skills/samples/complex-skillmanifest-2.0.0.json?range=71-90)]

## Sample manifest

This is a full sample v2.0 manifest for a skill that exposes multiple activities.

[!code-json[sample 2.0 manifest](~/../botframework-sdk/schemas/skills/samples/complex-skillmanifest-2.0.0.json)]

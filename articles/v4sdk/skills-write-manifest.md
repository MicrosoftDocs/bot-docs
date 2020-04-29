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

The current skill manifest schema version is [skill-manifest-2.1.preview-1.json](https://schemas.botframework.com/schemas/skills/skill-manifest-2.1.preview-1.json).
This schema uses draft 7 of the JSON schema vocabulary.

## Prerequisites

- Knowledge of [skills](skills-conceptual.md) and [skill bots](skills-about-skill-bots.md).
- Some familiarity with [JSON Schema](http://json-schema.org/) and the JSON format.

## The skill manifest

The skill manifest contains different categories of information:

- Metadata that describes the skill at a general level.
- A list of the endpoints that the skill provides.
- Optional lists of the activities the the skill can receive and proactively send.
- An optional list of the dispatch models the skill supports.
- An optional definitions object that contains schemas for objects referenced by other parts of the document.

The following is the full schema for the Bot Framework skill manifest.

| Category/Field | Type | Required | Description |
|:-|:-|:-|:-|
| **Metadata**
| $id | string | Required | The identifier for the skill manifest. |
| $schema | string | Required | The HTTPS URI of a a JSON schema resource that describes the format of the manifest. |
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
| activitiesSent | object | Optional | Describes the proactive activities that the skill can send. |
| **Dispatch models**
| dispatchModels | [dispatchModels](#dispatch-models) object | Optional | Describes the language models and top-level intents supported by the skill. See  for the schema for this object. |
| **Definitions**
| definitions | object | Optional | An object containing subschemas for objects used in the manifest. |

## Endpoints

Each endpoint object describes an endpoint supported by the skill.
See the [sample manifest](#sample-manifest) for an example endpoint list.

### endpoint object

Describes an endpoint supported by the skill.

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

## Dispatch models

The dispatch model contains a list of language models and a list of top-level intents supported by the skill.

A locale name is a combination of an ISO 639 two-letter lowercase culture code associated with a language and an optional ISO 3166 two-letter uppercase subculture code associated with a country or region, for example "en" or "en-US".

| Field | Type | Required | Description |
|:-|:-|:-|:-|
| intents | object containing named arrays of strings | Optional | A list of the intents supported by the skill. Each name is the locale the intent is for, and the array contains the intents for that locale.
| languages | object containing named arrays of [languageModel objects](#languagemodel-object) | Required | A list of the language models supported by the skill. Each name is the locale the language models are for, and the array contains the language modules for that locale.

See the [sample manifest](#sample-manifest) for an example dispatch model.

### languageModel object

Describes a language model for a given culture. The name is a locale name.

| Field | Type | Required | Description |
|:-|:-|:-|:-|
| contentType | string | Required | Type of the language model.
| description | string | Optional | A description of the language model.
| id | string | Required | Unique identifier for the language model.
| name | string | Required |Name for the language model.
| url | string | Required | The URL for the language model.

## Definitions

Each definition describes a schema that can be consumed by other parts of the document.
See the [sample manifest](#sample-manifest) for an example definition list.

## Sample manifest

This is a sample manifest for a skill that exposes multiple activities.

[!code-json[sample 2.1 manifest](~/../botframework-sdk/schemas/skills/samples/complex-skillmanifest-2.1.preview-0.json)]

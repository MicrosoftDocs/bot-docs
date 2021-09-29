---
title: How to write a skill manifest | Microsoft Docs
description: Learn how to use the Bot Framework skill manifest.
author: JonathanFingold
ms.author: kamrani
ms.topic: reference
ms.service: bot-service
ms.date: 07/08/2021
---

# Write a skill manifest

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

A _skill manifest_ is a JSON file that describes the actions the skill can take, its input and output parameters, and the skill's endpoints. The manifest contains the information a developer needs to access the skill from another bot.

This article describes supported versions of the Bot Framework skill manifest schema.

| Version | Notes |
|:-|:-|
| [version 2.2](https://schemas.botframework.com/schemas/skills/v2.2/skill-manifest.json) | Updated some URI properties to accept URI references. |
| [version 2.1](https://schemas.botframework.com/schemas/skills/v2.1/skill-manifest.json) | Adds ability to describe proactive activities the skill can send and the dispatch models the skill uses. |
| [version 2.0](https://schemas.botframework.com/schemas/skills/v2.0/skill-manifest.json) | Initial version. |

The Bot Framework skill manifest schemas use [draft 7](http://json-schema.org/specification-links.html#draft-7) of the JSON schema vocabulary.

## Prerequisites

- Knowledge of [skills](skills-conceptual.md).
- Some familiarity with [JSON Schema](http://json-schema.org/) and the JSON format.

## The skill manifest

The skill manifest contains different categories of information:

- Metadata that describes the skill at a general level.
- A list of the endpoints that the skill provides.
- Optional lists of the activities the skill can receive and proactively send.
- An optional definitions object that contains schemas for objects referenced by other parts of the document.
- An optional list of the dispatch models the skill supports.

### [v2.2](#tab/v2-2)

The following table describes the full schema for v2.2 of the Bot Framework skill manifest.

| Category/Field | Type/Format | Required | Description |
|:-|:-|:-|:-|
| **Metadata** |
| $id | string | Required | The identifier for the skill manifest. |
| $schema | string/uri | Required | The HTTPS URI of a JSON schema resource that describes the format of the manifest. For version 2.2, the URI is `https://schemas.botframework.com/schemas/skills/v2.2/skill-manifest.json`. |
| copyright | string | Optional | The copyright notice for the skill. |
| description | string | Optional | A human-readable description of the skill. |
| iconUrl | string/uri-reference | Optional | The URI of the icon to show for the skill. |
| license | string | Optional | The license agreement for the skill. |
| name | string | Required | The name of the skill. |
| privacyUrl | string/uri-reference | Optional | The URI of the privacy description for the skill. |
| publisherName | string | Required | The name of the skill publisher. |
| tags | string array | Optional | A set of tags for the skill. If present, each tag must be unique. |
| version | string | Required | The version of the skill the manifest describes. |
| **Endpoints** |
| endpoints | [endpoint](#endpoint-object) array | Required | The list of endpoints supported by the skill. At least one endpoint must be defined. Each endpoint must be unique. |
| **Activities** |
| activities | object containing named [activity objects](#activities) | Optional | The set of initial activities accepted by the skill. |
| activitiesSent | object containing named [activity objects](#activities) | Optional | Describes the proactive activities that the skill can send. |
| **Definitions** |
| definitions | object | Optional | An object containing subschemas for objects used in the manifest. |
| **Dispatch models** |
| dispatchModels | [dispatchModels](#dispatch-models) object | Optional | Describes the language models and top-level intents supported by the skill. See [Dispatch models](#dispatch-models) for the schema for this object. |

### [v2.1](#tab/v2-1)

The following table describes the full schema for v2.1.1 of the Bot Framework skill manifest.

| Category/Field | Type/Format | Required | Description |
|:-|:-|:-|:-|
| **Metadata** |
| $id | string | Required | The identifier for the skill manifest. |
| $schema | string/uri | Required | The HTTPS URI of a JSON schema resource that describes the format of the manifest. For version 2.1, the URI is `https://schemas.botframework.com/schemas/skills/v2.1/skill-manifest.json`. |
| copyright | string | Optional | The copyright notice for the skill. |
| description | string | Optional | A human-readable description of the skill. |
| iconUrl | string/uri | Optional | The URI of the icon to show for the skill. |
| license | string | Optional | The license agreement for the skill. |
| name | string | Required | The name of the skill. |
| version | string | Required | The version of the skill the manifest describes. |
| privacyUrl | string/uri | Optional | The URI of the privacy description for the skill. |
| publisherName | string | Required | The name of the skill publisher. |
| tags | string array | Optional | A set of tags for the skill. If present, each tag must be unique. |
| **Endpoints** |
| endpoints | [endpoint](#endpoint-object) array | Required | The list of endpoints supported by the skill. At least one endpoint must be defined. Each endpoint must be unique. |
| **Activities** |
| activities | object containing named [activity objects](#activities) | Optional | The set of initial activities accepted by the skill. |
| activitiesSent | object containing named [activity objects](#activities) | Optional | Describes the proactive activities that the skill can send. |
| **Definitions** |
| definitions | object | Optional | An object containing subschemas for objects used in the manifest. |
| **Dispatch models** |
| dispatchModels | [dispatchModels](#dispatch-models) object | Optional | Describes the language models and top-level intents supported by the skill. See [below](#dispatch-models) for the schema for this object. |

### [v2.0](#tab/v2-0)

The following table describes the full schema for v2.0 of the Bot Framework skill manifest.

| Category/Field | Type/Format | Required | Description |
|:-|:-|:-|:-|
| **Metadata** |
| $id | string | Required | The identifier for the skill manifest. |
| $schema | string/uri | Required | The HTTPS URI of a JSON schema resource that describes the format of the manifest. For version 2.0.0, the URI is `https://schemas.botframework.com/schemas/skills/v2.0/skill-manifest.json`. |
| copyright | string | Optional | The copyright notice for the skill. |
| description | string | Optional | A human-readable description of the skill. |
| iconUrl | string/uri | Optional | The URI of the icon to show for the skill. |
| license | string | Optional | The license agreement for the skill. |
| name | string | Required | The name of the skill. |
| version | string | Required | The version of the skill the manifest describes. |
| privacyUrl | string/uri | Optional | The URI of the privacy description for the skill. |
| publisherName | string | Required | The name of the skill publisher. |
| tags | string array | Optional | A set of tags for the skill. If present, each tag must be unique. |
| **Endpoints** |
| endpoints | [endpoint](#endpoint-object) array | Required | The list of endpoints supported by the skill. At least one endpoint must be defined. Each endpoint must be unique. |
| **Activities** |
| activities | object containing named [activity objects](#activities) | Required | The set of initial activities accepted by the skill. |
| **Definitions** |
| definitions | object | Optional | An object containing subschemas for objects used in the manifest. |

---

## Endpoints

Each endpoint object describes an endpoint supported by the skill.

### [v2.2](#tab/v2-2)

This example lists two endpoints for a skill.

[!code-json[v2.2 sample endpoints](~/../botframework-sdk/schemas/skills/v2.2/samples/complex-skillmanifest.json?range=17-32)]

### [v2.1](#tab/v2-1)

This example lists two endpoints for a skill.

[!code-json[v2.1 sample endpoints](~/../botframework-sdk/schemas/skills/v2.1/samples/complex-skillmanifest.json?range=17-32)]

### [v2.0](#tab/v2-0)

This example lists two endpoints for a skill.

[!code-json[v2.0 sample endpoints](~/../botframework-sdk/schemas/skills/v2.0/samples/complex-skillmanifest.json?range=17-32)]

---

### endpoint object

Describes an endpoint supported by the skill.

### [v2.2 / v2.1 / v2.0](#tab/v2-2+v2-1+v2-0)

| Field | Type/Format | Required | Description |
|:-|:-|:-|:-|
| description | string | Optional | A description of the endpoint. |
| endpointUrl | string/uri | Required | The URI endpoint for the skill. |
| msAppId | string | Required | The Microsoft AppId (GUID) for the skill, used to authenticate requests. Must match the regular expression: `^[0-9A-Fa-f]{8}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{4}-[0-9A-Fa-f]{12}$`. |
| name | string | Required | The unique name for the endpoint. |
| protocol | string | Optional | The supported Bot protocol. Default is "BotFrameworkV3", which represents the Bot Connector API version 3. Use the default value unless your skill specifically uses a different protocol. |

---

## Activities

Each activity object describes an activity accepted by the skill. The skill begins an action or task based on the initial activity it receives. The name associated with the activity object indicates the action or task the skill will perform.

Some activity types have a value property that can be used to provide extra input to the skill. When the skill ends (completes the action), it can provide a return value in the associated end-of-conversation activity's value property.

### [v2.2](#tab/v2-2)

The activity types allowed are: message, event, invoke, and _other_ activities. A skill can receive an invoke activity, but can't send one.

Here's a sample activity description.

[!code-json[v2.2 sample activity](~/../botframework-sdk/schemas/skills/v2.2/samples/complex-skillmanifest.json?range=84-94)]

### [v2.1](#tab/v2-1)

The activity types allowed are: message, event, invoke, and _other_ activities. A skill can receive an invoke activity, but can't send one.

Here's a sample activity description.

[!code-json[v2.1 sample activity](~/../botframework-sdk/schemas/skills/v2.1/samples/complex-skillmanifest.json?range=84-94)]

### [v2.0](#tab/v2-0)

The activity types allowed are: message, event, and invoke activities.

Here's a sample activity description.

[!code-json[v2.0 sample activity](~/../botframework-sdk/schemas/skills/v2.0/samples/complex-skillmanifest.json?range=34-44)]

---

### eventActivity object

Describes an event activity accepted or sent by the skill.
The meaning of an event activity is defined by its name field, which is meaningful within the scope of the skill.

### [v2.2 / v2.1 / v2.0](#tab/v2-2+v2-1+v2-0)

| Field       | Type   | Required | Description                                                                       |
|:------------|:-------|:---------|:----------------------------------------------------------------------------------|
| description | string | Optional | A description of the action the event should initiate.                            |
| name        | string | Required | The value of the event activity's name property.                                  |
| resultValue | object | Optional | A JSON schema definition of the type of object that the action can return.        |
| type        | string | Required | The activity type. Must be "event".                                               |
| value       | object | Optional | A JSON schema definition of the type of object that this action expects as input. |

---

### invokeActivity object

Describes an invoke activity accepted by the skill.
The meaning of an invoke activity is defined by its name field, which is meaningful within the scope of the skill.

### [v2.2 / v2.1 / v2.0](#tab/v2-2+v2-1+v2-0)

| Field       | Type   | Required | Description                                                                           |
|:------------|:-------|:---------|:--------------------------------------------------------------------------------------|
| description | string | Optional | A description of the action the invoke should initiate.                               |
| name        | string | Required | The value of the invoke activity's name property.                                     |
| resultValue | object | Optional | A JSON schema definition of the type of object that the associated action can return. |
| type        | string | Required | The activity type. Must be "invoke".                                                  |
| value       | object | Optional | A JSON schema definition of the type of object that this action expects as input.     |

---

### messageActivity object

Describes a message activity accepted or sent by the skill. The message activity's text property contains the user's or bot's utterance.

### [v2.2 / v2.1 / v2.0](#tab/v2-2+v2-1+v2-0)

| Field       | Type   | Required | Description                                                                           |
|:------------|:-------|:---------|:--------------------------------------------------------------------------------------|
| description | string | Optional | A description of the action.                                                          |
| resultValue | object | Optional | A JSON schema definition of the type of object that the associated action can return. |
| type        | string | Required | The activity type. Must be "message".                                                 |
| value       | object | Optional | A JSON schema definition of the type of object that this action expects as input.     |

---

### otherActivities object

Describes any other activity type accepted or sent by the skill.

### [v2.2 / v2.1](#tab/v2-2+v2-1)

| Field | Type | Required | Description |
|:-|:-|:-|:-|
| type | string | Required | The activity type. Must be one of the other Bot Framework activity types: "contactRelationUpdate", "conversationUpdate", "deleteUserData", "endOfConversation", "handoff", "installationUpdate", "messageDelete", "messageReaction", "messageUpdate", "suggestion", "trace", or "typing". |

The otherActivities object can include other properties, but the skill manifest schema does not define their meaning.

### [v2.0](#tab/v2-0)

Not defined in v2.0 of the skill manifest schema.

---

## Definitions

Each definition describes a subschema that can be consumed by other parts of the document.

### [v2.2](#tab/v2-2)

Here's a sample subschema for flight booking information.

[!code-json[v2.2 sample definition](~/../botframework-sdk/schemas/skills/v2.2/samples/complex-skillmanifest.json?range=127-146)]

### [v2.1](#tab/v2-1)

Here's a sample subschema for flight booking information.

[!code-json[v2.1 sample definition](~/../botframework-sdk/schemas/skills/v2.1/samples/complex-skillmanifest.json?range=127-146)]

### [v2.0](#tab/v2-0)

Here's a sample subschema for flight booking information.

[!code-json[v2.0 sample definition](~/../botframework-sdk/schemas/skills/v2.0/samples/complex-skillmanifest.json?range=71-90)]

---

## Dispatch models

The dispatch model contains a list of language models and a list of top-level intents supported by the skill. It is an advanced feature to enable a developer of a skill consumer to compose a language model that combines the features of the consumer and skill bots.

### [v2.2](#tab/v2-2)

A locale name is a combination of an ISO 639 two-letter lowercase culture code associated with a language and an optional ISO 3166 two-letter uppercase subculture code associated with a country or region, for example "en" or "en-US".

| Field | Type | Required | Description |
|:-|:-|:-|:-|
| intents | string array | Optional | A list of the top-level intents supported by the skill. Each intent must be unique. |
| languages | object containing named [languageModel](#languagemodel-object) arrays | Optional | A list of the language models supported by the skill. Each name is the locale the language models are for, and the array contains the language models for that locale. A dispatch model must support at least one locale. Each locale within the languages field must be unique. |

Here's a sample dispatch model that contains two languages models across three locales. It also describes two top-level intents that the skill can recognize.

[!code-json[sample activity](~/../botframework-sdk/schemas/skills/v2.1/samples/complex-skillmanifest.json?range=33-82)]

### languageModel object

Describes a language model for a given culture. The name is a locale name.

| Field       | Type/Format          | Required | Description                          |
|:------------|:---------------------|:---------|:-------------------------------------|
| contentType | string               | Required | Type of the language model.          |
| description | string               | Optional | A description of the language model. |
| name        | string               | Required | Name of the language model.          |
| url         | string/uri-reference | Required | The URL for the language model.      |

### [v2.1](#tab/v2-1)

A locale name is a combination of an ISO 639 two-letter lowercase culture code associated with a language and an optional ISO 3166 two-letter uppercase subculture code associated with a country or region, for example "en" or "en-US".

| Field | Type | Required | Description |
|:-|:-|:-|:-|
| intents | string array | Optional | A list of the top-level intents supported by the skill. Each intent must be unique. |
| languages | object containing named [languageModel](#languagemodel-object) arrays | Optional | A list of the language models supported by the skill. Each name is the locale the language models are for, and the array contains the language models for that locale. A dispatch model must support at least one locale. Each locale within the languages field must be unique. |

Here's a sample dispatch model that contains two languages models across three locales. It also describes two top-level intents that the skill can recognize.

[!code-json[sample activity](~/../botframework-sdk/schemas/skills/v2.1/samples/complex-skillmanifest.json?range=33-82)]

### languageModel object

Describes a language model for a given culture. The name is a locale name.

| Field       | Type/Format | Required | Description                          |
|:------------|:------------|:---------|:-------------------------------------|
| contentType | string      | Required | Type of the language model.          |
| description | string      | Optional | A description of the language model. |
| name        | string      | Required | Name of the language model.          |
| url         | string/uri  | Required | The URL for the language model.      |

### [v2.0](#tab/v2-0)

Not defined in v2.0 of the skill manifest schema.

---

## Sample manifest

### [v2.2](#tab/v2-2)

Here's a full sample v2.2 manifest for a skill that exposes multiple activities.

[!code-json[sample 2.2 manifest](~/../botframework-sdk/schemas/skills/v2.2/samples/complex-skillmanifest.json)]

### [v2.1](#tab/v2-1)

Here's a full sample v2.1 manifest for a skill that exposes multiple activities.

[!code-json[sample 2.1 manifest](~/../botframework-sdk/schemas/skills/v2.1/samples/complex-skillmanifest.json)]

### [v2.0](#tab/v2-0)

Here's a full sample v2.0 manifest for a skill that exposes multiple activities.

[!code-json[sample 2.0 manifest](~/../botframework-sdk/schemas/skills/v2.0/samples/complex-skillmanifest.json)]

---

## Next steps

- How to [Implement a skill](skill-implement-skill.md).
- How to [Use dialogs within a skill](skill-actions-in-dialogs.md).
- How to [Implement a skill consumer](skill-implement-consumer.md).
- How to [Use a dialog to consume a skill](skill-use-skilldialog.md).

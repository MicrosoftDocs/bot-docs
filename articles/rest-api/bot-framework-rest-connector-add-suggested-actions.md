---
title: Add suggested actions to messages | Microsoft Docs
description: Learn how to add suggested actions to messages using the Bot Connector service.
author: kbrandl
ms.author: v-kibran
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 05/12/2017
ms.reviewer: 
---

# Add suggested actions to messages
> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-add-suggested-actions.md)
> - [Node.js](../nodejs/bot-builder-nodejs-send-suggested-actions.md)
> - [REST](../rest-api/bot-framework-rest-connector-add-suggested-actions.md)

[!include[Introduction to suggested actions](~/includes/snippet-suggested-actions-intro.md)] 

> [!TIP]
> To learn how various channels render suggested actions, see the [Channel Inspector][channelInspector].

## Send suggested actions

To add suggested actions to a message, set the `suggestedActions` property of the [Activity][Activity] to specify the list of [CardAction][CardAction] objects that represent the buttons to be presented to the user. 

The following request sends a message that presents three suggested actions to the user. In this example request, `https://smba.trafficmanager.net/apis` represents the base URI; the base URI for requests that your bot issues may be different. For details about setting the base URI, see [API Reference](~/rest-api/bot-framework-rest-connector-api-reference.md#base-uri).

```http
POST https://smba.trafficmanager.net/apis/v3/conversations/abcd1234/activities/5d5cdc723
Authorization: Bearer ACCESS_TOKEN
Content-Type: application/json
```

```json
{
    "type": "message",
    "from": {
        "id": "12345678",
        "name": "sender's name"
    },
    "conversation": {
        "id": "abcd1234",
        "name": "conversation's name"
   },
   "recipient": {
        "id": "1234abcd",
        "name": "recipient's name"
    },
    "text": "I have colors in mind, but need your help to choose the best one.",
    "inputHint": "expectingInput",
    "suggestedActions": {
        "actions": [
            {
                "type": "imBack",
                "title": "Blue",
                "value": "Blue"
            },
            {
                "type": "imBack",
                "title": "Red",
                "value": "Red"
            },
            {
                "type": "imBack",
                "title": "Green",
                "value": "Green"
            }
        ]
    },
    "replyToId": "5d5cdc723"
}
```

When the user taps one of the suggested actions, the bot will receive a message from the user that contains the `value` of the corresponding action.

## Additional resources

- [Create messages](~/rest-api/bot-framework-rest-connector-create-messages.md)
- [Send and receive messages](~/rest-api/bot-framework-rest-connector-send-and-receive-messages.md)

[channelInspector]: https://docs.botframework.com/en-us/channel-inspector/channels/Facebook/#navtitle

[Activity]: ~/rest-api/bot-framework-rest-connector-api-reference.md#activity-object

[CardAction]: ~/rest-api/bot-framework-rest-connector-api-reference.md#cardaction-object

[SuggestedAction]: ~/rest-api/bot-framework-rest-connector-api-reference.md#suggestedactions-object
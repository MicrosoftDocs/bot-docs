---
title: Adaptive cards | Microsoft Docs
description: Learn how to configure adaptive cards.
author: vkannan
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/04/2017
---

# Configure adaptive cards

<a href="http://adaptivecards.io" target="_blank">Adaptive Cards</a> is a new schema that defines rich UI cards for use in several 
different endpoints including Microsoft Bot Framework channels. 

Conversation Designer provides a deeply integrated authoring environment to author, preview, and use adaptive cards in your bots. 

Adaptive cards can be defined in several different key places.

- A simple response to [action](conversation-designer-actions.md) for a task.
- In feedback state in a dialog.
- In prompt states in a dialog. Note that prompts can have separate cards: one for the response and another for re-prompting.

To define an adaptive card, navigate to the relevant editor. Browse and choose from one of the existing Adaptive Card 
Templates or build your own in the JSON code editor. 

<!--TODO: Insert screenshot -->

As you are building a card, a rich preview of the card is rendered in the authoring portal.

## Use entities and language generation templates
Adaptive cards support full language generation resolution.

* `entityName` uses entities inside the card.
* `responseTemplateName` uses simple or conditional response templates inside the card.

<!--
# Binding form flow input fields to bot entities
TODO: fill this out based on design/ implementation -->

<!-- ## Adaptive Card schema

You can learn more about adaptive cards here  TODO: Insert link to adaptive cards schema documentation -->

## Sample adaptive card payload

The following JSON shows the payload of an adaptive card.

```json
{
    "$schema": "https://microsoft.github.io/AdaptiveCards/schemas/adaptive-card.json",
    "type": "AdaptiveCard",
    "version": "1.0",
    "body": [
        {
            "speak": "<s>Serious Pie is a Pizza restaurant which is rated 9.3 by customers.</s>",
            "type": "ColumnSet",
            "columns": [
                {
                    "type": "Column",
                    "size": "2",
                    "items": [
                        {
                            "type": "TextBlock",
                            "text": "[Greeting], [TimeOfDayTemplate], You can eat in {location}",
                            "weight": "bolder",
                            "size": "extraLarge"
                        },
                        {
                            "type": "TextBlock",
                            "text": "9.3 · $$ · Pizza",
                            "isSubtle": true
                        },
                        {
                            "type": "TextBlock",
                            "text": "[builtin.feedback.display]",
                            "wrap": true
                        }
                    ]
                },
                {
                    "type": "Column",
                    "size": "1",
                    "items": [
                        {
                            "type": "Image",
                            "url": "http://res.cloudinary.com/sagacity/image/upload/c_crop,h_670,w_635,x_0,y_0/c_scale,w_640/v1397425743/Untitled-4_lviznp.jpg",
                            "size":"auto"
                        }
                    ]
                }
            ]
        }
    ],
    "actions": [
        {
            "type": "Action.Http",
            "method": "POST",
            "title": "More Info",
            "url": "http://foo.com"
        },
        {
            "type": "Action.Http",
            "method": "POST",
            "title": "View on Foursquare",
            "url": "http://foo.com"
        }
    ]
}
```

## Next step
> [!div class="nextstepaction"]
> [Connect to channels](conversation-designer-deploy.md)

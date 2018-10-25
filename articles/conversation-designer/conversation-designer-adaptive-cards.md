---
title: Configure adaptive cards | Microsoft Docs
description: Learn how to configure adaptive cards.
author: vkannan
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 12/13/2017
ROBOTS: NoIndex, NoFollow
---

# Configure adaptive cards
> [!IMPORTANT]
> Conversation Designer is not available to all customers yet. More details on
> availability of Conversation Designer will come later this year.

<a href="http://adaptivecards.io" target="_blank">Adaptive Cards</a> is a new schema that defines rich UI cards for use in several 
different endpoints including Microsoft Bot Framework channels. 

Conversation Designer provides a deeply integrated authoring environment to author, preview, and use adaptive cards in your bots. 

Adaptive cards can be defined in several different key places.

- A simple response to action for a task.
- In feedback state in a dialogue.
- In prompt states in a dialogue. Note that prompts can have separate cards: one for the response and another for re-prompting.

To define an adaptive card, navigate to the relevant editor. Browse and choose from one of the existing Adaptive Card 
Templates or build your own in the JSON code editor. 

As you are building a card, a rich preview of the card is rendered in the authoring portal.

> [!NOTE]
> Features of adaptive cards remain under ongoing development. All channels do not support all adaptive card features at this time. To see which features each channel supports, see the Channel status section.

## Input form

Adaptive cards can contain input forms. In Conversation Designer, forms are integrated with task entities. For example, if a field has an `id` of **myName** and the form `Submit` action is performed, a `taskEntity` with the name **myName** will be created and will contain the value of the field. 

The code snippet below shows how the **myName** entity is defined in code:

``javascript
{
   "type": "Input.Text",
   "id": "myName",
   "placeholder": "Last, First"
}
``

Additionally, if a field has an id of `@task` then the value of the field will be used as a task name. When this field is triggered (e.g.: a button click), then the named task will be executed. 

Take this snippet code for example:

``javascript
{
  'type': 'Action.Submit',
  'title': 'Search',
  'speak': '<s>Search</s>',
  'data': {
    '@task': 'Hotel Search'
  }
}
``

When this button is clicked, a submit action is triggered and the `context.sticky` will be set to `Hotel Search`. This will result in the **Hotel Search** task to execute. To use this feature, make sure the `@task` matches a task name that you have defined in Conversation Designer.

## Use entities and language generation templates
Adaptive cards support full language generation resolution.

* `entityName` uses entities inside the card.
* `responseTemplateName` uses simple or conditional response templates inside the card.

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


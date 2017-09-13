---
title: Response template | Microsoft Docs
description: How to set up response template for Conversation Designer bots.
author: vkannan
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/04/2017
ROBOTS: NoIndex, NoFollow
---

# Response template for Conversation Designer bots
> [!IMPORTANT]
> Conversation Designer is not available to all customers yet. More details on
> availability of Conversation Designer will come later this year.

Language generation enables your bot to communicate with the user in a rich and natural manner using variable response messages. These messages are managed through response templates in Conversation Designer.

Response templates enable response reuse and helps maintain a consistent tone and language in bot responses. 

## Create response templates

In Conversation Designer, you can create response templates that you can reuse in any situation where you need to send a response to a user. 

Simple response templates define a one-off collection of possible speak and display utterances. You can then use these templates in your bot's responses, prompt states, or in adaptive cards to re-construct a fully resolved string.

To add a simple response template, do the following:
1. From the left panel, click **Add**. A context menu will show up.
2. Click **Simple response**. An edit window appears in the main content panel.
3. In the **Simple response name** field, enter a name for this simple response.
4. In the **Bot's response to user** field, enter the response one phrase at a time.
5. From the upper right corner, click **Save** when you are finished setting up the simple response. 

For example, you can create an acknowledgement phrase template (call it, "AckPhrase") with the following items:

- Ok
- Sure
- You bet
- I'd be happy to help
- Glad to help

Using this template, the conversation runtime resolves to one of these strings randomly so that the bot's responses sound more natural instead of dull and monotonous.

## Conditional response templates

Conditional response templates specify multiple responses with conditions. The callback function you write helps the language generation engine decide which response string to use based on the condition you specified. 

For example, a time of day template should resolve to "good morning" or "good evening" based on the actual time of day. 

To add a conditional response template, do the following:
1. From left panel, click **Add**. A context menu will appear.
2. Click **Conditional response**. An edit window appears in the main content panel.
3. In the **Conditional response name** field, enter a name for this template.
4. In the **Conditional response** field, enter your phrases one at a time. For example, for the "timeOfDayTemplate", enter "Good morning" and "Good evening" as two possible responses in the template.
5. For the "Good morning" response, specify it's **Condition name** as "morning". Likewise, for the "Good evening" response, specify its **Condition name** as "evening". The custom script you will write will return either one of these condition.
6. In the **Code to execute on run** field, enter a callback function name (e.g.: `fnResolveTimeOfDayTemplate`). Then, click **View code** to load the *Scripts** editor. From there, you can define the implementation for this callback function.
7. In the upper right corner, click **Save** to save your changes and create this template.

Sample code for the *fnResolveTimeOfDayTemplate* callback function. This function will return the string that will match either the "morning" or "evening" **Condition name** specified by the response.

```javascript
export function fnTimeOfDayTemplate = function(context) {
    var currentTime = new Date().getHours();
    if(currentTime >= 12) {
        return "evening!";
    } else {
        return "morning!";
    }
}

```

## Send a response to user

To send a user a response using response templates, enclose either the template names in square brackets. For example, to use the "AckPhrase" simple response template in a feedback state, enter the **Bot's response to user** phrase as `[AckPhrase], I will get that done for you right away`

If you want to use square brackets in your responses, use "\" as the escape character to instruct the conversation runtime to skip resolution for square brackets.

If you have entities defined, you can use them in your response to user as well. You can refer to your entities in language generation by enclosing the entity name in curly brackets. For example, if your bot has a `location` entity, you can refer to it in your responses as follows: `[AckPhrase], I can help you find a table at {location}.`

## Nesting response templates

Response templates can be "nested"; one response template can have references to another response template. For example, you could use the `AckPhrase` simple response template and the `timeOfDayTemplate` conditional response template to construct a new `timeBasedGreeting` response template that uses both templates to resolve the final response. 

> [!TIP]
> While template nesting is a powerful feature, be sure to check that your nested templates do not cause an infinite loop. That is, avoid situations where you have `AckPhrase` calling `timeOfDayTemplate` and `timeOfDayTemplate` calling back to `AckPhrase`.

For example, create a new **Simple response** name `timeBasedGreeting` and enter the following text as a possible **Bot's response to user** for this template: `[timeOfDayTemplate] [AckPhrase], ... `

## Define user utterance as help tips

While defining user **Utterance**, you an also set an utterance as a **Help Tip**. To set an utterance as a **Help Tip**, click the `...` to the right of the utterance and select **Use as help tip**. 

The help tip is used in the builtin language generation template `[builtin-tasktips]` anywhere you define a **Bot's response to user** field. For example, you could compose a response that says: `Sorry I did not understand that. Here are some things you can try - [builtin.tasktips]`

If a task has no **Help Tip** defined, then the `[builtin-tasktips]` will resolve to an empty string. If a task has multiple **Help Tip** defined, then the `[builtin-tasktips]` will select one at random each time a response is given.

## Next step
> [!div class="nextstepaction"]
> [Adaptive cards](conversation-designer-adaptive-cards.md)

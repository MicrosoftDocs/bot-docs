---
title: Define a dialogue as a Do action | Microsoft Docs
description: Learn how to set up dialogue as a Do action.
author: vkannan
manager: kamrani
ms.topic: article
ms.prod: bot-framework
ms.date: 12/13/2017
ROBOTS: NoIndex, NoFollow
---

# Define a dialogue as a Do action
> [!IMPORTANT]
> Conversation Designer is not available to all customers yet. More details on
> availability of Conversation Designer will come later this year.

A dialogue covers the conversational model for a specific task. For example, a cafe bot that helps users book a table could define a task called "Book Table". The **Do** action would be bound to a dialogue that models the conversational flow between the bot and user. 

Dialogues are particularly useful when a bot engages in a back and forth conversation with the user to help complete a task.  Users rarely express all required values to complete a task in a single utterance. 

Booking a table requires the location, party size, date, and time preference. A bot that books tables needs to understand and handle various possible phrases the user could say. 

- *book a table*: In this case, none of the required entities were captured. The bot must now engage in a conversation with the user.
- *book a table for 7PM this Saturday*: In this case, date and time preference is specified but the bot must still gather the intended location and party size.

During a conversation, some entities may change based on user input. For example, if the user says "Book a table in Redmond for this Sunday at 7 pm." but the Redmond location closes at six on Sundays, the bot's dialogue should handle such invalid requests. 

Conversation Designer provides a drag-and-drop dialogue designer to help you visualize your conversation flow. The dialogue designer offers seven *dialogue states* you can use to model your conversation flow.

## Dialogue states

A dialogue is made up of conversational states. The dialogue itself is modeled as a structured, directed flow that provide the conversation runtime a structure for how to execute the conversational flow.

Dialogues come with many builtin states you can use. Supported builtin states include the following:

- [**Start**](#start-state): Represents the starting state for a conversational flow. All dialogues must have at least one start state defined.
- [**Return**](#return-state): Represents completion of the specific conversational flow. Given that conversational flows are composable, return instructs the conversation runtime to return execution to any possible callers of this dialogue.
- [**Decision**](#decision-state): Represents a point of branching in the conversational flow.
- [**Process**](#process-state): Represents a state where your bot is executing business logic.
- [**Prompt**](#prompt-state): Represents a state you can prompt the user for input. 
- [**Feedback**](#feedback-state): Represent a state you can provide feedback or confirmation to the user. For example, a dialogue confirming that the reservation has been made.
- [**Module**](#module-state): Represents a call to another dialogue. Since dialogue flows are composable by default,  this state can call either a shared dialogue or some other dialogue as defined under this task.

Each conversational state is connected to another using dialogue connectors in the dialogue designer.

Every dialogue state has an associated state editor that is used to specify the properties for that state, including callback function names for custom scripts. The **State Editor** located as a resize pane at the bottom of the dialogue designer main view port. To bring up the editor, double-click a state from the dialogue designer and a **State Editor** will display the properties for that state.
<!-- TODO: insert screenshot of the wrench in horizontal menu -->

The following sub-sections provide more details about each of these builtin dialogue states.

### Start state

Start state denotes the starting point of a dialogue. Required value is state **name**. The name is defaulted to "Start" and you can edit it to rename this state.

### Return state

Return state represents completion of the specific branch of the conversational flow. Since dialogues are composable, the state also instructs the conversation runtime to return execution to the caller dialogue. Required value is state **name**. The name is defaulted to "Return" and you can edit it to rename this state.

### Decision state

A decision state represents branching in the conversational flow. You can write custom script to evaluate which branch to follow. Depending on user input and business logic, the script will return one of the possible transition values. Each transition value prompts the conversation runtime to run a different branch of the dialogue.

Required properties for decision state:
- **Name**: Unique name for the decision state.
- **Code to execute on run**: Name of the callback function that implements your business logic to determine which branch of the conversation to take. 

#### Example code for decision state

The following sample callback function returns a decision that instruct the conversation runtime which branch to execute.

```javascript
module.exports.fnDecisionState = function(context) {
    var a = context.taskEntities['a'];
    if (a[0].value === '0') {
        return 'yes';
    }
    else if (a[0].value === '1') {
        return 'no';
    }
}

```

### Process state

Process state represents a point in the dialogue where the bot is either driving the conversation forward or attempting to perform the final task completion action. 

Required properties for process state:
- **Name**: Unique name for the process state
- **Code to execute on run**: Name of the callback function that implements your business logic.

#### Example code for process state

The following sample callback function gets the weather and returns the weather information to the user.

```javascript
module.exports.fnGetWeather = function(context) {
    var options =  {
        host: 'mock',
        path: '/get?a=b',
        method: 'get'
    };
    return http.request(options).then(function(response) {
        context.contextEntities['x'].value= response.statusCode;
        var jsonBody = JSON.parse(response.body);
		  // understand response
        if (response.statusCode != "200") {
            // error
        }
    });
}

```

### Prompt state

Prompt state asks the user for a specific piece of information. Prompt states embody a sub-dialogue system within them and so by definition are complex states. 

Within a prompt state, you can define the actual response to provide to the user and optionally include an adaptive card. You can then specify a trigger to parse and understand the user's response. This trigger can be either LUIS or a custom code recognizer using regex.  

If the user provide an invalid input, the bot can re-prompt the user for the same information. This behavior can also be defined in the prompt state editor. 

#### Prompting the user

Prompt response allows you to specify the message to use when **Prompting the user** for input. For example, to gather the date and time, possible responses might be "When would you like to come in?" or "When would you like to dine with us?"

#### Prompt listening for user input

After the user is prompted to respond, the conversation runtime will automatically listen for user input and try to understand what was said. Configure a trigger based on LUIS or custom code recognizer to try and understand user input and intent. This is similar to the task trigger.

#### Re-prompting the user

Use the re-prompt section to specify a response for each attempt. Each row in the re-prompt section corresponds to the re-prompt string used for that specific turn. The first response will be used for the first re-prompt, the second for the second, and so on. For example:

*I'm sorry, I did not understand, when did you want to come in?*
*My bad, I'm having a hard time understanding you. Let's try this again - when did you want to come in?*

#### Prompt callback functions

You can specify two different callback functions on a prompt state. 

1. **Before every prompt and reprompt**: Execute this function before every prompt or rerompt. This callback function expects a boolean return value where true means execute this prompt or reprompt and false means do not execute this prompt or reprompt. You can use `getTurnIndex()` to get the current turn index for that prompt execution.
2. **On responding**: Execute this function every time a prompt has been generated but before it has been sent to the user (including reprompt response). This enables the script to modify the message sent to the user.

#### Sample code

This code snippet shows an example for **before executing** callback.

```javascript
module.exports.fnBeforeExecuting = function(context) {
    if(context.responses[0].text === "C") {
        return false;
    }
	return true;
}
```

This code snippet shows an example for **On prompting** callback.

```javascript
module.exports.fnOnPrompting = function(context) {
    // include a hint card
    var activity = context.responses.slice(-1).pop();
    activity.attachments.push({
        "contentType": "application/vnd.microsoft.card.hero",
        "content": {
            "buttons": [
                {
                    "type": "imBack",
                    "title": "1",
                    "value": "1"
                },
                {
                    "type": "imBack",
                    "title": "2",
                    "value": "2"
                }
            ]
        }
    });
}

```

This code snippet shows an example for **Before reprompting** callback.

```javascript
module.exports.fnBeforeReprompting = function(context) {
    if(context.responses[0].text === "C") {
        return false;
    }
	return true;
}
```

### Feedback state

Use this state to provide a response back to user. Typical use cases for this includes the final outcome after the task completion attempt or to provide a response back to user in failure conditions, etc. 

Each feedback state requires a unique name, some possible response values and can optionally include adaptive card definitions. Learn more about [adaptive cards definition](conversation-designer-adaptive-cards.md).

Each feedback state also allows for an **On responding** callback function where you can write your custom script to modify the activity payload if required before it is sent to the user. 


```javascript
module.exports.fnOnResponding = function(context) {
    // include a hint card
    var activity = context.responses.slice(-1).pop();
    activity.attachments.push({
        "contentType": "application/vnd.microsoft.card.hero",
        "content": {
            "buttons": [
                {
                    "type": "imBack",
                    "title": "1",
                    "value": "1"
                },
                {
                    "type": "imBack",
                    "title": "2",
                    "value": "2"
                }
            ]
        }
    });

}
```

### Module state

A module state is used to add a reference to execute a sub-dialogue. Use this state to string dialogues together. 

Each module state requires a unique name and a pointer to a specific dialogue to execute. Possible options for dialogues to execute must live under **Shared dialogues** or under the specific **Tasks**.

## Multiple dialogues under a task

A task can have multiple dialogues. To add a dialogue to a task, simply select the task and click on the **Add** button in the left tree panel then click **Add dialogue**. This will add a new dialogue under the selected task. 

Since dialogues are composable, you can bound the root dialogue flow to the task that calls other dialogues under the task. This allows you to encapsulate sub-tasks and enable reuse. This also enables you to chain these dialogues in a conversational flow using *module* states.

## Next step
> [!div class="nextstepaction"]
> [Response templates](conversation-designer-response-templates.md)

---
title: Define a LUIS recognizer as task trigger | Microsoft Docs
description: Learn how to set up language understanding recognizer as task trigger using LUIS.ai
author: vkannan
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 07/04/2017
ROBOTS: NoIndex, NoFollow
---

# Define a LUIS recognizer as task trigger
> [!IMPORTANT]
> Conversation Designer is not available to all customers yet. More details on
> availability of Conversation Designer will come later this year.

Most often, user express their intent to perform a task in natural language. With Conversation Designer, you can easily set up a natural language understanding model for your bot powered by <a href="http://luis.ai" target="_blank">LUIS.ai</a>.

To do this, select the trigger type **User says or types something**. This will provide you with the option to specify the **intent** name. 

You can search for existing intents or create a new one in the **Which language intent?** field.

## Create a new intent

To create a new intent, type in the name for the intent and click **Create new intent**. Enter example utterances for the possible things you expect your users to say that should trigger this specific task.

For example, a cafe bot should be able to perform the task of finding cafe locations near the user. To handle this scenario, select **User says or types something** and set the **Intent name** to "LocationNearMe". Then, provide example utterances for this intent. For example: 
- *find locations near me*
- *find cafe locations near me*
- *is the Redmond store open now?*
- *is there a store in Seattle?*
- *what cafe locations are open now?*
- *where can I get something to eat?*
- *I would like to get something to eat*
- *I'm hungry*

Enter as many utterances as you can possibly think of that help express the user's intent to trigger this specific task.

## Default intents provisioned for your bot

By default, your bot is provisioned with 4 intents. 
- **None**: This is the fallback (default) intent for your bot. Use this intent to help capture things that your bot does not yet know how to respond to.
- **Help**: Set up with example utterances that help determine if user needs help. *Help, I need help, what can I say?, I'm stuck* and so on.
- **Greeting**: Set up with example utterances that help match Greeting intent such as *Hi, Hello, Good morning, How are you bot* and so on.
- **Cancel**: Set up with example utterances for cancel intent. *Stop, Cancel that, do not do it, revert* and so on.

## Create and label entities

Apart from helping determine user intent, language understanding can also help you determine specific entities of interest relevant to the task. 
As an example, when user says *find cafe locations near Redmond*, you might want to extract *Redmond* as a possible value for entity ***location***. 

To set up entities for the task, from the **Utterance** string, select the part of the utterance that should be an example for an entity value. Assign this to an existing entity or create a new one. 
To create a new entity, type the name of the entity into the **Search or create** field and click **Create new entity**. 

# Supported entity types

Language understanding provides you the power to create different types of entities. When you create a new entity, you must specify a `type`. 

Available types are :

- **Simple**: This is the *default* type.
- **List**: Use this type if your entity has a finite set of possible values. Example: *Color*, *City*.
- **Hierarchical**: Use this type to create entities with parent-child relationship. Example: *fromCity* and *toCity* both have *City* entity as parent
- **Composite**: Use this type to create groups of values that make up a meaningful unit. Example: *City* and *State* together make up the *Location* entity.

<!-- # pre-built entity types TBD -->

# Entities in use

As you create and add entities to the language understanding section, the **Entities in use** table updates with the list of entities this specific task uses. You can manually add other entities used in this task to the list. 

Available options are:

- **Code**: This is an entity that is created in your custom script. You can specify it here to help with authoring features like intellisense.

<!-- # Use as help tip TBD  -->

## Next step
> [!div class="nextstepaction"]
> [Code recognizer](conversation-designer-code-recognizer.md)

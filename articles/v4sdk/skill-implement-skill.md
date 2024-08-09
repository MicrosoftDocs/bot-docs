---
title: Implement a skill
description: Learn how to implement a skill, using the Bot Framework SDK.
keywords: skills
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: Gabo.Gilabert
ms.service: azure-ai-bot-service
ms.topic: how-to
ms.date: 08/08/2024
monikerRange: 'azure-bot-service-4.0'
ms.custom:
  - evergreen
---

# Implement a skill

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

You can use skills to extend another bot.
A _skill_ is a bot that can perform a set of tasks for another bot.

- A manifest describes a skill's interface. Developers who don't have access to the skill's source code can use the information in the manifest to design their skill consumer.
- A skill can use claims validation to manage which bots or users can access it.

This article demonstrates how to implement a skill that echoes the user's input.

[!INCLUDE [skills-and-identity-types](../includes/skills-and-identity-types.md)]

[!INCLUDE [java-python-sunset-alert](../includes/java-python-sunset-alert.md)]

## Prerequisites

- Knowledge of [bot basics](bot-builder-basics.md) and [skills](skills-conceptual.md).
- An Azure subscription (to deploy your skill). If you don't have one, create a [free account](https://azure.microsoft.com/free/?WT.mc_id=A261C142F) before you begin.
- A copy of the **skills simple bot-to-bot** sample in [**C#**](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/80.skills-simple-bot-to-bot), [**JavaScript**](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/80.skills-simple-bot-to-bot), [**Java**](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/80.skills-simple-bot-to-bot), or [**Python**](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/80.skills-simple-bot-to-bot).

> [!NOTE]
> Starting with version 4.11, you don't need an app ID and password to test a skill locally in the Bot Framework Emulator. An Azure subscription is still required to deploy your skill to Azure.

## About this sample

The **skills simple bot-to-bot** sample includes projects for two bots:

- The _echo skill bot_, which implements the skill.
- The _simple root bot_, which implements a root bot that consumes the skill.

This article focuses on the skill, which includes support logic in its bot and adapter.

### [C#](#tab/cs)

:::image type="content" source="./media/skills-simple-skill-cs.png" alt-text="Skill C# class diagram.":::

### [JavaScript](#tab/javascript)

:::image type="content" source="./media/skills-simple-skill-js.png" alt-text="Skill JavaScript class diagram.":::

### [Java](#tab/java)

:::image type="content" source="./media/skills-simple-skill-java.png" alt-text="Skill Java class diagram.":::

### [Python](#tab/python)

:::image type="content" source="./media/skills-simple-skill-python.png" alt-text="Skill Python class diagram.":::

---

For information about the simple root bot, see how to [Implement a skill consumer](skill-implement-consumer.md).

## Resources

For deployed bots, bot-to-bot authentication requires that each participating bot has valid identity information.
However, you can test multitenant skills and skill consumers locally with the Emulator without an app ID and password.

To make the skill available to user-facing bots, register the skill with Azure. For more information, see how to [register a bot with Azure AI Bot Service](../bot-service-quickstart-registration.md).

## Application configuration

Optionally, add the skill's identity information to its configuration file. If either the skill or skill consumer provides identity information, both must.

The _allowed callers_ array can restrict which skill consumers can access the skill.
To accept calls from any skill consumer, add an "*" element.

> [!NOTE]
> If you're testing your skill locally without bot identity information, neither the skill nor the skill consumer run the code to perform claims validation.

### [C#](#tab/cs)

**EchoSkillBot\appsettings.json**

Optionally, add the skill's identity information to the appsettings.json file.

[!code-csharp[configuration file](~/../botbuilder-samples/samples/csharp_dotnetcore/80.skills-simple-bot-to-bot/EchoSkillBot/appsettings.json)]

### [JavaScript](#tab/javascript)

**echo-skill-bot/.env**

Optionally, add the skill's identity information to the .env file.

[!code-ini[configuration file](~/../botbuilder-samples/samples/javascript_nodejs/80.skills-simple-bot-to-bot/echo-skill-bot/.env)]

### [Java](#tab/java)

**DialogSkillBot\resources\application.properties**

Optionally, add the skill's app ID and password to the application.properties file.

[!code-ini[configuration file](~/../botbuilder-samples/samples/java_springboot/80.skills-simple-bot-to-bot/DialogSkillBot/src/main/resources/application.properties)]

### [Python](#tab/python)

Optionally, add the skill's app ID and password to the config.py file.

**config.py**

[!code-python[configuration file](~/../botbuilder-samples/samples/python/80.skills-simple-bot-to-bot/echo-skill-bot/config.py?range=12-17)]

---

## Activity handler logic

### To accept input parameters

The skill consumer can send information to the skill. One way to accept such information is to accept them via the _value_ property on incoming messages. Another way is to handle event and invoke activities.

The skill in this example doesn't accept input parameters.

### To continue or complete a conversation

When the skill sends an activity, the skill consumer should forward the activity on to the user.

However, you need to send an `endOfConversation` activity when the skill finishes; otherwise, the skill consumer continues to forward user activities to the skill.
Optionally, use the activity's _value_ property to include a return value, and use the activity's _code_ property to indicate why the skill is ending.

#### [C#](#tab/cs)

**EchoSkillBot\Bots\EchoBot.cs**

[!code-csharp[OnMessageActivityAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/80.skills-simple-bot-to-bot/EchoSkillBot/Bots/EchoBot.cs?range=13-31)]

#### [JavaScript](#tab/javascript)

**echo-skill-bot/bot.js**

[!code-javascript[onMessage](~/../botbuilder-samples/samples/javascript_nodejs/80.skills-simple-bot-to-bot/echo-skill-bot/bot.js?range=10-26)]

#### [Java](#tab/java)

**echoSkillBot\EchoBot.java**

[!code-java[Message handler](~/../botbuilder-samples/samples/java_springboot/80.skills-simple-bot-to-bot/DialogSkillBot/src/main/java/com/microsoft/bot/sample/echoskillbot/EchoBot.java?range=28-52)]

#### [Python](#tab/python)

**echo-skill-bot/bots/echo_bot.py**

[!code-python[Message handler](~/../botbuilder-samples/samples/python/80.skills-simple-bot-to-bot/echo-skill-bot/bots/echo_bot.py?range=9-27)]

---

### To cancel the skill

For multi-turn skills, you would also accept `endOfConversation` activities from a skill consumer, to allow the consumer to cancel the current conversation.

The logic for this skill doesn't change from turn to turn. If you implement a skill that allocates conversation resources, add resource cleanup code to the end-of-conversation handler.

#### [C#](#tab/cs)

**EchoSkillBot\Bots\EchoBot.cs**

[!code-csharp[OnEndOfConversationActivityAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/80.skills-simple-bot-to-bot/EchoSkillBot/Bots/EchoBot.cs?range=33-39)]

#### [JavaScript](#tab/javascript)

**echo-skill-bot/bot.js**

 Use the `onUnrecognizedActivityType` method to add an end-of-conversation logic. In the handler, check whether the unrecognized activity's `type` equals `endOfConversation`.

[!code-javascript[onEndOfConversation](~/../botbuilder-samples/samples/javascript_nodejs/80.skills-simple-bot-to-bot/echo-skill-bot/bot.js?range=28-35)]

#### [Java](#tab/java)

**echoSkillBot\EchoBot.java**

[!code-java[End-of-conversation handler](~/../botbuilder-samples/samples/java_springboot/80.skills-simple-bot-to-bot/DialogSkillBot/src/main/java/com/microsoft/bot/sample/echoskillbot/EchoBot.java?range=55-61)]

#### [Python](#tab/python)

**echo-skill-bot/bots/echo_bot.py**

[!code-python[End-of-conversation handler](~/../botbuilder-samples/samples/python/80.skills-simple-bot-to-bot/echo-skill-bot/bots/echo_bot.py?range=29-33)]

---

## Claims validator

This sample uses an allowed callers list for claims validation. The skill's configuration file defines the list. The validator object then reads the list.

You must add a _claims validator_ to the authentication configuration. The claims are evaluated after the authentication header. Your validation code should throw an error or exception to reject the request. There are many reasons you might want to reject an otherwise authenticated request. For example:

- The skill is part of a paid-for service. User's not in the database shouldn't have access.
- The skill is proprietary. Only certain skill consumers can call the skill.

> [!IMPORTANT]
> If you don't provide a claims validator, your bot will generate an error or exception upon receiving an activity from the skill consumer.

<!--TODO Need a link for more information about claims and claims-based validation.-->

### [C#](#tab/cs)

The SDK provides an `AllowedCallersClaimsValidator` class that adds application-level authorization based on a simple list of IDs of the applications that are allowed to call the skill. If the list contains an asterisk (*), then all callers are allowed. The claims validator is configured in **Startup.cs**.

### [JavaScript](#tab/javascript)

The SDK provides an `allowedCallersClaimsValidator` class that adds application-level authorization based on a simple list of IDs of the applications that are allowed to call the skill. If the list contains an asterisk (*), then all callers are allowed. The claims validator is configured in **index.js**.

### [Java](#tab/java)

The SDK provides an `AllowedCallersClaimsValidator` class that adds application-level authorization based on a simple list of IDs of the applications that are allowed to call the skill. If the list contains an asterisk (*), then all callers are allowed. The claims validator is configured in **Application.java**.

### [Python](#tab/python)

Define a claims validation method that throws an error to reject an incoming request.

**echo-skill-bot/authentication/allowed_callers_claims_validator.py**

[!code-python[Claims validator](~/../botbuilder-samples/samples/python/80.skills-simple-bot-to-bot/echo-skill-bot/authentication/allowed_callers_claims_validator.py?range=8-44&highlight=28-33)]

---

## Skill adapter

When an error occurs, the skill's adapter should clear conversation state for the skill, and it should also send an `endOfConversation` activity to the skill consumer. To signal that the skill ended due to an error, use the _code_ property of the activity.

### [C#](#tab/cs)

**EchoSkillBot\SkillAdapterWithErrorHandler.cs**

[!code-csharp[HandleTurnError, SendErrorMessageAsync, and SendEoCToParentAsync](~/../botbuilder-samples/samples/csharp_dotnetcore/80.skills-simple-bot-to-bot/EchoSkillBot/SkillAdapterWithErrorHandler.cs?range=26-74)]

### [JavaScript](#tab/javascript)

**echo-skill-bot/index.js**

[!code-javascript[adapter.onTurnError](~/../botbuilder-samples/samples/javascript_nodejs/80.skills-simple-bot-to-bot/echo-skill-bot/index.js?range=79-121)]

### [Java](#tab/java)

**echoSkillBot\SkillAdapterWithErrorHandler.java**

[!code-csharp[Error handler](~/../botbuilder-samples/samples/java_springboot/80.skills-simple-bot-to-bot/DialogSkillBot/src/main/java/com/microsoft/bot/sample/echoskillbot/SkillAdapterWithErrorHandler.java?range=20-81)]

### [Python](#tab/python)

**echo-skill-bot/adapter_with_error_handler.py**

[!code-python[Error handler](~/../botbuilder-samples/samples/python/80.skills-simple-bot-to-bot/echo-skill-bot/adapter_with_error_handler.py?range=23-77&highlight=43-49)]

---

## Service registration

The _Bot Framework adapter_ uses an _authentication configuration_ object (set when the adapter is created) to validate the authentication header on incoming requests.

This sample adds claims validation to the authentication configuration and uses the _skill adapter with error handler_ described in the previous section.

### [C#](#tab/cs)

**EchoSkillBot\Startup.cs**

[!code-csharp[Configure authentication configuration and adapter](~/../botbuilder-samples/samples/csharp_dotnetcore/80.skills-simple-bot-to-bot/EchoSkillBot/Startup.cs?range=25-58)]

### [JavaScript](#tab/javascript)

**echo-skill-bot/index.js**

[!code-javascript[allowedCallers, claimsValidators, authConfig, credentialsFactory, bot auth, and adapter](~/../botbuilder-samples/samples/javascript_nodejs/80.skills-simple-bot-to-bot/echo-skill-bot/index.js?range=43-77)]

### [Java](#tab/java)

**echoSkillBot\Application.java**

[!code-java[Configure authentication configuration and adapter](~/../botbuilder-samples/samples/java_springboot/80.skills-simple-bot-to-bot/DialogSkillBot/src/main/java/com/microsoft/bot/sample/echoskillbot/Application.java?range=61-68)]

### [Python](#tab/python)

**echo-skill-bot/app.py**

[!code-python[configuration](~/../botbuilder-samples/samples/python/80.skills-simple-bot-to-bot/echo-skill-bot/app.py?range=19-30)]

---

## Skill manifest

A _skill manifest_ is a JSON file that describes the activities the skill can perform, its input and output parameters, and the skill's endpoints.
The manifest contains the information you need to access the skill from another bot.
The latest schema version is [v2.1](https://schemas.botframework.com/schemas/skills/v2.1/skill-manifest.json).

### [C#](#tab/cs)

**EchoSkillBot\wwwroot\manifest\echoskillbot-manifest-1.0.json**

[!code-json[Manifest](~/../botbuilder-samples/samples/csharp_dotnetcore/80.skills-simple-bot-to-bot/EchoSkillBot/wwwroot/manifest/echoskillbot-manifest-1.0.json)]

### [JavaScript](#tab/javascript)

**echo-skill-bot/manifest/echoskillbot-manifest-1.0.json**

[!code-json[Manifest](~/../botbuilder-samples/samples/javascript_nodejs/80.skills-simple-bot-to-bot/echo-skill-bot/manifest/echoskillbot-manifest-1.0.json)]

### [Java](#tab/java)

**DialogSkillBot\webapp\manifest\echoskillbot-manifest-1.0.json**

[!code-json[Manifest](~/../botbuilder-samples/samples/java_springboot/80.skills-simple-bot-to-bot/DialogSkillBot/src/main/webapp/manifest/echoskillbot-manifest-1.0.json)]

### [Python](#tab/python)

**echo_skill_bot/wwwroot/manifest/echoskillbot-manifest-1.0.json**

[!code-json[Manifest](~/../botbuilder-samples/samples/python/80.skills-simple-bot-to-bot/echo-skill-bot/wwwroot/manifest/echoskillbot-manifest-1.0.json)]

---

The _skill manifest schema_ is a JSON file that describes the schema of the skill manifest. The current schema version is [2.1.0](https://schemas.botframework.com/schemas/skills/v2.1/skill-manifest.json).

## Test the skill

At this point, you can test the skill in the Emulator as if it were a normal bot. However, to test it as a skill, you would need to [implement a skill consumer](skill-implement-consumer.md).

Download and install the latest [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md)

1. Run the echo skill bot locally on your machine. If you need instructions, refer to the `README` file for the [C#](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/80.skills-simple-bot-to-bot), [JavaScript](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/80.skills-simple-bot-to-bot), [Java](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/java_springboot/80.skills-simple-bot-to-bot), or [Python](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/80.skills-simple-bot-to-bot) sample.
1. Use the Emulator to test the bot. When you send an "end" or "stop" message to the skill, it sends an `endOfConversation` activity in addition to the reply message. The skill sends the `endOfConversation` activity to indicate the skill is finished.

:::image type="content" source="media/skills-simple-skill-test.png" alt-text="Example transcript showing the end-of-conversation activity.":::

### More about debugging

[!INCLUDE [skills-about-debugging](../includes/skills-about-debugging.md)]

## Next steps

> [!div class="nextstepaction"]
> [Implement a skill for Copilot Studio](skill-pva.md)

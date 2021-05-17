---
title: Add trace activities to your bot - Bot Service
description: Describes what the trace activity is in the Bot Framework SDK, and how to use it.
keywords: trace, activity, bot, Bot Framework SDK
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 04/02/2021
monikerRange: 'azure-bot-service-4.0'
---

# Add trace activities to your bot

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

<!-- What is it and why use it -->

A _trace activity_ is an activity that your bot can send to the Bot Framework Emulator.
You can use trace activities to interactively debug a bot, as they allow you to view information about your bot while it runs locally.

<!-- Details -->

Trace activities are sent only to the Emulator and not to any other client or channel.
The Emulator displays them in the log but not the main chat panel.

- Trace activities sent via the turn context are sent through the _send activity handlers_ registered on the turn context.
- Trace activities sent via the turn context are associated with the inbound activity, by applying the conversation reference, if there was one.
  For a proactive message, the _reply to ID_ will be a new GUID.
- Regardless of how it is sent, a trace activity never sets the _responded_ flag.

## To use a trace activity

In order to see a trace activity in the Emulator, you need a scenario in which your bot will send a trace activity, such as throwing an exception and sending a trace activity from the adapter's on turn error handler.

To send a trace activity from your bot:

1. Create a new activity.
   - Set its _type_ property to "trace". This is required.
   - Optionally set its _name_, _label_, _value_, and _value type_ properties, as appropriate for the trace.
1. Use the _turn context_ object's _send activity_ method to send the trace activity.
   - This method adds values for the remaining required properties of the activity, based on the incoming activity.
     These include the _channel ID_, _service URL_, _from_, and _recipient_ properties.

To view a trace activity in the Emulator:

1. Run the bot locally on your machine.
1. Test it using the Emulator.
   - Interact with the bot and use the steps in your scenario to generate the trace activity.
   - When your bot emits the trace activity, the trace activity is displayed in the Emulator log.

Here is a trace activity you might see if you ran the Core bot without first setting up the QnAMaker knowledge base that the bot relies upon.

![Screen shot of the Emulator](./media/using-trace-activities.png)

## Add a trace activity to the adapter's on-error handler

The adapter's _on turn error_ handler catches any otherwise uncaught exception thrown from the bot during a turn.
This is a good place for a trace activity, as you can send a user-friendly message to the user and send debugging information about the exception to the Emulator.

This example code is from the **Core Bot** sample. See the complete sample in [**C#**](https://aka.ms/cs-core-sample) or [**JavaScript**](https://aka.ms/js-core-sample) or [**Python**](https://aka.ms/py-core-sample). For Java the code below is from the SDK code [**Java**](https://aka.ms/botbuilder-java).

# [C#](#tab/csharp)

The adapter's **OnTurnError** handler creates the trace activity to include the exception information and sends it to the Emulator.

**AdapterWithErrorHandler.cs**

[!code-csharp[OnTurnError](~/../BotBuilder-Samples/samples/csharp_dotnetcore/13.core-bot/AdapterWithErrorHandler.cs?range=20-54&highlight=33-34)]

# [JavaScript](#tab/javascript)

The adapter's **onTurnError** handler creates the trace activity to include the exception information and sends it to the Emulator.

**index.js**

[!code-javascript[onTurnError](~/../BotBuilder-Samples/samples/javascript_nodejs/13.core-bot/index.js?range=36-59&highlight=9-15)]

# [Java](#tab/Java)

The adapter's **onTurnError** handler creates the trace activity to include the exception information and sends it to the Emulator. In the Java SDK an AdapterWithErrorHandler is included as part of the SDK in the com.microsoft.bot.integration package. The source code for the default adapter is shown below. You can  develop your own custom AdapterWithErrorHandler to provide any extended or additional functionality that might be desired.

**AdapterWithErrorHandler.cs**

```java
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

package com.microsoft.bot.integration;

import com.microsoft.bot.builder.ConversationState;

import java.util.concurrent.CompletableFuture;

import com.microsoft.bot.builder.MessageFactory;
import com.microsoft.bot.builder.TurnContext;
import com.microsoft.bot.connector.Channels;
import com.microsoft.bot.schema.Activity;
import com.microsoft.bot.schema.ActivityTypes;
import org.apache.commons.lang3.StringUtils;
import org.apache.commons.lang3.exception.ExceptionUtils;
import org.slf4j.LoggerFactory;

/**
 * An Adapter that provides exception handling.
 */
public class AdapterWithErrorHandler extends BotFrameworkHttpAdapter {
    private static final String ERROR_MSG_ONE = "The bot encountered an error or bug.";
    private static final String ERROR_MSG_TWO =
        "To continue to run this bot, please fix the bot source code.";

    /**
     * Constructs an error handling BotFrameworkHttpAdapter by providing an
     * {@link com.microsoft.bot.builder.OnTurnErrorHandler}.
     *
     * <p>
     * For this sample, a simple message is displayed. For a production Bot, a more
     * informative message or action is likely preferred.
     * </p>
     *
     * @param withConfiguration The Configuration object to use.
     */
    public AdapterWithErrorHandler(Configuration withConfiguration) {
        super(withConfiguration);

        setOnTurnError((turnContext, exception) -> {
            LoggerFactory.getLogger(AdapterWithErrorHandler.class).error("onTurnError", exception);

            return turnContext.sendActivities(
                MessageFactory.text(ERROR_MSG_ONE), MessageFactory.text(ERROR_MSG_TWO)
            ).thenCompose(resourceResponse -> sendTraceActivity(turnContext, exception));
        });
    }

    /**
     * Constructs an error handling BotFrameworkHttpAdapter by providing an
     * {@link com.microsoft.bot.builder.OnTurnErrorHandler}.
     *
     * <p>
     * For this sample, a simple message is displayed. For a production Bot, a more
     * informative message or action is likely preferred.
     * </p>
     *
     * @param withConfiguration     The Configuration object to use.
     * @param withConversationState For ConversationState.
     */
    public AdapterWithErrorHandler(
        Configuration withConfiguration,
        ConversationState withConversationState
    ) {
        super(withConfiguration);

        setOnTurnError((turnContext, exception) -> {
            LoggerFactory.getLogger(AdapterWithErrorHandler.class).error("onTurnError", exception);

            return turnContext.sendActivities(
                MessageFactory.text(ERROR_MSG_ONE), MessageFactory.text(ERROR_MSG_TWO)
            ).thenCompose(resourceResponse -> sendTraceActivity(turnContext, exception))
                .thenCompose(stageResult -> {
                    if (withConversationState != null) {
                        // Delete the conversationState for the current conversation to prevent the
                        // bot from getting stuck in a error-loop caused by being in a bad state.
                        // ConversationState should be thought of as similar to "cookie-state" in a
                        // Web pages.
                        return withConversationState.delete(turnContext)
                            .exceptionally(deleteException -> {
                                LoggerFactory.getLogger(AdapterWithErrorHandler.class)
                                    .error("ConversationState.delete", deleteException);
                                return null;
                            });
                    }
                    return CompletableFuture.completedFuture(null);
                });
        });
    }

    private CompletableFuture<Void> sendTraceActivity(
        TurnContext turnContext,
        Throwable exception
    ) {
        if (StringUtils.equals(turnContext.getActivity().getChannelId(), Channels.EMULATOR)) {
            Activity traceActivity = new Activity(ActivityTypes.TRACE);
            traceActivity.setLabel("TurnError");
            traceActivity.setName("OnTurnError Trace");
            traceActivity.setValue(ExceptionUtils.getStackTrace(exception));
            traceActivity.setValueType("https://www.botframework.com/schemas/error");

            return turnContext.sendActivity(traceActivity).thenApply(resourceResponse -> null);
        }

        return CompletableFuture.completedFuture(null);
    }
}

```
# [Python](#tab/python)

The adapter's **on_error** handler creates the trace activity to include the exception information and sends it to the Emulator.

**adapter_with_error_handler.py**

[!code-python[on_error](~/../BotBuilder-Samples/samples/python/13.core-bot/adapter_with_error_handler.py?range=26-50&highlight=24-25)]

---

## Additional resources

- How to [Debug a bot with inspection middleware](../bot-service-debug-inspection-middleware.md) describes how to add middleware that emits trace activities.
- For debugging a deployed bot, you can use Application Insights. For more information, see [Add telemetry to your bot](bot-builder-telemetry.md).
- For detailed information about each activity type, see the [Bot Framework Activity schema](https://aka.ms/botSpecs-activitySchema).

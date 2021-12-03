---
title: Send media attachments with the Bot Framework SDK
description: Learn how to add images, video, audio, files, and other media attachments to messages sent using the Bot Framework SDK. 
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 12/02/2021
ms.custom: abs-meta-21q1
monikerRange: 'azure-bot-service-4.0'
---

# Send media attachments with Bot Framework SDK

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

Messages exchanged between user and bot can contain media attachments, such as images, video, audio, and files. The Bot Framework SDK supports the task of sending rich messages to the user. To determine the type of rich messages a channel (Facebook, Slack, etc.) supports, consult the channel's documentation for information about limitations.

## Prerequisites

- Knowledge of [bot basics](bot-builder-basics.md).
- The code in this article is based on the following samples:
  - **Using cards**: [C#](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/06.using-cards), [JavaScript](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/06.using-cards), [Java](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/06.using-cards), [Python](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/06.using-cards)
  - **Handling attachments**: [C#](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/15.handling-attachments), [JavaScript](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/15.handling-attachments), [Java](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/15.handling-attachments), [Python](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/15.handling-attachments)
  - **Suggested actions**: [C#](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/08.suggested-actions), [JavaScript](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/08.suggested-actions), [Java](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/08.suggested-actions), [Python](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/08.suggested-actions)

## Send attachments

To send the user content like an image or a video, you can add an attachment or list of attachments to a message.

See [Design the user experience](../bot-service-design-user-experience.md#cards) for examples of available cards.

See also [What is the size limit of a file transferred using channels?](../bot-service-resources-faq-general.md#what-is-the-size-limit-of-a-file-transferred-using-channels) in the FAQ.

### [C#](#tab/csharp)

All of the source code shown in this section is based on the [Handling attachments](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/15.handling-attachments) sample.

The `Attachments` property of the `Activity` object contains an array of `Attachment` objects that represent the media attachments and rich cards attached to the message. To add a media attachment to a message, create an `Attachment` object for the `reply` activity and set the `ContentType`, `ContentUrl`, and `Name` properties.

To create the reply message, define the text and then set up the attachments. Assigning the attachments to the reply is the same for each attachment type, however the various attachments are set up and defined differently, as seen in the following snippets. The code below is setting up the reply for an inline attachment:

**Bots/AttachmentsBot.cs**

[!code-csharp[reply inline](~/../botbuilder-samples/samples/csharp_dotnetcore/15.handling-attachments/Bots/AttachmentsBot.cs?range=105-106)]

Next, we look at the types of attachments. First is an inline attachment:

**Bots/AttachmentsBot.cs**

[!code-csharp[inline attachment](~/../botbuilder-samples/samples/csharp_dotnetcore/15.handling-attachments/Bots/AttachmentsBot.cs?range=167-178)]

Then, an uploaded attachment:

**Bots/AttachmentsBot.cs**

[!code-csharp[uploaded attachment](~/../botbuilder-samples/samples/csharp_dotnetcore/15.handling-attachments/Bots/AttachmentsBot.cs?range=181-215)]

Lastly, an internet attachment:

**Bots/AttachmentsBot.cs**

[!code-csharp[online attachment](~/../botbuilder-samples/samples/csharp_dotnetcore/15.handling-attachments/Bots/AttachmentsBot.cs?range=218-227)]

### [JavaScript](#tab/javascript)

The source code shown here is based on the [Handling attachments](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/15.handling-attachments) sample.

To use attachments, include the following libraries in your bot:

**bots/attachmentsBot.js**

[!code-javascript[attachments libraries](~/../botbuilder-samples/samples/javascript_nodejs/15.handling-attachments/bots/attachmentsBot.js?range=4)]

To create the reply message, define the text and then set up the attachments. Assigning the attachments to the reply is the same for each attachment type, however the various attachments are set up and defined differently, as seen in the following snippets. The code below is setting up the reply for an inline attachment:

**bots/attachmentsBot.js**

[!code-javascript[attachments](~/../botbuilder-samples/samples/javascript_nodejs/15.handling-attachments/bots/attachmentsBot.js?range=119,128-129)]

To send the user a single piece of content like an image or a video, you can send media in a few different ways. First, as an inline attachment:

**bots/attachmentsBot.js**

[!code-javascript[inline attachments](~/../botbuilder-samples/samples/javascript_nodejs/15.handling-attachments/bots/attachmentsBot.js?range=170-179)]

Then, an uploaded attachment:

**bots/attachmentsBot.js**

[!code-javascript[uploaded attachments](~/../botbuilder-samples/samples/javascript_nodejs/15.handling-attachments/bots/attachmentsBot.js?range=197-215)]

Lastly, an internet attachment contained in a URL:

**bots/attachmentsBot.js**

[!code-javascript[internet attachments](~/../botbuilder-samples/samples/javascript_nodejs/15.handling-attachments/bots/attachmentsBot.js?range=184-191)]

### [Java](#tab/java)

The source code shown in this section is based on the [Handling attachments](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/15.handling-attachments) sample.

The `getAttachments()` method of the `Activity` object contains an array of `Attachment` objects that represent the media attachments and rich cards attached to the message. To add a media attachment to a message, create an `Attachment` object for the `reply` activity and set the `ContentType`, `ContentUrl`, and `Name` properties.

To create the reply message, define the text and then set up the attachments. Assigning the attachments to the reply is the same for each attachment type, however the various attachments are set up and defined differently, as seen in the following snippets. The code below is setting up the reply for an inline attachment:

**AttachmentsBot.java**

[!code-java[reply inline](~/../botbuilder-samples/samples/java_springboot/15.handling-attachments/src/main/java/com/microsoft/bot/sample/attachments/AttachmentsBot.java?range=122-127)]

Next, we look at the types of attachments. First is an inline attachment:

**AttachmentsBot.java**

[!code-java[inline attachment](~/../botbuilder-samples/samples/java_springboot/15.handling-attachments/src/main/java/com/microsoft/bot/sample/attachments/AttachmentsBot.java?range=192-206)]

Then, an uploaded attachment:

**AttachmentsBot.java**

[!code-java[uploaded attachment](~/../botbuilder-samples/samples/java_springboot/15.handling-attachments/src/main/java/com/microsoft/bot/sample/attachments/AttachmentsBot.java?range=209-240)]

Lastly, an internet attachment:

**AttachmentsBot.java**

[!code-java[online attachment](~/../botbuilder-samples/samples/java_springboot/15.handling-attachments/src/main/java/com/microsoft/bot/sample/attachments/AttachmentsBot.java?range=242-250)]

### [Python](#tab/python)

The source code shown here is based on the [Handling attachments](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/15.handling-attachments) sample.

To create the reply message, define the text and then set up the attachments. Assigning the attachments to the reply is the same for each attachment type, however the various attachments are set up and defined differently, as seen in the following snippets.

The code below is setting up the reply for an inline attachment:

**bots/attachments_bot.py**

[!code-python[attachments](~/../botbuilder-samples/samples/python/15.handling-attachments/bots/attachments_bot.py?range=112-113)]

To send the user a single piece of content like an image or a video, you can send media in a few different ways. First, as an inline attachment:

**bots/attachments_bot.py**

[!code-python[inline attachments](~/../botbuilder-samples/samples/python/15.handling-attachments/bots/attachments_bot.py?range=153-170)]

Then, an uploaded attachment:

**bots/attachments_bot.py**

[!code-python[upload attachments](~/../botbuilder-samples/samples/python/15.handling-attachments/bots/attachments_bot.py?range=172-207)]

Lastly, an internet attachment contained in a URL:

**bots/attachments_bot.py**

[!code-python[internet attachments](~/../botbuilder-samples/samples/python/15.handling-attachments/bots/attachments_bot.py?range=209-218)]

---

If an attachment is an image, audio, or video, the Connector service will communicate attachment data to the channel in a way that enables the [channel](bot-builder-channeldata.md) to render that attachment within the conversation. If the attachment is a file, the file URL will be rendered as a hyperlink within the conversation.

## Send a hero card

Besides simple image or video attachments, you can attach a _hero card_, which allows you to combine images and buttons in one object, and send them to the user. Markdown is supported for most text fields, but support may vary by channel.

### [C#](#tab/csharp)

To compose a message with a hero card and button, you can attach a `HeroCard` object to a message.

The source code shown here is based on the [Handling attachments](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/15.handling-attachments) sample.

**Bots/AttachmentsBot.cs**

[!code-csharp[Hero card](~/../botbuilder-samples/samples/csharp_dotnetcore/15.handling-attachments/Bots/AttachmentsBot.cs?range=39-58)]

### [JavaScript](#tab/javascript)

To compose a message with a hero card and button, you can attach a `HeroCard` object to a message.

The source code shown here is based on the [Handling attachments](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/15.handling-attachments) sample.

**bots/attachmentsBot.js**

[!code-javascript[hero card](~/../botbuilder-samples/samples/javascript_nodejs/15.handling-attachments/bots/attachmentsBot.js?range=147-165)]

### [Java](#tab/java)

To compose a message with a hero card and button, you can attach a `HeroCard` object to a message.

The source code shown here is based on the [Handling attachments](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/15.handling-attachments) sample.

**AttachmentsBot.java**

[!code-java[Hero card](~/../botbuilder-samples/samples/java_springboot/15.handling-attachments/src/main/java/com/microsoft/bot/sample/attachments/AttachmentsBot.java?range=67-83)]

### [Python](#tab/python)

To compose a message with a hero card and button, you can attach a `HeroCard` object to a message.

The source code shown here is based on the [Handling attachments](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/15.handling-attachments) sample.

**bots/attachments_bot.py**

[!code-python[hero card](~/../botbuilder-samples/samples/python/15.handling-attachments/bots/attachments_bot.py?range=125-148)]

---

## Process events within rich cards

To process events within rich cards, use _card action_ objects to specify what should happen when the user selects a button or taps a section of the card. Each card action has a _type_ and _value_ property.

[!INCLUDE [Table of card action types](../includes/snippet-card-action-types.md)]

## Hero card using various event types

The following code shows examples using various rich card events.

### [C#](#tab/csharp)

For examples of all the available cards, see the [Using cards](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/06.using-cards) sample.

**Cards.cs**

[!code-csharp[GetHeroCard](~/../botbuilder-samples/samples/csharp_dotnetcore/06.using-cards/Cards.cs?range=28-41)]

**Cards.cs**

[!code-csharp[GetSigninCard](~/../botbuilder-samples/samples/csharp_dotnetcore/06.using-cards/Cards.cs?range=92-101)]

### [JavaScript](#tab/javascript)

For examples of all the available cards, see the [Using cards](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/06.using-cards) sample.

**dialogs/mainDialog.js**

[!code-javascript[createHeroCard](~/../botbuilder-samples/samples/javascript_nodejs/06.using-cards/dialogs/mainDialog.js?range=214-226)]

**dialogs/mainDialog.js**

[!code-javascript[createOAuthCard](~/../botbuilder-samples/samples/javascript_nodejs/06.using-cards/dialogs/mainDialog.js?range=228-234)]

### [Java](#tab/java)

For examples of all the available cards, see the [Using cards](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/06.using-cards) sample.

**Cards.java**

[!code-java[GetHeroCard](~/../botbuilder-samples/samples/java_springboot/06.using-cards/src/main/java/com/microsoft/bot/sample/usingcards/Cards.java?range=48-58)]

**Cards.java**

[!code-java[GetSigninCard](~/../botbuilder-samples/samples/java_springboot/06.using-cards/src/main/java/com/microsoft/bot/sample/usingcards/Cards.java?range=98-103)]


### [Python](#tab/python)

For examples of all the available cards, see the [Using cards](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/06.using-cards) sample.

**dialogs/main_dialog.py**

[!code-python[create_hero_card](~/../botbuilder-samples/samples/python/06.using-cards/dialogs/main_dialog.py?range=167-183)]

**dialogs/main_dialog.py**

[!code-python[create_oauth_card](~/../botbuilder-samples/samples/python/06.using-cards/dialogs/main_dialog.py?range=185-197)]

---

## Send an Adaptive Card

While you can use the _message factory_ to create a message that contains an attachment (of any sort), an _Adaptive Card_ is one specific type of attachment. Note that some channels don't support Adaptive Cards, and channels that do may only partially support them. For example, if you send an Adaptive Card in Facebook, the buttons won't work while texts and images work well. The message factory is a Bot Framework SDK helper class used to automate creation steps for you.

Adaptive Cards are an open card exchange format enabling developers to exchange UI content in a common and consistent way. However, not all channels support Adaptive Cards.

The [Adaptive Cards Designer](http://adaptivecards.io/designer/) provides a rich, interactive design-time experience for authoring adaptive cards.

> [!NOTE]
> You should test this feature with the channels your bot will use to determine whether those channels support adaptive cards.

### [C#](#tab/csharp)

To use Adaptive Cards, be sure to add the `AdaptiveCards` NuGet package.

The source code shown here is based on the [Using cards](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/06.using-cards) sample.

**Cards.cs**

This example reads the Adaptive Card JSON from a file and adds it as an attachment.

[!code-csharp[CreateAdaptiveCardAttachment](~/../botbuilder-samples/samples/csharp_dotnetcore/06.using-cards/Cards.cs?range=13-26&highlight=9-10)]

### [JavaScript](#tab/javascript)

To use Adaptive Cards, be sure to add the `adaptivecards` npm package.

The source code shown here is based on the [Using cards](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/06.using-cards) sample.

**dialogs/mainDialog.js**

This example reads the Adaptive Card JSON from a file and creates a message activity with the card attached.

[!code-javascript[import JSON file](~/../botbuilder-samples/samples/javascript_nodejs/06.using-cards/dialogs/mainDialog.js?range=6)]

[!code-javascript[createAdaptiveCard](~/../botbuilder-samples/samples/javascript_nodejs/06.using-cards/dialogs/mainDialog.js?range=178-180)]

### [Java](#tab/java)

The source code shown here is based on the [Using cards](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/06.using-cards) sample.

**Cards.java**

This example reads the Adaptive Card JSON from a file and adds it as an attachment.

[!code-java[CreateAdaptiveCardAttachment](~/../botbuilder-samples/samples/java_springboot/06.using-cards/src/main/java/com/microsoft/bot/sample/usingcards/Cards.java?range=30-46&highlight9-10)]

### [Python](#tab/python)

The source code shown here is based on the [Using cards](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/06.using-cards) sample.

**bots/main_dialog.py**

This example reads the Adaptive Card JSON from a file and creates a message activity with the card attached.

[!code-python[import JSON file](~/../botbuilder-samples/samples/python/06.using-cards/dialogs/main_dialog.py?range=33)]

[!code-python[create_adaptive_card](~/../botbuilder-samples/samples/python/06.using-cards/dialogs/main_dialog.py?range=131-132)]

---

## Send a carousel of cards

Messages can also include multiple attachments in a carousel layout, which places the attachments side by side and allows the user to scroll across.

### [C#](#tab/csharp)

The source code shown here is based on the [Using cards](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/06.using-cards) sample.

**Dialogs/MainDialog.cs**

First, create the reply and define the attachments as a list.

[!code-csharp[ShowCardStepAsync excerpt](~/../botbuilder-samples/samples/csharp_dotnetcore/06.using-cards/Dialogs/MainDialog.cs?range=61-66)]

Then add the attachments and set the layout type to _carousel_.
Here we're adding them one at a time, but feel free to manipulate the list to add the cards however you prefer.

[!code-csharp[ShowCardStepAsync excerpt](~/../botbuilder-samples/samples/csharp_dotnetcore/06.using-cards/Dialogs/MainDialog.cs?range=108-118)]

Once the attachments are added, you can send the reply just like any other.

[!code-csharp[ShowCardStepAsync excerpt](~/../botbuilder-samples/samples/csharp_dotnetcore/06.using-cards/Dialogs/MainDialog.cs?range=122-123)]

### [JavaScript](#tab/javascript)

The source code shown here is based on the [Using cards](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/06.using-cards) sample.

**dialogs/mainDialog.js**

Add the attachments and set the layout type to _carousel_.
Once the attachments are added, you can send the reply just like any other.

[!code-javascript[showCardStep excerpt](~/../botbuilder-samples/samples/javascript_nodejs/06.using-cards/dialogs/mainDialog.js?range=100-113)]

### [Java](#tab/java)

The source code shown here is based on the [Using cards](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/06.using-cards) sample.

**MainDialog.java**

First, create the reply and define the attachments as a list.

[!code-java[ShowCardStep excerpt](~/../botbuilder-samples/samples/java_springboot/06.using-cards/src/main/java/com/microsoft/bot/sample/usingcards/MainDialog.java?range=63-68)]

Then add the attachments and set the layout type to _carousel_.
Here we're adding them one at a time, but feel free to manipulate the list to add the cards however you prefer.

[!code-java[ShowCardStep excerpt](~/../botbuilder-samples/samples/java_springboot/06.using-cards/src/main/java/com/microsoft/bot/sample/usingcards/MainDialog.java?range=109-119)]

Once the attachments are added, you can send the reply just like any other.

[!code-java[ShowCardStep excerpt](~/../botbuilder-samples/samples/java_springboot/06.using-cards/src/main/java/com/microsoft/bot/sample/usingcards/MainDialog.java?range=123-124)]

### [Python](#tab/python)

The source code shown here is based on [Using cards](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/06.using-cards) sample.

**dialogs/main_dialog.py**

First, create the reply and define the attachments as a list.

[!code-python[show_card_step excerpt](~/../botbuilder-samples/samples/python/06.using-cards/dialogs/main_dialog.py?range=85)]

Then add the attachments and set the layout type to _carousel_.
Here we're adding them one at a time, but feel free to manipulate the list to add the cards however you prefer.

[!code-python[hero cards](~/../botbuilder-samples/samples/python/06.using-cards/dialogs/main_dialog.py?range=107-116)]

Once the attachments are added, you can send the reply just like any other.

[!code-python[hero cards](~/../botbuilder-samples/samples/python/06.using-cards/dialogs/main_dialog.py?range=118-119)]

---

<!-- TODO: Add a media card, such as video or audion. Revisit which examples we put here and link to the 06 through 08 samples. -->

### Code sample for processing Adaptive Card input

The following sample shows one way to use Adaptive Card inputs within a bot dialog class.
It extends the hero cards sample by validating the input received in the text field from the responding client.
You first need to add the text input and button functionality to the existing adaptive card by adding the following code just before the final bracket of **adaptiveCard.json**, located in the resources folder:

```json
"actions": [
  {
    "type": "Action.ShowCard",
    "title": "Text",
    "card": {
      "type": "AdaptiveCard",
      "body": [
        {
          "type": "Input.Text",
          "id": "text",
          "isMultiline": true,
          "placeholder": "Enter your comment"
        }
      ],
      "actions": [
        {
          "type": "Action.Submit",
          "title": "OK"
        }
      ]
    }
  }
]
```

Note that the ID of the text input field is set to `"text"`. When the user selects **OK**, the message the Adaptive Card generates will have a _value_ property that has a property named `"text"` which contains the information the user entered in the text input field of the card.

### [C#](#tab/csharp)

Our validator uses **Newtonsoft.json** to first convert this to a `JObject`,
and then create a trimmed text string for comparison. So add:

```csharp
using System;
using System.Linq;
using Newtonsoft.Json.Linq;
```

<!---------------------------------------------------------------------------------------->
<!-- This module has completely changed, this section needs to be completely re-written -->
<!---------------------------------------------------------------------------------------->

to **MainDialog.cs** and install the latest stable NuGet package of **Newtonsoft.Json**.
In the validator code we added the logic flow into the code comments.
This `ChoiceValidator` method is placed into the **Using cards** sample just after the closed brace public for declaration of MainDialog:

```csharp
private async Task ChoiceValidator(
    PromptValidatorContext promptContext,
    CancellationToken cancellationToken)
{
    // Retrieves Adaptive Card comment text as JObject.
    // looks for JObject field "text" and converts that input into a trimmed text string.
    var jobject = promptContext.Context.Activity.Value as JObject;
    var jtoken = jobject?["text"];
    var text = jtoken?.Value().Trim();

    // Logic: 1. if succeeded = true, just return promptContext
    //        2. if false, see if JObject contained Adaptive Card input.
    //               No = (bad input) return promptContext
    //               Yes = update Value field with JObject text string, return "true".
    if (!promptContext.Recognized.Succeeded && text != null)
    {
        var choice = promptContext.Options.Choices.FirstOrDefault(
        c => c.Value.Equals(text, StringComparison.InvariantCultureIgnoreCase));
        if (choice != null)
        {
            promptContext.Recognized.Value = new FoundChoice
            {
                Value = choice.Value,
            };
            return true;
        }
    }
    return promptContext.Recognized.Succeeded;
}
```

Now above in the `MainDialog` declaration change:

```csharp
// Define the main dialog and its related components.
AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
```

to:

```csharp
// Define the main dialog and its related components.
AddDialog(new ChoicePrompt(nameof(ChoicePrompt), ChoiceValidator));
```

This will invoke your validator to look for Adaptive Card input each time a new choice prompt is created.

### [JavaScript](#tab/javascript)

Open **mainDialog.js** and find the run method `async run(turnContext, accessor)`
This method handles incoming activity.
Just after the call `dialogSet.add(this);` add the following:

```JavaScript
// The following check looks for a non-existent text input
// plus Adaptive Card input in _activity.value.text
// If both conditions exist, the Activity Card text
// is copied into the text input field.
if(turnContext._activity.text == null
    && turnContext._activity.value.text != null) {
    this.logger.log('replacing null text with Activity Card text input');
    turnContext._activity.text = turnContext._activity.value.text;
  }
```

If this check finds a non-existent text input from the client, it looks to see if there is input from an Adaptive Card.
If an Adaptive Card input exists at `_activity.value.text`, it copies this into the normal text input field.

### [Java](#tab/java)

Our validator uses the **Serialization** helper from com.microsoft.bot.schema to first convert this to a `JsonNode`,
and then create a trimmed text string for comparison. We will also need a few other imports to complete this, so add:

```java
import com.fasterxml.jackson.databind.JsonNode;
import com.microsoft.bot.dialogs.prompts.PromptValidator;
import com.microsoft.bot.schema.Serialization;
import java.util.Optional;
import org.apache.commons.lang3.StringUtils;
```

<!---------------------------------------------------------------------------------------->
<!-- This module has completely changed, this section needs to be completely re-written -->
<!---------------------------------------------------------------------------------------->

to **MainDialog.java**.
In the validator code we added the logic flow into the code comments.
This `PromptValidator` expression is placed into the **Using cards** sample just after the closed brace public for declaration of MainDialog:

```java
PromptValidator<FoundChoice> validator = (promptContext) -> {
    // Retrieves Adaptive Card comment text as JObject.
    // looks for JObject field "text" and converts that input into a trimmed text
    // string.
    JsonNode jsonNode = Serialization.getAs(promptContext.getContext().getActivity().getValue(), JsonNode.class);
    JsonNode textNode = jsonNode != null ? jsonNode.get("text") : null;
    String text = textNode != null ? textNode.textValue() : "";

    // Logic: 1. if succeeded = true, just return promptContext
    // 2. if false, see if JObject contained Adaptive Card input.
    // No = (bad input) return promptContext
    // Yes = update Value field with JObject text string, return "true".
    if (!promptContext.getRecognized().getSucceeded() && text != null) {
        Optional<Choice> choice = promptContext.getOptions()
            .getChoices()
            .stream()
            .filter(c -> StringUtils.compareIgnoreCase(c.getValue(), text) == 0)
            .findFirst();

        if (choice.isPresent()) {
            promptContext.getRecognized().setValue(new FoundChoice() {
                {
                    setValue(choice.get().getValue());
                }
            });
            return CompletableFuture.completedFuture(true);
        }
    }
    return CompletableFuture.completedFuture(promptContext.getRecognized().getSucceeded());
};
```

Now above in the `MainDialog` declaration change:

```java
// Define the main dialog and its related components.
addDialog(new ChoicePrompt("ChoicePrompt"));
```

to:

```java
// Define the main dialog and its related components.
addDialog(new ChoicePrompt("ChoicePrompt", validator, null));
```

This will invoke your validator to look for Adaptive Card input each time a new choice prompt is created.

### [Python](#tab/python)

<!--The source code shown here is based on the [Suggested actions](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/08.suggested-actions) sample.-->

Create and send an activity with suggested actions to the user.

This `choice_validator` method is placed into the **Using cards** sample just after the closed brace public for declaration of `MainDialog`:

```python
@staticmethod
async def choice_validator(prompt_context: PromptValidatorContext) -> bool:
    if prompt_context.context.activity.value:
        text = prompt_context.context.activity.value["text"].lower()
        if not prompt_context.recognized.succeeded and text:
            matching_choices = [choice for choice in prompt_context.options.choices if choice.value.lower() == text]
            if matching_choices:
                choice = matching_choices[0]
                prompt_context.recognized.value = FoundChoice(
                    value=choice.value,
                    index=0,
                    score=1.0
                )
                return True

    return prompt_context.recognized.succeeded
```

Now above in the `MainDialog` declaration change:

```python
self.add_dialog(ChoicePrompt(CARD_PROMPT))
```

to:

```python
self.add_dialog(ChoicePrompt(CARD_PROMPT, MainDialog.choice_validator))
```

This will invoke your validator to look for Adaptive Card input each time a new choice prompt is created.

---

To test your code, once an Adaptive Card has been displayed, select the **Text** button, Enter a valid selection such as _Hero Card_ then select the **OK** button.

![Test Adaptive Card](media/adaptive-card-input.png)

1. The first input will be used to start a new dialog.
2. Select the **OK** button again and this input will be used to select a new card.

## Next steps

> [!div class="nextstepaction"]
> [Add buttons to guide user action](./bot-builder-howto-add-suggested-actions.md)

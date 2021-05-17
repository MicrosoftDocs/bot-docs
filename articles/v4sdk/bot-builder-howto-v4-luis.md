---
title: Add natural language understanding to your bot - Bot Service
description: Learn how to use LUIS for natural language understanding with the Bot Framework SDK.
keywords: Language Understanding, LUIS, intent, recognizer, entities, middleware
author: JonathanFingold
ms.author: kamrani
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.date: 06/16/2020
monikerRange: 'azure-bot-service-4.0'
---

# Add natural language understanding to your bot

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

The ability to understand what your user means conversationally and contextually can be a difficult task, but can provide your bot a more natural conversation feel. _Language Understanding (LUIS)_ is a cloud-based API service that enables you to do just that so that your bot can recognize the intent of user messages, allow for more natural language from your user, and better direct the conversation flow.

This topic walks you through adding LUIS to a flight booking application to recognize different intents and entities contained within user input.

## Prerequisites

- A [LUIS](https://www.luis.ai) account.
- A copy of the **Core Bot** sample in [**C#**](https://aka.ms/cs-core-sample), [**JavaScript**](https://aka.ms/js-core-sample), [**Java**](https://aka.ms/java-core-sample), or [**Python**](https://aka.ms/python-core-sample).
- Knowledge of [bot basics](bot-builder-basics.md), [natural language processing](https://docs.microsoft.com/azure/cognitive-services/luis/what-is-luis), and [managing bot resources](bot-file-basics.md).

## About this sample

This core bot sample shows an example of an airport flight booking application. It uses a LUIS service to recognize the user input and return the top recognized LUIS intent.

The language model contains three intents: `Book Flight`, `Cancel`, and `None`. LUIS will use these intents to understand what the user meant when they send a message to the bot. The language model also defines entities that LUIS can extract from the user's input, such as the origin or destination airport.

# [C#](#tab/csharp)

After each processing of user input, `DialogBot` saves the current state of both `UserState` and `ConversationState`. Once all the required information has been gathered the coding sample creates a demo flight booking reservation. In this article we'll be covering the LUIS aspects of this sample. However, the general flow of the sample is shown below:

- `OnMembersAddedAsync` is called when a new user is connected and displays a welcome card.
- `OnMessageActivityAsync` is called for each user input received.

![LUIS sample logic flow](./media/how-to-luis/luis-logic-flow.png)

The `OnMessageActivityAsync` module runs the appropriate dialog through the `Run` dialog extension method. Then the main dialog calls the LUIS helper to find the the top scoring user intent. If the top intent for the user input returns "BookFlight", the helper fills out information from the user that LUIS returned. After that, the main dialog starts the `BookingDialog`, which acquires additional information as needed from the user such as:

- `Origin` the originating city
- `TravelDate` the date to book the flight
- `Destination` the destination city

# [JavaScript](#tab/javascript)

After each processing of user input, `dialogBot` saves the current state of both `userState` and `conversationState`. Once all the required information has been gathered the coding sample creates a demo flight booking reservation. In this article we'll be covering the LUIS aspects of this sample. However, the general flow of the sample is shown below:

- `onMembersAdded` is called when a new user is connected and displays a welcome card.
- `OnMessage` is called for each user input received.

![LUIS sample javascript logic flow](./media/how-to-luis/luis-logic-flow-js.png)

The `onMessage` module runs the `mainDialog` which gathers user input.
Then the main dialog calls the LUIS helper `FlightBookingRecognizer` to find the top scoring user intent. If the top intent for the user input returns "BookFlight", the helper fills out information from the user that LUIS returned.
Upon the response back, `mainDialog` preserves information for the user returned by LUIS and starts `bookingDialog`. `bookingDialog` acquires additional information as needed from the user such as

- `destination` the destination city.
- `origin` the originating city.
- `travelDate` the date to book the flight.

# [Java](#tab/java)

1. After each processing of user input, `DialogBot` saves the current state of both `UserState` and `ConversationState`.
2. Once all the required information has been gathered the coding sample creates a demo flight booking reservation.
3. In this article we'll be covering the LUIS aspects of this sample. However, the general flow of the sample is shown below:

- `onMembersAdded` is called when a new user is connected and displays a welcome card.
- `onMessageActivity` is called for each user input received.

![LUIS sample logic flow](./media/how-to-luis/luis-logic-flow-java.png)

The `onMessageActivity` module runs the appropriate dialog through the `run` dialog extension method. Then the main dialog calls the LUIS helper to find the the top scoring user intent. If the top intent for the user input returns "BookFlight", the helper fills out information from the user that LUIS returned. After that, the main dialog starts the `BookingDialog`, which acquires additional information as needed from the user such as:

- `Origin` the originating city
- `TravelDate` the date to book the flight
- `Destination` the destination city


# [Python](#tab/python)

After each processing of user input, `DialogBot` saves the current state of both `user_state` and `conversation_state`. Once all the required information has been gathered the coding sample creates a demo flight booking reservation. In this article we'll be covering the LUIS aspects of this sample. However, the general flow of the sample is shown below:

- `on_members_added_activity` is called when a new user is connected and displays a welcome card.
- `on_message_activity` is called for each user input received.

![LUIS sample Python logic flow](./media/how-to-luis/luis-logic-flow-python.png)

The `on_message_activity` module runs the appropriate dialog through the `run_dialog` dialog extension method. Then the main dialog calls `LuisHelper` to find the the top scoring user intent. If the top intent for the user input returns "BookFlight", the helper function fills out information from the user that LUIS returned. After that, the main dialog starts the `BookingDialog`, which acquires additional information as needed from the user such as:

- `destination` the destination city.
- `origin` the originating city.
- `travel_date` the date to book the flight.

---

This article covers how to add LUIS to a bot. For information about using dialogs or state, see how to [gather user input using a dialog prompt](bot-builder-prompts.md) or [save user and conversation data](bot-builder-howto-v4-state.md), respectively.

## Create a LUIS app in the LUIS portal

1. Sign in to the [LUIS portal](https://www.luis.ai).
    - If you don't already have an account, create one.
    - If you don't already have an authoring resource, create one.
    - For more information, see the LUIS documentation on how to [sign in to the LUIS portal](/azure/cognitive-services/luis/luis-how-to-start-new-app#sign-in-to-luis-portal).
1. On the **My Apps** page, click **New app for conversation** and select **Import as JSON**.
1. In the **Import new app** dialog:
    1. Choose the **FlightBooking.json** file in the **CognitiveModels** folder of the sample.
    1. Enter `FlightBooking` as the optional name of the app, and click **Done**.
1. You may be prompted to upgrade your composite entities. You can ignore this and click **Remind me later**:

    ![ignore-composite-entities](./media/how-to-luis/luis-upgrade-composite-entities.png)

1. Train and publish your app.
    For more information, see the LUIS documentation on how to [train](/azure/cognitive-services/LUIS/luis-how-to-train) and [publish](/azure/cognitive-services/LUIS/publishapp) an app to the production environment.

### Why use entities

LUIS entities allow your bot to intelligently understand certain things or events that are different than the standard intents. This enables you to gather extra information from the user, which lets your bot respond more intelligently or possibly skip certain questions where it asks the user for that information. Along with definitions for the three LUIS intents 'Book Flight', 'Cancel', and 'None' the FlightBooking.json file also contains a set of entities such as 'From.Airport' and 'To.Airport'. These entities allow LUIS to detect and return additional information contained within the user's original input when they request a new travel booking.

## Obtain values to connect to your LUIS app

Once your LUIS app is published, you can access it from your bot. You will need to record several values to access your LUIS app from within your bot. You can retrieve that information using the LUIS portal.

### Retrieve application information from the LUIS.ai portal

The settings file (`appsettings.json`, `.env` or `config.py`) acts as the place to bring all service references together in one place. The information you retrieve will be added to this file in the next section.

1. Select your published LUIS app from [luis.ai](https://www.luis.ai).
1. With your published LUIS app open, select the **MANAGE** tab.
1. Select the **Settings** tab on the left side and record the value shown for _Application ID_ as \<YOUR_APP_ID>.
    > [!div class="mx-imgBorder"]
    > ![Manage LUIS app - Application Information](./media/how-to-luis/manage-luis-app-app-info.png)
1. Select the **Azure Resources** tab on the left side and select the **Authoring Resource** group.
    Record the value shown for _Location_ as \<YOUR_REGION> and _Primary Key_ as \<YOUR_AUTHORING_KEY>.
    > [!div class="mx-imgBorder"]
    > ![Manage LUIS app - Authoring Information](./media/how-to-luis/manage-luis-app-azure-resources.png)

### Update the settings file

# [C#](#tab/csharp)

Add the information required to access your LUIS app including application id, authoring key, and region into the `appsettings.json` file. These are the values you saved previously from your published LUIS app. Note that the API host name should be in the format `<your region>.api.cognitive.microsoft.com`.

**appsetting.json**

[!code-json[appsettings](~/../BotBuilder-Samples/samples/csharp_dotnetcore/13.core-bot/appsettings.json?range=1-7)]

# [JavaScript](#tab/javascript)

Add the information required to access your LUIS app including application id, authoring key, and region into the `.env` file. These are the values you saved previously from your published LUIS app. Note that the API host name should be in the format `<your region>.api.cognitive.microsoft.com`.

**.env**

[!code[env](~/../BotBuilder-Samples/samples/javascript_nodejs/13.core-bot/.env?range=1-5)]

# [Java](#tab/java)

Add the information required to access your LUIS app including application id, authoring key, and region into the `application.properties` file. These are the values you saved previously from your published LUIS app. Note that the API host name should be in the format `<your region>.api.cognitive.microsoft.com`.

**application.properties**

[!code-java[appsettings](~/../BotBuilder-Samples/samples/java_springboot/13.core-bot/src/main/resources/application.properties?range=1-6)]


# [Python](#tab/python)

Add the information required to access your LUIS app including application id, authoring key, and region into the `config.py` file. These are the values you saved previously from your published LUIS app. Note that the API host name should be in the format `<your region>.api.cognitive.microsoft.com`.

**config.py**

[!code-python[config.py](~/../botbuilder-samples/samples/python/13.core-bot/config.py?range=14-19)]

---

## Configure your bot to use your LUIS app

# [C#](#tab/csharp)

Be sure that the **Microsoft.Bot.Builder.AI.Luis** NuGet package is installed for your project.

To connect to the LUIS service, the bot pulls the information you added above from the appsetting.json file. The `FlightBookingRecognizer` class contains code with your settings from the appsetting.json file and queries the LUIS service by calling `RecognizeAsync` method.

**FlightBookingRecognizer.cs**

[!code-csharp[luisHelper](~/../BotBuilder-Samples/samples/csharp_dotnetcore/13.core-bot/FlightBookingRecognizer.cs?range=12-48)]

The `FlightBookingEx.cs` contains the logic to extract *From*, *To* and *TravelDate*; it extends the partial class `FlightBooking.cs` used to store LUIS results when calling `FlightBookingRecognizer.RecognizeAsync<FlightBooking>` from the `MainDialog.cs`.

**CognitiveModels\FlightBookingEx.cs**

[!code-csharp[luis helper](~/../BotBuilder-Samples/samples/csharp_dotnetcore/13.core-bot/CognitiveModels/FlightBookingEx.cs?range=8-35)]

# [JavaScript](#tab/javascript)

To use LUIS, your project needs to install the **botbuilder-ai** npm package.

To connect to the LUIS service, the bot uses the information you added above from the `.env` file. The `flightBookingRecognizer.js` class contains the code that imports your settings from the `.env` file and queries the LUIS service by calling `recognize()` method.

**dialogs/flightBookingRecognizer.js**

[!code-javascript[luis helper](~/../BotBuilder-Samples/samples/javascript_nodejs/13.core-bot/dialogs/flightBookingRecognizer.js?range=6-70)]

The logic to extract From, To and TravelDate is implemented as helper methods inside `flightBookingRecognizer.js`. These methods are used after calling `flightBookingRecognizer.executeLuisQuery()` from `mainDialog.js`

# [Java](#tab/java)

Be sure that the **com.microsoft.bot.bot-ai-luis-v3** package is added to your pom.xml file.

```xml
<dependency>
    <groupId>com.microsoft.bot</groupId>
    <artifactId>bot-ai-luis-v3</artifactId>
    <version>4.13.0</version>
</dependency>
```

To connect to the LUIS service, the bot pulls the information you added above from the application.properties file. The `FlightBookingRecognizer` class contains code with your settings from the application.properties file and queries the LUIS service by calling `recognize` method.

**FlightBookingRecognizer.java**

[!code-java[luisHelper](~/../BotBuilder-Samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/FlightBookingRecognizer.java?range=27-50)]

[!code-java[luisHelper](~/../BotBuilder-Samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/FlightBookingRecognizer.java?range=143-152)]

The `FlightBookingRecognizer.cs` contains the logic to extract *From*, *To* and *TravelDate*; and is called from from the `MainDialog.java` to decode the results of the Luis query result.

**FlightBookingRecognizer.java**

[!code-csharp[luis helper](~/../BotBuilder-Samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/FlightBookingRecognizer.java?range=72-141)]


# [Python](#tab/python)

Be sure that the **botbuilder-ai** PyPI package is installed for your project.

To connect to the LUIS service, the bot uses the information you added above from the `config.py` file. The `FlightBookingRecognizer` class contains the code that imports your settings from the `config.py` file and queries the LUIS service by calling `recognize()` method.

**flight_booking_recognizer.py**

[!code-python[config.py](~/../botbuilder-samples/samples/python/13.core-bot/flight_booking_recognizer.py?range=10-36&highlight=26)]

The logic to extract *From*, *To* and *travel_date* is implemented as helper methods from the `LuisHelper` class inside `luis_helper.py`. These methods are used after calling `LuisHelper.execute_luis_query()` from `main_dialog.py`

**helpers/luis_helper.py**

[!code-python[luis helper](~/../botbuilder-samples/samples/python/13.core-bot/helpers/luis_helper.py?range=30-102)]

---

LUIS is now configured and connected for your bot.

## Test the bot

Download and install the latest [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme)

1. Run the sample locally on your machine. If you need instructions, refer to the readme file for the [C# Sample](https://aka.ms/cs-core-sample), [JS Sample](https://aka.ms/js-core-sample) or [Python Sample](https://aka.ms/python-core-sample).

1. In the Emulator, type a message such as "travel to paris" or "going from paris to berlin". Use any utterance found in the file FlightBooking.json for training the intent "Book flight".

![LUIS booking input](./media/how-to-luis/luis-user-travel-input.png)

If the top intent returned from LUIS resolves to "Book flight" your bot will ask additional questions until it has enough information stored to create a travel booking. At that point it will return this booking information back to your user.

![LUIS booking result](./media/how-to-luis/luis-travel-result.png)

At this point the code bot logic will reset and you can continue to create additional bookings.

## Additional information

For more about LUIS, see the LUIS documentation:

- [What is Language Understanding (LUIS)?](/azure/cognitive-services/LUIS/what-is-luis)
- [Create a new LUIS app in the LUIS portal](/azure/cognitive-services/LUIS/luis-how-to-start-new-app)
- [Design with intent and entity models](/azure/cognitive-services/LUIS/luis-concept-model)
- [Migrate to V3 Authoring APIS](https://docs.microsoft.com/azure/cognitive-services/luis/luis-migration-authoring-entities)
- [Migrate to V3 Prediction APIs](https://docs.microsoft.com/azure/cognitive-services/luis/luis-migration-api-v3)

## Next steps

> [!div class="nextstepaction"]
> [Use QnA Maker to answer questions](./bot-builder-howto-qna.md)

---
title: Add natural language understanding to your bot
description: Learn how to use LUIS for natural language understanding in your bot.
keywords: Language Understanding, LUIS, intent, recognizer, entities, middleware
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.service: bot-service
ms.topic: how-to
ms.date: 12/07/2022
monikerRange: 'azure-bot-service-4.0'
---

# Add natural language understanding to your bot

[!INCLUDE [applies-to-v4](../includes/applies-to-v4-current.md)]

[!INCLUDE [luis-sunset-alert](../includes/luis-sunset-alert.md)]

The ability to understand what your user means conversationally and contextually can be a difficult task, but can provide your bot a more natural conversation feel. _Language Understanding (LUIS)_ is a cloud-based API service that enables you to do just that so that your bot can recognize the intent of user messages, allow for more natural language from your user, and better direct the conversation flow.

This topic walks you through adding LUIS to a flight booking application to recognize different intents and entities contained within user input.

[!INCLUDE [java-python-sunset-alert](../includes/java-python-sunset-alert.md)]

## Prerequisites

- A [LUIS](https://www.luis.ai) account.
- A copy of the **Core Bot** sample in [**C#**](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/13.core-bot), [**JavaScript**](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/13.core-bot), [**Java**](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/java_springboot/13.core-bot), or [**Python**](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/13.core-bot).
- Knowledge of [bot basics](bot-builder-basics.md) and [natural language processing](/azure/cognitive-services/luis/what-is-luis).

## About this sample

This core bot sample shows an example of an airport flight booking application. It uses a LUIS service to recognize the user input and return the top recognized LUIS intent.

The language model contains three intents: `Book Flight`, `Cancel`, and `None`. LUIS will use these intents to understand what the user meant when they send a message to the bot. The language model also defines entities that LUIS can extract from the user's input, such as the origin or destination airport.

# [C#](#tab/csharp)

After each processing of user input, `DialogBot` saves the current state of both `UserState` and `ConversationState`. Once all the required information has been gathered, the coding sample creates a demo flight booking reservation. In this article, we'll be covering the LUIS aspects of this sample. However, the general flow of the sample is:

- `OnMembersAddedAsync` is called when a new user is connected and displays a welcome card.
- `OnMessageActivityAsync` is called for each user input received.

:::image type="content" source="./media/how-to-luis/luis-logic-flow.png" alt-text="Class diagram outlining the structure of the C# sample.":::

The `OnMessageActivityAsync` module runs the appropriate dialog through the `Run` dialog extension method. Then the main dialog calls the LUIS helper to find the top scoring user intent. If the top intent for the user input returns "BookFlight", the helper fills out information from the user that LUIS returned. After that, the main dialog starts the `BookingDialog`, which acquires additional information as needed from the user such as:

- `Origin` the originating city
- `TravelDate` the date to book the flight
- `Destination` the destination city

# [JavaScript](#tab/javascript)

After each processing of user input, `dialogBot` saves the current state of both `userState` and `conversationState`. Once all the required information has been gathered, the coding sample creates a demo flight booking reservation. In this article, we'll be covering the LUIS aspects of this sample. However, the general flow of the sample is:

- `onMembersAdded` is called when a new user is connected and displays a welcome card.
- `OnMessage` is called for each user input received.

:::image type="content" source="./media/how-to-luis/luis-logic-flow-js.png" alt-text="Class diagram outlining the structure of the JavaScript sample.":::

The `onMessage` module runs the `mainDialog`, which gathers user input.
Then the main dialog calls the LUIS helper `FlightBookingRecognizer` to find the top scoring user intent. If the top intent for the user input returns "BookFlight", the helper fills out information from the user that LUIS returned.
Upon the response back, `mainDialog` preserves information for the user returned by LUIS and starts `bookingDialog`. `bookingDialog` acquires additional information as needed from the user such as

- `destination` the destination city.
- `origin` the originating city.
- `travelDate` the date to book the flight.

# [Java](#tab/java)

1. After each processing of user input, `DialogBot` saves the current state of both `UserState` and `ConversationState`.
1. Once all the required information has been gathered, the coding sample creates a demo flight booking reservation.
1. In this article, we'll be covering the LUIS aspects of this sample. However, the general flow of the sample is:

- `onMembersAdded` is called when a new user is connected and displays a welcome card.
- `onMessageActivity` is called for each user input received.

:::image type="content" source="./media/how-to-luis/luis-logic-flow-java.png" alt-text="Class diagram outlining the structure of the Java sample.":::

The `onMessageActivity` module runs the appropriate dialog through the `run` dialog extension method. Then the main dialog calls the LUIS helper to find the top scoring user intent. If the top intent for the user input returns "BookFlight", the helper fills out information from the user that LUIS returned. After that, the main dialog starts the `BookingDialog`, which acquires additional information as needed from the user such as:

- `Origin` the originating city
- `TravelDate` the date to book the flight
- `Destination` the destination city

# [Python](#tab/python)

After each processing of user input, `DialogBot` saves the current state of both `user_state` and `conversation_state`. Once all the required information has been gathered, the coding sample creates a demo flight booking reservation. In this article, we'll be covering the LUIS aspects of this sample. However, the general flow of the sample is:

- `on_members_added_activity` is called when a new user is connected and displays a welcome card.
- `on_message_activity` is called for each user input received.

:::image type="content" source="./media/how-to-luis/luis-logic-flow-python.png" alt-text="Class diagram outlining the structure of the Python sample.":::

The `on_message_activity` module runs the appropriate dialog through the `run_dialog` dialog extension method. Then the main dialog calls `LuisHelper` to find the top scoring user intent. If the top intent for the user input returns "BookFlight", the helper function fills out information from the user that LUIS returned. After that, the main dialog starts the `BookingDialog`, which acquires additional information as needed from the user such as:

- `destination` the destination city.
- `origin` the originating city.
- `travel_date` the date to book the flight.

---

This article covers how to add LUIS to a bot. For information about using dialogs or state, see how to [gather user input using a dialog prompt](bot-builder-prompts.md) or [save user and conversation data](bot-builder-howto-v4-state.md), respectively.

## Create a LUIS app in the LUIS portal

1. [Sign in to the LUIS portal][sign-in-luis-portal] and if needed [create an account][create-account] and [authoring resource][create-authoring-resource].
1. On the **Conversation apps** page in [LUIS][conversation-apps], select **Import**, then **Import as JSON**.
1. In the **Import new app** dialog:
    1. Choose the **FlightBooking.json** file in the **CognitiveModels** folder of the sample.
    1. Enter `FlightBooking` as the optional name of the app, and select **Done**.
1. The site may display **How to create an effective LUIS app** and **Upgrade your composite entities** dialogs. You can dismiss these dialogs and continue.
1. Train your app, then publish your app to the _production_ environment.
    For more information, see the LUIS documentation on how to [train](/azure/cognitive-services/LUIS/luis-how-to-train) and [publish](/azure/cognitive-services/LUIS/publishapp) an app.

### Why use entities

LUIS entities enable your bot to understand events beyond standard intents. This enables you to gather from users additional information, so your bot can ask questions and respond more intelligently. Along with definitions for the three LUIS intents 'Book Flight', 'Cancel', and 'None', the FlightBooking.json file also contains a set of entities such as 'From.Airport' and 'To.Airport'. These entities allow LUIS to detect and return additional information contained within the user's original input when they request a new travel booking.

## Obtain values to connect to your LUIS app

Once your LUIS app is published, you can access it from your bot. You'll need to record several values to access your LUIS app from within your bot. You can retrieve that information using the LUIS portal.

### Retrieve application information from the LUIS.ai portal

The settings file (`appsettings.json`, `.env` or `config.py`) acts as the place to bring all service references together in one place. The information you retrieve will be added to this file in the next section.

1. Select your published LUIS app from [luis.ai](https://www.luis.ai).
1. With your published LUIS app open, select the **MANAGE** tab.
1. Select the **Settings** tab on the left side and record the value shown for _Application ID_ as \<YOUR_APP_ID>.

     :::image type="content" source="./media/how-to-luis/manage-luis-app-app-info.png" alt-text="Screenshot of the Manage page displaying your application ID." lightbox="./media/how-to-luis/manage-luis-app-app-info.png":::

1. Select **Azure Resources**, then **Prediction Resource**. Record the value shown for _Location_ as \<YOUR_REGION> and _Primary Key_ as \<YOUR_AUTHORING_KEY>.

     :::image type="content" source="./media/how-to-luis/manage-luis-app-azure-resources.png" alt-text="Screenshot of the Manage page displaying your location and primary key." lightbox="./media/how-to-luis/manage-luis-app-azure-resources.png":::

     Alternatively, you can use the region and primary key for your authoring resource.

### Update the settings file

# [C#](#tab/csharp)

Add the information required to access your LUIS app including application ID, authoring key, and region into the `appsettings.json` file. In the previous step, you retrieved these values from your published LUIS app. The API host name should be in the format `<your region>.api.cognitive.microsoft.com`.

**appsetting.json**

[!code-json[appsettings](~/../BotBuilder-Samples/samples/csharp_dotnetcore/13.core-bot/appsettings.json)]

# [JavaScript](#tab/javascript)

Add the information required to access your LUIS app including application ID, authoring key, and region into the `.env` file. In the previous step, you retrieved these values from your published LUIS app. The API host name should be in the format `<your region>.api.cognitive.microsoft.com`.

**.env**

[!code-ini[.env file](~/../BotBuilder-Samples/samples/javascript_nodejs/13.core-bot/.env)]

# [Java](#tab/java)

Add the information required to access your LUIS app including application ID, authoring key, and region into the `application.properties` file. In the previous step, you retrieved these values from your published LUIS app. The API host name should be in the format `<your region>.api.cognitive.microsoft.com`.

**application.properties**

[!code-ini[appsettings](~/../BotBuilder-Samples/samples/java_springboot/13.core-bot/src/main/resources/application.properties)]

# [Python](#tab/python)

Add the information required to access your LUIS app including application ID, authoring key, and region into the `config.py` file. In the previous step, you retrieved these values from your published LUIS app. The API host name should be in the format `<your region>.api.cognitive.microsoft.com`.

**config.py**

[!code-python[config.py](~/../botbuilder-samples/samples/python/13.core-bot/config.py?range=14-19)]

---

## Configure your bot to use your LUIS app

# [C#](#tab/csharp)

Be sure that the **Microsoft.Bot.Builder.AI.Luis** NuGet package is installed for your project.

To connect to the LUIS service, the bot pulls the information you added to the appsetting.json file. The `FlightBookingRecognizer` class contains code with your settings from the appsetting.json file and queries the LUIS service by calling `RecognizeAsync` method.

**FlightBookingRecognizer.cs**

[!code-csharp[luisHelper](~/../BotBuilder-Samples/samples/csharp_dotnetcore/13.core-bot/FlightBookingRecognizer.cs?range=12-48)]

The `FlightBookingEx.cs` contains the logic to extract _From_, _To_ and _TravelDate_; it extends the partial class `FlightBooking.cs` used to store LUIS results when calling `FlightBookingRecognizer.RecognizeAsync<FlightBooking>` from the `MainDialog.cs`.

**CognitiveModels\FlightBookingEx.cs**

[!code-csharp[LUIS helper](~/../BotBuilder-Samples/samples/csharp_dotnetcore/13.core-bot/CognitiveModels/FlightBookingEx.cs?range=8-35)]

# [JavaScript](#tab/javascript)

To use LUIS, your project needs to install the **botbuilder-ai** npm package.

To connect to the LUIS service, the bot uses the information you added to the `.env` file. The `flightBookingRecognizer.js` class contains the code that imports your settings from the `.env` file and queries the LUIS service by calling `recognize()` method.

**dialogs/flightBookingRecognizer.js**

[!code-javascript[LUIS helper](~/../BotBuilder-Samples/samples/javascript_nodejs/13.core-bot/dialogs/flightBookingRecognizer.js?range=6-70)]

The logic to extract From, To and TravelDate is implemented as helper methods inside `flightBookingRecognizer.js`. These methods are used after calling `flightBookingRecognizer.executeLuisQuery()` from `mainDialog.js`

# [Java](#tab/java)

Be sure that the **com.microsoft.bot.bot-ai-luis-v3** package is added to your pom.xml file.

:::code language="xml" source="~/../BotBuilder-Samples/samples/java_springboot/13.core-bot/pom.xml" range="109-113":::

To connect to the LUIS service, the bot pulls the information you added to the application.properties file. The `FlightBookingRecognizer` class contains code with your settings from the application.properties file and queries the LUIS service by calling `recognize` method.

**FlightBookingRecognizer.java**

[!code-java[luisHelper](~/../BotBuilder-Samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/FlightBookingRecognizer.java?range=27-50)]

[!code-java[luisHelper](~/../BotBuilder-Samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/FlightBookingRecognizer.java?range=142-151)]

The `FlightBookingRecognizer.cs` contains the logic to extract _From_, _To_ and _TravelDate_; and is called from the `MainDialog.java` to decode the results of the Luis query result.

**FlightBookingRecognizer.java**

[!code-csharp[LUIS helper](~/../BotBuilder-Samples/samples/java_springboot/13.core-bot/src/main/java/com/microsoft/bot/sample/core/FlightBookingRecognizer.java?range=71-140)]

# [Python](#tab/python)

Be sure that the **botbuilder-ai** PyPI package is installed for your project.

To connect to the LUIS service, the bot uses the information you added to the `config.py` file. The `FlightBookingRecognizer` class contains the code that imports your settings from the `config.py` file and queries the LUIS service by calling `recognize()` method.

**flight_booking_recognizer.py**

[!code-python[config.py](~/../botbuilder-samples/samples/python/13.core-bot/flight_booking_recognizer.py?range=10-36&highlight=26)]

The logic to extract _From_, _To_ and _travel_date_ is implemented as helper methods from the `LuisHelper` class inside `luis_helper.py`. These methods are used after calling `LuisHelper.execute_luis_query()` from `main_dialog.py`

**helpers/luis_helper.py**

[!code-python[LUIS helper](~/../botbuilder-samples/samples/python/13.core-bot/helpers/luis_helper.py?range=30-102)]

---

LUIS is now configured and connected for your bot.

## Test the bot

Download and install the latest [Bot Framework Emulator](https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md)

1. Run the sample locally on your machine. If you need instructions, refer to the `README` file for the [C# Sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/csharp_dotnetcore/13.core-bot), [JS Sample](https://github.com/Microsoft/BotBuilder-Samples/tree/master/samples/javascript_nodejs/13.core-bot) or [Python Sample](https://github.com/microsoft/BotBuilder-Samples/tree/master/samples/python/13.core-bot).

1. In the Emulator, type a message such as "travel to paris" or "going from paris to berlin". Use any utterance found in the file FlightBooking.json for training the intent "Book flight".

If the top intent returned from LUIS resolves to "Book flight", your bot will ask more questions until it has enough information stored to create a travel booking. At that point it will return this booking information back to your user.

At this point, the code bot logic will reset and you can continue to create more bookings.

## Additional information

For more about LUIS, see the LUIS documentation:

- [What is Language Understanding (LUIS)?](/azure/cognitive-services/LUIS/what-is-luis)
- [Create a new LUIS app in the LUIS portal](/azure/cognitive-services/LUIS/luis-how-to-start-new-app)
- [Design with intent and entity models](/azure/cognitive-services/LUIS/luis-concept-model)
- [Migrate to V3 Authoring APIS](/azure/cognitive-services/luis/luis-migration-authoring-entities)
- [Migrate to V3 Prediction APIs](/azure/cognitive-services/luis/luis-migration-api-v3)

> [!TIP]
> Different parts of the SDK define separate _entity_ classes or elements.
> For message entities, see [Entities and activity types](../bot-service-activities-entities.md).

[sign-in-luis-portal]: /azure/cognitive-services/luis/sign-in-luis-portal
[create-account]: https://azure.microsoft.com/services/cognitive-services/
[conversation-apps]: https://www.luis.ai/applications
[create-authoring-resource]: /azure/cognitive-services/luis/luis-how-to-azure-subscription

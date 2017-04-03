---
title: Support localization | Microsoft Docs
description: Learn how to recognize the user's locale in a conversational application (bot).
keywords: bot framework, bot, localization, locale, globalization
author: DeniseMak
manager: rstand
ms.topic: article
ms.prod: bot-framework

ms.date: 02/24/2017
ms.reviewer: rstand
#ROBOTS: Index
---

# Support localization


Bot Builder includes a rich localization system for building bots that can communicate with the user in multiple languages. All of your bot's prompts can be localized using JSON files stored in your bots directory structure. If you’re using a system like LUIS to perform natural language processing you can configure your [LuisRecognizer](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.luisrecognizer) with a separate model for each language your bot supports and the SDK automatically selects the model that matches the user's preferred locale.

## Determine the locale by prompting the user
The first step to localizing your bot for the user is adding the ability to identify the users preferred language. The SDK provides a [session.preferredLocale()](https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#preferredlocale) method to both save and retrieve this preference on a per-user basis. The following example is a dialog to prompt the user for their preferred language and then save their choice.

``` javascript
bot.dialog('/localePicker', [
    function (session) {
        // Prompt the user to select their preferred locale
        builder.Prompts.choice(session, "What's your preferred language?", 'English|Español|Italiano');
    },
    function (session, results) {
        // Update preferred locale
        var locale;
        switch (results.response.entity) {
            case 'English':
                locale = 'en';
            case 'Español':
                locale = 'es';
            case 'Italiano':
                locale = 'it';
                break;
        }
        session.preferredLocale(locale, function (err) {
            if (!err) {
                // Locale files loaded
                session.endDialog("Your preferred language is now %s.", results.response.entity);
            } else {
                // Problem loading the selected locale
                session.error(err);
            }
        });
    }
]);
```

## Determine the locale by using analytics
Another way to determine the user's locale is to install a piece of middleware that uses a service like the [Text Analytics API](https://www.microsoft.com/cognitive-services/en-us/text-analytics-api) to automatically 
detect the users language based upon the text of the message they sent.

``` javascript
var request = require('request');

bot.use({
    receive: function (event, next) {
        if (event.text && !event.textLocale) {
            var options = {
                method: 'POST',
                url: 'https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/languages?numberOfLanguagesToDetect=1',
                body: { documents: [{ id: 'message', text: event.text }]},
                json: true,
                headers: {
                    'Ocp-Apim-Subscription-Key': '<YOUR API KEY>'
                }
            };
            request(options, function (error, response, body) {
                if (!error && body) {
                    if (body.documents && body.documents.length > 0) {
                        var languages = body.documents[0].detectedLanguages;
                        if (languages && languages.length > 0) {
                            event.textLocale = languages[0].iso6391Name;
                        }
                    }
                }
                next();
            });
        } else {
            next();
        }
    }
});
```


## Additional resources

To learn about how to localize a recognizer, see [Recognizing intent](~/nodejs/recognize-intent.md).

For more information on LUIS see [Understanding Natural Language][LUISConcepts]. 

[LUIS]: https://www.luis.ai/
[IMessage]: http://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.imessage
[IntentRecognizerSetOptions]: https://docs.botframework.com/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iintentrecognizersetoptions.html
[LuisRecognizer]: https://docs.botframework.com/en-us/node/builder/chat-reference/classes/_botbuilder_d_.luisrecognizer
[LUISSample]: https://github.com/Microsoft/BotBuilder/blob/master/Node/examples/basics-naturalLanguage/app.js
[LUISConcepts]: https://docs.botframework.com/en-us/node/builder/guides/understanding-natural-language/
[DisambiguationSample]: https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/feature-onDisambiguateRoute

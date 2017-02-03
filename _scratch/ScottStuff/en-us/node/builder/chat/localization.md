---
layout: page
title: Localization
permalink: /en-us/node/builder/chat/localization/
weight: 1135
parent1: Building your Bot Using the Bot Builder for Node.js
parent2: Chat Bots
---


Bot Builder includes a rich localization system for building bots that can communicate with the user in multiple languages. You can use JSON files stored in your bot's directory structure to provide localized prompts (see [Localizing prompts](#Localizing%20prompts)). If you’re using a system like [LUIS](https://luis.ai) to perform natural language processing, you can configure your [LuisRecognizer](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.luisrecognizer) with a separate model for each language that your bot supports and the SDK will automatically select the model matching the user's preferred locale.

<span style="color:red"><< I must have missed this, but do node bots only converse with users using prompts? What about localizing cards? What about Activity.text? >></span>

### Determining locale

The first step of localizing your bot is adding the ability to identify the user's preferred language. The SDK provides a [session.preferredLocale()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#preferredlocale) method to both save and retrieve the preferred language on a per user basis. The following example shows how you could use a dialog to prompt the user for their preferred language and then persist the user's choice.

{% highlight JavaScript %}
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
{% endhighlight %}

<span style="color:red"><< I thought the channel can pass the user's locale in the Activity.Locale field? >></span>


Another option is to install a piece of middleware that uses a service like Microsoft's [Text Analytics API](https://www.microsoft.com/cognitive-services/en-us/text-analytics-api) to automatically detect the user's language based upon the message's text.

{% highlight JavaScript %}
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
{% endhighlight %}

Calling `session.preferredLocale()` will automatically return the detected language if a user-selected locale hasn’t been assigned. The search order for `preferredLocale()` is:

* Locale saved by calling `session.preferredLocale()`. This value is stored in `session.userData['BotBuilder.Data.PreferredLocale']`
* Detected locale assigned to `session.message.textLocale`
* Bot's configured [default locale](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.iuniversalbotsettings#localizersettings)
* English ('en')

<span style="color:red"><< Regarding bullet #3, is this only if they publish the bot to the directory? >></span>

You can configure the bot's default locale during construction.

{% highlight JavaScript %}
var bot = new builder.UniversalBot(connector, {
    localizerSettings: { 
        defaultLocale: "es" 
    }
});
{% endhighlight %}

### Localizing prompts

The default localization system for Bot Builder is file based and lets a bot support multiple languages using JSON files stored on disk.  By default, the localization system will search for the bots prompts in the `./locale/<IETF TAG>/index.json` file, where `<IETF TAG>` is a valid [IETF language tag](https://en.wikipedia.org/wiki/IETF_language_tag) representing the preferred locale to use for prompts. The following shows a screenshot of the directory structure for a bot that supports three languages: English, Italian, and Spanish.   

 ![Locale Directory Structure](/en-us/images/builder/locale-dir.png)

The structure of the file is straight forward. It’s a simple JSON map of message IDs and localized text strings. If the value is an `array` instead of a `string`, the SDK randomly chooses a prompt from the array anytime that value is retrieved using [session.localizer.gettext()](/en-us/node/builder/chat-reference/interfaces/_botbuilder_d_.ilocalizer#gettext). Returning the localized version of a message generally happens automatically by simply passing the message ID in a call to [session.send()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.session#send) instead of language-specific text.

{% highlight JavaScript %}
bot.dialog("/", [
    function (session) {
        session.send("greeting");
        session.send("instructions");
        session.beginDialog('/localePicker');
    },
    function (session) {
        builder.Prompts.text(session, "text_prompt");
    },
{% endhighlight %}

Internally, the SDK will call `session.preferredLocale()` to get the user's preferred locale and will then use that in a call to `session.localizer.gettext()` to map the message ID to its localized text string. There are times when you may need to manually call the localizer. For example, because the enumeration values passed to [Prompts.choice()](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.prompts#choice) are never automatically localized, you may need to manually retrieve a localized list prior to calling the prompt.

<span style="color:red"><< what's it mean by "may need to manually..."? >></span>

{% highlight JavaScript %}
    var options = session.localizer.gettext(session.preferredLocale(), "choice_options");
    builder.Prompts.choice(session, "choice_prompt", options);
{% endhighlight %}

The default localizer will search for a message ID across multiple files and if it can’t find an ID (or you didn't provide localization files), it will simply return the text of ID, making the use of localization files transparent and optional. Files are searched in the following order:

* First, the `index.json` file under the locale returned by `session.preferredLocale()` is searched
* Next, if the locale included an optional subtag like `en-US`, then the root tag of `en` is searched
* Finally, the bot's configured default locale is searched

## Namespaced prompts

The default localizer supports using namespaces to avoid collisions between message IDs. Namespaces can also be overridden by the bot to essentially let a bot customize or re-skin the prompts from another namespace. Today, you can leverage this capability to customize the SDK’s built-in messages, letting you either add support for additional languages or to simply reword the SDK’s current messages. For example, you can change the SDK’s default error message by simply adding a file called `BotBuilder.json` to your bot's locale directory and then adding an entry for the `default_error` message ID.
 
 ![Locale Namespacing](/en-us/images/builder/locale-namespacing.png)

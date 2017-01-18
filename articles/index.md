#Overview

The Microsoft Bot Framework is a comprehensive offering that you use to build and deploy high quality bots for your users to enjoy wherever they are talking. The framework consists of the Bot Builder SDK, Bot Connector, Developer Portal, and Bot Directory. There's also an emulator that you can use to test your bot.

A bot is a web service that interacts with users in a conversational format. Users start conversations with your bot from any channel that you've configured your bot to work on (for example, Text/SMS, Skype, Slack, Facebook Messenger, and other popular services). You can design conversations to be freeform, natural language interactions or more guided ones where you provide the user choices or actions. The conversation can utilize simple text strings or something more complex such as rich cards that contain text, images, and action buttons.

The following conversation shows a bot that schedules salon appointments. The bot understands the user's intent, presents appointment options using action buttons, displays the user's selection when they tap an appointment, and then sends a thumbnail card that contains the appointment's specifics.

![salon bot example](https://docs.botframework.com/en-us/images/connector/salon_bot_example.png)

To build your bot, the Framework provides a [.NET SDK](/en-us/csharp/builder/sdkreference/) and [Node.js SDK](/en-us/node/builder/overview/). These SDKs provide features such as dialogs and built-in prompts that make interacting with users much simpler. For those using other languages, see the framework’s [REST API](/en-us/connector/overview/). The Bot Builder SDK is provided as open source on GitHub (see [BotBuilder](https://github.com/Microsoft/BotBuilder)).

To give your bot more human-like senses, you can incorporate LUIS for natural language understanding, Cortana for voice, and the Bing APIs for search. For more information about adding intelligence to your bot, see [Bot Intelligence](/en-us/bot-intelligence/getting-started/).

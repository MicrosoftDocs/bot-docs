---
title: Enable speech in the Web Chat channel | Microsoft Docs
description: Learn how to enable speech in the web chat control for a bot connected to the Web Chat channel.
author: DeniseMak
ms.author: v-demak
manager: rstand
ms.topic: article
ms.prod: bot-framework
ms.date: 09/22/2017
---

# How to enable speech in Web Chat
You can enable a voice interface in the Web Chat control. Users interact with the voice interface by using the microphone in the Web Chat control.

![Web chat speech sample](~/media/channel-webchat/webchat-sample-speech.png)

If the user types instead of speaking a response, Web Chat turns off the speech functionality and the bot gives only a textual response instead of speaking out loud. To re-enable the spoken response, the user can use the microphone to respond to the bot the next time. If the microphone is accepting input, it appears dark or filled-in. If it's grayed out, the user clicks on it to enable it.

## Customizing Web Chat for speech
To enable the speech functionality in Web Chat, you need to customize the JavaScript code that invokes the Web Chat control. You can try out voice-enabled Web Chat locally using the following steps: 

1. Clone the <a href="https://github.com/Microsoft/BotFramework-WebChat/">Web Chat GitHub repository</a>.
2. Run `npm install`.
3. Run `npm run build` to build the dependencies and stylesheets that Web Chat requires.
4. Edit the code in `/samples/speech/index.html` according to the type of speech support you want to add. The types of speech implementations are described in [Enable speech services](#enable-speech-services). 
5. Start a web server: `npm run start`
6. Aim your browser at `http://localhost:8000/samples?parameters`. For example, `http://localhost:8000/samples?s=YOURDIRECTLINESECRET` invokes the bot using a Direct Line secret. The parameters can be set in the query string and are described in the following list:

   * s = Direct Line secret.
   * t = Direct Line token. Obtain the token by calling Direct Line's Generate Token.
   * domain = optional URL of an alternate Direct Line endpoint.
   * webSocket = 'true' to use WebSocket to receive messages. The default value is false.
   * userid, username = ID (and optionally name) of the bot user.
   * botid, botname = ID (and optionally name) of the bot.

> [!TIP]
> For more information on how to obtain the Direct Line secret and token, see [Authentication](https://docs.microsoft.com/en-us/bot-framework/rest-api/bot-framework-rest-direct-line-3-0-authentication).

## Enable speech services
The customization allows you to add speech functionality in any of the following ways:

* **Browser-provided speech** - Use speech functionality built into the web browser. At this time, this functionality is only available on the Chrome browser.
* **Use Bing Speech service** - You can use the Bing Speech service to provide speech recognition and synthesis. This way of access speech functionality is supported by a variety of browsers. In this case, the processing is done on a server instead of on the browser.
* **Create a custom speech service** - You can create your own custom speech recognition and voice synthesis components.

### Browser-provided speech

The following code instantiates speech recognizer and speech synthesis components that come with the browser. This method of adding speech is not supported by all browsers. 

> [!NOTE] 
> Google Chrome supports the browser speech recognizer. However, Chrome blocks the microphone in the following cases:
> * If the URL of the page that contains Web Chat begins with `http://` instead of `https://`.
> * If the URL is a local file using the `file://` protocol instead of `http://localhost:8000`.

[!code-js[Specify speech options to use in-browser speech (JavaScript)](./includes/code/channel-connect-webchat-speech.js#BrowserSpeech)]

### Bing Speech service

The following code instantiates speech recognizer and speech synthesis components that use the Bing Speech service. The recognition and generation of speech is performed on the server. This mechanism is supported in multiple browsers. 

> [!TIP]
> You can use speech recognition priming to improve your bot's speech recognition accuracy if you use the Bing Speech service. For more information, check out the [Speech Support in Bot Framework](https://blog.botframework.com/2017/06/26/Speech-To-Text) blog post.

[!code-js[Specify speech options to use the Bing Speech API (JavaScript)](./includes/code/channel-connect-webchat-speech.js#BingSpeech)]

#### Use the Bing Speech service with a token

You also have the option to enable Cognitive Services speech recognition using a token. The token is generated in a secure back end using your API key.

The following example code shows how the token fetch is done from a secure back end to avoid exposing the API key.

[!code-js[Fetch a token to use with the Bing Speech API (JavaScript)](./includes/code/channel-connect-webchat-speech.js#FetchToken)]

### Custom Speech service

You can also provide your own custom speech recognition that implements ISpeechRecognizer or speech synthesis that implements ISpeechSynthesis. 

[!code-js[Fetch a token to use with a custom speech service (JavaScript)](./includes/code/channel-connect-webchat-speech.js#CustomSpeechService)]

### Pass the speech options to Web Chat

The following code passes the speech options to the Web Chat control:

[!code-js[Pass speech options to Web Chat (JavaScript)](./includes/code/channel-connect-webchat-speech.js#PassSpeechOptionsToWebChat)]

## Next steps
Now that you can enable voice interaction with Web Chat, learn how your bot constructs spoken messages and adjusts the state of the microphone:
* [Add speech to messages (C#)](https://docs.microsoft.com/en-us/bot-framework/dotnet/bot-builder-dotnet-text-to-speech)
* [Add speech to messages (Node.js)](https://docs.microsoft.com/en-us/bot-framework/nodejs/bot-builder-nodejs-text-to-speech)

## Additional resources

* You can [download the source code](https://github.com/Microsoft/BotFramework-WebChat) for the web chat control on GitHub.
* The [Bing Speech API documentation](https://docs.microsoft.com/en-us/azure/cognitive-services/speech/home) provides more information on the Bing Speech API.


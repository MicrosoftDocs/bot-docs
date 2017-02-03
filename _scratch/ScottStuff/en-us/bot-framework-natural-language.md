---
layout: page
title: Understanding Natural Language
permalink: /en-us/natural-language/
weight: 4047
parent1: none
---

<span style="color:red"><< We're currently linking to both, which should we be linking to?

* [https://www.microsoft.com/cognitive-services/en-us/language-understanding-intelligent-service-luis](https://www.microsoft.com/cognitive-services/en-us/language-understanding-intelligent-service-luis){:target="_blank"}
* [http://luis.ai](http://luis.ai){:target="_blank"}

>></span>


One of the keys to building a great bot is effectively determining the user's intent when they ask your bot to do something. Bots must be able to understand language the way people speak it&mdash;naturally and contextually. For example, when the user asks your bot to "get news about virtual reality companies," your bot needs to understand the user's intention (find news), and the key entities that are present (virtual reality companies). 

You could use something simple such as regular expressions to match terms in the user's text message, but for even more powerful intent recognition you should leverage the machine learning capabilities of Microsoft's [Language Understanding Intelligent Service (LUIS)](https://www.luis.ai/){:target="_blank"}. LUIS is able to process natural language using prebuilt Bing and Cortana models, or you can build custom-trained language models that are relevant to your bot. 

LUIS is designed to enable you to very quickly deploy an HTTP endpoint that will take the sentences you send it and interpret them in terms of the intention they convey and the key entities that are present. LUIS lets you custom design the set of intentions and entities that are relevant to your bot, and then guides you through the process of building a language understanding system. 


The first step of adding natural language support to your bot is to create your LUIS model. You do this by logging in to [LUIS](http://luis.ai){:target="_blank"} and creating a new LUIS application for your bot. This application is what you’ll use to add the intents and entities that LUIS will use to train your bot's model.

![Create LUIS Application](/en-us/images/builder/builder-luis-create-app.png)

In addition to creating a new LUIS application, you have the option of either importing an existing model (this is what you'll do when working with the Bot Builder examples that use LUIS) or using the prebuilt Cortana application. When you select the prebuilt Cortana application for English you’ll see a dialog similar to below. Notice that the URL listed on the dialog points to the model that LUIS published for your bot's LUIS application, and the URL will be stable for the lifetime of the application. 

![Prebuilt Cortana Application](/en-us/images/builder/builder-luis-default-app.png)

After traffic starts to flow into your LUIS application, LUIS uses active learning to improve itself. In the active learning process, LUIS identifies the interactions that it is relatively unsure of, and asks you to label them according to intent and entities. This has tremendous advantages: First, LUIS knows what it is unsure of and asks you to help where you will provide the maximum improvement in system performance. Secondly, by focusing on the important cases, LUIS learns as quickly as possible, and takes the minimum amount of your time. 

After training and publishing a model for a LUIS application, you can update and retrain the model all you want without having to even redeploy your bot. This is very handy in the early stages of building a bot because you’ll be retraining your model a lot.

For a short video about LUIS, see [Microsoft LUIS Tutorial](https://vimeo.com/145499419){:target="_blank"}.

For additional information about LUIS, see [Cognitive Services](https://www.microsoft.com/cognitive-services/en-us/language-understanding-intelligent-service-luis){:target="_blank"}. 

The Bot Builder SDKs have made it very simple to work with your LUIS models. For information about working with LUIS using Bot Builder for .NET SDK, see [LUIS Dialogs](/en-us/csharp/builder/dialogs-luis/), and for Bot Builder for Node SDK, see [IntentDialog](/en-us/node/builder/chat/IntentDialog/).



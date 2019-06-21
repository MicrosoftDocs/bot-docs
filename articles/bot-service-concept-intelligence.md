---
title: Cognitive Services | Microsoft Docs
description: Learn how to add artificial intelligence to your bots with Microsoft Cognitive Services to make them more useful and engaging.
keywords: language understanding, knowledge extraction, speech recognition, web search
author: RobStand
ms.author: rstand
manager: rstand
ms.topic: article
ms.service: bot-service
ms.subservice: cognitive-services
ms.date: 12/17/2017
monikerRange: 'azure-bot-service-3.0'
---

# Cognitive Services

Microsoft Cognitive Services let you tap into a growing collection of powerful AI algorithms developed by experts in the fields of computer vision, speech, natural language processing, knowledge extraction, and web search. The services simplify a variety of AI-based tasks, giving you a quick way to add state-of-the-art intelligence technologies to your bots with just a few lines of code. The APIs integrate into most modern languages and platforms. The APIs are also constantly improving, learning, and getting smarter, so experiences are always up to date. 

Intelligent bots respond as if they can see the world as people see it. They discover information and extract knowledge from different sources to provide useful answers, and, best of all, they learn as they acquire more experience to continuously improve their capabilities. 

## Language understanding

The interaction between users and bots is mostly free-form, so bots need to understand language naturally and contextually. The Cognitive Service Language APIs provide powerful language models to determine what users want, to identify concepts and entities in a given sentence, and ultimately to allow your bots to respond with the appropriate action. The five APIs support several text analytics capabilities, such as spell checking, sentiment detection, language modeling, and extraction of accurate and rich insights from text. 

Cognitive Services provides these APIs for language understanding:

- The <a href="https://www.microsoft.com/cognitive-services/en-us/language-understanding-intelligent-service-luis" target="_blank">Language Understanding Intelligent Service (LUIS)</a> is able to process natural language using pre-built or custom-trained language models. More details can be found on [Language understanding for bots](v4sdk/bot-builder-concept-luis.md)

- The <a href="https://www.microsoft.com/cognitive-services/en-us/text-analytics-api" target="_blank">Text Analytics API</a> detects sentiment, key phrases, topics, and language from text.

- The <a href="https://www.microsoft.com/cognitive-services/en-us/bing-spell-check-api" target="_blank">Bing Spell Check API</a> provides powerful spell check capabilities, and is able to recognize the difference between names, brand names, and slang.

- The <a href="https://docs.microsoft.com/en-us/azure/machine-learning/studio/text-analytics-module-tutorial" target ="_blank">Text Analytics Models in Azure Machine Learning Studio</a> allows you to build and operationalize text analytics models such as lemmatization and text-preprocessing. These models can help you address issues such as document classification or sentiment analysis problems.

Learn more about [language understanding][language] with Microsoft Cognitive Services.

## Knowledge extraction

Cognitive Services provides four knowledge APIs that enable you to identify named entities or phrases in unstructured text, add personalized recommendations, provide auto-complete suggestions based on natural interpretation of user queries, and search academic papers and other research like a personalized FAQ service.

- The <a href="https://www.microsoft.com/cognitive-services/en-us/entity-linking-intelligence-service" target="_blank">Entity Linking Intelligence Service</a> annotates unstructured text with the relevant entities mentioned in the text. Depending on the context, the same word or phrase may refer to different things. This service understands the context of the supplied text and will identify each entity in your text.    

- The <a href="https://www.microsoft.com/cognitive-services/en-us/knowledge-exploration-service" target="_blank">Knowledge Exploration Service</a> provides natural language interpretation of user queries and returns annotated interpretations to enable rich search and auto-completion experiences that anticipate what the user is typing. Instant query completion suggestions and predictive query refinements are based on your own data and application-specific grammars to enable your users to perform fast queries.    

- The  <a href="https://www.microsoft.com/cognitive-services/en-us/academic-knowledge-api" target="_blank">Academic Knowledge API</a> returns academic research papers, authors, journals, conferences, topics, and universities from the <a href="https://www.microsoft.com/en-us/research/project/microsoft-academic-graph/" target="_blank">Microsoft Academic Graph</a>. Built as a domain-specific example of the Knowledge Exploration Service, the Academic Knowledge API provides a knowledge base using a graph-like dialog with search capabilities over hundreds of millions of research-related entities. Search for a topic, a professor, a university, or a conference, and the API will provide relevant publications and related entities. The grammar also supports natural queries like "Papers by Michael Jordan about machine learning after 2010".

- The  <a href="https://qnamaker.ai" target="_blank">QnA Maker</a> is a free, easy-to-use, REST API and web-based service that trains AI to respond to users’ questions in a natural, conversational way. With optimized machine learning logic and the ability to integrate industry-leading language processing, QnA Maker distills semi-structured data like question and answer pairs into distinct, helpful answers.

Learn more about [knowledge extraction][knowledge] with Microsoft Cognitive Services.

## Speech recognition and conversion

Use the Speech APIs to add advanced speech skills to your bot that leverage industry-leading algorithms for speech-to-text and text-to-speech conversion, as well as speaker recognition. The Speech APIs use built-in language and acoustic models that cover a wide range of scenarios with high accuracy. 

For applications that require further customization, you can use the Custom Recognition Intelligent Service (CRIS). This allows you to calibrate the language and acoustic models of the speech recognizer by tailoring it to the vocabulary of the application, or even to the speaking style of your users.

There are three Speech APIs available in Cognitive Services to process or synthesize speech:

- The <a href="https://www.microsoft.com/cognitive-services/en-us/speech-api" target="_blank">Bing Speech API</a> provides speech-to-text and text-to-speech conversion capabilities.
- The <a href="https://www.microsoft.com/cognitive-services/en-us/custom-recognition-intelligent-service-cris" target="_blank">Custom Recognition Intelligent Service (CRIS)</a> allows you to create custom speech recognition models to tailor the speech-to-text conversion to an application's vocabulary or user's speaking style.
- The <a href="https://www.microsoft.com/cognitive-services/en-us/speaker-recognition-api" target="_blank">Speaker Recognition API</a> enables speaker identification and verification through voice.

The following resources provide additional information about adding speech recognition to your bot.

* [Bot Conversations for Apps video overview](https://channel9.msdn.com/events/Build/2017/P4114)
* [Microsoft.Bot.Client library for UWP or Xamarin apps](https://aka.ms/BotClient)
* [Bot Client Library Sample](https://aka.ms/BotClientSample)
* [Speech-enabled WebChat Client](https://aka.ms/BFWebChat)

Learn more about [speech recognition and conversion][speech] with Microsoft Cognitive Services.

## Web search

The Bing Search APIs enable you to add intelligent web search capabilities to your bots. With a few lines of code, you can access billions of webpages, images, videos, news, and other result types. You can configure the APIs to return results by geographical location, market, or language for better relevance. You can further customize your search using the supported search parameters, such as Safesearch to filter out adult content, and Freshness to return results according to a specific date.

There are five Bing Search APIs available in Cognitive Services.

- The <a href="https://www.microsoft.com/cognitive-services/en-us/bing-web-search-api" target="_blank">Web Search API</a> provides web, image, video, news and related search results with a single API call.

- The <a href="https://www.microsoft.com/cognitive-services/en-us/bing-image-search-api" target="_blank">Image Search API</a> returns image results with enhanced metadata (dominant color, image kind, etc.) and supports several image filters to customize the results.

- The <a href="https://www.microsoft.com/cognitive-services/en-us/bing-video-search-api" target="_blank">Video Search API</a> retrieves video results with rich metadata (video size, quality, price, etc.), video previews, and supports several video filters to customize the results.

- The <a href="https://www.microsoft.com/cognitive-services/en-us/bing-news-search-api" target="_blank">News Search API</a> finds news articles around the world that match your search query or are currently trending on the Internet.

- The  <a href="https://www.microsoft.com/cognitive-services/en-us/bing-autosuggest-api" target="_blank">Autosuggest API</a> offers instant query completion suggestions to complete your search query faster and with less typing. 

Learn more about [web search][search] with Microsoft Cognitive Services.

## Image and video understanding

The Vision APIs bring advanced image and video understanding skills to your bots. 
State-of-the-art algorithms allow you to process images or videos and get back information you can transform into actions. For example, you can use them to recognize objects, people's faces, age, gender or even feelings. 

The Vision APIs support a variety of image understanding features. They can identify mature or explicit content, estimate and accent colors, categorize the content of images, perform optical character recognition, and describe an image with complete English sentences. The Vision APIs also support several image and video processing capabilities, such as intelligently generating image or video thumbnails, or stabilizing the output of a video.

Cognitive Services provide four APIs you can use to process images or videos:

- The <a href="https://www.microsoft.com/cognitive-services/en-us/computer-vision-api" target="_blank">Computer Vision API</a> extracts rich information about images (such as objects or people), determines if the image contains mature or explicit content, and processes text (using OCR) in images.

- The <a href="https://www.microsoft.com/cognitive-services/en-us/emotion-api" target="_blank">Emotion API</a> analyzes human faces and recognizes their emotion across eight possible categories of human emotions.

- The <a href="https://www.microsoft.com/cognitive-services/en-us/face-api" target="_blank">Face API</a> detects human faces, compares them to similar faces, and can even organize people into groups according to visual similarity.

- The <a href="https://www.microsoft.com/cognitive-services/en-us/video-api" target="_blank">Video API</a> analyzes and processes video to stabilize video output, detects motion, tracks faces, and can generate a motion thumbnail summary of the video.

Learn more about [image and video understanding][vision] with Microsoft Cognitive Services.

## Additional resources

You can find comprehensive documentation of each product and their corresponding API references in the <a href="https://docs.microsoft.com/azure/cognitive-services" target="_blank">Cognitive Services documentation</a>.

[language]: https://docs.microsoft.com/en-us/azure/cognitive-services/luis/home
[search]: https://docs.microsoft.com/en-us/azure/cognitive-services/bing-web-search/search-the-web
[vision]: https://docs.microsoft.com/en-us/azure/cognitive-services/computer-vision/home
[knowledge]: https://docs.microsoft.com/en-us/azure/cognitive-services/kes/overview
[speech]: https://docs.microsoft.com/en-us/azure/cognitive-services/speech/home
[location]: https://docs.microsoft.com/en-us/azure/cognitive-services/

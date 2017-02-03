---
layout: page
title: Knowledge
permalink: /en-us/bot-intelligence/knowledge/
weight: 8250
parent1: Bot Intelligence
---


* TOC
{:toc}

## Summary
The Knowledge APIs enable you to identify named entities in unstructured text, add personalized recommendations and semantic search capabilities to your application, and query existing knowledge graphs. 

## API Overview
There are 4 Knowledge APIs available in Cognitive Services to annotate unstructured text with the relevant 'entities' that are referred to in the text, provide recommendations, and build your own or use existing knowledge graphs to enable rich search and auto-completion experiences in your application.

- The [Entity Linking Intelligence Service](https://www.microsoft.com/cognitive-services/en-us/entity-linking-intelligence-service){:target="_blank"} annotates unstructured text with the relevant entities mentioned in the text. Depending on the context, the same word or phrase may refer to different things. This service understands the context of the supplied text and will identify each entity in your text.    
- The [Recommendations API](https://www.microsoft.com/cognitive-services/en-us/bing-image-search-api){:target="_blank"} provides 'frequently bought together', 'customers who liked this product also liked these other products' as well as personalized recommendations based on a user's history. Use this service to build and train a model based on data that you provide, and then use this model to add recommendations to your application. 
- The [Knowledge Exploration Service](https://www.microsoft.com/cognitive-services/en-us/knowledge-exploration-service){:target="_blank"} provides natural language interpretation of user queries and returns annotated interpretations to enable rich search and auto-completion experiences that anticipate what the user is typing. Instant query completion suggestions and predictive query refinements are based your own data and application-specific grammars to enable your users to perform fast, knowledge-based graph queries.    
- Finally, the [Academic Knowledge API](https://www.microsoft.com/cognitive-services/en-us/academic-knowledge-api){:target="_blank"} returns academic research papers, authors, journals, conferences, topics, and universities from the [Microsoft Academic Graph](https://www.microsoft.com/en-us/research/project/microsoft-academic-graph/){:target="_blank"}. Built as a domain-specific example of the Knowledge Exploration Service, the Academic Knowledge API provides graph search capabilities over hundreds of millions of research-related entities. Search for a topic, a professor, a university, or a conference, and the API will provide relevant publications and related entities. The grammar also supports natural queries like 'papers by michael jordan about machine learning after 2010'.

## Use Cases for Bots
The Knowledge APIs can arm your bots with your own knowledge base and user history, allowing them to efficiently navigate your product catalogs to provide recommendations. 

## Getting Started
Before you get started, you need to obtain your own subscription key from the Microsoft Cognitive Services site. You can find detailed documentation about each API, including developer guides and API references by navigating to the Cognitive Services [documentation site](https://www.microsoft.com/cognitive-services/en-us/documentation){:target="_blank"} and selecting the API you are interested in from the navigation bar on the left side of the screen. 
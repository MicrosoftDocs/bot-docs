---
title: Create data-driven experiences with Azure Search  | Microsoft Docs
description: Learn how to create data-driven experiences with Azure Search and help users navigate large amounts of content in a bot with the Bot Framework SDK for .NET and Azure Search.
author: matthewshim-ms
ms.author: v-shimma
manager: kamrani
ms.topic: article
ms.service: bot-service
ms.subservice: sdk
ms.date: 1/28/2019
monikerRange: 'azure-bot-service-3.0'
---

# Create data-driven experiences with Azure Search 

[!INCLUDE [pre-release-label](../includes/pre-release-label-v3.md)]

> [!div class="op_single_selector"]
> - [.NET](../dotnet/bot-builder-dotnet-search-azure.md)
> - [Node.js](../nodejs/bot-builder-nodejs-search-azure.md)

You can add [Azure Search](https://azure.microsoft.com/en-us/services/search/) to a bot to help users navigate large amounts of content and create a data-driven exploration experience.

Azure Search is an Azure service that offers keyword search, built-in linguistics, custom scoring, faceted navigation, and more. Azure Search can also index content from various sources, including Azure SQL DB, DocumentDB, Blob Storage, and Table Storage. It supports "push" indexing for other sources of data, and it can open PDFs, Office documents, and other formats containing unstructured data. Once collected, the content goes into an Azure Search index, which the bot can then query.

## Prerequisites

Install the [Microsoft.Azure.Search](https://www.nuget.org/packages/Microsoft.Azure.Search/4.0.0-preview) Nuget package in your bot project.

The following three C# projects are required in your bot's solution. These projects provide additional functionality for bots and Azure Search. Fork the projects from [GitHub](https://aka.ms/v3-cs-search-demo) or download the source code directly.

- The **Search.Azure** project defines the Azure Service call.
- The **Search.Contracts** project defines generic interfaces and data models to handle data.
- The **Search.Dialogs** project includes various generic Bot Builder dialogs used to query Azure Search.

## Configure Azure Search settings

Configure the Azure Search settings in the **Web.config** file of the project using your own Azure Search credentials in the value fields. 
The constructor in the `AzureSearchClient` class will use these settings to register and bind the bot to the Azure Service.

```xml
<appSettings>
    <add key="SearchDialogsServiceName" value="Azure-Search-Service-Name" /> <!-- replace value field with Azure Service Name --> 
    <add key="SearchDialogsServiceKey" value="Azure-Search-Service-Primary-Key" /> <!-- replace value field with Azure Service Key --> 
    <add key="SearchDialogsIndexName" value="Azure-Search-Service-Index" /> <!-- replace value field with your Azure Search Index --> 
</appSettings>
```

## Create a search dialog

In your bot's project, create a new `AzureSearchDialog` class to call the Azure Service in your bot. This new class must inherit the `SearchDialog` class from the 
**Search.Dialogs** project, which handles most of the heavy lifting. The `GetTopRefiners()` override allows users to narrow/filter their search results without having to start the search over form the beginning, maintaining the search object's state. You can add your own custom refiners in the `TopRefiners` array to let your users filter or narrow down their search results. 

```cs
[Serializable]
public class AzureSearchDialog : SearchDialog
{
    private static readonly string[] TopRefiners = { "refiner1", "refiner2", "refiner3" }; // define your own custom refiners 

    public AzureSearchDialog(ISearchClient searchClient) : base(searchClient, multipleSelection: true)
    {
    }

    protected override string[] GetTopRefiners()
    {
        return TopRefiners;
    }
}
```

## Define the response data model

The **SearchHit.cs** class within the `Search.Contracts` project defines the relevant data to be parsed from the Azure Search response. 
For your bot the only mandatory inclusions are the `PropertyBag` IDictionary declaration and creation in the constructor. You can
define all other properties in this class relative to your bot's needs. 

```cs
[Serializable]
public class SearchHit
{
    public SearchHit()
    {
        this.PropertyBag = new Dictionary<string, object>();
    }

    public IDictionary<string, object> PropertyBag { get; set; }

    // customize the fields below as needed 
    public string Key { get; set; }

    public string Title { get; set; }

    public string PictureUrl { get; set; }

    public string Description { get; set; }
}
```

## After Azure Search responds 

Upon a successful query to the Azure Service, the search result will need to be parsed to retrieve the relevant data for the bot to display to 
the user. To enable this, you'll need to create a `SearchResultMapper` class. The `GenericSearchResult` object created in the constructor 
defines a list and dictionary to store results and facets respectively after each result is parsed respective to your bot's data models. 

Synchronize the properties in the `ToSearchHit` method to match the data model in **SearchHit.cs**. The `ToSearchHit` method will be executed 
and generate a new `SearchHit` for every result found in the response.  

```cs
public class SearchResultMapper : IMapper<DocumentSearchResult, GenericSearchResult>
{
    public GenericSearchResult Map(DocumentSearchResult documentSearchResult)
    {
        var searchResult = new GenericSearchResult();

        searchResult.Results = documentSearchResult.Results.Select(r => ToSearchHit(r)).ToList();
        searchResult.Facets = documentSearchResult.Facets?.ToDictionary(kv => kv.Key, kv => kv.Value.Select(f => ToFacet(f)));

        return searchResult;
    }

    private static GenericFacet ToFacet(FacetResult facetResult)
    {
        return new GenericFacet
        {
            Value = facetResult.Value,
            Count = facetResult.Count.Value
        };
    }

    private static SearchHit ToSearchHit(SearchResult hit)
    {
        return new SearchHit
        {
            // custom properties defined in SearchHit.cs 
            Key = (string)hit.Document["id"],
            Title = (string)hit.Document["title"],
            PictureUrl = (string)hit.Document["thumbnail"],
            Description = (string)hit.Document["description"]
        };
    }
}
```
After the results are parsed and stored, the information still needs to be displayed to the user. 
The `SearchHitStyler` class will need to be managed to accommodate the your data model from the `SearchHit` class. For example, the `Title`, `PictureUrl`, and `Description` properties from the **SearchHit.cs** class are used in the sample code to create a new card attachments. The following code creates a new card attachment for every `SearchHit` object in the  `GenericSearchResult` Results list to display to the user.   

```cs
[Serializable]
public class SearchHitStyler : PromptStyler
{
    public override void Apply<T>(ref IMessageActivity message, string prompt, IReadOnlyList<T> options, IReadOnlyList<string> descriptions = null)
    {
        var hits = options as IList<SearchHit>;
        if (hits != null)
        {
            var cards = hits.Select(h => new ThumbnailCard
            {
                Title = h.Title,
                Images = new[] { new CardImage(h.PictureUrl) },
                Buttons = new[] { new CardAction(ActionTypes.ImBack, "Pick this one", value: h.Key) },
                Text = h.Description
            });

            message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            message.Attachments = cards.Select(c => c.ToAttachment()).ToList();
            message.Text = prompt;
        }
        else
        {
            base.Apply<T>(ref message, prompt, options, descriptions);
        }
    }
}
```
The search results are displayed to the user and you've successfully added Azure Search to your bot.

## Samples

For two complete samples that show how to support Azure Search with bots using the Bot Framework SDK for .NET, see the 
[Real Estate bot sample](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/CSharp/demo-Search/RealEstateBot) or [Job Listing bot sample](https://github.com/Microsoft/BotBuilder-Samples/tree/v3-sdk-samples/CSharp/demo-Search/JobListingBot) in GitHub. 

## Additional resources

- [Azure Search][search]
- [Dialogs overview](bot-builder-dotnet-dialogs.md)

[search]: /azure/search/search-what-is-azure-search

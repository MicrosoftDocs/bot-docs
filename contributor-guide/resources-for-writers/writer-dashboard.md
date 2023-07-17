# Writer's dashboard

**Note**: the base URL for org-wide issues and PRs are different, as are the URLs for repo-specific queries.
See GitHub's [Searching issues and pull requests](https://docs.github.com/en/github/searching-for-information-on-github/searching-issues-and-pull-requests) help topic for more info on how to construct such queries.

## Issues

| Query | Composer | SDK | MicrosoftDocs | Microsoft |
|:-|:-|:-|:-|:-|
|<img width=180/>|<img width=100/>|<img width=90/>|<img width=110/>|<img width=100/>|
|Open issues|[all](https://github.com/MicrosoftDocs/composer-docs/issues)|[all](https://github.com/MicrosoftDocs/bot-docs/issues)|-|-|
|&bullet; assigned to **me**|[mine](https://github.com/MicrosoftDocs/composer-docs/issues/assigned/@me)|[mine](https://github.com/MicrosoftDocs/bot-docs/issues/assigned/@me)|[mine](https://github.com/issues?q=is%3Aissue+is%3Aopen+org%3AmicrosoftDocs+assignee%3A%40me)|[mine](https://github.com/issues?q=is%3Aissue+is%3Aopen+org%3Amicrosoft+assignee%3A%40me)|
|&bullet; not assigned |[unassigned](https://github.com/MicrosoftDocs/composer-docs/issues?q=is%3Aopen+is%3Aissue+no%3Aassignee)|[unassigned](https://github.com/MicrosoftDocs/bot-docs/issues?q=is%3Aopen+is%3Aissue+no%3Aassignee)|-|-|
|&bullet; not labeled|[unlabeled](https://github.com/MicrosoftDocs/composer-docs/issues?q=is%3Aopen+is%3Aissue+no%3Alabel)|[unlabeled](https://github.com/MicrosoftDocs/bot-docs/issues?q=is%3Aopen+is%3Aissue+no%3Alabel)|-|-|

## Pull requests

| Query | Composer | SDK | MicrosoftDocs | Microsoft |
|:-|:-|:-|:-|:-|
|<img width=180/>|<img width=100/>|<img width=90/>|<img width=110/>|<img width=100/>|
|Open PRs|[all](https://github.com/MicrosoftDocs/composer-docs-pr/pulls)|[all](https://github.com/MicrosoftDocs/bot-docs-pr/pulls)|-|-|
|&bullet; authored by me|[mine](https://github.com/MicrosoftDocs/composer-docs-pr/pulls/@me)|[mine](https://github.com/MicrosoftDocs/bot-docs-pr/pulls/@me)|[mine](https://github.com/pulls?q=is%3Apr+org%3AmicrosoftDocs+is%3Aopen+author%3A%40me)|[mine](https://github.com/pulls?q=is%3Apr+org%3Amicrosoft+is%3Aopen+author%3A%40me)|
|&emsp;&bullet; in draft |[in draft](https://github.com/MicrosoftDocs/composer-docs-pr/pulls/@me+draft%3Atrue)|[in draft](https://github.com/MicrosoftDocs/bot-docs-pr/pulls/@me+draft%3Atrue)|[in draft](https://github.com/pulls?q=is%3Apr+org%3AmicrosoftDocs+is%3Aopen+author%3A%40me+draft%3Atrue)|[in draft](https://github.com/pulls?q=is%3Apr+org%3Amicrosoft+is%3Aopen+author%3A%40me+draft%3Atrue)|
|&emsp;&bullet; awaiting review |[in review](https://github.com/MicrosoftDocs/composer-docs-pr/pulls/@me+draft%3Afalse+review%3Anone)|[in review](https://github.com/MicrosoftDocs/bot-docs-pr/pulls/@me+draft%3Afalse+review%3Anone)|[in review](https://github.com/pulls?q=is%3Apr+org%3AmicrosoftDocs+is%3Aopen+author%3A%40me+draft%3Afalse+review%3Anone)|[in review](https://github.com/pulls?q=is%3Apr+org%3Amicrosoft+is%3Aopen+author%3A%40me+draft%3Afalse+review%3Anone)|
|&emsp;&bullet; changes requested |[revising](https://github.com/MicrosoftDocs/composer-docs-pr/pulls/@me+draft%3Afalse+review%3Achanges_requested)|[revising](https://github.com/MicrosoftDocs/bot-docs-pr/pulls/@me+draft%3Afalse+review%3Achanges_requested)|[revising](https://github.com/pulls?q=is%3Apr+org%3AmicrosoftDocs+is%3Aopen+author%3A%40me+draft%3Afalse+review%3Achanges_requested)|[revising](https://github.com/pulls?q=is%3Apr+org%3Amicrosoft+is%3Aopen+author%3A%40me+draft%3Afalse+review%3Achanges_requested)|
|&emsp;&bullet; reviewed & approved |[ready'ish](https://github.com/MicrosoftDocs/composer-docs-pr/pulls?q=is%3Aopen+is%3Apr+author%3A%40me+review%3Aapproved)|[ready'ish](https://github.com/MicrosoftDocs/bot-docs-pr/pulls?q=is%3Aopen+is%3Apr+author%3A%40me+review%3Aapproved)|[ready'ish](https://github.com/pulls?q=is%3Apr+org%3AmicrosoftDocs+is%3Aopen+author%3A%40me+review%3Aapproved)|[ready'ish](https://github.com/pulls?q=is%3Apr+org%3Amicrosoft+is%3Aopen+author%3A%40me+review%3Aapproved)|
|&bullet; my review requested |[to review](https://github.com/MicrosoftDocs/composer-docs-pr/pulls/review-requested/@me)|[to review](https://github.com/MicrosoftDocs/bot-docs-pr/pulls/review-requested/@me)|[to review](https://github.com/pulls?q=is%3Apr+org%3AmicrosoftDocs+is%3Aopen+review-requested%3A@me)|[to review](https://github.com/pulls?q=is%3Apr+org%3Amicrosoft+is%3Aopen+review-requested%3A@me)|
|&bullet; labeled ready|[to merge](https://github.com/MicrosoftDocs/composer-docs-pr/pulls?q=is%3Aopen+is%3Apr+label%3A%22status%3A+ready%22) | [to merge](https://github.com/MicrosoftDocs/bot-docs-pr/pulls?q=is%3Aopen+is%3Apr+label%3A%22status%3A+ready%22) | - | - |

## Documentation quick links

|Resource|Notes|
|:-|:-|
|[Glossary](https://github.com/MicrosoftDocs/bot-docs-pr/blob/master/contributor-guide/bot-framework-glossary.md)|Evolving notes for product and service names, and bot-specific and -adjacent terminology.|
|[Text formatting guidelines](https://github.com/MicrosoftDocs/bot-docs-pr/blob/master/contributor-guide/formatting-text.md)|Rules of thumb for formatting text in our doc set.|

| Bot Framework documentation |  |
|:-|:-|
| [live](https://learn.microsoft.com/azure/bot-service/) | review site: [main](https://review.learn.microsoft.com/azure/bot-service/?branch=main) |
| **Other Microsoft documentation** |  |
| [Azure AI services](https://learn.microsoft.com/azure/cognitive-services/) |
| [Microsoft Power Virtual Agents](https://learn.microsoft.com/power-virtual-agents/) | review site: [main](https://review.learn.microsoft.com/power-virtual-agents/?branch=main) |
| [Microsoft Teams](https://learn.microsoft.com/microsoftteams/platform/) |
| [Speech service](https://learn.microsoft.com/azure/cognitive-services/speech-service/) |

## Additional repos

| Bot Framework SDK v3 archive |  |
|:-|:-|
| [microsoftDocs / bot-docs-archive-pr](https://github.com/MicrosoftDocs/bot-docs-archive-pr) | Archive of the Bot Framework SDK v3 documentation |
| **Intermediate (ref docs)** |  |
| [microsoftDocs / botbuilder-docs-sdk-dotnet](https://github.com/MicrosoftDocs/botbuilder-docs-sdk-dotnet) | For .NET/C# |
| [microsoftDocs / botbuilder-docs-sdk-java](https://github.com/MicrosoftDocs/botbuilder-docs-sdk-java) | For Java |
| [microsoftDocs / botbuilder-docs-sdk-python](https://github.com/MicrosoftDocs/botbuilder-docs-sdk-python) | For Python |
| [microsoftDocs / botbuilder-docs-sdk-typescript](https://github.com/MicrosoftDocs/botbuilder-docs-sdk-typescript) | For Node.js/TypeScript/JavaScript |
| **Other doc sets** |  |
| [microsoftDocs / msteams-docs](https://github.com/MicrosoftDocs/msteams-docs) | Microsoft Teams |
| [microsoftDocs / power-platform](https://github.com/MicrosoftDocs/power-platform) | Power Virtual Agents |
| **Code repos** |  |
| [microsoft / botframework-sdk](https://github.com/microsoft/botframework-sdk) | Central repo for the whole Bot Framework |
| &emsp; [ / Orchestrator](https://github.com/microsoft/botframework-sdk/tree/main/Orchestrator) | Where the Orchestrator code and reference material lives |
| [microsoft / BotBuilder-dotnet](https://github.com/Microsoft/BotBuilder-dotnet) | Bot Framework SDK for .NET (v4) |
| [microsoft / BotBuilder-java](https://github.com/Microsoft/BotBuilder-java) | Bot Framework SDK for Java (v4) |
| [microsoft / BotBuilder-js](https://github.com/Microsoft/BotBuilder-js) | Bot Framework SDK for JavaScript (v4) |
| [microsoft / BotBuilder-python](https://github.com/Microsoft/BotBuilder-python) | Bot Framework SDK for Python (v4) |
| [microsoft / BotBuilder-Samples](https://github.com/Microsoft/BotBuilder-Samples) | Bot Framework samples (v4) |
| [microsoft / Recognizers-Text](https://github.com/microsoft/Recognizers-Text) | Microsoft Recognizers Text (used by the dialogs library) |
| [microsoft / botframework-cli](https://github.com/microsoft/botframework-cli) | Bot Framework CLI |
| [microsoft / Botframework-composer](https://github.com/microsoft/Botframework-composer) | Bot Framework Composer |
| [microsoft / BotFramework-Emulator](https://github.com/microsoft/BotFramework-Emulator) | Bot Framework Emulator |
| [microsoft / botframework-components](https://github.com/microsoft/botframework-components) | Bot Framework Components, had been Virtual Assistant |
| [microsoft / BotFramework-Services](https://github.com/microsoft/BotFramework-Services) | Place to file issues found in the [Bot Framework portal](https://dev.botframework.com/). |
| [microsoft / BotFramework-WebChat](https://github.com/microsoft/BotFramework-WebChat) | Bot Framework Web Chat [component] |
| [microsoft / BotFramework-DirectLine-DotNet](https://github.com/microsoft/BotFramework-DirectLine-DotNet) | Bot Framework Direct Line C# client |
| [microsoft / botframework-directlinejs](https://github.com/microsoft/botframework-directlinejs) | Bot Framework Direct Line JavaScript client |
| **Community** |  |
| [BotBuilderCommunity/botbuilder-community-dotnet](https://github.com/BotBuilderCommunity/botbuilder-community-dotnet) | Bot Builder Community - .NET Extensions |
| [BotBuilderCommunity/botbuilder-community-js](https://github.com/BotBuilderCommunity/botbuilder-community-js) | Bot Builder Community - JavaScript Extensions |
| [BotBuilderCommunity/botbuilder-community-tools](https://github.com/BotBuilderCommunity/botbuilder-community-tools) | Bot Builder Community - Tools |
| [BotBuilderCommunity/botbuilder-community-python](https://github.com/BotBuilderCommunity/botbuilder-community-python) | Bot Builder Community - Python Extensions |
| [BotBuilderCommunity/botbuilder-community-java](https://github.com/BotBuilderCommunity/botbuilder-community-java) | Bot Builder Community - Java Extensions |

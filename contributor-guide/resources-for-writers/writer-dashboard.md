# Writer's dashboard

**TODO**: Create a copy of this page and replace `YourGitHubAlias` with your GitHub alias.

**Note**: the base URL for org-wide issues and PRs are different, as are the URLs for repo-specific queries.
See GitHub's [Searching issues and pull requests](https://docs.github.com/en/github/searching-for-information-on-github/searching-issues-and-pull-requests) help topic for more info on how to construct such queries.

## SDK and Composer issues and PRs

| SDK doc set |  |
|:-|:-|
| [All open **issues**](https://github.com/MicrosoftDocs/bot-docs/issues) | is:open is:issue |
| &bullet; [Open **issues** with no label](https://github.com/MicrosoftDocs/bot-docs/issues?q=is%3Aopen+is%3Aissue+no%3Alabel) | is:open is:issue no:label |
| &bullet; [Open customer **issues**](https://github.com/MicrosoftDocs/bot-docs/issues?q=is%3Aopen+is%3Aissue+label%3A%22source%3A+customer%22) | is:open is:issue label:"source: customer" |
| [All open **PRs**](https://github.com/MicrosoftDocs/bot-docs-pr/pulls) | is:open is:pr |
| **Composer doc set** |  |
| [All open **issues**](https://github.com/MicrosoftDocs/composer-docs/issues) | is:open is:issue |
| &bullet; [Open **issues** with no label](https://github.com/MicrosoftDocs/composer-docs/issues?q=is%3Aopen+is%3Aissue+no%3Alabel) | is:open is:issue no:label |
| &bullet; [Open customer **issues**](https://github.com/MicrosoftDocs/composer-docs/issues?q=is%3Aopen+is%3Aissue+label%3A%22source%3A+customer%22) | is:open is:issue label:"source: customer" |
| [All open **PRs**](https://github.com/MicrosoftDocs/composer-docs-pr/pulls) | is:open is:pr |

## Issues or PRs associated with your alias

For both doc and code repos:

| Query | Qualifiers |
|:-|:-|
| [Open **issues** _assigned_ to you](https://github.com/issues?q=is%3Aissue+is%3Aopen+org%3AmicrosoftDocs+org%3Amicrosoft+assignee%3AYourGitHubAlias) | is:issue is:open org:microsoftDocs org:microsoft<br/>assignee:YourGitHubAlias |
| [Your open **PR**s reviewed and **approved**](https://github.com/pulls?q=is%3Apr+org%3AmicrosoftDocs+org%3Amicrosoft+author%3AYourGitHubAlias+review%3Aapproved+is%3Aopen) | is:pr is:open org:microsoftDocs org:microsoft<br/>author:YourGitHubAlias review:approved |
| &bullet; [Not yet labeled as **ready**](https://github.com/pulls?q=is%3Apr+org%3AmicrosoftDocs+org%3Amicrosoft+author%3AYourGitHubAlias+review%3Aapproved+is%3Aopen+-label%3A%22status%3A+ready%22) ||
| &bullet; [Labeled as **ready**](https://github.com/pulls?q=is%3Apr+org%3AmicrosoftDocs+org%3Amicrosoft+author%3AYourGitHubAlias+review%3Aapproved+is%3Aopen+label%3A%22status%3A+ready%22) ||
| [Your open **PR**s with **changes requested**](https://github.com/pulls?q=is%3Apr+org%3AmicrosoftDocs+org%3Amicrosoft+author%3AYourGitHubAlias+review%3Achanges_requested+is%3Aopen) | is:pr is:open org:microsoftDocs org:microsoft<br/>author:YourGitHubAlias review:changes_requested |
| [Open, non-draft **PR**s awaiting your review](https://github.com/pulls?q=is%3Apr+is%3Aopen+draft%3Afalse+org%3AmicrosoftDocs+org%3Amicrosoft+review-requested%3AYourGitHubAlias+) | is:pr is:open draft:false org:microsoftDocs org:microsoft<br/>review-requested:YourGitHubAlias |
| [Your open **PR**s in **draft**](https://github.com/pulls?q=is%3Apr+is%3Aopen+draft%3Atrue+org%3AmicrosoftDocs+org%3Amicrosoft+author%3AYourGitHubAlias) | is:pr is:open draft:true org:microsoftDocs org:microsoft<br/>author:YourGitHubAlias |
| [Your open **PR**s labeled as in **writing**](https://github.com/pulls?q=is%3Apr+is%3Aopen+org%3AmicrosoftDocs+org%3Amicrosoft+author%3AYourGitHubAlias+label%3A%22status%3A+writing%22) | is:pr is:open org:microsoftDocs org:microsoft<br/>author:YourGitHubAlias label:"status: writing" |
| [Your open, _non-draft_ **PR**s awaiting review](https://github.com/pulls?q=is%3Apr+is%3Aopen+draft%3Afalse+org%3AmicrosoftDocs+org%3Amicrosoft+author%3AYourGitHubAlias+review%3Anone) | is:pr is:open draft:false org:microsoftDocs org:microsoft<br/>author:YourGitHubAlias review:none |
| [Open **issues** you authored](https://github.com/issues?q=is%3Aissue+is%3Aopen+org%3AmicrosoftDocs+org%3Amicrosoft+author%3AYourGitHubAlias+) | is:issue is:open org:microsoftDocs org:microsoft<br/>author:YourGitHubAlias |

## Documentation quick links

| Bot Framework documentation |  |
|:-|:-|
| [live](https://docs.microsoft.com/azure/bot-service/) | review site: [master](https://review.docs.microsoft.com/azure/bot-service/?branch=master) |
| **Other Microsoft documentation** |  |
| [Azure Cognitive Services](https://docs.microsoft.com/azure/cognitive-services/) |
| [Microsoft Power Virtual Agents](https://docs.microsoft.com/en-us/power-virtual-agents/) |
| [Microsoft Teams](https://docs.microsoft.com/microsoftteams/platform/) |
| [Speech service](https://docs.microsoft.com/azure/cognitive-services/speech-service/) |

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
| [microsoft / BotBuilder-dotnet](https://github.com/Microsoft/BotBuilder-dotnet) | Bot Framework SDK for .NET (v4) |
| [microsoft / BotBuilder-java](https://github.com/Microsoft/BotBuilder-java) | Bot Framework SDK for Java (v4) |
| [microsoft / BotBuilder-js](https://github.com/Microsoft/BotBuilder-js) | Bot Framework SDK for JavaScript (v4) |
| [microsoft / BotBuilder-python](https://github.com/Microsoft/BotBuilder-python) | Bot Framework SDK for Python (v4) |
| [microsoft / BotBuilder-Samples](https://github.com/Microsoft/BotBuilder-Samples) | Bot Framework samples (v4) |
| [microsoft / Recognizers-Text](https://github.com/microsoft/Recognizers-Text) | Microsoft Recognizers Text (used by the dialogs library) |
| [microsoft / botframework-cli](https://github.com/microsoft/botframework-cli) | Bot Framework CLI |
| [microsoft / Botframework-composer](https://github.com/microsoft/Botframework-composer) | Bot Framework Composer |
| [microsoft / BotFramework-Emulator](https://github.com/microsoft/BotFramework-Emulator) | Bot Framework Emulator |
| [microsoft / botframework-components](https://github.com/microsoft/botframework-components) | Bot Framework Solutions, aka Virtual Assistant |
| [microsoft / BotFramework-Services](https://github.com/microsoft/BotFramework-Services) | Place to file issues found in the [Bot Framework portal](https://dev.botframework.com/). |
| [microsoft / BotFramework-WebChat](https://github.com/microsoft/BotFramework-WebChat) | Bot Framework Web Chat [component] |
| [microsoft / botframework-directlinejs](https://github.com/microsoft/botframework-directlinejs) | Bot Framework Direct Line JavaScript client |
| **Community** |  |
| [BotBuilderCommunity/botbuilder-community-dotnet](https://github.com/BotBuilderCommunity/botbuilder-community-dotnet) | Bot Builder Community - .NET Extensions |
| [BotBuilderCommunity/botbuilder-community-js](https://github.com/BotBuilderCommunity/botbuilder-community-js) | Bot Builder Community - JavaScript Extensions |
| [BotBuilderCommunity/botbuilder-community-tools](https://github.com/BotBuilderCommunity/botbuilder-community-tools) | Bot Builder Community - Tools |
| [BotBuilderCommunity/botbuilder-community-python](https://github.com/BotBuilderCommunity/botbuilder-community-python) | Bot Builder Community - Python Extensions |
| [BotBuilderCommunity/botbuilder-community-java](https://github.com/BotBuilderCommunity/botbuilder-community-java) | Bot Builder Community - Java Extensions |

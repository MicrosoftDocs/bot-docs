# Writer's dashboard

**TODO**: Create a copy of this page and replace `YourGitHubAlias` with your GitHub alias.

**Note**: the base URL for org-wide issues and PRs are different, as are the URLs for repo-specific queries.

## SDK and Composer issues

| Query | Qualifiers |
|:-|:-|
| [Open **issues** in **bot-docs** with no label](https://github.com/MicrosoftDocs/bot-docs/issues?q=is%3Aopen+is%3Aissue+no%3Alabel) | is:open is:issue no:label |
| [Open **issues** in **composer-docs** with no label](https://github.com/MicrosoftDocs/composer-docs/issues?q=is%3Aopen+is%3Aissue+no%3Alabel) | is:open is:issue no:label |

## Issues or PRs associated with your alias

For both doc and code repos:

| Query | Qualifiers |
|:-|:-|
| [Open **issues** _assigned_ to you](https://github.com/issues?q=is%3Aissue+is%3Aopen+org%3AmicrosoftDocs+org%3Amicrosoft+assignee%3AYourGitHubAlias) | is:issue is:open org:microsoftDocs org:microsoft<br/>assignee:YourGitHubAlias |
| [Your open **PR**s reviewed and **approved**](https://github.com/pulls?q=is%3Apr+org%3AmicrosoftDocs+org%3Amicrosoft+author%3AYourGitHubAlias+review%3Aapproved+is%3Aopen) | is:pr is:open org:microsoftDocs org:microsoft<br/>author:YourGitHubAlias review:approved |
| [Your open **PR**s with **changes requested**](https://github.com/pulls?q=is%3Apr+org%3AmicrosoftDocs+org%3Amicrosoft+author%3AYourGitHubAlias+review%3Achanges_requested+is%3Aopen) | is:pr is:open org:microsoftDocs org:microsoft<br/>author:YourGitHubAlias review:changes_requested |
| [Open, non-draft **PR**s awaiting your review](https://github.com/pulls?q=is%3Apr+is%3Aopen+draft%3Afalse+org%3AmicrosoftDocs+org%3Amicrosoft+review-requested%3AYourGitHubAlias+) | is:pr is:open draft:false org:microsoftDocs org:microsoft<br/>review-requested:YourGitHubAlias |
| [Your open **PR**s in **draft**](https://github.com/pulls?q=is%3Apr+is%3Aopen+draft%3Atrue+org%3AmicrosoftDocs+org%3Amicrosoft+author%3AYourGitHubAlias) | is:pr is:open draft:true org:microsoftDocs org:microsoft<br/>author:YourGitHubAlias |
| [Your open, _non-draft_ **PR**s awaiting review](https://github.com/pulls?q=is%3Apr+is%3Aopen+draft%3Afalse+org%3AmicrosoftDocs+org%3Amicrosoft+author%3AYourGitHubAlias+review%3Anone) | is:pr is:open draft:false org:microsoftDocs org:microsoft<br/>author:YourGitHubAlias review:none |
| [Open **issues** you authored](https://github.com/issues?q=is%3Aissue+is%3Aopen+org%3AmicrosoftDocs+org%3Amicrosoft+author%3AYourGitHubAlias+) | is:issue is:open org:microsoftDocs org:microsoft<br/>author:YourGitHubAlias |

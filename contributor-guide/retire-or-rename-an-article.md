# Steps to follow when you retire, rename, or move an Bot Framework technical article

This guidance is for SMEs who are listed as the author of an Bot Framework technical article that needs to be retired, renamed, or moved in docs.microsoft.com/azure.

If you're a member of our Bot Framework community and you think an article should be retired for any reason, please leave a comment in the comment stream for the article to let the author know something is wrong with the article.

When authors want to retire, rename, or move articles, they need to follow specific steps to avoid bad experiences on the web site. Our goal should be to gracefully retire content so users of the website don't find broken links and receive 404 errors.

## Automated solution

If you have to move a large number of files, or all files in one directory to another, [Ralph's GitMover tool](https://github.com/squillace/gitwork/tree/master/dotnet/move) may allow you to automate most of this work.

## Manual steps

### Step 1: Set the article to NOINDEX and republish it (as appropriate)

Do this step if you are preparing to deprecate content and do not want it to be discoverable, but you want it to remain published to support inbound links. To do this, add the following line as the last entry in the metadata section of the article:

```yml
ROBOTS: NOINDEX
```

By using NOINDEX alone, you allow cross-links to current content that are embedded in the article to be crawled, and you avoid creating a dead-end for search crawlers.

### Step 2: Turn the original article into a redirect, and create the new file if you are renaming or moving a file

In our publishing workflow, the article you want to retire, rename, or move must remain in place so you can create a redirect to the new article or to the replacement content. You turn an article into a redirect by deleting the article metadata and content and adding just the redirect metadata. Make the changes that match what you want to do:

- **Retire**: Change the metadata so the article redirects to the service landing page. If the service is being deprecated, redirect the pages to the Bot Framework hub page on docs.

- **Rename**: Create a copy of the article, give the file its new name, and then change the metadata of the original article so the article redirects to the new one.

- **Move**: Create a copy of the article in the new location, and then change the metadata of the original article so the article redirects to the new one.

For example, if you want to move a file from the `articles` folder into a sub-folder, you need to update the original file to contain just metadata in the header. The `/azure/` part is important, as the root of the site is `docs.microsoft.com`.

```yml
redirect_url: /azure/azure-resource-manager/resource-manager-subscription-examples
redirect_document_id: TRUE 
```

For more information, see the OPS documentation on [How to retire, move, or rename a technical article](https://review.docs.microsoft.com/help/platform/retire-rename-articles?branch=main).

Do not delete articles from the bot-framework-docs-pr or bot-framework-docs repositories, period. If you delete an article, you cannot create the article-level redirects, which guarantees that customers will experience 404s.

### Step 3: Remove or update all cross-links to the article from the technical content repository

Do not rely on redirects to take care of cross-links from other articles. Update or remove the cross references to the article you are retiring, renaming, or moving, including links in articles owned by other authors.

1. Ensure you are working in an up-to-date local branch – run `git pull upstream master` (or the appropriate variation on this command).
1. Scan the bot-framework-docs-pr/articles folder and the bot-framework-docs-pr/includes folder for any articles and includes that link to the article you want to retire, move, or rename. Either remove the cross-links or replace them with an appropriate new cross-link. You can use a search and replace utility to find the cross-links if you have one installed. If you don't, you can use Windows PowerShell for free! Here's how to use PowerShell to find the cross-links:

   1. Start Windows PowerShell.
   1. At the PowerShell prompt, change into the bot-framework-docs-pr\articles folder:

     `cd bot-framework-docs-pr\articles`

   1. Type this command, which will list all files that contain a reference to the article you are deleting:

     `Get-ChildItem -Recurse -Include *.md* | Select-String "<the name of the topic you are deleting>" | group path | select name`

     If you prefer to send the list of file names to a text file (in this case, named psoutput.txt), you can:

     `Get-ChildItem -Recurse -Include *.md* | Select-String "<the name of the topic you are deleting>" | group path | select name | Out-File C:\Users\<your account>\psoutput.txt`

1. Add and commit all your changes, push them to your fork, and create a pull request to move your changes from your fork to the master branch of the main repository.

### Step 4: Publish  

Publish your changes to the article repository by submitting a pull request. Test that the redirects work in staging before you sign-off on the PR.

### Step 5: Cleanup tasks

These cleanup tasks need to be performed immediately after the changes are published. For details, see the OPS [Cleanup tasks list](https://review.docs.microsoft.com/help/platform/retire-rename-articles?branch=main#step-5-cleanup-tasks).

1. Update the FWLink tool.
1. Manage inbound links.
1. Remove cached pages from search engines (only if absolutely necessary).
1. Clean up redirect

#### Contributors' guide links

- [Overview article](../README.md)
- [Index of guidance articles](contributor-guide-index.md)

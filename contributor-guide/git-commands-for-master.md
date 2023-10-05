# Git commands for creating a new article or updating an existing article

## Standard process (working from master)

Follow the steps in this article to create a local working branch on your computer so that you can create a new article for the Bot Framework technical documentation section of learn.microsoft.com/Bot Framework or update an existing article.

1. Start Git Bash (or the command-line tool you use for Git).

   **Note:** If you are working in the public repository, change bot-framework-docs-pr to bot-framework-docs in all the commands.
1. Change to bot-framework-docs-pr:

    ```console
    cd bot-framework-docs-pr
    ```

1. Check out the master branch:

    ```console
    git checkout master
    ```

1. Create a fresh local working branch derived from the master branch:

    ```console
    git pull upstream master
    git checkout -B <working branch>
    ```

1. Let your fork know you created the local working branch:

    ```console
    git push origin <working branch>
    ```

1. Create your new article or make changes to an existing article. Use Windows Explorer to open and create new markdown files. After you create or modify your article and images, go to the next step.
1. Add and commit the changes you made:

    ```console
    git add --all
    git commit -m "<comment>"
    ```

    Or, to add only the specific files you modified:

    ```console
    git add <file path>
    git commit -m "<comment>"
    ```

1. Update your local working branch with changes from the upstream master branch:

    ```console
    git pull upstream master
    ```

1. Push the changes to your fork on GitHub:

    ```console
    git push origin <working branch>
    ```

1. When you are ready to submit your content to the upstream master branch for staging, validation, and/or publishing, in the GitHub UI, create a pull request from your fork to the master branch.
1. If you are an employee working in the private repository, the changes you submit are automatically staged and a staging link is written to the pull request. Please review your staged content and sign off in the pull request comments by adding the **#sign-off** comment.  This indicates the changes are ready to be pushed live.  If you don't want the pull request to be accepted - if you are just staging the changes - add the **#hold-off** note to the pull request.
1. If your update is small in scope, the PR may qualify for automatic merge. If not, the pull request review team reviews your pull request, provides feedback, and merges it if the PR meets [the minimum quality criteria](contributor-guide-pr-criteria).

## Publishing

* Articles are published at approximately 10:00 AM and 3:00 PM Pacific Time, Monday-Friday. It can take up to 30 minutes for articles to appear online after publishing. Remember your pull request has to be merged by a pull request reviewer before the changes can be included in the next scheduled publishing run. You need to work with your pull request reviewer ahead of time to ensure a pull request is merged for a specific publishing run. Otherwise, PRs are reviewed in the order they were submitted.
* If you are an employee working in the private repository, all pull requests are subject to validation rules that need to be addressed before the pull request can be merged.

## Working with release branches

When you are working with a release branch, the best way to create a local working branch from the release branch is to use this command syntax:

```console
git checkout -b <local working branch name> upstream/<upstream branch name>
```

This creates the local branch directly from the upstream branch, avoiding any local merging.

Then, to keep your local copy of the release branch up to date with the upstream version, run:

```console
git pull upstream <release-branch-name>
```

The person responsible for managing the release from the content side needs to update the release branch with updates from master on a daily basis.

**TIP:** If you receive a *fatal: Cannot update paths and switch to branch 'release-branch' at the same time* error when issuing the `checkout` command, execute `git fetch upstream`, then the checkout command. The `fetch` grabs all the new remote-tracking branches (such as the release branch you want to work with) and tags without merging those changes into your own branches.

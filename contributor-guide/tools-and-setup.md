# Install and set up tools for authoring in GitHub

Follow the steps in this article to set up tools for contributing to the Bot Framework technical documentation. Casual and occasional contributors probably can use the GitHub UI described in step 2.

If you're unfamiliar with Git, you might want to review some Git terminology: [https://help.github.com/articles/github-glossary](https://help.github.com/articles/github-glossary). In addition, this Stack Overflow post contains a glossary of Git terms you'll encounter here: [https://stackoverflow.com/questions/7076164/terminology-used-by-git](https://stackoverflow.com/questions/7076164/terminology-used-by-git)

## Create a GitHub account and set up your profile

To contribute to the Bot Framework technical content, you'll need a [GitHub](https://www.github.com) account. If you are a Microsoft contributor, you need to set up your GitHub account so you're clearly identified as a Microsoft employee. Set up your profile as follows:

- **Profile picture**: a picture of you (required)
- **Name**: your first and last name (required)
- **Email**: your Microsoft email address (optional)
- **Company**: Microsoft Corporation (required)
- **Location**: list your location (optional)

Your profile should resemble this profile:

![GitHub profile example](./media/tools-and-setup/githubprofile.png)

## Sign up for Livefyre

Every published technical article supports a comment stream provided by the [Livefyre](http://web.livefyre.com/) service.

As a Microsoft employee and article author or contributor, you need to sign up for Livefyre so you can participate in the comment stream for the article.

1. Your Livefyre account needs to be created within Microsoft Learn. Pick a published article, such as [What is the Microsoft identity platform?](https://learn.microsoft.com/azure/active-directory/develop/v2-overview).

1. At the bottom of the article, click the *Sign in* link in the comments section.

1. In the authentication dialog, click *Create New Account*.
  
1. Enter your profile information and click *Sign up*.
   - **Username**: your Microsoft email alias plus @MSFT, ie: *alias@MSFT*
   - **Email**: Your Microsoft.com email address.
  
## Modify articles using the GitHub UI

You might not need to follow all the steps in this article. It depends on the sort of content contribution you want or need to make.

### Submit a text-only change to an existing article

If you only need or want to make textual updates to an existing article, you probably don't need to follow the rest of the steps. You can use GitHub's web-based markdown editor to submit your changes.

1. Click the Edit button on the article page you want to modify.

1. Then, click the edit icon in the GitHub version of the article

   ![GitHub profile example](./media/tools-and-setup/editicon.PNG)

That opens the easy-to-use web editor that makes it easy to submit changes. You don't need to follow the other steps in this article.

### All other changes

The GitHub UI does support creation of new files and dragging and dropping images. However, when you work in the UI, managing branches can be confusing so we typically recommend you install the tools and learn the commands for creating and managing articles. If you want to use the UI, see:

- [Creating files on GitHub](https://github.com/blog/1327-creating-files-on-github)
- [Upload files to your repositories](https://github.com/blog/2105-upload-files-to-your-repositories)

For the following sorts of work, we strongly recommend you install and learn to use the tools:

- Making major changes to an article
- Creating and publishing a new article
- Adding new images or updating images
- Updating an article over a period of days without publishing changes each of those days
- Creating content for a release that has to go out on a certain day at a certain time

## Private or public repo?

The public repository is for the community and for occasional internal Microsoft contributors who are not listed as authors of articles or responsible as owners of articles.

If you are the author of an article and you are a Microsoft employee, you must work in the private repo. Your public repo contributions will be automatically closed by our repo automation. Request permissions to the private repo, per the instructions in the next section.

To easily switch from the public repo version of an article to the private repo version, just add "-pr" the URL, as shown here:

- github.com/Microsoft/bot-framework-docs/blob/master/articles/batch/batch-account-create-portal.md
- github.com/Microsoft/bot-framework-docs**-pr**/blob/master/articles/batch/batch-account-create-portal.md

## Permissions

Anybody with a GitHub account can contribute to Bot Framework technical content through our public repository at [MicrosoftDocs/bot-docs](https://github.com/Microsoft/bot-framework-docs). No special permissions are required.

If you are a Microsoft PM or writer who is working on Bot Framework content as a designated author or reviewer, you must work in our private content repository, bot-framework-docs-pr.

1. Visit [https://repos.opensource.microsoft.com](https://repos.opensource.microsoft.com) to join the Microsoft GitHub organization. Once you are a member of the Microsoft organization, you can access the private bot-framework-docs-pr repository.
1. Register your GitHub account with the publishing system before you submit your first pull request. To do this, visit https://op-portal-prod.azurewebsites.net/, and click "Sign in with GitHub". The one-time sign-in is all that is needed.

## Install Git for Windows

Install Git for Windows from [http://git-scm.com/download/win](http://git-scm.com/download/win). This download installs the Git version control system, and it installs Git Bash, the command-line app that you will use to interact with your local Git repository.

You can accept the default settings; if you want the commands to be available within the Windows command line, select the option that enables it.

![GitHub profile example](./media/tools-and-setup/gitbashinstall.png)

## Enable two-factor authentication

You have to enable two factor authentication (2FA) on your GitHub account if you are working in the private content repository. It's required.

To enable 2FA, follow the instructions in both the following GitHub help topics:

- [About Two-Factor Authentication](https://help.github.com/articles/about-two-factor-authentication/)
- [Creating an access token for command-line use](https://help.github.com/articles/creating-an-access-token-for-command-line-use/)

When you create the token, select all the scopes available in the token-creation UI ([details on each scope](https://developer.github.com/v3/oauth/#scopes))

After you enable 2FA, you have to enter the access token instead of your GitHub password at the command prompt when you try to access a GitHub repository from the command line. The access token is not the authentication code that you get in a text message when you set up 2FA. It's a long string that looks something like this:  fdd3b7d3d4f0d2bb2cd3d58dba54bd6bafcd8dee. A few notes about this:

- When you create your access token, save it in a text file to make it readily accessible when you need it.
- Later, when you need to paste the token, know there are two ways to paste in the command line:
  
  - Click the icon in the upper left corner of the command line window>Edit>Paste.
  - Right-click the icon in the upper left corner of the window and click Properties>Options>QuickEdit Mode. This configures the command line so you can paste by right-clicking in the command line window.

## Install a markdown editor

We author content using simple "markdown" notation in the files, rather than complex "markup" (HTML, XML, etc.). So, you'll need to install a markdown editor.

- **Atom**: Most of us use GitHub's Atom markdown editor: [http://atom.io](http://atom.io). It does not require a license for business use. It has spell check.
- **Notepad**: You can use Notepad for a very lightweight option.
- **Prose**: This is a lightweight, elegant, on-line, and open source markdown editor that offers a preview. Visit [http://prose.io](http://prose.io) and authorize Prose in your repository.
- **[Visual Studio Code](https://www.visualstudio.com/products/code-vs.aspx)** - Microsoft's entry in this space.

## Configure Atom

If you use Atom, you'll need to set a few things up.

- Atom defaults to using 2 spaces for tabs, but Markdown expects 4 spaces. If you leave it at the default of two, your article will look great in local preview, but not when it's imported into Azure. So, configure Atom to use 4 spaces - you can find this setting under File>Settings>Editor Settings>Tab Length.
- You will probably also want to turn on Soft Wrap in this section too, which does the same as "word wrap" in Notepad.
- To turn on the markdown preview, click Packages>Markdown Preview>Toggle Preview. You can use Ctrl-Shift-M to toggle the preview HTML view.

## Fork the repository and copy it to your computer

1. Create a fork of the repository in GitHub - go to the top-right of the page and click the Fork button. If prompted, select your account as the location where the fork should be created. This creates a copy of the repository within your Git Hub account. Generally speaking, technical writers and program managers need to fork bot-framework-docs-pr, the private repo. Community contributors need to fork bot-framework-docs, the public repo. You only need to fork one time; after your first setup, if you want to copy your fork to another computer, you only have to run the commands that follow in this section to copy the repo to your computer.  If you choose to create forks of both repositories, you will need to create a fork for each repository.
1. Copy the Personal Access Token that you got from [https://github.com/settings/tokens](https://github.com/settings/tokens). You can accept the default permissions for the token.  Save the Personal Access Token in a text file for later reuse.
1. Next, copy the repository to your computer with your credentials embedded in the command string.  To do this, open Git Bash and run it as an administrator. At the command prompt, enter the following command.  This command creates a bot-framework-docs(-pr) directory on your computer.  If you're using the default location, it will be at c:\users\\\<your Windows user name>\bot-framework-docs(-pr).

Public repo:

```console
git clone https://your_GitHub_user_name:your_token@github.com/your_GitHub_user_name/bot-framework-docs.git
```

Private repo:

```console
git clone https://your_GitHub_user_name:your_token@github.com/your_GitHub_user_name/bot-framework-docs-pr.git
```

For example, this clone command could look something like this:

```console
git clone https://smithj:b428654321d613773d423ef2f173ddf4a312345@github.com/smithj/bot-framework-docs-pr.git  
```

## Set remote repository connection and configure credentials

Create a reference to the root repository by entering these commands. This sets up connections to the repository in GitHub so that you can get the latest changes onto your local machine and push your changes back to GitHub. This command also configures your token locally so that you don't have to enter your name and password each time you try to access the upstream repo and your fork on GitHub.

Public repo:

```console
cd bot-framework-docs
git remote add upstream https://your_GitHub_user_name:your_token@github.com/Microsoft/bot-framework-docs.git
git fetch upstream
```

Private repo:

```console
cd bot-framework-docs-pr
git remote add upstream https://your_GitHub_user_name:your_token@github.com/Microsoft/bot-framework-docs-pr.git
git fetch upstream
```

This usually takes a while. After you do this, you won't have to fork again or enter your credentials again. You would only have to copy the forks to a local computer again if you set the tools up on another computer.

## Configure your user name and email locally

To ensure you are listed correctly as a contributor, you need to configure your user name and email locally in Git.

1. Start Git Bash, and switch into bot-framework-docs or bot-framework-docs-pr:

   ```console
   cd bot-framework-docs
   ```

   or

   ```console
   cd bot-framework-docs-pr
   ```

1. Configure your user name so it matches your name as you set it up in your GitHub profile:

    ```console
    git config --global user.name "John Doe"
    ```

1. Configure your email so it matches the primary email designated in your GitHub profile; if you're a MSFT employee, it should be your MSFT email address:

    ```console
    git config --global user.email "alias@example.com"
    ```

1. Type `git config -l` and review your local settings to ensure the user name and email in the configuration are correct.

## Next steps

- Understand the type of content that belongs in the technical content repo, and know what does not belong. See the [content channel guidance](content-channel-guidance.md)!
- Follow [these steps to create or modify an article and then submit it for publishing](git-commands-for-master.md).
- Copy [the markdown template](../markdown templates/markdown-template-for-new-articles.md) as the basis for a new article.
- Use [this checklist to verify your pull request will meet the quality criteria](contributor-guide-pr-criteria.md) for merging.

### Contributors' guide navigation

- [Overview article](../README.md)
- [Index of guidance articles](contributor-guide-index.md)

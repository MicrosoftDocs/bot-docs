Open a command prompt to log in to the Azure portal.

```cmd
az login
```

A browser window will open, allowing you to sign in.

### Set the subscription

Set the default subscription to use.

```cmd
az account set --subscription "<azure-subscription>"
```

If you are not sure which subscription to use for deploying the bot, you can view the list of `subscriptions` for your account by using `az account list` command.

Navigate to the bot folder.

```cmd
cd <local-bot-folder>
```
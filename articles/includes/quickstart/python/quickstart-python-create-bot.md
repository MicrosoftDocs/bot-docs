<!-- Include under "Create a bot" header
bot-builder-tutorial-create-basic-bot.md and bot-builder-python-quickstart.md -->

To create your bot, navigate to the directory you want your bot created in, then run:

```cmd
cookiecutter https://github.com/microsoft/BotBuilder-Samples/releases/download/Templates/echo.zip
```

This command copies all needed files from GitHub to create an Echo Bot based on the Python [echo template][echo-template]. You will be prompted for the *name* of the bot and a *description*. Name your bot **echo-bot** and set the description to **A bot that echoes back user response.** as shown below:

![set name and description](~/media/python/quickstart/set-name-description.png)

[echo-template]: https://github.com/microsoft/BotBuilder-Samples/tree/master/generators/python/app/templates/echo

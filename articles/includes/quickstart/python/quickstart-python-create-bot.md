<!-- Include under ## Create a bot H2 header -->

>[!NOTE]
>
> Some developers may find it useful to create Python bots in a [virtual envrionment](https://docs.python.org/3/library/venv.html). The steps below will work regardless if you're developing in a virtual environment or on your local machine.


1. Open a terminal. Install the necessary packages by running the following commands:

```cmd
pip install botbuilder-core`
pip install asyncio`
pip install aiohttp`
pip install cookiecutter==1.7.0`
```

The last package, cookiecutter, will be used to generate your bot. Verify that cookiecutter was installed correctly by running `cookiecutter --help`.

1. To create your bot run:

```cmd
cookiecutter https://github.com/microsoft/botbuilder-python/releases/download/Templates/echo.zip
```

This command creates an Echo Bot based on the Python [echo template](https://github.com/microsoft/BotBuilder-Samples/tree/master/generators/python/app/templates/echo).

1. You will be prompted for the *name* of the bot and a *description*. Name your bot `echo-bot` and set the description to `A bot that echoes back user response.` as shown below:

![set name and description](~/media/python/quickstart/set-name-description.png)

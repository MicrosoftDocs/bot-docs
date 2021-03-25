<!-- Include under "Prerequisites" header
bot-builder-tutorial-create-basic-bot.md and bot-builder-python-quickstart.md -->

- Python [3.6](https://www.python.org/downloads/release/python-369/), [3.7](https://www.python.org/downloads/release/python-375/), or [3.8](https://www.python.org/downloads/release/python-383/)
- [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme)
- Knowledge of asynchronous programming in Python
### Templates

1. Install the necessary packages by running the following commands:

    ```cmd
    pip install botbuilder-core
    pip install asyncio
    pip install aiohttp
    pip install cookiecutter==1.7.0
    ```

The last package, **cookiecutter**, will be used to generate your bot. Verify that it was installed correctly by running `cookiecutter --help`.

1. To create your bot run:

    ```cmd
    cookiecutter https://github.com/microsoft/BotBuilder-Samples/releases/download/Templates/echo.zip
    ```

    This command creates an Echo Bot based on the Python [echo template](https://github.com/microsoft/BotBuilder-Samples/tree/master/generators/python/app/templates/echo).

>[!NOTE]
>
> Some developers may find it useful to create Python bots in a [virtual environment](https://docs.python.org/3/library/venv.html). The steps below will work regardless if you're developing in a virtual environment or on your local machine.

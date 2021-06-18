<!-- Include under "Prerequisites" header
bot-builder-tutorial-create-basic-bot.md and bot-builder-python-quickstart.md -->

- Python [3.6][], [3.7][], or [3.8][]
- [Bot Framework Emulator][Emulator]
- Knowledge of asynchronous programming in Python

>[!TIP]
>
> Some developers may find it useful to create Python bots in a [virtual environment][virtual-environment]. The steps below will work regardless if you're developing in a virtual environment or on your local machine.

### Templates

Install the necessary packages by running the following commands:

```cmd
pip install botbuilder-core
pip install asyncio
pip install aiohttp
pip install cookiecutter==1.7.0
```

The last package, **cookiecutter**, will be used to generate your bot. Verify that it was installed correctly by running `cookiecutter --help`.

[3.6]: https://www.python.org/downloads/release/python-369/
[3.7]: https://www.python.org/downloads/release/python-375/
[3.8]: https://www.python.org/downloads/release/python-383/
[Emulator]: https://github.com/microsoft/BotFramework-Emulator/blob/master/README.md
[virtual-environment]:https://docs.python.org/3/library/venv.html

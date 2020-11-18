### To set breakpoints in Visual Studio Code

In Visual Studio Code, you can set breakpoints and run the bot in debug mode to step through your code. See also [Create a bot with the Bot Framework SDK for Python](~/python/bot-builder-python-quickstart.md).

1. Install the [Python extension](https://aka.ms/vscode-python-extension) in VS Code if you have not already done so. This extension provides rich support for Python in VS Code, including debugging.
1. Launch VS Code and open your bot project folder.
1. Set breakpoints as necessary. You can set breakpoints by hovering your mouse over the column to the left of the line numbers. A small red dot will appear. If you click on the dot, the breakpoint is set. If you click the dot again, the breakpoint is removed.
1. Select the `app.py`.
1. From the menu bar, click **Debug** and click **Start Debugging**.
1. Select **Python File** to debug the currently selected file.

   ![Set Python breakpoints in VS Code](~/media/bot-service-debug-bot/bot-debug-python-breakpoints.png)

1. Start the Bot Framework Emulator and connect to your bot as described in the [Debug with the Bot Framework Emulator](https://docs.microsoft.com/azure/bot-service/bot-service-debug-emulator) article.
1. From the Emulator, send your bot a message (for example, send the message "Hi"). Execution will stop at the line where you place the breakpoint.

   ![Debug Python in VS Code](~/media/bot-service-debug-bot/bot-debug-python-breakpoint-caught.png)

For more information, see [Debug your Python code](https://aka.ms/bot-debug-python).

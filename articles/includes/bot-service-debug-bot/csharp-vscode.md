### Debug a C# bot using breakpoints in Visual Studio Code

In Visual Studio Code, you can set breakpoints and run the bot in debug mode to step through your code. To set breakpoints in VS Code, do the following:

1. Launch VS Code and open your bot project folder.
1. Set breakpoints as necessary. You can set breakpoints by hovering your mouse over the column to the left of the line numbers. A small red dot will appear. If you click on the dot, the breakpoint is set. If you click the dot again, the breakpoint is removed.
1. From the menu bar, click **Run** and then click **Start Debugging**. Your bot will start running in debugging mode from the Terminal in Visual Studio Code.

   ![Set breakpoint in VS Code](~/media/bot-service-debug-bot/csharp-breakpoint-set.png)

1. Start the Bot Framework Emulator and connect to your bot as described in the [Debug with the Bot Framework Emulator](https://docs.microsoft.com/azure/bot-service/bot-service-debug-emulator) article.
1. From the emulator, send your bot a message (for example, send the message "Hi"). Execution will stop at the line where you place the breakpoint.

   ![Debug in VS](~/media/bot-service-debug-bot/breakpoint-caught-vscode.png)
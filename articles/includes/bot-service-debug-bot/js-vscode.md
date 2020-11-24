### To set breakpoints in Visual Studio Code

In Visual Studio Code, you can set breakpoints and run the bot in debug mode to step through your code. To set breakpoints in VS Code, do the following:

1. Launch VS Code and open your bot project folder.
2. From the menu bar, click **Debug** and click **Start Debugging**. If you are prompted to select a runtime engine to run your code, select **Node.js**. At this point, the bot is running locally.
3. Click the **.js** file and set breakpoints as necessary. In VS Code, you can set breakpoints by hovering your mouse over the column to the left of the line numbers. A small red dot will appear. If you click on the dot, the breakpoint is set. If you click the dot again, the breakpoint is removed.

   ![Set JavaScript breakpoint in VS Code](~/media/bot-service-debug-bot/breakpoint-set.png)

4. Start the Bot Framework Emulator and connect to your bot as described in the [Debug with the Bot Framework Emulator](https://docs.microsoft.com/azure/bot-service/bot-service-debug-emulator) article.
5. From the Emulator, send your bot a message (for example, send the message "Hi"). Execution will stop at the line where you place the breakpoint.

   ![Debug JavaScript in VS Code](~/media/bot-service-debug-bot/breakpoint-caught.png)

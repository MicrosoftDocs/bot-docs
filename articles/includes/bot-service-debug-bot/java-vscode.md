### To set breakpoints in Visual Studio Code

In Visual Studio Code, you can set breakpoints and run the bot in debug mode to step through your code. See also [Create a bot with the Bot Framework SDK for Java](~/java/bot-builder-java-quickstart.md).

1. Install the [Java Extension Pack](https://aka.ms/vscode-java-extension-pack) in VS Code if you have not already done so. This extension provides rich support for Java in VS Code, including debugging.
1. Launch VS Code and open your bot project folder.
1. Set breakpoints as necessary. You can set breakpoints by hovering your mouse over the column to the left of the line numbers. A small red dot will appear. If you click on the dot, the breakpoint is set. If you click the dot again, the breakpoint is removed.
1. Select the `EchoBot.java` file and add a breakpoint to a desired location.
1. From the menu bar, click **Run** and click **Start Debugging**.
1. Select **Java** if prompted to debug the currently selected file.

   ![Set Java breakpoints in VS Code](~/media/bot-service-debug-bot/bot-debug-java-breakpoints.png)

1. Start the Bot Framework Emulator and connect to your bot as described in the [Debug with the Bot Framework Emulator](/azure/bot-service/bot-service-debug-emulator) article.
1. From the Emulator, send your bot a message (for example, send the message "Hi"). Execution will stop at the line where you place the breakpoint.

   ![Debug java in VS Code](~/media/bot-service-debug-bot/bot-debug-java-breakpoint-caught.png)

For more information, see [Running and debugging Java](https://code.visualstudio.com/docs/java/java-debugging).

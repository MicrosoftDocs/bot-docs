### To set breakpoints in Visual Studio

In Visual Studio (VS), you can set breakpoints and run the bot in debug mode to step through your code. To set breakpoints in VS, do the following:

1. Navigate to your bot folder and open the **.sln** file. This will open the solution in VS.
2. From the menu bar, click **Build** and click **Build Solution**.
3. In the **Solution Explorer**, click the **.cs** file and set breakpoints as necessary. This file defines your main bot logic. In VS, you can set breakpoints by hovering your mouse over the column to the left of the line numbers. A small red dot will appear. If you click on the dot the breakpoint is set. If you click the dot again the breakpoint is removed.
4. From the menu, click **Debug** and click **Start Debugging**. At this point, the bot is running locally.

   ![Set breakpoint in VS](~/media/bot-service-debug-bot/breakpoint-set-vs.png)

<!--
   > [!NOTE]
   > If you get the "Value cannot be null" error, check to make sure your **Table Storage** setting is valid.
   > The **EchoBot** is default to using **Table Storage**. To use Table Storage in your bot, you need the table *name* and *key*. If you do not have a Table Storage instance ready, you can create one or for testing purposes, you can comment out the code that uses **TableBotDataStore** and uncomment the line of code that uses **InMemoryDataStore**. The **InMemoryDataStore** is intended for testing and prototyping only.
-->

5. Start the Bot Framework Emulator and connect to your bot as described in the section above.
6. From the emulator, send your bot a message (e.g.: send the message "Hi"). Execution will stop at the line where you place the breakpoint.

   ![Debug in VS](~/media/bot-service-debug-bot/breakpoint-caught-vs.png)
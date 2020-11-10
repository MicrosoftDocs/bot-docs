
<!-- Include under "Start your bot" header in the files:
bot-builder-tutorial-create-basic-bot.md and bot-builder-dotnet-sdk-quickstart.md -->

# [Visual Studio](#tab/vs)

In Visual Studio, start the project. This will build the application, deploy it to localhost, and launch the web browser to display the application's `default.htm` page. At this point, your bot is running locally on port 3978.

# [Visual Studio Code](#tab/vc)

### Run with Visual Studio Code

1. Open your bot project folder.
1. On the menu bar, click **Run**.
1. In the drop-down menu, select **Run Without Debugging**.

   ![vsc run](~/media/azure-bot-quickstarts/bot-builder-dotnet-vsc-run.png)

1. Select the **.Net Core** environment.

   ![vsc env](~/media/azure-bot-quickstarts/bot-builder-dotnet-vsc-environment.png)

This will build the application, deploy it to localhost, and launch the web browser to display the application's `default.htm` page. At this point, your bot is running locally on port 3978.

# [Command Line](#tab/cl)

To run your bot locally, execute the commands shown below.

1. Change into the project's folder (for example, EchoBot).

   ```cmd
      cd EchoBot
   ```

1. Run the bot.

   ```cmd
      dotnet run
   ```

This will build the application, deploy it to localhost, and launch the web browser to display the application's `default.htm` page. At this point, your bot is running locally on port 3978.

---
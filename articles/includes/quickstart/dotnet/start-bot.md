#### [Visual Studio](#tab/vs)

In Visual Studio

1. Open your bot project.
1. Run the project without debugging.

This will build the application, deploy it to localhost, and launch the web browser to display the application's `default.htm` page. At this point, your bot is running locally on port 3978.

#### [VS Code](#tab/vscode)

To run your bot from VS Code:

1. Open your bot project folder.

    If you're prompted to select a project, select the one for the bot you just created.

1. Go to **Run**, and then select **Run Without Debugging**.

   > [!div class="mx-imgBorder"]
   > ![vsc run](../../../media/azure-bot-quickstarts/bot-builder-dotnet-vsc-run.png)

   - Select the **.Net Core** environment.

   > [!div class="mx-imgBorder"]
   > ![vsc env](../../../media/azure-bot-quickstarts/bot-builder-dotnet-vsc-environment.png)

   - If this command updated your launch settings, save the changes and rerun the command.

This will build the application, deploy it to localhost, and launch the web browser to display the application's `default.htm` page. At this point, your bot is running locally on port 3978.

#### [CLI](#tab/cli)

To run your bot locally in a command prompt or terminal:

1. Change directories to the project folder for your bot.
1. Use `dotnet run` to start the bot.

   ```console
   dotnet run
   ```

This will build the application and deploy it to localhost. The application's default web page will not display, but at this point, your bot is running locally on port 3978.

---

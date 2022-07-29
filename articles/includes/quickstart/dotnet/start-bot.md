---
description: Procedure for getting C# bot templates by various means, part of the quickstart to create a basic bot.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 07/28/2022
---

#### [Visual Studio](#tab/vs)

In Visual Studio:

1. Open your bot project.
1. Run the project without debugging.
1. Visual Studio builds the application, deploys it to localhost, and launches the web browser to display the application's `default.htm` page.

At this point, your bot is running locally on port 3978.

#### [VS Code](#tab/vscode)

In Visual Studio Code:

1. Open your bot project folder.

    If you're prompted to select a project, select the one for the bot you created.

1. From the menu, select **Run**, and then **Run Without Debugging**.

    - If prompted to select an environment, select **.Net Core**.
    - If this command updated your launch settings, save the changes and rerun the command.

    The run command builds the application, deploys it to localhost, and launches the web browser to display the application's `default.htm` page.

At this point, your bot is running locally on port 3978.

#### [CLI](#tab/cli)

From a command prompt or terminal:

1. Change directories to the project folder for your bot.
1. Use `dotnet run` to start the bot.

    ```console
    dotnet run
    ```

1. This command builds the application and deploys it to localhost.

The application's default web page won't display, but at this point, your bot is running locally on port 3978.

---

---
description: Procedure for getting C# bot templates by various means, part of the quickstart to create a basic bot.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 10/10/2022
---

#### [Visual Studio](#tab/vs)

In Visual Studio, create a new bot project and use the **Echo Bot (Bot Framework v4 - .NET Core 3.1)** template. To see only bot templates, choose the **AI Bots** project type.

#### [VS Code](#tab/vscode)

Make sure that [.NET Core 3.1](https://dotnet.microsoft.com/download) is installed.

1. In Visual Studio Code, open a new terminal window.
1. Go to the directory in which you want to create your bot project.
1. Create a new echo bot project using the following command. Replace `<your-bot-name>` with the name to use for your bot project.

   ```console
   dotnet new echobot -n <your-bot-name>
   ```

#### [CLI](#tab/cli)

1. Open a new terminal window.
1. Go to the directory in which you want to create your bot project.
1. Create a new echo bot project using the following command. Replace `<your-bot-name>` with the name to use for your bot project.

   ```console
   dotnet new echobot -n <your-bot-name>
   ```

---

Thanks to the template, your project contains all the necessary code to create the bot in this quickstart. You don't need any more code to test your bot.

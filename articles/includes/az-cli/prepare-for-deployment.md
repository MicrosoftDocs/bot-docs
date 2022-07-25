---
description: Describes how to prepare a bot project for deployment. Assumes that the bot has already been provisioned in Azure.
author: JonathanFingold
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: include
ms.date: 03/03/2022
---

Prepare your project files before you deploy your bot.

### [C#](#tab/csharp)

1. Switch to your project's root folder. For C#, the root is the folder that contains the .csproj file.
1. Do a clean rebuild in _release mode_.
1. If you haven't done so before, run `az bot prepare-deploy` to add required files to the root of your local source code directory.
    This command generates a `.deployment` file in your bot project folder.

    ```azurecli
    az bot prepare-deploy --lang Csharp --code-dir "." --proj-file-path "<my-cs-proj>"
    ```

    | Option         | Description                                                                                                                 |
    |:---------------|:----------------------------------------------------------------------------------------------------------------------------|
    | lang           | The language or runtime of the bot. Use `Csharp`.                                                                           |
    | code-dir       | The directory to place the generated deployment files in. Use your project's root folder. Default is the current directory. |
    | proj-file-path | The path to the .csproj file for your bot, relative to the `code-dir` option.                                               |

1. Within your project's root folder, create a zip file that contains all files and subfolders.

### [JavaScript](#tab/javascript)

1. Switch to your project's root folder.
    - For _JavaScript_, the root is the folder that contains the app.js or index.js file.
    - For _TypeScript_, the root is the folder that contains the _src_ folder (where the bot.ts and index.ts files are).
1. Run `npm install`.
1. If you haven't done so before, run `az bot prepare-deploy` to add required files to the root of your local source code directory.
    This command generates a `web.config` file in your project folder.
    Azure App Services requires each Node.js bot to include a web.config file in its project root folder.

    ```azurecli
    az bot prepare-deploy --lang <language> --code-dir "."
    ```

    | Option   | Description                                                                                                                 |
    |:---------|:----------------------------------------------------------------------------------------------------------------------------|
    | lang     | The language or runtime of the bot. Use `Javascript` or `Typescript`.                                                       |
    | code-dir | The directory to place the generated deployment files in. Use your project's root folder. Default is the current directory. |

1. Within your project's root folder, create a zip file that contains all files and subfolders.

### [Java](#tab/java)

1. Switch to your project's root folder.
1. In the project directory, run the following command from the command line:

    ```console
    mvn clean package
    ````

### [Python](#tab/python)

> [!TIP]
> For Python bots, dependency installation is performed on the server.
> The ARM templates require your dependencies to be listed in a `requirements.txt` file.

1. If you're using a dependency and package manager:
    1. Convert your dependencies list to a `requirements.txt` file
    1. Add `requirements.txt` to the folder that contains `app.py`.
1. Within your project's root folder, create a zip file that contains all files and subfolders.

---

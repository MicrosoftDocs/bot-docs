#### [Visual Studio](#tab/vs)

In Visual Studio, create a new bot project using the **Echo Bot (Bot Framework v4 - .NET Core 3.1)** template. To see only bot templates, choose **AI Bots** from the project types.

:::image type="content" source="../../../media/azure-bot-quickstarts/bot-builder-dotnet-project-vs2019.png" alt-text="Visual Studio create a new project dialog":::

Thanks to the template, your project contains all the necessary code to create the bot in this quickstart. You don't need any additional code to test your bot.

#### [VS Code](#tab/vscode)

Make sure that [.NET Core 3.1](https://dotnet.microsoft.com/download) is installed.

1. In Visual Studio Code, open a new terminal window.
1. Navigate to the directory in which you want to create your bot project.
1. Create a new echo bot project using the following command. Replace `<your-bot-name>` with the name to use for your bot project.

   ```console
   dotnet new echobot -n <your-bot-name>
   ```

#### [CLI](#tab/cli)

1. Open a new terminal window.
1. Navigate to the directory in which you want to create your bot project.
1. Create a new echo bot project using the following command. Replace `<your-bot-name>` with the name to use for your bot project.

   ```console
   dotnet new echobot -n <your-bot-name>
   ```

---

> [!TIP]
> If you create a _Core_ bot:
>
> - Only the solution directory receives the bot name.
> - You'll need a LUIS language model. You can create a language model at [luis.ai](https://www.luis.ai). After creating the model, update the configuration file.

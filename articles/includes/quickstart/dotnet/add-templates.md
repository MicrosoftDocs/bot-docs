#### [Visual Studio](#tab/vs)

- [Visual Studio 2019 or later](https://www.visualstudio.com/downloads)
- [Bot Framework v4 SDK Templates for Visual Studio](https://marketplace.visualstudio.com/items?itemName=BotBuilder.botbuilderv4)

To add the bot templates to Visual Studio, download and install the [Bot Framework v4 SDK Templates for Visual Studio](https://marketplace.visualstudio.com/items?itemName=BotBuilder.botbuilderv4) VSIX file.

[!INCLUDE [.NET VSIX templates info](../../vsix-templates-versions.md)]

#### [VS Code / CLI](#tab/vscode+cli)

.NET Core Templates will help you to quickly build new conversational AI bots using Bot Framework v4. As of May 2020, these templates and the code they generate require .NET Core 3.1.

To install the Bot Framework templates:

1. Open a console window.

1. Download and install [.NET Core SDK download](https://dotnet.microsoft.com/download) version 3.1 or later.
1. You can use this command to determine which version of the .NET Core command-line interface you have installed.

   ```console
   dotnet --version
   ```

1. Install the three Bot Framework C# templates: the echo, core, and empty bot templates.

   ```console
   dotnet new -i Microsoft.Bot.Framework.CSharp.EchoBot
   dotnet new -i Microsoft.Bot.Framework.CSharp.CoreBot
   dotnet new -i Microsoft.Bot.Framework.CSharp.EmptyBot
   ```

1. Verify the templates have been installed correctly.

   ```console
   dotnet new --list
   ```

> [!NOTE]
> The above installation steps will install all three Bot Framework templates. You don't need to install all the templates and can install just the ones you will use. This article makes use of the _echo bot_ template.

---
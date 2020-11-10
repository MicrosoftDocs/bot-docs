<!-- Include under "Prerequisites" header in the files:
bot-builder-tutorial-create-basic-bot.md and bot-builder-dotnet-sdk-quickstart.md -->

- [.NET Core 3.1](https://dotnet.microsoft.com/download)
- [Bot Framework Emulator](https://aka.ms/bot-framework-emulator-readme)
- Knowledge of [ASP.Net Core](https://docs.microsoft.com/aspnet/core/) and [asynchronous programming in C#](https://docs.microsoft.com/dotnet/csharp/programming-guide/concepts/async/index)

### Templates

# [Visual Studio](#tab/vs)

- [Visual Studio 2019 or later](https://www.visualstudio.com/downloads)
- [Bot Framework SDK v4 template for C#](https://aka.ms/bot-vsix)

To add the bot templates to Visual Studio, download and install the [Bot Framework v4 SDK Templates for Visual Studio](https://aka.ms/bot-vsix) VSIX file.

[!INCLUDE [dotnet vsix templates info](~/includes/vsix-templates-versions.md)]

# [Visual Studio Code / Command Line](#tab/vc+cl)

.NET Core Templates will help you to quickly build new conversational AI bots using Bot Framework v4. As of May 2020, these templates and the code they generate require .NET Core 3.1.

To install the Bot Framework templates:

1. Open a console window.

1. Install [.NET Core SDK](https://dotnet.microsoft.com/download) version 3.1 or higher.
1. You can use this command to determine which version of the .NET Core command-line interface you have installed.

   ```cmd
   dotnet --version
   ```

1. Install the 3 Bot Framework C# templates: the echo, core, and empty bot templates.

   ```cmd
   dotnet new -i Microsoft.Bot.Framework.CSharp.EchoBot
   dotnet new -i Microsoft.Bot.Framework.CSharp.CoreBot
   dotnet new -i Microsoft.Bot.Framework.CSharp.EmptyBot
   ```

1. Verify the templates have been installed correctly.

   ```cmd
   dotnet new --list
   ```

> [!NOTE]
> The above installation steps will install all 3 Bot Framework templates. You do not need to install all 3 templates and can install just the ones you will use. This article makes use of the echo bot template.

---
> [!NOTE]
> Both .NET Core 2.1 and .NET Core 3.1 versions of the C# templates are available.
> When creating new bots in Visual Studio 2019, you should use the .NET Core 3.1 templates.
> The current bot samples use .NET Core 3.1 templates. You can find the samples that use .NET Core 2.1 templates in the [4.7-archive](https://github.com/microsoft/BotBuilder-Samples/tree/4.7-archive/samples/csharp_dotnetcore) branch of the BotBuilder-Samples repository.
> For information about deploying .NET Core 3.1 bots to Azure, see [Deploy your bot](~/bot-builder-deploy-az-cli.md).

### About templates

Templates let you quickly create bots using core capabilities. The following table is a quick overview of the kind of templates provided. For more information, see [.NET Core SDK Templates](https://github.com/microsoft/BotBuilder-Samples/tree/master/generators/dotnet-templates#net-core-sdk-templates)

> [!div class="mx-tdBreakAll"]
> |Templates|Description|When to use it|
> |----------|----------|---------|
> |Echo Bot|This template handles the very basics of sending messages to a bot. It produces a bot that simply "echoes" back to the user anything the user says to the bot.| You want a working bot with minimal features.|
> |Core Bot|The most advanced template that provides 6 core features every bot is likely to have. This template covers the core features of a Conversational-AI bot using LUIS.|You understand some of the core concepts of Bot Framework v4 and are beyond the concepts introduced in the Echo Bot template. You're familiar with or concepts such as language understanding using LUIS, managing multi-turn conversations with Dialogs, handling user initiated Dialog interruptions, and using Adaptive Cards to welcome your users.|
> |Empty Bot|A good template if you are familiar with Bot Framework v4, and simple want a basic skeleton project. Also a good option if you want to take sample code from the documentation and paste it into a minimal bot in order to learn.|You are a seasoned Bot Framework v4 developer. You've built bots before, and want the minimum skeleton of a bot to help you get started.|
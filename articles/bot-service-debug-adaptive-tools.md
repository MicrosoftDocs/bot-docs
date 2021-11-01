---
title: Debug bots using Bot Framework Adaptive Tools - Bot Service
description: Learn how to debug bots using the Bot Framework Adaptive Tools Microsoft VS Code extension.
author: emgrol
ms.author: iawilt
manager: shellyha
ms.reviewer: micchow
ms.topic: how-to
ms.service: bot-service
ms.date: 11/01/2021
---

# Debug with Adaptive Tools

[Bot Framework Adaptive Tools](https://github.com/microsoft/BotBuilder-Samples/tree/main/experimental/adaptive-tool) is a VS Code extension that helps developers handle .lg, .lu, and dialog (.dialog) files efficiently.

Adaptive Tools has a variety of tools and settings that make it easy to debug, analyze and enhance you language files. Features like syntax highlighting, diagnostic checks, and debugging let developers troubleshoot language files, while autocompletion and suggestion enhance and simplify the process of bot asset creation.

## Prerequisites

- Install [Visual Studio Code](https://code.visualstudio.com/download).
- Install the [Bot Framework Emulator](https://github.com/Microsoft/BotFramework-Emulator/blob/master/README.md).

- A bot with one or more of the following file types:
  - [.lg](file-format/bot-builder-lg-file-format.md)
  - [.lu](file-format/bot-builder-lu-file-format.md)
  - dialog (.dialog). You can find examples of .dialog files in the [AdaptiveBot](https://github.com/microsoft/BotBuilder-Samples/tree/main/samples/csharp_dotnetcore/adaptive-dialog/21.AdaptiveBot-declarative) sample.

## About Adaptive Tools

The [Adaptive Tools README](https://github.com/microsoft/BotBuilder-Samples/tree/main/experimental/adaptive-tool-readme) has all the information you need to install and use Adaptive Tools.

The `README` includes details about:

- How to [install](https://github.com/microsoft/BotBuilder-Samples/tree/main/experimental/adaptive-tool-readme#getting-started) Adaptive Tools.
- [Language features](https://github.com/microsoft/BotBuilder-Samples/tree/main/experimental/adaptive-tool-readme#language-features), like syntax highlighting and autocompletion.
- [Hover, suggestions, and template navigation](https://github.com/microsoft/BotBuilder-Samples/tree/main/experimental/adaptive-tool-#hover-suggestions-and-navigation) that make it easy to navigate and enhance your files.
- [Debugging](https://github.com/microsoft/BotBuilder-Samples/tree/main/experimental/adaptive-tool-readme#debugging) using the Emulator.
- [Custom settings](https://github.com/microsoft/BotBuilder-Samples/tree/main/experimental/adaptive-tool-readme#adaptive-tool-settings) in VS Code.

## Additional Information

- [.lg file format](file-format/bot-builder-lg-file-format.md)
- [.lu file format](file-format/bot-builder-lu-file-format.md)
- See the [troubleshooting index](bot-service-troubleshoot-index.md) information about all troubleshooting topics.

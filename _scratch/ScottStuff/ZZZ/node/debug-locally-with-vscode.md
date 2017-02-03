
layout: page
title: Debug Locally with VSCode
permalink: /en-us/node/builder/guides/debug-locally-with-vscode/
weight: 2612
parent1: Bot Builder for Node.js
parent2: Guides


* TOC
{:toc}

## Overview
If you’re building a bot for the Bot Connector Service on a Windows machine you can use the awesome [Bot Framework Emulator](/en-us/tools/bot-framework-emulator/){:target="_blank"} to debug your bot. Unfortunately, the emulator is currently Windows only so for Mac and Linux users you’ll need to explore other options. One option is to install [VSCode](https://code.visualstudio.com/) and use Bot Builders [TextBot](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.textbot.html){:target="_blank"} class to debug your bot running in a console window. This guide will walk you through doing just that.

## Launch VSCode
For purposes of this walkthrough we’ll use Bot Builders [TodoBot](https://github.com/Microsoft/BotBuilder/tree/master/Node/examples/todoBot) example. After you install VSCode on your machine you should open your bots project using “open folder”.

![Step 1: Launch VSCode](/en-us/images/builder/builder-debug-step1.png)

## Launch Bot
The TodoBot illustrates running a bot on multiple platforms which is the key to being able to debug your bot locally. To debug locally you need a version of your bot that can run from a console window using the [TextBot](/en-us/node/builder/chat-reference/classes/_botbuilder_d_.textbot.html){:target="_blank"} class. For the TodoBot we can run it locally by launching the textBot.js class. To properly debug this class using VScode we’ll want to launch node with the \-\-debug-brk flag which causes it to immediately break. So from a console window type “node \-\-debug-brk textBot.js”.

![Step 2: Launch Bot](/en-us/images/builder/builder-debug-step2.png)

## Configure VSCode
Before you can debug your now paused bot you’ll need to configure the VSCode node debugger. VSCode knows you project is using node but there are a lot of possible configurations for how you launch node so it wants you to go through a one-time setup for each project (aka folder.)  To setup the debugger select the debug tab on the lower left and press the green run button. VSCode will ask you to pick your debug environment and you can just select “node.js”. The default settings are fine for our purposes so no need to adjust anything. You should see a .vscode folder added to your project and if you don't want this checked into your Git repository you can add a '/**/.vscode' entry to your .gitignore file.

![Step 3: Configure VSCode](/en-us/images/builder/builder-debug-step3.png)

## Attach Debugger
Configuring the debugger resulted in two debug modes being added, Launch & Attach. Since our bot is paused in a console window we'll want to select the “Attach” mode and press the green run button again.

![Step 4: Attach Debugger](/en-us/images/builder/builder-debug-step4.png)

## Debug Bot
VSCode will attach to your bot paused on the first line of code and now you’re ready to set break points and debug your bot! Your bot can be communicated with from the console window so switch back to the console window your bot is running in and say “hello”.

![Step 5: Debug Bot](/en-us/images/builder/builder-debug-step5.png)


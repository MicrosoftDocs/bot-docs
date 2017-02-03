---
layout: page
title: Debugging 
permalink: /en-us/node/builder/debugging/
weight: 1500
parent1: Building your Bot Using the Bot Builder for Node.js
---

You should be able to accomplish most of your debugging using [Bot Framework Emulator](/en-us/emulator/). However, if the emulator doesn't meet your needs, this topic provides some debugging alternatives.

### Debugging locally using ngrok

If you're running on a mac and can't use the emulator, or you just want to debug an issue you're seeing when deployed, you can easily configure your bot to run locally using [ngrok](https://ngrok.com/){:target="_blank"}. First install ngrok and then from a console window type:

```
ngrok http 3978
```

This will configure an ngrok forwarding link that forwards requests to your bot running on port 3978. You'll then need to change your registered endpoint to the forwarding link in the Bot Framework developer portal (click **My bots**, click the bot to configure, click **Edit** on the Details card, change **Messaging endpoint**). The endpoint should look something 
like `https://0d6c4024.ngrok.io/api/messages` (don't forget to include the `/api/messages` at the end of the link).

You're now ready to start your bot in debug mode. If you're using [Visual Studio Code](https://code.visualstudio.com){:target="_blank"} you'll need to configure your launch `env` with your bot's App ID and Password. Configure the `.vscode/launch.json` file, press run, and you're ready to debug!

{% highlight JavaScript %}
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": "Launch",
            "type": "node",
            "request": "launch",
            "program": "${workspaceRoot}/app.js",
            "stopOnEntry": false,
            "args": [],
            "cwd": "${workspaceRoot}",
            "preLaunchTask": null,
            "runtimeExecutable": null,
            "runtimeArgs": [
                "--nolazy"
            ],
            "env": {
                "NODE_ENV": "development",
                "MICROSOFT_APP_ID": "Your bots App ID",
                "MICROSOFT_APP_PASSWORD": "Your bots Password"
            },
            "externalConsole": false,
            "sourceMaps": false,
            "outDir": null
        },   
{% endhighlight %}




You need to prepare your project files before you can deploy your C#, Javascript, or Typescript bot. See the Python information if you need to prepare the project files before deploying your Python bot.

<!-- **C# bots** -->
##### [C#](#tab/csharp)

```cmd
az bot prepare-deploy --lang Csharp --code-dir "." --proj-file-path "MyBot.csproj"
```

You must provide the path to the .csproj file relative to --code-dir. This can be performed via the --proj-file-path argument. The command would resolve --code-dir and --proj-file-path to "./MyBot.csproj".

This command generates a `.deployment` file in your bot project folder.

<!-- **JavaScript bots** -->
##### [JavaScript](#tab/javascript)

```cmd
az bot prepare-deploy --code-dir "." --lang Javascript
```

The command generates two `web.config` file in your project folder. Node.js apps need web.config to work with IIS on Azure App Services. Make sure web.config is saved to the root of your bot.

<!-- **TypeScript bots** -->
##### [TypeScript](#tab/typescript)

```cmd
az bot prepare-deploy --code-dir "." --lang Typescript
```

This command generates a `web.config` file in your project folder.

<!-- **Python bots** -->
##### [Python](#tab/python)

If you're using a dependency and package manager you need to convert your dependencies list to a `requirements.txt` file and add it to the folder that contains `app.py`. This is necessary because dependency installation happens on the server side for Python bots, not locally like it does for bots in other languages. The files in `deploymentTemplates` look for `requirements.txt`, and without the file your dependencies won't be installed.

---
